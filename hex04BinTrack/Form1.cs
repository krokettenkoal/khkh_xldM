using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics;
using hex04BinTrack.Properties;
using vconv122;
using hex04BinTrack.R;
using khkh_xldMii.Models;

namespace hex04BinTrack {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        class GoItem {
            public uint off;
            public int typ, len, tid;

            public GoItem(int tid, uint off, int typ, int len) {
                this.tid = tid;
                this.off = off;
                this.typ = typ;
                this.len = len;
            }

            public override string ToString() {
                string t = "";
                if (typ == 0) t = " EE";
                if (typ == 1) t = " VIF1";
                return string.Format("#{0,4} {1:X8} {2,3} {3}", tid, off, len, t);
            }
        }

        class CmpOff : IComparer {
            public int Compare(object xx, object yy) {
                GoItem x = (GoItem)xx;
                GoItem y = (GoItem)yy;
                return x.off.CompareTo(y.off);
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (!File.Exists(openFileDialogBin.FileName))
                openFileDialogBin.ShowDialog(this);
            if (!File.Exists(openFileDialogBin.FileName))
                Close();
            if (!File.Exists(openFileDialogText.FileName))
                openFileDialogText.ShowDialog(this);
            if (!File.Exists(openFileDialogText.FileName))
                Close();

#if false
            bin04 = File.ReadAllBytes(openFileDialogBin.FileName);
            vits = new BitArray(bin04.Length);
            vit2 = new BitArray(bin04.Length);

            using (StreamReader rr = new StreamReader(openFileDialogText.FileName, Encoding.Default)) {
                string row;
                int tid = 0;
                while (null != (row = rr.ReadLine())) {
                    Match M = Regex.Match(row, "^testRead4\\: pc0\\([0-9a-f]{8}\\) \\@ ([0-9a-f]{8})", RegexOptions.IgnoreCase);
                    if (M.Success) {
                        uint off = (Convert.ToUInt32(M.Groups[1].Value, 16) - 0x970B80);
                        if (off < 0x24F90) {
                            vits.Set((int)off, true);
                            listBox1.Items.Add(new GoItem(tid++, off, 0, 0));
                        }
                    }
                    M = Regex.Match(row, "^_VIF1chain p ([0-9a-f]{8}) c [ ]*([0-9]+)");
                    if (M.Success) {
                        uint off = (Convert.ToUInt32(M.Groups[1].Value, 16) - 0x970B80);
                        int qwc = Convert.ToInt32(M.Groups[2].Value);
                        while (qwc > 0) {
                            if (off < 0x24F90) {
                                vit2.Set((int)off, true);
                                listBox1.Items.Add(new GoItem(tid++, off, 1, qwc));
                            }
                            off += 16;
#if false
                            qwc--;
#else
                            qwc = 0;
#endif
                        }
                    }
                }
                rr.Close();
            }
            //sortBy(new CmpOff());
            readDomain();
            repaint();
#endif

            try {
                foreach (string fp in File.ReadAllLines(Path.Combine(Settings.Default.DirMemo, "mdlxmset.txt"))) {
                    if (fp.Trim().Length == 0) continue;
                    String[] cols = fp.Trim().Split('\t');
                    String mdlx = cols[0];
                    String mset = cols.Length >= 2 ? cols[1] : null;
                    ToolStripItem tsi = loaderPane.Items.Add(Path.GetFileName(mdlx));
                    tsi.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    tsi.Click += delegate(object sender2, EventArgs e2) {
                        loadMdlx(mdlx);
                        if (mset != null) loadMset(mset);
                    };
                }
            }
            catch (IOException) {
            }
        }

        class Domain : IComparable<Domain> {
            public int pos, len;
            public string name;

            public Domain() { }
            public Domain(string name, int pos, int len) {
                this.name = name;
                this.pos = pos;
                this.len = len;
            }

            public override string ToString() {
                return string.Format("{0:X5}-{1:X5} {2}", pos, pos + len - 1, name);
            }

            #region IComparable<Domain> メンバ

            public int CompareTo(Domain other) {
                int t = pos.CompareTo(other.pos);
                return t;
            }

            #endregion
        }

        class EntRoot {
            public int off;

            public EntRoot(int off) {
                this.off = off;
            }
        }

        class DMAtag {
            public int off, qwc;

            public DMAtag(int off, int qwc) {
                this.off = off;
                this.qwc = qwc;
            }
            public override string ToString() {
                return string.Format("{0:X5} c {1,2}", off, qwc);
            }
        }
        class DMAtag2 {
            public int off, qwc;
            public int sel;
            public int[] alaxi;

            public DMAtag2(int off, int qwc, int sel, int[] alaxi) {
                this.off = off;
                this.qwc = qwc;
                this.sel = sel;
                this.alaxi = alaxi;
            }
            public override string ToString() {
                return string.Format("{0:X5} c {1,2}", off, qwc);
            }
        }

