using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    class SPUConv {
        static int[,] f = new int[,] {
                        {    0,  0  },
                        {   60,  0  },
                        {  115, -52 },
                        {   98, -55 },
                        {  122, -60 } };

        public static byte[] ToWave(MemoryStream fs, int nSamplesPerSec) {
            int s_1 = 0, s_2 = 0;
            List<int> SB = new List<int>();
            while (fs.Position + 16 <= fs.Length) {
                byte[] start = new byte[16];
                Trace.Assert(16 == fs.Read(start, 0, 16));

                {
                    int predict_nr = (int)start[0];
                    int shift_factor = predict_nr & 0xf;
                    predict_nr >>= 4;
                    int flags = (int)start[1];

                    for (int nSample = 0; nSample < 14; nSample++) {
                        int d = (int)start[2 + nSample];
                        int s = ((d & 0xf) << 12);
                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);

                        int fa = (s >> shift_factor);
                        fa = fa + ((s_1 * f[predict_nr, 0]) >> 6) + ((s_2 * f[predict_nr, 1]) >> 6);
                        s_2 = s_1; s_1 = fa;
                        s = ((d & 0xf0) << 8);

                        SB.Add(fa);

                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);
                        fa = (s >> shift_factor);
                        fa = fa + ((s_1 * f[predict_nr, 0]) >> 6) + ((s_2 * f[predict_nr, 1]) >> 6);
                        s_2 = s_1; s_1 = fa;

                        SB.Add(fa);
                    }
                }
            }

            MemoryStream os = new MemoryStream();
            int nSamples = SB.Count;
            //int nSamplesPerSec = 44100;
            BinaryWriter wr = new BinaryWriter(os);
            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write((int)(4 + (8) + (2 + 2 + 4 + 4 + 2 + 2 + 2) + (8) + (4) + (8) + (2 * nSamples)));
            wr.Write(Encoding.ASCII.GetBytes("WAVE"));
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write((int)(2 + 2 + 4 + 4 + 2 + 2 + 2));
            wr.Write((short)(1));
            wr.Write((short)(1));
            wr.Write((int)(nSamplesPerSec));
            wr.Write((int)(nSamplesPerSec * 2));
            wr.Write((short)(2));
            wr.Write((short)(16));
            wr.Write((short)(0));
            wr.Write(Encoding.ASCII.GetBytes("fact"));
            wr.Write((int)(4));
            wr.Write((int)(nSamples));
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write((int)(2 * nSamples));
            for (int x = 0; x < nSamples; x++) {
                int v = Math.Max(-32768, Math.Min(32767, SB[x]));
                wr.Write((ushort)(v));
            }
            return os.ToArray();
        }

        public static byte[] ToWave2ch(MemoryStream fs, int nSamplesPerSec) {
            int s_1x = 0, s_2x = 0; // left channel
            int s_1y = 0, s_2y = 0; // right channel
            List<int> SBx = new List<int>();
            List<int> SBy = new List<int>();
            while (fs.Position + 32 <= fs.Length) {
                byte[] start = new byte[16];

                {
                    Trace.Assert(16 == fs.Read(start, 0, 16));

                    int predict_nr = (int)start[0];
                    int shift_factor = predict_nr & 0xf;
                    predict_nr >>= 4;
                    int flags = (int)start[1];

                    for (int nSample = 0; nSample < 14; nSample++) {
                        int d = (int)start[2 + nSample];
                        int s = ((d & 0xf) << 12);
                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);

                        int fa = (s >> shift_factor);
                        fa = fa + ((s_1x * f[predict_nr, 0]) >> 6) + ((s_2x * f[predict_nr, 1]) >> 6);
                        s_2x = s_1x; s_1x = fa;
                        s = ((d & 0xf0) << 8);

                        SBx.Add(fa);

                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);
                        fa = (s >> shift_factor);
                        fa = fa + ((s_1x * f[predict_nr, 0]) >> 6) + ((s_2x * f[predict_nr, 1]) >> 6);
                        s_2x = s_1x; s_1x = fa;

                        SBx.Add(fa);
                    }
                }

                {
                    Trace.Assert(16 == fs.Read(start, 0, 16));

                    int predict_nr = (int)start[0];
                    int shift_factor = predict_nr & 0xf;
                    predict_nr >>= 4;
                    int flags = (int)start[1];

                    for (int nSample = 0; nSample < 14; nSample++) {
                        int d = (int)start[2 + nSample];
                        int s = ((d & 0xf) << 12);
                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);

                        int fa = (s >> shift_factor);
                        fa = fa + ((s_1y * f[predict_nr, 0]) >> 6) + ((s_2y * f[predict_nr, 1]) >> 6);
                        s_2y = s_1y; s_1y = fa;
                        s = ((d & 0xf0) << 8);

                        SBy.Add(fa);

                        if (0 != (s & 0x8000)) s = (int)((uint)s | 0xffff0000U);
                        fa = (s >> shift_factor);
                        fa = fa + ((s_1y * f[predict_nr, 0]) >> 6) + ((s_2y * f[predict_nr, 1]) >> 6);
                        s_2y = s_1y; s_1y = fa;

                        SBy.Add(fa);
                    }
                }
            }

            MemoryStream os = new MemoryStream();
            int nSamples = SBx.Count;
            //int nSamplesPerSec = 44100;
            BinaryWriter wr = new BinaryWriter(os);
            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write((int)(4 + (8) + (2 + 2 + 4 + 4 + 2 + 2 + 2) + (8) + (4) + (8) + (4 * nSamples)));
            wr.Write(Encoding.ASCII.GetBytes("WAVE"));
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write((int)(2 + 2 + 4 + 4 + 2 + 2 + 2));
            wr.Write((short)(1));
            wr.Write((short)(2));
            wr.Write((int)(nSamplesPerSec));
            wr.Write((int)(nSamplesPerSec * 4));
            wr.Write((short)(4));
            wr.Write((short)(16));
            wr.Write((short)(0));
            wr.Write(Encoding.ASCII.GetBytes("fact"));
            wr.Write((int)(4));
            wr.Write((int)(nSamples));
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write((int)(4 * nSamples));
            for (int x = 0; x < nSamples; x++) {
                {
                    int v = Math.Max(-32768, Math.Min(32767, SBx[x]));
                    wr.Write((ushort)(v));
                }
                {
                    int v = Math.Max(-32768, Math.Min(32767, SBy[x]));
                    wr.Write((ushort)(v));
                }
            }
            return os.ToArray();
        }
    }
}
