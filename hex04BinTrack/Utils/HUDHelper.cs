using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace hex04BinTrack.Utils {
    class HUDHelper {
        internal void Clear() {
            bones.Clear();
        }

        public List<Bone> bones = new List<Bone>();

        public class Bone {
            public Vector3 vFrom;
            public Vector3 vTo;
            public string name;
        }
    }
}
