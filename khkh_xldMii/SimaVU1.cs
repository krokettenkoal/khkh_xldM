using System;
using System.Collections.Generic;
using System.Text;
using hex04BinTrack;
using System.Diagnostics;
using System.IO;
using SlimDX;

namespace khkh_xldMii.V {
    public class SimaVU1 {
        public static Body1 Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv) {
            MemoryStream si = new MemoryStream(vu1mem.vumem, true);
            BinaryReader br = new BinaryReader(si);

            //si.Position = 16 * (top2);
            //for (int x = 0; x < alaxi.Length; x++) {
            //    UtilMatrixio.write(wr, Ma[alaxi[x]]);
            //}

            si.Position = 16 * (tops);

            int v00 = br.ReadInt32();
            if (v00 != 1 && v00 != 2) throw new ProtInvalidTypeException();
            int v04 = br.ReadInt32();
            int v08 = br.ReadInt32();
            int v0c = br.ReadInt32();
            int v10 = br.ReadInt32(); // cnt box2
            int v14 = br.ReadInt32(); // off box2 {tx ty vi fl}
            int v18 = br.ReadInt32(); // off box1
            int v1c = br.ReadInt32(); // off matrices
            int v20 = (v00 == 1) ? br.ReadInt32() : 0; // cntvertscolor
            int v24 = (v00 == 1) ? br.ReadInt32() : 0; // offvertscolor
            int v28 = (v00 == 1) ? br.ReadInt32() : 0; // cnt spec
            int v2c = (v00 == 1) ? br.ReadInt32() : 0; // off spec
            int v30 = br.ReadInt32(); // cnt verts 
            int v34 = br.ReadInt32(); // off verts
            int v38 = br.ReadInt32(); // 
            int v3c = br.ReadInt32(); // cnt box1
            //Trace.Assert(v1c == top2 - tops, "top2 isn't identical!");

            si.Position = 16 * (tops + v18);
            int[] box1 = new int[v3c];
            for (int x = 0; x < box1.Length; x++) {
                box1[x] = br.ReadInt32();
            }

            Body1 body1 = new Body1();
            body1.t = tsel;
            body1.alvert = new Vector3[v30];
            body1.avail = (v28 == 0) && (v00 == 1);

            Vector3[] alv4 = new Vector3[v30];

            int vi = 0;
            si.Position = 16 * (tops + v34);
            for (int x = 0; x < box1.Length; x++) {
                Matrix M1 = Ma[alaxi[x]] * Mv;
                int ct = box1[x];
                for (int t = 0; t < ct; t++, vi++) {
                    float fx = br.ReadSingle();
                    float fy = br.ReadSingle();
                    float fz = br.ReadSingle();
                    float fw = br.ReadSingle();
                    Vector3 v3 = new Vector3(fx, fy, fz);
                    Vector3 v3t = Vector3.TransformCoordinate(v3, M1);
                    body1.alvert[vi] = v3t;
                    Vector4 v4 = new Vector4(fx, fy, fz, fw);
                    Vector4 v4t = Vector4.Transform(v4, M1);
                    alv4[vi] = new Vector3(v4t.X, v4t.Y, v4t.Z);
                }
            }

            body1.aluv = new Vector2[v10];
            body1.alvi = new int[v10];
            body1.alfl = new int[v10];

            si.Position = 16 * (tops + v14);
            for (int x = 0; x < v10; x++) {
                int tx = br.ReadUInt16() / 16; br.ReadUInt16();
                int ty = br.ReadUInt16() / 16; br.ReadUInt16();
                body1.aluv[x] = new Vector2(tx / 256.0f, ty / 256.0f);
                body1.alvi[x] = br.ReadUInt16(); br.ReadUInt16();
                body1.alfl[x] = br.ReadUInt16(); br.ReadUInt16();
            }

            if (v28 != 0) {
                si.Position = 16 * (tops + v2c);
                int vt0 = br.ReadInt32();
                int vt1 = br.ReadInt32();
                int vt2 = br.ReadInt32();
                int vt3 = br.ReadInt32();
                int vt4 = 0;
                if (v28 >= 5) {
                    vt4 = br.ReadInt32();
                    br.ReadInt32();
                    br.ReadInt32();
                    br.ReadInt32();
                }
                Vector3[] allocalvert = new Vector3[v30];
                int xi = 0;
                for (xi = 0; xi < vt0; xi++) {
                    int ai = br.ReadInt32();
                    allocalvert[xi] = body1.alvert[ai];
                }
                if (v28 >= 2) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt1; x++, xi++) {
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        allocalvert[xi] = alv4[i0] + alv4[i1];
                    }
                }
                if (v28 >= 3) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt2; x++, xi++) {
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        allocalvert[xi] = alv4[i0] + alv4[i1] + alv4[i2];
                    }
                }
                if (v28 >= 4) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt3; x++, xi++) {
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        allocalvert[xi] = alv4[i0] + alv4[i1] + alv4[i2] + alv4[i3];
                    }
                }
                if (v28 >= 5) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt4; x++, xi++) {
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        int i4 = br.ReadInt32();
                        allocalvert[xi] = alv4[i0] + alv4[i1] + alv4[i2] + alv4[i3] + alv4[i4];
                    }
                }
                body1.alvert = allocalvert;
            }

            return body1;
        }
    }

    public class Body1 {
        public Vector3[] alvert = null;
        public Vector2[] aluv = null;
        public int[] alvi = null;
        public int[] alfl = null;
        public int t = -1;
        public bool avail = false;
    }

    public class ProtInvalidTypeException : ApplicationException {
        public ProtInvalidTypeException() : base("Has to be typ1 or typ2") { }
    }
}
