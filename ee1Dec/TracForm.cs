//#define HACK
#define SHOWOV
//#define RECORDO2C
#define TRACERW

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Text.RegularExpressions;
using ee1Dec.Properties;
using System.CodeDom.Compiler;
using System.CodeDom;
using ee1Dec.C;
using System.Reflection;
using System.Drawing.Imaging;
using System.Threading;
using ee1Dec.Utils;
using ee1Dec.Models;
using ee1Dec.Interfaces;
using ee1Dec.Enums;
using ee1Dec.Utils.EEDis;
using ee1Dec.Utils.CSRecompiler;
using ee1Dec.Utils.HexText;
using ee1Dec.Controls;
using ee1Dec.Controls.HV;
using System.Xml.Serialization;

namespace ee1Dec {
    public partial class TracForm : Form, IExecEE {
        public TracForm() {
            InitializeComponent();
        }

        Hashtable dictvisit = new Hashtable();

        /// <summary>
        /// old pc to current pc. single step from and to.
        /// </summary>
        SortedDictionary<KeyValuePair<uint, uint>, object> dicto2c = new SortedDictionary<KeyValuePair<uint, uint>, object>(new Sorto2c());

        SortedSet<ulong> readMemFrom = new SortedSet<ulong>();

        BinaryReader brMem;

        TracReader tracReader;

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            uint pc = (4 * (uint)e.ItemIndex);
            sieeMem.Position = pc;
            uint word = brMem.ReadUInt32();
            ResHT res = pht.findRes((int)pc);
            string desc = (res != null) ? res.text : null;
            bool visit = dictvisit.ContainsKey(pc);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = pc.ToString("x7");
            var cpuInstr = EEDisarm.parse(word, pc);
            lvi.SubItems.Add(cpuInstr.ToString());
            lvi.SubItems.Add(desc);
            if (res != null && res.ov != 0) {
                //lvi.UseItemStyleForSubItems = false;
                lvi.SubItems[0].ForeColor = Color.Blue;
            }
            lvi.ImageIndex = (pc == this.cpuStat.pc) ? 1 : (visit ? 2 : -1);
            if (res != null && res.clr != Color.Empty) lvi.BackColor = res.clr;
            {
                // -1=Empty 0=End 1=D 2=U 3=JR
                var isJR = word == 0x03e00008;

                var isJmp = JxxUt.isJxx(cpuInstr.al[0]);
                //bool f0 = ((word & 0xFFFF0000) == 0x10000000);
                //bool f_1 = false;
                //if (pc >= 4) { sieeMem.Position = pc - 4; uint w_4 = brMem.ReadUInt32(); f_1 = ((w_4 & 0xFFFF0000) == 0x10000000); }
                //bool f_2 = false;
                //if (pc >= 8) { sieeMem.Position = pc - 8; uint w_8 = brMem.ReadUInt32(); f_2 = ((w_8 & 0xFFFF0000) == 0x10000000); }
                lvi.StateImageIndex = isJR ? (3) : isJmp ? (0) : -1;
            }
            e.Item = lvi;
        }

        MemoryStream sieeMem = null;

        private void TracForm_Load(object sender, EventArgs e) {
            Rectangle rc = Screen.PrimaryScreen.WorkingArea;
            Left = (rc.Left + rc.Right) / 2 - Size.Width / 2;
            Top = (rc.Top);
            Height = Math.Min(1000, rc.Height);

            ResetViews(new Trif { prefix = "notOpened", });
        }

        void ResetViews(Trif open) {
            sieeMem = new MemoryStream(this.e.ram, false);
            brMem = new BinaryReader(sieeMem);

            if (open.tracfp != null) {
                openTracBin(open.tracfp);
            }
            if (open.descfp != null) {
                openDescTxt(open.descfp);
            }

            lastOpen = open;

            dictvisit.Clear();
            readMemFrom.Clear();

            linkLabelPrefix.Text = tracPrefix;

            {
                goToPC();
            }
        }

        ParseHT pht = new ParseHT();

        private void openDescTxt(string fp) {
            pht.openHelp(fp);
        }

        CustEE e = new CustEE();


        PrivateManna anna = new PrivateManna();

        CPUState cpuStat = new CPUState();

        private void openTracBin(string fp) {
            if (Path.GetExtension(fp).ToLower().Equals(".zbin")) {
                cpuStat.fs = File.OpenRead(fp);
            }
            else {
                cpuStat.fs = File.OpenRead(fp);
            }

            cpuStat.tick = 0;
            cpuStat.pc = cpuStat.opc = 0;
            tracReader = Trac2Reader.CreateFrom(e, cpuStat);
            tracReader.Part1();

            dictvisit[cpuStat.pc] = null;
            linkLabel1.Text = Path.GetFileName(fp);
            hexVwer1.SetBin(e.ram);
            hexVwer2.SetBin(e.ram);

#if HACK
            mot2.Parse(sieeMem, "obj/P_EX110.mset", 0x009E0340);
#endif

            refreshEEr();
            refreshCOPr();

#if HACK
            hexVwer2.SetPos(0x3B6BB0); // first matrix write chance
#endif
        }

