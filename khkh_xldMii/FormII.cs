//#define UseMultiSamp
//#define UsePerspective

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using hex04BinTrack;
using ef1Declib;
using khkh_xldMii.V;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using SlimDX.Direct3D9;
using SlimDX;
using khkh_xldMii.Models.Mset;
using khkh_xldMii.Utils.Mset;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Models.Mdlx;
using khkh_xldMii.Models;

namespace khkh_xldMii {
    public partial class FormII : Form, ILoadf, IVwer {
        public FormII() {
            _Sora[1].parent = _Sora[0];
            _Sora[1].matrixIndexToAttach = 0xB2;
            _Sora[2].parent = _Sora[0];
            _Sora[2].matrixIndexToAttach = 0x56;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            listView1.Items.Add("(Drop your .mdlx file to window)");

            string[] args = Environment.GetCommandLineArgs();
            for (int x = 1; x < args.Length; x++) {
                string fmdlx = args[x];
                if (File.Exists(fmdlx) && Path.GetExtension(fmdlx).ToLower().Equals(".mdlx")) {
                    loadMdlx(fmdlx, 0);
                    loadMset(MatchUt.findMset(fmdlx), 0);
                }
            }

            radioButtonAny_CheckedChanged(null, null);

            //loadMdlx(@"V:\KH2.yaz0r\dump_kh2\obj\W_EX010_U0.mdlx", 1);
            //loadMset(@"H:\EMU\pcsx2-0.9.4\expa\obj_W_EX010_BTLF_R.mset", 1);
            //loadMdlx(@"V:\KH2.yaz0r\dump_kh2\obj\W_EX010_W0.mdlx", 2);
            //loadMset(@"H:\EMU\pcsx2-0.9.4\expa\obj_W_EX010_BTLF_L.mset", 2);
        }

