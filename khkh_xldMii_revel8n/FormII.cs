//#define UseMultiSamp
//#define UsePerspective

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using khkh_xldMii.Mx;
using vconv122;
using khkh_xldMii.Mo;
using System.Diagnostics;
using hex04BinTrack;
using khkh_xldMii.Mc;
using ef1Declib;
using khkh_xldMii.V;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Collections;
using SlimDX.Direct3D9;
using SlimDX;

namespace khkh_xldMii {
    public partial class FormII : Form, ILoadf, IVwer {
        public FormII() {
            _Sora[1].parent = _Sora[0];
            _Sora[1].iMa = 0xB2;
            _Sora[2].parent = _Sora[0];
            _Sora[2].iMa = 0x56;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        class MotInf {
            public Mt1 mt1;
            public float maxtick;
            public float mintick;
            public bool isRaw = false;
        }

        void loadMset(string fmset, int ty) {
            Mesh M = _Sora[ty];
            M.DisposeMset();

            if (File.Exists(fmset)) {
                using (FileStream fs = File.OpenRead(fmset)) {
                    M.mset = new Msetfst(fs, Path.GetFileName(fmset));

                    //Msetblk MB = new Msetblk(new MemoryStream(mset.al1[0].bin, false));
                    //Console.WriteLine();
                }

                if (ty == 0) {
                    listView1.Items.Clear();
                    foreach (Mt1 mt1 in M.mset.al1) {
                        ListViewItem lvi = listView1.Items.Add(mt1.id);
                        MotInf mi = new MotInf();
                        mi.mt1 = mt1;
                        if (mt1.isRaw) {
                            MsetRawblk blk = new MsetRawblk(new MemoryStream(mt1.bin, false));
                            mi.maxtick = blk.cntFrames;
                            mi.mintick = 0;
                        }
                        else {
                            Msetblk blk = new Msetblk(new MemoryStream(mt1.bin, false));
                            mi.maxtick = (blk.to.al11.Length != 0) ? blk.to.al11[blk.to.al11.Length - 1] : 0;
                            mi.mintick = (blk.to.al11.Length != 0) ? blk.to.al11[0] : 0;
                        }
                        lvi.Tag = mi;
                    }
                    listView1.Sorting = SortOrder.Ascending;
                    listView1.Sort();

                    button2.Enabled = (listView1.Items.Count > 0);
                }
                M.binMset = File.ReadAllBytes(fmset);
            }
            M.ol = null;
        }

        class PatTexSel {
            public byte texi, pati;

            public PatTexSel(byte texi, byte pati) {
                this.texi = texi;
                this.pati = pati;
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
            public Mlink ol = null;
            public PatTexSel[] pts = new PatTexSel[0];

            public Matrix[] Ma = null; // for keyblade
            public Mesh parent = null; // for keyblade
            public int iMa = -1; // for keyblade

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
                ol = null;
            }

            public void DisposeMdlx() {
                mdlx = null;
                timc = null;
                timf = null;
                foreach (Texture t in altex)
                    t.Dispose();
                altex.Clear();
                foreach (Texture t in altex1)
                    t.Dispose();
                altex1.Clear();
                foreach (Texture t in altex2)
                    t.Dispose();
                altex2.Clear();
                albody1.Clear();
                binMdlx = null;
                ctb.Close();
                ol = null;
                Ma = null;
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
                            M.timc = TIMc.Load(new MemoryStream(ent.bin, false));
                            M.timf = (M.timc.Length >= 1) ? M.timc[0] : null;
                            break;
                        case 4:
                            M.mdlx = new Mdlxfst(new MemoryStream(ent.bin, false));
                            break;
                    }
                }
            }
            M.binMdlx = File.ReadAllBytes(fmdlx);
            M.ol = null;
            reloadTex(ty);

