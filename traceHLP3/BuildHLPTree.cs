using System;
using System.Collections.Generic;
using System.Text;
using ee1Dec;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace traceHLP3 {
    public class BuildHLPTree {
        public SortedDictionary<uint, HLP> dictHLP = new SortedDictionary<uint, HLP>();
        public SortedDictionary<uint, object> dictUnkJTo = new SortedDictionary<uint, object>();

        uint pc0 = 0x001002b0; // min addr for program counter
        uint pc1 = 0x00325bd0; // max addr for program counter
        uint pc1tbl = 0x00100000 + 2968056; // max addr for static table

        // String fpdump = @"H:\Proj\khkh_xldM\MEMO\kh2fmdump\dump.raw";

        public BuildHLPTree() {
        }

        public void Build(String fpdump) {
            this.dictUnkJTo.Clear();
            this.dictHLP.Clear();

            using (FileStream fs = File.OpenRead(fpdump)) {
                fs.Position = pc0;
                BinaryReader br = new BinaryReader(fs);
                Queue<ChkHLP> hlpstart = new Queue<ChkHLP>();
                SortedDictionary<uint, object> dictHLPAddr = new SortedDictionary<uint, object>();
                hlpstart.Enqueue(new ChkHLP(0x100000, 0));
                for (uint x = pc0; x <= pc1; x += 4) {
                    Debug.Assert(fs.Position == x);
                    uint code = br.ReadUInt32();
                    if (0x27bd0000 == (code & 0xffff0000)) {
                        // addiu
                        int imm = (short)(code);
                        if (imm < 0) {
                            hlpstart.Enqueue(new ChkHLP(x, -imm));
                            dictHLPAddr[x] = null;
                        }
                    }
                }

                foreach (ChkHLP chk in hlpstart) {
                    Stack<NextPC> nextpc = new Stack<NextPC>();
                    nextpc.Push(new NextPC(chk.pc, 0));
                    HLP hlp = new HLP(chk.pc, chk.spadd);
                    SortedDictionary<uint, object> dictWalked = hlp.dictWalked;
                    uint[] alja;
                    Queue<uint> alnewpc = new Queue<uint>();
                    while (nextpc.Count != 0) {
                        NextPC npc = nextpc.Pop();
                        uint pc = npc.pc;
                        int jalcnt = npc.jalcnt;
                        uint lastpc = uint.MaxValue;
                        while (pc < lastpc) {
                            if (dictWalked.ContainsKey(pc)) break;
                            //Debug.Assert(pc != 0x00101e94); // TICK
                            dictWalked[pc] = null;
                            fs.Position = pc;
                            uint code = br.ReadUInt32();
                            EEis eis = EEDisarm.parse(code, (uint)pc);
                            String a = pc.ToString("x8");
                            String[] al = eis.al;
                            bool isjal = false;
                            alnewpc.Clear();
                            if (al[0].Equals("JR")) {
                                if (al[1].Equals("ra")) {
                                    if (jalcnt == 0)
                                        hlp.exitpc.Add(pc);
                                }
                                else if (LookupJRAddr(fs, br, pc, out alja)) {
                                    foreach (uint jpc in alja)
                                        alnewpc.Enqueue(jpc);
                                }
                                else {
                                    dictUnkJTo[pc] = null;
                                }
                                lastpc = pc + 8;
                            }
                            else if (UtTestopTy.IsGotoAlways(eis)) {
                                if (eis.al[1].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[1].Substring(1), 16));
                                }
                                else if (eis.al.Length >= 3 && eis.al[2].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[2].Substring(1), 16));
                                }
                                else if (eis.al.Length >= 4 && eis.al[3].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[3].Substring(1), 16));
                                }
                                else throw new InvalidDataException(eis.ToString());
                                lastpc = pc + 8;
                            }
                            else if (UtTestopTy.IsGotoSometimes(al[0]) || (isjal = UtTestopTy.IsGosub(al[0]))) {
                                if (eis.al[1].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[1].Substring(1), 16));
                                }
                                else if (eis.al.Length >= 3 && eis.al[2].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[2].Substring(1), 16));
                                }
                                else if (eis.al.Length >= 4 && eis.al[3].StartsWith("$")) {
                                    alnewpc.Enqueue(Convert.ToUInt32(eis.al[3].Substring(1), 16));
                                }
                                else {
                                    dictUnkJTo[pc] = null;
                                    hlp.cntUnkJAddr++;
                                }
                            }
                            else {

                            }

                            foreach (uint newpc in alnewpc) {
                                //Debug.Assert(newpc != 0x002efcc0); // S_EXPA
                                if (pc0 <= newpc && newpc <= pc1) {
                                    if (dictHLPAddr.ContainsKey(newpc)) {
                                        // Jで行っても，JALで行っても，とりあえずGOSUB扱いにする。
                                        hlp.gosubpcs[newpc] = null;
                                    }
                                    else {
                                        nextpc.Push(new NextPC(newpc, isjal ? jalcnt + 1 : jalcnt));
                                    }
                                }
                                else {
                                    hlp.cntOutOfJAddr++;
                                }
                            }
                            pc += 4;
                        }
                    }
                    //Debug.WriteLine("# " + hlp);
                    dictHLP[chk.pc] = hlp;
                }

                using (StreamWriter wr = new StreamWriter("hlp.pro", false, Encoding.ASCII)) {
                    wr.WriteLine("/* kh2fm hlp tree */");
                    foreach (KeyValuePair<uint, HLP> kv in dictHLP) {
                        String name = null;
                        switch (kv.Key) {
                            case 0x00128b38: name = "S_CALCF"; break;
                            case 0x00129c38: name = "S_CALCB"; break;
                            case 0x001adf00: name = "S_IEXPA"; break;
                            case 0x002efcc0: name = "S_EXPA"; break;
                            case 0x00101d68: name = "S_GTICK"; break;
                        }
                        if (name != null) {
                            //wr.WriteLine("fname(f{0:x7}, {1}).", kv.Key, name);
                        }

                        foreach (int newaddr in kv.Value.gosubpcs.Keys) {
                            wr.WriteLine("fcall(f{0:x7}, f{1:x7}).", kv.Key, newaddr);
                        }
                    }
                }
                using (StreamWriter wr = new StreamWriter("hlp.dot", false, Encoding.ASCII)) {
                    wr.WriteLine("digraph kh2 {");
                    foreach (KeyValuePair<uint, HLP> kv in dictHLP) {
                        String name = null;
                        switch (kv.Key) {
                            case 0x00128b38: name = "S_CALCF"; break;
                            case 0x00129c38: name = "S_CALCB"; break;
                            case 0x001adf00: name = "S_IEXPA"; break;
                            case 0x002efcc0: name = "S_EXPA"; break;
                            case 0x00101d68: name = "S_GTICK"; break;
                        }
                        if (name != null) {
                            wr.WriteLine(" f{0:x7} [label = \"{1}\"];", kv.Key, name);
                        }

                        foreach (int newaddr in kv.Value.gosubpcs.Keys) {
                            wr.WriteLine(" f{0:x7} -> f{1:x7};", kv.Key, newaddr);
                        }
                    }
                    wr.WriteLine("}");
                }

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Indent = true;
                xws.IndentChars = " ";
                using (XmlWriter wr = XmlTextWriter.Create("hlp.xml", xws)) {
                    wr.WriteStartDocument();
                    wr.WriteStartElement("a");
                    foreach (KeyValuePair<uint, HLP> kv in dictHLP) {
                        wr.WriteStartElement("hlp");
                        wr.WriteAttributeString("entrypc", kv.Value.entrypc.ToString("x7"));
                        wr.WriteAttributeString("spadd", kv.Value.spadd.ToString());
                        wr.WriteAttributeString("cnt-out-of-j-addr", kv.Value.cntOutOfJAddr.ToString());
                        wr.WriteAttributeString("cnt-unk-j-addr", kv.Value.cntUnkJAddr.ToString());
                        {
                            StringWriter w = new StringWriter();
                            int i = 0;
                            foreach (uint a in kv.Value.gosubpcs.Keys) {
                                if (0 == (i % 10)) w.WriteLine();
                                w.Write("{0:x7} ", a);
                                i++;
                            }
                            wr.WriteStartElement("gosubpcs");
                            wr.WriteString(w.ToString());
                            wr.WriteEndElement();
                        }
                        {
                            StringWriter w = new StringWriter();
                            int i = 0;
                            foreach (uint a in kv.Value.dictWalked.Keys) {
                                if (0 == (i % 10)) w.WriteLine();
                                w.Write("{0:x7} ", a);
                                i++;
                            }
                            wr.WriteStartElement("walked");
                            wr.WriteString(w.ToString());
                            wr.WriteEndElement();
                        }
                        wr.WriteEndElement();
                    }
                    wr.WriteEndElement();
                    wr.WriteEndDocument();
                    wr.Close();
                }

                using (FileStream fso = File.Create("dictHLP.bin")) {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fso, dictHLP);
                    fs.Close();
                }
