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
using ee1Dec;
using Microsoft.VisualBasic;

namespace traceHLP2 {
    public partial class Trform : Form {
        public Trform() {
            InitializeComponent();
        }

        DevHLP2 core;

        static string fpHLPtxt = @"H:\EMU\pcsx2-0.9.4\myrecvol3-642\_20081124213503_4148_642_HLP0.txt";
        static string fpHLPbin = @"H:\EMU\pcsx2-0.9.4\myrecvol3-642\vifwr2_642_002.bin";

        private void Trform_Load(object sender, EventArgs e) {
            core = new DevHLP2(
                fpHLPtxt,
                fpHLPbin,
                @"H:\Proj\khkh_xldM\MEMO\HLP2out\savewar.txt", //$o H:\Proj\khkh_xldM\MEMO\HLP2out\savewar.txt Å® WARARRAY@H:\Dev\pcsx2.0.9.4\pcsx2\kkdf2\Haxkhkh.c
                @"H:\Proj\khkh_xldM\MEMO\HLP2out\savetbarec.txt" //$o H:\Proj\khkh_xldM\MEMO\HLP2out\savetbarec.txt Å® TBAARRAY@H:\Dev\pcsx2.0.9.4\pcsx2\kkdf2\Haxkhkh.c
                );

            foreach (HLPi hlp in core.alHLPi) {
                ListViewItem lvi = lvMod.Items.Add(hlp.startAddr.ToString("x6"));
                lvi.Tag = hlp;
            }

            lvDA.VirtualListSize = 0x368000 / 4;

            hvee.SetBin(core.fvif.eeram);

            hvsp.SetBin(core.fvif.spad32k);

            {
                uint tadr = core.fvif.tadr;
                Parsevif1c pval = new Parsevif1c(core.fvif.si, core.fvif.spsi, tadr);
                {
                    ListViewItem lvi = lvVifacc.Items.Add(core.fvif.tadr.ToString("x8"));
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add("start");
                }
                foreach (Pvce o in pval.al) {
                    ListViewItem lvi = lvVifacc.Items.Add(o.tadr.ToString("x8"));
                    lvi.SubItems.Add(o.addr.ToString("x8"));
                    lvi.SubItems.Add(o.ty);
                }
            }

            {
                List<HRow> al = UtReadH.Parse(
                    fpHLPtxt
                    );
                byte[] l2l = new byte[] { 1, 2, 4, 8, 16 };
                SortedDictionary<uint, object> dictAdded = new SortedDictionary<uint, object>();
                foreach (HRow hr in al) {
                    if (hr is HSfpc) {
                        HSfpc o = (HSfpc)hr;
                        if (dictAdded.ContainsKey(o.mempos) == false) {
                            dictAdded[o.mempos] = null;

                            if (0 != (o.mempos & 0x80000000U)) {
                                hvsp.AddRangeMarked(
                                    Convert.ToInt32(o.mempos - 0xF0000000U),
                                    l2l[o.ty & 15],
                                    Color.FromArgb(100, labelMWritten.BackColor),
                                    Color.Green
                                    );
                            }
                            else {
                                hvee.AddRangeMarked(
                                    Convert.ToInt32(o.mempos),
                                    l2l[o.ty & 15],
                                    Color.FromArgb(100, labelMWritten.BackColor),
                                    Color.Green
                                    );
                            }
                        }
                    }
                }
            }
        }

        private void lvDA_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            uint pc = (uint)(e.ItemIndex << 2);
            ListViewItem lvi = e.Item = new ListViewItem(pc.ToString("x6"));
            uint v = core.fvif.ReadAt(pc);
            EEis eis = EEDisarm.parse(v, pc);
            lvi.SubItems.Add(eis.ToString());

