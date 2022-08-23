using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using SlimDX.Direct3D9;
using SlimDX;
using khkh_xldM;
using khiiMapv;
using parseSEQD.Seqd;
using parseSEQD.Layd;

namespace parseSEQD {
    public partial class PForm : Form {
        public PForm() {
            InitializeComponent();
        }

#if DEBUG
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\menu_fm_pause.2ld";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\menu_fm_title.2ld";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\tt_pt.2ld";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\obj_P_EX100.a.fm";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\field2d_jp_zz0command.2dd";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\field2d_jp_zz0field.2dd";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\field2d_jp_tt0command.2dd";
        //String fp2ld = @"H:\EMU\pcsx2-0.9.4\expa\field2d_jp_tt0field.2dd";
        //String fp2ld = @"H:\KH2fm.Falo\kh2\export\field2d\jp\tt1command.2dd";
        String fp2ld = @"H:\KH2fm.Falo\kh2\export\file\jp\he_co.2ld";
#endif

        MemoryStream si = null;

        List<Bitmap> alp = new List<Bitmap>();
        List<PicIMGD> alpi = new List<PicIMGD>();

        private void Form1_Load(object sender, EventArgs e) {
#if DEBUG
            if (File.Exists(fp2ld))
                Read2ld(fp2ld);
#endif

            if (lbbar.Items.Count != 0)
                lbbar.SelectedIndex = 0;
            if (lbLayd.Items.Count != 0)
                lbLayd.SelectedIndex = 0;
        }

        class Pair {
            public List<PicIMGD> alpi = new List<PicIMGD>();
            public byte[] layd = null, seqd = null;
            public String namepic = "", namelay = "";

            public override string ToString() {
                return namepic + "*" + namelay;
            }
        }
        List<Pair> alpair = new List<Pair>();

        private void Read2ld(String fp2ld) {
            alpair.Clear();
            lbbar.Items.Clear();
            lbLayd.Items.Clear();

            using (FileStream fs = File.OpenRead(fp2ld)) {
                String name1d = "", name18 = "";
                PicIMGD[] al1d = null;
                foreach (ReadBar.Barent bar in ReadBar.Explode(fs)) {
                    if (bar.k == 0x1D) { // IMGZ/IMGD
                        al1d = ParseIMGZ.TakeIMGZ(bar.bin);
                        name1d = bar.id;
                    }
                    else if (bar.k == 0x1C) { // LAYD/SEQD
                        Pair p = new Pair();
                        p.alpi.AddRange(al1d);
                        p.layd = bar.bin;
                        p.namepic = name1d;
                        p.namelay = bar.id;
                        lbbar.Items.Add(p);
                    }
                    else if (bar.k == 0x18) { // IMGD
                        al1d = new PicIMGD[] { ParseIMGD.TakeIMGD(new MemoryStream(bar.bin, false)) };
                        name18 = bar.id;
                    }
                    else if (bar.k == 0x19) { // SEDQ
                        Pair p = new Pair();
                        p.alpi.AddRange(al1d);
                        p.seqd = bar.bin;
                        p.namepic = name18;
                        p.namelay = bar.id;
                        lbbar.Items.Add(p);
                    }
                }
            }
        }

        private void lbbar_SelectedIndexChanged(object sender, EventArgs e) {
            Pair p = lbbar.SelectedItem as Pair;
            if (p == null) return;

            this.alpi.Clear();
            this.alpi.AddRange(p.alpi);

            if (p.layd != null) {
                ReadLayd(p.layd);
            }
            else {
                all1.Clear(); lvl1.Items.Clear();
                all2.Clear(); lvl2.Items.Clear();
                all3.Clear();

                Laydi o = new Laydi();
                o.pici = 0;
                o.si = new MemoryStream(p.seqd);

                ReadSeqd2(o);
            }
        }

        List<L1> all1 = new List<L1>();
        List<L2> all2 = new List<L2>();
        List<int> all3 = new List<int>();

        private void ReadLayd(byte[] bin) {
            MemoryStream si = new MemoryStream(bin, false);
            BinaryReader br = new BinaryReader(si);
            String head;
            if ((head = Encoding.ASCII.GetString(br.ReadBytes(4))) != "LAYD")
                throw new InvalidDataException(head + " ÅÇ LAYD");

            {
                all1.Clear();
                si.Position = 0x08;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 24 * x;
                    all1.Add(new L1(br));
                }
            }

            {
                int x = 0;
                ListView lv = lvl1;
                lv.Items.Clear();
                foreach (L1 o in all1) {
                    ListViewItem lvi = lv.Items.Add(x.ToString());
                    lvi.SubItems.Add(o.ToString());

                    Debug.WriteLine(String.Format("L1[{0,2}] {1}", x, o.ToString()));
                    x++;
                }
            }

