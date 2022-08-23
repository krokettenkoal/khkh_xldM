using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using vcBinTex4;
using System.Runtime.InteropServices;

namespace vconv122 {
    class ProgramVc {
        static void Main(string[] args) {
#if DEBUG1
            new ProgramVc().run(
                @"V:\KH2.yaz0r\dump_kh2\obj\M_EX770.mdlx",
                @"H:\Proj\khkh_xldM\MEMO\M_EX770.mdls"
                );
#endif
#if DEBUG
            new ProgramVc().run(
                @"V:\KH2.yaz0r\dump_kh2\obj\P_EX100.mdlx",
                @"H:\Proj\khkh_xldM\MEMO\P_EX100.mdls"
                );
#endif
        }

        private void run(string fmdlx, string fmdls) {
            byte[] bin = File.ReadAllBytes(fmdlx);
            ReadBar.Barent[] ents = ReadBar.Explode(new MemoryStream(bin, false));
            Vart vart = null;
            TIMf timf = null;
            for (int x = 0; x < ents.Length; x++) {
                switch (ents[x].k) {
                    case 4: // verts
                        vart = new Vart(ents[x].bin);
                        vart.readDomain();
                        Console.Write("");
                        break;
                    case 7: // tex
                        timf = new TIMf();
                        timf.Load(new MemoryStream(ents[x].bin, false));
                        Console.Write("");
                        break;
                }
            }

            if (true) { // build mdls
                List<byte[]> albin = new List<byte[]>();
                albin.Add(new MOBJ(timf, vart).build());
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);

                using (FileStream fs = File.Create(fmdls)) {
                    WzUtil.write(fs, 0x80);

                    MemoryStream os = new MemoryStream();
                    BinaryWriter wr = new BinaryWriter(os);
                    wr.Write((int)albin.Count);
                    for (int t = 0; t < albin.Count; t++) {
                        wr.Write((int)fs.Position);
                        fs.Write(albin[t], 0, albin[t].Length);
                        while (0 != (fs.Position & 0xFF)) fs.WriteByte(0);
                    }
                    wr.Write((int)fs.Position);
                    fs.Position = 0;
                    os.Position = 0;
                    os.WriteTo(fs);
                }
            }

            if (true) { // build mset
#if false
                List<byte[]> albin = new List<byte[]>();
                albin.Add(new MMTN(timf, vart).build());
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);
                albin.Add(new byte[0]);

