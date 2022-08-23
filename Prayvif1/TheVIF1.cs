using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Prayvif1 {
    public class VIF1 {
        byte[] vifmask = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        uint[] colval = new uint[4];
        uint[] rowval = new uint[4];
        int tops = 0;
        int baseval = 0;
        int offset = 0;
        int itop = 0;
        int top = 0;
        int mode = 0;
        bool DBF = false;

        TextWriter wr;
        GSim gsim;
        VPU1Sim v1;
        Outxml ox;

        public bool RunMicroProgram = true;

        public VIF1(GSim gsim, VPU1Sim v1, TextWriter wr, Outxml ox) {
            this.gsim = gsim;
            this.v1 = v1;
            this.wr = wr;
            this.ox = ox;
        }

        public void sim1ce(Stream vfs) {
            BinaryReader br = new BinaryReader(vfs);

            while (true) {
                uint v;
                try {
                    v = br.ReadUInt32();
                }
                catch (EndOfStreamException) {
                    break;
                }
                byte cmd = (byte)((v >> 24) & 0x7F);
                wr.Write("# " + CmdUt.About(cmd));

                switch (cmd) {
                    case 0x00: { // nop
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), "");
                            break;
                        }
                    case 0x01: { // stcycl
                            int cl = ((int)(v >> 0) & 0xFF);
                            int wl = ((int)(v >> 8) & 0xFF);
                            wr.Write(" cl " + cl + " wl " + wl);
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " cl " + cl + " wl " + wl);
                            break;
                        }
                    case 0x02: { // offset
                            offset = ((int)(v) & 0x3FF);
                            DBF = false;
                            wr.Write(" offset " + offset.ToString("x3"));
                            wr.WriteLine();
                            v1.SetTop(tops = baseval);
                            ox.Startvif(CmdUt.About(cmd), " offset " + offset.ToString("x3"));
                            break;
                        }
                    case 0x03: { // base
                            baseval = ((int)(v) & 0x3FF);
                            wr.Write(" base " + baseval.ToString("x3"));
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " base " + baseval.ToString("x3"));
                            break;
                        }
                    case 0x04: { // itop
#if true
                            throw new NotSupportedException("itop");
#else
                            itops = ((int)(v) & 0x3FF);
                            wr.Write(" itops " + itops.ToString("x3"));
                            wr.WriteLine();
                            break;
#endif
                        }
                    case 0x05: { // stmod
                            mode = ((int)(v) & 3);
                            wr.Write(" mode " + mode);
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " mode " + mode);
                            break;
                        }
                    case 0x06: { // mskpath3
                            int mask = ((int)(v >> 15) & 1);
                            wr.Write(" mask " + mask);
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " mask " + mask);
                            break;
                        }
                    case 0x07: { // mark
                            int mark = ((int)(v) & 0xFFFF);
                            wr.Write(" mark " + mark.ToString("x4"));
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " mark " + mark.ToString("x4"));
                            break;
                        }
                    case 0x10: { // flushe
                            ox.Startvif(CmdUt.About(cmd), "");
                            wr.WriteLine();
                            break;
                        }
                    case 0x11: { // flush
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), "");
                            break;
                        }
                    case 0x13: { // flusha
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), "");
                            break;
                        }
                    case 0x14: { // mscal
                            {
                                itop = tops;
                                v1.SetTop(top = tops & 0x03FF);
                                if (DBF) {
                                    tops = baseval;
                                    DBF = false;
                                }
                                else {
                                    tops = baseval + offset;
                                    DBF = true;
                                }
                            }
                            int addr = ((int)(v) & 0xFFFF);
                            wr.Write(" execaddr " + addr.ToString("x4"));
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " execaddr " + addr.ToString("x4"));
                            if (RunMicroProgram) v1.Activate(addr);
                            break;
                        }
                    case 0x17: { // mscnt
                            {
                                itop = tops;
                                v1.SetTop(top = tops & 0x03FF);
                                if (DBF) {
                                    tops = baseval;
                                    DBF = false;
                                }
                                else {
                                    tops = baseval + offset;
                                    DBF = true;
                                }
                            }
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), "");
                            if (RunMicroProgram) v1.Activate();
                            break;
                        }
                    case 0x15: { // mscalf
#if true
                            throw new NotSupportedException("mscalf");
#else
                            int addr = ((int)(v) & 0xFFFF);
                            wr.Write(" execaddr " + addr.ToString("x4"));
                            wr.WriteLine();
                            v1.Activate(addr);
                            v1.SetTop(tops = baseval + offset);
                            break;
#endif
                        }
                    case 0x20: { // stmask
                            uint r1 = br.ReadUInt32();
                            for (int x = 0; x < 16; x++) {
                                vifmask[x] = (byte)(((int)(r1 >> (2 * x))) & 3);
                            }
                            wr.Write(" " + r1.ToString("x8"));
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " " + r1.ToString("x8"));
                            break;
                        }
                    case 0x30: { // strow
                            rowval[0] = br.ReadUInt32();
                            rowval[1] = br.ReadUInt32();
                            rowval[2] = br.ReadUInt32();
                            rowval[3] = br.ReadUInt32();
                            wr.Write(""
                                + " " + rowval[0].ToString("x8")
                                + " " + rowval[1].ToString("x8")
                                + " " + rowval[2].ToString("x8")
                                + " " + rowval[3].ToString("x8")
                                );
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), ""
                                + " " + rowval[0].ToString("x8")
                                + " " + rowval[1].ToString("x8")
                                + " " + rowval[2].ToString("x8")
                                + " " + rowval[3].ToString("x8"));
                            break;
                        }
                    case 0x31: { // stcol
                            colval[0] = br.ReadUInt32();
                            colval[1] = br.ReadUInt32();
                            colval[2] = br.ReadUInt32();
                            colval[3] = br.ReadUInt32();
                            wr.Write(""
                                + " " + colval[0].ToString("x8")
                                + " " + colval[1].ToString("x8")
                                + " " + colval[2].ToString("x8")
                                + " " + colval[3].ToString("x8")
                                );
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), ""
                                + " " + colval[0].ToString("x8")
                                + " " + colval[1].ToString("x8")
                                + " " + colval[2].ToString("x8")
                                + " " + colval[3].ToString("x8"));
                            break;
                        }
                    case 0x4A: { // mpg
                            int size = ((int)(v >> 16) & 0xFF);
                            if (size == 0) size = 256;
                            int addr = ((int)(v) & 0xFFFF);
                            wr.Write(" size " + size);
                            wr.Write(" addr " + addr.ToString("x4"));
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " size " + size + " addr " + addr.ToString("x4"));
                            byte[] bin = br.ReadBytes(4 + 8 * size);
                            MemoryStream pwr = new MemoryStream(v1.Micro, true);
                            pwr.Position = 8 * addr;
                            pwr.Write(bin, 0, 8 * size);
                            v1.Hack();
                            v1.Clear();
                            break;
                        }
                    case 0x50: { // direct
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            wr.Write(" imm " + imm);
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " imm " + imm);
                            byte[] bin = br.ReadBytes(16 * imm);
                            gsim.Transfer2(bin);
                            break;
                        }
                    case 0x51: { // directhl
                            int imm = ((int)(v >> 0) & 0xFFFF);
                            wr.Write(" imm " + imm);
                            wr.WriteLine();
                            ox.Startvif(CmdUt.About(cmd), " imm " + imm);
                            byte[] bin = br.ReadBytes(16 * imm);
                            gsim.Transfer2(bin);
                            break;
                        }
                    default: { // unpack or ?
                            if (0x60 == (cmd & 0x60)) {
                                //Debug.WriteLine("# U");
                                int m = ((int)(cmd >> 4) & 1);
                                int vn = ((int)(cmd >> 2) & 0x3);
                                int vl = ((int)(cmd >> 0) & 0x3);

                                int size = ((int)(v >> 16) & 0xFF);
                                int flg = ((int)(v >> 15) & 1);
                                int usn = ((int)(v >> 14) & 1);
                                int addr = ((int)(v >> 0) & 0x3FF);

                                string wrs = "";
                                wrs += ((m != 0) ? " Masked" : " Unmasked");
                                wrs += ((usn != 0) ? " Unsigned" : " Signed");
                                if (flg != 0) wrs += (" addTops");
                                switch (cmd & 15) {
                                    case 0x0: wrs += (" S-32"); break;
                                    case 0x1: wrs += (" S-16"); break;
                                    case 0x2: wrs += (" S-8"); break;
                                    case 0x4: wrs += (" V2-32"); break;
                                    case 0x5: wrs += (" V2-16"); break;
                                    case 0x6: wrs += (" V2-8"); break;
                                    case 0x8: wrs += (" V3-32"); break;
                                    case 0x9: wrs += (" V3-16"); break;
                                    case 0xA: wrs += (" V4-8"); break;
                                    case 0xC: wrs += (" V4-32"); break;
                                    case 0xD: wrs += (" V4-16"); break;
                                    case 0xE: wrs += (" V4-8"); break;
                                    case 0xF: wrs += (" V4-5"); break;
                                    default: wrs += (" ?"); break;
                                }
                                wrs += (" addr " + addr.ToString("x3"));
                                wrs += (" qwc " + size);
                                wr.WriteLine(wrs);
                                ox.Startvif(CmdUt.About(cmd), wrs);

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
                                int newPos = Convert.ToInt32(vfs.Position) + cbTotal;

                                MemoryStream os = new MemoryStream(v1.Mem, true);
                                BinaryWriter pwr = new BinaryWriter(os);

                                os.Position = 16 * (addr + ((flg != 0) ? tops : 0));
                                bool nomask = (m == 0) ? true : false;

                                Reader reader = new Reader(br, vl, usn);
                                for (int y = 0; y < size; y++) {
                                    uint tmpv;
                                    switch (vn) {
                                        default:
                                            throw new NotSupportedException();
                                        case 0: // S-XX
                                            reader.Read(1);
                                            tmpv = reader.Vx;
                                            // X
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            switch (vifmask[(y & 3) * 4 + 3] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[3]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            break;
                                        case 1: // V2-XX
                                            reader.Read(1 | 2);
                                            // X
                                            tmpv = reader.Vx;
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Vy;
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            os.Position += 4;
                                            // W
                                            os.Position += 4;
                                            break;
                                        case 2: // V3-XX
                                            reader.Read(1 | 2 | 4);
                                            // X
                                            tmpv = reader.Vx;
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Vy;
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            tmpv = reader.Vz;
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            os.Position += 4;
                                            break;
                                        case 3: // V4-XX
                                            reader.Read(1 | 2 | 4 | 8);
                                            // X
                                            tmpv = reader.Vx;
                                            switch (vifmask[(y & 3) * 4 + 0] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[0]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Y
                                            tmpv = reader.Vy;
                                            switch (vifmask[(y & 3) * 4 + 1] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[1]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // Z
                                            tmpv = reader.Vz;
                                            switch (vifmask[(y & 3) * 4 + 2] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[2]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            // W
                                            tmpv = reader.Vw;
                                            switch (vifmask[(y & 3) * 4 + 3] & (nomask ? 0 : 7)) {
                                                case 0: pwr.Write(tmpv); break;
                                                case 1: pwr.Write(rowval[y & 3]); break;
                                                case 2: pwr.Write(colval[3]); break;
                                                case 3: os.Position += 4; break;
                                            }
                                            break;
                                    }
                                }

                                br.ReadBytes(newPos - Convert.ToInt32(vfs.Position));
                                break;
                            }
                            else {
                                Debug.Fail("?");
                            }
                            break;
                        }
                }
            }
        }


        class Reader {
            BinaryReader br;
            int vl;
            bool usn;

            public uint Vx = 0, Vy = 0, Vz = 0, Vw = 0;

            public Reader(BinaryReader br, int vl, int usn) {
                this.br = br;
                this.vl = vl;
                this.usn = (usn != 0) ? true : false;
            }
            public void Read(int mask) {
                if (vl == 3) {
                    uint r = br.ReadUInt16(); br.ReadUInt16();
                    Vx = ((r >> 0) & 0x1FU) << 3;
                    Vy = ((r >> 5) & 0x1FU) << 3;
                    Vz = ((r >> 10) & 0x1FU) << 3;
                    Vw = ((r >> 15) & 1U) << 7;
                }
                else {
                    if (0 != (mask & 1)) { Vx = Read(); }
                    if (0 != (mask & 2)) { Vy = Read(); }
                    if (0 != (mask & 4)) { Vz = Read(); }
                    if (0 != (mask & 8)) { Vw = Read(); }
                }
            }
            public uint Read() {
                switch (vl) {
                    case 0: // 32 bits
                        return br.ReadUInt32();
                    case 1: // 16 bits
                        if (usn) return br.ReadUInt16();
                        return (uint)(short)br.ReadInt16();
                    case 2: // 8 bits
                        if (usn) return br.ReadByte();
                        return (uint)(sbyte)br.ReadSByte();
                    case 3: // 5+5+5+1 bits
                    default:
                        throw new NotSupportedException("vl(" + vl + ")");
                }
            }
        }
    }
}
