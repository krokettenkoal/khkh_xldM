using System;
using System.Collections.Generic;
using System.Text;

namespace parseSEQD {
    public class KH2c4cvt {
        public static int ToWin(int i) {
            uint v = (uint)i;
            byte a = (byte)Math.Min(255, (255 & (v >> 24)) * 2);
            byte b = (byte)Math.Min(255, (255 & (v >> 16)) * 2);
            byte g = (byte)Math.Min(255, (255 & (v >> 8)) * 2);
            byte r = (byte)Math.Min(255, (255 & (v >> 0)) * 2);
            return (a << 24) | (r << 16) | (g << 8) | (b << 0);
        }
    }
}
