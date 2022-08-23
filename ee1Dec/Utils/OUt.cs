using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    class OUt {
        public static void Wr(TextWriter wr, uint off, BinaryReader br, String prefix, int cnt) {
            br.BaseStream.Position = off;
            for (int x = 0; x < cnt; x++) {
                float X = br.ReadSingle();
                float Y = br.ReadSingle();
                float Z = br.ReadSingle();
                float W = br.ReadSingle();
                wr.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", prefix, x, X, Y, Z, W);
            }
        }
    }
}
