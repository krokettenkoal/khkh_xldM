using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parsePAX_v2.Utils {
    public class StreamingOffset {
        public StreamingOffset(int initialOffset) {
            Offset = initialOffset;
        }

        public int Offset { get; private set; }

        public int Advance(int size) {
            var before = Offset;
            Offset += size;
            return before;
        }

        public override string ToString() => Offset.ToString();
    }
}
