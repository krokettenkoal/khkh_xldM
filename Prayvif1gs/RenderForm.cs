using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX.Direct3D;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.DirectX;

namespace Prayvif1gs {
    public partial class RenderForm : Form {
        public RenderForm() {
            InitializeComponent();
        }

        FileStream fs;

        private void Form1_Load(object sender, EventArgs e) {
            // H:\Proj\khkh_xldM\Prayvif2\bin\Debug\wrgs.txt
            // @"H:\Proj\khkh_xldM\Prayvif1\bin\Debug\wrgs.txt"
            fs = File.OpenRead(@"H:\Proj\khkh_xldM\Prayvif2\bin\Debug\wrgs.txt");
            loadIt();
        }

        Regex rexPRIM = new Regex(@"^prim\s+(\d+)$", RegexOptions.IgnoreCase);
        Regex rexXYZF2 = new Regex(@"^xyzf2\s+([0-9\.E\-]+)\s+([0-9\.E\-]+)\s+([0-9\.E\-]+)\s+([0-9\.E\-]+)$", RegexOptions.IgnoreCase);
        Regex rexRGBAQ = new Regex(@"^rgbaq\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)$", RegexOptions.IgnoreCase);

        class Tuple {
            public List<CustomVertex.PositionColoredTextured> alv = new List<CustomVertex.PositionColoredTextured>();

            int era;
            int vifi;

            public Tuple(int era, int vifi) {
                this.era = era;
                this.vifi = vifi;
            }

            public override string ToString() {
                return vifi + "." + era + "(" + alv.Count;
            }
        }

        List<Tuple> alt = new List<Tuple>();

        private void loadIt() {
            StreamReader rr = new StreamReader(fs);
            string row;
            int prim = 0;
            CustomVertex.PositionColoredTextured[] rb = new CustomVertex.PositionColoredTextured[4];
            CustomVertex.PositionColoredTextured tfan = new CustomVertex.PositionColoredTextured();
            int irb = 0, pos = 0, tstep = 0;
            alt.Clear();
            Tuple tuple;
            alt.Add(tuple = new Tuple(0, 0));
            Color clr = Color.Empty;
            while (null != (row = rr.ReadLine())) {
                Match M;
                do {
                    if (row.StartsWith("XGKICK ")) {
                        String[] cols = row.Split(' ');
                        alt.Add(tuple = new Tuple(int.Parse(cols[1]), int.Parse(cols[2])));
                        break;
                    }
                    M = rexPRIM.Match(row);
                    if (M.Success) {
                        prim = int.Parse(M.Groups[1].Value);
                        pos = tstep = 0;
                        break;
                    }
                    M = rexRGBAQ.Match(row);
                    if (M.Success) {
                        int R = int.Parse(M.Groups[1].Value);
                        int G = int.Parse(M.Groups[2].Value);
                        int B = int.Parse(M.Groups[3].Value);
                        int A = int.Parse(M.Groups[4].Value);
                        R = Math.Min(255, R * 2);
                        G = Math.Min(255, G * 2);
                        B = Math.Min(255, B * 2);
                        clr = Color.FromArgb(R, G, B);
                        break;
                    }
                    M = rexXYZF2.Match(row);
                    if (M.Success) {
                        CustomVertex.PositionColoredTextured v3 = new CustomVertex.PositionColoredTextured();
                        v3.X = (float.Parse(M.Groups[1].Value));
                        v3.Y = (float.Parse(M.Groups[2].Value));
                        v3.Z = (float.Parse(M.Groups[3].Value) / 3000.0f);
                        int ADC = int.Parse(M.Groups[4].Value);
                        v3.Color = clr.ToArgb();
                        v3.Tu = 0;
                        v3.Tv = 0;
                        if (pos == 0) tfan = v3;
                        rb[irb & 3] = v3; irb++; pos++;
                        switch (prim & 7) {
                            case 4: // Tstrip
                                if (pos >= 3 && ADC == 0) {
                                    if (0 == (tstep & 1)) {
                                        // first
                                        tuple.alv.Add(rb[(irb - 1) & 3]);
                                        tuple.alv.Add(rb[(irb - 2) & 3]);
                                        tuple.alv.Add(rb[(irb - 3) & 3]);
                                    }
                                    else {
                                        // second
                                        tuple.alv.Add(rb[(irb - 1) & 3]);
                                        tuple.alv.Add(rb[(irb - 3) & 3]);
                                        tuple.alv.Add(rb[(irb - 2) & 3]);
                                    }
                                    tstep++;
                                }
                                break;
                            case 5: // Tfan
                                if (pos >= 3 && ADC == 0) {
                                    tuple.alv.Add(tfan);
                                    tuple.alv.Add(rb[(irb - 2) & 3]);
                                    tuple.alv.Add(rb[(irb - 1) & 3]);
                                }
                                break;
                            case 6: // sprite
                                if (pos >= 2 && ADC == 0) {
                                    CustomVertex.PositionColoredTextured vt0 = rb[(irb - 1) & 3];
                                    CustomVertex.PositionColoredTextured vt3 = rb[(irb - 2) & 3];
                                    float x0 = vt0.X; float x2 = vt3.X;
                                    float y0 = vt0.Y; float y2 = vt3.Y;
                                    vt0.X = Math.Min(x0, x2);
                                    vt0.Y = Math.Min(y0, y2);
                                    vt3.X = Math.Max(x0, x2);
                                    vt3.Y = Math.Max(y0, y2);

                                    CustomVertex.PositionColoredTextured vt1 = vt0;
                                    vt1.X = vt3.X;
                                    vt1.Y = vt0.Y;
                                    CustomVertex.PositionColoredTextured vt2 = vt3;
                                    vt2.X = vt0.X;
                                    vt2.Y = vt3.Y;

                                    //vt0.Z = 0;// vt0.Color = Color.White.ToArgb();
                                    //vt1.Z = 0;// vt1.Color = Color.BlueViolet.ToArgb();
                                    //vt2.Z = 0;// vt2.Color = Color.BlueViolet.ToArgb();
                                    //vt3.Z = 0;// vt3.Color = Color.White.ToArgb();

                                    tuple.alv.Add(vt0); tuple.alv.Add(vt1); tuple.alv.Add(vt2);
                                    tuple.alv.Add(vt3); tuple.alv.Add(vt2); tuple.alv.Add(vt1);
                                }
                                break;
                            default:
                                Debug.Fail("prim = " + prim);
                                break;
                        }
                        break;
                    }
                } while (false);
            }

            int i = 0;
            foreach (Tuple t in alt) {
                lvI.Items.Add(t.ToString());
                i++;
            }
        }

