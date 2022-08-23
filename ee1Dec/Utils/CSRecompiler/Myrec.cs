#define InsertHere
#define UseRecOpt
#define EnableDebug
#define AllowCustVPU1

using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.IO;
using System.Diagnostics;
using ee1Dec.C;
using System.CodeDom.Compiler;
using ee1Dec.Utils;
using ee1Dec.Utils.EEDis;

namespace ee1Dec.Utils.CSRecompiler {
    public class Myrec {
        class JxxUt {
            public static bool isJxx(string inst) {
                return "#BEQ#BEQL#BGEZ#BGEZL#BGTZ#BGTZL#BLEZ#BLEZL#BLTZ#BLTZ#BNE#BNEL#BC0F#BC0FL#BC0T#BC0TL#BC1F#BC1FL#BC1T#BC1TL#BC2F#BC2FL#BC2T#BC2TL#J#JR#BGEZAL#BGEZALL#BLTZAL#BLTZALL#JAL#JALR#".IndexOf("#" + inst + "#") >= 0;
            }
        }

        public static void Rec1(uint addr, out CodeMemberMethod fn, Stream sieeMem) {
            fn = new CodeMemberMethod();
            fn.Name = LabUt.addr2Funct(addr);
            fn.Attributes = MemberAttributes.Public;

            CodeStatementCollection altSlot = null;

            CodePrimitiveExpression letex;
            CodeAssignStatement letpc = new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                letex = new CodePrimitiveExpression(0)
                );
            fn.Statements.Add(letpc);

