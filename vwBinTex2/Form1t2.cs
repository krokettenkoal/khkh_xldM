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
using System.Diagnostics;

using vcBinTex4;

namespace vwBinTex2 {
    public partial class Form1t2 : Form {
        public Form1t2() {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs == null)
                return;
            foreach (string f in fs) {
                openIt(f);
            }
        }

        private void openIt(string f) {
            using (FileStream fs = File.OpenRead(f)) {
                timf = new TIMf();
                timf.Load(fs);
            }

            renderIt();
        }

        int picSel = 0;
        TIMf timf = null;

        private void renderIt() {
            if (timf == null)
                return;
            Bitmap pic = null;
            if (timf.pics.Count != 0) {
                pic = timf.pics[timf.refal[Math.Max(0, Math.Min(timf.refal.Length - 1, picSel))]];
            }

            pictureBox1.Image = pic;

            if (pic != null) {
                pictureBox1.Size = new Size(pic.Width * 2, pic.Height * 2);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            toolStripStatusLabel1.Text = string.Format("#{0}, {1}Å~{2}, Len1({3}), Len2({4})"
                , picSel
                , (pic != null) ? pic.Width : 0, (pic != null) ? pic.Height : 0
                , timf.wcx, timf.wcy
                );
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            Form1_DragOver(sender, e);
        }

        private void Form1_DragOver(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Link;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Shift && !e.Control) {
                switch (e.KeyCode) {
                    case Keys.Left:
                        picSel--;
                        renderIt(); e.Handled = true; break;
                    case Keys.Right:
                        picSel++;
                        renderIt(); e.Handled = true; break;
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {

        }

        private void Form1t2_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialog1.FileName))
                openFileDialog1.ShowDialog(this);
            if (!File.Exists(openFileDialog1.FileName))
                return;

            openIt(openFileDialog1.FileName);
        }

        class TIMf {
            public int wcx, wcy, bpp;
            public byte[] picbin = null;
            public byte[] palbin = null;
            public byte[] refal = null;
            public byte[] texinf1bin = null;
            public byte[] texinf2bin = null;
            public List<Bitmap> pics = new List<Bitmap>();

