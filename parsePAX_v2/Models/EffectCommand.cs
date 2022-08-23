using parsePAX_v2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 16 bytes
    /// P14
    /// </summary>
    public class EffectCommand {
        [Data]
        public int Command { get; set; }
        [Data]
        public ushort ParamLength { get; set; }
        [Data]
        public ushort ParamsCount { get; set; }
        [Data]
        public int OffsetParameters { get; set; }
        [Data]
        public int Offset2 { get; set; }

        public EffectType EffectType => (EffectType)Command;

        public byte[][] ChunksList { get; set; }

        public override string ToString() => $"{EffectType} {ParamLength}x{ParamsCount} @{OffsetParameters} {Offset2}";
    }
}
