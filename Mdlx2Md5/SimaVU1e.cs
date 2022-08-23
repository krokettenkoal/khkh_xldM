using System;
using System.Collections.Generic;
using System.Text;
using hex04BinTrack;
using System.Diagnostics;
using System.IO;
using SlimDX;

// @ 9:04 2008/04/12 Mdlx2Md5

namespace khkh_xldMii.V {
    public class SimaVU1e {
        public static Body1e Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv) {
            MemoryStream si = new MemoryStream(vu1mem.vumem, true);
            BinaryReader br = new BinaryReader(si);

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

            si.Position = 16 * (tops + v18);
            int[] box1 = new int[v3c];
            for (int x = 0; x < box1.Length; x++) {
                box1[x] = br.ReadInt32();
            }

            Body1e body1 = new Body1e();
            body1.t = tsel;
            body1.alvertraw = new Vector3[v30];
            body1.avail = (v28 == 0) && (v00 == 1);
            body1.alni = new int[v30];

            int vi = 0;
            si.Position = 16 * (tops + v34);
            for (int x = 0; x < box1.Length; x++) {
                int ct = box1[x];
                for (int t = 0; t < ct; t++, vi++) {
                    body1.alni[vi] = alaxi[x];
                    float fx = br.ReadSingle();
                    float fy = br.ReadSingle();
                    float fz = br.ReadSingle();
                    float fw = br.ReadSingle();
                    Vector3 v3 = new Vector3(fx, fy, fz);
                    body1.alvertraw[vi] = Vector3.TransformCoordinate(v3, Mv);
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
                Vector3[] allocalvert = new Vector3[v30];
                int xi = 0;
                for (xi = 0; xi < vt0; xi++) {
                    int ai = br.ReadInt32();
                    allocalvert[xi] = body1.alvertraw[ai];
                }
                if (v28 >= 2) {
                    Debug.Fail("v28: " + v28);
                }
                if (v28 >= 3) {
                    Debug.Fail("v28: " + v28);
                }
                body1.alvertraw = allocalvert;
            }

            return body1;
        }
    }

    public class Body1e {
        public Vector3[] alvertraw = null; // small
        public Vector2[] aluv = null; // large
        public int[] alvi = null; // large2small
        public int[] alfl = null; // large
        public int[] alni = null; // small
        public int t = -1;
        public bool avail = false;
    }

    public class ProtInvalidTypeException : ApplicationException {
        public ProtInvalidTypeException() : base("Has to be typ1 or typ2") { }
    }
}
