using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace timVw {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.OemOpenBrackets:
                    wcx = Math.Max(wcx - 1, 1);
                    repaint();
                    break;
                case Keys.Oem6:
                    wcx = Math.Min(wcx + 1, 256);
                    repaint();
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialog1.FileName))
                if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
                    Environment.Exit(1);
            if (!File.Exists(openFileDialog1.FileName))
                Environment.Exit(1);

            openIt(openFileDialog1.FileName);
        }

        void openIt(string f) {
            using (FileStream fs = File.OpenRead(f)) {
                BinaryReader br = new BinaryReader(fs);

                fs.Position = 0x1C;
                int offBits = br.ReadInt32();
                int offPals = br.ReadInt32();

                fs.Position = offBits;
                bin = br.ReadBytes(offPals - offBits);

                fs.Position = offPals;
                palbin = br.ReadBytes(Convert.ToInt32(fs.Length) - 4 - offPals);

                fs.Close();
            }
            repaint();
        }

        byte[] bin = null;
        byte[] palbin = null;
        int wcx = 16;
        int zfact = 1;

#if true
        byte getR(byte a) { return a; }
        byte getG(byte a) { return a; }
        byte getB(byte a) { return a; }
#else
        byte getR(byte a) { return palbin[4 * a + 0]; }
        byte getG(byte a) { return palbin[4 * a + 1]; }
        byte getB(byte a) { return palbin[4 * a + 2]; }
#endif

        Bitmap makeStretch(Bitmap pic, int cx) {
            if (cx <= 1)
                return pic;
            Bitmap p2 = new Bitmap(pic.Width * cx, pic.Height * cx, pic.PixelFormat);
            using (Graphics cv = Graphics.FromImage(p2)) {
                Point[] pts = new Point[] {
                    new Point(0, 0),
                    new Point(pic.Width*cx, 0),
                    new Point(0, pic.Height*cx),
                };
                cv.PixelOffsetMode = PixelOffsetMode.None;
                cv.InterpolationMode = InterpolationMode.NearestNeighbor;
                cv.DrawImage(pic, pts, Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), GraphicsUnit.Pixel);
            }
            return p2;
        }

#if false
#elif true
        void repaint() {
            Bitmap pic = new Bitmap(256, 256, PixelFormat.Format32bppArgb);
            byte[] pix = new byte[256 * 4 * 256];
            for (int y = 0; y < 128; y++) {
                for (int x = 0; x < 64; x++) {
                    int srco = (x & 1) + (((x % 64) / 2) * 256) + (2 * (y % 128));
                    int dsto = (256 * 4 * y) + 4 * x;
                    byte b = bin[srco]; srco++;
                    pix[dsto] = getR(b); dsto++;
                    pix[dsto] = getG(b); dsto++;
                    pix[dsto] = getB(b); dsto++;
                    pix[dsto] = 255; dsto++;
                }
            }

            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, 256, 256), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                Marshal.Copy(pix, 0, bd.Scan0, pix.Length);
            }
            finally {
                pic.UnlockBits(bd);
            }
            pictureBox1.Image = makeStretch(pic, zfact);
        }
#elif false
        void repaint() {
            Bitmap pic = new Bitmap(256, 256, PixelFormat.Format32bppArgb);
            byte[] pix = new byte[256 * 4 * 256];
            int bcx = 256;
            int bcy = bin.Length / 256;
            for (int y = 0; y < bcy; y++) {
                byte xorx = 0;// (byte)(((y & 2) != 0) ? 1 : 0);
                for (int x = 0; x < bcx; x++) {
                    int srco = 256 * y + (x ^ xorx);
                    int dsto = bcx * 4 * y + 4 * x;
                    byte b = bin[srco]; srco++;
                    pix[dsto] = getR(b); dsto++;
                    pix[dsto] = getG(b); dsto++;
                    pix[dsto] = getB(b); dsto++;
                    pix[dsto] = 255; dsto++;
                }
            }

            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, 256, 256), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                Marshal.Copy(pix, 0, bd.Scan0, pix.Length);
            }
            finally {
                pic.UnlockBits(bd);
            }
            pictureBox1.Image = makeStretch(pic, zfact);
        }
#else
        void repaint() {
            int bcx = wcx;
            int bcy = Math.Max(1, bin.Length / wcx);
            Bitmap pic = new Bitmap(bcx, bcy, PixelFormat.Format32bppArgb);
            int o = 0, opix = 0;
            byte[] pix = new byte[bcx * 4 * bcy];
            for (int y = 0; y < bcy; y++) {
                byte vy = (byte)(((y & 2) != 0) ? 1 : 0);
                for (int x = 0; x < bcx; x++, o++) {
                    byte b = bin[o ^ vy];
                    pix[opix] = getR(b); opix++;
                    pix[opix] = getG(b); opix++;
                    pix[opix] = getB(b); opix++;
                    pix[opix] = 255; opix++;
                }
            }
            pictureBox1.Image = pic;
            toolStripStatusLabel1.Text = string.Format("{0} ~ {1}", bcx, bcy);

            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, bcx, bcy), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                Marshal.Copy(pix, 0, bd.Scan0, bd.Stride * bcy);
            }
            finally {
                pic.UnlockBits(bd);
            }
        }
#endif

        private void Form1_Paint(object sender, PaintEventArgs e) {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                foreach (string f in fs) {
                    openIt(f);
                    break;
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case ',':
                    zfact = Math.Max(1, zfact - 1);
                    repaint();
                    break;
                case '.':
                    zfact = Math.Min(16, zfact + 1);
                    repaint();
                    break;
            }
        }
    }
}