using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using System.Diagnostics;

namespace vYaik1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
        }

        private void redraw() {
            panel1.Invalidate();
            panel3.Invalidate();
            panel2.Invalidate();
            panel4.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e) {
            alOrg.Add(new Bone(new Vector3(0, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), +20.0f / 180.0f * 3.14f), -1));
            alOrg.Add(new Bone(new Vector3(80, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), +20.0f / 180.0f * 3.14f), 0));
            alOrg.Add(new Bone(new Vector3(80, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), +20.0f / 180.0f * 3.14f), 1));
            alOrg.Add(new Bone(new Vector3(80, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), 0.0f / 180.0f * 3.14f), 2));
            alOrg.Add(new Bone(new Vector3(0, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), +45.0f / 180.0f * 3.14f), 3));
            alOrg.Add(new Bone(new Vector3(0, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), -45.0f / 180.0f * 3.14f), 3));
            alOrg.Add(new Bone(new Vector3(30, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), 0.0f / 180.0f * 3.14f), 4));
            alOrg.Add(new Bone(new Vector3(30, 0, 0), Quaternion.RotationAxis(new Vector3(0, 0, 1), 0.0f / 180.0f * 3.14f), 5));

            if (true) {
                Matrix M = new Matrix();
                M.M11 = 1;
                M.M22 = 1;
                M.M44 = 1;
                Mv[0] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M31 = 1;
                M.M22 = 1;
                M.M44 = 1;
                Mv[1] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M11 = 1;
                M.M32 = -1;
                M.M44 = 1;
                Mv[2] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M44 = 1;
                Mv[3] = M;
            }

            recalc();
        }

        /// <summary>
        /// 一般的なBone構造
        /// </summary>
        class Bone {
            /// <summary>
            /// 頂点座標
            /// </summary>
            public Vector3 v = Vector3.Empty;
            /// <summary>
            /// 回転情報
            /// </summary>
            public Quaternion r = Quaternion.Identity;

            public int parent = -1;

            public Bone() { }
            public Bone(Vector3 v, Quaternion r, int parent) { this.v = v; this.r = r; this.parent = parent; }

            public Bone Clone() {
                return (Bone)MemberwiseClone();
            }

            public override string ToString() {
                return string.Format("Qx {0,10:0.000} Qy {1,10:0.000} Qz {2,10:0.000} Qw {3,10:0.000} | Tx {4,10:0.000} Ty {5,10:0.000} Tz {6,10:0.000}"
                    , r.X, r.Y, r.Z, r.W, v.X, v.Y, v.Z);
            }
        }
        /// <summary>
        /// 相対的な量を持つ　計算前の　Bone構造（オリジナル）
        /// </summary>
        List<Bone> alOrg = new List<Bone>();
        /// <summary>
        /// 絶対的な量を持つ　計算後の　Bone構造
        /// </summary>
        List<Bone> alGo = new List<Bone>();

        /// <summary>
        /// 到着地点
        /// </summary>
        Vector3 ptik = Vector3.Empty;

        /// <summary>
        /// 指定した子Boneを回転する。但し親Boneの回転情報は無視してprotqの方を使う
        /// </summary>
        /// <param name="bi">再計算したい子ボーン</param>
        /// <param name="protq">親ボーンの回転情報</param>
        void calcBone(int bi, Quaternion protq) {
            Bone o = alGo[bi];
            if (o.parent >= 0) {
                Bone p = alGo[o.parent];
                o.r = protq * o.r;
                o.v = p.v + Vector3.TransformCoordinate(alOrg[bi].v, Matrix.RotationQuaternion(protq));
            }
        }

        /// <summary>
        /// Boneを計算する
        /// </summary>
        void recalc() {
            if (alGo.Count == 0 || checkBoxKeep.Checked == false) {
                alGo.Clear();
                for (int t = 0; t < alOrg.Count; t++) {
                    Bone o = alOrg[t].Clone();
                    if (o.parent >= 0) {
                        Bone p = alGo[o.parent];
                        o.r = p.r * o.r;
                        o.v = p.v + Vector3.TransformCoordinate(o.v, Matrix.RotationQuaternion(p.r));
                    }
                    alGo.Add(o);
                }
            }
            if (ptik != Vector3.Empty) {
                int cw = (int)numericUpDownRepeat.Value;
                int a = 0;
                bool ffk = false;
                for (int t = 0; t < alOrg.Count; t++) {
                    Bone o = alGo[t];
                    if (t == 3) {
                        for (int w = 0; w < cw; w++) {
                            if (t - a == 3) { // move 2 joints
                                //
                                Quaternion rot2 = alGo[a + 2].r * CalcUtil.cross(alGo[a + 2].v, alGo[a + 3].v, ptik);
                                calcBone(a + 3, rot2);
                                //
                                Quaternion rot1 = alGo[a + 1].r * CalcUtil.cross(alGo[a + 1].v, alGo[a + 3].v, ptik);
                                calcBone(a + 2, rot1);
                                calcBone(a + 3, rot2);
                                //
                                Quaternion rot0 = alGo[a + 0].r * CalcUtil.cross(alGo[a + 0].v, alGo[a + 3].v, ptik);
                                calcBone(a + 1, rot0);
                                calcBone(a + 2, rot1);
                                calcBone(a + 3, rot2);
                                //
                                alGo[a + 0].r = rot0;
                                alGo[a + 1].r = rot1;
                                alGo[a + 2].r = rot2;
                            }
                        }
                        ffk = true;
                    }
                    if (ffk && o.parent >= 0) {
                        Bone p = alGo[o.parent];
                        o.r = p.r * alOrg[t].r;
                        o.v = p.v + Vector3.TransformCoordinate(alOrg[t].v, Matrix.RotationQuaternion(p.r));
                    }
                }
            }
            string s = "";
            for (int t = 0; t < Math.Min(4, alGo.Count); t++) {
                s += string.Format("{0}|", t) + alGo[t].ToString() + "\n";
            }
            labelHint.Text = s;
            labelHint.ForeColor = Color.FromArgb(50, this.ForeColor);
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
                v1 -= v0; v1.Normalize(); NaNUtil.testNaN(v1);
                v2 -= v0; v2.Normalize(); NaNUtil.testNaN(v2);
                float dot = Vector3.Dot(v1, v2); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                Vector3 cross = Vector3.Cross(v1, v2); NaNUtil.testNaN(cross);
                return Quaternion.RotationAxis(cross, rad);
            }
        }

        class NaNUtil {
            public static void testNaN(Vector3 v) {
                Debug.Assert(!float.IsNaN(v.X), "v.X is NaN");
                Debug.Assert(!float.IsNaN(v.Y), "v.Y is NaN");
                Debug.Assert(!float.IsNaN(v.Z), "v.Z is NaN");
            }
        }

        [Description("http://monsho.hp.infoseek.co.jp/dx/dx48.html")]
        private void Form1_Paint(object sender, PaintEventArgs e) {
        }

        /// <summary>
        /// ボーン間接をわかりやすく描画する
        /// </summary>
        class ArrowUtil {
            public static void draw(Graphics cv, Pen pen, Vector3 v0, Vector3 v1) {
                Vector3 vf = v1 - v0; vf.Normalize();
                Vector3 vr = Vector3.TransformCoordinate(vf, Matrix.RotationZ(3.14f / 2));
                float flen = 10;
                DUtil.drawLine(cv, pen, v0, v1);
                DUtil.drawCircle(cv, pen, v0, flen);
                Vector3 vxl = v0 - vr * flen;
                Vector3 vxr = v0 + vr * flen;
                Vector3 vxb = v0 - vf * flen;
                DUtil.drawLine(cv, pen, v0, vxl);
                DUtil.drawLine(cv, pen, v0, vxr);
                DUtil.drawLine(cv, pen, v0, vxb);
                DUtil.drawLine(cv, pen, vxl, v1);
                DUtil.drawLine(cv, pen, vxr, v1);
            }
        }
        class DUtil {
            public static void fillBPt(Graphics cv, Brush br, Vector3 v0) {
                cv.FillRectangle(br, v0.X - 1, v0.Y - 1, 3.0f, 3.0f);
            }
            public static void drawLine(Graphics cv, Pen pen, Vector3 v0, Vector3 v1) {
                cv.DrawLine(pen, v0.X, v0.Y, v1.X, v1.Y);
            }
            public static void drawCircle(Graphics cv, Pen pen, Vector3 v0, float r) {
                cv.DrawEllipse(pen, v0.X - r, v0.Y - r, 2 * r, 2 * r);
            }
            public static void drawAxStr(Graphics cv, Font font, Color clr, int x, int y, string s) {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                cv.DrawString(s, font, new SolidBrush(clr), x, y, sf);
            }
        }

        private void Form1_Resize(object sender, EventArgs e) {
            redraw();
        }

        private void numericUpDownRepeat_ValueChanged(object sender, EventArgs e) {
            recalc(); redraw();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        Matrix[] Mv = new Matrix[4];

        Matrix getM(object sender) {
            if (sender == panel2) return Mv[1];
            if (sender == panel3) return Mv[2];
            if (sender == panel4) return Mv[3];
            return Mv[0];
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Matrix M = getM(sender);
            Graphics cv = e.Graphics;
            System.Drawing.Drawing2D.GraphicsState gstate = cv.Save();
            Control Me = (Control)sender;
            try {
                int cx = Me.ClientSize.Width;
                int cy = Me.ClientSize.Height;
                cv.TranslateTransform(cx / 2, cy / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                cv.DrawLine(Pens.Gray, 0, -100, 0, +100);
                cv.DrawLine(Pens.Gray, -100, 0, +100, 0);
                if (sender == panel1) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, cy / 2 - 10, "+y"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+x"); }
                if (sender == panel2) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, cy / 2 - 10, "+y"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+z"); }
                if (sender == panel3) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, cy / 2 - 10, "+z"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+x"); }

                for (int t = 0; t < alGo.Count; t++) {
                    Bone o = alGo[t];
                    Bone p = (o.parent < 0) ? null : alGo[o.parent];
                    if (p != null) {
                        ArrowUtil.draw(cv, Pens.DarkBlue, Vector3.TransformCoordinate(p.v, M), Vector3.TransformCoordinate(o.v, M));
                    }
                }
                DUtil.fillBPt(cv, Brushes.Magenta, Vector3.TransformCoordinate(ptik, M));
            }
            finally {
                cv.Restore(gstate);
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            ((Control)sender).Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                int cx = ((Control)sender).ClientSize.Width;
                int cy = ((Control)sender).ClientSize.Height;

                if (sender == panel1) ptik = new Vector3(e.X - cx / 2, +(e.Y - cy / 2), ptik.Z);
                if (sender == panel2) ptik = new Vector3(ptik.X, +(e.Y - cy / 2), +(e.X - cx / 2));
                if (sender == panel3) ptik = new Vector3(e.X - cx / 2, ptik.Y, -(e.Y - cy / 2));

                recalc(); redraw();
            }
        }
    }
}