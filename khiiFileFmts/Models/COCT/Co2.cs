using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiFileFmts.Models.COCT {
    public class Co2 {
        [Data] public short MinX { get; set; }
        [Data] public short MinY { get; set; }
        [Data] public short MinZ { get; set; }
        [Data] public short MaxX { get; set; }
        [Data] public short MaxY { get; set; }
        [Data] public short MaxZ { get; set; }
        [Data] public short Co3Index { get; set; }
        [Data] public short Co3Count { get; set; }
        [Data] public short Unk10 { get; set; }
        [Data] public short Unk12 { get; set; }
    }
}
