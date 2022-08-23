using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T3 {
        public byte c00;  // misc0
        public byte c01;  // misc1
        public ushort c02;  // iki
        public ushort c04;  // ikp
        public short c06;  // misc2
        public short c08;    // misc3a
        public short c0a;    // misc3b

        public short T8Idx => c06;
        public ushort BoneIdx => c02;
        public ushort IKHelperIdx => c04;

        public T3(BinaryReader br) {
            this.c00 = br.ReadByte();
            this.c01 = br.ReadByte();
            this.c02 = br.ReadUInt16();
            this.c04 = br.ReadUInt16();
            this.c06 = br.ReadInt16();
            this.c08 = br.ReadInt16();
            this.c0a = br.ReadInt16();
        }

        public override string ToString() {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}"
                , c00, c01, c02, c04, c06, c08, c0a);
        }
    }
}
