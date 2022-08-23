using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace traceHLP3 {
    public partial class DALv : UserControl {
        public DALv() {
            InitializeComponent();
        }

        private void DALv_Load(object sender, EventArgs e) {

        }

        uint pc0 = 0;
        uint pc1 = 0;

        public uint PC0 { get { return pc0; } set { this.pc0 = value; recalc(); } }
        public uint PC1 { get { return pc1; } set { this.pc1 = value; recalc(); } }

        void recalc() {
            lv.VirtualListSize = (int)Math.Max(0, (pc1 - pc0) >> 2);
            lv.Invalidate();
        }

        ParserHaxkh2fm p = null;

        public ParserHaxkh2fm ParserOb {
            get { return p; }
            set {
                if (this.p != null) {
                    p.REGChanged -= new EventHandler(p_REGChanged);
                }

                this.p = value;

                if (this.p != null) {
                    p.REGChanged += new EventHandler(p_REGChanged);
                }
            }
        }

        uint pc = 0;

        void p_REGChanged(object sender, EventArgs e) {
            pc = p.PC;
            lv.Invalidate();
        }

        private void lv_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            int i = e.ItemIndex;
            int off = (int)(pc0 + 4 * i);
            ListViewItem lvi = e.Item = new ListViewItem(off.ToString("x7"));
            lvi.ImageIndex = (pc == off) ? 0 : -1;
            if (p != null) {
                String s = p.GetDisarm((uint)off);
                lvi.SubItems.Add(s);
            }
        }

        public void EnsureVisiblePC(uint pc) {
            int i = (int)(pc - pc0) >> 2;
            if (i < 0 || i >= lv.VirtualListSize)
                throw new ArgumentOutOfRangeException("pc");
            ListViewItem lvi = lv.Items[(int)i];
            lvi.Focused = true;
            lvi.EnsureVisible();
        }

        private void lv_KeyDown(object sender, KeyEventArgs e) { OnKeyDown(e); }
        private void lv_KeyUp(object sender, KeyEventArgs e) { OnKeyUp(e); }
        private void lv_KeyPress(object sender, KeyPressEventArgs e) { OnKeyPress(e); }
    }
}
