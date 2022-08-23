#define HACK

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using vu1Disarm;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Readmset;
using vu1Sim.gif;
using System.Security.Cryptography;
using CustVPU1;

namespace vu1Sim {
    public partial class Form1vu1 : Form {
        public Form1vu1() {
            InitializeComponent();
        }

        Form2 form2;

        class FSet {
            public string name, fpbin, fptxt;

            public FSet(string name, string fpbin, string fptxt) {
                this.name = name;
                this.fpbin = fpbin;
                this.fptxt = fptxt;
            }
        }

        List<FSet> alfs = new List<FSet>();

        void readfset() {
            alfs.Clear();

            if (File.Exists(openFileDialogfs.FileName) == false)
                openFileDialogfs.ShowDialog(this);
            if (File.Exists(openFileDialogfs.FileName) == false)
                return;

            foreach (String row in File.ReadAllText(openFileDialogfs.FileName, Encoding.Default).Replace("\r\n", "\n").Split('\n')) {
                String[] cols = row.Split('\t');
                if (cols.Length == 3 && File.Exists(cols[1]) && File.Exists(cols[2])) {
                    alfs.Add(new FSet(cols[0], cols[1], cols[2]));
                }
            }
        }

        private void Form1vu1_Load(object sender, EventArgs e) {
            form2 = new Form2();

            readfset();

            if (alfs.Count != 0) {
                openHelp(alfs[0].fptxt);
                openBin(alfs[0].fpbin);
            }

            form2.Show();
            form2.Left = this.Right;
            form2.Top = this.Top;
        }

        class STkns {
            public string[] keys;
            public string val;

            public STkns(string parse) {
                int p = parse.IndexOf('=');
                string lv, rv;
                if (p < 0) {
                    lv = parse;
                    rv = "";
                }
                else {
                    lv = parse.Substring(0, p);
                    rv = parse.Substring(p + 1);
                }

                this.keys = lv.Split(':');
                this.val = rv;
            }
        }
        class HTknUt {
            public static int parseAt(string text) {
                if (!text.StartsWith("@")) throw new NotSupportedException(text);
                return int.Parse(text.Substring(1));
            }
            public static HTRSel parseViVf(string text) {
                if (text.StartsWith("vi")) return HTRSel.vi0 + int.Parse(text.Substring(2));
                if (text.Equals("accx")) return HTRSel.accx;
                if (text.Equals("accy")) return HTRSel.accy;
                if (text.Equals("accz")) return HTRSel.accz;
                if (text.Equals("accw")) return HTRSel.accw;
                if (text.Equals("I")) return HTRSel.I;
                if (text.Equals("Q")) return HTRSel.Q;
                if (text.Equals("R")) return HTRSel.R;
                if (text.Equals("P")) return HTRSel.P;
                Match M;
                M = Regex.Match(text, "^vf([0-9]+)([xyzw])$");
                if (M.Success) {
                    int rs;
                    switch (M.Groups[2].Value[0]) {
                        case 'x': rs = 0; break;
                        case 'y': rs = 1; break;
                        case 'z': rs = 2; break;
                        case 'w': rs = 3; break;
                        default: throw new NotSupportedException();
                    }
                    return HTRSel.vf0x + 4 * int.Parse(M.Groups[1].Value) + rs;
                }
                throw new NotSupportedException(text);
            }
        }
        class HTBase {
        }
        class HTDesc : HTBase {
            public int off;
            public string text;

            public HTDesc(int off, string text) {
                this.off = off;
                this.text = text;
            }
        }
        class HTMean : HTBase {
            public int off;
            public HTRSel vivf;
            public string text;

            public HTMean(int off, HTRSel vivf, string text) {
                this.off = off;
                this.vivf = vivf;
                this.text = text;
            }
        }
        class HTDump : HTBase {
            public int off;
            public int pane;
            public HTRSel vivf;
            public string text;

            public HTDump(int off, int pane, HTRSel vivf, string text) {
                this.off = off;
                this.pane = pane;
                this.vivf = vivf;
                this.text = text;
            }
        }

        List<HTBase> alht = new List<HTBase>();
        SortedList<int, HTDesc> dicthtdesc = new SortedList<int, HTDesc>();

        void openHelp(string fp) {
            alht.Clear();
            foreach (string row in File.ReadAllLines(fp)) {
                STkns st = new STkns(row);
                if (st.keys.Length >= 3 && st.keys[1].Equals("mean")) {
                    alht.Add(new HTMean(HTknUt.parseAt(st.keys[0]), HTknUt.parseViVf(st.keys[2]), st.val));
                }
                else if (st.keys.Length >= 2 && st.keys[1].Equals("desc")) {
                    alht.Add(new HTDesc(HTknUt.parseAt(st.keys[0]), st.val));
                }
                else if (st.keys.Length >= 4 && st.keys[1].Equals("dump")) {
                    alht.Add(new HTDump(HTknUt.parseAt(st.keys[0]), int.Parse(st.keys[2]), HTknUt.parseViVf(st.keys[3]), st.val));
                }
            }

            dicthtdesc.Clear();
            foreach (HTBase ht in alht) {
                if (ht is HTDesc) dicthtdesc[((HTDesc)ht).off] = (HTDesc)ht;
            }
        }

        class HTF2iUt {
            public static ushort F2US(float val) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write(val);
                os.Position = 0;
                return new BinaryReader(os).ReadUInt16();
            }
        }
        class HTRegUt {
            public static int extract(HTRSel vivf, VU1 w1) {
                if (HTRSel.vi0 <= vivf && vivf <= HTRSel.vi15) {
                    return w1.vi[(int)vivf];
                }
                else if (HTRSel.vf0x <= vivf && vivf <= HTRSel.vf31w) {
                    int a = (int)(vivf - HTRSel.vf0x);
                    return HTF2iUt.F2US(w1.GetVFOf(a / 4, a % 4));
                }
                else if (vivf == HTRSel.accx) return HTF2iUt.F2US(w1.acc.x);
                else if (vivf == HTRSel.accy) return HTF2iUt.F2US(w1.acc.y);
                else if (vivf == HTRSel.accz) return HTF2iUt.F2US(w1.acc.z);
                else if (vivf == HTRSel.accw) return HTF2iUt.F2US(w1.acc.w);
                else if (vivf == HTRSel.I) return HTF2iUt.F2US(w1.I);
                else if (vivf == HTRSel.Q) return HTF2iUt.F2US(w1.Q);
                else if (vivf == HTRSel.P) return HTF2iUt.F2US(w1.P);
                throw new NotSupportedException();
            }
        }

        const int MEMMASK = 16383;

        void doHelper() {
            int off = w1.tpc / 8;
            foreach (HTBase ht in alht) {
                HTMean htMean = ht as HTMean;
                if (htMean != null && htMean.off == off) {
                    form2.SetMean(htMean.vivf, htMean.text);
                }
                HTDump htDump = ht as HTDump;
                if (htDump != null && htDump.off == off) {
                    HexVwer o = null; Label lab = null;
                    switch (htDump.pane) {
                        case 1: o = hexVwer1; lab = labelDump1; break;
                        case 2: o = hexVwer2; lab = labelDump2; break;
                        default: continue;
                    }
                    o.SetPos((HTRegUt.extract(htDump.vivf, w1) * 16) & MEMMASK);
                    lab.Text = htDump.text;
                    continue;
                }
            }
        }

