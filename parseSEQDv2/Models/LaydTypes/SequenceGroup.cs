using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Models.LaydTypes {
    /// <summary>
    /// Lay2: 20 bytes
    /// </summary>
    public class SequenceGroup {
        [Data]
        public ushort PropertyIndex { get; set; } // L1 index
        [Data]
        public ushort PropertyCount { get; set; } // L1 cnt
        [Data]
        public int v04 { get; set; } // zero?
        [Data]
        public int v08 { get; set; } // zero?
        [Data]
        public int v0c { get; set; } // zero?
        [Data]
        public int v10 { get; set; } // zero?

        public SequenceGroup(BinaryReader br) {
            PropertyIndex = br.ReadUInt16();
            PropertyCount = br.ReadUInt16();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
            v0c = br.ReadInt32();
            v10 = br.ReadInt32();
        }

        public SequenceGroup() { }

        public static SequenceGroup WrapFor2dd => new SequenceGroup { PropertyIndex = 0, PropertyCount = 1, };

        public override string ToString() {
            return String.Format("prop({0}, {1}) {2} {3} {4} {5}"
                , PropertyIndex, PropertyCount, v04, v08, v0c, v10);
        }
    }
}
