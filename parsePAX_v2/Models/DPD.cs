using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    public class DPD {
        public const int Signature = 0x96;

        [Data] public int Sig { get; set; }

        [Data] public int n31 { get; set; }
        [Data] public List<int> EffectsGroupOffsets { get; set; }

        [Data] public int n32 { get; set; }
        [Data] public List<int> Dat32Offsets { get; set; }

        [Data] public int n33 { get; set; }
        [Data] public List<int> Dat33Offsets { get; set; }

        [Data] public int n34 { get; set; }
        [Data] public List<int> Dat34Offsets { get; set; }

        [Data] public int n35 { get; set; }
        [Data] public List<int> Dat35Offsets { get; set; }

        static Logger log = LogManager.GetCurrentClassLogger();

        public static DPD ReadObject(Stream stream, int baseOffset) {
            log.Info("DPDHeader at {0:x}", baseOffset);
            stream.Position = baseOffset;
            var it = BinaryMapping.ReadObject<DPD>(stream);

            it.EffectsGroupList = it.EffectsGroupOffsets
                .Select(offset => EffectsGroup.ReadObject(stream, baseOffset + offset))
                .ToList();

            it.Dat32List = it.Dat32Offsets
                .Select(
                    offset => {
                        stream.Position = baseOffset + offset;
                        log.Info("Dat32 at {0:x}", stream.Position);
                        return BinaryMapping.ReadObject<Dat32>(stream);
                    }
                )
                .ToList();

            it.Dat33List = it.Dat33Offsets
                .Select(
                    offset => {
                        stream.Position = baseOffset + offset;
                        log.Info("Dat33 at {0:x}", stream.Position);
                        return BinaryMapping.ReadObject<Dat33>(stream);
                    }
                )
                .ToList();

            return it;
        }

        public List<EffectsGroup> EffectsGroupList { get; set; }
        public List<Dat32> Dat32List { get; set; }
        public List<Dat33> Dat33List { get; set; }
        public List<Dat34> Dat34List { get; set; }
        public List<Dat35> Dat35List { get; set; }
    }
}
