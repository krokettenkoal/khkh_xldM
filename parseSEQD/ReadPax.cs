//#define OUTPUT_CHUNK

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using SlimDX;
using parseSEQD.Binuts;

namespace parseSEQD.Paxx {
    public interface IRefUtil {
        Int64 Off { get; }
        Int64 Len { get; }
        String Name { get; }
    }
    public class ReadPax2 : IRefUtil {
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return si.Length - baseoff; } }
        public String Name { get { return "PAX"; } }

        public ReadPax2(BinaryReader br, Int64 off) {
            this.br = br;
            this.baseoff = off;
            this.si = br.BaseStream;
        }

        public List<P1> P1s {
            get {
                List<P1> al = new List<P1>();
                si.Position = baseoff + 8;
                int cnt = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    al.Add(new P1(br, baseoff + 0x10 + 0x50 * x));
                }
                return al;
            }
        }

        public Po82 Po82 {
            get {
                si.Position = baseoff + 12;
                int offt82 = br.ReadInt32();

                return new Po82(br, baseoff + offt82);
            }
        }
    }

    public class Po82 : IRefUtil {
        public BinaryReader br;
        public Int64 baseoff;
        public Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 16; } }
        public String Name { get { return "Po82"; } }

        public Po82(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;

            Read();
        }

        void Read() {
            si.Position = baseoff;
            int id82 = br.ReadInt32();
            if (id82 != 0x82) throw new InvalidDataException("!82");
        }

        public int CountP2 {
            get {
                si.Position = baseoff + 12;
                return br.ReadInt32();
            }
        }

        public List<P2> P2s {
            get {
                int cnt = CountP2;
                List<P2> al = new List<P2>();
                for (int x = 0; x < cnt; x++) {
                    al.Add(new P2(br, baseoff + 16 + 32 * x, this));
                }
                return al;
            }
        }
    }

    public class Po96 : IRefUtil {
        public BinaryReader br;
        public Int64 baseoff;
        public Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 4; } }
        public String Name { get { return "Po96"; } }

        public Po96(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;

            Read();
        }

        void Read() {
            si.Position = baseoff;
            int is96 = br.ReadInt32();
            if (is96 != 0x96) throw new InvalidDataException("!96");

            OffP11s = new List<int>();
            CountP11 = br.ReadInt32();
            for (int x = 0; x < CountP11; x++) {
                OffP11s.Add(br.ReadInt32());
            }

            OffP21s = new List<int>();
            CountP21 = br.ReadInt32();
            for (int x = 0; x < CountP21; x++) {
                OffP21s.Add(br.ReadInt32());
            }

            OffP31s = new List<int>();
            CountP31 = br.ReadInt32();
            for (int x = 0; x < CountP31; x++) {
                OffP31s.Add(br.ReadInt32());
            }

            OffP41s = new List<int>();
            CountP41 = br.ReadInt32();
            for (int x = 0; x < CountP41; x++) {
                OffP41s.Add(br.ReadInt32());
            }

            OffP51s = new List<int>();
            CountP51 = br.ReadInt32();
            for (int x = 0; x < CountP51; x++) {
                OffP51s.Add(br.ReadInt32());
            }
        }

        public int CountP11;
        public int CountP21;
        public int CountP31;
        public int CountP41;
        public int CountP51;

        public List<int> OffP11s;
        public List<int> OffP21s;
        public List<int> OffP31s;
        public List<int> OffP41s;
        public List<int> OffP51s;

        public List<P11> P11s {
            get {
                List<P11> al = new List<P11>();
                foreach (int off in OffP11s) {
                    al.Add(new P11(br, baseoff + off));
                }
                return al;
            }
        }
        public List<P21> P21s {
            get {
                List<P21> al = new List<P21>();
                foreach (int off in OffP21s) {
                    al.Add(new P21(br, baseoff + off));
                }
                return al;
            }
        }
        public List<P31> P31s {
            get {
                List<P31> al = new List<P31>();
                foreach (int off in OffP31s) {
                    al.Add(new P31(br, baseoff + off));
                }
                return al;
            }
        }
        public List<P41> P41s {
            get {
                List<P41> al = new List<P41>();
                foreach (int off in OffP41s) {
                    al.Add(new P41(br, baseoff + off));
                }
                return al;
            }
        }
        public List<P51> P51s {
            get {
                List<P51> al = new List<P51>();
                foreach (int off in OffP51s) {
                    al.Add(new P51(br, baseoff + off));
                }
                return al;
            }
        }
    }

