using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hex04BinTrack;
using System.IO;
using System.Diagnostics;
using SlimDX.Direct3D9;
using SlimDX;
using khiiMapv.Put;
using khiiMapv.Models.Coct;
using khiiMapv.Models;
using System.Linq;
using khiiMapv.Utils;
using khiiMapv.Q3Radiant;

namespace khiiMapv {
    public partial class Visf : Form {
        public Visf() {
            InitializeComponent();
        }
        public Visf(List<DContext> dcList, List<CollisionSet> collisionSets, DoctReader doct) {
            this.dcList = dcList;
            this.collisionSets = collisionSets;
            this.doct = doct;
            InitializeComponent();
        }

        private readonly List<DContext> dcList = null;
        private readonly List<CollisionSet> collisionSets;
        private readonly DoctReader doct;
        private Device device;
        private int[][] occlusionCullingVifpktList = null;
        private int maxVifPkts = -1;

        class ContextInfo {
            /// <summary>
            /// Sets of 3 indices, in order to render one triangle
            /// </summary>
            public uint[] tripletIndices;

            public int texIndex;
            public int packetIndex;

            public bool shadow;
        }

        class DContextRef {
            public List<ContextInfo> contextInfoList = new List<ContextInfo>();
            public DContext dc;
        }

        List<DContextRef> dcRefList = new List<DContextRef>();
        List<Texture> texList = new List<Texture>();

        Direct3D p3D;

        class LMap {
            public byte[] al = new byte[256];

            public LMap() {
                for (int x = 0; x < 256; x++) {
                    al[x] = (byte)Math.Min(255, 2 * x);
                }
            }
        }

        LMap lm = new LMap();

        PresentParameters PP {
            get {
                PresentParameters pp = new PresentParameters();
                pp.Windowed = true;
                pp.SwapEffect = SwapEffect.Discard;
                pp.AutoDepthStencilFormat = Format.D24X8;
                pp.EnableAutoDepthStencil = true;
                pp.BackBufferHeight = 1024;
                pp.BackBufferWidth = 1024;
                return pp;
            }
        }

        private void p1_Load(object sender, EventArgs e) {
            p3D = new Direct3D();
            disposer.Add(p3D);
            device = new Device(p3D, 0, DeviceType.Hardware, p1.Handle, CreateFlags.HardwareVertexProcessing, PP);
            disposer.Add(device);

            device.SetRenderState(RenderState.Lighting, false);
            device.SetRenderState(RenderState.ZEnable, true);

            device.SetRenderState(RenderState.AlphaTestEnable, true);
            device.SetRenderState(RenderState.AlphaRef, 2);
            device.SetRenderState<Compare>(RenderState.AlphaFunc, Compare.GreaterEqual);

            device.SetRenderState(RenderState.AlphaBlendEnable, true);
            device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
            device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
            device.SetRenderState(RenderState.DestinationBlendAlpha, Blend.InverseSourceAlpha);

            device.SetRenderState<Cull>(RenderState.CullMode, Cull.Counterclockwise);

            device.SetRenderState(RenderState.FogColor, p1.BackColor.ToArgb());
            device.SetRenderState(RenderState.FogStart, 5.0f);
            device.SetRenderState(RenderState.FogEnd, 30000.0f);
            device.SetRenderState(RenderState.FogDensity, 0.0001f);
            device.SetRenderState<FogMode>(RenderState.FogVertexMode, FogMode.Exponential);

            p1.MouseWheel += new MouseEventHandler(p1_MouseWheel);

            List<CustomVertex.PositionColoredTextured> wholeVertices = new List<CustomVertex.PositionColoredTextured>();

            foreach (DContext dContext in dcList) {
                ToolStripButton tsiIfRender = new ToolStripButton(dContext.name);
                tsiIfRender.DisplayStyle = ToolStripItemDisplayStyle.Text;
                tsiIfRender.CheckOnClick = true;
                tsiIfRender.Tag = dContext;
                tsiIfRender.Checked = dContext.initialVisible;
                tsiIfRender.CheckedChanged += new EventHandler(tsiIfRender_CheckedChanged);

                toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(tspModels), tsiIfRender);
            }

            foreach (var coll in collisionSets) {
                ToolStripButton tsiRenderColl = new ToolStripButton(coll.name);
                tsiRenderColl.DisplayStyle = ToolStripItemDisplayStyle.Text;
                tsiRenderColl.CheckOnClick = true;
                tsiRenderColl.Tag = coll;
                tsiRenderColl.Checked = false;
                tsiRenderColl.CheckedChanged += TsiRenderColl_CheckedChanged;

                toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(tspCollisions), tsiRenderColl);
            }

