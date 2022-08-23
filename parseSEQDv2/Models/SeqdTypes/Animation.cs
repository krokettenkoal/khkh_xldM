using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xe.BinaryMapper;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Models.SeqdTypes {
    public class Animation { // 0x90 bytes
        /// <summary>
        /// Flags
        /// </summary>
        /// <remarks>
        /// Copied from `OpenKh\OpenKh.Kh2\Sequence.cs`:
        /// 
        /// ```
        /// 0000 0001 = (0 = SINC INTERPOLATION, 1 = LINEAR INTERPOLATION)
        /// 0000 0008 = (0 = BOUNCING START FROM CENTER, 1 = BOUNCING START FROM X / MOVE FROM Y)
        /// 0000 0010 = (0 = ENABLE BOUNCING, 1 = IGNORE BOUNCING)
        /// 0000 0020 = (0 = ENABLE ROTATION, 1 = IGNORE ROTATION)
        /// 0000 0040 = (0 = ENABLE SCALING, 1 = IGNORE SCALING)
        /// 0000 0080 = (0 = ENABLE COLOR FADING, 1 = IGNORE COLOR FADING)
        /// 0000 0400 = (0 = ENABLE COLOR MASKING, 1 = IGNORE COLOR MASKING)
        /// 0000 4000 = (0 = ENABLE XYB, 1 = IGNORE XYB)
        /// ```
        /// </remarks>
        [Data]
        public int Flags { get; set; } // flags
        [Data]
        public int FrameGroupIndex { get; set; } // Q3 idx
        [Data]
        public int FrameStart { get; set; } // start time
        [Data]
        public int FrameEnd { get; set; } // end time
        [Data]
        public int Xa0 { get; set; }
        [Data]
        public int Xa1 { get; set; }
        [Data]
        public int Ya0 { get; set; }
        [Data]
        public int Ya1 { get; set; }
        [Data]
        public int Xb0 { get; set; }
        [Data]
        public int Xb1 { get; set; }
        [Data]
        public int Yb0 { get; set; }
        [Data]
        public int Yb1 { get; set; }
        [Data]
        public float Unk30 { get; set; }
        [Data]
        public float Unk34 { get; set; }
        [Data]
        public float Unk38 { get; set; }
        [Data]
        public float Unk3c { get; set; }
        [Data]
        public float StartAngle { get; set; } // M11? start angle
        [Data]
        public float EndAngle { get; set; } // M21? end angle
        [Data]
        public float ScaleStart { get; set; } // M31?
        [Data]
        public float ScaleEnd { get; set; } // M41?
        [Data]
        public float ScaleXStart { get; set; } // M12? sx0
        [Data]
        public float ScaleXEnd { get; set; } // M22? sx1
        [Data]
        public float ScaleYStart { get; set; } // M32? sy0
        [Data]
        public float ScaleYEnd { get; set; } // M42? sy1
        [Data]
        public float Unk60 { get; set; } // M13?
        [Data]
        public float Unk64 { get; set; } // M23?
        [Data]
        public float Unk68 { get; set; } // M33?
        [Data]
        public float Unk6c { get; set; } // M43?
        [Data]
        public float BounceXStart { get; set; } // M14? tx0?
        [Data]
        public float BounceXEnd { get; set; } // M24? tx1?
        [Data]
        public float BounceYStart { get; set; } // M34? ty0?
        [Data]
        public float BounceYEnd { get; set; } // M44? ty1?
        [Data]
        public int Unk80 { get; set; } // vtx clr0?
        /// <summary>
        /// Blend?
        /// </summary>
        /// <remarks>
        /// 0:?
        /// 1:?
        /// 2:?
        /// </remarks>
        [Data]
        public int ColorBlend { get; set; } // vtx clr1?
        [Data]
        public uint ColorStart { get; set; } // vtx clr2?
        [Data]
        public uint ColorEnd { get; set; } // vtx clr3?

        [YamlIgnore]
        public int FrameTimeLength => FrameEnd - FrameStart;

        public override string ToString() {
            return $"{Flags:x8} fg({FrameGroupIndex,2}) t({FrameStart,4},{FrameEnd,4}) Xa({Xa0,4},{Xa1,4}) Ya({Ya0,4},{Ya1,4}) Xb({Xb0,4},{Xb1,4}) Yb({Yb0,4},{Yb1,4})"
                + $" ?({Unk30},{Unk34},{Unk38},{Unk3c}) rot({Ut.R2d(StartAngle),4},{Ut.R2d(EndAngle),4})"
                + $" Scale({ScaleStart,4}, {ScaleEnd,4}) Sx({ScaleXStart,4}, {ScaleXEnd,4}) Sy({ScaleYStart,4}, {ScaleYEnd,4})"
                + $" ?({Unk60}, {Unk64}, {Unk68}, {Unk6c}) Box({BounceXStart,4}, {BounceXEnd,4}) Boy({BounceYStart,4}, {BounceYEnd,4})"
                + $" ?({Unk80:X8}) Blend({ColorBlend}) Color({ColorStart:X8} {ColorEnd:X8}) "
                ;
        }

        class Ut {
            public static int R2d(float v) {
                return (int)(v / 3.14159f * 180.0f);
            }

            public static String B2s(int v) {
                String s = "";
                for (int x = 0; x < 32; x++) {
                    s += (0 != (v & (0x80000000 >> x))) ? "*" : "-";
                }
                return s;
            }
        }

        // Q4[20] 
        // 0000: F3 7B 04 00 02 00 00 00 00 00 00 00 32 00 00 00 
        // 0010: 00 00 00 00 00 00 00 00 F9 FF FF FF F9 FF FF FF 
        // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0040: 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 
        // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
        // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0080: 00 00 00 00 00 00 00 00 F0 00 00 80 F0 00 00 80

        // Q4[25] 
        // 0000: F0 7F 07 00 09 00 00 00 32 00 00 00 C8 00 00 00 
        // 0010: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0040: 00 00 00 00 00 00 00 00 00 00 80 3F 00 00 80 3F 
        // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
        // 0060: 00 00 00 00 00 00 80 3F 00 00 80 3F 00 00 80 3F 
        // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0080: 00 00 00 00 00 00 00 00 80 80 80 80 80 80 80 80

        // Q4[26] 
        // 0000: 01 68 00 00 05 00 00 00 00 00 00 00 1E 00 00 00 
        // 0010: 32 00 00 00 CE FF FF FF 00 00 00 00 00 00 00 00 
        // 0020: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0040: 92 0A 86 3F 00 00 00 00 99 99 19 3F 00 00 80 3F 
        // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
        // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0070: 00 00 48 C2 00 00 48 C2 00 00 A0 40 00 00 A0 40 
        // 0080: 01 00 02 00 00 00 00 00 80 80 80 00 80 80 80 80

        // Q4[35] 
        // 0000: D1 3F 04 00 04 00 00 00 47 00 00 00 54 00 00 00 
        // 0010: 32 00 00 00 32 00 00 00 00 00 00 00 00 00 00 00 
        // 0020: 00 00 00 00 00 00 00 00 02 00 00 00 02 00 00 00 
        // 0030: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0040: DB 0F 49 40 DB 0F 49 40 00 00 80 3F 00 00 80 3F 
        // 0050: 00 00 80 3F 00 00 80 3F 00 00 80 3F 00 00 80 3F 
        // 0060: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0070: 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
        // 0080: 00 00 00 00 00 00 00 00 80 80 80 80 80 80 80 80

        public Animation() { }

        public Animation(BinaryReader br) {
            Flags = br.ReadInt32(); FrameGroupIndex = br.ReadInt32(); FrameStart = br.ReadInt32(); FrameEnd = br.ReadInt32();
            Xa0 = br.ReadInt32(); Xa1 = br.ReadInt32(); Ya0 = br.ReadInt32(); Ya1 = br.ReadInt32();
            Xb0 = br.ReadInt32(); Xb1 = br.ReadInt32(); Yb0 = br.ReadInt32(); Yb1 = br.ReadInt32();
            Unk30 = br.ReadSingle(); Unk34 = br.ReadSingle(); Unk38 = br.ReadSingle(); Unk3c = br.ReadSingle();

            StartAngle = br.ReadSingle(); EndAngle = br.ReadSingle(); ScaleStart = br.ReadSingle(); ScaleEnd = br.ReadSingle();
            ScaleXStart = br.ReadSingle(); ScaleXEnd = br.ReadSingle(); ScaleYStart = br.ReadSingle(); ScaleYEnd = br.ReadSingle();
            Unk60 = br.ReadSingle(); Unk64 = br.ReadSingle(); Unk68 = br.ReadSingle(); Unk6c = br.ReadSingle();
            BounceXStart = br.ReadSingle(); BounceXEnd = br.ReadSingle(); BounceYStart = br.ReadSingle(); BounceYEnd = br.ReadSingle();

            Unk80 = br.ReadInt32(); ColorBlend = br.ReadInt32(); ColorStart = br.ReadUInt32(); ColorEnd = br.ReadUInt32();
        }
    }
}