            int exitVal = -1;
            while ((exitVal--) != 0) {
                uint pc = addr;
                sieeMem.Position = pc;
                BinaryReader br = new BinaryReader(sieeMem);
                CodeOpti cti = CodeOpti.Parse(br);
                CodeStatementCollection fns = (altSlot == null) ? fn.Statements : altSlot;
                if (cti is CodeOptiOne) {
                    uint word = ((CodeOptiOne)cti).w0;
                    EEis o = EEDisarm.parse(word, pc);

                    if (JxxUt.isJxx(o.al[0])) exitVal = 1;

                    fns.Add(new CodeCommentStatement(new CodeComment("@" + pc.ToString("X8") + "   " + o.ToString())));

#if InsertHere
                    if (true) {
                        CodeAssignStatement myopc = new CodeAssignStatement(
                            new CodeVariableReferenceExpression("opc"),
                            new CodePrimitiveExpression(pc)
                        );
                        fns.Add(myopc);
                    }
#endif

                    string opc = o.al[0];
                    string[] oal = o.al;
                    #region ### My Disarm ###
                    if (false) { }
                    else if (opc.Equals("LUI")) {
                        // EE> LUI t7, $0035
                        // cs> ee.t7.US[1] = 0x0035;
                        // cs> this.ee.t7.UD0 = 53 << 16;
                        long val = (int)(Convert.ToUInt16(oal[2].Substring(1, 4), 16) << 16);
                        CodeAssignStatement let = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(let);
                    }
                    else if (opc.Equals("ADDIU")) {
                        // EE> ADDIU t7, t7, $4260
                        // cs> ee.t7.SL[0] = ee.t7.SL[0] + (short)0x4260;
                        // cs> this.ee.t7.SD0 = ((int)((this.ee.t7.SD0 + 16992)));
                        short val = (short)Convert.ToUInt16(oal[3].Substring(1, 4), 16);
                        CodeAssignStatement let = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodeCastExpression(
                                typeof(int),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.Add,
                                    new CodePrimitiveExpression(val)
                                    )
                                )
                            );
                        fns.Add(let);
                    }
                    else if (opc.Equals("LQ")) {
                        // EE> LQ t0, $0000(t7)
                        // cs) MobUt.LQ(ee, ee.t0, 0x0000 + ee.t7.UL[0]);
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression call = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LQ"),
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(oal[1])),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), r21), "UL"), new CodePrimitiveExpression(0))
                                )
                            );
                        fns.Add(call);
                    }
                    else if (opc.Equals("SQ")) {
                        // EE> SQ t0, $0000(a0)
                        // cs) MobUt.SQ(ee, ee.t0, 0x0000 + ee.t7.UL[0]);
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression call = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SQ"),
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(oal[1])),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), r21), "UL"), new CodePrimitiveExpression(0))
                                )
                            );
                        fns.Add(call);
                    }
                    else if (opc.Equals("JR")) {
                        // EE> JR ra
                        // cs) pc = ee.ra.UL[0];    
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                            new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(oal[1])), "UL"), new CodePrimitiveExpression(0))
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("NOP") || opc.Equals("VWAITQ") || opc.Equals("VNOP") || opc.Equals("SYNC.L")) {
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "Latency")
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MTC1")) {
                        // EE> MTC1 zero, $f0
                        // cs) ee.fpr[0].f = MobUt.UL2F(ee.r0.UL[0]);
                        int fpri = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fpr"), new CodePrimitiveExpression(fpri)), "f"),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "UL2F"),
                                new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), ReUt.GPR(oal[1])), "UL"), new CodePrimitiveExpression(0))
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SD")) {
                        // EE> SD s0, $0000(sp)
                        // cs) MobUt.SD(ee, ee.s0, 0x0000 + ee.sp.UL[0]);
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SD"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("C.EQ.S")) {
                        // EE> C.EQ.S $f12, $f0
                        // cs) ee.fcr31_23 = ee.fpr[12].f == ee.fpr[0].f;
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.ValueEquality,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BC1T")) {
                        // EE> BC1T $0011b484
                        // cs) if (ee.fcr31_23) pc = 0x0011B484;
                        uint off = Convert.ToUInt32(oal[1].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DADDU")) {
                        // EE> DADDU s0, a0, zero
                        // cs) ee.s0.UD[0] = ee.a0.UD[0] + ee.r0.UD[0];
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[2]),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRud0(oal[3])
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("JAL")) {
                        // EE> JAL $0011bdc8
                        // cs) ee.ra.UL[0] = 0x0011B46C + 8; pc = 0x0011BDC8;
                        uint off = Convert.ToUInt32(oal[1].Substring(1), 16);
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refGPRul0("ra"),
                                new CodePrimitiveExpression(addr + 8)
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                );
                            fns.Add(stmt);
                        }
                    }
                    else if (opc.Equals("MOV.S")) {
                        // EE> MOV.S $f12, $f0
                        // cs) ee.fpr[12].f = ee.fpr[0].f;
                        int fd = int.Parse(oal[1].Substring(2));
                        int fs = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fd),
                            CodeUt.refFPRf(fs)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LD")) {
                        // EE> LD ra, $0008(sp)
                        // cs) MobUt.LD(ee, ee.ra, 0x0008 + ee.sp.UL0);
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LD"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LWC1")) {
                        // EE> LWC1 $f0, $0004(s1)
                        // cs) MobUt.LWC1(ee, ee.fpr[0], 4 + ee.s1.UL0);
                        int ft = int.Parse(oal[1].Substring(2));
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LWC1"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refFPR(ft),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SWC1")) {
                        // EE> SWC1 $f20, $0048(sp)
                        // cs) MobUt.SWC1(this.ee, this.ee.fpr[20], (0x0048 + this.ee.s1.UL0));
                        int ft = int.Parse(oal[1].Substring(2));
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SWC1"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refFPR(ft),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("C.LT.S")) {
                        // EE> C.LT.S $f20, $f0
                        // cs) ee.fcr31_23 = ee.fpr[20].f < ee.fpr[0].f
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.LessThan,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BC1FL")) {
                        // EE> BC1FL $0011b894
                        // cs) if (!ee.fcr31_23) pc = 0x0011b894;
                        uint off = Convert.ToUInt32(oal[1].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                                CodeBinaryOperatorType.ValueEquality,
                                new CodePrimitiveExpression(false)
                            ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("MULA.S")) {
                        // EE> MULA.S $f13, $f13
                        // cs) ee.fpracc.f = ee.fpr[13].f * ee.fpr[13].f;
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRACCf(),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.Multiply,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MADD.S")) {
                        // EE> MADD.S $f0, $f12, $f12
                        // cs) ee.fpr[0].f = ee.fpracc.f + ee.fpr[12].f * ee.fpr[12].f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MADD"),
                                CodeUt.refFPRACCf(),
                                CodeUt.refFPRf(fsi),
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SQRT.S")) {
                        // EE> SQRT.S $f0, $f0
                        // cs) ee.fpr[0].f = (float)Math.Sqrt(ee.fpr[0].f);
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeCastExpression(
                                typeof(float),
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Math)), "Sqrt"),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Math)), "Abs"),
                                        CodeUt.refFPRf(fti)
                                        )
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BC1F")) {
                        // EE> BC1F $0011ba50
                        // cs) if (!ee.fcr31_23) pc = 0x0011ba50;
                        uint off = Convert.ToUInt32(oal[1].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                                CodeBinaryOperatorType.ValueEquality,
                                new CodePrimitiveExpression(false)
                            ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("NEG.S")) {
                        // EE> NEG.S $f12, $f20
                        // cs) ee.fpr[12].f = -ee.fpr[20].f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(0),
                                CodeBinaryOperatorType.Subtract,
                                CodeUt.refFPRf(fsi)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SUB.S")) {
                        // EE> SUB.S $f12, $f1, $f12
                        // cs) ee.fpr[12].f = ee.fpr[1].f - ee.fpr[12].f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.Subtract,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("ADD.S")) {
                        // EE> ADD.S $f2, $f2, $f3
                        // cs) ee.fpr[2].f = ee.fpr[2].f + ee.fpr[3].f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LQC2")) {
                        // EE> LQC2 vf1, $0000(sp)
                        // cs) MobUt.LQC2(ee, ee.VF[1], 0x0000 + ee.sp.UL0);
                        int ft = int.Parse(oal[1].Substring(2));
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LQC2"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refVF(ft),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VMUL.")) {
                        // EE> VMUL.xyz vf1, vf1, vf2
                        // cs) ee.VF[1].x = ee.VF[1].x * ee.VF[2].x;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 5) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MUL"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, a)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc == ("VCLIPw.xyz")) {
                        // EE> vclipw.xyz vf5, vf28w
                        // untested 2013/1/2
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2).TrimEnd('w'));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "VCLIP"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refVF(fsi),
                            CodeUt.refVF(fti)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VADDx.") || opc.StartsWith("VADDy.") || opc.StartsWith("VADDz.") || opc.StartsWith("VADDw.")) {
                        // EE> VADDy.x vf1, vf1, vf1y
                        // cs) ee.VF[1].x = ee.VF[1].x + ee.VF[1].y;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[4]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Add,
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("QMFC2")) {
                        // EE> QMFC2 t0, vf1
                        // cs) MobUt.QMFC2(ee.t0, ee.VF[1]);
                        int fd = int.Parse(oal[2].Substring(2));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "QMFC2"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refVF(fd)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("C.LE.S")) {
                        // EE> C.LE.S $f1, $f0
                        // cs) ee.fcr31_23 = (ee.fpr[1].f <= ee.fpr[0].f);
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.LessThanOrEqual,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BC1TL")) {
                        uint off = Convert.ToUInt32(oal[1].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "fcr31_23"),
                                CodeBinaryOperatorType.ValueEquality,
                                new CodePrimitiveExpression(true)
                            ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("BEQ")) {
                        uint off = Convert.ToUInt32(oal[3].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[1]),
                                CodeBinaryOperatorType.ValueEquality,
                                CodeUt.refGPRud0(oal[2])
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BEQL")) {
                        uint off = Convert.ToUInt32(oal[3].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[1]),
                                CodeBinaryOperatorType.ValueEquality,
                                CodeUt.refGPRud0(oal[2])
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("SQC2")) {
                        int ft = int.Parse(oal[1].Substring(2));
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SQC2"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refVF(ft),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VSUB.")) {
                        // EE> VSUB.xyzw vf1, vf1, vf2
                        // cs) ee.VF[1].x = ee.VF[1].x - ee.VF[2].x;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 5) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Subtract,
                                        CodeUt.refVFa(fti, a)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("VSQRT")) {
                        // EE> VSQRT Q, vf2x
                        // cs) ee.Vq.f = (float)Math.Sqrt(ee.VF[2].x);
                        int fti = int.Parse(oal[2].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int ftfi = BcUt.BC2I(oal[2][oal[2].Length - 1]);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refVqf(),
                            new CodeCastExpression(
                                typeof(float),
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Math)), "Sqrt"),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Math)), "Abs"),
                                        CodeUt.refVFa(fti, ftfi)
                                    )
                                )
                            )
                        );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("CFC2")) {
                        // EE> CFC2 t0, $vi22
                        // cs) MobUt.CFC2(ee.t0, ee.VI[22]);
                        int ft = int.Parse(oal[2].Substring(3));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "CFC2"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refVI(ft)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("CTC2")) {
                        // EE> ctc2 v0, I
                        // untested 2013/1/2
                        int id = int.Parse(oal[2].Substring(3));
                        CodeExpressionStatement stmt = new CodeExpressionStatement(
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "CTC2"),
                                CodeUt.refGPR(oal[1]),
                                CodeUt.refVI(id)
                            )
                        );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VADDq.")) {
                        // EE> VADDq.x vf2, vf0, Q
                        // cs) ee.VF[2].x = ee.VF[0].x + ee.Vq.f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Add,
                                        CodeUt.refVqf()
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("VDIV")) {
                        // EE> VDIV Q, vf0w, vf2x
                        // cs) ee.Vq.f = ee.VF[0].w / ee.VF[2].x;
                        int fsi = int.Parse(oal[2].TrimEnd('x', 'y', 'z', 'w').Substring(2));
                        int fti = int.Parse(oal[3].TrimEnd('x', 'y', 'z', 'w').Substring(2));
                        int fsfi = BcUt.BC2I(oal[2][oal[2].Length - 1]);
                        int ftfi = BcUt.BC2I(oal[3][oal[3].Length - 1]);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refVqf(),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refVFa(fsi, fsfi),
                                CodeBinaryOperatorType.Divide,
                                CodeUt.refVFa(fti, ftfi)
                            )
                        );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VMULq.")) {
                        // EE> VMULq.xyz vf1, vf1, Q
                        // cs) ee.VF[1].x = ee.VF[1].x * ee.Vq.f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Multiply,
                                        CodeUt.refVqf()
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMULAx.") || opc.StartsWith("VMULAy.") || opc.StartsWith("VMULAz.") || opc.StartsWith("VMULAw.")) {
                        // EE> VMULAx.xyzw ACC, vf1, vf5x
                        // cs) ee.Vacc.x = ee.VF[1].x * ee.VF[5].x;
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[5]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVacca(a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Multiply,
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMADDAx.") || opc.StartsWith("VMADDAy.") || opc.StartsWith("VMADDAz.") || opc.StartsWith("VMADDAw.")) {
                        // EE> VMADDAy.xyzw ACC, vf2, vf5y
                        // cs) ee.Vacc.x += ee.VF[2].x * ee.VF[5].y;
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[6]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 8) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVacca(a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MADD"),
                                        CodeUt.refVacca(a),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMADDx.") || opc.StartsWith("VMADDy.") || opc.StartsWith("VMADDz.") || opc.StartsWith("VMADDw.")) {
                        // EE> VMADDw.xyzw vf5, vf4, vf5w
                        // cs) ee.VF[5].x = ee.Vacc.x + ee.VF[4].x * ee.VF[5].w;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[5]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MADD"),
                                        CodeUt.refVacca(a),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("BNEL")) {
                        // EE> BNEL s1, zero, $00124e04
                        uint off = Convert.ToUInt32(oal[3].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[1]),
                                CodeBinaryOperatorType.IdentityInequality,
                                CodeUt.refGPRud0(oal[2])
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("MSUBA.S")) {
                        // EE> MSUBA.S $f23, $f23
                        // cs) ee.fpracc.f = ee.fpracc.f - ee.fpr[23].f * ee.fpr[23].f;
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRACCf(),
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MSUB"),
                                    CodeUt.refFPRACCf(),
                                    CodeUt.refFPRf(fsi),
                                    CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MSUB.S")) {
                        // EE> MSUB.S $f1, $f22, $f22
                        // cs) ee.fpr[1].f = ee.fpracc.f - ee.fpr[22].f * ee.fpr[22].f;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRACCf(),
                                CodeBinaryOperatorType.Subtract,
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refFPRf(fsi),
                                    CodeBinaryOperatorType.Multiply,
                                    CodeUt.refFPRf(fti)
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MUL.S")) {
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.Multiply,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DIV.S")) {
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRf(fdi),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refFPRf(fsi),
                                CodeBinaryOperatorType.Divide,
                                CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SW")) {
                        // EE> SW zero, $0008(s1)
                        // cs) MobUt.SW(ee, ee.r0, 0x0008 + ee.s1.UL0);
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SW"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VOPMULA.")) {
                        // EE> VOPMULA.xyz ACC, vf1, vf2
                        // cs)  ee.Vacc.x = ee.VF[1].y * ee.VF[2].z;
                        //      ee.Vacc.y = ee.VF[1].z * ee.VF[2].x;
                        //      ee.Vacc.z = ee.VF[1].x * ee.VF[2].y;
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVacca(0),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refVFa(fsi, 1),
                                    CodeBinaryOperatorType.Multiply,
                                    CodeUt.refVFa(fti, 2)
                                    )
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVacca(1),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refVFa(fsi, 2),
                                    CodeBinaryOperatorType.Multiply,
                                    CodeUt.refVFa(fti, 0)
                                    )
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVacca(2),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refVFa(fsi, 0),
                                    CodeBinaryOperatorType.Multiply,
                                    CodeUt.refVFa(fti, 1)
                                    )
                                );
                            fns.Add(stmt);
                        }
                    }
                    else if (opc.StartsWith("VOPMSUB.")) {
                        // EE> VOPMSUB.xyz vf3xyz, vf2, vf1
                        // cs)  ee.VF[3].x = ee.Vacc.x - ee.VF[2].y * ee.VF[1].z;
                        //      ee.VF[3].y = ee.Vacc.y - ee.VF[2].z * ee.VF[1].x;
                        //      ee.VF[3].z = ee.Vacc.z - ee.VF[2].x * ee.VF[1].y;
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVFa(fdi, 0),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "OPMSUB"),
                                        CodeUt.refVacca(0),
                                        CodeUt.refVFa(fsi, 1),
                                        CodeUt.refVFa(fti, 2)
                                    )
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVFa(fdi, 1),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "OPMSUB"),
                                        CodeUt.refVacca(1),
                                        CodeUt.refVFa(fsi, 2),
                                        CodeUt.refVFa(fti, 0)
                                    )
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refVFa(fdi, 2),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "OPMSUB"),
                                        CodeUt.refVacca(2),
                                        CodeUt.refVFa(fsi, 0),
                                        CodeUt.refVFa(fti, 1)
                                    )
                                );
                            fns.Add(stmt);
                        }
                    }
                    else if (opc.Equals("ANDI")) {
                        // EE> ANDI t7, t6, $0007
                        // cs) ee.t7.UD0 = ee.t6.US[0] & 0x0007;
                        ushort val = Convert.ToUInt16(oal[3].Substring(1, 4), 16);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeCastExpression(
                                typeof(ushort),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRus0(oal[2]),
                                    CodeBinaryOperatorType.BitwiseAnd,
                                    new CodePrimitiveExpression(val)
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BNE")) {
                        // EE> BNE t7, zero, $00128254
                        uint off = Convert.ToUInt32(oal[3].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[1]),
                                CodeBinaryOperatorType.IdentityInequality,
                                CodeUt.refGPRud0(oal[2])
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSRL")) {
                        // EE> DSRL t2, t3, 9
                        // cs) MobUt.DSRL(ee.t2, ee.t3, 9);
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSRL"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSRLV")) {
                        // EE> dsrlv t7, t4, t3
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSRLV"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SB")) {
                        // EE> SB t7, $0020(sp)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SB"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLL")) {
                        // EE> SLL t7, s0, 1
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SLL"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("XORI")) {
                        // EE> xori t6, t6, $0001
                        // untested 2013/1/2
                        uint val = Convert.ToUInt32(oal[3].Substring(1), 16);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "XORI"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("ADDU")) {
                        // EE> ADDU t5, t7, sp
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodeCastExpression(
                                typeof(int),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.Add,
                                    CodeUt.refGPRsd0(oal[3])
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DADD")) {
                        // EE> dadd t3, t3, v0
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodeCastExpression(
                                typeof(Int64),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.Add,
                                    CodeUt.refGPRsd0(oal[3])
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LW")) {
                        // EE> LW t7, $0004(a0)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LW"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LWU")) {
                        // EE> lwu t0, $0038(s5)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LWU"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("JALR")) {
                        // EE> JALR ra, v0
                        // cs)  ee.ra.UD0 = 0x001206B4 + 8;
                        //      pc = ee.v0.UL0;
                        Debug.Assert(!oal[1].Equals(oal[2]), o.ToString());
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refGPRud0(oal[1]),
                                new CodePrimitiveExpression(addr + 8)
                                );
                            fns.Add(stmt);
                        }
                        if (true) {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                CodeUt.refGPRul0(oal[2])
                                );
                            fns.Add(stmt);
                        }
                    }
                    else if (opc.Equals("LHU")) {
                        // EE> LHU a2, $0010(t7)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LHU"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLT")) {
                        // EE> SLT t6, t5, a2
                        // cs) ee.t6.UD0 = Convert.ToByte(ee.t5.SD0 < ee.a2.SD0);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToByte"),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.LessThan,
                                    CodeUt.refGPRsd0(oal[3])
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LBU")) {
                        // EE> LBU t6, $0000(a2)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LBU"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLTI")) {
                        // EE> SLTI t7, s0, $0003
                        // cs) ee.t7.UD0 = Convert.ToByte(ee.s0.SD0 < 3);
                        short val = (short)Convert.ToUInt16(oal[3].Substring(1, 4), 16);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToByte"),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.LessThan,
                                    new CodePrimitiveExpression(val)
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLTIU")) {
                        // EE> SLTIU t7, t6, $002c
                        ulong val = (ulong)((short)Convert.ToUInt16(oal[3].Substring(1, 4), 16));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToByte"),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRud0(oal[2]),
                                    CodeBinaryOperatorType.LessThan,
                                    new CodePrimitiveExpression(val)
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("ORI")) {
                        // EE> ORI t7, t7, $ffff
                        ushort val = Convert.ToUInt16(oal[3].Substring(1, 4), 16);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[2]),
                                CodeBinaryOperatorType.BitwiseOr,
                                new CodePrimitiveExpression(val)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("AND")) {
                        // EE> AND t6, s4, t7
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[2]),
                                CodeBinaryOperatorType.BitwiseAnd,
                                CodeUt.refGPRud0(oal[3])
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("J")) {
                        // EE> J $002ff210
                        uint val = Convert.ToUInt32(oal[1].Substring(1), 16);
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MFC1")) {
                        // EE> MFC1 t3, $f12
                        // cs) MobUt.MFC1(ee.t3, ee.fpr[12]);
                        int fsi = int.Parse(oal[2].Substring(2));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "MFC1"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refFPR(fsi)
                        );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SH")) {
                        // EE> SH t7, $0000(a0)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SH"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("ADDI")) {
                        // EE> ADDI a1, a1, $0010
                        short val = (short)Convert.ToUInt16(oal[3].Substring(1, 4), 16);
                        CodeAssignStatement let = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodeCastExpression(
                                typeof(int),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRsd0(oal[2]),
                                    CodeBinaryOperatorType.Add,
                                    new CodePrimitiveExpression(val)
                                    )
                                )
                            );
                        fns.Add(let);
                    }
                    else if (opc.StartsWith("VSUBx.") || opc.StartsWith("VSUBy.") || opc.StartsWith("VSUBz.") || opc.StartsWith("VSUBw.")) {
                        // EE> VSUBx.z vf6, vf5, vf4x
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[4]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Subtract,
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMOVE.")) {
                        // EE> VMOVE.xyzw vf9, vf5
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    CodeUt.refVFa(fsi, a)
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMINIi.")) {
                        // EE> vminii.xyzw vf1, vf1, I
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MINI"),
                                        CodeUt.refVFa(fsi, a),
                                        new CodePropertyReferenceExpression(CodeUt.refVI(21), "f") //Vi
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("QMTC2")) {
                        // EE> QMTC2 t0, vf6
                        int fd = int.Parse(oal[2].Substring(2));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "QMTC2"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refVF(fd)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.StartsWith("VMR32.")) {
                        // EE> VMR32.xyzw vf8, vf9
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    CodeUt.refVFa(fsi, (a + 1) & 3)
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMULx.") || opc.StartsWith("VMULy.") || opc.StartsWith("VMULz.") || opc.StartsWith("VMULw.")) {
                        // EE> VMULx.x vf8, vf8, vf6x
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[4]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MUL"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMINIx.") || opc.StartsWith("VMINIy.") || opc.StartsWith("VMINIz.") || opc.StartsWith("VMINIw.")) {
                        // EE> vminiz.w vf1, vf1, vf16z
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[5]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MINI"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMAXx.") || opc.StartsWith("VMAXy.") || opc.StartsWith("VMAXz.") || opc.StartsWith("VMAXw.")) {
                        // EE> ?
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        int bci = BcUt.BC2I(opc[4]);
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MAX"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, bci)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VADD.")) {
                        // EE> VADD.xyzw vf1, vf1, vf2
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 5) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refVFa(fsi, a),
                                        CodeBinaryOperatorType.Add,
                                        CodeUt.refVFa(fti, a)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMAX.")) {
                        // EE> vmax.xyz vf5, vf1, vf2
                        // untested 2013/1/2
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 5) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MAX"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, a)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VMINI.")) {
                        // EE> vmini.xyz vf6, vf1, vf2
                        // untested 2013/1/2
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        int fti = int.Parse(oal[3].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fdi, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MINI"),
                                        CodeUt.refVFa(fsi, a),
                                        CodeUt.refVFa(fti, a)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("LH")) {
                        // EE> LH t6, $0004(a0)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LH"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLTU")) {
                        // EE> SLTU s0, zero, s0
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(Convert)), "ToByte"),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRud0(oal[2]),
                                    CodeBinaryOperatorType.LessThan,
                                    CodeUt.refGPRud0(oal[3])
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("XOR")) {
                        // EE> XOR t7, t7, t6
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "XOR"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SUBU")) {
                        // EE> SUBU t7, s2, t4
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            new CodeCastExpression(
                                typeof(int),
                                new CodeBinaryOperatorExpression(
                                    CodeUt.refGPRud0(oal[2]),
                                    CodeBinaryOperatorType.Subtract,
                                    CodeUt.refGPRud0(oal[3])
                                    )
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MULT")) {
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "MULT"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MULTU")) {
                        // EE> ?
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "MULTU"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BLTZ")) {
                        // EE> BLTZ t7, $0012d33c
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.LessThan,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BGEZ")) {
                        // EE> BGEZ t4, $0012c860
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.GreaterThanOrEqual,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BGEZL")) {
                        // EE> BGEZL t7, $0012c9dc
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.GreaterThanOrEqual,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("PCPYLD")) {
                        // EE> PCPYLD t2, t7, t5
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PCPYLD"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PCPYUD")) {
                        // EE> PCPYUD t1, t4, t6
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PCPYUD"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PEXTUW")) {
                        // EE> PEXTUW t7, t3, t2
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PEXTUW"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PEXTLW")) {
                        // EE> PEXTLW t6, t3, t2
                        // untested 2013/1/1
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PEXTLW"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSRL32")) {
                        // EE> DSRL32 t7, t5, 52
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSRL32"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSLL32")) {
                        // EE> DSLL32 t4, t4, 21
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSLL32"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BLEZ")) {
                        // EE> BLEZ v0, $00162464
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.LessThanOrEqual,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BLEZL")) {
                        // EE> blezl t3, $001f13f0
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.LessThanOrEqual,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("CVT.W.S")) {
                        // EE> CVT.W.S $f0, $f1
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "CVT_W"),
                            CodeUt.refFPR(fdi),
                            CodeUt.refFPR(fsi)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("OR")) {
                        // EE> OR s0, s0, v0
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRud0(oal[1]),
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[2]),
                                CodeBinaryOperatorType.BitwiseOr,
                                CodeUt.refGPRud0(oal[3])
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("ADD")) {
                        // EE> add t7, s4, v1
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "ADD"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MOVN")) {
                        // EE> MOVN t5, t6, t7
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRud0(oal[3]),
                                CodeBinaryOperatorType.IdentityInequality,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                CodeUt.refGPRud0(oal[1]),
                                CodeUt.refGPRud0(oal[2])
                                )
                        );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SRL")) {
                        // EE> SRL t3, s4, 16
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SRL"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SRA")) {
                        // EE> SRA t7, t7, 24
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SRA"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MADDA.S")) {
                        // EE> MADDA.S $f17, $f3
                        int fsi = int.Parse(oal[1].Substring(2));
                        int fti = int.Parse(oal[2].Substring(2));
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refFPRACCf(),
                            new CodeMethodInvokeExpression(
                                new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "MADD"),
                                    CodeUt.refFPRACCf(),
                                    CodeUt.refFPRf(fsi),
                                    CodeUt.refFPRf(fti)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BGTZ")) {
                        // EE> BGTZ s1, $003000b0
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.GreaterThan,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("BGTZL")) {
                        // EE> ?
                        // untested 2013/1/2
                        uint off = Convert.ToUInt32(oal[2].Substring(1), 16);
                        CodeConditionStatement stmt = new CodeConditionStatement(
                            new CodeBinaryOperatorExpression(
                                CodeUt.refGPRsd0(oal[1]),
                                CodeBinaryOperatorType.GreaterThan,
                                new CodePrimitiveExpression(0)
                                ),
                            new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"), "pc"),
                                new CodePrimitiveExpression(off)
                                )
                            );
                        fns.Add(stmt);
                        altSlot = stmt.TrueStatements;
                    }
                    else if (opc.Equals("CVT.S.W")) {
                        // EE> CVT.S.W $f2, $f2
                        int fdi = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "CVT_S"),
                            CodeUt.refFPR(fdi),
                            CodeUt.refFPR(fsi)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SLLV")) {
                        // EE> SLLV t6, t6, t5
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SLLV"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("LB")) {
                        // EE> LB t6, $0000(s3)
                        uint val = (uint)((short)Convert.ToUInt16(oal[2].Substring(1, 4), 16));
                        string r21 = oal[2].Substring(6, 2);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "LB"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodeBinaryOperatorExpression(
                                new CodePrimitiveExpression(val),
                                CodeBinaryOperatorType.Add,
                                CodeUt.refGPRul0(r21)
                                )
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSLLV")) {
                        // EE> DSLLV t5, t7, t3
                        // untested 2013/1/1
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSLLV"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("POR")) {
                        // EE> por v0, zero, zero
                        // untested 2013/1/1
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "POR"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PAND")) {
                        // EE> pand v0, v0, t3
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PAND"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSLL")) {
                        // EE> dsll a3, a3, 16
                        // untested 2013/1/1
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSLL"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSRA")) {
                        // EE> dsra a2, a2, 16
                        // untested 2013/1/1
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSRA"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("DSRA32")) {
                        // EE> dsra32 t2, t2, 16
                        // untested 2013/1/1
                        int val = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "DSRA32"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("SYSCALL")) {
                        // EE> syscall (00000)
                        // untested 2013/1/1
                        int val = Convert.ToInt32(oal[1].Substring(1), 16);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "SYSCALL"),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PEXTLB")) {
                        // EE> pextlb v0, zero, v0
                        // untested 2013/1/1
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PEXTLB"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PEXTLH")) {
                        // EE> pextlh v0, zero, v0
                        // untested 2013/1/1
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PEXTLH"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MFLO")) {
                        // EE> mflo a2
                        // cs) ee.a2.SD0 = ee.LO;
                        // untested 2013/1/2
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            CodeUt.refLO()
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MFHI")) {
                        // EE> mfhi a1
                        // cs) ee.a1.SD0 = ee.HI;
                        // untested 2013/1/2
                        CodeAssignStatement stmt = new CodeAssignStatement(
                            CodeUt.refGPRsd0(oal[1]),
                            CodeUt.refHI()
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MFC0")) {
                        // EE> mfc0 v1, Status
                        // untested 2013/1/2
                        int val = int.Parse(oal[2]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "MFC0"),
                            new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                            CodeUt.refGPR(oal[1]),
                            new CodePrimitiveExpression(val)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("MOVZ")) {
                        // EE> movz v1, t7, t6
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "MOVZ"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PCPYH")) {
                        // EE> pcpyh v1, t0
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PCPYH"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PADDW")) {
                        // EE> paddw v0, v0, a2
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PADDW"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PROT3W")) {
                        // EE> prot3w t6, t6
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PROT3W"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PPACW")) {
                        // EE> ppacw t4, t6, zero
                        // untested 2013/1/2
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PPACW"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PPACH")) {
                        // EE> ppach v0, zero, v0
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PPACH"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PPACB")) {
                        // EE> ppacb v0, zero, v0
                        // untested 2013/1/3
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PPACB"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            CodeUt.refGPR(oal[3])
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("PSRLW")) {
                        // EE> psrlw v0, v0, 7
                        // untested 2013/1/3
                        int sa = int.Parse(oal[3]);
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "PSRLW"),
                            CodeUt.refGPR(oal[1]),
                            CodeUt.refGPR(oal[2]),
                            new CodePrimitiveExpression(sa)
                            );
                        fns.Add(stmt);
                    }
                    else if (opc.Equals("VRXOR")) {
                        // EE> vrxor R, vf2x
                        // untested 2013/1/2
                        int fsi = int.Parse(oal[2].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        for (int a = 0; a < 4; a++) {
                            if (oal[2].IndexOf("xyzw"[a], 2) >= 0) {
                                CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "VRXOR"),
                                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                                    CodeUt.refVFa(fsi, a)
                                    );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VRNEXT.")) {
                        // EE> vrnext.x vf1, R
                        {
                            CodeExpressionStatement stmt = new CodeExpressionStatement(
                                new CodeMethodInvokeExpression(
                                    new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "VRNEXT"),
                                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                                    new CodePrimitiveExpression(true)
                                )
                            );
                            fns.Add(stmt);
                        }
                        int fti = int.Parse(oal[1].Substring(2).TrimEnd('x', 'y', 'z', 'w'));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 6) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("MobUt"), "VRNEXT"),
                                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                                        new CodePrimitiveExpression(false)
                                    )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.Equals("DIV")) {
                        // EE> div t0, a1
                        // cs) ee.LO = ee.t0.SL[0] / ee.a1.SL[0];
                        // cs) ee.HI = ee.t0.SL[0] % ee.a1.SL[0];
                        // untested 2013/1/2
                        {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refLO(),
                                new CodeCastExpression(
                                    typeof(int),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refGPRsl0(oal[1]),
                                        CodeBinaryOperatorType.Divide,
                                        CodeUt.refGPRsl0(oal[2])
                                        )
                                    )
                                );
                            fns.Add(stmt);
                        }
                        {
                            CodeAssignStatement stmt = new CodeAssignStatement(
                                CodeUt.refHI(),
                                new CodeCastExpression(
                                    typeof(int),
                                    new CodeBinaryOperatorExpression(
                                        CodeUt.refGPRsl0(oal[1]),
                                        CodeBinaryOperatorType.Modulus,
                                        CodeUt.refGPRsl0(oal[2])
                                        )
                                    )
                                );
                            fns.Add(stmt);
                        }
                    }