            dcRefList.Clear();
            texList.Clear();
            int[] ringBuff = new int[4];
            int ringIdx = 0;
            foreach (DContext dc in dcList) {
                var dcRef = new DContextRef {
                    dc = dc,
                };
                int baseTexIdx = texList.Count;
                foreach (Bitmap pic in dc.o7.pics) {
                    MemoryStream os = new MemoryStream();
                    pic.Save(os, System.Drawing.Imaging.ImageFormat.Png);
                    os.Position = 0;
                    Texture t;
                    texList.Add(t = Texture.FromStream(device, os));
                    disposer.Add(t);
                }
                if (dc.o4Mdlx != null) {
                    foreach (KeyValuePair<int, Parse4Mdlx.Model> pair in dc.o4Mdlx.dictModel) {
                        ContextInfo contextInfo = new ContextInfo();
                        int baseIdx = wholeVertices.Count;

                        Parse4Mdlx.Model model = pair.Value;
                        wholeVertices.AddRange(model.alv);

                        List<uint> tripletList = new List<uint>();
                        for (int x = 0; x < model.alv.Count; x++) {
                            tripletList.Add((uint)(baseIdx + x));
                        }

                        contextInfo.tripletIndices = tripletList.ToArray();
                        contextInfo.texIndex = baseTexIdx + pair.Key;
                        contextInfo.packetIndex = 0;
                        dcRef.contextInfoList.Add(contextInfo);
                    }
                    foreach (KeyValuePair<int, Parse4Mdlx.Model> pair in dc.o4Mdlx.dictShadow) {
                        ContextInfo contextInfo = new ContextInfo();
                        int baseIdx = wholeVertices.Count;

                        Parse4Mdlx.Model model = pair.Value;
                        wholeVertices.AddRange(model.alv);

                        List<uint> tripletList = new List<uint>();
                        for (int x = 0; x < model.alv.Count; x++) {
                            tripletList.Add((uint)(baseIdx + x));
                        }

                        contextInfo.shadow = true;
                        contextInfo.tripletIndices = tripletList.ToArray();
                        contextInfo.texIndex = baseTexIdx + pair.Key;
                        contextInfo.packetIndex = 0;
                        dcRef.contextInfoList.Add(contextInfo);
                    }
                }
                else if (dc.o4Map != null) {
                    var doctDecisionTarget = ReferenceEquals(dc, dcList.First());
                    if (doctDecisionTarget) {
                        occlusionCullingVifpktList = dc.o4Map.alb2.ToArray();
                        maxVifPkts = dc.o4Map.alvifpkt.Count;
                    }

                    for (var packetIndex = 0; packetIndex < dc.o4Map.alvifpkt.Count; packetIndex++) {
                        {
                            Vifpli vifRef = dc.o4Map.alvifpkt[packetIndex];
                            byte[] vifpkt = vifRef.vifpkt;
                            VU1Mem memo = new VU1Mem();
                            ParseVIF1 pv1 = new ParseVIF1(memo);
                            pv1.Parse(new MemoryStream(vifpkt, false), 0x00);
                            foreach (byte[] ram in pv1.almsmem) {
                                ContextInfo ci = new ContextInfo();

                                MemoryStream si = new MemoryStream(ram, false);
                                BinaryReader br = new BinaryReader(si);

                                br.ReadInt32();
                                br.ReadInt32();
                                br.ReadInt32();
                                br.ReadInt32();
                                int v10 = br.ReadInt32(); // v10: cnt tex&vi&flg verts
                                int v14 = br.ReadInt32(); // v14: off tex&vi&flg verts
                                br.ReadInt32();
                                br.ReadInt32();
                                int v20 = br.ReadInt32(); // v20: cnt clr verts
                                int v24 = br.ReadInt32(); // v24: off clr verts
                                br.ReadInt32();
                                br.ReadInt32();
                                int v30 = br.ReadInt32(); // v30: cnt pos vert
                                int v34 = br.ReadInt32(); // v34: off pos vert

                                List<uint> altris = new List<uint>();
                                int baseIdx = wholeVertices.Count;
                                for (int i = 0; i < v10; i++) {
                                    si.Position = 16 * (v14 + i);
                                    int tx = br.ReadInt16(); br.ReadInt16();
                                    int ty = br.ReadInt16(); br.ReadInt16();
                                    int vi = br.ReadInt16(); br.ReadInt16();
                                    int fl = br.ReadInt16(); br.ReadInt16();
                                    si.Position = 16 * (v34 + vi);
                                    Vector3 v3;
                                    v3.X = -br.ReadSingle();
                                    v3.Y = +br.ReadSingle();
                                    v3.Z = +br.ReadSingle();

                                    si.Position = 16 * (v24 + i);
                                    int fR = (byte)br.ReadUInt32();
                                    int fG = (byte)br.ReadUInt32();
                                    int fB = (byte)br.ReadUInt32();
                                    int fA = (byte)br.ReadUInt32();

                                    if (v24 == 0) {
                                        fR = 255;
                                        fG = 255;
                                        fB = 255;
                                        fA = 255;
                                    }

                                    ringBuff[ringIdx & 3] = baseIdx + i;
                                    ringIdx++;

                                    //Debug.WriteLine("# " + fl.ToString("x2"));

                                    if (fl == 0x00) {
                                    }
                                    else if (fl == 0x10) {
                                    }
                                    else if (fl == 0x20) {
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 1) & 3]));
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 2) & 3]));
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 3) & 3]));
                                    }
                                    else if (fl == 0x30) {
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 1) & 3]));
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 3) & 3]));
                                        altris.Add(Convert.ToUInt32(ringBuff[(ringIdx - 2) & 3]));
                                    }

                                    Color clr = Color.FromArgb(lm.al[fA], lm.al[fR], lm.al[fG], lm.al[fB]);

                                    CustomVertex.PositionColoredTextured cv = new CustomVertex.PositionColoredTextured(
                                        v3,
                                        clr.ToArgb(),
                                        +tx / 16.0f / 256.0f,
                                        +ty / 16.0f / 256.0f
                                        );
                                    wholeVertices.Add(cv);
                                }

                                ci.tripletIndices = altris.ToArray();
                                ci.texIndex = baseTexIdx + vifRef.texi;
                                ci.packetIndex = packetIndex;
                                dcRef.contextInfoList.Add(ci);
                            }
                        }
                    }
                }
                dcRefList.Add(dcRef);
            }

            if (wholeVertices.Count == 0) {
                wholeVertices.Add(new CustomVertex.PositionColoredTextured());
            }

            {
                vb = new VertexBuffer(
                    device,
                    (cntVerts = wholeVertices.Count) * CustomVertex.PositionColoredTextured.Size,
                    Usage.Points,
                    CustomVertex.PositionColoredTextured.Format,
                    Pool.Managed
                    );
                disposer.Add(vb);
                DataStream gs = vb.Lock(0, 0, LockFlags.None);
                try {
                    foreach (CustomVertex.PositionColoredTextured v3 in wholeVertices) {
                        gs.Write(v3);
                    }
                }
                finally {
                    vb.Unlock();
                }
            }

            lCntVert.Text = cntVerts.ToString("#,##0");

            int cntIdx = 0;

            refIdxBufList.Clear();
            int numOf = 0;
            foreach (DContextRef dcRef in dcRefList) {
                foreach (ContextInfo contextInfo in dcRef.contextInfoList) {
                    if (contextInfo.tripletIndices.Length != 0) {
                        IndexBuffer idxBuf = new IndexBuffer(
                            device,
                            4 * contextInfo.tripletIndices.Length,
                            Usage.None,
                            Pool.Managed,
                            false
                            );
                        cntIdx += contextInfo.tripletIndices.Length;
                        disposer.Add(idxBuf);
                        DataStream gs = idxBuf.Lock(0, 0, LockFlags.None);
                        try {
                            foreach (uint i in contextInfo.tripletIndices) {
                                gs.Write(i);
                            }
                        }
                        finally {
                            idxBuf.Unlock();
                        }
                        RefIdxBuf refIdxBuf = new RefIdxBuf();
                        refIdxBuf.idxBuf = idxBuf;
                        refIdxBuf.idxCount = contextInfo.tripletIndices.Length;
                        refIdxBuf.texIdx = contextInfo.texIndex;
                        refIdxBuf.packetIdx = contextInfo.packetIndex;
                        refIdxBuf.name = dcList[numOf].name;
                        refIdxBuf.dc = dcRef.dc;
                        refIdxBuf.shadow = contextInfo.shadow;
                        refIdxBufList.Add(refIdxBuf);
                    }
                    else {
                        RefIdxBuf refIdxBuf = new RefIdxBuf();
                        refIdxBuf.idxBuf = null;
                        refIdxBuf.idxCount = 0;
                        refIdxBuf.texIdx = contextInfo.texIndex;
                        refIdxBuf.packetIdx = contextInfo.packetIndex;
                        refIdxBuf.name = dcList[numOf].name;
                        refIdxBuf.dc = dcRef.dc;
                        refIdxBufList.Add(refIdxBuf);
                    }
                }
                numOf++;
            }

            lCntTris.Text = (cntIdx / 3).ToString("#,##0");

            Console.Write("");
        }

        private void TsiRenderColl_CheckedChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        void tsiIfRender_CheckedChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        TempMesh hitBoxList = new TempMesh();
        TempMesh showCoBBoxes = new TempMesh();

        List<ComObject> disposer = new List<ComObject>();

        void p1_MouseWheel(object sender, MouseEventArgs e) {
            fov.Value = (Decimal)Math.Max(
                Convert.ToSingle(fov.Minimum),
                Math.Min(
                    Convert.ToSingle(fov.Maximum),
                    Math.Max(
                        1.0f,
                        Convert.ToSingle(fov.Value) + e.Delta / 200.0f
                        )
                    )
                );
            p1.Invalidate();
        }

        class RefIdxBuf {
            public IndexBuffer idxBuf;
            /// <summary>
            /// idxCount = 3 * numTris
            /// </summary>
            public int idxCount;
            public int texIdx;
            public int packetIdx;
            public string name = "";

            /// <summary>
            /// needed to choose rendering on/off
            /// </summary>
            public DContext dc;

            public bool shadow;
        }

        List<RefIdxBuf> refIdxBufList = new List<RefIdxBuf>();
        VertexBuffer vb;
        int cntVerts = 0;

        Vector3 CameraEye {
            get {
                return new Vector3(Convert.ToSingle(eyeX.Value), Convert.ToSingle(eyeY.Value), Convert.ToSingle(eyeZ.Value));
            }
            set {
                eyeX.Value = Math.Max(eyeX.Minimum, Math.Min(eyeX.Maximum, (decimal)value.X));
                eyeY.Value = Math.Max(eyeY.Minimum, Math.Min(eyeY.Maximum, (decimal)value.Y));
                eyeZ.Value = Math.Max(eyeZ.Minimum, Math.Min(eyeZ.Maximum, (decimal)value.Z));
            }
        }

        Vector3 Target {
            get {
                return Vector3.TransformCoordinate(
                    Vector3.UnitX,
                    Matrix.RotationYawPitchRoll(
                        Convert.ToSingle(yaw.Value) / 180.0f * 3.14159f,
                        Convert.ToSingle(pitch.Value) / 180.0f * 3.14159f,
                        Convert.ToSingle(roll.Value) / 180.0f * 3.14159f
                        )
                    );
            }
        }

        Vector3 CameraUp {
            get {
                return Vector3.TransformCoordinate(
                    Vector3.UnitY,
                    Matrix.RotationYawPitchRoll(
                        0,
                        Convert.ToSingle(pitch.Value) / 180.0f * 3.14159f,
                        0
                    )
                );
            }
        }

        Vector3 TargetX {
            get {
                return Vector3.TransformCoordinate(
                    Vector3.UnitX,
                    Matrix.RotationYawPitchRoll(
                        Convert.ToSingle(yaw.Value) / 180.0f * 3.14159f,
                        0,
                        0
                        )
                    );
            }
        }

        private void p1_Paint(object sender, PaintEventArgs e) {
            Size size = p1.ClientSize;
            float aspect = (size.Height != 0) ? size.Width / (float)size.Height : 0;

            // http://reply.mydns.jp/reply/2009/07/visual-basic2008-slimdx-game-no4.html

            var cameraEye = CameraEye;

            device.SetTransform(TransformState.World, Matrix.Identity);
            device.SetTransform(TransformState.View, Matrix.LookAtLH(
                cameraEye,
                cameraEye + Target,
                CameraUp
                ));
            device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(
                Convert.ToSingle(fov.Value) / 180.0f * 3.14159f,
                aspect,
                Convert.ToSingle(50),
                Convert.ToSingle(5000000)
                ));

            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, p1.BackColor, 1, 0);
            device.BeginScene();

            DoctComputer doctComputed = null;
            var visibleVifPkts = new HashSet<int>();
            var enableDoct = tsbEnableDoct.Checked;

            if (doct != null && occlusionCullingVifpktList != null && occlusionCullingVifpktList.Length >= 1) {
                doctComputed = new DoctComputer(doct, new Plane(-cameraEye, -Target));

                foreach (var table2Idx in doctComputed.containedTable2Idx) {
                    foreach (var vifPktIdx in occlusionCullingVifpktList[table2Idx]) {
                        visibleVifPkts.Add(vifPktIdx);
                    }
                }

                tslDoctInfo.Text = $"Visible vifpkt group count: {doctComputed.containedTable2Idx.Count:#,##0}/{occlusionCullingVifpktList.Length:#,##0}, Visible num vifpkts: {visibleVifPkts.Count:#,##0}/{maxVifPkts:#,##0}";
            }

            var renderDContextList = GetCheckedDContextList()
                .ToArray();

            {
                device.SetTextureStageState(0, TextureStage.ColorOperation, (cbVertexColor.Checked) ? TextureOperation.Modulate : TextureOperation.SelectArg1);
                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Diffuse);

                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
                device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);

                device.SetRenderState(RenderState.FogEnable, cbFog.Checked);

                if (vb != null) {
                    var displayVifpkt = (int)numVifpkt.Value;
                    device.SetStreamSource(0, vb, 0, CustomVertex.PositionColoredTextured.Size);
                    device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
                    foreach (RefIdxBuf refIdxBuf in refIdxBufList) {
                        if (true
                            && refIdxBuf.idxBuf != null
                            && renderDContextList.Contains(refIdxBuf.dc)
                            && (refIdxBuf.shadow ? tsbRenderShadow.Checked : tsbRenderModel.Checked)
                            && (false
                                || !enableDoct
                                || refIdxBuf.name != "MAP"
                                || visibleVifPkts.Contains(refIdxBuf.packetIdx)
                            )
                            && (false
                                || refIdxBuf.name != "MAP"
                                || displayVifpkt == -1
                                || refIdxBuf.packetIdx == displayVifpkt
                            )
                        ) {
                            device.SetRenderState(RenderState.FogEnable, cbFog.Checked && refIdxBuf.name.Equals("MAP"));

                            if (refIdxBuf.shadow) {
                                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg2);
                                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.SelectArg2);
                            }
                            else {
                                device.SetTextureStageState(0, TextureStage.ColorOperation, (cbVertexColor.Checked) ? TextureOperation.Modulate : TextureOperation.SelectArg1);
                                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
                            }

                            device.Indices = refIdxBuf.idxBuf;
                            device.SetTexture(0, texList[refIdxBuf.texIdx & 65535]);
                            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cntVerts, 0, refIdxBuf.idxCount / 3);
                        }
                    }
                }
            }

            var checkedCollisionSets = GetCheckedCollisionSet()
                .ToArray();
            if (checkedCollisionSets.Any()) {
                triRender.Clear();

                var displayPlane = tsbPlane.Checked;

                var yellow = Color.Yellow.ToArgb();

                foreach (var item in checkedCollisionSets) {
                    var collision = item.collision;

                    if (displayPlane) {
                        foreach (Co2 co2 in collision.alCo2) {
                            for (int x = co2.Co3frm; x < co2.Co3to; x++) {
                                var co3 = collision.alCo3[x];
                                var clip = collision.alCo5[co3.PlaneCo5];

                                var bbox = co2.BBox;

                                if (co3.Co6 != -1) {
                                    bbox = collision.alCo6[co3.Co6].BBox;
                                }

                                var invertedClip = new Plane(clip.Normal, -clip.D);

                                var north = new Plane(0, 0, 1, bbox.Maximum.Z);
                                var south = new Plane(0, 0, -1, -bbox.Minimum.Z);
                                var up = new Plane(0, 1, 0, bbox.Maximum.Y);
                                var down = new Plane(0, -1, 0, -bbox.Minimum.Y);
                                var east = new Plane(1, 0, 0, bbox.Maximum.X);
                                var west = new Plane(-1, 0, 0, -bbox.Minimum.X);

                                var clipPoly = WindingUtil.MakeFaceWinding(
                                    new Plane[] { north, south, up, down, east, west, invertedClip },
                                    invertedClip
                                );

                                if (clipPoly != null) {
                                    for (int t = 0; t < clipPoly.Count - 2; t++) {
                                        triRender.AddTri(clipPoly[0], clipPoly[1 + t], clipPoly[2 + t], yellow);
                                    }
                                }
                            }
                        }
                    }
                    else {
                        foreach (Co2 co2 in collision.alCo2) {
                            for (int x = co2.Co3frm; x < co2.Co3to; x++) {
                                Co3 co3 = collision.alCo3[x];
                                if (0 <= co3.vi0 && 0 <= co3.vi1 && 0 <= co3.vi2) {
                                    {
                                        triRender.AddTri(collision.alCo4[co3.vi0], collision.alCo4[co3.vi2], collision.alCo4[co3.vi1], yellow);
                                    }
                                    if (0 <= co3.vi3) {
                                        triRender.AddTri(collision.alCo4[co3.vi3], collision.alCo4[co3.vi2], collision.alCo4[co3.vi0], yellow);
                                    }
                                }
                            }
                        }
                    }
                }

                showCoBBoxes.Clear();

                var greenHalf = new Color4(Color.FromArgb(200, Color.LightGreen)).ToArgb();

                if (numCo1.Value >= 0) {
                    int indexCo1 = (int)numCo1.Value;

                    foreach (var set in checkedCollisionSets) {
                        var collision = set.collision;
                        if (indexCo1 < collision.alCo1.Count) {
                            var co1 = collision.alCo1[indexCo1];

                            showCoBBoxes.AddBox(co1.Min, co1.Max, greenHalf);
                        }
                    }
                }

                if (numCo2.Value >= 0) {
                    int indexCo2 = (int)numCo2.Value;

                    foreach (var set in checkedCollisionSets) {
                        var collision = set.collision;
                        if (indexCo2 < collision.alCo2.Count) {
                            var co2 = collision.alCo2[indexCo2];

                            showCoBBoxes.AddBox(co2.Min, co2.Max, greenHalf);
                        }
                    }
                }

                if (numCo3.Value >= 0) {
                    int indexCo3 = (int)numCo3.Value;

                    var orange = new Color4(Color.Orange).ToArgb();

                    foreach (var set in checkedCollisionSets) {
                        var collision = set.collision;
                        if (indexCo3 < collision.alCo3.Count) {
                            var co3 = collision.alCo3[indexCo3];

                            if (0 <= co3.Co6) {
                                var co6 = collision.alCo6[co3.Co6];
                                showCoBBoxes.AddBox(co6.Min, co6.Max, greenHalf);

                                {
                                    var clip = collision.alCo5[co3.PlaneCo5];
                                    var invertedClip = new Plane(clip.Normal, -clip.D);

                                    var bbox = co6.BBox;

                                    var north = new Plane(0, 0, 1, bbox.Maximum.Z);
                                    var south = new Plane(0, 0, -1, -bbox.Minimum.Z);
                                    var up = new Plane(0, 1, 0, bbox.Maximum.Y);
                                    var down = new Plane(0, -1, 0, -bbox.Minimum.Y);
                                    var east = new Plane(1, 0, 0, bbox.Maximum.X);
                                    var west = new Plane(-1, 0, 0, -bbox.Minimum.X);

                                    var clipPoly = WindingUtil.MakeFaceWinding(
                                        new Plane[] { north, south, up, down, east, west, invertedClip },
                                        invertedClip
                                    );

                                    if (clipPoly != null) {
                                        for (int t = 0; t < clipPoly.Count - 2; t++) {
                                            showCoBBoxes.AddTri(clipPoly[0], clipPoly[1 + t], clipPoly[2 + t], orange);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (triRender.vertList.Count != 0) {
                    device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                    device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Diffuse);

                    device.SetRenderState(RenderState.AlphaBlendEnable, false);
                    device.SetRenderState(RenderState.Lighting, true);
                    device.SetRenderState(RenderState.AlphaTestEnable, false);
                    device.SetRenderState(RenderState.ShadeMode, ShadeMode.Gouraud);
                    device.SetRenderState(RenderState.NormalizeNormals, true);

                    device.EnableLight(0, true);
                    Light l = device.GetLight(0);
                    l.Direction = Target;
                    l.Diffuse = new Color4(-1);
                    device.SetLight(0, l);

                    Material M = device.Material;
                    device.Material = M;

                    Matrix Mw = Matrix.Scaling(-1, -1, -1);
                    device.SetTransform(TransformState.World, Mw);

                    device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                    device.DrawUserPrimitives<CustomVertex.PositionNormalColored>(PrimitiveType.TriangleList, triRender.vertList.Count / 3, triRender.vertList.ToArray());

                    device.SetRenderState(RenderState.AlphaBlendEnable, true);
                    device.SetRenderState(RenderState.Lighting, false);
                    device.SetRenderState(RenderState.AlphaTestEnable, true);
                }
            }

            foreach (var tempMesh in new TempMesh[] { hitBoxList, showCoBBoxes }) {
                if (tempMesh.vecList.Any()) {
                    device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                    device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Diffuse);

                    device.SetRenderState(RenderState.Lighting, true);
                    device.SetRenderState(RenderState.AlphaTestEnable, false);
                    device.SetRenderState(RenderState.ShadeMode, ShadeMode.Flat);
                    device.SetRenderState(RenderState.NormalizeNormals, true);

                    device.EnableLight(0, true);
                    Light l = device.GetLight(0);
                    l.Direction = Target;
                    l.Diffuse = new Color4(-1);
                    device.SetLight(0, l);

                    tempMesh.BeginEffect?.Invoke(device);

                    var worldMatrix = Matrix.Scaling(-1, -1, -1);
                    device.SetTransform(TransformState.World, worldMatrix);

                    device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                    device.DrawIndexedUserPrimitives(
                        PrimitiveType.TriangleList,
                        0,
                        tempMesh.vecList.Count,
                        tempMesh.idxList.Count / 3,
                        tempMesh.idxList.ToArray(),
                        Format.Index32,
                        tempMesh.vecList
                            .ToArray(),
                        CustomVertex.PositionNormalColored.Size
                    );

                    tempMesh.EndEffect?.Invoke(device);

                    device.SetRenderState(RenderState.Lighting, false);
                    device.SetRenderState(RenderState.AlphaTestEnable, true);
                    device.SetRenderState(RenderState.ShadeMode, ShadeMode.Gouraud);
                }
            }

            device.EndScene();

            try {
                device.Present();
            }
            catch (Direct3D9Exception) {
                // Perhaps: SlimDX.Direct3D9.Direct3D9Exception: D3DERR_DEVICELOST: Device lost (-2005530520)
                p1.Hide();
                return;
            }
        }

        IEnumerable<CollisionSet> GetCheckedCollisionSet() => toolStrip1.Items
            .OfType<ToolStripButton>()
            .Where(it => it.Checked)
            .Select(it => it.Tag)
            .OfType<CollisionSet>();

        IEnumerable<DContext> GetCheckedDContextList() => toolStrip1.Items
            .OfType<ToolStripButton>()
            .Where(it => it.Checked)
            .Select(it => it.Tag)
            .OfType<DContext>();

        DrawTri triRender = new DrawTri();

        class DrawTri {
            /// <summary>
            /// vtx 0,1,2 -> triangle#1
            /// vtx 3,4,5 -> triangle#2
            /// ...
            /// </summary>
            public List<CustomVertex.PositionNormalColored> vertList = new List<CustomVertex.PositionNormalColored>();

            public void AddTri(Vector4 w0, Vector4 w1, Vector4 w2, int clr) {
                AddTri(
                    new Vector3(w0.X, w0.Y, w0.Z),
                    new Vector3(w1.X, w1.Y, w1.Z),
                    new Vector3(w2.X, w2.Y, w2.Z),
                    clr
                );
            }

            public void AddTri(Vector3 w0, Vector3 w1, Vector3 w2, int clr) {
                Vector3 v0 = new Vector3(w0.X, w0.Y, w0.Z);
                Vector3 v1 = new Vector3(w1.X, w1.Y, w1.Z);
                Vector3 v2 = new Vector3(w2.X, w2.Y, w2.Z);

                Vector3 nv = Vector3.Cross(v1 - v0, v1 - v2);

                vertList.Add(new CustomVertex.PositionNormalColored(v0, nv, clr));
                vertList.Add(new CustomVertex.PositionNormalColored(v1, nv, clr));
                vertList.Add(new CustomVertex.PositionNormalColored(v2, nv, clr));
            }

            public void Clear() {
                vertList.Clear();
            }
        }

        Point curs = Point.Empty, firstcur = Point.Empty;

        private void p1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
                firstcur = p1.PointToScreen(curs = new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Middle) {
                var cameraEye = CameraEye;
                var target = Target;

                var ray = new Ray(-cameraEye, -target);

                hitBoxList.Clear();

                var boxHalfSize = new Vector3(20, 20, 20);

                var lime = new Color4(Color.Lime).ToArgb();

                foreach (var set in GetCheckedCollisionSet()) {
                    var computer = new CoctComputer(set.collision, ray);

                    foreach (var hitPoint in computer.hitPointList) {
                        hitBoxList.AddBox(
                            hitPoint - boxHalfSize,
                            hitPoint + boxHalfSize,
                            lime
                        );
                    }

                    tslCoctHit.Text = string.Concat(
                        $"Hit {computer.hitPointList.Count:#,##0}, ",
                        "Co2 unk: ", string.Join(",", computer.co2List.Select(it => $"({it.v10:X4} {it.v12})").Distinct()),
                        "Co7 unk: ", string.Join(",", computer.co7List.Select(it => $"{it.v00:X8}").Distinct())
                        );
                }

                p1.Invalidate();
            }
        }

        private void p1_MouseMove(object sender, MouseEventArgs e) {
            if (curs != Point.Empty) {
                int dX = e.X - curs.X;
                int dY = e.Y - curs.Y;
                if (dX != 0 || dY != 0) {
                    if (0 != (e.Button & MouseButtons.Left)) {
                        yaw.Value += (decimal)(dX / 3.0f);
                        roll.Value = (decimal)(Math.Max(-89, Math.Min(+89, (Convert.ToSingle(roll.Value) % 360.0f) - dY / 3.0f)));
                        //roll.Value += (decimal)(-dY / 3.0f);

                        Cursor.Position = firstcur;
                        p1.Invalidate();
                    }
                    else if (0 != (e.Button & MouseButtons.Right)) {
                        yaw.Value += (decimal)(dX / 3.0f);
                        CameraEye += Target * -dY;

                        Cursor.Position = firstcur;
                        p1.Invalidate();
                    }
                }
                return;
            }
        }

        private void p1_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                curs = Point.Empty;
            }
        }

        private void Visf_Load(object sender, EventArgs e) {
            SetStyle(ControlStyles.ResizeRedraw, true);

            showCoBBoxes.BeginEffect = device => {
                device.SetRenderState(RenderState.ZEnable, false);

                device.SetRenderState<Compare>(RenderState.ZFunc, Compare.Always);
            };
            showCoBBoxes.EndEffect = device => {
                device.SetRenderState(RenderState.ZEnable, true);

                device.SetRenderState<Compare>(RenderState.ZFunc, Compare.LessEqual);
            };
        }

        private void tsbExpBlenderpy_Click(object sender, EventArgs e) {
            Directory.CreateDirectory("bpyexp");

            DataStream gs = vb.Lock(0, 0, LockFlags.ReadOnly);
            try {
                CustomVertex.PositionColoredTextured[] verts = new CustomVertex.PositionColoredTextured[cntVerts];
                for (int x = 0; x < cntVerts; x++) {
                    verts[x] = (CustomVertex.PositionColoredTextured)gs.Read<CustomVertex.PositionColoredTextured>();
                }

                int basepici = 0;
                for (int w = 0; w < dcRefList.Count; w++) {
                    var dcRef = dcRefList[w];

                    Mkbpy maker = new Mkbpy();
                    maker.StartTex();

                    String basedir = "bpyexp\\" + dcRef.dc.name;
                    Directory.CreateDirectory(basedir);

                    int pici = 0;
                    {
                        DContext dc = dcList[w];
                        foreach (Bitmap pic in dc.o7.pics) {
                            String fppic = String.Format("t{0:000}.png", pici);
                            pic.Save(basedir + "\\" + fppic, System.Drawing.Imaging.ImageFormat.Png);
                            maker.AddTex(Path.GetFullPath(basedir + "\\" + fppic)
                                , String.Format("Tex{0:000}", pici)
                                , String.Format("Mat{0:000}", pici)
                                );
                            pici++;
                        }
                    }
                    maker.EndTex();
                    foreach (ContextInfo ci in dcRef.contextInfoList) {
                        maker.StartMesh();
                        for (int trii = 0; trii < ci.tripletIndices.Length / 3; trii++) {
                            maker.AddV(verts[ci.tripletIndices[trii * 3 + 0]]);
                            maker.AddV(verts[ci.tripletIndices[trii * 3 + 2]]);
                            maker.AddV(verts[ci.tripletIndices[trii * 3 + 1]]);
                            maker.AddColorVtx(new Color[] {
                            Color.FromArgb(verts[ci.tripletIndices[trii * 3 + 0]].Color),
                            Color.FromArgb(verts[ci.tripletIndices[trii * 3 + 2]].Color),
                            Color.FromArgb(verts[ci.tripletIndices[trii * 3 + 1]].Color),
                            });
                            maker.AddTuv(
                                (ci.texIndex & 65535) - basepici,
                                verts[ci.tripletIndices[trii * 3 + 0]].Tu, 1 - verts[ci.tripletIndices[trii * 3 + 0]].Tv,
                                verts[ci.tripletIndices[trii * 3 + 2]].Tu, 1 - verts[ci.tripletIndices[trii * 3 + 2]].Tv,
                                verts[ci.tripletIndices[trii * 3 + 1]].Tu, 1 - verts[ci.tripletIndices[trii * 3 + 1]].Tv
                                );
                        }
                        maker.EndMesh(ci.packetIndex);
                    }
                    maker.Finish();
                    File.WriteAllText(basedir + "\\mesh.py", maker.ToString(), Encoding.ASCII);

                    basepici += pici;
                }
            }
            finally {
                vb.Unlock();
            }

            Process.Start("explorer.exe", " bpyexp");
        }

        class Mkbpy {
            int i = 0;
            int cntv = 0;
            StringWriter wr = new StringWriter();

            String vcoords = "";
            String vfaces = "";
            StringWriter uvs = new StringWriter();
            int uvi = 0;
            List<int> alRefMati = new List<int>();
            StringWriter vcs = new StringWriter();

            Matrix mtxLoc2Blender;

            public Mkbpy() {
                wr.WriteLine("# http://f11.aaa.livedoor.jp/~hige/index.php?%5B%5BPython%A5%B9%A5%AF%A5%EA%A5%D7%A5%C8%5D%5D");
                wr.WriteLine("# http://www.blender.org/documentation/248PythonDoc/index.html");
                wr.WriteLine();
                wr.WriteLine("# good for Blender 2.4.8a");
                wr.WriteLine("# with Python 2.5.4");
                wr.WriteLine();
                wr.WriteLine("# Import instruction:");
                wr.WriteLine("# * Launch Blender 2.4.8a");
                wr.WriteLine("# * In Blender, type Shift+F11, then open then Script Window");
                wr.WriteLine("# * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py");
                wr.WriteLine("# * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!");
                wr.WriteLine("# * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.");
                wr.WriteLine();
                wr.WriteLine("print \"-- Start importing \"");
                wr.WriteLine();
                wr.WriteLine("import Blender");
                wr.WriteLine();
                wr.WriteLine("scene = Blender.Scene.GetCurrent()");
                wr.WriteLine();

                mtxLoc2Blender = Matrix.RotationX(90.0f / 180.0f * 3.14159f);
                float f = 1.0f / 100;
                mtxLoc2Blender = Matrix.Multiply(mtxLoc2Blender, Matrix.Scaling(-f, f, f));
            }

            public void StartTex() {
                wr.WriteLine("imgs = []");
                wr.WriteLine("mats = []");
            }
            public void AddTex(String fp, String tid, String mid) {
                wr.WriteLine("img = Blender.Image.Load('{0}')", fp.Replace("\\", "/"));
                wr.WriteLine("tex = Blender.Texture.New('{0}')", tid);
                wr.WriteLine("tex.image = img");
                wr.WriteLine("mat = Blender.Material.New('{0}')", mid);
                wr.WriteLine("mat.setTexture(0, tex, Blender.Texture.TexCo.UV, Blender.Texture.MapTo.COL)");
                wr.WriteLine("mat.setMode('Shadeless')");
                wr.WriteLine("mats += [mat]");
                wr.WriteLine("imgs += [img]");
            }
            public void EndTex() {

            }

            public void StartMesh() {
                vcoords = "";
                cntv = 0;

                uvs = new StringWriter();
                uvi = 0;

                alRefMati.Clear();

                vcs = new StringWriter();
            }
            public void AddV(CustomVertex.PositionColoredTextured v) {
                if (vcoords != "")
                    vcoords += ",";
                Vector3 v3 = Vector3.TransformCoordinate(v.Position, mtxLoc2Blender);
                vcoords += string.Format("[{0},{1},{2}]", v3.X, v3.Y, v3.Z);

                cntv++;
            }

            public void AddColorVtx(Color[] clrs) {
                for (int x = 0; x < clrs.Length; x++) {
                    vcs.WriteLine("me.faces[{0}].col[{1}].a = {2}", uvi, x, clrs[x].A);
                    vcs.WriteLine("me.faces[{0}].col[{1}].r = {2}", uvi, x, clrs[x].R);
                    vcs.WriteLine("me.faces[{0}].col[{1}].g = {2}", uvi, x, clrs[x].G);
                    vcs.WriteLine("me.faces[{0}].col[{1}].b = {2}", uvi, x, clrs[x].B);
                }
            }

            public void AddTuv(int texi, float tu0, float tv0, float tu1, float tv1, float tu2, float tv2) {
                Debug.Assert(texi < 256);

                if (alRefMati.IndexOf(texi) < 0)
                    alRefMati.Add(texi);
                int mati = alRefMati.IndexOf(texi);

                uvs.WriteLine("me.faces[{0}].uv = [Blender.Mathutils.Vector({1:0.000},{2:0.000}),Blender.Mathutils.Vector({3:0.000},{4:0.000}),Blender.Mathutils.Vector({5:0.000},{6:0.000}),]", uvi, tu0, tv0, tu1, tv1, tu2, tv2);
                uvs.WriteLine("me.faces[{0}].mat = {1}", uvi, mati);
                uvs.WriteLine("me.faces[{0}].image = imgs[{1}]", uvi, texi);
                uvi++;
            }

            public void EndMesh(int vifi) {
                if (cntv == 0)
                    return;

                vfaces = "";
                for (int x = 0; x < cntv / 3; x++) {
                    if (vfaces != "")
                        vfaces += ",";
                    vfaces += string.Format("[{0},{1},{2}]", 3 * x, 3 * x + 1, 3 * x + 2);
                }

                String meshName = string.Format("vifpkt{0:0000}-mesh", vifi);
                String objName = string.Format("vifpkt{0:0000}-obj{1}", vifi, i);
                i++;

                wr.WriteLine("coords = [" + vcoords + "]");
                wr.WriteLine("faces = [" + vfaces + "]");
                wr.WriteLine("me = Blender.Mesh.New('" + meshName + "')");
                wr.WriteLine("me.verts.extend(coords)");
                wr.WriteLine("me.faces.extend(faces)");
                wr.WriteLine("me.faceUV = True");
                for (int x = 0; x < alRefMati.Count; x++) {
                    wr.WriteLine("me.materials += [mats[{0}]]", alRefMati[x]);
                }
                wr.Write(uvs.ToString());
                wr.WriteLine("me.vertexColors = True");
                wr.Write(vcs.ToString());
                wr.WriteLine("ob = scene.objects.new(me, '" + objName + "')");
                wr.WriteLine("");
            }

            public void Finish() {
                wr.WriteLine("print \"-- Ended importing \"");
            }

            public override string ToString() {
                return wr.ToString();
            }
        }

        delegate void _SetPos(Vector3 v);

        private void eyeX_ValueChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        [Flags]
        enum Keyrun {
            None = 0,
            W = 1, S = 2, A = 4, D = 8, Up = 16, Down = 32,
        }

        Keyrun kr = Keyrun.None;

        private void p1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W: kr |= Keyrun.W; break;
                case Keys.S: kr |= Keyrun.S; break;
                case Keys.A: kr |= Keyrun.A; break;
                case Keys.D: kr |= Keyrun.D; break;
                case Keys.Up: kr |= Keyrun.Up; break;
                case Keys.Down: kr |= Keyrun.Down; break;
            }
            if (kr != Keyrun.None && !timerRun.Enabled) timerRun.Start();
        }

        private void p1_KeyUp(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.W: kr &= ~Keyrun.W; break;
                case Keys.S: kr &= ~Keyrun.S; break;
                case Keys.A: kr &= ~Keyrun.A; break;
                case Keys.D: kr &= ~Keyrun.D; break;
                case Keys.Up: kr &= ~Keyrun.Up; break;
                case Keys.Down: kr &= ~Keyrun.Down; break;
            }
            if (kr == Keyrun.None && timerRun.Enabled) timerRun.Stop();
        }

        Vector3 LeftVec {
            get {
                return
                    Vector3.TransformCoordinate(
                        Vector3.TransformCoordinate(
                            TargetX,
                            Matrix.RotationY(-90.0f / 180.0f * 3.14159f)
                            ),
                        Matrix.RotationYawPitchRoll(
                            0,
                            Convert.ToSingle(pitch.Value) / 180.0f * 3.14159f,
                            0
                            )
                        );
            }
        }

        int Speed { get { return 0 != (ModifierKeys & Keys.Shift) ? 60 : 30; } }

        private void timerRun_Tick(object sender, EventArgs e) {
            if (0 != (kr & Keyrun.W)) {
                CameraEye += Target * Speed;
            }
            if (0 != (kr & Keyrun.S)) {
                CameraEye -= Target * Speed;
            }
            if (0 != (kr & Keyrun.A)) {
                CameraEye += LeftVec * Speed;
            }
            if (0 != (kr & Keyrun.D)) {
                CameraEye -= LeftVec * Speed;
            }
            if (0 != (kr & Keyrun.Up)) {
                CameraEye += Vector3.UnitY * Speed;
            }
            if (0 != (kr & Keyrun.Down)) {
                CameraEye -= Vector3.UnitY * Speed;
            }
            p1.Invalidate();
        }

        private void p1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void Visf_FormClosing(object sender, FormClosingEventArgs e) {
            disposer.Reverse();
            foreach (ComObject o in disposer) {
                o.Dispose();
            }
        }

        private void cbFog_CheckedChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void tsbShowColl_Click(object sender, EventArgs e) { p1.Invalidate(); }

        private void tsbShowGeo_Click(object sender, EventArgs e) { p1.Invalidate(); }

        private void tsbEnableDoct_Click(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void tsbPlane_Click(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void numCo3_ValueChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void numCo2_ValueChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void numCo1_ValueChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void numVifpkt_ValueChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

        private void tsbRenderModel_Click(object sender, EventArgs e) {
            p1.Invalidate();

        }

        private void tsbRenderShadow_Click(object sender, EventArgs e) {
            p1.Invalidate();

        }

        private void p1_SizeChanged(object sender, EventArgs e) {
            p1.Invalidate();
        }

    }

    namespace Put {
        /// <summary>
        /// Supply temporary mesh builder. It is for rendering collision map.
        /// </summary>
        public class TempMesh {
            public List<CustomVertex.PositionNormalColored> vecList = new List<CustomVertex.PositionNormalColored>();
            public List<int> idxList = new List<int>();

            public Action<Device> BeginEffect;
            public Action<Device> EndEffect;

            public void Clear() {
                vecList.Clear();
                idxList.Clear();
            }

            public void AddBox(Vector3 v0, Vector3 v1, int clr, bool outside = true) {
                int firstVec = vecList.Count;
                int firstIdx = idxList.Count;

                var minX = Math.Min(v0.X, v1.X);
                var minY = Math.Min(v0.Y, v1.Y);
                var minZ = Math.Min(v0.Z, v1.Z);
                var maxX = Math.Max(v0.X, v1.X);
                var maxY = Math.Max(v0.Y, v1.Y);
                var maxZ = Math.Max(v0.Z, v1.Z);

                var list = new List<Vector3>();

                list.Add(new Vector3(minX, minY, minZ));
                list.Add(new Vector3(maxX, minY, minZ));
                list.Add(new Vector3(minX, maxY, minZ));
                list.Add(new Vector3(maxX, maxY, minZ));
                list.Add(new Vector3(minX, minY, maxZ));
                list.Add(new Vector3(maxX, minY, maxZ));
                list.Add(new Vector3(minX, maxY, maxZ));
                list.Add(new Vector3(maxX, maxY, maxZ));

                //       * 6 - 7
                // 2 - 3 | | U |
                // | D | | 4 - 5
                // 0 - 1 *

                AddQuad(0, 2, 3, 1, clr, list, outside); // Bottom
                AddQuad(4, 5, 7, 6, clr, list, outside); // Up
                AddQuad(2, 6, 7, 3, clr, list, outside); // n
                AddQuad(0, 1, 5, 4, clr, list, outside); // s
                AddQuad(0, 4, 6, 2, clr, list, outside); // w
                AddQuad(1, 3, 7, 5, clr, list, outside); // e
            }

            void AddQuad(int i0, int i1, int i2, int i3, int clr, IList<Vector3> list, bool outside) {
                if (outside) {
                    AddTri(list[i0], list[i2], list[i1], clr);
                    AddTri(list[i2], list[i0], list[i3], clr);
                }
                else {
                    AddTri(list[i0], list[i1], list[i2], clr);
                    AddTri(list[i2], list[i3], list[i0], clr);
                }
            }

            public void AddTri(Vector3 w0, Vector3 w1, Vector3 w2, int clr) {
                int firstVec = vecList.Count;

                var norm = Vector3.Cross(w1 - w0, w1 - w2);
                norm.Normalize();

                vecList.Add(new CustomVertex.PositionNormalColored(w0, norm, clr));
                vecList.Add(new CustomVertex.PositionNormalColored(w1, norm, clr));
                vecList.Add(new CustomVertex.PositionNormalColored(w2, norm, clr));

                idxList.Add(firstVec + 0);
                idxList.Add(firstVec + 1);
                idxList.Add(firstVec + 2);
            }
        }
    }
}