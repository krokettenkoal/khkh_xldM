using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;
using System.IO;
using khkh_xldMii;

// kh2jp

namespace PortSCalc {
    class Program {
        static void Main(string[] args) {
            new Program().Run();
        }

        internal int cjMdlx = 0xE5;

        void Run() {
            using (FileStream fsMdlx = File.OpenRead(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX110.mdlx")) {
                using (FileStream fsMset = File.OpenRead(@"H:\KH2.yaz0r\dump_kh2\obj\P_EX110.mset")) {
                    ReadBar.Barent[] alanb = ReadBar.Explode2(fsMset);
                    ReadBar.Barent[] ala = ReadBar.Explode2(new MemoryStream(alanb[2].bin));
                    Msetacc mset = new Msetacc(ala[0].bin);

                    ReadBar.Barent[] alhdr = ReadBar.Explode2(fsMdlx);
                    Mdlxacc mdlx = new Mdlxacc(alhdr[0].bin);

                    InfoTbl it = new InfoTbl(mset.cjMdlx);
                    it.offMdlx04 = mdlx;

                    new CALCT(it, mset).S_CALCT(); CopyUt.ExportSRT(it, "SRT_calct.txt");
                    new CALCF(it, mset, 8).S_CALCF(); CopyUt.ExportSRT(it, "SRT_calcf.txt");
                }
            }
        }

        class CopyUt {
            public static void ExportSRT(InfoTbl it, String fn) {
                StringWriter wr = new StringWriter();
                OUt.Wr(wr, it.Sxyz, "Sxyz");
                OUt.Wr(wr, it.Rxyz, "Rxyz");
                OUt.Wr(wr, it.Txyz, "Txyz");
                File.WriteAllText(fn, wr.ToString(), Encoding.ASCII);
            }

            class OUt {
                public static void Wr(TextWriter wr, Vector4[] al, String prefix) {
                    for (int x = 0; x < al.Length; x++) {
                        float X = al[x].X;
                        float Y = al[x].Y;
                        float Z = al[x].Z;
                        float W = al[x].W;
                        wr.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", prefix, x, X, Y, Z, W);
                    }
                }
            }
        }
    }

    public class Mdlxacc {
        MemoryStream si;
        BinaryReader br;

        public Mdlxacc(byte[] bin) {
            si = new MemoryStream(bin, false);
            br = new BinaryReader(si);

            {
                int cx = cntAxBone;
                for (int x = 0; x < cx; x++) alaxb.Add(GetAxBone(x));
            }
        }

        public List<AxBone> alaxb = new List<AxBone>();

        public int offAxBone { get { si.Position = 0x90 + 0x14; return br.ReadInt32(); } }

        public int cntAxBone { get { si.Position = 0x90 + 0x10; return br.ReadUInt16(); } }

        public AxBone GetAxBone(int x) {
            si.Position = offAxBone + 0x90 + 64 * x;
            AxBone o = new AxBone();
            o.i = br.ReadInt16();
            o.ri = br.ReadInt16();
            o.pi = br.ReadInt16();
            o.rpi = br.ReadInt16();
            si.Seek(8, SeekOrigin.Current);
            o.sx = br.ReadSingle();
            o.sy = br.ReadSingle();
            o.sz = br.ReadSingle();
            o.sw = br.ReadSingle();

            o.rx = br.ReadSingle();
            o.ry = br.ReadSingle();
            o.rz = br.ReadSingle();
            o.rw = br.ReadSingle();

            o.tx = br.ReadSingle();
            o.ty = br.ReadSingle();
            o.tz = br.ReadSingle();
            o.tw = br.ReadSingle();
            return o;
        }
    }


    public class T1 {
        /// <summary>
        /// @0
        /// </summary>
        public ushort joint;
        /// <summary>
        /// @2
        /// </summary>
        public ushort ax;
        /// <summary>
        /// @4
        /// </summary>
        public float value;
    }
    public class T2 {
        /// <summary>
        /// @0
        /// </summary>
        public ushort joint;
        /// <summary>
        /// @2
        /// </summary>
        public byte ax;
        /// <summary>
        /// @3
        /// </summary>
        public byte t9cnt;
        /// <summary>
        /// @4
        /// </summary>
        public ushort t9index;
    }
    public class T9 {
        /// <summary>
        /// @0
        /// </summary>
        public ushort t11off;
        /// <summary>
        /// @2
        /// </summary>
        public ushort t10index;
        /// <summary>
        /// @4
        /// </summary>
        public ushort t12index1;
        /// <summary>
        /// @6
        /// </summary>
        public ushort t12index2;
    }
    /// <summary>
    /// 8 bytes
    /// </summary>
    public class T7 {
        /// <summary>
        /// @0
        /// </summary>
        public ushort hw0;
        /// <summary>
        /// @0
        /// </summary>
        public ushort hw2;

