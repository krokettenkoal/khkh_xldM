using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using System.IO;
using khiiMapv;
using SlimDX;

namespace parseSEQD {
    public partial class ThreeD : UserControl {
        public ThreeD() {
            this.Disposed += new EventHandler(ThreeD_Disposed);
            InitializeComponent();
        }

        void ThreeD_Disposed(object sender, EventArgs e) {
            Cleartex();

            if (d3d != null) d3d.Dispose();
            d3d = null;

            if (device != null) device.Dispose();
            device = null;

            if (vb != null) vb.Dispose();
            vb = null;
        }

        private void ThreeD_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            device = new Device(d3d, 0, DeviceType.Hardware, this.Handle, CreateFlags.SoftwareVertexProcessing, PP);

            vb = new VertexBuffer(
                device,
                1000 * CustomVertex.PositionColoredTextured.StrideSize,
                Usage.Points,
                CustomVertex.PositionColoredTextured.Format,
                Pool.Managed
                );
        }

        public void Cleartex() {
            foreach (Texture o in alt) {
                o.Dispose();
            }
            alt.Clear();
        }

        public void Addtex(Bitmap pic) {
            if (pic == null) throw new ArgumentNullException("pic");

            using (MemoryStream os = new MemoryStream()) {
                pic.Save(os, System.Drawing.Imaging.ImageFormat.Png);
                os.Position = 0;
                alt.Add(Texture.FromStream(device, os, Usage.None, Pool.Managed));
            }
        }

        PresentParameters PP {
            get {
                PresentParameters pp = new PresentParameters();
                pp.BackBufferCount = 1;
                pp.BackBufferHeight = ClientSize.Height;
                pp.BackBufferWidth = ClientSize.Width;
                pp.EnableAutoDepthStencil = true;
                pp.AutoDepthStencilFormat = Format.D24X8;
                pp.SwapEffect = SwapEffect.Discard;
                return pp;
            }
        }

        [NonSerialized()]
        Direct3D d3d = new Direct3D();

        [NonSerialized()]
        Device device = null;

        [NonSerialized()]
        VertexBuffer vb = null;

        [NonSerialized()]
        List<Texture> alt = new List<Texture>();

        int lockp = 0;

        private void ThreeD_Paint(object sender, PaintEventArgs e) {
            if (DesignMode) {
                return;
            }

            if (device == null)
                return;
            if (device.TestCooperativeLevel().IsFailure)
                return;
            if (lockp != 0)
                return;
            ++lockp;

            device.BeginScene();
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, this.BackColor, 1.0f, 0);
            {
                device.SetRenderState(RenderState.CullMode, Cull.None);

                device.SetRenderState(RenderState.AlphaFunc, Compare.GreaterEqual);
                device.SetRenderState(RenderState.AlphaRef, 1);
                device.SetRenderState(RenderState.AlphaTestEnable, true);

                device.SetRenderState(RenderState.ColorVertex, true);
                device.SetRenderState(RenderState.Lighting, false);

                device.SetRenderState(RenderState.AlphaBlendEnable, true);
                device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
                device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
                device.SetRenderState(RenderState.DestinationBlendAlpha, Blend.InverseSourceAlpha);

                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.Modulate);
                device.SetTextureStageState(0, TextureStage.ColorArg0, TextureArgument.Diffuse);
                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
                device.SetTextureStageState(0, TextureStage.AlphaArg0, TextureArgument.Diffuse);
                device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);

                device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.Modulate);
                device.SetTextureStageState(1, TextureStage.ColorArg0, TextureArgument.Current);
                device.SetTextureStageState(1, TextureStage.ColorArg1, TextureArgument.Constant);
                device.SetTextureStageState(1, TextureStage.AlphaOperation, TextureOperation.Modulate);
                device.SetTextureStageState(1, TextureStage.AlphaArg0, TextureArgument.Current);
                device.SetTextureStageState(1, TextureStage.AlphaArg1, TextureArgument.Constant);

                device.SetTransform(TransformState.Projection, Matrix.OrthoLH(ClientSize.Width, -ClientSize.Height, -100, +100));
                device.SetRenderState(RenderState.ZFunc, Compare.Always);

                if (altm != null) {
                    for (int x = 0; x < altm.Length; x++) {
                        Trimesh m = altm[x];
                        if (m.ali.Count == 0 || m.alv.Count == 0 || m.cntTris == 0) continue;
                        device.SetTexture(0, alt[m.texi]);
                        device.SetTextureStageState(1, TextureStage.Constant, m.constantColor);

                        device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
                        device.DrawIndexedUserPrimitives<int, CustomVertex.PositionColoredTextured>(
                            PrimitiveType.TriangleList,
                            0,
                            0,
                            0,
                            m.alv.Count,
                            m.cntTris,
                            m.ali.ToArray(),
                            Format.Index32,
                            m.alv.ToArray(),
                            CustomVertex.PositionColoredTextured.StrideSize
                            );
                    }
                }
            }
            device.EndScene();
            device.Present();

            --lockp;
        }

        public Trimesh[] Trimeshes { get { return altm; } set { altm = value; Invalidate(); } }

        Trimesh[] altm = null;

        public class Trimesh {
            public int texi = 0;
            public int cntTris = 0;
            public int constantColor = -1;
            public List<CustomVertex.PositionColoredTextured> alv = new List<CustomVertex.PositionColoredTextured>();
            public List<int> ali = new List<int>();
        }

        private void ThreeD_Resize(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            if (device != null) {
                device.Reset(PP);
            }
        }
    }
}
