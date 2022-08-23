using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ee1Dec;
using System.Xml;

namespace traceHLP2 {
    public class DevHLP2 {
        public DevHLP2(string fphlptxt, string fpvifwr2, string fpsavewar, string fpsavetbarec) {
            List<HRow> alHR = UtReadH.Parse(fphlptxt); // @"H:\EMU\pcsx2-0.9.4\myrec\_20081105201751_16716_776_HLP0.txt"

            Stack<HSP> deeper = new Stack<HSP>();
            SortedDictionary<uint, HLPe> dictHLP = new SortedDictionary<uint, HLPe>();
            List<KeyValuePair<uint, uint>> all2r = new List<KeyValuePair<uint, uint>>();
            SortedDictionary<uint, SortedDictionary<uint, object>> dictJalrtopcs = new SortedDictionary<uint, SortedDictionary<uint, object>>();
            int ivif1call = 0;
            foreach (HRow hr in alHR) {
                if (hr is HVif1) {
                    HVif1 o = (HVif1)hr;
                    HLPe e = dictHLP[deeper.Peek().addr];
                    e.alvif1call.Add(new KeyValuePair<int, uint>(ivif1call++, o.tadr));
                }
                if (hr is HSP) {
                    HSP o = (HSP)hr;
                    if (o.addr != 0x001E5A44) // skip buggy "sp-48".
                        if (0 == (o.addr & 0x80000000) && 0 == (o.sp & 0x80000000)) { // trace no kernel stack.
                            if (o.delta != 0) {
                                //*Debug.WriteLine("# " + new string(' ', deeper.Count) + o.delta.ToString("+0;-0;0"));
                                if (o.delta < 0) {
                                    // go down
                                    deeper.Push(o);

                                    if (dictHLP.ContainsKey(o.addr) == false) {
                                        dictHLP[o.addr] = new HLPe();
                                    }
                                }
                                else if (deeper.Count != 0) {
                                    // go up
                                    HSP peer = deeper.Pop();
                                    if (peer.delta == -o.delta) {
                                        if (peer.sp + peer.delta == o.sp) {
                                            //*Debug.WriteLine("% " + peer.addr.ToString("x8") + ":" + o.addr.ToString("x8"));

                                            dictHLP[peer.addr].cntal++;

                                            if (dictHLP[peer.addr].ale.IndexOf(o.addr) < 0) {
                                                dictHLP[peer.addr].ale.Add(o.addr);
                                            }

                                            if (deeper.Count != 0) { // left exists.
                                                all2r.Add(new KeyValuePair<uint, uint>(deeper.Peek().addr, peer.addr));
                                            }
                                        }
                                        else {
                                            //*Debug.WriteLine("---");
                                            deeper.Clear();
                                        }
                                    }
                                    else {
                                        // sweap all stack
                                        //*Debug.WriteLine("***");
                                        deeper.Clear();
                                    }
                                }
                            }
                        }
                }
                else if (hr is HJalrtopc) {
                    HJalrtopc o = (HJalrtopc)hr;
                    if (dictJalrtopcs.ContainsKey(o.addr) == false) {
                        dictJalrtopcs[o.addr] = new SortedDictionary<uint, object>();
                    }
                    dictJalrtopcs[o.addr][o.topc] = null;
                }
            }

#if true
            {
                WalkRec war = new WalkRec();
                WalkRec tbarec = new WalkRec();
                foreach (KeyValuePair<uint, uint> kv in all2r) {
                    if (dictHLP.ContainsKey(kv.Key)) dictHLP[kv.Key].cntar++;
                }
                this.fvif = new Fvifwr2(fpvifwr2); // @"H:\EMU\pcsx2-0.9.4\vifwr2_776_000.bin"
#if false // Write HLP info into debugger console
                {
                    StringWriter twr = new StringWriter();
                    foreach (KeyValuePair<uint, HLPe> kv in dictHLP) {
                        twr.WriteLine(string.Format("# {0:x8}   ({1,4},{2,4})   <Guess:{3}>", kv.Key, kv.Value.cntal, kv.Value.cntar, UtGuessHLPe.Guess(kv.Value)));
                        foreach (uint ae in kv.Value.ale) {
                            twr.WriteLine("#   " + (ae - 4U).ToString("x8"));
                        }
                        UtWalk walk = new UtWalk(fvif, kv.Key, war, dictJalrtopcs);
                        foreach (string row in walk.ToString().Split('\n')) {
                            twr.WriteLine("&   " + row);
                        }
                    }
                    File.WriteAllText("HLPout.txt", twr.ToString());
                }
#endif

#if true // Write HLP info into xml
                {
                    XmlDocument xmlo = new XmlDocument();
                    XmlElement elroot = xmlo.CreateElement("R");
                    xmlo.AppendChild(elroot);
                    foreach (KeyValuePair<uint, HLPe> kv in dictHLP) {
                        XmlElement elproc = xmlo.CreateElement("proc");
                        elroot.AppendChild(elproc);
                        elproc.SetAttribute("ip", kv.Key.ToString("x8"));
                        elproc.SetAttribute("call-l", kv.Value.cntal.ToString()); // 実際に呼ばれた回数。
                        elproc.SetAttribute("call-r", kv.Value.cntar.ToString()); // 実際に呼んだ回数。proc中にJALが有っても，呼ばれてなかったら計数しない。
                        elproc.SetAttribute("guess", UtGuessHLPe.Guess(kv.Value));

                        {
                            String s = "";
                            int cntws = 0;
                            foreach (KeyValuePair<int, uint> ia in kv.Value.alvif1call) {
                                s += string.Format("{0}:{1:x8}", ia.Key, ia.Value);
                                s += ((cntws != 0 && (cntws % 10) == 0) ? "\n" : " "); cntws++;
                            }

                            if (s != "") {
                                XmlElement elt = xmlo.CreateElement("vif1call");
                                elt.AppendChild(xmlo.CreateTextNode(s.TrimEnd()));

                                elproc.AppendChild(elt);
                            }
                        }
                        {
                            String s = "";
                            SortedDictionary<uint, object> filter = new SortedDictionary<uint, object>();
                            foreach (KeyValuePair<uint, uint> l2r in all2r) {
                                if (l2r.Key == kv.Key)
                                    filter[l2r.Value] = null;
                            }

                            int cntws = 0;
                            foreach (uint a in filter.Keys) {
                                s += (a).ToString("x8");
                                s += ((cntws != 0 && (cntws % 10) == 0) ? "\n" : " "); cntws++;
                            }

                            if (s != "") {
                                XmlElement elt = xmlo.CreateElement("r-proc"); // 呼んだ子proc。現proc中にJALが有っても，呼んでいなければ，計上しない。
                                elt.AppendChild(xmlo.CreateTextNode(s.TrimEnd()));

                                elproc.AppendChild(elt);
                            }
                        }

#if false
                        {
                            String s = "";
                            foreach (uint ae in kv.Value.ale) {
                                s += (ae - 4U).ToString("x8") + " ";
                            }

                            XmlElement elt = xmlo.CreateElement("walk-exit");
                            elt.AppendChild(xmlo.CreateCDataSection(s.TrimEnd()));
                            elproc.AppendChild(elt);
                        }
#endif
                    }
                    xmlo.Save("HLP2.xml");
                }
#endif
                foreach (KeyValuePair<uint, HLPe> kv in dictHLP) {
                    UtWalk walk = new UtWalk(fvif, kv.Key, war, dictJalrtopcs);
                    foreach (uint pc in walk.dictUnkys.Keys) tbarec.dictPC[pc] = null;
                    HLPi o = new HLPi(kv.Key, kv.Value, walk);
                    alHLPi.Add(o);
                }
                war.Save(fpsavewar);
                tbarec.Save(fpsavetbarec);
            }
#endif
        }