                using (FileStream fs = File.Create(fmdls)) {
                    WzUtil.write(fs, 0x80);

                    MemoryStream os = new MemoryStream();
                    BinaryWriter wr = new BinaryWriter(os);
                    wr.Write((int)albin.Count);
                    for (int t = 0; t < albin.Count; t++) {
                        wr.Write((int)fs.Position);
                        fs.Write(albin[t], 0, albin[t].Length);
                        while (0 != (fs.Position & 0xFF)) fs.WriteByte(0);
                    }
                    wr.Write((int)fs.Position);
                    fs.Position = 0;
                    os.Position = 0;
                    os.WriteTo(fs);
                }
#endif
            }
        }
    }
    class MOBJ {
        TIMf tim;
        Vart vart;
        public MOBJ(TIMf tim, Vart vart) {
            this.tim = tim;
            this.vart = vart;
        }

        public byte[] mkTexi(int cnt) {
            MemoryStream os = new MemoryStream();
            for (int x = 0; x < cnt; x++) {
                os.Write(new byte[] { 0x00, 0x04, 0x07, 0x07, 0x80, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 16);
            }
            return os.ToArray();
        }
        public byte[] mkTexa(TIMf tim) {
            MemoryStream os = new MemoryStream();
            foreach (STim o in tim.alt) {
                os.Write(o.picbin, 0, 0x4000);
            }
            return os.ToArray();
        }
        public byte[] mkPala(TIMf tim, Vart vart) {
            MemoryStream os = new MemoryStream();
#if false
            foreach (STim o in tim.alt) {
                os.Write(o.palbin, 0, 4 * 256);
            }
#else
            for (int t = 0; t < tim.alt.Count; t++) {
                os.Write(tim.alt[t].palbin, 0, 4 * 256);
            }
#endif
            return os.ToArray();
        }
        public byte[] mkAmatz() {
            MemoryStream os = new MemoryStream();
            BinaryWriter wr = new BinaryWriter(os);
            for (int x = 0; x < vart.alaxbone.Count; x++) {
                wr.Write((float)1); // @0 :x0=0
                wr.Write((float)1); // @4 :y0=0
                wr.Write((float)1); // @8 :z0=0
                wr.Write((int)x); // @12 :w0=boneindex
                wr.Write((float)vart.alaxbone[x].x2); // @16 :x1
                wr.Write((float)vart.alaxbone[x].y2); // @20 :y1
                wr.Write((float)vart.alaxbone[x].z2); // @24 :z1
                wr.Write((int)0); // @28 :w1=0
                wr.Write((float)vart.alaxbone[x].x3); // @32 :x2
                wr.Write((float)vart.alaxbone[x].y3); // @36 :y2
                wr.Write((float)vart.alaxbone[x].z3); // @40 :z2
                wr.Write((int)(vart.alaxbone[x].parent & 0xFFFF)); // @44 :w2=parentindex
            }
            return os.ToArray();
        }

        public byte[] mkVSt(int which) {
            VU1Mem vu1mem = new VU1Mem();
            ParseVIF1 vif1 = new ParseVIF1(vu1mem);
            vconv122.Vart.DMAtag2 tag = vart.listBox3[which];
            vif1.Parse(new MemoryStream(vart.bin04, tag.off, tag.qwc * 16, false), 0x40);
            Vr vr = new Vr();
            vr.add(vu1mem, 0x40, 0);

            MemoryStream os = new MemoryStream();
            BinaryWriter wr = new BinaryWriter(os);
            os.Write(new byte[] { 0xED, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x00, 0x01, 0x00, 0x80, 0xEC, 0x6C }, 0, 16);
            for (int t = 0; t < tag.alaxi.Length; t++) {
                wr.Write(0);
                wr.Write((int)tag.alaxi[t]);
                wr.Write((int)t);
                for (int w = 0; w < 116; w++) os.WriteByte(0);
            }
            for (int t = 0; t < vr.altris.Count / 3; t++) {
                os.Write(new byte[] { 0x01, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x40, 0x3E, 0x30, 0x12, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 32);
                // x0,y0,z0 - ?
                // w0 - belonging
                // x1,y1,z1 - pos
                // x2,y2 - tu tv
                for (int w = 0; w < 3; w++) {
                    V6 v6 = vr.altris[3 * t + w];
                    if (v6 == null) v6 = vr.altris[3 * t + ((w + 1) % 3)];
                    if (v6 == null) v6 = vr.altris[3 * t + ((w + 2) % 3)];

                    wr.Write((float)1);
                    wr.Write((float)1);
                    wr.Write((float)1);
                    wr.Write((int)v6.sel);

                    wr.Write((float)v6.v4.x);
                    wr.Write((float)v6.v4.y);
                    wr.Write((float)v6.v4.z);
                    wr.Write((float)1);

                    wr.Write((float)(v6.texpt.X / 256.0f));
                    wr.Write((float)(v6.texpt.Y / 256.0f));
                    wr.Write((float)1);
                    wr.Write((float)0);
                }
            }
            os.Write(new byte[] { 0x00, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x17, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, 32);
            return os.ToArray();
        }
        public byte[] mkStep40() {
            MemoryStream body = new MemoryStream();
            WzUtil.write(body, 0x10 + 0x10 * vart.listBox3.Count + 0x50);

            MemoryStream os = new MemoryStream();
            BinaryWriter wr = new BinaryWriter(os, Encoding.ASCII);
            wr.Write((int)vart.alaxbone.Count); // @0x00 :bonecnt
            wr.Write((int)body.Position); // @0x04 :boneoff
            wr.Write((int)0); // @0x08 :off?
            wr.Write((int)vart.listBox3.Count); // @0x0c :cnt partitions

            byte[] amatz = mkAmatz();
            body.Write(amatz, 0, amatz.Length);

            for (int x = 0; x < vart.listBox3.Count; x++) {
                wr.Write((int)0); // +0x0 :?
                wr.Write((int)vart.listBox3[x].ti); // +0x4 :texindex? // TODO ñÑÇﬂÇÈéñ
                wr.Write((int)vart.listBox3[x].ti); // +0x8 :texindex?
                wr.Write((int)body.Position); // +0xc :offsetverts
                byte[] binvst = mkVSt(x);
                body.Write(binvst, 0, binvst.Length);
            }

            body.Position = 0;
            os.Position = 0;
            os.WriteTo(body);
            return body.ToArray();
        }
        public byte[] build() {
            byte[] texinfo = mkTexi(tim.alt.Count);
            byte[] texall = mkTexa(tim);
            byte[] palall = mkPala(tim, vart);
            byte[] step40 = mkStep40();

            MemoryStream body = new MemoryStream();
            WzUtil.write(body, 0x40);
            int off40 = (int)body.Position;
            body.Write(step40, 0, step40.Length);
            int offtexinfo = (int)body.Position;
            body.Write(texinfo, 0, texinfo.Length);
            int offtexall = (int)body.Position;
            body.Write(texall, 0, texall.Length);
            int offpalall = (int)body.Position;
            body.Write(palall, 0, palall.Length);

            BinaryWriter wr = new BinaryWriter(body, Encoding.ASCII);
            body.Position = 0;
            wr.Write((int)-1); // @0x00 // (replaced later)
            wr.Write((int)-1); // @0x04 // (replaced later)
            wr.Write((int)offtexinfo); // @0x08 :off    texinfo
            wr.Write((int)(texinfo.Length)); // @0x0c :sizetexinfo (0x10 Å~ texcnt)
            wr.Write((int)offtexall); // @0x10 :offtex
            wr.Write((int)(texall.Length)); // @0x14 :sizetex
            wr.Write((int)offpalall); // @0x18 :offpalbin
            wr.Write((int)(palall.Length)); // @0x1c :sizepalbin
            wr.Write((int)off40); // @0x20 :off40
            wr.Write((int)(step40.Length)); // @0x24 :size40
            wr.Write((int)0); // @0x28 :off?
            wr.Write((int)0); // @0x2c :?
            wr.Write((int)0); // @0x30 :off?
            wr.Write((int)0); // @0x34 :?
            wr.Write((int)0); // @0x38 :?
            wr.Write((int)0); // @0x3c :?

            return TokMk.enplace(body.ToArray());
        }
    }
    class WzUtil {
        public static void write(Stream os, int size) {
            for (int t = 0; t < size; t++) os.WriteByte(0);
        }
    }
    class Vr {
        public void add(VU1Mem M, int tops, int tops2) {
            MemoryStream si = new MemoryStream(M.vumem, false);
            BinaryReader br = new BinaryReader(si);
            si.Position = 16 * tops;
            int v00 = br.ReadInt32();
            if (v00 != 1 && v00 != 2) throw new NotSupportedException();
            int v04 = br.ReadInt32();
            int v08 = br.ReadInt32();
            int v0c = br.ReadInt32();
            int v10 = br.ReadInt32(); // cntindices
            int v14 = br.ReadInt32(); // offindices
            int v18 = br.ReadInt32(); // offi2 (axbone)
            int v1c = br.ReadInt32(); // offi2last
            int v20 = (v00 == 1) ? br.ReadInt32() : 0; // cntvertscolor
            int v24 = (v00 == 1) ? br.ReadInt32() : 0; // offvertscolor
            int v28 = (v00 == 1) ? br.ReadInt32() : 0;
            int v2c = (v00 == 1) ? br.ReadInt32() : 0;
            int v30 = br.ReadInt32(); // cntverts 
            int v34 = br.ReadInt32(); // offverts
            int v38 = br.ReadInt32(); // 
            int v3c = br.ReadInt32(); // cnt axbone

            Point[] texpts = new Point[v10];
            int[] vertexindices = new int[v10];
            int[] vertexsteps = new int[v10];
            V4[] vtxpts = new V4[v30];
            byte[] alrefax = null;
            alrefax = new byte[v30];

            si.Position = 16 * (tops + v14);
            for (int t = 0; t < v10; t++) {
                int tx = br.ReadUInt16() / 16; br.ReadUInt16();
                int ty = br.ReadUInt16() / 16; br.ReadUInt16();
                texpts[t] = new Point(tx, ty);
                vertexindices[t] = br.ReadUInt16(); br.ReadUInt16();
                vertexsteps[t] = br.ReadUInt16(); br.ReadUInt16();
            }

            si.Position = 16 * (tops + v34);
            for (int t = 0; t < v30; t++) {
                float vx = (br.ReadSingle());
                float vy = (br.ReadSingle());
                float vz = (br.ReadSingle());
                float vw = (br.ReadSingle());
                vtxpts[t] = new V4(vx, vy, vz, vw);
            }

            si.Position = 16 * (tops + v18);
            for (int t = 0, pos = 0; t < v3c; t++) {
                int cx = br.ReadInt32();
                for (int x = 0; x < cx; x++, pos++) {
                    alrefax[pos] = Convert.ToByte(t);
                }
            }

            //si.Position = 16 * (tops2);
            //for (int t = 0; t < v3c; t++) {
            //    alax[t] = UtilMatrixio.read(br);
            //}

            for (int t = 0; t < v10; t++) {
                int vi = vertexindices[t];
                add1(vtxpts[vi], alrefax[vi], texpts[t], vertexsteps[t]);
            }

            //alent.Add(new Ent(entryindex, v10, v30, v00));
            //entryindex++;
        }

        V6[] v6 = new V6[] { null, null, null, null };
        int iv6 = 0;

        public List<V6> altris = new List<V6>();

        private void add1(V4 v4, byte refax, Point texpt, int step) {
            v6[iv6] = new V6(v4, texpt, refax);
            iv6 = (iv6 + 1) & 3;

            if (step == 0x10) { // í«â¡
            }
            else if (step == 0x20) { // a tri-strip
                altris.Add(v6[(iv6 - 1) & 3]);
                altris.Add(v6[(iv6 - 3) & 3]);
                altris.Add(v6[(iv6 - 2) & 3]);
            }
            else if (step == 0x30) { // a tri
                altris.Add(v6[(iv6 - 1) & 3]);
                altris.Add(v6[(iv6 - 2) & 3]);
                altris.Add(v6[(iv6 - 3) & 3]);
            }
            else throw new NotSupportedException("ïsñæÇ»step Å® " + step);
        }
    }
    public class V4 {
        public float x, y, z, w;

        public V4() { }
        public V4(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
    public class V6 {
        public V4 v4 = null;
        public Point texpt = Point.Empty;
        public int sel = -1;

        public V6(V4 v4, Point texpt, int sel) {
            this.v4 = v4;
            this.texpt = texpt;
            this.sel = sel;
        }
    }
    class TokMk {
        public static byte[] enplace(byte[] bin) {
            MemoryStream os = new MemoryStream();
            BinaryWriter wr = new BinaryWriter(os);
            wr.Write((byte)0x4D);
            wr.Write((byte)0x4F);
            wr.Write((byte)0x42);
            wr.Write((byte)0x4A);
            wr.Write((int)bin.Length - 8);
            wr.Write(bin, 8, (int)(bin.Length - 8));
            wr.Write((byte)0x5F);
            wr.Write((byte)0x4B);
            wr.Write((byte)0x4E);
            wr.Write((byte)0x35);
            return os.ToArray();
        }
    }
}
namespace vconv122 {
    public class VU1Mem {
        public byte[] vumem = new byte[16 * 1024];
    }
    class ParseVIF1 {
        VU1Mem vu1;

        public ParseVIF1(VU1Mem vu1) {
            this.vu1 = vu1;
        }

        class Reader {
            BinaryReader br;
            int vl;
            bool usn;

            public Reader(BinaryReader br, int vl, int usn) {
                this.br = br;
                this.vl = vl;
                this.usn = (usn != 0) ? true : false;
            }
            public uint Read() {
                switch (vl) {
                    case 0: // 32 bits
                        return br.ReadUInt32();
                    case 1: // 16 bits
                        if (usn) return br.ReadUInt16();
                        return (uint)(int)br.ReadInt16();
                    case 2: // 8 bits
                        if (usn) return br.ReadByte();
                        return (uint)(sbyte)br.ReadSByte();
                    default:
                        throw new NotSupportedException("vl(" + vl + ")");
                }
            }
        }

        public void Parse(MemoryStream si) {
            Parse(si, 0);
        }
        public void Parse(MemoryStream si, int tops) {
            MemoryStream os = new MemoryStream(vu1.vumem, true);
            BinaryWriter bw = new BinaryWriter(os);

            byte[] vifmask = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            uint[] colval = new uint[4];
            uint[] rowval = new uint[4];

            BinaryReader br = new BinaryReader(si);
            while (si.Position < si.Length) {
                long curPos = si.Position;
                uint v = br.ReadUInt32();
                int cmd = ((int)(v >> 24) & 0x7F);
                switch (cmd) {
                    case 0x00: break; // nop
                    case 0x01: { // stcycl
                            int cl = ((int)(v >> 0) & 0xFF);
                            int wl = ((int)(v >> 8) & 0xFF);
                            break;
                        }
                    case 0x02: break; // offset
                    case 0x03: break; // base
                    case 0x04: break; // itop
                    case 0x05: break; // stmod
                    case 0x06: break; // mskpath3
                    case 0x07: break; // mark
                    case 0x10: break; // flushe
                    case 0x11: break; // flush
                    case 0x13: break; // flusha
                    case 0x14: break; // mscal
                    case 0x17: break; // mscnt
                    case 0x15: break; // mscalf
                    case 0x20: { // stmask
                            uint r1 = br.ReadUInt32();
                            for (int x = 0; x < 16; x++) {
                                vifmask[x] = (byte)(((int)(r1 >> (2 * x))) & 3);
                            }
                            break;
                        }
                    case 0x30: { // strow
                            rowval[0] = br.ReadUInt32();
                            rowval[1] = br.ReadUInt32();
                            rowval[2] = br.ReadUInt32();
                            rowval[3] = br.ReadUInt32();
                            break;
                        }
                    case 0x31: { // stcol
                            colval[0] = br.ReadUInt32();
                            colval[1] = br.ReadUInt32();
                            colval[2] = br.ReadUInt32();
                            colval[3] = br.ReadUInt32();
                            break;
                        }
                    case 0x4A: break; // mpg
                    case 0x50: { // direct
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            si.Position = (si.Position + 15) & (~15);
                            si.Position += 16 * imm;
                            break;
                        }
                    case 0x51: { // directhl
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            si.Position = (si.Position + 15) & (~15);
                            si.Position += 16 * imm;
                            break;
                        }
                    default: { // unpack or ?
                            if (0x60 == (cmd & 0x60)) {
                                int m = ((int)(cmd >> 4) & 1);
                                int vn = ((int)(cmd >> 2) & 0x3);
                                int vl = ((int)(cmd >> 0) & 0x3);

                                int size = ((int)(v >> 16) & 0xFF);
                                int flg = ((int)(v >> 15) & 1);
                                int usn = ((int)(v >> 14) & 1);
                                int addr = ((int)(v >> 0) & 0x3FF);

                                int cbEle = 0, cntEle = 1;
                                switch (vl) {
                                    case 0: cbEle = 4; break;
                                    case 1: cbEle = 2; break;
                                    case 2: cbEle = 1; break;
                                    case 3: cbEle = 2; break;
                                }
                                switch (vn) {
                                    case 0: cntEle = 1; break;
                                    case 1: cntEle = 2; break;
                                    case 2: cntEle = 3; break;
                                    case 3: cntEle = 4; break;
                                    default: Debug.Fail("Ahh vn!"); break;
                                }
                                int cbTotal = (cbEle * cntEle * size + 3) & (~3);
                                long newPos = si.Position + cbTotal;

                                os.Position = 16 * (tops + addr);
                                bool nomask = (m == 0) ? true : false;

                                Reader reader = new Reader(br, vl, usn);
                                for (int y = 0; y < size; y++) {
                                    uint tmpv;
                                    switch (vn) {
                                        case 0: // S-XX
                                        default:
                                            tmpv = reader.Read();
                                            // X
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            switch (vifmask[(y & 3) * 4 + 3] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[3]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            break;
                                        case 1: // V2-XX
                                            // X
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            os.Position += 4;
                                            // W
                                            os.Position += 4;
                                            break;
                                        case 2: // V3-XX
                                            // X
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            os.Position += 4;
                                            break;
                                        case 3: // V4-XX
                                            // X
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            tmpv = reader.Read();
                                            switch (vifmask[(y & 3) * 4 + 3] & (nomask ? 0 : 7)) {
                                                case 0: bw.Write(tmpv); break;
                                                case 1: bw.Write(rowval[y & 3]); break;
                                                case 2: bw.Write(colval[3]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            break;
                                    }
                                }

                                si.Position = newPos;
                                break;
                            }
                            break;
                        }
                }
            }
        }
    }
}
namespace vconv122 {
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
}
namespace vconv122 {
    public class Vart {
        public class EntRoot {
            public int off;

            public EntRoot(int off) {
                this.off = off;
            }
        }
        public class Domain : IComparable<Domain> {
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

            #region IComparable<Domain> ÉÅÉìÉo

            public int CompareTo(Domain other) {
                int t = pos.CompareTo(other.pos);
                return t;
            }

            #endregion
        }
        public class DMAtag2 {
            public int off, qwc;
            public int[] alaxi;
            public int ti;

            public DMAtag2(int off, int qwc, int[] alaxi, int ti) {
                this.off = off;
                this.qwc = qwc;
                this.alaxi = alaxi;
                this.ti = ti;
            }
            public override string ToString() {
                return string.Format("{0:X5} c {1,2}", off, qwc);
            }
        }

        public List<DMAtag2> listBox3 = new List<DMAtag2>();
        public List<Domain> listBox2 = new List<Domain>();

        public byte[] bin04 = null;

        public Vart(byte[] bin04) {
            this.bin04 = bin04;
        }
        public void readDomain() {
            MemoryStream si = new MemoryStream(bin04, false);
            BinaryReader br = new BinaryReader(si);
            Queue<EntRoot> aler = new Queue<EntRoot>();
            aler.Enqueue(new EntRoot(0x90));
            List<Domain> aldom = new List<Domain>();
            listBox3.Clear();
            int postbl3 = 0;
            while (aler.Count != 0) {
                EntRoot er = aler.Dequeue();
                si.Position = er.off + 0x10;
                int cnt2 = br.ReadUInt16();
                si.Position = er.off + 0x1C;
                int cnt1 = br.ReadUInt16();

                aldom.Add(new Domain("T3.1(" + postbl3 + ")", er.off, 0x20 * (1 + cnt1))); postbl3++;

                for (int c1 = 0; c1 < cnt1; c1++) {
                    si.Position = er.off + 0x20 + 0x20 * c1;
                    br.ReadInt32();
                    int texi = br.ReadInt32();
                    si.Position = er.off + 0x20 + 0x20 * c1 + 0x10;
                    int pos1 = br.ReadInt32() + er.off;
                    int pos2 = br.ReadInt32() + er.off;
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
                        listBox3.Add(new DMAtag2(offsrc, qwcsrc, alaxi[tpos], texi)); tpos++;

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
            listBox2.Clear();
            listBox2.AddRange(aldom.ToArray());
        }

        public List<AxBone> alaxbone = new List<AxBone>();

        private void parseT2Struc(int off2, int len2) {
            MemoryStream si = new MemoryStream(bin04, off2, len2, false);
            BinaryReader br = new BinaryReader(si);

            alaxbone.Clear();
            for (int x = 0; x < len2 / 0x40; x++) {
                alaxbone.Add(UtilAxBoneReader.read(br));
            }
            //Array.ForEach<AxBone>(alaxbone.ToArray(), new Action<AxBone>(test0));
        }

        class RUtil {
            public static int RoundUpto16(int val) {
                return (val + 15) & (~15);
            }
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
    public class AxBone {
        public int cur, parent, v08, v0c;
        public float x1, y1, z1, w1;
        public float x2, y2, z2, w2;
        public float x3, y3, z3, w3;

        public AxBone Clone() {
            return (AxBone)MemberwiseClone();
        }
    }
}
namespace vconv122 {
    class STim {
        public byte[] picbin = null;
        public byte[] palbin = null;

        public STim(byte[] picbin, byte[] palbin) {
            this.picbin = picbin;
            this.palbin = palbin;
        }
    }
    class TIMf {
        public List<STim> alt = new List<STim>();

        public void Load(Stream fs) {
            int wcx, wcy;
            byte[] picbin = null;
            byte[] palbin = null;
            byte[] refal = null;
            byte[] texinf1bin = null;
            byte[] texinf2bin = null;

            BinaryReader br = new BinaryReader(fs);
            fs.Position = 8;
            wcx = br.ReadInt32(); // @ 0x08
            wcy = br.ReadInt32(); // @ 0x0C
            int woff = br.ReadInt32(); // @ 0x10
            int texinf1off = br.ReadInt32(); // @0x14
            int texinf2off = br.ReadInt32(); // @0x18
            fs.Position = woff;
            refal = br.ReadBytes(wcy);
            fs.Position = 0x1c;
            int picoff = br.ReadInt32();
            int paloff = br.ReadInt32();

            fs.Position = texinf1off;
            texinf1bin = br.ReadBytes(texinf2off - texinf1off);
            fs.Position = texinf2off;
            texinf2bin = br.ReadBytes(picoff - texinf2off);
            fs.Position = picoff;
            picbin = br.ReadBytes(paloff - picoff);
            palbin = br.ReadBytes(Convert.ToInt32(fs.Length) - 4 - paloff);

            byte[] gsram = new byte[4 * 1024 * 1024];

            // TEXTURE MAPPING PROGRAM
            for (int wy = 0; wy < wcy; wy++) {
                if (true) {
                    // TEXTURE PREPARATION PROGRAM
                    for (int wi = 0; wi < 2; wi++) {
                        int wx = (wi == 0) ? 0 : (1 + refal[wy]);
                        fs.Position = texinf1off + 144 * wx + 0x20;
                        UInt64 v0 = br.ReadUInt64();
                        int sbp = ((int)(v0 >> 0) & 0x3FFF);
                        int sbw = ((int)(v0 >> 16) & 0x3F);
                        int spsm = ((int)(v0 >> 24) & 0x3F);
                        int dbp = ((int)(v0 >> 32) & 0x3FFF);
                        int dbw = ((int)(v0 >> 48) & 0x3F);
                        int dpsm = ((int)(v0 >> 56) & 0x3F);
                        Trace.Assert(br.ReadUInt64() == 0x50, "Invalid");

                        fs.Position = texinf1off + 144 * wx + 0x40;
                        UInt64 v2 = br.ReadUInt64();
                        int rrw = ((int)(v2 >> 0) & 0xFFF);
                        int rrh = ((int)(v2 >> 32) & 0xFFF);
                        Trace.Assert(br.ReadUInt64() == 0x52, "Invalid");

                        fs.Position = texinf1off + 144 * wx + 0x60;
                        UInt64 v4 = br.ReadUInt64();
                        int nloop = ((int)(v4 >> 0) & 0x3FFF);

                        fs.Position = texinf1off + 144 * wx + 0x70;
                        UInt64 v5 = br.ReadUInt64();
                        int ilen = ((int)(v5 >> 0) & 0x3FFF);
                        int ioff = ((int)(v5 >> 32) & 0x7FFFFFFF);
                        Trace.Assert(nloop == ilen, "Invalid");

                        fs.Position = ioff;
                        byte[] ibin = new byte[16 * ilen];
                        int r = fs.Read(ibin, 0, 16 * ilen);

                        Trace.Assert(dpsm == 0);
                        int dbh = Convert.ToInt32(ibin.Length) / 8192 / dbw;
                        ibin = Reform32.Encode32(ibin, dbw, dbh);

                        Array.Copy(ibin, 0, gsram, 256 * dbp, 16 * ilen);

                        Console.Write("");
                    }
                }
                if (true) {
                    Debug.Assert(refal[wy] < wcx, "Invalid");

                    fs.Position = texinf2off + 160 * wy + 0x20;
                    UInt64 v0 = br.ReadUInt64();
                    Trace.Assert(v0 == 0, "Invalid");
                    Trace.Assert(br.ReadUInt64() == 0x3F, "Invalid");

                    fs.Position = texinf2off + 160 * wy + 0x30;
                    UInt64 v1 = br.ReadUInt64();
                    Trace.Assert(v1 == 0, "Invalid");
                    Trace.Assert(br.ReadUInt64() == 0x34, "Invalid");

                    fs.Position = texinf2off + 160 * wy + 0x40;
                    UInt64 v2 = br.ReadUInt64();
                    Trace.Assert(v2 == 0, "Invalid");
                    Trace.Assert(br.ReadUInt64() == 0x36, "Invalid");

                    fs.Position = texinf2off + 160 * wy + 0x50;
                    UInt64 v3 = br.ReadUInt64();
                    int psm = ((int)(v3 >> 0) & 0x3F);
                    int cbp = ((int)(v3 >> 37) & 0x3FFF);
                    int cpsm = ((int)(v3 >> 51) & 0xF);
                    int csm = ((int)(v3 >> 55) & 0x1);
                    int csa = ((int)(v3 >> 56) & 0x1F);
                    int cld = ((int)(v3 >> 61) & 0x7);
                    Trace.Assert(br.ReadUInt64() == 0x16, "Invalid");

                    fs.Position = texinf2off + 160 * wy + 0x70;
                    UInt64 v5 = br.ReadUInt64();
                    int tbp0 = ((int)(v5 >> 0) & 0x3FFF);
                    int tbw = ((int)(v5 >> 14) & 0x3F);
                    int psmX = ((int)(v5 >> 20) & 0x3F);
                    int tw = ((int)(v5 >> 26) & 0xF);
                    int th = ((int)(v5 >> 30) & 0xF);
                    int tcc = ((int)(v5 >> 34) & 0x1);
                    int tfx = ((int)(v5 >> 35) & 0x3);
                    int cbpX = ((int)(v5 >> 37) & 0x3FFF);
                    int cpsmX = ((int)(v5 >> 51) & 0xF);
                    int csmX = ((int)(v5 >> 55) & 0x1);
                    int csaX = ((int)(v5 >> 56) & 0x1F);
                    int cldX = ((int)(v5 >> 56) & 0x7);
                    Trace.Assert(br.ReadUInt64() == 0x06, "Invalid");
                    //TransUtil.Exist(texbuf, cbpX);
                    //Trace.Assert(texbuf.ContainsKey(cbpX), "Invalid");
                    //Trace.Assert(texbuf.ContainsKey(tbp0), "Invalid");
                    //Trace.Assert(cpsmX == 0, "Unsupported");
                    //Trace.Assert(csmX == 0, "Unsupported");
                    //Trace.Assert(csaX == 0, "Unsupported");

                    int sizetbp0 = (1 << tw) * (1 << th);
                    byte[] buftbp0 = new byte[sizetbp0];
                    Array.Copy(gsram, 256 * tbp0, buftbp0, 0, buftbp0.Length);
                    byte[] bufcbpX = new byte[8192];
                    Array.Copy(gsram, 256 * cbpX, bufcbpX, 0, bufcbpX.Length);

                    STim ipic = null;
                    if (psmX == 0x13) ipic = TexUtil.Decode8(buftbp0, bufcbpX, tbw, 1 << tw, 1 << th);
                    if (psmX == 0x14) ipic = TexUtil.Decode4(buftbp0, bufcbpX, tbw, 1 << tw, 1 << th);
                    alt.Add(ipic);
                }
            }
        }

        class TexUtil {
            public static STim Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                tbw /= 2;
                Debug.Assert(tbw != 0, "Invalid");
                byte[] bin = Reform8.Decode8(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }
                ColorPalette pals = pic.Palette;
                int psi = 0;

                byte[] palb2 = new byte[1024];
                for (int t = 0; t < 256; t++) {
                    int toi = vwBinTex2.KHcv8pal.repl(t);
                    Array.Copy(palbin, 4 * t + 0, palb2, 4 * toi + 0, 4);
                }
                Array.Copy(palb2, 0, palbin, 0, 1024);

                for (int t = 0; t < 256; t++) {
                    int toi = (t & 0xE7) | (((t & 0x10) != 0) ? 0x08 : 0) | (((t & 0x08) != 0) ? 0x10 : 0);
                    Array.Copy(palbin, 4 * t, palb2, toi * 4, 4);
                }
                Array.Copy(palb2, 0, palbin, 0, 1024);

                for (int t = 0; t < 256; t++) {
                    if (palbin[4 * t + 3] != 0x80) {
                        palbin[4 * t + 0] = 0;
                        palbin[4 * t + 1] = 0;
                        palbin[4 * t + 2] = 0;
                        palbin[4 * t + 3] = 0;
                    }
                }

                for (int pi = 0; pi < 256; pi++) {
                    pals.Entries[pi] = CUtil.Gamma(Color.FromArgb(
                        palbin[psi + 4 * pi + 3],
                        Math.Min(255, palbin[psi + 4 * pi + 0] + 1),
                        Math.Min(255, palbin[psi + 4 * pi + 1] + 1),
                        Math.Min(255, palbin[psi + 4 * pi + 2] + 1)
                        ), É¡);
                }
                pic.Palette = pals;
                //pic.Save("Éø.png", ImageFormat.Png);

                byte[] fixbin = new byte[128 * 128];
                for (int y = 0; y < 128; y++) {
                    for (int x = 0; x < 128; x++) {
                        fixbin[128 * y + x] = bin[(x * cx / 128) + cx * (y * cy / 128)];
                    }
                }
                return new STim(fixbin, palbin);
            }

            public static STim Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                tbw /= 2;
                Debug.Assert(tbw != 0, "Invalid");
                byte[] bin = Reform4.Decode4(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }
                ColorPalette pals = pic.Palette;
                int psi = 0;
                for (int pi = 0; pi < 16; pi++) {
                    pals.Entries[pi] = CUtil.Gamma(Color.FromArgb(
                        palbin[psi + 4 * pi + 3],
                        palbin[psi + 4 * pi + 0],
                        palbin[psi + 4 * pi + 1],
                        palbin[psi + 4 * pi + 2]
                        ), É¡);
                }
                pic.Palette = pals;

                byte[] bin8 = new byte[cx * cy];
                for (int y = 0; y < cy; y++) {
                    for (int x = 0; x < cx; x++) {
                        bin8[cx * y + x] = (byte)(((x & 1) == 0) ? (bin[(cx * y + x) >> 1] / 16) : (bin[(cx * y + x) >> 1] % 16));
                    }
                }
                byte[] fixbin = new byte[128 * 128];
                for (int y = 0; y < 128; y++) {
                    for (int x = 0; x < 128; x++) {
                        fixbin[128 * y + x] = bin8[(x * cx / 128) + cx * (y * cy / 128)];
                    }
                }
                return new STim(fixbin, palbin);
            }
        }

        public const float É¡ = 0.5f;

        class CUtil {
            public static Color Gamma(Color a, float gamma) {
                return Color.FromArgb(
                    255,//a.A,
                    Math.Min(255, (int)(Math.Pow(a.R / 255.0, gamma) * 255.0)),
                    Math.Min(255, (int)(Math.Pow(a.G / 255.0, gamma) * 255.0)),
                    Math.Min(255, (int)(Math.Pow(a.B / 255.0, gamma) * 255.0))
                    );
            }
        }
    }
}
