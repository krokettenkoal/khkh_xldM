using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 1st data, vertex data
    /// 16 bytes
    /// </summary>
    public class D1Vert {
        public short x0 { get; set; }
        public short y0 { get; set; }
        public short x1 { get; set; }
        public short y1 { get; set; }
        public short x2 { get; set; }
        public short y2 { get; set; }
        public float w { get; set; }

        public override string ToString() => $"({x0},{y0}) ({x1},{y1}) ({x2},{y2}) {w}";
        
        public D1Vert(BinaryReader br) {
            x0 = br.ReadInt16();
            y0 = br.ReadInt16();
            x1 = br.ReadInt16();
            y1 = br.ReadInt16();
            x2 = br.ReadInt16();
            y2 = br.ReadInt16();
            w = br.ReadSingle();
        }
    }
}