            {
                all2.Clear();
                si.Position = 0x10;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 20 * x;
                    all2.Add(new L2(br));
                }
            }

            {
                int x = 0;
                ListView lv = lvl2;
                lv.Items.Clear();
                foreach (L2 o in all2) {
                    ListViewItem lvi = lv.Items.Add(x.ToString());
                    lvi.SubItems.Add(o.ToString());

                    Debug.WriteLine(String.Format("L2[{0,2}] {1}", x, o.ToString()));
                    x++;
                }
            }

            {
                all3.Clear();
                si.Position = 0x18;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                si.Position = off;
                for (int x = 0; x < cnt; x++) {
                    int offIt = br.ReadInt32();
                    all3.Add(offIt);
                }
            }

            {
                lbLayd.Items.Clear();
                for (int x = 0; x < all3.Count; x++) {
                    int offSeqd = all3[x];
                    MemoryStream siSeqd = new MemoryStream(bin, offSeqd, bin.Length - offSeqd, false);
                    Laydi o = new Laydi();
                    o.si = siSeqd;
                    o.pici = x;
                    lbLayd.Items.Add(o);
                }
            }
        }

        private void lbLayd_SelectedIndexChanged(object sender, EventArgs e) {
            Laydi o = lbLayd.SelectedItem as Laydi;
            if (o == null) return;
            ReadSeqd2(o);
        }

        class Laydi {
            public int pici = 0;
            public MemoryStream si;

            public override string ToString() {
                return pici.ToString();
            }
        }

        int pici = 0; // seqd picture

        void ReadSeqd2(Laydi o) {
            Bitmap picsrc = alpi[this.pici = o.pici].pic;
            {
                p1.Cleartex();
                foreach (PicIMGD pi in alpi) {
                    p1.Addtex(pi.pic);
                }
            }

            eatSeqd(o.si);

            {
                ListView lv = lvq5;
                lv.Items.Clear();
                int x = 0;
                foreach (Q5 ob in al5) {
                    ListViewItem lvi = lv.Items.Add(x.ToString()); x++;
                    lvi.SubItems.Add(ob.ToString());
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            {
                ListView lv = lvq4;
                lv.Items.Clear();
                int x = 0;
                foreach (Q4 ob in al4) {
                    ListViewItem lvi = lv.Items.Add(x.ToString()); x++;
                    lvi.SubItems.Add(ob.ToString());
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            {
                ListView lv = lvq3;
                lv.Items.Clear();
                int x = 0;
                foreach (Q3 ob in al3) {
                    ListViewItem lvi = lv.Items.Add(x.ToString()); x++;
                    lvi.SubItems.Add(ob.ToString());
                    lvi.Checked = true;
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            {
                ListView lv = lvq2;
                lv.Items.Clear();
                int x = 0;
                foreach (Q2 ob in al2) {
                    ListViewItem lvi = new ListViewItem(x.ToString()); x++;
                    lvi.SubItems.Add(ob.ToString());
                    lvi.Checked = true;
                    lv.Items.Add(lvi);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            {
                ListView lv = lvq1;
                lv.Items.Clear();
                int x = 0;
                foreach (Q1 ob in al1) {
                    ListViewItem lvi = lv.Items.Add(x.ToString()); x++;
                    lvi.SubItems.Add(ob.ToString());
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }


            flppf.Controls.Clear();
            flppf.SuspendLayout();

            for (int x = 0; x < al1.Count; x++) {
                Q1 o1 = al1[x];
                Bitmap p = new Bitmap(Math.Max(1, o1.Width), Math.Max(1, Math.Abs(o1.Height)));
                using (Graphics cv = Graphics.FromImage(p)) {
                    cv.DrawImage(picsrc, new Point[] { Point.Empty, new Point(o1.Width, 0), new Point(0, o1.Height) }, new Rectangle(o1.Left, o1.Top, o1.Width, o1.Height), GraphicsUnit.Pixel);
                }

                alp.Add(p);

                {
                    Label la = new Label();
                    la.AutoSize = true;
                    la.Text = x.ToString();
                    flppf.Controls.Add(la);
                    PictureBox pb = new PictureBox();
                    pb.Image = p;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pb.BorderStyle = BorderStyle.Fixed3D;
                    flppf.Controls.Add(pb);
                }
            }

            flppf.ResumeLayout();
        }

        List<Q1> al1 = new List<Q1>();
        List<Q2> al2 = new List<Q2>();
        List<Q3> al3 = new List<Q3>();
        List<Q4> al4 = new List<Q4>();
        List<Q5> al5 = new List<Q5>();

        private void eatSeqd(MemoryStream si) {
            this.si = si;
            BinaryReader br = new BinaryReader(si);
            {
                si.Position = 8;
                int cntq1 = br.ReadInt32();
                int offq1 = br.ReadInt32();
                al1.Clear();
                for (int x = 0; x < cntq1; x++) {
                    si.Position = offq1 + 0x2C * x;
                    Q1 o = new Q1(br);
                    al1.Add(o);
                    Debug.WriteLine(String.Format("Q1[{0,2}] {1}", x, o.ToString()));
                }
            }
            {
                si.Position = 0x10;
                int cntq2 = br.ReadInt32();
                int offq2 = br.ReadInt32();
                al2.Clear();
                for (int x = 0; x < cntq2; x++) {
                    si.Position = offq2 + 20 * x;
                    Q2 o = new Q2(br);
                    al2.Add(o);
                    Debug.WriteLine(String.Format("Q2[{0,2}] {1}", x, o.ToString()));
                }
            }
            {
                si.Position = 0x18;
                int cntq3 = br.ReadInt32();
                int offq3 = br.ReadInt32();
                al3.Clear();
                for (int x = 0; x < cntq3; x++) {
                    si.Position = offq3 + 4 * x;
                    Q3 o = new Q3(br);
                    al3.Add(o);
                    Debug.WriteLine(String.Format("Q3[{0,2}] {1}", x, o.ToString()));
                }
            }
            {
                si.Position = 0x20;
                int cntq4 = br.ReadInt32();
                int offq4 = br.ReadInt32();
                al4.Clear();
                for (int x = 0; x < cntq4; x++) {
                    si.Position = offq4 + 0x90 * x;
                    Q4 o = new Q4(br);
                    al4.Add(o);
                    Debug.WriteLine(String.Format("Q4[{0,2}] {1}", x, o.ToString()));
                }
            }
            {
                si.Position = 0x28;
                int cntq5 = br.ReadInt32();
                int offq5 = br.ReadInt32();
                al5.Clear();
                for (int x = 0; x < cntq5; x++) {
                    si.Position = offq5 + 0x24 * x;
                    Q5 o = new Q5(br);
                    al5.Add(o);
                    Debug.WriteLine(String.Format("Q5[{0,2}] {1}", x, o.ToString()));
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { }

        float fvX = 0, fvY = 0;

        private void recalc() {
            List<ThreeD.Trimesh> al = new List<ThreeD.Trimesh>();

            double tcx = alpi[pici].pic.Width;
            double tcy = alpi[pici].pic.Height;
            int time = (int)numt.Value;
            for (int e = 0; e < al5.Count; e++) {
                Q5 o5 = al5[e];
                if (!lvq5.Items[e].Checked) continue;

                int localt;
                if (o5.Tick1 == 0) {
                    localt = 0;
                }
                else if (o5.Tick2 == 0) {
                    localt = Math.Min(time, o5.Tick1);
                }
                else {
                    localt = (time < o5.Tick1)
                        ? time
                        : o5.Tick1 + ((time - o5.Tick1) % (o5.Tick2 - o5.Tick1))
                    ;
                }
                localt = time;

                int d0 = o5.I4;
                int d1 = d0 + o5.C4;

                for (int d = d0; d < d1; d++) {
                    Q4 o4 = al4[d];
                    int layt = localt - o4.Startt; // layer time
                    float fact = (layt) / (float)Math.Max(1, o4.Endt - o4.Startt);
                    if (fact < 0 || 1 < fact) continue;
                    if (!lvq4.Items[d].Checked) continue;

                    Matrix Ma = Matrix.Identity;

                    Ma *= Matrix.Translation(
                        fvY * (o4.TiY0 + (o4.TiY1 - o4.TiY0) * fact),
                        fvX * (o4.TiX0 + (o4.TiX1 - o4.TiX0) * fact),
                        0
                        );
                    Ma *= Matrix.Scaling(
                        o4.ScaleX0 + (o4.ScaleX1 - o4.ScaleX0) * fact,
                        o4.ScaleY0 + (o4.ScaleY1 - o4.ScaleY0) * fact,
                        0
                        );
                    Ma *= Matrix.RotationZ(
                        (o4.StartAngle + (o4.EndAngle - o4.StartAngle) * fact)
                        );
#if false
                    Ma *= Matrix.RotationY(
                        (o4.TiX0 + (o4.TiX1 - o4.TiX0) * fact) / 1.0f * 360.0f / 180.0f * 3.14159f
                        );
                    Ma *= Matrix.RotationX(
                        (o4.TiY0 + (o4.TiY1 - o4.TiY0) * fact)
                        );
#endif
                    Ma *= Matrix.Translation(
                        o4.X0 + (o4.X1 - o4.X0) * fact + CUt.Lerp4(0, o4.TX0, o4.TX1, 0, fact),
                        o4.Y0 + (o4.Y1 - o4.Y0) * fact + CUt.Lerp4(0, o4.TY0, o4.TY1, 0, fact),
                        0
                        );

                    int clr = CUt.Lerp(o4.Col2, o4.Col3, fact);

                    ThreeD.Trimesh M = new ThreeD.Trimesh();
                    M.texi = pici;
                    M.constantColor = KH2c4cvt.ToWin(clr);

                    int c = o4.I3;
                    if (lvq3.Items[c].Checked) {
                        Q3 o3 = al3[c];

                        for (int b = o3.I2; b < o3.I2 + o3.C2; b++) {
                            if (!lvq2.Items[b].Checked) continue;
                            Q2 o2 = al2[b];
                            Q1 o1 = al1[o2.I1];
                            Vector3[] alpos = new Vector3[] {
                                new Vector3(o2.Left, o2.Top, 0),
                                new Vector3(o2.Right, o2.Top, 0),
                                new Vector3(o2.Left, o2.Bottom, 0),
                                new Vector3(o2.Right, o2.Bottom, 0),
                            };
                            Vector3.TransformCoordinate(alpos, ref Ma, alpos);
                            // 0-1
                            // 2-3
                            CustomVertex.PositionColoredTextured v0 = new CustomVertex.PositionColoredTextured(alpos[0], o1.Color0, (float)(o1.Left / tcx), (float)(o1.Top / tcy));
                            CustomVertex.PositionColoredTextured v1 = new CustomVertex.PositionColoredTextured(alpos[1], o1.Color1, (float)(o1.Right / tcx), (float)(o1.Top / tcy));
                            CustomVertex.PositionColoredTextured v2 = new CustomVertex.PositionColoredTextured(alpos[2], o1.Color2, (float)(o1.Left / tcx), (float)(o1.Bottom / tcy));
                            CustomVertex.PositionColoredTextured v3 = new CustomVertex.PositionColoredTextured(alpos[3], o1.Color3, (float)(o1.Right / tcx), (float)(o1.Bottom / tcy));
                            int vi = M.alv.Count;
                            M.alv.Add(v0);
                            M.alv.Add(v1);
                            M.alv.Add(v2);
                            M.alv.Add(v3);
                            M.ali.Add(vi + 0); M.ali.Add(vi + 1); M.ali.Add(vi + 2);
                            M.ali.Add(vi + 3); M.ali.Add(vi + 2); M.ali.Add(vi + 1);
                            M.cntTris += 2;
                        }
                    }
                    al.Add(M);
                }
            }

            p1.Trimeshes = al.ToArray();
        }

        class CUt {
            public static int Lerp(int c0, int c1, float f) {
                byte x0 = (byte)(c0 >> 24);
                byte y0 = (byte)(c0 >> 16);
                byte z0 = (byte)(c0 >> 8);
                byte w0 = (byte)(c0 >> 0);

                byte x1 = (byte)(c1 >> 24);
                byte y1 = (byte)(c1 >> 16);
                byte z1 = (byte)(c1 >> 8);
                byte w1 = (byte)(c1 >> 0);
                return 0
                    | (((byte)(x0 + (x1 - x0) * f)) << 24)
                    | (((byte)(y0 + (y1 - y0) * f)) << 16)
                    | (((byte)(z0 + (z1 - z0) * f)) << 8)
                    | (((byte)(w0 + (w1 - w0) * f)) << 0)
                    ;
            }

            public static float Lerp4(float v0, float v1, float v2, float v3, float fact) {
                // 0 1 2 3
                if (fact <= 0.33f) {
                    return v0 + (v1 - v0) * fact * 3;
                }
                else if (fact <= 0.66f) {
                    return v1 + (v2 - v1) * (fact - 0.33f) * 3;
                }
                else {
                    return v2 + (v3 - v2) * (fact - 0.66f) * 3;
                }
            }
        }

        private void p1_Paint(object sender, PaintEventArgs e) {
#if false
            Graphics cv = e.Graphics;
            cv.ResetTransform();
            cv.TranslateTransform(p1.Width / 2, p1.Height / 2);
            cv.ScaleTransform(3, 3);

            int curt = (int)numt.Value;

            for (int x = 0; x < al2.Count; x++) {
                bool fSel = x == 4;
                if (!fSel) continue;
                Q2 o2 = al2[x];

                Point pt = Point.Empty;
                float ra = 0;
                float sx = 1, sy = 1, tx = 0, ty = 0;
                for (int z = 24; z < 24 + 15; z++) {
                    Q4 o4 = al4[z];
                    if (o4.I3 == 4) {
                        if (o4.Startt <= curt && curt <= o4.Endt) {
                            float f = (curt - o4.Startt) / (float)(o4.Endt - o4.Startt);
                            pt.X = (int)(o4.X0 + (o4.X1 - o4.X0) * f);
                            pt.Y = (int)(o4.Y0 + (o4.Y1 - o4.Y0) * f);
                            ra = LUt.Lerp(o4.StartAngle / 3.14159f * 180.0f, o4.EndAngle / 3.14159f * 180.0f, f);
                            sx = LUt.Lerp(o4.ScaleX0, o4.ScaleX1, f);
                            sy = LUt.Lerp(o4.ScaleY0, o4.ScaleY1, f);
                            //tx = LUt.Lerp(o4.TX0, o4.TX1, f);
                            //ty = LUt.Lerp(o4.TY0, o4.TY1, f);
                        }
                    }
                }
                cv.TranslateTransform(pt.X + tx, pt.Y + ty, System.Drawing.Drawing2D.MatrixOrder.Prepend);
                cv.RotateTransform(ra, System.Drawing.Drawing2D.MatrixOrder.Prepend);
                cv.ScaleTransform(sx, sy, System.Drawing.Drawing2D.MatrixOrder.Prepend);

                Rectangle rc = Rectangle.FromLTRB(o2.Left, o2.Top, o2.Right, o2.Bottom);
                cv.DrawRectangle(fSel ? Pens.Blue : Pens.LightGray, rc);
                cv.DrawString("Ç†", p1.Font, Brushes.Black, rc);
                cv.DrawRectangle(Pens.Red, new Rectangle(-1, -1, 3, 3));
            }
#endif
        }

        class LUt {
            public static float Lerp(float v0, float v1, float f) {
                return v0 + (v1 - v0) * f;
            }
        }

        private void numt_ValueChanged(object sender, EventArgs e) {
            QueueRecalc();
        }

        private void QueueRecalc() {
            timerRecalc.Start();
        }

        private void PForm_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? (e.AllowedEffect & DragDropEffects.Copy) : DragDropEffects.None;
        }

        private void PForm_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (alfp != null) {
                foreach (String fp in alfp) {
                    Read2ld(fp);
                    if (lbbar.Items.Count != 0)
                        lbbar.SelectedIndex = 0;
                    if (lbLayd.Items.Count != 0)
                        lbLayd.SelectedIndex = 0;
                    break;
                }
            }
        }

        private void lvq5_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvq5.Items) {
                lvi.Checked = lvi.Selected;
            }

            selectOnlyThisQ5ToolStripMenuItem_Click(sender, e);
        }

        private void lvq2_ItemChecked(object sender, ItemCheckedEventArgs e) {
            QueueRecalc();
        }

        private void timerRecalc_Tick(object sender, EventArgs e) {
            timerRecalc.Stop();

            recalc();
        }

        private void sc1_Panel1_Paint(object sender, PaintEventArgs e) { }

        private void selectOnlyThisQ3ToolStripMenuItem_Click(object sender, EventArgs e) {
            bool[] alf2 = new bool[al2.Count];

            foreach (ListViewItem lvi in lvq3.Items) {
                lvi.Checked = lvi.Selected;

                if (lvi.Checked) {
                    Q3 o3 = al3[lvi.Index];
                    int b0 = o3.I2;
                    int b1 = b0 + o3.C2;
                    for (int b = b0; b < b1; b++) alf2[b] = true;
                }
            }

            foreach (ListViewItem lvi in lvq4.Items) {
                Q4 o4 = al4[lvi.Index];

                lvi.Checked = lvq3.Items[o4.I3].Checked;
            }

            bool[] alf1 = new bool[al1.Count];

            foreach (ListViewItem lvi2 in lvq2.Items) {
                lvi2.Checked = alf2[lvi2.Index];

                if (lvi2.Checked) {
                    Q2 o2 = al2[lvi2.Index];
                    alf1[o2.I1] = true;
                }
            }

            foreach (ListViewItem lvi in lvq1.Items) {
                lvi.Checked = alf1[lvi.Index];
            }

            Ut.EnvisibleChecked(lvq4);
            Ut.EnvisibleChecked(lvq2);
            Ut.EnvisibleChecked(lvq1);

            QueueRecalc();
        }

        private void selectOnlyThisQ5ToolStripMenuItem_Click(object sender, EventArgs e) {
            bool[] alf4 = new bool[al4.Count];

            foreach (ListViewItem lvi in lvq5.Items) {
                if (lvi.Checked = lvi.Selected) {
                    Q5 o5 = al5[lvi.Index];

                    for (int b = 0; b < o5.C4; b++) alf4[o5.I4 + b] = true;
                }
            }

            bool[] alf3 = new bool[al3.Count];

            foreach (ListViewItem lvi in lvq4.Items) {
                if (lvi.Checked = alf4[lvi.Index]) {
                    Q4 o4 = al4[lvi.Index];

                    alf3[o4.I3] = true;
                }
            }

            bool[] alf2 = new bool[al2.Count];

            foreach (ListViewItem lvi in lvq3.Items) {
                if (lvi.Checked = alf3[lvi.Index]) {
                    Q3 o3 = al3[lvi.Index];
                    int b0 = o3.I2;
                    int b1 = b0 + o3.C2;
                    for (int b = b0; b < b1; b++) alf2[b] = true;
                }
            }

            bool[] alf1 = new bool[al1.Count];

            foreach (ListViewItem lvi2 in lvq2.Items) {
                lvi2.Checked = alf2[lvi2.Index];

                if (lvi2.Checked) {
                    Q2 o2 = al2[lvi2.Index];
                    alf1[o2.I1] = true;
                }
            }

            foreach (ListViewItem lvi in lvq1.Items) {
                lvi.Checked = alf1[lvi.Index];
            }

            Ut.EnvisibleChecked(lvq4);
            Ut.EnvisibleChecked(lvq3);
            Ut.EnvisibleChecked(lvq2);
            Ut.EnvisibleChecked(lvq1);

            numt.Value = 0;

            QueueRecalc();
        }

        class Ut {
            public static void EnvisibleChecked(ListView lv) {
                foreach (int i in lv.CheckedIndices) {
                    lv.TopItem = lv.Items[i];
                    break;
                }
            }
        }

        private void cbAutoAdv_CheckedChanged(object sender, EventArgs e) {
            timerAutoAdv.Enabled = cbAutoAdv.Checked;
        }

        private void timerAutoAdv_Tick(object sender, EventArgs e) {
            numt.Value = Math.Min(numt.Maximum, numt.Value + 1);
        }
    }
    namespace Seqd {
        public class Q5 { // 0x24 bytes
            int v00; // Q4 idx
            int v02; // Q4 cnt
            int v04;
            int v06; // flg?
            int v08; // tick1
            int v0a;
            int v0c; // tick2
            int v10; // v?
            int v14; // v?
            int v18; // v?
            int v1c; // v?
            int v20; // v?

            public int I4 { get { return v00; } }
            public int C4 { get { return v02; } }
            public int Tick1 { get { return v08; } }
            public int Tick2 { get { return v0c; } }

            public Q5(BinaryReader br) {
                v00 = br.ReadUInt16();
                v02 = br.ReadUInt16();
                v04 = br.ReadUInt16();
                v06 = br.ReadInt16();
                v08 = br.ReadInt16();
                v0a = br.ReadUInt16(); // A 6 bytes
                v0c = br.ReadInt32();  // V

                v10 = br.ReadInt32();
                v14 = br.ReadInt32();
                v18 = br.ReadInt32();
                v1c = br.ReadInt32();
                v20 = br.ReadInt32();
            }

            public override string ToString() {
                return String.Format("({0,3},{1,2}) {2} {3:x4} {4,3} {5} {6,3}  {7,3} {8,3} {9,3} {10,3} {11,3}"
                    , v00, v02, v04, v06, v08, v0a, v0c
                    , v10, v14, v18, v1c, v20
                    );
            }
        }

        class Q5x { // 0x24 bytes
            String s;

            public Q5x(BinaryReader br) {
                s = HBUt.B2s(br.ReadBytes(0x24));
            }

            public override string ToString() {
                return s;
            }
        }

        public class Q4 { // 0x90 bytes
            int v00; // flags
            int v04; // Q3 idx
            int v08; // start time
            int v0c; // end time
            int v10; // x0?
            int v14; // y0?
            int v18; // x1?
            int v1c; // y1?
            int v20;
            int v24;
            int v28;
            int v2c;
            int v30;
            int v34;
            int v38;
            int v3c;
            float v40; // M11? start angle
            float v44; // M21? end angle
            float v48; // M31?
            float v4c; // M41?
            float v50; // M12? sx0
            float v54; // M22? sx1
            float v58; // M32? sy0
            float v5c; // M42? sy1
            float v60; // M13?
            float v64; // M23?
            float v68; // M33?
            float v6c; // M43?
            float v70; // M14? tx0?
            float v74; // M24? tx1?
            float v78; // M34? ty0?
            float v7c; // M44? ty1?
            int v80; // vtx clr0?
            int v84; // vtx clr1?
            int v88; // vtx clr2?
            int v8c; // vtx clr3?

            public float TiX0 { get { return v60; } }
            public float TiX1 { get { return v64; } }
            public float TiY0 { get { return v68; } }
            public float TiY1 { get { return v6c; } }
            public float TX0 { get { return v70; } }
            public float TX1 { get { return v74; } }
            public float TY0 { get { return v78; } }
            public float TY1 { get { return v7c; } }
            public float ScaleX0 { get { return v50; } }
            public float ScaleX1 { get { return v54; } }
            public float ScaleY0 { get { return v58; } }
            public float ScaleY1 { get { return v5c; } }
            public float StartAngle { get { return v40; } }
            public float EndAngle { get { return v44; } }
            public int I3 { get { return v04; } }
            public int Startt { get { return v08; } }
            public int Endt { get { return v0c; } }
            public int X0 { get { return v10; } }
            public int X1 { get { return v14; } }
            public int Y0 { get { return v18; } }
            public int Y1 { get { return v1c; } }

            public int Col0 { get { return v80; } }
            public int Col1 { get { return v84; } }
            public int Col2 { get { return v88; } }
            public int Col3 { get { return v8c; } }

            public override string ToString() {
                return String.Format("{0:x8} {1,2} t({2,4},{3,4}) ({4,4},{5,4},{6,4},{7,4}) C({8:X8},{9:X8},{10:X8},{11:X8}) R({12,4},{13,4}) ?({14}, {15}) S({16}, {17}, {18}, {19}) IM({20}, {21}, {22}, {23}) TI({24}, {25}, {26}, {27})"
                    , v00, v04, v08, v0c, v10, v14, v18, v1c
                    , v80, v84, v88, v8c

                    , Ut.R2d(v40), Ut.R2d(v44), v48, v4c
                    , v50, v54, v58, v5c
                    , v60, v64, v68, v6c
                    , v70, v74, v78, v7c
                    );
            }

            class Ut {
                public static int R2d(float v) {
                    return (int)(v / 3.14159f * 180.0f);
                }

                public static String B2s(int v) {
                    String s = "";
                    for (int x = 0; x < 32; x++) {
                        s += (0 != (v & (0x80000000 >> x))) ? "*" : "-";
                    }
                    return s;
                }
            }

            // Q4[20] 
            // 0000: F3 7B 04 00 02 00 00 00 00 00 00 00 32 00 00 00 
            // 0010: 00 00 00 00 00 00 00 00 F9 FF FF FF F9 FF FF FF 
            // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0040: 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 
            // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
            // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0080: 00 00 00 00 00 00 00 00 F0 00 00 80 F0 00 00 80

            // Q4[25] 
            // 0000: F0 7F 07 00 09 00 00 00 32 00 00 00 C8 00 00 00 
            // 0010: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0040: 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 
            // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
            // 0060: 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 
            // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0080: 00 00 00 00 00 00 00 00 80 80 80 80 80 80 80 80

            // Q4[26] 
            // 0000: 01 68 00 00 05 00 00 00 00 00 00 00 1E 00 00 00 
            // 0010: 32 00 00 00 CE FF FF FF 00 00 00 00 00 00 00 00 
            // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0040: 92 0A 86 3F 00 00 00 00 99 99 19 3F 00 00 80 3F 
            // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
            // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0070: 00 00 48 C2 00 00 48 C2 00 00 A0 40 00 00 A0 40 
            // 0080: 01 00 02 00 00 00 00 00 80 80 80 00 80 80 80 80

            // Q4[35] 
            // 0000: D1 3F 04 00 04 00 00 00 47 00 00 00 54 00 00 00 
            // 0010: 32 00 00 00 32 00 00 00 00 00 00 00 00 00 00 00 
            // 0020: 00 00 00 00 00 00 00 00 02 00 00 00 02 00 00 00 
            // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0040: DB 0F 49 40 DB 0F 49 40 00 00 80 3F 00 00 80 3F 
            // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
            // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
            // 0080: 00 00 00 00 00 00 00 00 80 80 80 80 80 80 80 80

            public Q4(BinaryReader br) {
                v00 = br.ReadInt32(); v04 = br.ReadInt32(); v08 = br.ReadInt32(); v0c = br.ReadInt32();
                v10 = br.ReadInt32(); v14 = br.ReadInt32(); v18 = br.ReadInt32(); v1c = br.ReadInt32();
                v20 = br.ReadInt32(); v24 = br.ReadInt32(); v28 = br.ReadInt32(); v2c = br.ReadInt32();
                v30 = br.ReadInt32(); v34 = br.ReadInt32(); v38 = br.ReadInt32(); v3c = br.ReadInt32();

                v40 = br.ReadSingle(); v44 = br.ReadSingle(); v48 = br.ReadSingle(); v4c = br.ReadSingle();
                v50 = br.ReadSingle(); v54 = br.ReadSingle(); v58 = br.ReadSingle(); v5c = br.ReadSingle();
                v60 = br.ReadSingle(); v64 = br.ReadSingle(); v68 = br.ReadSingle(); v6c = br.ReadSingle();
                v70 = br.ReadSingle(); v74 = br.ReadSingle(); v78 = br.ReadSingle(); v7c = br.ReadSingle();

                v80 = br.ReadInt32(); v84 = br.ReadInt32(); v88 = br.ReadInt32(); v8c = br.ReadInt32();
            }
        }

        class Q4x { // 0x90 bytes
            String s;

            public Q4x(BinaryReader br) {
                s = HBUt.B2s(br.ReadBytes(0x90));
            }

            public override string ToString() {
                return s;
            }
        }

        public class Q3 { // 4 bytes
            ushort v0; // Q2 idx
            ushort v1; // ?

            public int I2 { get { return v0; } }
            public int C2 { get { return v1; } }

            public Q3(BinaryReader br) {
                v0 = br.ReadUInt16();
                v1 = br.ReadUInt16();
            }

            public override string ToString() {
                return String.Format("{0,3} {1,3}", v0, v1);
            }
        }

        public class Q2 { // 20 bytes
            int v0;
            int v1;
            int v2;
            int v3;
            int v4; // Q1 idx

            public int Left { get { return v0; } }
            public int Top { get { return v1; } }
            public int Right { get { return v2; } }
            public int Bottom { get { return v3; } }
            public int Width { get { return v2 - v0; } }
            public int Height { get { return v3 - v1; } }
            public int I1 { get { return v4; } }

            public Q2(BinaryReader br) {
                v0 = br.ReadInt32();
                v1 = br.ReadInt32();
                v2 = br.ReadInt32();
                v3 = br.ReadInt32();
                v4 = br.ReadInt32();
            }

            public override string ToString() {
                return String.Format("({0,4},{1,4},{2,4},{3,4}) {4,4}", v0, v1, v2, v3, v4);
            }
        }

        public class Q1 { // 0x2C bytes
            int v0; // 0
            int v1; // pic x0
            int v2; // pic y0
            int v3; // pic x1
            int v4; // pic y1
            float v5; // 0
            float v6; // 0
            int v7; // vclr0
            int v8; // vclr1
            int v9; // vclr2
            int va; // vclr3

            public int Height { get { return v4 - v2; } }
            public int Width { get { return v3 - v1; } }
            public int Top { get { return v2; } }
            public int Left { get { return v1; } }
            public int Right { get { return v3; } }
            public int Bottom { get { return v4; } }

            public int Color0 { get { return KH2c4cvt.ToWin(v7); } }
            public int Color1 { get { return KH2c4cvt.ToWin(v8); } }
            public int Color2 { get { return KH2c4cvt.ToWin(v9); } }
            public int Color3 { get { return KH2c4cvt.ToWin(va); } }

            public Q1(BinaryReader br) {
                v0 = br.ReadInt32();
                v1 = br.ReadInt32();
                v2 = br.ReadInt32();
                v3 = br.ReadInt32();
                v4 = br.ReadInt32();
                v5 = br.ReadSingle();
                v6 = br.ReadSingle();
                v7 = br.ReadInt32();
                v8 = br.ReadInt32();
                v9 = br.ReadInt32();
                va = br.ReadInt32();
            }

            public override string ToString() {
                return String.Format("{0,3} ({1,3},{2,3},{3,3},{4,3}) {5,3} {6,3}  {7:x8} {8:x8} {9:x8} {10:x8}", v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, va);
            }
        }

        class HBUt {
            public static String B2s(byte[] bin) {
                return B2s(bin, 0, bin.Length);
            }

            public static String B2s(byte[] bin, int x, int cx) {
                StringWriter wr = new StringWriter();
                cx += x;
                for (; x < cx; x++) {
                    wr.Write(" {0:X2}", bin[x]);
                }
                return wr.ToString().TrimStart(' ');
            }
        }
    }

    namespace Layd {
        public class L2 { // 20 bytes
            public int v00; // L1 index
            public int v02; // L1 cnt
            public int v04;
            public int v08;
            public int v0c;
            public int v10;

            public L2(BinaryReader br) {
                v00 = br.ReadInt16();
                v02 = br.ReadInt16();
                v04 = br.ReadInt32();
                v08 = br.ReadInt32();
                v0c = br.ReadInt32();
                v10 = br.ReadInt32();
            }

            public override string ToString() {
                return String.Format("{0,2} {1} {2} {3} {4} {5}"
                    , v00, v02, v04, v08, v0c, v10);
            }
        }

        public class L1 { // 24 bytes
            public int v00; // texi
            public int v04; // seqi
            public int v08;
            public int v0c;
            public int v10;
            public int v14;

            public L1(BinaryReader br) {
                v00 = br.ReadInt32();
                v04 = br.ReadInt32();
                v08 = br.ReadInt32();
                v0c = br.ReadInt32();
                v10 = br.ReadInt32();
                v14 = br.ReadInt32();
            }

            public override string ToString() {
                return String.Format("{0,2} {1} {2,2} {3,3} {4,3} {5,3}"
                    , v00, v04, v08, v0c, v10, v14);
            }
        }
    }
}
