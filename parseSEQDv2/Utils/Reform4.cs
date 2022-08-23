using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class Reform4 {
        static byte[] tbl4bc = new byte[] {
                 0, 2, 8,10,
                 1, 3, 9,11,
                 4, 6,12,14,
                 5, 7,13,15,
                16,18,24,26,
                17,19,25,27,
                20,22,28,30,
                21,23,29,31,
            };
        static readonly int[] tbl4col0 = new int[32 * 4] {
                0,32,128,160,256,288,384,416,8,40,136,168,264,296,392,424,16,48,144,176,272,304,400,432,24,56,152,184,280,312,408,440,
                64,96,192,224,320,352,448,480,72,104,200,232,328,360,456,488,80,112,208,240,336,368,464,496,88,120,216,248,344,376,472,504,
                260,292,388,420,4,36,132,164,268,300,396,428,12,44,140,172,276,308,404,436,20,52,148,180,284,316,412,444,28,60,156,188,
                324,356,452,484,68,100,196,228,332,364,460,492,76,108,204,236,340,372,468,500,84,116,212,244,348,380,476,508,92,124,220,252,
            };
        static readonly int[] tbl4col1 = new int[32 * 4] {
                256,288,384,416,0,32,128,160,264,296,392,424,8,40,136,168,272,304,400,432,16,48,144,176,280,312,408,440,24,56,152,184,
                320,352,448,480,64,96,192,224,328,360,456,488,72,104,200,232,336,368,464,496,80,112,208,240,344,376,472,504,88,120,216,248,
                4,36,132,164,260,292,388,420,12,44,140,172,268,300,396,428,20,52,148,180,276,308,404,436,28,60,156,188,284,316,412,444,
                68,100,196,228,324,356,452,484,76, 108,204,236,332,364,460,492,84,116,212,244,340,372,468,500,92,124,220,252,348, 380,476,508,
            };

        /// <summary>
        /// Decode 4-bpp bitmap from PSMT4 form stated in GS.
        /// </summary>
        /// <param name="bin">PSMT4 binary</param>
        /// <param name="bw">Width in 128 pixels unit</param>
        /// <param name="bh">Height in 128 pixels unit</param>
        /// <returns>4bpp bitmap</returns>
        public static byte[] Decode4(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 128 * bh; wy += 128) {
                for (int wx = 0; wx < 128 * bw; wx += 128) {
                    int wlby = 8192 * ((wx / 128) + bw * (wy / 128));
                    for (int by = 0; by < 128; by += 16) {
                        for (int bx = 0; bx < 128; bx += 32) {
                            int blby = 256 * tbl4bc[(bx / 32) + 4 * (by / 16)];
                            for (int cc = 0; cc < 4; cc++) {
                                int clby = 64 * cc;
                                int[] col = ((cc & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int pc = 0; pc < 128; pc++) {
                                    int pxby = (col[pc] / 8);
                                    int pxbi = (col[pc] % 8);
                                    byte v = (byte)((bin[wlby + blby + clby + pxby] >> pxbi) & 15);

                                    int outx = wx + bx + 0 + (pc % 32);
                                    int outy = wy + by + 4 * cc + (pc / 32);
                                    int offw = outx + 128 * bw * outy;

                                    byte tv = pic[offw / 2];
                                    if (0 == (offw & 1)) { //!
                                        tv &= 0x0f;
                                        tv |= (byte)(v << 4);
                                    }
                                    else {
                                        tv &= 0xf0;
                                        tv |= v;
                                    }
                                    pic[offw / 2] = tv;
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
        /// <param name="bh">128 pixels unit</param>
        /// <returns></returns>
        public static byte[] Encode4(byte[] bin, int bw, int bh) {
            byte[] pic = new byte[bin.Length];
            for (int wy = 0; wy < 128 * bh; wy += 128) {
                for (int wx = 0; wx < 128 * bw; wx += 128) {
                    int wlby = 8192 * ((wx / 128) + bw * (wy / 128));
                    for (int by = 0; by < 128; by += 16) {
                        for (int bx = 0; bx < 128; bx += 32) {
                            int blby = 256 * tbl4bc[(bx / 32) + 4 * (by / 16)];
                            for (int cc = 0; cc < 4; cc++) {
                                int clby = 64 * cc;
                                int[] col = ((cc & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int pc = 0; pc < 128; pc++) {
                                    int outx = wx + bx + 0 + (pc % 32);
                                    int outy = wy + by + 4 * cc + (pc / 32);
                                    int offw = outx + 128 * bw * outy;

                                    byte tv = bin[offw / 2];
                                    if (0 != (offw & 1)) { // upper //!
                                        tv &= 15;
                                    }
                                    else { // lower
                                        tv >>= 4;
                                    }

                                    int pxby = (col[pc] / 8);
                                    int pxbi = (col[pc] % 8);
                                    int offr = wlby + blby + clby + pxby;
                                    byte v = pic[offr];
                                    if (pxbi == 0) { // lower
                                        v &= 0xf0;
                                        v |= tv;
                                    }
                                    else if (pxbi == 4) { // upper
                                        v &= 0x0f;
                                        v |= (byte)(tv << 4);
                                    }
                                    pic[offr] = v;
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
        public static void Encode4b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128) {
            for (int vy = 0; vy < rrh; vy++) {
                int gy = dday + vy;
                for (int vx = 0; vx < rrw; vx++) {
                    int gx = ddax + vx;

                    int wlby = 8192 * ((gx / 128) + bw128 * (gy / 128));
                    int blby = 256 * tbl4bc[((gx & 127) / 32) + 4 * ((gy & 127) / 16)];

                    int cc = (gy & 15) / 4;
                    int clby = 64 * cc;
                    int[] col = ((cc & 1) == 0) ? tbl4col0 : tbl4col1;
                    int pc = (gx & 31) + 32 * (gy & 3);

                    int offr = vx + rrw * vy;

                    byte tv = src[offr / 2];
                    if (0 != (offr & 1)) { // upper //!
                        tv &= 15;
                    }
                    else { // lower
                        tv >>= 4;
                    }

                    int pxby = (col[pc] / 8);
                    int pxbi = (col[pc] % 8);
                    int offwr = baseoff + wlby + blby + clby + pxby;
                    byte v = gsram[offwr];
                    if (pxbi == 0) { // lower
                        v &= 0xf0;
                        v |= tv;
                    }
                    else if (pxbi == 4) { // upper
                        v &= 0x0f;
                        v |= (byte)(tv << 4);
                    }
                    gsram[offwr] = v;
                }
            }
        }

        /// <summary>
        /// Decode4. No byte alignment in scanline performed, in case of cx=1,3,5,7,9,11,...
        /// </summary>
        /// <param name="gsram"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="readoff"></param>
        /// <param name="bw128"></param>
        /// <returns></returns>
        public static byte[] Decode4c(byte[] gsram, int cx, int cy, int readoff, int bw128) {
            byte[] pic = new byte[(cx * cy + 1) / 2];
            for (int y = 0; y < cy; y++) {
                for (int x = 0; x < cx; x++) {
                    int wlby = 8192 * ((x / 128) + bw128 * (y / 128));

                    int blby = 256 * tbl4bc[((x & 127) / 32) + 4 * ((y & 127) / 16)];

                    int cc = (y & 15) / 4;
                    int clby = 64 * cc;
                    int[] col = ((cc & 1) == 0) ? tbl4col0 : tbl4col1;
                    int pc = (x & 31) + 32 * (y & 3);
                    int pxby = (col[pc] / 8);
                    int pxbi = (col[pc] % 8);
                    byte v = (byte)((gsram[readoff + wlby + blby + clby + pxby] >> pxbi) & 15);

                    int offw = x + cx * y;

                    byte tv = pic[offw / 2];
                    if (0 == (offw & 1)) {
                        tv &= 0x0f;
                        tv |= (byte)(v << 4);
                    }
                    else {
                        tv &= 0xf0;
                        tv |= v;
                    }
                    pic[offw / 2] = tv;
                }
            }
            return pic;
        }
    }
}
