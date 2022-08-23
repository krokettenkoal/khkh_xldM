using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 16 bytes
    /// </summary>
    public class Dat31Vertex {
        [Data] public short x0 { get; set; }
        [Data] public short y0 { get; set; }
        [Data] public short x1 { get; set; }
        [Data] public short y1 { get; set; }
        [Data] public short x2 { get; set; }
        [Data] public short y2 { get; set; }
        [Data] public float w { get; set; }

        public override string ToString() => $"({x0},{y0}) ({x1},{y1}) ({x2},{y2}) {w}";
    }
}
