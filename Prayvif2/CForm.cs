using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Prayvif1;
using System.Threading;

namespace Prayvif2 {
    public partial class CForm : Form {
        public CForm() {
            InitializeComponent();
        }

        TcpListener tl = null;

        private void CForm_Load(object sender, EventArgs e) {
            tl = new TcpListener(new IPEndPoint(IPAddress.Loopback, 13131));
            tl.Start();

            tl2 = new TcpListener(new IPEndPoint(IPAddress.Loopback, 13132));
            tl2.Start();
        }

        bool allow = true;

        private void timer1_Tick(object sender, EventArgs e) {
            if (!tl.Pending()) return;
            if (!allow) return;
            {
                int cntaccept = (int)numaccept.Value;
                if (cntaccept < 1) return;
                numaccept.Value = cntaccept - 1;
            }
            using (WIP wip = WIP.Show(this)) {
                allow = false;
                using (TcpClient tc = tl.AcceptTcpClient()) {
                    NetworkStream si = tc.GetStream();
                    BinaryReader br = new BinaryReader(si);
                    String[] rows = Encoding.ASCII.GetString(br.ReadBytes(1024)).Split('\x1a')[0].Split('\n');
                    if (rows[0] != "#!Prayvif2")
                        throw new NotSupportedException(rows[0]);
                    uint tadr = 0;
                    foreach (String row in rows) {
                        String[] cols = row.Split('=');
                        if (cols[0] == "tadr" && cols.Length == 2) {
                            tadr = Convert.ToUInt32(cols[1], 16);
                        }
                    }
                    if (tadr != 0) {
                        sicomm = new MemReadStream(si);
                        brcomm = new BinaryReader(sicomm);

                        Outxml ox = new Outxml();
                        ox.Startfile("out.xml", Environment.CurrentDirectory);

                        using (StreamWriter wr = new StreamWriter("vifexec.txt", false, Encoding.UTF8)) {
                            using (StreamWriter wrgs = new StreamWriter("wrgs.txt", false, Encoding.UTF8)) {
                                GSim gsim = new GSim(wr, new BPyWriter(), ox);
                                VPU1Sim v1 = new VPU1Sim(gsim, wr);
                                VIF1 vif1 = new VIF1(gsim, v1, wr, ox);

                                vfs = new VifwrStream3(sicomm, tadr, wr, ox);
                                vif1.sim1ce(vfs);
                            }
                        }
                        ox.Save("out.xml");
                    }
                }
                allow = true;
            }
        }

        MemReadStream sicomm;
        BinaryReader brcomm;

        VifwrStream3 vfs;

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

        bool isfine = false;
        uint tadr = uint.MaxValue;
        int id;
        uint qwc;
        uint addr;
        Queue<uint> asr = new Queue<uint>();