        private void refreshCOPr() {
            StringBuilder s = new StringBuilder();
            for (int t = 0; t < 32; t++) {
                s.AppendFormat("$f{0,-2} |{1,15}\n", t, e.fpr[t].f);
            }
            s.AppendFormat("ACC  |{0,15}", e.fpracc.f);
            labelCOP1Reg.Text = s.ToString();

            s.Length = 0;
            for (int t = 0; t < 32; t++) {
                s.AppendFormat("VF{0,-2} |{1,15}|{2,15}|{3,15}|{4,15}\n", t, e.VF[t].x, e.VF[t].y, e.VF[t].z, e.VF[t].w);
            }
            s.AppendFormat("ACC  |{0,15}|{1,15}|{2,15}|{3,15}\n", e.Vacc.x, e.Vacc.y, e.Vacc.z, e.Vacc.w);
            s.AppendFormat("P    |{0,15}\n", e.Vp.f);
            s.AppendFormat("Q    |{0,15}\n", e.Vq.f);
            s.Append("\n");
            for (int t = 0; t < 8; t++) {
                s.AppendFormat("     |VI{0,-2} |{1:x8} |VI{2,-2} |{3:x8} |VI{4,-2} |{5:x8} |VI{6,-2} |{7:x8}\n"
                    , t, e.VI[t].UL
                    , 8 + t, e.VI[8 + t].UL
                    , 16 + t, e.VI[16 + t].UL
                    , 24 + t, e.VI[24 + t].UL
                    );
            }

            labelCOP2.Text = s.ToString();
        }

