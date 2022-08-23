using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace khiiMapv {
    public partial class BEXForm : Form {
        RDForm parent;

        public BEXForm(RDForm exporter) {
            this.parent = exporter;

            InitializeComponent();
        }

        private void bSelOut_Click(object sender, EventArgs e) {
            fbdOut.SelectedPath = tbOutDir.Text;
            if (fbdOut.ShowDialog(this) == DialogResult.OK) {
                tbOutDir.Text = fbdOut.SelectedPath;
            }
        }

        private void bAddfp_Click(object sender, EventArgs e) {
            ofdfp.FileName = "";
            if (ofdfp.ShowDialog(this) == DialogResult.OK) {
                foreach (String fp in ofdfp.FileNames) {
                    Addfp(fp);
                }
            }
        }

        public void Addfp(string fp) {
            int i;
            if ((i = lvfp.Items.IndexOfKey(fp)) >= 0) {
                lvfp.Items[i].Selected = true;
                return;
            }
            ListViewItem lvi = new ListViewItem(Path.GetFileName(fp));
            lvi.Tag = fp;
            using (Icon ico = Icon.ExtractAssociatedIcon(fp)) {
                il.Images.Add(ico);
                lvi.ImageIndex = il.Images.Count - 1;
                lvi.Name = fp;
            }
            lvfp.Items.Add(lvi);
        }

        private void BEXForm_Load(object sender, EventArgs e) {
            tbOutDir.Text = Path.Combine(Application.StartupPath, "export");
        }

        private void bAddfp_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy | DragDropEffects.Link : DragDropEffects.None);
        }

        private void bAddfp_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (alfp != null)
                foreach (String fp in alfp) {
                    Addfp(fp);
                }
        }

        private void lvfp_KeyDown(object sender, KeyEventArgs e) {
            int cx;
            if (e.KeyCode == Keys.Delete)
                while ((cx = lvfp.SelectedItems.Count) != 0) {
                    lvfp.SelectedItems[cx - 1].Remove();
                }
        }

        private void bExp_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "Are you ready?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            foreach (ListViewItem lvi in lvfp.Items) {
                String fp = (String)lvi.Tag;

                lCur.Text = fp;
                Update();

                try {
                    parent.LoadAny(fp);
                    parent.ExpallTo(Path.Combine(tbOutDir.Text, Path.GetFileName(fp)));
                }
                catch (Exception) {

                }
            }

            lCur.Text = "...";
            lvfp.Items.Clear();

            Process.Start("explorer.exe", " \"" + tbOutDir.Text + "\"");
        }

        private void bOpenOut_Click(object sender, EventArgs e) {
            Directory.CreateDirectory(tbOutDir.Text);

            Process.Start("explorer.exe", " \"" + tbOutDir.Text + "\"");
        }
    }
}