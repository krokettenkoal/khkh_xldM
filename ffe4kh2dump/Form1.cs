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
using ffe4kh2dump.Properties;

namespace ffe4kh2dump {
    public partial class SelForm : Form {
        public SelForm() {
            InitializeComponent();
        }

        String fplist { get { return Path.Combine(Application.StartupPath, "list.txt"); } }
        String fpexe { get { return Path.Combine(Application.StartupPath, "kh2dump.exe"); } }
        String fobjent { get { return Path.Combine(Application.StartupPath, "dump_kh2\\00objentry.bin"); } }

        private void SelForm_Load(object sender, EventArgs e) {
            if (File.Exists(fplist)) {
                textBox1.Text = Regex.Replace(File.ReadAllText(fplist, Encoding.ASCII), "\r\n|\r|\n", "\r\n");
            }

            foreach (string s in Environment.GetLogicalDrives()) {
                comboBoxDrv.Items.Add(s);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!File.Exists(fpexe)) {
                MessageBox.Show(this, "Need kh2dump.exe.\r\n\r\n" + "Place ffekh2dump tool at your kh2dump.exe folder. \r\n\r\n" + "Then restart ffekh2dump tool at that folder.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (comboBoxDrv.Text.Length < 1 || !File.Exists(Path.Combine(comboBoxDrv.Text, "kh2.img"))) {
                MessageBox.Show(this, "kh2 media not found! \r\n\r\n" + "Confirm your DVD ROM drive and whether media is injected.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            {
                MemoryStream os = new MemoryStream();
                foreach (string s in textBox1.Lines) {
                    if (s.Length < 1) continue;
                    os.Write(new byte[16], 0, 16);
                    byte[] fn1 = Encoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(s));
                    os.Write(fn1, 0, fn1.Length);
                    os.Write(new byte[32 - fn1.Length], 0, 32 - fn1.Length);
                    byte[] fn2 = Encoding.ASCII.GetBytes(s);
                    os.Write(fn2, 0, fn2.Length);
                    os.Write(new byte[48 - fn2.Length], 0, 48 - fn2.Length);
                }
                File.WriteAllBytes(fobjent, os.ToArray());
            }

            ProcessStartInfo psi = new ProcessStartInfo("kh2dump.exe", Path.Combine(comboBoxDrv.Text, "kh2"));
            psi.UseShellExecute = false;
            using (Flag flag = new Flag(this)) {
                Process p = Process.Start(psi);
                p.WaitForExit();
            }
        }

        class Flag : IDisposable {
            PictureBox pb;

            public Flag(Form form) {
                pb = new PictureBox();
                pb.Parent = form;
                pb.BackgroundImage = Resources.WIP;
                pb.BackgroundImageLayout = ImageLayout.Tile;
                pb.Dock = DockStyle.Fill;
                pb.Show();
                pb.BringToFront();
                form.Update();
            }

            #region IDisposable ƒƒ“ƒo

            public void Dispose() {
                pb.Dispose();
            }

            #endregion
        }

        private void comboBoxDrv_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void SelForm_FormClosing(object sender, FormClosingEventArgs e) {
            File.WriteAllText(fplist, textBox1.Text, Encoding.ASCII);
            Settings.Default.Save();
        }
    }
}