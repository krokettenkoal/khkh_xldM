using khiiMapv.Extensions;
using khiiMapv.Models.HexView;
using khiiMapv.Models.PAX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using vwBinTex2;

namespace khiiMapv.Models.PAX {
    public class ParseDPD {
        public List<DpdPic> dpdPicList = new List<DpdPic>();
        public int dpdOffset;
        public MicroLabels labels = new MicroLabels();

        public List<D1> d1List = new List<D1>();
        public List<D3> d3List = new List<D3>();

        public ParseDPD(ArraySegment<byte> bytes) {
            var si = bytes.ToMemoryStream();
            var br = new BinaryReader(si);

            dpdOffset = bytes.Offset;

            si.Position = 0;
            int test96 = br.ReadInt32();
            if (test96 != 0x96) throw new NotSupportedException("!96");

            labels.AddLabel("dpd", dpdOffset);
            labels.AddLabel(".num1", dpdOffset + Convert.ToInt32(si.Position));

            int num1 = br.ReadInt32();
            List<int> ofs1List = new List<int>(); // dmac tag off?
            for (int t = 0; t < num1; t++) ofs1List.Add(br.ReadInt32());

            labels.AddLabel(".num2", dpdOffset + Convert.ToInt32(si.Position));

            int num2 = br.ReadInt32();
            List<int> ofs2List = new List<int>(); // tex off?
            for (int t = 0; t < num2; t++) ofs2List.Add(br.ReadInt32());

            labels.AddLabel(".num3", dpdOffset + Convert.ToInt32(si.Position));

            int num3 = br.ReadInt32();
            List<int> ofs3List = new List<int>(); // ?
            for (int t = 0; t < num3; t++) ofs3List.Add(br.ReadInt32());

            labels.AddLabel(".num4", dpdOffset + Convert.ToInt32(si.Position));

            int num4 = br.ReadInt32();
            List<int> ofs4List = new List<int>(); // ?
            for (int t = 0; t < num4; t++) ofs4List.Add(br.ReadInt32());

            labels.AddLabel(".num5", dpdOffset + Convert.ToInt32(si.Position));

            int num5 = br.ReadInt32();
            List<int> ofs5List = new List<int>(); // ?
            for (int t = 0; t < num5; t++) ofs5List.Add(br.ReadInt32());

            for (int t = 0; t < ofs1List.Count; t++) {
                int ofs1 = ofs1List[t];
                si.Position = ofs1;

                labels.AddLabel($"D1[{t}]", dpdOffset + Convert.ToInt32(si.Position));

                // P11 176 bytes
                d1List.Add(new D1(br));
            }

            for (int t = 0; t < ofs2List.Count; t++) {
                int ofs2 = ofs2List[t];
                si.Position = ofs2;

                labels.AddLabel($"D2[{t}]", dpdOffset + Convert.ToInt32(si.Position));

                br.ReadInt32(); // @0
                br.ReadInt16(); // @4
                int fmt = br.ReadInt16(); // @6
                br.ReadInt32(); // @8
                int tcx = br.ReadInt16(); // @12
                int tcy = br.ReadInt16(); // @14

                var subLabels = new MicroLabels();
                subLabels.AddLabel($"D2[{t}]", dpdOffset + ofs2);
                subLabels.AddLabel(".Fmt", dpdOffset + ofs2 + 6);
                subLabels.AddLabel(".Width", dpdOffset + ofs2 + 12);
                subLabels.AddLabel(".Height", dpdOffset + ofs2 + 14);

                if (fmt == 0x13) {
                    subLabels.AddLabel(".Bitmap", dpdOffset + ofs2 + 32);
                    subLabels.AddLabel(".Palette", dpdOffset + ofs2 + 32 + tcx * tcy);

                    si.Position = ofs2 + 32;
                    byte[] pic = br.ReadBytes(tcx * tcy);
                    byte[] pal = br.ReadBytes(1024);
                    dpdPicList.Add(
                        new DpdPic {
                            index2 = t,
                            bitmap = BUt.Make8(pic, pal, tcx, tcy),
                            offset = dpdOffset + ofs2 + 32,
                            labels = subLabels,
                        }
                    );
                }
            }

            for (int t = 0; t < ofs3List.Count; t++) {
                int ofs3 = ofs3List[t];
                si.Position = ofs3;

                labels.AddLabel($"D3[{t}]", dpdOffset + ofs3);

                D3 d3 = Xe.BinaryMapper.BinaryMapping.ReadObject<D3>(br);
                d3List.Add(d3);

                Debug.Assert(d3.cnt1 == d3.cnt2);

                si.Position = ofs3 + 0x20;

                d3.d301List.AddRange(
                    Enumerable.Range(0, d3.cnt1)
                        .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<D301>(br))
                        .ToArray()
                );

                for (int s = 0; s < d3.d301List.Count; s++) {
                    si.Position = ofs3 + 0x10 + d3.d301List[s].Ofs;
                    var d302 = Xe.BinaryMapper.BinaryMapping.ReadObject<D302>(br);
                    d302.PacketData = br.ReadBytes(d302.PacketLength);

                    d3.d301List[s].D302List.Add(d302);
                }
            }

            for (int t = 0; t < ofs4List.Count; t++) {
                int ofs4 = ofs4List[t];
                si.Position = ofs4;

                labels.AddLabel($"D4[{t}]", dpdOffset + Convert.ToInt32(si.Position));
            }

            for (int t = 0; t < ofs5List.Count; t++) {
                int ofs5 = ofs5List[t];
                si.Position = ofs5;

                labels.AddLabel($"D5[{t}]", dpdOffset + Convert.ToInt32(si.Position));
            }

        }

        class BUt {
            public static Bitmap Make8(byte[] pic, byte[] pal, int cx, int cy) {
                Bitmap p = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                BitmapData bd = p.LockBits(new Rectangle(0, 0, p.Width, p.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try {
                    Marshal.Copy(pic, 0, bd.Scan0, Math.Min(bd.Stride * bd.Height, pic.Length));
                }
                finally {
                    p.UnlockBits(bd);
                }

                ColorPalette cp = p.Palette;
                for (int x = 0; x < 256; x++) {
                    int t = x;
                    int toi = KHcv8pal_swap34.repl(x);
                    cp.Entries[toi] = Color.FromArgb(
                        Math.Min(255, pal[4 * t + 3] * 2),
                        pal[4 * t + 0],
                        pal[4 * t + 1],
                        pal[4 * t + 2]
                        );
                }
                p.Palette = cp;
                //p.Save("prize.png", ImageFormat.Png);
                //File.WriteAllBytes("prize.bin", pic);
                //File.WriteAllBytes("prize.pal", pal);

                return p;
            }
        }
    }
}
