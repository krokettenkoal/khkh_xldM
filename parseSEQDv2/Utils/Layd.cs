using parseSEQDv2.Models.LaydTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Utils {
    public class Layd {
        [YamlIgnore]
        public PicIMGD[] Pictures { get; set; }
        public List<SequenceProperty> PropList { get; set; } = new List<SequenceProperty>();
        public List<SequenceGroup> GroupList { get; set; } = new List<SequenceGroup>();
        [YamlIgnore]
        public List<int> ListSeqdOffset { get; set; } = new List<int>();

        /// <summary>
        /// Seqd is externally inserted.
        /// </summary>
        public List<Seqd> SeqdList { get; set; } = new List<Seqd>();

        private string display;

        public Layd(PicIMGD[] pictures, byte[] bin, string display) {
            Pictures = pictures;
            this.display = display;

            MemoryStream stream = new MemoryStream(bin, false);
            BinaryReader reader = new BinaryReader(stream);
            String head;
            if ((head = Encoding.ASCII.GetString(reader.ReadBytes(4))) != "LAYD") {
                throw new InvalidDataException(head + " ≠ LAYD");
            }

            {
                stream.Position = 0x08;
                int cnt = reader.ReadInt32();
                int off = reader.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    stream.Position = off + 24 * x;
                    PropList.Add(new SequenceProperty(reader));
                }
            }

            {
                stream.Position = 0x10;
                int cnt = reader.ReadInt32();
                int off = reader.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    stream.Position = off + 20 * x;
                    GroupList.Add(new SequenceGroup(reader));
                }
            }

            {
                stream.Position = 0x18;
                int cnt = reader.ReadInt32();
                int off = reader.ReadInt32();
                stream.Position = off;
                for (int x = 0; x < cnt; x++) {
                    ListSeqdOffset.Add(reader.ReadInt32());
                }
            }
        }

        public override string ToString() => display;

        public Layd() { }

        public static Layd Wrap2dd(PicIMGD[] pictures, Seqd seqd, string display) {
            var it = new Layd();
            it.Pictures = pictures;
            it.SeqdList.Add(seqd);
            it.display = display;
            it.GroupList.Add(SequenceGroup.WrapFor2dd);
            it.PropList.Add(SequenceProperty.WrapFor2dd);
            return it;
        }

        public MemoryStream PackLayd() {
            var encoding = Encoding.GetEncoding("latin1");
            MemoryStream laydStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(laydStream, encoding);

            writer.Write("LAYD".ToCharArray(), 0, 4);
            writer.Write((int)0x100);
            writer.Write((int)PropList.Count);
            var sequencePropertyOffset = new DelayInt32Writer(writer, 1);
            writer.Write((int)GroupList.Count);
            var sequenceGroupOffset = new DelayInt32Writer(writer, 1);
            writer.Write((int)SeqdList.Count);
            var seqdOffsetListOffset = new DelayInt32Writer(writer, 1);

            sequencePropertyOffset.Set(0, Convert.ToInt32(laydStream.Position));
            foreach (var (one, index) in PropList.Select((item, index) => (item, index))) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            sequenceGroupOffset.Set(0, Convert.ToInt32(laydStream.Position));
            foreach (var (one, index) in GroupList.Select((item, index) => (item, index))) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            seqdOffsetListOffset.Set(0, Convert.ToInt32(laydStream.Position));
            var seqdOffsetList = new DelayInt32Writer(writer, SeqdList.Count);

            foreach (var (one, index) in SeqdList.Select((item, index) => (item, index))) {
                seqdOffsetList.Set(index, Convert.ToInt32(laydStream.Position));
                MemoryStream seqdStream = one.PackSeqd();
                seqdStream.Position = 0;
                seqdStream.CopyTo(laydStream);
            }

            return laydStream;
        }
    }
}