        class MatchUt {
            public static string findMset(string fmdlx) {
                string s1 = fmdlx.Substring(0, fmdlx.Length - 5) + ".mset";
                if (File.Exists(s1)) return s1;
                string s2 = Regex.Replace(s1, "_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
                if (File.Exists(s2)) return s2;
                string s4 = Regex.Replace(s1, "_[a-z]+(_[a-z]+\\.)", "$1", RegexOptions.IgnoreCase);
                if (File.Exists(s4)) return s4;
                string s3 = Regex.Replace(s1, "_[a-z]+_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
                if (File.Exists(s3)) return s3;
                return s1;
            }
        }

        class MotionDesc {
            public OneMotion oneMotion;
            public float maxTick;
            public float minTick;
        }

        void loadMset(string msetPath, int ty) {
            Mesh M = _Sora[ty];
            M.DisposeMset();

            if (File.Exists(msetPath)) {
                using (FileStream fs = File.OpenRead(msetPath)) {
                    M.mset = new Msetfst(fs, Path.GetFileName(msetPath));

                    //Msetblk MB = new Msetblk(new MemoryStream(mset.al1[0].bin, false));
                    //Console.WriteLine();
                }

                if (ty == 0) {
                    listView1.Items.Clear();
                    foreach (OneMotion oneMotion in M.mset.motionList) {
                        ListViewItem lvi = listView1.Items.Add(oneMotion.label);
                        MotionDesc desc = new MotionDesc();
                        desc.oneMotion = oneMotion;
                        if (oneMotion.isRaw) {
                            AnbRawReader reader = new AnbRawReader(new MemoryStream(oneMotion.anbBin, false));
                            desc.maxTick = reader.cntFrames;
                            desc.minTick = 0;
                        }
                        else {
                            AnbReader reader = new AnbReader(new MemoryStream(oneMotion.anbBin, false));
                            desc.maxTick = (reader.model.t11List.Length != 0) ? reader.model.t11List[reader.model.t11List.Length - 1] : 0;
                            desc.minTick = (reader.model.t11List.Length != 0) ? reader.model.t11List[0] : 0;
                        }
                        lvi.Tag = desc;
                    }
                    listView1.Sorting = SortOrder.Ascending;
                    listView1.Sort();
                }
                M.binMset = File.ReadAllBytes(msetPath);
            }
            M.emuRunner = null;
        }

        class TexturePair {
            public byte texIndex;
            public byte faceIndex;

            public TexturePair(byte texIndex, byte faceIndex) {
                this.texIndex = texIndex;
                this.faceIndex = faceIndex;
            }
        }

        class Mesh : IDisposable {
            public Mdlxfst mdlx = null;
            public Texex2[] timc = null;
            public Texex2 timf = null;
            public Msetfst mset = null;
            public List<Texture> altex = new List<Texture>();
            public List<Texture> altex1 = new List<Texture>();
            public List<Texture> altex2 = new List<Texture>();
            public List<Body1> albody1 = new List<Body1>();
            public byte[] binMdlx;
            public byte[] binMset;
            public CaseTris ctb = new CaseTris();
            public Mlink emuRunner = null;
            public TexturePair[] texturePairList = new TexturePair[0];

            public Matrix[] matrixList = null; // for keyblade
            public Mesh parent = null; // for keyblade
            public int matrixIndexToAttach = -1; // for keyblade

            public bool Present { get { return mdlx != null && mset != null; } }

            #region IDisposable メンバ

            public void Dispose() {
                DisposeMdlx();
                DisposeMset();
            }

            #endregion

            public void DisposeMset() {
                mset = null;
                binMset = null;
                emuRunner = null;
            }

            public void DisposeMdlx() {
                mdlx = null;
                timc = null;
                timf = null;
                altex.Clear();
                altex1.Clear();
                altex2.Clear();
                albody1.Clear();
                binMdlx = null;
                ctb.Close();
                emuRunner = null;
                matrixList = null;
            }
        }

        Mesh[] _Sora = new Mesh[] { new Mesh(), new Mesh(), new Mesh(), };

        void loadMdlx(string fmdlx, int ty) {
            if (ty == 0) {
                listView1.Items.Clear();
                listView1.Items.Add("(Drop your .mdlx file to window)");
            }

            Mesh M = _Sora[ty];
            M.DisposeMdlx();

            ReadBar.Barent[] ents;
            using (FileStream fs = File.OpenRead(fmdlx)) {
                ents = ReadBar.Explode(fs);
                foreach (ReadBar.Barent ent in ents) {
                    switch (ent.k) {
                        case 7:
                            M.timc = TIMCollection.Load(new MemoryStream(ent.bin, false));
                            M.timf = (M.timc.Length >= 1) ? M.timc[0] : null;
                            break;
                        case 4:
                            M.mdlx = new Mdlxfst(new MemoryStream(ent.bin, false));
                            break;
                    }
                }
            }
            M.binMdlx = File.ReadAllBytes(fmdlx);
            M.emuRunner = null;
            reloadTex(ty);
        }

        private void FormII_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void FormII_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                foreach (string fmdlx in fs) {
                    if (Path.GetExtension(fmdlx).ToLower().Equals(".mdlx")) {
                        fmdlxDropped = fmdlx;
                        Timer t = new Timer();
                        t.Tick += new EventHandler(t_Tick);
                        t.Start();
                        break;
                    }
                }
            }
        }

        string fmdlxDropped = null;

        void t_Tick(object sender, EventArgs e) {
            ((Timer)sender).Dispose();
            string fmdlx = fmdlxDropped;

            loadMdlx(fmdlx, 0);
            loadMset(MatchUt.findMset(fmdlx), 0);
            recalc();
        }

        class CaseTris : IDisposable {
            public VertexBuffer vb;
            public VertexFormat vf;
            public int cntPrimitives, cntVert;
            public Sepa[] alsepa;

            #region IDisposable メンバ

            public void Dispose() {
                Close();
            }

            #endregion

