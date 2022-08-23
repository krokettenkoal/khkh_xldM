using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CustVPU1;
using System.Diagnostics;

namespace Prayvif1 {
    public class GSim : IGSim {
        TextWriter wr;
        Outxml ox;

        public GSim(TextWriter wr, GSWr wrgs, Outxml ox) {
            this.wr = wr;
            this.p1 = new GIFt(wrgs, ox);
            this.ox = ox;
        }

        /// <summary>
        /// From VPU1
        /// </summary>
        /// <param name="bin"></param>
        public void Transfer1(byte[] bin) {
            wr.WriteLine("  * Transfer1: bytes " + bin.Length);
            transfer(p1, bin);
        }

        /// <summary>
        /// From VIF1
        /// </summary>
        /// <param name="bin"></param>
        public void Transfer2(byte[] bin) {
            wr.WriteLine(" * Transfer2: bytes " + bin.Length);
            transfer(p1, bin);
        }

        GIFt p1;

        class GIFt {
            public UInt64 w0;
            public UInt64 w8;
            public int nloop;
            public bool eop;
            public bool pre;
            public int prim;
            public byte flg;
            public byte nreg;
            public byte[] regs;

            public int state = 0;
            public int pos = 0, posMax = 0;
            public MemoryStream siso = new MemoryStream();

            string[] RegDesc = "PRIM,RGBAQ,ST,UV,XYZF2,XYZ2,TEX0_1,TEX0_2,CLAMP_1,CLAMP_2,FOG,-,XYZF3,XYZ3,A+D,nop".Split(',');
            int cntc = 0;

            MemoryStream osa = new MemoryStream(8);
            BinaryReader bra;
            BinaryWriter wra;

            GSWr wrgs;
            Outxml ox;

            public GIFt(GSWr wrgs, Outxml ox) {
                bra = new BinaryReader(osa);
                wra = new BinaryWriter(osa);
                this.wrgs = wrgs;
                this.ox = ox;
            }

