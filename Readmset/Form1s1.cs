//#define _P_EX_110 // roxas
#define _P_EX_110_2nd

// M_EX770_MEMO.mset

// 実験対象 M_EX530.mdlx 位置が不整合 → ok
//  1 足
//  43 本
//  64 胴体

// 実験対象 M_EX540.mdlx 足が不整合 左右に振っていない

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using khkh_xldM;
using Readmset.Properties;

namespace Readmset {
    public partial class Form1s1 : Form {
        public Form1s1() {
            InitializeComponent();
        }

        private void Form1s1_Load(object sender, EventArgs e) {
#if _P_EX_110   
            openFileDialogTestreadText.FileName = @"H:\Proj\khkh_xldM\MEMO\testRead4.200706030001.txt";

            if (!File.Exists(openFileDialogMset.FileName))
                openFileDialogMset.ShowDialog(this);
            if (!File.Exists(openFileDialogMset.FileName))
                Close();

            loadMset(openFileDialogMset.FileName);

            if (!File.Exists(openFileDialogTestreadText.FileName))
                openFileDialogTestreadText.ShowDialog(this);
            if (!File.Exists(openFileDialogTestreadText.FileName))
                Close();

            loadText(openFileDialogTestreadText.FileName, openFileDialogTestreadDictUser.FileName, null);

#elif _P_EX_110_2nd
            //openFileDialogTestreadText.FileName = @"H:\Proj\khkh_xldM\MEMO\tr5\tr5.0.txt";
            openFileDialogTestreadText.FileName = @"H:\Proj\khkh_xldM\MEMO\tr5\tr5.x1.0.txt";

            if (File.Exists(openFileDialogMset.FileName) == false)
                openFileDialogMset.ShowDialog(this);
            if (File.Exists(openFileDialogMset.FileName)) {
                loadMset(openFileDialogMset.FileName);
            }

            if (File.Exists(openFileDialogTestreadText.FileName) == false)
                openFileDialogTestreadText.ShowDialog(this);
            if (File.Exists(openFileDialogTestreadText.FileName)) {
                loadText5(openFileDialogTestreadText.FileName, openFileDialogTestreadDictUser.FileName, null);
            }

#else
            openFileDialogMset.FileName = @"V:\KH2.yaz0r\dump_kh2\obj\M_EX770_MEMO.mset";

            if (!File.Exists(openFileDialogMset.FileName))
                openFileDialogMset.ShowDialog(this);
            if (!File.Exists(openFileDialogMset.FileName))
                Close();

            sysloadMset(openFileDialogMset.FileName);
#endif
        }

        class ReadAcc {
            public int i, addr, fake, accessor;

            public ReadAcc(int i, int addr, int fake, int accessor) {
                this.i = i;
                this.addr = addr;
                this.fake = fake;
                this.accessor = accessor;
            }

            public override string ToString() {
                return string.Format("#{2} {0:X6} {1,4}", fake, accessor, i);
            }
        }

        SortedDictionary<int, int> acc2addrmap = new SortedDictionary<int, int>();

        private void loadText5(string fp, string fpxmlDictLoadFrom, string fpxmlDictSaveTo) {
            Regex rex = new Regex("^tr5 pc ([0-9A-F]{8}) @ ([0-9A-F]{8})", RegexOptions.IgnoreCase);
            SortedDictionary<int, int> dictuser = new SortedDictionary<int, int>();
            if (fpxmlDictLoadFrom != null) {
                IFormatter ser = new BinaryFormatter();
                using (FileStream fs = File.OpenRead(fpxmlDictLoadFrom)) {
                    dictuser = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    acc2addrmap = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    fs.Close();
                }
            }
            int index = 0;
            foreach (string row in File.ReadAllLines(fp)) {
                Match M = rex.Match(row);
                if (M.Success) {
                    int user = Convert.ToInt32(M.Groups[1].Value, 16);
                    int addr = Convert.ToInt32(M.Groups[2].Value, 16);

                    int accessor = 0;
                    if (dictuser.ContainsKey(user)) {
                        accessor = dictuser[user];
                    }
                    else {
                        dictuser[user] = (accessor = dictuser.Count);
                        acc2addrmap[accessor] = user;
                    }

                    addr += -0x9E0340;
                    listBox1.Items.Add(new ReadAcc(index, addr, (addr + STATIC_DELTA) & 0xFFFFFF, accessor));
                    hexVwer1.AddMark(addr);
                    index++;
                }
            }
            if (fpxmlDictSaveTo != null) {
                IFormatter ser = new BinaryFormatter();
                using (FileStream fs = File.Create(fpxmlDictSaveTo)) {
                    ser.Serialize(fs, dictuser);
                    ser.Serialize(fs, acc2addrmap);
                    fs.Close();
                }
            }
        }
        private void loadText(string fp, string fpxmlDictLoadFrom, string fpxmlDictSaveTo) {
            Regex rex = new Regex("^A\\>testRead4\\: pc0\\(([0-9A-F]{8})\\) @ ([0-9A-F]{8})", RegexOptions.IgnoreCase);
            SortedDictionary<int, int> dictuser = new SortedDictionary<int, int>();
            if (fpxmlDictLoadFrom != null) {
                IFormatter ser = new BinaryFormatter();
                using (FileStream fs = File.OpenRead(fpxmlDictLoadFrom)) {
                    dictuser = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    acc2addrmap = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    fs.Close();
                }
            }
            int index = 0;
            foreach (string row in File.ReadAllLines(fp)) {
                Match M = rex.Match(row);
                if (M.Success) {
                    int user = Convert.ToInt32(M.Groups[1].Value, 16);
                    int addr = Convert.ToInt32(M.Groups[2].Value, 16);

                    int accessor = 0;
                    if (dictuser.ContainsKey(user)) {
                        accessor = dictuser[user];
                    }
                    else {
                        dictuser[user] = (accessor = dictuser.Count);
                        acc2addrmap[accessor] = user;
                    }

                    addr += -0x9E0340;
                    listBox1.Items.Add(new ReadAcc(index, addr, (addr + STATIC_DELTA) & 0xFFFFFF, accessor));
                    hexVwer1.AddMark(addr);
                    index++;
                }
            }
            if (fpxmlDictSaveTo != null) {
                IFormatter ser = new BinaryFormatter();
                using (FileStream fs = File.Create(fpxmlDictSaveTo)) {
                    ser.Serialize(fs, dictuser);
                    ser.Serialize(fs, acc2addrmap);
                    fs.Close();
                }
            }
        }

