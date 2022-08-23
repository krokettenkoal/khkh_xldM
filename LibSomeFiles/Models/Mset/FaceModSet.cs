using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii.Models.Mset {
    public class FaceModSet {
        public List<FaceMod> list = new List<FaceMod>();

        public FaceModSet(MemoryStream si) {
            if (si.Length < 2) return;
            BinaryReader br = new BinaryReader(si);
            byte cnt1 = br.ReadByte(); // @0x00
            byte cnt2 = br.ReadByte(); // @0x01
            br.ReadUInt16(); // @0x02
            for (int t1 = 0; t1 < cnt1; t1++) {
                FaceMod f1 = new FaceMod();
                try {
                    f1.v0 = br.ReadInt16();
                    f1.v2 = br.ReadInt16();
                    if (f1.v0 == 0 && f1.v2 == -1 && t1 != 0) {
                        f1.v4 = 0;
                        f1.v6 = 0;
                    }
                    else {
                        f1.v4 = br.ReadInt16();
                        if (f1.v2 != -1) {
                            f1.v6 = br.ReadInt16();
                        }
                        else {
                            f1.v6 = 0;
                        }
                    }
                }
                catch (EndOfStreamException) { }
                list.Add(f1);
            }
        }
    }
}
