using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ef1Declib2;
using System.Diagnostics;
using Prayvif1;
using System.IO;

namespace parsePAX_ {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Mlink2 rec = new Mlink2();

        uint hwRead32(uint mem) {
            Debug.WriteLine(String.Format("#R {0:x8}", mem));
            if (false) { }
            else if (mem == 0x10000000) { return T0_COUNT; }
            else if (mem == 0x10003020) { return GIF.STAT; }
            else if (mem == 0x10003C00) { return VIF1.STAT; }
            else if (mem == 0x1000A000) { return D2.CHCR; }
            else if (mem == 0x1000A030) { return D2.TADR; }
            else if (mem == 0x10009000) { return D1.CHCR; }
            else if (mem == 0x1000E010) { return D_STAT; }
            else {
                Debug.Fail(String.Format("hwRead {0:x8}", mem));
            }
            return 0;
        }
        void hwWrite32(uint mem, uint v) {
            Debug.WriteLine(String.Format("#W {0:x8} {1:x8}", mem, v));
            if (false) {
            }
            else if (mem == 0x1000E010) {//w
                D_STAT = v;
            }
            else if (mem == 0x1000a030) {//w
                D2.TADR = v;
            }
            else if (mem == 0x1000a020) {//w
                D2.QWC = v;
            }
            else if (mem == 0x1000a010) {//w
                D2.MADR = v;
            }
            else if (mem == 0x1000a000) {//w
                D2.CHCR = v;

                if (D2.STR) {
                    switch (D2.MOD) {
                        case 0: //Normal
                            {
                                byte[] bin = MUt.Cut(rec.cee, D2.MADR, 16 * D2.QWC);
                                oxml.StartGIFdma(D2.MADR, D2.QWC, bin);
                                gs.Transfer3(bin);
                                D2.MADR += 16 * D2.QWC;
                                break;
                            }
                        case 1: //Chain
                            {
                                readChain(D2);
                                break;
                            }
                        default: throw new NotSupportedException();
                    }
                    D2.STR = false;
                }
            }
            else {
                Debug.Fail(String.Format("hwWrite {0:x8} {1:x8}", mem, v));
            }
        }

        public uint D_STAT = 0;

        class Refm {
            public byte[] bin;
            public int off;
        }

        Refm GetPSM(uint addr) {
            Refm r = new Refm();
            if (0 != (addr & 0x80000000U)) {
                r.bin = rec.cee.spr;
                r.off = (int)addr & 0x3FFF;
            }
            else {
                r.bin = rec.cee.ram;
                r.off = (int)addr;
            }
            return r;
        }

        class Pkt {
            public uint x;
            public uint cx;
            public int ty;

            public Pkt(uint x, uint cx, int ty) {
                this.x = x;
                this.cx = cx;
                this.ty = ty;
            }
        }

        void readChain(DMAC Dx) {
            bool isfine = false;
            Queue<uint> asr = new Queue<uint>();
            Debug.Assert(Dx.TADR != 0);
            while (!isfine) {
                uint tadr = Dx.TADR;
                Refm refm = GetPSM(tadr);
                UInt64 tag = BitConverter.ToUInt64(refm.bin, refm.off);

                int id = ((int)(tag >> 28)) & 7;
                uint qwc = ((uint)(tag)) & 65535;
                uint addr = ((uint)(tag >> 32));

                Eat(Dx, tadr, new Pkt(tadr + 8, 8, 8));
                switch (id) {
                    case 0: // refe
                        Eat(Dx, tadr, new Pkt(addr, 16U * qwc, 0));
                        isfine = true; tadr = uint.MaxValue - 1;
                        break;
                    case 1: // cnt
                        Eat(Dx, tadr, new Pkt(tadr + 16, 16 * qwc, 1));
                        tadr += 16U + 16U * qwc;
                        break;
                    case 2: // next
                        Eat(Dx, tadr, new Pkt(tadr + 16, 16 * qwc, 2));
                        tadr = addr;
                        break;
                    case 3: // ref
                        Eat(Dx, tadr, new Pkt(addr, 16 * qwc, 3));
                        tadr += 16;
                        break;
                    case 4: // refs
                        Eat(Dx, tadr, new Pkt(addr, 16 * qwc, 4));
                        tadr += 16;
                        break;
                    case 5: // call
                        Eat(Dx, tadr, new Pkt(tadr + 16, 16 * qwc, 5));
                        asr.Enqueue((uint)(tadr + 16 + 16 * qwc));
                        tadr = addr;
                        break;
                    case 6: // ret
                        Eat(Dx, tadr, new Pkt(tadr + 16, 16 * qwc, 6));
                        if (asr.Count == 0) {
                            isfine = true; tadr = uint.MaxValue - 1;
                        }
                        else {
                            tadr = asr.Dequeue();
                        }
                        break;
                    case 7: // end
                        Eat(Dx, tadr, new Pkt(tadr + 16, 16 * qwc, 7));
                        isfine = true; tadr = uint.MaxValue - 1;
                        break;
                    default:
                        throw new NotSupportedException("DMAc.id " + id);
                }
                Debug.Assert(tadr != 0);
                Dx.TADR = tadr;
            }
        }

