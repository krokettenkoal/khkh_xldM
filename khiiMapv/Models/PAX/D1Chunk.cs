using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.PAX {
    /// <summary>
    /// DPD 1st data, chunk header
    /// </summary>
    public class D1Chunk {
        public int ChunkType { get; }
        public ushort ChunkStride { get; }
        public ushort ChunkCount { get; }
        public int ChunkOffset { get; }
        public int v0c { get; }

        public List<byte[]> BinList { get; set; }

        public override string ToString() => $"type({ChunkType}) stride({ChunkStride}) count({ChunkCount}) offset({ChunkOffset}) ?({v0c})";
        
        public D1Chunk(BinaryReader br) {
            ChunkType = br.ReadInt32();
            ChunkStride = br.ReadUInt16();
            ChunkCount = br.ReadUInt16();
            ChunkOffset = br.ReadInt32();
            v0c = br.ReadInt32();
        }

        public void ReadEntity(BinaryReader br, int topOff) {
            br.BaseStream.Position = topOff + ChunkOffset;
            BinList = Enumerable.Range(0, ChunkCount)
                .Select(it => br.ReadBytes(ChunkStride))
                .ToList();
        }
    }
}
