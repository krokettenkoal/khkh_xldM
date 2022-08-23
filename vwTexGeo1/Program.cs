using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace vwTexGeo1 {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpYou();
            }
            using (FileStream fs = File.OpenRead(args[0])) {
                GS gs = new GS();
                BinaryReader br = new BinaryReader(fs);
                while (fs.Position < fs.Length) {
                    uint id = br.ReadUInt32();
                    uint size = br.ReadUInt32();
                    byte[] bin = br.ReadBytes(Convert.ToInt32(size));

                    if (size > 0x3FFF) continue;

                    Console.WriteLine(string.Format("{0,3} {1,6}", id, size));
                    switch (id) {
                        case 1:
                        case 2:
                        case 3: {
                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader brint = new BinaryReader(si);
                                while (si.Position < si.Length) {
                                    byte[] pack = brint.ReadBytes(16);
                                    Trace.Assert(pack.Length == 16);
                                    gs.Read(pack);
                                }
                                break;
                            }
                    }
                }
                fs.Close();
                gs.pic.Save("output.png", ImageFormat.Png);
            }
        }

        class GS {
            int st = 0;
            int curp = 0, cntp = 0;
            int prim = 0, nreg = 0;

            byte[] regs = new byte[16];
            Color curclr = Color.Black;
            bool isprim = false;
            float curX = 0, curY = 0;
            List<Prim> alprim = new List<Prim>();

            public Bitmap pic;
            Graphics cv;

            class Prim {
                public PointF pt = PointF.Empty;
                public Color clr = Color.Empty;
            }

            public GS() {
                pic = new Bitmap(2048, 2048, PixelFormat.Format16bppRgb565);
                cv = Graphics.FromImage(pic);
            }

            internal void Read(byte[] bin) {
                if (st == 0) {
                    MemoryStream si = new MemoryStream(bin, false);
                    BinaryReader br = new BinaryReader(si);

                    UInt64 v0 = br.ReadUInt64();
                    int nloop = ((int)(v0 >> 0)) & 0x7FFF;
                    int eop = ((int)(v0 >> 15)) & 1;
                    int pre = ((int)(v0 >> 46)) & 1;
                    this.prim = ((int)(v0 >> 47)) & 0x7FF;
                    int flg = ((int)(v0 >> 58)) & 3;
                    this.nreg = ((int)(v0 >> 60)) & 15;
                    this.isprim = (pre != 0);

                    alprim.Clear();

                    for (int x = 0; x < 8; x++) {
                        byte v1 = br.ReadByte();
                        regs[2 * x + 0] = (byte)((v1) & 15);
                        regs[2 * x + 1] = (byte)((v1 >> 4) & 15);
                    }

                    switch (flg) {
                        case 0:
                            curp = 0; cntp = nloop * nreg;
                            if (cntp != 0) st = 1;
                            break;
                        case 1:
                            curp = 0; cntp = nloop * nreg;
                            if (cntp != 0) st = 2;
                            break;
                        case 2:
                            curp = 0; cntp = nloop;
                            if (cntp != 0) st = 3;
                            break;
                        default:
                            throw new NotSupportedException("FLG#" + flg + " isn't supported");
                    }
                }
                else if (st == 1) {
                    MemoryStream si = new MemoryStream(bin, false);
                    BinaryReader br = new BinaryReader(si);

                    byte reg = regs[curp % nreg];
                    switch (reg) {
                        case 1: //RGBAQ
                            curclr = Color.FromArgb(bin[12], bin[0], bin[4], bin[8]);
                            break;
                        case 2: //ST
                            break;
                        case 3: //UV
                            break;
                        case 4: //XYZF2
                            {
                                int vX = br.ReadUInt16();
                                br.ReadUInt16();
                                int vY = br.ReadUInt16();
                                curX = FPUtil.FP16f4(vX);
                                curY = FPUtil.FP16f4(vY);
                                kick();
                                break;
                            }
                        case 6: //TEX0_1
                            break;
                        case 8: //CLAMP_1
                            break;
                        case 14: //A+D
                            break;
                        case 7: // TEX0_2
                            break;
                        case 5: // XYZ2
                            {
                                int vX = br.ReadUInt16();
                                br.ReadUInt16();
                                int vY = br.ReadUInt16();
                                curX = FPUtil.FP16f4(vX);
                                curY = FPUtil.FP16f4(vY);
                                kick();
                                break;
                            }
                        default:
                            throw new NotSupportedException("REG#" + reg + " in PACKEDmode isn't supported");
                    }
                    curp++;
                    if (curp == cntp) { st = 0; }
                }
                else if (st == 2) {
                    for (int w = 0; w < 2; w++) {
                        MemoryStream si = new MemoryStream(bin, w * 8, 8, false);
                        BinaryReader br = new BinaryReader(si);
                        byte reg = regs[curp % nreg];
                        switch (reg) {
                            case 0: // PRIM
                                {
                                    this.prim = br.ReadUInt16() & 0x7FF;
                                    this.isprim = true;
                                    break;
                                }
                            case 5: // XYZ2
                                {
                                    int vX = br.ReadUInt16();
                                    int vY = br.ReadUInt16();
                                    curX = FPUtil.F12p4(vX);
                                    curY = FPUtil.F12p4(vY);
                                    kick();
                                    break;
                                }
                            case 4: // XYZF2
                                {
                                    int vX = br.ReadUInt16();
                                    int vY = br.ReadUInt16();
                                    curX = FPUtil.F12p4(vX);
                                    curY = FPUtil.F12p4(vY);
                                    kick();
                                    break;
                                }
                            case 12: // XYZF3
                                {
                                    int vX = br.ReadUInt16();
                                    int vY = br.ReadUInt16();
                                    curX = FPUtil.F12p4(vX);
                                    curY = FPUtil.F12p4(vY);
                                    break;
                                }
                            case 13:
                            case 255:
                                throw new NotSupportedException("REG#" + reg + " in REGmode isn't supported");
                        }

                        curp++;
                        if (curp == cntp) { st = 0; break; }
                    }
                }
                else if (st == 3) {
                    curp++;
                    if (curp == cntp) { st = 0; }
                }
            }

            private void kick() {
                Prim p = new Prim();
                p.clr = curclr;
                p.pt = new PointF(curX, curY);
                alprim.Add(p);

                int pt = (prim & 7);
                switch (pt) {
                    case 4: // Tri strip
                        if (alprim.Count == 3) {
                            Prim p0 = alprim[0];
                            Prim p1 = alprim[1];
                            Prim p2 = alprim[2];
                            alprim.RemoveAt(0);
                            PointF[] pts = new PointF[] { p0.pt, p1.pt, p2.pt };
                            cv.DrawPolygon(Pens.Yellow, pts);
                        }
                        break;
                    case 3: // Tri
                        if (alprim.Count == 3) {
                            Prim p0 = alprim[0];
                            Prim p1 = alprim[1];
                            Prim p2 = alprim[2];
                            alprim.RemoveRange(0, 3);
                            Pen pen = new Pen(Color.FromArgb(200, Color.Red));
                            PointF[] pts = new PointF[] { p0.pt, p1.pt, p2.pt };
                            cv.DrawPolygon(pen, pts);
                        }
                        break;
                    case 6: // Sprite
                        if (alprim.Count == 2) {
                            Prim p0 = alprim[0];
                            Prim p1 = alprim[1];
                            alprim.RemoveRange(0, 2);
                            Brush br = new SolidBrush(Color.FromArgb(100, Color.Wheat));
                            RectangleF rc = RCUtil.From2(p0.pt, p1.pt);
                            cv.FillRectangle(br, rc);
                        }
                        break;
                    case 2: // Line strip
#if false
                        while (alprim.Count >= 2) {
                            Prim p0 = alprim[0];
                            Prim p1 = alprim[1];
                            alprim.RemoveAt(0);
                            Pen pen = new Pen(Color.FromArgb(200, Color.Red));
                            cv.DrawLine(pen, p0.pt, p1.pt);
                        }
#endif
                        alprim.Clear();
                        break;
                    default:
                        throw new NotSupportedException("Primitive type#" + pt + " isn't supported");
                }
            }
        }

        class RCUtil {
            public static RectangleF From2(PointF p1, PointF p2) {
                return RectangleF.FromLTRB(
                    Math.Min(p1.X, p2.X),
                    Math.Min(p1.Y, p2.Y),
                    Math.Max(p1.X, p2.X),
                    Math.Max(p1.Y, p2.Y)
                    );
            }
        }

        class FPUtil {
            public static float FP16f4(int val) {
#if true
                return ((val >> 0) & 0x7FF);
#else
                int s = (val >> 15) & 1;
                int f = (val >> 10) & 0xF;
                int v = (val & 0x7FF);
                float r = 1.0f;
                if (0 != (v & 0x400)) r += 0.5f;
                if (0 != (v & 0x200)) r += 0.25f;
                if (0 != (v & 0x100)) r += 0.125f;
                if (0 != (v & 0x080)) r += 0.0625f;
                if (0 != (v & 0x040)) r += 0.03125f;
                if (0 != (v & 0x020)) r += 0.015625f;
                if (0 != (v & 0x010)) r += 0.0078125f;
                if (0 != (v & 0x008)) r += 0.00390625f;
                if (0 != (v & 0x004)) r += 0.001953125f;
                if (0 != (v & 0x002)) r += 0.0009765625f;
                if (0 != (v & 0x001)) r += 0.00048828125f;
                switch (f) {
                    case 15: r *= 256; break;
                    case 14: r *= 128; break;
                    case 13: r *= 64; break;
                    case 12: r *= 32; break;
                    case 11: r *= 16; break;
                    case 10: r *= 8; break;
                    case 9: r *= 4; break;
                    case 8: r *= 2; break;
                    case 7: r *= 1; break;
                    case 6: r /= 2; break;
                    case 5: r /= 4; break;
                    case 4: r /= 8; break;
                    case 3: r /= 16; break;
                    case 2: r /= 32; break;
                    case 1: r /= 64; break;
                    case 0: r = 0; break;
                }
                if (s != 0) r = -r;
                return r;
#endif
            }

            internal static float F12p4(int val) {
                return (val >> 4) & 0xFFF;
            }
        }

        private static void helpYou() {
            Console.WriteLine("vwTexGeo <GSpassthru.bin>");
            Environment.Exit(1);
        }
    }
}
