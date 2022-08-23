using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.CSRecompiler {
    public class LabUt {
        public static string addr2Lab(uint k) {
            return "_" + k.ToString("x8");
        }

        public static string addr2Funct(uint k) {
            return "funct" + k.ToString("x8");
        }

        public static string addr2libfn(uint addr) {
            return "_" + addr.ToString("x8") + ".dll";
        }
    }
}
