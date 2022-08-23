using NLog;
using parsePAX_v2.Extensions;
using parsePAX_v2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    public class Pax {
        public const int Signature = 0x5f584150;

        [Data] public int Sig { get; set; }
        [Data] public int v04 { get; set; }
        [Data] public int CountDat1 { get; set; }
        [Data] public int OffsetHeader2 { get; set; }
        [Data] public List<Dat1> Dat1List { get; set; }

        public DPX DPX { get; set; }

        static Logger log = LogManager.GetCurrentClassLogger();

        public static Pax ReadObject(Stream stream, int baseOffset) {
            log.Info("PaxHeader at {0:x}", baseOffset);
            var it = BinaryMapping.ReadObject<Pax>(stream, baseOffset);
            it.DPX = DPX.ReadObject(stream, baseOffset + it.OffsetHeader2);
            return it;
        }
        /*
        public ParsePax(Stream pax) {
            // 0 PAX_
            // 4 ?
            // 8 CountDat1. 80 bytes per entry.
            // 12 Offset Header2
            // 16 Dat1 entries

            if (pax.ReadInt32(0) != Signature) {
                throw new InvalidDataException();
            }
            var nDat1 = pax.ReadInt32(8);
            Dat1List = pax.ReadList<Dat1>(16, nDat1);

            var offsetHeader2 = pax.ReadInt32(12);
            var header2 = BinaryMappingLite.ReadObject<Header2>(pax, offsetHeader2);
            Dat2List = pax.ReadList<Dat2>(offsetHeader2 + 16, header2.nDat2);

            Header3List = Dat2List
                .Select(it => new Header3(pax, offsetHeader2 + it.OffsetToHeader3))
                .ToList();

        }

        public List<Dat1> Dat1List { get; }
        public List<Dat2> Dat2List { get; }
        public List<Header3> Header3List { get; }
        */
    }
}