        byte[] bin;
#if _P_EX_110
        const int STATIC_DELTA = -98768; // for P_EX110.mset
#else
        const int STATIC_DELTA = -0x8A0; // for M_EX770_MEMO.mset
#endif
        // const int STATIC_DELTA = 0;

        int fbaseoff = 0;
        int fsize = 0;

        class PosTbl {
            public int tbloff = 0x90;

            public int va0;
            public int va2; // cnt t4
            public int va8; // off t5 (each 64 bytes)  { cnt_t5 = va2 -va0 }
            public int vac; // off t4 (each 4 bytes)
            public int vb0; // cnt t11
            public int vb4; // off t1 (each 8 bytes)
            public int vb8; // cnt t1
            public int vc0; // off t2 (each 6 bytes)
            public int vc4; // cnt t2
            public int vc8; // off t2` (each 6 bytes)
            public int vcc; // cnt t2`
            public int vd0; // off t9 (each 8 bytes)
            public int vd4; // off t11 (each 4 bytes)
            public int vd8; // off t10 (each 4 bytes)
            public int vdc; // off t12 (each 4 bytes)
            public int ve0; // off t3 (each 12 bytes)
            public int ve4; // cnt t3
            public int ve8;
            public int vec; // off t8 (each 48 bytes)  { cnt_t8 = cnt_t2` }
            public int vf0; // off t7 (each 8 bytes)
            public int vf4; // cnt t7
            public int vf8; // off t6 (each 12 bytes)
            public int vfc; // cnt t6

            public PosTbl(Stream si) {
                BinaryReader br = new BinaryReader(si);
                int off = tbloff - 0x90;

                // ORG→
                si.Position = off + 0xA0;
                va0 = br.ReadUInt16();
                va2 = br.ReadUInt16(); // cnt t4
                si.Position = off + 0xA8;
                va8 = br.ReadInt32(); // off t5 (each 64 bytes)  { cnt_t5 = va2 -va0 }
                vac = br.ReadInt32(); // off t4 (each 4 bytes)

                si.Position = off + 0xB0;
                vb0 = br.ReadInt32(); // cnt t11
                vb4 = br.ReadInt32(); // off t1 (each 8 bytes)
                vb8 = br.ReadInt32(); // cnt t1
                si.Position = off + 0xC0;
                vc0 = br.ReadInt32(); // off t2 (each 6 bytes)
                vc4 = br.ReadInt32(); // cnt t2
                vc8 = br.ReadInt32(); // off t2` (each 6 bytes)
                vcc = br.ReadInt32(); // cnt t2`
                si.Position = off + 0xD0;
                vd0 = br.ReadInt32(); // off t9 (each 8 bytes)
                vd4 = br.ReadInt32(); // off t11 (each 4 bytes)
                vd8 = br.ReadInt32(); // off t10 (each 4 bytes)
                vdc = br.ReadInt32(); // off t12 (each 4 bytes)
                si.Position = off + 0xE0;
                ve0 = br.ReadInt32(); // off t3 (each 12 bytes)
                ve4 = br.ReadInt32(); // cnt t3
                ve8 = br.ReadInt32();
                vec = br.ReadInt32(); // off t8 (each 48 bytes)  { cnt_t8 = cnt_t2` }
                si.Position = off + 0xF0;
                vf0 = br.ReadInt32(); // off t7 (each 8 bytes)
                vf4 = br.ReadInt32(); // cnt t7
                vf8 = br.ReadInt32(); // off t6 (each 12 bytes)
                vfc = br.ReadInt32(); // cnt t6
                // ←ORG
            }
        }