        [DebuggerDisplay("({x}, {y}, {z}, {w})")]
        class Vec4 {
            public float x, y, z, w;

            public Vec4(float x, float y, float z, float w) {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }
            public static Vec4 Empty {
                get { return new Vec4(0, 0, 0, 0); }
            }
        }
        class VU1 {
            public int tpc = -1, itop = -1, top = -1, lasttpc = -1;
            public Vec4[] vf = new Vec4[32];
            public ushort[] vi = new ushort[16];
            public Vec4 acc = Vec4.Empty;
            public float I = 0;
            public float Q = 0;
            public float P = 0;
            public byte[] Mem = new byte[16384];
            public byte[] Micro = new byte[16384];
            public uint CF = 0;
            public int MACflag = 0;
            public int bpc = -1; // branch pc queued for delay slot
            public bool branch = false;

            public ushort VI0 { get { return vi[0]; } set { vi[0] = value; } }
            public ushort VI1 { get { return vi[1]; } set { vi[1] = value; } }
            public ushort VI2 { get { return vi[2]; } set { vi[2] = value; } }
            public ushort VI3 { get { return vi[3]; } set { vi[3] = value; } }
            public ushort VI4 { get { return vi[4]; } set { vi[4] = value; } }
            public ushort VI5 { get { return vi[5]; } set { vi[5] = value; } }
            public ushort VI6 { get { return vi[6]; } set { vi[6] = value; } }
            public ushort VI7 { get { return vi[7]; } set { vi[7] = value; } }
            public ushort VI8 { get { return vi[8]; } set { vi[8] = value; } }
            public ushort VI9 { get { return vi[9]; } set { vi[9] = value; } }
            public ushort VI10 { get { return vi[10]; } set { vi[10] = value; } }
            public ushort VI11 { get { return vi[11]; } set { vi[11] = value; } }
            public ushort VI12 { get { return vi[12]; } set { vi[12] = value; } }
            public ushort VI13 { get { return vi[13]; } set { vi[13] = value; } }
            public ushort VI14 { get { return vi[14]; } set { vi[14] = value; } }
            public ushort VI15 { get { return vi[15]; } set { vi[15] = value; } }

            public Vec4 VF0 { get { return vf[0]; } }
            public Vec4 VF1 { get { return vf[1]; } }
            public Vec4 VF2 { get { return vf[2]; } }
            public Vec4 VF3 { get { return vf[3]; } }
            public Vec4 VF4 { get { return vf[4]; } }
            public Vec4 VF5 { get { return vf[5]; } }
            public Vec4 VF6 { get { return vf[6]; } }
            public Vec4 VF7 { get { return vf[7]; } }
            public Vec4 VF8 { get { return vf[8]; } }
            public Vec4 VF9 { get { return vf[9]; } }
            public Vec4 VF10 { get { return vf[10]; } }
            public Vec4 VF11 { get { return vf[11]; } }
            public Vec4 VF12 { get { return vf[12]; } }
            public Vec4 VF13 { get { return vf[13]; } }
            public Vec4 VF14 { get { return vf[14]; } }
            public Vec4 VF15 { get { return vf[15]; } }
            public Vec4 VF16 { get { return vf[16]; } }
            public Vec4 VF17 { get { return vf[17]; } }
            public Vec4 VF18 { get { return vf[18]; } }
            public Vec4 VF19 { get { return vf[19]; } }
            public Vec4 VF20 { get { return vf[20]; } }
            public Vec4 VF21 { get { return vf[21]; } }
            public Vec4 VF22 { get { return vf[22]; } }
            public Vec4 VF23 { get { return vf[23]; } }
            public Vec4 VF24 { get { return vf[24]; } }
            public Vec4 VF25 { get { return vf[25]; } }
            public Vec4 VF26 { get { return vf[26]; } }
            public Vec4 VF27 { get { return vf[27]; } }
            public Vec4 VF28 { get { return vf[28]; } }
            public Vec4 VF29 { get { return vf[29]; } }
            public Vec4 VF30 { get { return vf[30]; } }
            public Vec4 VF31 { get { return vf[31]; } }

            public float GetVFOf(int vf, int xyzw) {
                switch (xyzw) {
                    case 0: return this.vf[vf].x;
                    case 1: return this.vf[vf].y;
                    case 2: return this.vf[vf].z;
                    case 3: return this.vf[vf].w;
                }
                throw new NotSupportedException();
            }
        }

        VU1 w1 = null;
        vu1Disarm.VU1Da Da = new vu1Disarm.VU1Da();

        class SerUt {
            public static Vec4 readVec4(BinaryReader br) {
                float x = br.ReadSingle();
                float y = br.ReadSingle();
                float z = br.ReadSingle();
                float w = br.ReadSingle();
                return new Vec4(x, y, z, w);
            }
            public static ushort readVi(BinaryReader br) {
                return br.ReadUInt16();
            }

            public static float readF(BinaryReader br) {
                return br.ReadSingle();
            }
        }

        private void openBin(string fp) {
            linkLabel1.Text = Path.GetFileName(fp);
            w1 = new VU1();
            using (FileStream fs = File.OpenRead(fp)) {
                byte[] hdr = new BinaryReader(fs).ReadBytes(1024);
                string shdr = Encoding.ASCII.GetString(hdr);
                int hdrlen = shdr.IndexOf((char)0x1a);
                string[] hdrlns = shdr.Substring(0, hdrlen).Split('\n');
                if (!hdrlns[0].Equals("v1")) throw new NotSupportedException("Only v1 is supported!");
                for (int t = 1; t < hdrlns.Length; t++) {
                    string[] kv = hdrlns[t].Split('=');
                    if (kv[0].Equals("tpc")) w1.tpc = Convert.ToInt32(kv[1], 16);
                    if (kv[0].Equals("itop")) w1.itop = Convert.ToInt32(kv[1], 16);
                    if (kv[0].Equals("top")) w1.top = Convert.ToInt32(kv[1], 16);
                }

                fs.Position = 1024;
                BinaryReader br = new BinaryReader(fs);
                for (int t = 0; t < 32; t++)
                    w1.vf[t] = SerUt.readVec4(br);
                for (int t = 0; t < 16; t++)
                    w1.vi[t] = SerUt.readVi(br);

                w1.acc = SerUt.readVec4(br);
                w1.I = SerUt.readF(br);
                w1.Q = SerUt.readF(br);
                SerUt.readF(br);
                w1.P = SerUt.readF(br);

                fs.Position = 1024 + 1024;
                Trace.Assert(fs.Read(w1.Mem, 0, 16384) == 16384);
                Trace.Assert(fs.Read(w1.Micro, 0, 16384) == 16384);

#if HACK
                //w1.Mem[w1.top * 16 + 0x28] = 1;
                w1.VI8 = (ushort)(0x85 - w1.top);
#endif

                Da.Decode(w1.Micro);

                listView1.Items.Clear();
                for (int t = 0; t < 2048; t++) {
                    ListViewItem lvi = listView1.Items.Add(t.ToString());
                    lvi.SubItems.Add(Da.L[t].ToString());
                    lvi.SubItems.Add(Da.U[t].ToString());

                    if (dicthtdesc.ContainsKey(t)) {
                        lvi.SubItems.Add(dicthtdesc[t].text);
                    }
                    else {
                        lvi.SubItems.Add("");
                    }
                }
            }
            refreshRegs();
            refreshList();
            refreshBin();

            tbHashMicro.Text = "md5=" + B2H.Conv(MD5.Create().ComputeHash(w1.Micro));

            //Clipboard.SetText(labelRegs.Text);
        }

