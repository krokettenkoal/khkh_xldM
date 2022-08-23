using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Readmset {
    public enum PgScrollType {
        Absolute, ScreenSizeBased,
    }

    public partial class HexVwer : UserControl {
        public HexVwer() {
            InitializeComponent();
        }

        [NonSerialized]
        MemoryStream si = null;

        [NonSerialized]
        int curoff = 0;

        [NonSerialized]
        Hashtable marks = new Hashtable();

        [NonSerialized]
        int cursel = -1;

        [NonSerialized]
        Hashtable mark2 = new Hashtable();

        int unitPg = 512;
        bool antiFlick = true;
        int offDelta = 0x0;
        int byteWidth = 16;
        PgScrollType pgScrollType = PgScrollType.ScreenSizeBased;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("絶対量でページスクロールする時の移動量。PgScrollがAbsoluteである場合に採用されます。")]
        public int UnitPg {
            get { return unitPg; }
            set { unitPg = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("再描画する時のちらつきを軽減します(trueの場合)。")]
        public bool AntiFlick {
            get { return antiFlick; }
            set { antiFlick = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("左端に表示されるアドレスに加算する定数を指定します。")]
        public int OffDelta {
            get { return offDelta; }
            set { offDelta = value; Invalidate(); }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("一行に表示するバイト数を指定します。")]
        public int ByteWidth {
            get { return byteWidth; }
            set {
                if (value < 1) throw new ArgumentOutOfRangeException("value");
                byteWidth = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("ページスクロールする時の移動量の考え方を指定します。\nAbsoluteの場合，常にUnitPgで指定された量を移動します。\nScreenSizeBasedの場合，画面に表示されている量を移動します。")]
        public PgScrollType PgScroll {
            get { return pgScrollType; }
            set { pgScrollType = value; }
        }

        public IDictionary Mark2 {
            get { return mark2; }
        }


        public void SetBin(byte[] bin) {
            SetBin(bin, 0, Convert.ToInt32(bin.Length));
        }
        public void SetBin(byte[] bin, int off, int len) {
            si = new MemoryStream(bin, off, len, false);
            Invalidate();
        }

        class MeasureStrUtil {
            public static SizeF calc1(Graphics cv, Font font) {
                SizeF fs1 = cv.MeasureString("iw", font);
                SizeF fs2 = cv.MeasureString("iwiw", font);
                return new SizeF((fs2.Width - fs1.Width) / 2, fs2.Height);
            }
        }

        public int GetLineCnt() {
            if (pic.Height == 1)
                return 1;
            return ClientSize.Height / pic.Height;
        }

        Bitmap pic = new Bitmap(1, 1);

        private void HexVwer_Paint(object sender, PaintEventArgs e) {
            if (si == null) return;

            SizeF fs = MeasureStrUtil.calc1(e.Graphics, this.Font);
            int bcx = (int)(fs.Width * (10 + 3 * byteWidth + 1 + byteWidth)) + 1;
            int bcy = (int)(fs.Height);
            if (pic.Width < bcx || pic.Height < bcy) {
                pic = new Bitmap(bcx, bcy);
            }

            Brush forebr = new SolidBrush(this.ForeColor);
            Brush markbr = new SolidBrush(Color.FromArgb(30, Color.BlueViolet));
            Brush selbr = new SolidBrush(Color.FromArgb(80, Color.Green));
            using (Graphics cv = Graphics.FromImage(pic)) {
                int lc = (int)(ClientSize.Height / bcy);
                int tempoff = curoff;
                si.Position = tempoff;
                int pixy = 0;
                for (int li = 0; li < lc && tempoff < (int)si.Length; li++, tempoff += byteWidth) {
                    cv.Clear(this.BackColor);
                    string s = string.Format("{0:X8}: ", tempoff + offDelta);

                    byte[] bin = new byte[byteWidth];
                    for (int ci = 0; ci < byteWidth; ci++) {
                        if (ci + tempoff < (int)si.Length) {
                            s += (bin[ci] = (byte)si.ReadByte()).ToString("X2");
                        }
                        else {
                            s += "  ";
                        }
                        s += ' ';
                    }
                    s += ' ';
                    for (int ci = 0; ci < byteWidth; ci++) {
                        char c = (char)bin[ci];
                        if (char.IsLetterOrDigit(c) || c == ' ') {
                            s += c;
                        }
                        else {
                            s += '.';
                        }
                    }
                    for (int ci = 0; ci < byteWidth; ci++) {
                        if (cursel == tempoff + ci) {
                            cv.FillRectangle(selbr, new RectangleF(fs.Width * (10 + 3 * ci), 0, fs.Width * 2 + 1, fs.Height));
                        }
                        else if (mark2.ContainsKey(tempoff + ci)) {
                            cv.FillRectangle(new SolidBrush((Color)mark2[tempoff + ci]), new RectangleF(fs.Width * (10 + 3 * ci), 0, fs.Width * 2 + 1, fs.Height));
                        }
                        else if (marks.ContainsKey(tempoff + ci)) {
                            cv.FillRectangle(markbr, new RectangleF(fs.Width * (10 + 3 * ci), 0, fs.Width * 2 + 1, fs.Height));
                        }
                    }
                    float x1bin = fs.Width * (10);
                    foreach (RangeMarked rm in alrm) {
                        int off1 = rm.off - tempoff;
                        int off2 = off1 + rm.len;

                        int loff1 = Math.Max(0, Math.Min(byteWidth, off1));
                        int loff2 = Math.Max(0, Math.Min(byteWidth, off2));

                        if (loff1 != loff2) {
                            SolidBrush tempbr = new SolidBrush(rm.clr);

                            RectangleF rc = new RectangleF(
                                fs.Width * (10 + 3 * loff1),
                                0,
                                fs.Width * (3 * (loff2 - loff1)),
                                pic.Height
                            );
                            cv.FillRectangle(tempbr, rc);

                            Pen temppen = new Pen(rm.clrborder);

                            int upper1 = Math.Max(0, Math.Min(byteWidth, off1 - byteWidth));
                            int upper2 = Math.Max(0, Math.Min(byteWidth, off2 - byteWidth));
                            int lower1 = Math.Max(0, Math.Min(byteWidth, off1 + byteWidth));
                            int lower2 = Math.Max(0, Math.Min(byteWidth, off2 + byteWidth));

                            if (true) { // up-side left line
                                int ml = Math.Min(lower1, loff1);
                                int mr = Math.Max(lower1, loff1);
                                if (ml != mr) {
                                    cv.DrawLine(temppen, x1bin + fs.Width * (3 * ml), 0, x1bin + fs.Width * (3 * mr) - 1, 0);
                                }
                            }
                            if (true) { // up-side right line
                                int ml = Math.Min(lower2, loff2);
                                int mr = Math.Max(lower2, loff2);
                                if (ml != mr) {
                                    cv.DrawLine(temppen, x1bin + fs.Width * (3 * ml), 0, x1bin + fs.Width * (3 * mr) - 1, 0);
                                }
                            }
                            if (true) { // down-side left line
                                int ml = Math.Min(upper1, loff1);
                                int mr = Math.Max(upper1, loff1);
                                if (ml != mr) {
                                    cv.DrawLine(temppen, x1bin + fs.Width * (3 * ml), pic.Height - 1, x1bin + fs.Width * (3 * mr) - 1, pic.Height - 1);
                                }
                            }
                            if (true) { // down-side right line
                                int ml = Math.Min(upper2, loff2);
                                int mr = Math.Max(upper2, loff2);
                                if (ml != mr) {
                                    cv.DrawLine(temppen, x1bin + fs.Width * (3 * ml), pic.Height - 1, x1bin + fs.Width * (3 * mr) - 1, pic.Height - 1);
                                }
                            }

                            cv.DrawLine(temppen, rc.X, 0, rc.X, pic.Height);
                            cv.DrawLine(temppen, rc.Right - 1, 0, rc.Right - 1, pic.Height);
                        }
                    }

                    cv.DrawString(s, this.Font, forebr, 0, 0);
                    e.Graphics.DrawImageUnscaled(pic, 0, pixy);
                    pixy += pic.Height;
                }
                if (antiFlick) {
                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), Rectangle.FromLTRB(0, pixy, pic.Width, ClientSize.Height));
                }
            }
        }

        public void SetPos(int pos) {
            curoff = Math.Max(0, pos);
            Invalidate();
        }

        public int GetPos() {
            return curoff;
        }

        public int CalcPgSize() {
            switch (pgScrollType) {
                case PgScrollType.Absolute:
                    return unitPg;
                case PgScrollType.ScreenSizeBased:
                    return byteWidth * GetLineCnt();
            }
            throw new NotSupportedException();
        }

        private void HexVwer_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                    SetPos(curoff - byteWidth);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    SetPos(curoff + byteWidth);
                    e.Handled = true;
                    break;
                case Keys.Left:
                    SetPos(curoff + 1);
                    e.Handled = true;
                    break;
                case Keys.Right:
                    SetPos(curoff - 1);
                    e.Handled = true;
                    break;
                case Keys.Home:
                    SetPos(0);
                    e.Handled = true;
                    break;
                case Keys.PageUp:
                    SetPos(curoff - CalcPgSize());
                    e.Handled = true;
                    break;
                case Keys.PageDown:
                    SetPos(curoff + CalcPgSize());
                    e.Handled = true;
                    break;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData) {
            switch (keyData) {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.PageUp:
                case Keys.PageDown:
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    HexVwer_KeyDown(this, e);
                    return e.Handled;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void HexVwer_Load(object sender, EventArgs e) {
        }

        public void AddMark(int addr) {
            marks[addr] = null;
            Invalidate();
        }

        public void SetSel(int sel) {
            cursel = sel;
            Invalidate();
        }

        const int WM_ERASEBKGND = 0x14;

        protected override void WndProc(ref Message m) {
            if (!DesignMode && si != null && antiFlick && m.Msg == WM_ERASEBKGND) {
                using (Graphics cv = Graphics.FromHdc(m.WParam)) {
                    System.Drawing.Drawing2D.GraphicsState gs = cv.Save();
                    cv.ExcludeClip(Rectangle.FromLTRB(0, 0, pic.Width, ClientSize.Height));
                    cv.FillRectangle(new SolidBrush(this.BackColor), ClientRectangle);
                    cv.Restore(gs);
                    m.Result = new IntPtr(1);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        [NonSerialized]
        List<RangeMarked> alrm = new List<RangeMarked>();

        public void AddRangeMarked(int off, int len, Color color, Color clrborder) {
            alrm.Add(new RangeMarked(off, len, color, clrborder));
        }

        public List<RangeMarked> RangeMarkedList {
            get { return alrm; }
        }
    }

    public class RangeMarked {
        public int off, len;
        public Color clr, clrborder;

        public RangeMarked(int off, int len, Color clr, Color clrborder) {
            this.off = off;
            this.len = len;
            this.clr = clr;
            this.clrborder = clrborder;
        }
    }
}