        /// <summary>
        /// @6
        /// </summary>
        public ushort hw6;
    }
    /// <summary>
    /// 12 bytes
    /// </summary>
    public class T3 {
        public byte b0;
        public byte b1;
        public ushort hw2;
        public ushort hw4;
        public ushort hw6;
        public int w8;
    }
    public class T8 {
    }
    public class T13 {
        public ushort hw0;
    }

    public class Msetacc {
        MemoryStream si;
        BinaryReader br;

        public Msetacc(byte[] bin) {
            si = new MemoryStream(bin, false);
            br = new BinaryReader(si);

            {
                for (int x = 0; x < cjMset; x++) {
                    alt5.Add(Gett5(x));
                }
                alt5.Add(null);
            }

            {
                for (int x = 0; x < cntt7; x++) {
                    alt7.Add(Gett7(x));
                }
                alt7.Add(null);
            }
            {
                for (int x = 0; x < cntt3; x++) {
                    alt3.Add(Gett3(x));
                }
                alt3.Add(null);
            }
            {
                for (int x = 0; x < cntt4; x++) {
                    alt4.Add(Gett4(x));
                }
            }
        }

        public List<AxBone> alt5 = new List<AxBone>(); // modified by CALCF

        /// <summary>
        /// Count joints in Mdlx
        /// </summary>
        public ushort cjMdlx { get { si.Position = 0x90 + 0x10; return br.ReadUInt16(); } }

        public int cjMset { get { return cntt5 - cjMdlx; } }

        public int offt1 { get { si.Position = 0x90 + 0x24; return br.ReadInt32(); } }
        public int cntt1 { get { si.Position = 0x90 + 0x28; return br.ReadInt32(); } }

        public int offt2 { get { si.Position = 0x90 + 0x30; return br.ReadInt32(); } }
        public int cntt2 { get { si.Position = 0x90 + 0x34; return br.ReadInt32(); } }

        public int offt3 { get { si.Position = 0x90 + 0x50; return br.ReadInt32(); } }
        public int cntt3 { get { si.Position = 0x90 + 0x54; return br.ReadInt32(); } }

        public int offt2x { get { si.Position = 0x90 + 0x38; return br.ReadInt32(); } }
        public int cntt2x { get { si.Position = 0x90 + 0x3C; return br.ReadInt32(); } }

        public int offt4 { get { si.Position = 0x90 + 0x1C; return br.ReadInt32(); } }
        public int cntt4 { get { si.Position = 0x90 + 0x12; return br.ReadInt32(); } }

        public int offt5 { get { si.Position = 0x90 + 0x18; return br.ReadInt32(); } }
        /// <summary>
        /// cjMdlx+cjMset
        /// </summary>
        public int cntt5 { get { si.Position = 0x90 + 0x12; return br.ReadUInt16(); } }

        public int offt7 { get { si.Position = 0x90 + 0x60; return br.ReadInt32(); } }
        public int cntt7 { get { si.Position = 0x90 + 0x64; return br.ReadInt32(); } }

        public int offt8 { get { si.Position = 0x90 + 0x5c; return br.ReadInt32(); } }

        public int offt9 { get { si.Position = 0x90 + 0x40; return br.ReadInt32(); } }

        public int offt10 { get { si.Position = 0x90 + 0x48; return br.ReadInt32(); } }

        public int offt11 { get { si.Position = 0x90 + 0x44; return br.ReadInt32(); } }
        public int cntt11 { get { si.Position = 0x90 + 0x20; return br.ReadInt32(); } }

        public int offt12 { get { si.Position = 0x90 + 0x4C; return br.ReadInt32(); } }

        public int offt13 { get { si.Position = 0x90 + 0xA0; return br.ReadInt32(); } }