        class B2H {
            public static string Conv(byte[] bin) {
                string s = "";
                foreach (byte b in bin) s += b.ToString("x2");
                return s;
            }
        }

        void refreshBin() {
            listBox1.Items.Clear();
            for (int t = 0; t < 1024; t++) {
                string s = (t * 16).ToString("X4") + ": ";
                for (int x = 0; x < 16; x++) {
                    s += w1.Mem[16 * t + x].ToString("X2") + " ";
                }
                listBox1.Items.Add(s);
            }

            hexVwer1.SetBin(w1.Mem);
            hexVwer2.SetBin(w1.Mem);
        }
        void refreshBin(int para) {
            int t = para;
            {
                string s = (t * 16).ToString("X4") + ": ";
                for (int x = 0; x < 16; x++) {
                    s += w1.Mem[16 * t + x].ToString("X2") + " ";
                }
                listBox1.Items[t] = (s);
            }
        }

        int lasttpc = -1;

        void refreshList() {
            if (lasttpc != -1) {
                listView1.Items[lasttpc / 8].ImageIndex = -1;
            }
            int i = w1.tpc / 8;
            listView1.Items[i].ImageIndex = 1;
            listView1.Items[i].Focused = true;
            listView1.EnsureVisible(i);

            lasttpc = w1.tpc;
        }

        void refreshRegs() {
            string s = "";
            for (int t = 0; t < 16; t++)
                s += string.Format("vi{0,-2} {1,5}  {1:x4}  {2}\n", t, w1.vi[t], form2.summary_viHelp(t));
            for (int t = 0; t < 32; t++)
                s += string.Format("vf{0,-2} {1,15}|{2,15}|{3,15}|{4,15}\n"
                    , t
                    , w1.vf[t].x
                    , w1.vf[t].y
                    , w1.vf[t].z
                    , w1.vf[t].w
                    );
            s += string.Format("acc  {1,15} {2,15} {3,15} {4,15}  {5}\n"
                , 0
                , w1.acc.x
                , w1.acc.y
                , w1.acc.z
                , w1.acc.w
                , form2.summary_accHelp()
                );
            s += string.Format("I    {0,15}  {1}\n", w1.I, form2.IHelp);
            s += string.Format("Q    {0,15}  {1}\n", w1.Q, form2.QHelp);
            s += string.Format("R    {0,15}  {1}\n", 0, form2.RHelp);
            s += string.Format("P    {0,15}  {1}\n", w1.P, form2.PHelp);

            form2.labelRegs.Text = s;
            labelTick.Text = "Tick: " + tickcnt;
        }

