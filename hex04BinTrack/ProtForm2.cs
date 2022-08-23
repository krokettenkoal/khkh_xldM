#define USETEX
//#define REDUCE_TEX_Q
#define HIDE
//#define IKD

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using hex04BinTrack.T;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using vconv122;
using hex04BinTrack.Properties;
using System.Xml;
using System.Drawing.Imaging;
using SlimDX.Direct3D9;
using SlimDX;
using khkh_xldMii.Models;
using hex04BinTrack.VertexFormats;
using hex04BinTrack.Utils;
using System.Net.Http;
using Newtonsoft.Json;
using OpenKh.Research.Kh2Anim.Models;
using System.Linq;
using hex04BinTrack.Models.Anim;

namespace hex04BinTrack {
    public partial class ProtForm2 : Form {
        public ProtForm2() {
            InitializeComponent();
        }
        public ProtForm2(Texex2[] timc, ReadMset mset) {
            this.timc = timc;
            this.mset = mset;
            InitializeComponent();
        }

        private ReadMset mset = null;
        private Texex2[] timc = null;

        public MemoryStream MdlxInMem { get; set; }
        public MemoryStream Motion { get; set; }

        class CaseLineset : IDisposable {
            public VertexBuffer vb;
            public int cntPrimitives, cntVert;

            #region IDisposable メンバ

            public void Dispose() {
                if (vb != null)
                    vb.Dispose();
                vb = null;
            }

            #endregion
        }
        class CasePoints : IDisposable {
            public VertexBuffer vb;
            public int cntPrimitives, cntVert;

            #region IDisposable メンバ

            public void Dispose() {
                if (vb != null)
                    vb.Dispose();
                vb = null;
            }

            #endregion
        }

        class CaseUtil {
            const byte DivCnt = 32;

            public static void DrawHelperObjects(
                Device device,
                CaseLineset clax,
                CaseLineset clrax,
                Matrix[] matrixList,
                List<AxBone> primary,
                int sel,
                List<AxBone> secondary,
                int nPrimaryBones,
                bool drawPrimarySkeleton,
                bool drawSecondarySkeleton,
                HUDHelper hud
            ) {
                int nSecondaryBones = (secondary != null) ? secondary.Count : 0;

                if (clrax.vb != null)
                    clrax.vb.Dispose();
                clrax.vb = null;
                if (true) {
                    clrax.cntVert = DivCnt * 2 * 3;
                    clrax.cntPrimitives = 0;
                    clrax.vb = new VertexBuffer(
                        device,
                        PosClr.StrideSize * clrax.cntVert,
                        Usage.Points,
                        PosClr.Format,
                        Pool.Managed
                        );
                    DataStream gs = clrax.vb.Lock(0, 0, 0);
                    try {
                        PosClr v = new PosClr();
                        Vector3 offv = (sel < 0) ? Vector3.Zero : Vector3.TransformCoordinate(Vector3.Zero, matrixList[sel]);
                        float cf = 100.0f; // ジャイロ口径サイズ
                        float[] ptx = new float[DivCnt];
                        float[] pty = new float[DivCnt];
                        for (int t = 0; t < DivCnt; t++) {
                            float r0 = ((float)(t + 0)) / DivCnt * 6.2831f;
                            ptx[t] = (float)(Math.Cos(r0) * cf);
                            pty[t] = (float)(Math.Sin(r0) * cf);
                        }

                        for (int ax = 0; ax < 3; ax++) {
                            if (ax == 0)
                                v.Color = Color.Red.ToArgb();
                            if (ax == 1)
                                v.Color = Color.LightGreen.ToArgb();
                            if (ax == 2)
                                v.Color = Color.Blue.ToArgb();
                            for (int c = 0; c < DivCnt; c++, clrax.cntPrimitives++) {
                                for (int w = 0; w < 2; w++) {
                                    Vector3 vt, v3;
                                    int cw = (c + w) % DivCnt;
                                    if (ax == 2)
                                        vt = new Vector3(ptx[cw], pty[cw], 0);
                                    else if (ax == 1)
                                        vt = new Vector3(ptx[cw], 0, pty[cw]);
                                    else
                                        vt = new Vector3(0, ptx[cw], pty[cw]);
                                    v3 = offv + Vector3.TransformCoordinate(
                                        vt,
                                        Matrix.RotationQuaternion(
                                            (sel < 0) ? Quaternion.Identity : ExtractRotationQuaternionFrom(matrixList[sel])
                                        )
                                    );
                                    v.X = v3.X;
                                    v.Y = v3.Y;
                                    v.Z = v3.Z;
                                    gs.Write(v);
                                }
                            }
                        }
                    }
                    finally {
                        clrax.vb.Unlock();
                    }
                }

                if (clax.vb != null)
                    clax.vb.Dispose();
                clax.vb = null;
                if (true) {
                    clax.cntVert = 2 * 3;
                    clax.cntPrimitives = 0;
                    clax.vb = new VertexBuffer(
                        device,
                        PosClr.StrideSize * clax.cntVert,
                        Usage.Points,
                        PosClr.Format,
                        Pool.Managed
                        );
                    DataStream gs = clax.vb.Lock(0, 0, 0);
                    try {
                        PosClr v = new PosClr();
                        Vector3 offv = (sel < 0) ? Vector3.Zero : Vector3.TransformCoordinate(Vector3.Zero, matrixList[sel]);
                        float cf = 30.0f;
                        for (int ax = 0; ax < 3; ax++, clax.cntPrimitives++) {
                            if (ax == 0)
                                v.Color = Color.Red.ToArgb();
                            if (ax == 1)
                                v.Color = Color.LightGreen.ToArgb();
                            if (ax == 2)
                                v.Color = Color.BlueViolet.ToArgb();
                            Vector3 v3 = Vector3.TransformCoordinate(
                                new Vector3(
                                    (ax == 0) ? cf : 0,
                                    (ax == 1) ? cf : 0,
                                    (ax == 2) ? cf : 0
                                ),
                                Matrix.RotationQuaternion(
                                    (sel < 0) ? Quaternion.Identity : ExtractRotationQuaternionFrom(matrixList[sel])
                                )
                            );
                            v.X = offv.X;
                            v.Y = offv.Y;
                            v.Z = offv.Z;
                            gs.Write(v);
                            v.X += v3.X;
                            v.Y += v3.Y;
                            v.Z += v3.Z;
                            gs.Write(v);
                        }
                    }
                    finally {
                        clax.vb.Unlock();
                    }
                }

                hud.Clear();

                if (nPrimaryBones + nSecondaryBones != 0) {
                    if (drawPrimarySkeleton) {
                        for (int idx = 0; idx < nPrimaryBones; idx++) {
                            int parent = primary[idx].parent;

                            var vTo = Vector3.TransformCoordinate(Vector3.Zero, matrixList[idx]);
                            var vFrom = (parent < 0)
                                ? vTo
                                : Vector3.TransformCoordinate(Vector3.Zero, matrixList[parent]);

                            hud.bones.Add(new HUDHelper.Bone { vTo = vTo, vFrom = vFrom, name = $"#{idx}" });
                        }
                    }

                    if (drawSecondarySkeleton) {
                        for (int idx = 0; idx < nSecondaryBones; idx++) {
                            int parent = secondary[idx].parent;

                            var vTo = Vector3.TransformCoordinate(Vector3.Zero, matrixList[nPrimaryBones + idx]);
                            var vFrom = (parent < 0)
                                ? vTo
                                : Vector3.TransformCoordinate(Vector3.Zero, matrixList[parent]);

                            hud.bones.Add(new HUDHelper.Bone { vTo = vTo, vFrom = vFrom, name = $"#{nPrimaryBones + idx}" });
                        }
                    }
                }
            }

            private static Quaternion ExtractRotationQuaternionFrom(Matrix matrix) {
                matrix.Decompose(out Vector3 absS, out Quaternion absR, out Vector3 absT);
                return absR;
            }
        }

        Direct3D direct3d;
        Device device;
        IndexBuffer ib;
        VertexBuffer vb;

        HUDHelper hud = new HUDHelper();
        CaseLineset clv = new CaseLineset();
        CaseLineset clax = new CaseLineset();
        CaseLineset clrax = new CaseLineset();
        CasePoints cpv = new CasePoints();
        CaseLineset clresik = new CaseLineset();
        CasePoints cpresik = new CasePoints();

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if (device.Present().IsFailure)
                return;

#if USETEX
            bool fUseTex = radioButtonSelTex.Checked;
#else
            bool fUseTex = false;
#endif

            if (radioButtonSelWire.Checked) {
                device.SetRenderState(RenderState.FillMode, FillMode.Wireframe);
                device.SetRenderState(RenderState.Lighting, false);
            }
            else if (fUseTex) {
                device.SetRenderState(RenderState.FillMode, FillMode.Solid);
                device.SetRenderState(RenderState.Lighting, false);
            }
            else {
                Vector3 vdir = new Vector3(0, 0, -1);
                vdir = Vector3.TransformCoordinate(vdir, Matrix.RotationQuaternion(Quaternion.Invert(quat)));

                device.SetRenderState(RenderState.FillMode, FillMode.Solid);
                device.SetRenderState(RenderState.Lighting, true);
                device.EnableLight(0, true);
                Light l0 = device.GetLight(0);
                l0.Type = LightType.Directional;
                l0.Direction = vdir;
                l0.Diffuse = Color.White;
                l0.Ambient = Color.BlueViolet;
                device.SetLight(0, l0);
            }

            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, panel1.BackColor, 1.0f, 0);
            device.BeginScene();

