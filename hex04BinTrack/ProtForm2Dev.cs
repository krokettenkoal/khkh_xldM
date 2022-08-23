//#define USEB1B2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hex04BinTrack.R;
using System.IO;
using System.Diagnostics;
using vconv122;
using System.Xml;
using SlimDX.Direct3D9;
using SlimDX;
using khkh_xldMii.Models;
using hex04BinTrack.VertexFormats;
using hex04BinTrack.Utils;

namespace hex04BinTrack {
    public partial class ProtForm2Dev : Form {
        public ProtForm2Dev() {
            InitializeComponent();
        }
        public ProtForm2Dev(F2DRi f2) {
            this.f2 = f2;
            InitializeComponent();
        }

        class CaseTris : IDisposable {
            public VertexBuffer vb;
            public VertexFormat vf;
            public int cntPrimitives, cntVert;
            public Sepa[] alsepa;
            public int StrideSize { get { return VertexFmtSize.Compute(vf); } }

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

        F2DRi f2 = null;

        private void ProtForm2Dev_Load(object sender, EventArgs e) {

        }

        Direct3D direct3d;
        Device device;
        CaseTris ctbody = new CaseTris();
        List<Body1> albody1 = null;
        List<Texture> altex = new List<Texture>();

        class Body1 {
            public Vector3[] alvert = null;
            public Vector2[] aluv = null;
            public int[] alvi = null;
            public int[] alfl = null;
            public int t = -1;
            public bool avail = false;
        }

        class UtB1B2 {
            public static Matrix readMtx(BinaryReader br) {
                Matrix M = new Matrix();
                M.M11 = br.ReadSingle(); M.M12 = br.ReadSingle(); M.M13 = br.ReadSingle(); M.M14 = br.ReadSingle();
                M.M21 = br.ReadSingle(); M.M22 = br.ReadSingle(); M.M23 = br.ReadSingle(); M.M24 = br.ReadSingle();
                M.M31 = br.ReadSingle(); M.M32 = br.ReadSingle(); M.M33 = br.ReadSingle(); M.M34 = br.ReadSingle();
                M.M41 = br.ReadSingle(); M.M42 = br.ReadSingle(); M.M43 = br.ReadSingle(); M.M44 = br.ReadSingle();
                return M;
            }
        }

        class Save2XML {
            public XmlDocument xmlo = new XmlDocument();

            public Save2XML(AxBone[] alaxb, Matrix[] Ma) {
                XmlElement elroot = xmlo.CreateElement("Model");
                xmlo.AppendChild(elroot);
                {
                    XmlElement elp = xmlo.CreateElement("alaxb");
                    elroot.AppendChild(elp);
                    for (int x = 0; x < alaxb.Length; x++) {
                        XmlElement ela = xmlo.CreateElement("AxBone");
                        elp.AppendChild(ela);
                        AxBone o = alaxb[x];

                        XUt.Add(ela, "parent", o.parent.ToString());
                        XUt.Add(ela, "current", o.cur.ToString());

                        if (o.x1 != 0) XUt.Add(ela, "scale_x", o.x1.ToString());
                        if (o.y1 != 0) XUt.Add(ela, "scale_y", o.y1.ToString());
                        if (o.z1 != 0) XUt.Add(ela, "scale_z", o.z1.ToString());

                        if (o.x2 != 0) XUt.Add(ela, "rot_x", o.x2.ToString());
                        if (o.y2 != 0) XUt.Add(ela, "rot_y", o.y2.ToString());
                        if (o.z2 != 0) XUt.Add(ela, "rot_z", o.z2.ToString());

                        if (o.x3 != 0) XUt.Add(ela, "trans_x", o.x3.ToString());
                        if (o.y3 != 0) XUt.Add(ela, "trans_y", o.y3.ToString());
                        if (o.z3 != 0) XUt.Add(ela, "trans_z", o.z3.ToString());
                    }
                }
                {
                    XmlElement elp = xmlo.CreateElement("Ma");
                    elroot.AppendChild(elp);
                    for (int x = 0; x < Ma.Length; x++) {
                        XmlElement ela = xmlo.CreateElement("Matrix");
                        elp.AppendChild(ela);
                        Matrix o = Ma[x];
                        XUt.Add(ela, "M11", o.M11.ToString());
                        XUt.Add(ela, "M12", o.M12.ToString());
                        XUt.Add(ela, "M13", o.M13.ToString());
                        XUt.Add(ela, "M14", o.M14.ToString());
                        XUt.Add(ela, "M21", o.M21.ToString());
                        XUt.Add(ela, "M22", o.M22.ToString());
                        XUt.Add(ela, "M23", o.M23.ToString());
                        XUt.Add(ela, "M24", o.M24.ToString());
                        XUt.Add(ela, "M31", o.M31.ToString());
                        XUt.Add(ela, "M32", o.M32.ToString());
                        XUt.Add(ela, "M33", o.M33.ToString());
                        XUt.Add(ela, "M34", o.M34.ToString());
                        XUt.Add(ela, "M41", o.M41.ToString());
                        XUt.Add(ela, "M42", o.M42.ToString());
                        XUt.Add(ela, "M43", o.M43.ToString());
                        XUt.Add(ela, "M44", o.M44.ToString());
                    }
                }
            }

