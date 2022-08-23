using System.Diagnostics;
using SlimDX.Direct3D9;
using SlimDX;
using hex04BinTrack;
using ef1Declib;
using khkh_xldMii;
using khkh_xldMii.V;
using khkh_xldMii.Mx;
using khkh_xldMii.Mo;
using khkh_xldMii.Models;
using mdlx2aset.Model;
using mdlx2aset.Motion;
using mdlx2aset.Utils;
using mdlx2aset.Interfaces;
using Msetfst = khkh_xldMii.Mo.Msetfst;
using Mesh = mdlx2aset.Model.Mesh;

namespace mdlx2aset {
    public class MdlxConvert : IControllerBindable, IDisposable {

        /// <summary>
        /// Currently loaded motions. By default, these are loaded from the MSET file corresponding to the loaded MDLX file. However, it is possible to bind the motions of another model. For reference, check the IControllerBindable interface.
        /// </summary>
        private List<MotionInformation> Motions { get; } = new();

        private IntPtr _handle;
        private bool disposed;
        private int _currentTick = 0;
        private int enterLock = 0;

        [Flags]
        private enum UpdateFlags : uint {
            None = 0x00,
            Body = 0x01,
            Transforms = 0x02,
            Motion = 0x04,
            Indices = 0x08,
            Vertices = 0x10,

            Buffers = Indices | Vertices,
            Base = Body | Transforms | Motion | Buffers,
            Animate = Motion | Vertices,
        }

        /// <summary>
        /// Mesh array of the currently loaded model
        /// </summary>
        private Mesh[] Model { get; } = new Mesh[] { new Mesh(), new Mesh(), new Mesh(), };

        //  SlimDX/DirectX device/objects
        //  TODO: Remove if possible
        private Device SlimDevice { get; }
        private Direct3D D3D { get; } = new();
        private static PresentParameters PP {
            get => new() {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = Format.D24X8,
                BackBufferWidth = 1920,
                BackBufferHeight = 1080
            };
        }

        private MdlxConvert(IntPtr handle) {
            Model[1].parent = Model[0];
            Model[1].iMa = 0xB2;
            Model[2].parent = Model[0];
            Model[2].iMa = 0x56;

            //  TODO: Remove if possible
            _handle = handle;
            SlimDevice = new Device(D3D, 0, DeviceType.Hardware, _handle, CreateFlags.HardwareVertexProcessing, PP);
            ResetDevice();
        }

        #region Private methods

