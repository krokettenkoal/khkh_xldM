using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

// TODO: GSpassthru.binîVèoóÕÇâêÕÇµçƒê∂Ç∑ÇÈ

namespace ParseGSlogging {
    class Program {
        static void helpYou() {
            Console.Error.WriteLine("ParseGSlogging <GSpassthru.bin>");
            Environment.Exit(1);
        }
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpYou();
            }
            SortedDictionary<uint, string> M = new SortedDictionary<uint, string>();
            M[1] = "GSgifTransfer1";
            M[2] = "GSgifTransfer2";
            M[3] = "GSgifTransfer3";
            M[11] = "GSreadFIFO";
            M[21] = "GSvsync";
            M[12] = "GSreadFIFO2";
            M[31] = "GSwriteCSR";
            M[32] = "GSsetFrameSkip";
            M[33] = "GSsetGameCRC";
            M[41] = "UpdatePREG";
            using (FileStream fs = File.OpenRead(args[0])) {
                BinaryReader br = new BinaryReader(fs);
                try {
                    GIF gif = new GIF();
                    int fno = 0;
                    while (true) {
                        uint id = br.ReadUInt32();
                        uint size = br.ReadUInt32();
                        string func = M[id];
                        byte[] bin = br.ReadBytes(Convert.ToInt32(size));
                        MemoryStream si = new MemoryStream(bin, false);
                        Debug.Assert(size <= 65536);

                        Console.ResetColor();
                        Console.WriteLine("{0,-16}{1,6:0}  {2}", func, size, hexbin(bin));

                        switch (id) {
                            case 1:
                            case 2:
                            case 3: {
                                    BinaryReader brint = new BinaryReader(si);
                                    while (si.Position < si.Length) {
                                        byte[] GIFtag = brint.ReadBytes(16);
                                        Trace.Assert(GIFtag.Length == 16);
                                        Console.ResetColor();
                                        Console.WriteLine("{0,-16}{0,6} >{1}<", "", hexbin(GIFtag));
                                        gif.Read(fno, GIFtag);
                                        if (id == 1 && gif.stall) break;
                                    }
                                    break;
                                }
                            case 41: {
                                    BinaryReader brint = new BinaryReader(si);
                                    uint mask = brint.ReadUInt32();
                                    if (0 != (mask & 0x00001)) brint.ReadUInt64(); //PMODE 
                                    if (0 != (mask & 0x00002)) brint.ReadUInt64(); //SMODE1
                                    if (0 != (mask & 0x00004)) brint.ReadUInt64(); //SMODE2 
                                    if (0 != (mask & 0x00008)) brint.ReadUInt64(); //SRFSH
                                    if (0 != (mask & 0x00010)) brint.ReadUInt64(); //SYNCH1 
                                    if (0 != (mask & 0x00020)) brint.ReadUInt64(); //SYNCH2 
                                    if (0 != (mask & 0x00040)) brint.ReadUInt64(); //SYNCV 
                                    if (0 != (mask & 0x00080)) gif.DISPFB1 = brint.ReadUInt64(); //DISPFB1 
                                    if (0 != (mask & 0x00100)) gif.DISPLAY1 = brint.ReadUInt64(); //DISPLAY1 
                                    if (0 != (mask & 0x00200)) gif.DISPFB2 = brint.ReadUInt64(); //DISPFB2 
                                    if (0 != (mask & 0x00400)) gif.DISPLAY2 = brint.ReadUInt64(); //DISPLAY2 
                                    if (0 != (mask & 0x00800)) brint.ReadUInt64(); //EXTBUF 
                                    if (0 != (mask & 0x01000)) brint.ReadUInt64(); //EXTDATA 
                                    if (0 != (mask & 0x02000)) gif.EXTWRITE = brint.ReadUInt64(); //EXTWRITE 
                                    if (0 != (mask & 0x04000)) brint.ReadUInt64(); //BGCOLOR 
                                    if (0 != (mask & 0x08000)) brint.ReadUInt64(); //CSR 
                                    if (0 != (mask & 0x10000)) brint.ReadUInt64(); //IMR 
                                    if (0 != (mask & 0x20000)) brint.ReadUInt64(); //BUSDIR 
                                    if (0 != (mask & 0x40000)) brint.ReadUInt64(); //SIGLBLID 
                                    break;
                                }

                        }
                        fno++;
                    }
                }
                catch (IOException) {
                }
            }
        }

        class CCUtil {
            public static void GSREG() {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            public static void GSprim() {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            internal static void GSPREG() {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }

        class GIF {
            int stat = 0;

            int nloop = -1;
            int nreg = -1;
            int curpos = -1; // PACKEDmode
            int cntpos = -1; // PACKEDmode
            int rpos = -1; // REGLISTmode
            int rend = -1; // REGLISTmode
            int tpos = -1; // IMAGEmode
            int tend = -1;// IMAGEmode
            byte[] regs = new byte[16];

            public UInt64 dispfb1 = 0, dispfb2 = 0, display1 = 0, display2 = 0, extwrite = 0;
            public bool stall = false;

            public UInt64 EXTWRITE {
                set {
                    extwrite = value;
                    CCUtil.GSPREG();
                    Console.WriteLine("# EXTWRITE: WRITE({0})"
                        , ((uint)(value) & 1)
                        );
                    Debug.Fail("EXTWRITE is enabled?");
                    Console.Write("");
                }
            }
            public UInt64 DISPFB1 {
                set {
                    dispfb1 = value;
                    CCUtil.GSPREG();
                    Console.WriteLine("# DISPFB1: FBP({0,3}),FBW({1,2}),PSM({2}),DBX({3,4}),DBY({4,4})"
                        , ((uint)(value) & 0x1FF)
                        , ((uint)(value >> 9) & 0x3F)
                        , PSMUtil.Decode((uint)(value >> 15) & 0x1F)
                        , ((uint)(value >> 32) & 0x7FF)
                        , ((uint)(value >> 43) & 0x7FF)
                        );
                    Console.Write("");
                }
            }
            public UInt64 DISPFB2 {
                set {
                    dispfb2 = value;
                    CCUtil.GSPREG();
                    Console.WriteLine("# DISPFB2: FBP({0,3}),FBW({1,2}),PSM({2}),DBX({3,4}),DBY({4,4})"
                        , ((uint)(value) & 0x1FF)
                        , ((uint)(value >> 9) & 0x3F)
                        , PSMUtil.Decode((uint)(value >> 15) & 0x1F)
                        , ((uint)(value >> 32) & 0x7FF)
                        , ((uint)(value >> 43) & 0x7FF)
                        );
                    Console.Write("");
                }
            }
            public UInt64 DISPLAY1 {
                set {
                    display1 = value;
                    CCUtil.GSPREG();
                    Console.WriteLine("# DISPLAY1: DX({0,4}),DY({1,4}),MAGH({2,2}),MAGV({3,2}),DW({4,4}),DH({5,4})"
                        , ((uint)(value) & 0xFFF)
                        , ((uint)(value >> 12) & 0x7FF)
                        , ((uint)(value >> 23) & 0xF)
                        , ((uint)(value >> 27) & 3)
                        , ((uint)(value >> 32) & 0xFFF)
                        , ((uint)(value >> 44) & 0x7FF)
                    );
                    Console.Write("");
                }
            }
            public UInt64 DISPLAY2 {
                set {
                    display2 = value; CCUtil.GSPREG();
                    Console.WriteLine("# DISPLAY1: DX({0,4}),DY({1,4}),MAGH({2,2}),MAGV({3,2}),DW({4,4}),DH({5,4})"
                        , ((uint)(value) & 0xFFF)
                        , ((uint)(value >> 12) & 0x7FF)
                        , ((uint)(value >> 23) & 0xF)
                        , ((uint)(value >> 27) & 3)
                        , ((uint)(value >> 32) & 0xFFF)
                        , ((uint)(value >> 44) & 0x7FF)
                    );
                    Console.Write("");
                }
            }

            public void Read(int fno, byte[] bin) {
                if (stat == 0) {
                    stall = false;
                    MemoryStream si = new MemoryStream(bin, false);
                    BinaryReader br = new BinaryReader(si);
                    UInt64 v0 = br.ReadUInt64();
                    nloop = (int)(v0 & 0x7FFF);
                    int eop = (int)((v0 >> 15) & 1);
                    int pre = (int)((v0 >> 46) & 3);
                    int prim = (int)((v0 >> 47) & 0x7FF);
                    int flg = (int)((v0 >> 58) & 3);
                    nreg = (int)((v0 >> 60) & 15);
                    for (int x = 0; x < 8; x++) {
                        byte a = br.ReadByte();
                        regs[2 * x + 0] = (byte)(a % 16);
                        regs[2 * x + 1] = (byte)(a / 16);
                    }

                    if (prim != 0) {
                        string pt = "";
                        switch (prim & 7) {
                            case 0: pt = "Point"; break;
                            case 1: pt = "Line"; break;
                            case 2: pt = "Line Strip"; break;
                            case 3: pt = "Tri"; break;
                            case 4: pt = "Tri Strip"; break;
                            case 5: pt = "Tri Fan"; break;
                            case 6: pt = "Sprite"; break;
                            case 7: pt = "X"; break;
                        }
                        Debug.WriteLine(string.Format("{0,-10} {1,5}ÅÄ{2,2}Å®{3,5}"
                            , pt
                            , nloop
                            , nreg
                            , nloop / nreg
                            ));
                        Console.Write("");
                    }

                    Console.WriteLine("# id({0,8:0}) nloop({1,5}) eop({2}) pre({3}) prim({4,4}) flg({5}) nreg({6,2})"
                        , fno, nloop, eop, pre, prim, flg, nreg
                        );
                    curpos = 0;
                    cntpos = nreg * nloop;
                    if (flg == 0) {
                        if (cntpos != 0) stat = 1;
                    }
                    else if (flg == 1) {
                        if (cntpos != 0) {
                            stat = 2;
                            rpos = 0;
                            rend = nreg * nloop;
                        }
                    }
                    else if (flg == 2) {
                        if (nloop != 0) {
                            stat = 3;
                            tpos = 0;
                            tend = nloop;
                        }
                    }
                    else Debug.Fail("Unknown FLG in GSprimitive");
                }
                else if (stat == 1) {
                    byte curreg = regs[curpos % nreg];
                    switch (curreg) { // PACKEDmode
                        default:
                            Debug.Fail("Unknown REG#" + curreg + " in PACKEDmode of GSprimitive");
                            break;
                        case 0x1: // RGBAQ(0x01)
                            {
                                byte vR = bin[0];
                                byte vG = bin[4];
                                byte vB = bin[8];
                                byte vA = bin[12];
                                CCUtil.GSprim();
                                Console.WriteLine("# RGBAQ = {0:X2} {1:X2} {2:X2} {3:X2}", vR, vG, vB, vA);
                                break;
                            }
                        case 0x2: // ST(0x02)
                            {
                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader br = new BinaryReader(si);
                                string S = br.ReadSingle().ToString();
                                string T = br.ReadSingle().ToString();
                                CCUtil.GSprim();
                                Console.WriteLine("# ST = {0} {1}", S, T);
                                break;
                            }
                        case 0x3: // UV(0x03)
                            {
                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader br = new BinaryReader(si);
                                string U = FFPx.Decode14F4(br.ReadUInt32() & 0x3FFF);
                                string V = FFPx.Decode14F4(br.ReadUInt32() & 0x3FFF);
                                CCUtil.GSprim();
                                Console.WriteLine("# UV = {0} {1}", U, V);
                                break;
                            }
                        case 0x04: // XYZF2(0x04)
                            {
                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader br = new BinaryReader(si);
                                float X = FFP.Decode16F4(br.ReadUInt32() & 0xFFFF);
                                float Y = FFP.Decode16F4(br.ReadUInt32() & 0xFFFF);
                                uint Z = (br.ReadUInt32() >> 4) & 0xFFFFFF;
                                uint v3 = br.ReadUInt32();
                                uint F = (v3 >> 4) & 0xFF;
                                uint ADC = (v3 >> 15) & 1;
                                CCUtil.GSprim();
                                Console.WriteLine("# XYZF2 = {0} {1} {2} {3} {4}", X, Y, Z, F, ADC);
                                break;
                            }
                        case 0x5: // XYZ2(0x05)
                            {
                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader br = new BinaryReader(si);
                                float X = FFP.Decode16F4(br.ReadUInt32() & 0xFFFF);
                                float Y = FFP.Decode16F4(br.ReadUInt32() & 0xFFFF);
                                uint Z = br.ReadUInt32();
                                uint v3 = br.ReadUInt32();
                                uint ADC = (v3 >> 15) & 1;
                                CCUtil.GSprim();
                                Console.WriteLine("# XYZ2 = {0} {1} {2} {3}", X, Y, Z, ADC);
                                break;
                            }
                        case 0x6: // TEX0_1
                            {
                                CCUtil.GSprim();
                                Console.WriteLine("# TEX0_1");
                                break;
                            }
                        case 0x8: // CLAMP_1
                            {
                                CCUtil.GSprim();
                                Console.WriteLine("# CLAMP_1");
                                break;
                            }
                        case 0xE: // A+D(0x0e)
                            {
                                byte gsreg = bin[8];
                                CCUtil.GSprim();
                                Console.WriteLine("# REG[0x{0:X2}] = {1}", gsreg, hexbin(bin, 0, 8));

                                MemoryStream si = new MemoryStream(bin, false);
                                BinaryReader br = new BinaryReader(si);
                                switch (gsreg) {
                                    case 0x06:
                                    case 0x07: { // TEX0_1 TEX0_2
                                            UInt64 v0 = br.ReadUInt64();
                                            int tbp0 = (int)((v0 >> 0) & 0x3FFF);
                                            int tbw = (int)((v0 >> 14) & 0x3F);
                                            int psm = (int)((v0 >> 20) & 0x3F);
                                            int tw = (int)((v0 >> 26) & 15);
                                            int th = (int)((v0 >> 26) & 15);
                                            int tcc = (int)((v0 >> 34) & 1);
                                            int tfx = (int)((v0 >> 35) & 3);
                                            int cbp = (int)((v0 >> 37) & 0x3FFF);
                                            int cpsm = (int)((v0 >> 51) & 15);
                                            int csm = (int)((v0 >> 55) & 1);
                                            int csa = (int)((v0 >> 55) & 1);
                                            int cld = (int)((v0 >> 61) & 7);
                                            CCUtil.GSREG();
                                            Console.WriteLine("# {0}: TBP0({1,5}),TBW({2,2}),PSM({3}),TW({4,4}),TH({5,4}),TCC({6})" + Environment.NewLine +
                                                    "#         TFX({7}),CBP({8,5}),CPSM({9}),CSM({10}),CSA({11,2}),CLD({12})"
                                                    , (gsreg == 6) ? "TEX0_1" : "TEX0_2"
                                                    , tbp0, tbw, PSFUtil.Decode((byte)psm), 1 << tw, 1 << th, tcc, TFXUtil.Decode(tfx), cbp, CPSMUtil.Decode(cpsm), csm, csa, cld
                                                    );
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x16:
                                    case 0x17: { // TEX2_1 TEX2_2
                                            UInt64 v0 = br.ReadUInt64();
                                            int psm = (int)((v0 >> 20) & 0x3F);
                                            int cbp = (int)((v0 >> 37) & 0x3FFF);
                                            int cpsm = (int)((v0 >> 51) & 15);
                                            int csm = (int)((v0 >> 55) & 1);
                                            int csa = (int)((v0 >> 55) & 1);
                                            int cld = (int)((v0 >> 61) & 7);
                                            CCUtil.GSREG();
                                            Console.WriteLine("# {0}: PSM({1}),CBP({2,5}),CPSM({3}),CSM({4}),CSA({5,2}),CLD({6})"
                                                    , (gsreg == 6) ? "TEX2_1" : "TEX2_2"
                                                    , PSFUtil.Decode((byte)psm), cbp, CPSMUtil.Decode(cpsm), csm, csa, cld
                                                    );
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x4C:
                                    case 0x4D: { // FRAME_1 FRAME 2
                                            UInt64 v0 = br.ReadUInt64();
                                            CCUtil.GSREG();
                                            Console.WriteLine("# {0}: FBP({1,3}),FBW({2,2}),PSM({3}),FBMASK({4:X8})"
                                                    , (gsreg == 0x4C) ? "FRAME_1" : "FRAME_2"
                                                    , (uint)((v0 >> 0) & 0x1FF)
                                                    , (uint)((v0 >> 16) & 0x3F)
                                                    , PSFUtil.Decode((byte)((v0 >> 24) & 0x3F))
                                                    , (uint)((v0 >> 32))
                                                    );
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x50: { // BITBLTBUF
                                            UInt64 v0 = br.ReadUInt64();
                                            CCUtil.GSREG();
                                            Console.WriteLine(
                                                    "# BITBLTBUF: SBP({0,5}) SBW({1,2}) SPSM({2})" + Environment.NewLine +
                                                    "#            DBP({3,5}) DBW({4,2}) DPSM({5})"
                                                    , ((v0) & 0x3FFF)
                                                    , ((v0 >> 16) & 0x3F)
                                                    , PSFUtil.Decode((byte)((v0 >> 24) & 0x3F))
                                                    , ((v0 >> 32) & 0x3FFF)
                                                    , ((v0 >> 48) & 0x3F)
                                                    , PSFUtil.Decode((byte)((v0 >> 56) & 0x3F))
                                                    );
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x51: { // TRXPOS
                                            int ssax = br.ReadUInt16() & 0x7FF;
                                            int ssay = br.ReadUInt16() & 0x7FF;
                                            int dsax = br.ReadUInt16() & 0x7FF;
                                            int v3 = br.ReadUInt16();
                                            int dsay = v3 & 0x7FF;
                                            int dir = (byte)((v3 >> 11) & 3);
                                            CCUtil.GSREG();
                                            Console.WriteLine("# TRXPOS: SSAX({0,4})SSAY({1,4})DSAX({2,4})DSAY({3,4})DIR({4})"
                                                    , ssax, ssay, dsax, dsay, dir
                                                    );
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x52: { // TRXREG
                                            uint rrw = br.ReadUInt32() & 0xFFF;
                                            uint rrh = br.ReadUInt32() & 0xFFF;
                                            CCUtil.GSREG();
                                            Console.WriteLine("# TRXREG: RRW({0,4})RRH({0,4})", rrw, rrh);
                                            Console.Write("");
                                            break;
                                        }
                                    case 0x53: { // TRXDIR
                                            uint xdir = br.ReadUInt32() & 3;
                                            CCUtil.GSREG();
                                            Console.WriteLine("# TRXDIR: XDIR({0})", xdir);
                                            Console.Write("");

                                            break;
                                        }
                                }

                                break;
                            }
                    }
                    curpos++;
                    if (curpos == cntpos) { stall = true; stat = 0; }
                }
                else if (stat == 2) {
                    for (int w = 0; w < 2 && rpos < rend; w++, rpos++) {
                        int offset = 8 * w;
                        byte curreg = regs[rpos % nreg];
                        switch (curreg) {
                            default:
                                Debug.Fail("Unknown REG#" + curreg + " in REGLISTmode of GSprimitive");
                                break;
                            case 0: // PRIM(0x00)
                                CCUtil.GSprim();
                                Console.WriteLine("# PRIM");
                                break;
                            case 1: // RGBAQ(0x01)
                                {
                                    MemoryStream si = new MemoryStream(bin, offset, 8, false);
                                    BinaryReader br = new BinaryReader(si);
                                    byte R = br.ReadByte();
                                    byte G = br.ReadByte();
                                    byte B = br.ReadByte();
                                    byte A = br.ReadByte();
                                    uint Q = br.ReadUInt32();
                                    CCUtil.GSprim();
                                    Console.WriteLine("# RGBAQ = {0:X2} {1:X2} {2:X2} {3:X2} {4:X8}", R, G, B, A, Q);
                                    break;
                                }
                            case 3: // UV(0x03)
                                {
                                    MemoryStream si = new MemoryStream(bin, offset, 8, false);
                                    BinaryReader br = new BinaryReader(si);
                                    float U = FFP.Decode14F4((uint)(br.ReadUInt16() & 0x3FFF));
                                    float V = FFP.Decode14F4((uint)(br.ReadUInt16() & 0x3FFF));
                                    CCUtil.GSprim();
                                    Console.WriteLine("# UV = {0} {1}", U, V);
                                    break;
                                }
                            case 5: // XYZ2(0x05) {
                                {
                                    MemoryStream si = new MemoryStream(bin, offset, 8, false);
                                    BinaryReader br = new BinaryReader(si);
                                    float X = FFP.Decode16F4(br.ReadUInt16());
                                    float Y = FFP.Decode16F4(br.ReadUInt16());
                                    uint Z = br.ReadUInt32();
                                    CCUtil.GSprim();
                                    Console.WriteLine("# XYZ2 = {0} {1} {2}", X, Y, Z);
                                    break;
                                }
                            case 15: // NOP(0x0F)
                                {
                                    CCUtil.GSprim();
                                    Console.WriteLine("# NOP");
                                    break;
                                }
                        }
                    }
                    if (rpos == rend) { stall = true; stat = 0; }
                }
                else if (stat == 3) {
                    tpos++;
                    if (tpos == tend) { stall = true; stat = 0; }
                }
            }
        }

        class TFXUtil {
            public static string Decode(int val) {
                switch (val) {
                    case 0: return "M.";
                    case 1: return "D.";
                    case 2: return "H1";
                    case 3: return "H2";
                }
                return null;
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
        class PSMUtil {
            public static string Decode(uint val) {
                return Decode((byte)val);
            }
            public static string Decode(byte val) {
                switch (val) {
                    case 0x00: return "PSMCT32 ";
                    case 0x01: return "PSMCT24 ";
                    case 0x02: return "PSMCT16 ";
                    case 0x0A: return "PSMCT16S";
                    case 0x12: return "PS-GPU24";
                    case 0x17: return "23?     ";
                }
                return null;
            }
        }
        class PSFUtil {
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
                }
                return null;
            }
        }
        class FFPx {
            public static string Decode14F4(uint val) {
                return val.ToString("X4");
            }
            public static string Decode16F4(uint val) {
                return val.ToString("X4");
            }
        }
        class FFP {
            public static float Decode14F4(uint val) {
                return (float)((val >> 4)
                    + (((val & 0x01) != 0) ? 1.0f / 2.0f : 0.0f)
                    + (((val & 0x02) != 0) ? 1.0f / 2.0f / 2.0f : 0.0f)
                    + (((val & 0x04) != 0) ? 1.0f / 2.0f / 2.0f / 2.0f : 0.0f)
                    + (((val & 0x08) != 0) ? 1.0f / 2.0f / 2.0f / 2.0f / 2.0f / 2.0f : 0.0f)
                    );
            }
            public static float Decode16F4(uint val) {
                float v = (float)(((val >> 4) & 0xFFF)
                    + (((val & 0x01) != 0) ? 1.0f / 2.0f : 0.0f)
                    + (((val & 0x02) != 0) ? 1.0f / 2.0f / 2.0f : 0.0f)
                    + (((val & 0x04) != 0) ? 1.0f / 2.0f / 2.0f / 2.0f : 0.0f)
                    + (((val & 0x08) != 0) ? 1.0f / 2.0f / 2.0f / 2.0f / 2.0f / 2.0f : 0.0f)
                    );
                return v;
            }
        }

        private static object hexbin(BinaryReader brint, int size) {
            string s = "";
            for (int x = 0; x < size; x++) {
                s += brint.ReadByte().ToString("X2") + " ";
            }
            return s;
        }

        private static object hexbin(byte[] bin) {
            string str = "";
            int x = 0, cx = bin.Length;
            for (; x < cx && x < 16; x++) {
                str += bin[x].ToString("X2") + " ";
            }
            if (x != cx) str += "...";
            return str;
        }

        private static object hexbin(byte[] bin, int x, int cx) {
            string str = "";
            cx += x;
            for (; x < cx; x++) {
                str += bin[x].ToString("X2") + " ";
            }
            return str;
        }
    }
}