            class XUt {
                public static XmlElement Add(XmlElement el, String key, String val) {
                    XmlElement elNew = el.OwnerDocument.CreateElement(key);
                    elNew.AppendChild(el.OwnerDocument.CreateTextNode(val));
                    el.AppendChild(elNew);
                    return elNew;
                }
            }
        }

        Save2XML s2xml = null;

        void Uset2() {
            Matrix[] Ma = new Matrix[f2.alaxbone.Count];
            Vector3[] Va = new Vector3[Ma.Length];
            Quaternion[] Qa = new Quaternion[Ma.Length];
            for (int x = 0; x < Ma.Length; x++) {
                Quaternion Qo;
                Vector3 Vo;
                AxBone axb = f2.alaxbone[x];
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
                if (axb.x2 != 0) Qt *= (Quaternion.RotationAxis(Vector3.UnitX, axb.x2));
                if (axb.y2 != 0) Qt *= (Quaternion.RotationAxis(Vector3.UnitY, axb.y2));
                if (axb.z2 != 0) Qt *= (Quaternion.RotationAxis(Vector3.UnitZ, axb.z2));
                Qa[x] = Qt * Qo;

                Ma[x] = Matrix.RotationQuaternion(Qa[x]) * Matrix.Translation(Va[x]);
            }
#if DEBUG
            s2xml = new Save2XML(f2.alaxbone.ToArray(), Ma);
            s2xml.xmlo.Save(Path.Combine(Application.StartupPath, "ModelHelp.xml"));
#endif

#if USEB1B2
            if (true) {
                MemoryStream os = new MemoryStream();
                new BinaryWriter(os).Write(File.ReadAllBytes(@"H:\Proj\khkh_xldM\MEMO\expSim\a.1ACAD80.bin"));
                BinaryReader br = new BinaryReader(os);
                os.Position = 64 * 0xE5 * hScrollBarTick.Value;
                for (int x = 0; x < 229; x++) {
                    Ma[x] = UtB1B2.readMtx(br);
                }
            }
#endif

            albody1 = new List<Body1>();

            foreach (RPart rp in f2.alrp) {
                MemoryStream si = new MemoryStream(rp.vu1mem.vumem, true);
                BinaryReader br = new BinaryReader(si, Encoding.GetEncoding(932));

                //si.Position = 16 * (rp.top2);
                //for (int x = 0; x < rp.alaxi.Length; x++) {
                //    UtilMatrixio.write(wr, Ma[rp.alaxi[x]]);
                //}

                si.Position = 16 * (rp.tops);

                int v00 = br.ReadInt32();
                if (v00 != 1 && v00 != 2) throw new ProtInvalidTypeException();
                int v04 = br.ReadInt32();
                int v08 = br.ReadInt32();
                int v0c = br.ReadInt32();
                int v10 = br.ReadInt32(); // cnt box2
                int v14 = br.ReadInt32(); // off box2 {tx ty vi fl}
                int v18 = br.ReadInt32(); // off box1
                int v1c = br.ReadInt32(); // off matrices
                int v20 = (v00 == 1) ? br.ReadInt32() : 0; // cntvertscolor
                int v24 = (v00 == 1) ? br.ReadInt32() : 0; // offvertscolor
                int v28 = (v00 == 1) ? br.ReadInt32() : 0; // cnt spec
                int v2c = (v00 == 1) ? br.ReadInt32() : 0; // off spec
                int v30 = br.ReadInt32(); // cnt verts 
                int v34 = br.ReadInt32(); // off verts
                int v38 = br.ReadInt32(); // 
                int v3c = br.ReadInt32(); // cnt box1
                Trace.Assert(v1c == rp.top2 - rp.tops, "top2 isn't identical!");

                si.Position = 16 * (rp.tops + v18);
                int[] box1 = new int[v3c];
                for (int x = 0; x < box1.Length; x++) {
                    box1[x] = br.ReadInt32();
                }

                Body1 body1 = new Body1();
                body1.t = rp.tsel;
                body1.alvert = new Vector3[v30];
                body1.avail = (v00 == 1);// (v28 == 0) && (v00 == 1);

                Vector3[] alv4 = new Vector3[v30];

                int vi = 0;
                si.Position = 16 * (rp.tops + v34);
                for (int x = 0; x < box1.Length; x++) {
                    Matrix M1 = Ma[rp.alaxi[x]];
                    int ct = box1[x];
                    for (int t = 0; t < ct; t++, vi++) {
                        float fx = br.ReadSingle();
                        float fy = br.ReadSingle();
                        float fz = br.ReadSingle();
                        float fw = br.ReadSingle();

                        Vector3 v3 = new Vector3(fx, fy, fz);
                        Vector3 v3t = Vector3.TransformCoordinate(v3, M1);
                        body1.alvert[vi] = v3t;

                        Vector4 v4 = new Vector4(fx, fy, fz, fw);
                        Vector4 v4t = Vector4.Transform(v4, M1);
                        alv4[vi] = new Vector3(v4t.X, v4t.Y, v4t.Z);
                    }
                }

                body1.aluv = new Vector2[v10];
                body1.alvi = new int[v10];
                body1.alfl = new int[v10];

                si.Position = 16 * (rp.tops + v14);
                for (int x = 0; x < v10; x++) {
                    int tx = br.ReadUInt16() / 16; br.ReadUInt16();
                    int ty = br.ReadUInt16() / 16; br.ReadUInt16();
                    body1.aluv[x] = new Vector2(tx / 256.0f, ty / 256.0f);
                    body1.alvi[x] = br.ReadUInt16(); br.ReadUInt16();
                    body1.alfl[x] = br.ReadUInt16(); br.ReadUInt16();
                }

                if (v28 != 0) {
                    si.Position = 16 * (rp.tops + v2c);
                    int vt0 = br.ReadInt32();
                    int vt1 = br.ReadInt32();
                    int vt2 = br.ReadInt32();
                    int vt3 = br.ReadInt32();
                    Vector3[] allocalvert = new Vector3[v30];
                    int xi = 0;
                    for (xi = 0; xi < vt0; xi++) {
                        int ai = br.ReadInt32();
                        allocalvert[xi] = body1.alvert[ai];
                    }
                    if (v28 >= 2) {
                        si.Position = (si.Position + 15) & (~15);
                        for (int x = 0; x < vt1; x++, xi++) {
                            int i0 = br.ReadInt32();
                            int i1 = br.ReadInt32();
                            allocalvert[xi] = alv4[i0] + alv4[i1];
                        }
                    }
                    if (v28 >= 3) {
                        Debug.Fail("v28: " + v28);
                    }
                    body1.alvert = allocalvert;
                }

                albody1.Add(body1);
            }
        }

