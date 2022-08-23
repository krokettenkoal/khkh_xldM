using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.DirectX;

namespace vikDIAG {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        class Box {
            public Vector2 vec2;

            public PointF p2 { get { return new PointF(vec2.X, vec2.Y); } }

            public Box(Vector2 vec2) {
                this.vec2 = vec2;
            }
        }
        List<Box> albox = new List<Box>();
        Vector2 tpt2 = Vector2.Empty;

        class Ut {
            public static Vector2 Tov2(string s) {
                string[] al = s.Split(',');
                return new Vector2(float.Parse(al[0]), float.Parse(al[1]));
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            albox.Clear();
            foreach (ListViewItem lvi in listView1.SelectedItems) {
                XmlElement eli = (XmlElement)lvi.Tag;
                if (eli != null) {
                    foreach (XmlElement elo in eli.SelectNodes("o")) {
                        albox.Add(new Box(Ut.Tov2(elo.GetAttribute("v2"))));
                    }
                    tpt2 = Ut.Tov2(eli.GetAttribute("tpt2"));
                    label1.Text = string.Format("rad={0}Åã", (int)(float.Parse(eli.GetAttribute("rad")) / 3.14159f * 180.0f));
                }
                break;
            }

            pb1.Invalidate();
        }

        class XrsUt {
            public static void Draw(Graphics cv, PointF p0) {
                float w = 2;
                cv.DrawLine(Pens.Blue, p0.X - w, p0.Y - w, p0.X + w, p0.Y + w);
                cv.DrawLine(Pens.Blue, p0.X - w, p0.Y + w, p0.X + w, p0.Y - w);
            }
        }
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

        XmlDocument xmlo = new XmlDocument();

        private void Form1_Load(object sender, EventArgs e) {
            xmlo.Load(@"H:\Proj\khkh_xldM\MEMO\IKDIAG\IKDIAG.xml");
            int gi = 0;
            foreach (XmlElement elg in xmlo.SelectNodes("/IKDIAG/g")) {
                int ii = 0;
                foreach (XmlElement eli in elg.SelectNodes("i")) {
                    ListViewItem lvi = listView1.Items.Add("#" + gi + " " + ii);
                    lvi.Tag = eli;
                    ii++;
                }
                gi++;
            }
        }

        private void pb1_Paint(object sender, PaintEventArgs e) {
            Graphics cv = e.Graphics;
            int x1 = pb1.ClientSize.Width / 2;
            int y1 = pb1.ClientSize.Height / 2;
            cv.TranslateTransform(x1, y1);
            cv.ScaleTransform(2, 2);
            cv.DrawLine(Pens.Silver, -x1 + 2, 0, x1 - 2, 0);
            cv.DrawLine(Pens.Silver, 0, -y1 + 2, 0, y1 - 2);
            XrsUt.Draw(cv, new PointF(tpt2.X, tpt2.Y));

            for (int i = 0; i < albox.Count - 1; i++) {
                Box b0 = albox[i];
                Box b1 = albox[i + 1];
                ArrUt.Draw(cv, b0.p2, b1.p2);
            }
        }
    }
}