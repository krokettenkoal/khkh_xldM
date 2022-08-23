using ee1Dec.C;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ee1Dec.Utils {
    class RiAddrUt {
        public static uint Resolve(string s, CustEE e) {
            try {
                return Convert.ToUInt32(s, 16);
            }
            catch (FormatException) {
                if (s.StartsWith(":")) {
                    Stack<uint> stk = new Stack<uint>();
                    foreach (string tkn in Regex.Replace(Regex.Replace(s.Substring(1), "([\\+\\-])", " $& "), "[ 　\t]+", " ").Split(' ')) {
                        if (tkn.Length == 0) continue;
                        // "#zero#at# #v0# #v1# #a0# #a1# #a2# #a3# #t0# #t1# #t2# #t3# #t4# #t5# #t6# #t7# #s0# #s1# #s2# #s3# #s4# #s5# #s6# #s7# #t8# #t9# #k0# #k1#
                        // #gp# #sp# #r8# #ra#".IndexOf("#" + var + "#");
                        switch (tkn) {
                            case "r0": stk.Push((uint)(e.r0.UL0)); continue;
                            case "at": stk.Push((uint)(e.at.UL0)); continue;
                            case "v0": stk.Push((uint)(e.v0.UL0)); continue;
                            case "v1": stk.Push((uint)(e.v1.UL0)); continue;
                            case "a0": stk.Push((uint)(e.a0.UL0)); continue;
                            case "a1": stk.Push((uint)(e.a1.UL0)); continue;
                            case "a2": stk.Push((uint)(e.a2.UL0)); continue;
                            case "a3": stk.Push((uint)(e.a3.UL0)); continue;
                            case "t0": stk.Push((uint)(e.t0.UL0)); continue;
                            case "t1": stk.Push((uint)(e.t1.UL0)); continue;
                            case "t2": stk.Push((uint)(e.t2.UL0)); continue;
                            case "t3": stk.Push((uint)(e.t3.UL0)); continue;
                            case "t4": stk.Push((uint)(e.t4.UL0)); continue;
                            case "t5": stk.Push((uint)(e.t5.UL0)); continue;
                            case "t6": stk.Push((uint)(e.t6.UL0)); continue;
                            case "t7": stk.Push((uint)(e.t7.UL0)); continue;
                            case "s0": stk.Push((uint)(e.s0.UL0)); continue;
                            case "s1": stk.Push((uint)(e.s1.UL0)); continue;
                            case "s2": stk.Push((uint)(e.s2.UL0)); continue;
                            case "s3": stk.Push((uint)(e.s3.UL0)); continue;
                            case "s4": stk.Push((uint)(e.s4.UL0)); continue;
                            case "s5": stk.Push((uint)(e.s5.UL0)); continue;
                            case "s6": stk.Push((uint)(e.s6.UL0)); continue;
                            case "s7": stk.Push((uint)(e.s7.UL0)); continue;
                            case "t8": stk.Push((uint)(e.t8.UL0)); continue;
                            case "t9": stk.Push((uint)(e.t9.UL0)); continue;
                            case "k0": stk.Push((uint)(e.k0.UL0)); continue;
                            case "k1": stk.Push((uint)(e.k1.UL0)); continue;
                            case "gp": stk.Push((uint)(e.gp.UL0)); continue;
                            case "sp": stk.Push((uint)(e.sp.UL0)); continue;
                            case "s8": stk.Push((uint)(e.s8.UL0)); continue;
                            case "ra": stk.Push((uint)(e.ra.UL0)); continue;
                        }
                        if (tkn == "+") {
                            stk.Push(stk.Pop() + stk.Pop());
                            continue;
                        }
                        if (tkn == "-") {
                            stk.Push(stk.Pop() + stk.Pop());
                            continue;
                        }
                        try {
                            stk.Push(Convert.ToUInt32(tkn, 16));
                            continue;
                        }
                        catch (FormatException err) {
                            throw new ERR(err);
                        }
                    }
                    return stk.Pop();
                }
                throw new ERR(new FormatException());
            }
        }

        public class ERR : ApplicationException {
            public ERR(Exception err) : base("Failed to resolve", err) { }
        }
    }
}
