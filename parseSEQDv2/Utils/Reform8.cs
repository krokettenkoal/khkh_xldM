using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class Reform8 {
        static readonly byte[] tbl8bc = new byte[] {
                 0, 1, 4, 5,16,17,20,21,
                 2, 3, 6, 7,18,19,22,23,
                 8, 9,12,13,24,25,28,29,
                10,11,14,15,26,27,30,31,
            };
        static readonly byte[] tbl8c0 = new byte[16 * 4] {
                0,4,16,20,32,36,48,52,2,6,18,22,34,38,50,54,
                8,12,24,28,40,44,56,60,10,14,26,30,42,46,58,62,
                33,37,49,53,1,5,17,21,35,39,51,55,3,7,19,23,
                41,45,57,61,9,13,25,29,43,47,59,63,11,15,27,31,
            };
        static readonly byte[] tbl8c1 = new byte[16 * 4] {
                32,36,48,52,0,4,16,20,34,38,50,54,2,6,18,22,
                40,44,56,60,8,12,24,28,42,46,58,62,10,14,26,30,
                1,5,17,21,33,37,49,53,3,7,19,23,35,39,51,55,
                9,13,25,29,41,45,57,61,11,15,27,31,43,47,59,63,
            };

        /// <summary>
        /// Decode 8-bpp bitmap from PSMT8 form stated in GS.
        /// </summary>
        /// <param name="bin">PSMT8 binary</param>
        /// <param name="bw">Width in 128 pixels unit</param>
        /// <param name="bh">Height in 64 pixels unit</param>
        /// <returns>4bpp bitmap</returns>
        public static byte[] Decode8(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 64 * bh; wy += 64) {
                for (int wx = 0; wx < 128 * bw; wx += 128) {
                    int wlby = 8192 * ((wx / 128) + bw * (wy / 64));
                    for (int by = 0; by < 64; by += 16) {
                        for (int bx = 0; bx < 128; bx += 16) {
                            int blby = 256 * tbl8bc[(bx / 16) + 8 * (by / 16)];
                            for (int cc = 0; cc < 4; cc++) {
                                int clby = 64 * cc;
                                byte[] col = ((cc & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int pc = 0; pc < 64; pc++) {
                                    int offr = wlby + blby + clby + col[pc];

                                    int outx = wx + bx + 0 + (pc % 16);
                                    int outy = wy + by + 4 * cc + (pc / 16);
                                    int offw = outx + 128 * bw * outy;

                                    pic[offw] = bin[offr];
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
        /// <param name="bin">raw buffer</param>
        /// <param name="bw">128 pixels unit</param>
        /// <param name="bh">64 pixels unit</param>
        /// <returns></returns>
        public static byte[] Encode8(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 64 * bh; wy += 64) {
                for (int wx = 0; wx < 128 * bw; wx += 128) {
                    int wlby = 8192 * ((wx / 128) + bw * (wy / 64));
                    for (int by = 0; by < 64; by += 16) {
                        for (int bx = 0; bx < 128; bx += 16) {
                            int blby = 256 * tbl8bc[(bx / 16) + 8 * (by / 16)];
                            for (int cc = 0; cc < 4; cc++) {
                                int clby = 64 * cc;
                                byte[] col = ((cc & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int pc = 0; pc < 64; pc++) {
                                    int offr = wlby + blby + clby + col[pc];

                                    int outx = wx + bx + 0 + (pc % 16);
                                    int outy = wy + by + 4 * cc + (pc / 16);
                                    int offw = outx + 128 * bw * outy;

                                    pic[offr] = bin[offw];
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
        /// <param name="bw128">128 pixels unit (dest screen width)</param>
        public static void Encode8b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128) {
            for (int vy = 0; vy < rrh; vy++) {
                int gy = dday + vy;
                for (int vx = 0; vx < rrw; vx++) {
                    int gx = ddax + vx;

                    int wlby = 8192 * ((gx / 128) + bw128 * (gy / 64));
                    int blby = 256 * tbl8bc[((gx & 127) / 16) + 8 * ((gy & 63) / 16)];
                    int cc = (gy & 15) / 4;
                    int clby = 64 * cc;
                    byte[] col = ((cc & 1) == 0) ? tbl8c0 : tbl8c1;
                    int pc = (gx & 15) + 16 * (gy & 3);

                    int offwr = baseoff + wlby + blby + clby + col[pc];

                    int offr = vx + rrw * vy;

                    gsram[offwr] = src[offr];
                }
            }
        }

        public static byte[] Decode8c(byte[] gsram, int cx, int cy, int readoff, int bw128) {
            byte[] pic = new byte[cx * cy];
            for (int y = 0; y < cy; y++) {
                for (int x = 0; x < cx; x++) {
                    int wlby = 8192 * ((x / 128) + bw128 * (y / 64));
                    int blby = 256 * tbl8bc[((x & 127) / 16) + 8 * ((y & 63) / 16)];
                    int cc = (y & 15) / 4;
                    int clby = 64 * cc;
                    byte[] col = ((cc & 1) == 0) ? tbl8c0 : tbl8c1;
                    int pc = (x & 15) + 16 * (y & 3);

                    int offr = readoff + wlby + blby + clby + col[pc];

                    int offw = x + cx * y;

                    pic[offw] = gsram[offr];
                }
            }
            return pic;
        }
    }
}
