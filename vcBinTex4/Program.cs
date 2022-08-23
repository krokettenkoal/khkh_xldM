using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace vcBinTex4 {
    class Program {
        static void helpYou() {
            Console.Error.WriteLine("Encode bin to PSCMT32, then decode from PSMT4HL/PSMT8 to png image");
            Console.Error.WriteLine("vcBinTex4 <00000937.bin> <937.png> <bpp: 4> <bw: 4> <bh: 4> <bw2: 4> <bh2: 4>");
            Environment.Exit(1);
        }

        static void Main(string[] args) {
            if (args.Length < 7) {
                helpYou();
            }
            int bpp = int.Parse(args[2]);
            int bw1 = int.Parse(args[3]);
            int bh1 = int.Parse(args[4]);
            int bw2 = int.Parse(args[5]);
            int bh2 = int.Parse(args[6]);
            int cx = 0; int cy = 0;
            if (bpp == 4) { cx = 128 * bw2; cy = 128 * bh2; }
            if (bpp == 8) { cx = 128 * bw2; cy = 64 * bh2; }

            byte[] tbl4 = new byte[16] { 0, 17, 34, 51, 68, 85, 102, 119, 136, 153, 170, 187, 204, 221, 238, 255 };
            byte[] bin = null;
            byte[] picbin = new byte[cx * cy * 3];

            if (bpp == 4) {
                bin = Reform4.Decode4(Reform32.Encode32(File.ReadAllBytes(args[0]), bw1, bh1), bw2, bh2);
                for (int y = 0; y < cy; y++) {
                    for (int x = 0; x < cx; x++) {
                        int off = cx * y + x;
                        byte v = tbl4[(0 == (off & 1)) ? (byte)(bin[off / 2] >> 4) : (byte)(bin[off / 2] & 15)];
                        int offw = 3 * (cx * y + x);
                        picbin[offw] = v; offw++;
                        picbin[offw] = v; offw++;
                        picbin[offw] = v; offw++;
                    }
                }
            }
            else if (bpp == 8) {
                bin = Reform8.Decode8(Reform32.Encode32(File.ReadAllBytes(args[0]), bw1, bh1), bw2, bh2);
                for (int y = 0; y < cy; y++) {
                    for (int x = 0; x < cx; x++) {
                        int off = cx * y + x;
                        byte v = bin[off];
                        int offw = 3 * (cx * y + x);
                        picbin[offw] = v; offw++;
                        picbin[offw] = v; offw++;
                        picbin[offw] = v; offw++;
                    }
                }
            }
            else throw new NotSupportedException("Unsupported bpp -> " + bpp);

            Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format24bppRgb);
            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, pic.PixelFormat);
            try {
                Trace.Assert(bd.Stride * bd.Height == picbin.Length);
                Marshal.Copy(picbin, 0, bd.Scan0, picbin.Length);
            }
            finally {
                pic.UnlockBits(bd);
            }
            pic.Save(args[1]);
        }

    }
}
