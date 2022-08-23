using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class ResHT {
        public int off;
        public string text = null;
        public Color clr = Color.Empty;
        public List<string> alform = new List<string>();
        public int ov = 0;
        public List<string> alovtext = new List<string>();
        public string memmemo = null;

        public ResHT(int off) {
            this.off = off;
        }
    }
}
