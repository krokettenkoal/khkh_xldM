using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using parseSEQDv2.Models.D3D;
using System.IO;
using SlimDX;

namespace parseSEQDv2.Controls.D3D {
    public partial class D3DDevicePanel : UserControl {
        private Direct3D direct3D = new Direct3D();

        private Device device = null;

        private List<Texture> textureList = new List<Texture>();

        private int renderLock = 0;

        private CustomMesh[] meshes = new CustomMesh[0];

        public D3DDevicePanel() {
            Disposed += D3DDevicePanel_Disposed;
            InitializeComponent();
        }

        private void D3DDevicePanel_Disposed(object sender, EventArgs e) {
            foreach (var disposable in textureList) {
                disposable.Dispose();
            }
            textureList.Clear();

            direct3D?.Dispose();
            direct3D = null;

            device?.Dispose();
            device = null;
        }

        private void D3DDevicePanel_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }

            SetStyle(ControlStyles.UserPaint, true); // avoid flicker
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // avoid flicker
            SetStyle(ControlStyles.Opaque, true); // avoid flicker
            SetStyle(ControlStyles.ResizeRedraw, true); // full scene rendering is required on resize

            device = new Device(
                direct3D: direct3D,
                adapter: 0,
                deviceType: DeviceType.Hardware,
                controlHandle: this.Handle,
                createFlags: CreateFlags.SoftwareVertexProcessing,
                presentParameters: CurrentPresentParameters
            );
        }

        public int MaximumVertexCount => 1000;

        public void AddBitmapToTextureList(Bitmap bitmap) {
            if (bitmap == null) {
                throw new ArgumentNullException("bitmap");
            }

            using (MemoryStream stream = new MemoryStream()) {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;
                textureList.Add(Texture.FromStream(device, stream, Usage.None, Pool.Managed));
            }
        }

        PresentParameters CurrentPresentParameters {
            get {
                var it = new PresentParameters {
                    BackBufferCount = 1,
                    BackBufferHeight = ClientSize.Height,
                    BackBufferWidth = ClientSize.Width,
                    EnableAutoDepthStencil = true,
                    AutoDepthStencilFormat = Format.D24X8,
                    SwapEffect = SwapEffect.Discard
                };
                return it;
            }
        }

        public CustomMesh[] CustomMeshesForRendering {
            get {
                return meshes;
            }
            set {
                meshes = value ?? new CustomMesh[0];
                Invalidate();
            }
        }

        public bool PS2ScreenSize { get; set; }

        private void D3DDevicePanel_Resize(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }
            if (device != null) {
                device.Reset(CurrentPresentParameters);
            }
        }

        private void D3DDevicePanel_Paint(object sender, PaintEventArgs e) {
            if (DesignMode) {
                return;
            }

            if (device == null) {
                return;
            }
            if (device.TestCooperativeLevel().IsFailure) {
                return;
            }
            if (renderLock != 0) {
                return;
            }

            ++renderLock;

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

                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.Modulate2X);
                device.SetTextureStageState(0, TextureStage.ColorArg0, TextureArgument.Diffuse);
                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate2X);
                device.SetTextureStageState(0, TextureStage.AlphaArg0, TextureArgument.Diffuse);
                device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);

                device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.Modulate2X);
                device.SetTextureStageState(1, TextureStage.ColorArg0, TextureArgument.Current);
                device.SetTextureStageState(1, TextureStage.ColorArg1, TextureArgument.Constant);
                device.SetTextureStageState(1, TextureStage.AlphaOperation, TextureOperation.Modulate2X);
                device.SetTextureStageState(1, TextureStage.AlphaArg0, TextureArgument.Current);
                device.SetTextureStageState(1, TextureStage.AlphaArg1, TextureArgument.Constant);

                if (PS2ScreenSize) {
                    var projectionMatrix = Matrix.Identity
                        * Matrix.Translation(-256, -209, 0)
                        * Matrix.OrthoLH(512, -418, -100, +100)
                        ;
                    device.SetTransform(TransformState.Projection, projectionMatrix);

                }
                else {
                    var projectionMatrix = Matrix.OrthoLH(ClientSize.Width, -ClientSize.Height, -100, +100);
                    device.SetTransform(TransformState.Projection, projectionMatrix);
                }
                device.SetRenderState(RenderState.ZFunc, Compare.Always);

                if (meshes != null) {
                    for (int x = 0; x < meshes.Length; x++) {
                        var mesh = meshes[x];
                        if (mesh.indices.Count == 0 || mesh.vertices.Count == 0 || mesh.countTriangles == 0) {
                            continue;
                        }
                        device.SetTexture(0, textureList[mesh.textureIndex]);
                        device.SetTextureStageState(1, TextureStage.Constant, mesh.constantColor);

                        device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
                        device.DrawIndexedUserPrimitives<int, CustomVertex.PositionColoredTextured>(
                            PrimitiveType.TriangleList,
                            0,
                            0,
                            0,
                            mesh.vertices.Count,
                            mesh.countTriangles,
                            mesh.indices.ToArray(),
                            Format.Index32,
                            mesh.vertices.ToArray(),
                            CustomVertex.PositionColoredTextured.StrideSize
                            );
                    }
                }
            }

            device.EndScene();
            device.Present();

            --renderLock;
        }
    }
}
