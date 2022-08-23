using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiFileFmts.Models.COCT {
    public class Co3 {
        [Data] public short Unk00 { get; set; }
        [Data] public short VertIndex0 { get; set; }
        [Data] public short VertIndex1 { get; set; }
        [Data] public short VertIndex2 { get; set; }
        [Data] public short VertIndex3 { get; set; }
        [Data] public short PlaneIndex { get; set; }
        [Data] public short Unk0c { get; set; }
        [Data] public short Unk0e { get; set; }

    }
}
