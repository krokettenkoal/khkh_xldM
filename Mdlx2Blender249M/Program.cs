using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using khkh_xldMii;
using khkh_xldMii.V;
using hex04BinTrack;
using vconv122;
using System.Diagnostics;
using System.Windows.Forms;
using Mdlx2Blender249M.Properties;
using SlimDX;
using khkh_xldMii.Models;
using khkh_xldMii.Models.Mset;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Models.Mdlx;

namespace Mdlx2Blender249M {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpYa(); return;
            }
            switch (args[0].ToLowerInvariant()) {
                case "/conv2":
                    if (args.Length < 4) {
                        helpYa(); return;
                    }
                    new Program().Run(args[1], args[2], args[3]);
                    break;

                case "/select2": {
                        Form form = new Form();

                        OpenFileDialog ofdMdlx = new OpenFileDialog();
                        ofdMdlx.Filter = "*.mdlx|*.mdlx||";
                        ofdMdlx.Title = "Select a mdlx";
                        ofdMdlx.Multiselect = false;
                        if (ofdMdlx.ShowDialog(form) == DialogResult.OK) {
                            OpenFileDialog ofdDat = new OpenFileDialog();
                            ofdDat.Filter = "*.dat|*.dat||";
                            ofdDat.Title = "Select a matrix dump";
                            ofdDat.Multiselect = false;
                            if (ofdDat.ShowDialog(form) == DialogResult.OK) {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "Blender py (*.py)|*.py||";
                                sfd.FileName = Path.Combine(Path.GetDirectoryName(ofdMdlx.FileName), Path.GetFileNameWithoutExtension(ofdMdlx.FileName).ToLowerInvariant() + ".py");
                                sfd.Title = "Save py";
                                if (sfd.ShowDialog(form) == DialogResult.OK) {
                                    new Program().Run(ofdMdlx.FileName, ofdDat.FileName, sfd.FileName);
                                }
                            }
                        }
                    }
                    break;

                case "/bfa05dec-7976-4d79-a1b9-cf5a32c1f628":
                    //new Program().Run(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX110.mdlx", @"C:\A\p_ex110.py");
                    new Program().Run(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX100.mdlx", @"H:\DL\khkh_xldMii 8 (hacked)\A001_A001_2.dat", @"C:\A\p_ex100b.py");

                    //new Program().Run(@"H:\KH2.yaz0r\dump_kh2\obj\H_EX500_BTLF.mdlx", @"C:\A\h_ex500_btlf.py");
                    //new Program().Run(@"H:\KH2.yaz0r\dump_kh2\obj\H_EX740.mdlx", @"C:\A\h_ex740.py");
                    //new Program().Run(@"H:\KH2.yaz0r\dump_kh2\obj\H_ZZ010.mdlx", @"C:\A\h_zz010.py");
                    break;

                default:
                    helpYa();
                    break;
            }
        }

        private static void helpYa() {
            Console.Error.WriteLine("Mdlx2Blender249M /conv2 p_ex100.mdlx unk.dat p_ex100.py");
            Console.Error.WriteLine("Mdlx2Blender249M /select2");
            Environment.Exit(1);
        }

        // # Import instruction:
        // # * Launch Blender 2.4.8a
        // # * In Blender, type Shift+F11, then open then Script Window
        // # * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py
        // # * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!
        // # * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.

        void Run(String fpmdlx, String fpdat, String fpout) {
            if (fpmdlx == null) throw new NullReferenceException("fpmdlx");
            if (fpdat == null) throw new NullReferenceException("fpdat");
            if (fpout == null) throw new NullReferenceException("fpout");
            Texex2[] tal = null;
            Mdlxfst mdlx = null;
            using (FileStream fmdlx = File.OpenRead(fpmdlx)) {
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
            }
            MatrixDat dat = new MatrixDat();
            using (FileStream fdat = File.OpenRead(fpdat)) {
                dat.Read(new BinaryReader(fdat));
            }
            StringWriter wr1 = new StringWriter();
            wr1.WriteLine(Properties.Resources.Base);
            wr1.WriteLine("print 'IMPORTER3.4M-1 -- {0} @{1} {2}'"
                , Path.GetFileName(fpmdlx)
                , DateTime.Now.ToShortTimeString()
                , DateTime.Now.ToShortDateString()
                );
            wr1.WriteLine("print 'start --'");

            foreach (T31 t31 in mdlx.alt31) {
                List<MdlxBSProvider> alBSP = new List<MdlxBSProvider>();
                alBSP.Add(new MdlxBSProvider(t31.t21.alaxb.ToArray()));

                foreach (MdlxBSProvider bsp in alBSP) {
                    AxBone[] alaxb = bsp.alaxb;
                    Matrix[] Ma = bsp.Ma;
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
                        for (int x = 0; x < Ma.Length; x++)
                            Ma[x] = Matrix.Identity;
                    }
                    List<Body1e> albody1 = new List<Body1e>();
                    Matrix Mv = Matrix.Identity;
                    foreach (T13vif t13 in t31.al13) {
                        VU1Mem mem = new VU1Mem();
                        int tops = 0x40, top2 = 0x220;
                        new ParseVIF1(mem).Parse(new MemoryStream(t13.bin, false), tops);
                        Body1e b1 = SimaVU1e.Sima(mem, Ma, tops, top2, t13.texi, t13.alaxi, Mv);
                        albody1.Add(b1);
                    }

                    List<Mati> almat = new List<Mati>();
                    {
                        int texi = 0;
                        foreach (var tex in tal[0].bitmapList) {
                            string matname = Path.GetFileNameWithoutExtension(fpmdlx).Replace(".", "_").ToLowerInvariant() + "x" + texi;
                            string name = matname + ".png";
                            string fp = Path.Combine(Path.GetDirectoryName(fpout), name);
                            almat.Add(new Mati(matname, fp));
                            tex.bitmap.Save(fp, System.Drawing.Imaging.ImageFormat.Png);
                            texi++;
                        }
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
                                    if (b1.alfl[x] == 0x20) {
                                        ff3 o3 = new ff3(b1.t,
                                            alo1[(ai - ord[0]) & 3],
                                            alo1[(ai - ord[1]) & 3],
                                            alo1[(ai - ord[2]) & 3]
                                            );
                                        ffmesh.al3.Add(o3);
                                    }
                                    else if (b1.alfl[x] == 0x30) {
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
                                        Vector3 mixedPos = Vector3.Zero;
                                        foreach (MJ1 mj1 in b1.alalni[x]) {
                                            mixedPos += VCUt.V4To3(Vector4.Transform(b1.alvertraw[mj1.vertexIndex], Ma[mj1.matrixIndex]));
                                        }
                                        ffmesh.alpos.Add(mixedPos);
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

                        SortedDictionary<int, List<int>> map = new SortedDictionary<int, List<int>>();
                        wr1.WriteLine("mat = MyMats()");
                        {   // materials
                            for (int x = 0; x < almat.Count; x++) {
                                Mati M = almat[x];
                                wr1.WriteLine("mat.AddImage('{0}', '{1}', '{2}', True)", M.texname, M.matname, M.fp.Replace("\\", "/"));
                            }
                        }

                        {   // joints, bones
                            wr1.WriteLine("bone = MyBone()");
                            wr1.WriteLine("bone.Prepare(scene, 1)");
                            wr1.WriteLine("bone.AddBone('ROOT', None, 0, 0, 0, 0, 1, 0)");
                            for (int x = 0; x < Ma.Length; x++) {
                                Vector3 v0 = (Vector3.TransformCoordinate(new Vector3(0, 0, 0), Ma[x]));
                                Vector3 v1 = (Vector3.TransformCoordinate(new Vector3(0, 1, 0), Ma[x]));

                                wr1.WriteLine("bone.AddBone('{0}', {1}, {2:r}, {3:r}, {4:r}, {5:r}, {6:r}, {7:r})"
                                    , naming.B(x)
                                    , "'ROOT'"
                                    , v0.X, v0.Y, v0.Z
                                    , v1.X, v1.Y, v1.Z
                                    );
                            }
                            wr1.WriteLine("bone.PrepareEnd()");
                        }

                        wr1.WriteLine("alMyMesh = []");
                        wr1.WriteLine("ya = MyMesh()");
                        wr1.WriteLine("ya.PrepareMesh('Sola')");

                        {   // position xyz
                            int mapi = 0;
                            int ct = ffmesh.al3.Count;
                            wr1.WriteLine("ya.AddCoords([");
                            for (int t = 0; t < ct; t++) {
                                ff3 X3 = ffmesh.al3[t];
                                for (int i = 0; i < X3.al1.Length; i++) {
                                    ff1 X1 = X3.al1[i];
                                    Vector3 v = (ffmesh.alpos[X1.vi]);
                                    wr1.WriteLine(" [{0:r},{1:r},{2:r}], # pos{3}.{4}", v.X, v.Y, v.Z, t, i);

                                    if (map.ContainsKey(X1.vi) == false) map[X1.vi] = new List<int>();
                                    map[X1.vi].Add(mapi); mapi++;
                                }
                            }
                            wr1.WriteLine(" ])");
                        }
                        {   // mats
                            for (int x = 0; x < almat.Count; x++) {
                                wr1.WriteLine("ya.AddMat(mat.GetMat({0}))", x);
                            }
                        }
                        {   // AddColorUvMatFaces
                            int cf = ffmesh.al3.Count;
                            wr1.WriteLine("ya.AddColorUvMatFaces(");
                            // faces
                            wr1.WriteLine(" [");
                            for (int f = 0; f < cf; f++) {
                                wr1.WriteLine(" [{0}, {1}, {2}], #face{3} tri", 3 * f + 0, 3 * f + 1, 3 * f + 2, f);
                            }
                            wr1.WriteLine(" ],[");
                            // clr
                            for (int f = 0; f < cf; f++) {
                                wr1.WriteLine(" [[255,255,255,255],[255,255,255,255],[255,255,255,255]], #face{0} clr", f);
                            }
                            wr1.WriteLine(" ],[");
                            // uv
                            for (int f = 0; f < cf; f++) {
                                wr1.WriteLine(" [{0}], #face{1} uv", LUt.GetUV2(ffmesh.al3[f], ffmesh.alst), f);
                            }
                            wr1.WriteLine(" ],[");
                            // matimg
                            for (int f = 0; f < cf; f++) {
                                wr1.WriteLine(" [{0}, mat.GetImage({1})], #face{2} MatImg", ffmesh.al3[f].mati, ffmesh.al3[f].mati, f);
                            }
                            wr1.WriteLine(" ]");
                            wr1.WriteLine(" )");
                        }

                        wr1.WriteLine("ya.MeshToOb('Sola0')");

                        {   // vertgrps

                            SortedDictionary<MJ1, List<int>> dict = new SortedDictionary<MJ1, List<int>>(new LocalsMJ1()); // [grp] = vi[]
                            for (int x = 0; x < ffmesh.almtxuse.Count; x++) {
                                foreach (MJ1 k in ffmesh.almtxuse[x]) {
                                    int v = x;
                                    if (dict.ContainsKey(k) == false) dict[k] = new List<int>();
                                    dict[k].Add(v);
                                }
                            }
                            foreach (KeyValuePair<MJ1, List<int>> kv in dict) {
                                wr1.WriteLine("ya.SetVertGr2('{0}',{1:r},{2})"
                                    , naming.B(kv.Key.matrixIndex)
                                    , kv.Key.factor
                                    , LUt.GetVi(kv.Value, map)
                                    );
                            }
                        }

                        wr1.WriteLine("bone.AddChildWithArmature(ya.yaOb)");
                        wr1.WriteLine("alMyMesh.append(ya)");
                    }
                    {
                        String key = "key";

                        int cnt1 = alaxb.Length;

                        wr1.WriteLine("Aniut2.SetPoseMtx('{0}', bone, [", key);
                        for (int t = 0; t < dat.cntFrames; t++) {
                            wr1.WriteLine(" {'frame':" + (1 + t) + ",");
                            wr1.WriteLine("  'joints': [");
                            for (int ji = 0; ji < cnt1; ji++) {
                                {
                                    int pi = alaxb[ji].parent;
                                    String bName = naming.B(ji);
                                    wr1.Write("   {'b':'" + bName + "',");
                                    Matrix m = dat.GetMatrix(t, ji);
                                    wr1.Write("'matrix':[ [{0:r}, {1:r}, {2:r}, {3:r}], [{4:r}, {5:r}, {6:r}, {7:r}], [{8:r}, {9:r}, {10:r}, {11:r}], [{12:r}, {13:r}, {14:r}, {15:r}], ],"
                                        , m.M11, m.M12, m.M13, m.M14
                                        , m.M11, m.M22, m.M23, m.M24
                                        , m.M31, m.M32, m.M33, m.M34
                                        , m.M41, m.M42, m.M43, m.M44
                                        );
                                    wr1.WriteLine("},");
                                }
                            }
                            wr1.WriteLine("  ]");
                            wr1.WriteLine(" },");
                        }
                        wr1.WriteLine(" ])");
                    }
                    wr1.WriteLine("for o in alMyMesh:");
                    wr1.WriteLine(" o.yaOb.select(False)");
                    wr1.WriteLine("bone.SetScale(0.1, 0.1, 0.1)");
                    wr1.WriteLine("bone.armObj.select(True)");
                    wr1.WriteLine("scene.update(1)");

                    wr1.WriteLine();

                    wr1.WriteLine("Blender.Window.RedrawAll()");
                    wr1.WriteLine("print 'end --'");
                    break;
                }
                break;
            }

            String strsave = wr1.ToString();
#if DEBUG
            File.WriteAllText(@"H:\Proj\khkh_xldM\Mdlx2Blender249M\blender\testimp.py", strsave, Encoding.ASCII);
#endif
            File.WriteAllText(fpout, strsave, Encoding.ASCII);
        }

        class MsetBSProvider : MdlxBSProvider {
            internal AnbModel blk;
            internal String key;

            internal MsetBSProvider(AxBone[] alaxb, AnbModel blk, String key) {
                this.blk = blk;
                this.key = key;

                int cnt1 = alaxb.Length;
                int cnt2 = blk.t5List.Count;

                this.alaxb = alaxb = UtF.FillKeyframe(alaxb, blk, float.NaN, true).ToArray();
                this.Ma = new Matrix[alaxb.Length];

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
        }

        class MdlxBSProvider {
            internal AxBone[] alaxb;
            internal Matrix[] Ma;

            internal MdlxBSProvider() { }

            internal MdlxBSProvider(AxBone[] alaxb) {
                this.alaxb = alaxb;
                this.Ma = new Matrix[alaxb.Length];

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
        }

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

            public static object GetUV2(ff3 X3, List<Vector2> alst) {
                String s = "";
                foreach (ff1 X1 in X3.al1) {
                    Vector2 v = alst[X1.ti];
                    s += String.Format("[{0:r},{1:r}],", v.X, v.Y);
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
            public ff3(int mati, ff1 x, ff1 y, ff1 z) {
                this.mati = mati;
                this.al1 = new ff1[] { x, y, z };
            }

            public ff1[] al1;
            public int mati;
        }
        class ffMesh {
            public List<Vector3> alpos = new List<Vector3>();
            public List<Vector2> alst = new List<Vector2>();
            public List<ff3> al3 = new List<ff3>();
            public List<MJ1[]> almtxuse = new List<MJ1[]>();
        }

        class Vw {
            public int vi;
            public int wi;
        }
        class Skin {
            public AxBone[] alaxb;
            public List<Vw[]> alvw = new List<Vw[]>();
            public List<float> alw = new List<float>();
        }

        /// <summary>
        /// UtFrame
        /// </summary>
        class UtF {
            public static List<AxBone> FillKeyframe(AxBone[] alaxb1, AnbModel toval, float frame, bool fInc) {
                List<AxBone> alres = new List<AxBone>();
                int cnt1 = fInc ? alaxb1.Length : (alaxb1.Length - toval.t5List.Count);
                int cnt2 = toval.t5List.Count;
                int cx = cnt1 + cnt2;
                for (int x = 0; x < cx; x++) {
                    bool is1st = x < cnt1;
                    AxBone axb = (is1st ? alaxb1[x] : toval.t5List[x - cnt1]).Clone();
                    if (!float.IsNaN(frame)) {
                        foreach (T1 t1 in toval.t1List) {
                            if (t1.c00 == x) {
                                float fv = t1.c04;
                                switch (t1.c02) {
                                    case 0: axb.x1 = fv; break;// Sx
                                    case 1: axb.y1 = fv; break;// Sy
                                    case 2: axb.z1 = fv; break;// Sz
                                    case 3: axb.x2 = fv; break;// Rx
                                    case 4: axb.y2 = fv; break;// Ry
                                    case 5: axb.z2 = fv; break;// Rz
                                    case 6: axb.x3 = fv; break;// Tx
                                    case 7: axb.y3 = fv; break;// Ty
                                    case 8: axb.z3 = fv; break;// Tz
                                }
                            }
                        }
                        if (is1st)
                            foreach (T2 t2 in toval.t2List) {
                                int pos = t2.c00;
                                int ax = t2.c02;

                                int tpos = 8 * pos + (ax & 15) - 3;
                                int xpos = tpos / 8;
                                if (xpos == x) {
                                    float yv = YVal.calc2(frame, t2.al9f);

                                    if (false) { }
                                    else if ((tpos % 8) == 0)
                                        axb.x2 = yv;
                                    else if ((tpos % 8) == 1)
                                        axb.y2 = yv;
                                    else if ((tpos % 8) == 2)
                                        axb.z2 = yv;
                                    else if ((tpos % 8) == 3)
                                        axb.x3 = yv;
                                    else if ((tpos % 8) == 4)
                                        axb.y3 = yv;
                                    else if ((tpos % 8) == 5)
                                        axb.z3 = yv;
                                }
                            }
                        else
                            foreach (T2 t2 in toval.t2xList) {
                                int pos = t2.c00;
                                int ax = t2.c02;

                                int tpos = 8 * pos + (ax & 15) - 3;
                                int xpos = cnt1 + tpos / 8;
                                if (xpos == x) {
                                    float yv = YVal.calc2(frame, t2.al9f);

                                    if (false) { }
                                    else if ((tpos % 8) == 0)
                                        axb.x2 = yv;
                                    else if ((tpos % 8) == 1)
                                        axb.y2 = yv;
                                    else if ((tpos % 8) == 2)
                                        axb.z2 = yv;
                                    else if ((tpos % 8) == 3)
                                        axb.x3 = yv;
                                    else if ((tpos % 8) == 4)
                                        axb.y3 = yv;
                                    else if ((tpos % 8) == 5)
                                        axb.z3 = yv;
                                }
                            }
                    }
                    alres.Add(axb);
                }
                return alres;
            }

            [Flags]
            public enum M { // Mask
                None = 0, S = 1, R = 2, T = 4,
                All = S | R | T,
            }

            class SUt {
                SortedDictionary<float, M[]> dict;
                int cnt12;

                internal SUt(SortedDictionary<float, M[]> dict, int cnt12) {
                    this.dict = dict;
                    this.cnt12 = cnt12;
                }

                internal void Set(float frame, int ji, M val) {
                    if (dict.ContainsKey(frame) == false) {
                        dict[frame] = new M[cnt12];
                    }

                    dict[frame][ji] |= val;
                }
            }

            public static SortedDictionary<float, M[]> GetKeyframes(int cnt1, int cnt2, AnbModel toval) {
                SortedDictionary<float, M[]> dict = new SortedDictionary<float, M[]>();
                SUt sUt = new SUt(dict, cnt1 + cnt2);
                foreach (T2 t2 in toval.t2List) {
                    int pos = t2.c00;
                    int ax = t2.c02;

                    int tpos = 8 * pos + (ax & 15) - 3;
                    int xpos = tpos / 8;
                    foreach (T9f t9 in t2.al9f) {
                        if (false) { }
                        else if ((tpos % 8) == 0)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 1)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 2)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 3)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 4)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 5)
                            sUt.Set(t9.v0, xpos, M.T);
                    }
                }
                foreach (T2 t2 in toval.t2xList) {
                    int pos = t2.c00;
                    int ax = t2.c02;

                    int tpos = 8 * pos + (ax & 15) - 3;
                    int xpos = cnt1 + tpos / 8;
                    foreach (T9f t9 in t2.al9f) {
                        if (false) { }
                        else if ((tpos % 8) == 0)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 1)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 2)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 3)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 4)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 5)
                            sUt.Set(t9.v0, xpos, M.T);
                    }
                }
                return dict;
            }

            class YVal {
                public static float calc2(float fpos, List<T9f> alt9) {
                    T9f t9a = null;
                    for (int x9 = 0; x9 < alt9.Count; x9++) {
                        T9f t9b = alt9[x9];
                        if (t9a != null) {
                            if (fpos <= t9b.v0) {
                                float yv = Composite2.calc(
                                    fpos,
                                    t9a.v0, t9a.v1, t9a.v3,
                                    t9b.v0, t9b.v1, t9b.v2
                                    );

                                return yv;
                            }
                        }
                        t9a = t9b;
                    }
                    if (t9a != null)
                        return t9a.v1;
                    return float.NaN;
                }

            }

            class Composite2 {
                public static float calc(float tick, float v0x, float v0y, float v0w, float v1x, float v1y, float v1z) {
                    tick -= v0x;
                    v1x -= v0x;
                    float ratio = tick / v1x;
                    float r1 = ((1.0f - ratio) * v0y) + (ratio * v1y);
                    float r2 = ((1.0f - ratio) * v0w) + (ratio * v1z);
                    return r1 + r2;
                }
            }
        }
    }

    public class MatrixDat {
        internal Matrix[] matrices = new Matrix[0];
        internal int cntMatrices = 0;
        internal int cntFrames = 0;

        public void Read(BinaryReader br) {
            Stream fs = br.BaseStream;
            cntMatrices = Convert.ToInt32(br.ReadUInt32());
            cntFrames = Convert.ToInt32(br.ReadUInt32());
            int cnt = cntMatrices * cntFrames;
            matrices = new Matrix[cnt];
            for (int x = 0; x < cnt; x++) matrices[x] = MUt.Read(br);
        }

        class MUt {
            internal static Matrix Read(BinaryReader br) {
                Matrix M = new Matrix();
                M.M11 = br.ReadSingle(); M.M12 = br.ReadSingle(); M.M13 = br.ReadSingle(); M.M14 = br.ReadSingle();
                M.M21 = br.ReadSingle(); M.M22 = br.ReadSingle(); M.M23 = br.ReadSingle(); M.M24 = br.ReadSingle();
                M.M31 = br.ReadSingle(); M.M32 = br.ReadSingle(); M.M33 = br.ReadSingle(); M.M34 = br.ReadSingle();
                M.M41 = br.ReadSingle(); M.M42 = br.ReadSingle(); M.M43 = br.ReadSingle(); M.M44 = br.ReadSingle();
                return M;
            }
        }

        internal Matrix GetMatrix(int frame, int x) {
            return matrices[x + frame * cntMatrices];
        }
    }
}
