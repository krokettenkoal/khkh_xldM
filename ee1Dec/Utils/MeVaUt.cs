using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    class MeVaUt {
        public static string format(MemoryStream si, uint off) {
            BinaryReader br = new BinaryReader(si);
            si.Position = off;
            string res = br.ReadUInt32().ToString("x8");
            si.Position = off;
            res += " " + br.ReadSingle();
            return res;
        }
    }
}
