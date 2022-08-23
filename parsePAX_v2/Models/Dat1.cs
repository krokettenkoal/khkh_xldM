using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    public class Dat1 {
        [Data] public ushort v00 { get; set; }
        [Data] public ushort v02 { get; set; }
        [Data] public ushort v04 { get; set; }
        [Data] public ushort v06 { get; set; }

        [Data] public int v08 { get; set; }
        [Data] public int v0c { get; set; }
        [Data] public int v10 { get; set; }
        [Data] public int v14 { get; set; }
        [Data] public int v18 { get; set; }

        [Data] public float v1c { get; set; }
        [Data] public float v20 { get; set; }
        [Data] public float v24 { get; set; }
        [Data] public float v28 { get; set; }
        [Data] public float v2c { get; set; }
        [Data] public float v30 { get; set; }
        [Data] public float v34 { get; set; }
        [Data] public float v38 { get; set; }
        [Data] public float v3c { get; set; }

        [Data] public int v40 { get; set; }
        [Data] public int v44 { get; set; }
        [Data] public int v48 { get; set; }
        [Data] public int v4c { get; set; }

    }
}
