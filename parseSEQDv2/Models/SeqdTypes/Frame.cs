using parseSEQDv2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xe.BinaryMapper;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Models.SeqdTypes {
    /// <summary>
    /// Part(s) in picture
    /// </summary>
    public class Frame {
        // 0x2C bytes
        [Data]
        public int v0 { get; set; } // 0
        [Data]
        public int Left { get; set; } // pic x0
        [Data]
        public int Top { get; set; } // pic y0
        [Data]
        public int Right { get; set; } // pic x1
        [Data]
        public int Bottom { get; set; } // pic y1
        [Data]
        public float TextureScrollX { get; set; } // 0
        [Data]
        public float TextureScrollY { get; set; } // 0
        [Data]
        public uint Color0 { get; set; } // vclr0
        [Data]
        public uint Color1 { get; set; } // vclr1
        [Data]
        public uint Color2 { get; set; } // vclr2
        [Data]
        public uint Color3 { get; set; } // vclr3

        [YamlIgnore]
        public int Height { get { return Bottom - Top; } }
        [YamlIgnore]
        public int Width { get { return Right - Left; } }

        [YamlIgnore]
        public int WinColor0 { get { return KH2c4cvt.ToWin(Color0); } }
        [YamlIgnore]
        public int WinColor1 { get { return KH2c4cvt.ToWin(Color1); } }
        [YamlIgnore]
        public int WinColor2 { get { return KH2c4cvt.ToWin(Color2); } }
        [YamlIgnore]
        public int WinColor3 { get { return KH2c4cvt.ToWin(Color3); } }

        public Frame() { }

        public Frame(BinaryReader br) {
            v0 = br.ReadInt32();
            Left = br.ReadInt32();
            Top = br.ReadInt32();
            Right = br.ReadInt32();
            Bottom = br.ReadInt32();
            TextureScrollX = br.ReadSingle();
            TextureScrollY = br.ReadSingle();
            Color0 = br.ReadUInt32();
            Color1 = br.ReadUInt32();
            Color2 = br.ReadUInt32();
            Color3 = br.ReadUInt32();
        }

        public override string ToString() {
            return String.Format("{0,3} ({1,3},{2,3},{3,3},{4,3}) {5,3} {6,3}  {7:x8} {8:x8} {9:x8} {10:x8}"
                , v0, Left, Top, Right, Bottom, TextureScrollX, TextureScrollY
                , Color0, Color1, Color2, Color3);
        }
    }
}
