using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class DelayInt32Writer {
        private readonly BinaryWriter writer;
        private readonly int count;
        private readonly long writePos;

        public DelayInt32Writer(BinaryWriter writer, int count = 1) {
            this.writer = writer;
            this.count = count;
            this.writePos = writer.BaseStream.Position;
            writer.BaseStream.Seek(4 * count, SeekOrigin.Current);
        }

        public void Set(int index, int value) {
            var savedPos = writer.BaseStream.Position;
            {
                writer.BaseStream.Position = writePos + 4 * index;
                writer.Write(value);
            }
            writer.BaseStream.Position = savedPos;
        }
    }
}
