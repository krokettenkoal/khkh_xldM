using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 16 bytes
    /// </summary>
    public class DPX {
        /// <summary>
        /// Always 0x82
        /// </summary>
        [Data] public int Signature { get; set; }
        [Data] public int v04 { get; set; }
        [Data] public int v08 { get; set; }
        /// <summary>
        /// Dat2 continues after this 16 bytes PaxHeader2.
        /// </summary>
        [Data] public int CountDat2 { get; set; }
        [Data] public List<DPDRef> DPDRefList { get; set; }

        static Logger log = LogManager.GetCurrentClassLogger();

        public static DPX ReadObject(Stream stream, int baseOffset) {
            log.Info("DPXHeader at {0:x}", baseOffset);
            stream.Position = baseOffset;
            var it = BinaryMapping.ReadObject<DPX>(stream);
            foreach (var dpdRef in it.DPDRefList) {
                dpdRef.ReadSubObjects(stream, baseOffset);
            }
            return it;
        }
    }
}
