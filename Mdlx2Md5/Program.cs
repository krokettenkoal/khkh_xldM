using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using vconv122;
using khkh_xldMii;
using System.Drawing.Imaging;
using khkh_xldMii.V;
using hex04BinTrack;
using System.Diagnostics;
using khkh_xldMii.Models;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Models.Mdlx;
using SlimDX;

namespace Mdlx2Md5 {
    class Program {
        static void Main(string[] args) {
#if DEBUG
            //new Program().Run(@"H:\KH2fm.OpenKH\obj\P_EX100.mdlx", @"H:\Doom 3\sora\models\md5\kh2\p_ex100.md5mesh");
            new Program().Run(@"H:\KH2fm.OpenKH\obj\P_EX100.mdlx", @"C:\A\p_ex100.md5mesh");
#else
            if (args.Length < 1) {
                Console.Error.WriteLine("Mdlx2Md5 <P_EX100.mdlx>");
                Environment.Exit(1);
            }
            new Program().Run(args[0], Path.Combine(Path.GetDirectoryName(args[0]), Path.GetFileNameWithoutExtension(args[0]) + ".md5mesh"));
#endif
        }

        class Nameut {
            string dir;
            string fn;

            public Nameut(string dir, string fn) {
                this.dir = dir;
                this.fn = fn;
            }
            public string Getfp(int i) {
#if DEBUG
                return @"C:\A\" + GetMatName(i) + ".jpg";
#else
                return Path.Combine(dir, GetMatName(i) + ".jpg");
#endif
            }
            public string GetMatName(int i) {
                return Path.GetFileNameWithoutExtension(fn) + "_" + i;
            }
        }

