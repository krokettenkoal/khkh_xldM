using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    static class StreamAlignment {
        internal static void Int128(Stream baseStream) {
            var padSize = (16 - (((int)baseStream.Position) & 15)) & 15;
            if (padSize != 0) {
                baseStream.Write(new byte[padSize], 0, padSize);
            }
        }
    }
}
