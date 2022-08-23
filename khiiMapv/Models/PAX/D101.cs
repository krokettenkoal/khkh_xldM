using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 1st data, 1st sub-data
    /// </summary>
    public class D101 {
        public int v00;
        public int v04;
        public int v08;
        public int v0c;
        public int v10;
        public int v14;
        public int v18;
        public int v1c;
        public int v20;
        public ushort v24;
        public ushort numDat1Chunk;

        public D1Chunk[] d1ChunkList;

        public override string ToString() => $"{v00} {v04} {v08} {v0c} {v10} {v14} {v18} {v1c} {v20} {v24} numChunks({numDat1Chunk})";

        public D101(BinaryReader br) {
            var topOff = Convert.ToInt32(br.BaseStream.Position);

            v00 = br.ReadInt32();
            v04 = br.ReadInt32();
            v08 = br.ReadInt32();
            v0c = br.ReadInt32();

            v10 = br.ReadInt32();
            v14 = br.ReadInt32();
            v18 = br.ReadInt32();
            v1c = br.ReadInt32();

            v20 = br.ReadInt32();
            v24 = br.ReadUInt16();
            numDat1Chunk = br.ReadUInt16();

            d1ChunkList = Enumerable.Range(0, numDat1Chunk)
                .Select(it => new D1Chunk(br))
                .ToArray();

            foreach (var it in d1ChunkList) {
                it.ReadEntity(br, topOff);
            }
        }
    }
}