        private void panel1_Load(object sender, EventArgs e) {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = Format.D16;
            direct3d = new Direct3D();
            device = new Device(direct3d, 0, DeviceType.Hardware, panel1.Handle, CreateFlags.SoftwareVertexProcessing, pp);
            devReset();

            reshape();

            panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);

            if (f2 != null) {
                Uset2();

                listView1.Items.Clear();
                if (f2 != null) {
                    int i = 0;
                    foreach (RPart rp in f2.alrp) {
                        VIFTy vty = new VIFTy(rp.vu1mem, rp.tops);
                        ListViewItem lvi = listView1.Items.Add("#" + i + " " + rp.off.ToString("X") + " " + rp.tsel + " " + vty.v00 + " " + vty.v28);
                        lvi.Tag = (int)((albody1[i].avail ? 1 : 0) | ((vty.v00 == 2) ? 2 : 0));
                        if (albody1[i].avail) lvi.Checked = true;
                        i++;
                    }
                }
            }

            calcbody(ctbody);
        }

        class VIFTy {
            public int v00;
            public int v28;

            public VIFTy(VU1Mem vu1, int tops) {
                MemoryStream si = new MemoryStream(vu1.vumem, false);
                BinaryReader br = new BinaryReader(si);
                si.Position = 16 * tops;
                v00 = br.ReadInt32();
                si.Position = 16 * tops + 0x28;
                v28 = br.ReadInt32();
            }
        }