            if (seli == null) {

            }
            else if (pc == seli.startAddr) {
                CAUt.Apply(lvi, labelLStartAddr);
            }
            else if (seli.exits.ale.IndexOf(pc) >= 0) {
                CAUt.Apply(lvi, labelLExitAddr);
            }
            else if (seli.walk.dictUnkys.ContainsKey(pc)) {
                CAUt.Apply(lvi, labelLUnkys);
            }
            else if (seli.walk.dictUnks.ContainsKey(pc)) {
                CAUt.Apply(lvi, labelLUnks);
            }
            else if (seli.walk.dictTerm.ContainsKey(pc)) {
                CAUt.Apply(lvi, labelLTermination);
            }
            else if (seli.walk.dictAlready.ContainsKey(pc)) {
                CAUt.Apply(lvi, labelLAlreadyWalked);
            }
        }

        HLPi seli = null;

        private void lvMod_DoubleClick(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvMod.SelectedItems) {
                HLPi hlp = (HLPi)lvi.Tag;
                if (hlp == null) break;
                seli = hlp;
                GotoAddr(hlp.startAddr);
                lvDA.Invalidate();
                {
                    lvTopics.Items.Clear();
                    {
                        uint addr = hlp.startAddr;
                        ListViewItem lvia = lvTopics.Items.Add(addr.ToString("x6"));
                        CAUt.Apply(lvia, labelLStartAddr);
                        lvia.Tag = new AddrIt(addr, AddrTy.Start);
                    }
                    foreach (uint addr in hlp.exits.ale) {
                        ListViewItem lvia = lvTopics.Items.Add(addr.ToString("x6"));
                        CAUt.Apply(lvia, labelLExitAddr);
                        lvia.Tag = new AddrIt(addr, AddrTy.Term);
                    }
                    foreach (uint addr in hlp.walk.dictTerm.Keys) {
                        ListViewItem lvia = lvTopics.Items.Add(addr.ToString("x6"));
                        CAUt.Apply(lvia, labelLTermination);
                        lvia.Tag = new AddrIt(addr, AddrTy.Exit);
                    }
                    foreach (uint addr in hlp.walk.dictUnkys.Keys) {
                        ListViewItem lvia = lvTopics.Items.Add(addr.ToString("x6"));
                        CAUt.Apply(lvia, labelLUnkys);
                        lvia.Tag = new AddrIt(addr, AddrTy.Unkys);
                    }
                    foreach (uint addr in hlp.walk.dictUnks.Keys) {
                        ListViewItem lvia = lvTopics.Items.Add(addr.ToString("x6"));
                        CAUt.Apply(lvia, labelLUnks);
                        lvia.Tag = new AddrIt(addr, AddrTy.Unks);
                    }
                    lvTopics.Sorting = SortOrder.Ascending;
                    lvTopics.Sort();
                }
                break;
            }
        }

        enum AddrTy {
            Start, Exit, Unkys, Term, Unks,
        }
        class AddrIt {
            public AddrIt(uint addr, AddrTy ty) { this.addr = addr; this.ty = ty; }

            public uint addr;
            public AddrTy ty;
        }

        class CAUt {
            public static void Apply(ListViewItem lvi, Label o) {
                lvi.BackColor = o.BackColor;
                lvi.ForeColor = o.ForeColor;
            }
        }

        private void lvDA_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case 'g': {
                        if (e.Handled)
                            break;
                        string addrIn = Interaction.InputBox("addr?", "Type addr to go", "0", -1, -1);
                        try {
                            uint pc = Convert.ToUInt32(addrIn, 16);
                            GotoAddr(pc);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        catch (FormatException) { }
                        e.Handled = true;
                        break;
                    }
            }
        }

        bool GotoAddr(uint pc) {
            int i = (int)(pc >> 2);
            if (i < 0 || i >= lvDA.VirtualListSize)
                return false;
            ListViewItem lvi = lvDA.Items[i];
            lvDA.EnsureVisible(i);
            lvi.Focused = true;
            lvi.Selected = true;
            return true;
        }

        private void lvMod_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void lvTopics_DoubleClick(object sender, EventArgs e) {

        }

        private void lvTopics_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvTopics.SelectedItems) {
                AddrIt o = lvi.Tag as AddrIt;
                if (o != null) {
                    GotoAddr(o.addr);
                    break;
                }
                break;
            }
        }

        private void hvee_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar) {
                case 'g': {
                        if (e.Handled)
                            break;
                        string addrIn = Interaction.InputBox("addr?", "Type addr to go", "0", -1, -1);
                        try {
                            uint pc = Convert.ToUInt32(addrIn, 16);
                            HvxxSetPos(pc);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        catch (FormatException) { }
                        e.Handled = true;
                        break;
                    }
            }
        }

        void HvxxSetPos(uint pc) {
            if (pc < 32U * 1024U * 1024U) {
                HveeSetPos(pc);
            }
            else if (0xF0000000U <= pc && pc <= 0xF0007FFF) {
                HvspSetPos(pc & 0x7FFFU);
            }
        }

        void HveeSetPos(uint pos) {
            hvee.SetPos((int)pos);
        }

        void HvspSetPos(uint pos) {
            hvsp.SetPos((int)pos);
        }

        private void lvVifacc_DoubleClick(object sender, EventArgs e) {

            foreach (ListViewItem lvi in lvVifacc.SelectedItems) {
                uint addr = Convert.ToUInt32(lvi.Text, 16);
                HvxxSetPos(addr);
                break;
            }
        }

    }

    public class Pvce { // parse vif chain mode entry
        public string ty;
        public uint tadr, addr;
        public ushort qwc;

        public Pvce(string ty, uint tadr, uint addr, ushort qwc) {
            this.ty = ty;
            this.tadr = tadr;
            this.addr = addr;
            this.qwc = qwc;
        }
    }

    public class Parsevif1c {
        public List<Pvce> al = new List<Pvce>();

        public Parsevif1c(Stream siee, Stream sisp, uint tadr) {
            BinaryReader bree = new BinaryReader(siee);
            BinaryReader brsp = new BinaryReader(sisp);
            Stack<uint> asr = new Stack<uint>();
            while (true) {
                UInt64 tag;
                if (tadr < 0x80000000U) {
                    siee.Position = tadr;
                    tag = bree.ReadUInt64();
                }
                else {
                    sisp.Position = tadr - 0xF0000000U;
                    tag = brsp.ReadUInt64();
                }
                byte id = (byte)((((uint)tag) >> 28) & 7);
                ushort qwc = (ushort)tag;
                uint addr = (uint)(tag >> 32);
                byte irq = (byte)(((uint)tag >> 31) & 1);
                switch (id) {
                    case 0://refe
                        al.Add(new Pvce("refe", tadr, addr, qwc));
                        goto endTag;

                    case 1://cnt
                        al.Add(new Pvce("cnt", tadr, tadr + 16U, qwc));
                        tadr = Convert.ToUInt32(tadr + 16 + 16 * qwc);
                        break;

                    case 2://next
                        al.Add(new Pvce("next", tadr, tadr + 16U, qwc));
                        tadr = Convert.ToUInt32(addr);
                        break;

                    case 3://ref
                        al.Add(new Pvce("ref", tadr, addr, qwc));
                        tadr += 16;
                        break;

                    case 4://refs
                        al.Add(new Pvce("refs", tadr, addr, qwc));
                        tadr += 16;
                        break;

                    case 5://call
                        al.Add(new Pvce("call", tadr, tadr + 16U, qwc));
                        asr.Push(Convert.ToUInt32(tadr + 16 + 16 * qwc));
                        tadr = addr;
                        break;

                    case 6://ret
                        al.Add(new Pvce("ret", tadr, tadr + 16U, qwc));
                        if (asr.Count == 0) goto endTag;
                        tadr = asr.Pop();
                        break;

                    case 7://end
                        al.Add(new Pvce("end", tadr, tadr + 16U, qwc));
                        goto endTag;

                }
            }
        endTag:
            ;
        }
    }
}