            calcbody(M.ctb, M, null, 0, UpdateFlags.Base);
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
            public uint[] altri3;

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
                alsepa = null;
                altri3 = null;
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
            //foreach (Mesh M in _Sora) M.ctb.Close();

            foreach (ListViewItem lvi in listView1.SelectedItems) {
                if (lvi.Tag != null) {
                    calcbody(_Sora[0].ctb, _Sora[0], ((MotInf)lvi.Tag).mt1, tick, UpdateFlags.Animate);

                    if (_Sora[1].Present) {
                        Mt1 weap = UtwexMotionSel.Sel(((MotInf)lvi.Tag).mt1.k1, _Sora[1].mset.al1);
                        if (weap != null && _Sora[1].iMa != -1) {
                            if (_Sora[1].Present) calcbody(_Sora[1].ctb, _Sora[1], weap, tick, UpdateFlags.Animate);
                        }
                    }
                    if (_Sora[2].Present) {
                        Mt1 weap = UtwexMotionSel.Sel(((MotInf)lvi.Tag).mt1.k1, _Sora[2].mset.al1);
                        if (weap != null && _Sora[2].iMa != -1) {
                            if (_Sora[2].Present) calcbody(_Sora[2].ctb, _Sora[2], weap, tick, UpdateFlags.Animate);
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
                MotInf mi = (MotInf)lvi.Tag;
                M.pts = SelTexfacUt.Sel(M.timf.alp, tick, mi.mt1.fm);
                break;
            }
        }

        class UtwexMotionSel {
            public static Mt1 Sel(int k1, List<Mt1> al1) {
                foreach (Mt1 o in al1) {
                    if (o.k1 == k1)
                        return o;
                }
                return null;
            }
        }

        float tick = 0;

        [Flags]
        private enum UpdateFlags : uint
        {
            None        = 0x00,
            Body        = 0x01,
            Transforms  = 0x02,
            Motion      = 0x04,
            Indices     = 0x08,
            Vertices    = 0x10,

            Buffers     = Indices | Vertices,
            Base = Body | Transforms | Motion | Buffers,
            Animate = Motion | Vertices,
        }

        void calcbody(CaseTris ct, Mesh M, Mt1 mt1, float _tick, UpdateFlags flags)
        {
            Mdlxfst mdlx = M.mdlx;
            Msetfst mset = M.mset;
            List<Body1> albody1 = M.albody1;
            Mlink ol = M.ol;

            if ((flags & UpdateFlags.Body) != UpdateFlags.None)
            {
                //ct.Close();
                albody1.Clear();
            }

            if (mdlx != null)
            {
                T31 t31 = mdlx.alt31[0];

                Matrix[] Ma = t31.Ma;
                Matrix[] Minv = t31.Minv;

                if (mt1 != null &&
                    ((flags & UpdateFlags.Motion) != UpdateFlags.None))
                {
                    if (mt1.isRaw)
                    {
                        MsetRawblk blk = new MsetRawblk(new MemoryStream(mt1.bin, false));
                        int t0 = Math.Max(0, Math.Min(blk.cntFrames - 1, (int)Math.Floor(_tick)));
                        int t1 = Math.Max(0, Math.Min(blk.cntFrames - 1, (int)Math.Ceiling(_tick)));
                        if (t0 == t1) {
                            MsetRM rm = blk.alrm[t0];
                            Ma = M.Ma = rm.al.ToArray();
                        }
                        else {
                            MsetRM rm0 = blk.alrm[t0]; float f1 = _tick % 1.0f;
                            MsetRM rm1 = blk.alrm[t1]; float f0 = 1.0f - f1;
                            Ma = M.Ma = new Matrix[blk.cntJoints];
                            for (int t = 0; t < Ma.Length; t++) {
                                Matrix M1 = new Matrix();
                                M1.M11 = rm0.al[t].M11 * f0 + rm1.al[t].M11 * f1;
                                M1.M21 = rm0.al[t].M21 * f0 + rm1.al[t].M21 * f1;
                                M1.M31 = rm0.al[t].M31 * f0 + rm1.al[t].M31 * f1;
                                M1.M41 = rm0.al[t].M41 * f0 + rm1.al[t].M41 * f1;
                                M1.M12 = rm0.al[t].M12 * f0 + rm1.al[t].M12 * f1;
                                M1.M22 = rm0.al[t].M22 * f0 + rm1.al[t].M22 * f1;
                                M1.M32 = rm0.al[t].M32 * f0 + rm1.al[t].M32 * f1;
                                M1.M42 = rm0.al[t].M42 * f0 + rm1.al[t].M42 * f1;
                                M1.M13 = rm0.al[t].M13 * f0 + rm1.al[t].M13 * f1;
                                M1.M23 = rm0.al[t].M23 * f0 + rm1.al[t].M23 * f1;
                                M1.M33 = rm0.al[t].M33 * f0 + rm1.al[t].M33 * f1;
                                M1.M43 = rm0.al[t].M43 * f0 + rm1.al[t].M43 * f1;
                                M1.M14 = rm0.al[t].M14 * f0 + rm1.al[t].M14 * f1;
                                M1.M24 = rm0.al[t].M24 * f0 + rm1.al[t].M24 * f1;
                                M1.M34 = rm0.al[t].M34 * f0 + rm1.al[t].M34 * f1;
                                M1.M44 = rm0.al[t].M44 * f0 + rm1.al[t].M44 * f1;
                                Ma[t] = M1;
                            }
                        }
                    }
                    else
                    {
                        Msetblk blk = new Msetblk(new MemoryStream(mt1.bin, false));
                        MemoryStream os = new MemoryStream();
                        if (ol == null)
                            ol = M.ol = new Mlink();
                        ol.Permit(new MemoryStream(M.binMdlx, false), blk.cntb1, new MemoryStream(M.binMset, false), blk.cntb2, mt1.off, _tick, os);

                        BinaryReader br = new BinaryReader(os);
                        os.Position = 0;
                        Ma = M.Ma = new Matrix[blk.cntb1];
                        for (int t = 0; t < blk.cntb1; t++) {
                            Matrix M1 = new Matrix();
                            M1.M11 = br.ReadSingle(); M1.M12 = br.ReadSingle(); M1.M13 = br.ReadSingle(); M1.M14 = br.ReadSingle();
                            M1.M21 = br.ReadSingle(); M1.M22 = br.ReadSingle(); M1.M23 = br.ReadSingle(); M1.M24 = br.ReadSingle();
                            M1.M31 = br.ReadSingle(); M1.M32 = br.ReadSingle(); M1.M33 = br.ReadSingle(); M1.M34 = br.ReadSingle();
                            M1.M41 = br.ReadSingle(); M1.M42 = br.ReadSingle(); M1.M43 = br.ReadSingle(); M1.M44 = br.ReadSingle();
                            Ma[t] = M1;
                        }
                    }
                }

                if ((flags & UpdateFlags.Transforms) != UpdateFlags.None)
                {
                    int cnt = Ma.Length;
                    int mn = 0;
                    for (mn = 0; mn < cnt; ++mn)
                    {
                        Minv[mn] = Matrix.Invert(Ma[mn]);
                    }
                }

                Matrix Mv = Matrix.Identity;
                if (M.parent != null && M.iMa != -1)
                {
                    Mv = M.parent.Ma[M.iMa];
                }

                if ((flags & UpdateFlags.Body) != UpdateFlags.None)
                {
                    foreach (T13vif t13 in t31.al13)
                    {
                        int tops = 0x220;
                        int top2 = 0;
                        VU1Mem vu1mem = new VU1Mem();
                        new ParseVIF1(vu1mem).Parse(new MemoryStream(t13.bin, false), tops);
                        Body1 body1 = SimaVU1.Sima(vu1mem, Ma, tops, top2, t13.texi, t13.alaxi, Mv);
                        albody1.Add(body1);
                    }
                }

                if ((flags & UpdateFlags.Indices) != UpdateFlags.None)
                {
                    //if (ct.alsepa == null || ct.altri3 == null)
                    if (true)
                    {
                        List<uint> altri3 = new List<uint>();
                        List<Sepa> alsepa = new List<Sepa>();
                        if (true)
                        {
                            int svi = 0;
                            int bi = 0;
                            uint[] alvi = new uint[4];
                            int ai = 0;
                            int il = (int)_tick;
                            int[] ord = new int[] { 1, 2, 3 };
                            foreach (Body1 b1 in albody1)
                            {
                                int cntPrim = 0;
                                for (int x = 0; x < b1.alvi.Length; x++)
                                {
                                    alvi[ai] = (uint)((b1.alvi[x]) | (bi << 12) | (x << 24));
                                    ai = (ai + 1) & 3;
                                    if (b1.alfl[x] == 0x20)
                                    {
                                        altri3.Add(alvi[(ai - ord[(0 + (il * 103)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(1 + (il * 103)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(2 + (il * 103)) % 3]) & 3]);
                                        cntPrim++;
                                    }
                                    else if (b1.alfl[x] == 0x30)
                                    {
                                        altri3.Add(alvi[(ai - ord[(0 + (il << 1)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(2 + (il << 1)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(1 + (il << 1)) % 3]) & 3]);
                                        cntPrim++;
                                    }
                                    else if (b1.alfl[x] == 0x00) // double sided
                                    {
                                        altri3.Add(alvi[(ai - ord[(0 + (il * 103)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(1 + (il * 103)) % 3]) & 3]);
                                        altri3.Add(alvi[(ai - ord[(2 + (il * 103)) % 3]) & 3]);
                                        cntPrim++;
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
                        ct.altri3 = altri3.ToArray();
                    }
                }

                if ((flags & UpdateFlags.Vertices) != UpdateFlags.None)
                {
                    ct.cntVert = (ct.altri3 != null) ? (ct.altri3.Length) : (0);
                    ct.cntPrimitives = 0;

                    if (ct.cntVert != 0)
                    {
                        int vertexCount = ct.altri3.Length;
                        PTex3[] va = new PTex3[vertexCount];
                        for (int x = 0; x < vertexCount; ++x)
                        {
                            uint xx = ct.altri3[x];
                            uint vi = xx & 4095;
                            uint bi = (xx >> 12) & 4095;
                            uint xi = (xx >> 24) & 4095;
                            Body1 b1 = albody1[(int)bi];

                            Matrix tm = new Matrix();
                            Vector3 v3 = b1.alvert[vi];

                            int weightCount = b1.albi[vi].Length;
                            int wn = 0;
                            for (wn = 0; wn < weightCount; ++wn)
                            {
                                tm += (Minv[b1.albi[vi][wn]] * Ma[b1.albi[vi][wn]]) * b1.albw[vi][wn];
                            }
                            v3 = Vector3.TransformCoordinate(v3, tm);

                            va[x] = new PTex3(v3, new Vector2(b1.aluv[xi].X, b1.aluv[xi].Y));
                        }
                        if (ct.vb == null)
                        {
                            ct.vb = new VertexBuffer(
                                device,
                                PTex3.Size * ct.cntVert,
                                0,
                                ct.vf = PTex3.Format,
                                Pool.Managed
                                );
                        }
                        DataStream gs = ct.vb.Lock(0, 0, 0);
                        try
                        {
                            gs.WriteRange(va);
                            gs.Position = 0;
                        }
                        finally
                        {
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
            device = new Device(d3d, 0, DeviceType.Hardware, panel1.Handle, CreateFlags.HardwareVertexProcessing, PP);
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
            public static PatTexSel[] Sel(List<Patc> alp, float tick, FacMod fm) {
                PatTexSel[] sel = new PatTexSel[alp.Count];
                foreach (Fac1 f1 in fm.alf1) {
                    if (f1.v2 != -1 && f1.v0 <= tick && tick < f1.v2) {
                        for (int x = 0; x < alp.Count; x++) {
                            int curt = (int)(tick - f1.v0) / 8;
                            foreach (Texfac tf in alp[x].altf) {
                                if (tf.i0 == f1.v6) {
                                    if (curt <= 0) {
                                        if (sel[x] == null) {
                                            sel[x] = new PatTexSel((byte)alp[x].texi, (byte)tf.v6);
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
                            foreach (STim st in M.timf.alt) {
                                M.altex.Add(TUt.FromBitmap(device, st.pic));
                                //st.pic.Save(@"H:\Proj\khkh_xldM\MEMO\pattex\t" + ty + "." + t + ".png", ImageFormat.Png); t++;
                            }
                            if (x == 0) {
                                for (int p = 0; p < 2; p++) {
                                    for (int pi = 0; ; pi++) {
                                        Bitmap pic = M.timf.GetPattex(p, pi);
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
                                if (isLooksChange && M.pts.Length >= 1 && M.pts[0] != null && sepa.t == M.pts[0].texi) alt.Add(M.altex1[M.pts[0].pati]);
                                if (isLooksChange && M.pts.Length >= 2 && M.pts[1] != null && sepa.t == M.pts[1].texi) alt.Add(M.altex2[M.pts[1].pati]);
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
                    numericUpDownAutoNext.Value = (decimal)((MotInf)lvi.Tag).maxtick + numericUpDownStep.Value;
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
                    Mt1 mt1 = ((MotInf)lvi.Tag).mt1;

                    Msetblk blk = new Msetblk(new MemoryStream(mt1.bin, false));
                    T31 t31 = _Sora[0].mdlx.alt31[0];

                    Mlink ol = _Sora[0].ol = new Mlink();
                    MemoryStream fsMdlx = new MemoryStream(_Sora[0].binMdlx, false);
                    MemoryStream fsMset = new MemoryStream(_Sora[0].binMset, false);

                    for (float t = 0; t <= 300; t++) {
                        float[] Svec, Rvec, Tvec;
                        ol.Permit_DEB(fsMdlx, blk.cntb1, fsMset, blk.cntb2, mt1.off, t, out Svec, out Rvec, out Tvec);
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
                case 1: _Sora[1].iMa = joint; break;
                case 2: _Sora[2].iMa = joint; break;

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

        private enum ExportState : int
        {
            Initialization,
            Processing,
            Saving,
            Finished,
            Cancelling,
        }

        private class ExportStatus
        {
            public string animName;

            public int animCount;
            public int animIndex;

            public int frameCount;
            public int frameIndex;

            public int jointCount;
            public int jointIndex;
        }

        ExportProgress progressForm;

        private void ExportASET(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (sender as BackgroundWorker);
            ListViewItem[] lvc = (e.Argument as ListViewItem[]);

            ExportStatus progress = new ExportStatus();

            progress.animName = "";

            progress.animCount = 0;
            progress.animIndex = 0;

            progress.frameCount = 0;
            progress.frameIndex = 0;

            progress.jointCount = 0;
            progress.jointIndex = 0;

            worker.ReportProgress((int)ExportState.Initialization, progress);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            // get main mesh
            Mesh m = this._Sora[0];

            if (m == null || m.mdlx == null || m.mset == null || lvc.Length <= 0)
                return;

            T31 t = m.mdlx.alt31[0];

            // Create a new memory stream
            MemoryStream mat_data = new MemoryStream();
            BinaryWriter mat_writer = new BinaryWriter(mat_data);
            int max_ticks = 1;
            int bone_count = t.t21.alaxb.Count;
            int anim_count = lvc.Length;

            progress.animCount = anim_count;
            progress.jointCount = bone_count;

            long file_start = mat_writer.BaseStream.Position;

            mat_writer.Write(("ASET").ToCharArray(0, 4)); // bone count (4 bytes)
            mat_writer.Write((int)0); // padding (4 bytes)
            mat_writer.Write(bone_count); // bone count (4 bytes)
            mat_writer.Write(anim_count); // animation count (4 bytes)

            // get start of animation offset array
            long offsets_start = mat_writer.BaseStream.Position;

            // move past animation offset array
            mat_writer.BaseStream.Position += ((anim_count * 4) + (0x0F)) & (~0x0F);

            worker.ReportProgress((int)ExportState.Initialization, progress);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            int anim_num = 0;
            // Get the frame count (ticks)
            foreach (ListViewItem lvi in lvc)
            {
                string anim_name = lvi.Text;

                progress.animName = anim_name;
                progress.animIndex = anim_num;

                anim_name = anim_name.Replace('#', '_');

                // get motion info
                MotInf mi = ((MotInf)lvi.Tag);
                max_ticks = (int)mi.maxtick;

                progress.frameIndex = 0;
                progress.frameCount = max_ticks;

                worker.ReportProgress((int)ExportState.Processing, progress);

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // animation data position
                long anim_start = mat_writer.BaseStream.Position;

                // data offset array position
                mat_writer.BaseStream.Position = offsets_start + (anim_num * 4);

                // write animation data offset
                mat_writer.Write((int)anim_start); // bone count (4 bytes)

                // reset animation data position
                mat_writer.BaseStream.Position = anim_start;

                // write animation header
                mat_writer.Write((int)anim_num); // animation index (4 bytes)
                mat_writer.Write((int)max_ticks); // frame (tick) count (4 bytes)
                mat_writer.Write((int)0); // padding (4 bytes)
                mat_writer.Write((int)0); // padding (4 bytes)

                // write animation name
                mat_writer.Write(anim_name.ToCharArray(0, anim_name.Length));

                // move to matrix data start
                mat_writer.BaseStream.Position += (32 - anim_name.Length);

                // increment animation index
                ++anim_num;

                // output all the matrix transforms for each frame
                for (int i = 0; i < max_ticks; i++)
                {
                    progress.frameIndex = i;
                    progress.jointIndex = 0;

                    worker.ReportProgress((int)ExportState.Processing, progress);

                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // set current tick
                    //tick = i;
                    // this tell his viewer to calculate the animation for the frame
                    this.calcbody(m.ctb, m, mi.mt1, i, UpdateFlags.Motion);

                    // output each matrix for each bone (matrix4x4 * 4 bytes)

                    for (int bn = 0; bn < bone_count; ++bn)
                    {
                        progress.jointIndex = bn;

                        //worker.ReportProgress((int)ExportState.Processing, progress);

                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        Matrix mat = m.Ma[bn];
                        /* Matrix Format
                           [M11][M12][M13][M14]
                           [M21][M22][M23][M24]
                           [M31][M32][M33][M34]
                           [M41][M42][M43][M44]
                        */
                        mat_writer.Write((float)mat.M11); mat_writer.Write((float)mat.M12); mat_writer.Write((float)mat.M13);
                        mat_writer.Write((float)mat.M14);

                        mat_writer.Write((float)mat.M21); mat_writer.Write((float)mat.M22); mat_writer.Write((float)mat.M23);
                        mat_writer.Write((float)mat.M24);

                        mat_writer.Write((float)mat.M31); mat_writer.Write((float)mat.M32); mat_writer.Write((float)mat.M33);
                        mat_writer.Write((float)mat.M34);

                        mat_writer.Write((float)mat.M41); mat_writer.Write((float)mat.M42); mat_writer.Write((float)mat.M43);
                        mat_writer.Write((float)mat.M44);
                    }

                    worker.ReportProgress((int)ExportState.Processing, progress);
                }

                worker.ReportProgress((int)ExportState.Processing, progress);
            }

            worker.ReportProgress((int)ExportState.Processing, progress);

            // reset animation data position
            mat_writer.BaseStream.Position = 0x0C;
            mat_writer.Write((int)anim_num);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            // Generate the file name
            string filename = m.mset.motionID;
            // change file extension
            filename = filename.ToUpper().Replace("MSET", "ASET");
            // open file
            FileStream outfile = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);

            worker.ReportProgress((int)ExportState.Saving, progress);

            // Output all the bytes to a file
            byte[] b_data = mat_data.ToArray();

            outfile.Write(b_data, 0, b_data.Length);

            outfile.Close();

            worker.ReportProgress((int)ExportState.Finished, progress);

            //MessageBox.Show("Animation transforms dumped.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ExportStatus status = (e.UserState as ExportStatus);

            // update animation status
            progressForm.animProgressBar.Minimum = 0;
            progressForm.animProgressBar.Maximum = status.animCount;
            progressForm.animProgressBar.Value = status.animIndex;
            // update frame status
            progressForm.frameProgressBar.Minimum = 0;
            progressForm.frameProgressBar.Maximum = status.frameCount;
            progressForm.frameProgressBar.Value = status.frameIndex;

            progressForm.animProgressLabel.Text = string.Format("[{0,5} /{1,5} ] - {2}", status.animIndex, status.animCount, status.animName);
            progressForm.frameProgressLabel.Text = string.Format("[{0,5} /{1,5} ]", status.frameIndex, status.frameCount);

            switch ((ExportState)e.ProgressPercentage)
            {
            case ExportState.Initialization:
                {
                    progressForm.statusLabel.Text = "Initializing...";
                }
                break;
            case ExportState.Processing:
                {
                    progressForm.statusLabel.Text = "Processing...";
                }
                break;
            case ExportState.Saving:
                {
                    progressForm.statusLabel.Text = "Saving...";
                }
                break;
            case ExportState.Finished:
                {
                    progressForm.statusLabel.Text = "Finished!";
                }
                break;
            case ExportState.Cancelling:
                {
                    progressForm.statusLabel.Text = "Export Aborted.";
                }
                break;
            }
        }

        private void worker_ProgressCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressForm.Close();
            if (!e.Cancelled)
                MessageBox.Show("Animation transforms dumped.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void worker_CancelProgress(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressForm = new ExportProgress();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += ExportASET;
            backgroundWorker1.ProgressChanged += worker_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += worker_ProgressCompleted;

            progressForm.cancelExportButton.Click += worker_CancelProgress;

            ListViewItem[] items = null;

            if (listView1.CheckedItems.Count > 0)
            {
                items = new ListViewItem[listView1.CheckedItems.Count];
                listView1.CheckedItems.CopyTo(items, 0);
            }
            else if (listView1.Items.Count > 0)
            {
                items = new ListViewItem[listView1.Items.Count];
                listView1.Items.CopyTo(items, 0);
            }

            if (items != null)
            {
                backgroundWorker1.RunWorkerAsync(items);

                progressForm.ShowDialog(this);
            }

            backgroundWorker1.CancelAsync();
            backgroundWorker1.DoWork -= ExportASET;
            backgroundWorker1.ProgressChanged -= worker_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted -= worker_ProgressCompleted;

            progressForm.Close();
            progressForm.Dispose();
            progressForm = null;
        }

        private void FormII_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Mesh M in _Sora)
            {
                if (M != null)
                {
                    M.Dispose();
                }
            }

            device.Dispose();
            d3d.Dispose();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                // check or un-check all items
                bool check = (listView1.CheckedItems.Count != listView1.Items.Count);
                foreach (ListViewItem lvi in listView1.Items)
                {
                    lvi.Checked = check;
                }
            }
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
