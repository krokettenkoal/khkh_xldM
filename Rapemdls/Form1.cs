using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Rapemdls.Mdlxo;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Rapemdls {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialogMdls.FileName))
                openFileDialogMdls.ShowDialog(this);
            if (File.Exists(openFileDialogMdls.FileName))
                loadMdls(openFileDialogMdls.FileName);
        }

        byte[] bin = null;
        Mdlx mdlx = null;

        private void loadMdls(string fp) {
            bin = File.ReadAllBytes(fp);
            hexVwer1.SetBin(bin);
            mdlx = new Mdlx();
            mdlx.read(bin);

            //hexVwer1.OffDelta = -mdlx.alent[0].off;

            hexVwer1.RangeMarkedList.Clear();
            hexVwer1.Mark2.Clear();
            int tbl80 = 0x80;
            E0 e0 = mdlx.e0;
            Color clrs;
            clrs = Color.RoyalBlue; hexVwer1.AddRangeMarked(tbl80 + e0.v08, e0.v0c, Color.FromArgb(50, clrs), Color.FromArgb(100, clrs));
            clrs = Color.RoyalBlue; hexVwer1.AddRangeMarked(tbl80 + e0.v10, e0.v14, Color.FromArgb(50, clrs), Color.FromArgb(100, clrs));
            clrs = Color.RoyalBlue; hexVwer1.AddRangeMarked(tbl80 + e0.v18, e0.v1c, Color.FromArgb(50, clrs), Color.FromArgb(100, clrs));

            if (true) {
                clrs = Color.OrangeRed;
                int baseoff = tbl80 + e0.v20;
                foreach (S5 s5 in e0.s4.als5) {
                    foreach (Hive hive in s5.alhive) {
                        hexVwer1.AddRangeMarked(baseoff + hive.off, hive.len, Color.FromArgb(50, clrs), Color.FromArgb(100, clrs));
                    }
                }
            }

            if (true) {
                listView1.Items.Clear();
                int x = 0;
                foreach (Vone vone in e0.s4.alvr) {
                    listView1.Items.Add("#" + x + " " + FmtVoneUtil.Format(vone));
                    x++;
                }
            }
            if (true) {
                listBox1.Items.Clear();
                int t = 0;
                foreach (S5 s5 in e0.s4.als5) {
                    int x = 0;
                    foreach (Hive hive in s5.alhive) {
                        listBox1.Items.Add(new S5Refi("#" + t + "." + x.ToString("000") + " " + (hive.t00 & 0xFFFF).ToString("X4") + " " + (hive.off.ToString("X4")) + " " + (hive.vi), s5, hive));
                        x++;
                    }
                    t++;
                }
            }
            linkLabel1.Text = Path.GetFileNameWithoutExtension(fp);
        }
        class S5Refi {
            public string text;
            public S5 s1;
            public Hive hive;

            public S5Refi(string text, S5 s1, Hive hive) {
                this.text = text;
                this.s1 = s1;
                this.hive = hive;
            }
            public override string ToString() {
                return text;
            }
        }
        class FmtVoneUtil {
            public static string Format(Vone o) {
                return string.Format(
                    "{0,10:0.000} {1,10:0.000} {2,10:0.000} {3,3}|{4,10:0.000} {5,10:0.000} {6,10:0.000} {7,10:0.000}|{8,10:0.000} {9,10:0.000} {10,10:0.000} {11:X8} {12:X6}"
                    , o.x0, o.y0, o.z0, o.w0
                    , o.x1, o.y1, o.z1, o.w1
                    , o.x2, o.y2, o.z2, o.w2, o.w2 >> 10
                    );
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            S5Refi s1 = listBox1.SelectedItem as S5Refi;
            if (s1 == null) return;
            int x = 0;
            string row = "";
            foreach (Vone vone in s1.hive.al) {
                row += ("#" + x + " " + FmtVoneUtil.Format(vone)) + "\r\n";
                x++;
            }
            row += new string(' ', 164);
            labelV.Text = row;
        }

        private void buttonThreed_Click(object sender, EventArgs e) {
            using (ProtForm3 form = new ProtForm3()) {
                form.init(mdlx.e0.s4, new Texexp(mdlx.e0));
                form.ShowDialog(this);
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                foreach (string f in fs) {
                    if (string.Compare(Path.GetExtension(f), ".mdls") == 0) {
                        loadMdls(f);
                        break;
                    }
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void buttonTex_Click(object sender, EventArgs e) {
            using (TexForm form = new TexForm(new Texexp(mdlx.e0))) {
                form.ShowDialog(this);
            }
        }

        private void buttonMset_Click(object sender, EventArgs e) {
            Form2 form = new Form2();
            form.Show();
        }
    }
    public class Texexp {
        E0 e0;

        public Texexp(E0 e0) {
            this.e0 = e0;
        }
        public Bitmap Explode(int x) {
            int cnt = e0.v14 / 16384;
            if ((uint)x >= (uint)cnt) return null;
            Bitmap pic = new Bitmap(128, 128, PixelFormat.Format8bppIndexed);
            if (true) {
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, 128, 128), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try {
                    Marshal.Copy(e0.bin, e0.v10 + 16384 * x, bd.Scan0, 16384);
                }
                finally {
                    pic.UnlockBits(bd);
                }
            }

            if (true) {
                MemoryStream si = new MemoryStream(e0.bin, e0.v18 + 1024 * x, e0.v1c - 1024 * x, false);
                Debug.Assert(si.Length >= 1024);
                ColorPalette pal = pic.Palette;
                for (int t = 0; t < 256; t++) {
                    byte vr = (byte)Math.Min(255, si.ReadByte() + 1);
                    byte vg = (byte)Math.Min(255, si.ReadByte() + 1);
                    byte vb = (byte)Math.Min(255, si.ReadByte() + 1);
                    byte va = (byte)Math.Min(255, si.ReadByte() + 0);
                    pal.Entries[(t & 0xE7) | (((t & 0x10) != 0) ? 0x08 : 0) | (((t & 0x08) != 0) ? 0x10 : 0)] = Color.FromArgb(vr, vg, vb);
                }
                pic.Palette = pal;
            }

            return pic;
        }
    }
}
namespace Rapemdls.Mdlxo {
    class Mdlx {
        public void read(byte[] bin) {
            try {
                MemoryStream si = new MemoryStream(bin, false);
                BinaryReader br = new BinaryReader(si);
                int cnt = br.ReadInt32();
                int[] offs = new int[cnt + 1];
                for (int x = 0; x < cnt + 1; x++) offs[x] = br.ReadInt32();
                alent.Clear();
                for (int x = 0; x < cnt; x++) {
                    si.Position = offs[x];
                    int size = offs[x + 1] - offs[x];
                    byte[] raw = new byte[size];
                    si.Read(raw, 0, size);
                    alent.Add(new Ent(offs[x], raw));
                    if (x == 0) { e0 = new E0(); e0.read(raw); }
                }
            }
            catch (Exception err) {
                Debug.WriteLine(err);
                throw;
            }
        }

        public E0 e0 = null;
        public List<Ent> alent = new List<Ent>();
    }
    public class E0 {
        public void read(byte[] bin) {
            this.bin = bin;
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);
            si.Position = 0x08;
            v08 = br.ReadInt32(); // off S1 (dmac?)
            v0c = br.ReadInt32(); // cb S1
            v10 = br.ReadInt32(); // off S2 (texture?)
            v14 = br.ReadInt32(); // cb S2
            v18 = br.ReadInt32(); // off S3 (array of singles)
            v1c = br.ReadInt32(); // cb S3
            v20 = br.ReadInt32(); // off S4 (vone struc?)
            v24 = br.ReadInt32(); // size S4
            v28 = br.ReadInt32(); // off ?

            si.Position = v20;
            byte[] s4bin = br.ReadBytes(v24);
            s4 = new S4(s4bin);

            si.Position = v18;
            palbin = br.ReadBytes(v1c);
        }

        public int v08, v0c;
        public int v10, v14, v18, v1c;
        public int v20, v24, v28;
        public byte[] bin;
        public S4 s4;
        public byte[] palbin = null;
    }
    /// <summary>
    /// 16 bytes header component in S4 contents.
    /// </summary>
    public class S5 {
        public int v00;
        public int v04;
        public int v08;
        public int v0c;
        public List<Hive> alhive = new List<Hive>();
    }
    /// <summary>
    /// bone contents structure?
    /// </summary>
    public class S4 {
        public S4(byte[] bin) {
            this.bin = bin;
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);
            v00 = br.ReadInt32(); // cnt vones in root vone
            v04 = br.ReadInt32(); // off root vone
            v08 = br.ReadInt32(); // cnt?
            v0c = br.ReadInt32(); // cnt S4T1

            als5.Clear();
            for (int x = 0; x < v0c; x++) {
                S5 s1 = new S5();
                s1.v00 = br.ReadInt32(); // ?
                s1.v04 = br.ReadInt32(); // tex index?
                s1.v08 = br.ReadInt32(); // tex index?
                s1.v0c = br.ReadInt32(); // offset
                als5.Add(s1);
            }

            si.Position = v04;
            for (int x = 0; x < v00; x++)
                alvr.Add(ReadVoneUtil.read(br));

            for (int t = 0; t < v0c; t++) {
                S5 s1 = als5[t];
                si.Position = s1.v0c;
                int cntx1 = 0;
                int cntx2 = 0;
                for (int x = 0; ; x++) {
                    int curoff = (int)si.Position;
                    Hive hive = new Hive();
                    int t00 = hive.t00 = br.ReadInt32(); // 1 or ?
                    int t04 = hive.t04 = br.ReadInt32(); // cb cluster
                    hive.off = curoff;
                    hive.len = t04;
                    if (t00 == 1) {
                        int t08 = br.ReadInt32(); // cnt vones if vone cluster
                        int t0c = br.ReadInt32(); // vone index?
                        int t10 = br.ReadInt32();
                        int t14 = br.ReadInt32();
                        int t18 = br.ReadInt32();
                        int t1c = br.ReadInt32();
                        hive.vi = t0c;
                        for (int z = 0; z < t08; z++) {
                            hive.al.Add(ReadVoneUtil.read(br));
                        }
                        cntx1 += t08;
                        cntx2++;
                    }
                    else if (t00 == 0x8000) {
                        if (t04 == 0) break;
                    }
                    else if (0 == (t00 & 0xFFFF)) {
                        hive.len = 0x80;
                        int t08 = hive.t04 = br.ReadInt32();
                        hive.voneval = t04;
                        hive.vonekey = t08;
                    }
                    else if (false
                        || 0xED == (t00 & 0xFFFF)
                        || 0xF0 == (t00 & 0xFFFF)
                        || 0xEE == (t00 & 0xFFFF)
                        || 0xEB == (t00 & 0xFFFF)
                        || 0xEA == (t00 & 0xFFFF)
                        || 0xE8 == (t00 & 0xFFFF)
                        || 0xE7 == (t00 & 0xFFFF)
                        || 0x7A == (t00 & 0xFFFF)
                        || 0xE9 == (t00 & 0xFFFF)
                        || 0xDC == (t00 & 0xFFFF)
                        || 0xE6 == (t00 & 0xFFFF)
                        || 0xEF == (t00 & 0xFFFF)
                        || 0x82 == (t00 & 0xFFFF)
                        || 0xE0 == (t00 & 0xFFFF)
                    ) {
                        hive.len = 0x10;
                    }
                    else {
                        hive.len = 0x10;
                        //Debug.Fail("? ¨ " + t00.ToString("X"));
                    }
                    s1.alhive.Add(hive);
                    Debug.Assert(hive.len > 0);
                    si.Position = curoff + hive.len;
                }
            }
        }

        public List<S5> als5 = new List<S5>();
        public int v00, v04, v08, v0c;
        public List<Vone> alvr = new List<Vone>();
        public byte[] bin;
    }
    public class Hive {
        public int t00, t04;
        public int vi;
        public int off = 0, len = 0;
        public int vonekey = -1, voneval = -1;
        public List<Vone> al = new List<Vone>();
    }
    public class ReadVoneUtil {
        public static Vone read(BinaryReader br) {
            Vone o = new Vone();
            o.x0 = br.ReadSingle(); o.y0 = br.ReadSingle(); o.z0 = br.ReadSingle(); o.w0 = br.ReadInt32();
            o.x1 = br.ReadSingle(); o.y1 = br.ReadSingle(); o.z1 = br.ReadSingle(); o.w1 = br.ReadSingle();
            o.x2 = br.ReadSingle(); o.y2 = br.ReadSingle(); o.z2 = br.ReadSingle(); o.w2 = br.ReadInt32();
            return o;
        }
    }
    public class Vone {
        public float x0, y0, z0;
        public int w0;
        public float x1, y1, z1, w1;
        public float x2, y2, z2;
        public int w2;

        public int Parent {
            get { int v = (w2 & 1023); return (v == 1023) ? -1 : v; }
        }
        public int Belonging {
            get { return w0; }
        }

        public Vone Clone() {
            return (Vone)MemberwiseClone();
        }
    }

    public class Ent {
        public byte[] bin;
        public int off;

        public Ent(int off, byte[] bin) {
            this.off = off;
            this.bin = bin;
        }
    }
}