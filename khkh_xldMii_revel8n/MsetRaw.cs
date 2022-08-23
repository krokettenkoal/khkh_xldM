using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SlimDX;

namespace khkh_xldMii {
    public class MsetRM {
        public List<Matrix> al = new List<Matrix>();
    }
    public class MsetRawblk {
        public List<MsetRM> alrm = new List<MsetRM>();
        public int cntJoints;
        public int cntFrames { get { return alrm.Count; } }

        public MsetRawblk(Stream si) {
            BinaryReader br = new BinaryReader(si);
            si.Position = 0x90;
            int v90 = br.ReadInt32();
            if (v90 != 1) throw new NotSupportedException("v90 != 1");
            si.Position = 0xA0;
            int va0 = cntJoints = br.ReadInt32(); // @0xa0 cnt axbone
            si.Position = 0xB4;
            int vb4 = br.ReadInt32(); // @0xb4 cnt frames

            si.Position = 0xF0;

            alrm.Capacity = vb4;
            for (int t = 0; t < vb4; t++) {
                MsetRM rm = new MsetRM();
                rm.al.Capacity = va0;
                alrm.Add(rm);
                for (int x = 0; x < va0; x++) {
                    Matrix M1 = new Matrix();
                    M1.M11 = br.ReadSingle(); M1.M12 = br.ReadSingle(); M1.M13 = br.ReadSingle(); M1.M14 = br.ReadSingle();
                    M1.M21 = br.ReadSingle(); M1.M22 = br.ReadSingle(); M1.M23 = br.ReadSingle(); M1.M24 = br.ReadSingle();
                    M1.M31 = br.ReadSingle(); M1.M32 = br.ReadSingle(); M1.M33 = br.ReadSingle(); M1.M34 = br.ReadSingle();
                    M1.M41 = br.ReadSingle(); M1.M42 = br.ReadSingle(); M1.M43 = br.ReadSingle(); M1.M44 = br.ReadSingle();
                    rm.al.Add(M1);
                }
            }
        }
    }
}