        float fzval = 1;
        Vector3 offset = Vector3.Zero;
        Quaternion quat = Quaternion.Identity;

        void reshape() {
            int cxw = panel1.Width;
            int cyw = panel1.Height;
            float fx = (cxw > cyw) ? ((float)cxw / cyw) : 1.0f;
            float fy = (cxw < cyw) ? ((float)cyw / cxw) : 1.0f;

            Matrix mvo = Matrix.Identity;

            float fact = (float)(fzval * 0.5f * 100);

            device.SetTransform(TransformState.Projection, Matrix.OrthoLH(
                fx * fact, fy * fact, fact / +1, fact / -1
                ));
            Matrix mv = Matrix.RotationQuaternion(quat);
            mv.M41 += offset.X;
            mv.M42 += offset.Y;
            mv.M43 += offset.Z;
            device.SetTransform(TransformState.View, mvo * mv);
        }

        void device_DeviceResizing(object sender, CancelEventArgs e) {
            reshape();
        }

        void device_DeviceReset(object sender, EventArgs e) {
            devReset();
        }

        void device_DeviceLost(object sender, EventArgs e) {

        }

        private void devReset() {
            device.SetRenderState(RenderState.Lighting, false);
            device.SetRenderState(RenderState.ZEnable, true);
            //device.SetSamplerState(0, SamplerStageStates.MagFilter, (int)TextureFilter.Linear);

            altex.Clear();
            foreach (TIMBitmap st in f2.texexal[0].bitmapList) {
                altex.Add(TexUtil.FromBitmap(device, st.bitmap, Usage.None, Pool.Managed));
            }
        }

        void panel1_MouseWheel(object sender, MouseEventArgs e) {
            fzval = Math.Max(1.0f, Math.Min(100.0f, fzval + (e.Delta / 120)));
            doReshape();
        }

        private void doReshape() {
            reshape();
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if (device == null || device.TestCooperativeLevel().IsFailure) return;

            bool isWire = radioButtonWire.Checked;
            bool isTexsk = radioButtonTex.Checked;

            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, panel1.BackColor, 1.0f, 0);
            device.BeginScene();
            device.SetRenderState<FillMode>(RenderState.FillMode, isWire ? FillMode.Wireframe : FillMode.Solid);
            if (ctbody != null && ctbody.vb != null) {
                device.SetStreamSource(0, ctbody.vb, 0, ctbody.StrideSize);
                device.VertexFormat = ctbody.vf;
                device.Indices = null;
                foreach (Sepa sepa in ctbody.alsepa) {
                    if (listView1.Items[sepa.sel].Checked) {
                        device.SetTexture(0, (isTexsk && sepa.t <= altex.Count) ? altex[sepa.t] : null);
                        device.DrawPrimitives(PrimitiveType.TriangleList, sepa.svi, sepa.cnt);
                    }
                }
            }
            device.EndScene();

            device.Present();
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e) {

        }

