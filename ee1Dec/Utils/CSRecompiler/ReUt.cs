using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.CSRecompiler {
    class ReUt {
        public static string GPR(string text) {
            if (text.Equals("zero")) return "r0";
            return text;
        }
    }
}
