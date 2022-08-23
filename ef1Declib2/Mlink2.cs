//#define UsePressed_eeram
#define AllowRec1
//#define Allow_DEB_eeram01

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ee1Dec.C;
using ee1Dec;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Xml;
using ef1Declib2.Properties;
using System.Diagnostics;

namespace ef1Declib2 {
    public class Mlink2 {
        Mobpax_ o1 = new Mobpax_();
        CustEE ee = new CustEE();

        public CustEE cee { get { return ee; } }

        class CnfUt {
            public static string findeeram {
                get {
                    string s1 = Settings.Default.eerambin;
                    if (File.Exists(s1)) return s1;
                    string s2 = Path.Combine(Environment.CurrentDirectory, "RAM.bin");
                    if (File.Exists(s2)) return s2;
                    string s3 = Path.Combine(Environment.CurrentDirectory, "eeramx.bin");
                    if (File.Exists(s3)) return s3;
                    return s3;
                }
            }
        }

        public void InitPax() {
            o1.Init0();
            o1.Init1(ee);

#if UsePressed_eeram
            Szexp.Decode(Resources.eeramx, ee.ram, ee.ram.Length);
#else
            using (FileStream fsi = File.OpenRead(CnfUt.findeeram)) {
                fsi.Read(ee.ram, 0, 32 * 1024 * 1024);
            }
#endif
        }

        public void RunPax() {
            ee.at.UD0 = 0U;
            ee.v0.UD0 = 0U;
            ee.v1.UD0 = 0U;
            ee.a0.UD0 = 0x1a64820;
            ee.a1.UD0 = 0U;
            ee.a2.UD0 = 0U;
            ee.a3.UD0 = 0U;
            ee.t0.UD0 = 0U;
            ee.t1.UD0 = 0U;
            ee.t2.UD0 = 0U;
            ee.t3.UD0 = 0U;
            ee.t4.UD0 = 0U;
            ee.t5.UD0 = 0U;
            ee.t6.UD0 = 0U;
            ee.t7.UD0 = 0U;
            ee.s0.UD0 = 0U;
            ee.s1.UD0 = 0U;
            ee.s2.UD0 = 0U;
            ee.s3.UD0 = 0U;
            ee.s4.UD0 = 0U;
            ee.s5.UD0 = 0U;
            ee.s6.UD0 = 0U;
            ee.s7.UD0 = 0U;
            ee.t8.UD0 = 0U;
            ee.t9.UD0 = 0U;
            ee.k0.UD0 = 0U;
            ee.k1.UD0 = 0U;
            ee.gp.UD0 = 0U;
            ee.sp.UD0 = 0x2000000U;
            ee.s8.UD0 = 0U;
            ee.ra.UD0 = 0xFFFFFFFFU;

            ee.eeWrite32(ee.a0.UL0 + 0x88U, 0x53FAA0);
            //ee.eeWrite32(ee.a0.UL0 + 0x8CU, );
            //ee.eeWrite32(ee.a0.UL0 + 0x78U, );
            //ee.eeWrite32(ee.a0.UL0 + 0x7CU, );
            //ee.eeWrite32(ee.a0.UL0 + 0x70U, );

            for (int x = 0; x < 32; x++) ee.fpr[x].f = 0;

            ee.pc = 0x1e66e8;
            while (ee.pc != 0xFFFFFFFFU) {
                if (o1.pfns.ContainsKey(ee.pc) || MobRecUt.Rec1(ee.pc, o1.pfns, ee)) {
                    o1.pfns[ee.pc]();
                }
                else throw new RecfnnotFound(ee.pc, "pax_");
            }
        }

        public static byte[] eeram = File.ReadAllBytes(CnfUt.findeeram);
    }

    class MobRecUt {
        public delegate void Tx8();

