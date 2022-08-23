using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.EEDis {
    public class EEis {
        public string[] al = null;

        public EEis(string opc) {
            al = new string[] { opc };
        }
        public EEis(string opc, string opr1) {
            al = new string[] { opc, opr1 };
        }
        public EEis(string opc, string opr1, string opr2) {
            al = new string[] { opc, opr1, opr2 };
        }
        public EEis(string opc, string opr1, string opr2, string opr3) {
            al = new string[] { opc, opr1, opr2, opr3 };
        }

        public override string ToString() {
            string s = al[0] + " ";
            for (int x = 1; x < al.Length; x++) {
                if (x != 1) s += ", ";
                s += al[x];
            }
            return s;
        }
    }
}
