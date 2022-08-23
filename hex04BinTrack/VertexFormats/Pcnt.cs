using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SlimDX.Direct3D9;
using SlimDX;

namespace hex04BinTrack.VertexFormats {
    [StructLayout(LayoutKind.Sequential)]
    public struct Pcnt {
        public const VertexFormat Format = VertexFormat.Diffuse | VertexFormat.Normal | VertexFormat.Position | VertexFormat.Texture1;

        public float X;
        public float Y;
        public float Z;
        public float Nx;
        public float Ny;
        public float Nz;
        public int Color;
        public float Tu;
        public float Tv;

        public Pcnt(Vector3 pos, Vector3 nor, Vector2 tex, int c) {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            Nx = nor.X;
            Ny = nor.Y;
            Nz = nor.Z;
            Tu = tex.X;
            Tv = tex.Y;
            Color = c;
        }
        public Pcnt(float xval, float yval, float zval, float nxval, float nyval, float nzval, int c, float tuval, float tvval) {
            X = xval;
            Y = yval;
            Z = zval;
            Nx = nxval;
            Ny = nyval;
            Nz = nzval;
            Tu = tuval;
            Tv = tvval;
            Color = c;
        }

        public Vector3 Normal {
            get { return new Vector3(Nx, Ny, Nz); }
            set { Nx = value.X; Ny = value.Y; Nz = value.Z; }
        }
        public Vector3 Position {
            get { return new Vector3(X, Y, Z); }
            set { X = value.X; Y = value.Y; Z = value.Z; }
        }
        public static int StrideSize {
            get { return 4 * 9; }
        }
    }
}
