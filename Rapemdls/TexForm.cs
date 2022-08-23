using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Rapemdls {
    public partial class TexForm : Form {
        public TexForm() {
            InitializeComponent();
        }
        public TexForm(Texexp texexp) {
            this.texexp = texexp;
            InitializeComponent();
        }

        Texexp texexp = null;

        private void TexForm_Load(object sender, EventArgs e) {
            selective(0);
        }

        int cursel = 0;

        void selective(int x) {
            if (texexp != null) {
                cursel = x = Math.Max(0, x);
                Bitmap pic = texexp.Explode(x);
                this.BackgroundImage = pic;
                label1.Text = x.ToString();
            }
        }

        private void TexForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                case Keys.Left:
                    selective(cursel - 1); break;
                case Keys.Down:
                case Keys.Right:
                    selective(cursel + 1); break;
            }
        }

        private void TexForm_DoubleClick(object sender, EventArgs e) {
            BackgroundImage.Save("temp.bmp", ImageFormat.Bmp);
            System.Diagnostics.Process.Start("explorer.exe", " /select,temp.bmp");
        }
    }
}