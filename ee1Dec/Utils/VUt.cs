using ee1Dec.C;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    class VUt {
        public static void readVec(Vec o, BinaryReader br) {
            o.F[0] = br.ReadSingle();
            o.F[1] = br.ReadSingle();
            o.F[2] = br.ReadSingle();
            o.F[3] = br.ReadSingle();
        }
        public static void readInt(VIv o, BinaryReader br) {
            o.UL = br.ReadUInt32();
        }
        public static void readfv(VIv o, BinaryReader br) {
            o.f = br.ReadSingle();
        }
    }
}
