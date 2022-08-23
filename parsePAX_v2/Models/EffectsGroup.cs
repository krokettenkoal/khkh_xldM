using NLog;
using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;

namespace parsePAX_v2.Models {
    /// <summary>
    /// 272 bytes +variable
    /// P11 P12
    /// </summary>
    public class EffectsGroup {
        [Data(Count = 8)]
        public List<Dat31Vertex> Vertices { get; set; }

        [Data]
        public Vector4 v80 { get; set; }
        [Data]
        public Vector4 v90 { get; set; }
        [Data]
        public Vector4 va0 { get; set; }
        [Data]
        public Vector4 vb0 { get; set; }
        [Data(Count = 0x50)]
        public byte[] vc0 { get; set; }

        [Data(Count = 16)]
        public byte[] v110 { get; set; }

        public List<Effect> EffectList { get; set; }

        static Logger log = LogManager.GetCurrentClassLogger();

        public static EffectsGroup ReadObject(Stream stream, int baseOffset) {
            log.Info("EffectsGroupHeader at {0:x}", baseOffset);
            stream.Position = baseOffset;

            var it = BinaryMapping.ReadObject<EffectsGroup>(stream);
            {
                var innerBaseOffset = baseOffset + 0x110;
                var offset = 0x10;
                it.EffectList = new List<Effect>();
                var reader = new BinaryReader(stream);
                while (true) {
                    var effectHeaderOffset = innerBaseOffset + offset;
                    stream.Position = effectHeaderOffset;
                    var nextOffset = reader.ReadInt32();
                    if (nextOffset == 0) {
                        break;
                    }
                    offset = nextOffset;

                    stream.Position = effectHeaderOffset;
                    log.Info("EffectHeader at {0:x}", stream.Position);

                    var one = BinaryMapping.ReadObject<Effect>(stream);
                    it.EffectList.Add(one);

                    foreach (var effectCommand in one.EffectCommandList)
                    {
                        var rawList = new List<byte[]>();
                        stream.Position = effectHeaderOffset + effectCommand.OffsetParameters;
                        for (int x = 0; x < effectCommand.ParamsCount; x++)
                        {
                            var raw = new byte[effectCommand.ParamLength];
                            if (raw.Length != stream.Read(raw, 0, raw.Length))
                            {
                                // crossing between bar entries?
                                //throw new EndOfStreamException();
                            }
                            rawList.Add(raw);
                        }
                        effectCommand.ChunksList = rawList.ToArray();
                    }
                }
            }

            return it;
        }
    }
}
