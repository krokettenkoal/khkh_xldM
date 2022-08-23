using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;
using SlimDX;
using System.Runtime.InteropServices;
using hex04BinTrack.Utils;

namespace hex04BinTrack.VertexFormats {
    [StructLayout(LayoutKind.Sequential)]
    public struct Pnc {
        public const VertexFormat Format = VertexFormat.Diffuse | VertexFormat.Normal | VertexFormat.Position;

        public static int StrideSize { get { return VertexFmtSize.Compute(Format); } }

        public float X;
        public float Y;
        public float Z;
        public float Nx;
        public float Ny;
        public float Nz;
        public int Color;

        public Pnc(Vector3 pos, Vector3 nor, int c) {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            Nx = nor.X;
            Ny = nor.Y;
            Nz = nor.Z;
            Color = c;
        }

        public Vector3 Normal { get { return new Vector3(Nx, Ny, Nz); } set { Nx = value.X; Ny = value.Y; Nz = value.Z; } }
    }
}
