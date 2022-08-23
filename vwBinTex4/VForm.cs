using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using vcBinTex4;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace vwBinTex4 {
    public partial class VForm : Form {
        public VForm() {
            InitializeComponent();
        }

        private void VForm_Load(object sender, EventArgs e) {
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif0005.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif0009.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif0033.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif5083.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif5070.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif3710.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\vif3624.bin");
            //openIt(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\ttrap\gif0437.bin");
            //openIt(@"H:\Proj\khkh_xldM\parsePAX_\bin\Debug\work\gif0003.bin");
            openIt(@"C:\A\PEX100_0.bin");
        }

        private void openIt(string fp) {
            flppic.Controls.Clear();

            if (!File.Exists(fp)) return;

            byte[] raw1 = File.ReadAllBytes(fp);

            // 8
            if (raw1.Length == 65536) ReadAs8(256, 256, 2, raw1);
            if (raw1.Length == 32768) ReadAs8(256, 128, 2, raw1);
            if (raw1.Length == 16384) ReadAs8(128, 128, 1, raw1);
            if (raw1.Length == 32768) ReadAs8(256, 128, 4, raw1);
            if (raw1.Length == 4096) ReadAs8(64, 64, 2, raw1);
            if (raw1.Length == 114688) ReadAs8(256, 448, 4, raw1);
            if (raw1.Length == 8192) ReadAs8(128, 64, 2, raw1);
            if (raw1.Length == 4096) ReadAs8(128, 32, 2, raw1);
            if (raw1.Length == 131072) ReadAs8(512, 256, 4, raw1);
            if (raw1.Length == 16384) ReadAs8(256, 64, 4, raw1);

            // 4
            if (raw1.Length == 32768) ReadAs4(256, 256, 2, raw1);
            if (raw1.Length == 16384) ReadAs4(256, 128, 1, raw1);
            if (raw1.Length == 16384) ReadAs4(128, 256, 1, raw1);
            if (raw1.Length == 8192) ReadAs4(128, 128, 1, raw1);
            if (raw1.Length == 65536) ReadAs4(512, 256, 4, raw1);
            if (raw1.Length == 512) ReadAs4(64, 16, 1, raw1);

            lfp.Text = fp;
        }

        private void ReadAs8(int cx, int cy, int DBW, byte[] raw1) {
            // 64KB, 8bpp, 256x256, DBW=2
            // 32KB, 8bpp, 256x128, DBW=1
            // 32KB, 8bpp, 256x128, DBW=4

            //int cx = 256, cy = 256;
            //int DBW = 2;

            {
                byte[] raw2 = Reform32.Encode32(raw1, DBW, raw1.Length / 8192 / DBW);
                byte[] bin = Reform8.Decode8(raw2, cx / 128, cy / 64);
                Bitmap pic = BUt.Make8(cx, cy, bin);
                AddPic(pic, "32to8");
            }
            {
                Bitmap pic = BUt.Make8(cx, cy, raw1);
                AddPic(pic, "flat");
            }
        }


        private void ReadAs4(int cx, int cy, int DBW, byte[] raw1) {
            // 32KB, 4bpp, 256x256, DBW=2
            // 16KB, 4bpp, 128x256, DBW=1

            //int cx = 128, cy = 256;
            //int DBW = 1;

            {
                byte[] raw2 = Reform32.Encode32(raw1, DBW, raw1.Length / 8192 / DBW);
                byte[] bin = Reform4.Decode4(raw2, Math.Max(1, cx / 128), cy / 128);
                Bitmap pic = BUt.Make4(cx, cy, bin);
                AddPic(pic, "32to4");
            }
            {
                Bitmap pic = BUt.Make4(cx, cy, raw1);
                AddPic(pic, "flat");
            }
        }

        class NUt {
            internal static string GetName(PixelFormat pf) {
                switch (pf) {
                    case PixelFormat.Format4bppIndexed: return "4bpp";
                    case PixelFormat.Format8bppIndexed: return "8bpp";
                }
                return "?";
            }
        }

        private void AddPic(Bitmap pic, String ty) {
            String name = String.Format("{0}x{1}x{2} ({3})", pic.Width, pic.Height, NUt.GetName(pic.PixelFormat), ty);
            if (flppic.Controls.IndexOfKey(name) >= 0) return;

            FlowLayoutPanel flp = new FlowLayoutPanel();
            {
                PictureBox pb = new PictureBox();
                pb.Image = pic;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                flp.Controls.Add(pb);
                //flp.SetFlowBreak(pb, true);
            }
            {
                Label la = new Label();
                la.Text = name;
                la.AutoSize = true;
                flp.Controls.Add(la);
            }
            flp.AutoSize = true;
            flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flp.BorderStyle = BorderStyle.FixedSingle;
            flp.FlowDirection = FlowDirection.TopDown;
            flp.Name = name;
            flppic.Controls.Add(flp);
        }

        class BUt {
            public static Bitmap Make8(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                ColorPalette pal = pic.Palette;
                for (int x = 0; x < 256; x++) {
                    pal.Entries[x] = Color.FromArgb(255, x, x, x);
                }
                pic.Palette = pal;

                return pic;
            }

            public static Bitmap Make32(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format32bppArgb);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                return pic;
            }

            public static Bitmap Make4(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                ColorPalette pal = pic.Palette;
                for (int x = 0; x < 16; x++) {
                    int v = Math.Min(255, 16 * x);
                    pal.Entries[x] = Color.FromArgb(255, v, v, v);
                }
                pic.Palette = pal;

                return pic;
            }
        }

        private void VForm_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void VForm_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (alfp == null) return;
            openIt(alfp[0]);
        }
    }
}