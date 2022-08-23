using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiFileFmts.Models.COCT {
    public class Co1 {
        [Data] public short Unk00 { get; set; }
        [Data] public short Unk02 { get; set; }
        [Data] public short Unk04 { get; set; }
        [Data] public short Unk06 { get; set; }
        [Data] public short Unk08 { get; set; }
        [Data] public short Unk0a { get; set; }
        [Data] public short Unk0c { get; set; }
        [Data] public short Unk0e { get; set; }
        [Data] public short MinX { get; set; }
        [Data] public short MinY { get; set; }
        [Data] public short MinZ { get; set; }
        [Data] public short MaxX { get; set; }
        [Data] public short MaxY { get; set; }
        [Data] public short MaxZ { get; set; }
        [Data] public short Unk1c { get; set; }
        [Data] public short Unk1e { get; set; }

    }
}
