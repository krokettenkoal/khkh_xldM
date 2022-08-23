using khiiMapv.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khiiMapv.Models.PAX {
    public class ParseDPX {
        public List<ParseDPD> dpdList = new List<ParseDPD>();
        public int dpxOffset;

        public ParseDPX(ArraySegment<byte> bytes) {
            var si = bytes.ToMemoryStream();
            var br = new BinaryReader(si);

            dpxOffset = bytes.Offset;

            si.Position = 0;
            int test82 = br.ReadInt32();
            if (test82 != 0x82) throw new NotSupportedException("!82");

            si.Position = 0xC;
            int cx = br.ReadInt32();

            for (int x = 0; x < cx; x++) {
                si.Position = 16 + 32 * x;
                int off96 = (br.ReadInt32());

                dpdList.Add(new ParseDPD(bytes.Shrink(off96, 0)));
            }
        }

    }
}
