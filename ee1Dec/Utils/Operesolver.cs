using ee1Dec.C;
using ee1Dec.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ee1Dec.Utils {
    class Operesolver {
        public uint val = 0;
        public float fval = 0;
        public Operty operty = Operty.Invalid;

        public bool resolve(string var, CustEE e) {
            if (true) {
                int eer = "#zero#at# #v0# #v1# #a0# #a1# #a2# #a3# #t0# #t1# #t2# #t3# #t4# #t5# #t6# #t7# #s0# #s1# #s2# #s3# #s4# #s5# #s6# #s7# #t8# #t9# #k0# #k1# #gp# #sp# #s8# #ra#".IndexOf("#" + var + "#");
                if (eer != -1) {
                    eer /= 5;
                    val = e.GPR[eer].UL[0];
                    operty = Operty.Val;
                    return true;
                }
            }
            if (true) {
                Match M = Regex.Match(var, "^\\$([0-9a-f]{4})\\((..)\\)", RegexOptions.IgnoreCase);
                if (M.Success) {
                    int off = Convert.ToInt32(M.Groups[1].Value, 16);
                    int eer = "#zero#at# #v0# #v1# #a0# #a1# #a2# #a3# #t0# #t1# #t2# #t3# #t4# #t5# #t6# #t7# #s0# #s1# #s2# #s3# #s4# #s5# #s6# #s7# #t8# #t9# #k0# #k1# #gp# #sp# #s8# #ra#".IndexOf("#" + M.Groups[2].Value + "#");
                    if (eer != -1) {
                        eer /= 5;
                        val = (uint)(e.GPR[eer].UL[0] + ((short)off));
                        operty = Operty.Off;
                        return true;
                    }
                }
            }
            if (true) {
                Match M = Regex.Match(var, "\\$f([0-9]+)$");
                if (M.Success) {
                    int ri = int.Parse(M.Groups[1].Value);
                    fval = e.fpr[ri].f;
                    operty = Operty.FVal;
                    return true;
                }
            }
            return false;
        }
    }
}