        internal Fvifwr2 fvif;
        internal List<HLPi> alHLPi = new List<HLPi>();
    }

    public class HLPi {
        public HLPi(uint startAddr, HLPe termination, UtWalk walk) {
            this.startAddr = startAddr;
            this.exits = termination;
            this.walk = walk;
        }

        public uint startAddr; // start pc detected by stack tracing (ADD sp).
        public HLPe exits; // end pc detected by stack tracing (ADD sp).
        public UtWalk walk;
    }

    public class WalkRec {
        public SortedDictionary<uint, object> dictPC = new SortedDictionary<uint, object>();

        class Wr : IDisposable {
            TextWriter wr;
            int x;

            public Wr(TextWriter wr) {
                this.wr = wr;
                this.x = 0;
            }
            public void Write(uint pc) {
                if (x == 0) {
                    wr.Write("\t\t");
                }
                wr.Write("CASEx({0:x8}): ", pc);
                x++;
                if (x == 10) {
                    x = 0;
                    wr.WriteLine();
                }
            }

            #region IDisposable メンバ

            public void Dispose() {
                wr.WriteLine();
            }

            #endregion
        }

        public void Save(string fpsavewar) {
            using (StreamWriter wr = new StreamWriter(fpsavewar, false, Encoding.ASCII)) { // @"H:\Proj\khkh_xldM\MEMO\HLP2out\savewar.txt"
                wr.WriteLine("\t\t// c " + dictPC.Count + " t " + DateTime.Now.ToFileTimeUtc() + " ");
                using (Wr ww = new Wr(wr)) {
                    foreach (uint pc in dictPC.Keys) {
                        ww.Write(pc);
                    }
                }
                wr.Flush();
            }
        }
    }