#if false
                using (SQLiteConnection db = new SQLiteConnection("Data Source=HLP.db")) {
                    db.Open();
                    new SQLiteCommand("BEGIN EXCLUSIVE;");
                    new SQLiteCommand("DROP TABLE IF EXISTS WalkedPC; CREATE TABLE WalkedPC (HLP text, PC text);", db).ExecuteNonQuery();
                    new SQLiteCommand("DROP TABLE IF EXISTS Gosub; CREATE TABLE Gosub (HLP text, GosubHLP text);", db).ExecuteNonQuery();

                    foreach (KeyValuePair<uint, HLP> kv in dictHLP) {
                        foreach (uint pc in kv.Value.dictWalked.Keys) {
                            new SQLiteCommand("INSERT INTO WalkedPC VALUES ('" + kv.Key.ToString("x8") + "','" + pc.ToString("x8") + "');", db).ExecuteNonQuery();
                        }
                        foreach (uint pc in kv.Value.gosubpcs.Keys) {
                            new SQLiteCommand("INSERT INTO Gosub VALUES ('" + kv.Key.ToString("x8") + "','" + pc.ToString("x8") + "');", db).ExecuteNonQuery();
                        }
                    }
                    new SQLiteCommand("COMMIT;", db).ExecuteNonQuery();
                    db.Close();
                }
