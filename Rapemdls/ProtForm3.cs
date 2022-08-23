#define USETEX
//#define MEGA

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using Rapemdls.Mdlxo;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace Rapemdls {
    public partial class ProtForm3 : Form {
        public ProtForm3() {
            InitializeComponent();
        }

        Device device;
        List<Texture> altex = new List<Texture>();
        Texexp texexp = null;

        private void panel1_Paint(object sender, PaintEventArgs e) {
            devDraw();
        }

        /// <summary>
        /// デバイスを描画しなさい
        /// </summary>
        private void devDraw() {
            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            device.BeginScene();
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, panel1.BackColor, 1.0f, 0);

            bool wf = checkBoxWire.Checked;
            bool fl = !checkBoxWire.Checked;

            Vector3 vlf = Vector3.TransformCoordinate(new Vector3(0, 0, 1), Matrix.RotationQuaternion(Quaternion.Invert(quat)));

            device.RenderState.FillMode = wf ? FillMode.WireFrame : FillMode.Solid;
            device.RenderState.Lighting = false;// fl;
            device.RenderState.ZBufferFunction = Compare.LessEqual;
            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Position = Vector3.Empty;
            device.Lights[0].Direction = -vlf;
            device.Lights[0].Enabled = fl;

            if (vb != null) {
                device.SetStreamSource(0, vb, 0);
                device.VertexFormat = PositionNormalColoredTextured.Format;
                device.Indices = null;
                foreach (Drew o in aldrew) {
#if USETEX
                    if (!wf) {
                        device.SetTexture(0, altex[o.tex]);
                        device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.Modulate);
                        device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                        device.SetTextureStageState(0, TextureStageStates.ColorArgument2, (int)TextureArgument.Diffuse);
                        device.SetSamplerState(0, SamplerStageStates.MinFilter, (int)TextureFilter.Linear);
                        device.SetSamplerState(0, SamplerStageStates.MagFilter, (int)TextureFilter.Linear);
                    }
#endif
                    device.DrawPrimitives(o.prim, o.offv, o.cntPrimitives);
                }

                device.SetTexture(0, null);
                device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg2);
            }


            device.RenderState.Lighting = false;
            device.RenderState.ZBufferFunction = Compare.Always;
            device.RenderState.PointSize = 3.0f;
            device.RenderState.FillMode = FillMode.Solid;

            if (checkBoxShow2.Checked) {
                if (o骨Β.vb != null) {
                    device.SetStreamSource(0, o骨Β.vb, 0);
                    device.VertexFormat = o骨Β.fvf;
                    device.DrawPrimitives(o骨Β.prim, 0, o骨Β.cntPrims);
                }
                if (o接点Β.vb != null) {
                    device.SetStreamSource(0, o接点Β.vb, 0);
                    device.VertexFormat = o接点Β.fvf;
                    device.DrawPrimitives(o接点Β.prim, 0, o接点Β.cntPrims);
                }
            }
            else {
                if (o骨.vb != null) {
                    device.SetStreamSource(0, o骨.vb, 0);
                    device.VertexFormat = o骨.fvf;
                    device.DrawPrimitives(o骨.prim, 0, o骨.cntPrims);
                }
                if (o接点.vb != null) {
                    device.SetStreamSource(0, o接点.vb, 0);
                    device.VertexFormat = o接点.fvf;
                    device.DrawPrimitives(o接点.prim, 0, o接点.cntPrims);
                }
            }
            if (oAx3 != null) {
                device.SetStreamSource(0, oAx3.vb, 0);
                device.VertexFormat = oAx3.fvf;
                device.DrawPrimitives(oAx3.prim, 0, oAx3.cntPrims);
            }

            device.EndScene();
            device.Present();
        }

        /// <summary>
        /// デバイスをサイズ変更に対応しなさい
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void device_DeviceResizing(object sender, CancelEventArgs e) {
            devReshape();
        }

        float cxvwpt = 10.0f;
        float cyvwpt = 10.0f;
        float czvwpt = 10.0f;
        float fact = 10.0f;

        class NotAvailTexException : Exception {
        }

        private void devReshape() {
            int cxw = panel1.Width;
            int cyw = panel1.Height;
            float fx = (cxw > cyw) ? ((float)cxw / cyw) : 1.0f;
            float fy = (cxw < cyw) ? ((float)cyw / cxw) : 1.0f;

            if (true) {
#if false
                device.Transform.Projection = Matrix.OrthoLH(
                    fx * cxvwpt * fact, fy * cyvwpt * fact, czvwpt * fact / +1, czvwpt * fact / -1
                    );
                Matrix V = Matrix.Identity;
                V.Multiply(Matrix.RotationQuaternion(quat));
                V.Multiply(Matrix.Translation(voff));
                device.Transform.View = V;
#else
                Vector3 vf = Vector3.TransformCoordinate(new Vector3(0, 0, 1 * fact), Matrix.RotationQuaternion(Quaternion.Invert(quat)));
                Vector3 vu = Vector3.TransformCoordinate(new Vector3(0, 1 * fact, 0), Matrix.RotationQuaternion(Quaternion.Invert(quat)));

                device.Transform.Projection = Matrix.OrthoLH(
                    fx * cxvwpt * fact, fy * cyvwpt * fact, czvwpt * fact / +1, czvwpt * fact / -1
                    );

                Matrix V = Matrix.Identity;
                V.Multiply(Matrix.Translation(voff));
                V.Multiply(Matrix.LookAtRH(
                    -vf,
                    vf,
                    vu
                    ));
                device.Transform.View = V;
#endif
            }
        }

        /// <summary>
        /// デバイスを再初期化しなさい
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void device_DeviceReset(object sender, EventArgs e) {
            devReset();
        }

        /// <summary>
        /// RenderStateを初期化しなさい
        /// </summary>
        private void devReset() {
            device.RenderState.ColorVertex = true;

            foreach (Texture res in altex) {
                res.Dispose();
            }
            altex.Clear();
            if (texexp != null) {
                for (int t = 0; ; t++) {
                    Bitmap pic = texexp.Explode(t);
                    if (pic == null) break;
#if USETEX
#if MEGA
                    Texture tex = new Texture(device, 256, 256, 0, Usage.Dynamic, Format.A32B32G32R32F, Pool.Default);
                    int pitch;
                    GraphicsStream gs = tex.LockRectangle(0, LockFlags.None, out pitch);
                    try {
                        for (int y = 0; y < 256; y++)
                            for (int x = 0; x < 256; x++) {
                                Color c00 = pic.GetPixel(Math.Min(127, x / 2 + 0), Math.Min(127, y / 2 + 0));
                                Color c01 = pic.GetPixel(Math.Min(127, x / 2 + 1), Math.Min(127, y / 2 + 0));
                                Color c10 = pic.GetPixel(Math.Min(127, x / 2 + 0), Math.Min(127, y / 2 + 1));
                                Color c11 = pic.GetPixel(Math.Min(127, x / 2 + 1), Math.Min(127, y / 2 + 1));

                                float f00 = 0, f01 = 0, f10 = 0, f11 = 0;
                                switch ((y & 1)) {
                                    case 0:
                                        switch ((x & 1)) {
                                            case 0: f00 = 1; break;
                                            case 1: f00 = f01 = 0.5f; break;
                                        }
                                        break;
                                    case 1:
                                        switch ((x & 1)) {
                                            case 0: f00 = f10 = 0.5f; break;
                                            case 1: f00 = f10 = f01 = f11 = 0.25f; break;
                                        }
                                        break;
                                }

                                gs.Write((float)((c00.R * f00 + c01.R * f01 + c10.R * f10 + c11.R * f11) / 255.0f));
                                gs.Write((float)((c00.G * f00 + c01.G * f01 + c10.G * f10 + c11.G * f11) / 255.0f));
                                gs.Write((float)((c00.B * f00 + c01.B * f01 + c10.B * f10 + c11.B * f11) / 255.0f));
                                gs.Write((float)(1.0f));
                            }
                    }
                    finally {
                        tex.UnlockRectangle(0);
                    }
                    altex.Add(tex);
#else
                    try {
                        Texture tex;
                        try {
                            tex = new Texture(device, 128, 128, 0, Usage.Dynamic, Format.A32B32G32R32F, Pool.Default);
                        }
                        catch (InvalidCallException) {
                            throw new NotAvailTexException();
                        }
                        int pitch;
                        GraphicsStream gs = tex.LockRectangle(0, LockFlags.None, out pitch);
                        try {
                            for (int y = 0; y < 128; y++)
                                for (int x = 0; x < 128; x++) {
                                    Color c = pic.GetPixel(x, y);
                                    gs.Write((float)((c.R) / 255.0f));
                                    gs.Write((float)((c.G) / 255.0f));
                                    gs.Write((float)((c.B) / 255.0f));
                                    gs.Write((float)(1.0f));
                                }
                        }
                        finally {
                            tex.UnlockRectangle(0);
                        }
                        altex.Add(tex);
                    }
                    catch (NotAvailTexException) {
                        try {
                            Texture tex;
                            try {
                                tex = new Texture(device, 128, 128, 0, Usage.Dynamic, Format.A8R8G8B8, Pool.Default);
                            }
                            catch (InvalidCallException) {
                                throw new NotAvailTexException();
                            }
                            int pitch;
                            GraphicsStream gs = tex.LockRectangle(0, LockFlags.None, out pitch);
                            try {
                                for (int y = 0; y < 128; y++)
                                    for (int x = 0; x < 128; x++) {
                                        Color c = pic.GetPixel(x, y);
                                        gs.Write((byte)(c.B));
                                        gs.Write((byte)(c.G));
                                        gs.Write((byte)(c.R));
                                        gs.Write((byte)(255));
                                    }
                            }
                            finally {
                                tex.UnlockRectangle(0);
                            }
                            altex.Add(tex);
                        }
                        catch (NotAvailTexException) {
                            throw;
                        }
                    }
#endif
#endif
                }
            }
        }

        private void ProtForm3_Load(object sender, EventArgs e) {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = DepthFormat.D16;
            device = new Device(0, DeviceType.Hardware, panel1, CreateFlags.SoftwareVertexProcessing, pp);
            devReset();
            device.DeviceReset += new EventHandler(device_DeviceReset);
            device.DeviceResizing += new CancelEventHandler(device_DeviceResizing);

            devReshape();

            recalc();
            calcvbib();

            panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);

            for (int t = 0; t < s4.als5.Count; t++) {
                listBox1.Items.Add(t.ToString());
            }
