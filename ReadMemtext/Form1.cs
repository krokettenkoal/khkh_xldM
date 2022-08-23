using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace ReadMemtext {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_DoubleClick(object sender, EventArgs e) {
            load();
        }

        private void Form1_Load(object sender, EventArgs e) {
            textBox2.Text = File.ReadAllText(@"H:\Proj\khkh_xldM\MEMO\OUTPUT013.txt", Encoding.GetEncoding(932));
            load();
            loadCall();
        }

        private void loadCall() {
            treeView1.Nodes.Clear();
            TreeNodeCollection tnc = treeView1.Nodes;
            foreach (string row in textBox3.Text.Replace("\r\n", "\n").Split('\n')) {
                Match M = Regex.Match(row, "0010235C  a0 ([0-9a-f]{8}) t7 ([0-9a-f]{8})", RegexOptions.IgnoreCase);
                if (M.Success) {
                    uint a0addr = Convert.ToUInt32(M.Groups[1].Value, 16);
                    uint t7addr = Convert.ToUInt32(M.Groups[2].Value, 16);
                    TreeNode tn = tnc.Add(row);

                    if (true) { string name = findRefOf(a0addr); if (name == null) name = "?"; tn.Nodes.Add(name); }
                    if (true) { string name = findRefOf(t7addr); if (name == null) name = "?"; tn.Nodes.Add(name); }

                    tn.ExpandAll();
                }
            }
        }

        class Item {
            public uint addr;
            public int sizeMax;
            public string id;

            public Item(string id, uint addr, int sizeMax) {
                this.id = id;
                this.addr = addr;
                this.sizeMax = sizeMax;
            }
            public bool IsIn(uint ta) {
                if (addr <= ta && ta < addr + sizeMax)
                    return true;
                return false;
            }
            public bool IsIn(uint a1, uint a2) {
                if (addr <= a1 && a1 < addr + sizeMax)
                    return true;
                if (addr <= a2 && a2 < addr + sizeMax)
                    return true;
                return false;
            }
        }
        List<Item> alref = new List<Item>();

        string findRefOf(uint ta) {
            foreach (Item o in alref) {
                if (o.IsIn(ta))
                    return o.id;
            }
            return null;
        }

        private void load() {
            listView1.Items.Clear();
            alref.Clear();
            string a0id = "";
            foreach (string row in textBox2.Text.Replace("\r\n", "\n").Split('\n')) {
                Match M;
                M = Regex.Match(row, "^S_EXPA\\: a0\\(([0-9a-f]{8})\\), a1\\([ ]+([0-9]+)\\), a2\\([ ]+([0-9]+)\\)", RegexOptions.IgnoreCase);
                if (M.Success) {
                    uint addr = Convert.ToUInt32(M.Groups[1].Value, 16);
                    int sizeMax = Convert.ToInt32(M.Groups[2].Value, 10);

                    Item o = new Item(a0id, addr, sizeMax);
                    alref.Add(o);

                    foreach (ListViewItem lvia in listView1.Items) {
                        Item oo = (Item)lvia.Tag;
                        if (oo.IsIn(addr, (uint)(addr + sizeMax)))
                            lvia.ForeColor = Color.Red;
                    }

                    ListViewItem lvi = listView1.Items.Add(addr.ToString("X8"));
                    lvi.SubItems.Add((addr + sizeMax).ToString("X8"));
                    lvi.SubItems.Add(sizeMax.ToString("#,##0"));
                    lvi.SubItems.Add(a0id); a0id = null;
                    lvi.Tag = o;
                }
                M = Regex.Match(row, "^S_IEXPA\\: v0\\([0-9a-f]{8}\\), a1\\([0-9a-f]{8}\\), ra\\([0-9a-f]{8}\\), \\*a0\\(\"(.+?)\"\\)", RegexOptions.IgnoreCase);
                if (M.Success) {
                    a0id = M.Groups[1].Value;
                }
            }
        }

        class Cmp : System.Collections.IComparer {
            int col = 0;

            public Cmp(int c) { this.col = c; }

            #region IComparer ÉÅÉìÉo

            public int Compare(object xx, object yy) {
                ListViewItem xxx = (ListViewItem)xx; string x = xxx.SubItems[col].Text;
                ListViewItem yyy = (ListViewItem)yy; string y = yyy.SubItems[col].Text;
                return x.CompareTo(y);
            }

            #endregion
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
            listView1.ListViewItemSorter = new Cmp(e.Column);
        }
    }
}