using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CustVPU1;
using System.Diagnostics;

namespace Prayvif1 {
    // Prayvif1
    // Prayvif2
    // parsePAX_@16:06 2013/01/02
    public class GSim : IGSim {
        TextWriter wr;
        Outxml ox;
        BPyWriter bpy;

        public GSim(TextWriter wr, BPyWriter bpy, Outxml ox) {
            this.wr = wr;
            this.p1 = new GIFt(ox, bpy);
            this.ox = ox;
            this.bpy = bpy;
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

        /// <summary>
        /// From GIF
        /// </summary>
        /// <param name="bin"></param>
        public void Transfer3(byte[] bin) {
            wr.WriteLine(" * Transfer3: bytes " + bin.Length);
            transfer(p1, bin);
        }

        GIFt p1;

        void transfer(GIFt p, byte[] bin) {
            p.transfer(bin);
        }

        public void reset() {
            p1.reset();
        }
    }

    public class GIFt {
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

        Outxml ox;
        BPyWriter bpy;

        public int SBP = 0, SBW = 0, DBP = 0, DBW = 0;
        public byte SPSM = 0, DPSM = 0;
        public int TPDir = 0;
        public int SSAX = 0, SSAY = 0, DDAX = 0, DDAY = 0;
        public int RRW = 0, RRH = 0;
        public int TRXDIR = 0;
        public int TBP0 = 0, TBW = 0, TW = 0, TH = 0, TCC = 0, TFX = 0, CBP = 0, CSA = 0;
        public byte PSM = 0, CPSM = 0;

        public GIFt(Outxml ox, BPyWriter bpy) {
            bra = new BinaryReader(osa);
            wra = new BinaryWriter(osa);
            this.ox = ox;
            (this.bpy = bpy).gift = this;
        }

        public void transfer(byte[] bin) {
            ox.StartGS();
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
                    string ty = "IMG.";
                    if (pre) {
                        bpy.StartPrimitive((byte)(prim & 7), 0 != ((prim >> 8) & 1), 0 != ((prim >> 6) & 1), 0 != ((prim >> 4) & 1));
                    }
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

                    ox.StartGIFtag2(nloop, eop, pre, prim, flg, nreg, regs);
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
                        siso.Position = 0;
                        bpy.SendIMG(siso);
                    }
                    if ((flg & 1) != 0) {
                        // REGLIST
                        siso.Position = 0;
                        for (int i = 0; i < nloop; i++) {
                            for (int x = 0; x < nreg; x++) {
                                UInt64 w0 = brx.ReadUInt64();
                                ReadGSReg(regs[x], w0);
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
                                            ox.StartPacked(string.Format("ST    {0} {1} {2}", S, T, Q));
                                            bpy.SetSTQ(S, T, Q);
                                            break;
                                        }
                                    case 3: { // UV
                                            float U = (brx.ReadUInt32() & 0x3FFF) / 16.0f;
                                            float V = (brx.ReadUInt32() & 0x3FFF) / 16.0f;
                                            brx.ReadUInt32();
                                            brx.ReadUInt32();
                                            ox.StartPacked(string.Format("UV    {0} {1}", U, V));
                                            bpy.SetUV(U, V);
                                            break;
                                        }
                                    case 4: { // XYZF2
                                            float X = brx.ReadUInt16() / 16.0f; brx.ReadUInt16();
                                            float Y = brx.ReadUInt16() / 16.0f; brx.ReadUInt16();
                                            float Z = (brx.ReadInt32() >> 4) & 0xFFFFFF;
                                            int ADC = (brx.ReadInt32() >> 15) & 1;
                                            ox.StartPacked(string.Format("XYZF2 {0,10:0.000} {1,10:0.000} {2,10:0.000} {3}", X, Y, Z, ADC));
                                            bpy.SetXYZF(X, Y, Z, 0 != ADC);
                                            break;
                                        }
                                    case 1: { // RGBAQ
                                            byte R = (byte)brx.ReadUInt32();
                                            byte G = (byte)brx.ReadUInt32();
                                            byte B = (byte)brx.ReadUInt32();
                                            byte A = (byte)brx.ReadUInt32();
                                            ox.StartPacked(string.Format("RGBAQ {0,10} {1,10} {2,10} {3,10}", R, G, B, A));
                                            bpy.SetRGBAQ(R, G, B, A);
                                            break;
                                        }
                                    case 0x0e: { // A+D
                                            UInt64 w0 = brx.ReadUInt64();
                                            Byte b8 = (byte)brx.ReadUInt64();
                                            Debug.WriteLine("> " + GSREGUt.GetName(b8));
                                            ReadGSReg(b8, w0);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    state = 0;
                }
            }
            ox.EndGS();
        }

