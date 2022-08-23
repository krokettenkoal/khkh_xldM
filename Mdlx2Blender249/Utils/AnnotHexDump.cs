using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdlx2Blender249.Utils {
    /// <summary>
    /// Dump annotated hex dump
    /// </summary>
    public class AnnotHexDump {
        private readonly byte[] _raw;
        private readonly List<Comment> _comments = new List<Comment>();

        public AnnotHexDump(byte[] raw) {
            _raw = raw;
        }

        private class Comment {
            internal int offset;
            internal int length;
            internal string comment;
        }

        public AnnotHexDump AddComment(int offset, int length, string comment) {
            _comments.Add(new Comment {
                offset = offset,
                length = length,
                comment = comment,
            });
            return this;
        }

        public void WriteTo(TextWriter writer) {
            writer.WriteLine("ADDRESS  00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 0123456789ABCDEF");
            writer.WriteLine("-------- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- ----------------");
            for (int cursor = 0; cursor < _raw.Length; cursor += 16) {
                var pos = cursor;
                writer.Write($"{pos:X8} ");
                for (int t = 0; t < 16 && cursor + t < _raw.Length; t++) {
                    writer.Write($"{_raw[cursor + t]:X2} ");
                }
                for (int t = 0; t < 16 && cursor + t < _raw.Length; t++) {
                    writer.Write(PrintChar(_raw[cursor + t]));
                }

                {
                    var hits = _comments.Where(it => cursor <= it.offset && it.offset < cursor + 16);
                    var sideComments = new List<string>(
                        hits.Select(hit => hit.comment)
                    );

                    if (sideComments.Any()) {
                        for (int y = 0; y < sideComments.Count; y++) {
                            if (y != 0) {
                                writer.Write("                                                                         ");
                            }
                            writer.WriteLine(" ; " + sideComments[y]);
                        }
                    }
                    else {
                        writer.WriteLine();
                    }
                }

            }
        }

        private static char PrintChar(byte v) {
            if (v < 32 || 126 < v) {
                return '.';
            }
            return (char)v;
        }
    }
}
