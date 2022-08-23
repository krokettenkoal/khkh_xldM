using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class KHcv8pal_v2 {
        readonly static byte[] alt = new byte[] {
            /**/ 0, 1, 2, 3, 4, 5, 6, 7,16,17,18,19,20,21,22,23,
            /**/ 8, 9,10,11,12,13,14,15,24,25,26,27,28,29,30,31,
        };
        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst) {
            for (int x = 0; x < 256; x++) {
                for (int t = 0; t < 4; t++)
                    bDst[offDst + 4 * (x) + t] = bSrc[offSrc + 4 * (alt[(x & 31)] + (x & (~31))) + t];
            }
        }
    }
}
