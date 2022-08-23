using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 3rd data. this is header
    /// 32 bytes
    /// </summary>
    public class D3 {
        [Data] public int Unk00 { get; set; }
        [Data] public int Unk04 { get; set; }
        [Data] public int Unk08 { get; set; }
        [Data] public int Unk0c { get; set; }
        /// <summary>
        /// offset?
        /// </summary>
        [Data] public int Unk10 { get; set; }
        [Data] public ushort cnt1 { get; set; }
        [Data] public ushort cnt2 { get; set; }
        [Data] public int Unk18 { get; set; }
        /// <summary>
        /// Size -16
        /// </summary>
        [Data] public int Unk1c { get; set; }

        public List<D301> d301List = new List<D301>();

        public override string ToString() {
            return $"{cnt1}, {cnt2}";
        }
    }
}
