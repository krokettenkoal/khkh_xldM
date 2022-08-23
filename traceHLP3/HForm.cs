using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ee1Dec;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Xml;

// kh2fm
// 0034dc98 : next lower memory free?
// 0034dc9c : next upper memory free?

// 00363e80 lo free?
// 00363e88 hi free?


// # t  535
// # dmaVIF1 tadr 01951480
// # t  536
// # dmaVIF1 tadr 018d1480
// # t  537
// # dmaVIF1 tadr 01951480
// # t  538
// # dmaVIF1 tadr 018d1480

namespace traceHLP3 {
    public partial class HForm : Form {
        public HForm() {
            InitializeComponent();
        }

        static string fpeeram = @"H:\EMU\Pcsx2-0.9.6\Haxkh2fm\dump000.bin";

        uint pc0 = 0x001002b0; // min addr for program counter
        uint pc1 = 0x00325bd0; // max addr for program counter

        private void HForm_Load(object sender, EventArgs e) {
            daLv.PC0 = pc0;
            daLv.PC1 = pc1;
            parserOb.Parse(fpeeram);
            hv.SetBin(parserOb.eeram);
        }

        class QueryGotoAddrUt {
            static String[] regs = "zero:at:v0:v1:a0:a1:a2:a3:t0:t1:t2:t3:t4:t5:t6:t7:s0:s1:s2:s3:s4:s5:s6:s7:t8:t9:k0:k1:gp:sp:s8:ra".Split(':');

            public static bool Hear(ParserHaxkh2fm parser, out uint addr) {
                String s = Interaction.InputBox("address?", "", "", -1, -1);
                if (parser != null)
                    for (int x = 0; x < 32; x++) {
                        if (s == regs[x]) {
                            addr = parser.GetREGui(x);
                            return true;
                        }
                    }
                if (uint.TryParse(s, NumberStyles.HexNumber, null, out addr)) {
                    return true;
                }
                return false;
            }
        }

        private void hv_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.Handled == false && char.ToLowerInvariant(e.KeyChar) == 'g') {
                e.Handled = true;

                uint a;
                if (QueryGotoAddrUt.Hear(parserOb, out a)) {
                    hv.SetPos(Convert.ToInt32(a));
                }
            }
        }
        private void daLv_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.Handled == false) {
                if (e.KeyChar == 'g') {
                    e.Handled = true;

                    uint a;
                    if (QueryGotoAddrUt.Hear(parserOb, out a)) {
                        try {
                            daLv.EnsureVisiblePC(a);
                        }
                        catch (ArgumentOutOfRangeException) {
                            MessageBox.Show(this, String.Format("is it ? {0:x7} … {1:x7} … {2:x7} ", pc0, a, pc1), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void daLv_KeyDown(object sender, KeyEventArgs e) {
            if (e.Handled == false) {
                if (e.KeyCode == Keys.Multiply && e.Alt && !e.Control && !e.Shift) {
                    e.Handled = e.SuppressKeyPress = true;

                    daLv.EnsureVisiblePC(parserOb.PC);
                }
            }
        }

        private void buttonGotoRA_Click(object sender, EventArgs e) {
            parserOb.PC = parserOb.GetREGui(ParserHaxkh2fm.RI.ra);
        }

        private void buttonST_Click(object sender, EventArgs e) {
            UtRem rem = new UtRem();

            lvCall.Items.Clear();

            uint pc = parserOb.PC;
            uint sp = parserOb.GetREGui(ParserHaxkh2fm.RI.sp);
            while (true) {
                try {
                    uint hlp = rem.FindHLPfrmPC(pc);
                    THLP o = rem.GetTHLP(hlp);

                    if (o.raoff < 0) {
                        Debug.Fail("No ra stored!");
                        break;
                    }

                    {
                        ListViewItem lvi = lvCall.Items.Add(pc.ToString("x7") + " |");
                        lvi.Tag = new ItemCall(pc, sp);
                    }
                    {
                        ListViewItem lvi = lvCall.Items.Add(hlp.ToString("x7") + " |" + sp.ToString("x7") + " +" + o.spadd);
                        lvi.Tag = new ItemCall(hlp, sp);
                    }

                    parserOb.sieeram.Position = sp + o.raoff;
                    uint parentpc = parserOb.breeram.ReadUInt32();
                    pc = parentpc - 8u;
                    sp = (uint)(sp + o.spadd);
                }
                catch (KeyNotFoundException) {
                    break;
                }
            }

        }

        class ItemCall {
            public uint pc, sp;

            public ItemCall(uint pc, uint sp) {
                this.pc = pc;
                this.sp = sp;
            }
        }

        private void lvCall_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvCall.SelectedItems) {
                ItemCall o = (ItemCall)lvi.Tag;
                daLv.EnsureVisiblePC(parserOb.PC = o.pc);
                stackAna.SetSP(o.sp);
                break;
            }
        }
    }

    [Serializable()]
    public class HLP {
        public uint entrypc;
        public List<uint> exitpc = new List<uint>();
        public SortedDictionary<uint, object> gosubpcs = new SortedDictionary<uint, object>();
        public SortedDictionary<uint, object> dictWalked = new SortedDictionary<uint, object>();
        public int spadd = 0;
        public int cntOutOfJAddr = 0, cntUnkJAddr = 0;

        public override string ToString() {
            return String.Format("pc {0:x8} sp+{1,-3} steps {2,3} exits {3} gosubs {4} ooja {5} uja {6}", entrypc, spadd, dictWalked.Count, exitpc.Count, gosubpcs.Count
                , cntOutOfJAddr, cntUnkJAddr);
        }

        public HLP(uint entrypc, int spadd) {
            this.entrypc = entrypc;
            this.spadd = spadd;
        }
    }
}