        private void readDomain() {
            MemoryStream si = new MemoryStream(bin04, false);
            BinaryReader br = new BinaryReader(si);
            Queue<EntRoot> aler = new Queue<EntRoot>();
            aler.Enqueue(new EntRoot(0x90));
            List<Domain> aldom = new List<Domain>();
            listBox3.Items.Clear();
            int postbl3 = 0;
            while (aler.Count != 0) {
                EntRoot er = aler.Dequeue();
                si.Position = er.off + 0x10;
                int cnt2 = br.ReadUInt16();
                si.Position = er.off + 0x1C;
                int cnt1 = br.ReadUInt16();

                aldom.Add(new Domain("T3.1(" + postbl3 + ")", er.off, 0x20 * (1 + cnt1))); postbl3++;

                for (int c1 = 0; c1 < cnt1; c1++) {
                    si.Position = er.off + 0x20 + 0x20 * c1 + 0x10;
                    int pos1 = br.ReadInt32() + er.off;
                    int pos2 = br.ReadInt32() + er.off;
                    si.Position = er.off + 0x20 + 0x20 * c1 + 0x04;
                    int texi = br.ReadInt32();
                    si.Position = pos2;
                    int cnt1a = br.ReadInt32();
                    aldom.Add(new Domain("T1.1(" + c1 + ")", pos2, RUtil.RoundUpto16(4 + 4 * cnt1a)));
                    List<int> alv1 = new List<int>(cnt1a);
                    for (int c1a = 0; c1a < cnt1a; c1a++) alv1.Add(br.ReadInt32());

                    List<int> aloffDMAtag = new List<int>();
                    List<int[]> alaxi = new List<int[]>();
                    List<int> alaxref = new List<int>();
                    aloffDMAtag.Add(pos1);
                    si.Position = pos1 + 16;
                    for (int c1a = 0; c1a < cnt1a; c1a++) {
                        if (alv1[c1a] == -1) {
                            aloffDMAtag.Add((int)si.Position + 0x10);
                            si.Position += 0x20;
                        }
                        else {
                            si.Position += 0x10;
                        }
                    }
                    for (int c1a = 0; c1a < cnt1a; c1a++) {
                        if (c1a + 1 == cnt1a) {
                            alaxref.Add(alv1[c1a]);
                            alaxi.Add(alaxref.ToArray()); alaxref.Clear();
                        }
                        else if (alv1[c1a] == -1) {
                            alaxi.Add(alaxref.ToArray()); alaxref.Clear();
                        }
                        else {
                            alaxref.Add(alv1[c1a]);
                        }
                    }

                    int pos1a = (int)si.Position;
                    aldom.Add(new Domain("T1.2DMAc(" + c1 + ")", pos1, pos1a - pos1));

                    int tpos = 0;
                    foreach (int offDMAtag in aloffDMAtag) {
                        si.Position = offDMAtag;
                        // Source Chain Tag
                        int qwcsrc = (br.ReadInt32() & 0xFFFF);
                        int offsrc = (br.ReadInt32() & 0x7FFFFFFF) + er.off;
                        listBox3.Items.Add(new DMAtag2(offsrc, qwcsrc, texi, alaxi[tpos])); tpos++;

                        aldom.Add(new Domain("vif", offsrc, 16 * qwcsrc));
                    }
                }

                si.Position = er.off + 0x14;
                int off2 = br.ReadInt32();
                if (off2 != 0) {
                    off2 += er.off;
                    int len2 = 0x40 * cnt2;
                    aldom.Add(new Domain("T2(" + 0 + ")", off2, len2));
                    parseT2Struc(off2, len2);
                }

                si.Position = er.off + 0x18;
                int off4 = br.ReadInt32();
                if (off4 != 0) {
                    off4 += er.off;
                    int len4 = off2 - off4;
                    aldom.Add(new Domain("T3.2(" + 0 + ")", off4, len4));
                }

                si.Position = er.off + 0xC;
                int off3 = br.ReadInt32();
                if (off3 != 0) {
                    off3 += er.off;
                    aler.Enqueue(new EntRoot(off3));
                }
            }

            aldom.Sort();
            listBox2.Items.Clear();
            listBox2.Items.AddRange((object[])aldom.ToArray());
        }

        private void parseT2Struc(int off2, int len2) {
            MemoryStream si = new MemoryStream(bin04, off2, len2, false);
            BinaryReader br = new BinaryReader(si);

            alaxbone.Clear();
            for (int x = 0; x < len2 / 0x40; x++) {
                alaxbone.Add(UtilAxBoneReader.read(br));
            }
            Array.ForEach<AxBone>(alaxbone.ToArray(), new Action<AxBone>(test0));
        }

