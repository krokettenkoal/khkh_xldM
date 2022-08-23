using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using ee1Dec;

namespace traceHLP3 {
    public partial class ParserHaxkh2fm : Component {
        public ParserHaxkh2fm() {
            CTor();
            InitializeComponent();
        }

        private void CTor() {
            breeram = new BinaryReader(sieeram = new MemoryStream(eeram, true));
            brREGs = new BinaryReader(siREGs = new MemoryStream(regs, true));
        }

        public ParserHaxkh2fm(IContainer container) {
            CTor();
            container.Add(this);

            InitializeComponent();
        }

        public byte[] eeram = new byte[32 * 1024 * 1024];
        byte[] regs = new byte[512];

        public MemoryStream sieeram;
        MemoryStream siREGs;

        public BinaryReader breeram;
        BinaryReader brREGs;

        public event EventHandler REGChanged;

        public void Parse(String fp) {
            using (FileStream fs = File.OpenRead(fp)) {
                BinaryReader br = new BinaryReader(fs);
                String[] alrow = Encoding.GetEncoding(932).GetString(br.ReadBytes(1024)).Split('\x1a')[0].Split('\n');
                if (alrow[0] != "#!/usr/bin/parseHaxkh2fm") throw new InvalidDataException();
                int off_eeram = -1, off_regs = -1;
                uint pc = 0;
                for (int y = 1; y < alrow.Length; y++) {
                    String[] alcol = alrow[y].Split('=');
                    if (alcol.Length != 2) continue;
                    if (alcol[0] == "off_eeram") off_eeram = Convert.ToInt32(alcol[1]);
                    if (alcol[0] == "off_regs") off_regs = Convert.ToInt32(alcol[1]);
                    if (alcol[0] == "pc") pc = Convert.ToUInt32(alcol[1]);
                }
                if (off_eeram < 0) throw new FileNotFoundException("off_eeram");
                if (off_regs < 0) throw new FileNotFoundException("off_regs");
                if (pc < 0) throw new FileNotFoundException("pc");

                fs.Position = off_eeram;
                if (fs.Read(eeram, 0, eeram.Length) != eeram.Length) throw new EndOfStreamException("eeram");

                fs.Position = off_regs;
                if (fs.Read(regs, 0, regs.Length) != regs.Length) throw new EndOfStreamException("regs");

                this.pc = pc;

                fs.Close();
            }
            if (REGChanged != null)
                REGChanged(this, new EventArgs());
        }

        public String GetDisarm(uint off) {
            try {
                sieeram.Position = off;
                uint word = breeram.ReadUInt32();
                EEis eis = EEDisarm.parse(word, off);
                String s = eis.al[0];
                for (int x = 1; x < eis.al.Length; x++) {
                    if (x == 1) {
                        s = s.PadRight(10, ' ') + " ";
                    }
                    else {
                        s += ", ";
                    }
                    s += eis.al[x];
                }
                return s;
            }
            catch (Exception err) {
                return err.Message;
            }
        }

        uint pc = 0;

        public uint PC {
            get { return pc; }
            set {
                pc = value;
                if (REGChanged != null) REGChanged(this, new EventArgs());
            }
        }

        public String GetREGStr(int ri) {
            siREGs.Position = 16 * (ri & 31);
            String s0 = brREGs.ReadUInt32().ToString("x8");
            String s1 = brREGs.ReadUInt32().ToString("x8");
            String s2 = brREGs.ReadUInt32().ToString("x8");
            String s3 = brREGs.ReadUInt32().ToString("x8");
            return s3 + " " + s2 + " " + s1 + " " + s0;
        }

        public enum RI {
            r0, at, v0, v1, a0, a1, a2, a3,
            t0, t1, t2, t3, t4, t5, t6, t7,
            s0, s1, s2, s3, s4, s5, s6, s7,
            t8, t9, k0, k1, gp, sp, s8, ra,
        }

        public uint GetREGui(RI ri) { return GetREGui((int)ri); }

        public uint GetREGui(int ri) {
            siREGs.Position = 16 * (ri & 31);
            return brREGs.ReadUInt32();
        }
    }
}
