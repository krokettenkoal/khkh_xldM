using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class Reform32 {
        static readonly byte[] tbl32pao = new byte[16] {
                 0, 1, 4, 5, 8, 9,12,13,
                 2, 3, 6, 7,10,11,14,15,
            };
        static readonly byte[] tbl32bc = new byte[32] {
                 0, 1, 4, 5,16,17,20,21,
                 2, 3, 6, 7,18,19, 22,23,
                 8, 9,12,13,24,25,28,29,
                10,11,14,15,26, 27,30,31,
            };
        /// <summary>
        /// Encode from HWREG written bin into PSMCT32 form stayed in GS.
        /// </summary>
        /// <param name="bin">your raw HWREG bin</param>
        /// <param name="bw">Width in unit of 64px</param>
        /// <param name="bh">Height in unit of 32px</param>
        /// <returns>PSMCT32 binary</returns>
        public static byte[] Encode32(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 32 * bh; wy += 32) {
                for (int wx = 0; wx < 64 * bw; wx += 64) {
                    int wwby = 8192 * ((wx / 64) + bw * (wy / 32));
                    for (int by = 0; by < 32; by += 8) {
                        for (int bx = 0; bx < 64; bx += 8) {
                            int bbby = 256 * tbl32bc[((bx / 8) + (by / 8) * 8)];
                            for (int cc = 0; cc < 4; cc++) {
                                int ccby = 64 * cc;
                                for (int pc = 0; pc < 16; pc++) {
                                    int outx = wx + bx + 0 + (pc % 8);
                                    int outy = wy + by + 2 * cc + (pc / 8);
                                    int offw = 4 * (outx + 64 * bw * outy);

                                    int offr = 4 * tbl32pao[pc] + ccby + bbby + wwby;

                                    pic[offr + 0] = bin[offw + 0];
                                    pic[offr + 1] = bin[offw + 1];
                                    pic[offr + 2] = bin[offw + 2];
                                    pic[offr + 3] = bin[offw + 3];
                                }
                            }
                        }
                    }
                }
            }
#if false
                File.WriteAllBytes("temp.bin", pic);
#endif
            return pic;
        }

        /// <summary>
        /// </summary>
        /// <param name="bin">your raw HWREG bin</param>
        /// <param name="bw">Width in unit of 64px</param>
        /// <param name="bh">Height in unit of 32px</param>
        /// <returns>PSMCT32 binary</returns>
        public static byte[] Decode32(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 32 * bh; wy += 32) {
                for (int wx = 0; wx < 64 * bw; wx += 64) {
                    int wwby = 8192 * ((wx / 64) + bw * (wy / 32));
                    for (int by = 0; by < 32; by += 8) {
                        for (int bx = 0; bx < 64; bx += 8) {
                            int bbby = 256 * tbl32bc[((bx / 8) + (by / 8) * 8)];
                            for (int cc = 0; cc < 4; cc++) {
                                int ccby = 64 * cc;
                                for (int pc = 0; pc < 16; pc++) {
                                    int outx = wx + bx + 0 + (pc % 8);
                                    int outy = wy + by + 2 * cc + (pc / 8);
                                    int offw = 4 * (outx + 64 * bw * outy);

                                    int offr = 4 * tbl32pao[pc] + ccby + bbby + wwby;

                                    pic[offw + 0] = bin[offr + 0];
                                    pic[offw + 1] = bin[offr + 1];
                                    pic[offw + 2] = bin[offr + 2];
                                    pic[offw + 3] = bin[offr + 3];
                                }
                            }
                        }
                    }
                }
            }
            return pic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">source pic</param>
        /// <param name="gsram">dest buff</param>
        /// <param name="rrw">src width</param>
        /// <param name="rrh">src height</param>
        /// <param name="ddax">dst x</param>
        /// <param name="dday">dst y</param>
        /// <param name="baseoff">gsram write offset</param>
        /// <param name="bw64">64 pixels unit (dest screen width)</param>
        public static void Encode32b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw64) {
            for (int vy = 0; vy < rrh; vy++) {
                int gy = dday + vy;
                for (int vx = 0; vx < rrw; vx++) {
                    int gx = ddax + vx;

                    int wwby = 8192 * ((gx / 64) + bw64 * (gy / 32));
                    int bbby = 256 * tbl32bc[(((gx & 63) / 8) + ((gy & 31) / 8) * 8)];
                    int cc = (gy & 7) / 2;
                    int ccby = 64 * cc;
                    int pc = (gx & 7) + 8 * (gy & 1);

                    int offr = 4 * (vx + rrw * vy);

                    int offw = baseoff + 4 * tbl32pao[pc] + ccby + bbby + wwby;

                    gsram[offw + 0] = src[offr + 0];
                    gsram[offw + 1] = src[offr + 1];
                    gsram[offw + 2] = src[offr + 2];
                    gsram[offw + 3] = src[offr + 3];
                }
            }
        }

        public static byte[] Decode32c(byte[] gsram, int cx, int cy, int readoff, int bw64) {
            byte[] pic = new byte[4 * cx * cy];
            for (int y = 0; y < cy; y++) {
                for (int x = 0; x < cx; x++) {
                    int wwby = 8192 * (((x) / 64) + bw64 * ((y) / 32));
                    int bbby = 256 * tbl32bc[(((x & 63) / 8) + ((y & 31) / 8) * 8)];
                    int cc = (y & 7) / 2;
                    int ccby = 64 * cc;
                    int pc = (x & 7) + 8 * (y & 1);

                    int offw = 4 * (x + cx * y);

                    int offr = readoff + 4 * tbl32pao[pc] + ccby + bbby + wwby;

                    pic[offw + 0] = gsram[offr + 0];
                    pic[offw + 1] = gsram[offr + 1];
                    pic[offw + 2] = gsram[offr + 2];
                    pic[offw + 3] = gsram[offr + 3];
                }
            }
            return pic;
        }
    }
}
