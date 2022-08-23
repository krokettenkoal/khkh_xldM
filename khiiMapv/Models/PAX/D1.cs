using khiiMapv.Utils;
using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 1st data. this is header
    /// </summary>
    public class D1 {
        public D1Vert[] v00;
        public Vector4 v80;
        public Vector4 v90;
        public Vector4 va0;
        public byte[] vb0;
        public List<int> ofsD101List = new List<int>();

        public List<D101> d101List = new List<D101>();

        /// <summary>
        /// for debug
        /// </summary>
        public int topOffset;

        public D1(BinaryReader br) {
            topOffset = Convert.ToInt32(br.BaseStream.Position);

            v00 = Enumerable.Range(0, 8)
                .Select(it => new D1Vert(br))
                .ToArray();
            v80 = DxReadUtil.ReadVector4(br);
            v90 = DxReadUtil.ReadVector4(br);
            va0 = DxReadUtil.ReadVector4(br);
            vb0 = br.ReadBytes(0x60);
            // P12 @0x110
            // P13*
            int topD101 = Convert.ToInt32(br.BaseStream.Position);
            int nextD101 = 0x10;
            while (true) {
                br.BaseStream.Position = topD101 + nextD101;
                var ofs = br.ReadInt32();
                if (ofs == 0) {
                    break;
                }
                // P13
                ofsD101List.Add(topD101 + nextD101);
                nextD101 = ofs;
            }
            foreach (var ofsD101 in ofsD101List) {
                br.BaseStream.Position = ofsD101;
                d101List.Add(new D101(br));
            }
        }

        public string Describe() {
            var writer = new StringWriter();
            writer.WriteLine($"v00:");
            writer.WriteLine($" {v00[0]}");
            writer.WriteLine($" {v00[1]}");
            writer.WriteLine($" {v00[2]}");
            writer.WriteLine($" {v00[3]}");
            writer.WriteLine($" {v00[4]}");
            writer.WriteLine($" {v00[5]}");
            writer.WriteLine($" {v00[6]}");
            writer.WriteLine($" {v00[7]}");
            writer.WriteLine($"v80: {v80}");
            writer.WriteLine($"v90: {v90}");
            writer.WriteLine($"va0: {va0}");

            writer.WriteLine($"ChunkBoxes n={d101List.Count}:");
            for (int y = 0; y < d101List.Count; y++) {
                var d101 = d101List[y];
                writer.WriteLine($"Box#{y}: {d101}");
                for (int x = 0; x < d101.d1ChunkList.Length; x++) {
                    var chunk = d101.d1ChunkList[x];
                    writer.WriteLine($" Chunk#{x}: {chunk}");
                    for (int z = 0; z < chunk.BinList.Count; z++) {
                        var bin = chunk.BinList[z];
                        writer.WriteLine($"  Bin#{z}:");
                        writer.Write(HexDump.Print(bin, linePrefix: "    "));
                    }
                }
            }
            return writer.ToString();
        }
    }
}
