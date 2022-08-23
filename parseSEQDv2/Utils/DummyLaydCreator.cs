using parseSEQDv2.Models.LaydTypes;
using parseSEQDv2.Models.SeqdTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    class DummyLaydCreator {
        internal Layd layd = new Layd();

        public DummyLaydCreator() {
            for (int t = 0; t < 18; t++) {
                layd.GroupList.Add(
                    new SequenceGroup {
                        PropertyIndex = 0,
                        PropertyCount = 1,
                    }
                );
            }
            for (int t = 0; t < 30; t++) {
                layd.PropList.Add(
                    new SequenceProperty {
                        TextureIndex = 8,
                        SequenceIndex = 0,
                        AnimationGroup = 0,//titlesAnimGrps[t],
                        ShowAtFrame = 0,
                        PositionX = 0,
                        PositionY = 0,
                    }
                );
            }
            {
                var seq = new Seqd();
                {
                    {
                        {
                            var frame = new Frame();
                            frame.Left = 0;
                            frame.Top = 0;
                            frame.Right = 256;
                            frame.Bottom = 128;
                            {
                                frame.TextureScrollX = 1e-4f;
                                frame.TextureScrollY = 0;
                            }
                            frame.Color0 = 0x80808080U;
                            frame.Color1 = 0x80808080U;
                            frame.Color2 = 0x80808080U;
                            frame.Color3 = 0x80808080U;
                            seq.FrameList.Add(frame);
                        }
                        var index = 0;
                        {
                            var ex = new FrameEx();
                            ex.Left = 0;
                            ex.Top = 0;
                            ex.Right = ex.Left + 256;
                            ex.Bottom = ex.Top + 128;
                            ex.FrameIndex = index;
                            seq.FrameExList.Add(ex);
                        }
                        {
                            var group = new FrameGroup();
                            group.Start = (ushort)index;
                            group.Count = 1;
                            seq.FrameGroupList.Add(group);
                        }
                    }
                }

                List<Animation> setFrameEndToMax = new List<Animation>();
                // background 1 tile
                {
                    var anim = new Animation();
                    anim.Flags = 0;// 0x00043b71;
                    anim.FrameGroupIndex = 0;
                    anim.FrameStart = 0;
                    anim.FrameEnd = 200;
                    anim.Xa0 = 0;// 32 * x;
                    anim.Ya0 = 0;// 64 * y;
                    anim.Xa1 = 0;// 32 * x;
                    anim.Ya1 = 0;// anim.Ya0 + 32;
                    anim.Xb0 = 0;
                    anim.Xb1 = 0;
                    anim.ScaleStart = 1;
                    anim.ScaleEnd = 1;
                    anim.ScaleXStart = 1;
                    anim.ScaleXEnd = 1;
                    anim.ScaleYStart = 1;
                    anim.ScaleYEnd = 1;
                    anim.Unk60 = 0;
                    anim.Unk64 = 0;
                    anim.Unk68 = 0;
                    anim.Unk6c = 0;
                    anim.BounceXStart = 0;
                    anim.BounceYStart = 0;
                    anim.BounceXEnd = 0;
                    anim.BounceYEnd = 0;
                    anim.ColorBlend = 0;
                    anim.ColorStart = 0x80808080U;
                    anim.ColorEnd = 0x80808080U;

                    seq.AnimList.Add(anim);
                }

                {
                    var ag = new AnimationGroup();
                    ag.AnimationIndex = 0;
                    ag.Count = (ushort)seq.AnimList.Count;
                    ag.Unk04 = 1;
                    ag.Unk06 = 0;
                    ag.Tick1 = 0;// 32 * index;
                    ag.Tick2 = 0;
                    ag.Unk10 = 0;
                    ag.Unk14 = 0;
                    ag.Unk18 = 0;
                    ag.Unk1c = 0;
                    ag.Unk20 = 0;

                    seq.AnimGroupList.Add(ag);
                }

                for (int dup = 0; dup < 1; dup++) {
                    layd.SeqdList.Add(seq);
                }
            }
        }
    }
}
