using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.Coct {
    public class Co6 {
        public short v00 { get; set; }
        public short v02 { get; set; }
        public short v04 { get; set; }
        public short v06 { get; set; }
        public short v08 { get; set; }
        public short v0a { get; set; }

        public Vector3 Min { get { return new Vector3(v00, v02, v04); } }
        public Vector3 Max { get { return new Vector3(v06, v08, v0a); } }
        public BoundingBox BBox => new BoundingBox(Min, Max);

        public Co6(BinaryReader br) {
            v00 = br.ReadInt16();
            v02 = br.ReadInt16();
            v04 = br.ReadInt16();
            v06 = br.ReadInt16();
            v08 = br.ReadInt16();
            v0a = br.ReadInt16();
        }

        public override string ToString() => $"({v00,6},{v02,6},{v04,6}) ({v06,6},{v08,6},{v0a,6})";
    }
}
