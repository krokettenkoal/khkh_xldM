using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class HTOut : HTBase {
        public string form;

        public HTOut(int off, string form) {
            this.off = off;
            this.form = form;
        }
    }
}
