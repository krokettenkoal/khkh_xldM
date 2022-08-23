using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;
using System.IO;

namespace khiiMapv.Models.Coct {
    public class CollisionReader {
        public List<Co1> alCo1 = new List<Co1>();
        public List<Co2> alCo2 = new List<Co2>();
        public List<Co3> alCo3 = new List<Co3>();
        public List<Vector4> alCo4 = new List<Vector4>();
        public List<Plane> alCo5 = new List<Plane>();
        public List<Co6> alCo6 = new List<Co6>();
        public List<Co7> alCo7 = new List<Co7>();

        public void Read(Stream si) {
            BinaryReader br = new BinaryReader(si);
            if (br.ReadByte() != (byte)'C') throw new InvalidDataException();
            if (br.ReadByte() != (byte)'O') throw new InvalidDataException();
            if (br.ReadByte() != (byte)'C') throw new InvalidDataException();
            if (br.ReadByte() != (byte)'T') throw new InvalidDataException();
            if (br.ReadInt32() != 1) throw new InvalidDataException();

            {
                si.Position = 8;
                int cntCo1 = br.ReadInt32();
                si.Position = 0x18;
                int offCo1 = br.ReadInt32();
                int lenCo1 = br.ReadInt32();

                si.Position = offCo1;
                while (si.Position < offCo1 + lenCo1) alCo1.Add(new Co1(br));
            }

            {
                si.Position = 0x20;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) alCo2.Add(new Co2(br));
            }

            {
                si.Position = 0x28;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) alCo3.Add(new Co3(br));
            }

            {
                si.Position = 0x30;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) {
                    Vector4 v = new Vector4();
                    v.X = br.ReadSingle();
                    v.Y = br.ReadSingle();
                    v.Z = br.ReadSingle();
                    v.W = br.ReadSingle();
                    alCo4.Add(v);
                }
            }

            {
                si.Position = 0x38;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) {
                    Plane v = new Plane();
                    v.Normal.X = br.ReadSingle();
                    v.Normal.Y = br.ReadSingle();
                    v.Normal.Z = br.ReadSingle();
                    v.D = br.ReadSingle();
                    alCo5.Add(v);
                }
            }

            {
                si.Position = 0x40;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) alCo6.Add(new Co6(br));
            }

            {
                si.Position = 0x48;
                int off = br.ReadInt32();
                int len = br.ReadInt32();

                si.Position = off;
                while (si.Position < off + len) alCo7.Add(new Co7(br));
            }
        }
    }
}