        void test0(AxBone ax) {
            Debug.Assert(ax.v08 == 0);
        }

        List<AxBone> alaxbone = new List<AxBone>();

        class RUtil {
            public static int RoundUpto16(int val) {
                return (val + 15) & (~15);
            }
        }

        private void sortBy(IComparer cmp) {
            ArrayList al = new ArrayList(listBox1.Items);
            al.Sort(cmp);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(al.ToArray());
        }

        const int cntlin = 32;

        private void repaint() {
            // 00000000: xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx xx | ................
            Bitmap pic;
            Font fnt = label1.Font;
            SizeF size;
            using (Graphics cv = CreateGraphics()) {
                cv.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                size = cv.MeasureString("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", fnt);
                size.Width /= 100;
                pic = new Bitmap((int)(size.Width * (60 + 20 * 4)), (int)(size.Height * cntlin));
            }
            using (Graphics cv = Graphics.FromImage(pic)) {
                List<int> markoff = new List<int>();
                List<int> mark2off = new List<int>();
                int coff = curoff;
                int len = bin04.Length;
                MemoryStream si = new MemoryStream(bin04, false);
                BinaryReader br = new BinaryReader(si);
                for (int y = 0; y < cntlin; y++) {
                    string lin = coff.ToString("X8") + ": ";
                    int off;

                    off = coff;
                    for (int x = 0; x < 16 && off < len; off++, x++) {
                        lin += bin04[off].ToString("X2") + " ";

                        if (vits.Get(off))
                            markoff.Add(16 * y + x);
                        if (vit2.Get(off))
                            mark2off.Add(16 * y + x);
                    }
                    while (lin.Length < 58) lin += ' ';
                    lin += "| ";

                    off = coff;
#if false
                    si.Position = off;
                    for (int x = 0; x < 4 && off + 4 <= len; off += 4, x++) {
                        float f = br.ReadSingle();
                        lin += string.Format("{0,20}", f);
                    }
#else
                    for (int x = 0; x < 16 && off < len; off++, x++) {
                        char c = (char)bin04[off];
                        if (char.IsLetterOrDigit(c) || char.IsSymbol(c)) {
                            lin += c;
                        }
                        else {
                            lin += '.';
                        }
                    }
#endif

                    cv.DrawString(lin, fnt, Brushes.Black, new PointF(0, size.Height * y));
                    coff += 16;
                }
                if (true) { // primary
                    Brush brush = new SolidBrush(Color.FromArgb(100, Color.LightSalmon));
                    foreach (int aoff in markoff) {
                        cv.FillRectangle(brush
                            , size.Width * ((aoff % 16) * 3 + 10)
                            , size.Height * ((aoff / 16))
                            , size.Width * 2
                            , size.Height);
                    }
                }
                if (true) { // seconday
                    Brush brush = new SolidBrush(Color.FromArgb(100, Color.LightGreen));
                    foreach (int aoff in mark2off) {
                        cv.FillRectangle(brush
                            , size.Width * ((aoff % 16) * 3 + 10)
                            , size.Height * ((aoff / 16))
                            , size.Width * 2
                            , size.Height);
                    }
                }
                if (true) { // seconday
                    Brush brush = new SolidBrush(Color.FromArgb(50, Color.BlueViolet));
                    int aoff = seloff - curoff;
                    if (0 <= aoff && aoff < 512) {
                        cv.FillRectangle(brush
                            , size.Width * ((aoff % 16) * 3 + 10)
                            , size.Height * ((aoff / 16))
                            , size.Width * 2
                            , size.Height);
                    }
                }
                if (true) { // domain coloured
                    Brush brush = new SolidBrush(Color.FromArgb(20, Color.Green));
                    int baseoff = curoff;
                    int baselen = 16 * cntlin;
                    foreach (Domain dom in listBox2.Items) {
                        int off1 = dom.pos - baseoff;
                        int off2 = dom.pos + dom.len - baseoff;

                        int xof1 = Math.Max(0, Math.Min(baselen, off1));
                        int xof2 = Math.Max(0, Math.Min(baselen, off2));
                        bool first = true;
                        while (xof1 < xof2) {
                            bool last = ((xof1 & (~15)) == (xof2 & (~15)));
                            bool final = (xof2 - xof1) <= 16;
                            int vx1 = (xof1 & 15);
                            int vx2 = 1 + (last ? (xof2 & 15) : (15));
                            RFiller.fill(cv, brush, size,
                                10 + 3 * vx1,
                                xof1 / 16,
                                10 + 3 * vx2 - 1,
                                xof1 / 16 + 1,
                                first, final
                                );
                            first = false;
                            xof1 = (xof1 + 16) & (~15);
                        }
                    }
                }
            }
            pictureBox1.Image = pic;
        }