            if (ib != null && vb != null && renderModel.Checked) {
                VertexFormat fvf = vb.Description.FVF;
                device.SetStreamSource(0, vb, 0, VertexFmtSize.Compute(fvf));
                device.VertexFormat = fvf;
                device.Indices = null;
                int off = 0, cnt = 0;
                if (fUseTex) {
                    device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                    device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                    device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.Anisotropic);
                    device.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Anisotropic);
                }
                for (int t = 0; t < alptris.Count; t++) {
                    if (fUseTex)
                        device.SetTexture(0, altex[t]);
                    cnt = alptris[t];
                    device.DrawPrimitives(PrimitiveType.TriangleList, off, cnt);
                    off += 3 * cnt;
                }
                if (fUseTex) {
                    device.SetTexture(0, null);
                    device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Diffuse);
                }
            }

            Bitmap hudBitmap = null;

            if (true) {
                device.SetRenderState(RenderState.FillMode, FillMode.Solid);
                device.SetRenderState(RenderState.Lighting, false);
                device.SetRenderState(RenderState.PointSize, Math.Min(device.GetRenderState(RenderState.PointSizeMax), 4));
                device.SetRenderState(RenderState.ZFunc, Compare.Always);

                if (checkBoxShowBones.Checked && hud.bones.Any()) {
                    var size = panel1.ClientSize;
                    hudBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
                    using (var canvas = Graphics.FromImage(hudBitmap)) {
                        canvas.TranslateTransform(size.Width / 2, size.Height / 2);

                        var projection = device.GetTransform(TransformState.Projection);
                        var view = device.GetTransform(TransformState.View);
                        var world = device.GetTransform(TransformState.World);
                        var newView = Matrix.OrthoLH(panel1.ClientSize.Width, -panel1.ClientSize.Height, -1, 1);
                        var toView = view * projection * Matrix.Invert(newView);

                        var existances = new List<BoneExistence>();

                        foreach (var bone in hud.bones) {
                            var pos0 = Vector3.TransformCoordinate(bone.vFrom, toView);
                            var pos1 = Vector3.TransformCoordinate(bone.vTo, toView);

                            canvas.DrawLine(new Pen(Brushes.LightGreen, 2), pos0.X, pos0.Y, pos1.X, pos1.Y);
                            existances.Add(new BoneExistence { pos = new Point((int)pos1.X, (int)pos1.Y), name = bone.name });
                        }
                        foreach (var group in existances.GroupBy(it => it.pos)) {
                            var pos = group.Key;
                            canvas.DrawRectangle(Pens.Yellow, pos.X - 3, pos.Y - 3, 6, 6);
                            canvas.DrawString(
                                string.Join(", ", group.Select(it => it.name)),
                                panel1.Font, Brushes.BlueViolet, new PointF(pos.X + 10, pos.Y - 15)
                            );
                        }
                    }
                }
                if (checkBoxShowBones.Checked && clv.vb != null) {
                    device.SetStreamSource(0, clv.vb, 0, VertexFmtSize.Compute(PosClr.Format));
                    device.VertexFormat = PosClr.Format;
                    device.Indices = null;
                    device.DrawPrimitives(PrimitiveType.LineList, 0, clv.cntPrimitives);
                }
                if (checkBox3ax.Checked && clax.vb != null) {
                    device.SetStreamSource(0, clax.vb, 0, VertexFmtSize.Compute(PosClr.Format));
                    device.VertexFormat = PosClr.Format;
                    device.Indices = null;
                    device.DrawPrimitives(PrimitiveType.LineList, 0, clax.cntPrimitives);
                }
                if (checkBoxShowBones.Checked && cpv.vb != null) {
                    device.SetStreamSource(0, cpv.vb, 0, VertexFmtSize.Compute(PosClr.Format));
                    device.VertexFormat = PosClr.Format;
                    device.Indices = null;
                    device.DrawPrimitives(PrimitiveType.PointList, 0, cpv.cntPrimitives);
                }
                device.SetRenderState(RenderState.ZFunc, Compare.Less);
                if (checkBoxGyro.Checked && clrax.vb != null) {
                    device.SetStreamSource(0, clrax.vb, 0, VertexFmtSize.Compute(PosClr.Format));
                    device.VertexFormat = PosClr.Format;
                    device.Indices = null;
                    device.DrawPrimitives(PrimitiveType.LineList, 0, clrax.cntPrimitives);
                }
            }

            device.EndScene();
            device.Present();

            if (hudBitmap != null) {
                e.Graphics.DrawImageUnscaled(hudBitmap, new Rectangle(Point.Empty, hudBitmap.Size));
            }
        }

        class BoneExistence {
            internal Point pos;
            internal string name;
        }

        PresentParameters PP {
            get {
                PresentParameters pp = new PresentParameters();
                pp.Windowed = true;
                pp.SwapEffect = SwapEffect.Discard;
                pp.EnableAutoDepthStencil = true;
                pp.AutoDepthStencilFormat = Format.D16;
                pp.BackBufferHeight = Math.Max(1, panel1.ClientSize.Height);
                pp.BackBufferWidth = Math.Max(1, panel1.ClientSize.Width);
                return pp;
            }
        }

        private void ProtForm_Load(object sender, EventArgs e) {
            this.Disposed += new EventHandler(ProtForm2_Disposed);

            direct3d = new Direct3D();
            device = new Device(direct3d, 0, DeviceType.Hardware, panel1.Handle, CreateFlags.SoftwareVertexProcessing, PP);
            devReset();

            offset = Settings.Default.offset;
            quat = Settings.Default.quat;
            zval = Settings.Default.zval;
            if (Settings.Default.Sel_wire)
                radioButtonSelWire.Checked = true;
            if (Settings.Default.Sel_solid)
                radioButtonSelSolid.Checked = true;
            if (Settings.Default.Sel_tex)
                radioButtonSelTex.Checked = true;

            reshape();
            prepare();
            calcvbib();
            calc骨vb();

            panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);

            foreach (AxBone a in alaxbone) {
                ListViewItem lvi = listView1.Items.Add(FmtAxBone.Format(a));
                lvi.Tag = a;
            }

            if (mset != null) {
                foreach (ReadAnib anib_ in mset.alanb) {
                    var anib = anib_;
                    ToolStripMenuItem tsi = new ToolStripMenuItem(anib_.name);
                    tsi.Click += delegate (object sender2, EventArgs e2) {
                        byBakery = null;
                        Motion = anib.motion;
                        SetToval(anib.toval);
                        checkBoxSpv_CheckedChanged(null, null);

                        foreach (ToolStripItem cur in contextMenuStripAnib.Items) {
                            if (cur is ToolStripMenuItem) {
                                ((ToolStripMenuItem)cur).Checked = cur == tsi;
                            }
                        }
                    };
                    contextMenuStripAnib.Items.Add(tsi);
                }
            }
        }

        void ProtForm2_Disposed(object sender, EventArgs e) {
            List<IDisposable> trashbox = new List<IDisposable>(new IDisposable[] { clv, clax, clrax, cpv, clresik, cpresik, ib, vb, device, direct3d });

            foreach (IDisposable obj in trashbox) if (obj != null) obj.Dispose();
        }

        class FmtAxBone {
            public static string Format(AxBone a) {
                return string.Format(
                    "{0,-3} {1,3}|{4,7:f}{5,7:f}{6,7:f}{7,7:f}|{8,7:f}{9,7:f}{10,7:f}{11,7:f}|{12,7:f}{13,7:f}{14,7:f}{15,7:f}|\n"
                    , a.cur, a.parent, 0, 0
                    , a.x1, a.y1, a.z1, a.w1
                    , a.x2, a.y2, a.z2, a.w2
                    , a.x3, a.y3, a.z3, a.w3
                    );
            }
            public static string Format(AxBone a, string hint) {
                return string.Format(
                    "{0,-3} {1,3}|{2}|{3}|{16}|{4,7:f}{5,7:f}{6,7:f}{7,7:f}|{8,7:f}{9,7:f}{10,7:f}{11,7:f}|{12,7:f}{13,7:f}{14,7:f}{15,7:f}|\n"
                    , a.cur, a.parent, a.v08, a.v0c
                    , a.x1, a.y1, a.z1, a.w1
                    , a.x2, a.y2, a.z2, a.w2
                    , a.x3, a.y3, a.z3, a.w3
                    , hint
                    );
            }
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e) {
            zval = Math.Max(1.0f, zval + e.Delta / 200.0f);
            reshape();
            panel1.Invalidate();
        }

        int cntVert, cntPrimitives;

        Point[] texstpts = new Point[3] { Point.Empty, Point.Empty, Point.Empty };

        public void calcvbib() {
            var altsort = meshBuilder.triSortList;
            var altris = meshBuilder.triList;

            cntVert = 0;
            foreach (List<V6> alt in altsort) { cntVert += alt.Count; }
            cntPrimitives = cntVert / 3;

            alptris.Clear();

            if (vb != null) {
                vb.Dispose();
                vb = null;
            }
            if (cntVert != 0) {
                vb = new VertexBuffer(
                    device,
                    Pcnt.StrideSize * cntVert,
                    Usage.Points,
                    Pcnt.Format,
                    Pool.Managed
                    );
                DataStream gs = vb.Lock(0, 0, 0);
                try {
                    Pcnt v = new Pcnt();
                    for (int w = 0; w < altsort.Count; w++) {
                        altris = altsort[w];
                        alptris.Add(altris.Count / 3);
                        for (int t = 0; t < altris.Count / 3; t++) {
                            v.Color = Color.White.ToArgb();
                            Vector3 vn = UtilCrossCalc.calc(altris[3 * t + 0].v4, altris[3 * t + 1].v4, altris[3 * t + 2].v4);
                            vn.Normalize();
                            v.Normal = vn;
                            for (int s = 0; s < 3; s++) {
                                V4 v4 = altris[3 * t + s].v4;
                                v.X = v4.x;
                                v.Y = v4.y;
                                v.Z = v4.z;
                                Point tpt = altris[3 * t + s].texpt;
                                v.Tu = ((float)tpt.X) / 256.0f;// altw[w].Width;
                                v.Tv = ((float)tpt.Y) / 256.0f;// altw[w].Height;
                                gs.Write(v);
                            }
                        }
                    }
                    vbinfUpdate("vb", gs.Length);
                }
                finally {
                    vb.Unlock();
                }
            }
            if (ib != null) {
                ib.Dispose();
                ib = null;
            }
            if (cntPrimitives != 0) {
                ib = new IndexBuffer(
                    device,
                    2 * 3 * cntPrimitives,
                    Usage.Points,
                    Pool.Managed,
                    true
                    );
                DataStream gs = ib.Lock(0, 0, 0);
                try {
                    for (int t = 0; t < cntVert; t++) {
                        gs.Write((ushort)(t));
                    }
                }
                finally {
                    ib.Unlock();
                }
            }
        }

        private void vbinfUpdate(string key, long val) {
            string t = val.ToString("#,##0") + " B";
            foreach (ListViewItem lvi in listViewVBInf.Items) {
                if (lvi.Text.Equals(key)) {
                    lvi.SubItems[1].Text = t;
                    return;
                }
            }
            if (true) {
                ListViewItem lvi = listViewVBInf.Items.Add(key);
                lvi.SubItems.Add(t);
            }
        }

        class UtilCrossCalc {
            public static Vector3 calc(V4 v1, V4 v2, V4 v3) {
                Vector3 vl = new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
                Vector3 vr = new Vector3(v3.x - v2.x, v3.y - v2.y, v3.z - v2.z);
                return Vector3.Cross(vl, vr);
            }
        }

        List<Texture> altex = new List<Texture>();
        List<Size> altw = new List<Size>();
        List<int> alptris = new List<int>();

        private void devReset() {
            device.SetRenderState(RenderState.Lighting, false);
            device.SetRenderState(RenderState.ZEnable, true);
            device.SetRenderState(RenderState.AlphaFunc, Compare.Greater);
            device.SetRenderState(RenderState.AlphaRef, 0);
            device.SetRenderState(RenderState.BlendOperation, BlendOperation.Subtract);
            device.SetRenderState(RenderState.AlphaBlendEnable, false);

            Usetex();
        }

        int timi = 0;

        public Texex2 TheTimf {
            get { return timc[timi]; }
        }

        private void Usetex() {
#if USETEX
            foreach (Resource res in altex)
                res.Dispose();
            altex.Clear();
            altw.Clear();
            byte[] al = new byte[] {
                (byte)((int)numericUpDownTex0.Value),
                (byte)((int)numericUpDownTex1.Value),
                (byte)((int)numericUpDownTex2.Value),
            };
            for (int t = 0; TheTimf != null; t++) {
                Bitmap pic = TheTimf.GetTex2(t, al);
                if (pic == null)
                    break;
#if REDUCE_TEX_Q
                Bitmap p2 = new Bitmap(32, 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics cv = Graphics.FromImage(p2)) {
                    cv.DrawImage(pic, Rectangle.FromLTRB(0, 0, 32, 16));
                }
                Texture tex = Texture.FromBitmap(device, p2, Usage.None, Pool.Managed);
                altex.Add(tex);
#else
                Texture tex = TexUtil.FromBitmap(device, pic, Usage.None, Pool.Managed);
                altex.Add(tex);
#endif
                altw.Add(new Size(pic.Width, pic.Height));
            }
#else
            altw.Clear();
            for (int t = 0; t < 100; t++) altw.Add(new Size(100, 100));
#endif
        }

        int cxvwpt = 100;
        int cyvwpt = 100;
        int czvwpt = 100;
        float zval = 1;
        Quaternion quat = Quaternion.Identity;
        Vector3 offset = Vector3.Zero;

        void reshape() {
            int cxw = panel1.Width;
            int cyw = panel1.Height;
            float fx = (cxw > cyw) ? ((float)cxw / cyw) : 1.0f;
            float fy = (cxw < cyw) ? ((float)cyw / cxw) : 1.0f;

            Matrix mvo = Matrix.Identity;

            float fact = (float)(zval);

            device.SetTransform(TransformState.Projection, Matrix.OrthoLH(
                fx * cxvwpt * fact, fy * cyvwpt * fact, czvwpt * fact / +1, czvwpt * fact / -1
                ));
            Matrix mv = Matrix.RotationQuaternion(quat);
            mv.M41 += offset.X;
            mv.M42 += offset.Y;
            mv.M43 += offset.Z;
            device.SetTransform(TransformState.View, mvo * mv);
        }

        class V4 {
            public float x, y, z, w;

            public V4() { }
            public V4(float x, float y, float z, float w) {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }
        }

        class V6 {
            public V4 v4 = null;
            public Point texpt = Point.Empty;
            public int sel = -1;

            public V6(V4 v4, Point texpt, int sel) {
                this.v4 = v4;
                this.texpt = texpt;
                this.sel = sel;
            }
        }

        class Ent {
            public int i, cntvert, cntvtx, prot;

            public Ent(int i, int cntvert, int cntvtx, int prot) {
                this.i = i;
                this.cntvert = cntvert;
                this.cntvtx = cntvtx;
                this.prot = prot;
            }

            public override string ToString() {
                return string.Format("#{2,3} {1,3} {2,3} {3}", i, cntvtx, cntvert, prot);
            }
        }

        List<Deferred> deferredList = new List<Deferred>();

        class Deferred {
            public VU1Mem vu1;
            public int tops;
            public int[] alaxi;
            public int tsel;

            public Deferred(VU1Mem vu1, int tsel, int tops, int[] alaxi) {
                this.vu1 = vu1;
                this.tsel = tsel;
                this.tops = tops;
                this.alaxi = alaxi;
            }
        }

        public void PushDeferred(VU1Mem M, int tsel, int tops, int[] alaxi) {
            deferredList.Add(new Deferred(M, tsel, tops, alaxi));
        }
        public void InitBone(List<AxBone> alaxbone) {
            this.alaxbone = alaxbone;
        }

        List<AxBone> alaxbone = null;

        Matrix[] matrices = new Matrix[0];
        AxBone[] burntBoneList = null;

        MeshBuilder meshBuilder = new MeshBuilder();

        void prepare() {
            meshBuilder.Clear();

            burntBoneList = UtilApplyMsetoesToAxBone.apply(
                alaxbone,
                toval,
                CurTick,
                MaxTick,
                checkBoxUseDefval.Checked,
                checkBoxApplyMotion.Checked
            )
                .ToArray();

            float tick = (float)numericUpDown1.Value;

            if (useBakery.Checked && Motion != null) {
                matrices = CallBakery(tick);
            }
            else {
                matrices = BasixAxBoneUtil4.composeMatrices(
                    burntBoneList,
                    toval,
                    tick,
                    (toval != null && checkBoxUseik.Checked) ? new InvkParser(listView1.Items.Count + listView2.Items.Count, toval) : null,
                    listView1.Items.Count
                    );
            }

            foreach (Deferred it in deferredList) {
                var localMatrices = BasixAxBoneUtil3.migrate(it.alaxi, matrices);
                meshBuilder.AppendMesh(it.vu1, it.tsel, it.tops, localMatrices);
            }
        }

        HttpClient httpClient = new HttpClient();

        Matrix[][] byBakery;

        private Matrix[] CallBakery(float tick) {
            if (byBakery == null) {
                var ticks = new List<float>();
                var maxTime = (int)MaxTick;
                for (float time = 0; time <= maxTime; time += bakeryReso) {
                    ticks.Add(time);
                }

                var writer = new StringWriter(new StringBuilder(1024 * 1024 * 10));
                JsonSerializer.CreateDefault().Serialize(
                    writer,
                    new BuildMatricesRequest {
                        KeyFrames = ticks.ToArray(),
                        Mdlx = MdlxInMem.ToArray(),
                        Motion = Motion.ToArray(),
                    }
                );

                var resp = httpClient.PostAsync(
                    Settings.Default.BakeryUrl,
                    new StringContent(
                        writer.ToString(),
                        Encoding.UTF8,
                        "application/json"
                    )
                )
                    .Result;

                var respModel = JsonConvert.DeserializeObject<BuildMatricesResponse>(
                    resp.Content.ReadAsStringAsync().Result
                );

                byBakery = new Matrix[respModel.Matrices.Length][];
                for (int y = 0; y < byBakery.Length; y++) {
                    var matrices = new Matrix[respModel.MatrixCount];
                    byBakery[y] = matrices;
                    var frame = respModel.Matrices[y];
                    for (int mtxIdx = 0; mtxIdx < respModel.MatrixCount; mtxIdx++) {
                        var mtx = new Matrix();

                        mtx.M11 = frame[mtxIdx][0];
                        mtx.M12 = frame[mtxIdx][1];
                        mtx.M13 = frame[mtxIdx][2];
                        mtx.M14 = frame[mtxIdx][3];

                        mtx.M21 = frame[mtxIdx][4];
                        mtx.M22 = frame[mtxIdx][5];
                        mtx.M23 = frame[mtxIdx][6];
                        mtx.M24 = frame[mtxIdx][7];

                        mtx.M31 = frame[mtxIdx][8];
                        mtx.M32 = frame[mtxIdx][9];
                        mtx.M33 = frame[mtxIdx][10];
                        mtx.M34 = frame[mtxIdx][11];

                        mtx.M41 = frame[mtxIdx][12];
                        mtx.M42 = frame[mtxIdx][13];
                        mtx.M43 = frame[mtxIdx][14];
                        mtx.M44 = frame[mtxIdx][15];

                        matrices[mtxIdx] = mtx;
                    }
                }
            }

            return byBakery[(int)(tick / bakeryReso) % byBakery.Length];
        }

        float bakeryReso = 1 / 2f;

        float MinTick {
            get {
#if true
                return 0;
#else
                if (toval != null) {
                    return toval.mintick;
                }
                return 1;
#endif
            }
        }
        float CurTick {
            get {
                return (float)numericUpDown1.Value % MaxTick;
            }
        }
        float MaxTick {
            get {
                if (toval != null) {
                    return toval.maxtick;
                }
                return 1;
            }
        }

        private void calc骨vb() {
            int sel1 = -1;
            foreach (int temp in listView1.SelectedIndices) { sel1 = temp; break; }
            int sel2 = -1;
            foreach (int temp in listView2.SelectedIndices) { sel2 = temp; break; }

            int sel = (sel2 < 0) ? sel1 : (listView1.Items.Count + sel2);
            CaseUtil.DrawHelperObjects(
                device, clax, clrax, matrices, alaxbone, sel, (toval != null) ? toval.t5Bone : null, alaxbone.Count,
                checkBoxSpv.Checked, !checkBoxSpv.Checked,
                hud
            );

            if (clv.vb != null)
                vbinfUpdate("B'ones ls vb", clv.vb.Description.SizeInBytes);
            if (cpv.vb != null)
                vbinfUpdate("B'ones ps vb", cpv.vb.Description.SizeInBytes);

            if (burntBoneList != null && sel >= 0) {
                float ex1 = float.NaN;
                float ey1 = float.NaN;
                float ez1 = float.NaN;
                float ex2 = float.NaN;
                float ey2 = float.NaN;
                float ez2 = float.NaN;
                float ex3 = float.NaN;
                float ey3 = float.NaN;
                float ez3 = float.NaN;
                if (toval != null) {
                    foreach (T1 t1 in toval.t1Static) {
                        if (t1.c00 == sel) {
                            float yval = t1.c04;
                            switch (t1.c02) {
                                case 0:
                                    ex1 = yval;
                                    break;
                                case 1:
                                    ey1 = yval;
                                    break;
                                case 2:
                                    ez1 = yval;
                                    break;
                                case 3:
                                    ex2 = yval;
                                    break;
                                case 4:
                                    ey2 = yval;
                                    break;
                                case 5:
                                    ez2 = yval;
                                    break;
                                case 6:
                                    ex3 = yval;
                                    break;
                                case 7:
                                    ey3 = yval;
                                    break;
                                case 8:
                                    ez3 = yval;
                                    break;
                            }
                        }
                    }
                    foreach (T2 t2 in toval.t2Anim) {
                        if (t2.c00 == sel) {
                            float tick = (float)numericUpDown1.Value;
                            float yval = YVal.calc2(CurTick, t2.t9TimeLines);
                            switch (t2.c02 & 15) {
                                case 0:
                                    ex1 = yval;
                                    break;
                                case 1:
                                    ey1 = yval;
                                    break;
                                case 2:
                                    ez1 = yval;
                                    break;
                                case 3:
                                    ex2 = yval;
                                    break;
                                case 4:
                                    ey2 = yval;
                                    break;
                                case 5:
                                    ez2 = yval;
                                    break;
                                case 6:
                                    ex3 = yval;
                                    break;
                                case 7:
                                    ey3 = yval;
                                    break;
                                case 8:
                                    ez3 = yval;
                                    break;
                                default:
                                    Debug.Fail("t2.c02 = " + t2.c02);
                                    break;
                            }
                        }
                    }
                    int baset2t = listView1.Items.Count;
                    foreach (T2 t2 in toval.t2x) {
                        if (t2.c00 == sel - baset2t) {
                            float tick = (float)numericUpDown1.Value;
                            float yval = YVal.calc2(CurTick, t2.t9TimeLines);
                            switch (t2.c02) {
                                case 0:
                                    ex1 = yval;
                                    break;
                                case 1:
                                    ey1 = yval;
                                    break;
                                case 2:
                                    ez1 = yval;
                                    break;
                                case 3:
                                    ex2 = yval;
                                    break;
                                case 4:
                                    ey2 = yval;
                                    break;
                                case 5:
                                    ez2 = yval;
                                    break;
                                case 6:
                                    ex3 = yval;
                                    break;
                                case 7:
                                    ey3 = yval;
                                    break;
                                case 8:
                                    ez3 = yval;
                                    break;
                            }
                        }
                    }
                }

                AxBone ax = burntBoneList[sel];
                matrices[sel].Decompose(out Vector3 absS, out Quaternion absR, out Vector3 absT);
                labelSelaxb.Text = string.Concat(
                    string.Format(
                    "bone #{0,3}\n"
                    + "loc S {1,10:0.000}{2,10:0.000}{3,10:0.000}{4,10:0.000}\n"
                    + "loc R {5,10:0.000}{6,10:0.000}{7,10:0.000}{8,10:0.000}\n"
                    + "loc T {9,10:0.000}{10,10:0.000}{11,10:0.000}{12,10:0.000}\n\n"
                        , sel
                        , ax.x1, ax.y1, ax.z1, ax.w1
                        , ax.x2, ax.y2, ax.z2, ax.w2
                        , ax.x3, ax.y3, ax.z3, ax.w3
                    ),
                    string.Format(""
                    + "mod S {0,10:0.000}{1,10:0.000}{2,10:0.000}\n"
                    + "mod R {3,10:0.000}{4,10:0.000}{5,10:0.000}\n"
                    + "mod T {6,10:0.000}{7,10:0.000}{8,10:0.000}\n\n"
                        , ex1, ey1, ez1
                        , ex2, ey2, ez2
                        , ex3, ey3, ez3
                    ),
                    string.Format(""
                    + "abs s {0,10:0.000}{1,10:0.000}{2,10:0.000}\n"
                    + "abs q {3,10:0.000}{4,10:0.000}{5,10:0.000}{6,10:0.000}\n"
                    + "abs t {7,10:0.000}{8,10:0.000}{9,10:0.000}\n\n"
                        , absS.X, absS.Y, absS.Z
                        , absR.X, absR.Y, absR.Z, absR.W
                        , absT.X, absT.Y, absT.Z
                    ),
                    ""
                );
            }
        }

        void calcResikvb() {
        }

        class TransfUtil {
            public static V4 calc(V4 v, Matrix A) {
                Vector3 v3 = new Vector3(v.x, v.y, v.z);
                Vector3 v3t = Vector3.TransformCoordinate(v3, A);
                return new V4(
                    +v3t.X,
                    +v3t.Y,
                    +v3t.Z,
                    v.w
                    );
            }
        }

        class MeshBuilder {
            /// <summary>
            /// 3つで良いのだが，取り敢えず4つ
            /// </summary>
            private V6[] v6 = new V6[] { null, null, null, null };
            private int iv6 = 0;

            internal List<V6> triList = null;
            internal List<List<V6>> triSortList = new List<List<V6>>();

            private int entryindex = 0;

            private List<Ent> entList = new List<Ent>();

            internal void AppendMesh(VU1Mem M, int tsel, int tops, Matrix[] alax) {
                MemoryStream si = new MemoryStream(M.vumem, false);
                BinaryReader br = new BinaryReader(si);
                si.Position = 16 * tops;
                int v00 = br.ReadInt32();
                if (v00 == 2)
                    return;
                if (v00 != 1 && v00 != 2)
                    throw new ProtInvalidTypeException();
                int v04 = br.ReadInt32();
                int v08 = br.ReadInt32();
                int v0c = br.ReadInt32();
                int v10 = br.ReadInt32(); // cntindices
                int v14 = br.ReadInt32(); // offindices
                int v18 = br.ReadInt32(); // offi2 (axbone)
                int v1c = br.ReadInt32(); // off matrices
                int v20 = (v00 == 1) ? br.ReadInt32() : 0; // cntvertscolor
                int v24 = (v00 == 1) ? br.ReadInt32() : 0; // offvertscolor
                int v28 = (v00 == 1) ? br.ReadInt32() : 0;
                int v2c = (v00 == 1) ? br.ReadInt32() : 0;
                int v30 = br.ReadInt32(); // cntverts 
                int v34 = br.ReadInt32(); // offverts
                int v38 = br.ReadInt32(); // 
                int v3c = br.ReadInt32(); // cnt axbone

                Point[] texpts = new Point[v10];
                int[] vertexindices = new int[v10];
                int[] vertexsteps = new int[v10];
                V4[] vtxpts = new V4[v30];

                byte[] alrefax = null;
                alrefax = new byte[v30];

                si.Position = 16 * (tops + v14);
                for (int t = 0; t < v10; t++) {
                    int tx = br.ReadUInt16() / 16;
                    br.ReadUInt16();
                    int ty = br.ReadUInt16() / 16;
                    br.ReadUInt16();
                    texpts[t] = new Point(tx, ty);
                    vertexindices[t] = br.ReadUInt16();
                    br.ReadUInt16();
                    vertexsteps[t] = br.ReadUInt16();
                    br.ReadUInt16();
                }

                si.Position = 16 * (tops + v34);
                for (int t = 0; t < v30; t++) {
                    float vx = (br.ReadSingle());
                    float vy = (br.ReadSingle());
                    float vz = (br.ReadSingle());
                    float vw = (br.ReadSingle());
                    vtxpts[t] = new V4(vx, vy, vz, vw);
                }

                si.Position = 16 * (tops + v18);
                for (int t = 0, pos = 0; t < v3c; t++) {
                    int cx = br.ReadInt32();
                    for (int x = 0; x < cx; x++, pos++) {
                        alrefax[pos] = Convert.ToByte(t);
                    }
                }

                for (int t = 0; t < v10; t++) {
                    int vi = vertexindices[t];
                    Add1(TransfUtil.calc(vtxpts[vi], alax[alrefax[vi]]), texpts[t], tsel, vertexsteps[t], entryindex);
                }

                entList.Add(new Ent(entryindex, v10, v30, v00));
                entryindex++;
            }

            private void Add1(V4 v4, Point texpt, int tsel, int step, int tag) {
                v6[iv6] = new V6(v4, texpt, tag);
                iv6 = (iv6 + 1) & 3;

                while (triSortList.Count <= tsel) {
                    triSortList.Add(new List<V6>());
                }
                triList = triSortList[tsel];

                if (step == 0x10) { // 追加
                }
                else if (step == 0x20) { // a tri-strip
                    triList.Add(v6[(iv6 - 1) & 3]);
                    triList.Add(v6[(iv6 - 2) & 3]);
                    triList.Add(v6[(iv6 - 3) & 3]);
                }
                else if (step == 0x30) { // a tri
                    triList.Add(v6[(iv6 - 1) & 3]);
                    triList.Add(v6[(iv6 - 3) & 3]);
                    triList.Add(v6[(iv6 - 2) & 3]);
                }
                else if (step == 0x00) { // two sided!
                    triList.Add(v6[(iv6 - 1) & 3]);
                    triList.Add(v6[(iv6 - 2) & 3]);
                    triList.Add(v6[(iv6 - 3) & 3]);

                    triList.Add(v6[(iv6 - 1) & 3]);
                    triList.Add(v6[(iv6 - 3) & 3]);
                    triList.Add(v6[(iv6 - 2) & 3]);
                }
                else {
                    throw new NotSupportedException("不明なstep → " + step);
                }
            }

            internal void Clear() {
                entList.Clear();
                triSortList.Clear();
                entryindex = 0;
            }
        }

        Point curs = Point.Empty;

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                curs = new Point(e.X, e.Y);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (curs != Point.Empty) {
                int dX = e.X - curs.X;
                int dY = e.Y - curs.Y;
                if (dX != 0 || dY != 0) {
                    curs = new Point(e.X, e.Y);
                    bool pressShift = 0 != (Control.ModifierKeys & Keys.Shift);
                    if (!pressShift) {
                        quat *= (Quaternion.RotationYawPitchRoll(dX / 100.0f, dY / 100.0f, 0));
                        quat.Normalize();
                        if (quat.Length() < 1e-5)
                            quat = Quaternion.Identity;
                    }
                    else {
                        Vector3 v3off = new Vector3(dX, -dY, 0);
                        offset += (v3off);
                    }
                    reshape();
                    panel1.Invalidate();
                }
                return;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                curs = Point.Empty;
            }
        }

        private void checkBoxWf_CheckedChanged(object sender, EventArgs e) {
            panel1.Refresh();
        }

        ASel asel = null;

        class ASel {
            MotionObj toval;
            int sel;
            AxBone refa;
            T1 refrx = null, refry = null, refrz = null;

            public ASel(MotionObj toval, int sel, List<AxBone> alaxbone) {
                this.toval = toval;
                this.sel = sel;
                this.refa = alaxbone[sel];

                if (toval != null) {
                    for (int t = 0; t < toval.t1Static.Count; t++) {
                        if (toval.t1Static[t].c00 == sel) {
                            switch (toval.t1Static[t].c02) {
                                case 3:
                                    refrx = toval.t1Static[t];
                                    break;
                                case 4:
                                    refry = toval.t1Static[t];
                                    break;
                                case 5:
                                    refrz = toval.t1Static[t];
                                    break;
                            }
                        }
                    }
                }
            }

            public float x2 {
                get {
                    if (refrx != null) {
                        return refrx.c04;
                    }
                    else {
                        return refa.x2;
                    }
                }
                set {
                    if (refrx != null) {
                        refrx.c04 = value;
                    }
                    else {
                        refa.x2 = value;
                    }
                }
            }
            public float y2 {
                get {
                    if (refry != null) {
                        return refry.c04;
                    }
                    else {
                        return refa.y2;
                    }
                }
                set {
                    if (refry != null) {
                        refry.c04 = value;
                    }
                    else {
                        refa.y2 = value;
                    }
                }
            }
            public float z2 {
                get {
                    if (refrz != null) {
                        return refrz.c04;
                    }
                    else {
                        return refa.z2;
                    }
                }
                set {
                    if (refrz != null) {
                        refrz.c04 = value;
                    }
                    else {
                        refa.z2 = value;
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            ListViewItem lvi = listView1.FocusedItem;
            if (lvi == null) {
                asel = null;
            }
            else {
                asel = new ASel(toval, lvi.Index, alaxbone);

                if (checkBoxShowBones.Checked || checkBox3ax.Checked || checkBoxGyro.Checked) {
                    calc骨vb();
                    panel1.Invalidate();
                }
            }
        }

        private void buttonLoadMset_Click(object sender, EventArgs e) {

        }

        private void buttonLoadMset_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void buttonLoadMset_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs == null)
                return;
            foreach (string f in fs) {
                if (string.Compare(Path.GetExtension(f), ".mset") == 0) {
                    //loadMset(f);
                    MessageBox.Show(f);
                    break;
                }
            }
        }

        void SetToval(MotionObj toval) {
            this.toval = toval;

            lTickRange.Text = String.Format("min={0} max={1}", toval.mintick, toval.maxtick);

            listView1.Items.Clear();
            for (int t = 0; t < alaxbone.Count; t++) {
                int test = 0;
                for (int a1 = 0; a1 < toval.t1Static.Count; a1++) {
                    if (toval.t1Static[a1].c00 == t) {
                        switch (toval.t1Static[a1].c02 & 15) {
                            case 0:
                                test |= 0x001;
                                break;
                            case 1:
                                test |= 0x002;
                                break;
                            case 2:
                                test |= 0x004;
                                break;
                            case 3:
                                test |= 0x010;
                                break;
                            case 4:
                                test |= 0x020;
                                break;
                            case 5:
                                test |= 0x040;
                                break;
                            case 6:
                                test |= 0x100;
                                break;
                            case 7:
                                test |= 0x200;
                                break;
                            case 8:
                                test |= 0x400;
                                break;
#if HIDE
                            default:
                                break;
#else
                            default: Debug.Assert(false, "alt1.c02 → " + toval.alt1[a1].c02 + "?"); break;
#endif
                        }
                    }
                }
                for (int a2 = 0; a2 < toval.t2Anim.Count; a2++) {
                    if (toval.t2Anim[a2].c00 == t) {
                        switch (toval.t2Anim[a2].c02 & 15) {
                            case 0:
                                test |= 0x001000;
                                break;
                            case 1:
                                test |= 0x002000;
                                break;
                            case 2:
                                test |= 0x004000;
                                break;
                            case 3:
                                test |= 0x010000;
                                break;
                            case 4:
                                test |= 0x020000;
                                break;
                            case 5:
                                test |= 0x040000;
                                break;
                            case 6:
                                test |= 0x100000;
                                break;
                            case 7:
                                test |= 0x200000;
                                break;
                            case 8:
                                test |= 0x400000;
                                break;
#if HIDE
                            default:
                                break;
#else
                            default: Debug.Assert(false, "alt2.c02 → " + toval.alt2[a2].c02 + "?"); break;
#endif
                        }
                    }
                }

                string hint = string.Concat(
                    ((test & 0x001) != 0) ? "x" : " ",
                    ((test & 0x002) != 0) ? "y" : " ",
                    ((test & 0x004) != 0) ? "z" : " ",
                    ((test & 0x010) != 0) ? "X" : " ",
                    ((test & 0x020) != 0) ? "Y" : " ",
                    ((test & 0x040) != 0) ? "Z" : " ",
                    ((test & 0x100) != 0) ? "x" : " ",
                    ((test & 0x200) != 0) ? "y" : " ",
                    ((test & 0x400) != 0) ? "z" : " ",
                    "|",
                    ((test & 0x001000) != 0) ? "x" : " ",
                    ((test & 0x002000) != 0) ? "y" : " ",
                    ((test & 0x004000) != 0) ? "z" : " ",
                    ((test & 0x010000) != 0) ? "X" : " ",
                    ((test & 0x020000) != 0) ? "Y" : " ",
                    ((test & 0x040000) != 0) ? "Z" : " ",
                    ((test & 0x100000) != 0) ? "x" : " ",
                    ((test & 0x200000) != 0) ? "y" : " ",
                    ((test & 0x400000) != 0) ? "z" : " ",
                    ""
                    );
                ListViewItem lvi = listView1.Items.Add(FmtAxBone.Format(alaxbone[t], hint));
                lvi.Tag = alaxbone[t];
            }

            listView2.Items.Clear();
            for (int t = 0; t < toval.t5Bone.Count; t++) {
                int test = 0;
                for (int a2 = 0; a2 < toval.t2x.Count; a2++) {
                    if (toval.t2x[a2].c00 == t) {
                        switch (toval.t2x[a2].c02 & 15) {
                            case 0:
                                test |= 0x001000;
                                break;
                            case 1:
                                test |= 0x002000;
                                break;
                            case 2:
                                test |= 0x004000;
                                break;
                            case 3:
                                test |= 0x010000;
                                break;
                            case 4:
                                test |= 0x020000;
                                break;
                            case 5:
                                test |= 0x040000;
                                break;
                            case 6:
                                test |= 0x100000;
                                break;
                            case 7:
                                test |= 0x200000;
                                break;
                            case 8:
                                test |= 0x400000;
                                break;
                        }
                    }
                }
                string hint = string.Concat(
                    "---------|",
                    ((test & 0x001000) != 0) ? "X" : " ",
                    ((test & 0x002000) != 0) ? "Y" : " ",
                    ((test & 0x004000) != 0) ? "Z" : " ",
                    ((test & 0x010000) != 0) ? "x" : " ",
                    ((test & 0x020000) != 0) ? "y" : " ",
                    ((test & 0x040000) != 0) ? "z" : " ",
                    ((test & 0x100000) != 0) ? "X" : " ",
                    ((test & 0x200000) != 0) ? "Y" : " ",
                    ((test & 0x400000) != 0) ? "Z" : " ",
                    ""
                    );

                ListViewItem lvi = listView2.Items.Add(FmtAxBone.Format(toval.t5Bone[t], hint));
                lvi.Tag = toval.t5Bone[t];
            }

            listViewIKInf.Items.Clear();
            for (int t = 0; t < toval.t3IKC.Count; t++) {
                var one = toval.t3IKC[t];
                ListViewItem lvi = listViewIKInf.Items.Add($"{t,3}: {one.c00:X2} {one.c01:X2} {one.c02,3} {one.c04,3} {one.c06,3} {one.c08,3} {one.c0a,3}");
            }

            listBoxOrder.Items.Clear();
            for (int t = 0; t < toval.t4Joint.Count; t++) {
                listBoxOrder.Items.Add(string.Format("{0,3}  {1:x2}", toval.t4Joint[t].c00, toval.t4Joint[t].c02));
            }

            {
                treeViewVars.Nodes.Clear();
                for (int c = 0; c < 2; c++) {
                    int off = (c == 0) ? 0 : Cnt1;
                    foreach (T2 t2 in (c == 0) ? toval.t2Anim : toval.t2x) {
                        string k;
                        switch (t2.c02) {
                            case 0:
                                k = "Sx";
                                break;
                            case 1:
                                k = "Sy";
                                break;
                            case 2:
                                k = "Sz";
                                break;
                            case 3:
                                k = "Rx";
                                break;
                            case 4:
                                k = "Ry";
                                break;
                            case 5:
                                k = "Rz";
                                break;
                            case 6:
                                k = "Tx";
                                break;
                            case 7:
                                k = "Ty";
                                break;
                            case 8:
                                k = "Tz";
                                break;
                            default:
                                k = t2.c02.ToString();
                                break;
                        }
                        TreeNode tn2 = treeViewVars.Nodes.Add(string.Format("#{0:000}.{1}", off + t2.c00, k));
                        tn2.Tag = t2;
                        foreach (T9 t9 in t2.t9TimeLines) {
                            TreeNode tn9 = tn2.Nodes.Add(string.Format("{0}|{1,7:0.00}|{2,7:0.00}|{3,6:0.00}|{4,6:0.00}"
                                , "NLHZ"[t9.Interpolation]
                                , t9.KeyFrame
                                , t9.Value
                                , t9.EaseIn
                                , t9.EaseOut
                            ));
                            tn9.Tag = t9;
                        }
                    }
                }
            }
            {
                treeViewFixes.Nodes.Clear();
                foreach (T1 t1 in toval.t1Static) {
                    string k;
                    switch (t1.c02) {
                        case 0:
                            k = "Sx";
                            break;
                        case 1:
                            k = "Sy";
                            break;
                        case 2:
                            k = "Sz";
                            break;
                        case 3:
                            k = "Rx";
                            break;
                        case 4:
                            k = "Ry";
                            break;
                        case 5:
                            k = "Rz";
                            break;
                        case 6:
                            k = "Tx";
                            break;
                        case 7:
                            k = "Ty";
                            break;
                        case 8:
                            k = "Tz";
                            break;
                        default:
                            k = t1.c02.ToString();
                            break;
                    }
                    TreeNode tn1 = treeViewFixes.Nodes.Add(string.Format("#{0:000}.{1} ={2,8:0.00}", t1.c00, k, t1.c04));
                }
            }

            {
                // t6
                var list = t6List;
                var items = toval.t6;
                list.Items.Clear();
                for (var x = 0; x < items.Count; x++) {
                    var item = items[x];
                    list.Items.Add($"{x,3}: {item}");
                }
            }

            {
                // t7
                var list = t7List;
                var items = toval.t7;
                list.Items.Clear();
                for (var x = 0; x < items.Count; x++) {
                    var item = items[x];
                    list.Items.Add(item.ToString());
                }
            }

            {
                // t8
                var list = t8List;
                var items = toval.t8;
                list.Items.Clear();
                for (var x = 0; x < items.Count; x++) {
                    var item = items[x];
                    list.Items.Add(item.ToString());
                }
                list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            prepare();

            numericUpDown1.Value = 0;
        }

        public int Cnt1 { get { return alaxbone.Count; } }

        MotionObj toval = null;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            if (toval == null)
                return;

            prepare();
            calcvbib();
            calc骨vb();
            calcResikvb();
            panel1.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            numericUpDown1.Value = (numericUpDown1.Value + numericUpDown2.Value) % numericUpDown1.Maximum;
        }

        private void checkBoxAutoStep_CheckedChanged(object sender, EventArgs e) {
            timer1.Enabled = checkBoxAutoStep.Checked;
        }

        private void checkBoxSpv_CheckedChanged(object sender, EventArgs e) {
            prepare();

            if (device != null) {
                calcvbib();
                calc骨vb();
                calcResikvb();
            }

            panel1.Invalidate();
        }

        private void checkBoxShowVone_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e) {
            ListViewItem lvi = listView2.FocusedItem;
            if (lvi == null) {

            }
            else {
                if (checkBoxShowBones.Checked || checkBox3ax.Checked || checkBoxGyro.Checked) {
                    calc骨vb();
                    panel1.Invalidate();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void radioButtonSelWire_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void numericUpDownSelt_ValueChanged(object sender, EventArgs e) {
            if (TheTimf != null) {
                byte[] al = new byte[] {
                    (byte)((int)numericUpDownTex0.Value),
                    (byte)((int)numericUpDownTex1.Value),
                    (byte)((int)numericUpDownTex2.Value),
                };
                pictureBox1.Image = TheTimf.GetTex2((int)numericUpDownSelt.Value, al);

                Bitmap p1 = (Bitmap)pictureBox1.Image;
                if (p1 != null) {
                    Bitmap pic = new Bitmap(160, 160, PixelFormat.Format24bppRgb);
                    Color[] pal = p1.Palette.Entries;
                    using (Graphics cv = Graphics.FromImage(pic)) {
                        cv.Clear(Color.Black);
                        for (int c = 0; c < pal.Length; c++) {
                            Rectangle rc = new Rectangle((c % 16) * 10, (c / 16) * 10, 10, 10);
                            cv.FillRectangle(new SolidBrush(pal[c]), rc);
                        }
                    }
                    pbpal.Image = pic;
                }
                else {
                    pbpal.Image = null;
                }
            }
        }

        private void checkBoxUseik_CheckedChanged(object sender, EventArgs e) {
            checkBoxSpv_CheckedChanged(null, null);
        }

        private void numericUpDownKlv_ValueChanged(object sender, EventArgs e) {
            checkBoxSpv_CheckedChanged(null, null);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            Bitmap pic = (Bitmap)pictureBox1.Image;
            if (pic != null) {
                Color c = pic.GetPixel(e.X, e.Y);
                labelTexptclr.Text = string.Format("({0}, {1})→({2:X2},{3:X2},{4:X2},{5:X2}) {6}"
                    , e.X, e.Y, c.R, c.G, c.B, c.A, pic.PixelFormat
                    );
            }
        }

        int selik = -1;

        private void listViewIKInf_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (int sel in listViewIKInf.SelectedIndices) {
                selik = sel;
                calcResikvb();
                panel1.Invalidate();
                break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            Settings.Default.offset = offset;
            Settings.Default.quat = quat;
            Settings.Default.zval = zval;
            if (radioButtonSelWire.Checked)
                Settings.Default.Sel_wire = true;
            if (radioButtonSelSolid.Checked)
                Settings.Default.Sel_solid = true;
            if (radioButtonSelTex.Checked)
                Settings.Default.Sel_tex = true;

            Settings.Default.Save();
            MessageBox.Show("Save successful");
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            zval = 1.0f;
            offset = Vector3.Zero;
            quat = Quaternion.Identity;

            reshape();
            panel1.Invalidate();
        }

        private void treeViewVars_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            TreeNode tn = treeViewVars.SelectedNode;
            if (tn != null) {
                if (tn.Tag is T2) {
                    T2 t2 = (T2)tn.Tag;
                    SortedDictionary<int, string> dict = new SortedDictionary<int, string>();
                    float maxval = 0;
                    foreach (T9 t9 in t2.t9TimeLines) {
                        dict[(int)t9.KeyFrame] = string.Format("{0},{1}", t9.KeyFrame, t9.Value);
                        maxval = Math.Max(maxval, t9.KeyFrame);
                    }
#if false
                    StringBuilder s = new StringBuilder();
                    for (float x = 0; x <= maxval; x++) {
                        string text;
                        if (dict.TryGetValue((int)x, out text)) {
                            s.Append(text);
                        }
                        s.AppendLine();
                    }
                    Clipboard.SetText(s.ToString());
#endif
                }
            }
        }

        private void numericUpDownTex0_ValueChanged(object sender, EventArgs e) {
            numericUpDownTex0.Enabled = (1 <= TheTimf.bitmapList.Count);
            numericUpDownTex1.Enabled = (2 <= TheTimf.bitmapList.Count);
            numericUpDownTex2.Enabled = (3 <= TheTimf.bitmapList.Count);
            Usetex();
            numericUpDownSelt_ValueChanged(null, null);
            panel1.Invalidate();
        }

        private void listBoxOrder_SelectedIndexChanged(object sender, EventArgs e) {
            string s = listBoxOrder.SelectedItem as string;
            if (s != null) {
                int sel = int.Parse(s.Trim().Split(' ')[0]);
                int cnt1 = listView1.Items.Count;
                int cnt2 = listView2.Items.Count;
                if (sel < cnt1) { // primary
                    {
                        ListViewItem lvi = listView1.Items[sel];
                        lvi.Focused = true;
                        lvi.Selected = true;
                    }
                    foreach (ListViewItem lvi in listView2.SelectedItems) {
                        lvi.Selected = false;
                    }
                }
                else if (sel - cnt1 < cnt2) {
                    {
                        ListViewItem lvi = listView2.Items[sel - cnt1];
                        lvi.Focused = true;
                        lvi.Selected = true;
                    }
                }
            }
        }

        private void numericUpDownSeltc_ValueChanged(object sender, EventArgs e) {
            timi = Math.Max(0, Math.Min(timc.Length - 1, (int)numericUpDownSeltc.Value));
            numericUpDownSelt_ValueChanged(null, null);
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Middle)) {
                String fp = Path.Combine(Application.StartupPath, "Save.png");
                pictureBox1.Image.Save(fp, ImageFormat.Png);
                Process.Start("explorer.exe", " /select,\"" + fp + "\"");
            }
        }

        private void buttonSelAnim_Click(object sender, EventArgs e) {
            contextMenuStripAnib.Show(buttonSelAnim, Point.Empty);
        }

        private void panel1_SizeChanged(object sender, EventArgs e) {
            device.Reset(PP);
            devReset();
            reshape();
        }

        private void useBakery_CheckedChanged(object sender, EventArgs e) {
            checkBoxSpv_CheckedChanged(null, null);
        }

        private void renderModel_CheckedChanged(object sender, EventArgs e) {
            checkBoxSpv_CheckedChanged(null, null);
        }

        private void buttonGetLocs_Click(object sender, EventArgs e) {
            StringWriter wr = new StringWriter();
            for (int x = 0; x < matrices.Length; x++) {
                matrices[x].Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation);
                wr.WriteLine("{0:000} [{1}, {2}, {3}] [{4}, {5}, {6}, {7}]"
                    , x
                    , translation.X, translation.Y, translation.Z
                    , rotation.W, rotation.X, rotation.Y, rotation.Z
                    );
            }
            textBoxLocs.Text = wr.ToString();
        }
    }
    class UtilApplyMsetoesToAxBone {
        public static List<AxBone> apply(List<AxBone> xxx, MotionObj toval, float tick, float maxtick, bool useDefval, bool useMotion) {
            List<AxBone> al = new List<AxBone>();
            foreach (AxBone o in xxx)
                al.Add(o.Clone());
            if (toval != null) {
                foreach (AxBone o in toval.t5Bone)
                    al.Add(o.Clone());

                if (useDefval) {
                    foreach (T1 t1 in toval.t1Static) {
                        int pos = t1.c00;
                        int ax = t1.c02;
                        float yv = t1.c04;
                        AxBone v = al[pos];
                        switch (ax) {
                            case 0:
                                v.x1 = yv;
                                break;
                            case 1:
                                v.y1 = yv;
                                break;
                            case 2:
                                v.z1 = yv;
                                break;
                            case 3:
                                v.x2 = yv;
                                break;
                            case 4:
                                v.y2 = yv;
                                break;
                            case 5:
                                v.z2 = yv;
                                break;
                            case 6:
                                v.x3 = yv;
                                break;
                            case 7:
                                v.y3 = yv;
                                break;
                            case 8:
                                v.z3 = yv;
                                break;
                        }
                        al[pos] = v;
                    }
                }
                if (useMotion) {
                    foreach (T2 t2 in toval.t2x) {
                        int pos = t2.c00 + xxx.Count;
                        int ax = t2.c02;
                        float yv = YVal.calc2(tick, t2.t9TimeLines);
                        AxBone v = al[pos];
                        switch (ax) {
                            case 3:
                                v.x2 = yv;
                                break;
                            case 4:
                                v.y2 = yv;
                                break;
                            case 5:
                                v.z2 = yv;
                                break;
                            case 6:
                                v.x3 = yv;
                                break;
                            case 7:
                                v.y3 = yv;
                                break;
                            case 8:
                                v.z3 = yv;
                                break;
                        }
                        al[pos] = v;
                    }
                    foreach (T2 t2 in toval.t2Anim) {
                        int pos = t2.c00;
                        int ax = t2.c02;

                        int tpos = 8 * pos + (ax & 15) - 3;
                        int xpos = tpos / 8;
                        AxBone v = al[xpos];

                        float yv = YVal.calc2(tick, t2.t9TimeLines);

                        if (false) { }
                        else if ((tpos % 8) == 0)
                            v.x2 = yv;
                        else if ((tpos % 8) == 1)
                            v.y2 = yv;
                        else if ((tpos % 8) == 2)
                            v.z2 = yv;
                        else if ((tpos % 8) == 3)
                            v.x3 = yv;
                        else if ((tpos % 8) == 4)
                            v.y3 = yv;
                        else if ((tpos % 8) == 5)
                            v.z3 = yv;

                        al[xpos] = v;
                    }
                }
            }
            return al;
        }
    }

    class YVal {
        public static float calc(float tick, List<T9> alt9) {
            float fmax = alt9[alt9.Count - 1].KeyFrame;
            float fpos = tick % fmax;

            return calc2(fpos, alt9);
        }

        public static float calc2(float fpos, List<T9> alt9) {
            T9 t9a = null;
            for (int x9 = 0; x9 < alt9.Count; x9++) {
                T9 t9b = alt9[x9];
                if (t9a != null) {
                    if (fpos <= t9b.KeyFrame) {
                        float yv = Composite2.calc(
                            fpos,
                            t9a.KeyFrame, t9a.Value, t9a.EaseOut,
                            t9b.KeyFrame, t9b.Value, t9b.EaseIn
                            );

                        return yv;
                    }
                }
                t9a = t9b;
            }
            if (t9a != null)
                return t9a.Value;
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

    class Composite1 {
        public static float calc(float tick, float v0x, float v0y, float v0w, float v1x, float v1y, float v1z) {
            float v001284c4 = 1;
            float v001284e0 = 3;
            float f20 = tick;

            float f0 = v1x;
            float f17 = v1y;
            float f16 = v1x;
            float f13 = v0x;
            float f14 = v0y;
            float f15 = v0w;
            float f18 = v1z;

            float f12 = f20;
            float f7;
            float f5;
            float f3;
            float f2;
            float f4;
            float f1;
            float f6;
            float acc;


            f16 = f16 - f13;
            f12 = f12 - f13;
            f7 = v001284c4;
            //Debug.Assert(Math.Abs(f16) >= 1E-5);
            f16 = f17 / f16;
            f5 = f12 * f12;
            f3 = v001284e0;
            f2 = f5 * f12;
            f3 = f5 * f3;
            f1 = f16 * f16;
            f5 = f5 * f16;
            f2 = f2 * f1;
            f3 = f3 * f1;
            f6 = f5 + f5;
            f4 = f2 + f2;
            f5 = f2 - f5;
            f4 = f4 * f16;
            f2 = f2 - f6;
            f1 = f4 - f3;
            f2 = f2 + f12;
            f1 = f1 + f7;
            f3 = f3 - f4;
            acc = f14 * f1;
            acc = acc + f17 * f3;
            acc = acc + f15 * f2;
            f0 = acc + f18 * f5;

            return f0;
        }
    }

    class InvkParser {
        class Tbl {
            public int IKt = -1, IKj0 = -1, IKj1 = -1;

            internal void Reset() {
                IKt = IKj0 = IKj1 = -1;
            }
        }

        public InvkParser(int cnt3, MotionObj toval) {
            invkList = new InvkEnt[cnt3];
            for (int x = 0; x < invkList.Length; x++) invkList[x] = new InvkEnt();
            int i3 = 0;
            int clxz = -1;
            Tbl t1 = new Tbl(); // cnt1
            Tbl t2 = new Tbl(); // cnt2
            foreach (T4 v4 in toval.t4Joint) {
                int joint = v4.c00;
                ushort jf = v4.c02;

                Tbl t = (joint < cnt3) ? t1 : t2;

                T3 v3;
                while (i3 < toval.t3IKC.Count && (v3 = toval.t3IKC[i3]).c02 == joint) {
                    switch (v3.c00) {
                        case 0:
                            invkList[joint].Source = v3.c04;
                            invkList[joint].CopyPos = true;
                            break;
                        case 1:
                            invkList[joint].Source = v3.c04;
                            invkList[joint].CopyScale = true;
                            break;
                        case 2:
                            invkList[joint].Source = v3.c04;
                            invkList[joint].CopyRot = true;
                            break;
                        case 3:
                            invkList[joint].ikTarget = t.IKt = v3.c04;
                            break;
                    }

                    i3++;
                }

                if (0 != (jf & 0x02)) {
                    invkList[joint].IK1 = true; t.IKj0 = joint;
                }
                if (0 != (jf & 0x01)) {
                    invkList[joint].IK1a = true;
                }
                if (0 != (jf & 0x20)) {
                    invkList[joint].IK2 = true; t.IKj1 = joint;
                }
                if (0 != (jf & 0x04)) {
                    clxz = 2;
                    invkList[joint].IK3 = true;

                    invkList[joint].ikTarget = t.IKt;
                    invkList[joint].ikJoint0 = t.IKj0;
                    invkList[joint].ikJoint1 = t.IKj1;
                    invkList[joint].ikJoint2 = joint;
                    invkList[joint].RunIK3 = t.IKt >= 0 && t.IKj0 >= 0 && t.IKj1 >= 0;

                    t.Reset();
                }
                if (0 != (jf & 0x08)) {
                    invkList[joint].IK3a = true;
                }

                if (clxz == 1) invkList[joint].ClearXYZ = true;
                if (clxz == 0) invkList[joint].ClearXYZ = true;
                clxz--;

                alInOrd.Add(invkList[joint]);
            }
        }

        internal InvkEnt[] invkList;
        internal List<InvkEnt> alInOrd = new List<InvkEnt>();
    }
    class InvkEnt {
        public bool CopyPos;
        public bool CopyScale;
        public bool CopyRot;
        public bool ClearXYZ = false;
        public bool RunIK3 = false;
        public int ikTarget = -1;
        public int ikJoint0 = -1;
        public int ikJoint1 = -1;
        public int ikJoint2 = -1;

        public bool IK1 = false;
        public bool IK1a = false;
        public bool IK2 = false;
        public bool IK3 = false;
        public bool IK3a = false;

        public int Source = -1;
    }

    class BasixAxBoneUtil4 {
        public static Matrix[] composeMatrices(AxBone[] boneList, MotionObj motion, float tick, InvkParser invk, int cnt1) {
            Matrix[] matrices;
            if (motion == null) {
                matrices = new Matrix[boneList.Length];
                for (int boneIdx = 0; boneIdx < boneList.Length; boneIdx++) {
                    AxBone ax = boneList[boneIdx];
                    Debug.Assert(ax.parent == -1 || (ax.parent != -1 && ax.parent < boneIdx));
                    RTv localTransform = new RTv(ax.x1, ax.y1, ax.z1, ax.x2, ax.y2, ax.z2, ax.x3, ax.y3, ax.z3);
                    var localMatrix = localTransform.Make();
                    matrices[boneIdx] = localMatrix;
                    if (ax.parent >= 0) {
                        matrices[boneIdx] *= matrices[ax.parent];
                    }
                }
            }
            else {
                matrices = new Matrix[boneList.Length];
                for (int ei = 0; ei < motion.t4Joint.Count; ei++) {
                    int boneIdx = motion.t4Joint[ei].c00;
                    AxBone ax = boneList[boneIdx];
                    float x1 = ax.x1;
                    float y1 = ax.y1;
                    float z1 = ax.z1;
                    float x2 = ax.x2;
                    float y2 = ax.y2;
                    float z2 = ax.z2;
                    float x3 = ax.x3;
                    float y3 = ax.y3;
                    float z3 = ax.z3;

                    InvkEnt ie = (invk != null) ? invk.invkList[boneIdx] : new InvkEnt();

                    if (ie.ClearXYZ) {
                        x2 = y2 = z2 = 0;
                    }

                    RTv localTransform = new RTv(x1, y1, z1, x2, y2, z2, x3, y3, z3);
                    var localMatrix = localTransform.Make();

                    matrices[boneIdx] = localMatrix;

                    int parent = ax.parent;
                    if (parent >= 0) {
                        matrices[boneIdx] *= matrices[parent];
                    }

                    if (false) { }
                    else if (ie.RunIK3) {
                        for (int z = 0; z < 3; z++) {
                            Vector3 targetPoint = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikTarget]);
                            {   // 1
                                Matrix baseMatrix = matrices[ie.ikJoint0];
                                Vector3 nx = Vector3.TransformNormal(new Vector3(1, 0, 0), baseMatrix);
                                Vector3 ny = Vector3.TransformNormal(new Vector3(0, 1, 0), baseMatrix);
                                Vector3 nz = Vector3.TransformNormal(new Vector3(0, 0, 1), baseMatrix);

                                Vector3 p1 = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikJoint0]);
                                Vector3 p2 = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikJoint1]);
                                Vector3 p3 = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikJoint2]);
                                Vector2 Tp1 = Vector2.Zero;
                                Vector2 Tp3 = IKUt.Dot2(p3 - p1, nx, nz);
                                Vector2 Ttpt = IKUt.Dot2(targetPoint - p1, nx, nz);

                                float rad;
                                rad = IKUt.Limit(IKUt.cross(Tp1, Tp3, Ttpt), boneList[ie.ikJoint0].w1);
                                Quaternion rq = Quaternion.RotationAxis(ny, rad);
                                var rm = Matrix.RotationQuaternion(rq);

                                matrices[ie.ikJoint1] *= Matrix.Translation(IKUt.rotate(p1, p2, rq));
                                matrices[ie.ikJoint2] *= Matrix.Translation(IKUt.rotate(p1, p3, rq));
                                matrices[ie.ikJoint0] *= (rm);
                                matrices[ie.ikJoint1] *= (rm);
                                matrices[ie.ikJoint2] *= (rm);
                            }
                            {   // 0
                                Matrix Mv = matrices[ie.ikJoint1];
                                Vector3 vx = Vector3.TransformNormal(new Vector3(1, 0, 0), Mv);
                                Vector3 vz = Vector3.TransformNormal(new Vector3(0, 0, 1), Mv);
                                Vector3 vy = Vector3.Cross(vx, vz);
                                Vector3 p2 = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikJoint1]);
                                Vector3 p3 = Vector3.TransformCoordinate(Vector3.Zero, matrices[ie.ikJoint2]);
                                Vector2 Tp2 = Vector2.Zero;
                                Vector2 Tp3 = IKUt.Dot2(p3 - p2, vx, vz);
                                Vector2 Ttpt = IKUt.Dot2(targetPoint - p2, vx, vz);

                                float rad;
                                rad = IKUt.Limit(IKUt.cross(Tp2, Tp3, Ttpt), boneList[ie.ikJoint1].w1);
                                Quaternion rq = Quaternion.RotationAxis(vy, rad);
                                var rm = Matrix.RotationQuaternion(rq);

                                matrices[ie.ikJoint2] *= Matrix.Translation(IKUt.rotate(p2, p3, rq));
                                matrices[ie.ikJoint1] *= rm;
                                matrices[ie.ikJoint2] *= rm;
                            }
                        }
                        //TODO: matrices[boneIdx].tv = matrices[ie.ikTarget].tv;
                    }
                    else if (ie.CopyPos) {
                        var dest = matrices[boneIdx];
                        var source = matrices[ie.Source];

                        dest.M41 = source.M41;
                        dest.M42 = source.M42;
                        dest.M43 = source.M43;
                        dest.M44 = source.M44;

                        matrices[boneIdx] = dest;
                    }
                    else if (ie.CopyRot) {
                        var dest = matrices[boneIdx];
                        var source = matrices[ie.Source];

                        dest.M11 = source.M11;
                        dest.M12 = source.M12;
                        dest.M13 = source.M13;
                        dest.M14 = source.M14;

                        dest.M21 = source.M21;
                        dest.M22 = source.M22;
                        dest.M23 = source.M23;
                        dest.M24 = source.M24;

                        dest.M31 = source.M31;
                        dest.M32 = source.M32;
                        dest.M33 = source.M33;
                        dest.M34 = source.M34;

                        matrices[boneIdx] = dest;
                    }
                }
            }
            return matrices;
        }

        class IKUt {
            public static float cross(Vector2 v0, Vector2 v1, Vector2 v2) {
                v1 -= v0;
                v1.Normalize();
                Debug.Assert(!float.IsNaN(v1.X) && !float.IsNaN(v1.Y), "v1 is NaN");
                v2 -= v0;
                v2.Normalize();
                Debug.Assert(!float.IsNaN(v2.X) && !float.IsNaN(v2.Y), "v2 is NaN");
                float dot = Vector2.Dot(v1, v2);
                Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot));
                Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                float ccw = Vector3.Cross(new Vector3(v1.X, v1.Y, 0), new Vector3(v2.X, v2.Y, 0)).Z;
                return rad * ((ccw < 0) ? -1 : +1);
            }
            public static Vector3 rotate(Vector3 v0, Vector3 v1, Quaternion quat) {
                return Vector3.TransformCoordinate(v1 - v0, SlimDX.Matrix.RotationQuaternion(quat));
            }
            public static Vector2 Dot2(Vector3 vin, Vector3 vx, Vector3 vy) {
                return new Vector2(Vector3.Dot(vin, vx), Vector3.Dot(vin, vy));
            }
            public static float Limit(float rad, float w) {
                if (w == 0)
                    w = 3.14159f;
                else
                    w = w / 180.0f * 3.14159f;
                return Math.Max(-w, Math.Min(+w, rad));
            }
        }
    }

    class BasixAxBoneUtil3 {
        static IKDIAG ikd = new IKDIAG();

        class IKStat {
            public int ik0p = -1, ik2p = -1, ik3p = -1;
            public List<int> alki = new List<int>();
        }

        public static void calc(List<RTv> matrices, AxBone[] alab, MotionObj toval, float tick, bool applyik, int cnt1, out StringBuilder s) {
            s = new StringBuilder();
            if (toval == null) {
                matrices.Clear();
                for (int bi = 0; bi < alab.Length; bi++) {
                    AxBone ax = alab[bi];
                    Debug.Assert(ax.parent == -1 || (ax.parent != -1 && ax.parent < bi));
                    RTv A = new RTv(ax.x1, ax.y1, ax.z1, ax.x2, ax.y2, ax.z2, ax.x3, ax.y3, ax.z3);
                    if (ax.parent >= 0)
                        A.Multiply(matrices[ax.parent]);
                    matrices.Add(A);
                }
                return;
            }
            else {
                int i3 = 0;
                matrices.Clear();
                for (int t = 0; t < alab.Length; t++)
                    matrices.Add(RTv.Empty);
                IKStat[] aliks = new IKStat[] { new IKStat(), new IKStat() };
                for (int ei = 0; ei < toval.t4Joint.Count; ei++) {
                    int bi = toval.t4Joint[ei].c00;
                    AxBone ax = alab[bi];
                    float x1 = ax.x1;
                    float y1 = ax.y1;
                    float z1 = ax.z1;
                    float x2 = ax.x2;
                    float y2 = ax.y2;
                    float z2 = ax.z2;
                    float x3 = ax.x3;
                    float y3 = ax.y3;
                    float z3 = ax.z3;

                    IKStat iks = aliks[(bi < cnt1) ? 0 : 1];
                    List<int> alki = iks.alki;
                    iks.ik0p = -1;
                    iks.ik2p = -1;

                    while (i3 < toval.t3IKC.Count && toval.t3IKC[i3].c02 == bi) {
                        switch (toval.t3IKC[i3].c00) {
                            case 2: {
                                    iks.ik2p = toval.t3IKC[i3].c04;
                                    break;
                                }
                            case 3: { // 1st
                                    iks.ik3p = toval.t3IKC[i3].c04;
                                    alki.Clear();
                                    break;
                                }
                            case 0: { // 2nd
                                    iks.ik0p = toval.t3IKC[i3].c04;
                                    break;
                                }
                        }
                        i3++;
                    }
                    if (iks.ik3p != -1) {
                        alki.Add(bi);
                    }

                    RTv A = new RTv(x1, y1, z1, x2, y2, z2, x3, y3, z3);

                    int parent = ax.parent;
                    if (parent >= 0) {
                        A.Multiply(matrices[parent]);
                    }

                    matrices[bi] = A;

                    if (applyik) {
                        if (iks.ik3p != -1 && iks.ik0p != -1) {
                            if (alki.Count == 4) {
                                for (int z = 0; z < 1; z++) {
                                    Vector3 tpt = matrices[iks.ik3p].tv;
                                    ikd.StartGrp();
                                    {   // 2
                                        Matrix Mv = matrices[alki[0]].Make();
                                        Vector3 vx = Vector3.TransformNormal(new Vector3(1, 0, 0), Mv);
                                        Vector3 vz = Vector3.TransformNormal(new Vector3(0, 0, 1), Mv);
                                        Vector3 vy = Vector3.Cross(vx, vz);
                                        Vector3 p0 = matrices[alki[0]].tv;
                                        Vector3 p1 = matrices[alki[1]].tv;
                                        Vector3 p2 = matrices[alki[2]].tv;
                                        Vector3 p3 = matrices[alki[3]].tv;
                                        Vector2 Tp0 = Vector2.Zero;
                                        Vector2 Tp3 = IKUt.Dot2(p3 - p0, vx, vz);
                                        Vector2 Ttpt = IKUt.Dot2(tpt - p0, vx, vz);

                                        float rad;
                                        rad = IKUt.Limit(IKUt.cross(Tp0, Tp3, Ttpt), alab[alki[0]].w1);
                                        Quaternion rq = Quaternion.RotationAxis(vy, rad);
                                        ikd.Add(matrices, alki, vx, vz, 0, tpt);
                                        matrices[alki[1]].tv = IKUt.rotate(p0, p1, rq);
                                        matrices[alki[2]].tv = IKUt.rotate(p0, p2, rq);
                                        matrices[alki[3]].tv = IKUt.rotate(p0, p3, rq);
                                        matrices[alki[0]].rq *= rq;
                                        matrices[alki[1]].rq *= rq;
                                        matrices[alki[2]].rq *= rq;
                                        matrices[alki[3]].rq *= rq;
                                        ikd.Add(matrices, alki, vx, vz, rad, tpt);
                                    }
                                    {   // 1
                                        Matrix Mv = matrices[alki[1]].Make();
                                        Vector3 vx = Vector3.TransformNormal(new Vector3(1, 0, 0), Mv);
                                        Vector3 vz = Vector3.TransformNormal(new Vector3(0, 0, 1), Mv);
                                        Vector3 vy = Vector3.Cross(vx, vz);
                                        Vector3 p1 = matrices[alki[1]].tv;
                                        Vector3 p2 = matrices[alki[2]].tv;
                                        Vector3 p3 = matrices[alki[3]].tv;
                                        Vector2 Tp1 = Vector2.Zero;
                                        Vector2 Tp3 = IKUt.Dot2(p3 - p1, vx, vz);
                                        Vector2 Ttpt = IKUt.Dot2(tpt - p1, vx, vz);

                                        float rad;
                                        rad = IKUt.Limit(IKUt.cross(Tp1, Tp3, Ttpt), alab[alki[1]].w1);
                                        Quaternion rq = Quaternion.RotationAxis(vy, rad);
                                        ikd.Add(matrices, alki, vx, vz, 0, tpt);
                                        matrices[alki[2]].tv = IKUt.rotate(p1, p2, rq);
                                        matrices[alki[3]].tv = IKUt.rotate(p1, p3, rq);
                                        matrices[alki[1]].rq *= rq;
                                        matrices[alki[2]].rq *= rq;
                                        matrices[alki[3]].rq *= rq;
                                        ikd.Add(matrices, alki, vx, vz, rad, tpt);
                                    }
                                    {   // 0
                                        Matrix Mv = matrices[alki[2]].Make();
                                        Vector3 vx = Vector3.TransformNormal(new Vector3(1, 0, 0), Mv);
                                        Vector3 vz = Vector3.TransformNormal(new Vector3(0, 0, 1), Mv);
                                        Vector3 vy = Vector3.Cross(vx, vz);
                                        Vector3 p2 = matrices[alki[2]].tv;
                                        Vector3 p3 = matrices[alki[3]].tv;
                                        Vector2 Tp2 = Vector2.Zero;
                                        Vector2 Tp3 = IKUt.Dot2(p3 - p2, vx, vz);
                                        Vector2 Ttpt = IKUt.Dot2(tpt - p2, vx, vz);

                                        float rad;
                                        rad = IKUt.Limit(IKUt.cross(Tp2, Tp3, Ttpt), alab[alki[2]].w1);
                                        Quaternion rq = Quaternion.RotationAxis(vy, rad);
                                        ikd.Add(matrices, alki, vx, vz, 0, tpt);
                                        matrices[alki[3]].tv = IKUt.rotate(p2, p3, rq);
                                        matrices[alki[2]].rq *= rq;
                                        matrices[alki[3]].rq *= rq;
                                        ikd.Add(matrices, alki, vx, vz, rad, tpt);
                                    }

                                    ikd.EndGrp();
                                }
                            }
                            {
                                s.Append("I-");
                                foreach (int refi in alki)
                                    s.Append("[" + refi + "]");
                                s.AppendLine();
                            }

                            //matrices[bi].rq = matrices[ik3p].rq;

                            iks.ik3p = -1;
                        }
                        else if (iks.ik0p != -1) {
                            matrices[bi] = matrices[iks.ik0p];
                            s.Append("A [" + bi + "] <- [" + iks.ik0p + "]");
                            s.AppendLine();
                        }
                        if (iks.ik2p != -1) {
                            matrices[bi].rq = matrices[iks.ik2p].rq;
                        }
                    }
                }
                ikd.Save();
            }
        }

        class IKDIAG {
            public XmlDocument xmlo = new XmlDocument();
            public XmlElement elroot = null;
            public XmlElement elg = null;

            public IKDIAG() {
#if IKD
                elroot = xmlo.CreateElement("IKDIAG");
                xmlo.AppendChild(elroot);
#endif
            }

            [Conditional("IKD")]
            public void StartGrp() {
                elg = xmlo.CreateElement("g");
                elroot.AppendChild(elg);
            }

            [Conditional("IKD")]
            public void EndGrp() {
                elg = null;
            }

            [Conditional("IKD")]
            public void Add(List<RTv> matrices, List<int> alki, Vector3 vx, Vector3 vz, float rad, Vector3 tpt) {
                XmlElement el = xmlo.CreateElement("i");
                elg.AppendChild(el);

                el.SetAttribute("vx", vx.X + "," + vx.Y + "," + vx.Z);
                el.SetAttribute("vz", vz.X + "," + vz.Y + "," + vz.Z);
                el.SetAttribute("rad", "" + rad);
                el.SetAttribute("tpt", tpt.X + "," + tpt.Y + "," + tpt.Z);
                el.SetAttribute("tpt2", Vector3.Dot(tpt, vx) + "," + Vector3.Dot(tpt, vz));
                foreach (int ki in alki) {
                    RTv o = matrices[ki];
                    XmlElement elo = xmlo.CreateElement("o");
                    el.AppendChild(elo);
                    elo.SetAttribute("tv", o.tv.X + "," + o.tv.Y + "," + o.tv.Z);
                    elo.SetAttribute("v2", Vector3.Dot(o.tv, vx) + "," + Vector3.Dot(o.tv, vz));
                }
            }

            [Conditional("IKD")]
            public void Save() {
                xmlo.Save(@"H:\Proj\khkh_xldM\MEMO\IKDIAG\IKDIAG.xml");
            }
        }

        class IKUt {
            public static float cross(Vector2 v0, Vector2 v1, Vector2 v2) {
                v1 -= v0;
                v1.Normalize();
                Debug.Assert(!float.IsNaN(v1.X) && !float.IsNaN(v1.Y), "v1 is NaN");
                v2 -= v0;
                v2.Normalize();
                Debug.Assert(!float.IsNaN(v2.X) && !float.IsNaN(v2.Y), "v2 is NaN");
                float dot = Vector2.Dot(v1, v2);
                Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot));
                Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                float ccw = Vector3.Cross(new Vector3(v1.X, v1.Y, 0), new Vector3(v2.X, v2.Y, 0)).Z;
                return rad * ((ccw < 0) ? -1 : +1);
            }
            public static Vector3 rotate(Vector3 v0, Vector3 v1, Quaternion quat) {
                return Vector3.TransformCoordinate(v1 - v0, SlimDX.Matrix.RotationQuaternion(quat)) + v0;
            }
            public static Vector2 Dot2(Vector3 vin, Vector3 vx, Vector3 vy) {
                return new Vector2(Vector3.Dot(vin, vx), Vector3.Dot(vin, vy));
            }
            public static float Limit(float rad, float w) {
                if (w == 0)
                    w = 3.14159f;
                else
                    w = w / 180.0f * 3.14159f;
                return Math.Max(-w, Math.Min(+w, rad));
            }
        }

        class Ut5 {
            public static Vector3 Vec3(AxBone axb) {
                return new Vector3(axb.x3, axb.y3, axb.z3);
            }
        }

        static void recalcIt(RTv T, Vector3 ptv, Quaternion rq, Vector3 otv) {
            Vector3 av = Vector3.TransformCoordinate(otv, Matrix.RotationQuaternion(rq));
            T.tv = av + ptv;
            T.rq *= (rq);
        }

        public static Matrix[] migrate(int[] alaxi, Matrix[] matrices) {
            Matrix[] localMatrices = new Matrix[alaxi.Length];

            for (int boneIdx = 0; boneIdx < alaxi.Length; boneIdx++) {
                int srcIdx = alaxi[boneIdx];
                localMatrices[boneIdx] = matrices[srcIdx];
            }
            return localMatrices;
        }
    }

