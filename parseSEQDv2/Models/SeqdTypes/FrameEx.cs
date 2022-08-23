using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Models.SeqdTypes {
    public class FrameEx { // 20 bytes
        [Data]
        public int Left { get; set; }
        [Data]
        public int Top { get; set; }
        [Data]
        public int Right { get; set; }
        [Data]
        public int Bottom { get; set; }
        [Data]
        public int FrameIndex { get; set; } // Q1 idx

        [YamlIgnore]
        public int Width { get { return Right - Left; } }
        [YamlIgnore]
        public int Height { get { return Bottom - Top; } }

        public FrameEx() { }

        public FrameEx(BinaryReader br) {
            Left = br.ReadInt32();
            Top = br.ReadInt32();
            Right = br.ReadInt32();
            Bottom = br.ReadInt32();
            FrameIndex = br.ReadInt32();
        }

        public override string ToString() {
            return String.Format("({0,4},{1,4},{2,4},{3,4}) f({4})"
                , Left, Top, Right, Bottom, FrameIndex);
        }
    }
}
