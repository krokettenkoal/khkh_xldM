using parseSEQDv2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Models.Unbar {
    public class BarYaml {
        public Item[] BarItems { get; set; }

        public class Item {
            public string Id { get; set; }
            public int Key { get; set; }
            public string File { get; set; }
        }

        public MemoryStream PackBar(string baseDir) {
            MemoryStream barStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(barStream);
            var encoding = Encoding.GetEncoding("latin1");

            writer.Write(encoding.GetBytes("BAR\x1"));
            writer.Write((int)BarItems.Length);
            writer.Write((int)0);
            writer.Write((int)0);
            List<DelayInt32Writer> barOffsets = new List<DelayInt32Writer>();
            List<DelayInt32Writer> barLengths = new List<DelayInt32Writer>();
            foreach (var item in BarItems) {
                writer.Write((int)item.Key);
                writer.Write(encoding.GetBytes(item.Id.PadRight(4, '\0').Substring(0, 4)));
                barOffsets.Add(new DelayInt32Writer(writer, 1));
                barLengths.Add(new DelayInt32Writer(writer, 1));
            }

            foreach (var (item, index) in BarItems.Select((item, index) => (item, index))) {
                var binFile = Path.Combine(baseDir, item.File);
                var bin = File.ReadAllBytes(binFile);
                StreamAlignment.Int128(barStream);
                barOffsets[index].Set(0, Convert.ToInt32(barStream.Position));
                barLengths[index].Set(0, bin.Length);
                writer.Write(bin);
            }

            return barStream;
        }
    }
}