#if false
    class CalcUtil {
        /// <summary>
        /// v0を基準にして，v1方向からv2方向に向くようなQuaternionを計算する。外積＋内積で算出する。
        /// </summary>
        /// <param name="v0">基点</param>
        /// <param name="v1">当来の点</param>
        /// <param name="v2">目標の点</param>
        /// <param name="rmax">最大とする回転量の絶対値(rad)</param>
        /// <returns>回転軸</returns>
        public static Quaternion cross(Vector3 v0, Vector3 v1, Vector3 v2, float rmax) {
            v1 -= v0; v1.Normalize();
            v2 -= v0; v2.Normalize();
            float dot = (Vector3.Dot(v1, v2)); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
            float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
            float rrad = Math.Abs(rmax / 180.0f * 3.1415f);
            float lrad = Math.Max(-rrad, Math.Min(rrad, rad));
            Vector3 cross = Vector3.Cross(v1, v2);
            return Quaternion.RotationAxis(cross, rad);
        }

        /// <summary>
        /// v0を基準にして，v1方向からv2方向に向くようなQuaternionを計算する。外積＋内積で算出する。
        /// </summary>
        /// <param name="v0">基点</param>
        /// <param name="v1">当来の点</param>
        /// <param name="v2">目標の点</param>
        /// <returns>回転軸</returns>
        public static Quaternion cross(Vector3 v0, Vector3 v1, Vector3 v2) {
            v1 -= v0; v1.Normalize();
            v2 -= v0; v2.Normalize();
            float dot = (Vector3.Dot(v1, v2)); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
            float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
            Vector3 cross = Vector3.Cross(v1, v2);
            return Quaternion.RotationAxis(cross, rad);
        }

        /// <summary>
        /// v0を起点として，v1方向からv2方向に向くようなQuaternionを計算する。但し回転軸にはvaxを使用する。
        /// </summary>
        /// <param name="v0">起点</param>
        /// <param name="v1">到来の点</param>
        /// <param name="v2">目標の点</param>
        /// <param name="vax">回転軸</param>
        /// <returns>四元数</returns>
        public static Quaternion ik_ph2(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 vax) {
            Quaternion rottoax = cross(Vector3.Zero, new Vector3(0, 1, 0), vax); // xyz座標系から，vax軸の座標系への回転。
            Vector3 drv1 = Vector3.TransformCoordinate(v1 - v0, Matrix.RotationQuaternion(rottoax));
            Vector3 drv2 = Vector3.TransformCoordinate(v2 - v0, Matrix.RotationQuaternion(rottoax));
            drv1.Y = 0;
            drv2.Y = 0;
            Quaternion rotflavor = cross(Vector3.Zero, drv1, drv2);
            float a = 0;
            Vector3 axis = Vector3.Zero;
            Quaternion.ToAxisAngle(rotflavor, ref axis, ref a);
            rotflavor = Quaternion.RotationAxis(vax, a);
            return rotflavor;
        }
    }
