using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parseSEQDv2.Models.SeqdTypes {
    public class AnimationGroup { // 0x24 bytes
        [Data]
        public ushort AnimationIndex { get; set; } // Q4 idx
        [Data]
        public ushort Count { get; set; } // Q4 cnt
        [Data]
        public ushort Unk04 { get; set; }
        [Data]
        public ushort Unk06 { get; set; } // flg?
        [Data]
        public int Tick1 { get; set; } // tick1
        [Data]
        public int Tick2 { get; set; } // tick2
        [Data]
        public int Unk10 { get; set; } // v?
        [Data]
        public int Unk14 { get; set; } // v?
        [Data]
        public int Unk18 { get; set; } // v?
        [Data]
        public int Unk1c { get; set; } // v?
        [Data]
        public int Unk20 { get; set; } // v?

        public AnimationGroup() { }

        public AnimationGroup(BinaryReader br) {
            AnimationIndex = br.ReadUInt16();
            Count = br.ReadUInt16();
            Unk04 = br.ReadUInt16();
            Unk06 = br.ReadUInt16();
            Tick1 = br.ReadInt32();
            Tick2 = br.ReadInt32();

            Unk10 = br.ReadInt32();
            Unk14 = br.ReadInt32();
            Unk18 = br.ReadInt32();
            Unk1c = br.ReadInt32();
            Unk20 = br.ReadInt32();
        }

        public override string ToString() {
            return String.Format("ani({0,3},{1,2}) {2} {3:x4} {4,3} {6,3}  {7,3} {8,3} {9,3} {10,3} {11,3}"
                , AnimationIndex, Count, Unk04, Unk06, Tick1, 0, Tick2
                , Unk10, Unk14, Unk18, Unk1c, Unk20
                );
        }
    }
}
