using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Prayvif2 {
    public partial class WIP : UserControl {
        public WIP() {
            InitializeComponent();
        }

        public static WIP Show(Control parent) {
            WIP o = new WIP();
            o.Parent = parent;
            o.Location = Point.Empty;
            o.Size = parent.ClientSize;
            o.TabIndex = 0;
            o.TabStop = false;
            o.BringToFront();
            o.Update();
            return o;
        }
    }
}
