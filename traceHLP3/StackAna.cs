using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace traceHLP3 {
    public partial class StackAna : UserControl {
        public StackAna() {
            InitializeComponent();
        }

        ParserHaxkh2fm parserOb = null;

        public ParserHaxkh2fm ParserOb {
            get { return parserOb; }
            set {
                if (parserOb != null) {
                    parserOb.REGChanged -= new EventHandler(parserOb_Parsed);
                }

                parserOb = value;

                if (parserOb != null) {
                    parserOb.REGChanged += new EventHandler(parserOb_Parsed);
                }
            }
        }

        void parserOb_Parsed(object sender, EventArgs e) {
            recalc();
        }

        void recalc() {
            sp = parserOb.GetREGui(ParserHaxkh2fm.RI.sp);
            Invalidate();
        }

        uint sp = 0;

        private void lv_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            uint off = sp + (uint)(4 * e.ItemIndex);
            ListViewItem lvi = e.Item = new ListViewItem(off.ToString("x7"));
            if (parserOb != null) {
                parserOb.sieeram.Position = off;
                uint val = parserOb.breeram.ReadUInt32();
                lvi.SubItems.Add(val.ToString("x8"));
            }
        }

        public void SetSP(uint sp) {
            this.sp = sp;
            lv.TopItem = lv.Items[0];
            lv.Invalidate();
        }
    }
}
