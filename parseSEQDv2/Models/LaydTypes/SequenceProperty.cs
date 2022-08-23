using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parseSEQDv2.Models.LaydTypes {
    /// <summary>
    /// Lay1: 24 bytes
    /// </summary>
    public class SequenceProperty {
        [Data]
        public int TextureIndex { get; set; } // texture index?
        [Data]
        public int SequenceIndex { get; set; } // seq index?
        [Data]
        public int AnimationGroup { get; set; } // ?
        [Data]
        public int ShowAtFrame { get; set; } // degree?
        [Data]
        public int PositionX { get; set; } // x?
        [Data]
        public int PositionY { get; set; } // y?

        public SequenceProperty(BinaryReader br) {
            TextureIndex = br.ReadInt32();
            SequenceIndex = br.ReadInt32();
            AnimationGroup = br.ReadInt32();
            ShowAtFrame = br.ReadInt32();
            PositionX = br.ReadInt32();
            PositionY = br.ReadInt32();
        }

        public SequenceProperty() { }

        public static SequenceProperty WrapFor2dd => new SequenceProperty { };

        public override string ToString() {
            return String.Format("tex({0}) seq({1}) anigrp({2,2}) at({3,3}) ({4,3}, {5,3})"
                , TextureIndex, SequenceIndex, AnimationGroup, ShowAtFrame, PositionX, PositionY);
        }
    }
}