        /// <summary>
        /// Load the specified MDLX file
        /// </summary>
        /// <param name="fmdlx">Path of the MDLX file to load</param>
        /// <param name="ty">Internal id/index to load the model at. 0 is the main/default model.</param>
        private void LoadMdlx(string fmdlx, int ty = 0) {
            if (ty == 0) {
                Motions.Clear();
            }

            Mesh M = Model[ty];
            M.DisposeMdlx();

            ReadBar.Barent[] ents;
            using (FileStream fs = File.OpenRead(fmdlx)) {
                ents = ReadBar.Explode(fs);

                foreach (ReadBar.Barent ent in ents) {
                    if (ent.bin is null)
                        continue;

                    switch (ent.k) {
                        case 7:
                            M.timc = TIMCollection.Load(new MemoryStream(ent.bin, false));
                            if (M.timc is not null)
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

            //  TODO: Remove if possible
            ReloadTextures(ty);
            CalcBody(M.ctb, M, null, 0, UpdateFlags.Base);
        }
        private void LoadMset(string fmset, int ty = 0) {
            Mesh M = Model[ty];
            M.DisposeMset();

            if (File.Exists(fmset)) {
                using (FileStream fs = File.OpenRead(fmset)) {
                    M.mset = new Msetfst(fs, Path.GetFileName(fmset));

                    //Msetblk MB = new Msetblk(new MemoryStream(mset.al1[0].bin, false));
                    //Console.WriteLine();
                }

                if (ty == 0) {
                    foreach (Mt1 mt1 in M.mset.al1) {
                        MotionInformation mi = new() {
                            mt1 = mt1
                        };

                        if (mt1.isRaw && mt1.bin is not null) {
                            var blk = new MsetRawblk(new MemoryStream(mt1.bin, false));
                            mi.maxtick = blk.cntFrames;
                            mi.mintick = 0;
                        }
                        else if (mt1.bin is not null) {
                            var blk = new Msetblk(new MemoryStream(mt1.bin, false));
                            mi.maxtick = (blk.to.al11 is not null && blk.to.al11.Length != 0) ? blk.to.al11[^1] : 0;
                            mi.mintick = (blk.to.al11 is not null && blk.to.al11.Length != 0) ? blk.to.al11[0] : 0;
                        }

                        Motions.Add(mi);
                    }
                }
                M.binMset = File.ReadAllBytes(fmset);
            }
            M.ol = null;
        }

        //  TODO: Remove if possible
        private void ReCalc() {
            //foreach (Mesh M in _Sora) M.ctb.Close();

            foreach (var motion in Motions) {
                if (motion.mt1 is null)
                    continue;

                //  TODO: Remove if possible
                CalcBody(Model[0].ctb, Model[0], motion.mt1, _currentTick, UpdateFlags.Animate);

                if (Model[1] is not null && Model[1].Present && Model[1].mset is not null) {
                    var weap = UtwexMotionSel.Sel(motion.mt1.k1, Model[1].mset.al1);

                    if (weap != null && Model[1].iMa != -1) {
                        if (Model[1].Present)
                            CalcBody(Model[1].ctb, Model[1], weap, _currentTick, UpdateFlags.Animate);
                    }

                }

                if (Model[2] is not null && Model[2].Present && Model[2].mset is not null) {
                    var weap = UtwexMotionSel.Sel(motion.mt1.k1, Model[2].mset.al1);

                    if (weap != null && Model[2].iMa != -1) {
                        if (Model[2].Present)
                            CalcBody(Model[2].ctb, Model[2], weap, _currentTick, UpdateFlags.Animate);
                    }
                }

                //  TODO: Remove if possible
                CalcPatchTextures(Model[0], _currentTick);

                break;
            }
        }
        private void CalcPatchTextures(Mesh M, float tick) {
            if (M.timf == null)
                return;

            foreach (var motion in Motions) {
                if (motion.mt1 is null || motion.mt1.fm is null)
                    continue;

                M.pts = SelTexfacUt.Sel(M.timf.facePatchList, tick, motion.mt1.fm);
                break;
            }
        }
        private void CalcBody(CaseTris ct, Mesh? M, Mt1? mt1, float _tick, UpdateFlags flags) {
            if (M is null || mt1 is null || M.mdlx is null || M.mset is null)
                return;

            var mdlx = M.mdlx;
            var albody1 = M.albody1;
            var ol = M.ol;

            if ((flags & UpdateFlags.Body) != UpdateFlags.None) {
                //ct.Close();
                albody1.Clear();
            }

            if (mdlx is not null) {

                var t31 = mdlx.alt31[0];
                var Ma = t31.Ma;
                var Minv = t31.Minv;

                if (mt1 is not null && mt1.bin is not null &&
                    ((flags & UpdateFlags.Motion) != UpdateFlags.None)) {

                    if (mt1.isRaw) {
                        var blk = new MsetRawblk(new MemoryStream(mt1.bin, false));
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
                                var M1 = new Matrix {
                                    M11 = rm0.al[t].M11 * f0 + rm1.al[t].M11 * f1,
                                    M21 = rm0.al[t].M21 * f0 + rm1.al[t].M21 * f1,
                                    M31 = rm0.al[t].M31 * f0 + rm1.al[t].M31 * f1,
                                    M41 = rm0.al[t].M41 * f0 + rm1.al[t].M41 * f1,
                                    M12 = rm0.al[t].M12 * f0 + rm1.al[t].M12 * f1,
                                    M22 = rm0.al[t].M22 * f0 + rm1.al[t].M22 * f1,
                                    M32 = rm0.al[t].M32 * f0 + rm1.al[t].M32 * f1,
                                    M42 = rm0.al[t].M42 * f0 + rm1.al[t].M42 * f1,
                                    M13 = rm0.al[t].M13 * f0 + rm1.al[t].M13 * f1,
                                    M23 = rm0.al[t].M23 * f0 + rm1.al[t].M23 * f1,
                                    M33 = rm0.al[t].M33 * f0 + rm1.al[t].M33 * f1,
                                    M43 = rm0.al[t].M43 * f0 + rm1.al[t].M43 * f1,
                                    M14 = rm0.al[t].M14 * f0 + rm1.al[t].M14 * f1,
                                    M24 = rm0.al[t].M24 * f0 + rm1.al[t].M24 * f1,
                                    M34 = rm0.al[t].M34 * f0 + rm1.al[t].M34 * f1,
                                    M44 = rm0.al[t].M44 * f0 + rm1.al[t].M44 * f1
                                };
                                Ma[t] = M1;
                            }
                        }
                    }
                    else {
                        var blk = new Msetblk(new MemoryStream(mt1.bin, false));
                        var os = new MemoryStream();
                        ol ??= M.ol = new Mlink();

                        if (M.binMdlx is not null && M.binMset is not null)
                            ol.Permit(new MemoryStream(M.binMdlx, false), blk.cntb1, new MemoryStream(M.binMset, false), blk.cntb2, mt1.off, _tick, os);

                        var br = new BinaryReader(os);
                        os.Position = 0;
                        Ma = M.Ma = new Matrix[blk.cntb1];

                        for (int t = 0; t < blk.cntb1; t++) {
                            var M1 = new Matrix {
                                M11 = br.ReadSingle(),
                                M12 = br.ReadSingle(),
                                M13 = br.ReadSingle(),
                                M14 = br.ReadSingle(),
                                M21 = br.ReadSingle(),
                                M22 = br.ReadSingle(),
                                M23 = br.ReadSingle(),
                                M24 = br.ReadSingle(),
                                M31 = br.ReadSingle(),
                                M32 = br.ReadSingle(),
                                M33 = br.ReadSingle(),
                                M34 = br.ReadSingle(),
                                M41 = br.ReadSingle(),
                                M42 = br.ReadSingle(),
                                M43 = br.ReadSingle(),
                                M44 = br.ReadSingle()
                            };
                            Ma[t] = M1;
                        }
                    }
                }

                if (Ma is not null && Minv is not null && (flags & UpdateFlags.Transforms) != UpdateFlags.None) {
                    int cnt = Ma.Length;
                    for (var mn = 0; mn < cnt; ++mn) {
                        Minv[mn] = Matrix.Invert(Ma[mn]);
                    }
                }

                var Mv = Matrix.Identity;
                if (M.parent != null && M.iMa != -1 && M.parent.Ma is not null) {
                    Mv = M.parent.Ma[M.iMa];
                }

                if (Ma is not null && (flags & UpdateFlags.Body) != UpdateFlags.None) {
                    foreach (T13vif t13 in t31.al13) {
                        int tops = 0x220;
                        int top2 = 0;
                        var vu1mem = new VU1Mem();
                        new ParseVIF1(vu1mem).Parse(new MemoryStream(t13.bin, false), tops);
                        Body1 body1 = SimaVU1.Sima(vu1mem, Ma, tops, top2, t13.texi, t13.alaxi, Mv);
                        albody1.Add(body1);
                    }
                }

                if ((flags & UpdateFlags.Indices) != UpdateFlags.None) {
                    //if (ct.alsepa == null || ct.altri3 == null)
                    {
                        var altri3 = new List<uint>();
                        var alsepa = new List<Sepa>();

                        {
                            int svi = 0;
                            int bi = 0;
                            uint[] alvi = new uint[4];
                            int ai = 0;
                            int il = (int)_tick;
                            int[] ord = new int[] { 1, 2, 3 };

                            foreach (Body1 b1 in albody1) {
                                if (b1.alvi is null || b1.alfl is null)
                                    continue;

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

                if ((flags & UpdateFlags.Vertices) != UpdateFlags.None) {
                    ct.cntVert = (ct.altri3 != null) ? (ct.altri3.Length) : (0);
                    ct.cntPrimitives = 0;

                    if (ct.cntVert != 0 && ct.altri3 is not null) {

                        int vertexCount = ct.altri3.Length;
                        PTex3[] va = new PTex3[vertexCount];

                        for (int x = 0; x < vertexCount; ++x) {
                            uint xx = ct.altri3[x];
                            uint vi = xx & 4095;
                            uint bi = (xx >> 12) & 4095;
                            uint xi = (xx >> 24) & 4095;
                            Body1 b1 = albody1[(int)bi];

                            var tm = new Matrix();

                            if (b1.alvert is null || b1.albi is null || b1.albw is null || b1.aluv is null || Minv is null || Ma is null)
                                continue;

                            var v3 = b1.alvert[vi];

                            int weightCount = b1.albi[vi].Length;

                            for (var wn = 0; wn < weightCount; ++wn) {
                                tm += (Minv[b1.albi[vi][wn]] * Ma[b1.albi[vi][wn]]) * b1.albw[vi][wn];
                            }

                            v3 = Vector3.TransformCoordinate(v3, tm);
                            va[x] = new PTex3(v3, new Vector2(b1.aluv[xi].X, b1.aluv[xi].Y));
                        }

                        ct.vb ??= new VertexBuffer(
                                SlimDevice,
                                PTex3.Size * ct.cntVert,
                                0,
                                ct.vf = PTex3.Format,
                                Pool.Managed
                                );

                        var gs = ct.vb.Lock(0, 0, 0);

                        try {
                            gs.WriteRange(va);
                            gs.Position = 0;
                        }
                        finally {
                            ct.vb.Unlock();
                        }
                    }
                }
            }
        }

        private void ResetDevice() {
            if (SlimDevice is null)
                return;

            SlimDevice.SetRenderState(RenderState.Lighting, false);
            SlimDevice.SetRenderState(RenderState.ZEnable, true);

            SlimDevice.SetRenderState(RenderState.AlphaBlendEnable, true);
            SlimDevice.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            SlimDevice.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);

            //device.SetRenderState(RenderState.CullMode, Cull.Clockwise);

            ReloadTextures(-1);
        }
        private void ReloadTextures(int ty) {
            if (SlimDevice is not null) {
                int x = 0;

                foreach (var M in Model) {

                    if (x == ty || ty == -1) {
                        M.altex.Clear();
                        M.altex1.Clear();
                        M.altex2.Clear();

                        if (M.timf != null) {
                            //int t = 0;
                            foreach (var st in M.timf.bitmapList) {
                                M.altex.Add(TUt.FromBitmap(SlimDevice, st.bitmap));
                                //st.pic.Save(@"H:\Proj\khkh_xldM\MEMO\pattex\t" + ty + "." + t + ".png", ImageFormat.Png); t++;
                            }

                            if (x == 0) {

                                for (int p = 0; p < 2; p++) {
                                    for (int pi = 0; ; pi++) {
                                        var pic = M.timf.GetPatchBitmap(p, pi);
                                        if (pic == null)
                                            break;
                                        //pic.Save(@"H:\Proj\khkh_xldM\MEMO\pattex\p" + p + "." + pi + ".png", ImageFormat.Png);
                                        switch (p) {
                                            case 0: M.altex1.Add(TUt.FromBitmap(SlimDevice, pic)); break;
                                            case 1: M.altex2.Add(TUt.FromBitmap(SlimDevice, pic)); break;
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
        private void Tick(object sender, EventArgs e) {
            try {
                if (enterLock++ == 0) {
                    try {
                        _currentTick++;
                    }
                    catch (Exception) {
                        throw;
                    }
                }
            }
            finally {
                enterLock--;
            }
        }

        //  TODO: Remove if possible
        private void BtnDEB_Click(object sender, EventArgs e) {
            foreach (var motion in Motions) {
                if (Model[0] is null || Model[0].mdlx is null || Model[0].binMdlx is null || Model[0].binMset is null)
                    break;

                var mt1 = motion.mt1;

                if (mt1 is null || mt1.bin is null)
                    continue;

                var blk = new Msetblk(new MemoryStream(mt1.bin, false));
                _ = Model[0].mdlx.alt31[0];

                var ol = Model[0].ol = new Mlink();
                var fsMdlx = new MemoryStream(Model[0].binMdlx, false);
                var fsMset = new MemoryStream(Model[0].binMset, false);

                for (float t = 0; t <= 300; t++) {
                    ol.Permit_DEB(fsMdlx, blk.cntb1, fsMset, blk.cntb2, mt1.off, t, out _, out var Rvec, out _);
                    Debug.WriteLine(string.Format("{0},{1}", t, Rvec[169 * 4 + 0]));
                }

                break;
            }
        }
        /// <summary>
        /// Export an ASET file of the currently loaded MDLX/MSET combo
        /// </summary>
        /// <param name="progress">Event handler for progress updates</param>
        /// <returns>True if the export has been succesfull</returns>
        private bool ExportASET(ExportProgress progress) {
            var status = new ExportStatus();

            progress.Update(ExportState.Initialization, status);

            if (progress.CancellationPending) {
                return false;
            }

            // get main mesh
            var m = Model[0];

            if (m == null || m.mdlx == null || m.mset == null || Motions.Count <= 0)
                return false;

            var t = m.mdlx.alt31[0];

            // Create a new memory stream
            var mat_data = new MemoryStream();
            var mat_writer = new BinaryWriter(mat_data);
            var bone_count = t.t21 != null ? t.t21.alaxb.Count : 0;
            var anim_count = Motions.Count;

            status.animCount = anim_count;
            status.jointCount = bone_count;
            _ = mat_writer.BaseStream.Position;

            mat_writer.Write(new char[] { 'A', 'S', 'E', 'T' }); // bone count (4 bytes)
            mat_writer.Write(0); // padding (4 bytes)
            mat_writer.Write(bone_count); // bone count (4 bytes)
            mat_writer.Write(anim_count); // animation count (4 bytes)

            // get start of animation offset array
            long offsets_start = mat_writer.BaseStream.Position;

            // move past animation offset array
            mat_writer.BaseStream.Position += ((anim_count * 4) + (0x0F)) & (~0x0F);

            progress.Update(ExportState.Initialization, status);

            if (progress.CancellationPending) {
                return false;
            }

            int anim_num = 0;
            // Get the frame count (ticks)
            foreach (var motion in Motions) {
                if (motion.mt1 == null)
                    continue;

                var anim_name = motion.mt1.id;

                status.animName = anim_name;
                status.animIndex = anim_num;

                anim_name = anim_name.Replace('#', '_');

                // get motion info
                var max_ticks = (int)motion.maxtick;

                status.frameIndex = 0;
                status.frameCount = max_ticks;

                progress.Update(ExportState.Processing, status);

                if (progress.CancellationPending) {
                    return false;
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
                mat_writer.Write(anim_num); // animation index (4 bytes)
                mat_writer.Write(max_ticks); // frame (tick) count (4 bytes)
                mat_writer.Write(0); // padding (4 bytes)
                mat_writer.Write(0); // padding (4 bytes)

                // write animation name
                mat_writer.Write(anim_name.ToCharArray(0, anim_name.Length));

                // move to matrix data start
                mat_writer.BaseStream.Position += (32 - anim_name.Length);

                // increment animation index
                ++anim_num;

                // output all the matrix transforms for each frame
                for (int i = 0; i < max_ticks; i++) {
                    status.frameIndex = i;
                    status.jointIndex = 0;

                    progress.Update(ExportState.Processing, status);

                    if (progress.CancellationPending) {
                        return false;
                    }

                    // set current tick
                    //tick = i;
                    // this tell his viewer to calculate the animation for the frame
                    //  TODO: Remove if possible
                    CalcBody(m.ctb, m, motion.mt1, i, UpdateFlags.Motion);

                    // output each matrix for each bone (matrix4x4 * 4 bytes)

                    for (int bn = 0; bn < bone_count; ++bn) {
                        status.jointIndex = bn;

                        //worker.ReportProgress((int)ExportState.Processing, progress);

                        if (progress.CancellationPending) {
                            return false;
                        }

                        if (m.Ma is null)
                            continue;

                        var mat = m.Ma[bn];
                        /* Matrix Format
                           [M11][M12][M13][M14]
                           [M21][M22][M23][M24]
                           [M31][M32][M33][M34]
                           [M41][M42][M43][M44]
                        */
                        mat_writer.Write(mat.M11);
                        mat_writer.Write(mat.M12);
                        mat_writer.Write(mat.M13);
                        mat_writer.Write(mat.M14);

                        mat_writer.Write(mat.M21);
                        mat_writer.Write(mat.M22);
                        mat_writer.Write(mat.M23);
                        mat_writer.Write(mat.M24);

                        mat_writer.Write(mat.M31);
                        mat_writer.Write(mat.M32);
                        mat_writer.Write(mat.M33);
                        mat_writer.Write(mat.M34);

                        mat_writer.Write(mat.M41);
                        mat_writer.Write(mat.M42);
                        mat_writer.Write(mat.M43);
                        mat_writer.Write(mat.M44);
                    }

                    progress.Update(ExportState.Processing, status);
                }

                progress.Update(ExportState.Processing, status);
            }

            progress.Update(ExportState.Processing, status);

            // reset animation data position
            mat_writer.BaseStream.Position = 0x0C;
            mat_writer.Write(anim_num);

            if (progress.CancellationPending) {
                return false;
            }

            // Generate the file name
            string filename = m.mset.motionID;
            // change file extension
            filename = Path.ChangeExtension(filename, ".ASET");
            // open file
            var outfile = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);

            progress.Update(ExportState.Saving, status);

            // Output all the bytes to a file
            byte[] b_data = mat_data.ToArray();

            outfile.Write(b_data, 0, b_data.Length);

            outfile.Close();

            progress.Update(ExportState.Finished, status);
            //MessageBox.Show("Animation transforms dumped.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }

        #endregion

        #region ILoadf ƒƒ“ƒo

        public void LoadOf(int x, string fp) {
            switch (x) {
                case 0: LoadMdlx(fp, 0); break;
                case 1: LoadMset(fp, 0); break;

                case 2: LoadMdlx(fp, 1); break;
                case 3: LoadMset(fp, 1); break;

                case 4: LoadMdlx(fp, 2); break;
                case 5: LoadMset(fp, 2); break;

                default: throw new NotSupportedException();
            }
        }

        public void SetJointOf(int x, int joint) {
            switch (x) {
                case 1: Model[1].iMa = joint; break;
                case 2: Model[2].iMa = joint; break;

                default: throw new NotSupportedException();
            }
        }

        #endregion

        #region IDisposable

        // Implement IDisposable.
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            // Check to see if Dispose has already been called.
            if (disposed)
                return;

            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing) {
                // Dispose managed resources.
                foreach (var M in Model) {
                    if (M is not null) {
                        M.Dispose();
                    }
                }

                SlimDevice?.Dispose();
                D3D?.Dispose();
            }

            //  Clean up unmanaged resources
            //CloseHandle(_handle);
            _handle = IntPtr.Zero;
            disposed = true;
        }

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# finalizer syntax for finalization code.
        // This finalizer will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide finalizer in types derived from this class.
        ~MdlxConvert() {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(disposing: false);
        }

        #endregion


        #region Static methods
        /// <summary>
        /// Converts the MDLX file at the specifies path to an ASET file. The method requires a corresponding MSET file in the same directory as the MDLX file.
        /// </summary>
        /// <param name="mdlxPath">The path of the MDLX file to convert</param>
        /// <param name="handle">A reference to ControlHandle calling the method (required by SlimDX). This will be removed in future versions for better cross-platform support.</param>
        /// <param name="onProgress">Callback function for the export progress. The function is called at several states of the export process containing the current state/status information.</param>
        /// <returns>True if the conversion has been successful</returns>
        public static bool ToAset(string mdlxPath, IntPtr handle, Action<ExportState, ExportStatus> onProgress) {
            if (!Path.GetExtension(mdlxPath).ToLower().Equals(".mdlx") ||
                !File.Exists(mdlxPath) ||
                !MdlxMatch.HasMset(mdlxPath)) {
                return false;
            }

            //  Initialize converter
            using MdlxConvert converter = new(handle);
            converter.LoadMdlx(mdlxPath);
            converter.LoadMset(MdlxMatch.FindMset(mdlxPath));

            //  Export ASET file
            var progress = new ExportProgress();
            progress.OnProgress += onProgress;

            return converter.ExportASET(progress);
        }

        #endregion
    }
}