            public void Load(FileStream fs) {
                pics.Clear();

                BinaryReader br = new BinaryReader(fs);
                fs.Position = 8;
                wcx = br.ReadInt32(); // @ 0x08
                wcy = br.ReadInt32(); // @ 0x0C
                int woff = br.ReadInt32(); // @ 0x10
                int texinf1off = br.ReadInt32(); // @0x14
                int texinf2off = br.ReadInt32(); // @0x18
                fs.Position = woff;
                refal = br.ReadBytes(wcy);
                fs.Position = 0x1c;
                int picoff = br.ReadInt32();
                int paloff = br.ReadInt32();

                fs.Position = texinf1off;
                texinf1bin = br.ReadBytes(texinf2off - texinf1off);
                fs.Position = texinf2off;
                texinf2bin = br.ReadBytes(picoff - texinf2off);
                fs.Position = picoff;
                picbin = br.ReadBytes(paloff - picoff);
                palbin = br.ReadBytes(Convert.ToInt32(fs.Length) - 4 - paloff);

                byte[] gsram = new byte[4 * 1024 * 1024];

                // TEXTURE MAPPING PROGRAM
                for (int wy = 0; wy < wcy; wy++) {
                    if (true) {
                        // TEXTURE PREPARATION PROGRAM
                        for (int wi = 0; wi < 2; wi++) {
                            int wx = (wi == 0) ? 0 : (1 + refal[wy]);
                            fs.Position = texinf1off + 144 * wx + 0x20;
                            UInt64 v0 = br.ReadUInt64();
                            int sbp = ((int)(v0 >> 0) & 0x3FFF);
                            int sbw = ((int)(v0 >> 16) & 0x3F);
                            int spsm = ((int)(v0 >> 24) & 0x3F);
                            int dbp = ((int)(v0 >> 32) & 0x3FFF);
                            int dbw = ((int)(v0 >> 48) & 0x3F);
                            int dpsm = ((int)(v0 >> 56) & 0x3F);
                            Trace.Assert(br.ReadUInt64() == 0x50, "Invalid");

                            fs.Position = texinf1off + 144 * wx + 0x40;
                            UInt64 v2 = br.ReadUInt64();
                            int rrw = ((int)(v2 >> 0) & 0xFFF);
                            int rrh = ((int)(v2 >> 32) & 0xFFF);
                            Trace.Assert(br.ReadUInt64() == 0x52, "Invalid");

                            fs.Position = texinf1off + 144 * wx + 0x60;
                            UInt64 v4 = br.ReadUInt64();
                            int nloop = ((int)(v4 >> 0) & 0x3FFF);

                            fs.Position = texinf1off + 144 * wx + 0x70;
                            UInt64 v5 = br.ReadUInt64();
                            int ilen = ((int)(v5 >> 0) & 0x3FFF);
                            int ioff = ((int)(v5 >> 32) & 0x7FFFFFFF);
                            Trace.Assert(nloop == ilen, "Invalid");

                            fs.Position = ioff;
                            byte[] ibin = new byte[16 * ilen];
                            int r = fs.Read(ibin, 0, 16 * ilen);

                            Trace.Assert(dpsm == 0);
                            int dbh = Convert.ToInt32(ibin.Length) / 8192 / dbw;
                            ibin = Reform32.Encode32(ibin, dbw, dbh);

                            Array.Copy(ibin, 0, gsram, 256 * dbp, 16 * ilen);

                            Console.Write("");
                        }
                    }
                    if (true) {
                        Debug.Assert(refal[wy] < wcx, "Invalid");

                        fs.Position = texinf2off + 160 * wy + 0x20;
                        UInt64 v0 = br.ReadUInt64();
                        Trace.Assert(v0 == 0, "Invalid");
                        Trace.Assert(br.ReadUInt64() == 0x3F, "Invalid");

                        fs.Position = texinf2off + 160 * wy + 0x30;
                        UInt64 v1 = br.ReadUInt64();
                        Trace.Assert(v1 == 0, "Invalid");
                        Trace.Assert(br.ReadUInt64() == 0x34, "Invalid");

                        fs.Position = texinf2off + 160 * wy + 0x40;
                        UInt64 v2 = br.ReadUInt64();
                        Trace.Assert(v2 == 0, "Invalid");
                        Trace.Assert(br.ReadUInt64() == 0x36, "Invalid");

                        fs.Position = texinf2off + 160 * wy + 0x50;
                        UInt64 v3 = br.ReadUInt64(); // TEX2_x
                        int psm = ((int)(v3 >> 20) & 0x3F);
                        int cbp = ((int)(v3 >> 37) & 0x3FFF);
                        int cpsm = ((int)(v3 >> 51) & 0xF);
                        int csm = ((int)(v3 >> 55) & 0x1);
                        int csa = ((int)(v3 >> 56) & 0x1F);
                        int cld = ((int)(v3 >> 61) & 0x7);
                        Trace.Assert(br.ReadUInt64() == 0x16, "Invalid");

                        fs.Position = texinf2off + 160 * wy + 0x70;
                        UInt64 v5 = br.ReadUInt64(); // TEX0_x
                        int tbp0 = ((int)(v5 >> 0) & 0x3FFF);
                        int tbw = ((int)(v5 >> 14) & 0x3F);
                        int psmX = ((int)(v5 >> 20) & 0x3F);
                        int tw = ((int)(v5 >> 26) & 0xF);
                        int th = ((int)(v5 >> 30) & 0xF);
                        int tcc = ((int)(v5 >> 34) & 0x1);
                        int tfx = ((int)(v5 >> 35) & 0x3);
                        int cbpX = ((int)(v5 >> 37) & 0x3FFF);
                        int cpsmX = ((int)(v5 >> 51) & 0xF);
                        int csmX = ((int)(v5 >> 55) & 0x1);
                        int csaX = ((int)(v5 >> 56) & 0x1F);
                        int cldX = ((int)(v5 >> 56) & 0x7);
                        Trace.Assert(br.ReadUInt64() == 0x06, "Invalid");
                        //TransUtil.Exist(texbuf, cbpX);
                        //Trace.Assert(texbuf.ContainsKey(cbpX), "Invalid");
                        //Trace.Assert(texbuf.ContainsKey(tbp0), "Invalid");
                        //Trace.Assert(cpsmX == 0, "Unsupported");
                        //Trace.Assert(csmX == 0, "Unsupported");
                        //Trace.Assert(csaX == 0, "Unsupported");

                        int sizetbp0 = (1 << tw) * (1 << th);
                        byte[] buftbp0 = new byte[sizetbp0];
                        Array.Copy(gsram, 256 * tbp0, buftbp0, 0, buftbp0.Length);
                        byte[] bufcbpX = new byte[4 * 256];
                        Array.Copy(gsram, 256 * cbpX, bufcbpX, 0, bufcbpX.Length);

                        Bitmap ipic = null;
                        if (psmX == 0x13) ipic = TexUtil.Decode8(buftbp0, bufcbpX, tbw, 1 << tw, 1 << th);
                        if (psmX == 0x14) ipic = TexUtil.Decode4(buftbp0, bufcbpX, tbw, 1 << tw, 1 << th);
                        pics.Add(ipic);

                        Console.Write("");
                    }
                }
            }