#endif

    [DebuggerDisplay("R({rq.X}, {rq.Y}, {rq.Z}, {rq.W}) T({tv.X}, {tv.Y}, {tv.Z}) S({sv.X}, {sv.Y}, {sv.Z})")]
    class RTv {
        public Vector3 tv = Vector3.Zero;
        public Vector3 sv = new Vector3(1, 1, 1);
        public Quaternion rq = Quaternion.Identity;

        public static RTv Empty {
            get { return new RTv(0, 0, 0, 0, 0, 0, 0, 0, 0); }
        }

        public RTv(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3) {
            rq = Quaternion.Identity;
            if (x2 != 0)
                rq *= (Quaternion.RotationAxis(new Vector3(1, 0, 0), x2));
            if (y2 != 0)
                rq *= (Quaternion.RotationAxis(new Vector3(0, 1, 0), y2));
            if (z2 != 0)
                rq *= (Quaternion.RotationAxis(new Vector3(0, 0, 1), z2));
            tv = new Vector3(x3, y3, z3);
            sv = new Vector3(x1, y1, z1);
        }

        public void Multiply(RTv o) {
            Matrix A = Matrix.RotationQuaternion(o.rq);
            Vector3 av = Vector3.TransformCoordinate(new Vector3(tv.X * sv.X, tv.Y * sv.Y, tv.Z * sv.Z), A);
            av += (o.tv);
            tv = av;
            rq *= (o.rq);
        }
        public Matrix Make() {
            return Matrix.RotationQuaternion(rq) * Matrix.Translation(tv);
        }

        public RTv Clone() {
            return (RTv)MemberwiseClone();
        }
    }
}
namespace hex04BinTrack.T {
    public class ReadMset {
        public ReadMset(Stream si) {
            foreach (ReadBar2.Barent ent in ReadBar2.Explode(si, 0)) {
                if (ent.k == 0x11) {
                    alanb.Add(new ReadAnib(si, ent));
                }
            }
        }

