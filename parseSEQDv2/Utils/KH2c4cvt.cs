using System;
using System.Collections.Generic;
using System.Text;

namespace parseSEQDv2.Utils {
    public class KH2c4cvt {
        public static int ToWin(uint v) {
            byte a = (byte)Math.Min(255, (255 & (v >> 24)) * 1);
            byte b = (byte)Math.Min(255, (255 & (v >> 16)) * 1);
            byte g = (byte)Math.Min(255, (255 & (v >> 8)) * 1);
            byte r = (byte)Math.Min(255, (255 & (v >> 0)) * 1);
            return (a << 24) | (r << 16) | (g << 8) | (b << 0);
        }
    }
}
