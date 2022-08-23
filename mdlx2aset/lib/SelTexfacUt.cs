using khkh_xldMii.Mo;
using khkh_xldMii.Models;

namespace mdlx2aset.Model {
    internal class SelTexfacUt {
        public static PatTexSel[] Sel(List<FacePatch> alp, float tick, FacMod fm) {
            PatTexSel[] sel = new PatTexSel[alp.Count];
            foreach (Fac1 f1 in fm.alf1) {
                if (f1.v2 != -1 && f1.v0 <= tick && tick < f1.v2) {
                    for (int x = 0; x < alp.Count; x++) {
                        int curt = (int)(tick - f1.v0) / 8;
                        foreach (FaceTexture tf in alp[x].faceTextureList) {
                            if (tf.i0 == f1.v6) {
                                if (curt <= 0) {
                                    if (sel[x] == null) {
                                        sel[x] = new PatTexSel((byte)alp[x].textureIndex, (byte)tf.v6);
                                        break;
                                    }
                                }
                                curt -= tf.v2;
                            }
                        }
                    }
                }
            }
            return sel;
        }
    }
}
