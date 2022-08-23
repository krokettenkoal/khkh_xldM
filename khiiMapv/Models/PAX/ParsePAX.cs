using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using vwBinTex2;
using System.Diagnostics;
using khiiMapv.Extensions;

namespace khiiMapv.Models.PAX {
    public class ParsePAX {
        public ParseDPX dpx;

        public ParsePAX(ArraySegment<byte> bytes) {
            var si = bytes.ToMemoryStream();
            var br = new BinaryReader(si);
            si.Position = 0;

            if (si.ReadByte() != 0x50) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x41) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x58) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x5F) throw new NotSupportedException("!PAX");

            si.Position = 0xC;
            int v0c = br.ReadInt32(); // off-to-tbl82

            dpx = new ParseDPX(bytes.Shrink(v0c, 0));
        }
    }
}
