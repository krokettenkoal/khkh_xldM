using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace vcBinTex5 {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 4) {
                helpYou();
            }
            int cx = int.Parse(args[2]);
            int cy = int.Parse(args[3]);

            byte[] tbl4 = new byte[16] { 0, 17, 34, 51, 68, 85, 102, 119, 136, 153, 170, 187, 204, 221, 238, 255 };
            byte[] bin = File.ReadAllBytes(args[0]);
            byte[] picbin = new byte[cx * cy * 3];

            for (int ty = 0; ty < cy; ty++) {
                for (int tx = 0; tx < cx; tx++) {
                    int o = tx + cx * ty;
                    int pxby = (o / 2);
                    int pxbi = (o % 2) * 4;
                    byte v = (byte)tbl4[((bin[pxby] >> pxbi) & 15)];

                    int outx = tx;
                    int outy = ty;
                    int offw = cx * 3 * outy + 3 * outx;
                    picbin[offw] = v; offw++;
                    picbin[offw] = v; offw++;
                    picbin[offw] = v; offw++;
                }
            }

            Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format24bppRgb);
            BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            try {
                Trace.Assert(picbin.Length == bd.Stride * bd.Height);
                Marshal.Copy(picbin, 0, bd.Scan0, picbin.Length);
            }
            finally {
                pic.UnlockBits(bd);
            }
            pic.Save(args[1]);
        }

        private static void helpYou() {
            Console.Error.WriteLine("Convert PSMT4HH/PSMT4HL into 4-bpp png image");
            Console.Error.WriteLine("vcBinTex5 <00000149.bin> <149.png> <width: 512> <height: 512>");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("* Technically it only flips lower byte and higher byte.");
            Environment.Exit(1);
        }
    }
}
