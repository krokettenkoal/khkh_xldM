using System;
using System.Collections.Generic;
using System.Text;

namespace khkh_xldMii.Utils {
    internal class RUtil {
        public static int RoundUpto16(int val) {
            return (val + 15) & (~15);
        }
    }
}
