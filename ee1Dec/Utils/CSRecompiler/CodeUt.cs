using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.CSRecompiler {
    public class CodeUt {
        public static CodeExpression refGPR(string gpr) {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr));
        }
        public static CodeExpression refGPRul0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "UL0");
        }
        public static CodeExpression refFPRf(int val) {
            return new CodeFieldReferenceExpression(new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fpr"), new CodePrimitiveExpression(val)), "f");
        }
        public static CodeExpression refGPRud0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "UD0");
        }
        public static CodeExpression refGPRsd0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "SD0");
        }
        public static CodeExpression refFPR(int val) {
            return new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fpr"), new CodePrimitiveExpression(val));
        }
        public static CodeExpression refFPRACCf() {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fpracc"), "f");
        }
        public static CodeExpression refVF(int ft) {
            return new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "VF"), new CodePrimitiveExpression(ft));
        }
        public static CodeExpression refVFa(int fi, int a) {
            return new CodePropertyReferenceExpression(new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "VF"), new CodePrimitiveExpression(fi)), "" + "xyzw"[a]);
        }
        public static CodeExpression refVqf() {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "Vq"), "f");
        }
        public static CodeExpression refVI(int ft) {
            return new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "VI"), new CodePrimitiveExpression(ft));
        }
        public static CodeExpression refVacca(int a) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "Vacc"), "" + "xyzw"[a]);
        }
        public static CodeExpression refGPRus0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "US0");
        }
        public static CodeExpression refGPRub0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "UB0");
        }
        public static CodeExpression refGPRsl0(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "SL0");
        }
        public static CodeExpression refGPRud1(string gpr) {
            return new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(gpr)), "UD1");
        }
        public static CodeExpression refGPRula(string gpr, int a) {
            return new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), gpr), "UL"), new CodePrimitiveExpression(a));
        }
        public static CodeExpression refVacc() {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "Vacc");
        }
        public static CodeExpression refLO() {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "LO");
        }
        public static CodeExpression refHI() {
            return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "HI");
        }
    }
}