            class TransUtil {
                public static void Exist(SortedDictionary<int, byte[]> texbuf, int offset) {
                    if (texbuf.ContainsKey(offset))
                        return;
                    foreach (int k in texbuf.Keys) {
                        if (offset < k)
                            continue;
                        byte[] bin = texbuf[k];
                        byte[] fake = new byte[bin.Length];
                        int span = (offset - k) * 256;
                        Array.Copy(bin, span, fake, 0, bin.Length - span);
                        texbuf[offset] = fake;
                        break;
                    }
                }
            }

            class TexUtil {
                public static Bitmap Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy) {
                    Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                    tbw /= 2;
                    Debug.Assert(tbw != 0, "Invalid");
                    byte[] bin = Reform8.Decode8(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
                    BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                    try {
                        int buffSize = bd.Stride * bd.Height;
                        Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                    }
                    finally {
                        pic.UnlockBits(bd);
                    }
                    ColorPalette pals = pic.Palette;
                    for (int pi = 0; pi < 256; pi++) {
                        int rpi = KHcv8pal.repl(pi);
                        pals.Entries[rpi] = (Color.FromArgb(
                            (palbin[4 * (pi) + 3] + 1),
                            (palbin[4 * (pi) + 0] + 1),
                            (palbin[4 * (pi) + 1] + 1),
                            (palbin[4 * (pi) + 2] + 1)
                            ));
                    }
                    pic.Palette = pals;
                    return pic;
                }

                public static Bitmap Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy) {
                    Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                    tbw /= 2;
                    Debug.Assert(tbw != 0, "Invalid");
                    byte[] bin = Reform4.Decode4(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
                    BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

                    try {
                        int buffSize = bd.Stride * bd.Height;
                        Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                    }
                    finally {
                        pic.UnlockBits(bd);
                    }
                    ColorPalette pals = pic.Palette;
                    int psi = 0;
                    for (int pi = 0; pi < 16; pi++) {
                        pals.Entries[pi] = (Color.FromArgb(
                            palbin[psi + 4 * (pi) + 3],
                            palbin[psi + 4 * (pi) + 0],
                            palbin[psi + 4 * (pi) + 1],
                            palbin[psi + 4 * (pi) + 2]
                            ));
                    }
                    pic.Palette = pals;
                    return pic;
                }
            }

            public const float GammaFactor = 0.5f;

            class CUtil {
                public static Color Gamma(Color a, float gamma) {
                    return Color.FromArgb(
                        255,//a.A,
                        Math.Min(255, (int)(Math.Pow(a.R / 255.0, gamma) * 255.0)),
                        Math.Min(255, (int)(Math.Pow(a.G / 255.0, gamma) * 255.0)),
                        Math.Min(255, (int)(Math.Pow(a.B / 255.0, gamma) * 255.0))
                        );
                }
            }

            class CLUT8 {
                public static int perform(int t) {
                    return (t & 0xFFE7) | (((t & 0x10) != 0) ? 0x08 : 0x00) | (((t & 0x08) != 0) ? 0x10 : 0x00);
                }
                static int[] tblrepl = new int[] { 0, 1, 8, 9, 2, 3, 10, 11, 4, 5, 12, 13, 6, 7, 14, 15 };
                public static int repl(int t) {
                    return (t & 0xF0) | tblrepl[t & 0xF];
                }
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e) {
            pictureBox1.Image.Save("temp.bmp", ImageFormat.Bmp);
            Process.Start("explorer.exe", " /select,temp.bmp");
        }
    }
}