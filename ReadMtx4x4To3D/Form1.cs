using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX;
using System.Drawing.Imaging;

namespace ReadMtx4x4To3D {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            loadIt(@"H:\EMU\pcsx2-0.9.4\memo\ss4.dump.raw");
        }

        FileStream fs;

        class Cand {
            public int off; // offset of Bone Matrices
            public int cnt; // count of Bone Matrices
            public int offMv; // offset Matrix of Viewport conversion (1 matrix in VIF packet)

            public Cand(int off, int cnt, int offMv) {
                this.off = off;
                this.cnt = cnt;
                this.offMv = offMv;
            }
        }
        Cand[] alcand = new Cand[] {
            new Cand(0x01AC12D0 - 0x17 * 0x40, 0xE5, 0x1968BC0), // Roxas
            new Cand(0x01AC6030 - 2 * 0x40, 12, 0x1968E90), // Keyblade
        };

        private void loadIt(string fp) {
            if (fs != null) {
                fs.Dispose();
                fs = null;
            }
            fs = File.OpenRead(fp);

            Cand o = alcand[0];
            fs.Position = o.off;
            for (int x = 0; x < o.cnt; x++) {
                Matrix M = Ut.ReadMtx(new BinaryReader(fs));
                Vector3 v0 = new Vector3(0, 0, 0); v0 = Vector3.TransformCoordinate(v0, M);
                Vector3 v1 = new Vector3(10, 10, 10); v1 = Vector3.TransformCoordinate(v1, M);
                string s = string.Format("#{0,3} T({1,8:0.00},{2,8:0.00},{3,8:0.00})"
                    , x
                    , v0.X, v0.Y, v0.Z
                    );
                listView1.Items.Add(s);
            }
        }

        class Ut {
            public static Matrix ReadMtx(BinaryReader br) {
                Matrix M = new Matrix();
                M.M11 = br.ReadSingle(); M.M12 = br.ReadSingle(); M.M13 = br.ReadSingle(); M.M14 = br.ReadSingle();
                M.M21 = br.ReadSingle(); M.M22 = br.ReadSingle(); M.M23 = br.ReadSingle(); M.M24 = br.ReadSingle();
                M.M31 = br.ReadSingle(); M.M32 = br.ReadSingle(); M.M33 = br.ReadSingle(); M.M34 = br.ReadSingle();
                M.M41 = br.ReadSingle(); M.M42 = br.ReadSingle(); M.M43 = br.ReadSingle(); M.M44 = br.ReadSingle();
                return M;
            }
        }

        private void panel1_Load(object sender, EventArgs e) {

        }

        Bitmap pic = new Bitmap(500, 500);

        private void panel1_Paint(object sender, PaintEventArgs e) {
            float sx = 15;
            using (Graphics cv = Graphics.FromImage(pic)) {
                System.Drawing.Drawing2D.Matrix A = cv.Transform;
                Size size = panel1.ClientSize;
                A.Translate(-2048, -2048, System.Drawing.Drawing2D.MatrixOrder.Append);
                A.Scale(500.0f / 4096.0f, 500.0f / 4096.0f, System.Drawing.Drawing2D.MatrixOrder.Append);
                A.Scale(sx, sx, System.Drawing.Drawing2D.MatrixOrder.Append);
                A.Translate(size.Width / 2, size.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                cv.Transform = A;

                cv.Clear(panel1.BackColor);

                cv.DrawLine(Pens.LightGray, 1, 1, 4000, 1);
                cv.DrawLine(Pens.LightGray, 1, 1, 1, 4000);

                Pen[] pens = new Pen[] { Pens.Green, Pens.Yellow };
                BinaryReader br = new BinaryReader(fs);
                for (int x = 0; x < alcand.Length; x++) {
                    Cand o = alcand[x];
                    fs.Position = o.offMv;
                    Matrix Mv = Ut.ReadMtx(br);

                    fs.Position = o.off;
                    for (int t = 0; t < o.cnt; t++) {
                        Matrix M = Ut.ReadMtx(br);
                        M = Matrix.Multiply(M, Mv);
                        float vx = M.M41 / M.M44;
                        float vy = M.M42 / M.M44;
                        RectangleF rc = new RectangleF(vx, vy, 3, 3);
                        rc.Offset(-1, -1);
                        cv.DrawRectangle(pens[x], rc.X, rc.Y, rc.Width, rc.Height);
                    }
                }
            }
            e.Graphics.DrawImageUnscaled(pic, 0, 0);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string fp in fs) {
                loadIt(fp);
                break;
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : e.Effect);
        }
    }
}