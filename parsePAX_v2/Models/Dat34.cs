using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 48 bytes
    /// </summary>
    public class Dat34 {
        public ushort v00 { get; set; }
        public ushort v02 { get; set; }
        public ushort v04 { get; set; }
        public ushort v06 { get; set; }
        public int v08 { get; set; }
        public int v0c { get; set; }

        public int v10 { get; set; }
        public int v14 { get; set; }// off0 (based on +0x10)
        public int v18 { get; set; } // off1 (based on +0x10)
        public int v1c { get; set; } // off2 (based on +0x10)

        public ushort v20 { get; set; }
        public ushort v22 { get; set; }
        public ushort v24 { get; set; }
        public ushort v26 { get; set; }
        public int v28 { get; set; }
        public int v2c { get; set; }
    }
}