#if false
            for (int t = 0; t < alaxv.Count; t++) {
                Axv o = alaxv[t];
                listView1.Items.Add(string.Format(
                    "#{0:000} {1,2}|{2,10:0.000} {3,10:0.000} {4,10:0.000}|{6,10:0.000} {7,10:0.000} {8,10:0.000} {9,10:0.000}"
                    , t, o.parentIndex
                    , o.offset.X, o.offset.Y, o.offset.Z, 0
                    , o.quat.X, o.quat.Y, o.quat.Z, o.quat.W
                    ));
            }
#else
            for (int t = 0; t < s4.alvr.Count; t++) {
                Vone vone = s4.alvr[t];
                listView1.Items.Add(string.Format(
                    "#{0:000} {1,3}|{9,10:0.000}"
                    , t, vone.Parent
                    , vone.x0, vone.y0, vone.z0, vone.w0
                    , vone.x1, vone.y1, vone.z1, vone.w1
                    , vone.x2, vone.y2, vone.z2, vone.w2
                    ));
            }
#endif
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e) {
            fact = Math.Max(1, Math.Min(100, fact + e.Delta / 120));

            devReshape(); panel1.Invalidate();
        }

        VertexBuffer vb = null;
        int cntPrimitives = 0, cntVerts = 0;
        int sels5 = -1, selhive = -1;
        List<Drew> aldrew = new List<Drew>();

        class Drew {
            public int offv = 0, cntPrimitives = 0;
            public int tex = 0;
            public PrimitiveType prim = PrimitiveType.TriangleList;
        }

        class Arial {
            public VertexBuffer vb = null;

            public int cntVerts = 0;
            public int cntPrims = 0;
            public PrimitiveType prim = PrimitiveType.TriangleList;
            public VertexFormats fvf = VertexFormats.None;
        }
        Arial o骨 = new Arial();
        Arial o骨Β = new Arial();
        Arial o接点 = new Arial();
        Arial o接点Β = new Arial();
        Arial oAx3 = new Arial();

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionNormalColoredTextured {
            public const VertexFormats Format = VertexFormats.Diffuse | VertexFormats.Normal | VertexFormats.Position | VertexFormats.Texture1;

            public float X;
            public float Y;
            public float Z;
            public float Nx;
            public float Ny;
            public float Nz;
            public int Color;
            public float Tu;
            public float Tv;

            public PositionNormalColoredTextured(Vector3 pos, Vector3 nor, Vector2 tex, int c) {
                X = pos.X;
                Y = pos.Y;
                Z = pos.Z;
                Nx = nor.X;
                Ny = nor.Y;
                Nz = nor.Z;
                Tu = tex.Y;
                Tv = tex.X;
                Color = c;
            }
            public PositionNormalColoredTextured(float xval, float yval, float zval, float nxval, float nyval, float nzval, int c, float tuval, float tvval) {
                X = xval;
                Y = yval;
                Z = zval;
                Nx = nxval;
                Ny = nyval;
                Nz = nzval;
                Tu = tuval;
                Tv = tvval;
                Color = c;
            }

            public Vector3 Normal {
                get { return new Vector3(Nx, Ny, Nz); }
                set { Nx = value.X; Ny = value.Y; Nz = value.Z; }
            }
            public Vector3 Position {
                get { return new Vector3(X, Y, Z); }
                set { X = value.X; Y = value.Y; Z = value.Z; }
            }
            public static int StrideSize {
                get { return 4 * 9; }
            }
        }

        class Axv {
            public Quaternion quat = Quaternion.Identity;
            public Quaternion temp = Quaternion.Identity;
            public Vector3 offset = Vector3.Empty;
            public int parentIndex = -1;
        }
        List<Axv> alaxv = new List<Axv>();
        List<Axv> alaxw = new List<Axv>();

        U3 testMotion() {
            if (u1 == null) return null;
            int iAnim = Math.Min(hScrollBarAnimSel.Value, u1.u2.alu3.Count - 1);
            if ((uint)u1.u2.alu3.Count <= (uint)iAnim) return null;
            U3 o = u1.u2.alu3[iAnim];
            return o;
        }

        List<Vone> alvrlast = null;

        Axv baseAxw {
            get {
                Axv o = new Axv();
                o.offset = Vector3.Empty;
                o.parentIndex = -1;
                o.quat = Quaternion.RotationAxis(new Vector3(0, 1, 0), 1.57079f);
                return o;
            }
        }

        void recalc() {
            List<Vone> alvr = new List<Vone>();
            foreach (Vone vone in s4.alvr) alvr.Add(vone.Clone());

            U3 u3 = testMotion();

            if (u3 != null && checkBoxApplyPatch.Checked) {
                for (int t = 0; t < u3.alu8.Count; t++) {
                    U8 u8 = u3.alu8[t];
                    Vone vone = alvr[u8.v00];
                    switch (u8.v02) {
                        case 1: vone.x0 = u8.v04; break;
                        case 2: vone.y0 = u8.v04; break;
                        case 3: vone.z0 = u8.v04; break;
                        case 4: vone.x1 = Sel1.calc(vone.w1, vone.x1, u8.v04); ; break;
                        case 5: vone.y1 = Sel1.calc(vone.w1, vone.y1, u8.v04); ; break;
                        case 6: vone.z1 = Sel1.calc(vone.w1, vone.z1, u8.v04); ; break;
                        case 7: vone.x2 = u8.v04; break;
                        case 8: vone.y2 = u8.v04; break;
                        case 9: vone.z2 = u8.v04; break;
                        default: Debug.Fail("U8.v02 → " + u8.v02 + " is ?"); break;
                    }
                }
            }
            if (u3 != null && checkBoxApplyMotion.Checked) {
                for (int c4 = 0; c4 < u3.alu4a.Count; c4++) {
                    float pos = Convert.ToSingle(hScrollBarFrame.Value);
                    Vone vone = alvr[u3.alu4a[c4].v00];
                    float yval = YaVal.calc(pos, u3.alu5, u3.alu4a[c4].v03, u3.alu4a[c4].v04);
                    switch (u3.alu4a[c4].v02) {
                        case 1: vone.x0 = yval; break;
                        case 2: vone.y0 = yval; break;
                        case 3: vone.z0 = yval; break;
                        case 4: vone.x1 = yval; break;
                        case 5: vone.y1 = yval; break;
                        case 6: vone.z1 = yval; break;
                        case 7: vone.x2 = yval; break;
                        case 8: vone.y2 = yval; break;
                        case 9: vone.z2 = yval; break;
                        default: Debug.Fail("U4.v02 → " + u3.alu4a[c4].v02 + " is ?"); break;
                    }
                }
            }

            if (alifApply == null) {
                alifApply = new System.Collections.BitArray(6 * alvr.Count, false);
                alvalApply = new float[alifApply.Count];
            }
            else {
                for (int t = 0; t < alifApply.Count; t++) {
                    if (alifApply[t]) {
                        Vone vone = alvr[t / 6];
                        switch (t % 6) {
                            case 0: vone.x1 = alvalApply[t]; break;
                            case 1: vone.y1 = alvalApply[t]; break;
                            case 2: vone.z1 = alvalApply[t]; break;
                            case 3: vone.x2 = alvalApply[t]; break;
                            case 4: vone.y2 = alvalApply[t]; break;
                            case 5: vone.z2 = alvalApply[t]; break;
                        }
                    }
                }
            }

            bool fUseik = checkBoxUseik.Checked && (u3 != null);

            alvrlast = alvr;

            // b'ones set 2
            alaxw.Clear();
            if (u3 != null) {
                List<Vone> alwr = new List<Vone>();

                for (int t = 0; t < u3.alu9.Count; t++) {
                    U9 u9 = u3.alu9[t];
                    Vone o = new Vone();
                    o.x1 = u9.v10;
                    o.y1 = u9.v14;
                    o.z1 = u9.v18;
                    o.x2 = u9.v1c;
                    o.y2 = u9.v20;
                    o.z2 = u9.v24;
                    o.w2 = u9.v0c;
                    alwr.Add(o);
                }

                if (u3 != null && checkBoxApplyMotion.Checked) {
                    for (int c4 = 0; c4 < u3.alu4b.Count; c4++) {
                        float pos = Convert.ToSingle(hScrollBarFrame.Value);
                        Vone vone = alwr[u3.alu4b[c4].v00];
                        float yval = YaVal.calc(pos, u3.alu5, u3.alu4b[c4].v03, u3.alu4b[c4].v04);
                        switch (u3.alu4b[c4].v02) {
                            case 1: vone.x0 = yval; break;
                            case 2: vone.y0 = yval; break;
                            case 3: vone.z0 = yval; break;
                            case 4: vone.x1 = yval; break;
                            case 5: vone.y1 = yval; break;
                            case 6: vone.z1 = yval; break;
                            case 7: vone.x2 = yval; break;
                            case 8: vone.y2 = yval; break;
                            case 9: vone.z2 = yval; break;
                            default: Debug.Fail("U4.v02 → " + u3.alu4b[c4].v02 + " is ?"); break;
                        }
                    }
                }

                for (int t = 0; t < alwr.Count; t++) {
                    Vone vo = alwr[t];
                    Axv ax = new Axv();
                    ax.quat = Quaternion.Identity;
                    ax.quat.Multiply(Quaternion.RotationAxis(new Vector3(1, 0, 0), vo.x1));
                    ax.quat.Multiply(Quaternion.RotationAxis(new Vector3(0, 1, 0), vo.y1));
                    ax.quat.Multiply(Quaternion.RotationAxis(new Vector3(0, 0, 1), vo.z1));
                    ax.offset = new Vector3(vo.x2, vo.y2, vo.z2);
                    ax.parentIndex = vo.Parent;
                    Axv px = (vo.Parent < 0) ? baseAxw : (alaxw[vo.Parent]);
                    if (px != null) {
                        ax.quat = ax.quat * px.quat;
                        ax.offset = px.offset + Vector3.TransformCoordinate(ax.offset, Matrix.RotationQuaternion(px.quat));
                    }
                    alaxw.Add(ax);
                }
            }

            // b'ones set 1
            alaxv.Clear();
            int iki = -1;
            for (int t = 0; t < alvr.Count; t++) {
                Vone vo = alvr[t];
                Axv ax = new Axv();
                ax.quat = UtilVone.tor(vo);
                ax.offset = UtilVone.tov(vo);
                ax.parentIndex = vo.Parent;
                Axv px = (vo.Parent < 0) ? null : (alaxv[vo.Parent]);
                if (px != null) {
                    ax.quat = ax.quat * px.quat;
                    ax.offset = px.offset + Vector3.TransformCoordinate(ax.offset, Matrix.RotationQuaternion(px.quat));
                }
                alaxv.Add(ax);

                bool fSubjectik0 = (u3 != null) && (0 != (u3.alu7[t].v00 & 0x0400000));
                bool fSubjectik1 = (u3 != null) && (0 != (u3.alu7[t].v00 & 0x0800000));

                if (fUseik && u3 != null && fSubjectik0) {
                    iki = t;
                }
                if (fUseik && u3 != null && fSubjectik1) {
                    int b2i = ((u3.alu7[t].v00 >> 11) & 255) - 1;
                    Vector3 ptik = alaxw[b2i].offset;

                    for (int w = 0; w < 5; w++) {
                        if (iki == -1) { }
                        else if (t - iki == 1) {
                            //
                            alaxv[iki + 0].temp = alaxv[iki + 0].quat * CalcUtil.cross(
                                alaxv[iki + 0].offset,
                                alaxv[iki + 1].offset,
                                ptik
                                );

                            calcIt(alvr[iki + 1], alaxv[iki + 1]);
                            //
                            alaxv[iki + 0].quat = alaxv[iki + 0].temp;
                        }
                        else if (t - iki == 2) {
                            //
                            alaxv[iki + 1].temp = alaxv[iki + 1].quat * CalcUtil.cross(
                                alaxv[iki + 1].offset,
                                alaxv[iki + 2].offset,
                                ptik
                                );

                            calcIt(alvr[iki + 2], alaxv[iki + 2]);
                            //
                            alaxv[iki + 0].temp = alaxv[iki + 0].quat * CalcUtil.cross(
                                alaxv[iki + 0].offset,
                                alaxv[iki + 2].offset,
                                ptik
                                );

                            calcIt(alvr[iki + 1], alaxv[iki + 1]);
                            calcIt(alvr[iki + 2], alaxv[iki + 2]);
                            //
                            alaxv[iki + 0].quat = alaxv[iki + 0].temp;
                            alaxv[iki + 1].quat = alaxv[iki + 1].temp;
                        }
                    }
                    ax.quat = UtilVone.tor(vo) * px.quat;

                    iki = -1;
                }
            }
        }

        class UtilVone {
            public static Vector3 tov(Vone vo) {
                return new Vector3(vo.x2, vo.y2, vo.z2);
            }
            public static Quaternion tor(Vone vo) {
                Quaternion quat = Quaternion.Identity;
                quat.Multiply(Quaternion.RotationAxis(new Vector3(1, 0, 0), vo.x1));
                quat.Multiply(Quaternion.RotationAxis(new Vector3(0, 1, 0), vo.y1));
                quat.Multiply(Quaternion.RotationAxis(new Vector3(0, 0, 1), vo.z1));
                return quat;
            }
        }

        void calcIt(Vone vo, Axv ax) {
            Axv px = (vo.Parent < 0) ? null : (alaxv[vo.Parent]);
            if (px != null) {
                ax.quat = ax.quat * px.temp;
                ax.offset = px.offset + Vector3.TransformCoordinate(new Vector3(vo.x2, vo.y2, vo.z2), Matrix.RotationQuaternion(px.temp));
            }
        }

        class CalcUtil {
            /// <summary>
            /// v0を基準にして，v1方向からv2方向に向くようなQuaternionを計算する。外積＋内積で算出する。
            /// </summary>
            /// <param name="v0">基点</param>
            /// <param name="v1">当来の点</param>
            /// <param name="v2">目標の点</param>
            /// <param name="r">回転量</param>
            /// <returns>回転軸</returns>
            public static Quaternion cross(Vector3 v0, Vector3 v1, Vector3 v2) {
                v1 -= v0; v1.Normalize();
                v2 -= v0; v2.Normalize();
                float dot = (Vector3.Dot(v1, v2)); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                Vector3 cross = Vector3.Cross(v1, v2);
                return Quaternion.RotationAxis(cross, rad);
            }
        }

        private void calcvbib() {
            if (vb != null) {
                vb.Dispose();
                vb = null;
            }
            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            if (s4 == null)
                return;
            cntPrimitives = 0;
            cntVerts = 0;
            aldrew.Clear();
            if (true) {
                int offv = 0;
                foreach (S5 s5 in s4.als5) {
                    int cntv = 0;
                    foreach (Hive hive in s5.alhive) {
                        cntv += Math.Max(0, (hive.al.Count - 2)) * 3;
                        cntv += Math.Max(0, (hive.al.Count - 2)) * 3;
                    }
                    Drew ents5 = new Drew();
                    ents5.tex = s5.v04;
                    ents5.offv = offv;
                    ents5.cntPrimitives = cntv / 3;
                    aldrew.Add(ents5);
                    offv += cntv;
                    cntVerts += cntv;
                }
            }

            cntPrimitives = cntVerts / 3;

            vb = new VertexBuffer(
                typeof(PositionNormalColoredTextured),
                cntVerts,
                device,
                Usage.None,
                PositionNormalColoredTextured.Format,
                Pool.Managed
                );
            GraphicsStream gs = vb.Lock(0, 0, 0);
            try {
                PositionNormalColoredTextured v = new PositionNormalColoredTextured();
                PositionNormalColoredTextured[] vvv = new PositionNormalColoredTextured[3];
                int[] altb = new int[200];
                for (int a = 0; a < 200; a++) altb[a] = a;
                for (int a = 0; a < s4.als5.Count; a++) {
                    S5 s5 = s4.als5[a];
                    for (int x = 0, cx = s5.alhive.Count; x < cx; x++) {
                        Hive hive = s5.alhive[x];
                        if (hive.vonekey != -1) {
                            altb[hive.vonekey] = hive.voneval;
                        }
                        for (int t = 0; t < hive.al.Count - 2; t++) {
                            if (a == sels5) {
                                if (x == selhive) {
                                    v.Color = Color.OrangeRed.ToArgb();
                                }
                                else {
                                    v.Color = Color.Yellow.ToArgb();
                                }
                            }
                            else { v.Color = Color.White.ToArgb(); }

                            for (int w = 0; w < 3; w++) {
                                int op = ((t & 1) == 1) ? (((w == 1) ? +1 : 0) + ((w == 2) ? -1 : 0)) : 0;

                                Vone vo = hive.al[t + w + op];
                                Vector3 v3p = new Vector3(vo.x1, vo.y1, vo.z1);
                                Axv axv = alaxv[altb[vo.Belonging]];
                                Quaternion v3q = axv.quat;

                                v3p = Vector3.TransformCoordinate(v3p, Matrix.RotationQuaternion(v3q)) + axv.offset;
                                v.Position = v3p;
                                v.Tu = hive.al[t + w + op].x2;
                                v.Tv = hive.al[t + w + op].y2;
                                vvv[w] = v;
                            }

                            int k0 = 0;
                            int k1 = 2;
                            int k2 = 1;
                            Vector3 nv = Vector3.Cross(vvv[k1].Position - vvv[k0].Position, vvv[k2].Position - vvv[k0].Position);
                            nv.Normalize();
                            // normal side
                            vvv[k0].Normal = vvv[k1].Normal = vvv[k2].Normal = nv;
                            gs.Write(vvv[k0]);
                            gs.Write(vvv[k1]);
                            gs.Write(vvv[k2]);

                            // inverted side. for simulating no-cull with correct lighting
                            vvv[k0].Normal = vvv[k2].Normal = vvv[k1].Normal = -nv;
                            gs.Write(vvv[k0]);
                            gs.Write(vvv[k2]);
                            gs.Write(vvv[k1]);
                        }
                    }
                }
            }
            finally {
                vb.Unlock();
            }

            calcAx3vb();
            calc骨vb();
            calc接点vb();
            calc骨Βvb();
            calc接点Βvb();
        }

        class MIfneq0 {
            public static float calc(float pos, float v0, float v1) {
                return (pos != 0) ? v1 * pos : v1;
            }
        }
        class MA1 {
            public static float calc(float pos, float v0, float v1) {
                return v1 + pos * v0;
            }
        }
        class Add1 {
            public static float calc(float pos, float v0, float v1) {
                return v0 + v1;
            }
        }
        class Sub1 {
            public static float calc(float pos, float v0, float v1) {
                return v0 - v1;
            }
        }
        class M1 {
            public static float calc(float pos, float v0, float v1) {
                return pos * v1;
            }
        }
        class Sel1 {
            public static float calc(float pos, float v0, float v1) {
                return v1;
            }
        }
        class RateAdd {
            public static float calc(float pos, float v0, float v1) {
                return pos * v0 + v1;
            }
        }

        class TwoLerp {
            public static float calc(float pos, float v1, float v0) {
                return (v0 * (1.0f - pos)) + v1 * (pos);
            }
        }

        class YaVal {
            public static float calc(float pos, List<U5> alu5, int cnt, int off) {
                pos = (int)pos % alu5[off + cnt - 1].v02;
                int x = 1;
                for (; x < cnt && alu5[off + x].v02 < (int)pos; x++) ;
                float f0 = alu5[off + x - 1].v02;
                float v0 = alu5[off + x - 1].v04;
                float f1 = alu5[off + x].v02;
                float v1 = alu5[off + x].v04;
                f1 -= f0;
                pos -= f0;
                f0 = 0;
                pos /= f1;
                float yv = v0 * (1.0f - pos) + v1 * (pos);
                return yv;
            }
        }

        Vector3 axoff = Vector3.Empty;
        Quaternion axqr = Quaternion.Identity;

        void calcAx3vb() {
            if (oAx3.vb != null) {
                oAx3.vb.Dispose();
                oAx3.vb = null;
            }
            oAx3.cntVerts = 0;
            oAx3.cntPrims = 0;

            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            oAx3.cntVerts = 2 * 6;
            oAx3.prim = PrimitiveType.LineList;
            oAx3.fvf = CustomVertex.PositionColored.Format;

            oAx3.vb = new VertexBuffer(
                typeof(CustomVertex.PositionColored),
                oAx3.cntVerts,
                device,
                Usage.Points,
                oAx3.fvf,
                Pool.Managed
                );
            GraphicsStream gs = oAx3.vb.Lock(0, 0, 0);
            try {
                float r = 20.0f;

                Vector3 v0 = axoff;
                Vector3 vx = Vector3.TransformCoordinate(new Vector3(r, 0, 0), Matrix.RotationQuaternion(axqr));
                Vector3 vy = Vector3.TransformCoordinate(new Vector3(0, r, 0), Matrix.RotationQuaternion(axqr));
                Vector3 vz = Vector3.TransformCoordinate(new Vector3(0, 0, r), Matrix.RotationQuaternion(axqr));

                CustomVertex.PositionColored v = new CustomVertex.PositionColored();

                v.Color = Color.LightBlue.ToArgb(); v.Position = v0; gs.Write(v); // 0
                v.Color = Color.LightBlue.ToArgb(); v.Position = v0 + vx; gs.Write(v); // X

                v.Color = Color.LightGreen.ToArgb(); v.Position = v0; gs.Write(v); // 0
                v.Color = Color.LightGreen.ToArgb(); v.Position = v0 + vy; gs.Write(v); // Y

                v.Color = Color.Red.ToArgb(); v.Position = v0; gs.Write(v); // 0
                v.Color = Color.Red.ToArgb(); v.Position = v0 + vz; gs.Write(v); // Z

                v.Color = Color.LightBlue.ToArgb(); v.Position = v0 + vx; gs.Write(v); // X
                v.Color = Color.Red.ToArgb(); v.Position = v0 + vz; gs.Write(v); // Z

                v.Color = Color.LightGreen.ToArgb(); v.Position = v0 + vy; gs.Write(v); // Y
                v.Color = Color.Red.ToArgb(); v.Position = v0 + vz; gs.Write(v); // Z

                v.Color = Color.LightBlue.ToArgb(); v.Position = v0 + vx; gs.Write(v); // X
                v.Color = Color.LightGreen.ToArgb(); v.Position = v0 + vy; gs.Write(v); // Y

                oAx3.cntPrims = 3;
            }
            finally {
                oAx3.vb.Unlock();
            }
        }

        void calc接点vb() {
            if (o接点.vb != null) {
                o接点.vb.Dispose();
                o接点.vb = null;
            }
            o接点.cntVerts = 0;
            o接点.cntPrims = 0;

            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            o接点.cntVerts = alaxv.Count;
            o接点.prim = PrimitiveType.PointList;
            o接点.fvf = CustomVertex.PositionColored.Format;

            o接点.vb = new VertexBuffer(
                typeof(CustomVertex.PositionColored),
                o接点.cntVerts,
                device,
                Usage.Points,
                o接点.fvf,
                Pool.Managed
                );
            GraphicsStream gs = o接点.vb.Lock(0, 0, 0);
            try {
                CustomVertex.PositionColored v = new CustomVertex.PositionColored();
                for (int t = 0; t < alaxv.Count; t++) {
                    Axv ax = alaxv[t];
                    v.Color = Color.Magenta.ToArgb();
                    v.Position = ax.offset;
                    gs.Write(v);

                    o接点.cntPrims++;
                }
            }
            finally {
                o接点.vb.Unlock();
            }
        }

        void calc骨vb() {
            if (o骨.vb != null) {
                o骨.vb.Dispose();
                o骨.vb = null;
            }
            o骨.cntVerts = 0;
            o骨.cntPrims = 0;

            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            o骨.cntVerts = 2 * alaxv.Count;
            o骨.prim = PrimitiveType.LineList;
            o骨.fvf = CustomVertex.PositionColored.Format;

            o骨.vb = new VertexBuffer(
                typeof(CustomVertex.PositionColored),
                o骨.cntVerts,
                device,
                Usage.Points,
                o骨.fvf,
                Pool.Managed
                );
            GraphicsStream gs = o骨.vb.Lock(0, 0, 0);
            try {
                CustomVertex.PositionColored v0 = new CustomVertex.PositionColored(); v0.Color = Color.Yellow.ToArgb();
                CustomVertex.PositionColored v1 = new CustomVertex.PositionColored(); v1.Color = Color.Black.ToArgb();

                for (int t = 0; t < alaxv.Count; t++) {
                    Axv ax = alaxv[t];
                    v0.Position = ax.offset;
                    gs.Write(v0);

                    Axv px = (ax.parentIndex < 0) ? ax : alaxv[ax.parentIndex];
                    v1.Position = px.offset;
                    gs.Write(v1);

                    o骨.cntPrims++;
                }
            }
            finally {
                o骨.vb.Unlock();
            }
        }



        void calc接点Βvb() {
            if (o接点Β.vb != null) {
                o接点Β.vb.Dispose();
                o接点Β.vb = null;
            }
            o接点Β.cntVerts = 0;
            o接点Β.cntPrims = 0;

            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            o接点Β.cntVerts = alaxw.Count;
            o接点Β.prim = PrimitiveType.PointList;
            o接点Β.fvf = CustomVertex.PositionColored.Format;

            if (o接点Β.cntVerts == 0) return;

            o接点Β.vb = new VertexBuffer(
                typeof(CustomVertex.PositionColored),
                o接点Β.cntVerts,
                device,
                Usage.Points,
                o接点Β.fvf,
                Pool.Managed
                );
            GraphicsStream gs = o接点Β.vb.Lock(0, 0, 0);
            try {
                CustomVertex.PositionColored v = new CustomVertex.PositionColored();
                for (int t = 0; t < alaxw.Count; t++) {
                    Axv ax = alaxw[t];
                    v.Color = Color.Magenta.ToArgb();
                    v.Position = ax.offset;
                    gs.Write(v);

                    o接点Β.cntPrims++;
                }
            }
            finally {
                o接点Β.vb.Unlock();
            }
        }
        void calc骨Βvb() {
            if (o骨Β.vb != null) {
                o骨Β.vb.Dispose();
                o骨Β.vb = null;
            }
            o骨Β.cntVerts = 0;
            o骨Β.cntPrims = 0;

            try { device.TestCooperativeLevel(); }
            catch (DeviceLostException) { return; }
            catch (DeviceNotResetException) { return; }

            o骨Β.cntVerts = 2 * alaxw.Count;
            o骨Β.prim = PrimitiveType.LineList;
            o骨Β.fvf = CustomVertex.PositionColored.Format;

            if (o骨Β.cntVerts == 0) return;

            o骨Β.vb = new VertexBuffer(
                typeof(CustomVertex.PositionColored),
                o骨Β.cntVerts,
                device,
                Usage.Points,
                o骨Β.fvf,
                Pool.Managed
                );
            GraphicsStream gs = o骨Β.vb.Lock(0, 0, 0);
            try {
                CustomVertex.PositionColored v0 = new CustomVertex.PositionColored(); v0.Color = Color.Yellow.ToArgb();
                CustomVertex.PositionColored v1 = new CustomVertex.PositionColored(); v1.Color = Color.Black.ToArgb();

                for (int t = 0; t < alaxw.Count; t++) {
                    Axv ax = alaxw[t];
                    v0.Position = ax.offset;
                    gs.Write(v0);

                    Axv px = (ax.parentIndex < 0) ? ax : alaxw[ax.parentIndex];
                    v1.Position = px.offset;
                    gs.Write(v1);

                    o骨Β.cntPrims++;
                }
            }
            finally {
                o骨Β.vb.Unlock();
            }
        }



        class Prior {
            public Vector3 pos;

            public Prior(Vector3 pos) {
                this.pos = pos;
            }
        }

        class Quat {
            public static Quaternion calc(Vector3 v) {
                float t = 1.0f - v.X * v.X - v.Y * v.Y - v.Z * v.Z;
                float w = (t < 0) ? 0 : (float)Math.Sqrt(t);
                return new Quaternion(v.X, v.Y, v.Z, w);
            }
        }

        S4 s4 = null;

        public void init(S4 s4, Texexp texexp) {
            this.s4 = s4;
            this.texexp = texexp;
        }

        Point poslb = Point.Empty;
        Quaternion quat = Quaternion.Identity;
        Vector3 voff = Vector3.Empty;

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                poslb = new Point(e.X, e.Y);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                int dx = e.X - poslb.X;
                int dy = e.Y - poslb.Y;
                if (dx != 0 || dy != 0) {
                    poslb = new Point(e.X, e.Y);
                    if (0 != (Control.ModifierKeys & Keys.Shift)) {
                        float f = 1.0f / fact;
                        voff.Add(Vector3.TransformCoordinate(new Vector3(-dx * f, -dy * f, 0), Matrix.RotationQuaternion(Quaternion.Invert(quat))));
                    }
                    else {
                        quat.Multiply(Quaternion.RotationYawPitchRoll(dx * 0.01f, dy * 0.01f, 0));
                    }
                    devReshape(); panel1.Invalidate();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            int sel = listBox1.SelectedIndex;
            sels5 = sel;

            calcvbib(); panel1.Invalidate();

            if (sels5 >= 0) {
                listBox2.Items.Clear();
                for (int x = 0; x < s4.als5[sels5].alhive.Count; x++) {
                    Hive hive = s4.als5[sels5].alhive[x];
                    listBox2.Items.Add(string.Format("#{0:000} {1:X4} {2}", x, hive.t00, hive.al.Count));
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) {
            selhive = listBox2.SelectedIndex;

            calcvbib(); panel1.Invalidate();
        }

        private void checkBoxLighting_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void ProtForm3_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (Resource res in altex) {
                res.Dispose();
            }
            altex.Clear();
            if (vb != null) {
                vb.Dispose();
                vb = null;
            }
            if (device != null) {
                device.Dispose();
                device = null;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
#if true
            foreach (int sel in listView1.SelectedIndices) {
                if (sel < 0) return;
                Axv o = alaxv[sel];
                voff = -o.offset;

                axoff = o.offset;
                axqr = o.quat;
                calcAx3vb();

                devReshape(); panel1.Invalidate();
                break;
            }
#endif
            refreshBhint();
        }

        private void refreshBhint() {
            foreach (int sel in listView1.SelectedIndices) {
                Vone o = s4.alvr[sel];
                string s = string.Format(""
                    + "S {0,10:0.000} {1,10:0.000} {2,10:0.000} \n"
                    + "R {4,10:0.000} {5,10:0.000} {6,10:0.000} {7,10:0.000}\n"
                    + "T {8,10:0.000} {9,10:0.000} {10,10:0.000} \n---\n"
                    , o.x0, o.y0, o.z0, o.w0
                    , o.x1, o.y1, o.z1, o.w1
                    , o.x2, o.y2, o.z2, o.w2
                    );

                string r0 = "";
                string r1 = "";
                string r2 = "";
                string r3 = "";
                string r4 = "";
                string r5 = "";
                string r6 = "";
                string r7 = "";
                string r8 = "";
                U3 u3 = testMotion();
                if (u3 != null) {
                    foreach (U8 u8 in u3.alu8) {
                        if (u8.v00 == sel) {
                            switch (u8.v02) {
                                case 1: r0 = u8.v04.ToString("0.000"); break;
                                case 2: r1 = u8.v04.ToString("0.000"); break;
                                case 3: r2 = u8.v04.ToString("0.000"); break;
                                case 4: r3 = u8.v04.ToString("0.000"); break;
                                case 5: r4 = u8.v04.ToString("0.000"); break;
                                case 6: r5 = u8.v04.ToString("0.000"); break;
                                case 7: r6 = u8.v04.ToString("0.000"); break;
                                case 8: r7 = u8.v04.ToString("0.000"); break;
                                case 9: r8 = u8.v04.ToString("0.000"); break;
                            }
                        }
                    }
                }

                string q0 = "";
                string q1 = "";
                string q2 = "";
                string q3 = "";
                string q4 = "";
                string q5 = "";
                string q6 = "";
                string q7 = "";
                string q8 = "";
                if (alifApply[6 * sel + 0]) q3 = alvalApply[6 * sel + 0].ToString("0.000");
                if (alifApply[6 * sel + 1]) q4 = alvalApply[6 * sel + 1].ToString("0.000");
                if (alifApply[6 * sel + 2]) q5 = alvalApply[6 * sel + 2].ToString("0.000");
                if (alifApply[6 * sel + 3]) q6 = alvalApply[6 * sel + 3].ToString("0.000");
                if (alifApply[6 * sel + 4]) q7 = alvalApply[6 * sel + 4].ToString("0.000");
                if (alifApply[6 * sel + 5]) q8 = alvalApply[6 * sel + 5].ToString("0.000");

                s += string.Format(""
                    + "S {0,10} {1,10} {2,10} | {10,10} {11,10} {12,10}\n"
                    + "R {3,10} {4,10} {5,10} | {13,10} {14,10} {15,10}\n"
                    + "T {6,10} {7,10} {8,10} | {16,10} {17,10} {18,10}\n---\n"
                    , r0, r1, r2
                    , r3, r4, r5
                    , r6, r7, r8, 0
                    , q0, q1, q2
                    , q3, q4, q5
                    , q6, q7, q8
                    );
                labelBhint.Text = s;
                break;
            }
        }

        U1 u1 = null;

        private void buttonImportU_Click(object sender, EventArgs e) {
            u1 = Form2.u1;
            if (u1 == null) return;

            recalc();
            calcvbib();
            panel1.Invalidate();
            MessageBox.Show("ok");
        }

        private void hScrollBarAnimSel_Scroll(object sender, ScrollEventArgs e) {
            checkBoxApplyMotion_CheckedChanged(null, null);
        }

        private void checkBoxApplyMotion_CheckedChanged(object sender, EventArgs e) {
            recalc();
            calcvbib();
            panel1.Invalidate();
        }

        private void checkBoxApplyPatch_CheckedChanged(object sender, EventArgs e) {
            checkBoxApplyMotion_CheckedChanged(null, null);
        }

        private void timerStepFrame_Tick(object sender, EventArgs e) {
            int v = Math.Min(hScrollBarFrame.Maximum, hScrollBarFrame.Value + 1);
            if (v == hScrollBarFrame.Value) {
                checkBoxStartTimer.Checked = false;
                return;
            }
            hScrollBarFrame.Value = v;
            hScrollBarFrame_Scroll(null, null);
        }

        private void buttonOnce_Click(object sender, EventArgs e) {
            U3 u3 = testMotion();
            hScrollBarFrame.Minimum = 0;
            hScrollBarFrame.Maximum = 0x78;
            hScrollBarFrame.Value = 0;
            checkBoxStartTimer.Checked = true;
        }

        private void checkBoxStartTimer_CheckedChanged(object sender, EventArgs e) {
            timerStepFrame.Enabled = checkBoxStartTimer.Checked;
        }

        private void hScrollBarFrame_Scroll(object sender, ScrollEventArgs e) {
            recalc();
            calcvbib();
            panel1.Invalidate();
            try {
                panel1.Update();
            }
            catch (Exception) {
                checkBoxStartTimer.Checked = false;
                throw;
            }
        }

        private void checkBoxShow2_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void hScrollBarrx_Scroll(object sender, ScrollEventArgs e) {

        }

        System.Collections.BitArray alifApply = null;
        float[] alvalApply = null;

        private void buttonResetApply_Click(object sender, EventArgs e) {
            alifApply = null;

            recalc(); calcvbib(); panel1.Invalidate();
        }

        private void textBoxApplier_KeyDown(object sender, KeyEventArgs e) {
            int ax = 0;
            int add = 0;
            switch (e.KeyCode) {
                case Keys.NumPad1: ax = 0; add = -1; break;
                case Keys.NumPad7: ax = 0; add = +1; break;
                case Keys.NumPad2: ax = 1; add = -1; break;
                case Keys.NumPad8: ax = 1; add = +1; break;
                case Keys.NumPad3: ax = 2; add = -1; break;
                case Keys.NumPad9: ax = 2; add = +1; break;
            }
            if (add != 0) {
                foreach (int bi in listView1.SelectedIndices) {
                    int i = 6 * bi + ax;
                    float curval;
                    if (alifApply[i]) {
                        curval = alvalApply[i];
                    }
                    else if ((uint)bi < (uint)alvrlast.Count) {
                        switch (ax) {
                            default:
                            case 0: curval = alvrlast[bi].x1; break;
                            case 1: curval = alvrlast[bi].y1; break;
                            case 2: curval = alvrlast[bi].z1; break;
                            case 3: curval = alvrlast[bi].x2; break;
                            case 4: curval = alvrlast[bi].y2; break;
                            case 5: curval = alvrlast[bi].z2; break;
                        }
                        alifApply[i] = true;
                    }
                    else {
                        break;
                    }

                    alvalApply[i] = curval + add * (6.28f / 36);

                    listView1_SelectedIndexChanged(null, null); // refreshBhint();
                    recalc(); calcvbib(); panel1.Invalidate();
                    break;
                }
            }
        }

        private void buttonUsebin_Click(object sender, EventArgs e) {
            if (!File.Exists(openFileDialogUsebin.FileName))
                openFileDialogUsebin.ShowDialog();
            if (!File.Exists(openFileDialogUsebin.FileName))
                return;

            using (FileStream si = File.OpenRead(openFileDialogUsebin.FileName)) {
                BinaryReader br = new BinaryReader(si);
                for (int t = 0; t < alaxv.Count; t++) {
                    float x0 = br.ReadSingle();
                    float y0 = br.ReadSingle();
                    float z0 = br.ReadSingle();
                    int w0 = br.ReadInt32();
                    float x1 = br.ReadSingle();
                    float y1 = br.ReadSingle();
                    float z1 = br.ReadSingle();
                    int w1 = br.ReadInt32();
                    float x2 = br.ReadSingle();
                    float y2 = br.ReadSingle();
                    float z2 = br.ReadSingle();
                    int w2 = br.ReadInt32();
                    float x3 = br.ReadSingle();
                    float y3 = br.ReadSingle();
                    float z3 = br.ReadSingle();
                    int w3 = br.ReadInt32();

                    alifApply[6 * t + 0] = true; alvalApply[6 * t + 0] = x2;
                    alifApply[6 * t + 1] = true; alvalApply[6 * t + 1] = y2;
                    alifApply[6 * t + 2] = true; alvalApply[6 * t + 2] = z2;
                    alifApply[6 * t + 3] = true; alvalApply[6 * t + 3] = x3;
                    alifApply[6 * t + 4] = true; alvalApply[6 * t + 4] = y3;
                    alifApply[6 * t + 5] = true; alvalApply[6 * t + 5] = z3;
                }
            }

            recalc(); calcvbib(); panel1.Invalidate();
            MessageBox.Show("Use ok");
        }

        private void checkBoxUseik_CheckedChanged(object sender, EventArgs e) {
            recalc(); calcvbib(); panel1.Invalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            int sel = (int)numericUpDown1.Value;
            if ((uint)alaxw.Count <= (uint)sel) return;
            Axv o = alaxw[sel];
            voff = -o.offset;

            axoff = o.offset;
            axqr = o.quat;
            calcAx3vb();

            devReshape(); panel1.Invalidate();
        }

        private void textBoxApplier_TextChanged(object sender, EventArgs e) {

        }

        private void buttonCopyik_Click(object sender, EventArgs e) {
            StringBuilder s = new StringBuilder();

            List<Vone> alvr = alvrlast;

            for (int t = 0; t < alvr.Count; t++) {
                s.AppendFormat("alOrg.Add(new Bone(new Vector3({0}), new Quaternion({1}), {2}));\n"
                    , VUtil.toStr(new Vector3(alvr[t].x2, alvr[t].y2, alvr[t].z2))
                    , VUtil.rxryrzToStr(alvr[t].x1, alvr[t].y1, alvr[t].z1)
                    , alvr[t].Parent
                    );
            }
            U3 u3 = testMotion();
            for (int t = 0; t < u3.alu9.Count; t++) {
                U9 u9 = u3.alu9[t];
                s.AppendFormat("alOrg2.Add(new Bone(new Vector3({0}), new Quaternion({1}), {2}));\n"
                    , VUtil.toStr(new Vector3(u9.v1c, u9.v20, u9.v24))
                    , VUtil.rxryrzToStr(u9.v10, u9.v14, u9.v18)
                    , u9.v0c
                    );
            }

            s.Replace("\n", "\r\n");
            Clipboard.SetText(s.ToString());
            MessageBox.Show("copy ok");
        }

        class VUtil {
            public static string toStr(Vector3 v) {
                return string.Format("{0}f, {1}f, {2}f"
                    , v.X, v.Y, v.Z
                    );
            }
            public static string toStr(Quaternion v) {
                return string.Format("{0}f, {1}f, {2}f, {3}f"
                    , v.X, v.Y, v.Z, v.W
                    );
            }
            public static string rxryrzToStr(float rx, float ry, float rz) {
                Quaternion q = Quaternion.Identity;
                q.Multiply(Quaternion.RotationAxis(new Vector3(1, 0, 0), rx));
                q.Multiply(Quaternion.RotationAxis(new Vector3(0, 1, 0), ry));
                q.Multiply(Quaternion.RotationAxis(new Vector3(0, 0, 1), rz));
                return toStr(q);
            }
        }

    }
}