#if true
    public class ReadPaxx {
        MemoryStream si;
        BinaryReader br;

        BinutElementCollection bc = new BinutElementCollection();

        public BinutElementCollection BEC { get { return bc; } }

        List<P1> al1 = new List<P1>();
        List<P2> al2 = new List<P2>();

        public ReadPaxx(MemoryStream si) {
            br = new BinaryReader(this.si = si);

            using (BinutScope bsp1 = new BinutScope(bc, "P1")) {
                si.Position = 8;
                int cnt = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    int off1 = 0x10 + 0x50 * x;
                    using (BinutScope bsp1x = new BinutScope(bsp1, String.Format("{0}", x), off1, 0, 0x50)) {
                        si.Position = off1;
                        al1.Add(new P1(br, off1));
                    }
                }
            }

            int offt82;
            using (BinutScope bs82 = new BinutScope(bc, "82")) {
                si.Position = 12;
                offt82 = br.ReadInt32();
                using (BinutScope bs82x = new BinutScope(bs82, "0", offt82, 0, 4)) {
                    Read82(offt82, bs82x);
                }
            }

            using (BinutScope bs96 = new BinutScope(bc, "96")) {
                int offtx = offt82 + 16 + 32 * al2.Count;
                si.Position = offtx;
                int cnt = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = offtx + 4 * (1 + x);
                    int off = br.ReadInt32();

                    int off96 = offt82 + off;
                    using (BinutScope bs96x = new BinutScope(bs96, String.Format("{0}", x), off96, 0, 4)) {
                        Read96(off96, bs96x);
                    }
                }
            }
        }

        private void Read96(int offt96, BinutScope bs96x) {
            si.Position = offt96;

            int id96 = br.ReadInt32();
            if (id96 != 0x96) throw new InvalidDataException("!96");

            int readoff = offt96 + 4;

            using (BinutScope bs11 = new BinutScope(bs96x, "P11")) {
                // P11 [variables]
                si.Position = readoff;
                int cnt = br.ReadInt32();
                readoff += 4;
                for (int x = 0; x < cnt; x++, readoff += 4) {
                    si.Position = readoff;
                    int off = br.ReadInt32();
                    int off11 = offt96 + off;
                    using (BinutScope bs11x = new BinutScope(bs11, String.Format("{0}", x), off11, 0, 1)) {
                        ReadP11(offt96 + off, bs11x);
                    }
                }
            }

            using (BinutScope bs21 = new BinutScope(bs96x, "P21")) {
                // P21 [textures]
                si.Position = readoff;
                int cnt = br.ReadInt32();
                readoff += 4;
                for (int x = 0; x < cnt; x++, readoff += 4) {
                    si.Position = readoff;
                    int off = br.ReadInt32();
                    int off21 = offt96 + off;
                    using (BinutScope bs21x = new BinutScope(bs21, String.Format("{0}", x), off21, 0, 1)) {
                        ReadP21(off21);
                    }
                }
            }

            using (BinutScope bs31 = new BinutScope(bs96x, "P31")) {
                // P31 ?
                si.Position = readoff;
                int cnt = br.ReadInt32();
                readoff += 4;
                for (int x = 0; x < cnt; x++, readoff += 4) {
                    si.Position = readoff;
                    int off = br.ReadInt32();
                    int off31 = offt96 + off;
                    using (BinutScope bs31x = new BinutScope(bs31, String.Format("{0}", x), off31, 0, 1)) {
                        ReadP31(off31);
                    }
                }
            }

            using (BinutScope bs41 = new BinutScope(bs96x, "P41")) {
                // P41 ?
                si.Position = readoff;
                int cnt = br.ReadInt32();
                readoff += 4;
                for (int x = 0; x < cnt; x++, readoff += 4) {
                    si.Position = readoff;
                    int off = br.ReadInt32();
                    int off41 = offt96 + off;
                    using (BinutScope bs41x = new BinutScope(bs41, String.Format("{0}", x), off41, 0, 1)) {
                        ReadP41(off41, bs41x);
                    }
                }
            }

            using (BinutScope bs51 = new BinutScope(bs96x, "P51")) {
                // P51 ?
                si.Position = readoff;
                int cnt = br.ReadInt32();
                readoff += 4;
                for (int x = 0; x < cnt; x++, readoff += 4) {
                    si.Position = readoff;
                    int off = br.ReadInt32();
                    int off51 = offt96 + off;
                    using (BinutScope bs51x = new BinutScope(bs51, String.Format("{0}", x), off51, 0, 1)) {
                        ReadP51(off51);
                    }
                }
            }
        }

        private void ReadP51(int off51) {

        }

        private void ReadP41(int off41, BinutScope bs41x) {
            si.Position = off41;
            P41 p41 = new P41(br, off41);
            al41.Add(p41);

            int i = 0;
            si.Position = off41 + 0x10 + p41.v14;
            while (true) {
                using (BinutScope bsphdr = new BinutScope(bs41x, String.Format("v{0}", i), Convert.ToInt32(si.Position), 16, 16)) {
                    PdHdr pdhdr = new PdHdr(br, si.Position);
                    p41.alpdhdr.Add(pdhdr);
                    if (pdhdr.v1 == 0xFF)
                        break;

                    if (pdhdr.v1 == 0x06) {
                        for (int x = 0; x < pdhdr.v2; x++) {
                            p41.al06.Add(new Pd06(br));
                        }
                    }
                    else if (pdhdr.v1 == 0x04) {
                        for (int x = 0; x < pdhdr.v2; x++) {
                            p41.al04.Add(new Pd04(br));
                        }
                    }
                    else if (pdhdr.v1 == 0x02) {
                        for (int x = 0; x < pdhdr.v2; x++) {
                            p41.al02.Add(new Pd02(br));
                        }
                    }
                    else if (pdhdr.v1 == 0x00) {
                        for (int x = 0; x < pdhdr.v2; x++) {
                            p41.al00.Add(new Pd00(br));
                        }
                    }
                    else if (pdhdr.v1 == 0x01) {
                        for (int x = 0; x < pdhdr.v2; x++) {
                            p41.al01.Add(new Pd01(br));
                        }
                    }
                    else throw new NotSupportedException(pdhdr.ToString());

                    si.Position = (si.Position + 3) & (~3);
                    i++;
                }
            }

            {
                si.Position = off41 + 0x10 + p41.v18;
                int cnt = (p41.v1c - p41.v18) / 2 / 3;
                using (BinutScope bsvert = new BinutScope(bs41x, "offvert", Convert.ToInt32(si.Position), 6, 6 * cnt)) {
                    for (int x = 0; x < cnt; x++) {
                        int v1 = br.ReadInt16();
                        int v2 = br.ReadInt16();
                        int v3 = br.ReadInt16();
                        p41.alpos.Add(new Vector3(v1, v2, v3));
                    }
                }
            }
        }

        public List<P41> al41 = new List<P41>();

        private void ReadP31(int off31) {

        }

        private void ReadP21(int off21) {

        }

        List<P11> al11 = new List<P11>();

        private void ReadP11(int off11, BinutScope bs11x) {
            si.Position = off11;
            P11 o11 = new P11(br, off11);
            al11.Add(o11);

            int off12 = off11 + 0x110;
            using (BinutScope bs12 = new BinutScope(bs11x, "P12", off12, 0, 1)) {
                si.Position = off12;

                using (BinutScope bs13 = new BinutScope(bs12, "P13")) {
                    int rel13 = 0x10;
                    int i = 0;
                    while (true) {
                        int off13 = off12 + rel13;
                        si.Position = off12 + rel13;
                        int v = br.ReadInt32();
                        if (v == 0) break;
                        using (BinutScope bs13x = new BinutScope(bs13, String.Format("{0}", i), off13, 0, 1)) {
                            ReadP13(off12, off13, o11, bs13x);
                        }
                        rel13 = v;
                        i++;
                    }
                }
            }
        }

        class Devut {
            public static void WriteIt(Stream si, String fptxt, int unit, int cnt) {
                StringWriter wr = new StringWriter();
                if (!File.Exists(fptxt)) {
                    wr.Write("{0,10} ", unit);
                    for (int v = 0; v < unit; v++) {
                        wr.Write("{0:x2} ", v);
                    }
                    wr.WriteLine();
                }
                for (int x = 0; x < cnt; x++) {
                    wr.Write("{0,10:X} ", si.Position);
                    for (int v = 0; v < unit; v++) {
                        wr.Write("{0:x2} ", si.ReadByte());
                    }
                    wr.WriteLine();
                }
                File.AppendAllText(fptxt, wr.ToString());
            }
        }

        private void ReadP13(int off12, int off13, P11 o11, BinutScope bs13x) {
            si.Position = off13;
            P13 o13 = new P13(br, off13, null);
            //o11.al13.Add(o13);

            {
                si.Position = off13 + 0x26;

                List<P14> al14 = new List<P14>();
                int cnt14 = br.ReadUInt16();
                for (int x = 0; x < cnt14; x++) {
                    al14.Add(new P14(br, si.Position));
                }
                using (BinutScope bs14 = new BinutScope(bs13x, "P13")) {
                    for (int x = 0; x < cnt14; x++) {
                        P14 o14 = al14[x];
#if OUTPUT_CHUNK
                        si.Position = off12 + o14.ChunkOffset;
                        Devut.WriteIt(si, Path.Combine(@"C:\A", string.Format("{0:x2}.txt", o14.ChunkType)), o14.ChunkStride, o14.ChunkCount);
#endif
                        si.Position = off12 + o14.ChunkOffset;
                        using (BinutScope bs14x = new BinutScope(bs14, String.Format("{0:x2}#{1}", o14.ChunkType, x), off12 + o14.ChunkOffset, o14.ChunkStride, o14.ChunkStride * o14.ChunkCount)) {
                            switch (o14.ChunkType) {
                                case 0x1C:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al1c.Add(new Pc1E(br));
                                    }
                                    break;
                                case 0x1D:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al1d.Add(new Pc1D(br));
                                    }
                                    break;
                                case 0x1E:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al1e.Add(new Pc1E(br));
                                    }
                                    break;
                                case 0x1F:
                                    Trace.Assert(o14.ChunkStride == 12);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al1f.Add(new Pc1F(br));
                                    }
                                    break;
                                case 0x24:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al24.Add(new Pc1E(br));
                                    }
                                    break;
                                case 0x25:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al25.Add(new Pc1D(br));
                                    }
                                    break;
                                case 0x26:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al26.Add(new Pc1E(br));
                                    }
                                    break;
                                case 0x27:
                                    Trace.Assert(o14.ChunkStride == 12);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al27.Add(new Pc1F(br));
                                    }
                                    break;
                                case 0x28:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al28.Add(new Pc28(br));
                                    }
                                    break;
                                case 0x29:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al29.Add(new Pc6A(br));
                                    }
                                    break;
                                case 0x2A:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al2a.Add(new Pc1E(br));
                                    }
                                    break;
                                case 0x2B:
                                    Trace.Assert(o14.ChunkStride == 12);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al2b.Add(new Pc1F(br));
                                    }
                                    break;
                                case 0x57:
                                    Trace.Assert(o14.ChunkStride == 4);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al57.Add(new Pc57(br));
                                    }
                                    break;
                                case 0x6a:
                                    Trace.Assert(o14.ChunkStride == 12);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al6a.Add(new Pc6A(br));
                                    }
                                    break;
                                case 0x70:
                                    Trace.Assert(o14.ChunkStride == 4);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al70.Add(new Pc57(br));
                                    }
                                    break;
                                case 0x7c:
                                    Trace.Assert(o14.ChunkStride == 32);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al7c.Add(new Pc7c(br));
                                    }
                                    break;
                                case 0x89:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al89.Add(new Pc89(br));
                                    }
                                    break;
                                case 0xAE:
                                    Trace.Assert(o14.ChunkStride == 20);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.alae.Add(new PcAE(br));
                                    }
                                    break;
                                case 0xF1:
                                    Trace.Assert(o14.ChunkStride == 164);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.alf1.Add(new PcF1(br));
                                    }
                                    break;
                                case 0x8B:
                                    Trace.Assert(o14.ChunkStride == 40);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al8b.Add(new Pc8B(br));
                                    }
                                    break;
                                case 0x5E:
                                    Trace.Assert(o14.ChunkStride == 4);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al5e.Add(new Pc57(br));
                                    }
                                    break;
                                case 0x71:
                                    Trace.Assert(o14.ChunkStride == 4);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al71.Add(new Pc57(br));
                                    }
                                    break;
                                case 0x7D:
                                    Trace.Assert(o14.ChunkStride == 32);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al7d.Add(new Pc7D(br));
                                    }
                                    break;
                                case 0x8E:
                                    Trace.Assert(o14.ChunkStride == 20);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al8e.Add(new Pc8E(br));
                                    }
                                    break;
                                case 0x54:
                                    Trace.Assert(o14.ChunkStride == 4);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al54.Add(new Pc57(br));
                                    }
                                    break;
                                case 0xD0:
                                    Trace.Assert(o14.ChunkStride == 268);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.ald0.Add(new PcD0(br));
                                    }
                                    break;
                                case 0x69:
                                    Trace.Assert(o14.ChunkStride == 12);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al69.Add(new Pc69(br));
                                    }
                                    break;
                                case 0xD4:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.ald4.Add(new PcD4(br));
                                    }
                                    break;
                                case 0xC9:
                                    Trace.Assert(o14.ChunkStride == 220);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.alc9.Add(new PcC9(br));
                                    }
                                    break;
                                case 0xD7:
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.ald7.Add(new PcD7(br));
                                    }
                                    break;
                                case 0xDB:
                                    Trace.Assert(o14.ChunkStride == 40);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.aldb.Add(new PcDB(br));
                                    }
                                    break;
                                case 0x9D:
                                    Trace.Assert(o14.ChunkStride == 88);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al9d.Add(new Pc9D(br));
                                    }
                                    break;
                                case 0x3F: // 0000003f 24  1 000004b4 000004d0 ... 000030d4
                                    Trace.Assert(o14.ChunkStride == 24);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al3f.Add(new Pc3F(br));
                                    }
                                    break;
                                case 0x31: // 00000031 24  7 000003e0 0000048c ... 00003000
                                    Trace.Assert(o14.ChunkStride == 24);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al31.Add(new Pc31(br));
                                    }
                                    break;
                                case 0x42: // 00000042 24  1 000011ec 00001208 ... 00003e0c
                                    Trace.Assert(o14.ChunkStride == 24);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al42.Add(new Pc42(br));
                                    }
                                    break;
                                case 0x8A: // 0000008a 16  1 000004d8 000004ec ... @ 00000808
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al8a.Add(new Pc8A(br));
                                    }
                                    break;
                                case 0xB1: // 000000b1 20  2 00001d58 00001d84 ... @ 00049538
                                    Trace.Assert(o14.ChunkStride == 20);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.alb1.Add(new PcB1(br));
                                    }
                                    break;
                                case 0xD3: // 000000d3 16  1 0000060c 00000620 ... @ 0006597c
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.ald3.Add(new PcD3(br));
                                    }
                                    break;
                                case 0xA0: // 000000a0 28  1 0000171c 0000173c ... @ 0006e93c
                                    Trace.Assert(o14.ChunkStride == 28);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.ala0.Add(new PcA0(br));
                                    }
                                    break;
                                case 0x33: // 00000033 16  1 00000420 00000434 ... @ 00025600
                                    Trace.Assert(o14.ChunkStride == 16);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.al33.Add(new Pc33(br));
                                    }
                                    break;
                                case 0xB8: // 000000b8 28  2 00000150 0000018c ... @ 00031010
                                    Trace.Assert(o14.ChunkStride == 28);
                                    for (int a = 0; a < o14.ChunkCount; a++) {
                                        o13.alb8.Add(new PcB8(br));
                                    }
                                    break;

                                case 0xD5:
                                case 0xD6:
                                case 0xD8:
                                case 0xD9:
                                case 0x73:
                                case 0x72:
                                case 0xB7: // 000000b7 32  1 000001b0 000001d4 ... 00002dd0
                                case 0xC8: // 000000c8 20  1 000000ec 00000104 ... 00002d0c
                                case 0xCA: // 000000ca  8  1 00000048 00000054 ... 00002c68
                                case 0x5D: // 0000005d  4  1 0000085c 00000864 ... @ 0006da7c
                                case 0x60: // 00000060  4  1 00000868 00000870 ... @ 0006da88
                                case 0x75: // 00000075  4  1 0000170c 00001714 ... @ 0006e92c
                                case 0x5A: // 0000005a  4  1 000002b8 000002c0 ... @ 00001c08
                                case 0x74: // 00000074  4  1 0000048c 00000494 ... @ 0000570c
                                case 0xEE: // 000000ee 32  1 0000049c 000004c0 ... @ 0000571c
                                case 0x5B: // 0000005b  4  1 0000080c 00000814 ... @ 00005a8c
                                case 0x78: // 00000078  4  1 00000484 0000048c ... @ 00025664
                                case 0xD2: // 000000d2 16  1 00000548 0000055c ... @ 00003758
                                    break;

                                default:
                                    throw new NotSupportedException(o14 + " ... @ " + (off12 + o14.ChunkOffset).ToString("x8"));
                            }
                        }
                    }
                }
            }

            Console.Write("");
        }

        private void Read82(int offt82, BinutScope bs82) {
            si.Position = offt82;
            int id82 = br.ReadInt32();
            if (id82 != 0x82) throw new InvalidDataException("!82");
            si.Position = offt82 + 12;
            int cnt = br.ReadInt32();
            for (int x = 0; x < cnt; x++) {
                using (BinutScope bs = new BinutScope(bs82, String.Format("{0}", x), offt82 + 16 + 32 * x, 16, 32)) {
                    si.Position = offt82 + 16 + 32 * x;
                    al2.Add(new P2(br, si.Position, null));
                }
            }
        }
    }
