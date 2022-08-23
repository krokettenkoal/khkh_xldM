using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T1 {
        public ushort c00, c02;
        public float c04;

        public T1(BinaryReader br) {
            this.c00 = br.ReadUInt16();
            this.c02 = br.ReadUInt16();
            this.c04 = br.ReadSingle();
        }

        public override string ToString() {
            return string.Format("{0} {1} {2}"
                , c00, c02, c04);
        }
    }
}
