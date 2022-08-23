using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WalkeeGPr {
    public partial class GForm : Form {
        public GForm() {
            InitializeComponent();
        }

        String fpcs = @"H:\Proj\khkh_xldM\PortSCalc\CALCB2.cs";

        String[] Lns { get { return File.ReadAllLines(fpcs); } }

        SortedDictionary<string, string> dictfn2rows = new SortedDictionary<string, string>();

        private void Form1_Load(object sender, EventArgs e) {
            Reload(null, -1, -1);
        }

        void Reload(String func, int curlin, int sellin) {
            StringWriter wr = null;
            foreach (String row in Lns) {
                Match M;
                if (false) { }
                else if ((M = Regex.Match(row, "//\\{\\}S_(?<fn>\\w+)$")).Success) {
                    wr = new StringWriter();
                    wr.WriteLine(row);
                }
                else if ((M = Regex.Match(row, "//\\{\\}E_(?<fn>\\w+)$")).Success) {
                    wr.WriteLine(row);
                    dictfn2rows[M.Groups["fn"].Value] = wr.ToString();
                    wr = null;
                }
                else if (wr != null) {
                    wr.WriteLine(row);
                }
            }

            cbfn.Items.Clear();
            foreach (String fn in dictfn2rows.Keys) {
                cbfn.Items.Add(new Itemfn(fn, dictfn2rows[fn]));
            }

            {
                int i = (func != null) ? cbfn.Items.IndexOf(func) : 0;
                if (i < 0) i = 0;
                cbfn.SelectedIndex = i;
            }

            {
                if (curlin >= 0) {
                    lvS.TopItem = lvS.Items[curlin];
                }

                if (sellin >= 0) {
                    lvS.Items[sellin].Selected = true;
                    lvS.Items[sellin].Focused = true;
                }
            }

            WalkbyReg("s2");
        }

        class Itemfn {
            public String fn, rows;

            public Itemfn(String fn, String rows) {
                this.fn = fn;
                this.rows = rows;
            }

            public override string ToString() {
                return fn;
            }
        }

        private void cbfn_SelectedIndexChanged(object sender, EventArgs e) {
            Itemfn o = (Itemfn)cbfn.SelectedItem;
            if (o == null) return;
            lvS.Items.Clear();
            foreach (String row in o.rows.Split('\n')) {
                Match M = Regex.Match(row, "^\\s+//#(?<a>[0-9a-f]{7})\\s+(?<o>.+?)(?:;.+)?$");
                bool isAsm = M.Success;
                ListViewItem lvi = lvS.Items.Add(row);
                if (!isAsm) {
                    lvi.ForeColor = Color.LightGray;
                }
                else {
                    //#0129a2c  DADDU s0, a0, zero      ; s0=offMset
                    int addr = Convert.ToInt32(M.Groups["a"].Value, 16);
                    String code = Cleaner.Clean(M.Groups["o"].Value.Trim());
                    AL al = new AL(addr, code);
                    lvi.Tag = al;
                }
            }
        }

        class Cleaner {
            public static string Clean(String s) {
                return s.Split('!')[0];
            }
        }

        /// <summary>
        /// asm lang
        /// </summary>
        class AL {
            public int addr;
            public String code;

            public AL(int addr, String code) {
                this.addr = addr;
                this.code = code;
            }

            /// <summary>
            /// Opecode in lower case
            /// </summary>
            public String Opec { get { return code.Split(' ')[0].Trim().ToLowerInvariant(); } }

            /// <summary>
            /// Opecode (without broadcast part) in lower case
            /// </summary>
            public String VOpec { get { return new V(Opec).vopec; } }

            class V {
                static String[] alvbc = "vadda,vadd,vmadda,vmadd,vmax,vmini,vmsuba,vmsub,vmula,vmul,vsuba,vsub".Split(',');

                public String vopec, bc = "", dest = "";

                public V(String opec) {
                    foreach (string vbc in alvbc) {
                        if (opec.StartsWith(vbc)) {
                            String[] almore = opec.Substring(vbc.Length).Split('.');
                            bc = almore[0];
                            if (bc == "q") {
                                dest = almore[1];
                                vopec = vbc + "q";
                                return;
                            }
                            if (bc != "") {
                                dest = almore[1];
                                vopec = vbc + "bc";
                                return;
                            }
                            break;
                        }
                    }
                    if (opec.StartsWith("v") && opec.Contains(".")) {
                        String[] almore = opec.Split('.');
                        vopec = almore[0];
                        dest = almore[1];
                        return;
                    }
                    vopec = opec;
                }
            }

            public String Dest { get { return new V(Opec).dest; } }

            public String BC { get { return new V(Opec).bc; } }

            /// <summary>
            /// Operand in lower case
            /// </summary>
            public String[] Oper {
                get {
                    List<string> al = new List<string>();
                    foreach (String col in code.Split(new char[] { ' ' }, 2)[1].Split(',')) {
                        al.Add(col.Trim().ToLowerInvariant());
                    }
                    return al.ToArray();
                }
            }
        }

        AL CurAL {
            get {
                foreach (ListViewItem lvi in lvS.SelectedItems) {
                    AL al = (AL)lvi.Tag;
                    return al;
                }
                return null;
            }
        }

        static String[] REGNams { get { return "zero|at|v0|v1|a0|a1|a2|a3|t0|t1|t2|t3|t4|t5|t6|t7|s0|s1|s2|s3|s4|s5|s6|s7|t8|t9|k0|k1|gp|sp|s8|ra".Split('|'); } }

        private void cmsTrackr_Opening(object sender, CancelEventArgs e) {
            cmsTrackr.Items.Clear();
            AL al = CurAL;
            if (al == null) { e.Cancel = true; return; }
            foreach (String nam in REGNams) {
                if (Regex.IsMatch(al.code, "\\b" + nam + "\\b")) {
                    cmsTrackr.Items.Add(nam, null, ClickGPr);
                }
            }
            for (int x = 0; x < 32; x++) {
                String nam = "$f" + x;
                if (Regex.IsMatch(al.code, Regex.Escape(nam) + "\\b")) {
                    cmsTrackr.Items.Add(nam, null, ClickGPr);
                }
            }
            for (int x = 0; x < 32; x++) {
                String nam = "vf" + x;
                if (Regex.IsMatch(al.code, "\\b" + nam)) {
                    for (int w = 0; w < 4; w++)
                        cmsTrackr.Items.Add(nam + "xyzw"[w], null, ClickGPr);
                }
            }
        }

        class Walkal {
            SortedDictionary<int, AL> dicta2al = new SortedDictionary<int, AL>();
            int pc0 = -1;

            public Walkal(ListView lvS) {
                foreach (ListViewItem lvi in lvS.Items) {
                    AL al = (AL)lvi.Tag;
                    if (al == null) continue;
                    if (pc0 < 0) pc0 = al.addr;
                    if (!dicta2al.ContainsKey(al.addr)) dicta2al[al.addr] = al;
                }
            }

            public int PC0 { get { return pc0; } }

            public AL Get(int pc) {
                if (dicta2al.ContainsKey(pc))
                    return dicta2al[pc];
                return null;
            }
        }

        void ClickGPr(object sender, EventArgs e) {
            ToolStripItem tsi = (ToolStripItem)sender;
            String regn = tsi.Text;
            WalkbyReg(regn);

            lvS_SelectedIndexChanged(lvS, e);

            {
                AL al = CurAL;
                string refera, refval;
                if (recw.Lookup(al.addr, out refera, out refval)) {
                    foreach (ListViewItem lvi in lvS.Items) {
                        lvi.ImageIndex = -1;

                        AL o = lvi.Tag as AL;
                        if (o == null) continue;

                        string era, val;
                        if (al != null) {
                            if (recw.Lookup(o.addr, out era, out val)) {
                                lvi.BackColor = (era == refera) ? Color.FromKnownColor(KnownColor.Info) : lvS.BackColor;

                                Accessrec.V v = ar.Get(o.addr, regn);
                                lvi.ImageIndex = v.isw ? 0 : -1;
                            }
                        }
                    }
                }
                else {
                    foreach (ListViewItem lvi in lvS.Items) {
                        lvi.BackColor = lvS.BackColor;
                        lvi.ImageIndex = -1;
                    }
                }
            }
        }

        class DUt {
            public static string Getx(int siz, SortedDictionary<string, string> memval, string key) {
                key = Ute.Simplify(key);
                if (!memval.ContainsKey(key)) {
                    Debug.WriteLine("memval: " + key);
                    //Debug.Fail("memval: " + key);
                    return ((siz > 0) ? ",Getb,Gethw,,Getw,,,,Getd,,,,,,,,Getq" : ",,,,Getf").Split(',')[Math.Abs(siz)] + "(" + key + ")";
                }
                return memval[key];
            }
        }

        class Workq {
            public int pc;
            public SortedDictionary<string, string> regval;
            public SortedDictionary<string, string> memval;

            public Workq(int pc, SortedDictionary<string, string> regval, SortedDictionary<string, string> memval) {
                this.pc = pc;
                this.regval = regval;
                this.memval = memval;
            }
        }

        class Utrecwalk {
            List<string> alhist = new List<string>();
            SortedDictionary<int, string> dict = new SortedDictionary<int, string>();

            public bool Lookup(int pc, out string era, out string val) {
                if (dict.ContainsKey(pc)) {
                    val = dict[pc];
                    era = alhist.IndexOf(val).ToString();
                    return true;
                }
                era = "";
                val = "";
                return false;
            }

            public void WatchREG(int pc, string regn, string val) {
                int p = alhist.IndexOf(val);
                if (p < 0) {
                    alhist.Add(val);
                    p = alhist.Count - 1;
                }
                dict[pc] = val;
            }
        }

        delegate void _WatchREG(int pc, string regn, string val);

        Utrecwalk recw = new Utrecwalk();

        void WalkbyReg(string regn) {
            recw = new Utrecwalk();
            WalkbyReg(regn, recw.WatchREG);
            tsslTarreg.Text = regn;
        }

        class Ute {
            public static string Simplify(String s) {
                ELeaf l = new CExpr(s).v;
                l = Walk(l);
                return l.ToString();
            }

            private static ELeaf Walk(ELeaf l) {
                ELeaf lv = l.lv, rv = l.rv;

                if (l.t == ELT.BMinus || l.t == ELT.BPlus) {
                    if (rv.t == ELT.Int) {
                        int t = 0;
                        lv = CollectVal(lv, ref t);
                        t += (l.t == ELT.BMinus) ? (-rv.i) : (+rv.i);
                        if (lv == null) {
                            return new ELeaf(t);
                        }
                        if (t == 0) {
                            return lv;
                        }
                        if (t < 0) {
                            l.t = ELT.BMinus;
                            rv.i = -t;
                        }
                        else {
                            l.t = ELT.BPlus;
                            rv.i = t;
                        }
                    }
                }
                if (l.t == ELT.BPlus) {
                    if (lv.t == ELT.Str && lv.s == "offMset.mhdr") {
                        if (rv.t == ELT.Str) {
                            if (rv.s == "offt3") return new ELeaf("T3ptr");
                            if (rv.s == "offt4") return new ELeaf("T4ptr");
                            if (rv.s == "offt5") return new ELeaf("T5ptr");
                            if (rv.s == "offt7") return new ELeaf("T7ptr");
                        }
                    }
                }

                if (lv != null) lv = Walk(lv);
                if (rv != null) rv = Walk(rv);

                l.lv = lv;
                l.rv = rv;

                for (int x = 0; x < l.ala.Count; x++) {
                    l.ala[x] = Walk(l.ala[x]);
                }
                return l;
            }

            private static ELeaf CollectVal(ELeaf l, ref int t) {
                if (l.t == ELT.BMinus || l.t == ELT.BPlus) {
                    if (l.rv.t == ELT.Int) {
                        t += (l.t == ELT.BMinus) ? (-l.rv.i) : (+l.rv.i);
                        return CollectVal(l.lv, ref t);
                    }
                }
                if (l.t == ELT.Int) {
                    t += l.i;
                    return null;
                }
                return l;
            }
        }

        class Accessrec {
            /// <summary>
            /// Write to register
            /// </summary>
            /// <param name="regn">cop2ånÇ∆pcÇÕèúÇ≠</param>
            /// <returns></returns>
            public String WR(String regn) {
                K k = new K(pc, regn);
                V v = dict.ContainsKey(k) ? dict[k] : new V();
                dict[k] = v;

                v.isw = true;

                return regn;
            }

            public void Clear() {
                dict.Clear();
            }

            public int pc = 0;

            class C : IComparer<K> {
                #region IComparer<K> ÉÅÉìÉo

                public int Compare(K x, K y) {
                    int t;
                    t = x.pc.CompareTo(y.pc); if (t != 0) return t;
                    t = x.var.CompareTo(y.var); if (t != 0) return t;
                    return 0;
                }

                #endregion
            }

            public SortedDictionary<K, V> dict = new SortedDictionary<K, V>(new C());

            public class V {
                public bool isr = false, isw = false;
            }
            public struct K {
                public int pc;
                public string var;

                public K(int pc, string var) {
                    this.pc = pc;
                    this.var = var;
                }

                public override string ToString() {
                    return pc.ToString("x7") + " " + var;
                }
            }

            public V Get(int pc, String regn) {
                K k = new K(pc, regn);
                if (dict.ContainsKey(k))
                    return dict[k];
                return new V();
            }
        }

        Accessrec ar = new Accessrec();

        private void WalkbyReg(string regn, _WatchREG func) {
            Walkal w = new Walkal(lvS);
            int pc = w.PC0;
            SortedDictionary<int, object> dictfp = new SortedDictionary<int, object>(); // foot print. already walked

            SortedDictionary<string, string> regval = new SortedDictionary<string, string>(); // dict regval
            foreach (String nam in REGNams) regval[nam] = "org_" + nam;
            for (int x = 0; x < 32; x++) regval["$f" + x] = "0";
            for (int x = 0; x < 32; x++) for (int rw = 0; rw < 4; rw++) regval["vf" + x + "xyzw"[rw]] = "0";
            regval["a0"] = "offMset";
            regval["a1"] = "InfoTbl";
            regval["q"] = "0";
            //regval["s0"] = "offMset.mhdr";

            SortedDictionary<string, string> memval = new SortedDictionary<string, string>(); // dict memval
            memval[Ute.Simplify("(offMset)+4")] = "offMset.mhdr";
            memval[Ute.Simplify("(offMset)+8")] = "tmp8";
            memval[Ute.Simplify("(offMset)+12")] = "tmp9";
            memval[Ute.Simplify("(offMset)+16")] = "tmpa";
            memval[Ute.Simplify("(offMset)+20")] = "tmpb";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0012)")] = "cntt4";
            memval[Ute.Simplify("(offMset.mhdr)+(0x001c)")] = "offt4";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0018)")] = "offt5";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0054)")] = "cntt3";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0050)")] = "offt3";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0060)")] = "offt7";
            memval[Ute.Simplify("(offMset.mhdr)+(0x0064)")] = "cntt7";
            memval[Ute.Simplify("(InfoTbl+0x14)")] = "offMdlx04";
            memval[Ute.Simplify("(InfoTbl+0x1c)")] = "Sxyz";
            memval[Ute.Simplify("(InfoTbl+0x20)")] = "Rxyz";
            memval[Ute.Simplify("(InfoTbl+0x24)")] = "Txyz";
            memval[Ute.Simplify("(InfoTbl+0x28)")] = "tmp3";
            memval[Ute.Simplify("(InfoTbl+0x2c)")] = "tmp4";
            memval[Ute.Simplify("((offMset.mhdr)+(16))")] = "cjMdlx";
            memval[Ute.Simplify("T3ptr")] = "T3.b0";
            memval[Ute.Simplify("T3ptr+1")] = "T3.b1";
            memval[Ute.Simplify("T3ptr+2")] = "T3.hw2";
            memval[Ute.Simplify("T4ptr")] = "T4.w0";
            memval[Ute.Simplify("T7ptr+2")] = "T7.hw2";
            memval[Ute.Simplify("offMdlx04")] = "offMdlx04.vtbl";
            memval[Ute.Simplify("offMdlx04+4")] = "offMdlx04.mhdr";
            memval[Ute.Simplify("offMdlx04.mhdr+32")] = "offAxBone";

            Queue<Workq> q = new Queue<Workq>();
            q.Enqueue(new Workq(pc, regval, memval));
            ar.Clear();
            while (q.Count != 0) {
                Workq wq = q.Dequeue();
                pc = wq.pc;
                regval = wq.regval;
                memval = wq.memval;
                int delayslot = 0;
                int newpc = -1;
                bool mustgo = false;
                bool likely = false;
                while (true) {
                    AL al = w.Get(pc);
                    if (al == null) break;
                    if (dictfp.ContainsKey(pc)) break;
                    regval["zero"] = "0";
                    regval["vf0x"] = "0";
                    regval["vf0y"] = "0";
                    regval["vf0z"] = "0";
                    regval["vf0w"] = "1";
                    regval["$vi22"] = regval["q"];
                    ar.pc = pc;
                    //
                    switch (al.VOpec) {
                        case "addiu": { // ADDIU sp, sp, $fd00     
                                regval[ar.WR(al.Oper[0])] = "(" + regval[al.Oper[1]] + ")" + SCUt.DH2si16s(al.Oper[2]);
                                break;
                            }
                        case "sd": { // SD s0, $02a0(sp)        
                                memval[Ute.Simplify(SCUt.RI2s(regval, al.Oper[1]))] = regval[al.Oper[0]];
                                break;
                            }
                        case "addu":
                        case "daddu": { // DADDU s0, a0, zero  
                                regval[ar.WR(al.Oper[0])] = Ute.Simplify(string.Format("({0})+({1})", regval[al.Oper[1]], regval[al.Oper[2]]));
                                break;
                            }
                        case "subu": { // SUBU t7, s2, t4
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})-({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "swc1": { // SWC1 $f20, $02f0(sp)
                                memval[Ute.Simplify(SCUt.RI2s(regval, al.Oper[1]))] = regval[al.Oper[0]];
                                break;
                            }
                        case "lwc1": { // LWC1 $f0, $0000(t4)
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(-4, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "lbu": { // LBU t7, $0001(s1)
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(1, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "lw": { // LW t3, $0004(a0)   
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(4, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "lq": { // LQ t0, $0000(t6)
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(16, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "lhu": { // LHU t7, $0012(t6)  
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(2, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "lh": { // LH v0, $0004(v0)
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(2, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "ld": { // LD s0, $02a0(sp)  
                                regval[ar.WR(al.Oper[0])] = DUt.Getx(8, memval, SCUt.RI2s(regval, al.Oper[1]));
                                break;
                            }
                        case "mov.s": { // MOV.S $f1, $f0
                                regval[ar.WR(al.Oper[0])] = regval[al.Oper[1]];
                                break;
                            }
                        case "mfc1": { // MFC1 t0, $f0
                                regval[ar.WR(al.Oper[0])] = regval[al.Oper[1]];
                                break;
                            }
                        case "mtc1": { // MTC1 t0, $f0
                                regval[ar.WR(al.Oper[1])] = regval[al.Oper[0]];
                                break;
                            }

                        case "sw": { // SW a2, $01f0(sp)
                                memval[Ute.Simplify(SCUt.RI2s(regval, al.Oper[1]))] = regval[al.Oper[0]];
                                break;
                            }
                        case "sq": { // SQ t0, $0000(t4)
                                memval[Ute.Simplify(SCUt.RI2s(regval, al.Oper[1]))] = regval[al.Oper[0]];
                                break;
                            }
                        case "qmtc2": { // QMTC2 t0, vf1
                                regval[al.Oper[1] + "x"] = string.Format("Readx({0})", regval[al.Oper[0]]);
                                regval[al.Oper[1] + "y"] = string.Format("Ready({0})", regval[al.Oper[0]]);
                                regval[al.Oper[1] + "z"] = string.Format("Readz({0})", regval[al.Oper[0]]);
                                regval[al.Oper[1] + "w"] = string.Format("Readw({0})", regval[al.Oper[0]]);
                                break;
                            }
                        case "qmfc2": { // QMFC2 t0, vf5
                                regval[ar.WR(al.Oper[0])] = string.Format("qmfc2({0},{1},{2},{3})"
                                    , regval[al.Oper[1] + "x"]
                                    , regval[al.Oper[1] + "y"]
                                    , regval[al.Oper[1] + "z"]
                                    , regval[al.Oper[1] + "w"]
                                    );
                                break;
                            }

                        case "div.s": { // DIV.S $f0, $f0, $f1
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})/({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }

                        case "mult": { // MULT t6, t6, t7 
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})*({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "and": { // AND s4, s4, t7
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})&({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "movn": { // MOVN t5, t6, t7
                                regval[ar.WR(al.Oper[0])] = string.Format("({2})?({1}):({0})", regval[al.Oper[0]], regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "xor": { // XOR t7, t6, t7
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})^({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "sll": { // SLL t7, t7, 3  
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})<<{1}", regval[al.Oper[1]], al.Oper[2]);
                                break;
                            }
                        case "srl": { // SRL t3, s4, 16
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})>>{1}", regval[al.Oper[1]], al.Oper[2]);
                                break;
                            }
                        case "andi": { // ANDI t7, t7, $00ff
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})&{1}", regval[al.Oper[1]], SCUt.DH2CH(al.Oper[2]));
                                break;
                            }
                        case "ori": { // ORI t7, t7, $ffff
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})|{1}", regval[al.Oper[1]], SCUt.DH2CH(al.Oper[2]));
                                break;
                            }
                        case "lui": { // LUI t6, $0038
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})", SCUt.DH2CH(al.Oper[1]) + "0000");
                                break;
                            }
                        case "slt": { // SLT t7, s2, t3
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})<({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "slti":
                        case "sltiu": { // SLTIU t7, t6, $0009
                                regval[ar.WR(al.Oper[0])] = string.Format("({0})<{1}", regval[al.Oper[1]], SCUt.DH2ui16s(al.Oper[2]));
                                break;
                            }

                        case "jal": { // JAL $0011b420 
                                int jal = SCUt.DH2i(al.Oper[0]);
                                newpc = jal;
                                delayslot = 1;
                                break;
                            }
                        case "bnel":
                        case "beql": { // BEQL t7, zero, $0012a4c0
                                int go = SCUt.DH2i(al.Oper[2]);
                                mustgo = al.Oper[1] == al.Oper[0];
                                newpc = (go);
                                delayslot = 1;
                                likely = true;
                                break;
                            }
                        case "bne":
                        case "beq": { // BEQ s6, t3, $0012d4ec  
                                int go = SCUt.DH2i(al.Oper[2]);
                                mustgo = al.Oper[1] == al.Oper[0];
                                newpc = (go);
                                delayslot = 1;
                                break;
                            }
                        case "bltz": { // BLTZ t7, $0012d33c
                                int go = SCUt.DH2i(al.Oper[1]);
                                newpc = (go);
                                delayslot = 1;
                                break;
                            }
                        case "jr": { // JR t5
                                delayslot = 1;
                                mustgo = true;
                                break;
                            }
                        case "jalr": { // JALR ra, v0
                                delayslot = 1;
                                break;
                            }
                        case "vwaitq":
                        case "vnop":
                        case "nop": {
                                break;
                            }
                        case "lqc2": { // LQC2 vf1, $0000(t7)
                                regval[al.Oper[0] + "x"] = DUt.Getx(-4, memval, string.Format("({0})+(0)", SCUt.RI2s(regval, al.Oper[1])));
                                regval[al.Oper[0] + "y"] = DUt.Getx(-4, memval, string.Format("({0})+(4)", SCUt.RI2s(regval, al.Oper[1])));
                                regval[al.Oper[0] + "z"] = DUt.Getx(-4, memval, string.Format("({0})+(8)", SCUt.RI2s(regval, al.Oper[1])));
                                regval[al.Oper[0] + "w"] = DUt.Getx(-4, memval, string.Format("({0})+(12)", SCUt.RI2s(regval, al.Oper[1])));
                                break;
                            }
                        case "sqc2": { // SQC2 vf5, $0000(t8)
                                memval[Ute.Simplify(string.Format("({0})+(0)", SCUt.RI2s(regval, al.Oper[1])))] = regval[al.Oper[0] + "x"];
                                memval[Ute.Simplify(string.Format("({0})+(4)", SCUt.RI2s(regval, al.Oper[1])))] = regval[al.Oper[0] + "y"];
                                memval[Ute.Simplify(string.Format("({0})+(8)", SCUt.RI2s(regval, al.Oper[1])))] = regval[al.Oper[0] + "z"];
                                memval[Ute.Simplify(string.Format("({0})+(12)", SCUt.RI2s(regval, al.Oper[1])))] = regval[al.Oper[0] + "w"];
                                break;
                            }
                        case "cfc2": { // CFC2 t0, $vi22
                                regval[ar.WR(al.Oper[0])] = regval[al.Oper[1]];
                                break;
                            }

                        case "pextlw": { // PEXTLW t4, t1, t0
                                regval[ar.WR(al.Oper[0])] = string.Format("pextlw({0},{1})"
                                , regval[al.Oper[1]]
                                , regval[al.Oper[2]]
                                );
                                break;
                            }
                        case "pextuw": { // PEXTUW t5, t1, t0
                                regval[ar.WR(al.Oper[0])] = string.Format("pextuw({0},{1})"
                                , regval[al.Oper[1]]
                                , regval[al.Oper[2]]
                                );
                                break;
                            }
                        case "pcpyld": { // PCPYLD t0, t6, t4
                                regval[ar.WR(al.Oper[0])] = string.Format("pcpyld({0},{1})"
                                , regval[al.Oper[1]]
                                , regval[al.Oper[2]]
                                );
                                break;
                            }
                        case "pcpyud": { // PCPYUD t1, t4, t6
                                regval[ar.WR(al.Oper[0])] = string.Format("pcpyud({0},{1})"
                                , regval[al.Oper[1]]
                                , regval[al.Oper[2]]
                                );
                                break;
                            }


                        case "vaddbc": { // VADDy.x vf8, vf8, vf8y
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})+({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})+({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})+({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})+({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vaddq": { // VADDq.x vf2, vf0, Q
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})+({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})+({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})+({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})+({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vsub": { // VSUB.xyz vf4, vf0, vf4
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})-({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2] + "x"]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})-({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2] + "y"]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})-({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2] + "z"]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})-({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2] + "w"]);
                                break;
                            }
                        case "vmul": { // VMUL.xyzw vf1, vf1, vf2
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})*({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2] + "x"]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})*({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2] + "y"]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})*({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2] + "z"]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})*({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2] + "w"]);
                                break;
                            }
                        case "vmove": { // VMOVE.w vf5, vf0
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = regval[al.Oper[1] + "x"];
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = regval[al.Oper[1] + "y"];
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = regval[al.Oper[1] + "z"];
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = regval[al.Oper[1] + "w"];
                                break;
                            }
                        case "vmulq": { // VMULq.xyzw vf1, vf1, Q
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})*({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})*({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})*({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})*({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vmulbc": { // VMULx.xyzw vf1, vf1, vf4x
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})*({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})*({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})*({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})*({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vmulabc": { // VMULAx.xyzw ACC, vf1, vf5x
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})*({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})*({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})*({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})*({1})", regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vmaddabc": { // VMADDAz.xyzw ACC, vf3, vf5z
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})+(({1})*({2}))", regval[al.Oper[0] + "x"], regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})+(({1})*({2}))", regval[al.Oper[0] + "y"], regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})+(({1})*({2}))", regval[al.Oper[0] + "z"], regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})+(({1})*({2}))", regval[al.Oper[0] + "w"], regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vmaddbc": { // VMADDw.xyzw vf5, vf4, vf5w
                                String dest = al.Dest;
                                if (dest.Contains("x")) regval[al.Oper[0] + "x"] = string.Format("({0})+(({1})*({2}))", regval["acc" + "x"], regval[al.Oper[1] + "x"], regval[al.Oper[2]]);
                                if (dest.Contains("y")) regval[al.Oper[0] + "y"] = string.Format("({0})+(({1})*({2}))", regval["acc" + "y"], regval[al.Oper[1] + "y"], regval[al.Oper[2]]);
                                if (dest.Contains("z")) regval[al.Oper[0] + "z"] = string.Format("({0})+(({1})*({2}))", regval["acc" + "z"], regval[al.Oper[1] + "z"], regval[al.Oper[2]]);
                                if (dest.Contains("w")) regval[al.Oper[0] + "w"] = string.Format("({0})+(({1})*({2}))", regval["acc" + "w"], regval[al.Oper[1] + "w"], regval[al.Oper[2]]);
                                break;
                            }
                        case "vopmula": { // VOPMULA.xyz ACC, vf2, vf3
                                regval["acc" + "x"] = string.Format("({0})*({1})", regval[al.Oper[1] + "y"], regval[al.Oper[2] + "z"]);
                                regval["acc" + "y"] = string.Format("({0})*({1})", regval[al.Oper[1] + "z"], regval[al.Oper[2] + "x"]);
                                regval["acc" + "z"] = string.Format("({0})*({1})", regval[al.Oper[1] + "x"], regval[al.Oper[2] + "y"]);
                                break;
                            }
                        case "vopmsub": { // VOPMSUB.xyz vf5, vf3, vf2
                                regval["acc" + "x"] = string.Format("({0})-(({1})*({2}))", regval["acc" + "x"], regval[al.Oper[1] + "y"], regval[al.Oper[2] + "z"]);
                                regval["acc" + "y"] = string.Format("({0})-(({1})*({2}))", regval["acc" + "y"], regval[al.Oper[1] + "z"], regval[al.Oper[2] + "x"]);
                                regval["acc" + "z"] = string.Format("({0})-(({1})*({2}))", regval["acc" + "z"], regval[al.Oper[1] + "x"], regval[al.Oper[2] + "y"]);
                                break;
                            }
                        case "vdiv": { // VDIV Q, vf0w, vf8x
                                regval[al.Oper[0]] = string.Format("({0})/({1})", regval[al.Oper[1]], regval[al.Oper[2]]);
                                break;
                            }
                        case "vsqrt": { // VSQRT Q, vf2x
                                regval[al.Oper[0]] = string.Format("sqrt({0})", regval[al.Oper[1]]);
                                break;
                            }
                        default:
                            Trace.Fail(al.VOpec); throw new NotSupportedException(al.VOpec);
                    }
                    //
                    if (func != null) func(pc, regn, regval[regn]);
                    //
                    dictfp[pc] = null;
                    pc += 4;
                    if (delayslot == 1) {
                        delayslot = 2;

                        if (likely && !mustgo) {
                            q.Enqueue(new Workq(pc + 4, new SortedDictionary<string, string>(regval), new SortedDictionary<string, string>(memval)));
                        }
                    }
                    else if (delayslot == 2) {
                        if (likely) {
                            Debug.Assert(newpc >= 0);
                            q.Enqueue(new Workq(newpc, regval, memval));
                            break;
                        }
                        else {
                            if (newpc >= 0)
                                q.Enqueue(new Workq(newpc, new SortedDictionary<string, string>(regval), new SortedDictionary<string, string>(memval)));
                            if (mustgo)
                                break;
                        }
                    }
                }
            }
        }

        class SCUt {
            /// <summary>
            /// REG+imm to str
            /// </summary>
            /// <param name="regval"></param>
            /// <param name="oper1"></param>
            /// <returns></returns>
            public static string RI2s(SortedDictionary<string, string> regval, string oper1) {
                Match M = Regex.Match(oper1, "(?<I>\\$[0-9a-f]{4})\\((?<R>\\w+)\\)");
                Trace.Assert(M.Success, oper1);
                return string.Format("({0}){1}", regval[M.Groups["R"].Value], DH2si16s(M.Groups["I"].Value));
            }

            /// <summary>
            /// dollar hex to C hex
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static string DH2CH(string s) {
                Trace.Assert(s.StartsWith("$"));
                return "0x" + Convert.ToUInt16(s.Substring(1), 16).ToString("x4");
            }

            public static int DH2i(string s) {
                Trace.Assert(s.StartsWith("$"));
                return Convert.ToInt32(s.Substring(1), 16);
            }

            public static string DH2si16s(string s) {
                Trace.Assert(s.StartsWith("$"));
                return ((short)Convert.ToInt32(s.Substring(1), 16)).ToString("+0;-0");
            }

            public static string DH2ui16s(string s) {
                Trace.Assert(s.StartsWith("$"));
                return ((ushort)Convert.ToInt32(s.Substring(1), 16)).ToString("0");
            }
        }

        private void lvS_SelectedIndexChanged(object sender, EventArgs e) {
            AL al = CurAL;
            string era = "", val = "";
            if (al != null) {
                if (recw.Lookup(al.addr, out era, out val)) {
                }
            }
            tsslREGval.Text = (val.Length > 0) ? string.Format("{0}     era:{1}", val.Replace("&", "&&"), era) : "...";
        }

        private void lvS_KeyDown(object sender, KeyEventArgs e) {
            if (e.Shift || e.Alt || e.Control)
                return;
            if (e.Handled)
                return;
            if (e.KeyCode == Keys.F5) {
                int topi = lvS.TopItem.Index;
                int seli = -1;
                foreach (ListViewItem lvi in lvS.SelectedItems) {
                    seli = lvi.Index;
                    break;
                }
                Reload(Convert.ToString(cbfn.SelectedItem), topi, seli);
            }
        }
    }

    public class Lex {
        String s;
        int x, cx;

        public Lex(String s) {
            this.s = s;
            x = 0;
            cx = s.Length;
        }

        public bool Skip() {
            while (x < cx && s[x] == ' ') x++;
            return !EOF;
        }

        public bool EOF { get { return x >= cx; } }

        /// <summary>
        /// Rest 1 character
        /// </summary>
        public bool R1 { get { return x < cx; } }
        /// <summary>
        /// Rest 2 chars
        /// </summary>
        public bool R2 { get { return x + 1 < cx; } }

        /// <summary>
        /// Cut 1 character
        /// </summary>
        public String C1 { get { return s.Substring(x, 1); } }
        /// <summary>
        /// Cut 2 chars
        /// </summary>
        public String C2 { get { return s.Substring(x, 2); } }

        public void Step1() { x++; }
        public void Step2() { x += 2; }

        public bool TryReadInt(out int v) {
            if (R2 && String.Compare(C2, "0x", true) == 0) {
                Step2();
                String s = "";
                while (R1 && "0123456789abcdefABCDEF".IndexOf(C1) >= 0) {
                    s += C1;
                    Step1();
                }
                v = Convert.ToInt32(s, 16);
                return true;
            }
            if ("0123456789".IndexOf(C1) >= 0) {
                String s = C1;
                Step1();
                while (R1 && "0123456789".IndexOf(C1) >= 0) {
                    s += C1;
                    Step1();
                }
                v = Convert.ToInt32(s);
                return true;
            }
            v = 0;
            return false;
        }

        readonly String SKw0 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_.";
        readonly String SKw1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_.0123456789";

        public bool TryReadStr(out string s) {
            if (SKw0.IndexOf(C1) >= 0) {
                s = C1;
                Step1();
                while (R1 && SKw1.IndexOf(C1) >= 0) {
                    s += C1;
                    Step1();
                }
                return true;
            }
            s = null;
            return false;
        }
    }

    public class ELeaf {
        /// <summary>
        /// type
        /// </summary>
        public ELT t = ELT.None;

        /// <summary>
        /// str val
        /// </summary>
        public String s = null;

        /// <summary>
        /// int val
        /// </summary>
        public int i = 0;

        /// <summary>
        /// left val
        /// </summary>
        public ELeaf lv = null;

        /// <summary>
        /// right val
        /// </summary>
        public ELeaf rv = null;

        /// <summary>
        /// call name
        /// </summary>
        public String call = null;

        /// <summary>
        /// call args
        /// </summary>
        public List<ELeaf> ala = new List<ELeaf>();

        public ELeaf(int i) {
            this.t = ELT.Int;
            this.i = i;
        }

        public ELeaf(String s) {
            this.t = ELT.Str;
            this.s = s;
        }

        public ELeaf(ELT t, ELeaf lv) {
            this.t = t;
            this.lv = lv;
        }

        public ELeaf(ELT t, ELeaf lv, ELeaf rv) {
            this.t = t;
            this.lv = lv;
            this.rv = rv;
        }

        public ELeaf(String call, List<ELeaf> ala) {
            this.t = ELT.Call;
            this.call = call;
            this.ala = ala;
        }

        public override string ToString() {
            switch (t) {
                case ELT.Int: return i.ToString();
                case ELT.Str: return s;
                case ELT.Call: {
                        String res = "";
                        for (int x = 0; x < ala.Count; x++) {
                            if (x != 0) res += ", ";
                            res += ala[x].ToString();
                        }
                        return call + "(" + res + ")";
                    }
                case ELT.UPlus: return String.Format("+({0})", lv);
                case ELT.UMinus: return String.Format("-({0})", lv);

                case ELT.BMult: return String.Format("({0})*({1})", lv, rv);
                case ELT.BDiv: return String.Format("({0})/({1})", lv, rv);
                case ELT.BPlus: return String.Format("({0})+({1})", lv, rv);
                case ELT.BMinus: return String.Format("({0})-({1})", lv, rv);
                case ELT.BOr: return String.Format("({0})|({1})", lv, rv);
                case ELT.BAnd: return String.Format("({0})&({1})", lv, rv);
                case ELT.BXor: return String.Format("({0})^({1})", lv, rv);
                case ELT.BLSh: return String.Format("({0})<<({1})", lv, rv);
                case ELT.BLess: return String.Format("({0})<({1})", lv, rv);
            }
            return "?";
        }

    }
    public enum ELT {
        None,
        Int, Str, Call,
        UPlus, UMinus,

        BMult, BDiv,
        BPlus, BMinus, BOr, BAnd, BXor, BLSh, BLess,
    }

    public class CExpr : Lex {
        public CExpr(String s)
            : base(s) {
            v = ParseExpr1();
        }

        public ELeaf v;

        ELeaf ParseExpr2() {
            ELeaf lv = ParseElem();
            while (R1) {
                if (C1 == "*") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BMult, lv, rv);
                    continue;
                }
                if (C1 == "/") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BDiv, lv, rv);
                    continue;
                }
                break;
            }
            return lv;
        }

        ELeaf ParseExpr1() {
            ELeaf lv = ParseExpr2();
            while (R1) {
                if (R2) {
                    if (C2 == "<<") {
                        Step2();
                        ELeaf rv = ParseExpr2();
                        lv = new ELeaf(ELT.BLSh, lv, rv);
                        continue;
                    }
                }
                if (C1 == "+") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BPlus, lv, rv);
                    continue;
                }
                if (C1 == "-") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BMinus, lv, rv);
                    continue;
                }
                if (C1 == "|") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BOr, lv, rv);
                    continue;
                }
                if (C1 == "&") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BAnd, lv, rv);
                    continue;
                }
                if (C1 == "^") {
                    Step1();
                    ELeaf rv = ParseExpr2();
                    lv = new ELeaf(ELT.BXor, lv, rv);
                    continue;
                }
                break;
            }
            return lv;
        }

        private ELeaf ParseElem() {
            if (!Skip()) throw new EndOfStreamException();
            ELeaf res = null;
            if (C1 == "(") {
                Step1();
                res = ParseExpr1();
                if (C1 != ")") throw new InvalidDataException();
                Step1();
                return res;
            }
            if (C1 == "+") {
                Step1();
                return new ELeaf(ELT.UPlus, ParseElem());
            }
            if (C1 == "-") {
                Step1();
                return new ELeaf(ELT.UMinus, ParseElem());
            }
            int v;
            if (TryReadInt(out v)) {
                return new ELeaf(v);
            }
            String s;
            if (TryReadStr(out s)) {
                if (Skip() && C1 == "(") {
                    Step1();
                    List<ELeaf> ala = new List<ELeaf>();
                    while (true) {
                        ELeaf calla = ParseExpr1();
                        ala.Add(calla);
                        if (!Skip()) throw new EndOfStreamException();
                        if (C1 == ",") {
                            Step1();
                            continue;
                        }
                        if (C1 == ")") {
                            Step1();
                            break;
                        }
                        throw new InvalidDataException();
                    }
                    return new ELeaf(s, ala);
                }
                return new ELeaf(s);
            }
            throw new NotSupportedException();
        }
    }
}