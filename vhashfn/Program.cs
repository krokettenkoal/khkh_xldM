using System;
using System.Collections.Generic;
using System.Text;

namespace vhashfn {
    class Program {
        static void Main(string[] args) {
            uint v0 = o1ac6c8.calc(Encoding.ASCII.GetBytes("obj/W_EX010_RX.mset" + "\0"));
            Console.WriteLine(v0.ToString("x8"));
        }
    }

    static class o1ac6c8 {
        public static uint calc(byte[] bin) {
            int a0 = 0;
            uint t7 = bin[a0];
            uint v0 = uint.MaxValue;
            if (t7 != 0) {
                a0++;
                uint t4 = 0x04c11db7;
                uint t5 = 0x80000000;
                do {
                    t7 <<= 24;
                    int t6 = 7;
                    v0 ^= t7;
                    t7 = v0 & t5;
                    do {
                        if (t7 == 0) {
                            t7 = v0 << 1;
                            v0 <<= 1;
                        }
                        else {
                            t7 = v0 << 1;
                            v0 = t7 ^ t4;
                        }
                        t6--;
                        t7 = v0 & t5;
                    } while (t6 >= 0);
                    t7 = bin[a0];
                    a0++;
                } while (t7 != 0);
            }
            return ~v0;
        }
    }
}
