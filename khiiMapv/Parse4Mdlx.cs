using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;
using khkh_xldMii.V;
using System.IO;
using hex04BinTrack;
using System.Diagnostics;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Models;
using khkh_xldMii.Models.Mdlx;
using System.Linq;

namespace khiiMapv {
    public class Parse4Mdlx {
        public Mdlxfst mdlx;

        public Parse4Mdlx(byte[] entbin) {
            mdlx = new Mdlxfst(new MemoryStream(entbin, false));

            float scalf = 1f;

            foreach (K2Model t31 in mdlx.alt31) {
                AxBone[] alaxb = (t31.boneList ?? mdlx.alt31.First().boneList).bones.ToArray();
                Matrix[] Ma = new Matrix[alaxb.Length];
                {
                    Vector3[] Va = new Vector3[Ma.Length];
                    Quaternion[] Qa = new Quaternion[Ma.Length];
                    for (int x = 0; x < Ma.Length; x++) {
                        Quaternion Qo;
                        Vector3 Vo;
                        AxBone axb = alaxb[x];
                        int parent = axb.parent;
                        if (parent < 0) {
                            Qo = Quaternion.Identity;
                            Vo = Vector3.Zero;
                        }
                        else {
                            Qo = Qa[parent];
                            Vo = Va[parent];
                        }

                        Vector3 Vt = Vector3.TransformCoordinate(new Vector3(axb.x3, axb.y3, axb.z3), Matrix.RotationQuaternion(Qo));
                        Va[x] = Vo + Vt;

                        Quaternion Qt = Quaternion.Identity;
                        if (axb.x2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(1, 0, 0), axb.x2));
                        if (axb.y2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 1, 0), axb.y2));
                        if (axb.z2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 0, 1), axb.z2));
                        Qa[x] = Qt * Qo;
                    }
                    for (int x = 0; x < Ma.Length; x++) {
                        Matrix M = Matrix.RotationQuaternion(Qa[x]);
                        M *= (Matrix.Translation(Va[x]));
                        Ma[x] = M;
                    }
                }
                List<Body1e> albody1 = new List<Body1e>();
                Matrix Mv = Matrix.Identity;
                foreach (K2Vif t13 in t31.vifList) {
                    VU1Mem mem = new VU1Mem();
                    int tops = 0x40, top2 = 0x220;
                    new ParseVIF1(mem).Parse(new MemoryStream(t13.bin, false), tops);
                    Body1e b1 = SimaVU1e.Sima(mem, Ma, tops, top2, t13.textureIndex, t13.marixIndexArray, Mv);
                    albody1.Add(b1);
                }

                if (true) {
                    ffMesh ffmesh = new ffMesh();
                    if (true) {
                        int svi = 0;
                        int sti = 0;
                        ff1[] alo1 = new ff1[4];
                        int ai = 0;
                        int[] ord = new int[] { 1, 3, 2 };
                        foreach (Body1e b1 in albody1) {
                            for (int x = 0; x < b1.alvi.Length; x++) {
                                ff1 o1 = new ff1(svi + b1.alvi[x], sti + x);
                                alo1[ai] = o1;
                                ai = (ai + 1) & 3;
                                int fl = b1.alfl[x];
                                if (fl == 0x20 || fl == 0x00) {
                                    ff3 o3 = new ff3(b1.t,
                                        alo1[(ai - ord[0]) & 3],
                                        alo1[(ai - ord[1]) & 3],
                                        alo1[(ai - ord[2]) & 3]
                                        );
                                    ffmesh.al3.Add(o3);
                                }
                                if (fl == 0x30 || fl == 0x00) {
                                    ff3 o3 = new ff3(b1.t,
                                        alo1[(ai - ord[0]) & 3],
                                        alo1[(ai - ord[2]) & 3],
                                        alo1[(ai - ord[1]) & 3]
                                        );
                                    ffmesh.al3.Add(o3);
                                }
                            }
                            for (int x = 0; x < b1.alvertraw.Length; x++) {
                                if (b1.alalni[x] == null) {
                                    ffmesh.alpos.Add(Vector3.Zero);
                                    ffmesh.almtxuse.Add(new MJ1[0]);
                                    continue;
                                }
                                if (b1.alalni[x].Length == 1) {
                                    MJ1 mj1 = b1.alalni[x][0];
                                    mj1.factor = 1.0f;
                                    Vector3 vpos = Vector3.TransformCoordinate(VCUt.V4To3(b1.alvertraw[mj1.vertexIndex]), Ma[mj1.matrixIndex]);
                                    ffmesh.alpos.Add(vpos);
                                }
                                else {
                                    Vector3 vpos = Vector3.Zero;
                                    foreach (MJ1 mj1 in b1.alalni[x]) {
                                        vpos += VCUt.V4To3(Vector4.Transform(b1.alvertraw[mj1.vertexIndex], Ma[mj1.matrixIndex]));
                                    }
                                    ffmesh.alpos.Add(vpos);
                                }
                                ffmesh.almtxuse.Add(b1.alalni[x]);
                            }
                            for (int x = 0; x < b1.aluv.Length; x++) {
                                Vector2 vst = b1.aluv[x];
                                vst.Y = 1.0f - vst.Y; // !
                                ffmesh.alst.Add(vst);
                            }
                            svi += b1.alvertraw.Length;
                            sti += b1.aluv.Length;
                        }
                    }

                    {   // position xyz
                        int ct = ffmesh.al3.Count;
                        for (int t = 0; t < ct; t++) {
                            ff3 X3 = ffmesh.al3[t];
                            Model model;
                            var dict = (t31.type == 3) ? dictModel : dictShadow;
                            if (dict.TryGetValue(X3.texi, out model) == false) {
                                dict[X3.texi] = model = new Model();
                            }
                            for (int i = 0; i < X3.al1.Length; i++) {
                                ff1 X1 = X3.al1[i];
                                Vector3 v = (ffmesh.alpos[X1.vi] * scalf);
                                Vector2 txy = ffmesh.alst[X1.ti];
                                model.alv.Add(new CustomVertex.PositionColoredTextured(v, -1, txy.X, 1 - txy.Y));
                            }
                        }
                    }
                }
            }
        }

        public class Model {
            public List<CustomVertex.PositionColoredTextured> alv = new List<CustomVertex.PositionColoredTextured>();
        }

        public SortedDictionary<int, Model> dictModel = new SortedDictionary<int, Model>();
        public SortedDictionary<int, Model> dictShadow = new SortedDictionary<int, Model>();

        class LocalsMJ1 : IComparer<MJ1> {
            #region IComparer<MJ1> メンバ

            public int Compare(MJ1 x, MJ1 y) {
                int v;
                v = x.matrixIndex.CompareTo(y.matrixIndex); if (v != 0) return v;
                v = -x.factor.CompareTo(y.factor); if (v != 0) return v;
                return 0;
            }

            #endregion
        }

        class Mati {
            public string matname, texname;
            public string fp;

            public Mati(string basename, string fp) {
                this.matname = "mat" + basename;
                this.texname = "tex" + basename;
                this.fp = fp;
            }
        }

        Naming naming = new Naming();

        class Naming {
            public string B(int x) {
                return "B" + x.ToString("000");
            }
        }

        class VCUt {
            public static Vector3 V4To3(Vector4 v) {
                return new Vector3(v.X, v.Y, v.Z);
            }
        }

        class LUt {
            public static string GetFaces(ff3 X3) {
                String s = "";
                s += String.Format("{0},{1},{2}", X3.al1[0].vi, X3.al1[1].vi, X3.al1[2].vi);
                return s;
            }

            public static object GetUV(ff3 X3, List<Vector2> alst) {
                String s = "";
                foreach (ff1 X1 in X3.al1) {
                    Vector2 v = alst[X1.ti];
                    s += String.Format("Blender.Mathutils.Vector({0:r},{1:r}),", v.X, v.Y);
                }
                return s;
            }

            public static string GetVi(List<int> al, IDictionary<int, List<int>> map) {
                String s = "";
                foreach (int v in al)
                    if (map.ContainsKey(v))
                        foreach (int vv in map[v])
                            s += vv + ",";
                return "[" + s + "]";
            }
        }

        class Sepa {
            public int svi;
            public int cnt;
            public int t;
            public int sel;

            public Sepa(int startVertexIndex, int cntPrimitives, int ti, int sel) {
                this.svi = startVertexIndex;
                this.cnt = cntPrimitives;
                this.t = ti;
                this.sel = sel;
            }
        }

        class ff1 {
            public int vi, ti;

            public ff1(int vi, int ti) {
                this.vi = vi;
                this.ti = ti;
            }
        }
        class ff3 {
            public ff3(int texi, ff1 x, ff1 y, ff1 z) {
                this.texi = texi;
                this.al1 = new ff1[] { x, y, z };
            }

            public ff1[] al1;
            public int texi;
        }
        class ffMesh {
            public List<Vector3> alpos = new List<Vector3>();
            public List<Vector2> alst = new List<Vector2>();
            public List<ff3> al3 = new List<ff3>();
            public List<MJ1[]> almtxuse = new List<MJ1[]>();
        }
    }
}
