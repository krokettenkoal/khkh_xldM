using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace khiiMapv {
    public class ParseRADA {
        Stream si;
        BinaryReader br;

        public ParseRADA(Stream si) {
            this.br = new BinaryReader(this.si = si);
        }

        public void Parse() {
            BinaryReader br = new BinaryReader(si);

            si.Position = 4; int v04 = br.ReadUInt16();
            if (v04 != 4) throw new NotSupportedException("@04 != 4");
            si.Position = 0x24; int cx = br.ReadUInt16();
            si.Position = 0x26; int cy = br.ReadUInt16();

            si.Position = 0x40;
            byte[] raw1 = br.ReadBytes(cx * cy / 2);
            byte[] pal = br.ReadBytes(4 * 16);

            pic = BUt.Make4(cx, cy, raw1, pal);
        }

        public Bitmap pic = null;

        class BUt {
            public static Bitmap Make4(int cx, int cy, byte[] bin, byte[] palb) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

                for (int x = 0; x < bin.Length; x++) {
                    byte b = bin[x];
                    b = (byte)((b << 4) | (b >> 4));
                    bin[x] = b;
                }

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                ColorPalette pal = pic.Palette;
                for (int x = 0; x < 16; x++) {
                    int b = 4 * x;
                    pal.Entries[x] = Color.FromArgb(
                        Math.Min(255, 2 * palb[b + 3]),
                        palb[b + 0],
                        palb[b + 1],
                        palb[b + 2]
                        );
                }
                pic.Palette = pal;

                return pic;
            }
        }

    }
}
