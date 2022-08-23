using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;
using SlimDX;
using System.Runtime.InteropServices;
using hex04BinTrack.Utils;

namespace hex04BinTrack.VertexFormats {
    [StructLayout(LayoutKind.Sequential)]
    public struct PosClr {
        public const VertexFormat Format = VertexFormat.Diffuse | VertexFormat.Position;

        public static int StrideSize { get { return VertexFmtSize.Compute(Format); } }

        public float X;
        public float Y;
        public float Z;
        public int Color;

        public PosClr(Vector3 pos, int c) {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            Color = c;
        }
    }
}
