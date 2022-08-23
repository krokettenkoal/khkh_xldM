using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using khkh_xldMii.Properties;
using System.Diagnostics;

namespace khkh_xldMii {
    public partial class BCForm : Form {
        public BCForm() {
            InitializeComponent();
        }
        public BCForm(ILoadf pf) {
            this.pif = pf;
            InitializeComponent();
        }

        ILoadf pif = null;

        private void BCForm_Load(object sender, EventArgs e) {
            if (File.Exists(Program.fBPxml)) {
                XmlDocument xmlo = new XmlDocument();
                xmlo.Load(Program.fBPxml);
                XmlNamespaceManager xns = new XmlNamespaceManager(xmlo.NameTable);
                xns.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                foreach (XmlElement eli in xmlo.SelectNodes("/a:preset/a:item", xns)) {
                    listBox1.Items.Add(new Preset(eli));
                }
            }

            string s = "You can drop a file here to load.";
            toolTip1.SetToolTip(p1, s); p1.AllowDrop = true;
            toolTip1.SetToolTip(p2, s); p2.AllowDrop = true;
            toolTip1.SetToolTip(p3, s); p3.AllowDrop = true;
            toolTip1.SetToolTip(p4, s); p4.AllowDrop = true;
            toolTip1.SetToolTip(p5, s); p5.AllowDrop = true;
            toolTip1.SetToolTip(p6, s); p6.AllowDrop = true;
        }

        class Preset {
            XmlElement eli;
            XmlNamespaceManager xns;

            public Preset(XmlElement eli) {
                xns = new XmlNamespaceManager(eli.OwnerDocument.NameTable);
                xns.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                this.eli = eli;
            }

            public override string ToString() {
                return UtXmlGettext.Select(eli, "a:display-name/text()", xns);
            }

            public int GetJointOf(int i) {
                string[] keys = new string[] { 
                    "body-joint",
                    "right-hand-joint",
                    "left-hand-joint",
                };
                string text = UtXmlGettext.Select(eli, "a:" + keys[i] + "/text()", xns);
                if (text.Length != 0) return XmlConvert.ToInt32(text);
                return -1;
            }

            public string GetNameOf(int i) {
                string[] keys = new string[] { 
                    "body-mdlx",
                    "body-mset",
                    "right-hand-mdlx",
                    "right-hand-mset",
                    "left-hand-mdlx",
                    "left-hand-mset",
                };
                return UtXmlGettext.Select(eli, "a:" + keys[i] + "/text()", xns);
            }
        }

        PictureBox[] PS { get { return new PictureBox[] { p1, p2, p3, p4, p5, p6, }; } }
        String[] missfs = ".....".Split('.');

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            PictureBox[] ps = this.PS;

            foreach (Preset o in listBox1.SelectedItems) {
                for (int x = 0; x < 6; x++) {
                    ps[x].Image = null;
                    toolTip1.SetToolTip(ps[x], null);
                    missfs[x] = null;
                }
                int cnterr = 0;
                for (int x = 0; x < 6; x++) {
                    string fn = o.GetNameOf(x);
                    if (fn.Length == 0) {
                        ps[x].Image = Resources.DFH;
                        continue;
                    }
                    string fp = UtSearchf.Find(fn);
                    if (fp == null) {
                        openFileDialog1.FileName = fn;
                        openFileDialog1.Filter = "Missing file " + fn + "|" + fn + "|*.*|*.*||";
                        if (openFileDialog1.ShowDialog(this) == DialogResult.OK) {
                            fp = openFileDialog1.FileName;
                            UtSearchf.AddDirToList(Path.GetDirectoryName(fp));
                        }
                    }
                    if (fp == null) {
                        ps[x].Image = Resources.NG;
                        toolTip1.SetToolTip(ps[x], "Missing file --- Find that file, then drag it and drop here.\n\n" + fn);
                        add2Log("Missing --- " + fn + "\n");
                        missfs[x] = fn;
                        cnterr++;
                    }
                    else {
                        pif.LoadOf(x, fp);
                        ps[x].Image = Resources.Happy;
                        toolTip1.SetToolTip(ps[x], "Happy! no error. Loaded file is ...\n\n" + fp);
                    }
                }
                pif.SetJointOf(1, o.GetJointOf(1));
                pif.SetJointOf(2, o.GetJointOf(2));
                if (cnterr == 0) add2Log("OK\n");
                pif.DoRecalc();
                break;
            }
        }

        void add2Log(string text) {
            textBoxLog.Select(textBoxLog.TextLength, 0);
            textBoxLog.SelectedText = text.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }

        private void p1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : e.Effect;
        }

        private void p1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                foreach (string fp in fs) {
                    PictureBox[] ps = PS;
                    int x = Array.IndexOf(ps, sender);
                    if (x >= 0) {
                        pif.LoadOf(x, fp);
                        pif.DoRecalc();
                        ps[x].Image = Resources.Happy;
                        toolTip1.SetToolTip(ps[x], "Happy! there is no problem. Loaded file is ...\n\n" + fp);

                        UtSearchf.AddDirToList(Path.GetDirectoryName(fp));
                    }
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ((IVwer)pif).BackToViewer();
        }

        private void p1_Click(object sender, EventArgs e) {

        }

        private void p1_DoubleClick(object sender, EventArgs e) {
            int x = Array.IndexOf(PS, sender);
            if (x >= 0) {
                {
                    string fn = missfs[x];
                    openFileDialog1.Filter = "*.*|*.*||";
                    if (fn != null) openFileDialog1.Filter = fn + "|" + fn + "|*.*|*.*||";
                    openFileDialog1.FileName = fn;
                    if (openFileDialog1.ShowDialog(this) == DialogResult.OK) {
                        string fp = openFileDialog1.FileName;
                        pif.LoadOf(x, fp);
                        PS[x].Image = Resources.Happy;
                        toolTip1.SetToolTip(PS[x], "Happy! there is no problem. Loaded file is ...\n\n" + fp);

                        UtSearchf.AddDirToList(Path.GetDirectoryName(fp));
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            using (WC wc = new WC()) {
                Process.Start("http://en.wikipedia.org/wiki/Emoticon");
            }
        }
    }

    class UtSearchf {
        public static string Find(string fn) {
            if (File.Exists(Program.fSPtext)) {
                foreach (string dir in File.ReadAllLines(Program.fSPtext, Encoding.UTF8)) {
                    try {
                        string fp = Path.Combine(dir, fn);
                        if (File.Exists(fp)) {
                            return fp;
                        }
                    }
                    catch (ArgumentException) {
                        // ignore: invalid chars in path!
                    }
                }
            }
            return null;
        }

        public static void AddDirToList(string dir) {
            List<string> al = new List<string>();
            if (File.Exists(Program.fSPtext) && 0 != (File.GetAttributes(Program.fSPtext) & FileAttributes.ReadOnly))
                return;
            if (File.Exists(Program.fSPtext))
                al.AddRange(File.ReadAllLines(Program.fSPtext, Encoding.UTF8));
            if (al.IndexOf(dir) < 0) {
                al.Add(dir);
                File.WriteAllLines(Program.fSPtext, al.ToArray(), Encoding.UTF8);
            }
        }
    }

    class UtXmlGettext {
        public static string Select(XmlElement eli, string xp, XmlNamespaceManager xns) {
            XmlNode xn = eli.SelectSingleNode(xp, xns);
            if (xn != null)
                return xn.Value;
            return "";
        }
    }
}