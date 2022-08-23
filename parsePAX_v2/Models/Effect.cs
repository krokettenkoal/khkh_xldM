using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 40 bytes +variables
    /// P13
    /// </summary>
    public class Effect {
        [Data] public int v00 { get; set; }
        [Data] public int v04 { get; set; }
        [Data] public int v08 { get; set; }
        [Data] public int v0c { get; set; }

        [Data] public int v10 { get; set; }
        [Data] public int v14 { get; set; }
        [Data] public int v18 { get; set; }
        [Data] public int v1c { get; set; }

        [Data] public int v20 { get; set; }
        [Data] public ushort v24 { get; set; }

        [Data] public ushort EffectCommandCount { get; set; }
        [Data] public List<EffectCommand> EffectCommandList { get; set; }
    }
}