        class RFiller {
            public static void fill(Graphics cv, Brush br, SizeF size, int x1, int y1, int x2, int y2, bool first, bool last) {
                cv.FillRectangle(br, RectangleF.FromLTRB(
                    x1 * size.Width,
                    y1 * size.Height,
                    x2 * size.Width,
                    y2 * size.Height - (last ? 2 : 0)
                    ));
            }
        }

        byte[] bin04 = null;
        BitArray vits = null;
        BitArray vit2 = null;

        int curoff = 0;
        int seloff = 0;

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            int f = 0;
            switch (e.KeyCode) {
                case Keys.Home:
                    curoff = 0; f = 1; break;
                case Keys.Up:
                    curoff = curoff - 16; f = 1; break;
                case Keys.Down:
                    curoff += 16; f = 1; break;
                case Keys.Left:
                    curoff--; f = 1; break;
                case Keys.Right:
                    curoff++; f = 1; break;
                case Keys.PageUp:
                    curoff -= 16 * cntlin; f = 1; break;
                case Keys.PageDown:
                    curoff += 16 * cntlin; f = 1; break;
            }
            if (f != 0) {
                e.Handled = true;
                curoff = Math.Max(0, curoff);
                repaint();
            }
        }

        void goTo(int off) {
#if false
            seloff = (int)(gi.off);
            curoff = (int)(gi.off & (~15));
#else
            seloff = (int)off;
            curoff = (int)(off & (~511));
#endif
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            GoItem gi = (GoItem)listBox1.SelectedItem;
            if (gi == null) return;
            goTo((int)gi.off);
            repaint();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e) {
            GoItem gi = (GoItem)listBox1.SelectedItem;
            if (gi == null) return;
            parseVIFpacket((int)gi.off, gi.len);
        }

        class RangeUtil {
            public uint min = uint.MaxValue, max = uint.MinValue;

            public uint pass(uint val) {
                min = Math.Min(val, min);
                max = Math.Max(val, max);
                return val;
            }
            public UInt16 pass(UInt16 val) {
                min = Math.Min(val, min);
                max = Math.Max(val, max);
                return val;
            }
            public byte pass(byte val) {
                min = Math.Min(val, min);
                max = Math.Max(val, max);
                return val;
            }
        }

