using System;
using System.Collections.Generic;
using System.Text;

namespace khkh_xldMii.Models.Mdlx {
    public class K2Vif {
        public int off, len;
        public int textureIndex;
        public int v8;
        public int vc;
        public int[] marixIndexArray;
        public byte[] bin;

        public K2Vif(int off, int len, int textureIndex, int v8, int vc, int[] marixIndexArray, byte[] bin) {
            this.off = off;
            this.len = len;
            this.textureIndex = textureIndex;
            this.v8 = v8;
            this.vc = vc;
            this.marixIndexArray = marixIndexArray;
            this.bin = bin;
        }
    }
}
