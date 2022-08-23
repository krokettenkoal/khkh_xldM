using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vu1Sim {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            Rectangle rc = Screen.GetWorkingArea(this);
            Location = new Point(rc.Right - Width, rc.Top);

        }

        public string[] viHelp = "...............".Split('.');
        public string IHelp = "";
        public string QHelp = "";
        public string RHelp = "";
        public string PHelp = "";

        public string[] vfHelp = "...............................................................................................................................".Split('.');
        public string[] accHelp = "...".Split('.');

        public string summary_viHelp(int vi) {
            return viHelp[vi];
        }
        public string summary_vfHelp(int vf) {
            string[] s4 = new string[]{
                vfHelp[4 * vf + 0],
                vfHelp[4 * vf + 1],
                vfHelp[4 * vf + 2],
                vfHelp[4 * vf + 3],
            };
            return string.Join(",", s4);
        }

        public string summary_accHelp() {
            return string.Join(",", accHelp);
        }

        public void SetMean(HTRSel rs, string text) {
            if (HTRSel.vi0 <= rs && rs <= HTRSel.vi15) {
                viHelp[(int)(rs - HTRSel.vi0)] = text;
            }
            else if (rs == HTRSel.I) { IHelp = text; }
            else if (rs == HTRSel.Q) { QHelp = text; }
            else if (rs == HTRSel.R) { RHelp = text; }
            else if (rs == HTRSel.P) { PHelp = text; }
        }
    }
}