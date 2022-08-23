using System;
using System.Collections.Generic;
using SevenZip.Compression.LZMA;
using System.IO;
using SevenZip;

namespace Press_eeram {
    class Program {
        static void Main(string[] args) {
            using (FileStream si = File.OpenRead(args[0])) {
                using (FileStream os = File.Create(args[1])) {
                    Encoder e = new Encoder();

                    Int64 pos0 = os.Position;
                    e.Code(si, os, si.Length, 0, null);

                    Int64 pos1 = os.Position;
                    e.WriteCoderProperties(os);

                    Int64 pos2 = os.Position;

                    BinaryWriter wr = new BinaryWriter(os);
                    wr.Write(pos0);
                    wr.Write(pos1);
                    wr.Write(pos2);
                }
            }
        }
    }
    class Rep : ICodeProgress {
        #region ICodeProgress ÉÅÉìÉo

        public void SetProgress(long inSize, long outSize) {
            Console.WriteLine("{0,10} {1,10}", inSize, outSize);
        }

        #endregion
    }
}
