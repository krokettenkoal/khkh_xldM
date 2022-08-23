using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;

// éüÇÃåüèÿ --- 1300 MOVE.xyzw vf2, vf5 00339118

namespace CustVPU1 {
    [DebuggerDisplay("({x}, {y}, {z}, {w})")]
    public class Vec4 {
        public float x, y, z, w;

        public Vec4(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public static Vec4 Empty {
            get { return new Vec4(0, 0, 0, 0); }
        }
        public static Vec4 Initial {
            get { return new Vec4(0, 0, 0, 1); }
        }
    }

    public interface IGSim {
        void Transfer1(byte[] bin);
    };

    public class Core1 {
        public int pc = -1, top = -1;
        public Vec4[] vf = new Vec4[33]; // 32th is write for 0th
        public ushort[] vi = new ushort[17]; // 16th is write for 0th
        public Vec4 acc = Vec4.Empty;
        public float I = 0;
        public float Q = 0;
        public float P = 0;
        public byte[] Mem;
        public byte[] Micro;
        public uint CF = 0; // ( -z +z -y +y -x +x ) * 4
        public int MACflag = 0;
        public int bpc = -1; // branch pc queued for delay slot
        public bool branch = false;
        public bool E = false;
        public IGSim kicker;

        MemoryStream si;
        BinaryReader br;
        BinaryWriter wr;

        public ushort VI0 { get { return vi[0]; } set { } }
        public ushort VI1 { get { return vi[1]; } set { vi[1] = value; } }
        public ushort VI2 { get { return vi[2]; } set { vi[2] = value; } }
        public ushort VI3 { get { return vi[3]; } set { vi[3] = value; } }
        public ushort VI4 { get { return vi[4]; } set { vi[4] = value; } }
        public ushort VI5 { get { return vi[5]; } set { vi[5] = value; } }
        public ushort VI6 { get { return vi[6]; } set { vi[6] = value; } }
        public ushort VI7 { get { return vi[7]; } set { vi[7] = value; } }
        public ushort VI8 { get { return vi[8]; } set { vi[8] = value; } }
        public ushort VI9 { get { return vi[9]; } set { vi[9] = value; } }
        public ushort VI10 { get { return vi[10]; } set { vi[10] = value; } }
        public ushort VI11 { get { return vi[11]; } set { vi[11] = value; } }
        public ushort VI12 { get { return vi[12]; } set { vi[12] = value; } }
        public ushort VI13 { get { return vi[13]; } set { vi[13] = value; } }
        public ushort VI14 { get { return vi[14]; } set { vi[14] = value; } }
        public ushort VI15 { get { return vi[15]; } set { vi[15] = value; } }

        public Vec4 VF0 { get { return vf[0]; } }
        public Vec4 VF1 { get { return vf[1]; } }
        public Vec4 VF2 { get { return vf[2]; } }
        public Vec4 VF3 { get { return vf[3]; } }
        public Vec4 VF4 { get { return vf[4]; } }
        public Vec4 VF5 { get { return vf[5]; } }
        public Vec4 VF6 { get { return vf[6]; } }
        public Vec4 VF7 { get { return vf[7]; } }
        public Vec4 VF8 { get { return vf[8]; } }
        public Vec4 VF9 { get { return vf[9]; } }
        public Vec4 VF10 { get { return vf[10]; } }
        public Vec4 VF11 { get { return vf[11]; } }
        public Vec4 VF12 { get { return vf[12]; } }
        public Vec4 VF13 { get { return vf[13]; } }
        public Vec4 VF14 { get { return vf[14]; } }
        public Vec4 VF15 { get { return vf[15]; } }
        public Vec4 VF16 { get { return vf[16]; } }
        public Vec4 VF17 { get { return vf[17]; } }
        public Vec4 VF18 { get { return vf[18]; } }
        public Vec4 VF19 { get { return vf[19]; } }
        public Vec4 VF20 { get { return vf[20]; } }
        public Vec4 VF21 { get { return vf[21]; } }
        public Vec4 VF22 { get { return vf[22]; } }
        public Vec4 VF23 { get { return vf[23]; } }
        public Vec4 VF24 { get { return vf[24]; } }
        public Vec4 VF25 { get { return vf[25]; } }
        public Vec4 VF26 { get { return vf[26]; } }
        public Vec4 VF27 { get { return vf[27]; } }
        public Vec4 VF28 { get { return vf[28]; } }
        public Vec4 VF29 { get { return vf[29]; } }
        public Vec4 VF30 { get { return vf[30]; } }
        public Vec4 VF31 { get { return vf[31]; } }

        public Core1(byte[] mem, byte[] micro, IGSim kicker) {
            this.Mem = mem;
            this.Micro = micro;
            this.kicker = kicker;

            for (int x = 0; x < 32; x++) vf[x] = Vec4.Empty;
            for (int x = 0; x < 16; x++) vi[x] = 0;

            vf[0] = Vec4.Initial;
            vf[32] = Vec4.Empty;
            vi[16] = 0;

            si = new MemoryStream(mem);
            br = new BinaryReader(si);
            wr = new BinaryWriter(si);
        }

        void Cx() { MACflag &= ~(0x8888); }
        void Cy() { MACflag &= ~(0x4444); }
        void Cz() { MACflag &= ~(0x2222); }
        void Cw() { MACflag &= ~(0x1111); }

