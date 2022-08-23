using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using khkh_xldMii;

namespace timTexaVis {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        class TH {
            public int off, i;

            public TH(int i, int off) {
                this.i = i;
                this.off = off;
            }

            public override string ToString() {
                return "#" + i;
            }
        }

        List<TH> alt = new List<TH>();

        MemoryStream sitim = new MemoryStream();

        private void Form1_Load(object sender, EventArgs e) {
            loadMdlx(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX100.mdlx");
            loadMset(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX100.mset");
            //loadMset(@"H:\Dev\ffj\expack1anbz\mix\p_ex100-1.mset");
        }

        private void loadMset(string fmset) {
            listBox1.Items.Clear();
            using (FileStream fs = File.OpenRead(fmset)) {
                foreach (ReadBar.Barent ent1 in ReadBar.Explode(fs)) {
                    if (ent1.len != 0) {
                        foreach (ReadBar.Barent ent2 in ReadBar.Explode(new MemoryStream(ent1.bin, false))) {
                            if (ent2.k == 0x10) {
                                Mot1 o = new Mot1();
                                o.e1 = ent1;
                                o.e2 = ent2;
                                listBox1.Items.Add(o);
                            }
                        }
                    }
                }
            }
        }

        class Mot1 {
            public ReadBar.Barent e1, e2;

            public override string ToString() {
                return e1.id;
            }
        }

        void loadMdlx(string fmdlx) {
            listView1.Items.Clear();
            alt.Clear();
            sitim.SetLength(0);

            using (FileStream fs = File.OpenRead(fmdlx)) {
                foreach (ReadBar.Barent ent in ReadBar.Explode(fs)) {
                    if (ent.k == 7) {
                        byte[] bin = ent.bin;
                        sitim.Write(bin, 0, bin.Length);

                        int posTexa = -1, i = 0;
                        for (int x = 0; x < bin.Length; x += 16) {
                            if (bin[x + 0] == 0x5F && bin[x + 1] == 0x44 && bin[x + 2] == 0x4D && bin[x + 3] == 0x59) { // _DMY
                                if (posTexa != -1) {
                                    alt.Add(new TH(i++, posTexa));
                                    posTexa = -1;
                                }
                            }
                            if (bin[x + 8] == 0x54 && bin[x + 9] == 0x45 && bin[x + 10] == 0x58 && bin[x + 11] == 0x41) { // TEXA
                                posTexa = x + 16;
                            }
                        }
                    }
                }

                //alt.Add(new Task(0, 0x1AA90, 0x30090 - 0x1AA90, 0x1A690, 96, 48, 19));
                //alt.Add(new Task(1, 0x30610, 0x39C10 - 0x30610, 0x300A0, 48, 32, 25));

                foreach (TH t in alt) {
                    ListViewItem lvi = listView1.Items.Add(t.ToString());
                    lvi.Tag = t;
                }
            }
        }

        int patcx = 0, patcy = 0;

        class E1 {
            public int i0 = 0, i1 = 0, i2 = 0;
            public short v0, v2, v4, v6;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listView1.SelectedItems) {
                TH t = (TH)lvi.Tag;
                MemoryStream si = sitim;
                BinaryReader br = new BinaryReader(si);

                si.Position = t.off + 0x0C;
                int cntt2 = br.ReadUInt16(); // @0x0C cntt2
                int ycnt = br.ReadUInt16(); // @0x0E ycnt
                int patx = br.ReadUInt16(); // @0x10 patx
                int paty = br.ReadUInt16(); // @0x12 paty
                patcx = br.ReadUInt16(); // @0x14 patcx
                patcy = br.ReadUInt16(); // @0x16 patcy
                int offt1 = br.ReadInt32(); // @0x18 offt1
                int offt2 = br.ReadInt32(); // @0x1C offt2
                int picoff = br.ReadInt32(); // @0x20 picoff

                label1.Text = ""
                    + "cntt2=" + cntt2 + "\n"
                    + "ycnt=" + ycnt + "\n"
                    + "patx=" + patx + "\n"
                    + "paty=" + paty + "\n"
                    + "patcx=" + patcx + "\n"
                    + "patcy=" + patcy + "\n"
                    + "offt1=" + offt1 + "\n"
                    + "offt2=" + offt2 + "\n"
                    + "picoff=" + picoff + "\n"
                ;

                listView2.Items.Clear();
                StringBuilder s = new StringBuilder();
                int cntt1 = (offt2 - offt1) / 2;
                for (int y = 0; y < cntt1; y++) {
                    si.Position = t.off + offt1 + 2 * y;
                    int x = br.ReadInt16() - 1;
                    if (x < 0) {
                        s.AppendFormat("({0,3}) \n", y);
                    }
                    else {
                        si.Position = t.off + offt2 + 4 * x;
                        int offx1 = br.ReadInt32();
                        si.Position = t.off + offx1;
                        s.AppendFormat("({0,3},{1,2}) 0x{2:X4} \n", y, x, offx1);
                        if (x >= 0) {
                            int z = 0;
                            while (true) {
                                E1 e1 = new E1();
                                e1.i0 = y;
                                e1.i1 = x;
                                e1.i2 = z;
                                e1.v0 = br.ReadInt16();
                                e1.v2 = br.ReadInt16();
                                e1.v4 = br.ReadInt16();
                                e1.v6 = br.ReadInt16();
                                s.AppendFormat("#    {0,3} {1,3} {2,3} {3,3} \n", e1.v0, e1.v2, e1.v4, e1.v6);

                                ListViewItem lvi2 = listView2.Items.Add(string.Format("{0,3},{1,2},{2,2}|{3,4},{4,3},{5,3},{6,3}"
                                    , e1.i0, e1.i1, e1.i2
                                    , e1.v0, e1.v2, e1.v4, e1.v6
                                    ));
                                lvi2.Tag = e1;
                                lvi2.BackColor = ((e1.i1 & 1) == 0) ? Color.Yellow : Color.White;

                                z++;
                                if (e1.v0 < 0)
                                    break;
                            }
                        }
                    }
                }
                s.Replace("\n", "\r\n");
                textBox1.Text = s.ToString();

                Bitmap pic = new Bitmap(patcx, patcy * ycnt, PixelFormat.Format8bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try {
                    si.Position = t.off + picoff;
                    byte[] bin = br.ReadBytes(patcx * patcy * ycnt);
                    Marshal.Copy(bin, 0, bd.Scan0, bin.Length);
                }
                finally {
                    pic.UnlockBits(bd);
                }
                ColorPalette pal = pic.Palette;
                for (int x = 0; x < 256; x++)
                    pal.Entries[x] = Color.FromArgb(x, x, x);
                pic.Palette = pal;

                pb1.Image = pic;
                break;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)) {
                foreach (string fp in (string[])e.Data.GetData(DataFormats.FileDrop)) {
                    if (Path.GetExtension(fp).ToLower().Equals(".mdlx")) {
                        loadMdlx(fp);
                        break;
                    }
                }
            }
        }

        private void Form1_DragLeave(object sender, EventArgs e) {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listView2.SelectedItems) {
                E1 o = (E1)lvi.Tag;
                Bitmap p = new Bitmap(patcx, patcy);
                using (Graphics cv = Graphics.FromImage(p)) {
                    cv.DrawImageUnscaled(pb1.Image, 0, -patcy * o.v6);
                }
                pb2.Image = p;
                break;
            }
        }

