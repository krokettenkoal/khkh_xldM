using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;

namespace hex04BinTrack.Utils {
    public class VertexFmtSize {
        public static int Compute(VertexFormat vf) {
            int cb = 0;
            VertexFormat f;

            f = VertexFormat.Diffuse; if (0 != (vf & f)) { cb += 4; vf ^= f; }
            f = VertexFormat.Specular; if (0 != (vf & f)) { cb += 4; vf ^= f; }

            f = VertexFormat.Normal; if (0 != (vf & f)) { cb += 4 * 3; vf ^= f; }
            f = VertexFormat.Position; if (0 != (vf & f)) { cb += 4 * 3; vf ^= f; }

            f = VertexFormat.Texture1; if (0 != (vf & f)) { cb += 4 * 2; vf ^= f; }

            if (vf != VertexFormat.None) throw new NotSupportedException("vf: " + vf);
            return cb;
        }
    }
}
