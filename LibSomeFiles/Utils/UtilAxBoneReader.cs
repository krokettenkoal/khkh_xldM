using khkh_xldMii.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii.Utils {
    internal class UtilAxBoneReader {
        public static AxBone read(BinaryReader br) {
            var o = new AxBone();
            o.cur = br.ReadInt32();
            o.parent = br.ReadInt32();
            o.v08 = br.ReadInt32();
            o.v0c = br.ReadInt32();
            o.x1 = br.ReadSingle(); o.y1 = br.ReadSingle(); o.z1 = br.ReadSingle(); o.w1 = br.ReadSingle();
            o.x2 = br.ReadSingle(); o.y2 = br.ReadSingle(); o.z2 = br.ReadSingle(); o.w2 = br.ReadSingle();
            o.x3 = br.ReadSingle(); o.y3 = br.ReadSingle(); o.z3 = br.ReadSingle(); o.w3 = br.ReadSingle();
            return o;
        }
    }
}
