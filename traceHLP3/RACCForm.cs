using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using traceHLP3.Properties;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace traceHLP3 {
    public partial class RACCForm : Form {
        public RACCForm() {
            InitializeComponent();
        }

        //String fpracc = @"H:\Proj\khkh_xldM\MEMO\pax_\racc.txt";
        //String fpracc = @"H:\Proj\khkh_xldM\MEMO\pax_\racc-P41.txt";

        String fpeeram = @"H:\Proj\khkh_xldM\MEMO\pax_\dump000.bin";

        //String fphexbin = @"H:\EMU\Pcsx2-0.9.6\expa\obj\WORLD_POINT.a.fm";

        //String fpracc = @"H:\Proj\khkh_xldM\MEMO\pax_\racc-03.txt";
        //String fphexbin = @"H:\EMU\Pcsx2-0.9.6\expa\obj\N_MU070_RTN.mdlx";

        String fpracc = @"H:\Proj\khkh_xldM\MEMO\msetsel\racc.txt";
        String fphexbin = @"H:\EMU\Pcsx2-0.9.6\expa\obj\N_MU070_RTN.mset";

        List<Racc> alracc = new List<Racc>();

        uint pc0 = 0x001002b0; // min addr for program counter
        uint pc1 = 0x00325bd0; // max addr for program counter

        SortedDictionary<uint, int> dictCntPC = new SortedDictionary<uint, int>();

        private void H2Form_Load(object sender, EventArgs e) {
            //new BuildHLPTree().Build();

            parserOb.Parse(fpeeram);

            hv.SetBin(File.ReadAllBytes(fphexbin));

            daLv1.PC0 = pc0;
            daLv1.PC1 = pc1;

            SortedDictionary<uint, object> dictPC = new SortedDictionary<uint, object>();
            {
                foreach (String row in File.ReadAllLines(fpracc, Encoding.GetEncoding(932))) {
                    Match M = Regex.Match(row, "^pc\\s+(?<pc>[0-9a-f]{7})\\s+off\\s+(?<off>[0-9a-f]{8})\\s+cnt\\s+(?<cnt>\\d+)");
                    if (!M.Success) continue;
                    uint pc = Convert.ToUInt32(M.Groups["pc"].Value, 16);
                    uint off = Convert.ToUInt32(M.Groups["off"].Value, 16);
                    int cnt = Convert.ToInt32(M.Groups["cnt"].Value, 10);
                    alracc.Add(new Racc(pc, off, cnt));

                    dictPC[pc] = null;

                    if (dictCntPC.ContainsKey(pc)) dictCntPC[pc] = dictCntPC[pc] + 1;
                    else dictCntPC[pc] = 1;

                    int cb = R5900RSUt.GetSize(parserOb, pc);
                    Debug.Assert(cb != 0);
                    if (cb != 0) {
                        if (0 != (cb & 256)) {
                            hv.RangeMarkedList.Add(new Readmset.RangeMarked((int)off, (cb & 255), Color.FromArgb(60, Color.LightGreen), Color.Green));
                        }
                        else {
                            hv.RangeMarkedList.Add(new Readmset.RangeMarked((int)off, (cb & 255), Color.FromArgb(30, Color.Orange), Color.Red));
                        }
                    }
                }
            }

            foreach (uint pc in dictPC.Keys) {
                ListViewItem lvi = lvpc.Items.Add(pc.ToString("x7"));
                lvi.SubItems.Add(dictCntPC[pc].ToString());
                lvi.Tag = pc;
            }
        }

        SortedDictionary<String, object> dictCallHLP = new SortedDictionary<string, object>();

        class Racc {
            public uint pc;
            public uint off;
            public int cnt;

            public Racc(uint pc, uint off, int cnt) {
                this.pc = pc;
                this.off = off;
                this.cnt = cnt;
            }
        }

        private void lvpc_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvpc.SelectedItems) {
                uint pc = (uint)lvi.Tag;
                parserOb.PC = pc;
                daLv1.EnsureVisiblePC(pc);
                hv.Mark2.Clear();

                lboff.Items.Clear();
                foreach (Racc racc in alracc) {
                    if (pc == racc.pc) {
                        lboff.Items.Add(new Off(racc.off));
                        hv.Mark2[(int)racc.off] = Color.Fuchsia;
                    }
                }
                hv.Invalidate();
                break;
            }
        }

        struct Off {
            public uint off;

            public Off(uint off) {
                this.off = off;
            }

            public override string ToString() {
                return off.ToString("x8");
            }
        }

        private void lboff_SelectedIndexChanged(object sender, EventArgs e) {
            int a0 = hv.GetPos();
            int a1 = hv.GetPos() + 16 * hv.GetLineCnt();

            foreach (Off o in lboff.SelectedItems) {
                if (a0 <= o.off && o.off <= a1)
                    break;
                hv.SetPos((int)o.off & ~0xFF);
                break;
            }
        }
    }

    class R5900RSUt {
        public static int GetSize(ParserHaxkh2fm parserOb, uint pc) {
            parserOb.sieeram.Position = pc;
            uint word = parserOb.breeram.ReadUInt32();
            switch (word & 0xFC000000u) {
                case 0x80000000u: // LB
                case 0x90000000u: // LBU
                    return 1;
                case 0xD8000000u: // LD
                    return 8;
                case 0x68000000u: // LDL
                case 0x6C000000u: // LDR
                case 0x8C000000u: // LW
                case 0x9C000000u: // LWU
                    return 4;
                case 0x84000000u: // LH
                case 0x94000000u: // LHU
                case 0x88000000u: // LWL
                case 0x98000000u: // LWR
                    return 2;
                case 0xc4000000u: // LWC1
                    return 4 | 256;
            }
            return 0;
        }
    }
}