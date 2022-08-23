using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using khkh_xldMii;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using vwBinTex2;

namespace vwApdxFace1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            addApdx(@"H:\EMU\pcsx2-0.9.4\expa\obj_P_EX100.a.fm");
        }

        private void addApdx(string fapdx) {
            listBox1.Items.Add(fapdx);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            string fapdx = listBox1.SelectedItem as string;
            using (FileStream fs = File.OpenRead(fapdx)) {
                ReadBar.Barent[] ents = ReadBar.Explode(fs);
                foreach (ReadBar.Barent ent in ents) {
                    if (ent.k == 0x18) {
                        MemoryStream si = new MemoryStream(ent.bin, false);
                        si.Position = 0x40;
                        BinaryReader br = new BinaryReader(si);
                        byte[] picbin = br.ReadBytes(0x8000);
                        byte[] palbin = br.ReadBytes(1024);
                        Bitmap pic = new Bitmap(256, 256, PixelFormat.Format8bppIndexed);
                        BitmapData bd = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        try {
                            Marshal.Copy(picbin, 0, bd.Scan0, 0x8000);
                        }
                        finally {
                            pic.UnlockBits(bd);
                        }
                        ColorPalette pal = pic.Palette;
                        for (int t = 0; t < 256; t++) {
                            int s = KHcv8pal.repl(t);
                            pal.Entries[t] = Color.FromArgb(
                                palbin[4 * s + 3],
                                palbin[4 * s + 0],
                                palbin[4 * s + 1],
                                palbin[4 * s + 2]
                                );
                        }
                        pic.Palette = pal;
                        pictureBox1.Image = pic;
                        break;
                    }
                }
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                foreach (string fp in fs) {
                    if (Path.GetExtension(fp).Equals(".apdx")) {
                        listBox1.Items.Add(fp);
                    }
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
    }
}