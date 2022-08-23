using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using khkh_xldMii;

namespace Silent_anb2mset {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Silent_anb2mset <anm_tt_p_ex110_ETT00001C.anb>+");
                Environment.Exit(1);
            }
            string fanb = args[0];
            string fmset = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(fanb) + ".mset");

            int cnt = args.Length;
            using (FileStream fs = File.Create(fmset)) {
                BinaryWriter wr = new BinaryWriter(fs, Encoding.ASCII);
                wr.Write((uint)0x01524142);
                wr.Write((uint)cnt);
                wr.Write((uint)0);
                wr.Write((uint)0);

                List<byte[]> alblk = new List<byte[]>();
                int off = 0x10 + 0x10 * cnt;

                foreach (string fanbin in args) {
                    byte[] bin = File.ReadAllBytes(fanbin);

                    string theid = null;
                    MemoryStream si;
                    foreach (ReadBar.Barent ent in ReadBar.Explode(si = new MemoryStream(bin, false))) {
                        if (ent.k == 9) {
                            theid = ent.id;
                            break;
                        }
                    }

                    wr.Write((uint)0x11);
                    wr.Write(theid.ToCharArray(), 0, 4);
                    wr.Write((uint)off);
                    wr.Write((uint)bin.Length);

                    off += bin.Length;

                    alblk.Add(bin);
                }
                foreach (byte[] bin in alblk) {
                    wr.Write(bin);
                }
            }
        }
    }
}