    public class UtWalk {
        Fvifwr2 fvif;
        WalkRec war;
        SortedDictionary<uint, SortedDictionary<uint, object>> dictJalrtopcs;

        public UtWalk(Fvifwr2 fvif, uint addr, WalkRec war, SortedDictionary<uint, SortedDictionary<uint, object>> dictJalrtopcs) {
            this.fvif = fvif;
            this.war = war;
            this.dictJalrtopcs = dictJalrtopcs;
            Walk(addr);
        }

        internal SortedDictionary<uint, object> dictAlready = new SortedDictionary<uint, object>(); // footprint PCs by disasm walking
        internal SortedDictionary<uint, object> dictTerm = new SortedDictionary<uint, object>(); // known term (jr ra) PCs by disasm walking
        internal SortedDictionary<uint, object> dictUnkys = new SortedDictionary<uint, object>(); // unknown term (jalr) yet solved PCs by disasm walking
        internal SortedDictionary<uint, object> dictUnks = new SortedDictionary<uint, object>(); // unknown term (jalr) solved PCs by disasm walking
        Queue<uint> dictWait = new Queue<uint>();

        public override string ToString() {
            List<string> ale = new List<string>();
            foreach (uint pc in dictTerm.Keys) ale.Add(pc.ToString("x8"));
            List<string> alU = new List<string>();
            foreach (uint pc in dictUnkys.Keys) alU.Add(pc.ToString("x8"));

            return "a " + dictAlready.Count + "\n"
                + "exit ( " + string.Join(" ", ale.ToArray()) + " )\n"
                + "U ( " + string.Join(" ", alU.ToArray()) + " )"
                ;
        }

        void Walk(uint pc) {
            dictWait.Enqueue(pc);
            Walk();
        }