        void parseVIFpacket(int off, int len) {
            MemoryStream si = new MemoryStream(bin04, (int)off, 16 * len, false);
            BinaryReader br = new BinaryReader(si);
            StringBuilder s = new StringBuilder();
            while (si.Position < si.Length) {
                long curPos = si.Position;
                uint v = br.ReadUInt32();
                int cmd = ((int)(v >> 24) & 0x7F);
                s.AppendFormat("{0:X4} {1} "
                    , curPos
                    , ((v & 0x80000000) != 0) ? 'I' : ' '
                    );
                switch (cmd) {
                    case 0x00: s.AppendFormat("nop\n"); break;
                    case 0x01: {
                            int cl = ((int)(v >> 0) & 0xFF);
                            int wl = ((int)(v >> 8) & 0xFF);
                            s.AppendFormat("stcycl cl {0:x2} wl {1:x2}\n", cl, wl);
                            break;
                        }
                    case 0x02: s.AppendFormat("offset\n"); break;
                    case 0x03: s.AppendFormat("base\n"); break;
                    case 0x04: s.AppendFormat("itop\n"); break;
                    case 0x05: s.AppendFormat("stmod\n"); break;
                    case 0x06: s.AppendFormat("mskpath3\n"); break;
                    case 0x07: s.AppendFormat("mark\n"); break;
                    case 0x10: s.AppendFormat("flushe\n"); break;
                    case 0x11: s.AppendFormat("flush\n"); break;
                    case 0x13: s.AppendFormat("flusha\n"); break;
                    case 0x14: s.AppendFormat("mscal\n"); break;
                    case 0x17: s.AppendFormat("mscnt\n"); break;
                    case 0x15: s.AppendFormat("mscalf\n"); break;
                    case 0x20: {
                            uint r1 = br.ReadUInt32();
                            string stv = "";
                            for (int x = 0; x < 16; x++) {
                                if (0 == (x & 3)) stv += ' ';
                                stv += (((int)(r1 >> (2 * x))) & 3) + " ";
                            }
                            s.AppendFormat("stmask {0}\n", stv);
                            break;
                        }
                    case 0x30: {
                            uint r1 = br.ReadUInt32();
                            uint r2 = br.ReadUInt32();
                            uint r3 = br.ReadUInt32();
                            uint r4 = br.ReadUInt32();
                            s.AppendFormat("strow {0:x8} {1:x8} {2:x8} {3:x8}\n", r1, r2, r3, r4);
                            break;
                        }
                    case 0x31: {
                            uint r1 = br.ReadUInt32();
                            uint r2 = br.ReadUInt32();
                            uint r3 = br.ReadUInt32();
                            uint r4 = br.ReadUInt32();
                            s.AppendFormat("stcol {0:x8} {1:x8} {2:x8} {3:x8}\n", r1, r2, r3, r4);
                            break;
                        }
                    case 0x4A: s.AppendFormat("mpg\n"); break;
                    case 0x50: {
                            s.AppendFormat("direct\n");
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            si.Position = (si.Position + 15) & (~15);
                            si.Position += 16 * imm;
                            break;
                        }
                    case 0x51: {
                            s.AppendFormat("directhl\n");
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            si.Position = (si.Position + 15) & (~15);
                            si.Position += 16 * imm;
                            break;
                        }
                    default: {
                            if (0x60 == (cmd & 0x60)) {
                                RangeUtil swift = new RangeUtil();
                                int m = ((int)(cmd >> 4) & 1);
                                int vn = ((int)(cmd >> 2) & 0x3);
                                int vl = ((int)(cmd >> 0) & 0x3);

                                int size = ((int)(v >> 16) & 0xFF);
                                int flg = ((int)(v >> 15) & 1);
                                int usn = ((int)(v >> 14) & 1);
                                int addr = ((int)(v >> 0) & 0x3FF);

                                int cbEle = 0, cntEle = 1;
                                string sl = "";
                                switch (vl) {
                                    case 0: cbEle = 4; sl = "32"; break;
                                    case 1: cbEle = 2; sl = "16"; break;
                                    case 2: cbEle = 1; sl = "8"; break;
                                    case 3: cbEle = 2; sl = "5+5+5+1"; break;
                                }
                                string sn = "";
                                switch (vn) {
                                    case 0: cntEle = 1; sn = "S"; break;
                                    case 1: cntEle = 2; sn = "V2"; break;
                                    case 2: cntEle = 3; sn = "V3"; break;
                                    case 3: cntEle = 4; sn = "V4"; break;
                                    default: Debug.Fail("Ahh vn!"); break;
                                }
                                int cbTotal = (cbEle * cntEle * size + 3) & (~3);
                                long newPos = si.Position + cbTotal;

                                s.AppendFormat("unpack {0}-{1} c {2} a {3:X3} usn {4} flg {5} m {6}\n", sn, sl, size, addr, usn, flg, m);
                                if (vl == 0 && (vn == 2 || vn == 3)) {
                                    for (int y = 0; y < size; y++) {
                                        s.Append("    ");
                                        for (int x = 0; x < cntEle; x++) {
                                            //float flt = br.ReadSingle();
                                            //s.AppendFormat("{0,20}", flt);
                                            s.AppendFormat("{0:x8} ", swift.pass(br.ReadUInt32()));
                                        }
                                        s.Append("\n");
                                    }
                                }
                                else if (vl == 1) {
                                    for (int y = 0; y < size; y++) {
                                        s.Append("    ");
                                        for (int x = 0; x < cntEle; x++) {
                                            s.AppendFormat("{0:x4} ", swift.pass(br.ReadUInt16()));
                                        }
                                        s.Append("\n");
                                    }
                                }
                                else if (vl == 2) {
                                    for (int y = 0; y < size; y++) {
                                        s.Append("    ");
                                        for (int x = 0; x < cntEle; x++) {
                                            s.AppendFormat("{0:x2} ", swift.pass(br.ReadByte()));
                                        }
                                        s.Append("\n");
                                    }
                                }
                                si.Position = newPos;
                                s.Append("    "); s.AppendFormat("min({0}), max({1})\n", swift.min, swift.max);
                                break;
                            }
                            s.AppendFormat("{0:X2}\n", cmd); break;
                        }
                }
            }
            s.Replace("\n", "\r\n");
            textBoxVIF.Text = s.ToString();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e) {
            Domain dom = (Domain)listBox2.SelectedItem;
            if (dom == null) return;
            goTo(dom.pos);
            repaint();

            if (dom.name.StartsWith("T2")) {
                parseTestT2(dom.pos, dom.len);
            }
#if false
            if (dom.name.StartsWith("T3.2")) {
                parseT3_2(dom.pos, dom.len);
            }
#endif
        }

        class FloatVals {
            public int i;
            public float x, y, z, w;

            public FloatVals(int i, float x, float y, float z, float w) {
                this.i = i;
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            public override string ToString() {
                return string.Format("{0,2} {1,10:f} {2,10:f} {3,10:f} {4,10:f}", i, x, y, z, w);
            }
        }

        private void parseT3_2(int pos, int len) {
            MemoryStream si = new MemoryStream(bin04, pos, len, false);
            BinaryReader br = new BinaryReader(si);
            listBox4.Items.Clear();
            int i = 0;
            while (si.Position < si.Length) {
                float v1 = br.ReadSingle();
                float v2 = br.ReadSingle();
                float v3 = br.ReadSingle();
                float v4 = br.ReadSingle();
                listBox4.Items.Add(new FloatVals(i, v1, v2, v3, v4));
                i++;
            }
        }

