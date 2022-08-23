using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T6 {
        public short v00;
        public short v02;
        public float v04;
        public short v08;
        public short v0a;

        public T6(BinaryReader br) {
            v00 = br.ReadInt16();
            v02 = br.ReadInt16();
            v04 = br.ReadSingle();
            v08 = br.ReadInt16();
            v0a = br.ReadInt16();
        }

        public override string ToString() {
            return $"{v00,3} {v02,3} {v04,6} {v08,3} {v0a,3}";
        }
    }
}
