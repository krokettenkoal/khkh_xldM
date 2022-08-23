using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PMatchPic {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Bitmap pic1 = (Bitmap)Bitmap.FromFile("Q1.bmp");
            Bitmap pic2 = (Bitmap)Bitmap.FromFile("Q2.png");

            Trace.Assert(pic1.Width == 256 && pic1.Height == 256, "pic1.Width == 256 && pic1.Height == 256");
            Trace.Assert(pic2.Width == 256 && pic2.Height == 256, "pic2.Width == 256 && pic2.Height == 256");
            Trace.Assert(pic1.PixelFormat == PixelFormat.Format8bppIndexed, "pic1.PixelFormat == PixelFormat.Format8bppIndexed");

            byte[] bin = new byte[256 * 256];
            BitmapData bd = pic1.LockBits(Rectangle.FromLTRB(0, 0, 256, 256), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            try {
                Marshal.Copy(bd.Scan0, bin, 0, bin.Length);
            }
            finally {
                pic1.UnlockBits(bd);
            }

            Color[] clr = new Color[256];
            bool[] first = new bool[256];
            for (int t = 0; t < 256; t++) { clr[t] = Color.Empty; first[t] = true; }

            Bitmap pic3 = new Bitmap(256, 256);
            using (Graphics cv = Graphics.FromImage(pic3)) {
                cv.Clear(Color.Blue);
                int cntvio = 0;
                for (int y = 0; y < 256; y++) {
                    for (int x = 0; x < 256; x++) {
                        byte b = bin[256 * y + x];
                        Color clr2 = pic2.GetPixel(x, y);
                        if (first[b]) {
                            first[b] = false;
                            clr[b] = clr2;
                        }
                        else {
                            if (clr[b] != clr2) {
                                pic3.SetPixel(x, y, Color.White);
                                cntvio++;
                            }
                        }
                    }
                }

                Color[] fakepal = new Color[256];
                for (int t = 0; t < 256; t++)
                    fakepal[t] = Color.FromArgb(
                        255,
                        pic1.Palette.Entries[t].R - 0,
                        pic1.Palette.Entries[t].G - 0,
                        pic1.Palette.Entries[t].B - 0
                        );

                pictureBoxRealpal.Image = MkPal.make(fakepal);
                pictureBoxIdealpal.Image = MkPal.make(clr);

                this.Text += " cntvio=" + cntvio;

                StringBuilder s = new StringBuilder();
                s.AppendLine("1 to 2");
                for (int t = 0; t < 256; t++) {
                    int v;
                    for (v = 0; v < 256; v++) {
                        if (Clr3.Compare(fakepal[t], clr[v]) == 0)
                            break;
                    }
                    if (v == 256) v = -1;
                    s.AppendFormat("{0,3}¨{1,3} ({2,4}) {3:X4}¨{4:X4}\r\n"
                        , t, v, (v == -1) ? 9999 : v - t
                        , 4 * t, 4 * v
                        );
                }
                textBox1.Text = s.ToString();
            }

            pictureBox1.Image = pic1;
            pictureBox2.Image = pic2;
            pictureBox3.Image = pic3;
        }

    }
    class Clr3 {
        public static int Compare(Color c1, Color c2) {
            int t;
            if (0 != (t = c1.R.CompareTo(c2.R))) return t;
            if (0 != (t = c1.G.CompareTo(c2.G))) return t;
            if (0 != (t = c1.B.CompareTo(c2.B))) return t;
            return 0;
        }
    }
    class MkPal {
        public static Bitmap make(Color[] clrs) {
            Bitmap pic = new Bitmap(256, 256, PixelFormat.Format32bppArgb);
            using (Graphics cv = Graphics.FromImage(pic)) {
                for (int y = 0; y < 16; y++)
                    for (int x = 0; x < 16; x++)
                        using (Brush br = new SolidBrush(clrs[16 * y + x]))
                            cv.FillRectangle(br, new Rectangle(16 * x, 16 * y, 16, 16));
            }
            return pic;
        }
    }
}