        Device device;

        private void p1_Load(object sender, EventArgs e) {
            PresentParameters pp = new PresentParameters();
            pp.AutoDepthStencilFormat = DepthFormat.D16;
            pp.EnableAutoDepthStencil = true;
            pp.SwapEffect = SwapEffect.Discard;
            pp.Windowed = true;
            device = new Device(0, DeviceType.Hardware, p1, CreateFlags.SoftwareVertexProcessing, pp);
            device.DeviceResizing += new CancelEventHandler(device_DeviceResizing);
        }

        void device_DeviceResizing(object sender, CancelEventArgs e) {
            UpdateD3D();
            p1.Invalidate();
        }

        Rectangle rcBox = new Rectangle(0, 0, 4096, 4096);
        int roty = 0;

        static int ticks = 0;

        class Ut {
            public static void Lettext(Label l, String s) {
                if (l.Text != s) l.Text = s;
            }
        }

        void UpdateD3D() {
            Ut.Lettext(labelXL, rcBox.Left.ToString("+0;-0"));
            Ut.Lettext(labelXR, rcBox.Right.ToString("+0;-0"));
            Ut.Lettext(labelYT, rcBox.Top.ToString("+0;-0"));
            Ut.Lettext(labelYB, rcBox.Bottom.ToString("+0;-0"));

            try {
                device.CheckCooperativeLevel();
            }
            catch (DeviceNotResetException) {
                return;
            }

            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, p1.BackColor, 1.0f, 0);
            device.BeginScene();

