using System;
using System.Collections.Generic;
using System.Text;

namespace khkh_xldMii.Models.Mdlx {
    /// <summary>
    /// KH2 model
    /// </summary>
    public class K2Model {
        public int off;
        public int len;

        /// <summary>
        /// 0 is main, 1 is shadow, for usual.
        /// </summary>
        public int index;

        /// <summary>
        /// 2 for map, 3 for model, 4 for shadow, usually.
        /// </summary>
        public int type;

        public List<T11> al11 = new List<T11>();
        public List<T12> al12 = new List<T12>();
        public List<K2Vif> vifList = new List<K2Vif>();
        public K2BoneList boneList = null;
        public T32 t32 = null;

        public K2Model(int off, int len, int index, int type) {
            this.off = off;
            this.len = len;
            this.index = index;
            this.type = type;
        }
    }
}