        private void refreshEEr() {
            StringBuilder s = new StringBuilder();
            string[] r32 = EEr.GPR32;
            for (int t = 0; t < 32; t++) {
                uint[] ul = e.GPR[t].UL;
                s.AppendFormat("{0,-5} {1:x8}_{2:x8}_{3:x8}_{4:x8}\n", r32[t], ul[3], ul[2], ul[1], ul[0]);
            }
            labelEER.Text = s.ToString();
            labelTick.Text = "tick: " + cpuStat.tick.ToString();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Alt && e.KeyCode == Keys.Multiply) {
                goToPC();
                e.Handled = true; e.SuppressKeyPress = true;
            }
        }

        private void goToPC() {
#if false
            List<int> al = new List<int>();
            foreach (int v in listView1.SelectedIndices) al.Add(v);
            foreach (int v in al) listView1.Items[v].Selected = false;
#endif

            int sel = (int)(this.cpuStat.pc / 4);
            ListViewItem lvi = listView1.Items[sel];
            lvi.Focused = true;
            //lvi.Selected = true;
            listView1.EnsureVisible(sel);
        }

        uint[] r_pcs = new uint[1024];
        int r_pci = 0;

        void addToRecentPC(uint pc) {
            r_pcs[r_pci & 1023] = pc;
            r_pci++;
        }

        public void Stepee() {
            buttonStep_Click(null, null);
        }

        private void buttonStep_Click(object sender, EventArgs e) {
            if (true) { int selx = (int)this.cpuStat.pc / 4; listView1.RedrawItems(selx, selx, true); }

            if (cpuStat.fs.Position != cpuStat.fs.Length) {
                addToRecentPC(this.cpuStat.pc);
                singleStepSkipKernel();
                postVisitLogic();
                postVisitGui();

                hexVwer1.RangeMarkedList.Clear();
                foreach (var kv in tracReader.memUpdates) {
                    int k = kv.addr;
                    int v = kv.size;
                    if (k < 0) continue;
                    hexVwer1.RangeMarkedList.Add(new RangeMarked(k, v, clrHl, Color.Green));
                    hexVwer1.SetPos(k - (k % Math.Max(1, 16 * hexVwer1.GetLineCnt())));
                    hexVwer2.Invalidate();
                }

                if (true) {
                    int selx = (int)(this.cpuStat.pc / 4);
                    if (selx < listView1.VirtualListSize) {
                        listView1.RedrawItems(selx, selx, true);
                    }
                }

                goToPC();
                refreshEEr();
                refreshCOPr();
            }
        }

        private void singleStepSkipKernel() {
            while (!tracReader.EOF) {
                cpuStat.tick++;

                tracReader.Part2();

#if TRACERW
                if (cpuStat.pc < 1024 * 1024 * 32) {
                    sieeMem.Position = cpuStat.pc;
                    var word = brMem.ReadUInt32();
                    var opcode = word >> 26;
                    switch (opcode) {
                        case 0x1A: //LDL
                        case 0x1B: //LDR
                        case 0x1E: //LQ
                        case 0x20: //LB
                        case 0x21: //LH
                        case 0x22: //LWL
                        case 0x23: //LW
                        case 0x24: //LBU
                        case 0x25: //LHU
                        case 0x26: //LWR
                        case 0x27: //LWU
                        case 0x31: //LWC1
                        case 0x37: //LD
                            {
                                var rs = (word >> 21) & 31;
                                var target = (uint)(e.GPR[rs].SL0 + ((short)word));
                                readMemFrom.Add(((ulong)cpuStat.pc << 32) | target);
                                break;
                            }
                    }
                }
#endif

                tracReader.Part1();

                if (0x80000000 <= cpuStat.pc) {
                    continue;
                }

                break;
            }
        }

        private void postVisitLogic() {
            dictvisit[this.cpuStat.pc] = null;

#if RECORDO2C
            dicto2c[new KeyValuePair<uint, uint>(cpuStat.opc, this.cpuStat.pc)] = null;
#endif
        }

        private void postVisitGui() {
            ResHT res = pht.findRes((int)cpuStat.opc);
            if (res != null) {
                foreach (string form in res.alform) {
                    string fout =
                        Regex.Replace(
                            Regex.Replace(form, "\\{(zero|at|v0|v1|a0|a1|a2|a3|t0|t1|t2|t3|t4|t5|t6|t7|s0|s1|s2|s3|s4|s5|s6|s7|t8|t9|k0|k1|gp|sp|s8|ra|\\$f[0-9]+)(\\:[^\\}]+)?\\}", new MatchEvaluator(MEEvalval)),
                            "\\<\\<([\\:]?.+?)(\\:.+?)\\>\\>",
                            new MatchEvaluator(MEEvalval2)
                            );
                    int cx = textBoxLogging.TextLength;
                    textBoxLogging.Select(cx, 0);
                    textBoxLogging.SelectedText = fout + "\r\n";
                }
#if SHOWOV
                foreach (string ovtext in res.alovtext) {
                    string ovtemp = ovtext;
                    Operesolver oper = new Operesolver();
                    sieeMem.Position = cpuStat.opc;
                    uint word = brMem.ReadUInt32();
                    EEis @is = EEDisarm.parse(word, cpuStat.opc);

                    if (@is.al.Length == 3 && oper.resolve(@is.al[2], e) && oper.operty == Operty.Off) {
                        string finder1 = null;
                        if (finder1 == null) finder1 = anna.Find(oper.val, sieeMem);
                        if (finder1 != null) {
                            ovtemp += " " + finder1;
                        }
                    }

                    int cx = textBoxLogging.TextLength;
                    textBoxLogging.Select(cx, 0);
                    textBoxLogging.SelectedText = ovtemp + "\r\n";
                }
#endif
                textBoxLogging.Select(textBoxLogging.TextLength, 0);
            }

            listView1_SelectedIndexChanged(null, null);
        }

        public string MEEvalval(Match M) {
            Trace.Assert(M.Success);
            string var = M.Groups[1].Value;
            string fmt = (M.Groups.Count >= 3 && M.Groups[2].Value.Length >= 1) ? M.Groups[2].Value.Substring(1) : null;

            string res = null;
            int eer = "#zero#at# #v0# #v1# #a0# #a1# #a2# #a3# #t0# #t1# #t2# #t3# #t4# #t5# #t6# #t7# #s0# #s1# #s2# #s3# #s4# #s5# #s6# #s7# #t8# #t9# #k0# #k1# #gp# #sp# #r8# #ra#".IndexOf("#" + var + "#");
            if (eer != -1) {
                eer /= 5;
                res = e.GPR[eer].SL[0].ToString(fmt);
                return res;
            }
            if (var.StartsWith("$f")) {
                int fpr = int.Parse(var.Substring(2));
                res = e.fpr[fpr].f.ToString(fmt);
                return res;
            }

            return "?";
        }
        public string MEEvalval2(Match M) {
            Trace.Assert(M.Success);
            string var = M.Groups[1].Value;
            string fmt = (M.Groups.Count >= 3 && M.Groups[2].Value.Length >= 1) ? M.Groups[2].Value.Substring(1) : null;

            string res = null;
            try {
                // @0012bd6c:out=x<9.>vf4.xyz <<:a1 30 +:x>> * Mtx1.abc {a1:x} -> vf4.xyz <<:a1 30 +:x>>
                int addr = (int)RiAddrUt.Resolve(var, this.e);
                res = addr.ToString(fmt);
                return res;
            }
            catch (RiAddrUt.ERR) {

            }
            return "?";
        }

        public static readonly Color clrHl = Color.FromArgb(100, Color.LightGreen);

        bool ParseVarval(String var, out int pos) {
            if (var.StartsWith("*")) {
                int eer = "#zero#at# #v0# #v1# #a0# #a1# #a2# #a3# #t0# #t1# #t2# #t3# #t4# #t5# #t6# #t7# #s0# #s1# #s2# #s3# #s4# #s5# #s6# #s7# #t8# #t9# #k0# #k1# #gp# #sp# #r8# #ra#".IndexOf("#" + var.Substring(1) + "#");
                if (eer != -1) {
                    eer /= 5;
                    pos = e.GPR[eer].SL[0];
                    return true;
                }
            }
            pos = 0;
            return false;
        }

        private void hexVwer2_KeyPress(object sender, KeyPressEventArgs e) {
            switch (char.ToLower(e.KeyChar)) {
                case 'g': {
                        e.Handled = true;
                        string addr = Interaction.InputBox("Go to address?", "", ((HexVwer)sender).GetPos().ToString("x"), -1, -1);
                        if (addr.Length != 0) {
                            int pos;
                            if (ParseVarval(addr, out pos)) {
                                ((HexVwer)sender).SetPos(pos);
                            }
                            else {
                                try {
                                    pos = Convert.ToInt32(addr, 16);
                                    ((HexVwer)sender).SetPos(pos);
                                }
                                catch (FormatException) {

                                }
                            }
                        }
                        break;
                    }
            }
        }

        private void buttonRunUntil_Click(object sender, EventArgs e) {
            string addr = Interaction.InputBox("Run until your address?", "", "", -1, -1);
            if (addr.Length == 0) return;
            uint off;
            try {
                off = Convert.ToUInt32(addr, 16);
            }
            catch (FormatException) {
                return;
            }

            try {
                while (this.cpuStat.pc != off && cpuStat.fs.Position != cpuStat.fs.Length) {
                    addToRecentPC(this.cpuStat.pc);
                    singleStepSkipKernel();
                    postVisitLogic();
                }
                postVisitGui();
            }
            catch (EndOfStreamException) {
                MessageBox.Show("EOF reached while stepping. The display result must be incorrect.");
            }

            listView1.Invalidate();
            goToPC();
            refreshEEr();
            refreshCOPr();
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case 'g': {
                        e.Handled = true;
                        string addr = Interaction.InputBox("Go to address?", "", (listView1.FocusedItem.Index * 4).ToString("x"), -1, -1);
                        if (addr.Length != 0) {
                            try {
                                int pos = Convert.ToInt32(addr, 16);
                                int seli = Math.Min(listView1.Items.Count - 1, Math.Max(0, pos / 4));
                                listView1.FocusedItem = listView1.Items[seli];
                                listView1.EnsureVisible(seli);
                            }
                            catch (FormatException) {

                            }
                        }
                        break;
                    }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (int seli in listView1.SelectedIndices) {
                uint pc = (4 * (uint)seli);
                sieeMem.Position = pc;
                uint word = brMem.ReadUInt32();
                EEis @is = EEDisarm.parse(word, pc);
                StringBuilder s = new StringBuilder();
                for (int x = 1; x < @is.al.Length; x++) {
                    string var = @is.al[x];
                    string val = null;

                    if (true) {
                        int eer = "zero#at#  v0#  v1#  a0#  a1#  a2#  a3#  t0#  t1#  t2#  t3#  t4#  t5#  t6#  t7#  s0#  s1#  s2#  s3#  s4#  s5#  s6#  s7#  t8#  t9#  k0#  k1#  gp#  sp#  s8#  ra#".IndexOf(var + "#");
                        if (eer != -1) {
                            eer /= 5;
                            val = var + " = " + this.e.GPR[eer].SL[0].ToString("x8");
                            s.Append(val); s.AppendLine();
                        }
                    }
                    if (val == null) {
                        Match M = Regex.Match(var, "^\\$([0-9a-f]{4})\\((..)\\)", RegexOptions.IgnoreCase);
                        if (M.Success) {
                            int off = Convert.ToInt32(M.Groups[1].Value, 16);
                            int eer = "zero#at#  v0#  v1#  a0#  a1#  a2#  a3#  t0#  t1#  t2#  t3#  t4#  t5#  t6#  t7#  s0#  s1#  s2#  s3#  s4#  s5#  s6#  s7#  t8#  t9#  k0#  k1#  gp#  sp#  s8#  ra#".IndexOf(M.Groups[2].Value + "#");
                            if (eer != -1) {
                                eer /= 5;
                                uint at = (uint)(this.e.GPR[eer].UL[0] + ((short)off));
                                val = var + " @ " + (at).ToString("x8") + ((at < 0x2000000) ? " = " + MeVaUt.format(sieeMem, at) : "");
                                s.Append(val); s.AppendLine();
                            }
                        }
                    }
                    if (val == null) {
                        Match M = Regex.Match(var, "\\$f([0-9]+)$");
                        if (M.Success) {
                            int ri = int.Parse(M.Groups[1].Value);
                            val = var + " = " + this.e.fpr[ri].f.ToString();
                            s.Append(val); s.AppendLine();
                        }
                    }
                }
                labelContHint.Text = s.ToString();
                break;
            }
        }

        private void buttonEnterprise_Click(object sender, EventArgs e) {
#if false
            Mobrc3 mob = new Mobrc3(this.e, this.ees, this);
            mob.Exec2(dirExpSim);
#endif
#if false
            try {
                Mobrc2 mob = new Mobrc2(this.e, this.ees, this);
                mob.Exec2(dirExpSim);
            }
            catch (TraceDiffException err) {
                int cx = textBoxLogging.TextLength;
                textBoxLogging.Select(cx, 0);
                textBoxLogging.SelectedText = "<ERR> " + err.Message + "\r\n";

                MessageBox.Show(this, err.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
#endif
#if false
            Mobrc1 mob = new Mobrc1(this.e, this.ees, this);
            mob.Exec2a(dirExpSim, notifyIconResult);
#endif
#if false
            Mobrc1 mob = new Mobrc1(this.e, this.ees, this);
            mob.Exec3(Path.Combine(dirExpSim, "1ACAD80.bin"));
#endif
#if false
            Mob mob = new Mob(this.e, this.ees, this);
            mob.Exec2(Path.Combine(dirExpSim, "1ACAD80.bin"));
#endif
#if false
            try {
                Mob mob = new Mob(this.e, this.ees, this);
                mob.Exec();
            }
            catch (TraceDiffException err) {
                int cx = textBoxLogging.TextLength;
                textBoxLogging.Select(cx, 0);
                textBoxLogging.SelectedText = "<ERR> " + err.Message + "\r\n";

                MessageBox.Show(this, err.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (ExeceeException) {
                MessageBox.Show(this, "DIFF", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
#endif
#if false
            mot2.DEBUG();
#endif
#if false
            BinaryReader br = new BinaryReader(sieeMem);
            sieeMem.Position = 0x3B6BB0;
            byte[] b1 = br.ReadBytes(0xD00);
            sieeMem.Position = 0x1ACAD50;
            byte[] b2 = br.ReadBytes(0x3940);

            using (FileStream fs = File.Create(Path.Combine(Settings.Default.enterprisedir, "b1b2.bin"))) {
                new BinaryWriter(fs).Write(b2);
                new BinaryWriter(fs).Write(b1);
                fs.Close();
            }
#endif
        }

        private Traclist GetTraclist() {
            return (Traclist)new XmlSerializer(typeof(Traclist))
                .Deserialize(
                    new MemoryStream(
                        File.ReadAllBytes(Settings.Default.TraclistXml)
                    )
                );
        }

        private void buttonOpenWith_Click(object sender, EventArgs e) {
            var menu = new ContextMenuStrip();
            foreach (var open in GetTraclist().Open) {
                var tsi = menu.Items.Add(open.ToString());
                tsi.Click += tsi_Click;
                tsi.Tag = open;
            }
            menu.Show(buttonOpenWith, 0, 0);
        }

        Trif lastOpen;

        string tracPrefix => lastOpen.prefix;

        string dirExpSim {
            get {
                string dir = Path.Combine(Settings.Default.expsimdir, tracPrefix);
                Directory.CreateDirectory(dir);
                return dir;
            }
        }

        void tsi_Click(object sender, EventArgs e) {
            var open = (Trif)((ToolStripItem)sender).Tag;

            ResetViews(open);

            Invalidate(true);
        }

        private void buttonExpSim_Click(object sender, EventArgs e) {
            string dir = dirExpSim;

            SortedList<uint, object> dictVisit = new SortedList<uint, object>();
            foreach (uint k in dictvisit.Keys) dictVisit[k] = null;

#if TRACERW
            using (StreamWriter wr = new StreamWriter(Path.Combine(dir, "readmemfrm.txt"), false, Encoding.ASCII)) {
                foreach (ulong pair in readMemFrom) {
                    wr.WriteLine("{0:X16}", pair);
                }
                wr.Close();
            }
#endif

            using (StreamWriter wr = new StreamWriter(Path.Combine(dir, "visit.txt"), false, Encoding.ASCII)) {
                foreach (uint addr in dictVisit.Keys) {
                    wr.WriteLine("{0:x8}", addr);
                }
                wr.Close();
            }

            List<uint> alvis = new List<uint>(dictVisit.Keys);
            SortedList<uint, object> dictRec = new SortedList<uint, object>();
            if (alvis.Count != 0) {
                uint pc = alvis[0], opc = 0;
                SortedDictionary<uint, object> jpcz = new SortedDictionary<uint, object>();
                for (int t = 0; t < alvis.Count; opc = pc, t++) {
                    pc = alvis[t];

                    sieeMem.Position = pc;
                    uint word = brMem.ReadUInt32();
                    EEis o = EEDisarm.parse(word, pc);
                    if (JxxUt.isJxx(o.al[0])) {
                        jpcz[pc + 8] = null;

                        foreach (KeyValuePair<uint, uint> o2c in dicto2c.Keys) {
                            if (o2c.Key == pc + 4) {
                                dictRec[o2c.Value] = null;
                            }
                        }
                    }

                    if (t == 0 || opc + 4 != pc || jpcz.ContainsKey(pc)) {
                        dictRec[pc] = null;
                        continue;
                    }
                }
            }
            using (StreamWriter wr = new StreamWriter(Path.Combine(dir, "rec.txt"), false, Encoding.ASCII)) {
                foreach (uint addr in dictRec.Keys) {
                    wr.WriteLine("{0:x8}", addr);
                }
                wr.Close();
            }

            SortedList<string, object> dictDisarm = new SortedList<string, object>();

            using (StreamWriter wr = new StreamWriter(Path.Combine(dir, "opc.txt"), false, Encoding.ASCII)) {
                SortedList<string, object> dictOpc = new SortedList<string, object>();
                foreach (uint addr in dictvisit.Keys) {
                    uint pc = addr;
                    sieeMem.Position = pc;
                    uint word = brMem.ReadUInt32();
                    EEis o = EEDisarm.parse(word, pc);
                    dictOpc[o.al[0]] = null;
                    dictDisarm[o.ToString()] = null;
                }
                foreach (string opc in dictOpc.Keys) {
                    wr.WriteLine(opc);
                }
                wr.Close();
            }

            File.WriteAllLines(Path.Combine(dir, "opcopr.txt"), new List<string>(dictDisarm.Keys).ToArray());

#if RECORDO2C
            using (StreamWriter wr = new StreamWriter(Path.Combine(dir, "o2c.txt"), false, Encoding.ASCII)) {
                foreach (KeyValuePair<uint, uint> o2c in dicto2c.Keys) {
                    wr.WriteLine("{0:x8} {1:x8}", o2c.Key, o2c.Value);
                }
                wr.Close();
            }
#endif
            Process.Start(dir);

            MessageBox.Show("Export succeeded");
        }


        private void buttonTocs_Click(object sender, EventArgs e) {
            string dir = dirExpSim;

            List<string> aldirs = new List<string>(".".Split('|'));
            if (File.Exists(Path.Combine(dir, "recrefdir.txt"))) {
                using (StreamReader rr = new StreamReader(Path.Combine(dir, "recrefdir.txt"), Encoding.ASCII)) {
                    string row;
                    while (null != (row = rr.ReadLine())) {
                        aldirs.Add(row);
                    }
                }
            }

            SortedList<uint, object> dictVisit = new SortedList<uint, object>();
            foreach (string dirin in aldirs) {
                using (StreamReader rr = new StreamReader(Path.Combine(Path.Combine(dir, dirin), "visit.txt"), Encoding.ASCII)) {
                    string row;
                    while (null != (row = rr.ReadLine()) && row.Length != 0) {
                        dictVisit[Convert.ToUInt32(row, 16)] = null;
                    }
                }
            }

            SortedList<uint, object> dictRec = new SortedList<uint, object>();
            foreach (string dirin in aldirs) {
                using (StreamReader rr = new StreamReader(Path.Combine(Path.Combine(dir, dirin), "rec.txt"), Encoding.ASCII)) {
                    string row;
                    while (null != (row = rr.ReadLine()) && row.Length != 0) {
                        dictRec[Convert.ToUInt32(row, 16)] = null;
                    }
                }
                if (File.Exists(Path.Combine(Path.Combine(dir, dirin), "recman.txt"))) {
                    using (StreamReader rr = new StreamReader(Path.Combine(Path.Combine(dir, dirin), "recman.txt"), Encoding.ASCII)) {
                        string row;
                        while (null != (row = rr.ReadLine()) && row.Length != 0) {
                            dictRec[Convert.ToUInt32(row, 16)] = null;
                        }
                    }
                }
            }

            CodeNamespace nsc = new CodeNamespace("ee1Dec.C");

            CodeTypeDeclaration cls1 = new CodeTypeDeclaration("Mob" + tracPrefix);
            cls1.IsPartial = true;
            nsc.Types.Add(cls1);

            CodeNamespaceImport nsi = new CodeNamespaceImport("System.Collections.Generic");
            nsc.Imports.Add(nsi);

            List<uint> alppc = new List<uint>();

            for (int pi = 0; pi < dictRec.Count; pi++) {
                uint addr = dictRec.Keys[pi];
                alppc.Add(addr);

                CodeMemberMethod fn;
                Myrec.Rec1(addr, out fn, sieeMem);
                cls1.Members.Add(fn);
            }

            CodeMemberField fee;
            if (true) {
                fee = new CodeMemberField(typeof(CustEE), "ee");
                fee.InitExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(CustEE)));
                cls1.Members.Add(fee);

                CodeTypeReference tdicti2a = new CodeTypeReference("SortedDictionary");
                tdicti2a.TypeArguments.Add(new CodeTypeReference(typeof(uint)));
                tdicti2a.TypeArguments.Add(typeof(MobUt.Tx8));
                CodeMemberField fdicti2a = new CodeMemberField(tdicti2a, "dicti2a");
                fdicti2a.InitExpression = new CodeObjectCreateExpression(tdicti2a);
                cls1.Members.Add(fdicti2a);
            }

            if (true) {
                CodeMemberMethod initstate = new CodeMemberMethod();
                initstate.Name = "initstate";
                cls1.Members.Add(initstate);

                initstate.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "initregs")));
                initstate.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "initfns")));
            }

            if (true) {
                CodeMemberMethod initregs = new CodeMemberMethod();
                initregs.Name = "initregs";
                cls1.Members.Add(initregs);

                for (int t = 0; t < 32; t++) {
                    IsUt.SetGPR(initregs.Statements, "r0:at:v0:v1:a0:a1:a2:a3:t0:t1:t2:t3:t4:t5:t6:t7:s0:s1:s2:s3:s4:s5:s6:s7:t8:t9:k0:k1:gp:sp:s8:ra".Substring(3 * t, 2), this.e.GPR[t]);
                }
                for (int t = 0; t < 32; t++) {
                    IsUt.SetFPR(initregs.Statements, t, this.e.fpr[t]);
                }
                IsUt.SetFPRacc(initregs.Statements, this.e.fpracc);

                for (int t = 0; t < 32; t++) {
                    IsUt.SetVF(initregs.Statements, t, this.e.VF[t]);
                }
            }
            if (true) {
                CodeMemberMethod initfns = new CodeMemberMethod();
                initfns.Name = "initfns";
                cls1.Members.Add(initfns);

                foreach (uint ppc in alppc) {
                    // dicti2a[0x011B420] = new Txxxxxxxx(_011B420);

                    CodeAssignStatement stmt = new CodeAssignStatement(
                        new CodeArrayIndexerExpression(
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "dicti2a"),
                            new CodePrimitiveExpression(ppc)
                            ),
                        new CodeObjectCreateExpression(
                            typeof(MobUt.Tx8),
                            new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), LabUt.addr2Funct(ppc))
                            )
                    );
                    initfns.Statements.Add(stmt);
                }
            }

            if (true) {
                CodeDomProvider csc = CodeDomProvider.CreateProvider("cs");
                StringWriter wr = new StringWriter();
                csc.GenerateCodeFromNamespace(nsc, wr, new CodeGeneratorOptions());

                File.WriteAllText(Path.Combine(dirExpSim, "tocs." + tracPrefix + ".cs"), wr.ToString(), Encoding.UTF8);
            }
