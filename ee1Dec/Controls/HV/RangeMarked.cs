using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ee1Dec.Controls.HV {
    public class RangeMarked {
        public int off, len;
        public Color clr, clrborder;

        public RangeMarked(int off, int len, Color clr, Color clrborder) {
            this.off = off;
            this.len = len;
            this.clr = clr;
            this.clrborder = clrborder;
        }
    }
}
