using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace traceHLP3 {
    public partial class REGAna : UserControl {
        public REGAna() {
            InitializeComponent();
        }

        private void REGAna_Load(object sender, EventArgs e) {

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
            StringWriter wr = new StringWriter();
            String[] al = GPR32;
            for (int x = 0; x < 32; x++) {
                wr.WriteLine("{0}:{1}", al[x], (parserOb != null) ? parserOb.GetREGStr(x) : "");
            }
            wr.WriteLine("{0}:{1}", "pc", (parserOb != null) ? parserOb.PC.ToString("x8") : "");
            label1.Text = wr.ToString();
        }

        public static string[] GPR32 {
            get { return "r0:at:v0:v1:a0:a1:a2:a3:t0:t1:t2:t3:t4:t5:t6:t7:s0:s1:s2:s3:s4:s5:s6:s7:t8:t9:k0:k1:gp:sp:s8:ra".Split(':'); }
        }
    }
}
