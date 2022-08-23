using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class HTDesc : HTBase {
        public string text;

        public HTDesc(int off, string text) {
            this.off = off;
            this.text = text;
        }
    }
}
