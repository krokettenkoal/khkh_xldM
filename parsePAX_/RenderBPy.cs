using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Prayvif1;

namespace parsePAX_ {
    public partial class RenderBPy : UserControl {
        public RenderBPy() {
            InitializeComponent();
        }

        BPyWriter bpy;

        public void Eat(BPyWriter bpy) {
            this.bpy = bpy;
            Invalidate();
        }

        private void RenderBPy_Load(object sender, EventArgs e) {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void RenderBPy_Paint(object sender, PaintEventArgs e) {
            if (bpy == null) return;

            IList<BPyWriter.V> alv = bpy.Alv;
            IList<BPyWriter.Tri> alt = bpy.Alt;

            Graphics cv = e.Graphics;
            SizeF ptc = new SizeF(ClientSize.Width / 2, ClientSize.Height / 2);

            foreach (BPyWriter.Tri t in alt) {
                PointF p0 = PUt.Eat(alv[t.v0]) + ptc;
                PointF p1 = PUt.Eat(alv[t.v1]) + ptc;
                PointF p2 = PUt.Eat(alv[t.v2]) + ptc;

                cv.DrawPolygon(Pens.Black, new PointF[] { p0, p1, p2 });
            }
        }

        class PUt {
            const float fact = 1.5f;

            internal static PointF Eat(BPyWriter.V v) {
                return new PointF(
                    (v.v.X - 2048) * fact,
                    (-v.v.Y + 2048) * fact / 3f * 2f
                    );
            }
        }
    }
}
