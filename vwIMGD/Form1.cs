using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace vwIMGD {
    public partial class VwForm : Form {
        public VwForm() {
            InitializeComponent();
        }

        private void VwForm_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        private void VwForm_DragDrop(object sender, DragEventArgs e) {
            String[] fs = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (fs != null) {
                foreach (string fp in fs) alExp.Enqueue(fp);
            }
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e) {
            while (alExp.Count != 0) {
                String fp = alExp.Dequeue();
                if (0 != (File.GetAttributes(fp) & FileAttributes.Directory)) {
                    alExp.Enqueue(fp);
                }
                else {
                    alfp.Enqueue(fp);
                }
            }

        }

        Queue<string> alExp = new Queue<string>();
        Queue<string> alfp = new Queue<string>();
    }
}