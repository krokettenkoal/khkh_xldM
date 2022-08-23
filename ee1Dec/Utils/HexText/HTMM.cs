using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class HTMM : HTBase {
        public int afrom, ato;
        public string cls, text;

        public HTMM(int afrom, int ato, string cls, string text) {
            this.afrom = afrom;
            this.ato = ato;
            this.cls = cls;
            this.text = text;
        }
    }
}