            public void Close() {
                if (vb != null)
                    vb.Dispose();
                vb = null;
                vf = VertexFormat.None;
                cntPrimitives = 0;
                cntVert = 0;
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

        void recalc() {
            foreach (Mesh M in _Sora) M.ctb.Close();

            foreach (ListViewItem lvi in listView1.SelectedItems) {
                if (lvi.Tag != null) {
                    calcbody(_Sora[0].ctb, _Sora[0], ((MotionDesc)lvi.Tag).oneMotion);

                    if (_Sora[1].Present) {
                        OneMotion weap = UtwexMotionSel.Sel(((MotionDesc)lvi.Tag).oneMotion.indexInMset, _Sora[1].mset.motionList);
                        if (weap != null && _Sora[1].matrixIndexToAttach != -1) {
                            if (_Sora[1].Present) calcbody(_Sora[1].ctb, _Sora[1], weap);
                        }
                    }
                    if (_Sora[2].Present) {
                        OneMotion weap = UtwexMotionSel.Sel(((MotionDesc)lvi.Tag).oneMotion.indexInMset, _Sora[2].mset.motionList);
                        if (weap != null && _Sora[2].matrixIndexToAttach != -1) {
                            if (_Sora[2].Present) calcbody(_Sora[2].ctb, _Sora[2], weap);
                        }
                    }

                    calcPattex(_Sora[0], (float)numericUpDownTick.Value);
                }
                break;
            }
            panel1.Invalidate();
        }

        private void calcPattex(Mesh M, float tick) {
            foreach (ListViewItem lvi in listView1.SelectedItems) {
                MotionDesc mi = (MotionDesc)lvi.Tag;
                M.texturePairList = SelTexfacUt.Sel(M.timf.facePatchList, tick, mi.oneMotion.faceModSet);
                break;
            }
        }

        class UtwexMotionSel {
            public static OneMotion Sel(int k1, List<OneMotion> al1) {
                foreach (OneMotion o in al1) {
                    if (o.indexInMset == k1)
                        return o;
                }
                return null;
            }
        }

        float tick = 0;

        void calcbody(CaseTris ct, Mesh M, OneMotion mt1) {
            Mdlxfst mdlx = M.mdlx;
            Msetfst mset = M.mset;
            List<Body1> albody1 = M.albody1;
            Mlink emuRunner = M.emuRunner;

            ct.Close();
            albody1.Clear();

            if (mdlx != null && mset != null) {
                K2Model t31 = mdlx.alt31[0];

                Matrix[] Ma;
                if (mt1.isRaw) {
                    AnbRawReader blk = new AnbRawReader(new MemoryStream(mt1.anbBin, false));
                    int t0 = Math.Max(0, Math.Min(blk.cntFrames - 1, (int)Math.Floor(tick)));
                    int t1 = Math.Max(0, Math.Min(blk.cntFrames - 1, (int)Math.Ceiling(tick)));
                    if (t0 == t1) {
                        AnbRawAnimFrame rm = blk.frameSet[t0];
                        Ma = M.matrixList = rm.matrixList.ToArray();
                    }
                    else {
                        AnbRawAnimFrame frame1 = blk.frameSet[t0]; float f1 = tick % 1.0f;
                        AnbRawAnimFrame frame2 = blk.frameSet[t1]; float f0 = 1.0f - f1;
                        Ma = M.matrixList = new Matrix[blk.cntJoints];
                        for (int t = 0; t < Ma.Length; t++) {
                            Matrix M1 = new Matrix();
                            M1.M11 = frame1.matrixList[t].M11 * f0 + frame2.matrixList[t].M11 * f1;
                            M1.M21 = frame1.matrixList[t].M21 * f0 + frame2.matrixList[t].M21 * f1;
                            M1.M31 = frame1.matrixList[t].M31 * f0 + frame2.matrixList[t].M31 * f1;
                            M1.M41 = frame1.matrixList[t].M41 * f0 + frame2.matrixList[t].M41 * f1;
                            M1.M12 = frame1.matrixList[t].M12 * f0 + frame2.matrixList[t].M12 * f1;
                            M1.M22 = frame1.matrixList[t].M22 * f0 + frame2.matrixList[t].M22 * f1;
                            M1.M32 = frame1.matrixList[t].M32 * f0 + frame2.matrixList[t].M32 * f1;
                            M1.M42 = frame1.matrixList[t].M42 * f0 + frame2.matrixList[t].M42 * f1;
                            M1.M13 = frame1.matrixList[t].M13 * f0 + frame2.matrixList[t].M13 * f1;
                            M1.M23 = frame1.matrixList[t].M23 * f0 + frame2.matrixList[t].M23 * f1;
                            M1.M33 = frame1.matrixList[t].M33 * f0 + frame2.matrixList[t].M33 * f1;
                            M1.M43 = frame1.matrixList[t].M43 * f0 + frame2.matrixList[t].M43 * f1;
                            M1.M14 = frame1.matrixList[t].M14 * f0 + frame2.matrixList[t].M14 * f1;
                            M1.M24 = frame1.matrixList[t].M24 * f0 + frame2.matrixList[t].M24 * f1;
                            M1.M34 = frame1.matrixList[t].M34 * f0 + frame2.matrixList[t].M34 * f1;
                            M1.M44 = frame1.matrixList[t].M44 * f0 + frame2.matrixList[t].M44 * f1;
                            Ma[t] = M1;
                        }
                    }
                }
                else {
                    AnbReader blk = new AnbReader(new MemoryStream(mt1.anbBin, false));
                    MemoryStream os = new MemoryStream();
                    if (emuRunner == null) {
                        emuRunner = M.emuRunner = new Mlink();
                    }
                    emuRunner.Permit(new MemoryStream(M.binMdlx, false), blk.cntb1, new MemoryStream(M.binMset, false), blk.cntb2, mt1.anbOff, tick, os);

                    BinaryReader br = new BinaryReader(os);
                    os.Position = 0;
                    Ma = M.matrixList = new Matrix[blk.cntb1];
                    for (int t = 0; t < blk.cntb1; t++) {
                        Matrix M1 = new Matrix();
                        M1.M11 = br.ReadSingle(); M1.M12 = br.ReadSingle(); M1.M13 = br.ReadSingle(); M1.M14 = br.ReadSingle();
                        M1.M21 = br.ReadSingle(); M1.M22 = br.ReadSingle(); M1.M23 = br.ReadSingle(); M1.M24 = br.ReadSingle();
                        M1.M31 = br.ReadSingle(); M1.M32 = br.ReadSingle(); M1.M33 = br.ReadSingle(); M1.M34 = br.ReadSingle();
                        M1.M41 = br.ReadSingle(); M1.M42 = br.ReadSingle(); M1.M43 = br.ReadSingle(); M1.M44 = br.ReadSingle();
                        Ma[t] = M1;
                    }
                }

                Matrix Mv = Matrix.Identity;
                if (M.parent != null && M.matrixIndexToAttach != -1) {
                    Mv = M.parent.matrixList[M.matrixIndexToAttach];
                }

                foreach (K2Vif t13 in t31.vifList) {
                    int tops = 0x220;
                    int top2 = 0;
                    VU1Mem vu1mem = new VU1Mem();
                    new ParseVIF1(vu1mem).Parse(new MemoryStream(t13.bin, false), tops);
                    Body1 body1 = SimaVU1.Sima(vu1mem, Ma, tops, top2, t13.textureIndex, t13.marixIndexArray, Mv);
                    albody1.Add(body1);
                }

                if (true) {
                    List<uint> altri3 = new List<uint>();
                    List<Sepa> alsepa = new List<Sepa>();
                    if (true) {
                        int svi = 0;
                        int bi = 0;
                        uint[] alvi = new uint[4];
                        int ai = 0;
                        int il = (int)tick;
                        int[] ord = new int[] { 1, 2, 3 };
                        foreach (Body1 b1 in albody1) {
                            int cntPrim = 0;
                            for (int x = 0; x < b1.alvi.Length; x++) {
                                alvi[ai] = (uint)((b1.alvi[x]) | (bi << 12) | (x << 24));
                                ai = (ai + 1) & 3;
                                if (b1.alfl[x] == 0x20) {
                                    altri3.Add(alvi[(ai - ord[(0 + (il * 103)) % 3]) & 3]);
                                    altri3.Add(alvi[(ai - ord[(1 + (il * 103)) % 3]) & 3]);
                                    altri3.Add(alvi[(ai - ord[(2 + (il * 103)) % 3]) & 3]);
                                    cntPrim++;
                                }
                                else if (b1.alfl[x] == 0x30) {
                                    altri3.Add(alvi[(ai - ord[(0 + (il << 1)) % 3]) & 3]);
                                    altri3.Add(alvi[(ai - ord[(2 + (il << 1)) % 3]) & 3]);
                                    altri3.Add(alvi[(ai - ord[(1 + (il << 1)) % 3]) & 3]);
                                    cntPrim++;
                                }
                            }
                            alsepa.Add(new Sepa(svi, cntPrim, b1.t, bi));
                            svi += 3 * cntPrim;
                            bi++;
                        }
                    }

                    ct.alsepa = alsepa.ToArray();

                    ct.cntVert = altri3.Count;
                    ct.cntPrimitives = 0;

                    if (ct.cntVert != 0) {
                        ct.vb = new VertexBuffer(
                            device,
                            PTex3.Size * ct.cntVert,
                            0,
                            ct.vf = PTex3.Format,
                            Pool.Managed
                            );
                        DataStream gs = ct.vb.Lock(0, 0, 0);
                        try {
                            int cx = altri3.Count;
                            for (int x = 0; x < cx; x++) {
                                uint xx = altri3[x];
                                uint vi = xx & 4095;
                                uint bi = (xx >> 12) & 4095;
                                uint xi = (xx >> 24) & 4095;
                                Body1 b1 = albody1[(int)bi];
                                PTex3 v3 = new PTex3(b1.alvert[vi], new Vector2(b1.aluv[xi].X, b1.aluv[xi].Y));
                                gs.Write(v3);
                            }
                            gs.Position = 0;
                        }
                        finally {
                            ct.vb.Unlock();
                        }
                    }
                }
            }
        }

        Device device = null;
        Direct3D d3d = new Direct3D();

        PresentParameters PP {
            get {
                PresentParameters pp = new PresentParameters();
                pp.Windowed = true;
                pp.SwapEffect = SwapEffect.Discard;
                pp.EnableAutoDepthStencil = true;
                pp.AutoDepthStencilFormat = Format.D24X8;
                pp.BackBufferWidth = panel1.ClientSize.Width;
                pp.BackBufferHeight = panel1.ClientSize.Height;
                return pp;
            }
        }

        private void panel1_Load(object sender, EventArgs e) {
            device = new Device(d3d, 0, DeviceType.Hardware, panel1.Handle, CreateFlags.SoftwareVertexProcessing, PP);
            devReset();

            reshape();

            panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e) {
            fzval = Math.Max(1.0f, Math.Min(100.0f, fzval + (e.Delta / 120)));
            doReshape();
        }

        private void doReshape() {
            reshape();
            panel1.Invalidate();
        }

#if false
        float fzval = 1;
        Vector3 offset = Vector3.Empty;
        Quaternion quat = Quaternion.Identity;
#else
        float fzval = 5f;
        Vector3 offset = new Vector3(-4f, -66f, 0f);
        Quaternion quat = new Quaternion(0f, 0f, 0f, 1f);
#endif

        private void reshape() {
            int cxw = panel1.Width;
            int cyw = panel1.Height;
            float fx = (cxw > cyw) ? ((float)cxw / cyw) : 1.0f;
            float fy = (cxw < cyw) ? ((float)cyw / cxw) : 1.0f;

            float fact = (float)(fzval * 0.5f * 100);

            if (checkBoxPerspective.Checked) {
                float fnear = Convert.ToSingle(numnear.Value);
                float ffar = Convert.ToSingle(numfar.Value);
                float fox = Convert.ToSingle(numx.Value);
                float foy = Convert.ToSingle(numy.Value);
                float foz = Convert.ToSingle(numz.Value);
                float ffov = Convert.ToSingle(numfov.Value) / 180.0f * 3.14159f;

                device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovRH(
                    ffov, (cxw != 0) ? ((float)cxw / cyw) : 1, fnear, ffar
                    ));
                Matrix mv = Matrix.RotationQuaternion(quat);
                mv.M41 += offset.X + fox;
                mv.M42 += offset.Y + foy;
                mv.M43 += offset.Z + foz;
                device.SetTransform(TransformState.View, mv);
            }
            else {
                device.SetTransform(TransformState.Projection, Matrix.OrthoLH(
                    fx * fact, fy * fact, fact * 10, fact * -10
                    ));
                Matrix mv = Matrix.RotationQuaternion(quat);
                mv.M41 += offset.X;
                mv.M42 += offset.Y;
                mv.M43 += offset.Z;
                device.SetTransform(TransformState.View, mv);
            }
        }