        void Walk() {
            while (dictWait.Count != 0) {
                uint pc = dictWait.Dequeue();
                bool delaySlot = false;
                while (true) {
                    if (dictAlready.ContainsKey(pc)) break;
                    uint v = fvif.ReadAt(pc);
                    EEis eis = EEDisarm.parse(v, pc);
                    //--+
                    switch (eis.al[0]) {
                        case "SB":
                        case "SD":
                        case "SDL":
                        case "SDR":
                        case "SH":
                        case "SW":
                        case "SWL":
                        case "SWR":
                        case "SQ":
                        case "SWC1":
                            if (eis.al[2].EndsWith("(sp)"))
                                break;
                            if (0x105550 <= pc && pc <= 0x1056e0)
                                break;
                            war.dictPC[pc] = null;
                            break;
                        case "VISWR":
                        case "VSQD":
                        case "VSQI":
                            break;
                    }
                    //+--
                    dictAlready[pc] = null;
                    //+--
                    if (delaySlot) {
                        break;
                    }
                    if (false) { }
                    else if (eis.al[0].Equals("JR") && eis.al[1].Equals("ra")) {
                        delaySlot = true; dictTerm[pc] = null;
                    }
                    else if (eis.al[0].Equals("JR")) {
                        if (dictJalrtopcs.ContainsKey(pc)) {
                            foreach (uint topc in dictJalrtopcs[pc].Keys) {
                                dictWait.Enqueue(topc);
                            }
                            dictUnks[pc] = null;
                        }
                        else {
                            dictUnkys[pc] = null;
                        }
                    }
                    else if (eis.al[0].Equals("JALR")) {
                        if (dictJalrtopcs.ContainsKey(pc)) {
                            foreach (uint topc in dictJalrtopcs[pc].Keys) {
                                dictWait.Enqueue(topc);
                            }
                            dictUnks[pc] = null;
                        }
                        else {
                            dictUnkys[pc] = null;
                        }
                    }
                    else if (OputIntelli.IsGoto(eis)) {
                        if (eis.al[1].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[1].Substring(1), 16));
                        }
                        else if (eis.al.Length >= 3 && eis.al[2].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[2].Substring(1), 16));
                        }
                        else if (eis.al.Length >= 4 && eis.al[3].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[3].Substring(1), 16));
                        }
                        else Debug.Fail(eis.ToString());
                        delaySlot = true;
                    }
                    else if (OputIntelli.IsBranch(eis)) {
                        if (eis.al[1].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[1].Substring(1), 16));
                        }
                        else if (eis.al.Length >= 3 && eis.al[2].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[2].Substring(1), 16));
                        }
                        else if (eis.al.Length >= 4 && eis.al[3].StartsWith("$")) {
                            dictWait.Enqueue(Convert.ToUInt32(eis.al[3].Substring(1), 16));
                        }
                        else Debug.Fail(eis.ToString());
                    }
                    pc += 4;
                }
            }
        }

        class OputIntelli {
            public static bool IsGoto(EEis eis) {
                if (eis.al[0].Equals("BEQ") && eis.al[1].Equals("zero") && eis.al[2].Equals("zero"))
                    return true;
                string s0 = eis.al[0];
                if (".J.JR.".IndexOf("." + s0 + ".") >= 0)
                    return true;
                return false;
            }
            public static bool IsBranch(EEis eis) {
                string s0 = eis.al[0];
                if (".BEQ.BEQL.BGEZ.BGEZL.BGTZ.BGTZL.BLEZ.BLEZL.BLTZ.BLTZL.BNE.BNEL.|.BC0F.BC0FL.BC0T.BC0TL.|.BC1F.BC1FL.BC1T.BC1TL.|.BC2F.BC2FL.BC2T.BC2TL.".IndexOf("." + s0 + ".") >= 0)
                    return true;
                if (".BGEZAL.BGEZALL.BLTZAL.BLTZALL.|.JAL.JALR.|".IndexOf(s0) >= 0)
                    return true;
                return false;
            }
        }

        class Oput {
            public static bool IsGoto(string s0) {
                if (".BEQ.BEQL.BGEZ.BGEZL.BGTZ.BGTZL.BLEZ.BLEZL.BLTZ.BLTZL.BNE.BNEL.J.JR.|.BC0F.BC0FL.BC0T.BC0TL.|.BC1F.BC1FL.BC1T.BC1TL.|.BC2F.BC2FL.BC2T.BC2TL.".IndexOf("." + s0 + ".") >= 0)
                    return true;
                return false;
            }
            public static bool IsGosub(string s0) {
                if (".BGEZAL.BGEZALL.BLTZAL.BLTZALL.|.JAL.JALR.|".IndexOf(s0) >= 0)
                    return true;
                return false;
            }
        }
    }

    public class Fvifwr2 {
        public byte[] eeram;
        public MemoryStream si;
        public BinaryReader br;
        public uint tadr = 0;

        public byte[] spad32k;
        public MemoryStream spsi;
        public BinaryReader spbr;

        public uint ReadAt(uint a) {
            si.Position = a;
            return br.ReadUInt32();
        }

        public Fvifwr2(string fp) {
            using (FileStream fs = File.OpenRead(fp)) {
                {
                    byte[] bin = new BinaryReader(fs).ReadBytes(1024);
                    int r = Array.IndexOf(bin, 0x1A);
                    if (r < 0) r = bin.Length;
                    foreach (string row in Encoding.ASCII.GetString(bin, 0, r).Split('\n')) {
                        if (row.StartsWith("#"))
                            continue;
                        string[] cols = row.Split(new char[] { '=' }, 2);
                        if (cols.Length != 2)
                            continue;
                        if (cols[0].Equals("tadr"))
                            tadr = Convert.ToUInt32(cols[1], 16);
                    }
                }
                fs.Position = 1024;
                eeram = new BinaryReader(fs).ReadBytes(32 * 1024 * 1024);
                si = new MemoryStream(eeram, false);
                br = new BinaryReader(si);
                spad32k = new BinaryReader(fs).ReadBytes(32768);
                spsi = new MemoryStream(spad32k, false);
                spbr = new BinaryReader(spsi);
            }
        }
    }

    class UtGuessHLPe {
        public static string Guess(HLPe o) {
            if (o.cntal < o.cntar)
                return "Parent func";
            if (o.cntal > o.cntar)
                return "Child func";

            return "Balanced func";
        }
    }

    public class HLPe {
        public List<uint> ale = new List<uint>(); // array exit pc
        public int cntal = 0; // at left. input, entrance.
        public int cntar = 0; // at right. output, exit.
        public List<KeyValuePair<int, uint>> alvif1call = new List<KeyValuePair<int, uint>>(); // k=index, v=tadr
    }
    public class UtReadH {
        static Regex rsp = new Regex("^@([0-9a-f]{8})\\s+([0-9]+)\\s+([0-9a-f]{8})\\s+sp([\\+\\-])([0-9]+)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        static Regex rvif1 = new Regex("^@([0-9a-f]{8})\\s+([0-9]+)\\s+vif1\\s+tadr\\s+([0-9a-f]{8}) ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        static Regex rjalrtopc = new Regex("^@([0-9a-f]{8})\\s+([0-9]+)\\s+jalrtopc\\s+([0-9a-f]{8}) ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        static Regex rsfpc = new Regex("^@([0-9a-f]{8})\\s+([0-9]+)\\s+sfpc\\s+([0-9a-f]{2})\\s+([0-9a-f]{8})\\s+([0-9a-f]{8}) ", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public static List<HRow> Parse(string fp) {
            List<HRow> al = new List<HRow>();
            foreach (string row in File.ReadAllLines(fp, Encoding.Default)) {
                if (row.Length == 0)
                    continue;
                {
                    Match M = rsfpc.Match(row);
                    if (M.Success) {
                        HSfpc o = new HSfpc(
                            Convert.ToUInt32(M.Groups[1].Value, 16),
                            Convert.ToUInt32(M.Groups[2].Value),
                            Convert.ToByte(M.Groups[3].Value, 16),
                            Convert.ToUInt32(M.Groups[4].Value, 16),
                            Convert.ToUInt32(M.Groups[5].Value, 16)
                            );
                        al.Add(o);
                        continue;
                    }
                }
                {
                    Match M = rsp.Match(row);
                    if (M.Success) {
                        HSP o = new HSP(
                            Convert.ToUInt32(M.Groups[1].Value, 16),
                            Convert.ToUInt32(M.Groups[2].Value),
                            Convert.ToUInt32(M.Groups[3].Value, 16),
                            Convert.ToInt16(int.Parse(M.Groups[5].Value) * int.Parse(M.Groups[4].Value + "1"))
                            );
                        al.Add(o);
                        continue;
                    }
                }
                {
                    Match M = rvif1.Match(row);
                    if (M.Success) {
                        HVif1 o = new HVif1(
                            Convert.ToUInt32(M.Groups[1].Value, 16),
                            Convert.ToUInt32(M.Groups[2].Value),
                            Convert.ToUInt32(M.Groups[3].Value, 16)
                            );
                        al.Add(o);
                        continue;
                    }
                }
                {
                    Match M = rjalrtopc.Match(row);
                    if (M.Success) {
                        HJalrtopc o = new HJalrtopc(
                            Convert.ToUInt32(M.Groups[1].Value, 16),
                            Convert.ToUInt32(M.Groups[2].Value),
                            Convert.ToUInt32(M.Groups[3].Value, 16)
                            );
                        al.Add(o);
                        continue;
                    }
                }
                Debug.Fail(row);
            }
            return al;
        }
    }
    public class HRow {
        public uint addr, time;
    }
    public class HSP : HRow {
        public uint sp;
        public short delta;

        public HSP(uint addr, uint time, uint sp, short delta) {
            this.addr = addr;
            this.time = time;
            this.sp = sp;
            this.delta = delta;
        }
    }
    public class HVif1 : HRow {
        public uint tadr;

        public HVif1(uint addr, uint time, uint tadr) {
            this.addr = addr;
            this.time = time;
            this.tadr = tadr;
        }
    }
    public class HJalrtopc : HRow {
        public uint topc;

        public HJalrtopc(uint addr, uint time, uint topc) {
            this.addr = addr;
            this.time = time;
            this.topc = topc;
        }
    }
    public class HSfpc : HRow {
        public byte ty;
        public uint pcpos, mempos;

        public HSfpc(uint addr, uint time, byte ty, uint pcpos, uint mempos) {
            this.addr = addr;
            this.time = time;
            this.ty = ty;
            this.pcpos = pcpos;
            this.mempos = mempos;
        }

        public string _ty2s {
            get {
                string s = "?", t = "?";
                switch (ty & 0xF) {
                    case 0: s = "1byte"; break;
                    case 1: s = "2bytes"; break;
                    case 2: s = "4bytes"; break;
                    case 3: s = "8bytes"; break;
                    case 4: s = "16bytes"; break;
                }
                switch (ty >> 4) {
                    case 3: t = "Sxxx_coX;recStore_co class"; break;
                    case 2: t = "Sxxx_co class"; break;
                    case 1: t = "recStore class"; break;
                    case 0: t = "recMemConstWrite class"; break;
                }
                return t + " " + s;
            }
        }
    }
}