        float Mx(float f) {
            uint v = UtCore1.F2UL(f);
            uint exp = (v >> 23) & 0xFF;
            uint s = (v & 0x80000000U);

            if (s != 0) {
                MACflag |= 0x0080;
            }
            if (f == 0) {
                MACflag = (MACflag & ~0x8800) | 0x0008;
                return f;
            }
            switch (exp) {
                case 0:
                    MACflag = (MACflag & ~0x8000) | 0x0808;
                    return UtCore1.UL2F(s);
                case 255:
                    MACflag = (MACflag & ~0x0800) | 0x8000;
                    return UtCore1.UL2F(s | 0x7F7FFFFFU);
                default:
                    MACflag = (MACflag & ~0x8808);
                    return f;
            }
        }
        float My(float f) {
            uint v = UtCore1.F2UL(f);
            uint exp = (v >> 23) & 0xFF;
            uint s = (v & 0x80000000U);

            if (s != 0) {
                MACflag |= 0x0040;
            }
            if (f == 0) {
                MACflag = (MACflag & ~0x4400) | 0x0004;
                return f;
            }
            switch (exp) {
                case 0:
                    MACflag = (MACflag & ~0x4000) | 0x0404;
                    return UtCore1.UL2F(s);
                case 255:
                    MACflag = (MACflag & ~0x0400) | 0x4000;
                    return UtCore1.UL2F(s | 0x7F7FFFFFU);
                default:
                    MACflag = (MACflag & ~0x4404);
                    return f;
            }
        }
        float Mz(float f) {
            uint v = UtCore1.F2UL(f);
            uint exp = (v >> 23) & 0xFF;
            uint s = (v & 0x80000000U);

            if (s != 0) {
                MACflag |= 0x0020;
            }
            if (f == 0) {
                MACflag = (MACflag & ~0x2200) | 0x0002;
                return f;
            }
            switch (exp) {
                case 0:
                    MACflag = (MACflag & ~0x2000) | 0x0202;
                    return UtCore1.UL2F(s);
                case 255:
                    MACflag = (MACflag & ~0x0200) | 0x2000;
                    return UtCore1.UL2F(s | 0x7F7FFFFFU);
                default:
                    MACflag = (MACflag & ~0x2202);
                    return f;
            }
        }
        float Mw(float f) {
            uint v = UtCore1.F2UL(f);
            uint exp = (v >> 23) & 0xFF;
            uint s = (v & 0x80000000U);

            if (s != 0) {
                MACflag |= 0x0010;
            }
            if (f == 0) {
                MACflag = (MACflag & ~0x1100) | 0x0001;
                return f;
            }
            switch (exp) {
                case 0:
                    MACflag = (MACflag & ~0x1000) | 0x0101;
                    return UtCore1.UL2F(s);
                case 255:
                    MACflag = (MACflag & ~0x0100) | 0x1000;
                    return UtCore1.UL2F(s | 0x7F7FFFFFU);
                default:
                    MACflag = (MACflag & ~0x1101);
                    return f;
            }
        }

        static float vuDouble(float f) {
            uint v = UtCore1.F2UL(f);
            switch (v & 0x7F800000U) {
                case 0:
                    v &= 0x80000000U;
                    return UtCore1.UL2F(v);
                case 0x7F800000U:
                    v = (v & 0x80000000U) | 0x7F7FFFFFU;
                    return UtCore1.UL2F(v);
                default:
                    return f;
            }
        }