        class SUt {
            public static int isVI(string or1) {
                if (!or1.StartsWith("$vi")) throw new NotSupportedException(or1);
                return int.Parse(or1.Substring(3));
            }
            public static int Oimm11viisDest(string dest, string or2, VU1 w1) {
                Match M = Regex.Match(or2, "^([\\-]?)\\$([0-9a-f]+)\\(\\$vi([0-9]+)\\)$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(or2);
                int iDest;
                switch (dest) {
                    case "x": iDest = 0; break;
                    case "y": iDest = 1; break;
                    case "z": iDest = 2; break;
                    case "w": iDest = 3; break;
                    default: throw new NotSupportedException(dest);
                }
                int mark = M.Groups[1].Value.Equals("-") ? -1 : +1;
                int imm11 = Convert.ToInt32(M.Groups[2].Value, 16) * mark;
                int viis = Convert.ToInt32(M.Groups[3].Value);

                return imm11 + w1.vi[viis] * 16 + 4 * iDest;
            }

            public static int Oimm11viis(string or2, VU1 w1) {
                Match M = Regex.Match(or2, "^([\\-]?)\\$([0-9a-f]+)\\(\\$vi([0-9]+)\\)$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(or2);
                int mark = M.Groups[1].Value.Equals("-") ? -1 : +1;
                int imm11 = Convert.ToInt32(M.Groups[2].Value, 16) * mark;
                int viis = Convert.ToInt32(M.Groups[3].Value);

                return imm11 + w1.vi[viis] * 16;
            }

            public static ushort rILW(string dest, string or2, VU1 w1) {
                int off = Oimm11viisDest(dest, or2, w1);
                return (ushort)(w1.Mem[off & MEMMASK] | (w1.Mem[(off + 1) & MEMMASK] << 8));
            }

            const int MEMMASK = 16383;

            public static ushort isImm15u(string or3) {
                if (!or3.StartsWith("$")) throw new NotSupportedException(or3);
                return Convert.ToUInt16(or3.Substring(1), 16);
            }

            public static int isImm11s(string or3) {
                return Convert.ToInt32(or3);
            }

            public static int isDests(string dests) {
                int r = 0;
                foreach (char c in dests) {
                    switch (c) {
                        case 'x': r |= 8; break;
                        case 'y': r |= 4; break;
                        case 'z': r |= 2; break;
                        case 'w': r |= 1; break;
                        default: throw new NotSupportedException(dests);
                    }
                }
                return r;
            }

            public static int isVF(string or1) {
                if (!or1.StartsWith("vf")) throw new NotSupportedException(or1);
                return int.Parse(or1.Substring(2));
            }

            public static int isVIpp(string or2) {
                Match M = Regex.Match(or2, "^\\(\\$vi([0-9]+)\\+\\+\\)$");
                if (!M.Success) throw new NotSupportedException(or2);
                int vi = int.Parse(M.Groups[1].Value);
                return vi;
            }

            public static float rLQIa(int viis, VU1 w1, int iDest) {
                MemoryStream si = new MemoryStream(w1.Mem, false);
                si.Position = (16 * w1.vi[viis] + 4 * iDest) & MEMMASK;
                return new BinaryReader(si).ReadSingle();
            }

            internal static int isImm5s(string or3) {
                if (or3.StartsWith("$"))
                    return +Convert.ToByte(or3.Substring(1), 16);
                if (or3.StartsWith("-$"))
                    return -Convert.ToByte(or3.Substring(2), 16);
                throw new NotSupportedException(or3);
            }

            public static ushort rMTIR(string or2, VU1 w1) {
                Match M = Regex.Match(or2, "^VF([0-9]+)([xyzw])$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(or2);
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                int vffs = int.Parse(M.Groups[1].Value);
                char cfsf = M.Groups[2].Value[0];
                switch (cfsf) {
                    case 'x': new BinaryWriter(os).Write(w1.vf[vffs].x); break;
                    case 'y': new BinaryWriter(os).Write(w1.vf[vffs].y); break;
                    case 'z': new BinaryWriter(os).Write(w1.vf[vffs].z); break;
                    case 'w': new BinaryWriter(os).Write(w1.vf[vffs].w); break;
                    default: throw new NotSupportedException(or2);
                }
                os.Position = 0;
                ushort ret = new BinaryReader(os).ReadUInt16();
                return ret;
            }

            public static VFbc isVFbc(string or3) {
                Match M = Regex.Match(or3, "^VF([0-9]+)([xyzw])$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(or3);
                byte bc;
                switch (M.Groups[2].Value[0]) {
                    case 'x': bc = 0; break;
                    case 'y': bc = 1; break;
                    case 'z': bc = 2; break;
                    case 'w': bc = 3; break;
                    default: throw new NotSupportedException(or3);
                }
                byte vf = Convert.ToByte(M.Groups[1].Value);
                return new VFbc(vf, bc);
            }

            public static void wSQIa(int viit, VU1 w1, int iDest, float vf) {
                MemoryStream os = new MemoryStream(w1.Mem, true);
                os.Position = (16 * w1.vi[viit] + 4 * iDest) & MEMMASK; // up to 16384 bytes
                new BinaryWriter(os).Write(vf);
            }

            public static uint isImm24(string or1) {
                if (!or1.StartsWith("$")) throw new NotSupportedException(or1);
                return Convert.ToUInt32(or1.Substring(1), 16);
            }

            public static float rLQa(int off, VU1 w1, int iDest) {
                MemoryStream si = new MemoryStream(w1.Mem, false);
                si.Position = (off + 4 * iDest) & MEMMASK; // up to 16384 bytes
                return new BinaryReader(si).ReadSingle();
            }

            public static void wSQa(int off, VU1 w1, int iDest, float vf) {
                MemoryStream os = new MemoryStream(w1.Mem, true);
                os.Position = (off + 4 * iDest) & MEMMASK; // up to 16384 bytes
                new BinaryWriter(os).Write(vf);
            }

            public static int isVFxyz(string vf) {
                Match M = Regex.Match(vf, "^VF([0-9]+)xyz$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(vf);
                return int.Parse(M.Groups[1].Value);
            }

            public static int isVFw(string vf) {
                Match M = Regex.Match(vf, "^VF([0-9]+)w$", RegexOptions.IgnoreCase);
                if (!M.Success) throw new NotSupportedException(vf);
                return int.Parse(M.Groups[1].Value);
            }

            public static float rMFIR(ushort v) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write((int)(short)v);
                os.Position = 0;
                return new BinaryReader(os).ReadSingle();
            }
        }

        struct VFbc {
            public byte vf;
            public byte bc;

            public VFbc(byte vf, byte bc) {
                this.vf = vf;
                this.bc = bc;
            }
        }

        abstract class Latency {
            public int attv = 0; // run at throughput value
        }
        class SetVI : Latency {
            public int Vit;
            public ushort v;

            public SetVI(int Vit, ushort v) {
                this.Vit = Vit;
                this.v = v;
            }
        }
        class SetVF : Latency {
            public int Vft;
            public float x = float.NaN, y = float.NaN, z = float.NaN, w = float.NaN;
            public int Vd = 0; // 8=x, 4=y, 2=z, 1=w

            public SetVF(int Vft) {
                this.Vft = Vft;
            }
            public void SetX(float v) { this.x = v; Vd |= 8; }
            public void SetY(float v) { this.y = v; Vd |= 4; }
            public void SetZ(float v) { this.z = v; Vd |= 2; }
            public void SetW(float v) { this.w = v; Vd |= 1; }
        }
        class OpXGKick : Latency {
            public int Viis;

            public OpXGKick(int Viis) {
                this.Viis = Viis;
            }
        }

        int curtv = 0;

        List<Latency> alla = new List<Latency>();

        void doStep() {
            doHelper();

            w1.lasttpc = w1.tpc;
            int pos = w1.tpc / 8;
            bool b = false;
            if (true) { // ->->-> Lower ->->->
                int tv = 0, lv = 0; // throughput/latency value
                SetVI psi = null;
                SetVF psf = null;
                OpXGKick pxg = null;
                Pki L = Da.L[pos]; string LI = L.al[0]; string[] Lal = L.al;

                if (LI.Equals("nop")) {
                    tv = 1; lv = 4; //]nop
                }
                else if (LI.StartsWith("ILW.")) { //]ILW.
                    tv = 1; lv = 4;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), SUt.rILW(LI.Substring(4), Lal[2], w1)); // w1.vi[SUt.isVI(Lal[1])] = SUt.rILW(LI.Substring(4), Lal[2], w1);
                }
                else if (LI.Equals("IADDIU")) { //]IADDIU
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] + SUt.isImm15u(Lal[3]))); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] + SUt.isImm15u(Lal[3]));
                }
                else if (LI.Equals("IADDI")) { //]IADDI
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] + SUt.isImm5s(Lal[3]))); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] + SUt.isImm5s(Lal[3]));
                }
                else if (LI.Equals("IBEQ")) { //]IBEQ
                    tv = 2; lv = 2;
                    bool test = w1.vi[SUt.isVI(Lal[1])] == w1.vi[SUt.isVI(Lal[2])];
                    if (test) {
                        b = true; w1.bpc = w1.tpc + SUt.isImm11s(Lal[3]) * 8;
                    }
                }
                else if (LI.Equals("IBNE")) { //]IBNE
                    tv = 2; lv = 2;
                    bool test = w1.vi[SUt.isVI(Lal[1])] != w1.vi[SUt.isVI(Lal[2])];
                    if (test) {
                        b = true; w1.bpc = w1.tpc + SUt.isImm11s(Lal[3]) * 8;
                    }
                }
                else if (LI.Equals("B")) { //]B
                    tv = 2; lv = 2;
                    b = true; w1.bpc = w1.tpc + SUt.isImm11s(Lal[1]) * 8;
                }
                else if (LI.Equals("IAND")) { //]IAND
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] & w1.vi[SUt.isVI(Lal[3])])); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] & w1.vi[SUt.isVI(Lal[3])]);
                }
                else if (LI.Equals("BAL")) { //]BAL
                    tv = 2; lv = 2;
                    w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.tpc + 8 + 8);
                    b = true; w1.bpc = w1.tpc + SUt.isImm11s(Lal[2]) * 8;
                }
                else if (LI.Equals("ISUBIU")) { //]ISUBIU
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] - SUt.isImm15u(Lal[3]))); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] - SUt.isImm15u(Lal[3]));
                }
                else if (LI.Equals("XTOP")) { //]XTOP
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.top)); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.top);
                }
                else if (LI.Equals("IADD")) { //]IADD
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] + w1.vi[SUt.isVI(Lal[3])])); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] + w1.vi[SUt.isVI(Lal[3])]);
                }
                else if (LI.StartsWith("LQI.")) { //]LQI.
                    tv = 1; lv = 4;
                    int viis = SUt.isVIpp(Lal[2]);
                    int k = SUt.isDests(LI.Substring(4));
                    int vfft = SUt.isVF(Lal[1]);
                    if (vfft != 0) {
                        psf = new SetVF(vfft);
                        if (0 != (k & 8)) psf.SetX(SUt.rLQIa(viis, w1, 0));
                        if (0 != (k & 4)) psf.SetY(SUt.rLQIa(viis, w1, 1));
                        if (0 != (k & 2)) psf.SetZ(SUt.rLQIa(viis, w1, 2));
                        if (0 != (k & 1)) psf.SetW(SUt.rLQIa(viis, w1, 3));
                    }
                    w1.vi[viis]++;
                }
                else if (LI.StartsWith("SQI.")) { //]SQI.
                    tv = 1; lv = 4;
                    int vffs = SUt.isVF(Lal[1]);
                    int viit = SUt.isVIpp(Lal[2]);
                    int k = SUt.isDests(LI.Substring(4));
                    if (0 != (k & 8)) SUt.wSQIa(viit, w1, 0, w1.vf[vffs].x);
                    if (0 != (k & 4)) SUt.wSQIa(viit, w1, 1, w1.vf[vffs].y);
                    if (0 != (k & 2)) SUt.wSQIa(viit, w1, 2, w1.vf[vffs].z);
                    if (0 != (k & 1)) SUt.wSQIa(viit, w1, 3, w1.vf[vffs].w);
                    onMemWr(w1.vi[viit] & 1023);
                    w1.vi[viit]++;
                }
                else if (LI.Equals("MTIR")) { //]MTIR
                    tv = 1; lv = 1;
                    int viit = SUt.isVI(Lal[1]);
                    if (viit != 0) psi = new SetVI(viit, SUt.rMTIR(Lal[2], w1)); // w1.vi[viit] = SUt.rMTIR(Lal[2], w1);
                }
                else if (LI.StartsWith("MR32.")) { //]MR32
                    tv = 1; lv = 4;
                    int k = SUt.isDests(LI.Substring(5));
                    int vfft = SUt.isVF(Lal[1]);
                    int vffs = SUt.isVF(Lal[2]);
                    float tx = w1.vf[vffs].x;
                    float ty = w1.vf[vffs].y;
                    float tz = w1.vf[vffs].z;
                    float tw = w1.vf[vffs].w;
                    if (vfft != 0) {
                        psf = new SetVF(vfft);
                        if (0 != (k & 8)) psf.SetX(ty);
                        if (0 != (k & 4)) psf.SetY(tz);
                        if (0 != (k & 2)) psf.SetZ(tw);
                        if (0 != (k & 1)) psf.SetW(tx);
                    }
                }
                else if (LI.Equals("IOR")) { //]IOR
                    tv = 1; lv = 1;
                    int viid = SUt.isVI(Lal[1]);
                    if (viid != 0) psi = new SetVI(viid, (ushort)(w1.vi[SUt.isVI(Lal[2])] | w1.vi[SUt.isVI(Lal[3])])); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] | w1.vi[SUt.isVI(Lal[3])]);
                }
                else if (LI.StartsWith("ISW.")) { //]ISW.
                    tv = 1; lv = 4;
                    int off = SUt.Oimm11viisDest(LI.Substring(4), Lal[2], w1) & MEMMASK;
                    int vit = w1.vi[SUt.isVI(Lal[1])];
                    w1.Mem[off + 0] = (byte)(vit);
                    w1.Mem[off + 1] = (byte)(vit >> 8);
                    onMemWr(off / 16);
                }
                else if (LI.Equals("FCSET")) { //]FCSET
                    w1.CF = SUt.isImm24(Lal[1]);
                }
                else if (LI.Equals("LOI")) { //]LOI
                    w1.I = float.Parse(Lal[1]);
                }
                else if (LI.StartsWith("LQ.")) { //]LQ.
                    tv = 1; lv = 4;
                    int off = SUt.Oimm11viis(Lal[2], w1);
                    int k = SUt.isDests(LI.Substring(3));
                    int vfft = SUt.isVF(Lal[1]);
                    if (vfft != 0) {
                        psf = new SetVF(vfft);
                        if (0 != (k & 8)) psf.SetX(SUt.rLQa(off, w1, 0));
                        if (0 != (k & 4)) psf.SetY(SUt.rLQa(off, w1, 1));
                        if (0 != (k & 2)) psf.SetZ(SUt.rLQa(off, w1, 2));
                        if (0 != (k & 1)) psf.SetW(SUt.rLQa(off, w1, 3));
                    }
                }
                else if (LI.Equals("DIV")) { //]DIV
                    tv = 7; lv = 7;
                    if (!Lal[1].Equals("Q")) throw new NotSupportedException(L.ToString());

                    VFbc vffs = SUt.isVFbc(Lal[2]);
                    float vffsfsf;
                    switch (vffs.bc) {
                        case 0: vffsfsf = w1.vf[vffs.vf].x; break;
                        case 1: vffsfsf = w1.vf[vffs.vf].y; break;
                        case 2: vffsfsf = w1.vf[vffs.vf].z; break;
                        case 3: vffsfsf = w1.vf[vffs.vf].w; break;
                        default: throw new NotSupportedException(L.ToString());
                    }

                    VFbc vfft = SUt.isVFbc(Lal[3]);
                    float vfftftf;
                    switch (vffs.bc) {
                        case 0: vfftftf = w1.vf[vfft.vf].x; break;
                        case 1: vfftftf = w1.vf[vfft.vf].y; break;
                        case 2: vfftftf = w1.vf[vfft.vf].z; break;
                        case 3: vfftftf = w1.vf[vfft.vf].w; break;
                        default: throw new NotSupportedException(L.ToString());
                    }

                    w1.Q = vffsfsf / vfftftf;
                }
                else if (LI.Equals("IAND")) { //]IAND
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] & w1.vi[SUt.isVI(Lal[3])])); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] & w1.vi[SUt.isVI(Lal[3])]);
                }
                else if (LI.Equals("WAITQ")) { //]WAITQ
                    tv = 13; lv = 13;
                }
                else if (LI.Equals("WAITP")) { //]WAITP
                    tv = 54; lv = 54;
                }
                else if (LI.StartsWith("SQ.")) { //]SQ.
                    tv = 1; lv = 4;
                    int off = SUt.Oimm11viis(Lal[2], w1);
                    int k = SUt.isDests(LI.Substring(3));
                    int vffs = SUt.isVF(Lal[1]);
                    if (0 != (k & 8)) SUt.wSQa(off, w1, 0, w1.vf[vffs].x);
                    if (0 != (k & 4)) SUt.wSQa(off, w1, 1, w1.vf[vffs].y);
                    if (0 != (k & 2)) SUt.wSQa(off, w1, 2, w1.vf[vffs].z);
                    if (0 != (k & 1)) SUt.wSQa(off, w1, 3, w1.vf[vffs].w);
                    onMemWr((off / 16) & 2047);
                }
                else if (LI.Equals("FMAND")) { //]FMAND
                    if (SUt.isVI(Lal[1]) != 0) w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] & w1.MACflag);
                }
                else if (LI.Equals("ISUB")) { //]ISUB
                    tv = 1; lv = 1;
                    if (SUt.isVI(Lal[1]) != 0) psi = new SetVI(SUt.isVI(Lal[1]), (ushort)(w1.vi[SUt.isVI(Lal[2])] - w1.vi[SUt.isVI(Lal[3])])); // w1.vi[SUt.isVI(Lal[1])] = (ushort)(w1.vi[SUt.isVI(Lal[2])] - w1.vi[SUt.isVI(Lal[3])]);
                }
                else if (LI.Equals("FCAND")) { //]FCAND
                    if (SUt.isVI(Lal[1]) != 0) w1.vi[SUt.isVI(Lal[1])] = (ushort)(SUt.isImm24(Lal[2]) & w1.CF);
                }
                else if (LI.Equals("XGKICK")) { //]XGKICK
                    tv = 1; lv = 1;
                    pxg = new OpXGKick(SUt.isVI(Lal[1]));
                }
                else if (LI.Equals("JR")) { //]JR
                    tv = 2; lv = 2;
                    b = true; w1.bpc = w1.vi[SUt.isVI(Lal[1])];
                }
                else if (LI.StartsWith("MFIR.")) { //]MFIR
                    tv = 1; lv = 4;
                    int k = SUt.isDests(LI.Substring(5));
                    int vfft = SUt.isVF(Lal[1]);
                    int viis = SUt.isVI(Lal[2]);
                    if (vfft != 0) {
                        psf = new SetVF(vfft);
                        float f = SUt.rMFIR(w1.vi[viis]);
                        if (0 != (k & 8)) psf.SetX(f);
                        if (0 != (k & 4)) psf.SetY(f);
                        if (0 != (k & 2)) psf.SetZ(f);
                        if (0 != (k & 1)) psf.SetW(f);
                    }
                }
                else if (LI.StartsWith("MOVE.")) { //]MOVE
                    tv = 1; lv = 4;
                    int k = SUt.isDests(LI.Substring(5));
                    int vfft = SUt.isVF(Lal[1]);
                    int vffs = SUt.isVF(Lal[2]);
                    if (vfft != 0) {
                        psf = new SetVF(vfft);
                        if (0 != (k & 8)) psf.SetX(w1.vf[vffs].x);
                        if (0 != (k & 4)) psf.SetY(w1.vf[vffs].y);
                        if (0 != (k & 2)) psf.SetZ(w1.vf[vffs].z);
                        if (0 != (k & 1)) psf.SetW(w1.vf[vffs].w);
                    }
                }
                else throw new NotSupportedException(L.ToString());

                curtv += tv;
                for (int t = 0; t < alla.Count; ) {
                    if (alla[t].attv <= curtv) {
                        Latency pref = alla[t];
                        if (false) { }
                        else if (pref is SetVI) {
                            SetVI pv = (SetVI)pref;
                            w1.vi[pv.Vit] = pv.v;
                        }
                        else if (pref is SetVF) {
                            SetVF pv = (SetVF)pref;
                            if (0 != (pv.Vd & 8)) w1.vf[pv.Vft].x = pv.x;
                            if (0 != (pv.Vd & 4)) w1.vf[pv.Vft].y = pv.y;
                            if (0 != (pv.Vd & 2)) w1.vf[pv.Vft].z = pv.z;
                            if (0 != (pv.Vd & 1)) w1.vf[pv.Vft].w = pv.w;
                        }
                        else throw new NotSupportedException(pref.GetType().ToString());
                        alla.RemoveAt(t);
                    }
                    else t++;
                }
                if (psi != null) { psi.attv = curtv + lv; alla.Add(psi); }
                if (psf != null) { psf.attv = curtv + lv; alla.Add(psf); }
                if (pxg != null) {
                    int viis = ((OpXGKick)pxg).Viis;
                    int off = (w1.vi[viis] * 16) & MEMMASK;
                    MemoryStream os = new MemoryStream(16384);
                    os.Write(w1.Mem, off, 16384 - off);
                    os.Write(w1.Mem, 0, off);
                    os.Position = 0;
                    StringWriter wr = new StringWriter();
                    ParseGIF.Parse(os, wr);
                    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gif000.txt"), wr.ToString());
                }
            }

            if (true) { // ->->-> Upper ->->->
                Pki U = Da.U[pos]; string UI = U.al[0]; string[] Ual = U.al;
                if (UI.Equals("NOP")) { //[NOP
                }
                else if (UI.StartsWith("MULAx.") || UI.StartsWith("MULAy.") || UI.StartsWith("MULAz.") || UI.StartsWith("MULAw.")) { //[MULAbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[4]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(6));
                    int vffs = SUt.isVF(Ual[2]);
                    if (0 != (k & 8)) w1.acc.x = w1.vf[vffs].x * vfftbc;
                    if (0 != (k & 4)) w1.acc.y = w1.vf[vffs].y * vfftbc;
                    if (0 != (k & 2)) w1.acc.z = w1.vf[vffs].z * vfftbc;
                    if (0 != (k & 1)) w1.acc.w = w1.vf[vffs].w * vfftbc;
                }
                else if (UI.StartsWith("MADDAx.") || UI.StartsWith("MADDAy.") || UI.StartsWith("MADDAz.") || UI.StartsWith("MADDAw.")) { //[MADDAbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[5]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(7));
                    int vffs = SUt.isVF(Ual[2]);
                    if (0 != (k & 8)) w1.acc.x += w1.vf[vffs].x * vfftbc;
                    if (0 != (k & 4)) w1.acc.y += w1.vf[vffs].y * vfftbc;
                    if (0 != (k & 2)) w1.acc.z += w1.vf[vffs].z * vfftbc;
                    if (0 != (k & 1)) w1.acc.w += w1.vf[vffs].w * vfftbc;
                }
                else if (UI.StartsWith("MADDx.") || UI.StartsWith("MADDy.") || UI.StartsWith("MADDz.") || UI.StartsWith("MADDw.")) { //[MADDbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[4]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(6));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.acc.x + w1.vf[vffs].x * vfftbc;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.acc.y + w1.vf[vffs].y * vfftbc;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.acc.z + w1.vf[vffs].z * vfftbc;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.acc.w + w1.vf[vffs].w * vfftbc;
                    }
                }
                else if (UI.StartsWith("SUB.")) { //[SUB.
                    int k = SUt.isDests(UI.Substring(4));
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vfft = SUt.isVF(Ual[3]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x - w1.vf[vfft].x;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y - w1.vf[vfft].y;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z - w1.vf[vfft].z;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w - w1.vf[vfft].w;
                    }
                }
                else if (UI.StartsWith("ADD.")) { //[ADD.
                    int k = SUt.isDests(UI.Substring(4));
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vfft = SUt.isVF(Ual[3]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x + w1.vf[vfft].x;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y + w1.vf[vfft].y;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z + w1.vf[vfft].z;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w + w1.vf[vfft].w;
                    }
                }
                else if (UI.StartsWith("ADDx.") || UI.StartsWith("ADDy.") || UI.StartsWith("ADDz.") || UI.StartsWith("ADDw.")) { //[ADDbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[3]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(5));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x + vfftbc;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y + vfftbc;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z + vfftbc;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w + vfftbc;
                    }
                }
                else if (UI.StartsWith("MAXx.") || UI.StartsWith("MAXy.") || UI.StartsWith("MAXz.") || UI.StartsWith("MAXw.")) { //[MAXbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[3]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(5));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = Math.Max(w1.vf[vffs].x, vfftbc);
                        if (0 != (k & 4)) w1.vf[vffd].y = Math.Max(w1.vf[vffs].y, vfftbc);
                        if (0 != (k & 2)) w1.vf[vffd].z = Math.Max(w1.vf[vffs].z, vfftbc);
                        if (0 != (k & 1)) w1.vf[vffd].w = Math.Max(w1.vf[vffs].w, vfftbc);
                    }
                }
                else if (UI.StartsWith("ITOF0.")) { //[ITOF0.
                    int k = SUt.isDests(UI.Substring(6));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.IToF0(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.IToF0(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.IToF0(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.IToF0(w1.vf[vffs].w);
                    }
                }
                else if (UI.StartsWith("SUBx.") || UI.StartsWith("SUBy.") || UI.StartsWith("SUBz.") || UI.StartsWith("SUBw.")) { //[SUBbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[3]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(5));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x - vfftbc;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y - vfftbc;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z - vfftbc;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w - vfftbc;
                    }
                }
                else if (UI.StartsWith("MULx.") || UI.StartsWith("MULy.") || UI.StartsWith("MULz.") || UI.StartsWith("MULw.")) { //[MULbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[3]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(5));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x * vfftbc;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y * vfftbc;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z * vfftbc;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w * vfftbc;
                    }
                }
                else if (UI.StartsWith("MUL.")) { //[MUL.
                    int k = SUt.isDests(UI.Substring(4));
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vfft = SUt.isVF(Ual[3]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x * w1.vf[vfft].x;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y * w1.vf[vfft].y;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z * w1.vf[vfft].z;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w * w1.vf[vfft].w;
                    }
                }
                else if (UI.StartsWith("MULi.")) { //[MULI.
                    int k = SUt.isDests(UI.Substring(5));
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x * w1.I;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y * w1.I;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z * w1.I;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w * w1.I;
                    }
                }
                else if (UI.StartsWith("FTOI0.")) { //[FTOI0.
                    int k = SUt.isDests(UI.Substring(6));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.FToI0(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.FToI0(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.FToI0(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.FToI0(w1.vf[vffs].w);
                    }
                }
                else if (UI.StartsWith("MULq.")) { //[MULq.
                    int k = SUt.isDests(UI.Substring(5));
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = w1.vf[vffs].x * w1.Q;
                        if (0 != (k & 4)) w1.vf[vffd].y = w1.vf[vffs].y * w1.Q;
                        if (0 != (k & 2)) w1.vf[vffd].z = w1.vf[vffs].z * w1.Q;
                        if (0 != (k & 1)) w1.vf[vffd].w = w1.vf[vffs].w * w1.Q;
                    }
                }
                else if (UI.StartsWith("MINIx.") || UI.StartsWith("MINIy.") || UI.StartsWith("MINIz.") || UI.StartsWith("MINIw.")) { //[MINIbc.
                    VFbc vfbc = SUt.isVFbc(Ual[3]);
                    float vfftbc;
                    switch (UI[4]) {
                        case 'x': vfftbc = w1.vf[vfbc.vf].x; break;
                        case 'y': vfftbc = w1.vf[vfbc.vf].y; break;
                        case 'z': vfftbc = w1.vf[vfbc.vf].z; break;
                        case 'w': vfftbc = w1.vf[vfbc.vf].w; break;
                        default: throw new NotSupportedException(U.ToString());
                    }
                    int k = SUt.isDests(UI.Substring(6));
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = Math.Min(w1.vf[vffs].x, vfftbc);
                        if (0 != (k & 4)) w1.vf[vffd].y = Math.Min(w1.vf[vffs].y, vfftbc);
                        if (0 != (k & 2)) w1.vf[vffd].z = Math.Min(w1.vf[vffs].z, vfftbc);
                        if (0 != (k & 1)) w1.vf[vffd].w = Math.Min(w1.vf[vffs].w, vfftbc);
                    }
                }
                else if (UI.Equals("OPMULA.xyz")) { //[OPMULA.xyz
                    int vffs = SUt.isVF(Ual[2]);
                    int vfft = SUt.isVF(Ual[3]);
                    w1.acc.x = w1.vf[vffs].y * w1.vf[vfft].z;
                    w1.acc.y = w1.vf[vffs].z * w1.vf[vfft].x;
                    w1.acc.z = w1.vf[vffs].x * w1.vf[vfft].y;
                }
                else if (UI.Equals("OPMSUB.xyz")) { //[OPMSUB.xyz
                    int vffd = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vfft = SUt.isVF(Ual[3]);
                    if (vffd != 0) {
                        w1.vf[vffd].x = w1.acc.x - w1.vf[vffs].y * w1.vf[vfft].z;
                        w1.vf[vffd].y = w1.acc.y - w1.vf[vffs].z * w1.vf[vfft].x;
                        w1.vf[vffd].z = w1.acc.z - w1.vf[vffs].x * w1.vf[vfft].y;
                    }
                }
                else if (UI.StartsWith("FTOI4.")) { //[FTOI4.
                    int k = SUt.isDests(UI.Substring(6));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.FToI4(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.FToI4(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.FToI4(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.FToI4(w1.vf[vffs].w);
                    }
                }
                else if (UI.Equals("CLIPw.xyz")) { //[CLIPw.xyz
                    int vffs = SUt.isVF(Ual[1]);
                    int vfft = SUt.isVFw(Ual[2]);
                    // -z +z -y +y -x +x
                    w1.CF = w1.CF << 6;
                    float vfftw = Math.Abs(w1.vf[vfft].w);
                    w1.CF |= (w1.vf[vffs].x > +vfftw) ? 0x01U : 0;
                    w1.CF |= (w1.vf[vffs].x < -vfftw) ? 0x02U : 0;
                    w1.CF |= (w1.vf[vffs].y > +vfftw) ? 0x04U : 0;
                    w1.CF |= (w1.vf[vffs].y < -vfftw) ? 0x08U : 0;
                    w1.CF |= (w1.vf[vffs].z > +vfftw) ? 0x10U : 0;
                    w1.CF |= (w1.vf[vffs].z < -vfftw) ? 0x20U : 0;
                }
                else if (UI.StartsWith("FTOI12.")) { //[FTOI12.
                    int k = SUt.isDests(UI.Substring(7));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.FToI12(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.FToI12(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.FToI12(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.FToI12(w1.vf[vffs].w);
                    }
                }
                else if (UI.StartsWith("ITOF4.")) { //[ITOF4.
                    int k = SUt.isDests(UI.Substring(6));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.IToF4(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.IToF4(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.IToF4(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.IToF4(w1.vf[vffs].w);
                    }
                }
                else if (UI.StartsWith("ITOF12.")) { //[ITOF12.
                    int k = SUt.isDests(UI.Substring(7));
                    int vfft = SUt.isVF(Ual[1]);
                    int vffs = SUt.isVF(Ual[2]);
                    if (vfft != 0) {
                        if (0 != (k & 8)) w1.vf[vfft].x = ITOFUt.IToF12(w1.vf[vffs].x);
                        if (0 != (k & 4)) w1.vf[vfft].y = ITOFUt.IToF12(w1.vf[vffs].y);
                        if (0 != (k & 2)) w1.vf[vfft].z = ITOFUt.IToF12(w1.vf[vffs].z);
                        if (0 != (k & 1)) w1.vf[vfft].w = ITOFUt.IToF12(w1.vf[vffs].w);
                    }
                }
                else if (UI.StartsWith("MAX.")) { //[MAX.
                    int k = SUt.isDests(UI.Substring(4));
                    int vfft = SUt.isVF(Ual[3]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = Math.Max(w1.vf[vffs].x, w1.vf[vfft].x);
                        if (0 != (k & 4)) w1.vf[vffd].y = Math.Max(w1.vf[vffs].y, w1.vf[vfft].y);
                        if (0 != (k & 2)) w1.vf[vffd].z = Math.Max(w1.vf[vffs].z, w1.vf[vfft].z);
                        if (0 != (k & 1)) w1.vf[vffd].w = Math.Max(w1.vf[vffs].w, w1.vf[vfft].w);
                    }
                }
                else if (UI.StartsWith("MINI.")) { //[MINI.
                    int k = SUt.isDests(UI.Substring(5));
                    int vfft = SUt.isVF(Ual[3]);
                    int vffs = SUt.isVF(Ual[2]);
                    int vffd = SUt.isVF(Ual[1]);
                    if (vffd != 0) {
                        if (0 != (k & 8)) w1.vf[vffd].x = Math.Min(w1.vf[vffs].x, w1.vf[vfft].x);
                        if (0 != (k & 4)) w1.vf[vffd].y = Math.Min(w1.vf[vffs].y, w1.vf[vfft].y);
                        if (0 != (k & 2)) w1.vf[vffd].z = Math.Min(w1.vf[vffs].z, w1.vf[vfft].z);
                        if (0 != (k & 1)) w1.vf[vffd].w = Math.Min(w1.vf[vffs].w, w1.vf[vfft].w);
                    }
                }
                else throw new NotSupportedException(U.ToString());
            }

            // ->->-> Post

            w1.tpc += 8;
            if (w1.branch) {
                w1.branch = false;
                w1.tpc = w1.bpc;
            }
            else if (b) {
                w1.branch = true;
            }
            tickcnt++;
        }

        int tickcnt = 0;

        class ITOFUt {
            public static float IToF0(float vf) {
                return UtCore1.ITOF0(vf);
            }
            public static float IToF4(float vf) {
                return UtCore1.ITOF4(vf);
            }
            public static float IToF12(float vf) {
                return UtCore1.ITOF12(vf);
            }

            public static float FToI0(float vf) {
                return UtCore1.FTOI0(vf);
                //return IFUt.SI2F((int)IFUt.vuDouble(IFUt.F2UI(vf)));
            }
            public static float FToI4(float vf) {
                return UtCore1.FTOI4(vf);
                //return IFUt.SI2F(IFUt.float_to_int4(IFUt.vuDouble(IFUt.F2UI(vf))));
            }
            public static float FToI12(float vf) {
                return UtCore1.FTOI12(vf);
                //return IFUt.SI2F(IFUt.float_to_int12(IFUt.vuDouble(IFUt.F2UI(vf))));
            }

            public static float oFToI0(float vf) {
                return (ushort)((int)(vf));
            }
            public static float oFToI4(float vf) {
                return (ushort)((int)(vf * 16));
            }
            public static float oFToI12(float vf) {
                return (ushort)((int)(vf * 4096));
            }
        }

        class IFUt {
            public static float UI2F(uint v) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write(v);
                os.Position = 0;
                return new BinaryReader(os).ReadSingle();
            }
            public static float SI2F(int v) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write(v);
                os.Position = 0;
                return new BinaryReader(os).ReadSingle();
            }
            public static uint F2UI(float f) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write(f);
                os.Position = 0;
                return new BinaryReader(os).ReadUInt32();
            }

            public static float vuDouble(uint f) {
                switch (f & 0x7f800000u) {
                    case 0x0u:
                        f &= 0x80000000u;
                        return IFUt.UI2F(f);
                    case 0x7f800000u:
                        return IFUt.UI2F((f & 0x80000000u) | 0x7f7fffffu);
                    default:
                        return IFUt.UI2F(f);
                }
            }
            public static int float_to_int4(float x) {
                return (int)((float)x * (1.0f / 0.0625f));
            }
            public static int float_to_int12(float x) {
                return (int)((float)x * (1.0f / 0.000244140625f));
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            doStep();
            refreshList();
            refreshRegs();
        }

        private void onMemWr(int para) {
            refreshBin(para);
            hexVwer1.Invalidate();
            hexVwer2.Invalidate();
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
        }

        private void buttonRunUntill_Click(object sender, EventArgs e) {
            string spos = Interaction.InputBox("Run untill your specified offset.", "", "", -1, -1);
            if (spos.Length == 0) return;
            int pos = int.Parse(spos);

            do {
                doStep();
            } while (w1.tpc / 8 != pos);

            refreshList();
            refreshRegs();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (char.ToLower(e.KeyChar) == 'g') {
                e.Handled = true;
                string addr = Interaction.InputBox("Where you go? (e.g. 440)", "", "", -1, -1);
                if (addr.Length == 0) return;
                try {
                    int off = Convert.ToInt32(addr, 16);
                    listBox1.SelectedIndex = Math.Min(listBox1.Items.Count - 1, Math.Max(0, off / 16));
                }
                catch (FormatException) { MessageBox.Show("Invalid offset"); return; }
            }
        }

        private void buttonSelSeta_Click(object sender, EventArgs e) {
            ContextMenuStrip cms = new ContextMenuStrip();
            foreach (FSet fs in alfs) {
                ToolStripItem tsi = (ToolStripItem)cms.Items.Add(fs.name);
                tsi.Tag = fs;
                tsi.Click += new EventHandler(tsi_Click);
            }
            cms.Show(buttonSelSeta, Point.Empty);
        }

        void tsi_Click(object sender, EventArgs e) {
            ToolStripItem tsi = (ToolStripItem)sender;
            FSet fs = (FSet)tsi.Tag;

            openHelp(fs.fptxt);
            openBin(fs.fpbin);

            tickcnt = 0;
            labelTick.Text = "Tick: " + tickcnt;
        }
    }
    public enum HTRSel {
        vi0, vi1, vi2, vi3, vi4, vi5, vi6, vi7, vi8, vi9, vi10, vi11, vi12, vi13, vi14, vi15,
        vf0x, vf0y, vf0z, vf0w,
        vf1x, vf1y, vf1z, vf1w,
        vf2x, vf2y, vf2z, vf2w,
        vf3x, vf3y, vf3z, vf3w,
        vf4x, vf4y, vf4z, vf4w,
        vf5x, vf5y, vf5z, vf5w,
        vf6x, vf6y, vf6z, vf6w,
        vf7x, vf7y, vf7z, vf7w,
        vf8x, vf8y, vf8z, vf8w,
        vf9x, vf9y, vf9z, vf9w,
        vf10x, vf10y, vf10z, vf10w,
        vf11x, vf11y, vf11z, vf11w,
        vf12x, vf12y, vf12z, vf12w,
        vf13x, vf13y, vf13z, vf13w,
        vf14x, vf14y, vf14z, vf14w,
        vf15x, vf15y, vf15z, vf15w,
        vf16x, vf16y, vf16z, vf16w,
        vf17x, vf17y, vf17z, vf17w,
        vf18x, vf18y, vf18z, vf18w,
        vf19x, vf19y, vf19z, vf19w,
        vf20x, vf20y, vf20z, vf20w,
        vf21x, vf21y, vf21z, vf21w,
        vf22x, vf22y, vf22z, vf22w,
        vf23x, vf23y, vf23z, vf23w,
        vf24x, vf24y, vf24z, vf24w,
        vf25x, vf25y, vf25z, vf25w,
        vf26x, vf26y, vf26z, vf26w,
        vf27x, vf27y, vf27z, vf27w,
        vf28x, vf28y, vf28z, vf28w,
        vf29x, vf29y, vf29z, vf29w,
        vf30x, vf30y, vf30z, vf30w,
        vf31x, vf31y, vf31z, vf31w,
        accx, accy, accz, accw,
        I, Q, R, P,
    }
}