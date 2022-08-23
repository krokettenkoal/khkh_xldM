using System;
using System.Collections.Generic;
using System.Text;

namespace khkh_xldMii.Models.Mset {
    public class OneMotion {
        public uint anbOff;
        public uint anbLen;
        public string label;
        public byte[] anbBin;
        public int indexInMset;
        public FaceModSet faceModSet;
        public bool isRaw;

        public override string ToString() {
            return label;
        }
    }
}
