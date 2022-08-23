using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils {
    class Sorto2c : IComparer<KeyValuePair<uint, uint>> {
        #region IComparer<KeyValuePair<uint,uint>> メンバ

        public int Compare(KeyValuePair<uint, uint> x, KeyValuePair<uint, uint> y) {
            int t;
            if (0 != (t = x.Key.CompareTo(y.Key))) return t;
            if (0 != (t = x.Value.CompareTo(y.Value))) return t;
            return 0;
        }

        #endregion
    }

}
