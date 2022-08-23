using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    public class DoctReader {
        public int v8;

        /// <summary>
        /// Table1
        /// </summary>
        public List<Doct1> list1 = new List<Doct1>();

        /// <summary>
        /// Table2
        /// </summary>
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
            public BoundingBox bbox => new BoundingBox(min, max);
            public int link1;
            public int link2;
            public int unk1;

            public override string ToString() =>
                string.Concat(
                    string.Join(",", goTo.Select(one => $"{one,4}")),
                    " ",
                    $"({min.X,8},{min.Y,8},{min.Z,8}) ({max.X,8},{max.Y,8},{max.Z,8}) {link1,4} {link2,4} {unk1:X8}"
                );
        }
        public class Doct2 : IIndexFlagsMinMax {
            public int index { get; set; }

            public int? flags { get; set; }
            public Vector3 min { get; set; }
            public Vector3 max { get; set; }

            public BoundingBox bbox => new BoundingBox(min, max);

            public override string ToString() =>
                string.Concat(
                    $"{flags:X8}                                ",
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
                    var min = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var max = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var link1 = br.ReadUInt16();
                    var link2 = br.ReadUInt16();
                    list1.Add(new Doct1 {
                        index = y,
                        goTo = goTo,
                        min = min,
                        max = max,
                        link1 = link1,
                        link2 = link2,
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
        }
    }

}
