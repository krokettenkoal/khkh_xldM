using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace parseSEQDv2.Controls {
    public partial class VisibleOfFramesControl : UserControl {
        public VisibleOfFramesControl() {
            InitializeComponent();

            DoubleBuffered = true; // flicker less
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // flicker less
            SetStyle(ControlStyles.UserPaint, true); // flicker less
        }

        int frameCount = 1;
        public int FrameCount {
            get => frameCount;
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException();
                }
                frameCount = value;
                visibleMask = new BitArray(frameCount);
                PerformLayout();
            }
        }

        BitArray visibleMask = new BitArray(1);
        public BitArray VisibleMask {
            get => (BitArray)visibleMask.Clone();
            set {
                visibleMask = (BitArray)value.Clone();
                Invalidate();
            }
        }

        BitArray renderedMask = new BitArray(1);
        public BitArray RenderedMask {
            get => (BitArray)renderedMask.Clone();
            set {
                renderedMask = (BitArray)value.Clone();
                Invalidate();
            }
        }

        int xCount = 1;
        int yCount = 1;

        public override Size GetPreferredSize(Size proposedSize) {
            if (frameCount >= 1) {
                xCount = Math.Max(1, proposedSize.Width / BlkWidth);
                yCount = Math.Max(1, frameCount + xCount - 1) / xCount;
                return new Size(BlkWidth * xCount, BlkHeight * yCount);
            }
            return Size.Empty;
        }

        private readonly StringFormat sfMC = new StringFormat {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };

        const int BlkWidth = 24;
        const int BlkHeight = 16;

        private void VisibleOfFramesControl_Paint(object sender, PaintEventArgs e) {
            var canvas = e.Graphics;
            var brush = new SolidBrush(ForeColor);
            for (int a = 0; a < frameCount; a++) {
                int x = a % xCount;
                int y = a / xCount;
                var rc = new Rectangle(BlkWidth * x, BlkHeight * y, BlkWidth, BlkHeight);
                rc.Inflate(-2, -2);
                if (visibleMask[a]) {
                    var rcFill = rc;
                    rcFill.Width += 1;
                    rcFill.Height += 1;
                    if (renderedMask[a]) {
                        rcFill.Height -= 2;
                    }
                    canvas.FillRectangle(
                        Brushes.LimeGreen,
                        rcFill
                    );
                }
                if (renderedMask[a]) {
                    canvas.DrawLine(
                        Pens.Yellow,
                        rc.Left, rc.Bottom, rc.Right, rc.Bottom
                    );
                }
                rc.Inflate(-1, -1);
                canvas.DrawString(
                    a.ToString(),
                    Font,
                    brush,
                    rc,
                    sfMC
                );
            }
        }

        private void VisibleOfFramesControl_MouseDown(object sender, MouseEventArgs e) {
            int x = e.X / BlkWidth;
            int y = e.Y / BlkHeight;
            int cell = x + xCount * y;
            if (cell >= 0 && cell < visibleMask.Length) {
                switch (e.Button) {
                    case MouseButtons.Left:
                        visibleMask[cell] = true;
                        break;
                    case MouseButtons.Right:
                        visibleMask[cell] = false;
                        break;
                    case MouseButtons.Middle:
                        if (0 != (ModifierKeys & Keys.Shift)) {
                            visibleMask.SetAll(true);
                            visibleMask[cell] = false;
                        }
                        else {
                            visibleMask.SetAll(false);
                            visibleMask[cell] = true;
                        }
                        break;
                    default:
                        return;
                }
                Invalidate();
                VisibleMaskChanged?.Invoke(this, e);
            }
        }

        public event EventHandler VisibleMaskChanged;

        private void VisibleOfFramesControl_Load(object sender, EventArgs e) {

        }

        private void VisibleOfFramesControl_MouseMove(object sender, MouseEventArgs e) {
            VisibleOfFramesControl_MouseDown(sender, e);
        }
    }
}