#if AllowCustVPU1
                    else if (opc.StartsWith("VITOF0.")) {
                        // EE> vitof0.xyzw vf31, vf31
                        // untested 2013/1/2
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("CustVPU1.UtCore1"), "ITOF0"),
                                        CodeUt.refVFa(fsi, a)
                                        )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VFTOI0.")) {
                        // EE> vftoi0.xyzw vf1, vf1
                        // untested 2013/1/2
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("CustVPU1.UtCore1"), "FTOI0"),
                                        CodeUt.refVFa(fsi, a)
                                        )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VFTOI4.")) {
                        // EE> vftoi4.xyz vf15, vf15
                        // untested 2013/1/2
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("CustVPU1.UtCore1"), "FTOI4"),
                                        CodeUt.refVFa(fsi, a)
                                        )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VITOF12.")) {
                        // EE> vitof12.xyz vf1, vf1
                        // untested 2013/1/2
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 8) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("CustVPU1.UtCore1"), "ITOF12"),
                                        CodeUt.refVFa(fsi, a)
                                        )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
                    else if (opc.StartsWith("VITOF4.")) {
                        // EE> vitof4.xy vf20, vf20
                        // untested 2013/1/2
                        int fti = int.Parse(oal[1].Substring(2));
                        int fsi = int.Parse(oal[2].Substring(2));
                        for (int a = 0; a < 4; a++) {
                            if (opc.IndexOf("xyzw"[a], 7) >= 0) {
                                CodeAssignStatement stmt = new CodeAssignStatement(
                                    CodeUt.refVFa(fti, a),
                                    new CodeMethodInvokeExpression(
                                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("CustVPU1.UtCore1"), "ITOF4"),
                                        CodeUt.refVFa(fsi, a)
                                        )
                                );
                                fns.Add(stmt);
                            }
                        }
                    }
