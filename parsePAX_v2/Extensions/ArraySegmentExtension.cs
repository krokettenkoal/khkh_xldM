using parsePAX_v2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parsePAX_v2.Extensions {
    static class ArraySegmentExtension {
        public static float ReadSingle(this ArraySegment<byte> bytes, StreamingOffset offset) {
            if (bytes.Count < bytes.Offset + offset.Offset + 4) {
                throw new EndOfStreamException();
            }
            return BitConverter.ToSingle(bytes.Array, bytes.Offset + offset.Advance(4));
        }

        public static float ReadSingle(this ArraySegment<byte> bytes, int offset) {
            if (offset < 0) {
                throw new ArgumentOutOfRangeException();
            }
            return ReadSingle(bytes, new StreamingOffset(offset));
        }

        public static int ReadInt32(this ArraySegment<byte> bytes, StreamingOffset offset) {
            if (bytes.Count < bytes.Offset + offset.Offset + 4) {
                throw new EndOfStreamException();
            }
            return BitConverter.ToInt32(bytes.Array, bytes.Offset + offset.Advance(4));
        }

        public static int ReadInt32(this ArraySegment<byte> bytes, int offset) {
            if (offset < 0) {
                throw new ArgumentOutOfRangeException();
            }
            return ReadInt32(bytes, new StreamingOffset(offset));
        }

        public static short ReadInt16(this ArraySegment<byte> bytes, StreamingOffset offset) {
            if (bytes.Count < bytes.Offset + offset.Offset + 2) {
                throw new EndOfStreamException();
            }
            return BitConverter.ToInt16(bytes.Array, bytes.Offset + offset.Advance(2));
        }

        public static short ReadInt16(this ArraySegment<byte> bytes, int offset) {
            if (offset < 0) {
                throw new ArgumentOutOfRangeException();
            }
            return ReadInt16(bytes, new StreamingOffset(offset));
        }

        public static short ReadByte(this ArraySegment<byte> bytes, StreamingOffset offset) {
            if (bytes.Count < bytes.Offset + offset.Offset + 1) {
                throw new EndOfStreamException();
            }
            return bytes.Array[bytes.Offset + offset.Advance(1)];
        }

        public static short ReadByte(this ArraySegment<byte> bytes, int offset) {
            if (offset < 0) {
                throw new ArgumentOutOfRangeException();
            }
            return ReadByte(bytes, new StreamingOffset(offset));
        }

        public static List<T> ReadList<T>(this ArraySegment<byte> bytes, StreamingOffset offset, int count)
        where T : class {
            return Enumerable.Range(0, count)
                .Select(x => BinaryMappingLite.ReadObject<T>(bytes, offset))
                .ToList();
        }

        public static List<T> ReadList<T>(this ArraySegment<byte> bytes, int offset, int count)
            where T : class
            => ReadList<T>(bytes, new StreamingOffset(offset), count);

        public static List<int> ReadInt32List(this ArraySegment<byte> bytes, StreamingOffset offset, int count) {
            return Enumerable.Range(0, count)
                .Select(x => bytes.ReadInt32(offset))
                .ToList();
        }

        public static List<int> ReadInt32List(this ArraySegment<byte> bytes, int offset, int count)
            => ReadInt32List(bytes, new StreamingOffset(offset), count);
    }
}
