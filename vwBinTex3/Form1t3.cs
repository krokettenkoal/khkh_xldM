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

namespace vwBinTex3 {
    public partial class Form1t3 : Form {
        public Form1t3() {
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

        int wcx = 128;
        int wbpp = 16;
        FileStream fs = null;
        Bitmap pic = null;

        private void openIt(string f) {
            if (fs != null)
                fs.Close();
            fs = null;
            fs = File.OpenRead(f);
            renderIt();
        }

        static readonly byte[] tblT16BX = new byte[] {
            0,0,1,1,0,0,1,1, 2,2,3,3,2,2,3,3, 0,0,1,1,0,0,1,1, 2,2,3,3,2,2,3,3,
        };
        static readonly byte[] tblT16BY = new byte[] {
            0,1,0,1,2,3,2,3, 0,1,0,1,2,3,2,3, 4,5,4,5,6,7,6,7, 4,5,4,5,6,7,6,7,
        };

        static readonly byte[] tblT32p2c = new byte[16] { // PIXEL 2 COL map
             0, 1, 4, 5, 8, 9,12,13,
             2, 3, 6, 7,10,11,14,15,
        };
        static readonly byte[] tblT32b2p = new byte[32] { // BLOCK 2 PAGE map
             0, 1, 4, 5,16,17,20,21,
             2, 3, 6, 7,18,19,22,23,
             8, 9,12,13,24,25,28,29,
            10,11,14,15,26,27,30,31,
        };

        private void renderIt() {
            if (pic != null)
                pic.Dispose();
            if (fs == null)
                return;
            fs.Position = 0;
            byte[] bin = new byte[1024 * 128];
            fs.Read(bin, 0, bin.Length);
            pic = new Bitmap(64, 512, PixelFormat.Format24bppRgb);
            // 64 x 512 x 32
            int wcx = 64;
            int wcy = 512;
            int wbpp = 32;
            for (int wc = 0; wc < 16 * 128; wc++) {
                int wX = (wc % 8) * 8;
                int wY = (wc / 8) * 2;
                for (int ec = 0; ec < 16; ec++) { // 1 COLUMN  16 pixels
                    int eX = (ec & 7);
                    int eY = (ec / 8);

                    int off = 0
                        + /*COL*/(4 * tblT32p2c[ec])
                        + /*BLK*/(64 * wc)
                        ;

                    int A = bin[off]; off++;
                    int R = bin[off]; off++;
                    int G = bin[off]; off++;
                    int B = bin[off]; off++;
                    Color clr = Color.FromArgb(255, R, G, B);
                    int aX = wX + eX;
                    int aY = wY + eY;
                    pic.SetPixel(aX, aY, clr);
                }
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

        private void Form1t3_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialog1.FileName))
                openFileDialog1.ShowDialog(this);
            if (!File.Exists(openFileDialog1.FileName))
                return;

            openIt(openFileDialog1.FileName);
        }
    }
}