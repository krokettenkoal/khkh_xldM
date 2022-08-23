using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ee1Dec.Utils {
    class StandardColorComparer : IComparer<Color> {
        public int Compare(Color x, Color y) {
            if (x.A < y.A) {
                return -1;
            }
            if (x.A > y.A) {
                return 1;
            }
            if (x.GetHue() < y.GetHue()) {
                return -1;
            }
            if (x.GetHue() > y.GetHue()) {
                return 1;
            }
            if (x.GetSaturation() < y.GetSaturation()) {
                return -1;
            }
            if (x.GetSaturation() > y.GetSaturation()) {
                return 1;
            }
            if (x.GetBrightness() < y.GetBrightness()) {
                return -1;
            }
            if (x.GetBrightness() > y.GetBrightness()) {
                return 1;
            }
            return 0;
        }
    }
}
