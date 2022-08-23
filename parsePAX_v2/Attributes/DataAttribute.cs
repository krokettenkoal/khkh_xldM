using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parsePAX_v2.Attributes {
    /// <summary>
    /// Lite and alt version of: Xe.BinaryMapper.DataAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]

    class DataAttribute : Attribute {
        public DataAttribute() : this(0) {

        }
        public DataAttribute(int offset, int count = 1, int stride = 0, int bitIndex = -1) {
            Offset = offset;
            Count = count;
            Stride = stride;
            BitIndex = bitIndex;
        }

        public int? Offset { get; set; }
        public int Count { get; set; }
        public int Stride { get; set; }
        public int BitIndex { get; set; }
    }
}