        private void parseTestT2(int off, int len) {
            MemoryStream si = new MemoryStream(bin04, off, len);
            BinaryReader br = new BinaryReader(si);
            listBox4.Items.Clear();
            for (int t = 0; t < len / 0x40; t++) {
                si.Position = 0x40 * t;

                int v00 = br.ReadInt32();
                int v04 = br.ReadInt32();
                int v08 = br.ReadInt32();
                int v0c = br.ReadInt32();

                float x1 = (br.ReadSingle());
                float y1 = (br.ReadSingle());
                float z1 = (br.ReadSingle());
                float w1 = (br.ReadSingle());
                float x2 = (br.ReadSingle());
                float y2 = (br.ReadSingle());
                float z2 = (br.ReadSingle());
                float w2 = (br.ReadSingle());
                float x3 = (br.ReadSingle());
                float y3 = (br.ReadSingle());
                float z3 = (br.ReadSingle());
                float w3 = (br.ReadSingle());

                string text = string.Format(
                    "{0,3} {1,3}|{2}|{3}|{4,7:f}{5,7:f}{6,7:f}{7,7:f}|{8,7:f}{9,7:f}{10,7:f}{11,7:f}|{12,7:f}{13,7:f}{14,7:f}{15,7:f}|\n"
                    , v00, v04, v08, v0c
                    , x1, y1, z1, w1
                    , x2, y2, z2, w2
                    , x3, y3, z3, w3
                    );
                listBox4.Items.Add(text);
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e) { }

        private void textBox1_MouseDown(object sender, MouseEventArgs e) { }

        private void textBox1_MouseClick(object sender, MouseEventArgs e) {
            int d = e.Delta;
            if (d != 0) {
                int t = curoff;
                t = Math.Max(0, t + d);
                t = Math.Min(t, (int)bin04.Length);
                goTo(t);
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e) {
            DMAtag2 dt = (DMAtag2)listBox3.SelectedItem;
            if (dt == null) return;
            goTo(dt.off);
            repaint();
            parseVIFpacket(dt.off, dt.qwc);
        }


        class UtilKH2VU1 {
            public static int offsetMatrices(byte[] bin, int tops) {
                MemoryStream si = new MemoryStream(bin, false);
                si.Position = 16 * tops + 0x1c;
                return new BinaryReader(si).ReadInt32() + tops;
            }
        }

        const int TOPS = 0x220;

        private void buttonMake3D_Click(object sender, EventArgs e) {
            if (0 == (Form.ModifierKeys & Keys.Control)) {
                using (ProtForm2 form = new ProtForm2(timc, mset)) {
                    foreach (DMAtag2 tag in listBox3.Items) {
                        VU1Mem vu1mem = new VU1Mem();
                        ParseVIF1 vif1 = new ParseVIF1(vu1mem);
                        vif1.Parse(new MemoryStream(bin04, tag.off, tag.qwc * 16, false), TOPS);

                        form.PushDeferred(vu1mem, tag.sel, TOPS, tag.alaxi);
                    }
                    form.InitBone(alaxbone);
                    form.MdlxInMem = mdlxInMem;
                    form.ShowDialog(this);
                }
            }
            else {
                List<RPart> alrp = new List<RPart>();
                foreach (DMAtag2 tag in listBox3.Items) {
                    VU1Mem vu1mem = new VU1Mem();
                    ParseVIF1 vif1 = new ParseVIF1(vu1mem);
                    vif1.Parse(new MemoryStream(bin04, tag.off, tag.qwc * 16, false), TOPS);

                    int top2 = UtilKH2VU1.offsetMatrices(vu1mem.vumem, TOPS);

                    alrp.Add(new RPart(vu1mem, tag.sel, TOPS, top2, tag.alaxi, tag.off));
                }

                F2DRi f2dri = new F2DRi(alrp, alaxbone, timc);

                using (ProtForm2Dev form = new ProtForm2Dev(f2dri)) {
                    form.ShowDialog(this);
                }
            }
        }

        class DHexUtil {
            public static void hexbin(byte[] bin) {
                hexbin(bin, 0, bin.Length);
            }
            public static void hexbin(byte[] bin, int x, int cx) {
                for (int t = 0; t < cx; t += 16) {
                    string str = t.ToString("X8") + ": ";
                    for (int s = 0; s < 16 && s + t < cx; s++) {
                        str += (string.Format("{0:X2} ", bin[x + s + t]));
                    }
                    Debug.WriteLine(str);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            Form1_DragOver(sender, e);
        }

        private void Form1_DragOver(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fs == null) return;
            foreach (string f in fs) {
                if (string.Compare(Path.GetExtension(f), ".mdlx", true) == 0) {
                    loadMdlx(f);
                    var msetFile = Path.ChangeExtension(f, ".mset");
                    if (File.Exists(msetFile)) {
                        loadMset(msetFile);
                    }
                    break;
                }
            }
        }

        hex04BinTrack.T.ReadMset mset = null;

        void loadMset(string f) {
            using (FileStream fs = File.OpenRead(f)) {
                mset = new hex04BinTrack.T.ReadMset(fs);
            }
        }

        Texex2[] timc = null;

        private MemoryStream mdlxInMem = new MemoryStream();

        private void loadMdlx(string f) {
            using (FileStream fs = File.OpenRead(f)) {
                {
                    mdlxInMem.Position = 0;
                    mdlxInMem.SetLength(fs.Length);
                    fs.CopyTo(mdlxInMem);
                    fs.Position = 0;
                    mdlxInMem.Position = 0;
                }
                ReadBar.Barent[] ents = ReadBar.Explode(fs);
                fs.Close();
                foreach (ReadBar.Barent ent in ents) {
                    if (ent.k == 7) {
                        timc = TIMCollection.Load(new MemoryStream(ent.bin, false));
                    }
                    if (ent.k == 4) {
                        curoff = 0; seloff = -1;

                        bin04 = ent.bin;
                        vits = new BitArray(bin04.Length);
                        vit2 = new BitArray(bin04.Length);

                        listBox1.Items.Clear();

                        readDomain();
                        repaint();

                        linkLabelFn.Text = Path.GetFileName(f);
                        linkLabelFn.Tag = f;
                    }
                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void buttonLoad1_Click(object sender, EventArgs e) {
            loadMdlx(Settings.Default.Load1mdlx);
        }
    }

    public class ReadBar {
        public class Barent {
            public int k;
            public string id;
            public int off, len;
            public byte[] bin;
        }
        public static Barent[] Explode(Stream si) {
            BinaryReader br = new BinaryReader(si);
            if (br.ReadByte() != 'B' || br.ReadByte() != 'A' || br.ReadByte() != 'R' || br.ReadByte() != 1)
                throw new NotSupportedException();
            int cx = br.ReadInt32();
            br.ReadBytes(8);
            List<Barent> al = new List<Barent>();
            for (int x = 0; x < cx; x++) {
                Barent ent = new Barent();
                ent.k = br.ReadInt32();
                ent.id = Encoding.ASCII.GetString(br.ReadBytes(4)).TrimEnd((char)0);
                ent.off = br.ReadInt32();
                ent.len = br.ReadInt32();
                al.Add(ent);
            }
            for (int x = 0; x < cx; x++) {
                Barent ent = al[x];
                si.Position = ent.off;
                ent.bin = br.ReadBytes(ent.len);
                Debug.Assert(ent.bin.Length == ent.len);
            }
            return al.ToArray();
        }
    }

    public class ReadBar2 {
        public class Barent {
            public int k;
            public string id;
            public int off, len, reloff;
        }
        public static Barent[] Explode(Stream si, int off) {
            BinaryReader br = new BinaryReader(si);
            si.Position = off;
            if (br.ReadByte() != 'B' || br.ReadByte() != 'A' || br.ReadByte() != 'R' || br.ReadByte() != 1)
                throw new NotSupportedException();
            int cx = br.ReadInt32();
            br.ReadBytes(8);
            List<Barent> al = new List<Barent>();
            for (int x = 0; x < cx; x++) {
                Barent ent = new Barent();
                ent.k = br.ReadInt32();
                ent.id = Encoding.ASCII.GetString(br.ReadBytes(4)).TrimEnd((char)0);
                ent.off = off + (ent.reloff = br.ReadInt32());
                ent.len = br.ReadInt32();
                al.Add(ent);
            }
            return al.ToArray();
        }
    }

    public class AxBone {
        public int cur, parent, v08, v0c;
        public float x1, y1, z1, w1;
        public float x2, y2, z2, w2;
        public float x3, y3, z3, w3;

        public AxBone Clone() {
            return (AxBone)MemberwiseClone();
        }
    }

    public class UtilAxBoneWriter {
        public static void write(BinaryWriter wr, AxBone o) {
            wr.Write((int)o.cur);
            wr.Write((int)o.parent);
            wr.Write((int)o.v08);
            wr.Write((int)o.v0c);
            wr.Write((float)o.x1); wr.Write((float)o.y1); wr.Write((float)o.z1); wr.Write((float)o.w1);
            wr.Write((float)o.x2); wr.Write((float)o.y2); wr.Write((float)o.z2); wr.Write((float)o.w2);
            wr.Write((float)o.x3); wr.Write((float)o.y3); wr.Write((float)o.z3); wr.Write((float)o.w3);
        }
    }
    public class UtilAxBoneReader {
        public static AxBone read(BinaryReader br) {
            AxBone o = new AxBone();
            o.cur = br.ReadInt32();
            o.parent = br.ReadInt32();
            o.v08 = br.ReadInt32();
            o.v0c = br.ReadInt32();
            o.x1 = br.ReadSingle(); o.y1 = br.ReadSingle(); o.z1 = br.ReadSingle(); o.w1 = br.ReadSingle();
            o.x2 = br.ReadSingle(); o.y2 = br.ReadSingle(); o.z2 = br.ReadSingle(); o.w2 = br.ReadSingle();
            o.x3 = br.ReadSingle(); o.y3 = br.ReadSingle(); o.z3 = br.ReadSingle(); o.w3 = br.ReadSingle();
            return o;
        }
    }

    class UtilMatrixio {
        public static void write(BinaryWriter wr, SlimDX.Matrix A) {
            wr.Write((float)A.M11); wr.Write((float)A.M12); wr.Write((float)A.M13); wr.Write((float)A.M14);
            wr.Write((float)A.M21); wr.Write((float)A.M22); wr.Write((float)A.M23); wr.Write((float)A.M24);
            wr.Write((float)A.M31); wr.Write((float)A.M32); wr.Write((float)A.M33); wr.Write((float)A.M34);
            wr.Write((float)A.M41); wr.Write((float)A.M42); wr.Write((float)A.M43); wr.Write((float)A.M44);
        }
        public static SlimDX.Matrix read(BinaryReader br) {
            SlimDX.Matrix A = new SlimDX.Matrix();
            A.M11 = br.ReadSingle(); A.M12 = br.ReadSingle(); A.M13 = br.ReadSingle(); A.M14 = br.ReadSingle();
            A.M21 = br.ReadSingle(); A.M22 = br.ReadSingle(); A.M23 = br.ReadSingle(); A.M24 = br.ReadSingle();
            A.M31 = br.ReadSingle(); A.M32 = br.ReadSingle(); A.M33 = br.ReadSingle(); A.M34 = br.ReadSingle();
            A.M41 = br.ReadSingle(); A.M42 = br.ReadSingle(); A.M43 = br.ReadSingle(); A.M44 = br.ReadSingle();
            return A;
        }
    }

    class BasixAxBoneUtil {
        internal static void calc(VU1Mem vu1mem, int[] alaxi, int top2, AxBone[] alab) {
            MemoryStream si = new MemoryStream(vu1mem.vumem, true);
            BinaryWriter wr = new BinaryWriter(si);
            si.Position = 16 * top2;

            for (int bi = 0; bi < alaxi.Length; bi++) {
                int axi = alaxi[bi];
                List<int> alhier = new List<int>();
                while (axi != -1) {
                    alhier.Add(axi);
                    axi = alab[axi].parent;
                }

                SlimDX.Matrix A = SlimDX.Matrix.Identity;
                for (int wi = 0; wi < alhier.Count; wi++) {
                    int w = alhier[wi];
                    AxBone ax = alab[w];
                    if (ax.x2 != 0) A *= (SlimDX.Matrix.RotationX(ax.x2));
                    if (ax.y2 != 0) A *= (SlimDX.Matrix.RotationY(ax.y2));
                    if (ax.z2 != 0) A *= (SlimDX.Matrix.RotationZ(ax.z2));

                    A *= (SlimDX.Matrix.Translation(ax.x3, ax.y3, ax.z3));
                    //A.M41 += ax.x3;A.M42 += ax.y3;A.M43 += ax.z3;
                }
                UtilMatrixio.write(wr, A);
            }
        }
    }

    class UpdateAxBoneUtil {
        public static List<AxBone> apply(List<AxBone> xxx) {
            List<AxBone> al = new List<AxBone>();
            foreach (AxBone o in xxx) al.Add(o.Clone());
            string text = Clipboard.GetText();
            if (text != null) {
                foreach (string row in text.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n')) {
                    string[] cols = row.Split(',');
                    if (cols.Length == 3) {
                        int pos = int.Parse(cols[0]);
                        int ax = int.Parse(cols[1]);
                        float val = float.Parse(cols[2]);
                        AxBone v = al[pos];
                        if (false) { }
                        else if (ax == 0) v.x1 = val;
                        else if (ax == 1) v.y1 = val;
                        else if (ax == 2) v.z1 = val;
                        else if (ax == 3) v.x2 = val;
                        else if (ax == 4) v.y2 = val;
                        else if (ax == 5) v.z2 = val;
                        else if (ax == 6) v.x3 = val;
                        else if (ax == 7) v.y3 = val;
                        else if (ax == 8) v.z3 = val;
                        else if (ax == 0xA3 || ax == 0xA4 || ax == 0xA5) { }
                        else throw new NotSupportedException("Unknown ax=" + ax);
                        al[pos] = v;
                    }
                }
            }
            return al;
        }
    }

}