            int ii = 0;
            foreach (Tuple tuple in alt) {
                int i = ii; ii++;
                List<CustomVertex.PositionColoredTextured> alv = tuple.alv;
                if (alv.Count == 0) continue;

                if (lvI.SelectedIndices.Count == 0) { }
                else if (lvI.Items[i].Selected) { }
                else continue;

                VertexBuffer vb = new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), alv.Count, device, Usage.Points, CustomVertex.PositionColoredTextured.Format, Pool.Managed);
                try {
                    {
                        GraphicsStream gs = vb.Lock(0, 0, LockFlags.None);
                        try {
                            for (int x = 0; x < alv.Count; x++) {
                                gs.Write(alv[x]);
                            }
                        }
                        finally {
                            vb.Unlock();
                        }
                    }

                    VertexBuffer vb2 = new VertexBuffer(typeof(CustomVertex.PositionColoredTextured), alv.Count, device, Usage.Points, CustomVertex.PositionColoredTextured.Format, Pool.Managed);
                    try {

                        {
                            int cex = (rcBox.X + rcBox.Right) / 2;
                            Matrix M = Matrix.Translation(-cex, 0, 0);
                            M.Multiply(Matrix.RotationY(roty / 180.0f * 3.14159f));
                            M.Multiply(Matrix.Translation(+cex, 0, 0));
                            device.Transform.View = M;
                        }
                        device.Transform.Projection = Matrix.OrthoOffCenterLH(rcBox.Left, rcBox.Right, rcBox.Bottom, rcBox.Top, +5000.0f, -5000.0f);
                        device.RenderState.Lighting = false;
                        device.RenderState.ColorVertex = true;
                        device.RenderState.CullMode = Cull.None;
                        device.RenderState.ZBufferEnable = false;

                        {
                            {
                                device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
                                device.SetStreamSource(0, vb, 0);
                                device.DrawPrimitives(PrimitiveType.TriangleList, 0, alv.Count / 3);
                            }
                        }

                        {
                            device.ProcessVertices(0, 0, alv.Count, vb2, null);
                            GraphicsStream gs = vb2.Lock(0, 0, LockFlags.ReadOnly);
                            try {
                                float x0 = float.MaxValue, x1 = float.MinValue;
                                float y0 = float.MaxValue, y1 = float.MinValue;
                                for (int x = 0; x < alv.Count; x++) {
                                    CustomVertex.PositionColoredTextured v = (CustomVertex.PositionColoredTextured)gs.Read(typeof(CustomVertex.PositionColoredTextured));
                                    x0 = Math.Min(x0, v.X);
                                    x1 = Math.Max(x1, v.X);
                                    y0 = Math.Min(y0, v.Y);
                                    y1 = Math.Max(y1, v.Y);
                                }
                                labelMM.Text = string.Format("{0}\n{1}\n{2}\n{3}\n", x0, y0, x1, y1);

                                Fillut.L2R(pbB, x0, x1);
                                Fillut.T2B(pbR, y0, y1);
                            }
                            finally {
                                vb2.Unlock();
                            }
                        }
                    }
                    finally {
                        vb2.Dispose();
                    }
                }
                finally {
                    vb.Dispose();
                }
            }

            device.EndScene();
        }

        private void p1_Paint(object sender, PaintEventArgs e) {
            try {
                device.CheckCooperativeLevel();
            }
            catch (DeviceNotResetException) {
                return;
            }
            //Debug.WriteLine("# " + ticks); ticks++;

            device.Present(e.ClipRectangle, e.ClipRectangle, p1);
        }

        class Fillut {
            public static void L2R(PictureBox pb, float x0, float x1) {
                Size s = pb.ClientSize;
                Bitmap pic = new Bitmap(s.Width, s.Height);
                using (Graphics cv = Graphics.FromImage(pic)) {
                    x0 = (x0 + 1) / 2;
                    x1 = (x1 + 1) / 2;
                    cv.Clear(Color.Black);
                    cv.FillRectangle(Brushes.Blue, RectangleF.FromLTRB(s.Width * x0, 0, s.Width * x1, s.Height));
                }
                pb.Image = pic;
            }

            public static void T2B(PictureBox pb, float _y0, float _y1) {
                Size s = pb.ClientSize;
                Bitmap pic = new Bitmap(s.Width, s.Height);
                using (Graphics cv = Graphics.FromImage(pic)) {
                    float y0 = 1.0f - (_y1 + 1) / 2;
                    float y1 = 1.0f - (_y0 + 1) / 2;
                    cv.Clear(Color.Black);
                    cv.FillRectangle(Brushes.Blue, RectangleF.FromLTRB(0, s.Height * y0, s.Width, s.Height * y1));
                }
                pb.Image = pic;
            }
        }

        private void RenderForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.Handled) return;
            Rectangle rcBox = this.rcBox;
            int rotY = this.roty;
            switch (e.KeyCode) {
                case Keys.W: Tfrm(+0, -1); e.Handled = true; break;
                case Keys.S: Tfrm(+0, +1); e.Handled = true; break;
                case Keys.A: Tfrm(-1, +0); e.Handled = true; break;
                case Keys.D: Tfrm(+1, +0); e.Handled = true; break;
                case Keys.PageDown: Roty(-5); e.Handled = true; break;
                case Keys.PageUp: Roty(+5); e.Handled = true; break;
                case Keys.Home: Roty(); e.Handled = true; break;
                case Keys.OemOpenBrackets: Scalefrm(1.0f / 1.2f); e.Handled = true; break;
                case Keys.OemCloseBrackets: Scalefrm(1.2f / 1.0f); e.Handled = true; break;
            }
            if (this.rcBox != rcBox || this.roty != rotY) {
                UpdateD3D();
                p1.Invalidate();
            }
        }

        void Roty() {
            roty = 0;
        }
        void Roty(int d) {
            roty += d;
        }

        private void Tfrm(int dx, int dy) {
            rcBox.X += dx * rcBox.Width / 50;
            rcBox.Y += dy * rcBox.Height / 50;
        }

        private void Scalefrm(float f) {
            float x = (rcBox.X + rcBox.Right) / 2;
            float y = (rcBox.Y + rcBox.Bottom) / 2;
            float fw = Math.Max(10.0f, rcBox.Width * f);
            float fh = Math.Max(10.0f, rcBox.Height * f);
            rcBox = new Rectangle((int)(x - fw / 2), (int)(y - fh / 2), (int)fw, (int)fh);
        }

        private void p1_KeyDown(object sender, KeyEventArgs e) {
        }

        private void lvI_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateD3D();
            p1.Invalidate();
        }

        private void p1_Resize(object sender, EventArgs e) {
        }
    }
}