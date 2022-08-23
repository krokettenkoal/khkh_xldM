using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.Coct {
    public class Co1 {
        public short v00, v02, v04, v06, v08, v0a, v0c, v0e;
        public short v10, v12, v14, v16, v18, v1a, v1c, v1e;

        public int[] GetChildren() => new int[] { v00, v02, v04, v06, v08, v0a, v0c, v0e };
        public int Co2First => v1c;
        public int Co2Last => v1e;

        public Vector3 Min { get { return new Vector3(v10, v12, v14); } }
        public Vector3 Max { get { return new Vector3(v16, v18, v1a); } }
        public BoundingBox BBox => new BoundingBox(Min, Max);

        public override string ToString() {
            return String.Format("?({0,3},{1,3},{2,3},{3,3},{4,3},{5,3},{6,3},{7,3}) "
                , v00
                , v02
                , v04
                , v06
                , v08
                , v0a
                , v0c
                , v0e
                ) +
                String.Format("bbox-min({0,7}, {1,6}, {2,6}) bbox-max({3,6}, {4,6}, {5,6}) Co2frmTo({6,3},{7,3})"
                , v10
                , v12
                , v14
                , v16
                , v18
                , v1a
                , v1c
                , v1e
                );
        }

        public Co1(BinaryReader br) {
            v00 = br.ReadInt16();
            v02 = br.ReadInt16();
            v04 = br.ReadInt16();
            v06 = br.ReadInt16();
            v08 = br.ReadInt16();
            v0a = br.ReadInt16();
            v0c = br.ReadInt16();
            v0e = br.ReadInt16();

            v10 = br.ReadInt16();
            v12 = br.ReadInt16();
            v14 = br.ReadInt16();
            v16 = br.ReadInt16();
            v18 = br.ReadInt16();
            v1a = br.ReadInt16();
            v1c = br.ReadInt16();
            v1e = br.ReadInt16();
        }
    }
}