#endif

    public class Pd00 { // 0x14 bytes
        public int v00, v04, v08;
        public short v0c, v0e, v10, v12;

        public Pd00(BinaryReader br) {
            v00 = br.ReadInt32(); // vertex color?
            v04 = br.ReadInt32(); // vertex color?
            v08 = br.ReadInt32(); // vertex color?

            v0c = br.ReadInt16(); // off x16?
            v0e = br.ReadInt16(); // off x16?
            v10 = br.ReadInt16(); // off x16?
            v12 = br.ReadInt16(); // padding
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} ({3}, {4}, {5})", v00, v04, v08, v0c, v0e, v10);
        }
    }

    public class Pd01 { // 28 bytes
        public int v00, v04, v08;
        public short v0c, v0e, v10, v12, v14, v16, v18, v1a;

        public Pd01(BinaryReader br) {
            v00 = br.ReadInt32(); // vertex color?
            v04 = br.ReadInt32(); // vertex color?
            v08 = br.ReadInt32(); // vertex color?

            v0c = br.ReadInt16(); // off x16?
            v0e = br.ReadInt16(); // off x16?
            v10 = br.ReadInt16(); // off x16?
            v12 = br.ReadInt16(); // padding

            v14 = br.ReadInt16(); // off x16?
            v16 = br.ReadInt16(); // off x16?
            v18 = br.ReadInt16(); // off x16?
            v1a = br.ReadInt16(); // padding
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} ({3}, {4}, {5}) {6} ({7}, {8}, {9}) {10}"
                , v00, v04, v08
                , v0c, v0e, v10
                , v12
                , v14, v16, v18
                , v1a
                );
        }
    }

    public class Pd02 { // 32 bytes
        public int v00, v04, v08;
        public short v0c, v0e, v10, v12, v14, v16, v18, v1a, v1c, v1e;

        public Pd02(BinaryReader br) {
            v00 = br.ReadInt32(); // vertex color?
            v04 = br.ReadInt32(); // vertex color?
            v08 = br.ReadInt32(); // vertex color?

            v0c = br.ReadInt16(); // off x16?
            v0e = br.ReadInt16(); // off x16?
            v10 = br.ReadInt16(); // off x16?
            v12 = br.ReadInt16(); // padding

            v14 = br.ReadInt16(); // tex x0
            v16 = br.ReadInt16(); // tex y0
            v18 = br.ReadInt16(); // tex x1
            v1a = br.ReadInt16(); // tex y1
            v1c = br.ReadInt16(); // tex x2
            v1e = br.ReadInt16(); // tex y2
        }

        const float F = 4096;

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} ({3}, {4}, {5}) {6} ({7,4},{8,4},{9,4},{10,4},{11,4},{12,4})"
                , v00, v04, v08
                , v0c, v0e, v10
                , v12
                , v14 / F, v16 / F, v18 / F, v1a / F, v1c / F, v1e / F
                );
        }
    }

    public class Pd04 { // 0x18 bytes
        public int v00, v04, v08, v0c;
        public short v10, v12, v14, v16;

        public Pd04(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
            v0c = br.ReadInt32();

            v10 = br.ReadInt16();
            v12 = br.ReadInt16();
            v14 = br.ReadInt16();
            v16 = br.ReadInt16();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} {3:x8} ({4}, {5}, {6}, {7})"
                , v00, v04, v08, v0c
                , v10, v12, v14, v16);
        }
    }

    public class Pd06 { // 0x28 bytes
        public int v00, v04, v08, v0c;
        public ushort v10, v12, v14, v16;
        public short v18, v1a, v1c, v1e,
            v20, v22, v24, v26;

        public Pd06(BinaryReader br) {
            v00 = br.ReadInt32(); // vertex color?
            v04 = br.ReadInt32(); // vertex color?
            v08 = br.ReadInt32(); // vertex color?
            v0c = br.ReadInt32(); // vertex color?

            v10 = br.ReadUInt16(); // off x16?
            v12 = br.ReadUInt16(); // off x16?
            v14 = br.ReadUInt16(); // off x16?
            v16 = br.ReadUInt16(); // off x16?
            v18 = br.ReadInt16();
            v1a = br.ReadInt16();
            v1c = br.ReadInt16();
            v1e = br.ReadInt16();
            v20 = br.ReadInt16();
            v22 = br.ReadInt16();
            v24 = br.ReadInt16();
            v26 = br.ReadInt16();
        }

        const float F = 4096;

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} {3:x8}  ({4}, {5}, {6}, {7}), ({8,4},{9,4},{10,4},{11,4}), ({12,4},{13,4},{14,4},{15,4})"
                , v00, v04, v08, v0c
                , v10, v12, v14, v16
                , v18 / F, v1a / F, v1c / F, v1e / F
                , v20 / F, v22 / F, v24 / F, v26 / F
                );
        }
    }

    public class P41 : IRefUtil {
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 0x30; } }
        public String Name { get { return "P41"; } }

        public ushort v00 { get { si.Position = baseoff + 0x00; return br.ReadUInt16(); } }
        public ushort v02 { get { si.Position = baseoff + 0x02; return br.ReadUInt16(); } }
        public ushort v04 { get { si.Position = baseoff + 0x04; return br.ReadUInt16(); } }
        public ushort v06 { get { si.Position = baseoff + 0x06; return br.ReadUInt16(); } }
        public int v08 { get { si.Position = baseoff + 0x08; return br.ReadUInt16(); } }
        public int v0c { get { si.Position = baseoff + 0x0c; return br.ReadUInt16(); } }

        public int v10 { get { si.Position = baseoff + 0x10; return br.ReadUInt16(); } }
        public int v14 { get { si.Position = baseoff + 0x14; return br.ReadUInt16(); } } // off0 (based on +0x10)
        public int v18 { get { si.Position = baseoff + 0x18; return br.ReadUInt16(); } } // off1 (based on +0x10)
        public int v1c { get { si.Position = baseoff + 0x1c; return br.ReadUInt16(); } } // off2 (based on +0x10)

        public ushort v20 { get { si.Position = baseoff + 0x20; return br.ReadUInt16(); } }
        public ushort v22 { get { si.Position = baseoff + 0x22; return br.ReadUInt16(); } }
        public ushort v24 { get { si.Position = baseoff + 0x24; return br.ReadUInt16(); } }
        public ushort v26 { get { si.Position = baseoff + 0x26; return br.ReadUInt16(); } }
        public int v28 { get { si.Position = baseoff + 0x28; return br.ReadUInt16(); } }
        public int v2c { get { si.Position = baseoff + 0x2c; return br.ReadUInt16(); } }

        public List<PdHdr> alpdhdr = new List<PdHdr>();

        public List<Pd06> al06 = new List<Pd06>();
        public List<Pd04> al04 = new List<Pd04>();
        public List<Pd02> al02 = new List<Pd02>();
        public List<Pd01> al01 = new List<Pd01>();
        public List<Pd00> al00 = new List<Pd00>();

        public List<Vector3> alpos = new List<Vector3>();

        public P41(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;

            Read();
        }

        public void Read() {
            Int64 off = baseoff + 0x10 + v14;
            while (true) {
                PdHdr pdhdr = new PdHdr(br, off);
                alpdhdr.Add(pdhdr);
                if (pdhdr.v1 == 0xFF)
                    break;

                off += 16;

                if (pdhdr.v1 == 0x06) {
                    for (int x = 0; x < pdhdr.v2; x++, off += 0x28) {
                        si.Position = off;
                        al06.Add(new Pd06(br));
                    }
                }
                else if (pdhdr.v1 == 0x04) {
                    for (int x = 0; x < pdhdr.v2; x++, off += 0x18) {
                        si.Position = off;
                        al04.Add(new Pd04(br));
                    }
                }
                else if (pdhdr.v1 == 0x02) {
                    for (int x = 0; x < pdhdr.v2; x++, off += 32) {
                        si.Position = off;
                        al02.Add(new Pd02(br));
                    }
                }
                else if (pdhdr.v1 == 0x00) {
                    for (int x = 0; x < pdhdr.v2; x++, off += 0x14) {
                        si.Position = off;
                        al00.Add(new Pd00(br));
                    }
                }
                else if (pdhdr.v1 == 0x01) {
                    for (int x = 0; x < pdhdr.v2; x++, off += 28) {
                        si.Position = off;
                        al01.Add(new Pd01(br));
                    }
                }
                else throw new NotSupportedException(pdhdr.ToString());

                off = (off + 3) & (~3);
            }

            vpos1 = new VPos(br, baseoff + 0x10 + v18, v22 * 6);
            vpos2 = new VPos(br, baseoff + 0x10 + v1c, v22 * 6);

            {
                off = baseoff + 0x10 + v18;
                int cnt = (v1c - v18) / 2 / 3;
                for (int x = 0; x < cnt; x++, off += 6) {
                    si.Position = off;
                    int v1 = br.ReadInt16();
                    int v2 = br.ReadInt16();
                    int v3 = br.ReadInt16();
                    alpos.Add(new Vector3(v1, v2, v3));
                }
            }
        }

        public VPos vpos1, vpos2;

        public class VPos : IRefUtil {
            Int64 baseoff;
            int len;

            public Int64 Off { get { return baseoff; } }
            public Int64 Len { get { return len; } }
            public String Name { get { return "VPos"; } }

            public VPos(BinaryReader br, Int64 baseoff, int len) {
                this.baseoff = baseoff;
                this.len = len;
            }
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3} {4:x8} {5}", v00, v02, v04, v06, v08, v0c)
                + String.Format(" {0} {1:x4} {2:x4} {3:x4}", v10, v14, v18, v1c)
                + String.Format(" ({0}, {1}, {2}, {3}, {4}, {5})", v20, v22, v24, v26, v28, v2c)
                ;
        }
    }

    public class PdHdr : IRefUtil { // 16 bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 16; } }
        public String Name { get { return "PdHdr"; } }

        public byte v0 { get { si.Position = baseoff + 0; return br.ReadByte(); } } // 0
        public byte v1 { get { si.Position = baseoff + 1; return br.ReadByte(); } } // type
        public ushort v2 { get { si.Position = baseoff + 2; return br.ReadUInt16(); } } // cnt
        public int v4 { get { si.Position = baseoff + 4; return br.ReadInt32(); } } // 0
        public int v8 { get { si.Position = baseoff + 8; return br.ReadInt32(); } } // ?
        public int vc { get { si.Position = baseoff + 12; return br.ReadInt32(); } } // ?

        public PdHdr(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3} {4:x8} {5:x8}", v0, v1, v2, v4, v8, vc);
        }
    }

    public class PcB8 { // 28 bytes
        public uint v00;
        public ushort v04, v06;
        public uint v08;
        public int v0c, v10, v14, v18;

        public PcB8(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt16();
            v06 = br.ReadUInt16();
            v08 = br.ReadUInt32();
            v0c = br.ReadInt32();
            v10 = br.ReadInt32();
            v14 = br.ReadInt32();
            v18 = br.ReadInt32();
        }
    }

    public class Pc33 { // 16 bytes
        public uint v0;
        public int v4;
        public float v8;
        public uint vc;

        public Pc33(BinaryReader br) {
            v0 = br.ReadUInt32();
            v4 = br.ReadInt32();
            v8 = br.ReadSingle();
            vc = br.ReadUInt32();
        }
    }

    public class PcA0 { // 28 bytes
        public uint v00;
        public float v04, v08, v0c, v10, v14;
        public uint v18;

        public PcA0(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadSingle();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadUInt32();
        }
    }

    public class PcD3 { // 16 bytes
        public uint v0, v4, v8;
        public int vc;

        public PcD3(BinaryReader br) {
            v0 = br.ReadUInt32();
            v4 = br.ReadUInt32();
            v8 = br.ReadUInt32();
            vc = br.ReadInt32();
        }
    }

    public class PcB1 { // 20 bytes
        public uint v00;
        public ushort v04, v06;
        public uint v08, v0c, v10;

        public PcB1(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt16();
            v06 = br.ReadUInt16();
            v08 = br.ReadUInt32();
            v0c = br.ReadUInt32();
            v10 = br.ReadUInt32();
        }
    }

    public class Pc8A { // 16 bytes
        public uint v0, v4, v8, vc;

        public Pc8A(BinaryReader br) {
            v0 = br.ReadUInt32();
            v4 = br.ReadUInt32();
            v8 = br.ReadUInt32();
            vc = br.ReadUInt32();
        }
    }

    public class Pc42 { // 24 bytes
        public uint v00, v04, v08, v0c
            , v10, v14;

        public Pc42(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadUInt32();
            v0c = br.ReadUInt32();
            v10 = br.ReadUInt32();
            v14 = br.ReadUInt32();
        }
    }

    public class Pc31 { // 24 bytes
        public uint v00, v04, v08;
        public float v0c, v10, v14;

        public Pc31(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadUInt32();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
        }
    }

    public class Pc3F { // 24 bytes
        public uint v00, v04;
        public float v08;
        public uint v0c, v10, v14;

        public Pc3F(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadSingle();
            v0c = br.ReadUInt32();
            v10 = br.ReadUInt32();
            v14 = br.ReadUInt32();
        }
    }

    public class Pc9D { // 88 bytes
        public uint v00, v04, v08;
        public float v0c, v10, v14, v18;
        public uint v1c;
        public short v20, v22, v24, v26, v28, v2a, v2c, v2e
            , v30, v32, v34, v36, v38, v3a, v3c, v3e
            , v40, v42, v44, v46, v48, v4a, v4c, v4e
            , v50, v52, v54, v56;

        public Pc9D(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadUInt32();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadSingle();
            v1c = br.ReadUInt32();
            v20 = br.ReadInt16();
            v22 = br.ReadInt16();
            v24 = br.ReadInt16();
            v26 = br.ReadInt16();
            v28 = br.ReadInt16();
            v2a = br.ReadInt16();
            v2c = br.ReadInt16();
            v2e = br.ReadInt16();
            v30 = br.ReadInt16();
            v32 = br.ReadInt16();
            v34 = br.ReadInt16();
            v36 = br.ReadInt16();
            v38 = br.ReadInt16();
            v3a = br.ReadInt16();
            v3c = br.ReadInt16();
            v3e = br.ReadInt16();
            v40 = br.ReadInt16();
            v42 = br.ReadInt16();
            v44 = br.ReadInt16();
            v46 = br.ReadInt16();
            v48 = br.ReadInt16();
            v4a = br.ReadInt16();
            v4c = br.ReadInt16();
            v4e = br.ReadInt16();
            v50 = br.ReadInt16();
            v52 = br.ReadInt16();
            v54 = br.ReadInt16();
            v56 = br.ReadInt16();
        }
    }

    public class PcDB { // 40 bytes
        public uint v00, v04, v08;
        public float v0c;
        public uint v10, v14;
        public float v18;
        public uint v1c, v20, v24;

        public PcDB(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadUInt32();
            v0c = br.ReadSingle();
            v10 = br.ReadUInt32();
            v14 = br.ReadUInt32();
            v18 = br.ReadSingle();
            v1c = br.ReadUInt32();
            v20 = br.ReadUInt32();
            v24 = br.ReadUInt32();
        }
    }

    public class PcD7 { // 16 bytes
        public uint v0;
        public float v4, v8, vc;

        public PcD7(BinaryReader br) {
            v0 = br.ReadUInt32();
            v4 = br.ReadSingle();
            v8 = br.ReadSingle();
            vc = br.ReadSingle();
        }
    }

    public class PcC9 { // 220 bytes
        public uint v00, v04, v08, v0c
            , v10, v14, v18, v1c
            , v20, v24, v28, v2c
            , v30, v34, v38, v3c
            , v40, v44, v48;
        public float v4c
            , v50, v54, v58, v5c
            , v60, v64, v68, v6c
            , v70, v74, v78, v7c
            , v80, v84, v88, v8c
            , v90, v94, v98, v9c
            , va0, va4, va8, vac
            , vb0, vb4, vb8, vbc
            , vc0, vc4;
        public ushort vc8, vca, vcc, vce;
        public short vd0;
        public byte vd2, vd3, vd4, vd5, vd6, vd7;
        public float vd8;

        public PcC9(BinaryReader br) {
            v00 = br.ReadUInt32();
            v04 = br.ReadUInt32();
            v08 = br.ReadUInt32();
            v0c = br.ReadUInt32();
            v10 = br.ReadUInt32();
            v14 = br.ReadUInt32();
            v18 = br.ReadUInt32();
            v1c = br.ReadUInt32();
            v20 = br.ReadUInt32();
            v24 = br.ReadUInt32();
            v28 = br.ReadUInt32();
            v2c = br.ReadUInt32();
            v30 = br.ReadUInt32();
            v34 = br.ReadUInt32();
            v38 = br.ReadUInt32();
            v3c = br.ReadUInt32();
            v40 = br.ReadUInt32();
            v44 = br.ReadUInt32();
            v48 = br.ReadUInt32();

            v4c = br.ReadSingle();
            v50 = br.ReadSingle();
            v54 = br.ReadSingle();
            v58 = br.ReadSingle();
            v5c = br.ReadSingle();
            v60 = br.ReadSingle();
            v64 = br.ReadSingle();
            v68 = br.ReadSingle();
            v6c = br.ReadSingle();
            v70 = br.ReadSingle();
            v74 = br.ReadSingle();
            v78 = br.ReadSingle();
            v7c = br.ReadSingle();
            v80 = br.ReadSingle();
            v84 = br.ReadSingle();
            v88 = br.ReadSingle();
            v8c = br.ReadSingle();
            v90 = br.ReadSingle();
            v94 = br.ReadSingle();
            v98 = br.ReadSingle();
            v9c = br.ReadSingle();
            va0 = br.ReadSingle();
            va4 = br.ReadSingle();
            va8 = br.ReadSingle();
            vac = br.ReadSingle();
            vb0 = br.ReadSingle();
            vb4 = br.ReadSingle();
            vb8 = br.ReadSingle();
            vbc = br.ReadSingle();
            vc0 = br.ReadSingle();
            vc4 = br.ReadSingle();

            vc8 = br.ReadUInt16();
            vca = br.ReadUInt16();
            vcc = br.ReadUInt16();
            vce = br.ReadUInt16();
            vd0 = br.ReadInt16();
            vd2 = br.ReadByte();
            vd3 = br.ReadByte();
            vd4 = br.ReadByte();
            vd5 = br.ReadByte();
            vd6 = br.ReadByte();
            vd7 = br.ReadByte();
            vd8 = br.ReadSingle();
        }
    }

    public class PcD4 { // 16 bytes
        public uint v0;
        public float v4, v8, vc;

        public PcD4(BinaryReader br) {
            v0 = br.ReadUInt32();
            v4 = br.ReadSingle();
            v8 = br.ReadSingle();
            vc = br.ReadSingle();
        }
    }

    public class Pc69 { // 12 bytes
        public int v00, v04, v08;

        public Pc69(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
        }
    }

    public class PcD0 { // 268 bytes
        public int v00, v04;
        public byte v08, v09, v0a, v0b, v0c, v0d;
        public ushort v0e;
        public float v10, v14, v18;
        public ushort v1c, v1e, v20, v22;
        public byte v24, v25, v26, v27;
        public float v28, v2c, v30, v34,
            v38, v3c, v40, v44;
        public byte v48, v49, v4a, v4b;
        public int v4c, v50, v54, v58, v5c, v60, v64, v68, v6c;
        public Vector4[] v70 = new Vector4[9];

        public byte w00, w01, w02, w03, w04, w05, w06, w07;
        public ushort w08;
        public byte w0a, w0b;

        public PcD0(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadByte();
            v09 = br.ReadByte();
            v0a = br.ReadByte();
            v0b = br.ReadByte();
            v0c = br.ReadByte();
            v0d = br.ReadByte();
            v0e = br.ReadUInt16();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadSingle();
            v1c = br.ReadUInt16();
            v1e = br.ReadUInt16();
            v20 = br.ReadUInt16();
            v22 = br.ReadUInt16();
            v24 = br.ReadByte();
            v25 = br.ReadByte();
            v26 = br.ReadByte();
            v27 = br.ReadByte();
            v28 = br.ReadSingle();
            v2c = br.ReadSingle();
            v30 = br.ReadSingle();
            v34 = br.ReadSingle();
            v38 = br.ReadSingle();
            v3c = br.ReadSingle();
            v40 = br.ReadSingle();
            v44 = br.ReadSingle();
            v48 = br.ReadByte();
            v49 = br.ReadByte();
            v4a = br.ReadByte();
            v4b = br.ReadByte();
            v4c = br.ReadInt32();
            v50 = br.ReadInt32();
            v54 = br.ReadInt32();
            v58 = br.ReadInt32();
            v5c = br.ReadInt32();
            v60 = br.ReadInt32();
            v64 = br.ReadInt32();
            v68 = br.ReadInt32();
            v6c = br.ReadInt32();
            for (int x = 0; x < 9; x++) v70[x] = Ut.ReadV4(br);
            w00 = br.ReadByte();
            w01 = br.ReadByte();
            w02 = br.ReadByte();
            w03 = br.ReadByte();
            w04 = br.ReadByte();
            w05 = br.ReadByte();
            w06 = br.ReadByte();
            w07 = br.ReadByte();
            w08 = br.ReadUInt16();
            w0a = br.ReadByte();
            w0b = br.ReadByte();
        }

        class Ut {
            public static Vector4 ReadV4(BinaryReader br) {
                Vector4 v = new Vector4();
                v.X = br.ReadSingle();
                v.Y = br.ReadSingle();
                v.Z = br.ReadSingle();
                v.W = br.ReadSingle();
                return v;
            }
        }
    }

    public class Pc8E { // 20 bytes 
        public int v00, v04;
        public ushort v08;
        public byte v0c, v0d, v0e, v0f, v10;

        public Pc8E(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadUInt16();
            v0c = br.ReadByte();
            v0d = br.ReadByte();
            v0e = br.ReadByte();
            v0f = br.ReadByte();
            v10 = br.ReadByte();
        }
    }

    public class Pc7D { // 32 bytes 
        public int v00;
        public float v04, v08, v0c, v10, v14, v18;
        public byte v1c;

        public Pc7D(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadSingle();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadSingle();
            v1c = br.ReadByte();
        }
    }

    public class Pc8B { // 40 bytes 
        public int v00, v04;
        public float v08, v0c, v10, v14, v18, v1c;
        public byte v20, v21, v22, v23, v24, v25;

        public Pc8B(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadSingle();
            v1c = br.ReadSingle();
            v20 = br.ReadByte();
            v21 = br.ReadByte();
            v22 = br.ReadByte();
            v23 = br.ReadByte();
            v24 = br.ReadByte();
            v25 = br.ReadByte();
        }
    }

    public class PcF1 { // 164 bytes
        public int v00, v04;
        public byte v08, v09, v0a, v0b;
        public float v0c;
        public byte v10, v11, v12, v13,
            v14, v15, v16, v17;
        public float v18;
        public byte v1c, v1d, v1e;

        public float v90, v94, v98;
        public byte v9c, v9d, v9e,
            va0, va1;

        public PcF1(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadByte();
            v09 = br.ReadByte();
            v0a = br.ReadByte();
            v0b = br.ReadByte();
            v0c = br.ReadSingle();
            v10 = br.ReadByte();
            v11 = br.ReadByte();
            v12 = br.ReadByte();
            v13 = br.ReadByte();
            v14 = br.ReadByte();
            v15 = br.ReadByte();
            v16 = br.ReadByte();
            v17 = br.ReadByte();
            v18 = br.ReadSingle();
            v1c = br.ReadByte();
            v1d = br.ReadByte();
            v1e = br.ReadByte();
            br.ReadBytes(113);
            v90 = br.ReadSingle();
            v94 = br.ReadSingle();
            v98 = br.ReadSingle();
            v9c = br.ReadByte();
            v9d = br.ReadByte();
            v9e = br.ReadByte();
            br.ReadByte();
            va0 = br.ReadByte();
            va1 = br.ReadByte();
        }
    }

    public class Pc6A { // 16 bytes
        public int v00, v04, v08, v0c;

        public Pc6A(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
            v0c = br.ReadInt32();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} {3:x8}", v00, v04, v08, v0c);
        }
    }

    public class PcAE { // 20 bytes
        public int v00, v04, v06, v07, v08;

        public PcAE(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadUInt16();
            v06 = br.ReadByte();
            v07 = br.ReadByte();
            v08 = br.ReadByte();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x4} {2:x2} {3:x2} {4:x2}", v00, v04, v06, v07, v08);
        }
    }

    public class Pc1D { // 16 bytes
        public int v00, v04, v08, v0c;

        public Pc1D(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
            v0c = br.ReadInt32();
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3}", v00, v04, v08, v0c);
        }
    }

    public class Pc1E { // 16 bytes
        public int v00;
        public float v04, v08, v0c;

        public Pc1E(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadSingle();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1} {2} {3}", v00, v04, v08, v0c);
        }
    }

    public class Pc89 { // 16 bytes
        public int v0, v4, v8, v9, va, vb, vc, vd, ve, vf;

        public Pc89(BinaryReader br) {
            v0 = br.ReadInt32();
            v4 = br.ReadInt32();
            v8 = br.ReadByte();
            v9 = br.ReadByte();
            va = br.ReadByte();
            vb = br.ReadByte();
            vc = br.ReadByte();
            vd = br.ReadByte();
            ve = br.ReadByte();
            vf = br.ReadByte();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x2} {3:x2} {4:x2} {5:x2} {6:x2} {7:x2} {8:x2} {9:x2}"
                , v0, v4, v8, v9, va, vb, vc, vd, ve, vf);
        }
    }

    public class Pc7c { // 32 bytes
        public float v00, v04, v08, v0c, v10, v14, v18, v1c;

        public Pc7c(BinaryReader br) {
            v00 = br.ReadSingle();
            v04 = br.ReadSingle();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
            v10 = br.ReadSingle();
            v14 = br.ReadSingle();
            v18 = br.ReadSingle();
            v1c = br.ReadSingle();
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", v00, v04, v08, v0c, v10, v14, v18, v1c);
        }
    }

    public class Pc57 { // 4 bytes
        public int v00;

        public Pc57(BinaryReader br) {
            v00 = br.ReadInt32();
        }

        public override string ToString() {
            return String.Format("{0:x8}", v00);
        }
    }

    public class Pc28 { // 16 bytes
        public float v00, v04, v08, v0c;

        public Pc28(BinaryReader br) {
            v00 = br.ReadSingle();
            v04 = br.ReadSingle();
            v08 = br.ReadSingle();
            v0c = br.ReadSingle();
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3}", v00, v04, v08, v0c);
        }
    }

    public class Pc1F { // 12 bytes
        public int v00, v04, v06, v08, v0a;

        public Pc1F(BinaryReader br) {
            v00 = br.ReadInt32();
            v04 = br.ReadInt16();
            v06 = br.ReadInt16();
            v08 = br.ReadInt16();
            v0a = br.ReadInt16();
        }

        public override string ToString() {
            return String.Format("{0:x8} {1} {2} {3} {4}", v00, v04, v06, v08, v0a);
        }
    }

    public class P14 : IRefUtil { // 16 bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 16; } }
        public String Name { get { return "P14"; } }

        public int v00 { get { si.Position = baseoff + 0x0; return br.ReadInt32(); } }
        public ushort v04 { get { si.Position = baseoff + 0x4; return br.ReadUInt16(); } }
        public ushort v06 { get { si.Position = baseoff + 0x6; return br.ReadUInt16(); } }
        public int v08 { get { si.Position = baseoff + 0x8; return br.ReadInt32(); } }
        public int v0c { get { si.Position = baseoff + 0xc; return br.ReadInt32(); } }

        public int ChunkType { get { return v00; } }
        public int ChunkStride { get { return v04; } }
        public int ChunkCount { get { return v06; } }
        public int ChunkOffset { get { return v08; } }

        public P14(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }

        public override string ToString() {
            return String.Format("{0:x8} {1,2} {2,2} {3:x8} {4:x8}", v00, v04, v06, v08, v0c);
        }
    }

    public class P13 : IRefUtil { // 38 bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 38; } }
        public String Name { get { return "P13"; } }

        public int v00 { get { si.Position = baseoff + 0x00; return br.ReadInt32(); } }
        public int v04 { get { si.Position = baseoff + 0x04; return br.ReadInt32(); } }
        public int v08 { get { si.Position = baseoff + 0x08; return br.ReadInt32(); } }
        public int v0c { get { si.Position = baseoff + 0x0c; return br.ReadInt32(); } }

        public int v10 { get { si.Position = baseoff + 0x10; return br.ReadInt32(); } }
        public int v14 { get { si.Position = baseoff + 0x14; return br.ReadInt32(); } }
        public int v18 { get { si.Position = baseoff + 0x18; return br.ReadInt32(); } }
        public int v1c { get { si.Position = baseoff + 0x1c; return br.ReadInt32(); } }

        public int v20 { get { si.Position = baseoff + 0x20; return br.ReadInt32(); } }
        public ushort v24 { get { si.Position = baseoff + 0x24; return br.ReadUInt16(); } }

        P12 o12;

        public P13(BinaryReader br, Int64 baseoff, P12 o12) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
            this.o12 = o12;

            Read();
        }

        void Read() {
            foreach (P14 o14 in P14s) {
                Int64 off = o12.Off + o14.ChunkOffset;
                switch (o14.ChunkType) {
                    case 0x1C:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al1c.Add(new Pc1E(br));
                        }
                        break;
                    case 0x1D:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al1d.Add(new Pc1D(br));
                        }
                        break;
                    case 0x1E:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al1e.Add(new Pc1E(br));
                        }
                        break;
                    case 0x1F:
                        Trace.Assert(o14.ChunkStride == 12);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al1f.Add(new Pc1F(br));
                        }
                        break;
                    case 0x24:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al24.Add(new Pc1E(br));
                        }
                        break;
                    case 0x25:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al25.Add(new Pc1D(br));
                        }
                        break;
                    case 0x26:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al26.Add(new Pc1E(br));
                        }
                        break;
                    case 0x27:
                        Trace.Assert(o14.ChunkStride == 12);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al27.Add(new Pc1F(br));
                        }
                        break;
                    case 0x28:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al28.Add(new Pc28(br));
                        }
                        break;
                    case 0x29:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al29.Add(new Pc6A(br));
                        }
                        break;
                    case 0x2A:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al2a.Add(new Pc1E(br));
                        }
                        break;
                    case 0x2B:
                        Trace.Assert(o14.ChunkStride == 12);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al2b.Add(new Pc1F(br));
                        }
                        break;
                    case 0x57:
                        Trace.Assert(o14.ChunkStride == 4);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al57.Add(new Pc57(br));
                        }
                        break;
                    case 0x6a:
                        Trace.Assert(o14.ChunkStride == 12);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al6a.Add(new Pc6A(br));
                        }
                        break;
                    case 0x70:
                        Trace.Assert(o14.ChunkStride == 4);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al70.Add(new Pc57(br));
                        }
                        break;
                    case 0x7c:
                        Trace.Assert(o14.ChunkStride == 32);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al7c.Add(new Pc7c(br));
                        }
                        break;
                    case 0x89:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al89.Add(new Pc89(br));
                        }
                        break;
                    case 0xAE:
                        Trace.Assert(o14.ChunkStride == 20);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            alae.Add(new PcAE(br));
                        }
                        break;
                    case 0xF1:
                        Trace.Assert(o14.ChunkStride == 164);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            alf1.Add(new PcF1(br));
                        }
                        break;
                    case 0x8B:
                        Trace.Assert(o14.ChunkStride == 40);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al8b.Add(new Pc8B(br));
                        }
                        break;
                    case 0x5E:
                        Trace.Assert(o14.ChunkStride == 4);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al5e.Add(new Pc57(br));
                        }
                        break;
                    case 0x71:
                        Trace.Assert(o14.ChunkStride == 4);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al71.Add(new Pc57(br));
                        }
                        break;
                    case 0x7D:
                        Trace.Assert(o14.ChunkStride == 32);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al7d.Add(new Pc7D(br));
                        }
                        break;
                    case 0x8E:
                        Trace.Assert(o14.ChunkStride == 20);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al8e.Add(new Pc8E(br));
                        }
                        break;
                    case 0x54:
                        Trace.Assert(o14.ChunkStride == 4);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al54.Add(new Pc57(br));
                        }
                        break;
                    case 0xD0:
                        Trace.Assert(o14.ChunkStride == 268);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            ald0.Add(new PcD0(br));
                        }
                        break;
                    case 0x69:
                        Trace.Assert(o14.ChunkStride == 12);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al69.Add(new Pc69(br));
                        }
                        break;
                    case 0xD4:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            ald4.Add(new PcD4(br));
                        }
                        break;
                    case 0xC9:
                        Trace.Assert(o14.ChunkStride == 220);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            alc9.Add(new PcC9(br));
                        }
                        break;
                    case 0xD7:
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            ald7.Add(new PcD7(br));
                        }
                        break;
                    case 0xDB:
                        Trace.Assert(o14.ChunkStride == 40);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            aldb.Add(new PcDB(br));
                        }
                        break;
                    case 0x9D:
                        Trace.Assert(o14.ChunkStride == 88);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al9d.Add(new Pc9D(br));
                        }
                        break;
                    case 0x3F: // 0000003f 24  1 000004b4 000004d0 ... 000030d4
                        Trace.Assert(o14.ChunkStride == 24);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al3f.Add(new Pc3F(br));
                        }
                        break;
                    case 0x31: // 00000031 24  7 000003e0 0000048c ... 00003000
                        Trace.Assert(o14.ChunkStride == 24);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al31.Add(new Pc31(br));
                        }
                        break;
                    case 0x42: // 00000042 24  1 000011ec 00001208 ... 00003e0c
                        Trace.Assert(o14.ChunkStride == 24);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al42.Add(new Pc42(br));
                        }
                        break;
                    case 0x8A: // 0000008a 16  1 000004d8 000004ec ... @ 00000808
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al8a.Add(new Pc8A(br));
                        }
                        break;
                    case 0xB1: // 000000b1 20  2 00001d58 00001d84 ... @ 00049538
                        Trace.Assert(o14.ChunkStride == 20);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            alb1.Add(new PcB1(br));
                        }
                        break;
                    case 0xD3: // 000000d3 16  1 0000060c 00000620 ... @ 0006597c
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            ald3.Add(new PcD3(br));
                        }
                        break;
                    case 0xA0: // 000000a0 28  1 0000171c 0000173c ... @ 0006e93c
                        Trace.Assert(o14.ChunkStride == 28);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            ala0.Add(new PcA0(br));
                        }
                        break;
                    case 0x33: // 00000033 16  1 00000420 00000434 ... @ 00025600
                        Trace.Assert(o14.ChunkStride == 16);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            al33.Add(new Pc33(br));
                        }
                        break;
                    case 0xB8: // 000000b8 28  2 00000150 0000018c ... @ 00031010
                        Trace.Assert(o14.ChunkStride == 28);
                        for (int a = 0; a < o14.ChunkCount; a++, off += o14.ChunkStride) {
                            si.Position = off;
                            alb8.Add(new PcB8(br));
                        }
                        break;

                    case 0xD5:
                    case 0xD6:
                    case 0xD8:
                    case 0xD9:
                    case 0x73:
                    case 0x72:
                    case 0xB7: // 000000b7 32  1 000001b0 000001d4 ... 00002dd0
                    case 0xC8: // 000000c8 20  1 000000ec 00000104 ... 00002d0c
                    case 0xCA: // 000000ca  8  1 00000048 00000054 ... 00002c68
                    case 0x5D: // 0000005d  4  1 0000085c 00000864 ... @ 0006da7c
                    case 0x60: // 00000060  4  1 00000868 00000870 ... @ 0006da88
                    case 0x75: // 00000075  4  1 0000170c 00001714 ... @ 0006e92c
                    case 0x5A: // 0000005a  4  1 000002b8 000002c0 ... @ 00001c08
                    case 0x74: // 00000074  4  1 0000048c 00000494 ... @ 0000570c
                    case 0xEE: // 000000ee 32  1 0000049c 000004c0 ... @ 0000571c
                    case 0x5B: // 0000005b  4  1 0000080c 00000814 ... @ 00005a8c
                    case 0x78: // 00000078  4  1 00000484 0000048c ... @ 00025664
                    case 0xD2: // 000000d2 16  1 00000548 0000055c ... @ 00003758
                        break;

                    default:
                        throw new NotSupportedException(o14 + " ... @ " + (o12.Off + o14.ChunkOffset).ToString("x8"));
                }
            }
        }

        public List<P14> P14s {
            get {
                List<P14> al = new List<P14>();
                si.Position = baseoff + 0x26;
                int cx = br.ReadUInt16();
                for (int x = 0; x < cx; x++) {
                    al.Add(new P14(br, baseoff + 0x26 + 2 + 16 * x));
                }
                return al;
            }
        }

        public override string ToString() {
            return String.Format("{0:x8} {1:x8} {2:x8} {3} {4:x8} {5:x8} {6:x8} {7:x8} {8:x8} {9:x4}"
                , v00, v04, v08, v0c
                , v10, v14, v18, v1c
                , v20, v24);
        }

        public List<Pc1F> al1f = new List<Pc1F>();
        public List<Pc1F> al27 = new List<Pc1F>();
        public List<Pc28> al28 = new List<Pc28>();
        public List<Pc6A> al29 = new List<Pc6A>();
        public List<Pc1E> al2a = new List<Pc1E>();
        public List<Pc1F> al2b = new List<Pc1F>();
        public List<Pc57> al57 = new List<Pc57>();
        public List<Pc57> al70 = new List<Pc57>();
        public List<Pc7c> al7c = new List<Pc7c>();
        public List<Pc89> al89 = new List<Pc89>();
        public List<Pc1E> al1e = new List<Pc1E>();
        public List<Pc1E> al26 = new List<Pc1E>();
        public List<Pc1E> al1c = new List<Pc1E>();
        public List<Pc1D> al1d = new List<Pc1D>();
        public List<Pc1E> al24 = new List<Pc1E>();
        public List<Pc1D> al25 = new List<Pc1D>();
        public List<PcAE> alae = new List<PcAE>();
        public List<Pc6A> al6a = new List<Pc6A>();
        public List<PcF1> alf1 = new List<PcF1>();
        public List<Pc8B> al8b = new List<Pc8B>();
        public List<Pc57> al5e = new List<Pc57>();
        public List<Pc57> al71 = new List<Pc57>();
        public List<Pc7D> al7d = new List<Pc7D>();
        public List<Pc8E> al8e = new List<Pc8E>();
        public List<Pc57> al54 = new List<Pc57>();
        public List<PcD0> ald0 = new List<PcD0>();
        public List<Pc69> al69 = new List<Pc69>();
        public List<PcD4> ald4 = new List<PcD4>();
        public List<PcC9> alc9 = new List<PcC9>();
        public List<PcD7> ald7 = new List<PcD7>();
        public List<PcDB> aldb = new List<PcDB>();
        public List<Pc9D> al9d = new List<Pc9D>();
        public List<Pc3F> al3f = new List<Pc3F>();
        public List<Pc31> al31 = new List<Pc31>();
        public List<Pc42> al42 = new List<Pc42>();
        public List<Pc8A> al8a = new List<Pc8A>();
        public List<PcB1> alb1 = new List<PcB1>();
        public List<PcD3> ald3 = new List<PcD3>();
        public List<PcA0> ala0 = new List<PcA0>();
        public List<Pc33> al33 = new List<Pc33>();
        public List<PcB8> alb8 = new List<PcB8>();
    }

    public class P12 : IRefUtil { // ? bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 16; } }
        public String Name { get { return "P12"; } }

        public P12(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }

        public List<P13> P13s {
            get {
                List<P13> al = new List<P13>();
                int pos = 0x10;
                while (true) {
                    si.Position = baseoff + pos;
                    int npos = br.ReadInt32();
                    if (npos == 0) break;
                    al.Add(new P13(br, baseoff + pos, this));
                    pos = npos;
                }
                return al;
            }
        }
    }

    public class P51 : IRefUtil { // ? bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 0; } }
        public String Name { get { return "P51"; } }

        public P51(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }
    }

    public class P31 : IRefUtil { // ? bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 0; } }
        public String Name { get { return "P31"; } }

        public P31(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }
    }

    /// <summary>
    /// Texture
    /// </summary>
    public class P21 : IRefUtil { // ? bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 0; } }
        public String Name { get { return "P21"; } }

        public P21(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }
    }

    public class P11 : IRefUtil { // 176 bytes
        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 176; } }
        public String Name { get { return "P11"; } }

        public class Vx : IRefUtil {
            BinaryReader br;
            Int64 baseoff;
            Stream si;

            public Int64 Off { get { return baseoff; } }
            public Int64 Len { get { return 16; } }
            public String Name { get { return "Vx"; } }

            public short x0 { get { si.Position = baseoff + 0; return br.ReadInt16(); } }
            public short y0 { get { si.Position = baseoff + 2; return br.ReadInt16(); } }
            public short x1 { get { si.Position = baseoff + 4; return br.ReadInt16(); } }
            public short y1 { get { si.Position = baseoff + 6; return br.ReadInt16(); } }
            public short x2 { get { si.Position = baseoff + 8; return br.ReadInt16(); } }
            public short y2 { get { si.Position = baseoff + 10; return br.ReadInt16(); } }
            public float w { get { si.Position = baseoff + 12; return br.ReadSingle(); } }

            public Vx(BinaryReader br, Int64 baseoff) {
                this.br = br;
                this.baseoff = baseoff;
                this.si = br.BaseStream;
            }

            public override string ToString() {
                return String.Format("{0} {1} {2} {3} {4} {5} {6}"
                    , x0, y0, x1, y1, x2, y2, w);
            }
        }

        public Vx[] v00 {
            get {
                Vx[] al = new Vx[8];
                for (int x = 0; x < 8; x++) al[x] = new Vx(br, baseoff + 16 * x);
                return al;
            }
        }

        public Vector4 v80 { get { si.Position = baseoff + 0x80; return Ut.ReadV4(br); } }
        public Vector4 v90 { get { si.Position = baseoff + 0x90; return Ut.ReadV4(br); } }
        public Vector4 va0 { get { si.Position = baseoff + 0xa0; return Ut.ReadV4(br); } }
        public byte[] vb0 { get { si.Position = baseoff + 0xb0; return br.ReadBytes(0x60); } }

        public P11(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }

        public P12 P12 { get { return new P12(br, baseoff + 0x110); } }

        class Ut {
            public static Matrix ReadMatrix(BinaryReader br) {
                Matrix M = new Matrix();
                M.M11 = br.ReadSingle(); M.M12 = br.ReadSingle(); M.M13 = br.ReadSingle(); M.M14 = br.ReadSingle();
                M.M21 = br.ReadSingle(); M.M22 = br.ReadSingle(); M.M23 = br.ReadSingle(); M.M24 = br.ReadSingle();
                M.M31 = br.ReadSingle(); M.M32 = br.ReadSingle(); M.M33 = br.ReadSingle(); M.M34 = br.ReadSingle();
                M.M41 = br.ReadSingle(); M.M42 = br.ReadSingle(); M.M43 = br.ReadSingle(); M.M44 = br.ReadSingle();
                return M;
            }

            public static Vector4 ReadV4(BinaryReader br) {
                Vector4 v = new Vector4();
                v.X = br.ReadSingle();
                v.Y = br.ReadSingle();
                v.Z = br.ReadSingle();
                v.W = br.ReadSingle();
                return v;
            }
        }
    }

    public class P2 : IRefUtil { // 32 bytes
        public int v00 { get { si.Position = baseoff + 0x00; return br.ReadInt32(); } }
        public int v04 { get { si.Position = baseoff + 0x04; return br.ReadInt32(); } }
        public int v08 { get { si.Position = baseoff + 0x08; return br.ReadInt32(); } }
        public int v0c { get { si.Position = baseoff + 0x0c; return br.ReadInt32(); } }
        public int v10 { get { si.Position = baseoff + 0x10; return br.ReadInt32(); } }
        public int v14 { get { si.Position = baseoff + 0x14; return br.ReadInt32(); } }
        public int v18 { get { si.Position = baseoff + 0x18; return br.ReadInt32(); } }
        public int v1c { get { si.Position = baseoff + 0x1c; return br.ReadInt32(); } }

        BinaryReader br;
        Int64 baseoff;
        Stream si;
        Po82 o82;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 32; } }
        public String Name { get { return "P2"; } }

        public P2(BinaryReader br, Int64 baseoff, Po82 o82) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
            this.o82 = o82;
        }

        public Po96 Po96 {
            get {
                si.Position = baseoff;
                int offt96 = br.ReadInt32();
                return new Po96(br, o82.Off + offt96);
            }
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}"
                , v00, v08, v08, v0c, v10, v14, v18, v1c);
        }
    }

    public class P1 : IRefUtil { // 80 bytes
        public ushort v00 { get { si.Position = baseoff + 0; return br.ReadUInt16(); } }
        public ushort v02 { get { si.Position = baseoff + 2; return br.ReadUInt16(); } }
        public ushort v04 { get { si.Position = baseoff + 4; return br.ReadUInt16(); } }
        public ushort v06 { get { si.Position = baseoff + 6; return br.ReadUInt16(); } }

        public int v08 { get { si.Position = baseoff + 0x08; return br.ReadInt32(); } }
        public int v0c { get { si.Position = baseoff + 0x0C; return br.ReadInt32(); } }
        public int v10 { get { si.Position = baseoff + 0x10; return br.ReadInt32(); } }
        public int v14 { get { si.Position = baseoff + 0x14; return br.ReadInt32(); } }
        public int v18 { get { si.Position = baseoff + 0x18; return br.ReadInt32(); } }

        public float v1c { get { si.Position = baseoff + 0x1C; return br.ReadSingle(); } }
        public float v20 { get { si.Position = baseoff + 0x20; return br.ReadSingle(); } }
        public float v24 { get { si.Position = baseoff + 0x24; return br.ReadSingle(); } }
        public float v28 { get { si.Position = baseoff + 0x28; return br.ReadSingle(); } }
        public float v2c { get { si.Position = baseoff + 0x2C; return br.ReadSingle(); } }
        public float v30 { get { si.Position = baseoff + 0x30; return br.ReadSingle(); } }
        public float v34 { get { si.Position = baseoff + 0x34; return br.ReadSingle(); } }
        public float v38 { get { si.Position = baseoff + 0x38; return br.ReadSingle(); } }
        public float v3c { get { si.Position = baseoff + 0x3C; return br.ReadSingle(); } }

        public int v40 { get { si.Position = baseoff + 0x40; return br.ReadInt32(); } }
        public int v44 { get { si.Position = baseoff + 0x44; return br.ReadInt32(); } }
        public int v48 { get { si.Position = baseoff + 0x48; return br.ReadInt32(); } }
        public int v4c { get { si.Position = baseoff + 0x4c; return br.ReadInt32(); } }

        BinaryReader br;
        Int64 baseoff;
        Stream si;

        public Int64 Off { get { return baseoff; } }
        public Int64 Len { get { return 80; } }
        public String Name { get { return "P1"; } }

        public P1(BinaryReader br, Int64 baseoff) {
            this.br = br;
            this.baseoff = baseoff;
            this.si = br.BaseStream;
        }

        public override string ToString() {
            return String.Format("{0} {1} {2} {3} {4} {5} {6:x8} {7} {8}"
                , v00, v02, v04, v06, v08, v0c, v10, v14, v18)
                + String.Format(" {0} {1} {2} {3} {4} {5} {6} {7}"
                , v20, v24, v28, v2c, v30, v34, v38, v3c)
                + String.Format(" {0:x8} {1:x8} {2:x8} {3:x8}"
                , v40, v44, v48, v4c)
                ;
        }
    }
}
namespace parseSEQD.Binuts {
    public class BinutElementCollection : List<BinutElement> {
        public IDictionary<String, BinutElement> GetDictionary() {
            SortedDictionary<String, BinutElement> dict = new SortedDictionary<String, BinutElement>();
            foreach (BinutElement o in this) {
                dict[o.Name] = o;
            }
            return dict;
        }
    }
    public class BinutScope : IDisposable {
        BinutElementCollection bc;
        BinutScope parent = null;
        List<BinutScope> alchildren = new List<BinutScope>();

