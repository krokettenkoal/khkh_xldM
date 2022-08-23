using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using khkh_xldM;
using System.IO;
using System.Diagnostics;
using vcBinTex4;
using hex04BinTrack;
using System.Collections.Specialized;
using SlimDX;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using khiiMapv.Properties;
using System.Text.RegularExpressions;
using khiiMapv.Utils;
using khiiMapv.Models.PAX;
using khiiMapv.Models.HexView;
using khkh_xldMii.Models.Mdlx;
using khkh_xldMii.Models;
using khiiMapv.Models.Coct;
using khiiMapv.Models;
using System.Linq;

namespace khiiMapv {
    public partial class RDForm : Form {
        public RDForm() { InitializeComponent(); }
        public RDForm(String fpread) { this.fpread = fpread; InitializeComponent(); }

        String fpread;

        private void RDForm_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        byte[] fileBin = new byte[0];

        List<DContext> dcList = new List<DContext>();
        List<CollisionSet> collisionSets = new List<CollisionSet>();
        DoctReader doct = null;

        TreeNode AddNode(TreeNode node, string text, object tag) {
            var newNode = node.Nodes.Add(text);
            newNode.Tag = tag;
            return newNode;
        }

        void LoadMap(string fp) {
            linkLabel1.Text = fp;
            fileBin = File.ReadAllBytes(fp);
            hexViewer.SetBin(fileBin);

            treeView1.Nodes.Clear();
            errorList.Clear();

            dcList.Clear();

            var ext = Path.GetExtension(fp);
            TreeNode topNode = treeView1.Nodes.Add(ext);
            topNode.Tag = new Hexi(0);
            var stream = new MemoryStream(fileBin, false);
            switch (ext.ToLowerInvariant()) {
                case ".map":
                case ".mdlx":
                case ".apdx":
                case ".fm":
                case ".2dd":
                case ".bar":
                case ".2ld":
                case ".mset":

                case ".ard":
                case ".mag":
                    ParseBar(topNode, 0, stream);
                    break;
                case ".wd":
                    Parse20(topNode, 0, stream);
                    break;
                case ".vsb":
                    Parse22(topNode, 0, stream);
                    break;
                case ".pax":
                    Parse12(topNode, 0, new ArraySegment<byte>(fileBin));
                    break;
                case ".imd":
                    Parse18(topNode, 0, stream);
                    break;
                case ".imz":
                    Parse1D(topNode, 0, stream);
                    break;
                case ".modeltexture":
                    ParseTex(topNode, 0, stream);
                    break;
                case ".dpx":
                    Parse82(topNode, 0, new ArraySegment<byte>(fileBin));
                    break;
                case ".dpd":
                    Parse96(topNode, 0, new ArraySegment<byte>(fileBin));
                    break;
                case ".vas":
                    ParseVAGp(topNode, 0, stream, stereo: true);
                    break;
                case ".vag":
                    ParseVAGp(topNode, 0, stream, stereo: false);
                    break;
            }
        }