        Point curs = Point.Empty;

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                curs = e.Location;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                int dx = e.X - curs.X;
                int dy = e.Y - curs.Y;
                if (dx != 0 || dy != 0) {
                    curs = e.Location;

                    if (0 == (ModifierKeys & Keys.Shift)) {
                        quat *= (Quaternion.RotationYawPitchRoll(dx / 100.0f, dy / 100.0f, 0));
                    }
                    else {
                        offset += (new Vector3(dx, -dy, 0));
                    }
                    doReshape();
                }
            }
        }

        void calcbody(CaseTris ct) {
            ct.Close();

            if (f2 != null) {
                List<uint> altri3 = new List<uint>();
                List<Sepa> alsepa = new List<Sepa>();
                if (true) {
                    int svi = 0;
                    int bi = 0;
                    uint[] alvi = new uint[4];
                    int ai = 0;
                    foreach (Body1 b1 in albody1) {
                        int cntPrim = 0;
                        for (int x = 0; x < b1.alvi.Length; x++) {
                            alvi[ai] = (uint)((b1.alvi[x]) | (bi << 12) | (x << 24));
                            ai = (ai + 1) & 3;
                            if (b1.alfl[x] == 0x20) {
                                altri3.Add(alvi[(ai - 1) & 3]);
                                altri3.Add(alvi[(ai - 2) & 3]);
                                altri3.Add(alvi[(ai - 3) & 3]);
                                cntPrim++;
                            }
                            else if (b1.alfl[x] == 0x30) {
                                altri3.Add(alvi[(ai - 1) & 3]);
                                altri3.Add(alvi[(ai - 3) & 3]);
                                altri3.Add(alvi[(ai - 2) & 3]);
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
                        Pcnt.StrideSize * ct.cntVert,
                        Usage.Points,
                        ct.vf = Pcnt.Format,
                        Pool.Managed
                        );
                    DataStream gs = ct.vb.Lock(0, 0, 0);
                    try {
                        for (int x = 0; x < altri3.Count; x++) {
                            uint xx = altri3[x];
                            uint vi = xx & 4095;
                            uint bi = (xx >> 12) & 4095;
                            uint xi = (xx >> 24) & 4095;
                            Body1 b1 = albody1[(int)bi];
                            Pcnt v3 = new Pcnt(b1.alvert[vi], Vector3.Zero, b1.aluv[xi], Color.White.ToArgb());
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            listView2.Items.Clear();
            foreach (int sel in listView1.SelectedIndices) {
                Body1 b1 = albody1[sel];
                for (int x = 0; x < b1.alfl.Length; x++) {
                    int t = b1.alfl[x];
                    Debug.Assert(t == 0x10 || t == 0x20 || t == 0x30);
                    listView2.Items.Add(((t == 0x10) ? "" : "  ") + t.ToString("X2"));
                }
                break;
            }
        }

        private void radioButtonWire_CheckedChanged(object sender, EventArgs e) {
            panel1.Invalidate();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e) {
            panel1.Invalidate();
        }

        private void buttonSelGeneric_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listView1.Items) {
                bool f = ((int)lvi.Tag & 1) != 0;
                if (lvi.Checked != f) lvi.Checked = f;
            }
        }

        private void buttonSelShallow_Click(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listView1.Items) {
                bool f = ((int)lvi.Tag & 2) != 0;
                if (lvi.Checked != f) lvi.Checked = f;
            }
        }

        private void hScrollBarTick_Scroll(object sender, ScrollEventArgs e) {
            Uset2();
            panel1.Invalidate();
        }
    }

    public class ProtInvalidTypeException : Exception {
    }
}
namespace hex04BinTrack.R {
    public class RPart {
        public VU1Mem vu1mem;
        public int tsel;
        public int tops;
        public int top2;
        public int[] alaxi;
        public int off;

        public RPart(VU1Mem vu1mem, int tsel, int tops, int top2, int[] alaxi, int off) {
            this.vu1mem = vu1mem;
            this.tsel = tsel;
            this.tops = tops;
            this.top2 = top2;
            this.alaxi = alaxi;
            this.off = off;
        }
    }
    public class F2DRi {
        public List<RPart> alrp;
        public List<AxBone> alaxbone;
        public Texex2[] texexal;

        public F2DRi(List<RPart> alrp, List<AxBone> alaxbone, Texex2[] texexal) {
            this.alrp = alrp;
            this.alaxbone = alaxbone;
            this.texexal = texexal;
        }
    }
}
