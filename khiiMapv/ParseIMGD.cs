using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using vcBinTex4;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using vwBinTex2;
using System.Linq;

namespace khiiMapv {
    public class PicIMGD {
        public Bitmap pic = null;

        public PicIMGD(Bitmap p) {
            this.pic = p;
        }
    }
    public class ParseIMGZ {
        public static PicIMGD[] TakeIMGZ(byte[] bin) {
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);

            List<PicIMGD> al = new List<PicIMGD>();
            si.Position = 0x0C;
            int v0c = br.ReadInt32();
            for (int x = 0; x < v0c; x++) {
                int toff = br.ReadInt32();
                int tlen = br.ReadInt32();
                {
                    MemoryStream six = new MemoryStream(bin, toff, tlen, false);

                    PicIMGD p = ParseIMGD.TakeIMGD(six);
                    al.Add(p);
                }
            }
            return al.ToArray();
        }
    }
    public class ParseIMGD {
        public static PicIMGD TakeIMGD(Stream si) {
            si.Position = 0;
            if (si.ReadByte() != 0x49) throw new NotSupportedException("!IMGD");
            if (si.ReadByte() != 0x4D) throw new NotSupportedException("!IMGD");
            if (si.ReadByte() != 0x47) throw new NotSupportedException("!IMGD");
            if (si.ReadByte() != 0x44) throw new NotSupportedException("!IMGD");
            BinaryReader br = new BinaryReader(si);
            br.ReadInt32();
            int v08 = br.ReadInt32(); // tex off
            int v0c = br.ReadInt32(); // tex len?
            int v10 = br.ReadInt32(); // pal off
            int v14 = br.ReadInt32(); // pal len?
            si.Position = 0x1C;
            int v1c = br.ReadUInt16(); // cx
            int v1e = br.ReadUInt16(); // cy
            si.Position = 0x26;
            int v26 = br.ReadUInt16(); // pscmt4 or 8. 0x13==8bpp, 0x14==4bpp?, 0==32bpp
            si.Position = 0x3C;
            int v3c = br.ReadByte(); // 7=needDecode, 3=rawBMP
            bool needDecode = (v3c == 7);

            si.Position = v08;
            byte[] bTex = br.ReadBytes(v0c);

            si.Position = v10;
            byte[] bPal = br.ReadBytes(v14);

            int cx = v1c, cy = v1e;

            if (v26 == 0x13) {
                int bw1 = v1c / 128;
                int bh1 = v1e / 64;
                int bw2 = bw1;
                int bh2 = bh1;
                byte[] bin = needDecode
                    ? Reform8.Decode8(Reform32.Encode32(bTex, bw1, bh1), bw2, bh2)
                    : bTex;
                Debug.Assert(cx * cy == bin.Length);
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try {
                    Marshal.Copy(bin, 0, bd.Scan0, bin.Length);
                }
                finally {
                    pic.UnlockBits(bd);
                }

                {
                    byte[] palEnc = new byte[8192];
                    Array.Copy(bPal, 0, palEnc, 0, Math.Min(8192, bPal.Length));
                    //byte[] palDec = Reform32.Encode32(palEnc, 1, 1);
                    byte[] palDec = palEnc;

                    ColorPalette CP = pic.Palette;
                    for (int x = 0; x < 256; x++) {
                        int t = x;
                        int toi = KHcv8pal_swap34.repl(x);
                        CP.Entries[toi] = Color.FromArgb(
                            Math.Min(255, palDec[4 * t + 3] * 2),
                            palDec[4 * t + 0],
                            palDec[4 * t + 1],
                            palDec[4 * t + 2]
                            );
                    }
                    pic.Palette = CP;
                }
                return new PicIMGD(pic);
            }
            else if (v26 == 0x14) {
                int bw1 = v1c / 128;
                int bh1 = v1e / 128;
                int bw2 = bw1;
                int bh2 = bh1;
                byte[] bin = needDecode
                    ? Reform4.Decode4(Reform32.Encode32(bTex, bw1, bh1), bw2, bh2)
                    : HLUt.Swap(bTex);
                Debug.Assert(cx * cy / 2 == bin.Length);
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
                try {
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bd.Stride * bd.Height, bin.Length));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                {
                    byte[] palEnc = new byte[8192];
                    Array.Copy(bPal, 0, palEnc, 0, Math.Min(8192, bPal.Length));
                    //byte[] palDec = Reform32.Encode32(palEnc, 1, 1);
                    byte[] palDec = palEnc;

                    ColorPalette CP = pic.Palette;
                    for (int x = 0; x < 16; x++) {
                        int t = x;
                        int toi = x;// KHcv8pal_swap34.repl(x);
                        CP.Entries[toi] = Color.FromArgb(
                            Math.Min(255, palDec[4 * t + 3] * 2),
                            palDec[4 * t + 0],
                            palDec[4 * t + 1],
                            palDec[4 * t + 2]
                            );
                    }
                    pic.Palette = CP;
                }

                return new PicIMGD(pic);
            }
            else if (v26 == 0x00) {
                byte[] bin = (bTex);
                // Windows: BB GG RR AA
                //  PS2 GS: RR GG BB AA
                for (int x = 0; x < bin.Length; x += 4) {
                    var swap = bin[x];
                    bin[x] = bin[x + 2];
                    bin[x + 2] = swap;
                    bin[x + 3] = PalAlpha.x2[bin[x + 3]];
                }

                Debug.Assert(cx * cy * 4 == bin.Length);
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format32bppArgb);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                try {
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bd.Stride * bd.Height, bin.Length));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                return new PicIMGD(pic);
            }
            else throw new NotSupportedException("v26 = " + v26.ToString("X"));
        }

        class PalAlpha {
            public static byte[] x2 = Enumerable.Range(0, 256)
                .Select(it => (byte)Math.Min(255, it * 2))
                .ToArray();
        }

        class HLUt {
            public static byte[] Swap(byte[] bin) {
                int cx = bin.Length;
                byte[] pic = new byte[cx];
                for (int x = 0; x < cx; x++) {
                    byte b = bin[x];
                    pic[x] = (byte)((b >> 4) | (b << 4));
                }
                return pic;
            }
        }
    }
}