        public List<ReadAnib> alanb = new List<ReadAnib>();
    }

    public class ReadAnib {
        Stream si;

        public MotionObj toval;
        public String name;
        public MemoryStream motion;

        public ReadAnib(Stream si, ReadBar2.Barent ent) {
            this.si = si;
            this.toval = new MotionObj();
            this.name = ent.id;

            BinaryReader br = new BinaryReader(si);

            foreach (ReadBar2.Barent ent2 in ReadBar2.Explode(si, ent.off)) {
                if (ent2.k == 0x09) {
                    {
                        var buff = new byte[ent2.len];
                        si.Position = ent2.off;
                        si.Read(buff, 0, buff.Length);
                        motion = new MemoryStream(buff);
                    }
                    int tbloff = ent2.off + 0x90;
                    PosTbl p = new PosTbl(si, tbloff - 0x90);

                    if (p.Ver != 0) {
                        throw new NotSupportedException("Version " + p.Ver + " isn't supported yet!");
                    }

                    int offt1 = p.vb4;
                    int cntt1 = p.vb8;
                    for (int t = 0; t < cntt1; t++) {
                        si.Position = tbloff + offt1 + 8 * t; // t1
                        toval.t1Static.Add(new T1(br));
                    }

                    toval.mintick = 0;
                    toval.maxtick = p.TotalFrameCount;

                    for (int w = 0; w < 2; w++) {
                        int cntt2 = (w == 0) ? p.vc4 : p.vcc;
                        int offt2 = (w == 0) ? p.vc0 : p.vc8;
                        for (int t = 0; t < cntt2; t++) { // t2
                            si.Position = tbloff + offt2 + 6 * t; // 0x758
                            T2 t2 = new T2(br);
                            if (w == 0) toval.t2Anim.Add(t2);
                            else toval.t2x.Add(t2);

                            for (int t9 = 0; t9 < t2.c03; t9++) { // t9
                                si.Position = tbloff + p.vd0 + 8 * (t2.c04 + t9);

                                T9 timeLine = new T9(br);
                                t2.t9TimeLines.Add(timeLine);

                                if (true) { // t11
                                    si.Position = tbloff + p.vd4 + 4 * (timeLine.c00 / 4);
                                    timeLine.Interpolation = timeLine.c00 & 3;
                                    timeLine.KeyFrame = br.ReadSingle();
                                }
                                if (true) { // t10
                                    si.Position = tbloff + p.vd8 + 4 * timeLine.c02;
                                    timeLine.Value = br.ReadSingle();
                                }
                                if (true) { // t12
                                    si.Position = tbloff + p.vdc + 4 * timeLine.c04;
                                    timeLine.EaseIn = br.ReadSingle();
                                }
                                if (true) { // t12
                                    si.Position = tbloff + p.vdc + 4 * timeLine.c06;
                                    timeLine.EaseOut = br.ReadSingle();
                                }
                            }
                        }
                    }

                    int offt4 = p.vac;
                    int cntt4 = p.va2;
                    for (int t = 0; t < cntt4; t++) {
                        si.Position = tbloff + offt4 + 4 * t; // t4
                        toval.t4Joint.Add(new T4(br));
                    }

                    int offt5 = p.va8;
                    int cntt5 = p.va2 - p.va0;
                    for (int t = 0; t < cntt5; t++) {
                        si.Position = tbloff + offt5 + 64 * t; // t5
                        toval.t5Bone.Add(UtilAxBoneReader.read(br));
                    }

                    int offt3 = p.ve0;
                    int cntt3 = p.ve4;
                    for (int t = 0; t < cntt3; t++) {
                        si.Position = tbloff + offt3 + 12 * t; // t3
                        toval.t3IKC.Add(new T3(br));
                    }

                    {
                        // T6
                        var off = tbloff + p.vf8;
                        var cnt = p.vfc;
                        si.Position = off;
                        for (int t = 0; t < cnt; t++) {
                            toval.t6.Add(new T6(br));
                        }
                    }

                    {
                        // T7
                        var off = tbloff + p.vf0;
                        var cnt = p.vf4;
                        si.Position = off;
                        for (int t = 0; t < cnt; t++) {
                            toval.t7.Add(new T7(br));
                        }
                    }

                    {
                        // T8
                        var off = tbloff + p.vec;
                        var cnt = toval.t3IKC.Select(it => it.T8Idx + 1).Append(0).Max();
                        si.Position = off;
                        for (int t = 0; t < cnt; t++) {
                            toval.t8.Add(new T8(br));
                        }
                    }
                }
                else if (ent2.k == 0x10) {

                }
            }
        }