        private void devReset() {
            device.SetRenderState(RenderState.Lighting, false);
            device.SetRenderState(RenderState.ZEnable, true);

            device.SetRenderState(RenderState.AlphaBlendEnable, true);
            device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);

            //device.SetRenderState(RenderState.CullMode, Cull.Clockwise);

            reloadTex(-1);
        }

        class SelTexfacUt {
            public static TexturePair[] Sel(List<FacePatch> alp, float tick, FaceModSet fm) {
                TexturePair[] sel = new TexturePair[alp.Count];
                foreach (FaceMod f1 in fm.list) {
                    if (f1.v2 != -1 && f1.v0 <= tick && tick < f1.v2) {
                        for (int x = 0; x < alp.Count; x++) {
                            int curt = (int)(tick - f1.v0) / 8;
                            foreach (FaceTexture tf in alp[x].faceTextureList) {
                                if (tf.i0 == f1.v6) {
                                    if (curt <= 0) {
                                        if (sel[x] == null) {
                                            sel[x] = new TexturePair((byte)alp[x].textureIndex, (byte)tf.v6);
                                            break;
                                        }
                                    }
                                    curt -= tf.v2;
                                }
                            }
                        }
                    }
                }
                return sel;
            }
        }

        class TUt {
            public static Texture FromBitmap(Device device, Bitmap p) {
                MemoryStream os = new MemoryStream();
                p.Save(os, ImageFormat.Png);
                os.Position = 0;
                return Texture.FromStream(device, os);
            }
        }

