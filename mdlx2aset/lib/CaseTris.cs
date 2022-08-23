using SlimDX.Direct3D9;

namespace mdlx2aset.Model {
    internal class CaseTris : IDisposable {
        public VertexBuffer? vb;
        public VertexFormat vf;
        public int cntPrimitives, cntVert;
        public Sepa[]? alsepa;
        public uint[]? altri3;

        #region IDisposable ƒƒ“ƒo

        public void Dispose() {
            Close();
        }

        #endregion

        public void Close() {
            if (vb != null)
                vb.Dispose();
            vb = null;
            vf = VertexFormat.None;
            cntPrimitives = 0;
            cntVert = 0;
            alsepa = null;
            altri3 = null;
        }

    }
}
