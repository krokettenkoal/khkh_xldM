using khkh_xldMii.Mo;

namespace mdlx2aset.Motion {
    internal class UtwexMotionSel {
        public static Mt1? Sel(int k1, List<Mt1> al1) {
            foreach (Mt1 o in al1) {
                if (o.k1 == k1)
                    return o;
            }
            return null;
        }
    }
}
