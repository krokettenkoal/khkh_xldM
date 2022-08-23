using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace khiiMapv {
    public class CustomVertex {
        [StructLayout(LayoutKind.Sequential)]
        public struct PositionColoredTextured {
            public float X, Y, Z;
            public int Color;
            public float Tu, Tv;

            public PositionColoredTextured(Vector3 v, int clr, float tu, float tv) {
                this.X = v.X;
                this.Y = v.Y;
                this.Z = v.Z;
                this.Color = clr;
                this.Tu = tu;
                this.Tv = tv;
            }

            public PositionColoredTextured(float x, float y, float z, int clr, float tu, float tv) {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Color = clr;
                this.Tu = tu;
                this.Tv = tv;
            }

            public Vector3 Position { get { return new Vector3(X, Y, Z); } }

            public static VertexFormat Format { get { return VertexFormat.Position | VertexFormat.Diffuse | VertexFormat.Texture1; } }
            public static int Size { get { return Marshal.SizeOf(typeof(PositionColoredTextured)); } }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionColored {
            public float X, Y, Z;
            public int Color;

            public PositionColored(Vector3 v, int clr) {
                this.X = v.X;
                this.Y = v.Y;
                this.Z = v.Z;
                this.Color = clr;
            }

            public PositionColored(float x, float y, float z, int clr) {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Color = clr;
            }

            public Vector3 Position { get { return new Vector3(X, Y, Z); } }

            public static VertexFormat Format { get { return VertexFormat.Position | VertexFormat.Diffuse; } }
            public static int Size { get { return Marshal.SizeOf(typeof(PositionColored)); } }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Position {
            public float X, Y, Z;

            public Position(Vector3 v) {
                this.X = v.X;
                this.Y = v.Y;
                this.Z = v.Z;
            }

            public Position(float x, float y, float z) {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public static VertexFormat Format { get { return VertexFormat.Position; } }
            public static int Size { get { return Marshal.SizeOf(typeof(Position)); } }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionNormalColored {
            public float X, Y, Z;
            public float Nx, Ny, Nz;
            public int Color;

            public PositionNormalColored(Vector3 v, Vector3 n, int c) {
                this.X = v.X;
                this.Y = v.Y;
                this.Z = v.Z;
                this.Nx = n.X;
                this.Ny = n.Y;
                this.Nz = n.Z;
                this.Color = c;
            }

            public PositionNormalColored(float x, float y, float z, float nx, float ny, float nz, int c) {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Nx = nx;
                this.Ny = ny;
                this.Nz = nz;
                this.Color = c;
            }

            public static VertexFormat Format { get { return VertexFormat.Position | VertexFormat.Normal | VertexFormat.Diffuse; } }
            public static int Size { get { return Marshal.SizeOf(typeof(PositionNormalColored)); } }
        }
    }
}
