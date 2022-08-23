using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdlx2aset.Model {
    internal class Sepa {
        public int svi;
        public int cnt;
        public int t;
        public int sel;

        public Sepa(int startVertexIndex, int cntPrimitives, int ti, int sel) {
            this.svi = startVertexIndex;
            this.cnt = cntPrimitives;
            this.t = ti;
            this.sel = sel;
        }
    }
}