        void reloadTex(int ty) {
            if (device != null) {
                int x = 0;
                foreach (Mesh M in _Sora) {
                    if (x == ty || ty == -1) {
                        M.altex.Clear();
                        M.altex1.Clear();
                        M.altex2.Clear();
                        if (M.timf != null) {
                            //int t = 0;
                            foreach (TIMBitmap st in M.timf.bitmapList) {
                                M.altex.Add(TUt.FromBitmap(device, st.bitmap));
                                //st.pic.Save(@"H:\Proj\khkh_xldM\MEMO\pattex\t" + ty + "." + t + ".png", ImageFormat.Png); t++;
                            }
                            if (x == 0) {
                                for (int p = 0; p < 2; p++) {
                                    for (int pi = 0; ; pi++) {
                                        Bitmap pic = M.timf.GetPatchBitmap(p, pi);
                                        if (pic == null)
                                            break;
                                        //pic.Save(@"H:\Proj\khkh_xldM\MEMO\pattex\p" + p + "." + pi + ".png", ImageFormat.Png);
                                        switch (p) {
                                            case 0: M.altex1.Add(TUt.FromBitmap(device, pic)); break;
                                            case 1: M.altex2.Add(TUt.FromBitmap(device, pic)); break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    x++;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if (device == null || device.TestCooperativeLevel().IsFailure) return;

            bool isWire = false;// radioButtonWire.Checked;
            bool isTexsk = true;// radioButtonTex.Checked;
            bool isLooksChange = checkBoxLooks.Checked;

            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, panel1.BackColor, 1.0f, 0);
            device.BeginScene();
            device.SetRenderState(RenderState.FillMode, isWire ? FillMode.Wireframe : FillMode.Solid);
            foreach (Mesh M in _Sora) {
                CaseTris ctb = M.ctb;
                if (ctb != null && ctb.vb != null) {
                    device.SetStreamSource(0, ctb.vb, 0, PTex3.Size);
                    device.VertexFormat = ctb.vf;
                    device.Indices = null;
                    foreach (Sepa sepa in ctb.alsepa) {
                        if (true) {
                            device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.Disable);
                            device.SetTextureStageState(2, TextureStage.ColorOperation, TextureOperation.Disable);
                            device.SetTexture(0, null);

                            List<BaseTexture> alt = new List<BaseTexture>();

                            if (isTexsk) {
                                if (sepa.t < M.altex.Count) alt.Add(M.altex[sepa.t]);
                                if (isLooksChange && M.texturePairList.Length >= 1 && M.texturePairList[0] != null && sepa.t == M.texturePairList[0].texIndex) alt.Add(M.altex1[M.texturePairList[0].faceIndex]);
                                if (isLooksChange && M.texturePairList.Length >= 2 && M.texturePairList[1] != null && sepa.t == M.texturePairList[1].texIndex) alt.Add(M.altex2[M.texturePairList[1].faceIndex]);
                                alt.Remove(null);

                                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                                device.SetTexture(0, (alt.Count < 1) ? null : alt[0]);

                                if (alt.Count >= 2) {
                                    device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.BlendTextureAlpha);
                                    device.SetTextureStageState(1, TextureStage.ColorArg1, TextureArgument.Texture);
                                    device.SetTextureStageState(1, TextureStage.ColorArg2, TextureArgument.Current);
                                    device.SetTexture(1, alt[1]);

                                    if (alt.Count >= 3) {
                                        device.SetTextureStageState(2, TextureStage.ColorOperation, TextureOperation.BlendTextureAlpha);
                                        device.SetTextureStageState(2, TextureStage.ColorArg1, TextureArgument.Texture);
                                        device.SetTextureStageState(2, TextureStage.ColorArg2, TextureArgument.Current);
                                        device.SetTexture(2, alt[2]);
                                    }
                                }
                            }

                            device.DrawPrimitives(PrimitiveType.TriangleList, sepa.svi, sepa.cnt);
                        }
                    }
                }
            }
            device.EndScene();

            if (captureNow) {
                captureNow = false;
                using (Surface bb = device.GetBackBuffer(0, 0)) {
                    int frame = (int)numericUpDownFrame.Value;

                    if (checkBoxAsPNG.Checked) Surface.ToFile(bb, "_cap" + frame.ToString("00000") + ".png", ImageFileFormat.Png);
                    else Surface.ToFile(bb, "_cap" + frame.ToString("00000") + ".jpg", ImageFileFormat.Jpg);

                    numericUpDownFrame.Value += 1;
                }
            }

            device.Present();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (int sel in listView1.SelectedIndices) {
                numericUpDownTick.Value = 0;
                checkBoxAutoFill_CheckedChanged(null, null);
                recalc();
                break;
            }
        }

        Point curs = Point.Empty;

        bool hasLDn = false;

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                curs = e.Location;
                hasLDn = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left) && hasLDn) {
                hasLDn = false;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left) && hasLDn) {
                int dx = e.X - curs.X;
                int dy = e.Y - curs.Y;
                if (dx != 0 || dy != 0) {
                    curs = e.Location;

                    if (0 == (ModifierKeys & Keys.Shift)) {
                        quat *= Quaternion.RotationYawPitchRoll(dx / 100.0f, dy / 100.0f, 0);
                    }
                    else {
                        offset += (new Vector3(dx, -dy, 0));
                    }
                    doReshape();
                }
            }
        }

        private void checkBoxAnim_CheckedChanged(object sender, EventArgs e) {
            timerTick.Enabled = checkBoxAnim.Checked;
        }

        int enterLock = 0;
        bool captureNow = false;

        private void timerTick_Tick(object sender, EventArgs e) {
            try {
                if (enterLock++ == 0) {
                    try {
                        tick = (float)(numericUpDownTick.Value + numericUpDownStep.Value);

                        if (checkBoxAutoNext.Checked && (float)numericUpDownAutoNext.Value <= tick) {
                            if (checkBoxKeepCur.Checked) {
                                tick = 1;
                            }
                            else {
                                foreach (int s in listView1.SelectedIndices) {
                                    int sel = s + 1;
                                    if (sel < listView1.Items.Count) {
                                        listView1.Items[s].Selected = false;
                                        listView1.Items[sel].Selected = true;

                                        checkBoxAutoFill_CheckedChanged(null, null);
                                    }
                                    else {
                                        checkBoxAnim.Checked = false;
                                    }
                                    break;
                                }
                            }
                        }

                        if (checkBoxAutoRec.Checked) captureNow = true;

                        numericUpDownTick.Value = (decimal)tick;
                    }
                    catch (Exception) {
                        timerTick.Stop();
                        checkBoxAnim.Checked = false;
                        throw;
                    }
                }
            }
            finally {
                enterLock--;
            }
        }

        private void numericUpDownTick_ValueChanged(object sender, EventArgs e) {
            tick = (float)numericUpDownTick.Value;
            recalc();
        }

        private void checkBoxAutoFill_CheckedChanged(object sender, EventArgs e) {
            if (checkBoxAutoFill.Checked) {
                foreach (ListViewItem lvi in listView1.SelectedItems) {
                    numericUpDownAutoNext.Value = (decimal)((MotionDesc)lvi.Tag).maxTick + numericUpDownStep.Value;
                    break;
                }
            }
        }

        private void panel1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.End:
                    Debug.WriteLine("fzval = " + fzval + "f;");
                    Debug.WriteLine("offset = new Vector3(" + offset.X + "f, " + offset.Y + "f, " + offset.Z + "f);");
                    Debug.WriteLine("quat = new Quaternion(" + quat.X + "f, " + quat.Y + "f, " + quat.Z + "f, " + quat.W + "f);");
                    break;
                case Keys.Home:
                    fzval = 1;
                    offset = Vector3.Zero;
                    quat = Quaternion.Identity;
                    doReshape();
                    break;
                case Keys.PageDown:
                    fzval = 4f;
                    offset = new Vector3(28f, -92f, 0f);
                    quat = new Quaternion(-0.01664254f, -0.1349622f, -0.01049327f, 0.9906555f);
                    doReshape();
                    break;
                case Keys.PageUp:
                    fzval = 5f;
                    offset = new Vector3(-4f, -66f, 0f);
                    quat = new Quaternion(0f, 0f, 0f, 1f);
                    doReshape();
                    break;
            }
        }