        public T1 Gett1(int x) {
            si.Position = 0x90 + offt1 + 8 * x;
            T1 o = new T1();
            o.joint = br.ReadUInt16();
            o.ax = br.ReadUInt16();
            o.value = br.ReadSingle();
            return o;
        }
        public T2 Gett2(int x) {
            si.Position = 0x90 + offt2 + 6 * x;
            T2 o = new T2();
            o.joint = br.ReadUInt16();
            o.ax = br.ReadByte();
            o.t9cnt = br.ReadByte();
            o.t9index = br.ReadUInt16();
            return o;
        }
        public T2 Gett2x(int x) {
            si.Position = 0x90 + offt2x + 6 * x;
            T2 o = new T2();
            o.joint = br.ReadUInt16();
            o.ax = br.ReadByte();
            o.t9cnt = br.ReadByte();
            o.t9index = br.ReadUInt16();
            return o;
        }
        public float Gett11(int x) {
            si.Position = 0x90 + offt11 + 4 * x;
            return br.ReadSingle();
        }
        public T9 Gett9(int x) {
            si.Position = 0x90 + offt9 + 8 * x;
            T9 o = new T9();
            o.t11off = br.ReadUInt16();
            o.t10index = br.ReadUInt16();
            o.t12index1 = br.ReadUInt16();
            o.t12index2 = br.ReadUInt16();
            return o;
        }
        public float Gett10(int x) {
            si.Position = 0x90 + offt10 + 4 * x;
            return br.ReadSingle();
        }
        public float Gett12(int x) {
            si.Position = 0x90 + offt12 + 4 * x;
            return br.ReadSingle();
        }
        public int Gett4(int x) {
            si.Position = 0x90 + offt4 + 4 * x;
            return br.ReadInt32();
        }
        public T7 Gett7(int x) {
            si.Position = 0x90 + offt7 + 8 * x;
            T7 o = new T7();
            o.hw0 = br.ReadUInt16();
            o.hw2 = br.ReadUInt16();
            br.ReadUInt16();
            o.hw6 = br.ReadUInt16();
            return o;
        }
        public T3 Gett3(int x) {
            si.Position = 0x90 + offt3 + 12 * x;
            T3 o = new T3();
            o.b0 = br.ReadByte();
            o.b1 = br.ReadByte();
            o.hw2 = br.ReadUInt16();
            o.hw4 = br.ReadUInt16();
            o.hw6 = br.ReadUInt16();
            o.w8 = br.ReadInt32();
            return o;
        }
        public T8 Gett8(int x) {
            si.Position = 0x90 + offt8 + 48 * x;
            T8 o = new T8();
            return o;
        }
        public T13 Gett13(int x) {
            si.Position = 0x90 + offt13 + 2 * x;
            T13 o = new T13();
            o.hw0 = br.ReadUInt16();
            return o;
        }
        private AxBone Gett5(int x) {
            si.Position = 0x90 + offt5 + 64 * x;
            AxBone o = new AxBone();
            o.i = br.ReadInt16(); br.ReadInt16();
            o.pi = br.ReadInt16(); br.ReadInt16();
            br.ReadInt32();
            br.ReadInt32();

            o.sx = br.ReadSingle();
            o.sy = br.ReadSingle();
            o.sz = br.ReadSingle();
            o.sw = br.ReadSingle();

            o.rx = br.ReadSingle();
            o.ry = br.ReadSingle();
            o.rz = br.ReadSingle();
            o.rw = br.ReadSingle();

            o.tx = br.ReadSingle();
            o.ty = br.ReadSingle();
            o.tz = br.ReadSingle();
            o.tw = br.ReadSingle();
            return o;
        }

        public List<T7> alt7 = new List<T7>();
        public List<T3> alt3 = new List<T3>();
        public List<int> alt4 = new List<int>();
        public List<T13> alt13 = new List<T13>();

        public int vA0 { get { throw new NotImplementedException(); } }
    }

    public class AxBone {
        public short i, ri, pi, rpi;
        public float sx, sy, sz, sw;
        public float rx, ry, rz, rw;
        public float tx, ty, tz, tw;

        public Vector4 Sxyzw { get { return new Vector4(sx, sy, sz, sw); } }
        public Vector4 Rxyzw { get { return new Vector4(rx, ry, rz, rw); } }
        public Vector4 Txyzw { get { return new Vector4(tx, ty, tz, tw); } }
    }

    public class InfoTbl {
        public InfoTbl(int cj) {
            Sxyz = new Vector4[cj];
            Rxyz = new Vector4[cj];
            Txyz = new Vector4[cj];
            for (int x = 0; x < cj; x++) {
                Sxyz[x] = Vector4.Zero;
                Rxyz[x] = Vector4.Zero;
                Txyz[x] = Vector4.Zero;
            }
        }

        /// <summary>
        /// 0x14
        /// </summary>
        public Mdlxacc offMdlx04;
        /// <summary>
        /// 0x1C
        /// </summary>
        public Vector4[] Sxyz;
        /// <summary>
        /// 0x20
        /// </summary>
        public Vector4[] Rxyz;
        /// <summary>
        /// 0x24
        /// </summary>
        public Vector4[] Txyz;
        /// <summary>
        /// 0x28
        /// </summary>
        public int tmp3;
        /// <summary>
        /// 0x2C
        /// </summary>
        public int tmp4;
    }
}
