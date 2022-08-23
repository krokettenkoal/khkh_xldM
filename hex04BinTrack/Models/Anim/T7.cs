using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T7 {
        public short v00;
        public short v02;
        public short v04;
        public short v06;

        public T7(BinaryReader br) {
            v00 = br.ReadInt16();
            v02 = br.ReadInt16();
            v04 = br.ReadInt16();
            v06 = br.ReadInt16();
        }

        public override string ToString() {
            return $"{v00,3} {v02,3} {v04,3} {v06,3}";
        }
    }
}
