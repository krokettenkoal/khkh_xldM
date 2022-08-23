using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class HTClr : HTBase {
        public Color clr;

        public HTClr(int off, Color clr) {
            this.off = off;
            this.clr = clr;
        }
    }
}
