using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T4 {
        public ushort c00;
        public ushort c02;

        public T4(BinaryReader br) {
            this.c00 = br.ReadUInt16();
            this.c02 = br.ReadUInt16();
        }

        public override string ToString() {
            return string.Format("{0} {1}", c00, c02);
        }
    }
}
