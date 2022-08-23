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
    /// 32 bytes
    /// </summary>
    public class DPDRef {
        /// <summary>
        /// Offset 0 is at Header2.
        /// </summary>
        [Data] public int OffsetToDPD { get; set; }
        [Data] public int v04 { get; set; }
        [Data] public int v08 { get; set; }
        [Data] public int v0c { get; set; }
        [Data] public int v10 { get; set; }
        [Data] public int v14 { get; set; }
        [Data] public int v18 { get; set; }
        [Data] public int v1c { get; set; }

        public DPD DPD { get; set; }

        static Logger log = LogManager.GetCurrentClassLogger();

        public DPDRef ReadSubObjects(Stream stream, int baseOffset) {
            log.Info("DPDRef at {0:x}", baseOffset);
            DPD = DPD.ReadObject(stream, baseOffset + OffsetToDPD);
            return this;
        }
    }
}
