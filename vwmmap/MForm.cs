using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace vwmmap {
    public partial class MForm : Form {
        public MForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            flpans.Controls.Clear();

            int i = 0;
            foreach (Match Mx in Regex.Matches(tbq.Text, "([0-9a-f]{1,8})", RegexOptions.IgnoreCase)) {
                uint q = Convert.ToUInt32(Mx.Groups[1].Value, 16);

                if (flpans.Controls.Count != 0) {
                    flpans.SetFlowBreak(flpans.Controls[flpans.Controls.Count - 1], true);
                }
                {
                    Label li = new Label();
                    li.Text = string.Format("{0,2}", i);
                    li.AutoSize = true;
                    flpans.Controls.Add(li);
                    i++;
                }
                {
                    Label lq = new Label();
                    lq.Text = q.ToString("x");
                    lq.AutoSize = true;
                    flpans.Controls.Add(lq);
                }

                Regex rex = new Regex(tbRex.Text, RegexOptions.IgnoreCase);
                foreach (String row in File.ReadAllLines(tbfpt.Text, Encoding.ASCII)) {
                    Match M = rex.Match(row);
                    if (!M.Success) continue;

                    uint addr0 = Convert.ToUInt32(M.Groups["a"].Value, 16);
                    String fn = M.Groups["fn"].Value;
                    String fpin = Path.Combine(tbexpadir.Text, fn);
                    uint addr1 = addr0 + Convert.ToUInt32(new FileInfo(fpin).Length);

                    if (addr0 <= q && q < addr1) {
                        {
                            LinkLabel lfn = new LinkLabel();
                            lfn.Text = fn;
                            lfn.Tag = fpin;
                            lfn.Click += new EventHandler(lfn_Click);
                            lfn.AutoSize = true;
                            flpans.Controls.Add(lfn);
                        }
                        {
                            Label loff = new Label();
                            loff.Text = (q - addr0).ToString("x");
                            loff.AutoSize = true;
                            flpans.Controls.Add(loff);
                            flpans.SetFlowBreak(loff, true);
                        }
                        break;
                    }
                }
            }
        }

        void lfn_Click(object sender, EventArgs e) {
            Process.Start(@"H:\Util\STIR\Stirling.exe", " \"" + ((LinkLabel)sender).Tag + "\"");
        }

        private void tbq_Enter(object sender, EventArgs e) {
            tbq.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e) {
            tbq.Text = Regex.Replace(Clipboard.GetText(), "[\r\n]", " ");
            button1.PerformClick();
        }
    }
}