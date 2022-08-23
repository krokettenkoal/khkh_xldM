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

namespace traceHLP {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (File.Exists(openFileDialogTextin.FileName) == false) {
                openFileDialogTextin.ShowDialog();
            }
            if (File.Exists(openFileDialogTextin.FileName) != false) {
                loadTextin(openFileDialogTextin.FileName);
            }
        }

        private void loadTextin(string fp) {
            treeView1.Nodes.Clear();
            Regex r = new Regex("^@([0-9a-f]{8}) ([0-9 ]{10}) ([0-9a-f]{8}) sp([\\+\\-][0-9]+)", RegexOptions.IgnoreCase);
            Regex r2 = new Regex("^@([0-9a-f]{8}) VU1 ([0-9a-f]{8})", RegexOptions.IgnoreCase);
            Regex r3 = new Regex("^@([0-9a-f]{8}) wr ([0-9a-f]{8})", RegexOptions.IgnoreCase);
            Regex r4 = new Regex("^@([0-9a-f]{8}) !", RegexOptions.IgnoreCase);
            Stack<TreeNode> qtn = new Stack<TreeNode>();
            qtn.Push(treeView1.Nodes.Add("ROOT"));
            string[] rows = File.ReadAllLines(fp, Encoding.ASCII);
            foreach (string row in rows) {
                // @001029D8 1196430567 01FFFD30 sp-16       
                Match M = r.Match(row);
                if (M.Success) {
                    uint pc = Convert.ToUInt32(M.Groups[1].Value, 16);
                    int tt = int.Parse(M.Groups[2].Value.TrimStart());
                    uint sp = Convert.ToUInt32(M.Groups[3].Value, 16);
                    int step = int.Parse(M.Groups[4].Value);
                    if (0 == (0xC0000000 & pc) && pc != 0x001E2DD4 && pc != 0x00167B44) {
                        if (step < 0) {
                            string sf = null;
                            TreeNode tn = qtn.Peek().Nodes.Add(pc.ToString("X8") + (sf = Ut.Find(pc)));
                            if (sf != null) { tn.BackColor = Color.Blue; tn.ForeColor = Color.LightGray; }
                            Item o = new Item(sp);
                            o.fa = pc;
                            tn.Tag = o;
                            qtn.Push(tn);
                        }
                        else if (step > 0) {
                            if (qtn.Count != 0) {
                                TreeNode tn = qtn.Peek();
                                Item o = (Item)tn.Tag;
                                if (o != null) o.la = pc - 4;
                                tn.Expand();
                                //Debug.Assert(o.sp == (sp + step));
                                qtn.Pop();
                            }
                        }
                    }
                }
                else {
                    M = r2.Match(row);
                    if (M.Success) {
                        uint pc = Convert.ToUInt32(M.Groups[1].Value, 16);
                        uint addr = Convert.ToUInt32(M.Groups[2].Value, 16);
                        TreeNode tn = qtn.Peek().Nodes.Add("VU1 " + pc.ToString("X8"));
                        tn.BackColor = Color.LightGreen;
                    }
                    else {
                        M = r3.Match(row);
                        if (M.Success) {
                            uint pc = Convert.ToUInt32(M.Groups[1].Value, 16);
                            uint addr = Convert.ToUInt32(M.Groups[2].Value, 16);
                            TreeNode tn = qtn.Peek().Nodes.Add("wr " + pc.ToString("X8"));
                            tn.BackColor = Color.LightGreen;
                        }
                        else {
                            M = r4.Match(row);
                            if (M.Success) {
                                uint pc = Convert.ToUInt32(M.Groups[1].Value, 16);
                                TreeNode tn = qtn.Peek().Nodes.Add("!");
                                tn.BackColor = Color.Pink;
                            }
                        }
                    }
                }
            }
        }

        class Ut {
            public static string Find(uint pc) {
                string s = Finder(pc);
                if (s != null) return " -- " + s;
                return s;
            }
            public static string Finder(uint pc) {
                switch (pc) {
                    case 0x00129a18: return "S_CALCB";
                    case 0x00128918: return "S_CALCF";
                    case 0x00128260: return "S_CALCT";
                    case 0x0012d508: return "S_CALCY";
                }
                return null;
            }
        }

        class Item {
            public uint sp, fa = 0, la = 0;

            public Item(uint sp) {
                this.sp = sp;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            TreeNode tn = e.Node;
            string s = "";
            while (tn != null) {
                s = tn.Text + ' ' + s;
                tn = tn.Parent;
            }
            textBoxPath.Text = s.TrimEnd(' ');

            Item o = (Item)e.Node.Tag;
            if (o != null) {
                textBoxFE.Text = o.fa.ToString("X8") + "\r\n" + o.la.ToString("X8");
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs != null) {
                loadTextin(fs[0]);
            }
        }
    }
}