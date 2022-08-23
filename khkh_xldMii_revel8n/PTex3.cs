using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SlimDX.Direct3D9;
using SlimDX;

namespace khkh_xldMii {
    [StructLayout(LayoutKind.Sequential)]
    public struct PTex3 {
        public const VertexFormat Format = VertexFormat.Position | VertexFormat.Texture3;
        public const int Size = 4 + 4 + 4 + (4 * 2 * 3);

        public float X;
        public float Y;
        public float Z;
        public float Tu1, Tv1;
        public float Tu2, Tv2;
        public float Tu3, Tv3;

        public PTex3(Vector3 pos, Vector2 tex) {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            Tu1 = tex.X;
            Tv1 = tex.Y;
            Tu2 = tex.X;
            Tv2 = tex.Y;
            Tu3 = tex.X;
            Tv3 = tex.Y;
        }
    }
}
