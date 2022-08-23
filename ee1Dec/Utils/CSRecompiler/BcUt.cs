using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.CSRecompiler {
    class BcUt {
        public static int BC2I(char c) {
            switch (c) {
                case 'x': return 0;
                case 'y': return 1;
                case 'z': return 2;
                case 'w': return 3;
            }
            throw new ArgumentOutOfRangeException("c");
        }
    }
}
