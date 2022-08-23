using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class HTOv : HTBase {
        public string text;

        public HTOv(int off, string text) {
            this.off = off;
            this.text = text;
        }
    }
}