        void blitMset(MemoryStream si, int baseoff) {
            BinaryReader br = new BinaryReader(si);
            fbaseoff = baseoff;
            fsize = (int)si.Length;

            PosTbl p = new PosTbl(si);
            int tbloff = p.tbloff;

            listBoxJtbl.Items.Clear();
            listBoxJtbl.Items.Add(new Jtbl("t", 0, 16));
            listBoxJtbl.Items.Add(new Jtbl("t1", p.vb4, 8));
            listBoxJtbl.Items.Add(new Jtbl("t2", p.vc0, 6));
            listBoxJtbl.Items.Add(new Jtbl("t2x", p.vc8, 6));
            listBoxJtbl.Items.Add(new Jtbl("t3", p.ve0, 12));
            listBoxJtbl.Items.Add(new Jtbl("t4", p.vac, 4));
            listBoxJtbl.Items.Add(new Jtbl("t5", p.va8, 16)); // 64
            listBoxJtbl.Items.Add(new Jtbl("t6", p.vf8, 12));
            listBoxJtbl.Items.Add(new Jtbl("t7", p.vf0, 8));
            listBoxJtbl.Items.Add(new Jtbl("t8", p.vec, 16)); // 48
            listBoxJtbl.Items.Add(new Jtbl("t9", p.vd0, 8));
            listBoxJtbl.Items.Add(new Jtbl("t10", p.vd8, 4));
            listBoxJtbl.Items.Add(new Jtbl("t11", p.vd4, 4));
            listBoxJtbl.Items.Add(new Jtbl("t12", p.vdc, 4));

            int cntt9 = 0, cntt10 = 0, cntt12 = 0;
            if (true) { // cntt9
                si.Position = baseoff + tbloff + p.vc0 - baseoff; // t2
                for (int i2 = 0; i2 < p.vc4; i2++) {
                    br.ReadByte();
                    br.ReadByte();
                    br.ReadByte();
                    int tcx = br.ReadByte();
                    int tx = br.ReadUInt16();
                    cntt9 = Math.Max(cntt9, tx + tcx);
                }

                if (true) { // cntt10, cntt12
                    si.Position = baseoff + tbloff + p.vd0 - baseoff; // t9
                    for (int i9 = 0; i9 < cntt9; i9++) {
                        br.ReadUInt16();
                        int ti10 = br.ReadUInt16(); cntt10 = Math.Max(cntt10, ti10 + 1);
                        int ti12a = br.ReadUInt16(); cntt12 = Math.Max(cntt12, ti12a + 1);
                        int ti12b = br.ReadUInt16(); cntt12 = Math.Max(cntt12, ti12b + 1);
                    }
                }
            }
            int cntt8 = 0;
            if (true) {
                si.Position = baseoff + tbloff + p.ve0 - baseoff; // t3
                for (int i3 = 0; i3 < p.ve4; i3++) {
                    br.ReadUInt16();
                    br.ReadUInt16();
                    br.ReadUInt16();
                    int ti8 = br.ReadInt16(); cntt8 = Math.Max(cntt8, ti8 + 1);
                    br.ReadUInt16();
                    br.ReadUInt16();
                }
            }

            Color clr = Color.FromArgb(20, Color.Yellow);
            Color clrb = Color.FromArgb(50, Color.Green);

            hexVwer1.RangeMarkedList.Clear();

            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vb4, p.vb8 * 8, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vc0, p.vc4 * 6, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vc8, p.vcc * 6, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.ve0, p.ve4 * 12, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vac, p.va2 * 4, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.va8, (p.va2 - p.va0) * 64, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vf8, p.vfc * 12, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vf0, p.vf4 * 8, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vec, cntt8 * 48, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vd0, cntt9 * 8, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vd8, cntt10 * 4, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vd4, p.vb0 * 4, clr, clrb);
            hexVwer1.AddRangeMarked(baseoff + tbloff + p.vdc, cntt12 * 4, clr, clrb);
        }

        private void loadMset(string fp) {
            bin = File.ReadAllBytes(fp);
            hexVwer1.SetBin(bin);

            hexVwer1.OffDelta = STATIC_DELTA;

            if (fp.Equals(@"V:\KH2.yaz0r\dump_kh2\obj\P_EX110.mset")) {
                int baseoff = 98768;
                MemoryStream si = new MemoryStream(bin, baseoff, 0x5180, false);
                blitMset(si, baseoff);
            }
            else if (fp.Equals(@"V:\KH2.yaz0r\dump_kh2\obj\M_EX770_MEMO.mset")) {
                int baseoff = 0x8A0;
                MemoryStream si = new MemoryStream(bin, baseoff, 0x6E0, false);
                blitMset(si, baseoff);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (!checkBoxWarp1.Checked) return;

            ReadAcc o = (ReadAcc)listBox1.SelectedItem;
            if (o == null) return;

            int off = o.addr;
            hexVwer1.SetPos(ModUtil.calcMod(off, hexVwer1.ByteWidth * hexVwer1.GetLineCnt()));
            hexVwer1.SetSel(off);

            if (checkBoxSelective1.Checked) {
                listBox2.Items.Clear();
                bool assoc = checkBoxAssoc1.Checked;
                if (assoc) hexVwer1.Mark2.Clear();
                foreach (ReadAcc oo in listBox1.Items) {
                    if (o.accessor == oo.accessor) {
                        listBox2.Items.Add(oo);
                        if (assoc) hexVwer1.Mark2[oo.addr] = Color.FromArgb(40, Color.Yellow);
                    }
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) {
            ReadAcc o = (ReadAcc)listBox2.SelectedItem;
            if (o == null) return;

            int off = o.addr;
            hexVwer1.SetPos(ModUtil.calcMod(off, hexVwer1.ByteWidth * hexVwer1.GetLineCnt()));
            hexVwer1.SetSel(off);
        }

        class ModUtil {
            public static int calcMod(int offset, int unit) {
                return offset - (offset % unit);
            }
        }

        private void hexVwer1_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case '[':
                    hexVwer1.ByteWidth = Math.Max(1, hexVwer1.ByteWidth - 1);
                    e.Handled = true;
                    break;
                case ']':
                    hexVwer1.ByteWidth = Math.Max(1, hexVwer1.ByteWidth + 1);
                    e.Handled = true;
                    break;
            }
        }

        private void buttonCollect1_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();
            foreach (ReadAcc o in listBox1.SelectedItems) {
                s.Append(string.Format("@{0:00} {1:X4}[{2:X2}] \r\n", o.accessor, o.fake, bin[o.addr]));
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied to clipboard.");
        }

        private void buttonSpecial1_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();
            SortedList<int, int> callerset = new SortedList<int, int>();
            foreach (ReadAcc o in listBox1.Items) {
                if (2984 <= o.i && o.i <= 13909) {
                    callerset[o.accessor] = 0;
                    switch (o.accessor) {
                        case 101: // t4
                            s.Append("\r\n");
                            s.AppendFormat("@{0:00} t4[{1:X2}]={3:X4}  ", o.accessor, (o.fake - 0x4CB0) / 4, (o.fake - 0x4CB0) % 4, bin[o.addr] | (bin[o.addr + 1] << 8));
                            continue;
                        case 104: // t5
                            s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}={3:X4}  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, bin[o.addr] | (bin[o.addr + 1] << 8));
                            continue;

                        case 106:
                        case 155:
                        case 156:
                        case 157:
                        case 158:
                        case 159:
                        case 163:
                        case 165:
                        case 174:
                        case 202:
                        case 257:
                        case 258:
                        case 259:
                        case 265:
                        case 269:
                        case 270:
                        case 315:
                            s.AppendFormat("@{0:00} t3[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x34A4) / 12, (o.fake - 0x34A4) % 12, bin[o.addr]);
                            continue;
                        case 210:
                        case 211:
                        case 212:
                        case 213:
                        case 214:
                            s.AppendFormat("@{0:00} t3[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x3850) / 48, (o.fake - 0x3850) % 48, bin[o.addr]);
                            break;
                        case 105:
                        case 228:
                            s.AppendFormat("@{0:00} t7[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x3AF0) / 8, (o.fake - 0x3AF0) % 8, bin[o.addr]);
                            continue;
                        case 232:
                        case 233:
                        case 234:
                        case 244:
                        case 248:
                        case 250:
                        case 335:
                        case 336:
                        case 337:
                        case 338:
                        case 339:
                        case 340:
                        case 341:
                            s.AppendFormat("@{0:00} t6[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x3B80) / 12, (o.fake - 0x3B80) % 12, bin[o.addr]);
                            continue;

                        case 122: // t5.20
                            s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}=({3,6:0.0})  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, ReadSer.ReadFlt(bin, o.addr));
                            continue;
                        case 123: // t5.24
                            s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}=({3,6:0.0})  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, ReadSer.ReadFlt(bin, o.addr));
                            continue;
                        case 124: // t5.28
                            s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}=({3,6:0.0})  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, ReadSer.ReadFlt(bin, o.addr));
                            continue;

