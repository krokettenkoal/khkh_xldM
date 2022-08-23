using SlimDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace parseDOCT {
    class Program {
        static void Main(string[] args) {
            foreach (var file in Directory.GetFiles(@"C:\Users\KU\Documents", "*.bin")) {
                new Program().Run(file);
            }
        }

        private void Run(string file) {
            Console.WriteLine($"# {file}");
            using (var fs = File.OpenRead(file)) {
                var parse = new DoctReader(fs);

                var prefix = Path.GetFileNameWithoutExtension(file);
                Render(parse.list1, prefix, "T1");
                Render(parse.list2, prefix, "T2");
            }
            Console.WriteLine();
        }

        private void Render(IEnumerable<DoctReader.IIndexFlagsMinMax> list, string prefix, string tableType) {
            Func<Vector3, Vector2> To2 = vec => new Vector2(vec.X, vec.Z);
            Func<Vector2, PointF> ToF = vec => new PointF(vec.X, vec.Y);

            var allVec2 = list.Select(one => To2(one.min))
                .Concat(list.Select(one => To2(one.max)))
                .ToArray();

            var totalWidth = allVec2.Max(vec => vec.X) - allVec2.Min(vec => vec.X);
            var totalHeight = allVec2.Max(vec => vec.Y) - allVec2.Min(vec => vec.Y);

            float maxWide = 300.0f;

            float scale = Math.Min(1, maxWide / Math.Max(totalWidth, totalHeight));

            var bmWidth = (int)(totalWidth * scale);
            var bmHeight = (int)(totalHeight * scale);

            var offX = 0 - allVec2.Min(vec => vec.X);
            var offY = 0 - allVec2.Min(vec => vec.Y);

            var matrix = Matrix3x2.Identity
                * Matrix3x2.Translation(offX, offY)
                * Matrix3x2.Scale(scale, scale);

            foreach (var filterItem in list) {
                using (var bitmap = new Bitmap(bmWidth, bmHeight))
                using (var canvas = Graphics.FromImage(bitmap))
                using (var font = new Font("Century Gothic", 15)) {
                    canvas.Clear(Color.Black);
                    var sfMC = new StringFormat {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap,
                    };

                    for (var pass = 1; pass <= 3; pass++) {
                        foreach (var item in list) {
                            var highlight = ReferenceEquals(filterItem, item);

                            var min = Matrix3x2.TransformPoint(matrix, ToF(To2(item.min)));
                            var max = Matrix3x2.TransformPoint(matrix, ToF(To2(item.max)));
                            var bounds = RectangleF.FromLTRB(
                                min.X, min.Y - 1,
                                max.X, max.Y - 1
                            );
                            if (pass == 1) {
                                if (highlight) {
                                    canvas.FillRectangle(
                                        Brushes.DarkSlateGray,
                                        Rectangle.Truncate(bounds)
                                    );
                                }
                            }
                            if (pass == 2) {
                                canvas.DrawRectangle(
                                    Pens.LimeGreen,
                                    Rectangle.Truncate(bounds)
                                );
                            }
                            if (pass == 3) {
                                var textRect = bounds;
                                textRect.Inflate(-2, -2);
                                if (highlight) {
                                    canvas.DrawString(
                                        item.index.ToString(),
                                        font,
                                        Brushes.WhiteSmoke,
                                        textRect,
                                        sfMC
                                    );

                                    if (item.flags.HasValue) {
                                        canvas.DrawString(
                                            item.flags.Value.ToString("X8"),
                                            font,
                                            Brushes.Yellow,
                                            PointF.Empty
                                        );
                                    }
                                }
                            }
                        }
                    }

                    bitmap.Save($"{prefix}-{tableType}xz-{filterItem.index:000}.png");
                }
            }
        }


        class DoctReader {
            public int v8;
            public List<Doct1> list1 = new List<Doct1>();
            public List<Doct2> list2 = new List<Doct2>();

            public interface IIndexFlagsMinMax {
                int index { get; }
                int? flags { get; }
                Vector3 min { get; }
                Vector3 max { get; }
            }
            public class Doct1 : IIndexFlagsMinMax {
                public int index { get; set; }
                public int? flags => null;

                public IList<int> goTo;
                public Vector3 min { get; set; }
                public Vector3 max { get; set; }
                public int link1;
                public int link2;
                public int unk1;

                public override string ToString() =>
                    string.Concat(
                        string.Join(",", goTo.Select(one => $"{one,3}")),
                        " ",
                        $"({min.X,8},{min.Y,8},{min.Z,8}) ({max.X,8},{max.Y,8},{max.Z,8}) {link1,3} {link2,3} {unk1:X8}"
                    );
            }
            public class Doct2 : IIndexFlagsMinMax {
                public int index { get; set; }

                public int? flags { get; set; }
                public Vector3 min { get; set; }
                public Vector3 max { get; set; }

                public override string ToString() =>
                    string.Concat(
                        $"{flags:X8}                        ",
                        $"({min.X,8},{min.Y,8},{min.Z,8}) ({max.X,8},{max.Y,8},{max.Z,8})"
                    );
            }

            public DoctReader(Stream si) {
                var br = new BinaryReader(si);
                si.Position = 8;
                v8 = br.ReadInt32();
                Console.WriteLine($"v8 = {v8}");

                {
                    si.Position = 0x14;
                    var posT1 = br.ReadInt32();
                    var lenT1 = br.ReadInt32();

                    si.Position = posT1;
                    for (int y = 0; y < lenT1 / 48; y++) {
                        var goTo = new List<int>();
                        for (int x = 0; x < 8; x++) {
                            goTo.Add(br.ReadInt16());
                        }
                        list1.Add(new Doct1 {
                            index = y,
                            goTo = goTo,
                            min = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()),
                            max = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()),
                            link1 = br.ReadUInt16(),
                            link2 = br.ReadUInt16(),
                            unk1 = br.ReadInt32(),
                        });
                    }
                }

                {
                    si.Position = 0x1c;
                    var posT2 = br.ReadInt32();
                    var lenT2 = br.ReadInt32();

                    si.Position = posT2;
                    for (int y = 0; y < lenT2 / 28; y++) {
                        list2.Add(new Doct2 {
                            index = y,
                            flags = br.ReadInt32(),
                            min = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()),
                            max = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle()),
                        });
                    }
                }

                Console.WriteLine("## Table1");
                foreach (var pair in list1.Select((it, index) => (it, index))) {
                    Console.WriteLine($"{pair.index,3}:{pair.it}");
                }

                Console.WriteLine("## Table2");
                foreach (var pair in list2.Select((it, index) => (it, index))) {
                    Console.WriteLine($"{pair.index,3}:{pair.it}");
                }
            }
        }
    }
}
