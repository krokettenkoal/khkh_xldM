using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T2 {
        public byte c00, c01, c02, c03;
        public ushort c04; // t9_xxxx
        public List<T9> t9TimeLines = new List<T9>();

        public T2(BinaryReader br) {
            this.c00 = br.ReadByte();
            this.c01 = br.ReadByte();
            this.c02 = br.ReadByte();
            this.c03 = br.ReadByte();
            this.c04 = br.ReadUInt16(); // t9_xxxx
        }

        public override string ToString() {
            return string.Format("{0} {1} {2} {3} {4}"
                , c00, c01, c02, c03, c04);
        }
    }
}
