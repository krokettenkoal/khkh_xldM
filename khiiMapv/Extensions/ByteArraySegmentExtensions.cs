using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Extensions {
    static class ByteArraySegmentExtensions {
        public static MemoryStream ToMemoryStream(this ArraySegment<byte> that) {
            return new MemoryStream(that.Array, that.Offset, that.Count);
        }

        public static ArraySegment<byte> Shrink(this ArraySegment<byte> that, int top, int bottom) {
            return new ArraySegment<byte>(
                that.Array,
                that.Offset + top,
                that.Count - top - bottom
            );
        }
    }
}
