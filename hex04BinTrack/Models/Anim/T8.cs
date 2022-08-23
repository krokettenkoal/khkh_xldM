using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T8 {
        public int a, b;
        public float c;
        public float d;
        public float e0;
        public float e1;
        public float e2;
        public float e3;
        public float f0;
        public float f1;
        public float f2;
        public float f3;

        public T8(BinaryReader br) {
            a = br.ReadInt32();
            b = br.ReadInt32();
            c = br.ReadSingle();
            d = br.ReadSingle();
            e0 = br.ReadSingle();
            e1 = br.ReadSingle();
            e2 = br.ReadSingle();
            e3 = br.ReadSingle();
            f0 = br.ReadSingle();
            f1 = br.ReadSingle();
            f2 = br.ReadSingle();
            f3 = br.ReadSingle();
        }

        public override string ToString() {
            return $"{a,4} {b,4} {c,7:0.00} {d,7:0.00} ({e0,7:0.00},{e1,7:0.00},{e2,7:0.00},{e3,7:0.00}) ({f0,7:0.00},{f1,7:0.00},{f2,7:0.00},{f3,7:0.00})";
        }
    }
}
