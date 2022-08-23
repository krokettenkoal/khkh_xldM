using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StudySwizzle.Properties;
using vcBinTex4;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace StudySwizzle {
    public partial class StudyForm : Form {
        public StudyForm() {
            InitializeComponent();
        }

        byte[] gsram = new byte[4 * 1024 * 1024];

        private void MForm_Load(object sender, EventArgs e) {
            Test8Ex();
            Test4Ex();
        }

        private void Test4Ex() {
            Bitmap picIn = Resources._1024x768x4_Sample;

            int picw = picIn.Width, pich = picIn.Height;

            if (picIn.PixelFormat != PixelFormat.Format4bppIndexed) throw new NotSupportedException("picIn.PixelFormat != PixelFormat.Format4bppIndexed");

            byte[] picEnc1 = Reform4.Encode4(PBUt.GetBits(picIn), picw / 128, pich / 128);
            byte[] picEnc2 = Reform32.Decode32(picEnc1, picw / 128, pich / 128);

            byte[] palEnc = Reform32.Decode32(PBUt.GetPalBytes(picIn, 16 * 0), 1, 1);

            File.WriteAllBytes(String.Format("Pic_{0}x{1}x4_Swizzled_Sample.bin", picw, pich), picEnc2);
            File.WriteAllBytes(String.Format("Pal_{0}x{1}x4_Swizzled_Sample.bin", picw, pich), palEnc);

            byte[] picDec1 = Reform32.Encode32(picEnc2, picw / 128, pich / 128);
            byte[] picDec2 = Reform4.Decode4(picDec1, picw / 128, pich / 128);

            byte[] palDec = Reform32.Encode32(palEnc, 1, 1);

            File.WriteAllBytes(String.Format("Pic_{0}x{1}x4_Unswizzled.bin", picw, pich), picDec2);
            File.WriteAllBytes(String.Format("Pal_{0}x{1}x4_Unswizzled.bin", picw, pich), palDec);

            {
                Bitmap p = BUt.Make4(picw, pich, picDec2);
                ColorPalette pal = p.Palette;
                for (int x = 0; x < 256; x++) {
                    int t = KHcv8pal.repl(x) - 16 * 0; // 4th palette
                    if (t >= 0 && t < 16)
                        pal.Entries[t] = Color.FromArgb(
                            AcUt.GetA(palDec[4 * x + 3]),
                            palDec[4 * x + 0],
                            palDec[4 * x + 1],
                            palDec[4 * x + 2]
                            );
                }
                p.Palette = pal;
                pb4.Image = p;
            }
        }

        private void Test4Org() {
            int outcx = 256, outcy = 256;

            byte[] picEnc = Reform32.Encode32(Resources._256x256x4_Swizzled, 2, 2);

            byte[] picDec = Reform4.Decode4(picEnc, 2, 2);

            byte[] palDec = Reform32.Encode32(Resources.Pal16_Swizzled, 1, 1);

            {
                Bitmap p = BUt.Make4(outcx, outcy, picDec);
                ColorPalette pal = p.Palette;
                for (int x = 0; x < 256; x++) {
                    int t = KHcv8pal.repl(x) - 16 * 3; // 4th palette
                    if (t >= 0 && t < 16)
                        pal.Entries[t] = Color.FromArgb(
                            AcUt.GetA(palDec[4 * x + 3]),
                            palDec[4 * x + 0],
                            palDec[4 * x + 1],
                            palDec[4 * x + 2]
                            );
                }
                p.Palette = pal;
                pb4.Image = p;
            }
        }

        void Test8Ex() {
            Bitmap picIn = Resources._1024x768x8_Sample;

            int picw = picIn.Width, pich = picIn.Height;

            if (picIn.PixelFormat != PixelFormat.Format8bppIndexed) throw new NotSupportedException("picIn.PixelFormat != PixelFormat.Format8bppIndexed");

            byte[] picEnc1 = Reform8.Encode8(PBUt.GetBits(picIn), picw / 128, pich / 64);
            byte[] picEnc2 = Reform32.Decode32(picEnc1, picw / 128, pich / 64);

            byte[] palEnc = Reform32.Decode32(PBUt.GetPalBytes(picIn, 16 * 0), 1, 1);

            File.WriteAllBytes(String.Format("Pic_{0}x{1}x8_Swizzled_Sample.bin", picw, pich), picEnc2);
            File.WriteAllBytes(String.Format("Pal_{0}x{1}x8_Swizzled_Sample.bin", picw, pich), palEnc);

            byte[] picDec1 = Reform32.Encode32(picEnc2, picw / 128, pich / 64);
            byte[] picDec2 = Reform8.Decode8(picDec1, picw / 128, pich / 64);

            byte[] palDec = Reform32.Encode32(palEnc, 1, 1);

            File.WriteAllBytes(String.Format("Pic_{0}x{1}x8_Unswizzled.bin", picw, pich), picDec2);
            File.WriteAllBytes(String.Format("Pal_{0}x{1}x8_Unswizzled.bin", picw, pich), palDec);

            {
                Bitmap p = BUt.Make8(picw, pich, picDec2);
                ColorPalette pal = p.Palette;
                for (int x = 0; x < 256; x++) {
                    int t = KHcv8pal.repl(x);
                    pal.Entries[t] = Color.FromArgb(
                        AcUt.GetA(palDec[4 * x + 3]),
                        palDec[4 * x + 0],
                        palDec[4 * x + 1],
                        palDec[4 * x + 2]
                        );
                }
                p.Palette = pal;
                pb8.Image = p;
            }
        }

        void Test8Org() {
            int outcx = 256, outcy = 256;

            byte[] picEnc = Reform32.Encode32(Resources._256x256x8_Swizzled, 2, 4);

            byte[] picDec = Reform8.Decode8(picEnc, 2, 4);

            byte[] palDec = Reform32.Encode32(Resources.Pal256_Swizzled, 1, 1);

            {
                Bitmap p = BUt.Make8(outcx, outcy, picDec);
                ColorPalette pal = p.Palette;
                for (int x = 0; x < 256; x++) {
                    int t = KHcv8pal.repl(x);
                    pal.Entries[t] = Color.FromArgb(
                        AcUt.GetA(palDec[4 * x + 3]),
                        palDec[4 * x + 0],
                        palDec[4 * x + 1],
                        palDec[4 * x + 2]
                        );
                }
                p.Palette = pal;
                pb8.Image = p;
            }
        }

        class AcUt {
            public static byte GetA(byte a) {
                return (byte)Math.Min(a * 255 / 0x80, 255);
            }
        }

        class AeUt {
            public static byte GetA(byte a) {
                return (byte)Math.Min(a * 0x80 / 255, 0x80);
            }
        }

        class PBUt {
            public static byte[] GetBits(Bitmap pic) {
                BitmapData bd = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.ReadOnly, pic.PixelFormat);
                try {
                    byte[] bin = new byte[bd.Stride * bd.Height];
                    Marshal.Copy(bd.Scan0, bin, 0, bin.Length);
                    return bin;
                }
                finally {
                    pic.UnlockBits(bd);
                }
            }

            public static byte[] GetPalBytes(Bitmap pic, int startIndex) {
                byte[] bin = new byte[8192];
                ColorPalette pal = pic.Palette;
                Color[] ents = pal.Entries;
                int cx = ents.Length;
                for (int x = 0; x < 256; x++) {
                    int t = KHcv8pal.repl(x);
                    int srci = t - startIndex;
                    int dsti = x;
                    if (srci >= 0 && srci < cx) {
                        bin[4 * dsti + 3] = AeUt.GetA(ents[srci].A);
                        bin[4 * dsti + 0] = ents[srci].R;
                        bin[4 * dsti + 1] = ents[srci].G;
                        bin[4 * dsti + 2] = ents[srci].B;
                    }
                }
                return bin;
            }
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

                return pic;
            }
        }

        class KHcv8pal {
           public static int repl(int t) {
               int srcIndex = t;
               int dstIndex = (srcIndex & (1 | 128))
                   | (((srcIndex & 2) != 0) ? 8 : 0)
                   | (((srcIndex & 4) != 0) ? 2 : 0)
                   | (((srcIndex & 8) != 0) ? 4 : 0)
                   | (((srcIndex & 16) != 0) ? 32 : 0)
                   | (((srcIndex & 32) != 0) ? 64 : 0)
                   | (((srcIndex & 64) != 0) ? 16 : 0)
               ;
               return dstIndex;
           }
        }
    }
}