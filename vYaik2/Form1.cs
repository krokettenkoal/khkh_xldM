using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Microsoft.DirectX;

namespace vYaik2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            alpt.Add(new Ball(new PointF(0, 0)));
            alpt.Add(new Ball(new PointF(100, 50)));
            alpt.Add(new Ball(new PointF(0, 100)));
            alpt.Add(new Ball(new PointF(100, 200)));
        }

        private void pb1_Click(object sender, EventArgs e) {

        }

        class Ball {
            public PointF pt;

            public Vector2 Vec {
                get { return new Vector2(pt.X, pt.Y); }
                set { pt = new PointF(value.X, value.Y); }
            }

            public Ball(PointF pt) {
                this.pt = pt;
            }
        }
        List<Ball> alpt = new List<Ball>();

        class ArrUt {
            public static void Draw(Graphics cv, PointF p0, PointF p1) {
                Pen p = Pens.BlueViolet;
                float rl = 7;
                cv.DrawEllipse(p, p0.X - rl, p0.Y - rl, rl + rl, rl + rl);

                PointF p10 = new PointF(p1.X - p0.X, p1.Y - p0.Y);
                float pl = (float)(1.0 / Math.Sqrt((p10.X * p10.X) + (p10.Y * p10.Y)));
                p10.X *= pl;
                p10.Y *= pl;

                float rad = (float)(90.0 / 180.0 * Math.PI);
                PointF pfwd = p10;
                PointF prh = new PointF(
                    (float)(p10.X * Math.Cos(rad) + p10.Y * Math.Sin(rad)),
                    (float)(p10.X * -Math.Sin(rad) + p10.Y * Math.Cos(rad))
                    );

                cv.DrawLine(p, new PointF(p0.X - pfwd.X * rl, p0.Y - pfwd.Y * rl), p1);
                cv.DrawLine(p,
                    new PointF(p0.X - prh.X * rl, p0.Y - prh.Y * rl),
                    new PointF(p0.X + prh.X * rl, p0.Y + prh.Y * rl)
                    );
                cv.DrawLine(p, new PointF(p0.X - prh.X * rl, p0.Y - prh.Y * rl), p1);
                cv.DrawLine(p, new PointF(p0.X + prh.X * rl, p0.Y + prh.Y * rl), p1);
            }
        }

        System.Drawing.Drawing2D.Matrix tri = new System.Drawing.Drawing2D.Matrix();

        private void pb1_Paint(object sender, PaintEventArgs e) {
            Graphics cv = e.Graphics;
            Rectangle rc = pb1.ClientRectangle;
            {
                int x1 = (rc.X + rc.Right) / 2;
                int y1 = (rc.Y + rc.Bottom) / 2;
                cv.TranslateTransform(x1, y1);
                cv.DrawLine(Pens.Silver, new Point(-x1, 0), new Point(+x1, 0));
                cv.DrawLine(Pens.Silver, new Point(0, -y1), new Point(0, +y1));
            }
            for (int x = 0; x < alpt.Count - 1; x++) {
                ArrUt.Draw(cv, alpt[x].pt, alpt[x + 1].pt);
            }
            tri = cv.Transform;
            tri.Invert();
        }

        private void pb1_MouseDown(object sender, MouseEventArgs e) {
            Point[] pts = new Point[] { e.Location };
            tri.TransformPoints(pts);
            Specify(pts[0]);
        }

        private void pb1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                Point[] pts = new Point[] { e.Location };
                tri.TransformPoints(pts);
                Specify(pts[0]);
            }
        }

        private void Specify(Point pt) {
            Vector2 tpt = new Vector2(pt.X, pt.Y);
            // 0 1 2 3 PT
            //     2   PT
            //   1 2   PT
            // 0 1 2   PT

            float rad;

            rad = IKUt.cross(alpt[0].Vec, alpt[3].Vec, tpt);
            alpt[1].Vec = IKUt.rotate(alpt[0].Vec, alpt[1].Vec, rad);
            alpt[2].Vec = IKUt.rotate(alpt[0].Vec, alpt[2].Vec, rad);
            alpt[3].Vec = IKUt.rotate(alpt[0].Vec, alpt[3].Vec, rad);

            rad = IKUt.cross(alpt[1].Vec, alpt[3].Vec, tpt);
            alpt[2].Vec = IKUt.rotate(alpt[1].Vec, alpt[2].Vec, rad);
            alpt[3].Vec = IKUt.rotate(alpt[1].Vec, alpt[3].Vec, rad);

            rad = IKUt.cross(alpt[2].Vec, alpt[3].Vec, tpt);
            alpt[3].Vec = IKUt.rotate(alpt[2].Vec, alpt[3].Vec, rad);

            Debug.WriteLine("# " + (rad / 3.14159 * 180));
            pb1.Refresh();
        }

        class IKUt {
            public static float cross(Vector2 v0, Vector2 v1, Vector2 v2) {
                v1 -= v0; v1.Normalize(); Debug.Assert(!float.IsNaN(v1.X) && !float.IsNaN(v1.Y), "v1 is NaN");
                v2 -= v0; v2.Normalize(); Debug.Assert(!float.IsNaN(v2.X) && !float.IsNaN(v2.Y), "v2 is NaN");
                float dot = Vector2.Dot(v1, v2); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                float ccw = Vector2.Ccw(v1, v2);
                return rad * ((ccw < 0) ? -1 : +1);
            }
            public static Vector2 rotate(Vector2 v0, Vector2 v1, float rad) {
                v1.X -= v0.X;
                v1.Y -= v0.Y;
                v1 = Vector2.TransformCoordinate(v1, Microsoft.DirectX.Matrix.RotationQuaternion(Quaternion.RotationAxis(new Vector3(0, 0, +1), rad)));
                v1.X += v0.X;
                v1.Y += v0.Y;
                return v1;
            }
        }
    }
}