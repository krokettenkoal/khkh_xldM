using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CustVPU1 {
    public class WriteSimv1 {
        public static void Write(String fp, Core1 w1) {
            using (FileStream fs = File.Create(fp)) {
                byte[] hdr = Encoding.ASCII.GetBytes(
                    "v1\n"
                    + "tpc=" + w1.pc.ToString("x") + "\n"
                    + "top=" + w1.top.ToString("x") + "\n"
                    + "\x1A"
                    );
                fs.Write(hdr, 0, hdr.Length);
                FSUt.FillUntil(fs, 1024);

                BinaryWriter wr = new BinaryWriter(fs);
                for (int t = 0; t < 32; t++)
                    SerUt.writeVec4(wr, w1.vf[t]);
                for (int t = 0; t < 16; t++)
                    SerUt.writeVi(wr, w1.vi[t]);

                SerUt.writeVec4(wr, w1.acc);
                SerUt.writeF(wr, w1.I);
                SerUt.writeF(wr, w1.Q);
                SerUt.writeF(wr, 0f);
                SerUt.writeF(wr, w1.P);

                FSUt.FillUntil(fs, 1024 + 1024);
                fs.Write(w1.Mem, 0, 16384);
                fs.Write(w1.Micro, 0, 16384);
            }
        }

        class FSUt {
            public static void FillUntil(FileStream fs, Int64 pos) {
                byte[] bin = new byte[Convert.ToInt32(pos - fs.Position)];
                fs.Write(bin, 0, bin.Length);
            }
        }

        class SerUt {
            public static void writeVec4(BinaryWriter wr, Vec4 o) {
                wr.Write(o.x);
                wr.Write(o.y);
                wr.Write(o.z);
                wr.Write(o.w);
            }
            public static void writeVi(BinaryWriter wr, ushort o) {
                wr.Write(o);
            }

            public static void writeF(BinaryWriter wr, float o) {
                wr.Write(o);
            }
        }
    }
}