        public static bool Rec1(uint addr, SortedDictionary<uint, MobUt.Tx8> dicti2a, CustEE ee) {
#if AllowRec1
            string dirlib = Settings.Default.dirlib;
            string flib = Myrec.Getflib(addr, dirlib);
            if (!File.Exists(flib)) {
                Myrec.Privrec1(addr, new MemoryStream(Uteeram.eeram, false), dirlib);
                if (!File.Exists(flib)) return false;
            }

            Assembly lib = Assembly.LoadFile(flib);
            Type cls1 = lib.GetType("ee1Dec.C.Class1");
            object o = Activator.CreateInstance(cls1, ee);
            MethodInfo mi = cls1.GetMethod(LabUt.addr2Funct(addr));
            MobUt.Tx8 tx8 = (MobUt.Tx8)Delegate.CreateDelegate(typeof(MobUt.Tx8), o, mi);
            dicti2a[addr] = tx8;
            System.Diagnostics.Debug.WriteLine("## " + addr.ToString("X8"));
            return true;
#else
            return false;
#endif
        }
    }

    class Szexp {
        public static void Decode(byte[] eeramx, byte[] eeram, Int64 outSize) {
            MemoryStream sic = new MemoryStream(eeramx, false);
            BinaryReader br = new BinaryReader(sic);
            sic.Position = sic.Length - 8 * 3;
            Int64 pos0 = br.ReadInt64();
            Int64 pos1 = br.ReadInt64();
            Int64 pos2 = br.ReadInt64();
            sic.Position = pos1; byte[] prop = br.ReadBytes((int)(pos2 - pos1));
            sic.Position = pos0;

            SevenZip.Compression.LZMA.Decoder dec = new SevenZip.Compression.LZMA.Decoder();

            dec.SetDecoderProperties(prop);
            dec.Code(sic, new MemoryStream(eeram, true), pos1 - pos0, outSize, null);
        }
    }
}
namespace ee1Dec.C {
    public partial class Mobpax_ { // partial to your tocs.pax_.cs
        internal SortedDictionary<uint, MobUt.Tx8> pfns { get { return dicti2a; } }
        internal void Init0() { initfns(); }
        internal void Init1(CustEE ee) { this.ee = ee; }
    }

    class Uteeram {
        class CnfUt {
            public static string findeeram {
                get {
                    string[] al = new string[] {
                        Settings.Default.eerambin,
                        Path.Combine(Environment.CurrentDirectory, "eeram.bin"),
                    };
                    foreach (string s in al) {
                        if (File.Exists(s)) return s;
                    }
                    return al[1];
                }
            }
        }

        public static byte[] eeram = File.ReadAllBytes(CnfUt.findeeram);
    }

    class MobRecUt {
        public delegate void Tx8();

        public static bool Rec1(uint addr, SortedDictionary<uint, MobUt.Tx8> dicti2a, CustEE ee) {
#if AllowRec1
            string dirlib = Settings.Default.dirlib;
            string flib = Myrec.Getflib(addr, dirlib);
            if (!File.Exists(flib)) {
                Myrec.Privrec1(addr, new MemoryStream(Uteeram.eeram, false), dirlib);
                if (!File.Exists(flib)) return false;
            }

            Assembly lib = Assembly.LoadFile(flib);
            Type cls1 = lib.GetType("ee1Dec.C.Class1");
            object o = Activator.CreateInstance(cls1, ee);
            MethodInfo mi = cls1.GetMethod(LabUt.addr2Funct(addr));
            MobUt.Tx8 tx8 = (MobUt.Tx8)Delegate.CreateDelegate(typeof(MobUt.Tx8), o, mi);
            dicti2a[addr] = tx8;
            System.Diagnostics.Debug.WriteLine("## " + addr.ToString("X8"));
            return true;
#else
            return false;
#endif
        }
    }

    class Szexp {
        public static void Decode(byte[] eeramx, byte[] eeram, Int64 outSize) {
            MemoryStream sic = new MemoryStream(eeramx, false);
            BinaryReader br = new BinaryReader(sic);
            sic.Position = sic.Length - 8 * 3;
            Int64 pos0 = br.ReadInt64();
            Int64 pos1 = br.ReadInt64();
            Int64 pos2 = br.ReadInt64();
            sic.Position = pos1; byte[] prop = br.ReadBytes((int)(pos2 - pos1));
            sic.Position = pos0;

            SevenZip.Compression.LZMA.Decoder dec = new SevenZip.Compression.LZMA.Decoder();

            dec.SetDecoderProperties(prop);
            dec.Code(sic, new MemoryStream(eeram, true), pos1 - pos0, outSize, null);
        }
    }
}