        // frm Outxml.cs
        static String[] alPrim = "point,line,line strip,tri,tri strip,tri fan,sprite,X".Split(',');

        void ReadGSReg(byte b8, UInt64 w0) {
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
                case 0x50: // BITBLTBUF
                    ox.StartPacked(string.Format("BITBLTBUF SBP={0:x4} SBW={1} SPSM={2} DBP={3:x4} DBW={4} DPSM={5}"
                        , SBP = Bitut.Cuts32(w0, 0, 14)
                        , SBW = Bitut.Cuts32(w0, 16, 6)
                        , PSF2Util.Decode(SPSM = Bitut.Cutu8(w0, 24, 6))
                        , DBP = Bitut.Cuts32(w0, 32, 14)
                        , DBW = Bitut.Cuts32(w0, 48, 6)
                        , PSF2Util.Decode(DPSM = Bitut.Cutu8(w0, 56, 6))
                        ));
                    break;
                case 0x51: // TRXPOS
                    ox.StartPacked(string.Format("TRXPOS DIR={0} SSAX={1} SSAY={2} DDAX={3} DDAY={4}"
                        , "UL-to-LR,LL-to-UR,UR-to-LL,LR-to-UL".Split(',')[TPDir = Bitut.Cuts32(w0, 59, 2)]
                        , SSAX = Bitut.Cuts32(w0, 0, 11)
                        , SSAY = Bitut.Cuts32(w0, 16, 11)
                        , DDAX = Bitut.Cuts32(w0, 32, 11)
                        , DDAY = Bitut.Cuts32(w0, 48, 11)
                        ));
                    break;
                case 0x52: // TRXREG
                    ox.StartPacked(string.Format("TRXREG RRW={0} RRH={1}"
                        , RRW = Bitut.Cuts32(w0, 0, 12)
                        , RRH = Bitut.Cuts32(w0, 32, 12)
                        ));
                    break;
                case 0x53: // TRXDIR
                    ox.StartPacked(string.Format("TRXDIR XDIR={0}"
                        , "Host-to-Local,Local-to-Host,Local-to-Local,X".Split(',')[TRXDIR = Bitut.Cuts32(w0, 0, 2)]
                        ));
                    break;
                case 0x16: // TEX2_1
                    ox.StartPacked(string.Format("TEX2_1 PSM={0} CBP={1:x4} CPSM={2} CSM={3} CSA={4:x2} CLD={5}"
                        , PSF2Util.Decode(Bitut.Cutu8(w0, 0, 6))
                        , Bitut.Cuts32(w0, 37, 14)
                        , CPSMUtil.Decode(Bitut.Cutu8(w0, 51, 4))
                        , Bitut.Cuts32(w0, 55, 1)
                        , Bitut.Cuts32(w0, 56, 5)
                        , Bitut.Cuts32(w0, 61, 3)
                        ));
                    break;
                case 0x06: // TEX0_1
                    ox.StartPacked(string.Format("TEX0_1 TBP0={0:x4} TBW={1} PSM={2} TW={3} TH={4} TCC={5} TFX={6} CBP={7:x4} CPSM={8} CSM={9} CSA={10:x2} CLD={11}"
                        , TBP0 = Bitut.Cuts32(w0, 0, 14)
                        , TBW = Bitut.Cuts32(w0, 14, 6)
                        , PSF2Util.Decode(PSM = Bitut.Cutu8(w0, 20, 6))
                        , TW = Bitut.Cuts32(w0, 26, 4)
                        , TH = Bitut.Cuts32(w0, 30, 4)
                        , TCC = Bitut.Cuts32(w0, 34, 1)
                        , TFX = Bitut.Cuts32(w0, 35, 2)
                        , CBP = Bitut.Cuts32(w0, 37, 14)
                        , CPSMUtil.Decode(CPSM = Bitut.Cutu8(w0, 51, 4))
                        , Bitut.Cuts32(w0, 55, 1)
                        , CSA = Bitut.Cuts32(w0, 56, 5)
                        , Bitut.Cuts32(w0, 61, 3)
                        ));
                    break;
                case 0x01: // RGBAQ
                    ox.StartPacked(string.Format("RGBAQ R={0} G={1} B={2} A={3} Q={4}"
                        , Bitut.Cuts32(w0, 0, 8)
                        , Bitut.Cuts32(w0, 8, 8)
                        , Bitut.Cuts32(w0, 16, 8)
                        , Bitut.Cuts32(w0, 24, 8)
                        , fut.UIToF((uint)(w0 >> 32))
                        ));
                    bpy.SetRGBAQ(
                        Bitut.Cutu8(w0, 0, 8),
                        Bitut.Cutu8(w0, 8, 8),
                        Bitut.Cutu8(w0, 16, 8),
                        Bitut.Cutu8(w0, 24, 8)
                        );
                    break;
                case 0x08: // CLAMP_1
                    ox.StartPacked(string.Format("CLAMP_1 WMS={0} WMT={1} MINU={2} MAXU={3} MINV={4} MAXV={5}"
                        , "REPEAT,CLAMP,REGION_CLAMP,REGION_REPEAT".Split(',')[Bitut.Cuts32(w0, 0, 2)]
                        , "REPEAT,CLAMP,REGION_CLAMP,REGION_REPEAT".Split(',')[Bitut.Cuts32(w0, 2, 2)]
                        , Bitut.Cuts32(w0, 4, 10)
                        , Bitut.Cuts32(w0, 14, 10)
                        , Bitut.Cuts32(w0, 24, 10)
                        , Bitut.Cuts32(w0, 34, 10)
                        ));
                    break;
                case 0x14: // TEX1_1
                    ox.StartPacked(string.Format("TEX1_1 LCM={0} MXL={1} MMAG={2} MMIN={3} MTBA={4} L={5} K={6}"
                        , Bitut.Cutu8(w0, 0, 1)
                        , Bitut.Cutu8(w0, 2, 3)
                        , "NEAREST,LINEAR".Split(',')[Bitut.Cutu8(w0, 5, 1)]
                        , "NEAREST,LINEAR,NEAREST_MIPMAP_NEAREST,NEAREST_MIPMAP_LINEAR,LINEAR_MIPMAP_NEAREST,LINEAR_MIPMAP_LINEAR".Split(',')[Bitut.Cutu8(w0, 6, 3)]
                        , Bitut.Cutu8(w0, 9, 1)
                        , Bitut.Cuts32(w0, 19, 2)
                        , Bitut.Cuts32(w0, 32, 12)
                        ));
                    break;
                case 0x47: // TEST_1
                    ox.StartPacked(string.Format("TEST_1 ATE={0} ATST={1} AREF={2} AFAIL={3} DATE={4} DATM={5} ZTE={6} ZTST={7}"
                        , "off,on".Split(',')[Bitut.Cuts32(w0, 0, 1)]
                        , "NEVER,ALWAYS,LESS,LEQUAL,EQUAL,GEQUAL,GREATER,NOTEQUAL".Split(',')[Bitut.Cuts32(w0, 1, 3)]
                        , Bitut.Cuts32(w0, 4, 8)
                        , "KEEP,FB_ONLY,ZB_ONLY,RGB_ONLY".Split(',')[Bitut.Cuts32(w0, 12, 2)]
                        , "off,on".Split(',')[Bitut.Cuts32(w0, 14, 1)]
                        , Bitut.Cuts32(w0, 15, 1)
                        , "off,on".Split(',')[Bitut.Cuts32(w0, 16, 1)]
                        , "NEVER,ALWAYS,GEQUAL,GREATER".Split(',')[Bitut.Cuts32(w0, 17, 2)]
                        ));
                    break;
                case 0x4c: // FRAME_1
                    ox.StartPacked(string.Format("FRAME_1 FBP={0:x3} FBW={1} PSM={2} FBMSK={3}"
                        , Bitut.Cuts32(w0, 0, 9)
                        , Bitut.Cuts32(w0, 16, 6)
                        , FBPSM2Util.Decode(Bitut.Cutu8(w0, 24, 6))
                        , Bitut.Cuts32(w0, 32, 1)
                        ));
                    break;
                case 0x00: // PRIM
                    ox.StartPacked(string.Format("PRIM PRIM={0} IIP={1} TME={2} FGE={3} ABE={4} AA1={5} FST={6} CTXT={7} FIX={8}"
                        , alPrim[Bitut.Cuts32(w0, 0, 3)]
                        , "Flat,Gouraud".Split(',')[Bitut.Cutu8(w0,3,1)]
                        , "off,on".Split(',')[Bitut.Cutu8(w0, 4, 1)]
                        , "off,on".Split(',')[Bitut.Cutu8(w0, 5, 1)]
                        , "off,on".Split(',')[Bitut.Cutu8(w0, 6, 1)]
                        , "off,on".Split(',')[Bitut.Cutu8(w0, 7, 1)]
                        , "STQ,UV".Split(',')[Bitut.Cutu8(w0, 8, 1)]
                        , "Context1,Context2".Split(',')[Bitut.Cutu8(w0, 9, 1)]
                        , "Unfixed,Fixed".Split(',')[Bitut.Cutu8(w0, 10, 1)]
                        ));
                    break;
                case 0x03: // UV
                    ox.StartPacked(string.Format("UV U={0} V={1}"
                        , Bitut.Cuts32(w0, 0, 14) / 16.0f
                        , Bitut.Cuts32(w0, 16, 14) / 16.0f
                        ));
                    break;
                case 0x05: // XYZ2
                    ox.StartPacked(string.Format("XYZ2 X={0} Y={1} Z={2}"
                        , Bitut.Cuts32(w0, 0, 16) / 16.0f
                        , Bitut.Cuts32(w0, 16, 16) / 16.0f
                        , Bitut.Cutu32(w0, 32, 32)
                        ));
                    break;
                case 0x40: // SCISSOR_1
                    ox.StartPacked(string.Format("SCISSOR_1 SCAX0={0} SCAX1={1} SCAY0={2} SCAY1={3}"
                        , Bitut.Cuts32(w0, 0, 11)
                        , Bitut.Cuts32(w0, 16, 11)
                        , Bitut.Cuts32(w0, 32, 11)
                        , Bitut.Cuts32(w0, 48, 11)
                        ));
                    break;
                default:
                    ox.StartPacked(string.Format(GSREGUt.GetName(b8)));
                    break;
            }
        }

