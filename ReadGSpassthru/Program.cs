using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ReadGSpassthru {
    class Program {
        static void helpYou() {
            Console.Error.WriteLine("ReadGSpassthru <GSpassthru.bin>");
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
            using (FileStream fs = File.OpenRead(args[0])) {
                BinaryReader br = new BinaryReader(fs);
                try {
                    int flg = 0;
                    int fno = 0;
                    Writer wr = new Writer();
                    while (true) {
                        uint id = br.ReadUInt32();
                        uint size = br.ReadUInt32();
                        string func = M[id];
                        byte[] bin = br.ReadBytes(Convert.ToInt32(size));
                        MemoryStream si = new MemoryStream(bin, false);

                        Console.WriteLine("{0,-16}{1,6:0}  {2}", func, size, hexbin(bin));

                        if (flg == 0 && size == 96) {
                            flg = 1;
                        }
                        else if (flg == 1 || size >= 8192) {
                            flg = 0;
                            wr.writebin(fno.ToString("00000000") + ".bin", bin);
                            fno++;
                        }
                        fno++;
                    }
                }
                catch (IOException) {
                }
            }
        }

        class Writer {
            MD5 md5 = MD5.Create();
            SortedDictionary<Guid, object> dict = new SortedDictionary<Guid, object>();

            public void writebin(string f, byte[] bin) {
                Guid id = new Guid(md5.ComputeHash(bin));
                if (dict.ContainsKey(id)) {
                }
                else {
                    File.WriteAllBytes(f, bin);
                    dict[id] = null;
                }
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
    }
}
