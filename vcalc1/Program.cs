using System;
using System.Collections.Generic;
using System.Text;

namespace vcalc1 {
    class Program {
        static void Main(string[] args) {
            float v001284c4 = 1;
            float v001284e0 = 3;
            float f20 = 8.0f;

            float f0 = 14.000000f;
            float f17 = -0.002012f;
            float f16 = 14.000000f;
            float f13 = 0.000000f;
            float f14 = -0.001993f;
            float f15 = -0.000028f;
            float f18 = 0.000024f;

            float f12 = f20;
            float f7;
            float f5;
            float f3;
            float f2;
            float f4;
            float f1;
            float f6;
            float acc;

            f16 = f16 - f13;
            f12 = f12 - f13;
            f7 = v001284c4;
            f16 = f17 / f16;
            f5 = f12 * f12;
            f3 = v001284e0;
            f2 = f5 * f12;
            f3 = f5 * f3;
            f1 = f16 * f16;
            f5 = f5 * f16;
            f2 = f2 * f1;
            f3 = f3 * f1;
            f6 = f5 + f5;
            f4 = f2 + f2;
            f5 = f2 - f5;
            f4 = f4 * f16;
            f2 = f2 - f6;
            f1 = f4 - f3;
            f2 = f2 + f12;
            f1 = f1 + f7;
            f3 = f3 - f4;
            acc = f14 * f1;
            acc = acc + f17 * f3;
            acc = acc + f15 * f2;
            f0 = acc + f18 * f5;

            Console.WriteLine(f0);
        }
    }
}
