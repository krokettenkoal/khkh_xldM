using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class ParseIMGZ {
        public static PicIMGD[] TakeIMGZ(byte[] imgz, string id) {
            MemoryStream stream = new MemoryStream(imgz, false);
            BinaryReader reader = new BinaryReader(stream);

            List<PicIMGD> list = new List<PicIMGD>();
            stream.Position = 0x0C;
            int v0c = reader.ReadInt32();
            for (int x = 0; x < v0c; x++) {
                int offset = reader.ReadInt32();
                int length = reader.ReadInt32();
                {
                    MemoryStream six = new MemoryStream(imgz, offset, length, false);

                    PicIMGD imgd = ParseIMGD.TakeIMGD(six, $"{id}#{x}");
                    list.Add(imgd);
                }
            }
            return list.ToArray();
        }
    }
}
