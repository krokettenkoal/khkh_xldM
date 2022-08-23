using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiFileFmts.Models.COCT {
    public class CoctPack {
        public CoctPack(BinaryReader reader) {
            Header = Xe.BinaryMapper.BinaryMapping.ReadObject<CoctHeader>(reader);

            var stream = reader.BaseStream;

            {
                var section = Header.Co1;
                stream.Position = section.Offset;
                Co1 = Enumerable.Range(0, section.Length / 32)
                    .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<Co1>(reader))
                    .ToList();
            }
            {
                var section = Header.Co2;
                stream.Position = section.Offset;
                Co2 = Enumerable.Range(0, section.Length / 20)
                    .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<Co2>(reader))
                    .ToList();
            }
            {
                var section = Header.Co3;
                stream.Position = section.Offset;
                Co3 = Enumerable.Range(0, section.Length / 16)
                    .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<Co3>(reader))
                    .ToList();
            }
            {
                var section = Header.Co4;
                stream.Position = section.Offset;
                CoVert = Enumerable.Range(0, section.Length / 16)
                    .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<CoVert>(reader))
                    .ToList();
            }
            {
                var section = Header.Co5;
                stream.Position = section.Offset;
                CoPlane = Enumerable.Range(0, section.Length / 16)
                    .Select(_ => Xe.BinaryMapper.BinaryMapping.ReadObject<CoPlane>(reader))
                    .ToList();
            }
        }

        public CoctHeader Header { get; set; }
        public List<Co1> Co1 { get; set; }
        public List<Co2> Co2 { get; set; }
        public List<Co3> Co3 { get; set; }
        public List<CoVert> CoVert { get; set; }
        public List<CoPlane> CoPlane { get; set; }
    }
}
