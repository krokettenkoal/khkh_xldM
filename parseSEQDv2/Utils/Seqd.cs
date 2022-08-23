using parseSEQDv2.Models.SeqdTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Utils {
    public class Seqd {
        [YamlIgnore]
        public PicIMGD[] Pictures { get; }
        public List<Frame> FrameList { get; set; } = new List<Frame>();
        public List<FrameEx> FrameExList { get; set; } = new List<FrameEx>();
        public List<FrameGroup> FrameGroupList { get; set; } = new List<FrameGroup>();
        public List<Animation> AnimList { get; set; } = new List<Animation>();
        public List<AnimationGroup> AnimGroupList { get; set; } = new List<AnimationGroup>();
        [YamlIgnore]
        public string BarId { get; }

        private string display;

        public Seqd() { }

        public Seqd(PicIMGD[] pictures, ArraySegment<byte> binSeg, string display, string barId) {
            Pictures = pictures;
            this.display = display;
            this.BarId = barId;

            var si = new MemoryStream(binSeg.Array, binSeg.Offset, binSeg.Count, false);
            var br = new BinaryReader(si);

            {
                si.Position = 8;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 0x2C * x;
                    FrameList.Add(new Frame(br));
                }
            }
            {
                si.Position = 0x10;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 20 * x;
                    FrameExList.Add(new FrameEx(br));
                }
            }
            {
                si.Position = 0x18;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 4 * x;
                    FrameGroupList.Add(new FrameGroup(br));
                }
            }
            {
                si.Position = 0x20;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 0x90 * x;
                    AnimList.Add(new Animation(br));
                }
            }
            {
                si.Position = 0x28;
                int cnt = br.ReadInt32();
                int off = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    si.Position = off + 0x24 * x;
                    AnimGroupList.Add(new AnimationGroup(br));
                }
            }
        }

        public override string ToString() => display;

        public MemoryStream PackSeqd() {
            var seqdStream = new MemoryStream();
            var writer = new BinaryWriter(seqdStream);
            var encoding = Encoding.GetEncoding("latin1");

            writer.Write(encoding.GetBytes("SEQD"));
            writer.Write((int)0x160);

            writer.Write((int)FrameList.Count);
            var frameOffset = new DelayInt32Writer(writer, 1);

            writer.Write((int)FrameExList.Count);
            var frameExOffset = new DelayInt32Writer(writer, 1);

            writer.Write((int)FrameGroupList.Count);
            var frameGroupOffset = new DelayInt32Writer(writer, 1);

            writer.Write((int)AnimList.Count);
            var animOffset = new DelayInt32Writer(writer, 1);

            writer.Write((int)AnimGroupList.Count);
            var animGroupOffset = new DelayInt32Writer(writer, 1);

            frameOffset.Set(0, Convert.ToInt32(seqdStream.Position));
            foreach (var one in FrameList) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            frameExOffset.Set(0, Convert.ToInt32(seqdStream.Position));
            foreach (var one in FrameExList) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            frameGroupOffset.Set(0, Convert.ToInt32(seqdStream.Position));
            foreach (var one in FrameGroupList) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            animOffset.Set(0, Convert.ToInt32(seqdStream.Position));
            foreach (var one in AnimList) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            animGroupOffset.Set(0, Convert.ToInt32(seqdStream.Position));
            foreach (var one in AnimGroupList) {
                Xe.BinaryMapper.BinaryMapping.WriteObject(writer, one);
            }

            return seqdStream;
        }
    }
}