                        //s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, bin[o.addr]);
                    }
                    if (o.fake < 0x140)
                        continue;
                    s.Append(string.Format("@{0:00} {1:X4}={2:X2} ", o.accessor, o.fake, bin[o.addr]));
                }
            }
            s.Append("\r\n\r\n");
            foreach (int accessor in callerset.Keys) {
                s.AppendFormat("@{0:00} {1:X8}\r\n", accessor, acc2addrmap[accessor]);
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied to clipboard.");
        }

        class ReadSer {
            public static float ReadFlt(byte[] bin, int off) {
                MemoryStream si = new MemoryStream(bin, off, 4, false);
                BinaryReader br = new BinaryReader(si);
                return br.ReadSingle();
            }
        }

        private void buttonSpec2_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();
            SortedList<int, int> callerset = new SortedList<int, int>();
            foreach (ReadAcc o in listBox1.Items) {
                if (656 <= o.i && o.i <= 2890) {
                    if (o.fake < 0x140)
                        continue;
                    if (o.accessor == 48)
                        s.Append("\r\n");
                    callerset[o.accessor] = 0;
                    switch (o.accessor) {
                        case 48: // t2.4
                            s.AppendFormat("@{0:00} t2[{1:X2}].{2:X2}={3:X4}  ", o.accessor, (o.fake - 0x0758) / 6, (o.fake - 0x0758) % 6, bin[o.addr] | (bin[o.addr + 1] << 8));
                            continue;

                        case 50: // t2
                        case 55: // t2
                        case 58: // t2
                        case 59: // t2
                        case 63: // t2
                        case 64: // t2
                            s.AppendFormat("@{0:00} t2[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x0758) / 6, (o.fake - 0x0758) % 6, bin[o.addr]);
                            continue;

                        case 65: // t9.0
                        case 56: // t9.2
                        case 69: // t9.6
                        case 70: // t9.4
                            s.AppendFormat("@{0:00} t9[{1:X4}].{2:X2}={3:X4}  ", o.accessor, (o.fake - 0x0AA6) / 8, (o.fake - 0x0AA6) % 8, bin[o.addr] | (bin[o.addr + 1] << 8));
                            continue;

                        case 52: // t9
                        case 60: // t9
                        case 67: // t9?
                        case 68: // t9?
                        case 71: // t9?
                        case 72: // t9?
                            s.AppendFormat("@{0:00} t9[{1:X4}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x0AA6) / 8, (o.fake - 0x0AA6) % 8, bin[o.addr]);
                            continue;
                        case 43: // t11
                        case 66: // t11
                        case 61: // t11
                        case 74: // t11?
                        case 75: // t11?
                            s.AppendFormat("@{0:00} t11[{1:X4}].{2:X2}=({3,6:0.00})  ", o.accessor, (o.fake - 0x1C78) / 4, (o.fake - 0x1C78) % 4, ReadSer.ReadFlt(bin, o.addr));
                            continue;
                        case 57: // t10
                        case 73: // t10?
                        case 76: // t10?
                            s.AppendFormat("@{0:00} t10[{1:X4}].{2:X2}=({3,6:0.00})  ", o.accessor, (o.fake - 0x1DA0) / 4, (o.fake - 0x1DA0) % 4, ReadSer.ReadFlt(bin, o.addr));
                            continue;
                        case 77: // t12
                        case 78: // t12
                            s.AppendFormat("@{0:00} t12[{1:X4}].{2:X2}=({3,6:0.00})  ", o.accessor, (o.fake - 0x2498) / 4, (o.fake - 0x2498) % 4, ReadSer.ReadFlt(bin, o.addr));
                            continue;
                    }
                    s.Append(string.Format("@{0:00} {1:X4}={2:X2} ", o.accessor, o.fake, bin[o.addr]));
                }
            }
            s.Append("\r\n\r\n");
            foreach (int accessor in callerset.Keys) {
                s.AppendFormat("@{0:00} {1:X8}\r\n", accessor, acc2addrmap[accessor]);
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied to clipboard.");
        }

        private void buttonExp_Click(object sender, EventArgs e) {
            MemoryStream si = new MemoryStream(bin, fbaseoff, fsize, false);
            BinaryReader br = new BinaryReader(si);

            PosTbl p = new PosTbl(si);
            int tbloff = p.tbloff;

            // @48 t2[16].04=008D  
            // @50 t2[16].03=05  
            // @52 t9[008D].00=02  
            // @55 t2[16].02=03  
            // @56 t9[008D].02=006F       index_t10
            // @57 t10[6F].00=( -0.54)    val
            // @58 t2[16].02=03           ax
            // @59 t2[16].00=A3           bone_pos

            StringBuilder s = new StringBuilder();
            for (int w = 0; w < 2; w++) {
                int woff = (w == 0) ? (tbloff + p.vc0) : (tbloff + p.vc8);
                int wcnt = (w == 0) ? (p.vc4) : (p.vcc);
                for (int t2i = 0; t2i < wcnt; t2i++) {
                    si.Position = woff + 6 * t2i;
                    int rv0 = br.ReadUInt16();
                    int rv2 = br.ReadByte();
                    int rv3 = br.ReadByte();
                    int rv4 = br.ReadUInt16();

                    si.Position = tbloff + p.vd0 + 8 * rv4;
                    int lv0 = br.ReadUInt16();
                    int lv2 = br.ReadUInt16();

                    si.Position = tbloff + p.vd8 + 4 * lv2;
                    float val = br.ReadSingle();
                    s.AppendFormat("{0},{1},{2}\r\n", rv0, rv2, val);
                }
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("exp ok");
        }

        private void buttonTexport_Click(object sender, EventArgs e) {
            MemoryStream si = new MemoryStream(bin, fbaseoff, fsize, false);
            BinaryReader br = new BinaryReader(si);

            PosTbl p = new PosTbl(si);
            int tbloff = p.tbloff;

            StringBuilder s = new StringBuilder(1024 * 256);

            int cntt9 = 0;
            for (int t = 0; t < p.vc4; t++) { // t2 // t2a = modify for ?
                si.Position = tbloff + p.vc0 + 6 * t; // 0x758
                int c00 = br.ReadByte();
                int c01 = br.ReadByte();
                int c02 = br.ReadByte();
                int c03 = br.ReadByte();
                int c04 = br.ReadUInt16(); // t9_xxxx

                s.AppendFormat("t2a_{0:X4} = {1:X2} {2:X2} {3:X2} {4:X2} t9_{5:X4}\r\n"
                    , t, c00, c01, c02, c03, c04);
                cntt9 = Math.Max(cntt9, c04 + c03);
            }
            for (int t = 0; t < p.vcc; t++) { // t2` // t2b = modify for additional bone structure. boneIndex*0x40 ax*0x4 +0x20.
                si.Position = tbloff + p.vc8 + 6 * t; // 0x758
                int c00 = br.ReadByte();
                int c01 = br.ReadByte();
                int c02 = br.ReadByte();
                int c03 = br.ReadByte();
                int c04 = br.ReadUInt16(); // t9_xxxx
                s.AppendFormat("t2b_{0:X4} = {1:X2} {2:X2} {3:X2} {4:X2} t9_{5:X4}\r\n"
                    , t, c00, c01, c02, c03, c04);
                cntt9 = Math.Max(cntt9, c04 + c03);
            }
            s.Append("\r\n");

            int cntt10 = 0;
            int cntt12 = 0;
            int cntt11 = 0;
            for (int t = 0; t < cntt9; t++) { // t9
                si.Position = tbloff + p.vd0 + 8 * t;
                int c00 = br.ReadUInt16();
                int c02 = br.ReadUInt16(); // t10_xxxx
                int c04 = br.ReadUInt16(); // t12_xxxx
                int c06 = br.ReadUInt16(); // t12_xxxx
                s.AppendFormat("t9_{0:X4} = t11_{5:X4} ({1:X4}) t10_{2:X4} t12_{3:X4} t12_{4:X4} | v11_{5:X4} v10_{2:X4} v12_{3:X4} v12_{4:X4}\r\n"
                    , t, c00, c02, c04, c06, c00 / 4);
                cntt10 = Math.Max(cntt10, c02 + 1);
                cntt12 = Math.Max(cntt12, c04 + 1);
                cntt12 = Math.Max(cntt12, c06 + 1);
                cntt11 = Math.Max(cntt11, (c00 / 4) + 1);
            }
            s.Append("\r\n");

            for (int t = 0; t < cntt10; t++) {
                si.Position = tbloff + p.vd8 + 4 * t;
                float c00 = br.ReadSingle();
                s.Replace(string.Format("v10_{0:X4}", t), string.Format("{0,10:0.0000}", c00));
                s.AppendFormat("t10_{0:X4} = {1}\r\n"
                    , t, c00);
            }
            s.Append("\r\n");

            for (int t = 0; t < cntt12; t++) {
                si.Position = tbloff + p.vdc + 4 * t;
                float c00 = br.ReadSingle();
                s.Replace(string.Format("v12_{0:X4}", t), string.Format("{0,10:0.0000}", c00));
                s.AppendFormat("t12_{0:X4} = {1}\r\n"
                    , t, c00);
            }
            s.Append("\r\n");

            for (int t = 0; t < cntt11; t++) {
                si.Position = tbloff + p.vd4 + 4 * t;
                float c00 = br.ReadSingle();
                s.Replace(string.Format("v11_{0:X4}", t), string.Format("{0,10:0.0000}", c00));
                s.AppendFormat("t11_{0:X4} = {1}\r\n"
                    , t, c00);
            }
            s.Append("\r\n");

            s.Append("\r\n");

            for (int t = 0; t < p.va2; t++) { // t4
                si.Position = tbloff + p.vac + 4 * t;
                uint c00 = br.ReadUInt32();
                s.AppendFormat("t4_{0:X4} = {1:X8}\r\n"
                    , t, c00);
            }
            s.Append("\r\n");

            for (int t = 0; t < p.vb8; t++) { // t1
                si.Position = tbloff + p.vb4 + 8 * t;
                int c00 = br.ReadUInt16();
                int c02 = br.ReadUInt16();
                float c04 = br.ReadSingle();
                s.AppendFormat("t1_{0:X4} = {1:X4} {2:X4} {3,10:0.000}\r\n"
                    , t, c00, c02, c04);
            }
            s.Append("\r\n");

            for (int t = 0; t < p.ve4; t++) { // t3
                si.Position = tbloff + p.ve0 + 12 * t;
                int c00 = br.ReadByte();
                int c01 = br.ReadByte();
                int c02 = br.ReadUInt16();
                int c04 = br.ReadUInt16();
                int c06 = br.ReadUInt16();
                uint c08 = br.ReadUInt32();
                s.AppendFormat("t3_{0:X4} = {1:X2} {2:X2} key_{3:X4} key_{4:X4} {5:X4} {6:X8}\r\n"
                    , t, c00, c01, c02, c04, c06, c08);
            }
            s.Append("\r\n");

            for (int t = 0; t < p.va2 - p.va0; t++) { // t5
                si.Position = tbloff + p.va8 + 64 * t;
                int c00 = br.ReadUInt16();
                int c02 = br.ReadUInt16();
                int c04 = br.ReadUInt16();
                int c06 = br.ReadUInt16();
                UInt64 c08 = br.ReadUInt64();
                float c10 = br.ReadSingle();
                float c14 = br.ReadSingle();
                float c18 = br.ReadSingle();
                float c1c = br.ReadSingle();
                float c20 = br.ReadSingle();
                float c24 = br.ReadSingle();
                float c28 = br.ReadSingle();
                float c2c = br.ReadSingle();
                float c30 = br.ReadSingle();
                float c34 = br.ReadSingle();
                float c38 = br.ReadSingle();
                float c3c = br.ReadSingle();
                s.AppendFormat("t5_{0:X4} key_{1:X4} {2:X4} {3:X4} {4:X4} {5:X16} {6,10:0.000} {7,10:0.000} {8,10:0.000} {9,10:0.000}|{10,10:0.000} {11,10:0.000} {12,10:0.000} {13,10:0.000}|{14,10:0.000} {15,10:0.000} {16,10:0.000} {17,10:0.000}\r\n"
                    , t
                    , c00, c02, c04, c06, c08
                    , c10, c14, c18, c1c
                    , c20, c24, c28, c2c
                    , c30, c34, c38, c3c
                    );
            }
            s.Append("\r\n");


            Clipboard.SetText(s.ToString());
            MessageBox.Show("Texport ok");
        }

        class Jtbl {
            public string id;
            public int off, cx;

            public Jtbl(string id, int off, int cx) {
                this.id = id;
                this.off = off;
                this.cx = cx;
            }

            public override string ToString() {
                return string.Format("{0,3} {1:X6} {2,2}", id, off, cx);
            }
        }

        private void listBoxJtbl_DoubleClick(object sender, EventArgs e) {
            Jtbl o = (Jtbl)listBoxJtbl.SelectedItem;
            if (o == null) return;

            hexVwer1.SetPos(fbaseoff + 0x90 + o.off);
            hexVwer1.ByteWidth = o.cx;
        }

        private void buttonExportMsetoes_Click(object sender, EventArgs e) {
            MemoryStream si = new MemoryStream(bin, fbaseoff, fsize, false);
            BinaryReader br = new BinaryReader(si);
            PosTbl p = new PosTbl(si);
            int tbloff = p.tbloff;

            StringBuilder s = new StringBuilder();

            s.Append("msetoes ");

            int offt1 = p.vb4;
            int cntt1 = p.vb8;
            s.Append("t1 " + cntt1 + " { ");
            for (int t = 0; t < cntt1; t++) {
                si.Position = tbloff + offt1 + 8 * t; // t1
                int c00 = br.ReadUInt16();
                int c02 = br.ReadUInt16();
                float c04 = br.ReadSingle();
                s.Append("{ ");
                s.AppendFormat("{1:X4} {2:X4} {3} "
                    , t, c00, c02, c04);
                s.Append("} ");
            }
            s.Append("} \n");

            for (int w = 0; w < 2; w++) {
                int cntt2 = (w == 0) ? p.vc4 : p.vcc;
                int offt2 = (w == 0) ? p.vc0 : p.vc8;
                s.Append("t2 " + cntt2 + " { ");
                for (int t = 0; t < cntt2; t++) { // t2
                    si.Position = tbloff + offt2 + 6 * t; // 0x758
                    int c00 = br.ReadByte();
                    int c01 = br.ReadByte();
                    int c02 = br.ReadByte();
                    int c03 = br.ReadByte();
                    int c04 = br.ReadUInt16(); // t9_xxxx
                    s.Append("{ ");
                    s.AppendFormat("{1:X2} {2:X2} {3:X2} {4:X2} "
                        , t, c00, c01, c02, c03, c04);

                    s.Append("t9 " + c03 + " { ");
                    s.Append("\n");
                    for (int t9 = 0; t9 < c03; t9++) { // t9
                        si.Position = tbloff + p.vd0 + 8 * (c04 + t9);

                        int t9c00 = br.ReadUInt16(); // t11_xxxx
                        int t9c02 = br.ReadUInt16(); // t10_xxxx
                        int t9c04 = br.ReadUInt16(); // t12_xxxx
                        int t9c06 = br.ReadUInt16(); // t12_xxxx

                        s.Append("{ ");
                        s.AppendFormat("{0:X4} ", (t9c00));
                        if (true) { // t11
                            si.Position = tbloff + p.vd4 + 4 * (t9c00 / 4);
                            float t11c00 = br.ReadSingle();
                            s.AppendFormat("{0} ", t11c00);
                        }
                        if (true) { // t10
                            si.Position = tbloff + p.vd8 + 4 * t9c02;
                            float t10c00 = br.ReadSingle();
                            s.AppendFormat("{0} ", t10c00);
                        }
                        if (true) { // t12
                            si.Position = tbloff + p.vdc + 4 * t9c04;
                            float t12c00 = br.ReadSingle();
                            s.AppendFormat("{0} ", t12c00);
                        }
                        if (true) { // t12
                            si.Position = tbloff + p.vdc + 4 * t9c06;
                            float t12c00 = br.ReadSingle();
                            s.AppendFormat("{0} ", t12c00);
                        }

                        s.Append("} ");
                    }
                    s.Append("} ");
                    s.Append("} ");
                }
                s.Append("} ");
            }

            int offt4 = p.vac;
            int cntt4 = p.va2;
            s.Append("t4 " + cntt4 + " { ");
            s.Append("\n");
            for (int t = 0; t < cntt4; t++) {
                si.Position = tbloff + offt4 + 4 * t; // t4
                uint c00 = br.ReadUInt16();
                uint c02 = br.ReadUInt16();
                s.Append("{ ");
                s.AppendFormat("{1:X4} {2:X4} ", t, c00, c02);
                s.Append("} ");
            }
            s.Append("} ");

            int offt5 = p.va8;
            int cntt5 = p.va2 - p.va0;
            s.Append("t5 " + cntt5 + " { ");
            for (int t = 0; t < cntt5; t++) {
                si.Position = tbloff + offt5 + 64 * t; // t5
                int c00 = br.ReadUInt16();
                int c02 = br.ReadUInt16();
                int c04 = br.ReadUInt16();
                int c06 = br.ReadUInt16();
                UInt64 c08 = br.ReadUInt64();
                float c10 = br.ReadSingle();
                float c14 = br.ReadSingle();
                float c18 = br.ReadSingle();
                float c1c = br.ReadSingle();
                float c20 = br.ReadSingle();
                float c24 = br.ReadSingle();
                float c28 = br.ReadSingle();
                float c2c = br.ReadSingle();
                float c30 = br.ReadSingle();
                float c34 = br.ReadSingle();
                float c38 = br.ReadSingle();
                float c3c = br.ReadSingle();
                s.Append("\n");
                s.Append("{ ");
                s.AppendFormat("{1:X4} {2:X4} {3:X4} {4:X4} {5:X16}  {6} {7} {8} {9}  {10} {11} {12} {13}  {14} {15} {16} {17} "
                    , t
                    , c00, c02, c04, c06, c08
                    , c10, c14, c18, c1c
                    , c20, c24, c28, c2c
                    , c30, c34, c38, c3c
                    );
                s.Append("} ");
            }
            s.Append("} \n");

            int offt3 = p.ve0;
            int cntt3 = p.ve4;
            s.Append("t3 " + cntt3 + " { ");
            for (int t = 0; t < cntt3; t++) {
                si.Position = tbloff + offt3 + 12 * t; // t3
                int c00 = br.ReadByte();
                int c01 = br.ReadByte();
                int c02 = br.ReadUInt16();
                int c04 = br.ReadUInt16();
                int c06 = br.ReadUInt16();
                uint c08 = br.ReadUInt32();
                s.Append("\n");
                s.Append("{ ");
                s.AppendFormat("{1:X2} {2:X2} {3:X4} {4:X4} {5:X4} {6:X8} "
                    , t, c00, c01, c02, c04, c06, c08
                    );
                s.Append("} ");
            }
            s.Append("} ");

            s.Replace("\n", "\r\n");
            Clipboard.SetText(s.ToString());
            MessageBox.Show("msetoes copied to clipboard!");
        }

        private void buttonSpec3_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();
            SortedList<int, int> callerset = new SortedList<int, int>();
            foreach (ReadAcc o in listBox1.Items) {
                if (62 <= o.i && o.i <= 646) {
                    if (o.accessor == 35)
                        s.Append("\r\n");
                    callerset[o.accessor] = 0;
                    switch (o.accessor) {
                        case 35:
                        case 36:
                        case 38:
                            s.AppendFormat("@{0:00} t1[{1:X4}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x140) / 8, (o.fake - 0x140) % 8, bin[o.addr]);
                            continue;
                        case 37:
                            s.AppendFormat("@{0:00} t1[{1:X4}].{2:X2}=({3,6:0.00})  ", o.accessor, (o.fake - 0x140) / 8, (o.fake - 0x140) % 8, ReadSer.ReadFlt(bin, o.addr));
                            continue;

                        //s.AppendFormat("@{0:00} t4[{1:X2}]={3:X4}  ", o.accessor, (o.fake - 0x4CB0) / 4, (o.fake - 0x4CB0) % 4, bin[o.addr] | (bin[o.addr + 1] << 8));
                        //s.AppendFormat("@{0:00} t5[{1:X2}].{2:X2}={3:X2}  ", o.accessor, (o.fake - 0x3FB0) / 64, (o.fake - 0x3FB0) % 64, bin[o.addr]);
                    }
                    if (o.fake < 0x140)
                        continue;
                    s.Append(string.Format("@{0:00} {1:X4}={2:X2} ", o.accessor, o.fake, bin[o.addr]));
                }
            }
            s.Append("\r\n\r\n");
            foreach (int accessor in callerset.Keys) {
                s.AppendFormat("@{0:00} {1:X8}\r\n", accessor, acc2addrmap[accessor]);
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied to clipboard.");
        }

        private void Form1s1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1s1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs == null) return;
            foreach (string f in fs) {
                if (string.Compare(Path.GetExtension(f), ".mset", true) == 0 || string.Compare(Path.GetExtension(f), ".anb", true) == 0) {
                    sysloadMset(f);
                    break;
                }
            }
        }

        void sysloadMset(string f) {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBoxJtbl.Items.Clear();
            loadMset(f);
            hexVwer1.OffDelta = 0;
            refreshMsetTree();
        }

        class RSel {
            public int off, len;

            public RSel(int off, int len) {
                this.off = off;
                this.len = len;
            }
        }

        void refreshMsetTree() {
            treeView1.Nodes.Clear();
            ReadBar.Barent[] ents = ReadBar.Explode(new MemoryStream(bin, false));
            foreach (ReadBar.Barent ent in ents) {
                if (ent.len != 0 && 0x11 == (ent.k & 0xFFFF)) {
                    ReadBar.Barent[] ents2 = ReadBar.Explode(new MemoryStream(ent.bin, false));
                    foreach (ReadBar.Barent ent2 in ents2) {
                        if (0x09 == (ent2.k & 0xFFFF)) {
                            TreeNode tn2 = treeView1.Nodes.Add(ent.id + "#" + ent2.id);
                            tn2.Tag = new RSel(ent.off + ent2.off, ent2.len);
                        }
                    }
                }
            }
        }

        private void Form1s1_DragOver(object sender, DragEventArgs e) {

        }

        private void treeView1_DoubleClick(object sender, EventArgs e) {
            TreeNode tn = treeView1.SelectedNode;
            if (tn == null) return;
            RSel o = (RSel)tn.Tag;
            if (o == null) return;

            MemoryStream si = new MemoryStream(bin, o.off, o.len, false);
            blitMset(si, o.off);
            hexVwer1.OffDelta = -o.off;
        }

        private void buttonI2Call_Click(object sender, EventArgs e) {
            string fpxmlDictLoadFrom = openFileDialogTestreadDictUser.FileName;
            if (!File.Exists(fpxmlDictLoadFrom)) return;

            SortedDictionary<int, int> dictuser = new SortedDictionary<int, int>();
            if (fpxmlDictLoadFrom != null) {
                IFormatter ser = new BinaryFormatter();
                using (FileStream fs = File.OpenRead(fpxmlDictLoadFrom)) {
                    dictuser = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    acc2addrmap = (SortedDictionary<int, int>)ser.Deserialize(fs);
                    fs.Close();
                }
            }

            SortedDictionary<int, int> i2call = new SortedDictionary<int, int>();
            foreach (KeyValuePair<int, int> kv in dictuser) i2call[kv.Value] = kv.Key;

            StringBuilder s = new StringBuilder();
            foreach (KeyValuePair<int, int> kv in i2call) {
                s.AppendFormat("@{0,-5} {1:X8}\r\n", kv.Key, kv.Value);
            }
            Clipboard.SetText(s.ToString());
            MessageBox.Show("Copied text.");
        }

        private void buttonMsetls_Click(object sender, EventArgs e) {
            contextMenuStrip1.Items.Clear();
            try {
                foreach (string fp in File.ReadAllLines(Path.Combine(Settings.Default.DirMemo, "mset2list.txt"))) {
                    if (fp.Length == 0) continue;
                    ToolStripItem tsi = contextMenuStrip1.Items.Add("Load " + Path.GetFileName(fp));
                    tsi.Tag = fp;
                    tsi.Click += new EventHandler(tsi_Click);
                }
            }
            catch (IOException) {
                ToolStripItem tsi = contextMenuStrip1.Items.Add("(No items in mset2list.txt)");
                tsi.Enabled = false;
            }
            contextMenuStrip1.Show(buttonMsetls, 0, 0);
        }

        void tsi_Click(object sender, EventArgs e) {
            ToolStripItem tsi = (ToolStripItem)sender;
            sysloadMset((string)tsi.Tag);
        }

        private void buttonExportEasy_Click(object sender, EventArgs e) {
            buttonExportMsetoes_Click(null, null);
        }
    }
}