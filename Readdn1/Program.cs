using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Readdn1 {
    class Program {
        static void Main(string[] args) {
            new Program().Run();
        }

        byte[] eeram;
        MemoryStream si;
        BinaryReader br;
        int tadr = 0;

        byte[] ReadVifwr2(string fp) {
            using (FileStream fs = File.OpenRead(fp)) {
                BinaryReader br = new BinaryReader(fs);
                foreach (string row in Encoding.ASCII.GetString(br.ReadBytes(1024)).TrimEnd((char)0).Split(new char[] { (char)0x1A }, 1)[0].Split('\n')) {
                    if (row.StartsWith("#"))
                        continue;
                    string[] cols = row.Split(new char[] { '=' }, 2);
                    if (cols[0] == "tadr")
                        tadr = Convert.ToInt32(cols[1], 16);
                }
                eeram = br.ReadBytes(32 * 1024 * 1024);
            }
            return eeram;
        }

        void Run() {
            //eeram = File.ReadAllBytes(@"H:\EMU\pcsx2-0.9.4\myrec\_20081103182922_18624.raw");
            //eeram = ReadVifwr2(@"H:\EMU\pcsx2-0.9.4\vifwr2_188_000.bin");
            //eeram = ReadVifwr2(@"H:\EMU\pcsx2-0.9.4\vifwr2_576_003.bin");
            String fp = @"H:\EMU\pcsx2-0.9.4\myrecvol3-642\vifwr2_642_000.bin";

            eeram = ReadVifwr2(fp);
            si = new MemoryStream(eeram, false);
            br = new BinaryReader(si);

            Readfls();

            foreach (FE1 o in alfe1) {
                Console.WriteLine(o);
            }
            Console.WriteLine();

            foreach (FE2 o in alfe2) {
                Console.WriteLine(o);
            }
            Console.WriteLine();

            if (tadr != 0) {
                Readtadr();
                Console.WriteLine();
            }
        }

        private void Readtadr() {
            uint tadr = (uint)this.tadr;
            Stack<uint> asr = new Stack<uint>();
            while (true) {
                si.Position = tadr;
                UInt64 tag = br.ReadUInt64();
                byte id = (byte)((((uint)tag) >> 28) & 7);
                uint qwc = (ushort)tag;
                uint addr = (uint)(tag >> 32);
                byte irq = (byte)(((uint)tag >> 31) & 1);
                switch (id) {
                    case 0://refe
                        Console.WriteLine("_ refe @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , addr
                            , qwc
                            );
                        goto endTag;

                    case 1://cnt
                        Console.WriteLine("_ cnt  @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , tadr + 16
                            , qwc
                            );
                        tadr = Convert.ToUInt32(tadr + 16 + 16 * qwc);
                        break;

                    case 2://next
                        Console.WriteLine("_ next @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , tadr + 16
                            , qwc
                            );
                        tadr = Convert.ToUInt32(addr);
                        break;

                    case 3://ref
                        Console.WriteLine("_ ref  @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , addr
                            , qwc
                            );
                        tadr += 16;
                        break;

                    case 4://refs
                        Console.WriteLine("_ refs @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , addr
                            , qwc
                            );
                        tadr += 16;
                        break;

                    case 5://call
                        Console.WriteLine("_ call @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , tadr + 16
                            , qwc
                            );
                        asr.Push(Convert.ToUInt32(tadr + 16 + 16 * qwc));
                        tadr = addr;
                        break;

                    case 6://ret
                        Console.WriteLine("_ ret  @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , tadr + 16
                            , qwc
                            );
                        if (asr.Count == 0) goto endTag;
                        tadr = asr.Pop();
                        break;

                    case 7://end
                        Console.WriteLine("_ end  @ {0:x8} a {1:x8} qwc {2:x4} "
                            , tadr
                            , tadr + 16
                            , qwc
                            );
                        goto endTag;

                }
            }
        endTag:
            ;
        }

        class FE1 {
            public string name;
            public int v00; // 
            public int v04;
            public int v08;
            public int v0c;
            public int v10;
            public int v14;
            public int v38;
            public int v3c;
            public int v40;
            public int v44; // off raw
            public int v48; // ?
            public int v4c; // off fe2
            public int v50; // cb
            public int v54; // 
            public int v58; // 
            public int v5c; // 
            public int v60; // off next fe1
            public int v64; // 
            public int v68; // 
            public int v6c; // 

            public FE1(BinaryReader br) {
                v00 = br.ReadInt32();
                v04 = br.ReadInt32();
                v08 = br.ReadInt32();
                v0c = br.ReadInt32();
                v10 = br.ReadInt32();
                v14 = br.ReadInt32();
                name = BUt.Bin2Disp(br.ReadBytes(32));
                v38 = br.ReadInt32();
                v3c = br.ReadInt32();
                v40 = br.ReadInt32();
                v44 = br.ReadInt32();
                v48 = br.ReadInt32();
                v4c = br.ReadInt32();
                v50 = br.ReadInt32();
                v54 = br.ReadInt32();
                v58 = br.ReadInt32();
                v5c = br.ReadInt32();
                v60 = br.ReadInt32();
                v64 = br.ReadInt32();
                v68 = br.ReadInt32();
                v6c = br.ReadInt32();
            }

            public override string ToString() {
                return string.Format("{0,-25} {1:X8} {2,10:#,##0} {3:X8} {4:X8}", name, v44, v50, v4c, v00);
            }
        }
        List<FE1> alfe1 = new List<FE1>();

        private void Readfls() {
            for (int off = 0x381350; ; off += 96) { /*off < 0x381830*/
                si.Position = off;
                FE1 o = new FE1(br);
                if (o.name.Length == 0) break;
                alfe1.Add(o);
            }

            {
                si.Position = 0x4f645c;
                int cnt2 = br.ReadInt32();
                for (int off = 0x4f6480; ; off += 80) { /*off < 0x4f6d40*/
                    si.Position = off;
                    FE2 o = new FE2(br);
                    if (o.name.Length == 0) break;
                    alfe2.Add(o);
                }
            }
        }

        class FE2 {
            public int v0;
            public string name;
            public int off1, off2, off3;
            public int off4, off5, off6, off7;
            public int off8, off9, offa, offb;

            public FE2(BinaryReader br) {
                v0 = br.ReadInt32();
                name = BUt.Bin2Disp(br.ReadBytes(32));
                off1 = br.ReadInt32();
                off2 = br.ReadInt32();
                off3 = br.ReadInt32();
                off4 = br.ReadInt32();
                off5 = br.ReadInt32();
                off6 = br.ReadInt32();
                off7 = br.ReadInt32();
                off8 = br.ReadInt32();
                off9 = br.ReadInt32();
                offa = br.ReadInt32();
                offb = br.ReadInt32();
            }

            public override string ToString() {
                return string.Format("{0,-32} {1:X8}", name, off4);
            }
        }
        List<FE2> alfe2 = new List<FE2>();

        class BUt {
            public static string Bin2Disp(byte[] bin) {
                int t = Array.IndexOf(bin, (byte)0);
                return Encoding.ASCII.GetString(bin, 0, t);
            }
        }
    }
}