        FUt fut = new FUt();

        class FUt {
            MemoryStream os = new MemoryStream(4);
            BinaryReader br;
            BinaryWriter wr;

            public FUt() {
                br = new BinaryReader(os);
                wr = new BinaryWriter(os);
            }

            public float UIToF(uint v) {
                os.Position = 0;
                wr.Write(v);
                os.Position = 0;
                return br.ReadSingle();
            }
        }

        class CPSMUtil {
            public static string Decode(int val) {
                switch (val) {
                    case 0x00: return "PSMCT32 ";
                    case 0x02: return "PSMCT16 ";
                    case 0x0A: return "PSMCT16S";
                    case 0x01: return "1?      ";
                }
                return null;
            }
        }

        class PSF2Util {
            public static string Decode(byte val) {
                switch (val) {
                    case 0x00: return "PSMCT32 ";
                    case 0x01: return "PSMCT24 ";
                    case 0x02: return "PSMCT16 ";
                    case 0x0A: return "PSMCT16S";
                    case 0x13: return "PSMT8   ";
                    case 0x14: return "PSMT4   ";
                    case 0x1B: return "PSMT8H  ";
                    case 0x24: return "PSMT4HL ";
                    case 0x2C: return "PSMT4HH ";
                    case 0x30: return "PSMZ32  ";
                    case 0x31: return "PSMZ24  ";
                    case 0x32: return "PSMZ16  ";
                    case 0x3A: return "PSMZ16S ";
                }
                return null;
            }
        }

        class FBPSM2Util {
            public static string Decode(byte val) {
                switch (val) {
                    case 0x00: return "PSMCT32 ";
                    case 0x01: return "PSMCT24 ";
                    case 0x02: return "PSMCT16 ";
                    case 0x0A: return "PSMCT16S";
                    case 0x13: return "reserved";
                    case 0x14: return "reserved";
                    case 0x1B: return "reserved";
                    case 0x24: return "reserved";
                    case 0x2C: return "reserved";
                    case 0x30: return "PSMZ32  ";
                    case 0x31: return "PSMZ24  ";
                    case 0x32: return "PSMZ16  ";
                    case 0x3A: return "PSMZ16S ";
                }
                return null;
            }
        }

        class Bitut {
            public static int Cuts32(UInt64 val, int offset, int bits) {
                return Convert.ToInt32((val >> offset) & ((1u << bits) - 1));
            }

            public static object Cutu32(UInt64 val, int offset, int bits) {
                return Convert.ToUInt32((val >> offset) & ((1u << bits) - 1));
            }

            public static byte Cutu8(UInt64 val, int offset, int bits) {
                return Convert.ToByte((val >> offset) & ((1u << bits) - 1));
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
}