        private void checkBoxKeys_CheckedChanged(object sender, EventArgs e) {
            labelHelpKeys.Visible = checkBoxKeys.Checked;
        }

        private void button1_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listView1.SelectedItems) {
                if (lvi.Tag != null) {
                    OneMotion mt1 = ((MotionDesc)lvi.Tag).oneMotion;

                    AnbReader blk = new AnbReader(new MemoryStream(mt1.anbBin, false));
                    K2Model t31 = _Sora[0].mdlx.alt31[0];

                    Mlink emuRunner = _Sora[0].emuRunner = new Mlink();
                    MemoryStream fsMdlx = new MemoryStream(_Sora[0].binMdlx, false);
                    MemoryStream fsMset = new MemoryStream(_Sora[0].binMset, false);

                    for (float t = 0; t <= 300; t++) {
                        float[] Svec, Rvec, Tvec;
                        emuRunner.Permit_DEB(fsMdlx, blk.cntb1, fsMset, blk.cntb2, mt1.anbOff, t, out Svec, out Rvec, out Tvec);
                        Debug.WriteLine(string.Format("{0},{1}", t, Rvec[169 * 4 + 0]));
                    }
                }
                break;
            }
        }

        private void label3_DoubleClick(object sender, EventArgs e) {
            button1.Show();
        }

        private void fl1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : e.Effect;
        }

        private void fl1_DragDrop(object sender, DragEventArgs e) {
            MessageBox.Show("ok");
        }

        private void buttonBC_Click(object sender, EventArgs e) {
            if (bcform == null || (bcform != null && bcform.IsDisposed)) {
                bcform = new BCForm(this);
            }
            bcform.Show();
            bcform.Activate();
        }

        BCForm bcform = null;

        #region ILoadf メンバ

        public void LoadOf(int x, string fp) {
            using (WC wc = new WC()) {
                switch (x) {
                    case 0: loadMdlx(fp, 0); break;
                    case 1: loadMset(fp, 0); break;

                    case 2: loadMdlx(fp, 1); break;
                    case 3: loadMset(fp, 1); break;

                    case 4: loadMdlx(fp, 2); break;
                    case 5: loadMset(fp, 2); break;

                    default: throw new NotSupportedException();
                }
            }
        }

        public void SetJointOf(int x, int joint) {
            switch (x) {
                case 1: _Sora[1].matrixIndexToAttach = joint; break;
                case 2: _Sora[2].matrixIndexToAttach = joint; break;

                default: throw new NotSupportedException();
            }
        }

        public void DoRecalc() {
            recalc();
            panel1.Invalidate();
        }

        #endregion

        #region IVwer メンバ

        public void BackToViewer() {
            Activate();
        }

        #endregion

        private void checkBoxLooks_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void radioButtonAny_CheckedChanged(object sender, EventArgs e) {
            if (false) { }
            else if (radioButton10fps.Checked) {
                timerTick.Interval = 1000 / 10;
            }
            else if (radioButton30fps.Checked) {
                timerTick.Interval = 1000 / 30;
            }
            else if (radioButton60fps.Checked) {
                timerTick.Interval = 1000 / 60;
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            if (device != null) {
                device.Reset(PP);
                panel1.Invalidate();
                reshape();
            }
        }

        private void numnear_ValueChanged(object sender, EventArgs e) {
            panel1.Invalidate();
            reshape();
        }

        private void checkBoxPerspective_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
            reshape();
        }

    }

    public interface ILoadf {
        void LoadOf(int x, string fp);
        void SetJointOf(int x, int joint);
        void DoRecalc();
    }
    public interface IVwer {
        void BackToViewer();
    }
}