        void Run(string fpmdlx, string fpmd5) {
            using (FileStream fmdlx = File.OpenRead(fpmdlx)) {
                Texex2[] tal = null;
                Mdlxfst mdlx = null;
                foreach (ReadBar.Barent ent in ReadBar.Explode(fmdlx)) {
                    switch (ent.k) {
                        case 7: // timf
                            tal = TIMCollection.Load(new MemoryStream(ent.bin, false));
                            break;
                        case 4: // m_ex
                            mdlx = new Mdlxfst(new MemoryStream(ent.bin, false));
                            break;
                    }
                }
                Nameut nut = new Nameut(Path.GetDirectoryName(fpmd5), Path.GetFileNameWithoutExtension(fpmd5));
                IDictionary<string, string> matSymTarDict = new SortedDictionary<string, string>();
                List<string> almatName = new List<string>();
                {
                    int texi = 0;
                    foreach (var tex in tal[0].bitmapList) {
                        string fp = nut.Getfp(texi);
                        //string name = nut.GetName(texi);
                        string matname = nut.GetMatName(texi);
                        matSymTarDict[matname] = matname;
                        almatName.Add(matname);
                        tex.bitmap.Save(fp, ImageFormat.Jpeg);
                        texi++;
                    }
                }
                foreach (K2Model t31 in mdlx.alt31) {
                    AxBone[] alaxb = t31.boneList.bones.ToArray();
                    Matrix[] Ma = new Matrix[alaxb.Length];
                    Vector3[] Va = new Vector3[Ma.Length];
                    Quaternion[] Qa = new Quaternion[Ma.Length];
                    {
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
                                    ff1 o1 = new ff1(svi + b1.alvi[x], sti + x, b1.alni[b1.alvi[x]]);
                                    alo1[ai] = o1;
                                    ai = (ai + 1) & 3;
                                    if (b1.alfl[x] == 0x20) {
                                        ff3 o3 = new ff3(almatName[b1.t],
                                            alo1[(ai - ord[0]) & 3],
                                            alo1[(ai - ord[1]) & 3],
                                            alo1[(ai - ord[2]) & 3]
                                            );
                                        ffmesh.al3.Add(o3);
                                    }
                                    else if (b1.alfl[x] == 0x30) {
                                        ff3 o3 = new ff3(almatName[b1.t],
                                            alo1[(ai - ord[0]) & 3],
                                            alo1[(ai - ord[2]) & 3],
                                            alo1[(ai - ord[1]) & 3]
                                            );
                                        ffmesh.al3.Add(o3);
                                    }
                                }
                                for (int x = 0; x < b1.alvertraw.Length; x++) {
                                    Vector3 vpos = b1.alvertraw[x];// Vector3.TransformCoordinate(b1.alvertraw[x], Ma[b1.alni[x]]);
                                    ffmesh.alpos.Add(vpos);
                                }
                                for (int x = 0; x < b1.aluv.Length; x++) {
                                    Vector2 vst = b1.aluv[x];
                                    vst.Y = -vst.Y;
                                    ffmesh.alst.Add(vst);
                                }
                                svi += b1.alvertraw.Length;
                                sti += b1.aluv.Length;
                            }
                        }
                        {
                            D3Model M = new D3Model();
                            M.Qa = Qa;
                            M.Va = Va;
                            M.parents = new int[alaxb.Length];
                            for (int x = 0; x < alaxb.Length; x++) {
                                M.parents[x] = alaxb[x].parent;
                            }
                            D3Mesh Me = new D3Mesh();
                            M.alMesh.Add(Me);
                            for (int x = 0; x < ffmesh.al3.Count; x++) {
                                ff1[] F1 = ffmesh.al3[x].al1;
                                Me.alTri.Add(new int[] { 3 * x + 0, 3 * x + 1, 3 * x + 2, });

                                for (int a = 0; a < 3; a++) {
                                    D3We w0 = new D3We();
                                    w0.ji = F1[a].mi;
                                    w0.w = 1.0f;
                                    Quaternion Qo = Quaternion.Invert(Qa[w0.ji]);
                                    w0.pos = Vector3.TransformCoordinate(ffmesh.alpos[F1[a].vi], Matrix.RotationQuaternion(Qo));
                                    Me.alWe.Add(w0);
                                    D3Vert v0 = new D3Vert();
                                    v0.tex = ffmesh.alst[F1[a].ti];
                                    v0.wi = Me.alWe.Count - 1;
                                    v0.wc = 1;
                                    Me.alVert.Add(v0);
                                }
                            }
                            using (StreamWriter wr = new StreamWriter(fpmd5, false, Encoding.ASCII)) {
                                MD5MeshWriter.Write(wr, M);
                            }
                        }
                    }
                    break;
                }
            }
        }

        class QuaternionIdentify {
            public static Quaternion Run(Vector3 v) {
                float x = v.X;
                float y = v.Y;
                float z = v.Z;
                float t = 1.0f - x * x - y * y - z * z;
                float w = (t < 0)
                    ? (float)0
                    : (float)-Math.Sqrt(t);

                return new Quaternion(x, y, z, w);
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
            public int vi, ti, mi;

            public ff1(int vi, int ti, int mi) {
                this.vi = vi;
                this.ti = ti;
                this.mi = mi;
            }
        }
        class ff3 {
            public ff3(string matnamae, ff1 x, ff1 y, ff1 z) {
                this.matnamae = matnamae;
                this.al1 = new ff1[] { x, y, z };
            }

            public ff1[] al1;
            public string matnamae;
        }
        class ffMesh {
            public List<Vector3> alpos = new List<Vector3>();
            public List<Vector2> alst = new List<Vector2>();
            public List<ff3> al3 = new List<ff3>();
        }

        class D3Vert {
            public Vector2 tex;
            public int wi, wc;
        }
        class D3We {
            public int ji;
            public float w;
            public Vector3 pos;
        }
        class D3Mesh {
            public string matname;
            public List<D3Vert> alVert = new List<D3Vert>();
            public List<int[]> alTri = new List<int[]>();
            public List<D3We> alWe = new List<D3We>();
        }
        class D3Model {
            public List<D3Mesh> alMesh = new List<D3Mesh>();
            public Vector3[] Va;
            public Quaternion[] Qa;
            public int[] parents;
        }

        class VUt {
            public static string GetStr(float v) {
                return v.ToString("0.0000000000");
            }
        }

        class MD5MeshWriter {
            public static void Write(TextWriter wr, D3Model M) {
                Vector3[] Va = M.Va;
                Quaternion[] Qa = M.Qa;
                int cj = M.Va.Length;
                int cm = M.alMesh.Count;
                Debug.Assert(Va.Length == Qa.Length);
                wr.WriteLine("MD5Version 10");
                wr.WriteLine("commandline \"test\"");
                wr.WriteLine("numJoints " + cj);
                wr.WriteLine("numMeshes " + cm);
                wr.WriteLine("joints {");
                for (int j = 0; j < cj; j++) {
                    string jn = "origin";
                    if (j != 0) jn = "Joint" + j;
                    Quaternion Qo = Qa[j];
                    Qo.Normalize();
                    wr.WriteLine("\t" + "\"" + jn + "\" " + M.parents[j] + " " + String.Format("( {0} {1} {2} ) ( {3} {4} {5} )"
                        , VUt.GetStr(Va[j].X)
                        , VUt.GetStr(Va[j].Y)
                        , VUt.GetStr(Va[j].Z)
                        , VUt.GetStr(Qo.X)
                        , VUt.GetStr(Qo.Y)
                        , VUt.GetStr(Qo.Z)
                        ));
                }
                wr.WriteLine("}");
                for (int m = 0; m < cm; m++) {
                    D3Mesh Me = M.alMesh[m];
                    wr.WriteLine("mesh {");
                    Me.matname = "models/monsters/imp/imp";
                    wr.WriteLine("\t" + "shader \"" + Me.matname + "\"");
                    int vc = Me.alVert.Count;
                    wr.WriteLine("\t" + "numverts " + vc);
                    for (int vi = 0; vi < vc; vi++) {
                        D3Vert o = Me.alVert[vi];
                        wr.WriteLine("\t" + "vert " + vi + " " + String.Format("( {0} {1:r} ) {2} {3}"
                            , VUt.GetStr(o.tex.X)
                            , VUt.GetStr(o.tex.Y)
                            , o.wi, o.wc
                            ));
                    }
                    int tc = Me.alTri.Count;
                    wr.WriteLine("\t" + "numtris " + tc);
                    for (int ti = 0; ti < tc; ti++) {
                        int[] o = Me.alTri[ti];
                        Debug.Assert(o.Length == 3);
                        wr.WriteLine("\t" + "tri " + ti + " " + String.Format("{0} {1} {2}", o[0], o[1], o[2]));
                    }
                    int wc = Me.alWe.Count;
                    wr.WriteLine("\t" + "numweights " + wc);
                    for (int wi = 0; wi < wc; wi++) {
                        D3We o = Me.alWe[wi];
                        wr.WriteLine("\t" + "weight " + wi + " " + o.ji + " " + String.Format("{0} ( {1} {2} {3} )"
                            , VUt.GetStr(o.w)
                            , VUt.GetStr(o.pos.X)
                            , VUt.GetStr(o.pos.Y)
                            , VUt.GetStr(o.pos.Z)
                            ));
                    }
                    wr.WriteLine("}");
                }
            }
        }
    }
}
