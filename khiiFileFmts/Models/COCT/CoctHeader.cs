using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xe.BinaryMapper;
using YamlDotNet.Serialization;

namespace khiiFileFmts.Models.COCT {
    public class CoctHeader {
        [YamlIgnore] [Data] public int Magic { get; set; }
        [YamlIgnore] [Data] public int Ver1 { get; set; }
        /// <summary>
        /// NumCol?
        /// </summary>
        [Data] public int Unk08 { get; set; }
        [Data] public int Unk0c { get; set; }
        [Data] public int Unk10 { get; set; }
        [Data] public int Unk14 { get; set; }
        [YamlIgnore] [Data] public Section Co1 { get; set; }
        [YamlIgnore] [Data] public Section Co2 { get; set; }
        [YamlIgnore] [Data] public Section Co3 { get; set; }
        [YamlIgnore] [Data] public Section Co4 { get; set; }
        [YamlIgnore] [Data] public Section Co5 { get; set; }


        public class Section {
            [Data] public int Offset { get; set; }
            [Data] public int Length { get; set; }

            public override string ToString() => $"{Offset}, {Length}";
        }
    }
}