#endif

                Console.Write("");
            }
        }

        bool LookupJRAddr(Stream fs, BinaryReader br, uint pc, out uint[] aladdr) {
            aladdr = new uint[0];

            {
                Match M = null;

                {
                    String asm = UtLUp.GetAsmLines(fs, br, pc - 4 * 8, 9);
                    // 0x0010955c
                    Match temp = UtLUp.ExecRegex(asm, "^"
                        + "SLTIU t7 t3 \\$(?<cnt>[0-9a-f]{4})" + "\\s+"
                        + "BEQ t7 zero \\$00109578" + "\\s+"
                        + "MOVN a3 t2 t6" + "\\s+"
                        + "LUI t6 \\$(?<hi>[0-9a-f]{4})" + "\\s+"
                        + "SLL t7 t3 2" + "\\s+"
                        + "ADDIU t6 t6 \\$(?<lo>[0-9a-f]{4})" + "\\s+"
                        + "ADDU t7 t7 t6" + "\\s+"
                        + "LW t5 \\$0000\\(t7\\)" + "\\s+"
                        + "JR t5" + "\\s+"
                        );
                    if (temp != null) {
                        M = temp;
                    }
                }

                if (M != null) {
                    int cnt = Convert.ToInt32(M.Groups["cnt"].Value, 16);
                    ushort lo = Convert.ToUInt16(M.Groups["lo"].Value, 16);
                    ushort hi = Convert.ToUInt16(M.Groups["hi"].Value, 16);
                    int tbl = (hi << 16) + (short)lo;
                    Debug.Assert(pc0 <= tbl && tbl < pc1tbl);
                    fs.Position = tbl;

                    aladdr = new uint[cnt];
                    for (int t = 0; t < cnt; t++) aladdr[t] = br.ReadUInt32();

                    return true;
                }
            }
            {
                String asm = UtLUp.GetAsmLines(fs, br, pc - 4 * 7, 8);
                // 0x00100544
                Match M = Regex.Match(asm, "^"
                    + "SLTIU (?<t7>\\w+) (?<t6>\\w+) \\$(?<cnt>[0-9a-f]{4})" + "\\s+"
                    + "BEQ (?<t7>\\w+) zero \\$[0-9a-f]{8}" + "\\s+"
                    + "SLL (?<t7>\\w+) (?<t6>\\w+) 2" + "\\s+"
                    + "LUI (?<t6>\\w+) \\$(?<hi>[0-9a-f]{4})" + "\\s+"
                    + "ADDIU (?<t6>\\w+) (?<t6>\\w+) \\$(?<lo>[0-9a-f]{4})" + "\\s+"
                    + "ADDU (?<t7>\\w+) (?<t7>\\w+) (?<t6>\\w+)" + "\\s+"
                    + "LW (?<t5>\\w+) \\$0000\\((?<t7>\\w+)\\)" + "\\s+"
                    + "JR (?<t5>\\w+)"
                    );
                if (M.Success) {
                    int x, cx;
                    String s;
                    s = "t7"; for (x = 2, cx = M.Groups[s].Captures.Count; x < cx && M.Groups[s].Captures[1].Value == M.Groups[s].Captures[x].Value; x++) { }
                    if (x == cx) {
                        s = "t6"; for (x = 2, cx = M.Groups[s].Captures.Count; x < cx && M.Groups[s].Captures[1].Value == M.Groups[s].Captures[x].Value; x++) { }
                        if (x == cx) {
                            s = "t5"; for (x = 2, cx = M.Groups[s].Captures.Count; x < cx && M.Groups[s].Captures[1].Value == M.Groups[s].Captures[x].Value; x++) { }
                            if (x == cx) {
                                int cnt = Convert.ToInt32(M.Groups["cnt"].Value, 16);
                                ushort lo = Convert.ToUInt16(M.Groups["lo"].Value, 16);
                                ushort hi = Convert.ToUInt16(M.Groups["hi"].Value, 16);
                                int tbl = (hi << 16) + (short)lo;
                                Debug.Assert(pc0 <= tbl && tbl < pc1tbl);
                                fs.Position = tbl;

                                aladdr = new uint[cnt];
                                for (int t = 0; t < cnt; t++) aladdr[t] = br.ReadUInt32();

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        class UtLUp {
            public static String GetAsmLines(Stream fs, BinaryReader br, uint pc, int cnt) {
                fs.Position = pc;
                StringWriter wr = new StringWriter();
                for (int x = 0; x < cnt; x++, pc += 4) {
                    wr.WriteLine(String.Join(" ", EEDisarm.parse(br.ReadUInt32(), (uint)(pc)).al));
                }
                return wr.ToString();
            }

            public static Match ExecRegex(String asm, String pattern) {
                String[] regs = "zero:at:v0:v1:a0:a1:a2:a3:t0:t1:t2:t3:t4:t5:t6:t7:s0:s1:s2:s3:s4:s5:s6:s7:t8:t9:k0:k1:gp:sp:s8:ra".Split(':');
                foreach (String reg in regs) {
                    pattern = Regex.Replace(pattern, "\\b" + reg + "\\b", "(?<" + reg + ">\\w+)");
                }

                Match M = Regex.Match(asm, pattern);
                if (M.Success) {
                    foreach (String reg in regs) {
                        Group g = M.Groups[reg];
                        if (g != null) {
                            CaptureCollection cc = g.Captures;
                            for (int x = 1; x < cc.Count; x++) {
                                if (cc[0].Value != cc[x].Value)
                                    return null;
                            }
                        }
                    }
                    return M;
                }
                return null;
            }
        }

        struct ChkHLP {
            public int spadd;
            public uint pc;

            public ChkHLP(uint pc, int spadd) {
                this.pc = pc;
                this.spadd = spadd;
            }
        }


        struct NextPC {
            public uint pc;
            public int jalcnt;

            public NextPC(uint pc, int jalcnt) {
                this.pc = pc;
                this.jalcnt = jalcnt;
            }
        }

        class UtTestopTy {
            public static bool IsGotoAlways(EEis eis) {
                if (eis.al[0].Equals("BEQ") && eis.al[1].Equals("zero") && eis.al[2].Equals("zero"))
                    return true;
                string s0 = eis.al[0];
                if (".J.JR.".IndexOf("." + s0 + ".") >= 0)
                    return true;
                return false;
            }
            public static bool IsGotoSometimes(string s0) {
                if (".BEQ.BEQL.BGEZ.BGEZL.BGTZ.BGTZL.BLEZ.BLEZL.BLTZ.BLTZL.BNE.BNEL.|.BC0F.BC0FL.BC0T.BC0TL.|.BC1F.BC1FL.BC1T.BC1TL.|.BC2F.BC2FL.BC2T.BC2TL.".IndexOf("." + s0 + ".") >= 0)
                    return true;
                return false;
            }
            public static bool IsGosub(string s0) {
                if (".BGEZAL.BGEZALL.BLTZAL.BLTZALL.|.JAL.JALR.|".IndexOf("." + s0 + ".") >= 0)
                    return true;
                return false;
            }
        }

    }
}
