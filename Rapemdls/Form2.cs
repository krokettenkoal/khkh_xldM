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
using Microsoft.VisualBasic;

namespace Rapemdls {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialogMset.FileName))
                openFileDialogMset.ShowDialog(this);
            if (File.Exists(openFileDialogMset.FileName))
                loadMset(openFileDialogMset.FileName);

#if false
            if (!File.Exists(openFileDialogtr5.FileName))
                openFileDialogtr5.ShowDialog(this);
            if (File.Exists(openFileDialogtr5.FileName))
                loadTr5(openFileDialogtr5.FileName);
#endif
        }

        byte[] bin = null;

        private void loadMset(string fp) {
            bin = File.ReadAllBytes(fp);

            hexVwer1.SetBin(bin);

            parseMset(bin);

            linkLabel1.Text = Path.GetFileNameWithoutExtension(fp);
        }

        List<int> alToU3 = new List<int>();
        public static U1 u1 = null;

        private void parseMset(byte[] bin) {
            alst.Clear();
            alst.Add(new St(0x80, 0x40, 16, "U1"));
            int offToU1 = 0x80;
            int offToU2 = offToU1 + 0x40;

            alToU3.Clear();

            u1 = new U1();
            u1.u2 = new U2();

            if (true) { // U2
                MemoryStream si = new MemoryStream(bin, false);
                si.Position = offToU2;
                BinaryReader br = new BinaryReader(si);
                int cntU2 = 0;
                for (; ; cntU2++) {
                    int off = br.ReadInt32();
                    if (off == -1) break;
                    alToU3.Add(offToU2 + off);
                }

                alst.Add(new St(offToU2, 4 * (cntU2 + 1), 4, "U2(0)"));

                for (int t = 0; t < cntU2; t++) {
                    alst.Add(new St(alToU3[t], 0x40, 16, "U3(" + t + ")"));
                }
            }
            if (true) {
                for (int i3 = 0; i3 < alToU3.Count; i3++) { // U3
                    U3 u3 = new U3();
                    u1.u2.alu3.Add(u3);
                    MemoryStream si = new MemoryStream(bin, false);
                    BinaryReader br = new BinaryReader(si);
                    int offToU3 = alToU3[i3];
                    si.Position = offToU3;
                    int v00 = br.ReadInt32();
                    int v04 = br.ReadInt32();
                    int v08 = br.ReadInt32();
                    int v0c = br.ReadInt32();
                    int v10 = br.ReadInt32(); // cnt U7
                    int v14 = br.ReadInt32(); // off to U7
                    int v18 = br.ReadInt32(); // cnt U4
                    int v1c = br.ReadInt32(); // off to U4
                    int v20 = br.ReadInt32(); // cnt U4`
                    int v24 = br.ReadInt32(); // off to U4`
                    int v28 = br.ReadInt32(); // off to U5
                    int v2c = br.ReadInt32(); // cnt U9
                    int v30 = br.ReadInt32(); // off to U9
                    int v34 = br.ReadInt32(); // cnt U8
                    int v38 = br.ReadInt32(); // off to U8
                    int v3c = br.ReadInt32(); // off to U6

                    //Debug.Assert((uint)v1c < bin.Length);
                    Debug.Assert((uint)v24 < bin.Length);
                    Debug.Assert((uint)v28 < bin.Length);
                    alst.Add(new St(offToU3 + v1c, 6 * v18, 6, "U4(" + i3 + ")"));
                    alst.Add(new St(offToU3 + v24, 6 * v20, 6, "U4(" + i3 + ")`"));
                    if (true) { // scan U4
                        int maxcnt5 = 0;
                        if ((uint)v1c < bin.Length) {
                            si.Position = offToU3 + v1c;
                            for (int i4 = 0; i4 < v18; i4++) {
                                U4 u4 = new U4();
                                u3.alu4a.Add(u4);
                                u4.v00 = br.ReadUInt16();
                                u4.v02 = br.ReadByte();
                                int cnt5 = br.ReadByte(); u4.v03 = cnt5;
                                int index5 = br.ReadUInt16(); u4.v04 = index5;
                                maxcnt5 = Math.Max(maxcnt5, index5 + cnt5);
                            }
                        }
                        si.Position = offToU3 + v24;
                        for (int i4 = 0; i4 < v20; i4++) {
                            U4 u4 = new U4();
                            u3.alu4b.Add(u4);
                            u4.v00 = br.ReadUInt16();
                            u4.v02 = br.ReadByte();
                            int cnt5 = br.ReadByte(); u4.v03 = cnt5;
                            int index5 = br.ReadUInt16(); u4.v04 = index5;
                            maxcnt5 = Math.Max(maxcnt5, index5 + cnt5);
                        }
                        alst.Add(new St(offToU3 + v28, 16 * maxcnt5, 16, "U5(" + i3 + ")"));

                        if (true) { // U5
                            si.Position = offToU3 + v28;
                            for (int i5 = 0; i5 < maxcnt5; i5++) {
                                U5 u5 = new U5();
                                u3.alu5.Add(u5);
                                u5.v00 = br.ReadUInt16();
                                u5.v02 = br.ReadUInt16();
                                u5.v04 = br.ReadSingle();
                                u5.v08 = br.ReadSingle();
                                u5.v0c = br.ReadSingle();
                            }
                        }
                    }

                    if (true) { // U6
                        si.Position = offToU3 + v3c;
                        br.ReadInt32();
                        int cntU6sub = br.ReadUInt16();
                        alst.Add(new St(offToU3 + v3c, 16 + 16 * cntU6sub, 16, "U6(" + i3 + ")"));
                    }
                    if (true) { // U7
                        alst.Add(new St(offToU3 + v14, 4 * v10, 16, "U7(" + i3 + ")"));

                        si.Position = offToU3 + v14;
                        for (int i7 = 0; i7 < v10; i7++) {
                            U7 u7 = new U7();
                            u3.alu7.Add(u7);
                            u7.v00 = br.ReadInt32();
                        }
                    }
                    if (true) { // U8
                        alst.Add(new St(offToU3 + v38, 8 * v34, 8, "U8(" + i3 + ")"));

                        si.Position = offToU3 + v38;
                        for (int i8 = 0; i8 < v34; i8++) {
                            U8 u8 = new U8();
                            u3.alu8.Add(u8);
                            u8.v00 = br.ReadUInt16();
                            u8.v02 = br.ReadUInt16();
                            u8.v04 = br.ReadSingle();
                        }
                    }
                    if (true) { // U9
                        alst.Add(new St(offToU3 + v30, 0x28 * v2c, 0x28, "U9(" + i3 + ")"));

                        si.Position = offToU3 + v30;
                        for (int i9 = 0; i9 < v2c; i9++) {
                            U9 u9 = new U9();
                            u3.alu9.Add(u9);
                            u9.v00 = br.ReadSingle();
                            u9.v04 = br.ReadSingle();
                            u9.v08 = br.ReadSingle();
                            u9.v0c = br.ReadInt32();
                            u9.v10 = br.ReadSingle();
                            u9.v14 = br.ReadSingle();
                            u9.v18 = br.ReadSingle();
                            u9.v1c = br.ReadSingle();
                            u9.v20 = br.ReadSingle();
                            u9.v24 = br.ReadSingle();
                        }
                    }
                }
            }

            alst.Sort();

            listBoxst.Items.Clear();
            listBoxst.Items.AddRange(alst.ToArray());

            hexVwer1.RangeMarkedList.Clear();
            foreach (St o in alst) {
                hexVwer1.AddRangeMarked(o.off, o.len, Color.FromArgb(50, Color.OrangeRed), Color.FromArgb(100, Color.OrangeRed));
            }
        }

        class St : IComparable {
            public int off, len, cx;
            public string id;

            public St(int off, int len, int cx, string id) {
                this.off = off;
                this.len = len;
                this.cx = cx;
                this.id = id;
            }

            public override string ToString() {
                return string.Format("{0:X6} {1,-7} {2,2}", off, id, cx);
            }

            #region IComparable ƒƒ“ƒo

            public int CompareTo(object obj) {
                St o = (St)obj;
                return this.off.CompareTo(o.off);
            }

            #endregion
        }
        List<St> alst = new List<St>();

        class Tr5 {
            public int timet, pc0, addr, i, caller;

            public Tr5(int i, int timet, int pc0, int addr, int caller) {
                this.i = i;
                this.timet = timet;
                this.pc0 = pc0;
                this.addr = addr;
                this.caller = caller;
            }

            public override string ToString() {
                return string.Format("#{0} {1:X6} @{2:00}", i, addr, caller);
            }
        }
        List<Tr5> altr5 = new List<Tr5>();
        SortedList<int, int> pc2caller = new SortedList<int, int>();

        private void loadTr5(string fp) {
            listBoxtr.Items.Clear();

            using (StreamReader rr = new StreamReader(fp, Encoding.ASCII)) {
                string row;
                Regex rex = new Regex("^tr5 t ([0-9a-f]{8}) pc0 ([0-9a-f]{8}) @ ([0-9a-f]{8})", RegexOptions.IgnoreCase);
                int x = 0;
                while (null != (row = rr.ReadLine())) {
                    Match M = rex.Match(row);
                    if (M.Success) {
                        int pc0 = Convert.ToInt32(M.Groups[2].Value, 16);
                        if (pc2caller.ContainsKey(pc0) == false)
                            pc2caller[pc0] = pc2caller.Count;
                        int caller = pc2caller[pc0];
                        int off = Convert.ToInt32(M.Groups[3].Value, 16) - 0x1067B80;
                        hexVwer1.AddMark(off);
                        altr5.Add(new Tr5(
                            x,
                            Convert.ToInt32(M.Groups[1].Value, 16),
                            pc0,
                            off,
                            caller
                            ));
                        x++;
                    }
                }
            }

            listBoxtr.Items.AddRange(altr5.ToArray());
        }

        private void listBoxtr_SelectedIndexChanged(object sender, EventArgs e) {
            Tr5 o = (Tr5)listBoxtr.SelectedItem;
            if (o == null) return;
            hexVwer1.SetPos(o.addr & (~0x1FF));
            hexVwer1.SetSel(o.addr);
        }

        private void hexVwer1_KeyDown(object sender, KeyEventArgs e) { }

        private void hexVwer1_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case '[':
                    hexVwer1.ByteWidth = Math.Max(1, hexVwer1.ByteWidth - 1);
                    e.Handled = true;
                    return;
                case ']':
                    hexVwer1.ByteWidth = Math.Min(64, hexVwer1.ByteWidth + 1);
                    e.Handled = true;
                    return;
            }
        }

        private void listBoxst_SelectedIndexChanged(object sender, EventArgs e) {
            St o = (St)listBoxst.SelectedItem;
            if (o == null) return;
            hexVwer1.SetPos(o.off & (~0x1FF));
            hexVwer1.SetSel(o.off);
        }

        private void buttonExplode_Click(object sender, EventArgs e) {
            string which = Interaction.InputBox("Which? (0 to " + (alToU3.Count - 1) + ")", "", "0", -1, -1);
            if (which.Length == 0) return;
            int i = int.Parse(which);

            int offU3 = alToU3[i];
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);

            si.Position = offU3 + 0x18;
            int cntU4 = br.ReadInt32();
            int offU4 = br.ReadInt32() + offU3;
            si.Position = offU3 + 0x20;
            int cntU4b = br.ReadInt32();
            int offU4b = br.ReadInt32() + offU3;
            si.Position = offU3 + 0x28;
            int offU5 = br.ReadInt32() + offU3;
            si.Position = offU3 + 0x34;
            int cntU8 = br.ReadInt32();
            int offU8 = br.ReadInt32() + offU3;
            si.Position = offU3 + 0x10;
            int cntU7 = br.ReadInt32();
            int offU7 = br.ReadInt32() + offU3;
            si.Position = offU3 + 0x2C;
            int cntU9 = br.ReadInt32();
            int offU9 = br.ReadInt32() + offU3;

            StringBuilder s = new StringBuilder();

            int cntU5 = 0;

            si.Position = offU4;
            for (int t = 0; t < cntU4; t++) {
                int v00 = br.ReadUInt16();
                int v02 = br.ReadByte();
                int v03 = br.ReadByte();
                int v04 = br.ReadUInt16();
                cntU5 = Math.Max(cntU5, v04 + v03);
                s.AppendFormat("U4a_{0:X4} = {1:X4} {2:X2} {3:X2} U5_{4:X4}\r\n"
                    , t, v00, v02, v03, v04);
            }
            s.AppendLine();

            si.Position = offU4b;
            for (int t = 0; t < cntU4b; t++) {
                int v00 = br.ReadUInt16();
                int v02 = br.ReadByte();
                int v03 = br.ReadByte();
                int v04 = br.ReadUInt16();
                cntU5 = Math.Max(cntU5, v04 + v03);
                s.AppendFormat("U4b_{0:X4} = {1:X4} {2:X2} {3:X2} U5_{4:X4}\r\n"
                    , t, v00, v02, v03, v04);
            }
            s.AppendLine();

            si.Position = offU5;
            for (int t = 0; t < cntU5; t++) {
                int v00 = br.ReadUInt16();
                int v02 = br.ReadUInt16();
                float v04 = br.ReadSingle();
                float v08 = br.ReadSingle();
                float v0c = br.ReadSingle();
                s.AppendFormat("U5_{0:X4} = {1:X4} {2:X4} {3,10:0.000} {4,10:0.000} {5,10:0.000}\r\n"
                    , t, v00, v02, v04, v08, v0c);
            }
            s.AppendLine();

            si.Position = offU8;
            for (int t = 0; t < cntU8; t++) {
                int v00 = br.ReadUInt16();
                int v02 = br.ReadUInt16();
                float v04 = br.ReadSingle();
                s.AppendFormat("U8_{0:X4} = {1:X4} {2:X4} {3,10:0.000}\r\n"
                    , t, v00, v02, v04);
            }
            s.AppendLine();

            si.Position = offU7;
            for (int t = 0; t < cntU7; t++) {
                int v00 = br.ReadInt32();
                s.AppendFormat("U7_{0:X4} = {1:X8} | {2}\r\n"
                    , t, v00, BitaUtil.toStr(v00));
            }
            s.AppendLine();

            si.Position = offU9;
            for (int t = 0; t < cntU9; t++) {
                float v00 = br.ReadSingle();
                float v04 = br.ReadSingle();
                float v08 = br.ReadSingle();
                int v0c = br.ReadInt32();
                float v10 = br.ReadSingle();
                float v14 = br.ReadSingle();
                float v18 = br.ReadSingle();
                float v1c = br.ReadSingle();
                float v20 = br.ReadSingle();
                float v24 = br.ReadSingle();
                s.AppendFormat("U9_{0:X4} = {1,10:0.000} {2,10:0.000} {3,10:0.000}|{4,2}|{5,10:0.000} {6,10:0.000} {7,10:0.000}|{8,10:0.000} {9,10:0.000} {10,10:0.000}\r\n"
                    , t, v00, v04, v08, v0c, v10, v14, v18, v1c, v20, v24);
            }
            s.AppendLine();

            Clipboard.SetText(s.ToString());
            MessageBox.Show("Work done.");
        }

        class BitaUtil {
            public static string toStr(int val) {
                string s = "";
                for (int x = 0; x < 32; x++) {
                    s += (((val << x) & 0x80000000) != 0) ? '1' : '0';
                }
                return s;
            }
        }

        private void buttonExplodeU8_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();

            string which = Interaction.InputBox("Which? (0 to " + (alToU3.Count - 1) + ")", "", "0", -1, -1);
            if (which.Length == 0) return;
            int i = int.Parse(which);

            int offU3 = alToU3[i];
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);

            si.Position = offU3 + 0x34;
            int cntU8 = br.ReadInt32();
            int offU8 = br.ReadInt32() + offU3;

            si.Position = offU8;
            for (int t = 0; t < cntU8; t++) {
                int v00 = br.ReadUInt16();
                int v02 = br.ReadUInt16();
                float v04 = br.ReadSingle();
                s.AppendFormat("U8_{0:X4} = #{1:000} {4}{5}{6}{7}{8}{9}{10}{11}{12} {3,12:0.00000}\r\n"
                    , t, v00, v02, v04
                    , (v02 == 1) ? "Sx" : "  "
                    , (v02 == 2) ? "Sy" : "  "
                    , (v02 == 3) ? "Sz" : "  "
                    , (v02 == 4) ? "Rx" : "  "
                    , (v02 == 5) ? "Ry" : "  "
                    , (v02 == 6) ? "Rz" : "  "
                    , (v02 == 7) ? "Tx" : "  "
                    , (v02 == 8) ? "Ty" : "  "
                    , (v02 == 9) ? "Tz" : "  "
                    );
            }
            s.AppendLine();

            Clipboard.SetText(s.ToString());
            MessageBox.Show("Work done.");
        }

        private void buttonSpecu1_Click(object sender, EventArgs e) {
            for (int t = 35; t < 806; t++) {
                Tr5 o = (Tr5)listBoxtr.Items[t];
            }

            SortedDictionary<int, int> caller2pc = new SortedDictionary<int, int>();
            foreach (KeyValuePair<int, int> kv in pc2caller) caller2pc[kv.Value] = kv.Key;

            StringBuilder s = new StringBuilder();
            foreach (KeyValuePair<int, int> kv in caller2pc) {
                s.AppendFormat("@{0} {1:x8}\r\n", kv.Key, kv.Value);
            }

            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied.");
        }

        private void Form2_DragDrop(object sender, DragEventArgs e) {
            string[] fs = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (fs != null && fs.Length != 0) {
                loadMset(fs[0]);
            }
        }

        private void Form2_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

    }
}
namespace Rapemdls {
    public class U1 {
        public U2 u2 = null;
    }
    public class U2 {
        public List<U3> alu3 = new List<U3>();
    }
    public class U3 {
        public List<U9> alu9 = new List<U9>();
        public List<U4> alu4a = new List<U4>();
        public List<U4> alu4b = new List<U4>();
        public List<U5> alu5 = new List<U5>();
        public List<U8> alu8 = new List<U8>();
        public List<U7> alu7 = new List<U7>();
    }
    public class U9 {
        public float v00, v04, v08;
        public int v0c;
        public float v10, v14, v18, v1c, v20, v24;
    }
    public class U4 {
        public int v00, v02, v03, v04;
    }
    public class U5 {
        public int v00, v02;
        public float v04, v08, v0c;
    }
    public class U8 {
        public int v00, v02;
        public float v04;
    }
    public class U7 {
        public int v00;
    }
}