#if false
            if (true) {
                CodeDomProvider csc = CodeDomProvider.CreateProvider("vb");
                StringWriter wr = new StringWriter();
                csc.GenerateCodeFromType(cls1, wr, new CodeGeneratorOptions());

                File.WriteAllText(Path.Combine(dirExpSim, "tocs.vb"), wr.ToString(), Encoding.UTF8);
            }
            if (true) {
                CodeDomProvider csc = CodeDomProvider.CreateProvider("cpp");
                StringWriter wr = new StringWriter();
                csc.GenerateCodeFromType(cls1, wr, new CodeGeneratorOptions());

                File.WriteAllText(Path.Combine(dirExpSim, "tocs.h"), wr.ToString(), Encoding.UTF8);
            }
#endif

            MessageBox.Show("It succeeded");
        }

        private void buttonDEB2_Click(object sender, EventArgs e) {
#if false
            File.WriteAllBytes(Path.Combine(dirExpSim, "eeram'.bin"), this.e.ram);
#endif
#if false
            Myrec.Privrec1(0x0030051C, sieeMem, dirExpSim, @"");
#endif
        }

        private void notifyIconResult_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                WindowState = FormWindowState.Normal;
            }
            Activate();
        }

        private void listViewMMap_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            int off = e.ItemIndex * 16;
            ResHT res = pht.findRes(off);
            ListViewItem lvi = new ListViewItem();
            string text = off.ToString("x7") + " ";
            if (res != null) {
                text += res.memmemo;
            }
            lvi.Text = text;
            e.Item = lvi;
        }

        private void listViewMMap_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 'g') {
                e.Handled = true;
                string s = Interaction.InputBox("addr?", "", "", -1, -1);
                if (s.Length != 0) {
                    try {
                        int addr = (int)RiAddrUt.Resolve(s, this.e);
                        listViewMMap.TopItem = (listViewMMap.FocusedItem = listViewMMap.Items[addr / 16]);
                    }
                    catch (RiAddrUt.ERR) {
                    }
                }
                return;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Form form = new Form();
            form.Text = "Colors";
            form.Location = this.Location;
            form.Size = this.Size;
            form.StartPosition = FormStartPosition.Manual;
            ImageList il = new ImageList();
            il.ImageSize = new Size(16, 16);
            ListView lv = new ListView();
            lv.Parent = form;
            lv.Dock = DockStyle.Fill;
            lv.View = View.List;
            lv.SmallImageList = il;
            lv.Show();

            SortedDictionary<Color, String> clrs = new SortedDictionary<Color, string>(new StandardColorComparer());
            foreach (PropertyInfo pi in typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static)) {
                Color clr = (Color)pi.GetValue(null, null);
                clrs[clr] = pi.Name;
            }
            foreach (KeyValuePair<Color, String> kv in clrs) {
                Color clr = kv.Key;
                ListViewItem lvi = lv.Items.Add(kv.Value);
                Bitmap pic = new Bitmap(16, 16, PixelFormat.Format24bppRgb);
                using (Graphics cv = Graphics.FromImage(pic)) {
                    cv.Clear(clr);
                }
                il.Images.Add(pic);
                lvi.ImageIndex = il.Images.Count - 1;
            }
            form.Show();
        }

        private void buttonCopyseled_Click(object sender, EventArgs e) {
            StringWriter wr = new StringWriter();
            foreach (int i in listView1.SelectedIndices) {
                RetrieveVirtualItemEventArgs a = new RetrieveVirtualItemEventArgs(i);
                listView1_RetrieveVirtualItem(sender, a);
                ListViewItem lvi = a.Item;
                wr.WriteLine("//#" + lvi.Text + "  " + lvi.SubItems[1].Text);
            }
            var text = wr.ToString();
            if (text.Length != 0) {
                Clipboard.SetText(text);
                MessageBox.Show("Copied");
            }
        }

        private void outputSxyzRxyzTxyzToolStripMenuItem_Click(object sender, EventArgs e) {
            String saddr = Interaction.InputBox("addr InfoTbl?", "", "1abb590", -1, -1);
            if (saddr == "") return;

            sieeMem.Position = Convert.ToUInt32(saddr, 16) + 0x1CU;
            BinaryReader br = brMem;
            StringWriter wr = new StringWriter();
            uint Sxyz = br.ReadUInt32();
            uint Rxyz = br.ReadUInt32();
            uint Txyz = br.ReadUInt32();
            int cj = 0xE5;
            OUt.Wr(wr, Sxyz, br, "Sxyz", cj);
            OUt.Wr(wr, Rxyz, br, "Rxyz", cj);
            OUt.Wr(wr, Txyz, br, "Txyz", cj);
            Clipboard.SetText(wr.ToString());
        }

        private void buttonRUpd_Click(object sender, EventArgs e) {
            lbRecentpc.Items.Clear();
            for (int x = 0; r_pci - x - 1 >= 0 && x < 100; x++) {
                lbRecentpc.Items.Add(r_pcs[(r_pci - x - 1) & 1023].ToString("x7"));
            }
        }

        private void lbRecentpc_SelectedIndexChanged(object sender, EventArgs e) {
            String addr = lbRecentpc.SelectedItem as String;
            if (addr == null) return;

            int sel = Convert.ToInt32(addr, 16) >> 2;
            ListViewItem lvi = listView1.Items[sel];
            lvi.Focused = true;
            lvi.EnsureVisible();
            listView1.Update();
            for (int w = 0; w < 2; w++) {
                ControlPaint.FillReversibleRectangle(listView1.RectangleToScreen(lvi.GetBounds(ItemBoundsPortion.ItemOnly)), Color.White);
                if (w == 0) Thread.Sleep(222);
            }
        }

        private void createJMPTableToolStripMenuItem_Click(object sender, EventArgs e) {
            String addrs = Interaction.InputBox("0038,8368,9", "", "", -1, -1);
            if (addrs == "") return;

            String[] cols = addrs.Split(',');
            if (cols.Length != 3) return;

            int addr = (Convert.ToInt32(cols[0], 16) << 16) + (short)Convert.ToUInt16(cols[1], 16);
            int cnt = Convert.ToInt32(cols[2]);

            sieeMem.Position = addr;
            BinaryReader br = brMem;
            List<int> al = new List<int>();
            SortedDictionary<int, object> dict = new SortedDictionary<int, object>();
            for (int x = 0; x < cnt; x++) {
                int v;
                al.Add(v = br.ReadInt32());
                dict[v] = null;
            }

            {
                StringWriter wr = new StringWriter();
                String jtn = String.Format("JT{0:x7}", addr);
                wr.WriteLine("int[] {0} = {{", jtn);
                foreach (int at in al) {
                    wr.WriteLine(" 0x{0:x7},", at);
                }
                wr.WriteLine("};");
                wr.WriteLine();
                {
                    wr.Write("//{{}}{0}(", jtn);
                    for (int x = 0; x < al.Count; x++) {
                        if (x != 0) wr.Write(",");
                        wr.Write("{0:x7}", al[x]);
                    }
                    wr.Write(")");
                    wr.WriteLine();
                }
                wr.WriteLine("switch ({0}[xxx]) {{", jtn);
                foreach (int at in dict.Keys) {
                    wr.WriteLine(" case 0x{0:x7}: {{", at);
                    wr.WriteLine("  break;");
                    wr.WriteLine(" }");
                }
                wr.WriteLine("}");

                Clipboard.SetText(wr.ToString());
                MessageBox.Show(this, "Copied to clipboard", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDump_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.bin|*.bin||";
            sfd.FileName = "dump.bin";
            if (sfd.ShowDialog(this) == DialogResult.OK) {
                using (FileStream fs = File.Create(sfd.FileName)) {
                    int ffrm = Convert.ToInt32(tbfrm.Text, 16);
                    int fto = Convert.ToInt32(tbto.Text, 16);
                    fs.Write(
                        this.e.ram, ffrm, fto - ffrm
                        );
                    MessageBox.Show(this, "Ok", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