#endif
                    else {
                        Debug.WriteLine("# " + opc);
                        throw new NotSupportedException(String.Format("Not impl {0} {1:x8}", opc, pc));
                    }
                    #endregion

                    addr += 4;
                }
                else if (cti is CodeOptiVMx16) {
                    CodeOptiVMx16 ct16 = (CodeOptiVMx16)cti;
                    if (true) {

                        EEis o = EEDisarm.parse(ct16.w0, pc);
                        fns.Add(new CodeCommentStatement(new CodeComment("@" + pc.ToString("X8") + "   " + o.ToString())));
                        addr += 4; pc = addr;
                    }
                    if (true) {
                        EEis o = EEDisarm.parse(ct16.w1, pc);
                        fns.Add(new CodeCommentStatement(new CodeComment("@" + pc.ToString("X8") + "   " + o.ToString())));
                        addr += 4; pc = addr;
                    }
                    if (true) {
                        EEis o = EEDisarm.parse(ct16.w2, pc);
                        fns.Add(new CodeCommentStatement(new CodeComment("@" + pc.ToString("X8") + "   " + o.ToString())));
                        addr += 4; pc = addr;
                    }
                    if (true) {
                        EEis o = EEDisarm.parse(ct16.w3, pc);
                        fns.Add(new CodeCommentStatement(new CodeComment("@" + pc.ToString("X8") + "   " + o.ToString())));
                        addr += 4; pc = addr;
                    }

                    if (true) {
                        CodeMethodInvokeExpression stmt = new CodeMethodInvokeExpression(
                            new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(MobUt)), "VMx16"),
                            CodeUt.refVacc(),
                            CodeUt.refVF(ct16.v1fs), CodeUt.refVFa(ct16.v1ft, ct16.v1bc),
                            CodeUt.refVF(ct16.v2fs), CodeUt.refVFa(ct16.v2ft, ct16.v2bc),
                            CodeUt.refVF(ct16.v3fs), CodeUt.refVFa(ct16.v3ft, ct16.v3bc),
                            CodeUt.refVF(ct16.v4fs), CodeUt.refVFa(ct16.v4ft, ct16.v4bc),
                            CodeUt.refVF(ct16.v4fd)
                            );
                        fns.Add(stmt);
                    }
                }
                else throw new NotSupportedException(cti.GetType().ToString());
            }

            letex.Value = addr;
        }

        class CodeOpti {
            public static CodeOpti Parse(BinaryReader br) {
                uint w0 = br.ReadUInt32();

#if UseRecOpt
                // VMULAbc: 010010 1 1111 00000 00000 00110 1111 00
                //        & 111111 1 1111 00000 00000 11111 1111 00
                if (0x4BE001BCU == (w0 & 0xFFE007FCU)) {
                    uint w1 = br.ReadUInt32();
                    // VMADDAbc: 010010 1 1111 00000 00000 00010 1111 00
                    //         & 111111 1 1111 00000 00000 11111 1111 00
                    if (0x4BE000BCU == (w1 & 0xFFE007FCU)) {
                        uint w2 = br.ReadUInt32();
                        // VMADDAbc: 010010 1 1111 00000 00000 00010 1111 00
                        //         & 111111 1 1111 00000 00000 11111 1111 00
                        if (0x4BE000BCU == (w2 & 0xFFE007FCU)) {
                            uint w3 = br.ReadUInt32();
                            // VMADDbc: 010010 1 1111 00000 00000 00000 0010 00
                            //        & 111111 1 1111 00000 00000 00000 1111 00
                            if (0x4BE00008U == (w3 & 0xFFE0003CU)) {
                                CodeOptiVMx16 o = new CodeOptiVMx16();
                                o.v1fs = (byte)((w0 >> 11) & 31);
                                o.v1ft = (byte)((w0 >> 16) & 31);
                                o.v1bc = (byte)((w0 & 3));

                                o.v2fs = (byte)((w1 >> 11) & 31);
                                o.v2ft = (byte)((w1 >> 16) & 31);
                                o.v2bc = (byte)((w1 & 3));

                                o.v3fs = (byte)((w2 >> 11) & 31);
                                o.v3ft = (byte)((w2 >> 16) & 31);
                                o.v3bc = (byte)((w2 & 3));

                                o.v4fs = (byte)((w3 >> 11) & 31);
                                o.v4ft = (byte)((w3 >> 16) & 31);
                                o.v4fd = (byte)((w3 >> 6) & 31);
                                o.v4bc = (byte)((w3 & 3));

                                o.w0 = w0;
                                o.w1 = w1;
                                o.w2 = w2;
                                o.w3 = w3;
                                return o;
                            }
                        }
                    }
                }
#endif
                return new CodeOptiOne(w0);
            }
        }
        class CodeOptiVMx16 : CodeOpti {
            public byte v1fs, v2fs, v3fs, v4fs;
            public byte v1ft, v2ft, v3ft, v4ft;
            public byte v4fd;
            public byte v1bc, v2bc, v3bc, v4bc;

            public uint w0, w1, w2, w3;
        }
        class CodeOptiOne : CodeOpti {
            public uint w0;

            public CodeOptiOne(uint w0) {
                this.w0 = w0;
            }
        }

        public static string Getflib(uint addr, string dir) {
            return Path.Combine(dir, LabUt.addr2libfn(addr));
        }

        public static string Getfcs(uint addr, string dir) {
            return Path.Combine(dir, string.Format("_{0:x8}.cs", addr));
        }

        public static void Privrec1(uint addr, Stream sieeMem, string dir) {
            CodeCompileUnit cc = new CodeCompileUnit();

            CodeNamespace cns = new CodeNamespace("ee1Dec.C");
            cc.Namespaces.Add(cns);
            cc.ReferencedAssemblies.Add("Custeera.dll");
            cns.Imports.Add(new CodeNamespaceImport("System"));
            cns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));

            CodeTypeDeclaration cls1 = new CodeTypeDeclaration("Class1");
            cls1.Attributes = MemberAttributes.Public;
            cns.Types.Add(cls1);

            CodeConstructor ctor = new CodeConstructor();
            cls1.Members.Add(ctor);
            ctor.Attributes = MemberAttributes.Public;
            ctor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(CustEE), "ee"));
            ctor.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "ee"),
                new CodeArgumentReferenceExpression("ee")
                ));

            CodeMemberField ee = new CodeMemberField(typeof(CustEE), "ee");
            cls1.Members.Add(ee);