        public void LQ(int Rd, int Rft, int Ris, int Rimm) {
            if (Rft == 0) Rft = 32;
            si.Position = ((ushort)(vi[Ris] + Rimm)) << 4;
            float x = br.ReadSingle(); if (0 != (Rd & 8)) vf[Rft].x = x;
            float y = br.ReadSingle(); if (0 != (Rd & 4)) vf[Rft].y = y;
            float z = br.ReadSingle(); if (0 != (Rd & 2)) vf[Rft].z = z;
            float w = br.ReadSingle(); if (0 != (Rd & 1)) vf[Rft].w = w;
        }
        public void ITOF0(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.ITOF0(vf[Rfs].x);
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.ITOF0(vf[Rfs].y);
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.ITOF0(vf[Rfs].z);
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.ITOF0(vf[Rfs].w);
        }
        public void ITOF4(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.ITOF4(vf[Rfs].x);
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.ITOF4(vf[Rfs].y);
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.ITOF4(vf[Rfs].z);
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.ITOF4(vf[Rfs].w);
        }
        public void ITOF12(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.ITOF12(vf[Rfs].x);
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.ITOF12(vf[Rfs].y);
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.ITOF12(vf[Rfs].z);
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.ITOF12(vf[Rfs].w);
        }
        public void MULi(int Rd, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) * vuDouble(I)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) * vuDouble(I)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) * vuDouble(I)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) * vuDouble(I)); else Cw();
        }

        public void IADDIU(int Rit, int Ris, int Rimm) {
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)((ushort)vi[Ris] + (ushort)Rimm);
        }
        public void ILW(int Rd, int Rit, int Ris, int Rimm) {
            if (Rit == 0) Rit = 16;
            si.Position = (((ushort)(Rimm + vi[Ris])) << 4)
                | (((Rd & 8) != 0) ? 0 : 0)
                | (((Rd & 4) != 0) ? 4 : 0)
                | (((Rd & 2) != 0) ? 8 : 0)
                | (((Rd & 1) != 0) ? 12 : 0)
                ;
            vi[Rit] = br.ReadUInt16();
        }
        public void IAND(int Rit, int Ris, int Rid) {
            if (Rid == 0) Rid = 16;
            vi[Rid] = (ushort)(vi[Ris] & vi[Rit]);
        }
        public void ISUBIU(int Rit, int Ris, int Rimm) {
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)(vi[Ris] - Rimm);
        }
        public void XTOP(int Rit) {
            Trace.Assert(0 <= top, "!(0 <= top)");
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)top;
        }
        public void ISW(int Rd, int Rit, int Ris, int Rimm) {
            si.Position = ((ushort)(Rimm + vi[Ris])) << 4;
            if (0 != (Rd & 8)) wr.Write((uint)vi[Rit]); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 4)) wr.Write((uint)vi[Rit]); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 2)) wr.Write((uint)vi[Rit]); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 1)) wr.Write((uint)vi[Rit]); else si.Seek(4, SeekOrigin.Current);
        }
        public void IADD(int Rit, int Ris, int Rid) {
            if (Rid == 0) Rid = 16;
            vi[Rid] = (ushort)(vi[Ris] + vi[Rit]);
        }
        public void LQI(int Rd, int Rft, int Ris) {
            LQ(Rd, Rft, Ris, 0);
            if (Ris != 0) vi[Ris]++;
        }
        public void IADDI(int Rit, int Ris, int Rimm) {
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)(vi[Ris] + Rimm);
        }
        public void MULAbc(int Rd, int Rft, int Rfs, int Rbc) {
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) acc.x = Mx(vuDouble(vf[Rfs].x) * f); else Cx();
            if (0 != (Rd & 4)) acc.y = My(vuDouble(vf[Rfs].y) * f); else Cy();
            if (0 != (Rd & 2)) acc.z = Mz(vuDouble(vf[Rfs].z) * f); else Cz();
            if (0 != (Rd & 1)) acc.w = Mw(vuDouble(vf[Rfs].w) * f); else Cw();
        }
        public void MADDAbc(int Rd, int Rft, int Rfs, int Rbc) {
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) acc.x = Mx(vuDouble(acc.x) + vuDouble(vf[Rfs].x) * f); else Cx();
            if (0 != (Rd & 4)) acc.y = My(vuDouble(acc.y) + vuDouble(vf[Rfs].y) * f); else Cy();
            if (0 != (Rd & 2)) acc.z = Mz(vuDouble(acc.z) + vuDouble(vf[Rfs].z) * f); else Cz();
            if (0 != (Rd & 1)) acc.w = Mw(vuDouble(acc.w) + vuDouble(vf[Rfs].w) * f); else Cw();
        }
        public void MADDbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(acc.x) + vuDouble(vf[Rfs].x) * f); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(acc.y) + vuDouble(vf[Rfs].y) * f); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(acc.z) + vuDouble(vf[Rfs].z) * f); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(acc.w) + vuDouble(vf[Rfs].w) * f); else Cw();
        }
        public void SQ(int Rd, int Rit, int Rfs, int Rimm) {
            si.Position = ((ushort)(Rimm + vi[Rit])) << 4;
            if (0 != (Rd & 8)) wr.Write(vf[Rfs].x); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 4)) wr.Write(vf[Rfs].y); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 2)) wr.Write(vf[Rfs].z); else si.Seek(4, SeekOrigin.Current);
            if (0 != (Rd & 1)) wr.Write(vf[Rfs].w); else si.Seek(4, SeekOrigin.Current);
        }
        public void SQI(int Rd, int Rit, int Rfs) {
            SQ(Rd, Rit, Rfs, 0);
            if (Rit != 0) vi[Rit]++;
        }
        public void IOR(int Rit, int Ris, int Rid) {
            if (Rid == 0) Rid = 16;
            vi[Rid] = (ushort)(vi[Ris] | vi[Rit]);
        }
        public void FCSET(int Rimm) {
            CF = (uint)Rimm;
        }
        public void MAXbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = UtCore1.Usebc(vf[Rft], Rbc);
            if (0 != (Rd & 8)) vf[Rfd].x = UtCore1.MAX(vf[Rfs].x, f);
            if (0 != (Rd & 4)) vf[Rfd].y = UtCore1.MAX(vf[Rfs].y, f);
            if (0 != (Rd & 2)) vf[Rfd].z = UtCore1.MAX(vf[Rfs].z, f);
            if (0 != (Rd & 1)) vf[Rfd].w = UtCore1.MAX(vf[Rfs].w, f);
        }
        public void MINIbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = UtCore1.Usebc(vf[Rft], Rbc);
            if (0 != (Rd & 8)) vf[Rfd].x = UtCore1.MINI(vf[Rfs].x, f);
            if (0 != (Rd & 4)) vf[Rfd].y = UtCore1.MINI(vf[Rfs].y, f);
            if (0 != (Rd & 2)) vf[Rfd].z = UtCore1.MINI(vf[Rfs].z, f);
            if (0 != (Rd & 1)) vf[Rfd].w = UtCore1.MINI(vf[Rfs].w, f);
        }
        public void DIV(int Rftf, int Rfsf, int Rft, int Rfs) {
            // thx2pcsx2!
            float ft = vuDouble(UtCore1.Usebc(vf[Rft], Rftf));
            float fs = vuDouble(UtCore1.Usebc(vf[Rfs], Rfsf));

            if (ft == 0.0f) {
                if (fs == 0.0f) {

                }
                else {

                }
                if (0 != ((UtCore1.F2UL(ft) & 0x80000000u) ^ (UtCore1.F2UL(fs)) & 0x80000000u)) {
                    Q = UtCore1.UL2F(0xFF7FFFFFu);
                }
                else {
                    Q = UtCore1.UL2F(0x7F7FFFFFu);
                }
            }
            else {
                Q = vuDouble(fs / ft);
            }
        }
        public void MULbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) * f); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) * f); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) * f); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) * f); else Cw();
        }
        public void ADDbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) + f); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) + f); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) + f); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) + f); else Cw();
        }
        public void MUL(int Rd, int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) * vuDouble(vf[Rft].x)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) * vuDouble(vf[Rft].y)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) * vuDouble(vf[Rft].z)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) * vuDouble(vf[Rft].w)); else Cw();
        }
        public void WAITQ() {

        }
        public void MULq(int Rd, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) * vuDouble(Q)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) * vuDouble(Q)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) * vuDouble(Q)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) * vuDouble(Q)); else Cw();
        }
        public void FTOI0(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.FTOI0(vuDouble(vf[Rfs].x));
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.FTOI0(vuDouble(vf[Rfs].y));
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.FTOI0(vuDouble(vf[Rfs].z));
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.FTOI0(vuDouble(vf[Rfs].w));
        }
        public void SUB(int Rd, int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) - vuDouble(vf[Rft].x)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) - vuDouble(vf[Rft].y)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) - vuDouble(vf[Rft].z)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) - vuDouble(vf[Rft].w)); else Cw();
        }
        public void FTOI4(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.FTOI4(vuDouble(vf[Rfs].x));
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.FTOI4(vuDouble(vf[Rfs].y));
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.FTOI4(vuDouble(vf[Rfs].z));
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.FTOI4(vuDouble(vf[Rfs].w));
        }
        public void OPMULA(int Rft, int Rfs) {
            acc.x = Mx(vuDouble(vf[Rfs].y) * vuDouble(vf[Rft].z));
            acc.y = My(vuDouble(vf[Rfs].z) * vuDouble(vf[Rft].x));
            acc.z = Mz(vuDouble(vf[Rfs].x) * vuDouble(vf[Rft].y));
        }
        public void OPMSUB(int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            vf[Rfd].x = Mx(vuDouble(acc.x) - vuDouble(vf[Rfs].y) * vuDouble(vf[Rft].z));
            vf[Rfd].y = My(vuDouble(acc.y) - vuDouble(vf[Rfs].z) * vuDouble(vf[Rft].x));
            vf[Rfd].z = Mz(vuDouble(acc.z) - vuDouble(vf[Rfs].x) * vuDouble(vf[Rft].y));
        }
        public void FMAND(int Rit, int Ris) {
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)(vi[Ris] & MACflag);
        }
        public void MTIR(int Rfsf, int Rit, int Rfs) {
            if (Rit == 0) Rit = 16;
            vi[Rit] = (ushort)UtCore1.F2UL(UtCore1.Usebc(vf[Rfs], Rfsf));
        }
        public void ISUB(int Rd, int Rit, int Ris, int Rid) {
            if (Rid == 0) Rid = 16;
            vi[Rid] = (ushort)(vi[Ris] - vi[Rit]);
        }
        public void ADD(int Rd, int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) + vuDouble(vf[Rft].x)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) + vuDouble(vf[Rft].y)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) + vuDouble(vf[Rft].z)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) + vuDouble(vf[Rft].w)); else Cw();
        }
        public void XGKICK(int Ris) {
            si.Position = vi[Ris] << 4;
            Debug.WriteLine("# XGKICK @ " + vi[Ris].ToString("X3"));
            UtXGkick.Once(kicker, br);
        }
        public void SUBbc(int Rd, int Rft, int Rfs, int Rfd, int Rbc) {
            if (Rfd == 0) Rfd = 32;
            float f = vuDouble(UtCore1.Usebc(vf[Rft], Rbc));
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) - f); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) - f); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) - f); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) - f); else Cw();
        }
        public void CLIP(int Rft, int Rfs) {
            CF <<= 6;
            float pw = +Math.Abs(vuDouble(vf[Rft].w));
            float nw = -pw;
            if (vuDouble(vf[Rfs].x) > pw) CF |= 0x01;
            if (vuDouble(vf[Rfs].x) < nw) CF |= 0x02;
            if (vuDouble(vf[Rfs].y) > pw) CF |= 0x04;
            if (vuDouble(vf[Rfs].y) < nw) CF |= 0x08;
            if (vuDouble(vf[Rfs].z) > pw) CF |= 0x10;
            if (vuDouble(vf[Rfs].z) < nw) CF |= 0x20;
            CF &= 0xFFFFFFu;
        }
        public void FCAND(int Rimm) {
            vi[1] = (ushort)(((CF & (uint)Rimm) != 0) ? 1 : 0);
        }
        public void MFIR(int Rd, int Rft, int Ris) {
            if (Rft == 0) Rft = 32;
            float f = UtCore1.SL2F((short)vi[Ris]);
            if (0 != (Rd & 8)) vf[Rft].x = f;
            if (0 != (Rd & 4)) vf[Rft].y = f;
            if (0 != (Rd & 2)) vf[Rft].z = f;
            if (0 != (Rd & 1)) vf[Rft].w = f;
        }
        public void ABS(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = Math.Abs(vuDouble(vf[Rfs].x));
            if (0 != (Rd & 4)) vf[Rft].y = Math.Abs(vuDouble(vf[Rfs].y));
            if (0 != (Rd & 2)) vf[Rft].z = Math.Abs(vuDouble(vf[Rfs].z));
            if (0 != (Rd & 1)) vf[Rft].w = Math.Abs(vuDouble(vf[Rfs].w));
        }
        public void ADDi(int Rd, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = Mx(vuDouble(vf[Rfs].x) + vuDouble(I)); else Cx();
            if (0 != (Rd & 4)) vf[Rfd].y = My(vuDouble(vf[Rfs].y) + vuDouble(I)); else Cy();
            if (0 != (Rd & 2)) vf[Rfd].z = Mz(vuDouble(vf[Rfs].z) + vuDouble(I)); else Cz();
            if (0 != (Rd & 1)) vf[Rfd].w = Mw(vuDouble(vf[Rfs].w) + vuDouble(I)); else Cw();
        }
        public void MOVE(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = vf[Rfs].x;
            if (0 != (Rd & 4)) vf[Rft].y = vf[Rfs].y;
            if (0 != (Rd & 2)) vf[Rft].z = vf[Rfs].z;
            if (0 != (Rd & 1)) vf[Rft].w = vf[Rfs].w;
        }
        public void FTOI12(int Rd, int Rft, int Rfs) {
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = UtCore1.FTOI12(vf[Rfs].x);
            if (0 != (Rd & 4)) vf[Rft].y = UtCore1.FTOI12(vf[Rfs].y);
            if (0 != (Rd & 2)) vf[Rft].z = UtCore1.FTOI12(vf[Rfs].z);
            if (0 != (Rd & 1)) vf[Rft].w = UtCore1.FTOI12(vf[Rfs].w);
        }
        public void MR32(int Rd, int Rft, int Rfs) {
            float x = vf[Rfs].x;
            if (Rft == 0) Rft = 32;
            if (0 != (Rd & 8)) vf[Rft].x = vf[Rfs].y;
            if (0 != (Rd & 4)) vf[Rft].y = vf[Rfs].z;
            if (0 != (Rd & 2)) vf[Rft].z = vf[Rfs].w;
            if (0 != (Rd & 1)) vf[Rft].w = x;
        }
        public void MAX(int Rd, int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = UtCore1.MAX(vf[Rfs].x, vf[Rft].x);
            if (0 != (Rd & 4)) vf[Rfd].y = UtCore1.MAX(vf[Rfs].y, vf[Rft].y);
            if (0 != (Rd & 2)) vf[Rfd].z = UtCore1.MAX(vf[Rfs].z, vf[Rft].z);
            if (0 != (Rd & 1)) vf[Rfd].w = UtCore1.MAX(vf[Rfs].w, vf[Rft].w);
        }
        public void MINI(int Rd, int Rft, int Rfs, int Rfd) {
            if (Rfd == 0) Rfd = 32;
            if (0 != (Rd & 8)) vf[Rfd].x = UtCore1.MINI(vf[Rfs].x, vf[Rft].x);
            if (0 != (Rd & 4)) vf[Rfd].y = UtCore1.MINI(vf[Rfs].y, vf[Rft].y);
            if (0 != (Rd & 2)) vf[Rfd].z = UtCore1.MINI(vf[Rfs].z, vf[Rft].z);
            if (0 != (Rd & 1)) vf[Rfd].w = UtCore1.MINI(vf[Rfs].w, vf[Rft].w);
        }
    }

    static class UtXGkick {
        public static void Once(IGSim kicker, BinaryReader br) {
            while (true) {
                UInt64 v0 = br.ReadUInt64();
                UInt64 v8 = br.ReadUInt64();
                int nloop = ((UInt16)v0 & 0x7FFF);
                bool eop = ((UInt16)v0 & 0x8000) != 0;
                int flg = ((Byte)(v0 >> 58)) & 3;
                int nreg = ((Byte)(v0 >> 60)) & 15;

                int size = 0;
                switch (flg) {
                    case 0: // packed
                        size = 16 * nreg * nloop;
                        break;
                    case 1: // reglist
                        size = 8 * nreg * nloop;
                        if (0 != (size & 8)) size += 8;
                        break;
                    case 2: // image
                        size = 16 * nloop;
                        break;
                    default: throw new NotSupportedException("flg");
                }

                MemoryStream os = new MemoryStream(8 + size);
                BinaryWriter wr = new BinaryWriter(os);
                wr.Write(v0);
                wr.Write(v8);
                wr.Write(br.ReadBytes(size), 0, size);

                kicker.Transfer1(os.ToArray());
                if (eop) break;
            }
        }
    }

    public static class UtCore1 {
        class Fastcase {
            public MemoryStream os = new MemoryStream(4);
            public BinaryReader br;
            public BinaryWriter wr;

            public Fastcase() {
                br = new BinaryReader(os);
                wr = new BinaryWriter(os);
            }
        }

        static Fastcase fc = new Fastcase();

        public static uint F2UL(float f) {
#if true
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(f);
                fc.os.Position = 0;
                return fc.br.ReadUInt32();
            }
#else
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(f);
            os.Position = 0;
            return new BinaryReader(os).ReadUInt32();
#endif
        }

        public static float UL2F(uint v) {
#if true
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(v);
                fc.os.Position = 0;
                return fc.br.ReadSingle();
            }
