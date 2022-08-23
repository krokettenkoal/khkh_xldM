using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.Coct {
    public class Co3 {
        public short v0, v2, v4, v6, v8, va, vc, ve;

        public int vi0 { get { return v2; } }
        public int vi1 { get { return v4; } }
        public int vi2 { get { return v6; } }
        public int vi3 { get { return v8; } }

        public int PlaneCo5 { get { return va; } }
        public int Co6 { get { return vc; } }
        public int Co7 { get { return ve; } }

        public override string ToString() {
            return String.Format("{0,4} PolyCo4({1,4},{2,4},{3,4},{4,4}) PlaneCo5({5,3}) Co6({6,3}) Co7({7,3})"
                , v0, v2, v4, v6, v8, va, vc, ve);
        }

        public Co3(BinaryReader br) {
            v0 = br.ReadInt16();
            v2 = br.ReadInt16();
            v4 = br.ReadInt16();
            v6 = br.ReadInt16();
            v8 = br.ReadInt16();
            va = br.ReadInt16();
            vc = br.ReadInt16();
            ve = br.ReadInt16();
        }
    }
}
