using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CustVPU1;
using System.Reflection;
using System.Diagnostics;

namespace Prayvif1 {
    public class VPU1Sim {
        TextWriter wr;
        GSim gsim;
        Core1 c1;
        int era = 0;
        bool tryDisasm = true;
        GSWr gswr;
        int vifi = 0;

        public byte[] Mem = new byte[16 * 1024];
        public byte[] Micro = new byte[16 * 1024];

        SortedDictionary<int, Funct> dict = new SortedDictionary<int, Funct>();

        public VPU1Sim(GSim gsim, TextWriter wr, GSWr gswr) {
            this.gsim = gsim;
            this.wr = wr;
            this.c1 = new Core1(Mem, Micro, gsim);
            this.gswr = gswr;
        }

        public void Clear() {
            if (dict.Count != 0) {
                dict.Clear();
                era++;
                tryDisasm = true;
            }
        }

        public void Activate() {
            Debug.WriteLine("# a @ " + c1.pc.ToString("X4"));
            PreActivate();
            vifi++;
            gswr.wr.WriteLine("XGKICK " + era + " " + vifi);
            //if (vifi == 199) Debug.Fail("Here");
#if true
            if (tryDisasm) {
                tryDisasm = false;
                StringBuilder s = new StringBuilder();
                int v = 0x00337e08;
                vu1Disarm.VU1Da dis = new vu1Disarm.VU1Da();
                dis.Decode(c1.Micro);
                for (int x = 0; x < 2048; x++) {
                    dis.L[x].al[0] = dis.L[x].al[0].ToLowerInvariant();
                    dis.U[x].al[0] = dis.U[x].al[0].ToLowerInvariant();

                    s.AppendFormat("{0:x8}   {1} \n", v, dis.L[x]); v += 4;
                    s.AppendFormat("{0:x8}   {1} \n", v, dis.U[x]); v += 4;
                }
                s.Replace("\n", "\r\n");
                File.WriteAllText(FPUt.GetAsmPath(era), s.ToString());
            }
#endif
            c1.E = false;
            while (!c1.E) {
                Funct pfn;
                if (!dict.TryGetValue(c1.pc, out pfn)) {
                    Rec1 rec1 = new Rec1();
                    string fplib = FPUt.GetLibPath((uint)c1.pc, era);
                    string typeName, methodName;
                    rec1.Recompile(c1.Micro, (uint)c1.pc, fplib, out typeName, out methodName);
                    Assembly reclib = Assembly.LoadFile(fplib);
                    object cls1 = reclib.CreateInstance(typeName, false, BindingFlags.Default, null, new object[] { c1 }, null, null);
                    pfn = (Funct)Delegate.CreateDelegate(typeof(Funct), cls1, methodName);
                    dict[c1.pc] = pfn;
                }
                pfn.Invoke();
            }
        }

        private void PreActivate() {
            WriteSimv1.Write("vu1cpxmemzzz.bin", c1);
        }
        public void Activate(int addr) {
            c1.pc = addr;
            Activate();
        }

        static class FPUt {
            public static string GetAsmPath(int era) {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_vpu1_era" + era + ".txt");
            }

            public static string GetLibPath(uint pc) {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_" + pc.ToString("x4") + ".dll");
            }

            public static string GetLibPath(uint pc, int era) {
                String dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rec");
                Directory.CreateDirectory(dir);
                return Path.Combine(dir, "_" + era + "_" + pc.ToString("x4") + ".dll");
            }
        }

        public void SetTop(int top) {
            c1.top = top;
        }

        public void Hack() {
        }
    }
}
