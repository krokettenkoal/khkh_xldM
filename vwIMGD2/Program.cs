using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using khkh_xldMii;
using System.Diagnostics;
using vcBinTex4;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace vwIMGD2 {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("vwIMGD2 <folder or files>");
                Environment.Exit(1);
            }
            Queue<String> alfp = new Queue<string>();
            foreach (string fp in args) alfp.Enqueue(fp);
            while (alfp.Count != 0) {
                String fp = alfp.Dequeue();
                if (0 != (File.GetAttributes(fp) & FileAttributes.Directory)) {
                    foreach (string fp1 in Directory.GetFiles(fp)) alfp.Enqueue(fp1);
                    foreach (string fp1 in Directory.GetDirectories(fp)) alfp.Enqueue(fp1);
                }
                else {
                    using (FileStream fs = File.OpenRead(fp)) {
                        try {
                            new Program(fp).Take(ReadBar.Explode(fs), Path.GetFileName(fp));
                        }
                        catch (NotSupportedException) {

                        }
                    }
                }
            }
        }

        String fp;

        public Program(String fp) {
            this.fp = fp;
        }

        void Take(ReadBar.Barent[] ents, String cid) {
            foreach (ReadBar.Barent ent in ents) {
                byte[] b = ent.bin;
                if (16 <= ent.len && b[0] == 0x42 && b[1] == 0x41 && b[2] == 0x52 && b[3] == 1) {
                    // BAR1
                    Stream si = new MemoryStream(b, false);
                    Take(ReadBar.Explode(si), cid + "#" + ent.id);
                }
                else if (16 <= ent.len && b[0] == 0x49 && b[1] == 0x4D && b[2] == 0x47 && b[3] == 0x44) {
                    // IMGD
                    TakeIMGD(new MemoryStream(ent.bin, false), cid + "#" + ent.id);
                }
                else if (16 <= ent.len && b[0] == 0x49 && b[1] == 0x4D && b[2] == 0x47 && b[3] == 0x5A) {
                    // IMGZ
                    MemoryStream si = new MemoryStream(b, false);
                    BinaryReader br = new BinaryReader(si);
                    si.Position = 0x0C;
                    int v0c = br.ReadInt32();
                    for (int x = 0; x < v0c; x++) {
                        int toff = br.ReadInt32();
                        int tlen = br.ReadInt32();
                        TakeIMGD(new MemoryStream(b, toff, tlen, false), cid + "#" + ent.id + "#" + x);
                    }
                }
            }
        }

        void TakeIMGD(MemoryStream si, String cid) {
            si.Position = 0;
            Trace.Assert(si.ReadByte() == 0x49);
            Trace.Assert(si.ReadByte() == 0x4D);
            Trace.Assert(si.ReadByte() == 0x47);
            Trace.Assert(si.ReadByte() == 0x44);
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
            int v26 = br.ReadUInt16(); // pscmt4 or 8. 0x13==8bpp, 0x14==4bpp?

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
                byte[] bin = Reform8.Decode8(Reform32.Encode32(bTex, bw1, bh1), bw2, bh2);
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
                        int toi = vwBinTex2.KHcv8pal.repl(x);
                        CP.Entries[t] = Color.FromArgb(
                            Math.Min(255, palDec[4 * t + 3] * 2),
                            palDec[4 * t + 0],
                            palDec[4 * t + 1],
                            palDec[4 * t + 2]
                            );
                    }
                    pic.Palette = CP;
                }
                pic.Save(cid + ".png");
            }
            else if (v26 == 0x14) {
                int bw1 = v1c / 128;
                int bh1 = v1e / 128;
                int bw2 = bw1;
                int bh2 = bh1;
                byte[] bin = Reform4.Decode4(Reform32.Encode32(bTex, bw1, bh1), bw2, bh2);
                Debug.Assert(cx * cy / 2 == bin.Length);
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
                try {
                    Marshal.Copy(bin, 0, bd.Scan0, bin.Length);
                }
                finally {
                    pic.UnlockBits(bd);
                }
                ColorPalette CP = pic.Palette;
                for (int x = 0; x < 16; x++) CP.Entries[x] = Color.FromArgb(17 * x, 17 * x, 17 * x);
                pic.Palette = CP;
                pic.Save(cid + ".png");
            }
            else throw new NotSupportedException("v26 = " + v26.ToString("X"));
        }
    }
}
