using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;

namespace vrot {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        PointF[] pts = new PointF[] { PointF.Empty, PointF.Empty, PointF.Empty, };
        int sel = 0;

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            if (sender == radioButton1) sel = 0;
            if (sender == radioButton2) sel = 1;
            if (sender == radioButton3) sel = 2;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            pts[sel] = new PointF(e.X, e.Y);
            panel1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics cv = e.Graphics;

            Pen p2 = new Pen(Color.FromArgb(100, Color.Magenta), 1.5f);
            cv.DrawLine(p2, pts[0], pts[1]);
            cv.DrawLine(p2, pts[0], pts[2]);
            for (int x = 0; x < 3; x++) {
                Pen pen = null;
                if (x == 0) pen = Pens.Red;
                if (x == 1) pen = Pens.LightGreen;
                if (x == 2) pen = Pens.BlueViolet;
                Point pt = Point.Round(pts[x]);
                cv.FillRectangle(new SolidBrush(pen.Color), pt.X - 1, pt.Y - 1, 3, 3);
            }

            calc(cv);
        }

        private void calc(Graphics cv) {
            Vector3 vp = new Vector3(pts[0].X, pts[0].Y, 0);
            Vector3 vc1 = new Vector3(pts[1].X, pts[1].Y, 0) - vp;
            Vector3 vc2 = new Vector3(pts[2].X, pts[2].Y, 0) - vp;
            vc1.Normalize();
            vc2.Normalize();
            Vector3 vcross = Vector3.Cross(vc1, vc2);
            vcross.Normalize();
            float fd = Vector3.Dot(vc1, vc2);
            float fa = (float)Math.Acos(fd);

            Quaternion quat = Quaternion.RotationAxis(vcross, fa);
            Vector3 vt2 = Vector3.TransformCoordinate(vc1, Matrix.RotationQuaternion(quat));
            vt2.Normalize();
            label1.Text = ("(" + vcross + ")" + " " + fd + " " + fa + " " + "(" + vc2 + ")¨(" + vt2 + ")").Replace("\n", " ");

            vt2 *= 100.0f;
            cv.DrawLine(Pens.Maroon, new PointF(vp.X, vp.Y), new PointF(vp.X + vt2.X, vp.Y + vt2.Y));
        }
    }
}