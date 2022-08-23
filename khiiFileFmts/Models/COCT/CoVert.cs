using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;

namespace khiiFileFmts.Models.COCT {
    /// <summary>
    /// Co4
    /// </summary>
    public class CoVert {
        [Data] public float X { get; set; }
        [Data] public float Y { get; set; }
        [Data] public float Z { get; set; }
        [Data] public float W { get; set; }
    }
}