        private void Eat(DMAC Dx, uint tadr, Pkt pkt) {
            oxml.ParseDMAc(pkt.x, pkt.cx, DMATagTypes[pkt.ty], tadr);

            if (Dx == D2) { // GIF
                if (pkt.ty != 8) { // no upper64
                    byte[] bin = MUt.Cut(rec.cee, pkt.x, pkt.cx);
                    oxml.StartGIFdma(pkt.x, pkt.cx >> 4, bin);
                    gs.Transfer3(bin);
                }
            }
        }

        static string[] DMATagTypes = "refe,cnt,next,ref,refs,call,ret,end,upper64".Split(',');

        class GIFC {
            public uint STAT = 0;
        }
        GIFC GIF = new GIFC();
        class VIFC {
            public uint STAT = 0;
        }
        VIFC VIF1 = new VIFC();

        class DMAC {
            public uint QWC = 0, MADR = 0, CHCR = 0, TADR = 0;

            public bool DIR { get { return 0 != (CHCR & 0x0001U); } set { if (value)CHCR |= 0x0001U; else CHCR &= ~(0x0001U); } }
            public uint MOD { get { return (CHCR >> 2) & 0x3U; } set { CHCR |= (value & 0x3U) << 2; } }
            public bool STR { get { return 0 != (CHCR & 0x0100U); } set { if (value)CHCR |= 0x0100U; else CHCR &= ~(0x0100U); } }
            public ushort TAG { get { return (ushort)(CHCR >> 16); } set { CHCR |= (uint)(value) << 16; } }
        }

        DMAC D1 = new DMAC();
        DMAC D2 = new DMAC();
        uint T0_COUNT = 0;

        class MUt {
            internal static byte[] Cut(ee1Dec.C.CustEE cee, uint off, uint len) {
                byte[] bin = new byte[len];
                bool fSpr = (0 != (off & 0x80000000U));
                Buffer.BlockCopy(
                    fSpr ? cee.spr : cee.ram,
                    fSpr ? (int)off & 0x3FFF : (int)off,
                    bin,
                    0,
                    (int)len
                    );
                return bin;
            }
        }

        GSim gs;
        StreamWriter gswr;
        BPyWriter bpy;
        StreamWriter bpywr;
        Outxml oxml;

        String baseDir = Path.Combine(Application.StartupPath, "work");

        private void Form1_Load(object sender, EventArgs e) {
            Directory.CreateDirectory(baseDir);
            Environment.CurrentDirectory = baseDir;

            gs = new GSim(
                gswr = new StreamWriter("gs.txt"),
                bpy = new BPyWriter(
                    bpywr = new StreamWriter("bpy.py")
                    ),
                oxml = new Outxml()
                );

            gswr.AutoFlush = true;
            bpywr.AutoFlush = true;

            rec.cee.hwRead32 = hwRead32;
            rec.cee.hwWrite32 = hwWrite32;
            rec.InitPax();

            Run();
        }

        private void Run() {
            bpy.StartMesh("pax");

            oxml.Clear();
            oxml.Startfile("", baseDir);

            rec.RunPax();

            oxml.Save(Path.Combine(baseDir, "oxml.xml"));

            bpy.EndMesh();

            rbpy.Eat(bpy);
        }

        private void bRun_Click(object sender, EventArgs e) {
            Run();
        }
    }
}