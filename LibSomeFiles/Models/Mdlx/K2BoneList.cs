using System;
using System.Collections.Generic;
using System.Text;

namespace khkh_xldMii.Models.Mdlx {
    public class K2BoneList {
        public int off, len;
        public List<AxBone> bones = new List<AxBone>();

        public K2BoneList(int off, int len) {
            this.off = off;
            this.len = len;
        }
    }
}
