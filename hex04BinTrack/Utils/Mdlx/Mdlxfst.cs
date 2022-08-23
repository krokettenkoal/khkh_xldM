using khkh_xldMii.Models.Mdlx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii.Utils.Mdlx {
    public class Mdlxfst {
        public List<K2Model> alt31 = new List<K2Model>();

        public List<K2Model> ModelList => alt31;

        public Mdlxfst(Stream fs) {
            BinaryReader br = new BinaryReader(fs);
            Queue<int> modelOffsets = new Queue<int>();
            modelOffsets.Enqueue(0x90);
            int index = 0;
            while (modelOffsets.Count != 0) {
                int topOffset = modelOffsets.Dequeue();
                fs.Position = topOffset + 0x00;
                int type = br.ReadUInt16();
                fs.Position = topOffset + 0x10;
                int cnt2 = br.ReadUInt16();
                fs.Position = topOffset + 0x1C;
                int cnt1 = br.ReadUInt16();

                K2Model model;
                alt31.Add(model = new K2Model(topOffset, 0x20 * (1 + cnt1), index, type)); index++;

                for (int c1 = 0; c1 < cnt1; c1++) {
                    fs.Position = topOffset + 0x20u + 0x20u * c1 + 0x10u;
                    int pos1 = br.ReadInt32() + topOffset;
                    int pos2 = br.ReadInt32() + topOffset;
                    fs.Position = topOffset + 0x20 + 0x20 * c1 + 0x04;
                    int textureIndex = br.ReadInt32();
                    int v8 = br.ReadInt32();
                    int vc = br.ReadInt32();
                    fs.Position = pos2;
                    int numMatrix = br.ReadInt32();
                    T11 t11;
                    model.al11.Add(t11 = new T11(pos2, RUtil.RoundUpto16(4 + 4 * numMatrix), c1));
                    List<int> matrixIndexSource = new List<int>(numMatrix);
                    for (int x = 0; x < numMatrix; x++) {
                        matrixIndexSource.Add(br.ReadInt32());
                    }

                    List<int> aloffDMAtag = new List<int>();
                    List<int[]> matrixIndexArrayGroup = new List<int[]>();
                    List<int> matrixIndexArray = new List<int>();
                    aloffDMAtag.Add(pos1);
                    fs.Position = pos1 + 16;
                    for (int x = 0; x < numMatrix; x++) {
                        if (matrixIndexSource[x] == -1) {
                            aloffDMAtag.Add((int)fs.Position + 0x10);
                            fs.Position += 0x20;
                        }
                        else {
                            fs.Position += 0x10;
                        }
                    }
                    for (int x = 0; x < numMatrix; x++) {
                        if (x + 1 == numMatrix) {
                            matrixIndexArray.Add(matrixIndexSource[x]);
                            matrixIndexArrayGroup.Add(matrixIndexArray.ToArray());
                            matrixIndexArray.Clear();
                        }
                        else if (matrixIndexSource[x] == -1) {
                            matrixIndexArrayGroup.Add(matrixIndexArray.ToArray());
                            matrixIndexArray.Clear();
                        }
                        else {
                            matrixIndexArray.Add(matrixIndexSource[x]);
                        }
                    }

                    int pos1a = (int)fs.Position;
                    T12 t12;
                    model.al12.Add(t12 = new T12(pos1, pos1a - pos1, c1));

                    int dmaIndex = 0;
                    foreach (int offDMAtag in aloffDMAtag) {
                        fs.Position = offDMAtag;
                        // Source Chain Tag
                        int qwcsrc = (br.ReadInt32() & 0xFFFF);
                        int offsrc = (br.ReadInt32() & 0x7FFFFFFF) + topOffset;

                        fs.Position = offsrc;
                        byte[] bin = br.ReadBytes(16 * qwcsrc);
                        K2Vif vif;
                        model.vifList.Add(vif = new K2Vif(offsrc, 16 * qwcsrc, textureIndex, v8, vc, matrixIndexArrayGroup[dmaIndex], bin));
                        dmaIndex++;
                    }
                }

                fs.Position = topOffset + 0x14;
                int off2 = br.ReadInt32();
                if (off2 != 0) {
                    off2 += topOffset;
                    int len2 = 0x40 * cnt2;
                    model.boneList = new K2BoneList(off2, len2);

                    fs.Position = off2;
                    for (int x = 0; x < len2 / 0x40; x++) {
                        model.boneList.bones.Add(UtilAxBoneReader.read(br));
                    }
                }

                fs.Position = topOffset + 0x18;
                int off4 = br.ReadInt32();
                if (off4 != 0) {
                    off4 += topOffset;
                    int len4 = off2 - off4;
                    model.t32 = new T32(off4, len4);
                }

                fs.Position = topOffset + 0xC;
                int off3 = br.ReadInt32();
                if (off3 != 0) {
                    off3 += topOffset;
                    modelOffsets.Enqueue(off3);
                }
            }
        }
    }
}
