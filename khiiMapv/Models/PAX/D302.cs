using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 3rd data, 2nd sub-data
    /// </summary>
    public class D302 {
        [Data] public ushort Unk00 { get; set; }
        [Data] public ushort ShaderIndex { get; set; }
        [Data] public int PacketLength { get; set; }
        [Data] public int Unk08 { get; set; }
        [Data] public int Unk0c { get; set; }

        public byte[] PacketData { get; set; }
    }
}
