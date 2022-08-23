using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kh2fmarc {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpya();
            }
            switch (args[0].ToLowerInvariant()) {
                default:
                    helpya();
                    return;
                case "explode":
                    if (args.Length < 3) helpya(); {
                        Directory.CreateDirectory(args[2]);
                        List<Idx> alidx = Readidx(Path.Combine(args[1], "kh2.idx"));
                        using (FileStream fs = File.OpenRead(Path.Combine(args[1], "kh2.img"))) {
                            foreach (Idx v in alidx) {
                                Console.WriteLine(v);
                                fs.Position = 2048 * v.sec;
                                if (v.IsCompressed) {
                                    byte[] encoded = new BinaryReader(fs).ReadBytes((int)v.CompressedSize);
                                    byte[] decoded = o2efcc0.explode(encoded, (int)v.UncompressedSize);
                                    File.WriteAllBytes(Path.Combine(args[2], v.crc.ToString("x8") + ".output.bin"), decoded);
                                }
                                else {
                                    byte[] contents = new BinaryReader(fs).ReadBytes((int)v.UncompressedSize);
                                    File.WriteAllBytes(Path.Combine(args[2], v.crc.ToString("x8") + ".output.bin"), contents);
                                }
                            }
                        }
                        break;
                    }
                case "extract":
                    if (args.Length < 3) helpya(); {
                        Directory.CreateDirectory(args[2]);
                        List<Idx> alidx = Readidx(Path.Combine(args[1], "kh2.idx"));
                        using (FileStream fs = File.OpenRead(Path.Combine(args[1], "kh2.img"))) {
                            for (int t = 3; t < args.Length; t++) {
                                Console.WriteLine(args[t]);
                                uint digest = o1ac6c8.calc(Encoding.ASCII.GetBytes(args[t] + "\0"));
                                foreach (Idx v in alidx) {
                                    if (digest != v.crc) continue;
                                    Console.WriteLine(v);
                                    fs.Position = 2048 * v.sec;
                                    String fpinto = Path.Combine(args[2], args[t]);
                                    Directory.CreateDirectory(Path.GetDirectoryName(fpinto));
                                    if (v.IsCompressed) {
                                        byte[] encoded = new BinaryReader(fs).ReadBytes((int)v.CompressedSize);
                                        byte[] decoded = o2efcc0.explode(encoded, (int)v.UncompressedSize);
                                        File.WriteAllBytes(fpinto, decoded);
                                    }
                                    else {
                                        byte[] contents = new BinaryReader(fs).ReadBytes((int)v.UncompressedSize);
                                        File.WriteAllBytes(fpinto, contents);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case "extract_w_idx":
                    if (args.Length < 3) helpya(); {
                        Directory.CreateDirectory(args[2]);
                        List<Idx> alidx = Readidx(Path.Combine(args[1], "kh2.idx"));
                        foreach (String fpidx in Directory.GetFiles(args[2], "*.idx")) {
                            alidx.AddRange(Readidx(fpidx));
                        }
                        using (FileStream fs = File.OpenRead(Path.Combine(args[1], "kh2.img"))) {
                            for (int t = 3; t < args.Length; t++) {
                                Console.WriteLine(args[t]);
                                uint[] digests = Guess.More(args[t] + "\0");
                                foreach (Idx v in alidx) {
                                    if (Array.IndexOf<uint>(digests, v.crc) < 0) continue;
                                    Console.WriteLine(v);
                                    fs.Position = 2048 * v.sec;
                                    String fpinto = Path.Combine(args[2], args[t].Replace('/', Path.DirectorySeparatorChar));
                                    Directory.CreateDirectory(Path.GetDirectoryName(fpinto));
                                    if (v.IsCompressed) {
                                        byte[] encoded = new BinaryReader(fs).ReadBytes((int)v.CompressedSize);
                                        byte[] decoded = o2efcc0.explode(encoded, (int)v.UncompressedSize);
                                        File.WriteAllBytes(fpinto, decoded);
                                        File.WriteAllBytes(fpinto + ".raw", encoded);
                                    }
                                    else {
                                        byte[] contents = new BinaryReader(fs).ReadBytes((int)v.UncompressedSize);
                                        File.WriteAllBytes(fpinto, contents);
                                    }
                                }
                            }
                        }
                        break;
                    }
                case "extract_w_idx_filelist":
                    if (args.Length < 3) helpya(); {
                        Directory.CreateDirectory(args[2]);
                        List<Idx> alidx = Readidx(Path.Combine(args[1], "kh2.idx"));
                        foreach (String fpidx in Directory.GetFiles(args[2], "*.idx")) {
                            alidx.AddRange(Readidx(fpidx));
                        }
                        using (FileStream fs = File.OpenRead(Path.Combine(args[1], "kh2.img"))) {
                            for (int t = 3; t < args.Length; t++) {
                                if (File.Exists(args[t])) {
                                    foreach (String row1 in File.ReadAllLines(args[t], Encoding.ASCII)) {
                                        String row = row1.Trim();
                                        if (row.StartsWith("#") || row.StartsWith(";") || row.Length == 0)
                                            continue;
                                        Console.WriteLine(row);
                                        uint[] digests = Guess.More(row + "\0");
                                        foreach (Idx v in alidx) {
                                            if (Array.IndexOf<uint>(digests, v.crc) < 0) continue;
                                            Console.WriteLine(v);
                                            fs.Position = 2048 * v.sec;
                                            String fpinto = Path.Combine(args[2], row.Replace('/', Path.DirectorySeparatorChar));
                                            Directory.CreateDirectory(Path.GetDirectoryName(fpinto));
                                            if (v.IsCompressed) {
                                                byte[] encoded = new BinaryReader(fs).ReadBytes((int)v.CompressedSize);
                                                byte[] decoded = o2efcc0.explode(encoded, (int)v.UncompressedSize);
                                                File.WriteAllBytes(fpinto, decoded);
                                            }
                                            else {
                                                byte[] contents = new BinaryReader(fs).ReadBytes((int)v.UncompressedSize);
                                                File.WriteAllBytes(fpinto, contents);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
        }

        class Guess {
            public static uint[] More(string fp) {
                List<int> alp = new List<int>();
                {
                    int x = 0;
                    while (true) {
                        int p = fp.IndexOf('_', x);
                        if (p < 0) break;
                        alp.Add(p);
                        x = p + 1;
                    }
                }
                int cx = 1 << alp.Count;
                List<uint> digests = new List<uint>();
                char[] ca = fp.ToCharArray();
                int ct = alp.Count;
                for (int x = 0; x < cx; x++) {
                    for (int t = 0; t < ct; t++) {
                        ca[alp[t]] = (0 != (x & (1 << t))) ? '/' : '_';
                    }
                    digests.Add(o1ac6c8.calc(Encoding.ASCII.GetBytes(ca)));
                }
                return digests.ToArray();
            }
        }

        static void helpya() {
            Console.Error.WriteLine("Usage:");
            Console.Error.WriteLine(@" kh2fmarc explode P:\ C:\kh2fm.contents");
            Console.Error.WriteLine(@" kh2fmarc extract P:\ C:\kh2fm.contents 000tt.idx");
            Console.Error.WriteLine(@" kh2fmarc extract_w_idx P:\ C:\kh2fm.contents map_jp_tt01.map");
            Console.Error.WriteLine(@" kh2fmarc extract_w_idx_filelist P:\ C:\kh2fm.contents filelist.txt");
            Environment.Exit(1);
        }

#if false
        void test() {
            List<Idx> alidx = Readidx(@"P:\KH2.idx");
            uint digest = o1ac6c8.calc(Encoding.ASCII.GetBytes("00progress.bin\0"));
            foreach (Idx v in alidx) {
                if (v.crc == digest) {
                    Console.WriteLine(v);
                    using (FileStream fs = File.OpenRead(@"P:\kh2.img")) {
                        fs.Position = 2048 * v.sec;
                        byte[] encoded = new BinaryReader(fs).ReadBytes((int)v.CompressedSize);
                        File.WriteAllBytes(v.crc.ToString("x8") + ".bin", encoded);
                        byte[] decoded = o2efcc0.explode(encoded, (int)v.UncompressedSize);
                        File.WriteAllBytes(v.crc.ToString("x8") + ".d.bin", decoded);
                    }
                }
            }
            Console.Write("");
        }
#endif

        /*
         * kh2jp
         * 00objentry.bin 4010afef e917c : E9A3F
         * 00progress.bin 400b1398 e9190 : E9A53
         */

        static List<Idx> Readidx(string fp) {
            using (FileStream fs = File.OpenRead(fp)) {
                BinaryReader br = new BinaryReader(fs);
                int cnt = br.ReadInt32();
                List<Idx> al = new List<Idx>();
                for (int a = 0; a < cnt; a++) {
                    Idx o = new Idx();
                    o.crc = br.ReadUInt32();
                    o.v4 = br.ReadUInt16();
                    o.v6 = br.ReadUInt16();
                    o.sec = br.ReadUInt32();
                    o.size = br.ReadUInt32();
                    al.Add(o);
                }
                return al;
            }
        }

        class Idx {
            public uint crc;
            public ushort v4;
            public ushort v6;
            public uint sec; // first sector (2048 bytes unit)
            public uint size; // uncompressed file size (byte unit)

            public int CntSectors { get { return 1 + (v6 & 0x3fff); } } // number of sectors (2048 bytes unit)
            public bool IsCompressed { get { return 0 != (v6 & 0x4000); } }

            public int CompressedSize { get { return 2048 * CntSectors; } }
            public int UncompressedSize { get { return Convert.ToInt32(size); } }

            public override string ToString() {
                return string.Format("crc={0:x8}, size-packed={1:#,##0}, size-unpacked={2:#,##0}, is-packed={3}, offset={4:#,##0}", crc, CompressedSize, UncompressedSize, IsCompressed ? 1 : 0, 2048 * sec);
            }
        }
    }

    static class o2efcc0 {
        public static byte[] explode(byte[] bin, int expandedSize) {
            byte[] res = new byte[expandedSize];

            //#002efcc0
            int t4 = bin.Length - 1;
            byte t5;
            while (true) {
                t5 = bin[t4];
                if (t5 == 0) {
                    t4--;
                    continue;
                }
                break;
            }
            //#002efce4
            int t6 = expandedSize - 1;
            t4 -= 5;
            byte t0 = t5;
            if (t4 >= 0) {
                //#002efcf8
                int a1 = t6;
                byte t7 = bin[t4];
                while (true) {
                    t4--;
                    if (t7 == t0) {
                        //#002efd08
                        byte t1 = bin[t4];
                        t4--;
                        if (t1 != 0) {
                            //#002efd14
                            t7 = bin[t4];
                            int t3 = 0;
                            int t2 = t7 + 3;
                            t4--;
                            if (t2 != 0) {
                                while (true) {
                                    //#002efd28
                                    int _t7 = a1 + t1;
                                    t3++;
                                    byte _t6 = res[_t7];
                                    res[a1] = _t6;
                                    a1--;
                                    if (t3 < t2)
                                        continue;
                                    break;
                                }
                            }
                        }
                        else {
                            //#002efd58
                            res[a1] = t7;
                            a1--;
                        }
                    }
                    else {
                        //#002efd58
                        res[a1] = t7;
                        a1--;
                    }
                    //#002efd44
                    if (t4 >= 0) {
                        t7 = bin[t4];
                        continue;
                    }
                    break;
                }
            }
            return res;
        }
    }

    static class o1ac6c8 {
        public static uint calc(byte[] bin) {
            int a0 = 0;
            uint t7 = bin[a0];
            uint v0 = uint.MaxValue;
            if (t7 != 0) {
                a0++;
                uint t4 = 0x04c11db7;
                uint t5 = 0x80000000;
                do {
                    t7 <<= 24;
                    int t6 = 7;
                    v0 ^= t7;
                    t7 = v0 & t5;
                    do {
                        if (t7 == 0) {
                            t7 = v0 << 1;
                            v0 <<= 1;
                        }
                        else {
                            t7 = v0 << 1;
                            v0 = t7 ^ t4;
                        }
                        t6--;
                        t7 = v0 & t5;
                    } while (t6 >= 0);
                    t7 = bin[a0];
                    a0++;
                } while (t7 != 0);
            }
            return ~v0;
        }
    }
}