        public BinutScope(BinutElementCollection bc) {
            this.bc = bc;
        }
        public BinutScope(BinutElementCollection bc, string name) {
            this.bc = bc;

            this.name = (name);
        }
        public BinutScope(BinutScope parent) {
            parent.alchildren.Add(this);
            this.parent = parent;
            this.bc = parent.bc;
        }
        public BinutScope(BinutScope parent, string name) {
            parent.alchildren.Add(this);
            this.parent = parent;
            this.bc = parent.bc;

            this.name = (name);
        }
        public BinutScope(BinutScope parent, string name, int offset, int stride, int length) {
            parent.alchildren.Add(this);
            this.parent = parent;
            this.bc = parent.bc;

            this.name = (name);
            this.offset = offset;
            this.stride = stride;
            this.length = length;
        }

        int offset = 0, stride = 0, length = 0;
        string name = "?";
        bool enabled = true;
        bool hascommit = false;
        bool enlarge = true;

        public int Offset { get { return offset; } set { offset = value; } }
        public int Length { get { return length; } set { length = value; } }
        public int Stride { get { return stride; } set { stride = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool Enabled { get { return enabled; } set { enabled = value; } }
        public bool HasCommitted { get { return hascommit; } set { hascommit = value; } }
        public bool EnlargeRange { get { return enlarge; } set { enlarge = value; } }

        public void Commit() {
            if (HasCommitted) return;

            int x0 = Offset;
            int x1 = x0 + Length;

            if (enlarge) {
                foreach (BinutScope bs in alchildren) {
                    int t0 = bs.Offset;
                    int t1 = t0 + bs.Length;
                    if (bs.Enabled && t0 != t1) {
                        if (x0 == x1) {
                            x0 = t0;
                            x1 = t1;
                        }
                        else {
                            x0 = Math.Min(x0, t0);
                            x1 = Math.Max(x1, t1);
                        }
                    }
                }
            }

            if (Name != null) {
                bc.Add(new BinutElement(x0, Stride, x1 - x0, GetFullName(Name)));
            }

            if (enlarge) {
                Offset = x0;
                Length = x1 - x0;
            }

            HasCommitted = true;
        }

        public String GetFullName(String local) {
            String s = local;
            BinutScope bs = this.parent;
            while (bs != null) {
                s = bs.name + "/" + s;
                bs = bs.parent;
            }
            return s;
        }

        #region IDisposable メンバ

        public void Dispose() {
            Commit();
        }

        #endregion
    }
    public class BinutElement {
        int x, cx, pitch;
        string name;

        public BinutElement(int offset, int stride, int length, string name) {
            this.x = offset;
            this.cx = length;
            this.pitch = stride;
            this.name = name;
        }

        public int Offset { get { return x; } set { x = value; } }
        public int Length { get { return cx; } set { cx = value; } }
        public int Stride { get { return pitch; } set { pitch = value; } }
        public String Name { get { return name; } set { name = value; } }
    }
}