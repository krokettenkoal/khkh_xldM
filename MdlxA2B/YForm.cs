using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace MdlxA2B {
    public partial class YForm : Form {
        public YForm() {
            InitializeComponent();
        }

        String BaseDir { get { return Environment.CurrentDirectory; } }
        String fpxml { get { return Path.Combine(BaseDir, "BindPreset.xml"); } }

        XmlDocument xmlo = new XmlDocument();
        XmlNamespaceManager xnm;

        private void YForm_Load(object sender, EventArgs e) {
            xmlo.Load(fpxml);

            xnm = new XmlNamespaceManager(xmlo.NameTable);
            xnm.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");

            foreach (XmlElement el in xmlo.SelectNodes("/a:preset/a:item", xnm)) {
                XmlText xt = el.SelectSingleNode("a:display-name/text()", xnm) as XmlText;
                if (xt == null) continue;
                ToolStripMenuItem tsi = new ToolStripMenuItem(xt.Value);
                tsi.Tag = el;
                tsi.Click += new EventHandler(tsi_Click);
                bPresets.DropDownItems.Add(tsi);
            }
        }

        void tsi_Click(object sender, EventArgs e) {
            XmlElement eli = ((ToolStripMenuItem)sender).Tag as XmlElement;

            buttonMdlx.Text = Resolvefp(eli.SelectSingleNode("a:body-mdlx/text()", xnm));
            buttonMset.Text = Resolvefp(eli.SelectSingleNode("a:body-mset/text()", xnm));
            buttonLHMdlx.Text = Resolvefp(eli.SelectSingleNode("a:left-hand-mdlx/text()", xnm));
            buttonLHMset.Text = Resolvefp(eli.SelectSingleNode("a:left-hand-mset/text()", xnm));
            buttonRHMdlx.Text = Resolvefp(eli.SelectSingleNode("a:right-hand-mdlx/text()", xnm));
            buttonRHMset.Text = Resolvefp(eli.SelectSingleNode("a:right-hand-mset/text()", xnm));
            numlhj.Value = Resolvej(eli.SelectSingleNode("a:left-hand-joint/text()", xnm));
            numrhj.Value = Resolvej(eli.SelectSingleNode("a:right-hand-joint/text()", xnm));
        }

        private decimal Resolvej(XmlNode xt) {
            if (xt == null)
                return 0;
            return Convert.ToDecimal(xt.Value);
        }

        public string Resolvefp(XmlNode xt) {
            if (xt == null)
                return null;
            return Path.GetFullPath(xt.Value);
        }

        private void buttonMdlx_Click(object sender, EventArgs e) {
            Button b = (Button)sender;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = b.Text;
            ofd.Filter = b.Name.Contains("Mdlx") ? "*.mdlx|*.mdlx||" : "*.mset|*.mset||";
            if (ofd.ShowDialog(this) == DialogResult.OK) {
                b.Text = ofd.FileName;
            }
        }
    }
}