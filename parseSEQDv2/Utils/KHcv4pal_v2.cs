using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class KHcv4pal_v2 {
        readonly static sbyte[] tbl = new sbyte[] {
            /**/ 0, 1, 4, 5, 8, 9,12,13,
            /**/ 2, 3, 6, 7,10,11,14,15,
        };
        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst) {
            for (int x = 0; x < 16; x++) {
                for (int t = 0; t < 4; t++)
                    bDst[offDst + 4 * (x) + t] = bSrc[offSrc + 4 * (tbl[x]) + t];
            }
        }
    }
}
