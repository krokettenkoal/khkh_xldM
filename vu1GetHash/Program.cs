using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace vu1GetHash {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 2) {
                Console.Error.WriteLine("vu1GetHash . *.bin");
                Environment.Exit(1);
            }
            foreach (string fp in Directory.GetFiles(args[0], args[1], SearchOption.TopDirectoryOnly)) {
                if (new FileInfo(fp).Length == 1024 + 1024 + 16384 + 16384) {
                    using (FileStream fs = File.OpenRead(fp)) {
                        fs.Position = 1024 + 1024 + 16384;
                        MD5 a = MD5.Create();
                        byte[] bin = new BinaryReader(fs).ReadBytes(16384);
                        byte[] k16 = a.ComputeHash(bin);
                        Console.WriteLine(Path.GetFileName(fp) + "," + E.B(k16));
                    }
                }
            }
        }
        class E {
            public static string B(byte[] bin) {
                string s = "";
                for (int x = 0; x < bin.Length; x++) {
                    s += bin[bin.Length - x - 1].ToString("x2");
                }
                return s;
            }
        }
    }
}