        class PosTbl {
            public int tbloff = 0x90;

            public int Ver { get; } // 0=interpolated, 1=raw

            public int va0;
            public int va2; // cnt t4

            public int TotalFrameCount { get; }

            public int va8; // off t5 (each 64 bytes)  { cnt_t5 = va2 -va0 }
            public int vac; // off t4 (each 4 bytes)
            public int vb0; // cnt t11
            public int vb4; // off t1 (each 8 bytes)
            public int vb8; // cnt t1
            public int vc0; // off t2 (each 6 bytes)
            public int vc4; // cnt t2
            public int vc8; // off t2` (each 6 bytes)
            public int vcc; // cnt t2`
            public int vd0; // off t9 (each 8 bytes)
            public int vd4; // off t11 (each 4 bytes)
            public int vd8; // off t10 (each 4 bytes)
            public int vdc; // off t12 (each 4 bytes)
            public int ve0; // off t3 (each 12 bytes)
            public int ve4; // cnt t3
            public int ve8;
            public int vec; // off t8 (each 48 bytes)  { cnt_t8 = cnt_t2` }
            public int vf0; // off t7 (each 8 bytes)
            public int vf4; // cnt t7
            public int vf8; // off t6 (each 12 bytes)
            public int vfc; // cnt t6
            public float[] bbox;