        public class FacMod {
            public List<Fac1> alf1 = new List<Fac1>();

            public FacMod(MemoryStream si) {
                BinaryReader br = new BinaryReader(si);
                byte cnt1 = br.ReadByte(); // @0x00
                byte cnt2 = br.ReadByte(); // @0x01
                br.ReadUInt16(); // @0x02
                for (int t1 = 0; t1 < cnt1; t1++) {
                    Fac1 f1 = new Fac1();
                    f1.v0 = br.ReadInt16();
                    f1.v2 = br.ReadInt16();
                    if (f1.v0 == 0 && f1.v2 == -1 && t1 != 0) {
                        f1.v4 = 0;
                        f1.v6 = 0;
                    }
                    else {
                        f1.v4 = br.ReadInt16();
                        if (f1.v2 != -1) {
                            f1.v6 = br.ReadInt16();
                        }
                        else {
                            f1.v6 = 0;
                        }
                    }
                    alf1.Add(f1);
                }
            }
        }
        public class Fac1 {
            public short v0, v2, v4, v6;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            Mot1 o = (Mot1)listBox1.SelectedItem;
            if (o == null)
                return;
            StringBuilder s = new StringBuilder();
            byte[] bin = o.e2.bin;
            int cx = bin.Length;
            if (bin.Length == 0) return;

            List<int> alsep = new List<int>();
            alsep.Add(4);
            int off = 4;
            for (int t = 0; t < bin[0]; t++) {
                if (bin[off + 2] == 255 && bin[off + 3] == 255) {
                    if (bin[off + 0] == 0 && bin[off + 1] == 0 && t != 0) {
                        off += 4;
                    }
                    else {
                        off += 6;
                    }
                }
                else {
                    off += 8;
                }
                alsep.Add(off);
            }

            for (int x = 0; x < cx; x++) {
                if (alsep.IndexOf(x) >= 0)
                    s.Append("\r\n");
                s.AppendFormat("{0:X2} ", bin[x]);
            }
            textBox2.Text = s.ToString();
        }

        private void button1_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();
            foreach (Mot1 o in listBox1.Items) {
                int off1 = o.e1.off + o.e2.off + (0x8E3CB0 - 0x18170);
                int off2 = off1 + o.e2.len;
                s.AppendFormat("lea edx, [ecx -0x{0:X8}]\r\n", off1, off2);
                s.AppendFormat("cmp edx, 0x{1:X8} - 0x{0:X8}\r\n", off1, off2);
                s.AppendFormat("jb Trap\r\n");
            }
            Clipboard.SetText(s.ToString());
        }

        private void pb1_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Middle)) {
                Clipboard.SetImage(pb1.Image);
                MessageBox.Show("The image copied to clip board");
            }
        }
    }
}