using System;
using System.Collections.Generic;
using System.Text;

namespace vcBinTex4 {
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
