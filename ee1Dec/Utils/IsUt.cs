using ee1Dec.C;
using ee1Dec.Utils.CSRecompiler;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils {
    class IsUt {
        public static void SetGPR(CodeStatementCollection fns, string s, GPR gpr) {
            if (true) {
                CodeAssignStatement stmt = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), s), "UD0"),
                    new CodePrimitiveExpression(gpr.UD[0])
                    );
                fns.Add(stmt);
            }
            if (true) {
                CodeAssignStatement stmt = new CodeAssignStatement(
                    new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), s), "UD1"),
                    new CodePrimitiveExpression(gpr.UD[1])
                    );
                fns.Add(stmt);
            }
        }

        public static void SetFPR(CodeStatementCollection fns, int t, FPR fpr) {
            CodeAssignStatement stmt = new CodeAssignStatement(
                CodeUt.refFPRf(t),
                new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "UL2F"),
                    new CodePrimitiveExpression(MobUt.F2UL(fpr.f))
                )
            );
            fns.Add(stmt);
        }

        public static void SetVF(CodeStatementCollection fns, int t, Vec VF) {
            for (int w = 0; w < 4; w++) {
                CodeAssignStatement stmt = new CodeAssignStatement(
                    CodeUt.refVFa(t, w),
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "UL2F"),
                        new CodePrimitiveExpression(MobUt.F2UL(VF.F[w]))
                    )
                );
                fns.Add(stmt);
            }
        }

        public static void SetFPRacc(CodeStatementCollection fns, FPR acc) {
            CodeAssignStatement stmt = new CodeAssignStatement(
                CodeUt.refFPRACCf(),
                new CodeMethodInvokeExpression(
                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "UL2F"),
                    new CodePrimitiveExpression(MobUt.F2UL(acc.f))
                )
            );
            fns.Add(stmt);
        }
    }
}
