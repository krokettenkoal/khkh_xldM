using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace findBinInBin {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 2) {
                helpYou();
            }

            byte[] wanna = File.ReadAllBytes(args[1]);
            byte[] bin = new byte[1048576];
            Blocks blks = new Blocks();
            using (FileStream fs = File.OpenRead(args[0])) {
                // fs.Position = fs.Length * 40 / 100;

                for (int t = 0; ; t++) {
                    Int64 foff = fs.Position;
                    Console.WriteLine("{0:0.00} {1,3:0} {2,3:0}", ((1.0 * foff) / fs.Length), blks.alMatch.Count, blks.alCont.Count);
                    int r = fs.Read(bin, 0, 1048576);
                    if (r < 1)
                        break;
                    blks.find(bin, 0, r, wanna, foff);
                }
                fs.Close();
            }
        }

        private static void helpYou() {
            Console.Error.WriteLine("findBinInBin <KH2.IMG> <partial bin to find>");
            Environment.Exit(1);
        }

        class Block {
            public Int64 fileoff = 0;

            public int findoff = -1;
            public byte[] findbin = null;
        }
        class Blocks {
            public List<Block> alCont = new List<Block>();
            public List<Block> alMatch = new List<Block>();

            public void find(byte[] temp, int x, int cx, byte[] bin, Int64 foff) {
                int ox = x;
                cx += x;

                List<Block> alMore = new List<Block>(alCont);
                alCont.Clear();
                foreach (Block blk in alMore) {
                    byte[] findbin = blk.findbin;
                    int z = 0;
                    int zoff = blk.findoff;
                    int maxTemp = cx - x;
                    int cz = findbin.Length;
                    while (true) {
                        if (temp[x + z] != findbin[z + zoff])
                            break;
                        if (z == maxTemp) {
                            // Continue
                            blk.findoff = z + zoff;
                            alCont.Add(blk);
                            break;
                        }
                        if (z + zoff == cz) {
                            // Match
                            alMatch.Add(blk);
                            break;
                        }
                        z++;
                        continue;
                    }
                }

                for (x = ox; x < cx; x++) {
                    if (temp[x] == bin[0]) {
                        int maxTemp = cx - x;
                        int z = 0, cz = bin.Length;
                        while (true) {
                            if (z == maxTemp) {
                                // Continue
                                Block blk = new Block();
                                blk.fileoff = foff + x;
                                blk.findoff = z;
                                blk.findbin = bin;
                                alCont.Add(blk);
                                break;
                            }
                            if (z == cz) {
                                // Match
                                Block blk = new Block();
                                blk.fileoff = foff + x;
                                alMatch.Add(blk);
                                break;
                            }
                            if (temp[x + z] != bin[z])
                                break;
                            z++;
                            continue;
                        }
                    }
                }
            }
        }
    }
}
