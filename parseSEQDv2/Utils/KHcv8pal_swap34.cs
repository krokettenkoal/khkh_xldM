using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class KHcv8pal_swap34 {
        public static int repl(int x) {
            return 0
                | (((x & 0xE7)))
                | (((x & 0x10) != 0) ? 0x08 : 0x00)
                | (((x & 0x08) != 0) ? 0x10 : 0x00)
            ;
        }
    }
}
