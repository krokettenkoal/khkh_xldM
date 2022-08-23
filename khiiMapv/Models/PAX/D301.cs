using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 3rd data, 1st sub-data
    /// 8 bytes
    /// </summary>
    public class D301 {
        [Data] public ushort Ofs { get; set; }
        [Data] public byte Unk02 { get; set; }
        [Data] public byte PacketCount { get; set; }
        [Data] public int Unk04 { get; set; }

        public List<D302> D302List = new List<D302>();
    }
}
