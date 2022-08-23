using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    class BER { // BIG Endian Reader
        private BinaryReader br;

        public BER(BinaryReader br) {
            this.br = br;
        }

        public int ReadInt32() {
            byte[] bin = br.ReadBytes(4);
            return (bin[0] << 24) | (bin[1] << 16) | (bin[2] << 8) | (bin[3]);
        }

        public short ReadUInt16() {
            int r = br.ReadByte() << 8;
            return (short)(r | br.ReadByte());
        }
    }
}
