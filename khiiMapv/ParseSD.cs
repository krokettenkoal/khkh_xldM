using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using khiiMapv.Utils;

namespace khiiMapv {
    public class Wavo {
        public string fn;
        public byte[] bin;

        public Wavo(string fn, byte[] bin) {
            this.fn = fn;
            this.bin = bin;
        }
    }
    public class ParseSD {
        public static Wavo[] ReadIV(Stream fs) {
            BinaryReader br = new BinaryReader(fs);
            BER ber = new BER(br);

            fs.Position = 0x0C;
            int cnt = br.ReadInt32();
            KeyValuePair<int, int>[] poslen = new KeyValuePair<int, int>[cnt];

            fs.Position = 0x10;
            for (int x = 0; x < cnt; x++) {
                int k = br.ReadInt32();
                if (k >= 0) k += 16 + (8 * cnt);
                int v = br.ReadInt32();
                poslen[x] = new KeyValuePair<int, int>(k, v);
            }

            List<Wavo> al = new List<Wavo>();
            for (int x = 0; x < poslen.Length; x++) {
                int vagoff = poslen[x].Key;
                if (vagoff < 0)
                    continue;
                {
                    fs.Position = vagoff + 0x0C;
                    int cb = ber.ReadInt32() - 0x20;

                    fs.Position = vagoff + 0x10;
                    int sps = ber.ReadInt32();

                    fs.Position = vagoff + 0x40;
                    al.Add(new Wavo(
                        x.ToString("00") + ".wav", 
                        SPUConv.ToWave(new MemoryStream(br.ReadBytes(cb)), sps)
                        ));
                }
            }

            return al.ToArray();
        }
        public static Wavo[] ReadWD(Stream fs) {
            BinaryReader br = new BinaryReader(fs);

            fs.Position = 0x08;
            int cnt1 = br.ReadInt32(); // tbl1 
            int cnt2 = br.ReadInt32(); // tbl2

            int[] alofftbl2 = new int[cnt1];

            fs.Position = 0x20; // tbl1
            for (int x = 0; x < cnt1; x++) {
                alofftbl2[x] = br.ReadInt32();
            }
            int gakki = 0, ontei = 0;
            List<Wavi> alwavi = new List<Wavi>();
            for (int x = 0; x < cnt2; x++) {
                int off = 0x20 + 4 * ((cnt1 + 3) & (~3)) + 32 * x;
                int fki = Array.IndexOf(alofftbl2, off);
                if (fki >= 0) { gakki = fki; ontei = 0; }
                fs.Position = off + 16;
                if (br.ReadInt64() == 0 && br.ReadInt64() == 0) continue;
                fs.Position = off + 4;
                Wavi o = new Wavi();
                o.off = br.ReadInt32();
                o.gakki = gakki;
                o.ontei = ontei; ontei++;
                fs.Position = off + 22;
                o.sps = 0 + br.ReadUInt16();
                alwavi.Add(o);
            }
            List<Wavo> al = new List<Wavo>();
            for (int x = 0; x < alwavi.Count; x++) {
                Wavi o = alwavi[x];
                int off = 0x20 + 16 * ((cnt1 + 3) & (~3)) + 32 * cnt2 + o.off;
                fs.Position = off;
                while (fs.Position < fs.Length) {
                    byte[] bin = br.ReadBytes(16);
                    int t = 0;
                    while (t < 16 && bin[t] == 0) t++;
                    if (t == 16) break;
                }
                int sps = o.sps;
                int cb = Convert.ToInt32(fs.Position) - off - 0x20;
                fs.Position = off;
                al.Add(new Wavo(
                    o.gakki.ToString("000") + "." + o.ontei.ToString("00") + ".wav", 
                    SPUConv.ToWave(new MemoryStream(br.ReadBytes(cb)), sps))
                    );
            }
            return al.ToArray();
        }

        class Wavi {
            public int off;
            public int gakki, ontei, sps;
        }
    }
}
