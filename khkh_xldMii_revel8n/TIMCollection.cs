using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii.Models {
    public class TIMCollection {
        public static Texex2[] Load(Stream fs) {
            int pos = Convert.ToInt32(fs.Position);
            BinaryReader br = new BinaryReader(fs);
            List<int> offsets = new List<int>();
            if (br.ReadUInt32() == 0xffffffffU) {
                int cnt = br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    offsets.Add(pos + br.ReadInt32());
                }
            }
            else {
                offsets.Add(pos);
            }

            List<Texex2> al = new List<Texex2>();
            for (int x = 0; x < offsets.Count; x++) {
                int off0 = offsets[x];
                int off1 = (x + 1 < offsets.Count) ? offsets[x + 1] : Convert.ToInt32(fs.Length);
                byte[] bin = new byte[off1 - off0];
                fs.Position = off0;
                fs.Read(bin, 0, off1 - off0);

                al.Add(TIMReader.Load(new MemoryStream(bin, false)));
            }
            return al.ToArray();
        }
    }
}
