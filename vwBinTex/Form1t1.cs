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

namespace vwBinTex {
    public partial class Form1t1 : Form {
        public Form1t1() {
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

        int wcx = 128, wbpp = 16;
        FileStream fs = null;
        Bitmap pic = null;

        private void openIt(string f) {
            if (fs != null)
                fs.Close();
            fs = null;
            fs = File.OpenRead(f);
            renderIt();
        }

        static int RTo4(int x) {
            return (x + 3) & (~3);
        }

        private void renderIt() {
            if (pic != null)
                pic.Dispose();
            if (fs == null)
                return;
            fs.Position = 0;
            int pitchSrc = RTo4((wcx * wbpp) / 8);
            int wcy = Math.Min(512, (int)(fs.Length / pitchSrc));
            pic = new Bitmap(wcx, wcy, PixelFormat.Format24bppRgb);
            int pitchpic = RTo4(wcx * 3);
            byte[] picbin = new byte[pitchpic * wcy];

            int off = 0;
            for (int y = 0; y < wcy; y++) {
                off = y * pitchpic;
                for (int x = 0; x < wcx; x++) {
                    if (wbpp == 8) {
                        byte v = (byte)fs.ReadByte();
                        picbin[off] = (byte)(v); off++;
                        picbin[off] = (byte)(v); off++;
                        picbin[off] = (byte)(v); off++;
                    }
                    else if (wbpp == 16) {
                        ushort v = (ushort)(fs.ReadByte() << 8);
                        v |= (ushort)(fs.ReadByte() << 0);
                        picbin[off] = (byte)((((v >> 0) & 31)) << (3)); off++;
                        picbin[off] = (byte)((((v >> 5) & 31)) << (3)); off++;
                        picbin[off] = (byte)((((v >> 10) & 31)) << (3)); off++;
                    }
                    else if (wbpp == 24) {
                        picbin[off] = (byte)fs.ReadByte(); off++;
                        picbin[off] = (byte)fs.ReadByte(); off++;
                        picbin[off] = (byte)fs.ReadByte(); off++;
                    }
                    else if (wbpp == 32) {
                        fs.ReadByte();
                        picbin[off] = (byte)fs.ReadByte(); off++;
                        picbin[off] = (byte)fs.ReadByte(); off++;
                        picbin[off] = (byte)fs.ReadByte(); off++;
                    }
                }
            }

            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, wcx, wcy), ImageLockMode.WriteOnly, pic.PixelFormat);
            try {
                Trace.Assert(picbin.Length <= bd.Stride * bd.Height);
                Marshal.Copy(picbin, 0, bd.Scan0, picbin.Length);
            }
            finally {
                pic.UnlockBits(bd);
            }
            pictureBox1.Image = pic;
            toolStripStatusLabel1.Text = string.Format("{0}~{1} (~{2})", wcx, wcy, wbpp);
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

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            int wcx = this.wcx;
            int wbpp = this.wbpp;
            switch (e.KeyChar) {
                case '[':
                    wcx = Math.Max(1, wcx - 1); break;
                case ']':
                    wcx = Math.Min(512, wcx + 1); break;
                case '{':
                    wcx = Math.Max(1, wcx - 1 * 16); break;
                case '}':
                    wcx = Math.Min(512, wcx + 1 * 16); break;
                case '@':
                    switch (wbpp) {
                        case 8: wbpp = 16; break;
                        case 16: wbpp = 24; break;
                        case 24: wbpp = 32; break;
                        case 32:
                        default: wbpp = 8; break;
                    }
                    break;
            }
            if (wcx != this.wcx || wbpp != this.wbpp) {
                this.wcx = wcx;
                this.wbpp = wbpp;
                renderIt();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {

        }
    }
}