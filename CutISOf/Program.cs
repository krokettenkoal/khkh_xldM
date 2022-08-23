using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CutISOf {
    class Program {
        static byte[] at8000 = new byte[] {
                0x01,0x43,0x44,0x30,0x30,0x31,0x01,0x00,0x50,0x4C,0x41,0x59,0x53,0x54,0x41,0x54,
                0x49,0x4F,0x4E,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,
                0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,
                0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,
                0x20,0x20,0x20,0x20,0x20,0x20,0x20,0x20,
            };

        static void Main(string[] args) {
            if (args.Length < 3) {
                helpYou();
            }

            using (FileStream fs = File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read)) {
                fs.Position = 0x8000;
                BinaryReader br = new BinaryReader(fs);
                byte[] bin8k = br.ReadBytes(72);
                int t;
                for (t = 0; t < 72 && bin8k[t] == at8000[t]; t++) ;
                if (t != 72) throw new NotSupportedException("Your iso image not recognized (Has to be MODE1/2048).");

                int sec = int.Parse(args[1]);
                int cntblk = int.Parse(args[2]);
                Stream os = Console.OpenStandardOutput();
                for (int a = 0; a < cntblk; a++) {
                    fs.Position = 2048 * (sec + a);
                    byte[] bin = br.ReadBytes(2048);
                    os.Write(bin, 0, bin.Length);
                }
                fs.Close();
            }
        }

        private static void helpYou() {
            Console.Error.WriteLine("CutISOf <KH2.iso> <sector> <# of blocks>");
            Console.Error.WriteLine("");
            Console.Error.WriteLine("* It writes into stdout in binary mode.");
            Console.Error.WriteLine("* A block is always 2,048 bytes output per sector.");
            Environment.Exit(1);
        }
    }
}