        bool readDMAC() {
            if (isfine) return false;

            sicomm.Position = tadr;
            UInt64 tag = brcomm.ReadUInt64();
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

        class MemReadStream : Stream {
            NetworkStream sock;

            public MemReadStream(NetworkStream sock) {
                this.sock = sock;
            }

            public override bool CanRead {
                get { return true; }
            }

            public override bool CanSeek {
                get { return true; }
            }

            public override bool CanWrite {
                get { return false; }
            }

            public override void Flush() { }

            public override long Length {
                get { return 0xffffffff; }
            }

            Int64 pos = 0;

            public override long Position {
                get {
                    return pos;
                }
                set {
                    pos = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count) {
                if (count < 0) throw new ArgumentOutOfRangeException("count");

                MemoryStream os = new MemoryStream(16);
                BinaryWriter wr = new BinaryWriter(os);
                wr.Write((byte)'R');
                wr.Write((byte)' ');
                wr.Write((byte)' ');
                wr.Write((byte)' ');
                wr.Write(Convert.ToUInt32(pos));
                wr.Write(Convert.ToUInt32(count));
                wr.Write((int)0);
                byte[] req = os.ToArray();

                sock.Write(req, 0, 16);

                int res = count;

                while (count > 0) {
                    int r = sock.Read(buffer, offset, count);
                    if (r < 1) throw new EndOfStreamException();
                    offset += r;
                    count -= r;
                }
                return res;
            }

            public override long Seek(long offset, SeekOrigin origin) {
                switch (origin) {
                    case SeekOrigin.Begin: pos = offset; return pos;
                    case SeekOrigin.Current: pos += offset; return pos;
                }
                throw new Exception("The method or operation is not implemented.");
            }

            public override void SetLength(long value) { throw new Exception("The method or operation is not implemented."); }

            public override void Write(byte[] buffer, int offset, int count) { throw new Exception("The method or operation is not implemented."); }
        }

        private void buttonParseTTrap_Click(object sender, EventArgs e) {
            using (WIP wip = WIP.Show(this)) {
                String newdir = Path.Combine(Application.StartupPath, "ttrap");
                Directory.CreateDirectory(newdir);
                Environment.CurrentDirectory = newdir;

                Outxml ox = new Outxml();
                ox.Startfile("out.xml", Environment.CurrentDirectory);

                using (StreamWriter wr = new StreamWriter("vifexec.txt", false, Encoding.UTF8)) {
                    using (StreamWriter wrgs = new StreamWriter("wrgs.txt", false, Encoding.UTF8)) {
                        using (StreamWriter wrpy = new StreamWriter("b.py", false, Encoding.ASCII)) {
                            BPyWriter bpy = new BPyWriter(wrpy);
                            GSim gsim = new GSim(wr, bpy, ox);

                            using (FileStream fs = File.OpenRead(tbTTrap.Text)) {
                                BinaryReader br = new BinaryReader(fs);
                                String[] rows = Encoding.ASCII.GetString(br.ReadBytes(1024)).Split((char)0x1a)[0].Split('\n');
                                if (rows.Length < 2 || rows[0] != "#!/usr/bin/Parse_GIFdma_trap") throw new InvalidDataException();
                                int off = -1;
                                foreach (String row in rows) {
                                    if (row.StartsWith("#")) continue;
                                    String[] cols = row.Split('=');
                                    if (cols.Length != 2) continue;
                                    if (cols[0] == "offset") off = int.Parse(cols[1]);
                                }
                                if (off == -1) throw new InvalidDataException();

                                fs.Position = off;

                                int bini = 0;

                                while (fs.Position < fs.Length) {
                                    uint tag = br.ReadUInt32();
                                    uint way = br.ReadUInt32();
                                    uint a = br.ReadUInt32();
                                    uint qwc = br.ReadUInt32();
                                    byte[] bin = br.ReadBytes(Convert.ToInt32(16 * qwc));
                                    ox.StartGIFdma(a, qwc, bin);
                                    String fn = String.Format("gif{0:0000}", bini); bini++;
                                    bpy.StartMesh(fn);
                                    gsim.Transfer3(bin);
                                    bpy.EndMesh();
                                }
                            }
                        }
                    }
                }
                ox.Save("out.xml");
            }
        }

        TcpListener tl2;

        class GSiServ {
            TcpClient tcp;
            Thread thread;
            String dir;

            public GSiServ(String dir, TcpClient tcp) {
                this.dir = dir;
                this.tcp = tcp;
                this.thread = new Thread(Run);
                this.thread.Start();
            }

            public void Run() {
                Environment.CurrentDirectory = dir;
                try {
                    NetworkStream si = tcp.GetStream();
                    BinaryReader br = new BinaryReader(si);

                    Outxml ox = new Outxml();
                    ox.Startfile("out.xml", Environment.CurrentDirectory);
                    try {

                        using (StreamWriter wr = new StreamWriter("vifexec.txt", false, Encoding.UTF8)) {
                            using (StreamWriter wrgs = new StreamWriter("wrgs.txt", false, Encoding.UTF8)) {
                                using (StreamWriter wrpy = new StreamWriter("b.py", false, Encoding.ASCII)) {
                                    BPyWriter bpy = new BPyWriter(wrpy);
                                    GSim gsim = new GSim(wr, bpy, ox);
                                    try {
                                        int bini = 0;
                                        while (true) {
                                            uint command = br.ReadUInt32();
                                            if (command == 1) {
                                                uint addr = br.ReadUInt32();
                                                uint cb = br.ReadUInt32();
                                                byte[] bin = br.ReadBytes(Convert.ToInt32(cb));

                                                ox.StartGIFdma(addr, cb >> 4, bin);
                                                String fn = String.Format("gif{0:0000}", bini); bini++;
                                                bpy.StartMesh(fn);
                                                gsim.Transfer3(bin);
                                                bpy.EndMesh();
                                            }
                                            else if (command == 2) {
                                                uint x = br.ReadUInt32(); // addr
                                                uint cx = br.ReadUInt32(); // bytes
                                                uint ty = br.ReadUInt32();
                                                uint tadr = br.ReadUInt32();
                                                byte[] bin = br.ReadBytes(Convert.ToInt32(cx));

                                                ox.ParseDMAc(x, cx, DMATagTypes[ty], tadr);

                                                ox.StartGIFdma(x, cx >> 4, bin);
                                                String fn = String.Format("gif{0:0000}", bini); bini++;
                                                bpy.StartMesh(fn);
                                                gsim.Transfer3(bin);
                                                bpy.EndMesh();
                                            }
                                            else throw new InvalidDataException();
                                        }
                                    }
                                    catch (EndOfStreamException) {

                                    }
                                    catch (InvalidDataException) {

                                    }
                                }
                            }
                        }

                    }
                    finally {
                        ox.Save("out.xml");
                    }
                }
                finally {
                    tcp.Close();
                }
            }
        }

        static string[] DMATagTypes = "refe,cnt,next,ref,refs,call,ret,end,upper64".Split(',');

        int diri = 0;

        private void timer2_Tick(object sender, EventArgs e) {
            if (tl2 == null) return;

            if (!tl2.Pending()) return;

            String dir = Path.Combine(Application.StartupPath,
                String.Format("tcp13132\\{0:yyyyMMdd.HHmmss}.{1:00}", DateTime.Now, diri)
                );
            diri++;
            Directory.CreateDirectory(dir);

            TcpClient tcp = tl2.AcceptTcpClient();
            GSiServ serv = new GSiServ(dir, tcp);
        }
    }

    class VifwrStream3 : Stream {
        TextWriter wr;
        Outxml ox;

        public VifwrStream3(Stream fs, uint tadr, TextWriter wr, Outxml ox)
            : base() {
            this.fs = fs;
            this.tadr = tadr;
            this.wr = wr;
            this.ox = ox;

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
            public uint x, tadr;
            public uint cx;
            public int ty;

            public Pkt(uint x, uint cx, int ty, uint tadr) {
                this.x = x;
                this.cx = cx;
                this.ty = ty;
                this.tadr = tadr;
            }
        }
        Queue<Pkt> pq = new Queue<Pkt>();

        const int BASE = 0; // 1024;

        uint tforma(uint tadr) {
            // return (0 != (tadr & 0x80000000)) ? 32 * 1024 * 1024 + (tadr & 0x3FFF) : tadr;
            return tadr;
        }

        bool readDMAC() {
            if (isfine) return false;

            BinaryReader br = new BinaryReader(fs);
            fs.Position = BASE + tforma(tadr);
            UInt64 tag = br.ReadUInt64();
            id = ((int)(tag >> 28)) & 7;
            qwc = ((uint)(tag)) & 65535;
            addr = ((uint)(tag >> 32));

            pq.Enqueue(new Pkt(tadr + 8, 8, 8, tadr));
            switch (id) {
                case 0: // refe
                    pq.Enqueue(new Pkt(addr, 16U * qwc, 0, tadr));
                    isfine = true; tadr = uint.MaxValue - 1;
                    break;
                case 1: // cnt
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 1, tadr));
                    tadr += 16U + 16U * qwc;
                    break;
                case 2: // next
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 2, tadr));
                    tadr = addr;
                    break;
                case 3: // ref
                    pq.Enqueue(new Pkt(addr, 16 * qwc, 3, tadr));
                    tadr += 16;
                    break;
                case 4: // refs
                    pq.Enqueue(new Pkt(addr, 16 * qwc, 4, tadr));
                    tadr += 16;
                    break;
                case 5: // call
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 5, tadr));
                    asr.Enqueue((uint)(tadr + 16 + 16 * qwc));
                    tadr = addr;
                    break;
                case 6: // ret
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 6, tadr));
                    if (asr.Count == 0) {
                        isfine = true; tadr = uint.MaxValue - 1;
                    }
                    else {
                        tadr = asr.Dequeue();
                    }
                    break;
                case 7: // end
                    pq.Enqueue(new Pkt(tadr + 16, 16 * qwc, 7, tadr));
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
                    ox.ParseDMAc(p.x, p.cx, "refe,cnt,next,ref,refs,call,ret,end,upper64".Split(',')[p.ty], p.tadr);
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
            //ox.Readfile(buffer, _offset, len);
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
}