#else
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(v);
            os.Position = 0;
            return new BinaryReader(os).ReadSingle();
#endif
        }

        public static float SL2F(int v) {
#if true
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(v);
                fc.os.Position = 0;
                return fc.br.ReadSingle();
            }
#else
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(v);
            os.Position = 0;
            return new BinaryReader(os).ReadSingle();
#endif
        }

        public static float ITOF0(float fin) {
#if true
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(fin);
                fc.os.Position = 0;
                return fc.br.ReadInt32();
            }
#else
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(fin);
            os.Position = 0;
            return new BinaryReader(os).ReadInt32();
#endif
        }

        public static float ITOF4(float fin) {
#if true
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(fin);
                fc.os.Position = 0;
                return fc.br.ReadInt32() / 16.0f;
            }
#else
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(fin);
            os.Position = 0;
            return new BinaryReader(os).ReadInt32() / 16.0f;
#endif
        }

        public static float ITOF12(float fin) {
#if true
            // 468.75ms  lock ver
            lock (fc) {
                fc.os.Position = 0;
                fc.wr.Write(fin);
                fc.os.Position = 0;
                return fc.br.ReadInt32() / 4096.0f;
            }
#else
            // 1718.75ms newobj ver
            MemoryStream os = new MemoryStream(4);
            new BinaryWriter(os).Write(fin);
            os.Position = 0;
            return new BinaryReader(os).ReadInt32() / 4096.0f;
#endif
        }

        public static float Usebc(Vec4 vec4, int Rbc) {
            switch (Rbc) {
                case 0: return vec4.x;
                case 1: return vec4.y;
                case 2: return vec4.z;
                case 3: return vec4.w;
            }
            throw new ArgumentOutOfRangeException("Rbc");
        }

        public static float FTOI0(float vf) {
            return SL2F((int)(vf));
        }

        public static float FTOI4(float vf) {
            // Thanks 2 pcsx2!
            return SL2F((int)(vf * (1.0f / 0.0625f)));
        }

        public static float FTOI12(float vf) {
            // Thanks 2 pcsx2!
            return SL2F((int)(vf * (1.0f / 0.000244140625f)));
        }

        public static float MAX(float x, float y) {
            // Thanks 2 pcsx2!
            uint a = F2UL(x);
            uint b = F2UL(y);

            if (0 != (a & 0x80000000)) { // -a
                if (0 != (b & 0x80000000)) { // -b
                    return (a & 0x7fffffff) > (b & 0x7fffffff) ? y : x;
                }
                else { // +b
                    return y;
                }
            }
            else { // +a
                if (0 != (b & 0x80000000)) { // -b
                    return x;
                }
                else { // +b
                    return (a & 0x7fffffff) > (b & 0x7fffffff) ? x : y;
                }
            }
        }

        public static float MINI(float x, float y) {
            // Thanks 2 pcsx2!
            uint a = F2UL(x);
            uint b = F2UL(y);

            if (0 != (a & 0x80000000)) { // -a
                if (0 != (b & 0x80000000)) { // -b
                    return (a & 0x7fffffff) < (b & 0x7fffffff) ? y : x;
                }
                else { // +b
                    return x;
                }
            }
            else { // +a
                if (0 != (b & 0x80000000)) { // -b
                    return y;
                }
                else { // +b
                    return (a & 0x7fffffff) < (b & 0x7fffffff) ? x : y;
                }
            }
        }
    }

    public delegate void Funct();

    public class Rec1 {
        CodeCompileUnit cc = new CodeCompileUnit();
        CodeTypeDeclaration cls1;

        public Rec1() {
            CodeNamespace cns = new CodeNamespace("CustVPU1.C");
            cc.Namespaces.Add(cns);
            cc.ReferencedAssemblies.Add("CustVPU1.dll");
            cns.Imports.Add(new CodeNamespaceImport("System"));
            cns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));

            cls1 = new CodeTypeDeclaration("Class1");
            cls1.Attributes = MemberAttributes.Public;
            cns.Types.Add(cls1);

            CodeConstructor ctor = new CodeConstructor();
            cls1.Members.Add(ctor);
            ctor.Attributes = MemberAttributes.Public;
            ctor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Core1), "c1"));
            ctor.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"),
                new CodeArgumentReferenceExpression("c1")
                ));

            CodeMemberField fc1 = new CodeMemberField(typeof(Core1), "c1");
            cls1.Members.Add(fc1);
        }

        const int BranchDelta = 1;

        /// <summary>
        /// Recompile it.
        /// </summary>
        /// <param name="mem">microcode memory</param>
        /// <param name="pc">in byte unit</param>
        public void Recompile(byte[] mem, uint pc, string fplib, out string typeName, out string methodName) {
            MemoryStream si = new MemoryStream(mem, false);
            BinaryReader br = new BinaryReader(si);

            si.Position = pc;

            typeName = "CustVPU1.C.Class1";

            CodeMemberMethod fn = new CodeMemberMethod();
            fn.Name = methodName = LabUt.addr2Name(pc);
            fn.Attributes = MemberAttributes.Public;

            CodePrimitiveExpression valNextpc = new CodePrimitiveExpression(0);
            CodeAssignStatement setNextpc = new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "pc"),
                valNextpc
                );
            fn.Statements.Add(setNextpc);

            vu1Disarm.VU1Da dis = new vu1Disarm.VU1Da();
            dis.Decode(mem);

            int exitStep = -1;
            bool fE = false;

            CodeStatement lastlo = null;

            while ((exitStep--) != 0) {
                uint L = br.ReadUInt32();
                uint U = br.ReadUInt32();

                if (0 != (U & 0x40000000U)) {
                    fE = true;
                    exitStep = 1;
                }

                // Lower instruction L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L-L- [[[
                {
                    string P = dis.L[pc / 8].ToString();
                    fn.Statements.Add(new CodeCommentStatement("--- " + (pc + 0).ToString("X4") + " " + P));

                    uint v06 = BUt.From2To16((L) & 0x3F);
                    uint v65 = BUt.From2To16((L >> 6) & 0x1F);
                    uint v25 = BUt.From2To16((L >> 25));
                    int Rimm11 = (int)(L & 0x07FFU);
                    int Rimm11s = ((Rimm11 & 0x400) != 0) ? Rimm11 - 0x800 : Rimm11;
                    int Rimm15 = (int)(((L >> 21) & 0xFU) << 11) | Rimm11;
                    int Rimm5 = (int)((L >> 6) & 0x1F);
                    int Rimm5s = ((Rimm5 & 0x10) != 0) ? Rimm5 - 0x20 : Rimm5;
                    int Rimm24 = (int)(L & 0xFFFFFFU);
                    int Rid = (int)((L >> 6) & 0x1F);
                    int Ris = (int)((L >> 11) & 0x1F);
                    int Rfs = (int)((L >> 11) & 0x1F);
                    int Rft = (int)((L >> 16) & 0x1F);
                    int Rit = (int)((L >> 16) & 0x1F);
                    int Rd = (int)((L >> 21) & 0xF);
                    int Rfsf = (int)((L >> 21) & 3);
                    int Rftf = (int)((L >> 23) & 3);
                    CodeStatement stmt = null;
                    CodeExpression expr = null;
                    bool isIBR = false;

                    if (false) { }
                    else if (L == 0x8000033CU) { //[nop

                    }
                    else if ((U & 0x80000000) != 0) { //[LOI
                        stmt = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "I"),
                            new CodePrimitiveExpression(UtCore1.UL2F(L))
                            );
                    }
                    else if (v25 == 0x0100000) { //[B
                        valNextpc.Value = (int)(pc + 8 * (BranchDelta + Rimm11s)); // Signed
                        exitStep = 1;
                    }
                    else if (v25 == 0x0100001) { //[BAL
                        valNextpc.Value = (int)(pc + 8 * (BranchDelta + Rimm11s)); // Signed
                        stmt = new CodeAssignStatement(
                            CUt.VI(Rit),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression((int)pc + 16),
                                CodeBinaryOperatorType.Divide,
                                new CodePrimitiveExpression(8)
                                )
                            );
                        exitStep = 1;
                    }
                    else if (v25 == 0x0100100) { //[JR
                        stmt = CUt.SetPC(
                            new CodeBinaryOperatorExpression(
                                CUt.VI(Ris),
                                CodeBinaryOperatorType.Multiply,
                                new CodePrimitiveExpression(8)
                                )
                            );
                        exitStep = 1;
                    }
                    else if (v25 == 0x0101000) { //[IBEQ
                        stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CUt.VI(Rit),
                                CodeBinaryOperatorType.ValueEquality,
                                CUt.VI(Ris)
                            ),
                            CUt.SetPC(new CodePrimitiveExpression((int)(pc + 8 * (BranchDelta + Rimm11s)))) // Signed
                        );
                        exitStep = 1; isIBR = true;
                    }
                    else if (v25 == 0x0101001) { //[IBNE
                        stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodeBinaryOperatorExpression(
                                    CUt.VI(Rit),
                                    CodeBinaryOperatorType.ValueEquality,
                                    CUt.VI(Ris)
                                ),
                                CodeBinaryOperatorType.ValueEquality,
                                new CodePrimitiveExpression(false)
                            ),
                            CUt.SetPC(new CodePrimitiveExpression((int)(pc + 8 * (BranchDelta + Rimm11s)))) // Signed
                        );
                        exitStep = 1; isIBR = true;
                    }
                    else if (v25 == 0x0000000) { //[LQ
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "LQ"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm11s) // Signed
                            );
                    }
                    else if (v25 == 0x0000100) { //[ILW
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ILW"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm11s) // Signed
                            );
                    }
                    else if (v25 == 0x0001000) { //[IADDIU
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "IADDIU"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm15) // Unsigned
                            );
                    }
                    else if (v25 == 0x1000000 && v06 == 0x110100) { //[IAND
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "IAND"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rid)
                            );
                    }
                    else if (v25 == 0x0001001) { //[ISUBIU
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ISUBIU"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm15) // Unsigned
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x11010 && v06 == 0x111100) { //[XTOP
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "XTOP"),
                            new CodePrimitiveExpression(Rit)
                            );
                    }
                    else if (v25 == 0x0000101) { //[ISW
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ISW"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm11s) // Signed
                            );
                    }
                    else if (v25 == 0x1000000 && v06 == 0x110000) { //[IADD
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "IADD"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rid)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01101 && v06 == 0x111100) { //[LQI
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "LQI"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Ris)
                            );
                    }
                    else if (v25 == 0x1000000 && v06 == 0x110010) { //[IADDI
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "IADDI"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rimm5s) // Signed
                            );
                    }
                    else if (v25 == 0x0000001) { //[SQ
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "SQ"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rimm11s) // Signed
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01101 && v06 == 0x111101) { //[SQI
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "SQI"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v25 == 0x1000000 && v06 == 0x110101) { //[IOR
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "IOR"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rid)
                            );
                    }
                    else if (v25 == 0x0010001) { //[FCSET
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FCSET"),
                            new CodePrimitiveExpression(Rimm24)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01110 && v06 == 0x111100) { //[DIV
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "DIV"),
                            new CodePrimitiveExpression(Rftf),
                            new CodePrimitiveExpression(Rfsf),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01110 && v06 == 0x111111) { //[WAITQ
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "WAITQ")
                            );
                    }
                    else if (v25 == 0x0011010) { //[FMAND
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FMAND"),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01111 && v06 == 0x111100) { // [MTIR
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MTIR"),
                            new CodePrimitiveExpression(Rfsf),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v25 == 0x1000000 && v06 == 0x110001) { //[ISUB
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ISUB"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rit),
                            new CodePrimitiveExpression(Ris),
                            new CodePrimitiveExpression(Rid)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x11011 && v06 == 0x111100) { //[XGKICK
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "XGKICK"),
                            new CodePrimitiveExpression(Ris)
                            );
                    }
                    else if (v25 == 0x0010010) { //[FCAND
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FCAND"),
                            new CodePrimitiveExpression(Rimm24)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01111 && v06 == 0x111101) { // [MFIR
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MFIR"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Ris)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01100 && v06 == 0x111100) { //[MOVE
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MOVE"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v25 == 0x1000000 && v65 == 0x01100 && v06 == 0x111101) { //[MR32
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MR32"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else throw new NotSupportedException("L v25 " + v25.ToString("X") + " v65 " + v65.ToString("X5") + " v06 " + v06.ToString("X6") + " c " + L.ToString("X8") + " | " + P);

                    if (expr != null)
                        stmt = new CodeExpressionStatement(expr);
                    if (stmt != null) {
                        if (isIBR && lastlo != null) {
                            fn.Statements.Insert(fn.Statements.IndexOf(lastlo), stmt);
                        }
                        else {
                            fn.Statements.Add(stmt);
                        }
                    }
                    lastlo = stmt;
                }
                // Upper instruction U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U-U- ]]]
                {
                    string P = dis.U[pc / 8].ToString();
                    fn.Statements.Add(new CodeCommentStatement("--- " + (pc + 4).ToString("X4") + " " + P));

                    uint v326 = BUt.From2To16((U) & 0x3F);
                    uint v344 = BUt.From2To16((U >> 2) & 0x0F);
                    uint v385 = BUt.From2To16((U >> 6) & 0x1F);
                    int Rd = (int)((U >> 21) & 0xF);
                    int Rft = (int)((U >> 16) & 0x1F);
                    int Rfs = (int)((U >> 11) & 0x1F);
                    int Rfd = (int)((U >> 6) & 0x1F);
                    int Rbc = (int)((U) & 0x3);
                    CodeExpression expr = null;
                    if (false) { }
                    else if ((U & 0x80000000) != 0) { //[LOI
                    }
                    else if (v385 == 0x01011 && v326 == 0x111111) { //]NOP

                    }
                    else if (v385 == 0x00100 && v326 == 0x111100) { //]ITOF0
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ITOF0"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v326 == 0x011110) { //]MULi
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MULi"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v385 == 0x00110 && v344 == 0x1111) { //]MULAbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MULAbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v385 == 0x00010 && v344 == 0x1111) { //]MADDAbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MADDAbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v344 == 0x0010) { //]MADDbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MADDbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v344 == 0x0100) { //]MAXbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MAXbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v344 == 0x0101) { //]MINIbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MINIbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v344 == 0x0110) { //]MULbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MULbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v344 == 0x0000) { //]ADDbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ADDbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v326 == 0x101010) { //]MUL
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MUL"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v326 == 0x011100) { //]MULq
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MULq"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v385 == 0x00101 && v326 == 0x111100) { //]FTOI0
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FTOI0"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v326 == 0x101100) { //]SUB
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "SUB"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v385 == 0x00101 && v326 == 0x111101) { //]FTOI4
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FTOI4"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v385 == 0x01011 && v326 == 0x111110) { //]OPMULA
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "OPMULA"),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v326 == 0x101110) { //]OPMSUB
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "OPMSUB"),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v326 == 0x101000) { //]ADD
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ADD"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v344 == 0x0001) { //]SUBbc
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "SUBbc"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd),
                            new CodePrimitiveExpression(Rbc)
                            );
                    }
                    else if (v385 == 0x00111 && v326 == 0x111111) { //]CLIP
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "CLIP"),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v385 == 0x00111 && v326 == 0x111101) { //]ABS
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ABS"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v326 == 0x100010) { //]ADDi
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ADDi"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v385 == 0x00101 && v326 == 0x111110) { //]FTOI12
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "FTOI12"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v385 == 0x00100 && v326 == 0x111101) { //]ITOF4
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ITOF4"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v385 == 0x00100 && v326 == 0x111110) { //]ITOF12
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "ITOF12"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs)
                            );
                    }
                    else if (v326 == 0x101011) { //]MAX
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MAX"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else if (v326 == 0x101111) { //]MINI
                        expr = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "MINI"),
                            new CodePrimitiveExpression(Rd),
                            new CodePrimitiveExpression(Rft),
                            new CodePrimitiveExpression(Rfs),
                            new CodePrimitiveExpression(Rfd)
                            );
                    }
                    else throw new NotSupportedException("U v385 " + v385.ToString("X5") + " v326 " + v326.ToString("X6") + " c " + U.ToString("X8") + " | " + P);

                    if (expr != null) fn.Statements.Add(expr);
                }
                pc += 8;
            }

            if ((int)valNextpc.Value == 0) {
                valNextpc.Value = (int)pc;
            }

            if (fE) {
                fn.Statements.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "E"),
                        new CodePrimitiveExpression(true)
                    )
                );
            }

            cls1.Members.Add(fn);

            CodeDomProvider cp = CodeDomProvider.CreateProvider("cs");
            string fpcs = fplib + ".cs";
            {
                using (StreamWriter wr = new StreamWriter(fpcs)) {
                    CodeGeneratorOptions opts = new CodeGeneratorOptions();
                    cp.GenerateCodeFromCompileUnit(cc, wr, opts);
                    wr.Close();
                }
            }
            {
                CompilerParameters cps = new CompilerParameters();
                cps.GenerateExecutable = false;
                cps.OutputAssembly = fplib;
                cps.ReferencedAssemblies.Add("CustVPU1.dll");
                //cps.CompilerOptions = "/optimize";
                cps.CompilerOptions = "/debug";
                CompilerResults crs = cp.CompileAssemblyFromFile(cps, fpcs);
                if (crs.NativeCompilerReturnValue != 0) {
                    Debug.WriteLine("E-E-E-");
                    foreach (CompilerError err in crs.Errors) {
                        Debug.WriteLine(err);
                    }
                    Debug.WriteLine("-E-E-E");
                    Trace.Fail("Recompile failed.", crs.Errors.ToString());
                }
            }
        }

        /// <summary>
        /// Code utilities
        /// </summary>
        static class CUt {

            public static CodeAssignStatement SetPC(CodeExpression o) {
                return new CodeAssignStatement(
                    new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "pc"),
                    o
                    );
            }

            public static CodeFieldReferenceExpression VI(int r) {
                return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "c1"), "VI" + r);
            }

            public static CodeExpression Usepc(uint v) {
                return new CodeCastExpression(
                    typeof(ushort),
                    new CodePrimitiveExpression((int)(v))
                    );
            }
        }

        /// <summary>
        /// Label utilities
        /// </summary>
        static class LabUt {
            public static string addr2Name(uint pc) {
                return "_" + pc.ToString("x4");
            }
        }

        /// <summary>
        /// Bit conversion utilities
        /// </summary>
        static class BUt {
            public static uint From2To16(uint v) {
                uint r = 0;
                byte b = 0;
                while (v != 0) {
                    if (0 != (v & 1)) r |= (1U << (4 * b));
                    v >>= 1;
                    b++;
                }
                return r;
            }
            public static string To2(uint v, int cx) {
                string s = "";
                do {
                    s = s + (v & 1);
                    v >>= 1;
                    cx--;
                } while (cx != 0 && v != 0);
                return s;
            }
        }
    }
}