            public void transfer(byte[] bin) {
                BinaryReader br = new BinaryReader(new MemoryStream(bin, false));
                for (int rest = bin.Length / 16; rest != 0; ) {
                    if (state == 0) {
                        w0 = br.ReadUInt64();
                        w8 = br.ReadUInt64();
                        nloop = ((int)(w0)) & 0x7FFF;
                        eop = (((byte)(w0 >> 15)) & 1) != 0;
                        pre = (((byte)(w0 >> 46)) & 1) != 0;
                        prim = ((int)(w0 >> 47)) & 0x7FF;
                        flg = (byte)(((byte)(w0 >> 58)) & 3);
                        nreg = (byte)(((byte)(w0 >> 60)) & 15);
                        regs = new byte[nreg];
                        for (byte x = 0; x < nreg; x++) regs[x] = (byte)(((byte)(w8 >> (4 * x))) & 15);
                        state = 1;
                        siso.SetLength(0);
                        if (pre) {
                            wrgs.wr.WriteLine("PRIM " + prim);
                            ox.AddPrim("point,line,l-strip,triangle,t-strip,t-fan,spr,?".Split(',')[prim & 7]
                                + " " + "fs,gs".Split(',')[(prim >> 3) & 1]
                                + " tme " + ((prim >> 4) & 1)
                                + " fge " + ((prim >> 5) & 1)
                                + " abe " + ((prim >> 6) & 1)
                                + " aa1 " + ((prim >> 7) & 1)
                                + " fst " + ((prim >> 8) & 1)
                                + " ctxt " + ((prim >> 9) & 1)
                                + " fix " + ((prim >> 10) & 1)
                            );
                        }
                        string ty = "IMG.";
                        if ((flg & 2) != 0) {
                            // IMG
                            siso.Capacity = 16 * nloop;
                        }
                        else if ((flg & 1) != 0) {
                            // reglist
                            siso.Capacity = 8 * nloop * nreg;
                            ty = "REG.";
                        }
                        else {
                            // packed
                            siso.Capacity = 16 * nloop * nreg;
                            ty = "PACK";
                        }
                        string rd = "";
                        foreach (byte v in regs) {
                            rd += string.Format("({0})", RegDesc[v]);
                        }
                        Debug.WriteLine(string.Format("& {4,4} {0:X16} {1:X16} {2} {3}", w8, w0, ty, rd, cntc++));
                        ox.StartGIFtag(string.Format("{4,4} {0:X16} {1:X16} {2} {3}", w8, w0, ty, rd, cntc - 1));
                        rest--;
                    }
                    else if (siso.Length < siso.Capacity) {
                        siso.Write(br.ReadBytes(16), 0, 16);
                        rest--;
                    }
                    if (state != 0 && siso.Length == siso.Capacity) {
                        BinaryReader brx = new BinaryReader(siso);
                        if ((flg & 2) != 0) {
                            // IMG
                        }
                        if ((flg & 1) != 0) {
                            // REGLIST
                            siso.Position = 0;
                            for (int i = 0; i < nloop; i++) {
                                for (int x = 0; x < nreg; x++) {
                                    UInt64 w0 = brx.ReadUInt64();
                                }
                            }
                        }
                        else {
                            // PACKED
                            for (int i = 0; i < nloop; i++) {
                                for (int x = 0; x < nreg; x++) {
                                    siso.Position = 16 * x + 16 * nreg * i;
                                    switch (regs[x]) {
                                        case 2: { // ST
                                                float S = brx.ReadSingle();
                                                float T = brx.ReadSingle();
                                                float Q = brx.ReadSingle();
                                                brx.ReadSingle();
                                                wrgs.wr.WriteLine("ST    {0} {1} {2}", S, T, Q);
                                                ox.StartPacked(string.Format("ST    {0} {1} {2}", S, T, Q));
                                                break;
                                            }
                                        case 4: { // XYZF2
                                                float X = brx.ReadUInt16() / 16.0f; brx.ReadUInt16();
                                                float Y = brx.ReadUInt16() / 16.0f; brx.ReadUInt16();
                                                float Z = (brx.ReadInt32() >> 4) & 0xFFFFFF;
                                                int ADC = (brx.ReadInt32() >> 15) & 1;
                                                wrgs.wr.WriteLine("XYZF2 {0,10:0.000} {1,10:0.000} {2,10:0.000} {3}", X, Y, Z, ADC);
                                                ox.StartPacked(string.Format("XYZF2 {0,10:0.000} {1,10:0.000} {2,10:0.000} {3}", X, Y, Z, ADC));
                                                break;
                                            }
                                        case 1: { // RGBAQ
                                                byte R = (byte)brx.ReadUInt32();
                                                byte G = (byte)brx.ReadUInt32();
                                                byte B = (byte)brx.ReadUInt32();
                                                byte A = (byte)brx.ReadUInt32();
                                                wrgs.wr.WriteLine("RGBAQ {0,10} {1,10} {2,10} {3,10}", R, G, B, A);
                                                ox.StartPacked(string.Format("RGBAQ {0,10} {1,10} {2,10} {3,10}", R, G, B, A));
                                                break;
                                            }
                                        case 0x0e: { // A+D
                                                UInt64 w0 = brx.ReadUInt64();
                                                Byte b8 = (byte)brx.ReadUInt64();
                                                Debug.WriteLine("> " + GSREGUt.GetName(b8));
                                                switch (b8) {
                                                    case 0x4e: // ZBUF_1
                                                        ox.StartPacked(string.Format("ZBUF_1 ZBP {0:x3} PSM {1} ZMSK {2}"
                                                            , (int)((w0 >> 0) & 0x1FF)
                                                            , (byte)((w0 >> 24) & 0xF)
                                                            , (byte)((w0 >> 32) & 0x1)
                                                            ));
                                                        break;
                                                    case 0x42: // ALPHA_1
                                                        ox.StartPacked(string.Format("ALPHA_1 ({0} - {1}) – {2} â 7 + {3}"
                                                            , "Cs,Cd,0,?".Split(',')[(byte)((w0 >> 0) & 3)]
                                                            , "Cs,Cd,0,?".Split(',')[(byte)((w0 >> 2) & 3)]
                                                            , "As,Ad,FIX,?".Split(',')[(byte)((w0 >> 4) & 3)]
                                                            , "Cs,Cd,0,?".Split(',')[(byte)((w0 >> 6) & 3)]
                                                            , (byte)((w0 >> 32))
                                                            ));
                                                        break;
                                                    default:
                                                        ox.StartPacked(string.Format(GSREGUt.GetName(b8)));
                                                        break;
                                                }
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                        state = 0;
                    }
                }
            }

            public void reset() {
                state = 0;
            }

            static class GSREGUt {
                public static string GetName(byte x) {
                    switch (x) {
                        case 0x00: return "PRIM";
                        case 0x01: return "RGBAQ";
                        case 0x02: return "ST";
                        case 0x03: return "UV";
                        case 0x04: return "XYZF2";
                        case 0x05: return "XYZ2";
                        case 0x06: return "TEX0_1";
                        case 0x07: return "TEX0_2";
                        case 0x08: return "CLAMP_1";
                        case 0x09: return "CLAMP_2";
                        case 0x0A: return "FOG";
                        case 0x0C: return "XYZF3";
                        case 0x0D: return "XYZ3";
                        case 0x14: return "TEX1_1";
                        case 0x15: return "TEX1_2";
                        case 0x16: return "TEX2_1";
                        case 0x17: return "TEX2_2";
                        case 0x18: return "XYOFFSET_1";
                        case 0x19: return "XYOFFSET_2";
                        case 0x1A: return "PRMODECONT";
                        case 0x1B: return "PRMODE";
                        case 0x1C: return "TEXCLUT";
                        case 0x22: return "SCANMSK";
                        case 0x34: return "MIPTBP1_1";
                        case 0x35: return "MIPTBP1_2";
                        case 0x36: return "MIPTBP2_1";
                        case 0x37: return "MIPTBP2_2";
                        case 0x3B: return "TEXA";
                        case 0x3D: return "FOGCOL";
                        case 0x3F: return "TEXFLUSH";
                        case 0x40: return "SCISSOR_1";
                        case 0x41: return "SCISSOR_2";
                        case 0x42: return "ALPHA_1";
                        case 0x43: return "ALPHA_2";
                        case 0x44: return "DIMX";
                        case 0x45: return "DTHE";
                        case 0x46: return "COLCLAMP";
                        case 0x47: return "TEST_1";
                        case 0x48: return "TEST_2";
                        case 0x49: return "PABE";
                        case 0x4A: return "FBA_1";
                        case 0x4B: return "FBA_2";
                        case 0x4C: return "FRAME_1";
                        case 0x4D: return "FRAME_2";
                        case 0x4E: return "ZBUF_1";
                        case 0x4F: return "ZBUF_2";
                        case 0x50: return "BITBLTBUF";
                        case 0x51: return "TRXPOS";
                        case 0x52: return "TRXREG";
                        case 0x53: return "TRXDIR";
                        case 0x54: return "HWREG";
                        case 0x60: return "SIGNAL";
                        case 0x61: return "FINISH";
                        case 0x62: return "LABEL";
                    }
                    return x.ToString("x2");
                }
            }
        }

        void transfer(GIFt p, byte[] bin) {
            p.transfer(bin);
        }

        public void reset() {
            p1.reset();
        }
    }
}