using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    /// <summary>
    /// Private memory annalizer
    /// </summary>
    class PrivateManna {
        public string Find(uint off, Stream sieeMem) {
            if ((off - 0x1ABC520U) < 0xE50) {
                int k = (int)(off - 0x1ABC520U); // S -> ?
                return string.Format("yval.r[{0:X2}].{1}", k / 16, "xyzw"[(k / 4) % 4]);
            }
            if ((off - 0x1ABD370U) < 0xE50) {
                int k = (int)(off - 0x1ABD370U); // R -> T
                return string.Format("yval.t[{0:X2}].{1}", k / 16, "xyzw"[(k / 4) % 4]);
            }
            if ((off - 0x1ABB6D0U) < 0xE50) {
                int k = (int)(off - 0x1ABB6D0U); // T -> S
                return string.Format("yval.s[{0:X2}].{1}", k / 16, "xyzw"[(k / 4) % 4]);
            }
            return null;
        }
    }
}
