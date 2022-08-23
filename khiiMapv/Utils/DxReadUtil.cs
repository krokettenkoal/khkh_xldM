using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    static class DxReadUtil {
        public static Matrix ReadMatrix(BinaryReader br) {
            var mtx = new Matrix();
            mtx.M11 = br.ReadSingle(); mtx.M12 = br.ReadSingle(); mtx.M13 = br.ReadSingle(); mtx.M14 = br.ReadSingle();
            mtx.M21 = br.ReadSingle(); mtx.M22 = br.ReadSingle(); mtx.M23 = br.ReadSingle(); mtx.M24 = br.ReadSingle();
            mtx.M31 = br.ReadSingle(); mtx.M32 = br.ReadSingle(); mtx.M33 = br.ReadSingle(); mtx.M34 = br.ReadSingle();
            mtx.M41 = br.ReadSingle(); mtx.M42 = br.ReadSingle(); mtx.M43 = br.ReadSingle(); mtx.M44 = br.ReadSingle();
            return mtx;
        }

        public static Vector4 ReadVector4(BinaryReader br) {
            var vertex = new Vector4();
            vertex.X = br.ReadSingle();
            vertex.Y = br.ReadSingle();
            vertex.Z = br.ReadSingle();
            vertex.W = br.ReadSingle();
            return vertex;
        }
    }
}