        void ParseBar(TreeNode topNode, int topOff, MemoryStream si) {
            DContext curDc = new DContext();
            collisionSets.Clear();
            doct = null;
            int prior = 0;
            foreach (ReadBar.Barent ent in ReadBar.Explode(si)) {
                TreeNode entryNode = topNode.Nodes.Add(string.Format("{0} ({1:x2})", ent.id, ent.k & 0xFF));
                entryNode.Tag = new Hexi(topOff + ent.off, ent.len);
                if (ent.k < prior) {
                    if ((curDc.o4Map != null || curDc.o4Mdlx != null) && curDc.o7 != null) {
                        dcList.Add(curDc);
                    }
                    curDc = new DContext();
                }
                prior = ent.k;
                try {
                    var entryStream = new MemoryStream(ent.bin, false);
                    switch (ent.k) {
                        case 0x24:
                            Parse24(entryNode, topOff + ent.off, ent);
                            break;
                        case 4:
                            Parse4(entryNode, topOff + ent.off, ent, curDc);
                            curDc.name = ent.id;
                            break;
                        case 5:
                            Parse5(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 6:
                            collisionSets.Add(new CollisionSet {
                                name = ent.id + "_" + ent.k,
                                collision = Parse6(entryNode, topOff + ent.off, ent)
                            });
                            break;
                        case 10:
                            Parse0a(entryNode, topOff + ent.off, ent);
                            break;
                        case 12:
                            Parse0c(entryNode, topOff + ent.off, ent);
                            break;
                        case 0x0B:
                        case 0x0F:
                        case 0x13:
                            collisionSets.Add(new CollisionSet {
                                name = ent.id + "_" + ent.k,
                                collision = Parse6(entryNode, topOff + ent.off, ent)
                            });
                            break;
                        case 7:
                            curDc.o7 = ParseTex(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 0x18:
                            Parse18(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 0x20:
                            Parse20(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 0x1D:
                            Parse1D(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 0x22:
                            Parse22(entryNode, topOff + ent.off, entryStream);
                            break;
                        case 0x12:
                            Parse12(entryNode, topOff + ent.off, new ArraySegment<byte>(ent.bin));
                            break;
                        case 3:
                            Parse03(entryNode, topOff + ent.off, ent);
                            break;
                        case 2: // text?
                            Parse02(entryNode, topOff + ent.off, ent);
                            break;

                        case 1:
                        case 0x11:
                        case 0x14:
                            ParseBar(entryNode, topOff + ent.off, entryStream);
                            break;

                    }
                }
                catch (NotSupportedException err) {
                    errorList[entryNode] = err;
                    entryNode.ImageKey = entryNode.SelectedImageKey = "RightsRestrictedHS.png";
                }
            }
            {
                if ((curDc.o4Map != null || curDc.o4Mdlx != null) && curDc.o7 != null) dcList.Add(curDc);
            }

            topNode.Expand();
        }

        SortedDictionary<int, string> dictObjName = new SortedDictionary<int, string>();

        String GetObjName(int modelId) {
            String s;
            if (dictObjName.TryGetValue(modelId, out s))
                return s;
            return null;
        }

        private void Parse0c(TreeNode tn, int xoff, ReadBar.Barent ent) {
            MemoryStream si = new MemoryStream(ent.bin, false);
            BinaryReader br = new BinaryReader(si);

            try {
                si.Position = 12;
                int ct1 = br.ReadUInt16();
                int ct2 = br.ReadUInt16();

                int nextoff = 0x34;

                StringWriter wr = new StringWriter();

                for (int x = 0; x < ct1; x++) {
                    si.Position = nextoff + 0x40 * x;

                    int modelId = br.ReadInt32();
                    float fx = br.ReadSingle();
                    float fy = br.ReadSingle();
                    float fz = br.ReadSingle();
                    float fw = br.ReadSingle();

                    wr.WriteLine("a.{0} {1:X4} ({2}, {3}, {4}, {5}) ; {6}", x, modelId, fx, fy, fz, fw, GetObjName(modelId) ?? "?");
                }

                nextoff += 0x40 * ct1;

                wr.WriteLine();

                for (int x = 0; x < ct2; x++) {
                    si.Position = nextoff + 0x40 * x;

                    int xId0 = br.ReadUInt16();
                    int xId1 = br.ReadUInt16();
                    float fx = br.ReadSingle();
                    float fy = br.ReadSingle();
                    float fz = br.ReadSingle();
                    float fw = br.ReadSingle();
                    float ex = br.ReadSingle();
                    float ey = br.ReadSingle();
                    float ez = br.ReadSingle();
                    float ew = br.ReadSingle();

                    wr.WriteLine("b.{0} {1:X4} {10:X} ({2}, {3}, {4}, {5}) ({6}, {7}, {8}, {9})", x, xId0, fx, fy, fz, fw, ex, ey, ez, ew, xId1);
                }

                nextoff += 0x40 * ct2;

                {
                    TreeNode tnx = tn.Nodes.Add("appear");
                    tnx.Tag = new Strif(xoff, wr.ToString());
                }
            }
            catch (EndOfStreamException err) {
                throw new NotSupportedException("EOF", err);
            }

        }

        private void Parse0a(TreeNode tn, int xoff, ReadBar.Barent ent) {
            MemoryStream si = new MemoryStream(ent.bin, false);

            ParseRADA p = new ParseRADA(si);
            p.Parse();
            {
                TreeNode tnt = tn.Nodes.Add("radar");
                IMGDi res = new IMGDi(xoff, p.pic);
                tnt.Tag = res;
            }
        }

        private void Parse03(TreeNode tn, int xoff, ReadBar.Barent ent) {
            try {
                StringWriter wr = new StringWriter();
                new ParseAI.Parse03(wr).Run(ent.bin);
                {
                    TreeNode tnt = tn.Nodes.Add("A.I. code");
                    tnt.Tag = new Strif(xoff, wr.ToString());
                }
            }
            catch (Exception err) {
                throw new NotSupportedException("Parser error.", err);
            }
        }

        private void Parse02(TreeNode tn, int xoff, ReadBar.Barent ent) {
            try {
                MemoryStream si = new MemoryStream(ent.bin, false);
                BinaryReader br = new BinaryReader(si);

                byte[] bin = ent.bin;

                int w0 = br.ReadInt32();
                if (w0 != 1) throw new InvalidDataException("w0 != 1");

                StringWriter wr = new StringWriter();

                int cnt = br.ReadInt32();

                khiiMapv.Parse02.StrDec dec = new khiiMapv.Parse02.StrDec();
                for (int x = 0; x < cnt; x++) {
                    si.Position = 8 + 8 * x;
                    int key = br.ReadInt32();
                    int off = br.ReadInt32();

                    si.Position = off;

                    wr.WriteLine("{0:x8} {1} --", key, off);

                    foreach (khiiMapv.Parse02.StrCode st in dec.DecodeEvt(bin, off))
                        foreach (byte b in st.bin)
                            wr.Write("{0:x2} ", b);
                    wr.WriteLine();
                    wr.WriteLine(dec.DecodeEvt(bin, off));
                    wr.WriteLine();
                }

                {
                    TreeNode tnt = tn.Nodes.Add("String table");
                    tnt.Tag = new Strif(xoff, wr.ToString());
                }
            }
            catch (Exception err) {
                throw new NotSupportedException("Parser error.", err);
            }
        }

        private void Parse24(TreeNode tn, int xoff, ReadBar.Barent ent) {
            if (ent.id == "evt") {
                int pich = ent.bin.Length / 256;
                Bitmap pic = new Bitmap(256, 2 * pich);
                byte[] bin = ent.bin;
                for (int vy = 0; vy < pich; vy++) {
                    int y = (vy & 511) + 2 * (vy & (~511));
                    // 11001100
                    for (int x = 0; x < 256; x++) {
                        int b = bin[x + 256 * vy];
                        b = ((b & 0x30) << 2) | ((b & 0x03) << 4);
                        pic.SetPixel(x, y, Color.FromArgb(b, b, b));
                    }
                }
                for (int vy = 0; vy < pich; vy++) {
                    int y = (vy & 511) + 2 * (vy & (~511)) + 512;

                    for (int x = 0; x < 256; x++) {
                        int b = bin[x + 256 * vy];
                        b = ((b & 0xC0) << 0) | ((b & 0x0C) << 2);
                        pic.SetPixel(x, y, Color.FromArgb(b, b, b));
                    }
                }

                TreeNode tnp = tn.Nodes.Add("font");
                tnp.Tag = new IMGDi(xoff, pic);
            }
            else if (ent.id == "sys") {
                int pich = ent.bin.Length / 256;
                Bitmap pic = new Bitmap(256, 2 * pich);
                byte[] bin = ent.bin;
                for (int vy = 0; vy < pich; vy++) {
                    int y = vy;
                    // 11001100
                    for (int x = 0; x < 256; x++) {
                        int b = bin[x + 256 * vy];
                        b = ((b & 0x30) << 2) | ((b & 0x03) << 4);
                        pic.SetPixel(x, y, Color.FromArgb(b, b, b));
                    }
                }
                for (int vy = 0; vy < pich; vy++) {
                    int y = vy + pich;

                    for (int x = 0; x < 256; x++) {
                        int b = bin[x + 256 * vy];
                        b = ((b & 0xC0) << 0) | ((b & 0x0C) << 2);
                        pic.SetPixel(x, y, Color.FromArgb(b, b, b));
                    }
                }

                TreeNode tnp = tn.Nodes.Add("font");
                tnp.Tag = new IMGDi(xoff, pic);
            }
            else if (ent.id == "icon") {
                Bitmap pic = new Bitmap(256, 160, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                byte[] bin = ent.bin;
                BitmapData bd = pic.LockBits(new Rectangle(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try {
                    Marshal.Copy(bin, 0, bd.Scan0, 256 * 160);
                }
                finally {
                    pic.UnlockBits(bd);
                }
                ColorPalette pal = pic.Palette;
                byte[] palbin = new byte[8192];
                Buffer.BlockCopy(bin, 256 * 160, palbin, 0, 1024);
                //palbin = Reform32.Encode32(palbin, 1, 1);
                Color[] alc = pal.Entries;
                for (int x = 0; x < 256; x++) {
                    int srcIndex = x;
                    int dstIndex = x;//vwBinTex2.KHcv8pal.repl(x)
                    int paloff = 4 * srcIndex;
                    alc[dstIndex] = Color.FromArgb(
                        Math.Min(255, palbin[paloff + 3] * 2),
                        palbin[paloff + 0],
                        palbin[paloff + 1],
                        palbin[paloff + 2]
                        );
                }
                pic.Palette = pal;

                TreeNode tnp = tn.Nodes.Add("font");
                tnp.Tag = new IMGDi(xoff, pic);
            }
        }

        private DoctReader Parse5(TreeNode tn, int xoff, Stream si) {
            BinaryReader br = new BinaryReader(si);

            doct = new DoctReader(si);
            StringWriter writer = new StringWriter();

            writer.WriteLine("## Table1");
            foreach (var pair in doct.list1.Select((it, index) => new { it, index })) {
                writer.WriteLine($"{pair.index,3}:{pair.it}");
            }

            writer.WriteLine();

            writer.WriteLine("## Table2");
            foreach (var pair in doct.list2.Select((it, index) => new { it, index })) {
                writer.WriteLine($"{pair.index,3}:{pair.it}");
            }

            TreeNode tnt = tn.Nodes.Add("DOCT");
            tnt.Tag = new Strif(xoff, writer.ToString());

            return doct;
        }

        private CollisionReader Parse6(TreeNode tn, int xoff, ReadBar.Barent ent) {
            MemoryStream si = new MemoryStream(ent.bin, false);
            BinaryReader br = new BinaryReader(si);

            CollisionReader coll = new CollisionReader();
            coll.Read(si);
            StringWriter wr = new StringWriter();
            {
                {
                    int t = 0;
                    foreach (Co1 o in coll.alCo1) { wr.WriteLine("Co1[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (Co2 o in coll.alCo2) { wr.WriteLine("Co2[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (Co3 o in coll.alCo3) { wr.WriteLine("Co3[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (Vector4 o in coll.alCo4) { wr.WriteLine("Co4[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (Plane o in coll.alCo5) { wr.WriteLine("Co5[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (var o in coll.alCo6) { wr.WriteLine("Co6[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
                {
                    int t = 0;
                    foreach (var o in coll.alCo7) { wr.WriteLine("Co7[{0,3}] {1}", t, o); t++; }
                    wr.WriteLine("--");
                }
            }

            TreeNode tnt = tn.Nodes.Add("collision");
            tnt.Tag = new Strif(xoff, wr.ToString());

            return coll;
        }

        ListDictionary errorList = new ListDictionary(); // [TreeNode, Exception]

        const int BASE = 0x90;

        private void Parse4(TreeNode tn, int xoff, ReadBar.Barent ent, DContext curdc) {
            MemoryStream si = new MemoryStream(ent.bin, false);
            BinaryReader br = new BinaryReader(si);
            si.Position = BASE + 0x00;
            int v90 = br.ReadInt32();
            if (v90 == 2) {
                curdc.o4Map = Parse4_02(tn, xoff, ent);
                return;
            }
            if (v90 == 3) {
                curdc.o4Mdlx = Parse4_03(tn, xoff, ent);
                return;
            }
            throw new NotSupportedException("Unknown @90 .. " + v90);
        }

        private Parse4Mdlx Parse4_03(TreeNode tn, int xoff, ReadBar.Barent ent) {
            Parse4Mdlx res = new Parse4Mdlx(ent.bin);

            foreach (K2Model t31 in res.mdlx.alt31) {
                TreeNode tn31 = tn.Nodes.Add("Mdlx.T31");
                tn31.Tag = new Hexi(xoff + t31.off, t31.len);

                if (t31.boneList != null) {
                    TreeNode tnaxb = tn31.Nodes.Add("T21.alaxb");
                    StringWriter wr = new StringWriter();
                    wr.WriteLine("alaxb = Array of joints");
                    wr.WriteLine();
                    wr.WriteLine("current-joint-index,parent-joint-index");
                    wr.WriteLine(" scale x,y,z,w");
                    wr.WriteLine(" rotate x,y,z,w");
                    wr.WriteLine(" translate x,y,z,w");
                    wr.WriteLine();
                    foreach (AxBone axb in t31.boneList.bones) {
                        wr.WriteLine("{0},{1}", axb.cur, axb.parent);
                        wr.WriteLine(" {0},{1},{2},{3}", axb.x1, axb.y1, axb.z1, axb.w1);
                        wr.WriteLine(" {0},{1},{2},{3}", axb.x2, axb.y2, axb.z2, axb.w2);
                        wr.WriteLine(" {0},{1},{2},{3}", axb.x3, axb.y3, axb.z3, axb.w3);
                    }
                    tnaxb.Tag = new Strif(xoff + t31.boneList.off, wr.ToString());
                }

                int vifi = 0;
                foreach (K2Vif vif in t31.vifList) {
                    // Mdlx
                    TreeNode tnvif = tn31.Nodes.Add(String.Format("vifpkt{0} ({1},{2})({3},tp{4})({6},{5:X8})"
                        , vifi++
                        , vif.textureIndex & 0xffff
                        , vif.textureIndex >> 16
                        , vif.v8 & 0xffff
                        , vif.v8 >> 16
                        , vif.vc
                        , (0 != (2 & vif.vc)) ? $"UV{(vif.vc >> 2) & 15}" : ""
                    ));
                    tnvif.Tag = new Vifi(xoff + vif.off, vif.bin);
                }
            }
            return res;
        }

        private M4 Parse4_02(TreeNode tn, int xoff, ReadBar.Barent ent) {
            MemoryStream si = new MemoryStream(ent.bin, false);
            BinaryReader br = new BinaryReader(si);
            si.Position = BASE + 0x00;
            int v90 = br.ReadInt32();
            if (v90 != 2) throw new NotSupportedException("@90 != 2");
            si.Position = BASE + 0x10;
            int va0 = br.ReadInt32(); // va0: cnt-b1
            int va4 = br.ReadUInt16(); // va4: ?
            int va6 = br.ReadUInt16(); // va6: cnt-b2
            int va8 = br.ReadInt32(); // va8: off-b2
            int vac = br.ReadInt32(); // vac: off-b1t2

            List<int[]> alb2 = new List<int[]>();
            for (int i = 0; i < va6; i++) {
                si.Position = BASE + va8 + 4 * i;
                int offb2t2 = br.ReadInt32();

                si.Position = BASE + offb2t2;
                List<int> alb2t2 = new List<int>();
                while (true) {
                    int v = br.ReadUInt16();
                    if (v == 0xFFFF) break;
                    alb2t2.Add(v);
                }
                alb2.Add(alb2t2.ToArray());
            }

            List<int> alb1t2 = new List<int>();
            si.Position = BASE + vac;
            for (int i = 0; i < 1; i++) {
                int offb1t2t2 = br.ReadInt32();
                si.Position = BASE + offb1t2t2;
                for (int j = 0; j < va0; j++) {
                    alb1t2.Add(br.ReadUInt16());
                }
            }

            List<Vifpli> alvifpkt = new List<Vifpli>();
            for (int i = 0; i < va0; i++) {
                si.Position = BASE + 0x20 + 0x10 * i;
                int vifoff = br.ReadInt32(); // vifptr +@0
                int texi = br.ReadInt32(); // vifptr +@4
                int v8 = br.ReadInt32(); // vifptr +@8
                int vc = br.ReadInt32(); // vifptr +@12

                MemoryStream os = new MemoryStream();

                while (true) {
                    si.Position = BASE + vifoff;
                    int vp00 = br.ReadInt32();
                    int qwc = vp00 & 0xFFFF;
                    br.ReadInt32();
                    byte[] vifpkta = br.ReadBytes(8 + 16 * qwc);
                    os.Write(vifpkta, 0, vifpkta.Length);

                    if ((vp00 >> 28) == 6) // ID:ret
                        break;
                    vifoff += 16 + 16 * qwc;
                }
                byte[] vifpkt = os.ToArray();

                alvifpkt.Add(new Vifpli(vifpkt, texi));

                // MAP
                TreeNode tnt = tn.Nodes.Add(String.Format("vifpkt{0} ({1},{2})({3},tp{4})({6},{5:X8})"
                    , i
                    , texi & 0xffff
                    , texi >> 16
                    , v8 & 0xffff
                    , v8 >> 16
                    , vc
                    , (0 != (2 & vc)) ? $"UV{(vc >> 2) & 15}" : ""
                ));
                var labels = new MicroLabels();
                labels.AddLabel("vifpkt", xoff + BASE + vifoff);
                tnt.Tag = new Vifi(xoff + BASE + vifoff, labels, vifpkt);
            }

#if false
            {
                StringWriter wr = new StringWriter();

                {
                    wr.WriteLine("alb1t2 (List<int>). alb1t2 is an array of indirect index to vifpkt");
                    wr.WriteLine();
                    int i = 0;
                    wr.Write("\t" + "alb1t2 = [");
                    foreach (int v in alb1t2) {
                        if (i != 0)
                            wr.Write(", ");
                        wr.Write(v);
                        i++;
                    }
                    wr.Write("...");
                    wr.Write("]");
                    wr.WriteLine();
                }

                wr.WriteLine();

                {
                    wr.WriteLine("alb2 (List<int[]>). alb2 is a group of indexes to vifpkt indir index (alb1t2).");
                    wr.WriteLine();
                    int i = 0;
                    foreach (int[] alv in alb2) {
                        wr.Write("\t" + "alb2[{0}] = [", i);
                        int j = 0;
                        foreach (int v in alv) {
                            if (j != 0)
                                wr.Write(", ");
                            wr.Write(v);
                            j++;
                        }
                        wr.Write("]");
                        wr.WriteLine();
                        i++;
                    }
                }

                wr.WriteLine();

                tn.Tag = new Stri(xoff + BASE, wr.ToString());
            }
#endif
            {
                TreeNode tnt = tn.Nodes.Add("alb2");
                tnt.Tag = new Stri(xoff + BASE,
                    string.Join(
                        "\r\n",
                        alb2.Select(
                            (array, index) => $"{index,4}: " + string.Join(",", array.Select(it => $"{it,4}"))
                        )
                    )
                );
            }

            M4 res = new M4();
            res.alb1t2 = alb1t2;
            res.alb2 = alb2;
            res.alvifpkt = alvifpkt;
            return res;
        }

        class TexSimulator {
            public byte[] gs = new byte[1024 * 1024 * 4];
            public int t0PSM;
            public int offBin;
            public GSRegsRecorder recorder = new GSRegsRecorder();

            public void RunGSCase1(Stream si, string mode) {
                BinaryReader br = new BinaryReader(si);
                UInt64 cm;

                {
                    var pos = si.Position;
                    recorder.Walk(br, 4, $"{mode}:Case1");
                    si.Position = pos;
                }

                UInt64 r50 = br.ReadUInt64(); cm = br.ReadUInt64(); Debug.Assert(0x50 == cm, cm.ToString("X16") + " ≠ 0x50"); // 0x50 BITBLTBUF
                int SBP = ((int)(r50)) & 0x3FFF;
                int SBW = ((int)(r50 >> 16)) & 0x3F;
                int SPSM = ((int)(r50 >> 24)) & 0x3F;
                int DBP = ((int)(r50 >> 32)) & 0x3FFF;
                int DBW = ((int)(r50 >> 48)) & 0x3F;
                int DPSM = ((int)(r50 >> 56)) & 0x3F;
                Trace.Assert(SBP == 0);
                Trace.Assert(SBW == 0);
                Trace.Assert(SPSM == 0);
                Trace.Assert(DPSM == 0 || DPSM == 19 || DPSM == 20);

                UInt64 r51 = br.ReadUInt64(); cm = br.ReadUInt64(); Debug.Assert(0x51 == cm, cm.ToString("X16") + " ≠ 0x51"); // 0x51 TRXPOS
                int SSAX = ((int)(r51)) & 0x7FF;
                int SSAY = ((int)(r51 >> 16)) & 0x7FF;
                int DSAX = ((int)(r51 >> 32)) & 0x7FF;
                int DSAY = ((int)(r51 >> 48)) & 0x7FF;
                int DIR = ((int)(r51 >> 59)) & 3;
                Trace.Assert(SSAX == 0);
                Trace.Assert(SSAY == 0); //!
                Trace.Assert(DSAX == 0); //!
                Trace.Assert(DSAY == 0); //!
                Trace.Assert(DIR == 0); //!

                UInt64 r52 = br.ReadUInt64(); cm = br.ReadUInt64(); Debug.Assert(0x52 == cm, cm.ToString("X16") + " ≠ 0x52"); // 0x52 TRXREG
                int RRW = ((int)(r52 >> 0)) & 0xFFF;
                int RRH = ((int)(r52 >> 32)) & 0xFFF;

                UInt64 r53 = br.ReadUInt64(); cm = br.ReadUInt64(); Debug.Assert(0x53 == cm, cm.ToString("X16") + " ≠ 0x53"); // 0x53 TRXDIR
                int XDIR = ((int)(r53)) & 2;
                Trace.Assert(XDIR == 0);

                int eop = br.ReadUInt16();
                Trace.Assert(8 != (eop & 0x8000));

                si.Position += 18;
                offBin = br.ReadInt32();

                int cbTex = (eop & 0x7FFF) << 4;
                int MyDBH = (cbTex + 8192 - 1) / 8192 / DBW;

                {
                    byte[] binTmp = new byte[Math.Max(8192 * MyDBH, cbTex)]; // decoder needs at least 8kb
                    si.Position = offBin;
                    si.Read(binTmp, 0, cbTex);
                    byte[] binDec;
                    if (DPSM == 0) {
                        binDec = Reform32.Encode32(binTmp, DBW, MyDBH);
                    }
                    else if (DPSM == 19) {
                        binDec = Reform8.Encode8(binTmp, DBW / 2, cbTex / 8192 / (DBW / 2));
                    }
                    else if (DPSM == 20) {
                        binDec = Reform4.Encode4(binTmp, DBW / 2, cbTex / 8192 / Math.Max(1, DBW / 2));
                    }
                    else {
                        throw new NotSupportedException("DPSM = " + DPSM + "?");
                    }
                    Array.Copy(binDec, 0, gs, 256 * DBP, cbTex);
                }

                //Debug.WriteLine(string.Format("# p1 {0:x4}      {1,6} {2}", DBP, cbTex, DPSM));
            }

            public STim RunGSCase2(Stream si) {
                BinaryReader br = new BinaryReader(si);
                UInt64 command;

                {
                    var pos = si.Position;
                    recorder.Walk(br, 6, "Case2");
                    si.Position = pos;
                }

                UInt64 r3f = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x3F); // 0x3F TEXFLUSH
                UInt64 r34 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x34); // 0x34 MIPTBP1_1
                UInt64 r36 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x36); // 0x36 MIPTBP2_1
                UInt64 r16 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x16); // 0x16 TEX2_1
                int t2PSM = ((int)(r16 >> 20)) & 0x3F;
                int t2CBP = ((int)(r16 >> 37)) & 0x3FFF;
                int t2CPSM = ((int)(r16 >> 51)) & 0xF;
                int t2CSM = ((int)(r16 >> 55)) & 0x1;
                int t2CSA = ((int)(r16 >> 56)) & 0x1F;
                int t2CLD = ((int)(r16 >> 61)) & 0x7;
                Trace.Assert(t2PSM == 19); // PSMT8
                Trace.Assert(t2CPSM == 0); // PSMCT32
                Trace.Assert(t2CSM == 0); // CSM1
                Trace.Assert(t2CSA == 0);
                Trace.Assert(t2CLD == 4);

                UInt64 r14 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x14);// 0x14 TEX1_1
                int t1LCM = ((int)(r14 >> 0)) & 1;
                int t1MXL = ((int)(r14 >> 2)) & 7;
                int t1MMAG = ((int)(r14 >> 5)) & 1;
                int t1MMIN = ((int)(r14 >> 6)) & 7;
                int t1MTBA = ((int)(r14 >> 9)) & 1;
                int t1L = ((int)(r14 >> 19)) & 3;
                int t1K = ((int)(r14 >> 32)) & 0xFFF;

                UInt64 r06 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x06);// 0x06 TEX0_1
                int t0TBP0 = ((int)(r06 >> 0)) & 0x3FFF;
                int t0TBW = ((int)(r06 >> 14)) & 0x3F;
                t0PSM = ((int)(r06 >> 20)) & 0x3F;
                int t0TW = ((int)(r06 >> 26)) & 0xF;
                int t0TH = ((int)(r06 >> 30)) & 0xF;
                int t0TCC = ((int)(r06 >> 34)) & 0x1;
                int t0TFX = ((int)(r06 >> 35)) & 0x3;
                int t0CBP = ((int)(r06 >> 37)) & 0x3FFF;
                int t0CPSM = ((int)(r06 >> 51)) & 0xF;
                int t0CSM = ((int)(r06 >> 55)) & 0x1;
                int t0CSA = ((int)(r06 >> 56)) & 0x1F;
                int t0CLD = ((int)(r06 >> 61)) & 0x7;
                Trace.Assert(t0PSM == 19 || t0PSM == 20);
                Trace.Assert(t0TCC == 1);
                Trace.Assert(t0CPSM == 0);
                Trace.Assert(t0CSM == 0);
                //Trace.Assert(t0CSA == 0);
                Trace.Assert(t0CLD == 0);

                UInt64 r08 = br.ReadUInt64(); Trace.Assert((command = br.ReadUInt64()) == 0x08);// 0x08 CLAMP_1
                int c1WMS = ((int)(r08 >> 0)) & 0x3;
                int c1WMT = ((int)(r08 >> 2)) & 0x3;
                int c1MINU = ((int)(r08 >> 4)) & 0x3FF;
                int c1MAXU = ((int)(r08 >> 14)) & 0x3FF;
                int c1MINV = ((int)(r08 >> 24)) & 0x3FF;
                int c1MAXV = ((int)(r08 >> 34)) & 0x3FF;

                int sizetbp0 = (1 << t0TW) * (1 << t0TH);
                byte[] buftbp0 = new byte[Math.Max(8192, sizetbp0)]; // needs at least 8kb
                Array.Copy(gs, 256 * t0TBP0, buftbp0, 0, Math.Min(gs.Length - 256 * t0TBP0, Math.Min(buftbp0.Length, sizetbp0)));
                byte[] bufcbpX = new byte[8192];
                Array.Copy(gs, 256 * t0CBP, bufcbpX, 0, bufcbpX.Length);

                //Debug.WriteLine(string.Format("# p2 {0:x4} {1:x4} {2,2}", t0TBP0, t0CBP, t0CSA));

                STim st = null;
                if (t0PSM == 0x13) st = TexUt2.Decode8(buftbp0, bufcbpX, t0TBW, 1 << t0TW, 1 << t0TH);
                if (t0PSM == 0x14) st = TexUt2.Decode4Ps(buftbp0, bufcbpX, t0TBW, 1 << t0TW, 1 << t0TH, t0CSA);
                if (st != null) {
                    st.tfx = (TFX)t0TFX;
                    st.tcc = (TCC)t0TCC;
                    st.wms = (WM)c1WMS;
                    st.wmt = (WM)c1WMT;
                    st.minu = c1MINU;
                    st.maxu = c1MAXU;
                    st.minv = c1MINV;
                    st.maxv = c1MAXV;
                }
                return st;
            }
        }

        private MTex ParseTex(TreeNode tn, int xoff, Stream si) {
            BinaryReader br = new BinaryReader(si);

            int v00 = br.ReadInt32();
            if (v00 == 0) {
                // TIMf
                si.Position = 0;
                return ParseTex_TIMf(tn, xoff, si, br);
            }
            else if (v00 == -1) {
                // TIMc .. TIMformat collection
                int v04 = br.ReadInt32();

                List<int> aloff = new List<int>();
                for (int x = 0; x < v04; x++)
                    aloff.Add(br.ReadInt32());

                aloff.Add((int)si.Length);

                List<MTex> alres = new List<MTex>();

                for (int x = 0; x < v04; x++) {
                    si.Position = aloff[x];
                    byte[] bin = br.ReadBytes(aloff[x + 1] - aloff[x]);

                    TreeNode tnf = tn.Nodes.Add("TIMc" + x);

                    MemoryStream siF = new MemoryStream(bin, false);
                    BinaryReader brF = new BinaryReader(siF);

                    alres.Add(ParseTex_TIMf(tnf, xoff + aloff[x], siF, brF));
                }
                return alres.Count != 0 ? alres[0] : null;
            }
            else throw new NotSupportedException("Unknown v00 .. " + v00);
        }

        private MTex ParseTex_TIMf(TreeNode tn, int xoff, Stream si, BinaryReader br) {
            int v00 = br.ReadInt32();
            int v04 = br.ReadInt32();
            int v08 = br.ReadInt32(); // v08: cnt-pal1? each-size=0x90
            int v0c = br.ReadInt32(); // v0c: cnt-pal2? each-size=0xA0
            int v10 = br.ReadInt32(); // v10: off-pal2-tbl
            int v14 = br.ReadInt32(); // v14: off-pal1
            int v18 = br.ReadInt32(); // v18: off-pal2
            int v1c = br.ReadInt32(); // v1c: off-tex1?
            int v20 = br.ReadInt32(); // v20: off-tex2?

            SortedDictionary<int, int> map2to1 = new SortedDictionary<int, int>();

            si.Position = v10;
            for (int p1 = 0; p1 < v0c; p1++) {
                map2to1[p1] = br.ReadByte();
            }

            TexSimulator tc = new TexSimulator();

            List<int> offPalfrom1 = new List<int>();

            for (int p1 = 0; p1 < v08 + 1; p1++) {
                //int offp1 = v14 + 0x90 * p1;
                //si.Position = offp1 + 0x20;
                //tc.Do1(si);
                //int offPal = tc.offTex;
                //offPalfrom1.Add(offPal);
            }

            List<Bitmap> pics = new List<Bitmap>();
            for (int p2 = 0; p2 < v0c; p2++) {
                tc.recorder.AddRecord();

                int offp1pal = v14;
                si.Position = offp1pal + 0x20;
                tc.RunGSCase1(si, "PAL");
                int offPal = tc.offBin;

                int offp1tex = v14 + 0x90 * (1 + map2to1[p2]);
                si.Position = offp1tex + 0x20;
                tc.RunGSCase1(si, "TEX");
                int offTex = tc.offBin;

                int offp2 = v18 + 0xA0 * p2 + 0x20;
                si.Position = offp2;
                STim st = tc.RunGSCase2(si);

                st.offPal = offPal;
                st.offTex = offTex;

                {
                    var labels = new MicroLabels();
                    labels.AddLabel("offp1pal", xoff + offp1pal);
                    labels.AddLabel("offp1tex", xoff + offp1tex);
                    labels.AddLabel("offp2", xoff + offp2);
                    labels.AddLabel("offTex", xoff + offTex);
                    labels.AddLabel("offPal", xoff + offPal);
                    TreeNode tnt = tn.Nodes.Add(string.Format("tex{0} ({1})", p2, tc.t0PSM));
                    tnt.Tag = new Texi(xoff + offTex, labels, st);
                }
                pics.Add(st.Generate());
            }


            {
                var writer = new StringWriter();

                var records = tc.recorder.records;
                if (records.Any()) {
                    var csv = new Csvw(writer);
                    foreach (var key in records[0].Keys) {
                        csv.Write(key);
                    }

                    foreach (var record in records) {
                        csv.NextRecord();
                        foreach (var key in records[0].Keys) {
                            if (record.TryGetValue(key, out int value)) {
                                csv.Write(value.ToString());
                            }
                            else {
                                csv.Write("");
                            }
                        }
                    }

                }

                TreeNode tnt = tn.Nodes.Add("GS records csv");
                tnt.Tag = new Strif(xoff, writer.ToString());
            }

            return new MTex(pics);
        }

        class Texi : Hexi {
            public STim st;

            public Texi(int off, STim st)
                : base(off) {
                this.st = st;
            }
            public Texi(int off, MicroLabels mi, STim st)
                : base(off, mi) {
                this.st = st;
            }
        }

        class Vifi : Hexi {
            public byte[] vifpkt;

            public Vifi(int off, byte[] vifpkt)
                : base(off) {
                this.vifpkt = vifpkt;
            }
            public Vifi(int off, MicroLabels mi, byte[] vifpkt)
                : base(off, mi) {
                this.vifpkt = vifpkt;
            }
        }

        class Btni : Hexi {
            public EventHandler onClicked;

            public Btni(int off, EventHandler onClicked)
                : base(off) {
                this.onClicked = onClicked;
            }
        }

        class IMGDi : Hexi {
            public Bitmap bitmap;

            public IMGDi(int off, Bitmap bitmap, MicroLabels labels = null)
                : base(off) {
                this.bitmap = bitmap;
                this.microLabels = labels;
            }
        }

        class WAVi : Hexi {
            public Wavo w;

            public WAVi(int off, Wavo w)
                : base(off) {
                this.off = off;
                this.w = w;
            }
        }

        class Stri : Hexi {
            public String s;

            public Stri(int off, String s)
                : base(off) {
                this.s = s;
            }
        }

        class Strif : Hexi {
            public String s;

            public Strif(int off, String s)
                : base(off) {
                this.s = s;
            }
        }

        class Hexi {
            public int off, len;
            public MicroLabels microLabels = null;

            public Hexi(int off) {
                this.off = off;
                this.len = 0;
            }
            public Hexi(int off, int len) {
                this.off = off;
                this.len = len;
            }
            public Hexi(int off, MicroLabels microLabels) {
                this.off = off;
                this.microLabels = microLabels;
            }
            public Hexi(int off, int len, MicroLabels microLabels) {
                this.off = off;
                this.len = len;
                this.microLabels = microLabels;
            }
        }

        string fpld = null;

        private void RDForm_DragDrop(object sender, DragEventArgs e) {
            string[] fs = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (fs != null) {
                if (fs.Length >= 2) {
                    switch (MessageBox.Show(this, "Would you try batch export instead of viewer?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation)) {
                        case DialogResult.Yes:
                            BEXForm form = new BEXForm(this);
                            foreach (string fp in fs)
                                form.Addfp(fp);
                            form.Show();
                            return;
                        case DialogResult.No:
                            break;
                        default:
                            return;
                    }
                }
                foreach (string fp in fs) {
                    switch (Path.GetExtension(fp).ToLowerInvariant()) {
                        // * Currently accepts map mdlx apdx fm 2dd bar 2ld mset pax wd vsb ard imd mag
                        case ".map":
                        case ".mdlx":
                        case ".apdx":
                        case ".fm":
                        case ".2dd":
                        case ".bar":
                        case ".2ld":
                        case ".mset":
                        case ".pax":
                        case ".wd":
                        case ".vsb":

                        case ".ard":
                        case ".imd":
                        case ".imz":
                        case ".modeltexture":
                        case ".mag":

                        case ".dpd":
                        case ".dpx":

                        case ".vas":
                        case ".vag":
                            fpld = fp;
                            Application.Idle += new EventHandler(Application_Idle);
                            break;
                    }
                }
            }
        }

        void Application_Idle(object sender, EventArgs e) {
            if (fpld != null) {
                Application.Idle -= new EventHandler(Application_Idle);
                LoadMap((fpld));
                fpld = null;
            }
        }

        public void LoadAny(string fpld) {
            LoadMap((fpld));
        }

        class WUt {
            public static string Usebar(string fpld, int kid, string nid) {
                Directory.CreateDirectory("tmp");
                string fpto = Path.GetFullPath("tmp\\" + Path.GetFileName(fpld) + ".bar");
                using (FileStream fs = File.Create(fpto)) {
                    BinaryWriter wr = new BinaryWriter(fs, Encoding.ASCII);
                    wr.Write(Encoding.ASCII.GetBytes("BAR\x1"));
                    wr.Write((int)1);
                    wr.Write((int)0);
                    wr.Write((int)0);

                    wr.Write(kid);
                    wr.Write(Encoding.ASCII.GetBytes(nid.PadRight(4, '\0').Substring(0, 4)));
                    wr.Write((int)0x20);
                    using (FileStream si = File.OpenRead(fpld)) {
                        wr.Write((int)si.Length);

                        wr.Write(new BinaryReader(si).ReadBytes((int)si.Length));
                    }
                }
                return fpto;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e) { }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            switch (e.Action) {
                case TreeViewAction.ByKeyboard:
                case TreeViewAction.ByMouse:
                    TreeNode node = e.Node;
                    if (true) {
                        bErrorPop.Hide();
                        Exception err = (Exception)errorList[node];
                        if (err != null) {
                            bErrorPop.Show();
                            bErrorPop.Tag = err;
                        }
                    }
                    if (node != null) {
                        Hexi it = node.Tag as Hexi;
                        if (it != null) {
                            hexViewer.RangeMarkedList.Clear();
                            if (it.off >= 0) {
                                hexViewer.SetPos(it.off);
                                if (it.len > 0) {
                                    hexViewer.AddRangeMarked(it.off, it.len, Color.FromArgb(50, Color.LimeGreen), Color.FromArgb(200, Color.LimeGreen));
                                }
                            }
                            listViewMicroLabels.Items.Clear();
                            if (it.microLabels != null) {
                                foreach (var pair in it.microLabels.labelToOffset) {
                                    ListViewItem lvi = listViewMicroLabels.Items.Add(pair.Key);
                                    lvi.Tag = pair.Value;
                                }
                            }
                        }
                        Texi texi = it as Texi;
                        if (texi != null) {
                            setPicPane(new Image[] { texi.st.pic, texi.st.Generate() });
                            var bitmapInfo = $"{texi.st.pic.Width,3} x {texi.st.pic.Height,3} ({texi.st.pic.PixelFormat})";
                            labelHelpMore.Text = String.Format("tfx({0,-10}) tcc({1,-4}) wms({2,-7}) wmt({3,-7})  {4}\n", texi.st.tfx, texi.st.tcc, texi.st.wms, texi.st.wmt, bitmapInfo)
                                + ((texi.st.wms == WM.RClamp) ? String.Format("Horz-clamp({0,4},{1,4}) ", texi.st.minu, texi.st.maxu) : "")
                                + ((texi.st.wms == WM.RRepeat) ? String.Format("UMSK({0,4}) UFIX({1,4}) ", texi.st.minu, texi.st.maxu) : "")
                                + ((texi.st.wmt == WM.RClamp) ? String.Format("Vert-clamp({0,4},{1,4}) ", texi.st.minv, texi.st.maxv) : "")
                                + ((texi.st.wmt == WM.RRepeat) ? String.Format("VMSK({0,4}) VFIX({1,4}) ", texi.st.minv, texi.st.maxv) : "")
                                + texi.st.wmt
                            ;
                        }
                        Vifi vifi = it as Vifi;
                        if (vifi != null) {
                            string text = Parseivif.Parse(vifi.vifpkt);
                            setTextPane(text, false);
                        }
                        IMGDi di = it as IMGDi;
                        if (di != null) {
                            setPicPane(di.bitmap);
                        }
                        WAVi wi = it as WAVi;
                        if (wi != null) {
                            if (WavePlayer != null)
                                WavePlayer(wi);
                            else {
                                Directory.CreateDirectory("tmp");
                                String fp = "tmp\\" + "_" + wi.w.fn;
                                File.WriteAllBytes(fp, wi.w.bin);
                                Process.Start(fp);
                            }
                        }
                        Stri stri = it as Stri;
                        if (stri != null) {
                            setTextPane(stri.s, false);
                        }
                        Strif strif = it as Strif;
                        if (strif != null) {
                            setTextPane(strif.s, true);
                        }
                        Btni btni = it as Btni;
                        if (btni != null) {
                            while (p1.Controls.Count != 0) p1.Controls[0].Dispose();

                            Button b = new Button();
                            b.AutoSize = true;
                            b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                            b.Text = "Click";
                            b.Click += btni.onClicked;
                            p1.Controls.Add(b);
                        }
                    }
                    break;
            }
        }

        delegate void WavePlayerDelegate(WAVi wi);

        WavePlayerDelegate WavePlayer = null;

        class Parseivif {
            public static string Parse(byte[] bin04) {
                MemoryStream si = new MemoryStream(bin04, false);
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
                return s.ToString();
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
        }

        private void setTextPane(string text, bool fixedfont) {
            while (p1.Controls.Count != 0) p1.Controls[0].Dispose();

            TextBox tb = new TextBox();
            tb.Multiline = true;
            tb.ScrollBars = ScrollBars.Vertical;
            tb.BorderStyle = BorderStyle.None;
            tb.Dock = DockStyle.Fill;
            tb.ReadOnly = true;
            tb.Visible = true;
            tb.Parent = p1;

            if (fixedfont) {
                tb.Font = new Font(FontFamily.GenericMonospace, tb.Font.SizeInPoints);
                tb.WordWrap = false;
                tb.ScrollBars = ScrollBars.Both;
            }

            tb.Text = text;

            labelHelpMore.Text = "";
        }
        void setPicPane(Image pic) {
            while (p1.Controls.Count != 0) p1.Controls[0].Dispose();

            PictureBox pb = new PictureBox();
            pb.Parent = p1;
            pb.Image = pic;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.Visible = true;
            pb.MouseMove += new MouseEventHandler(pb_MouseMove);

            labelHelpMore.Text = "";
        }

        void setPicPane(Image[] pics) {
            while (p1.Controls.Count != 0) p1.Controls[0].Dispose();

            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Parent = p1;
            flp.Dock = DockStyle.Fill;
            foreach (Image pic in pics) {
                PictureBox pb = new PictureBox();
                pb.Image = pic;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.Visible = true;
                pb.MouseMove += new MouseEventHandler(pb_MouseMove);
                flp.Controls.Add(pb);
            }

            labelHelpMore.Text = "";
        }

        void pb_MouseMove(object sender, MouseEventArgs e) {
            PictureBox pb = (PictureBox)sender;
            Bitmap pic = (Bitmap)pb.Image;
            if (e.X < pic.Width && e.Y < pic.Height) {
                Color c = pic.GetPixel(e.X, e.Y);
                labelPixi.Text = ""
                    + "x:" + e.X + "\n"
                    + "y:" + e.Y + "\n"
                    + "\n"
                    + "R:" + c.R + "\n"
                    + "G:" + c.G + "\n"
                    + "B:" + c.B + "\n"
                    + "A:" + c.A + "\n"
                    ;
            }
        }

        private void listViewMicroLabels_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (ListViewItem lvi in listViewMicroLabels.SelectedItems) {
                hexViewer.SetPos((int)lvi.Tag);
                break;
            }
        }

        Visf visf;

        private void button1_Click(object sender, EventArgs e) {
            visf = new Visf(dcList, collisionSets, doct);
            visf.Show();
        }

        private void Parse1D(TreeNode tn, int xoff, Stream si) { // IMGZ
            BinaryReader br = new BinaryReader(si);

            si.Position = 0x0C;
            int v0c = br.ReadInt32();
            for (int x = 0; x < v0c; x++) {
                si.Position = 0x10 + 8 * x;
                int toff = br.ReadInt32();
                int tlen = br.ReadInt32();
                {
                    si.Position = toff;
                    MemoryStream six = new MemoryStream(br.ReadBytes(tlen), false);

                    PicIMGD p = ParseIMGD.TakeIMGD(six);
                    TreeNode tnt = tn.Nodes.Add("IMGD." + PalC2s.Guess(p));
                    tnt.Tag = new IMGDi(xoff, p.pic);
                }
            }
        }

        class PalC2s {
            public static string Guess(PicIMGD p) {
                return Guess(p.pic);
            }
            public static string Guess(Bitmap p) {
                switch (p.PixelFormat) {
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                        return "8";
                    case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                        return "4";
                }
                return "?";
            }
        }

        private void ParseVAGp(TreeNode node, int xoff, Stream fs, bool stereo) {
            var br = new BinaryReader(fs);
            BER ber = new BER(br);

            int vagoff = 0;
            {
                fs.Position = vagoff + 0x0C;
                int cb = ber.ReadInt32() - 0x20;

                fs.Position = vagoff + 0x10;
                int sps = ber.ReadInt32();

                fs.Position = vagoff + 0x20;
                var fileName = Encoding.ASCII.GetString(br.ReadBytes(32)).Split('\0')[0];

                fs.Position = vagoff + 0x40;
                var wavObj = (new Wavo(
                    fileName + ".wav",
                    stereo
                        ? SPUConv.ToWave2ch(new MemoryStream(br.ReadBytes(cb)), sps)
                        : SPUConv.ToWave(new MemoryStream(br.ReadBytes(cb)), sps)
                    ));

                var newNode = node.Nodes.Add(fileName);
                newNode.Tag = new WAVi(xoff, wavObj);
                setWAVi(newNode);
                node.Tag = wavObj;
            }

        }

        private void Parse96(TreeNode node, int xoff, ArraySegment<byte> bytes) { // dpd
            var dpd = new ParseDPD(bytes);

            AddDpd(dpd, node, xoff, bytes.Count, 0);
        }

        private void Parse82(TreeNode node, int xoff, ArraySegment<byte> bytes) { // dpx
            var dpx = new ParseDPX(bytes);

            AddDpx(dpx, node, xoff, bytes.Count);
        }

        private void Parse12(TreeNode node, int dataOff, ArraySegment<byte> bytes) { // pax
            var pax = new ParsePAX(bytes);

            AddDpx(pax.dpx, node, dataOff, bytes.Count);
        }

        private void AddDpx(ParseDPX dpx, TreeNode paxNode, int dataOff, int dataLen) {
            var dpxNode = AddNode(paxNode, "DPX", new Hexi(dataOff + dpx.dpxOffset, dataLen - dpx.dpxOffset));
            int x = 0;
            foreach (var dpd in dpx.dpdList) {
                AddDpd(dpd, dpxNode, dataOff, dataLen, x);
                ++x;
            }
        }

        private void AddDpd(ParseDPD dpd, TreeNode dpxNode, int dataOff, int dataLen, int x) {
            var dpdNode = AddNode(
                dpxNode,
                $"DPD{x}",
                new Hexi(dataOff + dpd.dpdOffset, dataLen - dpd.dpdOffset, dpd.labels.CopyAndShiftAll(dataOff))
            );

            foreach (var dpdPic in dpd.dpdPicList) {
                AddNode(dpdNode,
                    $"{x}.{dpdPic.index2}.p.{PalC2s.Guess(dpdPic.bitmap)}",
                    new IMGDi(
                        dataOff + dpdPic.offset,
                        dpdPic.bitmap,
                        dpdPic.labels.CopyAndShiftAll(dataOff)
                    )
                );
            }

            {
                int i = -1;
                foreach (var d1 in dpd.d1List) {
                    ++i;
                    var subNode = AddNode(
                        dpdNode,
                        $"D1[{i}]",
                        new Strif(dataOff + dpd.dpdOffset + d1.topOffset, d1.Describe())
                    );
                }
            }

        }

        private void Parse22(TreeNode tn, int xoff, Stream si) { // iopvoice
            BinaryReader br = new BinaryReader(si);

            Wavo[] al = ParseSD.ReadIV(si);
            foreach (Wavo o in al) {
                TreeNode tnt = tn.Nodes.Add(o.fn);
                tnt.Tag = new WAVi(xoff, o);
                setWAVi(tnt);
            }
        }

        private void Parse20(TreeNode tn, int xoff, Stream si) { // WD
            BinaryReader br = new BinaryReader(si);

            Wavo[] al = ParseSD.ReadWD(si);
            foreach (Wavo o in al) {
                TreeNode tnt = tn.Nodes.Add(o.fn);
                tnt.Tag = new WAVi(xoff, o);
                setWAVi(tnt);
            }
        }

        private void setWAVi(TreeNode tnt) {
            tnt.ImageKey = tnt.SelectedImageKey = "SpeechMicHS.png";
        }

        private void Parse18(TreeNode topNode, int xoff, Stream si) { // IMGD
            BinaryReader br = new BinaryReader(si);

            PicIMGD p = ParseIMGD.TakeIMGD(si);
            TreeNode subNode = topNode.Nodes.Add("IMGD." + PalC2s.Guess(p));
            subNode.Tag = new IMGDi(xoff, p.pic);
        }

        private void bserr_Click(object sender, EventArgs e) {
            Form form = new Form();
            form.StartPosition = FormStartPosition.Manual;
            form.Width = this.Width;
            form.Height = this.Height / 4;
            form.Location = bErrorPop.PointToScreen(new Point(0, bErrorPop.Height));
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            form.Text = "Error message from analiser";
            form.Show(this);
            TextBox tb = new TextBox();
            tb.BorderStyle = BorderStyle.None;
            tb.Multiline = true;
            tb.Parent = form;
            tb.Dock = DockStyle.Fill;
            tb.ScrollBars = ScrollBars.Vertical;
            tb.BackColor = Color.FromKnownColor(KnownColor.Info);
            tb.ForeColor = Color.FromKnownColor(KnownColor.InfoText);
            tb.Text = ((Exception)bErrorPop.Tag).ToString();
            tb.Show();

            Button buttonCloseMe = new Button();
            buttonCloseMe.Click += new EventHandler(buttonCloseMe_Click);
            buttonCloseMe.Parent = form;
            buttonCloseMe.Text = "Close!";
            form.AcceptButton = buttonCloseMe;
            form.CancelButton = buttonCloseMe;

            form.Activate();
            tb.Select();
            tb.SelectAll();
        }

        void buttonCloseMe_Click(object sender, EventArgs e) {
            ((Form)((Button)sender).Parent).Close();
        }

        private void RDForm_Load(object sender, EventArgs e) {
            {
                foreach (Match M in Regex.Matches(File.ReadAllText(Path.Combine(Application.StartupPath, "objname.txt"), Encoding.UTF8).Replace("\r", "\n"), "^(?<d>[0-9A-F]{4})=(?<n>.+)$", RegexOptions.IgnoreCase | RegexOptions.Multiline)) {
                    dictObjName[Convert.ToInt32(M.Groups["d"].Value, 16)] = M.Groups["n"].Value;
                }
            }

            if (fpread != null) {
                LoadMap((fpread));
            }
        }

        private void bExportBin_Click(object sender, EventArgs e) {
            TreeNode tn = treeView1.SelectedNode;
            if (tn == null) {
                MessageBox.Show(this, "No item selected!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Hexi o = tn.Tag as Hexi;
            if (o == null) {
                MessageBox.Show(this, "Export not supported!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (o.len == 0) {
                MessageBox.Show(this, "It does not declare bin LENGTH!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = tn.Text + ".bin";
                sfd.Filter = "*.bin|*.bin||";
                if (sfd.ShowDialog(this) == DialogResult.OK) {
                    using (FileStream fs = File.Create(sfd.FileName)) {
                        fs.Write(fileBin, o.off, o.len);
                        fs.Close();
                    }
                    MessageBox.Show(this, "Saved!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void bExportAll_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "Are you really explode all?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            String dirTo = Path.Combine(Application.StartupPath, "export\\" + Path.GetFileName(OpenedPath).Replace(".", "_"));

            Expall(treeView1.Nodes, dirTo);

            Process.Start("explorer.exe", " \"" + dirTo + "\"");
        }

        String OpenedPath { get { return linkLabel1.Text; } }

        public void ExpallTo(String dirTo) {
            Expall(treeView1.Nodes, dirTo);
        }

        void Expall(TreeNodeCollection tnc, String dirTo) {
            foreach (TreeNode tn in tnc) {
                UniName un = new UniName();
                foreach (TreeNode tn2 in tn.Nodes) {
                    Expthem(dirTo, tn2, un);
                }
            }
        }

        class UniName {
            SortedDictionary<string, string> dictUsed = new SortedDictionary<string, string>();

            public String Get(String fn) {
                for (int x = 0; x < 100; x++) {
                    String fnx = Path.GetFileNameWithoutExtension(fn) + ((x != 0) ? "~" + (1 + x) : "") + Path.GetExtension(fn);
                    if (dictUsed.ContainsKey(fnx))
                        continue;
                    dictUsed[fnx] = null;
                    return fnx;
                }
                return fn;
            }
        }

        private void Expthem(String dirTo, TreeNode tn, UniName un) {
            while (p1.Controls.Count != 0) p1.Controls[0].Dispose();

            String dirn = un.Get(tn.Text);

            WavePlayer = delegate (WAVi wi) {
                Directory.CreateDirectory(dirTo);
                File.WriteAllBytes(Path.Combine(dirTo, un.Get(dirn)), wi.w.bin); // already with file extension
            };

            treeView1.SelectedNode = tn;
            treeView1_AfterSelect(treeView1, new TreeViewEventArgs(tn, TreeViewAction.ByKeyboard));
            Update();

            WavePlayer = null;

            foreach (Control that in p1.Controls) {
                if (that is PictureBox) {
                    Directory.CreateDirectory(dirTo);
                    PictureBox o = (PictureBox)that;
                    o.Image.Save(Path.Combine(dirTo, un.Get(dirn + ".png")), ImageFormat.Png);
                }
                if (that is TextBox) {
                    Directory.CreateDirectory(dirTo);
                    TextBox o = (TextBox)that;
                    File.WriteAllText(Path.Combine(dirTo, un.Get(dirn + ".txt")), o.Text, Encoding.Default);
                }

                if (that is FlowLayoutPanel) {
                    int cnt = 1;
                    foreach (Control it in that.Controls) {
                        if (it is PictureBox) {
                            Directory.CreateDirectory(dirTo);
                            PictureBox o = (PictureBox)it;
                            o.Image.Save(Path.Combine(dirTo, un.Get(dirn + "_" + cnt + ".png")), ImageFormat.Png);
                            cnt++;
                        }
                    }
                }
            }

            {
                Hexi o = tn.Tag as Hexi;
                if (o.len != 0) {
                    Directory.CreateDirectory(dirTo);
                    using (FileStream fs = File.Create(Path.Combine(dirTo, un.Get(dirn + ".bin")))) {
                        fs.Write(fileBin, o.off, o.len);
                        fs.Close();
                    }
                }
            }

            UniName myun = new UniName();
            String subdirn = myun.Get(dirn);
            foreach (TreeNode tnChild in tn.Nodes) {
                Expthem(Path.Combine(dirTo, subdirn), tnChild, myun);
            }
        }

        private void bBatExp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            BEXForm form = new BEXForm(this);
            form.Show();
        }
    }

    public class Vifpli {
        public byte[] vifpkt;
        public int texi;

        public Vifpli(byte[] vifpkt, int texi) {
            this.vifpkt = vifpkt;
            this.texi = texi;
        }
    }
    public class M4 {
        public List<int> alb1t2;
        public List<int[]> alb2;
        public List<Vifpli> alvifpkt = new List<Vifpli>();
    }
    public class MTex {
        public Bitmap[] pics;
        public int[] alIndirIndex = new int[0];

        public MTex(List<Bitmap> pics) {
            this.pics = pics.ToArray();
        }
    }
}
