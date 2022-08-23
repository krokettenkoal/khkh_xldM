using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parseSEQDv2.Models.SeqdTypes {
    public class FrameGroup { // 4 bytes
        [Data]
        public ushort Start { get; set; } // Q2 idx (now FrameEx)
        [Data]
        public ushort Count { get; set; } // ?

        public FrameGroup() { }

        public FrameGroup(BinaryReader br) {
            Start = br.ReadUInt16();
            Count = br.ReadUInt16();
        }

        public override string ToString() {
            return String.Format("{0,3} {1,3}", Start, Count);
        }
    }
}
