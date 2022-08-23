using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Render2DMover {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void frameNum_ValueChanged(object sender, EventArgs e) {
            draw.Invalidate();
        }

        List<PointF[]> list = new List<PointF[]>();

        private void pasteBtn_Click(object sender, EventArgs e) {
            list.Clear();

            var lines = Clipboard.GetText().Replace("\r\n", "\n").Split('\n');
            foreach (var line in lines) {
                var cols = line.Replace("),(", "|").Replace("(", "").Replace(")", "").Split('|');
                var points = new List<PointF>();
                foreach (var col in cols) {
                    var xy = col.Split(',');
                    if (xy.Length == 2) {
                        points.Add(new PointF(float.Parse(xy[0]), float.Parse(xy[1])));
                    }
                }
                list.Add(points.ToArray());
            }

            draw.Invalidate();
        }

        private void draw_Paint(object sender, PaintEventArgs e) {
            if (list.Count != 0) {
                try {
                    int index = (int)frameNum.Value % list.Count;
                    var canvas = e.Graphics;
                    var maxX = draw.ClientSize.Width;
                    var maxY = draw.ClientSize.Height;
                    var cenX = maxX / 2;
                    var cenY = maxY / 2;
                    canvas.TranslateTransform(cenX, cenY);
                    canvas.DrawLine(Pens.Gray, -cenX, 0, cenX, 0);
                    canvas.DrawLine(Pens.Gray, 0, -cenY, 0, cenY);

                    var points = list[index];

                    DrawLine(canvas, Pens.Blue, points[0], points[1]);
                    DrawLine(canvas, Pens.Blue, points[1], points[2]);
                    DrawLine(canvas, Pens.Blue, points[2], points[3]);
                    DrawLine(canvas, Pens.Blue, points[3], points[4]);
                    DrawLine(canvas, Pens.Red, points[5], points[6]);
                    DrawLine(canvas, Pens.Red, points[6], points[7]);
                    DrawLine(canvas, Pens.Red, points[7], points[8]);
                }
                catch (OverflowException) {
                    // ignore
                }
                catch (IndexOutOfRangeException) {
                    // ignore
                }
            }
        }

        private void DrawLine(Graphics canvas, Pen pen, PointF a, PointF b) {
            canvas.DrawLine(pen, a, b);
            canvas.DrawRectangle(pen, b.X - 2, b.Y - 2, 4, 4);
        }

        private void Form1_Resize(object sender, EventArgs e) {
            draw.Invalidate();
        }
    }
}
