using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class KHcv4pal {
        readonly static sbyte[] tbl = new sbyte[] {
            /**/ 0, 1, 4, 5, 8, 9,12,13,
            /**/ 2, 3, 6, 7,10,11,14,15,
        };
        public static int repl(int t) {
            return tbl[t];
        }
    }
}
