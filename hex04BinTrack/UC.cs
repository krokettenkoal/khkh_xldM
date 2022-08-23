using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace hex04BinTrack {
    public partial class UC : UserControl {
        public UC() {
            InitializeComponent();
        }

        bool useTransparent = false;

        public bool UseTransparent {
            get { return useTransparent; }
            set { useTransparent = value; }
        }

        private void UC_Load(object sender, EventArgs e) {

        }

        const int WM_ERASEBKGND = 0x0014;

        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_ERASEBKGND && !DesignMode && useTransparent) {
                m.Result = new IntPtr(1);
                return;
            }
            base.WndProc(ref m);
        }
    }
}
