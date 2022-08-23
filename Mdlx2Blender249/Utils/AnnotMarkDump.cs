using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Mdlx2Blender249.Utils {
    public class AnnotMarkDump {
        private readonly byte[] _raw;
        private readonly List<Marked> _marks = new List<Marked>();
        private static readonly Brush _fontBrush = Brushes.Black;
        private static readonly Font _font = new Font("MS Gothic", 8);
        private static MemoryCache _colorBrushes = new MemoryCache("ColorBrushes");
        private static StringFormat _center = new StringFormat { Alignment = StringAlignment.Center, };

        public AnnotMarkDump(byte[] raw) {
            _raw = raw;
        }

        private class Marked {
            internal int offset;
            internal int length;
            internal string help;
            internal Color color;
        }

        internal void Mark(int offset, int length, string help, Color color) {
            _marks.Add(new Marked { offset = offset, length = length, help = help, color = color, });
        }

        internal Bitmap GetBitmap() {
            int last = _marks.Select(it => it.offset + it.length).OrderByDescending(it => it).FirstOrDefault();

            int leftHeader = 7 * 8;
            int topHeader = 12;

            int blockWidth = 16;
            int blockHeight = 9;
            int cy = (last + 15) / 16;

            var bitmap = new Bitmap(leftHeader + blockWidth * 16, topHeader + blockHeight * cy);
            using (var canvas = Graphics.FromImage(bitmap)) {
                canvas.Clear(Color.White);
                for (int x = 0; x < 16; x++) {
                    var rectX = new Rectangle(
                        leftHeader + blockWidth * x,
                        0,
                        blockWidth,
                        blockHeight
                    );

                    canvas.DrawString(
                        $"{x:X2}", _font, _fontBrush, rectX, _center
                    );
                }
                for (int pos = 0; pos < last; pos++) {
                    var x = pos & 15;
                    var y = pos >> 4;

                    var rect = new Rectangle(
                        leftHeader + blockWidth * x,
                        topHeader + blockHeight * y,
                        blockWidth,
                        blockHeight
                    );

                    if (x == 0) {
                        var rectLeftHeader = Rectangle.FromLTRB(
                            0, rect.Y,
                            rect.X, rect.Bottom
                        );
                        canvas.DrawString(
                            $"{pos:X8}", _font, _fontBrush, rectLeftHeader
                        );
                    }

                    var hit = _marks.Where(it => it.offset <= pos && pos < it.offset + it.length).FirstOrDefault();
                    if (hit != null) {
                        canvas.FillRectangle(
                            GetOr(_colorBrushes, hit.color.ToString(), () => new SolidBrush(hit.color)),
                            rect
                        );
                    }

                    canvas.DrawString(_raw[pos].ToString("X2"), _font, _fontBrush, rect, _center);
                }
            }

            return bitmap;
        }

        private static R GetOr<R>(MemoryCache cache, string key, Func<R> gen) {
            var it = cache.Get(key);
            if (it != null) {
                return (R)it;
            }
            else {
                it = gen();
                cache.Set(key, it, new CacheItemPolicy());
                return (R)it;
            }
        }
    }
}
