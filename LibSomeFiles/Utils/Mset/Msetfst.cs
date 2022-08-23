using khkh_xldMii.Models.Mset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii.Utils.Mset {
    public class Msetfst {
        public List<OneMotion> motionList = new List<OneMotion>();

        public Msetfst(Stream fs, string motionId) {
            ReadBar.Barent[] ents1 = ReadBar.Explode(fs);
            int indexInMset = 0;
            foreach (ReadBar.Barent ent1 in ents1) {
                switch (ent1.k) {
                    case 17:
                        if (ent1.len != 0) {
                            OneMotion oneMotion = new OneMotion();
                            ReadBar.Barent[] ents2 = ReadBar.Explode2(new MemoryStream(ent1.bin, false));
                            int k2 = 0;
                            foreach (ReadBar.Barent ent2 in ents2) {
                                switch (ent2.k) {
                                    case 9:
                                        oneMotion.anbOff = (uint)ent1.off + (uint)ent2.off;
                                        oneMotion.anbLen = (uint)ent1.len;
                                        oneMotion.label = ent1.id + "#" + ent2.id;
                                        oneMotion.anbBin = ent2.bin;
                                        oneMotion.indexInMset = indexInMset;
                                        oneMotion.isRaw = ent2.id.Equals("raw");
                                        break;
                                    case 16:
                                        oneMotion.faceModSet = new FaceModSet(new MemoryStream(ent2.bin, false));
                                        break;
                                }
                                k2++;
                            }
                            motionList.Add(oneMotion);
                        }
                        break;
                }
                indexInMset++;
            }
        }
    }
}
