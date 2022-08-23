using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Prayvif1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        VifwrStream2 vfs;

        private void Form1_Load(object sender, EventArgs e) {
            using (StreamWriter wr = new StreamWriter("vifexec.txt", false, Encoding.UTF8)) {
                using (StreamWriter wrgs = new StreamWriter("wrgs.txt", false, Encoding.UTF8)) {
                    wr.AutoFlush = true;
                    wrgs.AutoFlush = true;
                    GSWr gswr = new GSWr(wrgs);
                    GSim gsim = new GSim(wr, gswr, ox);
                    VPU1Sim v1 = new VPU1Sim(gsim, wr, gswr);
                    VIF1 vif1 = new VIF1(gsim, v1, wr, ox);
                    vif1.RunMicroProgram = true;

                    if (false) {
                        using (FileStream fsin = File.OpenRead(@"H:\EMU\pcsx2-0.9.4\myrec\gif1t_892.bin")) {
                            ox.Startfile("gif1t_892.bin");
                            fsin.Position = 0x400;
                            BinaryReader br = new BinaryReader(fsin);
                            while (fsin.Position < fsin.Length) {
                                uint tag = br.ReadUInt32();
                                if (tag == 0xfffe0000u) {
                                    fsin.Seek(12, SeekOrigin.Current);
                                    ox.StartDMAc(0, 0, "");
                                }
                                else if (tag == 0xfffc0000u) {
                                    int cb = br.ReadInt32();
                                    br.ReadInt32();
                                    br.ReadInt32();
                                    byte[] gift = br.ReadBytes(cb);
                                    gsim.Transfer1(gift);
                                }
                            }
                            ox.Endvif();
                        }
                    }
                    else {
                        List<string> alfn = new List<string>();

                        //alfn.Add("vifwr2_078.bin");
                        //for (int x = 79; x <= 154; x++) alfn.Add(string.Format("vifwr2_{0:000}.bin", x));
                        for (int x = 1; x <= 50; x++) alfn.Add(string.Format("vifwr2_891_{0:000}.bin", x));

                        foreach (string fn in alfn) {
                            //%wr.WriteLine("! " + fn);
                            ox.Startfile(fn);
                            // @"H:\Proj\khkh_xldM\MEMO\vifwr2"
                            using (FileStream fs = File.OpenRead(Path.Combine(@"H:\EMU\pcsx2-0.9.4\myrec", fn))) {
                                Debug.WriteLine("----- " + fn);
                                vfs = new VifwrStream2(fs, wr, ox);
                                vif1.sim1ce(vfs);
                            }
                        }
                    }
                    ox.Save("out.xml");
                }
            }
        }

        Outxml ox = new Outxml();
    }

    class VifwrStream2 : Stream {
        TextWriter wr;
        Outxml ox;

        public VifwrStream2(Stream fs, TextWriter wr, Outxml ox)
            : base() {
            this.fs = fs;
            this.wr = wr;
            this.ox = ox;

            byte[] hdr = new byte[1024];
            if (fs.Read(hdr, 0, 1024) != 1024) throw new EndOfStreamException();
            int r = Array.IndexOf(hdr, (byte)0x1a);
            string[] rows = Encoding.ASCII.GetString(hdr, 0, r).Split('\n');
            if (!rows[0].Equals("#!/bin/parse_haxkhkh_Trace_DMA_vif1_v2")) throw new NotSupportedException();
            for (int t = 1; t < rows.Length; t++) {
                string[] cols = rows[t].Split('=');
                if (cols[0].Equals("tadr")) {
                    tadr = Convert.ToUInt32(cols[1], 16);
                }
            }
            while (readDMAC()) ;
        }

        Stream fs;
        int cur = 0;
        uint tadr = uint.MaxValue;

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() {
            throw new Exception("The method or operation is not implemented.");
        }

        public override long Length {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Position {
            get {
                return cur;
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        int id;
        uint qwc;
        uint addr;
        bool isfine = false;
        Queue<uint> asr = new Queue<uint>();

        class Pkt {
            public uint x;
            public uint cx;
            public int ty;

            public Pkt(uint x, uint cx, int ty) {
                this.x = x;
                this.cx = cx;
                this.ty = ty;
            }
        }
        Queue<Pkt> pq = new Queue<Pkt>();

        const int BASE = 1024;

        uint tforma(uint tadr) {
            return (0 != (tadr & 0x80000000)) ? 32 * 1024 * 1024 + (tadr & 0x3FFF) : tadr;
        }

        bool readDMAC() {
            if (isfine) return false;

            BinaryReader br = new BinaryReader(fs);
            fs.Position = BASE + tforma(tadr);
            UInt64 tag = br.ReadUInt64();
            id = ((int)(tag >> 28)) & 7;
            qwc = ((uint)(tag)) & 65535;
            addr = ((uint)(tag >> 32));

            pq.Enqueue(new Pkt(tadr + 8, 8, 8));
            switch (id) {
                case 0: // refe
                    pq.Enqueue(new Pkt(addr, 16U * qwc, 0));
                    isfine = true; tadr = uint.MaxValue - 1;
                    break;
                case 1: // cnt
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 1));
                    tadr += 16U + 16U * qwc;
                    break;
                case 2: // next
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 2));
                    tadr = addr;
                    break;
                case 3: // ref
                    pq.Enqueue(new Pkt(addr, 16 * qwc, 3));
                    tadr += 16;
                    break;
                case 4: // refs
                    pq.Enqueue(new Pkt(addr, 16 * qwc, 4));
                    tadr += 16;
                    break;
                case 5: // call
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 5));
                    asr.Enqueue((uint)(tadr + 16 + 16 * qwc));
                    tadr = addr;
                    break;
                case 6: // ret
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 6));
                    if (asr.Count == 0) {
                        isfine = true; tadr = uint.MaxValue - 1;
                    }
                    else {
                        tadr = asr.Dequeue();
                    }
                    break;
                case 7: // end
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 7));
                    isfine = true; tadr = uint.MaxValue - 1;
                    break;
                default:
                    throw new NotSupportedException("DMAc.id " + id);
            }
            return true;
        }

        Pkt lastp = null;

        public override int Read(byte[] buffer, int offset, int count) {
            BinaryReader br = new BinaryReader(fs);
            int _offset = offset;
            int len = 0;
            while (count > 0) {
                if (pq.Count == 0)
                    break;
                Pkt p;
                if (lastp == null) {
                    lastp = p = pq.Peek();
                    ox.StartDMAc(p.x, p.cx, "refe,cnt,next,ref,refs,call,ret,end,DMAc8".Split(',')[p.ty]);
                }
                else {
                    p = lastp;
                }
                int cx = Math.Min((int)p.cx, count);
                fs.Position = BASE + tforma(p.x);
                if (fs.Read(buffer, offset, cx) != cx) throw new EndOfStreamException();
                offset += cx;
                count -= cx;
                p.x += (uint)cx;
                p.cx -= (uint)cx;
                cur += cx;
                len += cx;
                if (p.cx == 0) { pq.Dequeue(); lastp = null; }
            }
            ox.Readfile(buffer, _offset, len);
            return len;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class VifwrStream : Stream {
        public VifwrStream(Stream fs)
            : base() {
            this.fs = fs;
        }

        Stream fs;
        int cur = 0;

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() {
            throw new Exception("The method or operation is not implemented.");
        }

        public override long Length {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Position {
            get {
                return cur;
            }
            set {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int rest = 0;

        public override int Read(byte[] buffer, int offset, int count) {
            BinaryReader br = new BinaryReader(fs);
            int pos = 0;
            int maxpos = count;
            while (fs.Position != fs.Length && pos != maxpos) {
                while (rest == 0) {
                    int a = br.ReadInt32();
                    if ((uint)a == 0xFDFFFEFF) {

                    }
                    else {
                        rest = a;
                    }
                }
                if (rest != 0) {
                    int cbRead = Math.Min(maxpos - pos, rest);
                    int r = fs.Read(buffer, offset + pos, cbRead);
                    if (r == 0) break;
                    Trace.Assert(r >= 0, "r >= 0");
                    cur += r;
                    pos += r;
                    rest -= r;
                }
            }
            return pos;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value) {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    static class CmdUt {
        public static string About(byte cmd) {
            string s = "?";
            switch (cmd) {
                case 0: s = "nop"; break;
                case 1: s = "stcycl"; break;
                case 2: s = "offset"; break;
                case 3: s = "base"; break;
                case 4: s = "itop"; break;
                case 5: s = "stmod"; break;
                case 6: s = "mskpath3"; break;
                case 7: s = "mark"; break;
                case 16: s = "flushe"; break;
                case 17: s = "flush"; break;
                case 19: s = "flusha"; break;
                case 20: s = "mscal"; break;
                case 23: s = "mscnt"; break;
                case 21: s = "mscalf"; break;
                case 32: s = "stmask"; break;
                case 48: s = "strow"; break;
                case 49: s = "stcol"; break;
                case 74: s = "mpg"; break;
                case 80: s = "direct"; break;
                case 81: s = "directhl"; break;
            }
            if ((cmd & 0x60) == 0x60) s = "unpack";
            return cmd.ToString("X2") + " " + s;
        }
    }
}