#if InsertHere
            CodeMemberField opc = new CodeMemberField(typeof(uint), "opc");
            cls1.Members.Add(opc);
#endif

            CodeMemberMethod pfn;
            Myrec.Rec1(addr, out pfn, sieeMem);
            cls1.Members.Add(pfn);

            if (pfn.Statements.Count > 516) throw new RecProcIsTooLong(pfn.Statements.Count);

            CodeDomProvider cp = CodeDomProvider.CreateProvider("cs");
            CompilerParameters cps = new CompilerParameters();
            cps.GenerateExecutable = false;
            cps.OutputAssembly = Getflib(addr, dir);
#if EnableDebug
            cps.CompilerOptions = "/debug";
            cps.ReferencedAssemblies.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Custeera.dll"));
            cps.ReferencedAssemblies.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustVPU1.dll"));
#else
            cps.CompilerOptions = "/optimize";
#endif

#if EnableDebug
            String fpcs = Getfcs(addr, dir);
            using (StreamWriter wrcs = new StreamWriter(fpcs, false)) {
                cp.GenerateCodeFromCompileUnit(cc, wrcs, new CodeGeneratorOptions());
            }
            CompilerResults crs = cp.CompileAssemblyFromFile(cps, fpcs);
#else
            CompilerResults crs = cp.CompileAssemblyFromDom(cps, cc);
#endif

            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            StringWriter twr = new StringWriter();
            cp.GenerateCodeFromCompileUnit(cc, twr, new CodeGeneratorOptions());
            Debug.WriteLine("→→→ →→→ →→→");
            Debug.WriteLine(twr.ToString());
            Debug.WriteLine("←←← ←←← ←←←");

            foreach (string s in crs.Output) {
                Debug.WriteLine("# " + s);
            }
            Debug.WriteLine("");
        }

    }
}
