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
            [Obsolete("Use StrideSide")]
            public static int Size { get { return Marshal.SizeOf(typeof(PositionColoredTextured)); } }
            public static int StrideSize { get { return Marshal.SizeOf(typeof(PositionColoredTextured)); } }
        }
    }
}