            public float FrameLoop { get; }
            public float FrameEnd { get; }
            public float FPS { get; }
            public float FrameCount { get; }

            public PosTbl(Stream si, int baseoff) {
                BinaryReader br = new BinaryReader(si);
                int off = baseoff + tbloff - 0x90;

                si.Position = off + 0x90;
                Ver = br.ReadInt32();
                br.ReadInt32();
                br.ReadInt32();
                br.ReadInt32();

                va0 = br.ReadUInt16();
                va2 = br.ReadUInt16(); // cnt t4
                TotalFrameCount = br.ReadInt32();
                si.Position = off + 0xA8;
                va8 = br.ReadInt32(); // off t5 (each 64 bytes)  { cnt_t5 = va2 -va0 }
                vac = br.ReadInt32(); // off t4 (each 4 bytes)

                si.Position = off + 0xB0;
                vb0 = br.ReadInt32(); // cnt t11
                vb4 = br.ReadInt32(); // off t1 (each 8 bytes)
                vb8 = br.ReadInt32(); // cnt t1
                si.Position = off + 0xC0;
                vc0 = br.ReadInt32(); // off t2 (each 6 bytes)
                vc4 = br.ReadInt32(); // cnt t2
                vc8 = br.ReadInt32(); // off t2` (each 6 bytes)
                vcc = br.ReadInt32(); // cnt t2`
                si.Position = off + 0xD0;
                vd0 = br.ReadInt32(); // off t9 (each 8 bytes)
                vd4 = br.ReadInt32(); // off t11 (each 4 bytes)
                vd8 = br.ReadInt32(); // off t10 (each 4 bytes)
                vdc = br.ReadInt32(); // off t12 (each 4 bytes)
                si.Position = off + 0xE0;
                ve0 = br.ReadInt32(); // off t3 (each 12 bytes)
                ve4 = br.ReadInt32(); // cnt t3
                ve8 = br.ReadInt32();
                vec = br.ReadInt32(); // off t8 (each 48 bytes)  { cnt_t8 = cnt_t2` }
                si.Position = off + 0xF0;
                vf0 = br.ReadInt32(); // off t7 (each 8 bytes)
                vf4 = br.ReadInt32(); // cnt t7
                vf8 = br.ReadInt32(); // off t6 (each 12 bytes)
                vfc = br.ReadInt32(); // cnt t6
                bbox = new float[] {
                    br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),
                    br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),
                };
                FrameLoop = br.ReadSingle();
                FrameEnd = br.ReadSingle();
                FPS = br.ReadSingle();
                FrameCount = br.ReadSingle();
            }
        }
    }
}
