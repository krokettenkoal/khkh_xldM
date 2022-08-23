using SlimDX.Direct3D9;
using SlimDX;
using ef1Declib;
using khkh_xldMii.Mx;
using khkh_xldMii.Mo;
using khkh_xldMii.Models;
using khkh_xldMii.V;

namespace mdlx2aset.Model {

    internal class Mesh : IDisposable {
        public Mdlxfst? mdlx;
        public Texex2[]? timc;
        public Texex2? timf;
        public Msetfst? mset;
        public List<Texture> altex = new();
        public List<Texture> altex1 = new();
        public List<Texture> altex2 = new();
        public List<Body1> albody1 = new();
        public byte[]? binMdlx;
        public byte[]? binMset;
        public CaseTris ctb = new();
        public Mlink? ol;
        public PatTexSel[] pts = Array.Empty<PatTexSel>();

        public Matrix[]? Ma = null; // for keyblade
        public Mesh? parent = null; // for keyblade
        public int iMa = -1; // for keyblade

        public bool Present { get { return mdlx != null && mset != null; } }

        #region IDisposable ƒƒ“ƒo

        public void Dispose() {
            DisposeMdlx();
            DisposeMset();
        }

        #endregion

        public void DisposeMset() {
            mset = null;
            binMset = null;
            ol = null;
        }

        public void DisposeMdlx() {
            mdlx = null;
            timc = null;
            timf = null;
            foreach (Texture t in altex)
                t.Dispose();
            altex.Clear();
            foreach (Texture t in altex1)
                t.Dispose();
            altex1.Clear();
            foreach (Texture t in altex2)
                t.Dispose();
            altex2.Clear();
            albody1.Clear();
            binMdlx = null;
            ctb.Close();
            ol = null;
            Ma = null;
        }
    }
}
