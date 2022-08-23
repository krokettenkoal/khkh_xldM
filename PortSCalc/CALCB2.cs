using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SlimDX;

// marks

//jump target unknown ... //?j

//#012bf60  LW t4, $0004(s0) !skip:012bbfc

//BEQL, likelyÇÃèÍçáÅCifÇÃíÜÇ…delaySlotï™Çä‹Çﬁ

namespace PortSCalc {
    class CALCB2 {
        int at = 0, v0 = 0, v1 = 0,
            a0 = offMset,
            a1 = offInfoTbl, a2 = 0, a3 = 0, t0 = 0, t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0, t6 = 0, t7 = 0,
            s0 = offMsetMHdr, s1 = 0, s2 = 0, s3 = 0,
            s4 = 0, s5 = 0, s6 = 0, s7 = 0, t8 = 0, t9 = 0, k0 = 0, k1 = 0,
            gp = 0,
            sp = 0x0038eca0, s8 = 0, ra = 0;

        const int offMset = 0x009f8510;
        const int offInfoTbl = 0x1abb590;
        const int offMsetMHdr = 0x009f85a0;
        const int offMdlx04 = 0x00970b80;
        const int offMdlx04MHdr = 0x00970c10;
        const int offMdlx04Vtbl = 0x354398;

        Msetacc mset;
        Mdlxacc mdlx;

        public void S_CALCB() {
            //{}S_CALCB
            //#0129a18  ADDIU sp, sp, $fd00
            sp -= 768;
            //#0129a1c  ADDIU t7, zero, $000c
            t7 = 12;
            //#0129a20  SD s0, $02a0(sp)
            int sp02a0 = s0;
            //#0129a24  SD s1, $02a8(sp)
            int sp02a8 = s1;
            //#0129a28  SD s5, $02c8(sp)
            int sp02c8 = s5;
            //#0129a2c  DADDU s0, a0, zero
            s0 = offMset;
            //#0129a30  SD s6, $02d0(sp)
            int sp02d0 = s6;
            //#0129a34  SD s2, $02b0(sp)
            int sp02b0 = s2;
            //#0129a38  DADDU s5, a1, zero
            s5 = offInfoTbl;
            //#0129a3c  SD s3, $02b8(sp)
            int sp02b8 = s3;
            //#0129a40  SD s4, $02c0(sp)
            int sp02c0 = s4;
            //#0129a44  SD s7, $02d8(sp)
            int sp02d8 = s7;
            //#0129a48  SD s8, $02e0(sp)
            int sp02e0 = s8;
            //#0129a4c  SD ra, $02e8(sp)
            int sp02e8 = ra;
            //#0129a50  SWC1 $f20, $02f0(sp)
            float sp02f0 = f20;
            //#0129a54  LW t3, $0004(a0)
            t3 = offMsetMHdr;
            //#0129a58  SW a2, $01f0(sp)
            int sp01f0 = a2;
            //#0129a5c  DADDU a0, sp, zero
            a0 = sp;
            //#0129a60  SW t3, $01fc(sp)
            int sp01fc = t3;
            //#0129a64  SW a3, $01f4(sp)
            //#0129a68  LW t4, $01fc(sp)
            t4 = sp01fc;
            //#0129a6c  LW t6, $0054(t3)
            t6 = mset.cntt3;
            //#0129a70  SW t0, $01f8(sp)
            //#0129a74  MULT t6, t6, t7
            t6 *= t7;
            //#0129a78  LW t3, $0050(t3)
            t3 = mset.offt3;
            //#0129a7c  LW t5, $0060(t4)
            t5 = mset.offt7;
            //#0129a80  ADDU s1, t4, t3
            s1 = t4 + t3;
            //#0129a84  LW t7, $0064(t4)
            t7 = mset.cntt7;
            //#0129a88  LW t3, $01fc(sp)
            t3 = sp01fc;
            //#0129a8c  ADDU t6, s1, t6
            t6 += s1;
            //#0129a90  LW t4, $001c(t4)
            t4 = mset.offt4;
            //#0129a94  SLL t7, t7, 3
            t7 <<= 3;
            //#0129a98  ADDU s6, t3, t5
            s6 = t3 + t5;
            //#0129a9c  LW t3, $00a0(t3)
            t3 = mset.vA0;
            //#0129aa0  ADDU t7, s6, t7
            t7 += s6;
            //#0129aa4  LW t5, $01fc(sp)
            t5 = sp01fc;
            //#0129aa8  SW t6, $0204(sp)
            int sp0204 = t6;
            //#0129aac  ADDU t4, t5, t4
            t4 += t5;
            //#0129ab0  SW t7, $0208(sp)
            int sp0208 = t7; //-- end of T7
            //#0129ab4  ADDU t3, t5, t3
            t3 += t5;
            //#0129ab8  SW t4, $0200(sp)
            int sp0200 = t4;
            //#0129abc  JAL $0011b420
            Trace.Assert(a0 == sp - 768);
            Matrix sp0000 = Matrix.Identity; // fn011b420();

            //#0129ac0  SW t3, $020c(sp)
            int sp020c = t3;
            //#0129ac4  SW zero, $0210(sp)
            int sp0210 = 0;
            //#0129ac8  LW t6, $01fc(sp)
            t6 = sp01fc;
            //#0129acc  LHU t7, $0012(t6)
            t7 = mset.cntt4;
            //#0129ad0  BEQL t7, zero, $0012a4c0
            if (t7 == 0) {
                //#0129ad4  LD s0, $02a0(sp)
            }
            else {
                while (true) {
                    //#0129ad8  LW t7, $0200(sp)
                    t7 = sp0200;
                    //#0129adc  LW t5, $0004(s0)
                    t5 = offMsetMHdr;
                    //#0129ae0  LW s4, $0000(t7)
                    s4 = mset.alt4[Based(t7 + 0, mset.offt4, mset.cntt4, 4)];
                    //#0129ae4  LHU t4, $0010(t5)
                    t4 = mset.cjMdlx;
                    //#0129ae8  ANDI s2, s4, $ffff
                    s2 = s4 & 0xffff;
                    //#0129aec  SLT t7, s2, t4
                    t7 = (s2 < t4) ? 1 : 0;
                    //#0129af0  BEQL t7, zero, $0012d4f4
                    if (t7 == 0) {
                        //#0129af4  LW t6, $0018(t5)
                        t6 = mset.offt5;

                        //#012d4f4  SUBU t7, s2, t4
                        t7 = s2 - t4;
                        //#012d4f8  SLL t7, t7, 6
                        t7 <<= 6;
                        //#012d4fc  ADDU t6, t5, t6
                        t6 += t5;
                        //#012d500  BEQ zero, zero, $00129b10
                        //#012d504  ADDU v0, t6, t7
                        v0 = t6 + t7;
                        {
                            //#0129b10  LH v0, $0004(v0)
                            v0 = mset.alt5[Based(v0 + 4, mset.offt5 + 4, mset.cntt5, 64)].pi;
                        }
                    }
                    else {
                        //#0129af8  LW a0, $0014(s5)
                        a0 = offMdlx04;
                        //#0129afc  LW t7, $0000(a0)
                        t7 = offMdlx04Vtbl;
                        //#0129b00  LW v0, $0020(t7)
                        //#0129b04  JALR ra, v0
                        Debug.Fail("v0 = &offMdlx.axbone");
                        //#0129b08  SLL s3, s2, 6
                        s3 = s2 << 6;
                        //#0129b0c  ADDU v0, v0, s3
                        v0 += s3;
                        {
                            //#0129b10  LH v0, $0004(v0)
                            v0 = mdlx.alaxb[Based(v0 + 4, offMdlx04 + 4, mdlx.cntAxBone, 64)].pi;
                        }
                    }

                    //#0129b14  LW t3, $0208(sp)
                    t3 = sp0208;
                    //#0129b18  BEQ s6, t3, $0012d4ec
                    //#0129b1c  SW v0, $0214(sp)
                    int sp0214 = v0;
                    if (s6 == t3) {
                        //#012d4ec  BEQ zero, zero, $00129bc4
                        //#012d4f0  SLL s8, s2, 4
                        s8 = s2 << 4;
                    }
                    else {
                        //#0129b20  ANDI t6, s2, $ffff
                        t6 = s2 & 0xffff;
                        //#0129b24  LHU t7, $0000(s6)
                        t7 = mset.alt7[Based(s6 + 0, mset.offt7, mset.cntt7, 8)].hw0;
                        //#0129b28  DADDU s3, t6, zero
                        s3 = t6;
                        //#0129b2c  BNE t7, t6, $00129bc4
                        //#0129b30  SLL s8, s2, 4
                        s8 = s2 << 4;
                        if (t7 != t6) {
                        }
                        else {
                            //#0129b34  LHU a2, $0006(s6)
                            a2 = mset.alt7[Based(s6 + 6, mset.offt7 + 6, mset.cntt7, 8)].hw6;
                            while (true) {
                                //#0129b38  DADDU a0, s0, zero
                                a0 = s0;
                                //#0129b3c  JAL $00128e40
                                //#0129b40  DADDU a1, s5, zero
                                a1 = s5;
                                fn128e40();
                                //#0129b44  LHU t4, $0002(s6)
                                //#0129b48  ANDI t6, t4, $ffff
                                //#0129b4c  SLTIU t7, t6, $0009
                                //#0129b50  BEQ t7, zero, $00129ba0
                                //#0129b54  MOV.S $f1, $f0
                                if (true) {
                                    //#0129b58  SLL t7, t6, 2
                                    //#0129b5c  LUI t6, $0038
                                    //#0129b60  ADDIU t6, t6, $8368
                                    //#0129b64  ADDU t7, t7, t6
                                    //#0129b68  LW t5, $0000(t7)
                                    //#0129b6c  JR t5
                                    //#0129b70  NOP     
                                    {
                                        //{}JT0378368(0129b74,0129b74,0129b74,012d450,012d450,012d450,012d4ac,012d4ac,012d4ac)
                                        switch (JT0378368[xxx]) {
                                            case 0x0129b74: {
                                                    //#0129b74  LW t5, $0004(s0)
                                                    //#0129b78  LHU t3, $0010(t5)
                                                    //#0129b7c  SLT t7, s2, t3
                                                    //#0129b80  BEQL t7, zero, $0012d438
                                                    if (true) {
                                                        //#0129b84  LW t6, $0018(t5)

                                                        //#012d438  SUBU t7, s2, t3
                                                        //#012d43c  SLL t7, t7, 6
                                                        //#012d440  ADDU t6, t5, t6
                                                        //#012d444  ADDU t6, t6, t7
                                                        //#012d448  BEQ zero, zero, $00129b90
                                                        //#012d44c  ADDIU t6, t6, $0010
                                                    }
                                                    else {
                                                        //#0129b88  LW t7, $001c(s5)
                                                        //#0129b8c  ADDU t6, t7, s8
                                                    }
                                                    //#0129b90  ANDI t7, t4, $ffff
                                                    fn0129b94();
                                                    break;
                                                }
                                            case 0x012d450: {
                                                    //#012d450  LW t5, $0004(s0)
                                                    //#012d454  LHU t3, $0010(t5)
                                                    //#012d458  SLT t7, s2, t3
                                                    //#012d45c  BEQL t7, zero, $0012d494
                                                    if (true) {
                                                        //#012d460  LW t6, $0018(t5)

                                                        //#012d494  SUBU t7, s2, t3
                                                        //#012d498  SLL t7, t7, 6
                                                        //#012d49c  ADDU t6, t5, t6
                                                        //#012d4a0  ADDU t6, t6, t7
                                                        //#012d4a4  BEQ zero, zero, $0012d46c
                                                        //#012d4a8  ADDIU t6, t6, $0020
                                                    }
                                                    else {
                                                        //#012d464  LW t7, $0020(s5)
                                                        //#012d468  ADDU t6, t7, s8
                                                    }
                                                    //#012d46c  ANDI t7, t4, $ffff
                                                    //#012d470  LUI t4, $0038
                                                    //#012d474  ADDIU t7, t7, $fffd
                                                    //#012d478  ADDIU t4, t4, $8360
                                                    //#012d47c  SLL t7, t7, 2
                                                    //#012d480  LWC1 $f0, $0000(t4)
                                                    //#012d484  ADDU t7, t6, t7
                                                    //#012d488  MUL.S $f0, $f1, $f0
                                                    //#012d48c  BEQ zero, zero, $00129ba0
                                                    //#012d490  SWC1 $f0, $0000(t7)
                                                    break;
                                                }
                                            case 0x012d4ac: {
                                                    //#012d4ac  LW t5, $0004(s0)
                                                    //#012d4b0  LHU t3, $0010(t5)
                                                    //#012d4b4  SLT t7, s2, t3
                                                    //#012d4b8  BEQL t7, zero, $0012d4d4
                                                    if (true) {
                                                        //#012d4bc  LW t6, $0018(t5)

                                                        //#012d4d4  SUBU t7, s2, t3
                                                        //#012d4d8  SLL t7, t7, 6
                                                        //#012d4dc  ADDU t6, t5, t6
                                                        //#012d4e0  ADDU t6, t6, t7
                                                        //#012d4e4  BEQ zero, zero, $0012d4c8
                                                        //#012d4e8  ADDIU t6, t6, $0030
                                                    }
                                                    else {
                                                        //#012d4c0  LW t7, $0024(s5)
                                                        //#012d4c4  ADDU t6, t7, s8
                                                    }
                                                    //#012d4c8  ANDI t7, t4, $ffff
                                                    //#012d4cc  BEQ zero, zero, $00129b94
                                                    //#012d4d0  ADDIU t7, t7, $fffa
                                                    fn0129b94();
                                                    break;
                                                }
                                        }
                                    }
                                }
                                //#0129ba0  LUI t7, $ffdf
                                //#0129ba4  LW t5, $0208(sp)
                                //#0129ba8  ORI t7, t7, $ffff
                                //#0129bac  ADDIU s6, s6, $0008
                                //#0129bb0  BEQ s6, t5, $00129bc4
                                //#0129bb4  AND s4, s4, t7
                                if (true) {
                                    break;
                                }
                                //#0129bb8  LHU t7, $0000(s6)
                                //#0129bbc  BEQL t7, s3, $00129b38
                                if (true) {
                                    //#0129bc0  LHU a2, $0006(s6)
                                    continue;
                                }

                                break;
                            }
                        }
                    }
                    //#0129bc4  SW zero, $0218(sp)
                    //#0129bc8  ADDIU a2, zero, $ffff
                    //#0129bcc  SW zero, $021c(sp)
                    //#0129bd0  ADDIU a3, zero, $ffff
                    //#0129bd4  LW t6, $0204(sp)
                    //#0129bd8  DADDU v0, zero, zero
                    //#0129bdc  ADDIU v1, zero, $ffff
                    //#0129be0  BEQ s1, t6, $00129c58
                    //#0129be4  DADDU t3, zero, zero
                    if (true) {
                    }
                    else {
                        //#0129be8  ANDI t6, s2, $ffff
                        //#0129bec  LHU t7, $0002(s1)
                        //#0129bf0  BNE t7, t6, $00129c58
                        //#0129bf4  DADDU t4, t6, zero
                        if (true) {
                        }
                        else {
                            //#0129bf8  LBU t7, $0001(s1)
                            while (true) {
                                //#0129bfc  BEQ t7, zero, $00129c40
                                //#0129c00  LW t7, $0204(sp)
                                if (true) {
                                }
                                else {
                                    //#0129c04  LBU t6, $0000(s1)
                                    //#0129c08  SLTIU t7, t6, $000e
                                    //#0129c0c  BEQ t7, zero, $00129c3c
                                    //#0129c10  SLL t7, t6, 2
                                    if (true) {
                                    }
                                    else {
                                        //#0129c14  LUI t6, $0038
                                        //#0129c18  ADDIU t6, t6, $838c
                                        //#0129c1c  ADDU t7, t7, t6
                                        //#0129c20  LW t5, $0000(t7)
                                        //#0129c24  JR t5
                                        //#0129c28  NOP 
                                        {
                                            //{}JT037838c(0129c2c,0129c3c,012d3c8,012d3c8,012d3c8,012d3d0,012d3e0,0129c3c,0129c3c,0129c3c,0129c3c,0129c3c,012d3e8,012d410)
                                            switch (JT037838c[xxx]) {
                                                case 0x0129c2c: {
                                                        //#0129c2c  ADDIU t7, zero, $ffff
                                                        //#0129c30  BNE a2, t7, $0012d3b4
                                                        //#0129c34  NOP 
                                                        if (true) {
                                                            //#012d3b4  BEQL a3, t7, $00129c3c
                                                            if (true) {
                                                                //#012d3b8  LHU a3, $0004(s1)
                                                                break;
                                                            }
                                                            else {
                                                                //#012d3bc  BEQ zero, zero, $00129c40
                                                                //#012d3c0  LW t7, $0204(sp) !skip:0129c3c
                                                                break;
                                                            }
                                                        }
                                                        else {
                                                            //#0129c38  LHU a2, $0004(s1)
                                                            break;
                                                        }
                                                    }
                                                case 0x0129c3c: {
                                                        break;
                                                    }
                                                case 0x012d3c8: {
                                                        //#012d3c8  BEQ zero, zero, $00129c3c
                                                        //#012d3cc  SW s1, $0218(sp)
                                                        break;
                                                    }
                                                case 0x012d3d0: {
                                                        //#012d3d0  LHU a2, $0004(s1)
                                                        //#012d3d4  SW s1, $0218(sp)
                                                        //#012d3d8  BEQ zero, zero, $00129c3c
                                                        //#012d3dc  LHU a3, $0006(s1)
                                                        break;
                                                    }
                                                case 0x012d3e0: {
                                                        //#012d3e0  BEQ zero, zero, $00129c3c
                                                        //#012d3e4  DADDU t3, s1, zero
                                                        break;
                                                    }
                                                case 0x012d3e8: {
                                                        //#012d3e8  LW t7, $01fc(sp)
                                                        //#012d3ec  LHU t5, $0006(s1)
                                                        //#012d3f0  LW t6, $005c(t7)
                                                        //#012d3f4  LHU v1, $0004(s1)
                                                        //#012d3f8  ADDIU t7, zero, $0030
                                                        //#012d3fc  MULT t5, t5, t7
                                                        //#012d400  LW t7, $01fc(sp)
                                                        //#012d404  ADDU t6, t7, t6
                                                        //#012d408  BEQ zero, zero, $00129c3c
                                                        //#012d40c  ADDU v0, t6, t5
                                                        break;
                                                    }
                                                case 0x012d410: {
                                                        //#012d410  LW t6, $01fc(sp)
                                                        //#012d414  LHU t5, $0006(s1)
                                                        //#012d418  LW t7, $005c(t6)
                                                        //#012d41c  ADDIU t6, zero, $0030
                                                        //#012d420  MULT t5, t5, t6
                                                        //#012d424  LW t6, $01fc(sp)
                                                        //#012d428  ADDU t7, t6, t7
                                                        //#012d42c  ADDU t7, t7, t5
                                                        //#012d430  BEQ zero, zero, $00129c3c
                                                        //#012d434  SW t7, $021c(sp)
                                                        break;
                                                    }
                                            }
                                        }
                                    }

                                    //#0129c3c  LW t7, $0204(sp)
                                }
                                //#0129c40  ADDIU s1, s1, $000c
                                //#0129c44  BEQ s1, t7, $00129c5c
                                //#0129c48  LUI t7, $0003   !skip:0129c58
                                if (true) {
                                }
                                else {
                                    //#0129c4c  LHU t7, $0002(s1)
                                    //#0129c50  BEQL t7, t4, $00129bfc
                                    if (true) {
                                        //#0129c54  LBU t7, $0001(s1)
                                        continue;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    //#0129c58  LUI t7, $0003
                    //#0129c5c  AND t6, s4, t7
                    //#0129c60  BEQ t6, zero, $00129cf8
                    //#0129c64  SW zero, $0220(sp)
                    if (true) {
                    }
                    else {
                        //#0129c68  LUI t7, $0080
                        //#0129c6c  AND t7, s4, t7
                        //#0129c70  BEQ t7, zero, $0012d39c
                        //#0129c74  LW t4, $020c(sp)
                        if (true) {
                            //#012d39c  LUI t7, $0001
                            //#012d3a0  ADDIU t5, s2, $0001
                            //#012d3a4  XOR t7, t6, t7
                            //#012d3a8  ADDIU t6, s2, $0002
                            //#012d3ac  BEQ zero, zero, $00129c84
                            //#012d3b0  MOVN t5, t6, t7
                        }
                        else {
                            //#0129c78  LH t5, $0000(t4)
                            //#0129c7c  ADDIU t4, t4, $0002
                            //#0129c80  SW t4, $020c(sp)
                        }
                        //#0129c84  LW t6, $0204(sp)
                        //#0129c88  BEQ s1, t6, $00129ce4
                        //#0129c8c  ADDIU t4, zero, $0005
                        if (true) {
                        }
                        else {
                            //#0129c90  LBU t7, $0001(s1)
                            for (
                                ;
                                ;
                                //#0129cd8  ADDIU s1, s1, $000c
                                //#0129cdc  BNEL s1, t7, $00129c94
                                //#0129ce0  LBU t7, $0001(s1)
                                ) {
                                //#0129c94  BEQ t7, zero, $00129cd8
                                //#0129c98  LW t7, $0204(sp)
                                if (true) {
                                    continue;
                                }
                                //#0129c9c  LHU t7, $0002(s1)
                                //#0129ca0  BNE t7, t5, $00129cd8
                                //#0129ca4  LW t7, $0204(sp)
                                if (true) {
                                    continue;
                                }
                                //#0129ca8  LBU t6, $0000(s1)
                                //#0129cac  BLTZ t6, $00129ccc
                                //#0129cb0  DADDU t7, zero, zero
                                if (true) {
                                }
                                else {
                                    //#0129cb4  SLTI t7, t6, $0002
                                    //#0129cb8  BNE t7, zero, $00129ccc
                                    //#0129cbc  ADDIU t7, zero, $0001
                                    if (true) {
                                    }
                                    else {
                                        //#0129cc0  BNE t6, t4, $00129ccc
                                        //#0129cc4  DADDU t7, zero, zero
                                        if (true) {
                                        }
                                        else {
                                            //#0129cc8  ADDIU t7, zero, $0001
                                        }
                                    }
                                }

                                //#0129ccc  ANDI t7, t7, $00ff
                                //#0129cd0  BNE t7, zero, $0012d390
                                //#0129cd4  LW t7, $0204(sp)
                                if (true) {
                                    continue;
                                }

                                //#012d390  SW s1, $0220(sp)
                                //#012d394  BEQ zero, zero, $00129ce4
                                //#012d398  ADDIU s1, s1, $000c
                                break;
                            }
                        }
                        //#0129ce4  LW t4, $0220(sp)
                        //#0129ce8  BNE t4, zero, $00129cf8
                        //#0129cec  LUI t7, $fffc
                        if (true) {
                        }
                        else {
                            //#0129cf0  ORI t7, t7, $ffff
                            //#0129cf4  AND s4, s4, t7
                        }
                    }
                    //#0129cf8  BEQ t3, zero, $0012d2c8
                    //#0129cfc  LW t7, $0214(sp)
                    if (true) {
                        //#012d2c8  BLTZ t7, $0012d33c
                        //#012d2cc  SLL t7, t7, 4
                        if (true) {
                            //#012d33c  LW t5, $0004(s0)
                            //#012d340  LW t6, $0014(s0)
                            //#012d344  LHU t3, $0010(t5)
                            //#012d348  SLT t7, s2, t3
                            //#012d34c  BEQ t7, zero, $0012d374
                            //#012d350  ADDU t4, t6, s8
                            if (true) {
                                //#012d374  LW t6, $0018(t5)
                                //#012d378  SUBU t7, s2, t3
                                //#012d37c  SLL t7, t7, 6
                                //#012d380  ADDU t6, t5, t6
                                //#012d384  ADDU t6, t6, t7
                                //#012d388  BEQ zero, zero, $0012d35c
                                //#012d38c  ADDIU t6, t6, $0010
                            }
                            else {
                                //#012d354  LW t7, $001c(s5)
                                //#012d358  ADDU t6, t7, s8
                            }
                            //#012d35c  BEQL t4, t6, $00129df8
                            if (true) {
                                //#012d360  LUI t7, $0020
                            }
                            else {
                                //#012d364  LQ t0, $0000(t6)
                                //#012d368  SQ t0, $0000(t4)
                                //#012d36c  BEQ zero, zero, $00129df8
                                //#012d370  LUI t7, $0020
                            }
                        }
                        else {
                            //#012d2d0  LW t5, $0004(s0)
                            //#012d2d4  LW t6, $0014(s0)
                            //#012d2d8  LHU t3, $0010(t5)
                            //#012d2dc  ADDU t2, t6, t7
                            //#012d2e0  SLT t7, s2, t3
                            //#012d2e4  BEQ t7, zero, $0012d320
                            //#012d2e8  ADDU t4, t6, s8
                            if (true) {
                                //#012d320  LW t6, $0018(t5)
                                //#012d324  SUBU t7, s2, t3
                                //#012d328  SLL t7, t7, 6
                                //#012d32c  ADDU t6, t5, t6
                                //#012d330  ADDU t6, t6, t7
                                //#012d334  BEQ zero, zero, $0012d2f4
                                //#012d338  ADDIU t6, t6, $0010
                            }
                            else {
                                //#012d2ec  LW t7, $001c(s5)
                                //#012d2f0  ADDU t6, t7, s8
                            }
                            //#012d2f4  ADDIU t8, sp, $0120
                            //#012d2f8  LQC2 vf1, $0000(t2)
                            //#012d2fc  LQC2 vf2, $0000(t6)
                            //#012d300  VMUL.xyzw vf1, vf1, vf2
                            //#012d304  SQC2 vf1, $0000(t8)
                            //#012d308  BEQL t4, t8, $00129df8
                            if (true) {
                                //#012d30c  LUI t7, $0020
                            }
                            else {
                                //#012d310  LQ t0, $0000(t8)
                                //#012d314  SQ t0, $0000(t4)
                                //#012d318  BEQ zero, zero, $00129df8
                                //#012d31c  LUI t7, $0020
                            }
                        }
                    }
                    else {
                        //#0129d00  LHU t7, $0004(t3)
                        //#0129d04  LW t3, $0014(s0)
                        //#0129d08  SLL t7, t7, 4
                        //#0129d0c  ADDU t6, t3, t7
                        //#0129d10  ADDU t7, t3, s8
                        //#0129d14  BEQ t7, t6, $00129d2c
                        //#0129d18  LW t5, $0214(sp)
                        if (true) {
                        }
                        else {
                            //#0129d1c  LQ t0, $0000(t6)
                            //#0129d20  SQ t0, $0000(t7)
                            //#0129d24  LW t3, $0014(s0)
                            //#0129d28  LW t5, $0214(sp)
                        }
                        //#0129d2c  BLTZ t5, $0012d27c
                        //#0129d30  LW t5, $0004(s0)
                        if (true) {
                            //#012d27c  LHU t4, $0010(t5)
                            //#012d280  SLT t7, s2, t4
                            //#012d284  BEQL t7, zero, $0012d2b0
                            if (true) {
                                //#012d288  LW t6, $0018(t5)

                                //#012d2b0  SUBU t7, s2, t4
                                //#012d2b4  SLL t7, t7, 6
                                //#012d2b8  ADDU t6, t5, t6
                                //#012d2bc  ADDU t6, t6, t7
                                //#012d2c0  BEQ zero, zero, $0012d294
                                //#012d2c4  ADDIU t7, t6, $0010
                            }
                            else {
                                //#012d28c  LW t7, $001c(s5)
                                //#012d290  ADDU t7, t7, s8
                            }
                            //#012d294  ADDU t6, t3, s8
                            //#012d298  BEQL t7, t6, $00129df8
                            if (true) {
                                //#012d29c  LUI t7, $0020
                            }
                            else {
                                //#012d2a0  LQ t0, $0000(t6)
                                //#012d2a4  SQ t0, $0000(t7)
                                //#012d2a8  BEQ zero, zero, $00129df8
                                //#012d2ac  LUI t7, $0020
                            }
                        }
                        else {
                            //#0129d34  LHU t4, $0010(t5)
                            //#0129d38  SLT t7, s2, t4
                            //#0129d3c  BEQL t7, zero, $0012d264
                            if (true) {
                                //#0129d40  LW t6, $0018(t5)

                                //#012d264  SUBU t7, s2, t4
                                //#012d268  SLL t7, t7, 6
                                //#012d26c  ADDU t6, t5, t6
                                //#012d270  ADDU t6, t6, t7
                                //#012d274  BEQ zero, zero, $00129d4c
                                //#012d278  ADDIU t5, t6, $0010
                            }
                            else {
                                //#0129d44  LW t7, $001c(s5)
                                //#0129d48  ADDU t5, t7, s8
                            }
                            //#0129d4c  LW t6, $0214(sp)
                            //#0129d50  ADDU t7, t3, s8
                            //#0129d54  LWC1 $f0, $0000(t7)
                            //#0129d58  SLL t2, t6, 4
                            //#0129d5c  ADDU t6, t3, t2
                            //#0129d60  LWC1 $f1, $0000(t6)
                            //#0129d64  NOP 
                            //#0129d68  NOP 
                            //#0129d6c  DIV.S $f0, $f0, $f1
                            //#0129d70  SWC1 $f0, $0000(t5)
                            //#0129d74  LW t5, $0004(s0)
                            //#0129d78  LHU t4, $0010(t5)
                            //#0129d7c  SLT t7, s2, t4
                            //#0129d80  BEQL t7, zero, $0012d24c
                            if (true) {
                                //#0129d84  LW t6, $0018(t5)

                                //#012d24c  SUBU t7, s2, t4
                                //#012d250  SLL t7, t7, 6
                                //#012d254  ADDU t6, t5, t6
                                //#012d258  ADDU t6, t6, t7
                                //#012d25c  BEQ zero, zero, $00129d90
                                //#012d260  ADDIU t5, t6, $0010
                            }
                            else {
                                //#0129d88  LW t7, $001c(s5)
                                //#0129d8c  ADDU t5, t7, s8
                            }
                            //#0129d90  LW t7, $0014(s0)
                            //#0129d94  ADDU t6, t7, t2
                            //#0129d98  ADDU t7, t7, s8
                            //#0129d9c  LWC1 $f1, $0004(t6)
                            //#0129da0  LWC1 $f0, $0004(t7)
                            //#0129da4  NOP 
                            //#0129da8  NOP 
                            //#0129dac  DIV.S $f0, $f0, $f1
                            //#0129db0  SWC1 $f0, $0004(t5)
                            //#0129db4  LW t5, $0004(s0)
                            //#0129db8  LHU t4, $0010(t5)
                            //#0129dbc  SLT t7, s2, t4
                            //#0129dc0  BEQL t7, zero, $0012d234
                            if (true) {
                                //#0129dc4  LW t6, $0018(t5)

                                //#012d234  SUBU t7, s2, t4
                                //#012d238  SLL t7, t7, 6
                                //#012d23c  ADDU t6, t5, t6
                                //#012d240  ADDU t6, t6, t7
                                //#012d244  BEQ zero, zero, $00129dd0
                                //#012d248  ADDIU t5, t6, $0010
                            }
                            else {
                                //#0129dc8  LW t7, $001c(s5)
                                //#0129dcc  ADDU t5, t7, s8
                            }
                            //#0129dd0  LW t7, $0014(s0)
                            //#0129dd4  ADDU t6, t7, t2
                            //#0129dd8  ADDU t7, t7, s8
                            //#0129ddc  LWC1 $f1, $0008(t6)
                            //#0129de0  LWC1 $f0, $0008(t7)
                            //#0129de4  NOP 
                            //#0129de8  NOP 
                            //#0129dec  DIV.S $f0, $f0, $f1
                            //#0129df0  SWC1 $f0, $0008(t5)
                            //#0129df4  LUI t7, $0020
                        }
                    }
                    //#0129df8  AND t7, s4, t7
                    //#0129dfc  BEQ t7, zero, $0012a9d0
                    //#0129e00  LUI t7, $0040
                    if (true) {
                        //#012a9d0  SRL t3, s4, 16
                        //#012a9d4  MTC1 zero, $f20
                        //#012a9d8  SW t3, $024c(sp)
                        //#012a9dc  ANDI t6, t3, $0003
                        //#012a9e0  SW zero, $0224(sp)
                        //#012a9e4  ADDIU t7, zero, $0001
                        //#012a9e8  SW zero, $0228(sp)
                        //#012a9ec  BEQ t6, t7, $0012caf0
                        //#012a9f0  SW zero, $022c(sp)
                        if (true) {
                            //#012caf0  LW t4, $0004(s0)
                            //#012caf4  LHU t5, $0010(t4)
                            //#012caf8  SLT t7, s2, t5
                            //#012cafc  BEQ t7, zero, $0012d218
                            //#012cb00  ADDIU t3, sp, $0090
                            if (true) {
                                //#012d218  LW t6, $0018(t4)
                                //#012d21c  SUBU t7, s2, t5
                                //#012d220  SLL t7, t7, 6
                                //#012d224  ADDU t6, t4, t6
                                //#012d228  ADDU t6, t6, t7
                                //#012d22c  BEQ zero, zero, $0012cb0c
                                //#012d230  ADDIU t6, t6, $0030
                            }
                            else {
                                //#012cb04  LW t7, $0024(s5)
                                //#012cb08  ADDU t6, t7, s8
                            }
                            //#012cb0c  BEQ t3, t6, $0012cb24
                            //#012cb10  LUI t5, $0038
                            if (true) {
                            }
                            else {
                                //#012cb14  LQ t0, $0000(t6)
                                //#012cb18  SQ t0, $0000(t3)
                                //#012cb1c  LW t4, $0004(s0)
                                //#012cb20  LUI t5, $0038
                            }
                            //#012cb24  ADDIU t5, t5, $8364
                            //#012cb28  LWC1 $f0, $0000(t5)
                            //#012cb2c  SWC1 $f0, $009c(sp)
                            //#012cb30  LHU t6, $0010(t4)
                            //#012cb34  LW t4, $0214(sp)
                            //#012cb38  SLT t7, t4, t6
                            //#012cb3c  BEQ t7, zero, $0012d200
                            //#012cb40  SLL t5, t4, 6
                            if (true) {
                                //#012d200  SUBU t7, t4, t6
                                //#012d204  LW t6, $0010(s0)
                                //#012d208  SLL t7, t7, 6
                                //#012d20c  SW t5, $0250(sp)
                                //#012d210  BEQ zero, zero, $0012cb50
                                //#012d214  ADDU t6, t6, t7
                            }
                            else {
                                //#012cb44  LW t7, $002c(s5)
                                //#012cb48  SW t5, $0250(sp)
                                //#012cb4c  ADDU t6, t7, t5
                            }
                            //#012cb50  ADDIU t8, sp, $0120
                            //#012cb54  DADDU s7, t8, zero
                            //#012cb58  LQC2 vf5, $0000(t3)
                            //#012cb5c  LQC2 vf1, $0000(t6)
                            //#012cb60  LQC2 vf2, $0010(t6)
                            //#012cb64  LQC2 vf3, $0020(t6)
                            //#012cb68  LQC2 vf4, $0030(t6)
                            //#012cb6c  VMULAx.xyzw ACC, vf1, vf5x
                            //#012cb70  VMADDAy.xyzw ACC, vf2, vf5y
                            //#012cb74  VMADDAz.xyzw ACC, vf3, vf5z
                            //#012cb78  VMADDw.xyzw vf5, vf4, vf5w
                            //#012cb7c  SQC2 vf5, $0000(t8)
                            //#012cb80  ADDIU t6, sp, $0080
                            //#012cb84  BEQL t6, t8, $0012cb98
                            if (true) {
                            }
                            else {
                                //#012cb88  LW t7, $0008(s0)

                                //#012cb8c  LQ t0, $0000(t8)
                                //#012cb90  SQ t0, $0000(t6)
                                //#012cb94  LW t7, $0008(s0)
                            }
                            //#012cb98  ADDIU s3, sp, $0040
                            //#012cb9c  LW t3, $0250(sp)
                            //#012cba0  ADDU t7, t7, t3
                            //#012cba4  BEQ s3, t7, $0012cbcc
                            //#012cba8  NOP 
                            if (true) {
                            }
                            else {
                                //#012cbac  LQ t0, $0000(t7)
                                //#012cbb0  LQ t1, $0010(t7)
                                //#012cbb4  LQ t2, $0020(t7)
                                //#012cbb8  LQ t3, $0030(t7)
                                //#012cbbc  SQ t0, $0000(s3)
                                //#012cbc0  SQ t1, $0010(s3)
                                //#012cbc4  SQ t2, $0020(s3)
                                //#012cbc8  SQ t3, $0030(s3)
                            }
                            //#012cbcc  LQ t0, $0000(s3)
                            //#012cbd0  LQ t1, $0010(s3)
                            //#012cbd4  LQ t2, $0020(s3)
                            //#012cbd8  QMFC2 t3, vf0
                            //#012cbdc  LQC2 vf4, $0030(s3)
                            //#012cbe0  PEXTLW t4, t1, t0
                            //#012cbe4  PEXTUW t5, t1, t0
                            //#012cbe8  PEXTLW t6, t3, t2
                            //#012cbec  PEXTUW t7, t3, t2
                            //#012cbf0  PCPYLD t0, t6, t4
                            //#012cbf4  PCPYUD t1, t4, t6
                            //#012cbf8  PCPYLD t2, t7, t5
                            //#012cbfc  QMTC2 t0, vf1
                            //#012cc00  QMTC2 t1, vf2
                            //#012cc04  QMTC2 t2, vf3
                            //#012cc08  VMULAx.xyz ACC, vf1, vf4x
                            //#012cc0c  VMADDAy.xyz ACC, vf2, vf4y
                            //#012cc10  VMADDz.xyz vf4, vf3, vf4z
                            //#012cc14  VSUB.xyz vf4, vf0, vf4
                            //#012cc18  SQ t0, $0000(s3)
                            //#012cc1c  SQ t1, $0010(s3)
                            //#012cc20  SQ t2, $0020(s3)
                            //#012cc24  SQC2 vf4, $0030(s3)
                            //#012cc28  ADDIU t4, sp, $0080
                            //#012cc2c  LQC2 vf5, $0000(t4)
                            //#012cc30  LQC2 vf1, $0000(s3)
                            //#012cc34  LQC2 vf2, $0010(s3)
                            //#012cc38  LQC2 vf3, $0020(s3)
                            //#012cc3c  LQC2 vf4, $0030(s3)
                            //#012cc40  VMULAx.xyzw ACC, vf1, vf5x
                            //#012cc44  VMADDAy.xyzw ACC, vf2, vf5y
                            //#012cc48  VMADDAz.xyzw ACC, vf3, vf5z
                            //#012cc4c  VMADDw.xyzw vf5, vf4, vf5w
                            //#012cc50  SQC2 vf5, $0000(t8)
                            //#012cc54  ADDIU t5, sp, $00a0
                            //#012cc58  BEQ t5, t8, $0012cc6c
                            //#012cc5c  LW a2, $0220(sp)
                            if (true) {
                            }
                            else {
                                //#012cc60  LQ t0, $0000(s7)
                                //#012cc64  SQ t0, $0000(t5)
                                //#012cc68  LW a2, $0220(sp)
                            }
                            //#012cc6c  ADDIU a3, sp, $0110
                            //#012cc70  DADDU a0, s0, zero
                            //#012cc74  DADDU a1, s5, zero
                            //#012cc78  DADDU t0, s3, zero
                            //#012cc7c  JAL $00128de8
                            //#012cc80  ADDIU t1, sp, $00b0
                            //#012cc84  LW t6, $0218(sp)
                            if (test012cc88_then_012d130_else_012cc9c()) {
                                //#012d130  LW t6, $0014(s0)
                                //#012d134  LHU t5, $0010(t4)
                                //#012d138  SLT t7, s2, t5
                                //#012d13c  BEQ t7, zero, $0012d1e4
                                //#012d140  ADDU s7, t6, s8
                                if (true) {
                                    //#012d1e4  LW t6, $0018(t4)
                                    //#012d1e8  SUBU t7, s2, t5
                                    //#012d1ec  SLL t7, t7, 6
                                    //#012d1f0  SLL s3, s2, 6
                                    //#012d1f4  ADDU t6, t4, t6
                                    //#012d1f8  BEQ zero, zero, $0012d160
                                    //#012d1fc  ADDU v0, t6, t7
                                }
                                else {
                                    //#012d144  LW a0, $0014(s5)
                                    //#012d148  LW t7, $0000(a0)
                                    //#012d14c  LW v0, $0020(t7)
                                    //#012d150  JALR ra, v0
                                    //#012d154  SLL s3, s2, 6
                                    //#012d158  LW t4, $0004(s0)
                                    //#012d15c  ADDU v0, v0, s3
                                }
                                //#012d160  LWC1 $f1, $0000(s7)
                                //#012d164  LWC1 $f0, $001c(v0)
                                //#012d168  LHU t6, $0010(t4)
                                //#012d16c  SLT t7, s2, t6
                                //#012d170  BEQ t7, zero, $0012d1d0
                                //#012d174  MUL.S $f12, $f1, $f0
                                if (true) {
                                    //#012d1d0  SUBU t7, s2, t6
                                    //#012d1d4  LW t6, $000c(s0)
                                    //#012d1d8  SLL t7, t7, 6
                                    //#012d1dc  BEQ zero, zero, $0012d180
                                    //#012d1e0  ADDU a3, t6, t7
                                }
                                else {
                                    //#012d178  LW t7, $0028(s5)
                                    //#012d17c  ADDU a3, t7, s3
                                }
                                //#012d180  LHU t5, $0010(t4)
                                //#012d184  SLT t7, s2, t5
                                //#012d188  BEQL t7, zero, $0012d1b8
                                if (true) {
                                    //#012d18c  LW t6, $0018(t4)

                                    //#012d1b8  SUBU t7, s2, t5
                                    //#012d1bc  SLL t7, t7, 6
                                    //#012d1c0  ADDU t6, t4, t6
                                    //#012d1c4  ADDU t6, t6, t7
                                    //#012d1c8  BEQ zero, zero, $0012d198
                                    //#012d1cc  ADDIU t6, t6, $0020
                                }
                                else {
                                    //#012d190  LW t7, $0020(s5)
                                    //#012d194  ADDU t6, t7, s8
                                }
                                //#012d198  LWC1 $f14, $0000(t6)
                                //#012d19c  ADDIU a0, sp, $00a0
                                //#012d1a0  MTC1 zero, $f13
                                //#012d1a4  ADDIU a1, sp, $00b0
                                //#012d1a8  DADDU a2, zero, zero
                                //#012d1ac  DADDU t0, zero, zero
                                //#012d1b0  BEQ zero, zero, $0012cd84
                                //#012d1b4  DADDU t1, zero, zero
                            }
                            else {
                                //#012cc9c  LW t7, $0218(sp)
                                //#012cca0  LW t6, $0004(s0)
                                //#012cca4  LHU t5, $0004(t7)
                                //#012cca8  LHU t6, $0010(t6)
                                //#012ccac  SLT t7, t5, t6
                                //#012ccb0  BEQ t7, zero, $0012d120
                                //#012ccb4  SUBU t7, t5, t6
                                if (true) {
                                    //#012d120  LW t6, $0010(s0)
                                    //#012d124  BEQ zero, zero, $0012ccc0
                                    //#012d128  SLL t7, t7, 6
                                }
                                else {
                                    //#012ccb8  LW t6, $002c(s5)
                                    //#012ccbc  SLL t7, t5, 6
                                }
                                //#012ccc0  ADDU t6, t6, t7
                                //#012ccc4  ADDIU t7, t6, $0030
                                //#012ccc8  LQC2 vf5, $0000(t7)
                                //#012cccc  LQC2 vf1, $0000(s3)
                                //#012ccd0  LQC2 vf2, $0010(s3)
                                //#012ccd4  LQC2 vf3, $0020(s3)
                                //#012ccd8  LQC2 vf4, $0030(s3)
                                //#012ccdc  VMULAx.xyz ACC, vf1, vf5x
                                //#012cce0  VMADDAy.xyz ACC, vf2, vf5y
                                //#012cce4  VMADDAz.xyz ACC, vf3, vf5z
                                //#012cce8  VMADDw.xyz vf5, vf4, vf0w
                                //#012ccec  VMOVE.w vf5, vf0
                                //#012ccf0  SQC2 vf5, $0000(s7)
                                //#012ccf4  LW t4, $0004(s0)
                                //#012ccf8  LW t6, $0014(s0)
                                //#012ccfc  LHU t5, $0010(t4)
                                //#012cd00  ADDU t6, t6, s8
                                //#012cd04  SLT t7, s2, t5
                                //#012cd08  BEQ t7, zero, $0012d104
                                //#012cd0c  SW t6, $0230(sp)
                                if (true) {
                                    //#012d104  LW t6, $0018(t4)
                                    //#012d108  SUBU t7, s2, t5
                                    //#012d10c  SLL t7, t7, 6
                                    //#012d110  SLL s3, s2, 6
                                    //#012d114  ADDU t6, t4, t6
                                    //#012d118  BEQ zero, zero, $0012cd2c
                                    //#012d11c  ADDU v0, t6, t7
                                }
                                else {
                                    //#012cd10  LW a0, $0014(s5)
                                    //#012cd14  LW t7, $0000(a0)
                                    //#012cd18  LW v0, $0020(t7)
                                    //#012cd1c  JALR ra, v0
                                    //#012cd20  SLL s3, s2, 6
                                    //#012cd24  LW t4, $0004(s0)
                                    //#012cd28  ADDU v0, v0, s3
                                }
                                //#012cd2c  LW t3, $0230(sp)
                                //#012cd30  LWC1 $f0, $001c(v0)
                                //#012cd34  LWC1 $f1, $0000(t3)
                                //#012cd38  LHU t6, $0010(t4)
                                //#012cd3c  SLT t7, s2, t6
                                //#012cd40  BEQ t7, zero, $0012d0f0
                                //#012cd44  MUL.S $f12, $f1, $f0
                                if (true) {
                                    //#012d0f0  SUBU t7, s2, t6
                                    //#012d0f4  LW t6, $000c(s0)
                                    //#012d0f8  SLL t7, t7, 6
                                    //#012d0fc  BEQ zero, zero, $0012cd50
                                    //#012d100  ADDU a3, t6, t7
                                }
                                else {
                                    //#012cd48  LW t7, $0028(s5)
                                    //#012cd4c  ADDU a3, t7, s3
                                }
                                //#012cd50  LHU t5, $0010(t4)
                                //#012cd54  SLT t7, s2, t5
                                //#012cd58  BEQL t7, zero, $0012d0d8
                                if (true) {
                                    //#012cd5c  LW t6, $0018(t4)

                                    //#012d0d8  SUBU t7, s2, t5
                                    //#012d0dc  SLL t7, t7, 6
                                    //#012d0e0  ADDU t6, t4, t6
                                    //#012d0e4  ADDU t6, t6, t7
                                    //#012d0e8  BEQ zero, zero, $0012cd68
                                    //#012d0ec  ADDIU t6, t6, $0020
                                }
                                else {
                                    //#012cd60  LW t7, $0020(s5)
                                    //#012cd64  ADDU t6, t7, s8
                                }
                                //#012cd68  LWC1 $f14, $0000(t6)
                                //#012cd6c  ADDIU a0, sp, $00a0
                                //#012cd70  MTC1 zero, $f13
                                //#012cd74  ADDIU a1, sp, $00b0
                                //#012cd78  DADDU t1, s7, zero
                                //#012cd7c  DADDU a2, zero, zero
                                //#012cd80  DADDU t0, zero, zero
                            }
                            //#012cd84  JAL $00124a88
                            //#012cd88  MOV.S $f15, $f13
                            //#012cd8c  LUI t7, $0040
                            //#012cd90  AND t7, s4, t7
                            //#012cd94  BEQL t7, zero, $0012cddc
                            if (true) {
                                //#012cd98  LW t7, $0004(s0)
                            }
                            else {
                                //#012cd9c  LW t4, $0004(s0)
                                //#012cda0  LHU t6, $0010(t4)
                                //#012cda4  SLT t7, s2, t6
                                //#012cda8  BEQ t7, zero, $0012d0c8
                                //#012cdac  SUBU t7, s2, t6
                                if (true) {
                                    //#012d0c8  LW t6, $000c(s0)
                                    //#012d0cc  SLL t7, t7, 6
                                    //#012d0d0  BEQ zero, zero, $0012cdb8
                                    //#012d0d4  ADDU a0, t6, t7
                                }
                                else {
                                    //#012cdb0  LW t7, $0028(s5)
                                    //#012cdb4  ADDU a0, t7, s3
                                }
                                //#012cdb8  LHU t5, $0010(t4)
                                //#012cdbc  SLT t7, s2, t5
                                //#012cdc0  BEQL t7, zero, $0012d0b0
                                if (true) {
                                    //#012cdc4  LW t6, $0018(t4)

                                    //#012d0b0  SUBU t7, s2, t5
                                    //#012d0b4  SLL t7, t7, 6
                                    //#012d0b8  ADDU t6, t4, t6
                                    //#012d0bc  ADDU t6, t6, t7
                                    //#012d0c0  BEQ zero, zero, $0012cdd0
                                    //#012d0c4  ADDIU a1, t6, $0020
                                }
                                else {
                                    //#012cdc8  LW t7, $0020(s5)
                                    //#012cdcc  ADDU a1, t7, s8
                                }
                                //#012cdd0  JAL $0011b840
                                //#012cdd4  NOP 
                                //#012cdd8  LW t7, $0004(s0)
                            }
                            //#012cddc  LHU t7, $0010(t7)
                            //#012cde0  SLT t6, s2, t7
                            //#012cde4  BEQ t6, zero, $0012d0a0
                            //#012cde8  SUBU t7, s2, t7
                            if (true) {
                                //#012d0a0  LW t6, $000c(s0)
                                //#012d0a4  SLL t7, t7, 6
                                //#012d0a8  BEQ zero, zero, $0012cdf4
                                //#012d0ac  ADDU t6, t6, t7
                            }
                            else {
                                //#012cdec  LW t7, $0028(s5)
                                //#012cdf0  ADDU t6, t7, s3
                            }
                            //#012cdf4  LWC1 $f0, $0090(sp)
                            //#012cdf8  SWC1 $f0, $0030(t6)
                            //#012cdfc  LW t7, $0004(s0)
                            //#012ce00  LW t6, $0008(s0)
                            //#012ce04  LW t4, $0250(sp)
                            //#012ce08  LHU t5, $0010(t7)
                            //#012ce0c  ADDU t3, t6, t4
                            //#012ce10  SLT t7, s2, t5
                            //#012ce14  BEQ t7, zero, $0012d08c
                            //#012ce18  ADDU t4, t6, s3
                            if (true) {
                                //#012d08c  SUBU t7, s2, t5
                                //#012d090  LW t6, $000c(s0)
                                //#012d094  SLL t7, t7, 6
                                //#012d098  BEQ zero, zero, $0012ce24
                                //#012d09c  ADDU t6, t6, t7
                            }
                            else {
                                //#012ce1c  LW t7, $0028(s5)
                                //#012ce20  ADDU t6, t7, s3
                            }
                            //#012ce24  ADDIU t5, sp, $0130
                            //#012ce28  LQC2 vf1, $0000(t3)
                            //#012ce2c  LQC2 vf2, $0010(t3)
                            //#012ce30  LQC2 vf3, $0020(t3)
                            //#012ce34  LQC2 vf4, $0030(t3)
                            //#012ce38  LQC2 vf5, $0000(t6)
                            //#012ce3c  LQC2 vf6, $0010(t6)
                            //#012ce40  LQC2 vf7, $0020(t6)
                            //#012ce44  LQC2 vf8, $0030(t6)
                            //#012ce48  VMULAx.xyzw ACC, vf1, vf5x
                            //#012ce4c  VMADDAy.xyzw ACC, vf2, vf5y
                            //#012ce50  VMADDAz.xyzw ACC, vf3, vf5z
                            //#012ce54  VMADDw.xyzw vf5, vf4, vf5w
                            //#012ce58  VMULAx.xyzw ACC, vf1, vf6x
                            //#012ce5c  VMADDAy.xyzw ACC, vf2, vf6y
                            //#012ce60  VMADDAz.xyzw ACC, vf3, vf6z
                            //#012ce64  VMADDw.xyzw vf6, vf4, vf6w
                            //#012ce68  VMULAx.xyzw ACC, vf1, vf7x
                            //#012ce6c  VMADDAy.xyzw ACC, vf2, vf7y
                            //#012ce70  VMADDAz.xyzw ACC, vf3, vf7z
                            //#012ce74  VMADDw.xyzw vf7, vf4, vf7w
                            //#012ce78  VMULAx.xyzw ACC, vf1, vf8x
                            //#012ce7c  VMADDAy.xyzw ACC, vf2, vf8y
                            //#012ce80  VMADDAz.xyzw ACC, vf3, vf8z
                            //#012ce84  VMADDw.xyzw vf8, vf4, vf8w
                            //#012ce88  SQC2 vf5, $0000(t5)
                            //#012ce8c  SQC2 vf6, $0010(t5)
                            //#012ce90  SQC2 vf7, $0020(t5)
                            //#012ce94  SQC2 vf8, $0030(t5)
                            //#012ce98  BEQL t4, t5, $0012cec4
                            if (true) {
                                //#012ce9c  LW t4, $0004(s0)
                            }
                            else {
                                //#012cea0  LQ t0, $0000(t5)
                                //#012cea4  LQ t1, $0010(t5)
                                //#012cea8  LQ t2, $0020(t5)
                                //#012ceac  LQ t3, $0030(t5)
                                //#012ceb0  SQ t0, $0000(t4)
                                //#012ceb4  SQ t1, $0010(t4)
                                //#012ceb8  SQ t2, $0020(t4)
                                //#012cebc  SQ t3, $0030(t4)
                                //#012cec0  LW t4, $0004(s0)
                            }
                            //#012cec4  LHU t6, $0010(t4)
                            //#012cec8  SLT t7, s2, t6
                            //#012cecc  BEQ t7, zero, $0012d07c
                            //#012ced0  SUBU t7, s2, t6
                            if (true) {
                                //#012d07c  LW t6, $0010(s0)
                                //#012d080  SLL t7, t7, 6
                                //#012d084  BEQ zero, zero, $0012cedc
                                //#012d088  ADDU t6, t6, t7
                            }
                            else {
                                //#012ced4  LW t7, $002c(s5)
                                //#012ced8  ADDU t6, t7, s3
                            }
                            //#012cedc  LW t7, $0008(s0)
                            //#012cee0  ADDU t7, t7, s3
                            //#012cee4  BEQL t6, t7, $0012cf14
                            if (true) {
                                //#012cee8  LHU t6, $0010(t4)
                            }
                            else {
                                //#012ceec  LQ t0, $0000(t7)
                                //#012cef0  LQ t1, $0010(t7)
                                //#012cef4  LQ t2, $0020(t7)
                                //#012cef8  LQ t3, $0030(t7)
                                //#012cefc  SQ t0, $0000(t6)
                                //#012cf00  SQ t1, $0010(t6)
                                //#012cf04  SQ t2, $0020(t6)
                                //#012cf08  SQ t3, $0030(t6)
                                //#012cf0c  LW t4, $0004(s0)
                                //#012cf10  LHU t6, $0010(t4)
                            }
                            //#012cf14  SLT t7, s2, t6
                            //#012cf18  BEQ t7, zero, $0012d06c
                            //#012cf1c  SUBU t7, s2, t6
                            if (true) {
                                //#012d06c  LW t6, $0010(s0)
                                //#012d070  SLL t7, t7, 6
                                //#012d074  BEQ zero, zero, $0012cf28
                                //#012d078  ADDU t6, t6, t7
                            }
                            else {
                                //#012cf20  LW t7, $002c(s5)
                                //#012cf24  ADDU t6, t7, s3
                            }
                            //#012cf28  LW t7, $0014(s0)
                            //#012cf2c  ADDU t7, t7, s8
                            //#012cf30  LQC2 vf4, $0000(t7)
                            //#012cf34  LQC2 vf1, $0000(t6)
                            //#012cf38  LQC2 vf2, $0010(t6)
                            //#012cf3c  LQC2 vf3, $0020(t6)
                            //#012cf40  VMULx.xyzw vf1, vf1, vf4x
                            //#012cf44  VMULy.xyzw vf2, vf2, vf4y
                            //#012cf48  VMULz.xyzw vf3, vf3, vf4z
                            //#012cf4c  SQC2 vf1, $0000(t6)
                            //#012cf50  SQC2 vf2, $0010(t6)
                            //#012cf54  SQC2 vf3, $0020(t6)
                            //#012cf58  LW t4, $0004(s0)
                            //#012cf5c  LHU t6, $0010(t4)
                            //#012cf60  SLT t7, s2, t6
                            //#012cf64  BEQ t7, zero, $0012d05c
                            //#012cf68  SUBU t7, s2, t6
                            if (true) {
                                //#012d05c  LW t6, $0010(s0)
                                //#012d060  SLL t7, t7, 6
                                //#012d064  BEQ zero, zero, $0012cf74
                                //#012d068  ADDU t6, t6, t7
                            }
                            else {
                                //#012cf6c  LW t7, $002c(s5)
                                //#012cf70  ADDU t6, t7, s3
                            }
                            //#012cf74  ADDIU t6, t6, $0030
                            //#012cf78  ADDIU t5, sp, $0080
                            //#012cf7c  BEQL t6, t5, $0012cf94
                            if (true) {
                                //#012cf80  LW t6, $0220(sp)
                            }
                            else {
                                //#012cf84  LQ t0, $0000(t5)
                                //#012cf88  SQ t0, $0000(t6)
                                //#012cf8c  LW t4, $0004(s0)
                                //#012cf90  LW t6, $0220(sp)
                            }
                            //#012cf94  LHU a2, $0002(t6)
                            //#012cf98  LHU t6, $0010(t4)
                            //#012cf9c  ANDI t5, a2, $ffff
                            //#012cfa0  SLT t7, t5, t6
                            //#012cfa4  BEQ t7, zero, $0012d050
                            //#012cfa8  SUBU t7, t5, t6
                            if (true) {
                                //#012d050  LW t6, $0010(s0)
                                //#012d054  BEQ zero, zero, $0012cfb4
                                //#012d058  SLL t7, t7, 6
                            }
                            else {
                                //#012cfac  LW t6, $002c(s5)
                                //#012cfb0  SLL t7, t5, 6
                            }
                            //#012cfb4  LHU t5, $0010(t4)
                            //#012cfb8  ADDU t6, t6, t7
                            //#012cfbc  SLT t7, s2, t5
                            //#012cfc0  BEQ t7, zero, $0012d03c
                            //#012cfc4  ADDIU t3, t6, $0030
                            if (true) {
                                //#012d03c  SUBU t7, s2, t5
                                //#012d040  LW t6, $0010(s0)
                                //#012d044  SLL t7, t7, 6
                                //#012d048  BEQ zero, zero, $0012cfd0
                                //#012d04c  ADDU v0, t6, t7
                            }
                            else {
                                //#012cfc8  LW t7, $002c(s5)
                                //#012cfcc  ADDU v0, t7, s3
                            }
                            //#012cfd0  LHU t5, $0010(t4)
                            //#012cfd4  ANDI a2, a2, $ffff
                            //#012cfd8  SLT t7, a2, t5
                            //#012cfdc  BEQ t7, zero, $0012d020
                            //#012cfe0  SLL t7, a2, 4
                            if (true) {
                                //#012d020  LW t6, $0018(t4)
                                //#012d024  SUBU t7, a2, t5
                                //#012d028  SLL t7, t7, 6
                                //#012d02c  ADDU t6, t4, t6
                                //#012d030  ADDU t6, t6, t7
                                //#012d034  BEQ zero, zero, $0012cfec
                                //#012d038  ADDIU a2, t6, $0030
                            }
                            else {
                                //#012cfe4  LW t6, $0024(s5)
                                //#012cfe8  ADDU a2, t6, t7
                            }
                            //#012cfec  LQC2 vf5, $0000(a2)
                            //#012cff0  LQC2 vf1, $0000(v0)
                            //#012cff4  LQC2 vf2, $0010(v0)
                            //#012cff8  LQC2 vf3, $0020(v0)
                            //#012cffc  LQC2 vf4, $0030(v0)
                            //#012d000  VMULAx.xyz ACC, vf1, vf5x
                            //#012d004  VMADDAy.xyz ACC, vf2, vf5y
                            //#012d008  VMADDAz.xyz ACC, vf3, vf5z
                            //#012d00c  VMADDw.xyz vf5, vf4, vf0w
                            //#012d010  VMOVE.w vf5, vf0
                            //#012d014  SQC2 vf5, $0000(t3)
                            //#012d018  BEQ zero, zero, $00129e60
                            //#012d01c  LW t3, $01f4(sp)
                        }
                        else {
                            if (test012a9f4_then_012ba08_else_012aa04()) {
                                //#012ba08  ADDIU t7, zero, $ffff
                                if (test012ba0c_then_012ba40_else_012bb34()) {
                                    //#012ba40  BGEZL t4, $0012ca88
                                    if (true) {
                                        //#012ba44  LW t6, $0004(s0)

                                        //#012ca88  ADDIU a1, sp, $0040
                                        //#012ca8c  LW t3, $0214(sp)
                                        //#012ca90  LHU t6, $0010(t6)
                                        //#012ca94  SLT t7, t3, t6
                                        //#012ca98  BEQ t7, zero, $0012cadc
                                        //#012ca9c  DADDU t5, a1, zero
                                        if (true) {
                                            //#012cadc  LW t4, $0214(sp)
                                            //#012cae0  SUBU t7, t4, t6
                                            //#012cae4  LW t6, $0010(s0)
                                            //#012cae8  BEQ zero, zero, $0012caa8
                                            //#012caec  SLL t7, t7, 6
                                        }
                                        else {
                                            //#012caa0  LW t6, $002c(s5)
                                            //#012caa4  SLL t7, t3, 6
                                        }
                                        //#012caa8  ADDU t6, t6, t7
                                        //#012caac  BEQ t5, t6, $0012ba74
                                        //#012cab0  NOP 
                                        if (true) {
                                        }
                                        else {
                                            //#012cab4  LQ t0, $0000(t6)
                                            //#012cab8  LQ t1, $0010(t6)
                                            //#012cabc  LQ t2, $0020(t6)
                                            //#012cac0  LQ t3, $0030(t6)
                                            //#012cac4  SQ t0, $0000(t5)
                                            //#012cac8  SQ t1, $0010(t5)
                                            //#012cacc  SQ t2, $0020(t5)
                                            //#012cad0  SQ t3, $0030(t5)
                                            //#012cad4  BEQ zero, zero, $0012ba74
                                            //#012cad8  NOP 
                                        }
                                    }
                                    else {
                                        //#012ba48  ADDIU a1, sp, $0040
                                        //#012ba4c  BEQ a1, sp, $0012ba74
                                        //#012ba50  NOP 
                                        //#012ba54  LQ t0, $0000(sp)
                                        //#012ba58  LQ t1, $0010(sp)
                                        //#012ba5c  LQ t2, $0020(sp)
                                        //#012ba60  LQ t3, $0030(sp)
                                        //#012ba64  SQ t0, $0000(a1)
                                        //#012ba68  SQ t1, $0010(a1)
                                        //#012ba6c  SQ t2, $0020(a1)
                                        //#012ba70  SQ t3, $0030(a1)
                                    }
                                    //#012ba74  LQC2 vf1, $0000(a1)
                                    //#012ba78  LQC2 vf2, $0010(a1)
                                    //#012ba7c  LQC2 vf3, $0020(a1)
                                    //#012ba80  LQC2 vf4, $0030(a1)
                                    //#012ba84  VOPMULA.xyz ACC, vf2, vf3
                                    //#012ba88  VOPMSUB.xyz vf5, vf3, vf2
                                    //#012ba8c  VOPMULA.xyz ACC, vf3, vf1
                                    //#012ba90  VOPMSUB.xyz vf6, vf1, vf3
                                    //#012ba94  VOPMULA.xyz ACC, vf1, vf2
                                    //#012ba98  VOPMSUB.xyz vf7, vf2, vf1
                                    //#012ba9c  VMUL.xyz vf8, vf1, vf5
                                    //#012baa0  VMUL.xyz vf1, vf4, vf5
                                    //#012baa4  VMUL.xyz vf2, vf4, vf6
                                    //#012baa8  VMUL.xyz vf3, vf4, vf7
                                    //#012baac  VADDy.x vf8, vf8, vf8y
                                    //#012bab0  VADDy.x vf1, vf1, vf1y
                                    //#012bab4  VADDx.y vf2, vf2, vf2x
                                    //#012bab8  VADDx.z vf3, vf3, vf3x
                                    //#012babc  VADDz.x vf8, vf8, vf8z
                                    //#012bac0  VADDz.x vf4, vf1, vf1z
                                    //#012bac4  VADDz.y vf4, vf2, vf2z
                                    //#012bac8  VADDy.z vf4, vf3, vf3y
                                    //#012bacc  VDIV Q, vf0w, vf8x
                                    //#012bad0  QMFC2 t0, vf5
                                    //#012bad4  QMFC2 t1, vf6
                                    //#012bad8  QMFC2 t2, vf7
                                    //#012badc  QMFC2 t3, vf0
                                    //#012bae0  PEXTLW t4, t1, t0
                                    //#012bae4  PEXTUW t5, t1, t0
                                    //#012bae8  PEXTLW t6, t3, t2
                                    //#012baec  PEXTUW t7, t3, t2
                                    //#012baf0  PCPYLD t0, t6, t4
                                    //#012baf4  PCPYUD t1, t4, t6
                                    //#012baf8  PCPYLD t2, t7, t5
                                    //#012bafc  QMTC2 t0, vf1
                                    //#012bb00  QMTC2 t1, vf2
                                    //#012bb04  QMTC2 t2, vf3
                                    //#012bb08  VSUB.xyz vf4, vf0, vf4
                                    //#012bb0c  VWAITQ 
                                    //#012bb10  VMULq.xyzw vf1, vf1, Q
                                    //#012bb14  VMULq.xyzw vf2, vf2, Q
                                    //#012bb18  VMULq.xyzw vf3, vf3, Q
                                    //#012bb1c  VMULq.xyz vf4, vf4, Q
                                    //#012bb20  SQC2 vf1, $0000(a1)
                                    //#012bb24  SQC2 vf2, $0010(a1)
                                    //#012bb28  SQC2 vf3, $0020(a1)
                                    //#012bb2c  SQC2 vf4, $0030(a1)
                                    //#012bb30  LUI t7, $0004
                                }
                                //#012bb34  AND t7, s4, t7
                                //#012bb38  BEQ t7, zero, $0012c738
                                //#012bb3c  ADDIU t7, zero, $ffff
                                if (true) {
                                    if (test012c738_then_012c74c_else_012c8d0()) {
                                        //#012c74c  ADDIU s3, sp, $0040
                                        //#012c750  SQ v1, $0270(sp)
                                        //#012c754  DADDU a0, s0, zero
                                        //#012c758  DADDU a1, s5, zero
                                        //#012c75c  ADDIU t0, sp, $0080
                                        //#012c760  DADDU t1, s3, zero
                                        //#012c764  JAL $00128c78
                                        //#012c768  ADDIU t2, sp, $0090
                                        //#012c76c  ADDIU s7, sp, $0090
                                        //#012c770  LUI t3, $0038
                                        //#012c774  LQ v0, $0260(sp)
                                        //#012c778  ADDIU t3, t3, $8364
                                        //#012c77c  LQ v1, $0270(sp)
                                        //#012c780  LWC1 $f0, $0000(t3)
                                        //#012c784  ADDIU t5, sp, $0080
                                        //#012c788  BEQ v0, zero, $0012c848
                                        //#012c78c  SWC1 $f0, $009c(sp)
                                        if (true) {
                                        }
                                        else {
                                            //#012c790  LW t7, $0004(s0)
                                            //#012c794  LHU t7, $0010(t7)
                                            //#012c798  SLT t6, v1, t7
                                            //#012c79c  BEQ t6, zero, $0012c8c4
                                            //#012c7a0  SUBU t7, v1, t7
                                            if (true) {
                                                //#012c8c4  LW t6, $0010(s0)
                                                //#012c8c8  BEQ zero, zero, $0012c7ac
                                                //#012c8cc  SLL t7, t7, 6
                                            }
                                            else {
                                                //#012c7a4  LW t6, $002c(s5)
                                                //#012c7a8  SLL t7, v1, 6
                                            }
                                            //#012c7ac  ADDU t6, t6, t7
                                            //#012c7b0  ADDIU t8, sp, $0120
                                            //#012c7b4  ADDIU t7, t6, $0030
                                            //#012c7b8  LQC2 vf5, $0000(t7)
                                            //#012c7bc  LQC2 vf1, $0000(s3)
                                            //#012c7c0  LQC2 vf2, $0010(s3)
                                            //#012c7c4  LQC2 vf3, $0020(s3)
                                            //#012c7c8  LQC2 vf4, $0030(s3)
                                            //#012c7cc  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012c7d0  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012c7d4  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012c7d8  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012c7dc  SQC2 vf5, $0000(t8)
                                            //#012c7e0  ADDIU s3, sp, $0130
                                            //#012c7e4  LQ t0, $0000(t8)
                                            //#012c7e8  SQ t0, $0000(s3)
                                            //#012c7ec  SQ t5, $0280(sp)
                                            //#012c7f0  DADDU a1, v0, zero
                                            //#012c7f4  DADDU a0, s0, zero
                                            //#012c7f8  DADDU a2, s3, zero
                                            //#012c7fc  JAL $00127e20
                                            //#012c800  DADDU a3, s7, zero
                                            //#012c804  LW t4, $0214(sp)
                                            //#012c808  BGEZ t4, $0012c860
                                            //#012c80c  LQ t5, $0280(sp)
                                            if (true) {
                                                //#012c860  LW t7, $0004(s0)
                                                //#012c864  LW t3, $0214(sp)
                                                //#012c868  LHU t7, $0010(t7)
                                                //#012c86c  SLT t6, t3, t7
                                                //#012c870  BEQ t6, zero, $0012c8b4
                                                //#012c874  LW t4, $0214(sp)
                                                if (true) {
                                                    //#012c8b4  LW t6, $0010(s0)
                                                    //#012c8b8  SUBU t7, t4, t7
                                                    //#012c8bc  BEQ zero, zero, $0012c880
                                                    //#012c8c0  SLL t7, t7, 6
                                                }
                                                else {
                                                    //#012c878  LW t6, $002c(s5)
                                                    //#012c87c  SLL t7, t3, 6
                                                }
                                                //#012c880  ADDU t6, t6, t7
                                                //#012c884  LQC2 vf5, $0000(s7)
                                                //#012c888  LQC2 vf1, $0000(t6)
                                                //#012c88c  LQC2 vf2, $0010(t6)
                                                //#012c890  LQC2 vf3, $0020(t6)
                                                //#012c894  LQC2 vf4, $0030(t6)
                                                //#012c898  VMULAx.xyzw ACC, vf1, vf5x
                                                //#012c89c  VMADDAy.xyzw ACC, vf2, vf5y
                                                //#012c8a0  VMADDAz.xyzw ACC, vf3, vf5z
                                                //#012c8a4  VMADDw.xyzw vf5, vf4, vf5w
                                                //#012c8a8  SQC2 vf5, $0000(s3)
                                                //#012c8ac  BEQ zero, zero, $0012c838
                                                //#012c8b0  NOP 
                                            }
                                            else {
                                                //#012c810  LQC2 vf5, $0000(s7)
                                                //#012c814  LQC2 vf1, $0000(sp)
                                                //#012c818  LQC2 vf2, $0010(sp)
                                                //#012c81c  LQC2 vf3, $0020(sp)
                                                //#012c820  LQC2 vf4, $0030(sp)
                                                //#012c824  VMULAx.xyzw ACC, vf1, vf5x
                                                //#012c828  VMADDAy.xyzw ACC, vf2, vf5y
                                                //#012c82c  VMADDAz.xyzw ACC, vf3, vf5z
                                                //#012c830  VMADDw.xyzw vf5, vf4, vf5w
                                                //#012c834  SQC2 vf5, $0000(s3)
                                            }
                                            //#012c838  BEQL t5, s3, $0012c84c
                                            if (true) {
                                                //#012c83c  LUI t5, $0038
                                            }
                                            else {
                                                //#012c840  LQ t0, $0000(s3)
                                                //#012c844  SQ t0, $0000(t5)
                                            }
                                        }
                                        //#012c848  LUI t5, $0038
                                        //#012c84c  SLL s3, s2, 6
                                        //#012c850  ADDIU t5, t5, $8364
                                        //#012c854  LWC1 $f0, $0000(t5)
                                        //#012c858  BEQ zero, zero, $0012bbcc
                                        //#012c85c  SWC1 $f0, $008c(sp)
                                    }
                                    else {
                                        //#012c8d0  LHU t4, $0010(t5)
                                        //#012c8d4  SLT t7, s2, t4
                                        //#012c8d8  BEQ t7, zero, $0012ca68
                                        //#012c8dc  ADDIU s3, sp, $0090
                                        if (true) {
                                            //#012ca68  LW t6, $0018(t5)
                                            //#012ca6c  SUBU t7, s2, t4
                                            //#012ca70  SLL t7, t7, 6
                                            //#012ca74  ADDU t6, t5, t6
                                            //#012ca78  ADDU t6, t6, t7
                                            //#012ca7c  BEQ zero, zero, $0012c8e8
                                            //#012ca80  ADDIU t6, t6, $0030
                                        }
                                        else {
                                            //#012c8e0  LW t7, $0024(s5)
                                            //#012c8e4  ADDU t6, t7, s8
                                        }
                                        //#012c8e8  BEQL s3, t6, $0012c8fc
                                        if (true) {
                                            //#012c8ec  LUI t6, $0038
                                        }
                                        else {
                                            //#012c8f0  LQ t0, $0000(t6)
                                            //#012c8f4  SQ t0, $0000(s3)
                                            //#012c8f8  LUI t6, $0038
                                        }
                                        //#012c8fc  ADDIU t6, t6, $8364
                                        //#012c900  LWC1 $f0, $0000(t6)
                                        //#012c904  BEQ v0, zero, $0012ca60
                                        //#012c908  SWC1 $f0, $009c(sp)
                                        if (true) {
                                            //#012ca60  BEQ zero, zero, $0012c980
                                            //#012ca64  ADDIU t8, sp, $0120
                                        }
                                        else {
                                            //#012c90c  LW t6, $0004(s0)
                                            //#012c910  LHU t6, $0010(t6)
                                            //#012c914  SLT t7, v1, t6
                                            //#012c918  BEQ t7, zero, $0012ca50
                                            //#012c91c  ADDIU t5, sp, $0040
                                            if (true) {
                                                //#012ca50  SUBU t7, v1, t6
                                                //#012ca54  LW t6, $0010(s0)
                                                //#012ca58  BEQ zero, zero, $0012c928
                                                //#012ca5c  SLL t7, t7, 6
                                            }
                                            else {
                                                //#012c920  LW t6, $002c(s5)
                                                //#012c924  SLL t7, v1, 6
                                            }
                                            //#012c928  ADDU t6, t6, t7
                                            //#012c92c  ADDIU t8, sp, $0120
                                            //#012c930  ADDIU t7, t6, $0030
                                            //#012c934  LQC2 vf5, $0000(t7)
                                            //#012c938  LQC2 vf1, $0000(t5)
                                            //#012c93c  LQC2 vf2, $0010(t5)
                                            //#012c940  LQC2 vf3, $0020(t5)
                                            //#012c944  LQC2 vf4, $0030(t5)
                                            //#012c948  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012c94c  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012c950  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012c954  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012c958  SQC2 vf5, $0000(t8)
                                            //#012c95c  ADDIU a2, sp, $0130
                                            //#012c960  LQ t0, $0000(t8)
                                            //#012c964  SQ t0, $0000(a2)
                                            //#012c968  SQ t8, $0290(sp)
                                            //#012c96c  DADDU a1, v0, zero
                                            //#012c970  DADDU a0, s0, zero
                                            //#012c974  JAL $00127e20
                                            //#012c978  DADDU a3, s3, zero
                                            //#012c97c  LQ t8, $0290(sp)
                                        }
                                        //#012c980  LW t7, $0214(sp)
                                        //#012c984  BGEZL t7, $0012c9dc
                                        if (true) {
                                            //#012c988  LW t6, $0004(s0)

                                            //#012c9dc  LW t4, $0214(sp)
                                            //#012c9e0  LHU t6, $0010(t6)
                                            //#012c9e4  SLT t7, t4, t6
                                            //#012c9e8  BEQ t7, zero, $0012ca3c
                                            //#012c9ec  ADDIU t5, sp, $0080
                                            if (true) {
                                                //#012ca3c  LW t3, $0214(sp)
                                                //#012ca40  SUBU t7, t3, t6
                                                //#012ca44  LW t6, $0010(s0)
                                                //#012ca48  BEQ zero, zero, $0012c9f8
                                                //#012ca4c  SLL t7, t7, 6
                                            }
                                            else {
                                                //#012c9f0  LW t6, $002c(s5)
                                                //#012c9f4  SLL t7, t4, 6
                                            }
                                            //#012c9f8  ADDU t6, t6, t7
                                            //#012c9fc  LQC2 vf5, $0000(s3)
                                            //#012ca00  LQC2 vf1, $0000(t6)
                                            //#012ca04  LQC2 vf2, $0010(t6)
                                            //#012ca08  LQC2 vf3, $0020(t6)
                                            //#012ca0c  LQC2 vf4, $0030(t6)
                                            //#012ca10  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012ca14  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012ca18  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012ca1c  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012ca20  SQC2 vf5, $0000(t8)
                                            //#012ca24  BEQL t5, t8, $0012c9cc
                                            //#012ca28  LUI t4, $0038
                                            //#012ca2c  LQ t0, $0000(t8)
                                            //#012ca30  SQ t0, $0000(t5)
                                            //#012ca34  BEQ zero, zero, $0012c9cc
                                            //#012ca38  LUI t4, $0038
                                        }
                                        else {
                                            //#012c98c  ADDIU t7, sp, $0080
                                            //#012c990  LQC2 vf5, $0000(s3)
                                            //#012c994  LQC2 vf1, $0000(sp)
                                            //#012c998  LQC2 vf2, $0010(sp)
                                            //#012c99c  LQC2 vf3, $0020(sp)
                                            //#012c9a0  LQC2 vf4, $0030(sp)
                                            //#012c9a4  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012c9a8  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012c9ac  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012c9b0  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012c9b4  SQC2 vf5, $0000(t8)
                                            //#012c9b8  BEQ t7, t8, $0012c9cc
                                            //#012c9bc  LUI t4, $0038
                                            if (true) {
                                            }
                                            else {
                                                //#012c9c0  LQ t0, $0000(t8)
                                                //#012c9c4  SQ t0, $0000(t7)
                                                //#012c9c8  LUI t4, $0038
                                            }
                                        }
                                        //#012c9cc  SLL s3, s2, 6
                                        //#012c9d0  ADDIU t4, t4, $8364
                                        //#012c9d4  BEQ zero, zero, $0012c858
                                        //#012c9d8  LWC1 $f0, $0000(t4)
                                        {
                                            //#012c858  BEQ zero, zero, $0012bbcc
                                            //#012c85c  SWC1 $f0, $008c(sp)
                                        }
                                    }
                                }
                                else {
                                    //#012bb40  LW t6, $0004(s0)
                                    //#012bb44  LHU t6, $0010(t6)
                                    //#012bb48  SLT t7, s2, t6
                                    //#012bb4c  BEQ t7, zero, $0012c720
                                    //#012bb50  ADDIU t5, sp, $0080
                                    if (true) {
                                        //#012c720  SUBU t7, s2, t6
                                        //#012c724  SLL s3, s2, 6
                                        //#012c728  LW t6, $0010(s0)
                                        //#012c72c  SLL t7, t7, 6
                                        //#012c730  BEQ zero, zero, $0012bb60
                                        //#012c734  ADDU t6, t6, t7
                                    }
                                    else {
                                        //#012bb54  LW t7, $002c(s5)
                                        //#012bb58  SLL s3, s2, 6
                                        //#012bb5c  ADDU t6, t7, s3
                                    }
                                    //#012bb60  ADDIU t6, t6, $0030
                                    //#012bb64  BEQL t5, t6, $0012bb78
                                    if (true) {
                                        //#012bb68  LUI t6, $0038
                                    }
                                    else {
                                        //#012bb6c  LQ t0, $0000(t6)
                                        //#012bb70  SQ t0, $0000(t5)
                                        //#012bb74  LUI t6, $0038
                                    }
                                    //#012bb78  ADDIU t8, sp, $0120
                                    //#012bb7c  ADDIU t6, t6, $8364
                                    //#012bb80  ADDIU t4, sp, $0090
                                    //#012bb84  LWC1 $f0, $0000(t6)
                                    //#012bb88  ADDIU t7, sp, $0040
                                    //#012bb8c  SWC1 $f0, $008c(sp)
                                    //#012bb90  LQC2 vf5, $0000(t5)
                                    //#012bb94  LQC2 vf1, $0000(t7)
                                    //#012bb98  LQC2 vf2, $0010(t7)
                                    //#012bb9c  LQC2 vf3, $0020(t7)
                                    //#012bba0  LQC2 vf4, $0030(t7)
                                    //#012bba4  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012bba8  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012bbac  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012bbb0  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012bbb4  SQC2 vf5, $0000(t8)
                                    //#012bbb8  BEQL t4, t8, $0012bbcc
                                    if (true) {
                                        //#012bbbc  SWC1 $f0, $009c(sp)
                                    }
                                    else {
                                        //#012bbc0  LQ t0, $0000(t8)
                                        //#012bbc4  SQ t0, $0000(t4)
                                        //#012bbc8  SWC1 $f0, $009c(sp)
                                    }
                                }
                                //#012bbcc  LW t5, $0218(sp)
                                //#012bbd0  BEQ t5, zero, $0012c3e4
                                //#012bbd4  ADDIU t7, zero, $0003
                                if (true) {
                                    //#012c3e4  LW t7, $0004(s0)
                                    //#012c3e8  LHU t7, $0010(t7)
                                    //#012c3ec  SLT t6, s2, t7
                                    //#012c3f0  BEQ t6, zero, $0012c710
                                    //#012c3f4  SUBU t7, s2, t7
                                    if (true) {
                                        //#012c710  LW t6, $000c(s0)
                                        //#012c714  SLL t7, t7, 6
                                        //#012c718  BEQ zero, zero, $0012c400
                                        //#012c71c  ADDU a0, t6, t7
                                    }
                                    else {
                                        //#012c3f8  LW t7, $0028(s5)
                                        //#012c3fc  ADDU a0, t7, s3
                                    }
                                    //#012c400  JAL $0011b420
                                    //#012c404  NOP 
                                    //#012c408  LW t7, $021c(sp)
                                    //#012c40c  BEQ t7, zero, $0012c468
                                    //#012c410  ADDIU t8, sp, $0120
                                    if (true) {
                                    }
                                    else {
                                        //#012c414  LW t5, $0004(s0)
                                        //#012c418  LHU t4, $0010(t5)
                                        //#012c41c  SLT t7, s2, t4
                                        //#012c420  BEQ t7, zero, $0012c6f4
                                        //#012c424  DADDU a2, t8, zero
                                        if (true) {
                                            //#012c6f4  LW t6, $0018(t5)
                                            //#012c6f8  SUBU t7, s2, t4
                                            //#012c6fc  SLL t7, t7, 6
                                            //#012c700  ADDU t6, t5, t6
                                            //#012c704  ADDU t6, t6, t7
                                            //#012c708  BEQ zero, zero, $0012c430
                                            //#012c70c  ADDIU t6, t6, $0020
                                        }
                                        else {
                                            //#012c428  LW t7, $0020(s5)
                                            //#012c42c  ADDU t6, t7, s8
                                        }
                                        //#012c430  LQ t0, $0000(t6)
                                        //#012c434  SQ t0, $0000(a2)
                                        //#012c438  LW t5, $0004(s0)
                                        //#012c43c  LHU t4, $0010(t5)
                                        //#012c440  SLT t7, s2, t4
                                        //#012c444  BEQL t7, zero, $0012c6dc
                                        if (true) {
                                            //#012c448  LW t6, $0018(t5)

                                            //#012c6dc  SUBU t7, s2, t4
                                            //#012c6e0  SLL t7, t7, 6
                                            //#012c6e4  ADDU t6, t5, t6
                                            //#012c6e8  ADDU t6, t6, t7
                                            //#012c6ec  BEQ zero, zero, $0012c454
                                            //#012c6f0  ADDIU a3, t6, $0020
                                        }
                                        else {
                                            //#012c44c  LW t7, $0020(s5)
                                            //#012c450  ADDU a3, t7, s8
                                        }
                                        //#012c454  SQ t8, $0290(sp)
                                        //#012c458  DADDU a0, s0, zero
                                        //#012c45c  JAL $00127e20
                                        //#012c460  LW a1, $021c(sp)
                                        //#012c464  LQ t8, $0290(sp)
                                    }
                                    //#012c468  LW t4, $0004(s0)
                                    //#012c46c  LHU t6, $0010(t4)
                                    //#012c470  SLT t7, s2, t6
                                    //#012c474  BEQ t7, zero, $0012c6cc
                                    //#012c478  SUBU t7, s2, t6
                                    if (true) {
                                        //#012c6cc  LW t6, $000c(s0)
                                        //#012c6d0  SLL t7, t7, 6
                                        //#012c6d4  BEQ zero, zero, $0012c484
                                        //#012c6d8  ADDU a0, t6, t7
                                    }
                                    else {
                                        //#012c47c  LW t7, $0028(s5)
                                        //#012c480  ADDU a0, t7, s3
                                    }
                                    //#012c484  LHU t5, $0010(t4)
                                    //#012c488  SLT t7, s2, t5
                                    //#012c48c  BEQL t7, zero, $0012c6b4
                                    if (true) {
                                        //#012c490  LW t6, $0018(t4)

                                        //#012c6b4  SUBU t7, s2, t5
                                        //#012c6b8  SLL t7, t7, 6
                                        //#012c6bc  ADDU t6, t4, t6
                                        //#012c6c0  ADDU t6, t6, t7
                                        //#012c6c4  BEQ zero, zero, $0012c49c
                                        //#012c6c8  ADDIU a1, t6, $0020
                                    }
                                    else {
                                        //#012c494  LW t7, $0020(s5)
                                        //#012c498  ADDU a1, t7, s8
                                    }
                                    //#012c49c  JAL $0011b528
                                    //#012c4a0  SQ t8, $0290(sp)
                                    //#012c4a4  LW t4, $0004(s0)
                                    //#012c4a8  LHU t6, $0010(t4)
                                    //#012c4ac  SLT t7, s2, t6
                                    //#012c4b0  BEQ t7, zero, $0012c6a0
                                    //#012c4b4  LQ t8, $0290(sp)
                                    if (true) {
                                        //#012c6a0  SUBU t7, s2, t6
                                        //#012c6a4  LW t6, $000c(s0)
                                        //#012c6a8  SLL t7, t7, 6
                                        //#012c6ac  BEQ zero, zero, $0012c4c0
                                        //#012c6b0  ADDU t6, t6, t7
                                    }
                                    else {
                                        //#012c4b8  LW t7, $0028(s5)
                                        //#012c4bc  ADDU t6, t7, s3
                                    }
                                    //#012c4c0  ADDIU t6, t6, $0030
                                    //#012c4c4  ADDIU t3, sp, $0090
                                    //#012c4c8  BEQ t6, t3, $0012c4e0
                                    //#012c4cc  LW t5, $0214(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012c4d0  LQ t0, $0000(t3)
                                        //#012c4d4  SQ t0, $0000(t6)
                                        //#012c4d8  LW t4, $0004(s0)
                                        //#012c4dc  LW t5, $0214(sp)
                                    }
                                    //#012c4e0  BLTZ t5, $0012c4f4
                                    //#012c4e4  LUI t7, $0008
                                    if (true) {
                                    }
                                    else {
                                        //#012c4e8  AND t7, s4, t7
                                        //#012c4ec  BEQ t7, zero, $0012c5c8
                                        //#012c4f0  LW t6, $0214(sp)
                                    }
                                    //#012c4f4  LHU t4, $0010(t4)
                                    //#012c4f8  DADDU t3, sp, zero
                                    //#012c4fc  LW t7, $0008(s0)
                                    //#012c500  SLT t6, s2, t4
                                    //#012c504  BEQ t6, zero, $0012c5b4
                                    //#012c508  ADDU t5, t7, s3
                                    if (true) {
                                        //#012c5b4  SUBU t7, s2, t4
                                        //#012c5b8  LW t6, $000c(s0)
                                        //#012c5bc  SLL t7, t7, 6
                                        //#012c5c0  BEQ zero, zero, $0012c514
                                        //#012c5c4  ADDU t6, t6, t7
                                    }
                                    else {
                                        //#012c50c  LW t7, $0028(s5)
                                        //#012c510  ADDU t6, t7, s3
                                    }
                                    //#012c514  LQC2 vf1, $0000(t3)
                                    //#012c518  LQC2 vf2, $0010(t3)
                                    //#012c51c  LQC2 vf3, $0020(t3)
                                    //#012c520  LQC2 vf4, $0030(t3)
                                    //#012c524  LQC2 vf5, $0000(t6)
                                    //#012c528  LQC2 vf6, $0010(t6)
                                    //#012c52c  LQC2 vf7, $0020(t6)
                                    //#012c530  LQC2 vf8, $0030(t6)
                                    //#012c534  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012c538  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012c53c  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012c540  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012c544  VMULAx.xyzw ACC, vf1, vf6x
                                    //#012c548  VMADDAy.xyzw ACC, vf2, vf6y
                                    //#012c54c  VMADDAz.xyzw ACC, vf3, vf6z
                                    //#012c550  VMADDw.xyzw vf6, vf4, vf6w
                                    //#012c554  VMULAx.xyzw ACC, vf1, vf7x
                                    //#012c558  VMADDAy.xyzw ACC, vf2, vf7y
                                    //#012c55c  VMADDAz.xyzw ACC, vf3, vf7z
                                    //#012c560  VMADDw.xyzw vf7, vf4, vf7w
                                    //#012c564  VMULAx.xyzw ACC, vf1, vf8x
                                    //#012c568  VMADDAy.xyzw ACC, vf2, vf8y
                                    //#012c56c  VMADDAz.xyzw ACC, vf3, vf8z
                                    //#012c570  VMADDw.xyzw vf8, vf4, vf8w
                                    //#012c574  SQC2 vf5, $0000(t8)
                                    //#012c578  SQC2 vf6, $0010(t8)
                                    //#012c57c  SQC2 vf7, $0020(t8)
                                    //#012c580  SQC2 vf8, $0030(t8)
                                    //#012c584  BEQL t5, t8, $0012bbfc
                                    if (true) {
                                        //#012c588  LW t5, $0008(s0)
                                    }
                                    else {
                                        //#012c58c  LQ t0, $0000(t8)
                                        //#012c590  LQ t1, $0010(t8)
                                        //#012c594  LQ t2, $0020(t8)
                                        //#012c598  LQ t3, $0030(t8)
                                        //#012c59c  SQ t0, $0000(t5)
                                        //#012c5a0  SQ t1, $0010(t5)
                                        //#012c5a4  SQ t2, $0020(t5)
                                        //#012c5a8  SQ t3, $0030(t5)
                                        //#012c5ac  BEQ zero, zero, $0012bbfc
                                        //#012c5b0  LW t5, $0008(s0)
                                    }
                                }
                                else {
                                    //#012bbd8  LBU t6, $0000(t5)
                                    //#012bbdc  ANDI t5, t6, $00ff
                                    if (test012bbe0_then_012bf98_else_012bbfc()) {
                                        //#012bf98  ANDI t6, t6, $00ff
                                        //#012bf9c  ADDIU t7, zero, $0003
                                        //#012bfa0  BNE t6, t7, $0012c3dc
                                        //#012bfa4  LW t3, $0218(sp)
                                        if (true) {
                                            //#012c3dc  BEQ zero, zero, $0012bfb0
                                            //#012c3e0  LHU t5, $0006(t3)
                                        }
                                        else {
                                            //#012bfa8  LW t7, $0218(sp)
                                            //#012bfac  LHU t5, $0004(t7)
                                        }
                                        //#012bfb0  LW t6, $0004(s0)
                                        //#012bfb4  ADDIU t4, sp, $00b0
                                        //#012bfb8  LHU t6, $0010(t6)
                                        //#012bfbc  SLT t7, t5, t6
                                        //#012bfc0  BEQ t7, zero, $0012c3cc
                                        //#012bfc4  ADDIU t3, sp, $0040
                                        if (true) {
                                            //#012c3cc  SUBU t7, t5, t6
                                            //#012c3d0  LW t6, $0010(s0)
                                            //#012c3d4  BEQ zero, zero, $0012bfd0
                                            //#012c3d8  SLL t7, t7, 6
                                        }
                                        else {
                                            //#012bfc8  LW t6, $002c(s5)
                                            //#012bfcc  SLL t7, t5, 6
                                        }
                                        //#012bfd0  ADDU t6, t6, t7
                                        //#012bfd4  ADDIU s7, sp, $0180
                                        //#012bfd8  ADDIU t7, t6, $0030
                                        //#012bfdc  LQC2 vf5, $0000(t7)
                                        //#012bfe0  LQC2 vf1, $0000(t3)
                                        //#012bfe4  LQC2 vf2, $0010(t3)
                                        //#012bfe8  LQC2 vf3, $0020(t3)
                                        //#012bfec  LQC2 vf4, $0030(t3)
                                        //#012bff0  VMULAx.xyzw ACC, vf1, vf5x
                                        //#012bff4  VMADDAy.xyzw ACC, vf2, vf5y
                                        //#012bff8  VMADDAz.xyzw ACC, vf3, vf5z
                                        //#012bffc  VMADDw.xyzw vf5, vf4, vf5w
                                        //#012c000  SQC2 vf5, $0000(s7)
                                        //#012c004  BEQ t4, s7, $0012c018
                                        //#012c008  ADDIU t8, sp, $0120
                                        if (true) {
                                        }
                                        else {
                                            //#012c00c  LQ t0, $0000(s7)
                                            //#012c010  SQ t0, $0000(t4)
                                            //#012c014  ADDIU t8, sp, $0120
                                        }
                                        //#012c018  ADDIU t5, sp, $0090
                                        //#012c01c  DADDU a0, t8, zero
                                        //#012c020  LQC2 vf1, $0000(t4)
                                        //#012c024  LQC2 vf2, $0000(t5)
                                        //#012c028  VSUB.xyzw vf1, vf1, vf2
                                        //#012c02c  SQC2 vf1, $0000(s7)
                                        //#012c030  BEQ t8, s7, $0012c044
                                        //#012c034  LUI t6, $0038
                                        if (true) {
                                        }
                                        else {

                                            //#012c038  LQ t0, $0000(s7)
                                            //#012c03c  SQ t0, $0000(t8)
                                            //#012c040  LUI t6, $0038
                                        }
                                        //#012c044  SW zero, $0130(sp)
                                        //#012c048  ADDIU t6, t6, $8364
                                        //#012c04c  SW zero, $0138(sp)
                                        //#012c050  LWC1 $f0, $0000(t6)
                                        //#012c054  SWC1 $f0, $0134(sp)
                                        //#012c058  LW t7, $0004(s0)
                                        //#012c05c  LHU t7, $0010(t7)
                                        //#012c060  SLT t6, s2, t7
                                        //#012c064  BEQ t6, zero, $0012c3bc
                                        //#012c068  SUBU t7, s2, t7
                                        if (true) {
                                            //#012c3bc  LW t6, $000c(s0)
                                            //#012c3c0  SLL t7, t7, 6
                                            //#012c3c4  BEQ zero, zero, $0012c074
                                            //#012c3c8  ADDU a2, t6, t7
                                        }
                                        else {
                                            //#012c06c  LW t7, $0028(s5)
                                            //#012c070  ADDU a2, t7, s3
                                        }
                                        //#012c074  JAL $00128b20
                                        //#012c078  ADDIU a1, sp, $0130
                                        //#012c07c  JAL $0011b420
                                        //#012c080  ADDIU a0, sp, $0140
                                        //#012c084  LW t5, $0004(s0)
                                        //#012c088  LHU t4, $0010(t5)
                                        //#012c08c  SLT t7, s2, t4
                                        //#012c090  BEQL t7, zero, $0012c3a4
                                        if (true) {
                                            //#012c094  LW t6, $0018(t5)

                                            //#012c3a4  SUBU t7, s2, t4
                                            //#012c3a8  SLL t7, t7, 6
                                            //#012c3ac  ADDU t6, t5, t6
                                            //#012c3b0  ADDU t6, t6, t7
                                            //#012c3b4  BEQ zero, zero, $0012c0a0
                                            //#012c3b8  ADDIU t6, t6, $0020
                                        }
                                        else {
                                            //#012c098  LW t7, $0020(s5)
                                            //#012c09c  ADDU t6, t7, s8
                                        }
                                        //#012c0a0  LWC1 $f12, $0000(t6)
                                        //#012c0a4  JAL $0011b450
                                        //#012c0a8  ADDIU a0, sp, $0140
                                        //#012c0ac  LW t4, $0004(s0)
                                        //#012c0b0  LHU t6, $0010(t4)
                                        //#012c0b4  SLT t7, s2, t6
                                        //#012c0b8  BEQ t7, zero, $0012c390
                                        //#012c0bc  DADDU t5, t4, zero
                                        if (true) {
                                            //#012c390  SUBU t7, s2, t6
                                            //#012c394  LW t6, $000c(s0)
                                            //#012c398  SLL t7, t7, 6
                                            //#012c39c  BEQ zero, zero, $0012c0c8
                                            //#012c3a0  ADDU t4, t6, t7
                                        }
                                        else {
                                            //#012c0c0  LW t7, $0028(s5)
                                            //#012c0c4  ADDU t4, t7, s3
                                        }
                                        //#012c0c8  LHU t6, $0010(t5)
                                        //#012c0cc  SLT t7, s2, t6
                                        //#012c0d0  BEQ t7, zero, $0012c380
                                        //#012c0d4  SUBU t7, s2, t6
                                        if (true) {
                                            //#012c380  LW t6, $000c(s0)
                                            //#012c384  SLL t7, t7, 6
                                            //#012c388  BEQ zero, zero, $0012c0e0
                                            //#012c38c  ADDU t6, t6, t7
                                        }
                                        else {
                                            //#012c0d8  LW t7, $0028(s5)
                                            //#012c0dc  ADDU t6, t7, s3
                                        }
                                        //#012c0e0  ADDIU t5, sp, $01b0
                                        //#012c0e4  ADDIU t7, sp, $0140
                                        //#012c0e8  LQC2 vf1, $0000(t6)
                                        //#012c0ec  LQC2 vf2, $0010(t6)
                                        //#012c0f0  LQC2 vf3, $0020(t6)
                                        //#012c0f4  LQC2 vf4, $0030(t6)
                                        //#012c0f8  LQC2 vf5, $0000(t7)
                                        //#012c0fc  LQC2 vf6, $0010(t7)
                                        //#012c100  LQC2 vf7, $0020(t7)
                                        //#012c104  LQC2 vf8, $0030(t7)
                                        //#012c108  VMULAx.xyzw ACC, vf1, vf5x
                                        //#012c10c  VMADDAy.xyzw ACC, vf2, vf5y
                                        //#012c110  VMADDAz.xyzw ACC, vf3, vf5z
                                        //#012c114  VMADDw.xyzw vf5, vf4, vf5w
                                        //#012c118  VMULAx.xyzw ACC, vf1, vf6x
                                        //#012c11c  VMADDAy.xyzw ACC, vf2, vf6y
                                        //#012c120  VMADDAz.xyzw ACC, vf3, vf6z
                                        //#012c124  VMADDw.xyzw vf6, vf4, vf6w
                                        //#012c128  VMULAx.xyzw ACC, vf1, vf7x
                                        //#012c12c  VMADDAy.xyzw ACC, vf2, vf7y
                                        //#012c130  VMADDAz.xyzw ACC, vf3, vf7z
                                        //#012c134  VMADDw.xyzw vf7, vf4, vf7w
                                        //#012c138  VMULAx.xyzw ACC, vf1, vf8x
                                        //#012c13c  VMADDAy.xyzw ACC, vf2, vf8y
                                        //#012c140  VMADDAz.xyzw ACC, vf3, vf8z
                                        //#012c144  VMADDw.xyzw vf8, vf4, vf8w
                                        //#012c148  SQC2 vf5, $0000(t5)
                                        //#012c14c  SQC2 vf6, $0010(t5)
                                        //#012c150  SQC2 vf7, $0020(t5)
                                        //#012c154  SQC2 vf8, $0030(t5)
                                        //#012c158  BEQL t4, t5, $0012c184
                                        if (true) {
                                            //#012c15c  LW t4, $0004(s0)
                                        }
                                        else {
                                            //#012c160  LQ t0, $0000(t5)
                                            //#012c164  LQ t1, $0010(t5)
                                            //#012c168  LQ t2, $0020(t5)
                                            //#012c16c  LQ t3, $0030(t5)
                                            //#012c170  SQ t0, $0000(t4)
                                            //#012c174  SQ t1, $0010(t4)
                                            //#012c178  SQ t2, $0020(t4)
                                            //#012c17c  SQ t3, $0030(t4)
                                            //#012c180  LW t4, $0004(s0)
                                        }
                                        //#012c184  LHU t6, $0010(t4)
                                        //#012c188  SLT t7, s2, t6
                                        //#012c18c  BEQ t7, zero, $0012c370
                                        //#012c190  SUBU t7, s2, t6
                                        if (true) {
                                            //#012c370  LW t6, $000c(s0)
                                            //#012c374  SLL t7, t7, 6
                                            //#012c378  BEQ zero, zero, $0012c19c
                                            //#012c37c  ADDU t6, t6, t7
                                        }
                                        else {
                                            //#012c194  LW t7, $0028(s5)
                                            //#012c198  ADDU t6, t7, s3
                                        }
                                        //#012c19c  ADDIU t6, t6, $0030
                                        //#012c1a0  ADDIU t3, sp, $0090
                                        //#012c1a4  BEQ t6, t3, $0012c1bc
                                        //#012c1a8  LW t5, $0214(sp)
                                        if (true) {
                                            //#012c1ac  LQ t0, $0000(t3)
                                            //#012c1b0  SQ t0, $0000(t6)
                                            //#012c1b4  LW t4, $0004(s0)
                                            //#012c1b8  LW t5, $0214(sp)
                                        }
                                        //#012c1bc  BGEZ t5, $0012c298
                                        //#012c1c0  LW t6, $0214(sp)
                                        if (true) {
                                            //#012c298  LHU t3, $0010(t4)
                                            //#012c29c  SLL t7, t6, 6
                                            //#012c2a0  LW t6, $0008(s0)
                                            //#012c2a4  SLT t5, s2, t3
                                            //#012c2a8  ADDU t2, t6, t7
                                            //#012c2ac  BEQ t5, zero, $0012c35c
                                            //#012c2b0  ADDU t4, t6, s3
                                            if (true) {
                                                //#012c35c  SUBU t7, s2, t3
                                                //#012c360  LW t6, $000c(s0)
                                                //#012c364  SLL t7, t7, 6
                                                //#012c368  BEQ zero, zero, $0012c2bc
                                                //#012c36c  ADDU t6, t6, t7
                                            }
                                            else {
                                                //#012c2b4  LW t7, $0028(s5)
                                                //#012c2b8  ADDU t6, t7, s3
                                            }
                                            //#012c2bc  LQC2 vf1, $0000(t2)
                                            //#012c2c0  LQC2 vf2, $0010(t2)
                                            //#012c2c4  LQC2 vf3, $0020(t2)
                                            //#012c2c8  LQC2 vf4, $0030(t2)
                                            //#012c2cc  LQC2 vf5, $0000(t6)
                                            //#012c2d0  LQC2 vf6, $0010(t6)
                                            //#012c2d4  LQC2 vf7, $0020(t6)
                                            //#012c2d8  LQC2 vf8, $0030(t6)
                                            //#012c2dc  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012c2e0  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012c2e4  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012c2e8  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012c2ec  VMULAx.xyzw ACC, vf1, vf6x
                                            //#012c2f0  VMADDAy.xyzw ACC, vf2, vf6y
                                            //#012c2f4  VMADDAz.xyzw ACC, vf3, vf6z
                                            //#012c2f8  VMADDw.xyzw vf6, vf4, vf6w
                                            //#012c2fc  VMULAx.xyzw ACC, vf1, vf7x
                                            //#012c300  VMADDAy.xyzw ACC, vf2, vf7y
                                            //#012c304  VMADDAz.xyzw ACC, vf3, vf7z
                                            //#012c308  VMADDw.xyzw vf7, vf4, vf7w
                                            //#012c30c  VMULAx.xyzw ACC, vf1, vf8x
                                            //#012c310  VMADDAy.xyzw ACC, vf2, vf8y
                                            //#012c314  VMADDAz.xyzw ACC, vf3, vf8z
                                            //#012c318  VMADDw.xyzw vf8, vf4, vf8w
                                            //#012c31c  SQC2 vf5, $0000(s7)
                                            //#012c320  SQC2 vf6, $0010(s7)
                                            //#012c324  SQC2 vf7, $0020(s7)
                                            //#012c328  SQC2 vf8, $0030(s7)
                                            //#012c32c  BEQL t4, s7, $0012bbfc
                                            if (true) {
                                                //#012c330  LW t5, $0008(s0)
                                            }
                                            else {
                                                //#012c334  LQ t0, $0000(s7)
                                                //#012c338  LQ t1, $0010(s7)
                                                //#012c33c  LQ t2, $0020(s7)
                                                //#012c340  LQ t3, $0030(s7)
                                                //#012c344  SQ t0, $0000(t4)
                                                //#012c348  SQ t1, $0010(t4)
                                                //#012c34c  SQ t2, $0020(t4)
                                                //#012c350  SQ t3, $0030(t4)
                                                //#012c354  BEQ zero, zero, $0012bbfc
                                                //#012c358  LW t5, $0008(s0)
                                            }
                                        }
                                        else {
                                            //#012c1c4  LHU t4, $0010(t4)
                                            //#012c1c8  DADDU t3, sp, zero
                                            //#012c1cc  LW t7, $0008(s0)
                                            //#012c1d0  SLT t6, s2, t4
                                            //#012c1d4  BEQ t6, zero, $0012c284
                                            //#012c1d8  ADDU t5, t7, s3
                                            if (true) {
                                                //#012c284  SUBU t7, s2, t4
                                                //#012c288  LW t6, $000c(s0)
                                                //#012c28c  SLL t7, t7, 6
                                                //#012c290  BEQ zero, zero, $0012c1e4
                                                //#012c294  ADDU t6, t6, t7
                                            }
                                            else {
                                                //#012c1dc  LW t7, $0028(s5)
                                                //#012c1e0  ADDU t6, t7, s3
                                            }
                                            //#012c1e4  LQC2 vf1, $0000(t3)
                                            //#012c1e8  LQC2 vf2, $0010(t3)
                                            //#012c1ec  LQC2 vf3, $0020(t3)
                                            //#012c1f0  LQC2 vf4, $0030(t3)
                                            //#012c1f4  LQC2 vf5, $0000(t6)
                                            //#012c1f8  LQC2 vf6, $0010(t6)
                                            //#012c1fc  LQC2 vf7, $0020(t6)
                                            //#012c200  LQC2 vf8, $0030(t6)
                                            //#012c204  VMULAx.xyzw ACC, vf1, vf5x
                                            //#012c208  VMADDAy.xyzw ACC, vf2, vf5y
                                            //#012c20c  VMADDAz.xyzw ACC, vf3, vf5z
                                            //#012c210  VMADDw.xyzw vf5, vf4, vf5w
                                            //#012c214  VMULAx.xyzw ACC, vf1, vf6x
                                            //#012c218  VMADDAy.xyzw ACC, vf2, vf6y
                                            //#012c21c  VMADDAz.xyzw ACC, vf3, vf6z
                                            //#012c220  VMADDw.xyzw vf6, vf4, vf6w
                                            //#012c224  VMULAx.xyzw ACC, vf1, vf7x
                                            //#012c228  VMADDAy.xyzw ACC, vf2, vf7y
                                            //#012c22c  VMADDAz.xyzw ACC, vf3, vf7z
                                            //#012c230  VMADDw.xyzw vf7, vf4, vf7w
                                            //#012c234  VMULAx.xyzw ACC, vf1, vf8x
                                            //#012c238  VMADDAy.xyzw ACC, vf2, vf8y
                                            //#012c23c  VMADDAz.xyzw ACC, vf3, vf8z
                                            //#012c240  VMADDw.xyzw vf8, vf4, vf8w
                                            //#012c244  SQC2 vf5, $0000(s7)
                                            //#012c248  SQC2 vf6, $0010(s7)
                                            //#012c24c  SQC2 vf7, $0020(s7)
                                            //#012c250  SQC2 vf8, $0030(s7)
                                            //#012c254  BEQL t5, s7, $0012bbfc
                                            if (true) {
                                                //#012c258  LW t5, $0008(s0)
                                            }
                                            else {
                                                //#012c25c  LQ t0, $0000(s7)
                                                //#012c260  LQ t1, $0010(s7)
                                                //#012c264  LQ t2, $0020(s7)
                                                //#012c268  LQ t3, $0030(s7)
                                                //#012c26c  SQ t0, $0000(t5)
                                                //#012c270  SQ t1, $0010(t5)
                                                //#012c274  SQ t2, $0020(t5)
                                                //#012c278  SQ t3, $0030(t5)
                                                //#012c27c  BEQ zero, zero, $0012bbfc
                                                //#012c280  LW t5, $0008(s0)
                                            }
                                        }
                                    }
                                }
                                //#012bbfc  LW t4, $0004(s0)
                                //#012bc00  LHU t6, $0010(t4)
                                //#012bc04  SLT t7, s2, t6
                                //#012bc08  BEQ t7, zero, $0012bf38
                                //#012bc0c  SUBU t7, s2, t6
                                if (true) {
                                    //#012bf38  LW t6, $0010(s0)
                                    //#012bf3c  SLL t7, t7, 6
                                    //#012bf40  BEQ zero, zero, $0012bc18
                                    //#012bf44  ADDU t6, t6, t7
                                }
                                else {
                                    //#012bc10  LW t7, $002c(s5)
                                    //#012bc14  ADDU t6, t7, s3
                                }
                                //#012bc18  ADDU t5, t5, s3
                                //#012bc1c  BEQL t6, t5, $0012bc4c
                                if (true) {
                                    //#012bc20  LHU t6, $0010(t4)
                                }
                                else {
                                    //#012bc24  LQ t0, $0000(t5)
                                    //#012bc28  LQ t1, $0010(t5)
                                    //#012bc2c  LQ t2, $0020(t5)
                                    //#012bc30  LQ t3, $0030(t5)
                                    //#012bc34  SQ t0, $0000(t6)
                                    //#012bc38  SQ t1, $0010(t6)
                                    //#012bc3c  SQ t2, $0020(t6)
                                    //#012bc40  SQ t3, $0030(t6)
                                    //#012bc44  LW t4, $0004(s0)
                                    //#012bc48  LHU t6, $0010(t4)
                                }
                                //#012bc4c  SLT t7, s2, t6
                                //#012bc50  BEQ t7, zero, $0012bf28
                                //#012bc54  SUBU t7, s2, t6
                                if (true) {
                                    //#012bf28  LW t6, $0010(s0)
                                    //#012bf2c  SLL t7, t7, 6
                                    //#012bf30  BEQ zero, zero, $0012bc60
                                    //#012bf34  ADDU t6, t6, t7
                                }
                                else {
                                    //#012bc58  LW t7, $002c(s5)
                                    //#012bc5c  ADDU t6, t7, s3
                                }
                                //#012bc60  LW t7, $0014(s0)
                                //#012bc64  ADDU t7, t7, s8
                                //#012bc68  LQC2 vf4, $0000(t7)
                                //#012bc6c  LQC2 vf1, $0000(t6)
                                //#012bc70  LQC2 vf2, $0010(t6)
                                //#012bc74  LQC2 vf3, $0020(t6)
                                //#012bc78  VMULx.xyzw vf1, vf1, vf4x
                                //#012bc7c  VMULy.xyzw vf2, vf2, vf4y
                                //#012bc80  VMULz.xyzw vf3, vf3, vf4z
                                //#012bc84  SQC2 vf1, $0000(t6)
                                //#012bc88  SQC2 vf2, $0010(t6)
                                //#012bc8c  SQC2 vf3, $0020(t6)
                                //#012bc90  LW t6, $0004(s0)
                                //#012bc94  LHU t6, $0010(t6)
                                //#012bc98  SLT t7, s2, t6
                                //#012bc9c  BEQ t7, zero, $0012bf18
                                //#012bca0  SUBU t7, s2, t6
                                if (true) {
                                    //#012bf18  LW t6, $0010(s0)
                                    //#012bf1c  SLL t7, t7, 6
                                    //#012bf20  BEQ zero, zero, $0012bcac
                                    //#012bf24  ADDU t6, t6, t7
                                }
                                else {
                                    //#012bca4  LW t7, $002c(s5)
                                    //#012bca8  ADDU t6, t7, s3
                                }
                                //#012bcac  ADDIU t6, t6, $0030
                                //#012bcb0  ADDIU t7, sp, $0080
                                //#012bcb4  BEQ t6, t7, $0012bcc8
                                //#012bcb8  LW t3, $0218(sp)
                                if (true) {
                                }
                                else {
                                    //#012bcbc  LQ t0, $0000(t7)
                                    //#012bcc0  SQ t0, $0000(t6)
                                    //#012bcc4  LW t3, $0218(sp)
                                }
                                if (test012bcc8_then_012bce0_else_0129e60()) {
                                    //#012bce0  BGEZ t4, $0012bed8
                                    //#012bce4  LW t5, $0214(sp)
                                    if (true) {
                                        //#012bed8  ADDIU a1, sp, $0040
                                        //#012bedc  LW t6, $0008(s0)
                                        //#012bee0  SLL t7, t5, 6
                                        //#012bee4  ADDU t6, t6, t7
                                        //#012bee8  BEQ a1, t6, $0012bd14
                                        //#012beec  NOP 
                                    }
                                    else {
                                        //#012bce8  ADDIU a1, sp, $0040
                                        //#012bcec  BEQ a1, sp, $0012bd14
                                        //#012bcf0  NOP 
                                        if (true) {
                                        }
                                        else {
                                            //#012bcf4  LQ t0, $0000(sp)
                                            //#012bcf8  LQ t1, $0010(sp)
                                            //#012bcfc  LQ t2, $0020(sp)
                                            //#012bd00  LQ t3, $0030(sp)
                                            //#012bd04  SQ t0, $0000(a1)
                                            //#012bd08  SQ t1, $0010(a1)
                                            //#012bd0c  SQ t2, $0020(a1)
                                            //#012bd10  SQ t3, $0030(a1)
                                        }
                                    }
                                    //#012bd14  LQ t0, $0000(a1)
                                    //#012bd18  LQ t1, $0010(a1)
                                    //#012bd1c  LQ t2, $0020(a1)
                                    //#012bd20  QMFC2 t3, vf0
                                    //#012bd24  LQC2 vf4, $0030(a1)
                                    //#012bd28  PEXTLW t4, t1, t0
                                    //#012bd2c  PEXTUW t5, t1, t0
                                    //#012bd30  PEXTLW t6, t3, t2
                                    //#012bd34  PEXTUW t7, t3, t2
                                    //#012bd38  PCPYLD t0, t6, t4
                                    //#012bd3c  PCPYUD t1, t4, t6
                                    //#012bd40  PCPYLD t2, t7, t5
                                    //#012bd44  QMTC2 t0, vf1
                                    //#012bd48  QMTC2 t1, vf2
                                    //#012bd4c  QMTC2 t2, vf3
                                    //#012bd50  VMULAx.xyz ACC, vf1, vf4x
                                    //#012bd54  VMADDAy.xyz ACC, vf2, vf4y
                                    //#012bd58  VMADDz.xyz vf4, vf3, vf4z
                                    //#012bd5c  VSUB.xyz vf4, vf0, vf4
                                    //#012bd60  SQ t0, $0000(a1)
                                    //#012bd64  SQ t1, $0010(a1)
                                    //#012bd68  SQ t2, $0020(a1)
                                    //#012bd6c  SQC2 vf4, $0030(a1)
                                    //#012bd70  LW a3, $0004(s0)
                                    //#012bd74  LHU t6, $0010(a3)
                                    //#012bd78  SLT t7, s2, t6
                                    //#012bd7c  BEQ t7, zero, $0012bec8
                                    //#012bd80  SUBU t7, s2, t6
                                    if (true) {
                                        //#012bec8  LW t6, $000c(s0)
                                        //#012becc  SLL t7, t7, 6
                                        //#012bed0  BEQ zero, zero, $0012bd8c
                                        //#012bed4  ADDU t6, t6, t7
                                    }
                                    else {
                                        //#012bd84  LW t7, $0028(s5)
                                        //#012bd88  ADDU t6, t7, s3
                                    }
                                    //#012bd8c  LW t7, $0008(s0)
                                    //#012bd90  ADDIU t8, sp, $0120
                                    //#012bd94  ADDU t7, t7, s3
                                    //#012bd98  LQC2 vf1, $0000(a1)
                                    //#012bd9c  LQC2 vf2, $0010(a1)
                                    //#012bda0  LQC2 vf3, $0020(a1)
                                    //#012bda4  LQC2 vf4, $0030(a1)
                                    //#012bda8  LQC2 vf5, $0000(t7)
                                    //#012bdac  LQC2 vf6, $0010(t7)
                                    //#012bdb0  LQC2 vf7, $0020(t7)
                                    //#012bdb4  LQC2 vf8, $0030(t7)
                                    //#012bdb8  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012bdbc  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012bdc0  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012bdc4  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012bdc8  VMULAx.xyzw ACC, vf1, vf6x
                                    //#012bdcc  VMADDAy.xyzw ACC, vf2, vf6y
                                    //#012bdd0  VMADDAz.xyzw ACC, vf3, vf6z
                                    //#012bdd4  VMADDw.xyzw vf6, vf4, vf6w
                                    //#012bdd8  VMULAx.xyzw ACC, vf1, vf7x
                                    //#012bddc  VMADDAy.xyzw ACC, vf2, vf7y
                                    //#012bde0  VMADDAz.xyzw ACC, vf3, vf7z
                                    //#012bde4  VMADDw.xyzw vf7, vf4, vf7w
                                    //#012bde8  VMULAx.xyzw ACC, vf1, vf8x
                                    //#012bdec  VMADDAy.xyzw ACC, vf2, vf8y
                                    //#012bdf0  VMADDAz.xyzw ACC, vf3, vf8z
                                    //#012bdf4  VMADDw.xyzw vf8, vf4, vf8w
                                    //#012bdf8  SQC2 vf5, $0000(t8)
                                    //#012bdfc  SQC2 vf6, $0010(t8)
                                    //#012be00  SQC2 vf7, $0020(t8)
                                    //#012be04  SQC2 vf8, $0030(t8)
                                    //#012be08  BEQL t6, t8, $0012be34
                                    if (true) {
                                        //#012be0c  LW t7, $0004(s0)
                                    }
                                    else {
                                        //#012be10  LQ t0, $0000(t8)
                                        //#012be14  LQ t1, $0010(t8)
                                        //#012be18  LQ t2, $0020(t8)
                                        //#012be1c  LQ t3, $0030(t8)
                                        //#012be20  SQ t0, $0000(t6)
                                        //#012be24  SQ t1, $0010(t6)
                                        //#012be28  SQ t2, $0020(t6)
                                        //#012be2c  SQ t3, $0030(t6)
                                        //#012be30  LW t7, $0004(s0)
                                    }
                                    //#012be34  LHU t7, $0010(t7)
                                    //#012be38  SLT t6, s2, t7
                                    //#012be3c  BEQ t6, zero, $0012beb8
                                    //#012be40  SUBU t7, s2, t7
                                    if (true) {
                                        //#012beb8  LW t6, $000c(s0)
                                        //#012bebc  SLL t7, t7, 6
                                        //#012bec0  BEQ zero, zero, $0012be4c
                                        //#012bec4  ADDU t6, t6, t7
                                    }
                                    else {
                                        //#012be44  LW t7, $0028(s5)
                                        //#012be48  ADDU t6, t7, s3
                                    }
                                    //#012be4c  ADDIU t6, t6, $0030
                                    //#012be50  ADDIU t7, sp, $0090
                                    //#012be54  BEQL t6, t7, $0012be68
                                    if (true) {
                                        //#012be58  LUI t7, $0040
                                    }
                                    else {
                                        //#012be5c  LQ t0, $0000(t7)
                                        //#012be60  SQ t0, $0000(t6)
                                        //#012be64  LUI t7, $0040
                                    }
                                    //#012be68  AND t7, s4, t7
                                    //#012be6c  BEQ t7, zero, $00129e60
                                    //#012be70  LW t3, $01f4(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012be74  LW t4, $0004(s0)
                                        //#012be78  LHU t6, $0010(t4)
                                        //#012be7c  SLT t7, s2, t6
                                        //#012be80  BEQ t7, zero, $0012bea8
                                        //#012be84  SUBU t7, s2, t6
                                        if (true) {
                                            //#012bea8  LW t6, $000c(s0)
                                            //#012beac  SLL t7, t7, 6
                                            //#012beb0  BEQ zero, zero, $0012be90
                                            //#012beb4  ADDU a0, t6, t7
                                        }
                                        else {
                                            //#012be88  LW t7, $0028(s5)
                                            //#012be8c  ADDU a0, t7, s3
                                        }
                                        //#012be90  LHU t5, $0010(t4)
                                        //#012be94  SLT t7, s2, t5
                                        //#012be98  BEQL t7, zero, $00129e40
                                        if (true) {
                                            //#012be9c  LW t6, $0018(t4)
                                            fn0129e40();
                                        }
                                        else {
                                            //#012bea0  BEQ zero, zero, $0012a9b4
                                            //#012bea4  LW t7, $0020(s5)
                                            {
                                                fn012a9b4();
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                //#012aa04  BEQ t7, zero, $0012aa1c
                                //#012aa08  LW t3, $0218(sp)
                                if (true) {
                                }
                                else {
                                    //#012aa0c  LBU t7, $0000(t7)
                                    //#012aa10  XORI t7, t7, $0004
                                    //#012aa14  MOVN t3, zero, t7
                                    //#012aa18  SW t3, $0218(sp)
                                }
                                //#012aa1c  LW t4, $0004(s0)
                                //#012aa20  LHU t5, $0010(t4)
                                //#012aa24  SLT t7, s2, t5
                                //#012aa28  BEQ t7, zero, $0012b9ec
                                //#012aa2c  ADDIU t3, sp, $0090
                                if (true) {
                                    //#012b9ec  LW t6, $0018(t4)
                                    //#012b9f0  SUBU t7, s2, t5
                                    //#012b9f4  SLL t7, t7, 6
                                    //#012b9f8  ADDU t6, t4, t6
                                    //#012b9fc  ADDU t6, t6, t7
                                    //#012ba00  BEQ zero, zero, $0012aa38
                                    //#012ba04  ADDIU t6, t6, $0030
                                }
                                else {
                                    //#012aa30  LW t7, $0024(s5)
                                    //#012aa34  ADDU t6, t7, s8
                                }
                                //#012aa38  BEQ t3, t6, $0012aa50
                                //#012aa3c  LUI t5, $0038
                                if (true) {
                                    //#012aa40  LQ t0, $0000(t6)
                                    //#012aa44  SQ t0, $0000(t3)
                                    //#012aa48  LW t4, $0004(s0)
                                    //#012aa4c  LUI t5, $0038
                                }
                                //#012aa50  ADDIU t5, t5, $8364
                                //#012aa54  LWC1 $f0, $0000(t5)
                                //#012aa58  SWC1 $f0, $009c(sp)
                                //#012aa5c  LHU t6, $0010(t4)
                                //#012aa60  LW t4, $0214(sp)
                                //#012aa64  SLT t7, t4, t6
                                //#012aa68  BEQ t7, zero, $0012b9d4
                                //#012aa6c  SLL t5, t4, 6
                                if (true) {
                                    //#012b9d4  SUBU t7, t4, t6
                                    //#012b9d8  LW t6, $0010(s0)
                                    //#012b9dc  SLL t7, t7, 6
                                    //#012b9e0  SW t5, $0250(sp)
                                    //#012b9e4  BEQ zero, zero, $0012aa7c
                                    //#012b9e8  ADDU t6, t6, t7
                                }
                                else {
                                    //#012aa70  LW t7, $002c(s5)
                                    //#012aa74  SW t5, $0250(sp)
                                    //#012aa78  ADDU t6, t7, t5
                                }
                                //#012aa7c  ADDIU t8, sp, $0120
                                //#012aa80  LQC2 vf5, $0000(t3)
                                //#012aa84  LQC2 vf1, $0000(t6)
                                //#012aa88  LQC2 vf2, $0010(t6)
                                //#012aa8c  LQC2 vf3, $0020(t6)
                                //#012aa90  LQC2 vf4, $0030(t6)
                                //#012aa94  VMULAx.xyzw ACC, vf1, vf5x
                                //#012aa98  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012aa9c  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012aaa0  VMADDw.xyzw vf5, vf4, vf5w
                                //#012aaa4  SQC2 vf5, $0000(t8)
                                //#012aaa8  ADDIU t6, sp, $0080
                                //#012aaac  BEQ t6, t8, $0012aabc
                                //#012aab0  DADDU t7, t8, zero
                                if (true) {
                                }
                                else {
                                    //#012aab4  LQ t0, $0000(t7)
                                    //#012aab8  SQ t0, $0000(t6)
                                }
                                //#012aabc  LW t7, $0008(s0)
                                //#012aac0  ADDIU a1, sp, $0040
                                //#012aac4  LW t3, $0250(sp)
                                //#012aac8  ADDU t7, t7, t3
                                //#012aacc  BEQ a1, t7, $0012aaf4
                                //#012aad0  NOP 
                                if (true) {
                                }
                                else {
                                    //#012aad4  LQ t0, $0000(t7)
                                    //#012aad8  LQ t1, $0010(t7)
                                    //#012aadc  LQ t2, $0020(t7)
                                    //#012aae0  LQ t3, $0030(t7)
                                    //#012aae4  SQ t0, $0000(a1)
                                    //#012aae8  SQ t1, $0010(a1)
                                    //#012aaec  SQ t2, $0020(a1)
                                    //#012aaf0  SQ t3, $0030(a1)
                                }
                                //#012aaf4  LQ t0, $0000(a1)
                                //#012aaf8  LQ t1, $0010(a1)
                                //#012aafc  LQ t2, $0020(a1)
                                //#012ab00  QMFC2 t3, vf0
                                //#012ab04  LQC2 vf4, $0030(a1)
                                //#012ab08  PEXTLW t4, t1, t0
                                //#012ab0c  PEXTUW t5, t1, t0
                                //#012ab10  PEXTLW t6, t3, t2
                                //#012ab14  PEXTUW t7, t3, t2
                                //#012ab18  PCPYLD t0, t6, t4
                                //#012ab1c  PCPYUD t1, t4, t6
                                //#012ab20  PCPYLD t2, t7, t5
                                //#012ab24  QMTC2 t0, vf1
                                //#012ab28  QMTC2 t1, vf2
                                //#012ab2c  QMTC2 t2, vf3
                                //#012ab30  VMULAx.xyz ACC, vf1, vf4x
                                //#012ab34  VMADDAy.xyz ACC, vf2, vf4y
                                //#012ab38  VMADDz.xyz vf4, vf3, vf4z
                                //#012ab3c  VSUB.xyz vf4, vf0, vf4
                                //#012ab40  SQ t0, $0000(a1)
                                //#012ab44  SQ t1, $0010(a1)
                                //#012ab48  SQ t2, $0020(a1)
                                //#012ab4c  SQC2 vf4, $0030(a1)
                                //#012ab50  ADDIU t4, sp, $0080
                                //#012ab54  LQC2 vf5, $0000(t4)
                                //#012ab58  LQC2 vf1, $0000(a1)
                                //#012ab5c  LQC2 vf2, $0010(a1)
                                //#012ab60  LQC2 vf3, $0020(a1)
                                //#012ab64  LQC2 vf4, $0030(a1)
                                //#012ab68  VMULAx.xyzw ACC, vf1, vf5x
                                //#012ab6c  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012ab70  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012ab74  VMADDw.xyzw vf5, vf4, vf5w
                                //#012ab78  SQC2 vf5, $0000(t8)
                                //#012ab7c  ADDIU t5, sp, $00a0
                                //#012ab80  BEQ t5, t8, $0012ab90
                                //#012ab84  ADDIU t6, sp, $0120
                                if (true) {
                                }
                                else {
                                    //#012ab88  LQ t0, $0000(t6)
                                    //#012ab8c  SQ t0, $0000(t5)
                                }
                                //#012ab90  LW t7, $0204(sp)
                                //#012ab94  BEQ s1, t7, $0012abc8
                                //#012ab98  ADDIU s7, s2, $0001
                                if (true) {
                                    //#012abc8  LW t5, $0004(s0)
                                }
                                else {
                                    //#012ab9c  LHU t7, $0002(s1)
                                    //#012aba0  ANDI t6, s7, $ffff
                                    //#012aba4  BNEL t7, t6, $0012abcc
                                    if (true) {
                                        //#012aba8  LW t5, $0004(s0)
                                    }
                                    else {
                                        //#012abac  LW t3, $0204(sp)
                                        //#012abb0  ADDIU s1, s1, $000c
                                        while (true) {
                                            //#012abb4  BEQL s1, t3, $0012abcc
                                            if (true) {
                                                //#012abb8  LW t5, $0004(s0)
                                                break;
                                            }
                                            else {
                                                //#012abbc  LHU t7, $0002(s1)
                                                //#012abc0  BEQL t7, t6, $0012abb4
                                                //#012abc4  ADDIU s1, s1, $000c
                                                continue;
                                            }
                                        }
                                    }
                                }
                                //#012abcc  LW t7, $0014(s0)
                                //#012abd0  LHU t2, $0010(t5)
                                //#012abd4  ADDU t4, t7, s8
                                //#012abd8  SLT t7, s7, t2
                                //#012abdc  BEQ t7, zero, $0012b9b8
                                //#012abe0  ADDIU t3, t4, $0010
                                if (true) {
                                    //#012b9b8  LW t6, $0018(t5)
                                    //#012b9bc  SUBU t7, s7, t2
                                    //#012b9c0  SLL t7, t7, 6
                                    //#012b9c4  ADDU t6, t5, t6
                                    //#012b9c8  ADDU t6, t6, t7
                                    //#012b9cc  BEQ zero, zero, $0012abf0
                                    //#012b9d0  ADDIU t6, t6, $0010
                                }
                                else {
                                    //#012abe4  LW t6, $001c(s5)
                                    //#012abe8  SLL t7, s7, 4
                                    //#012abec  ADDU t6, t6, t7
                                }
                                //#012abf0  ADDIU t5, sp, $0120
                                //#012abf4  LQC2 vf1, $0000(t4)
                                //#012abf8  LQC2 vf2, $0000(t6)
                                //#012abfc  VMUL.xyzw vf1, vf1, vf2
                                //#012ac00  SQC2 vf1, $0000(t5)
                                //#012ac04  BEQ t3, t5, $0012ac14
                                //#012ac08  DADDU t6, t5, zero
                                if (true) {
                                }
                                else {
                                    //#012ac0c  LQ t0, $0000(t6)
                                    //#012ac10  SQ t0, $0000(t3)
                                }
                                //#012ac14  LW a2, $0220(sp)
                                //#012ac18  DADDU a0, s0, zero
                                //#012ac1c  DADDU a1, s5, zero
                                //#012ac20  ADDIU a3, sp, $00c0
                                //#012ac24  ADDIU t0, sp, $0040
                                //#012ac28  JAL $00128de8
                                //#012ac2c  ADDIU t1, sp, $00b0
                                //#012ac30  LW t7, $01f8(sp)
                                //#012ac34  BEQ t7, zero, $0012acd8
                                //#012ac38  LW t3, $0228(sp)
                                if (true) {
                                }
                                else {
                                    //#012ac3c  SW t7, $0224(sp)
                                    //#012ac40  LD t5, $0010(t7)
                                    //#012ac44  ADDIU t7, zero, $0001
                                    //#012ac48  DSLL32 t7, t7, 21
                                    //#012ac4c  AND t7, t5, t7
                                    //#012ac50  BNE t7, zero, $0012acd8
                                    //#012ac54  LW t3, $0228(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012ac58  ADDIU t4, zero, $0001
                                        //#012ac5c  ANDI t6, s2, $ffff
                                        //#012ac60  DSLL32 t4, t4, 21
                                        //#012ac64  LW t3, $0224(sp)
                                        //#012ac68  LHU t7, $001c(t3)
                                        //#012ac6c  BNE t7, t6, $0012b998
                                        //#012ac70  LW t7, $0224(sp)
                                        //#012ac74  DSRL32 t7, t5, 20
                                        //#012ac78  ADDIU t4, zero, $0001
                                        //#012ac7c  ANDI t7, t7, $0001
                                        //#012ac80  SW t4, $0228(sp)
                                        //#012ac84  SW t7, $022c(sp)
                                        //#012ac88  ADDIU s3, zero, $0020
                                        //#012ac8c  LW a0, $0014(s5)
                                        while (true) {
                                            //#012ac90  LW t7, $0000(a0)
                                            //#012ac94  LW v0, $0018(t7)
                                            //#012ac98  JALR ra, v0
                                            //#012ac9c  DADDU a1, s3, zero
                                            //#012aca0  BEQL s2, v0, $0012b97c
                                            if (true) {
                                                //#012aca4  LW a0, $0014(s5)

                                                //#012b97c  LW t6, $0000(a0)
                                                //#012b980  LW t7, $0014(t6)
                                                //#012b984  JALR ra, t7
                                                //#012b988  ADDIU a1, s3, $ffe0
                                                //#012b98c  BEQ zero, zero, $0012acb8
                                                //#012b990  MOV.S $f20, $f0
                                                break;
                                            }
                                            else {
                                                //#012aca8  ADDIU s3, s3, $0001
                                                //#012acac  SLTI t7, s3, $0024
                                                //#012acb0  BNEL t7, zero, $0012ac90
                                                if (true) {
                                                    //#012acb4  LW a0, $0014(s5)
                                                    continue;
                                                }
                                                break;
                                            }
                                        }
                                        //#012acb8  LW t5, $0224(sp)
                                        //#012acbc  ADDIU t7, zero, $0001
                                        //#012acc0  DSLL32 t7, t7, 19
                                        //#012acc4  LD t6, $0010(t5)
                                        //#012acc8  AND t6, t6, t7
                                        //#012accc  BNEL t6, zero, $0012acd4
                                        if (true) {
                                            //#012acd0  LWC1 $f20, $0018(t5)
                                        }
                                        //#012acd4  LW t3, $0228(sp)
                                    }
                                }
                                //#012acd8  BEQ t3, zero, $0012af80
                                //#012acdc  LW t4, $01f0(sp)
                                if (true) {
                                }
                                else {
                                    //#012ace0  ADDIU t7, sp, $0130
                                    //#012ace4  ADDIU s3, sp, $0140
                                    //#012ace8  ADDIU t5, sp, $00c0
                                    //#012acec  LQC2 vf5, $0000(t5)
                                    //#012acf0  LQC2 vf1, $0000(t4)
                                    //#012acf4  LQC2 vf2, $0010(t4)
                                    //#012acf8  LQC2 vf3, $0020(t4)
                                    //#012acfc  LQC2 vf4, $0030(t4)
                                    //#012ad00  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012ad04  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012ad08  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012ad0c  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012ad10  SQC2 vf5, $0000(s3)
                                    //#012ad14  BEQ t7, s3, $0012ad28
                                    //#012ad18  LWC1 $f0, $0134(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012ad1c  LQ t0, $0000(s3)
                                        //#012ad20  SQ t0, $0000(t7)
                                        //#012ad24  LWC1 $f0, $0134(sp)
                                    }
                                    //#012ad28  ADDIU t6, sp, $0120
                                    //#012ad2c  ADD.S $f0, $f0, $f20
                                    //#012ad30  BEQ t6, t7, $0012ad40
                                    //#012ad34  SWC1 $f0, $0134(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012ad38  LQ t0, $0000(t7)
                                        //#012ad3c  SQ t0, $0000(t6)
                                    }
                                    //#012ad40  LW t5, $0004(s0)
                                    //#012ad44  LW t7, $0014(s0)
                                    //#012ad48  LHU t4, $0010(t5)
                                    //#012ad4c  ADDU t7, t7, s8
                                    //#012ad50  ADDIU t7, t7, $0010
                                    //#012ad54  SLT t6, s7, t4
                                    //#012ad58  BEQ t6, zero, $0012b964
                                    //#012ad5c  SW t7, $0234(sp)
                                    if (true) {
                                        //#012b964  LW t6, $0018(t5)
                                        //#012b968  SUBU t7, s7, t4
                                        //#012b96c  SLL t7, t7, 6
                                        //#012b970  ADDU t6, t5, t6
                                        //#012b974  BEQ zero, zero, $0012ad7c
                                        //#012b978  ADDU v0, t6, t7
                                    }
                                    else {
                                        //#012ad60  LW a0, $0014(s5)
                                        //#012ad64  LW t7, $0000(a0)
                                        //#012ad68  LW v0, $0020(t7)
                                        //#012ad6c  JALR ra, v0
                                        //#012ad70  NOP 
                                        //#012ad74  SLL t7, s7, 6
                                        //#012ad78  ADDU v0, v0, t7
                                    }
                                    //#012ad7c  LW t7, $0234(sp)
                                    //#012ad80  LWC1 $f2, $001c(v0)
                                    //#012ad84  LWC1 $f0, $0000(t7)
                                    //#012ad88  LWC1 $f1, $0124(sp)
                                    //#012ad8c  MUL.S $f0, $f0, $f2
                                    //#012ad90  LW t3, $022c(sp)
                                    //#012ad94  ADD.S $f0, $f0, $f20
                                    //#012ad98  SUB.S $f1, $f1, $f0
                                    //#012ad9c  BEQ t3, zero, $0012ae00
                                    //#012ada0  SWC1 $f1, $0124(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012ada4  LW t5, $0004(s0)
                                        //#012ada8  LW t7, $0014(s0)
                                        //#012adac  LHU t4, $0010(t5)
                                        //#012adb0  ADDU t7, t7, s8
                                        //#012adb4  ADDIU t7, t7, $0010
                                        //#012adb8  SLT t6, s7, t4
                                        //#012adbc  BEQ t6, zero, $0012b94c
                                        //#012adc0  SW t7, $0238(sp)
                                        if (true) {
                                            //#012b94c  LW t6, $0018(t5)
                                            //#012b950  SUBU t7, s7, t4
                                            //#012b954  SLL t7, t7, 6
                                            //#012b958  ADDU t6, t5, t6
                                            //#012b95c  BEQ zero, zero, $0012ade0
                                            //#012b960  ADDU v0, t6, t7
                                        }
                                        else {
                                            //#012adc4  LW a0, $0014(s5)
                                            //#012adc8  LW t7, $0000(a0)
                                            //#012adcc  LW v0, $0020(t7)
                                            //#012add0  JALR ra, v0
                                            //#012add4  NOP 
                                            //#012add8  SLL t7, s7, 6
                                            //#012addc  ADDU v0, v0, t7
                                        }
                                        //#012ade0  LW t4, $0238(sp)
                                        //#012ade4  LWC1 $f2, $001c(v0)
                                        //#012ade8  LWC1 $f0, $0000(t4)
                                        //#012adec  LWC1 $f1, $0134(sp)
                                        //#012adf0  MUL.S $f0, $f0, $f2
                                        //#012adf4  ADD.S $f0, $f0, $f20
                                        //#012adf8  ADD.S $f1, $f1, $f0
                                        //#012adfc  SWC1 $f1, $0134(sp)
                                    }
                                    //#012ae00  ADDIU a0, sp, $0120
                                    //#012ae04  JAL $00143ec8
                                    //#012ae08  DADDU a1, s3, zero
                                    //#012ae0c  BEQ v0, zero, $0012af80
                                    //#012ae10  LW t5, $0224(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012ae14  LW t7, $0010(t5)
                                        //#012ae18  ORI t7, t7, $0001
                                        //#012ae1c  BEQ t5, s3, $0012ae2c
                                        //#012ae20  SW t7, $0010(t5)
                                        if (true) {
                                        }
                                        else {
                                            //#012ae24  LQ t0, $0000(s3)
                                            //#012ae28  SQ t0, $0000(t5)
                                        }
                                        //#012ae2c  ADDIU a3, sp, $0170
                                        //#012ae30  LW t6, $01f0(sp)
                                        //#012ae34  LQ t0, $0000(t6)
                                        //#012ae38  LQ t1, $0010(t6)
                                        //#012ae3c  LQ t2, $0020(t6)
                                        //#012ae40  LQ t3, $0030(t6)
                                        //#012ae44  SQ t0, $0000(a3)
                                        //#012ae48  SQ t1, $0010(a3)
                                        //#012ae4c  SQ t2, $0020(a3)
                                        //#012ae50  SQ t3, $0030(a3)
                                        //#012ae54  LQC2 vf1, $0000(a3)
                                        //#012ae58  LQC2 vf2, $0010(a3)
                                        //#012ae5c  LQC2 vf3, $0020(a3)
                                        //#012ae60  LQC2 vf4, $0030(a3)
                                        //#012ae64  VOPMULA.xyz ACC, vf2, vf3
                                        //#012ae68  VOPMSUB.xyz vf5, vf3, vf2
                                        //#012ae6c  VOPMULA.xyz ACC, vf3, vf1
                                        //#012ae70  VOPMSUB.xyz vf6, vf1, vf3
                                        //#012ae74  VOPMULA.xyz ACC, vf1, vf2
                                        //#012ae78  VOPMSUB.xyz vf7, vf2, vf1
                                        //#012ae7c  VMUL.xyz vf8, vf1, vf5
                                        //#012ae80  VMUL.xyz vf1, vf4, vf5
                                        //#012ae84  VMUL.xyz vf2, vf4, vf6
                                        //#012ae88  VMUL.xyz vf3, vf4, vf7
                                        //#012ae8c  VADDy.x vf8, vf8, vf8y
                                        //#012ae90  VADDy.x vf1, vf1, vf1y
                                        //#012ae94  VADDx.y vf2, vf2, vf2x
                                        //#012ae98  VADDx.z vf3, vf3, vf3x
                                        //#012ae9c  VADDz.x vf8, vf8, vf8z
                                        //#012aea0  VADDz.x vf4, vf1, vf1z
                                        //#012aea4  VADDz.y vf4, vf2, vf2z
                                        //#012aea8  VADDy.z vf4, vf3, vf3y
                                        //#012aeac  VDIV Q, vf0w, vf8x
                                        //#012aeb0  QMFC2 t0, vf5
                                        //#012aeb4  QMFC2 t1, vf6
                                        //#012aeb8  QMFC2 t2, vf7
                                        //#012aebc  QMFC2 t3, vf0
                                        //#012aec0  PEXTLW t4, t1, t0
                                        //#012aec4  PEXTUW t5, t1, t0
                                        //#012aec8  PEXTLW t6, t3, t2
                                        //#012aecc  PEXTUW t7, t3, t2
                                        //#012aed0  PCPYLD t0, t6, t4
                                        //#012aed4  PCPYUD t1, t4, t6
                                        //#012aed8  PCPYLD t2, t7, t5
                                        //#012aedc  QMTC2 t0, vf1
                                        //#012aee0  QMTC2 t1, vf2
                                        //#012aee4  QMTC2 t2, vf3
                                        //#012aee8  VSUB.xyz vf4, vf0, vf4
                                        //#012aeec  VWAITQ 
                                        //#012aef0  VMULq.xyzw vf1, vf1, Q
                                        //#012aef4  VMULq.xyzw vf2, vf2, Q
                                        //#012aef8  VMULq.xyzw vf3, vf3, Q
                                        //#012aefc  VMULq.xyz vf4, vf4, Q
                                        //#012af00  SQC2 vf1, $0000(a3)
                                        //#012af04  SQC2 vf2, $0010(a3)
                                        //#012af08  SQC2 vf3, $0020(a3)
                                        //#012af0c  SQC2 vf4, $0030(a3)
                                        //#012af10  LWC1 $f0, $0144(sp)
                                        //#012af14  ADDIU t7, sp, $00c0
                                        //#012af18  SUB.S $f0, $f0, $f20
                                        //#012af1c  SWC1 $f0, $0144(sp)
                                        //#012af20  LQC2 vf5, $0000(s3)
                                        //#012af24  LQC2 vf1, $0000(a3)
                                        //#012af28  LQC2 vf2, $0010(a3)
                                        //#012af2c  LQC2 vf3, $0020(a3)
                                        //#012af30  LQC2 vf4, $0030(a3)
                                        //#012af34  VMULAx.xyz ACC, vf1, vf5x
                                        //#012af38  VMADDAy.xyz ACC, vf2, vf5y
                                        //#012af3c  VMADDAz.xyz ACC, vf3, vf5z
                                        //#012af40  VMADDw.xyz vf5, vf4, vf0w
                                        //#012af44  SQC2 vf5, $0000(t7)
                                        //#012af48  ADDIU t3, sp, $00b0
                                        //#012af4c  ADDIU t4, sp, $0040
                                        //#012af50  DADDU t5, t7, zero
                                        //#012af54  LQC2 vf5, $0000(t5)
                                        //#012af58  LQC2 vf1, $0000(t4)
                                        //#012af5c  LQC2 vf2, $0010(t4)
                                        //#012af60  LQC2 vf3, $0020(t4)
                                        //#012af64  LQC2 vf4, $0030(t4)
                                        //#012af68  VMULAx.xyz ACC, vf1, vf5x
                                        //#012af6c  VMADDAy.xyz ACC, vf2, vf5y
                                        //#012af70  VMADDAz.xyz ACC, vf3, vf5z
                                        //#012af74  VMADDw.xyz vf5, vf4, vf0w
                                        //#012af78  VMOVE.w vf5, vf0
                                        //#012af7c  SQC2 vf5, $0000(t3)
                                    }
                                }
                                //#012af80  LW t6, $0218(sp)
                                //#012af84  BEQ t6, zero, $0012b7a8
                                //#012af88  LW t7, $0218(sp)
                                if (true) {
                                    //#012b7a8  LW t4, $0004(s0)
                                    //#012b7ac  LW t3, $0014(s0)
                                    //#012b7b0  LHU t5, $0010(t4)
                                    //#012b7b4  ADDU t6, t3, s8
                                    //#012b7b8  SLT t7, s2, t5
                                    //#012b7bc  BEQ t7, zero, $0012b930
                                    //#012b7c0  SW t6, $0244(sp)
                                    if (true) {
                                        //#012b930  LW t6, $0018(t4)
                                        //#012b934  SUBU t7, s2, t5
                                        //#012b938  SLL t7, t7, 6
                                        //#012b93c  SLL s3, s2, 6
                                        //#012b940  ADDU t6, t4, t6
                                        //#012b944  BEQ zero, zero, $0012b7e4
                                        //#012b948  ADDU v0, t6, t7
                                    }
                                    else {
                                        //#012b7c4  LW a0, $0014(s5)
                                        //#012b7c8  LW t7, $0000(a0)
                                        //#012b7cc  LW v0, $0020(t7)
                                        //#012b7d0  JALR ra, v0
                                        //#012b7d4  SLL s3, s2, 6
                                        //#012b7d8  LW t4, $0004(s0)
                                        //#012b7dc  ADDU v0, v0, s3
                                        //#012b7e0  LW t3, $0014(s0)
                                    }
                                    //#012b7e4  LW t7, $0244(sp)
                                    //#012b7e8  LWC1 $f0, $001c(v0)
                                    //#012b7ec  LWC1 $f1, $0000(t7)
                                    //#012b7f0  ADDU t7, t3, s8
                                    //#012b7f4  ADDIU t7, t7, $0010
                                    //#012b7f8  SW t7, $0248(sp)
                                    //#012b7fc  LHU t5, $0010(t4)
                                    //#012b800  SLT t7, s7, t5
                                    //#012b804  BEQ t7, zero, $0012b918
                                    //#012b808  MUL.S $f20, $f1, $f0
                                    if (true) {
                                        //#012b918  LW t6, $0018(t4)
                                        //#012b91c  SUBU t7, s7, t5
                                        //#012b920  SLL t7, t7, 6
                                        //#012b924  ADDU t6, t4, t6
                                        //#012b928  BEQ zero, zero, $0012b82c
                                        //#012b92c  ADDU v0, t6, t7
                                    }
                                    else {
                                        //#012b80c  LW a0, $0014(s5)
                                        //#012b810  LW t7, $0000(a0)
                                        //#012b814  LW v0, $0020(t7)
                                        //#012b818  JALR ra, v0
                                        //#012b81c  NOP 
                                        //#012b820  SLL t7, s7, 6
                                        //#012b824  LW t4, $0004(s0)
                                        //#012b828  ADDU v0, v0, t7
                                    }
                                    //#012b82c  LW t3, $0248(sp)
                                    //#012b830  LWC1 $f0, $001c(v0)
                                    //#012b834  LWC1 $f1, $0000(t3)
                                    //#012b838  LHU t6, $0010(t4)
                                    //#012b83c  SLT t7, s2, t6
                                    //#012b840  BEQ t7, zero, $0012b904
                                    //#012b844  MUL.S $f13, $f1, $f0
                                    if (true) {
                                        //#012b904  SUBU t7, s2, t6
                                        //#012b908  LW t6, $000c(s0)
                                        //#012b90c  SLL t7, t7, 6
                                        //#012b910  BEQ zero, zero, $0012b850
                                        //#012b914  ADDU a3, t6, t7
                                    }
                                    else {
                                        //#012b848  LW t7, $0028(s5)
                                        //#012b84c  ADDU a3, t7, s3
                                    }
                                    //#012b850  LHU t6, $0010(t4)
                                    //#012b854  SLT t7, s7, t6
                                    //#012b858  BEQ t7, zero, $0012b8f8
                                    //#012b85c  SUBU t7, s7, t6
                                    if (true) {
                                        //#012b8f8  LW t6, $000c(s0)
                                        //#012b8fc  BEQ zero, zero, $0012b868
                                        //#012b900  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012b860  LW t6, $0028(s5)
                                        //#012b864  SLL t7, s7, 6
                                    }
                                    //#012b868  LHU t5, $0010(t4)
                                    //#012b86c  ADDU t0, t6, t7
                                    //#012b870  SLT t7, s2, t5
                                    //#012b874  BEQL t7, zero, $0012b8e0
                                    if (true) {
                                        //#012b878  LW t6, $0018(t4)

                                        //#012b8e0  SUBU t7, s2, t5
                                        //#012b8e4  SLL t7, t7, 6
                                        //#012b8e8  ADDU t6, t4, t6
                                        //#012b8ec  ADDU t6, t6, t7
                                        //#012b8f0  BEQ zero, zero, $0012b884
                                        //#012b8f4  ADDIU t3, t6, $0020
                                    }
                                    else {
                                        //#012b87c  LW t7, $0020(s5)
                                        //#012b880  ADDU t3, t7, s8
                                    }
                                    //#012b884  LHU t5, $0010(t4)
                                    //#012b888  SLT t7, s7, t5
                                    //#012b88c  BEQ t7, zero, $0012b8c4
                                    //#012b890  SLL t7, s7, 4
                                    if (true) {
                                        //#012b8c4  LW t6, $0018(t4)
                                        //#012b8c8  SUBU t7, s7, t5
                                        //#012b8cc  SLL t7, t7, 6
                                        //#012b8d0  ADDU t6, t4, t6
                                        //#012b8d4  ADDU t6, t6, t7
                                        //#012b8d8  BEQ zero, zero, $0012b89c
                                        //#012b8dc  ADDIU t6, t6, $0020
                                    }
                                    else {
                                        //#012b894  LW t6, $0020(s5)
                                        //#012b898  ADDU t6, t6, t7
                                    }
                                    //#012b89c  LW t4, $024c(sp)
                                    //#012b8a0  MOV.S $f12, $f20
                                    //#012b8a4  LWC1 $f14, $0000(t3)
                                    //#012b8a8  ADDIU a0, sp, $00a0
                                    //#012b8ac  ANDI a2, t4, $0003
                                    //#012b8b0  LWC1 $f15, $0000(t6)
                                    //#012b8b4  ADDIU a2, a2, $fffe
                                    //#012b8b8  ADDIU a1, sp, $00b0
                                    //#012b8bc  BEQ zero, zero, $0012b100
                                    //#012b8c0  DADDU t1, zero, zero
                                }
                                else {
                                    //#012af8c  LW t6, $0004(s0)
                                    //#012af90  LHU t5, $0004(t7)
                                    //#012af94  LHU t6, $0010(t6)
                                    //#012af98  SLT t7, t5, t6
                                    //#012af9c  BEQ t7, zero, $0012b79c
                                    //#012afa0  SUBU t7, t5, t6
                                    if (true) {
                                        //#012b79c  LW t6, $0010(s0)
                                        //#012b7a0  BEQ zero, zero, $0012afac
                                        //#012b7a4  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012afa4  LW t6, $002c(s5)
                                        //#012afa8  SLL t7, t5, 6
                                    }
                                    //#012afac  ADDU t6, t6, t7
                                    //#012afb0  ADDIU t3, sp, $0120
                                    //#012afb4  ADDIU t7, t6, $0030
                                    //#012afb8  ADDIU t4, sp, $0040
                                    //#012afbc  LQC2 vf5, $0000(t7)
                                    //#012afc0  LQC2 vf1, $0000(t4)
                                    //#012afc4  LQC2 vf2, $0010(t4)
                                    //#012afc8  LQC2 vf3, $0020(t4)
                                    //#012afcc  LQC2 vf4, $0030(t4)
                                    //#012afd0  VMULAx.xyz ACC, vf1, vf5x
                                    //#012afd4  VMADDAy.xyz ACC, vf2, vf5y
                                    //#012afd8  VMADDAz.xyz ACC, vf3, vf5z
                                    //#012afdc  VMADDw.xyz vf5, vf4, vf0w
                                    //#012afe0  VMOVE.w vf5, vf0
                                    //#012afe4  SQC2 vf5, $0000(t3)
                                    //#012afe8  LW t4, $0004(s0)
                                    //#012afec  LW t3, $0014(s0)
                                    //#012aff0  LHU t5, $0010(t4)
                                    //#012aff4  ADDU t6, t3, s8
                                    //#012aff8  SLT t7, s2, t5
                                    //#012affc  BEQ t7, zero, $0012b780
                                    //#012b000  SW t6, $023c(sp)
                                    if (true) {
                                        //#012b780  LW t6, $0018(t4)
                                        //#012b784  SUBU t7, s2, t5
                                        //#012b788  SLL t7, t7, 6
                                        //#012b78c  SLL s3, s2, 6
                                        //#012b790  ADDU t6, t4, t6
                                        //#012b794  BEQ zero, zero, $0012b024
                                        //#012b798  ADDU v0, t6, t7
                                    }
                                    else {
                                        //#012b004  LW a0, $0014(s5)
                                        //#012b008  LW t7, $0000(a0)
                                        //#012b00c  LW v0, $0020(t7)
                                        //#012b010  JALR ra, v0
                                        //#012b014  SLL s3, s2, 6
                                        //#012b018  LW t4, $0004(s0)
                                        //#012b01c  ADDU v0, v0, s3
                                        //#012b020  LW t3, $0014(s0)
                                    }
                                    //#012b024  LW t7, $023c(sp)
                                    //#012b028  LWC1 $f0, $001c(v0)
                                    //#012b02c  LWC1 $f1, $0000(t7)
                                    //#012b030  ADDU t7, t3, s8
                                    //#012b034  ADDIU t7, t7, $0010
                                    //#012b038  SW t7, $0240(sp)
                                    //#012b03c  LHU t5, $0010(t4)
                                    //#012b040  SLT t7, s7, t5
                                    //#012b044  BEQ t7, zero, $0012b768
                                    //#012b048  MUL.S $f20, $f1, $f0
                                    if (true) {
                                        //#012b768  LW t6, $0018(t4)
                                        //#012b76c  SUBU t7, s7, t5
                                        //#012b770  SLL t7, t7, 6
                                        //#012b774  ADDU t6, t4, t6
                                        //#012b778  BEQ zero, zero, $0012b06c
                                        //#012b77c  ADDU v0, t6, t7
                                    }
                                    else {
                                        //#012b04c  LW a0, $0014(s5)
                                        //#012b050  LW t7, $0000(a0)
                                        //#012b054  LW v0, $0020(t7)
                                        //#012b058  JALR ra, v0
                                        //#012b05c  NOP 
                                        //#012b060  SLL t7, s7, 6
                                        //#012b064  LW t4, $0004(s0)
                                        //#012b068  ADDU v0, v0, t7
                                    }
                                    //#012b06c  LW t3, $0240(sp)
                                    //#012b070  LWC1 $f0, $001c(v0)
                                    //#012b074  LWC1 $f1, $0000(t3)
                                    //#012b078  LHU t6, $0010(t4)
                                    //#012b07c  SLT t7, s2, t6
                                    //#012b080  BEQ t7, zero, $0012b754
                                    //#012b084  MUL.S $f13, $f1, $f0
                                    if (true) {
                                        //#012b754  SUBU t7, s2, t6
                                        //#012b758  LW t6, $000c(s0)
                                        //#012b75c  SLL t7, t7, 6
                                        //#012b760  BEQ zero, zero, $0012b090
                                        //#012b764  ADDU a3, t6, t7
                                    }
                                    else {
                                        //#012b088  LW t7, $0028(s5)
                                        //#012b08c  ADDU a3, t7, s3
                                    }
                                    //#012b090  LHU t6, $0010(t4)
                                    //#012b094  SLT t7, s7, t6
                                    //#012b098  BEQ t7, zero, $0012b748
                                    //#012b09c  SUBU t7, s7, t6
                                    if (true) {
                                        //#012b748  LW t6, $000c(s0)
                                        //#012b74c  BEQ zero, zero, $0012b0a8
                                        //#012b750  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012b0a0  LW t6, $0028(s5)
                                        //#012b0a4  SLL t7, s7, 6
                                    }
                                    //#012b0a8  LHU t5, $0010(t4)
                                    //#012b0ac  ADDU t0, t6, t7
                                    //#012b0b0  SLT t7, s2, t5
                                    //#012b0b4  BEQL t7, zero, $0012b730
                                    if (true) {
                                        //#012b0b8  LW t6, $0018(t4)

                                        //#012b730  SUBU t7, s2, t5
                                        //#012b734  SLL t7, t7, 6
                                        //#012b738  ADDU t6, t4, t6
                                        //#012b73c  ADDU t6, t6, t7
                                        //#012b740  BEQ zero, zero, $0012b0c4
                                        //#012b744  ADDIU t3, t6, $0020
                                    }
                                    else {
                                        //#012b0bc  LW t7, $0020(s5)
                                        //#012b0c0  ADDU t3, t7, s8
                                    }
                                    //#012b0c4  LHU t5, $0010(t4)
                                    //#012b0c8  SLT t7, s7, t5
                                    //#012b0cc  BEQ t7, zero, $0012b714
                                    //#012b0d0  SLL t7, s7, 4
                                    if (true) {
                                        //#012b714  LW t6, $0018(t4)
                                        //#012b718  SUBU t7, s7, t5
                                        //#012b71c  SLL t7, t7, 6
                                        //#012b720  ADDU t6, t4, t6
                                        //#012b724  ADDU t6, t6, t7
                                        //#012b728  BEQ zero, zero, $0012b0dc
                                        //#012b72c  ADDIU t6, t6, $0020
                                    }
                                    else {
                                        //#012b0d4  LW t6, $0020(s5)
                                        //#012b0d8  ADDU t6, t6, t7
                                    }
                                    //#012b0dc  LW t4, $024c(sp)
                                    //#012b0e0  MOV.S $f12, $f20
                                    //#012b0e4  LWC1 $f14, $0000(t3)
                                    //#012b0e8  ADDIU a0, sp, $00a0
                                    //#012b0ec  ANDI a2, t4, $0003
                                    //#012b0f0  LWC1 $f15, $0000(t6)
                                    //#012b0f4  ADDIU a2, a2, $fffe
                                    //#012b0f8  ADDIU a1, sp, $00b0
                                    //#012b0fc  ADDIU t1, sp, $0120
                                }
                                //#012b100  JAL $00124a88
                                //#012b104  NOP 
                                //#012b108  LUI t7, $0040
                                //#012b10c  AND t7, s4, t7
                                //#012b110  BEQL t7, zero, $0012b158
                                if (true) {
                                    //#012b114  LW t7, $0004(s0)
                                }
                                else {
                                    //#012b118  LW t4, $0004(s0)
                                    //#012b11c  LHU t6, $0010(t4)
                                    //#012b120  SLT t7, s2, t6
                                    //#012b124  BEQ t7, zero, $0012b704
                                    //#012b128  SUBU t7, s2, t6
                                    if (true) {
                                        //#012b704  LW t6, $000c(s0)
                                        //#012b708  SLL t7, t7, 6
                                        //#012b70c  BEQ zero, zero, $0012b134
                                        //#012b710  ADDU a0, t6, t7
                                    }
                                    else {
                                        //#012b12c  LW t7, $0028(s5)
                                        //#012b130  ADDU a0, t7, s3
                                    }
                                    //#012b134  LHU t5, $0010(t4)
                                    //#012b138  SLT t7, s2, t5
                                    //#012b13c  BEQL t7, zero, $0012b6ec
                                    if (true) {
                                        //#012b140  LW t6, $0018(t4)

                                        //#012b6ec  SUBU t7, s2, t5
                                        //#012b6f0  SLL t7, t7, 6
                                        //#012b6f4  ADDU t6, t4, t6
                                        //#012b6f8  ADDU t6, t6, t7
                                        //#012b6fc  BEQ zero, zero, $0012b14c
                                        //#012b700  ADDIU a1, t6, $0020
                                    }
                                    else {
                                        //#012b144  LW t7, $0020(s5)
                                        //#012b148  ADDU a1, t7, s8
                                    }
                                    //#012b14c  JAL $0011b840
                                    //#012b150  NOP 
                                    //#012b154  LW t7, $0004(s0)
                                }
                                //#012b158  LHU t7, $0010(t7)
                                //#012b15c  SLT t6, s2, t7
                                //#012b160  BEQ t6, zero, $0012b6dc
                                //#012b164  SUBU t7, s2, t7
                                if (true) {
                                    //#012b6dc  LW t6, $000c(s0)
                                    //#012b6e0  SLL t7, t7, 6
                                    //#012b6e4  BEQ zero, zero, $0012b170
                                    //#012b6e8  ADDU t6, t6, t7
                                }
                                else {
                                    //#012b168  LW t7, $0028(s5)
                                    //#012b16c  ADDU t6, t7, s3
                                }
                                //#012b170  LWC1 $f0, $0090(sp)
                                //#012b174  SWC1 $f0, $0030(t6)
                                //#012b178  LW t7, $0004(s0)
                                //#012b17c  LW t6, $0008(s0)
                                //#012b180  LW t5, $0250(sp)
                                //#012b184  LHU t4, $0010(t7)
                                //#012b188  ADDU t3, t6, t5
                                //#012b18c  SLT t7, s2, t4
                                //#012b190  BEQ t7, zero, $0012b6c8
                                //#012b194  ADDU t5, t6, s3
                                if (true) {
                                    //#012b6c8  SUBU t7, s2, t4
                                    //#012b6cc  LW t6, $000c(s0)
                                    //#012b6d0  SLL t7, t7, 6
                                    //#012b6d4  BEQ zero, zero, $0012b1a0
                                    //#012b6d8  ADDU t6, t6, t7
                                }
                                else {
                                    //#012b198  LW t7, $0028(s5)
                                    //#012b19c  ADDU t6, t7, s3
                                }
                                //#012b1a0  ADDIU t7, sp, $0120
                                //#012b1a4  LQC2 vf1, $0000(t3)
                                //#012b1a8  LQC2 vf2, $0010(t3)
                                //#012b1ac  LQC2 vf3, $0020(t3)
                                //#012b1b0  LQC2 vf4, $0030(t3)
                                //#012b1b4  LQC2 vf5, $0000(t6)
                                //#012b1b8  LQC2 vf6, $0010(t6)
                                //#012b1bc  LQC2 vf7, $0020(t6)
                                //#012b1c0  LQC2 vf8, $0030(t6)
                                //#012b1c4  VMULAx.xyzw ACC, vf1, vf5x
                                //#012b1c8  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012b1cc  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012b1d0  VMADDw.xyzw vf5, vf4, vf5w
                                //#012b1d4  VMULAx.xyzw ACC, vf1, vf6x
                                //#012b1d8  VMADDAy.xyzw ACC, vf2, vf6y
                                //#012b1dc  VMADDAz.xyzw ACC, vf3, vf6z
                                //#012b1e0  VMADDw.xyzw vf6, vf4, vf6w
                                //#012b1e4  VMULAx.xyzw ACC, vf1, vf7x
                                //#012b1e8  VMADDAy.xyzw ACC, vf2, vf7y
                                //#012b1ec  VMADDAz.xyzw ACC, vf3, vf7z
                                //#012b1f0  VMADDw.xyzw vf7, vf4, vf7w
                                //#012b1f4  VMULAx.xyzw ACC, vf1, vf8x
                                //#012b1f8  VMADDAy.xyzw ACC, vf2, vf8y
                                //#012b1fc  VMADDAz.xyzw ACC, vf3, vf8z
                                //#012b200  VMADDw.xyzw vf8, vf4, vf8w
                                //#012b204  SQC2 vf5, $0000(t7)
                                //#012b208  SQC2 vf6, $0010(t7)
                                //#012b20c  SQC2 vf7, $0020(t7)
                                //#012b210  SQC2 vf8, $0030(t7)
                                //#012b214  BEQ t5, t7, $0012b23c
                                //#012b218  DADDU t4, t7, zero
                                if (true) {
                                }
                                else {
                                    //#012b21c  LQ t0, $0000(t4)
                                    //#012b220  LQ t1, $0010(t4)
                                    //#012b224  LQ t2, $0020(t4)
                                    //#012b228  LQ t3, $0030(t4)
                                    //#012b22c  SQ t0, $0000(t5)
                                    //#012b230  SQ t1, $0010(t5)
                                    //#012b234  SQ t2, $0020(t5)
                                    //#012b238  SQ t3, $0030(t5)
                                }
                                //#012b23c  LW t4, $0004(s0)
                                //#012b240  LHU t6, $0010(t4)
                                //#012b244  SLT t7, s2, t6
                                //#012b248  BEQ t7, zero, $0012b6b8
                                //#012b24c  SUBU t7, s2, t6
                                if (true) {
                                    //#012b6b8  LW t6, $0010(s0)
                                    //#012b6bc  SLL t7, t7, 6
                                    //#012b6c0  BEQ zero, zero, $0012b258
                                    //#012b6c4  ADDU t6, t6, t7
                                }
                                else {
                                    //#012b250  LW t7, $002c(s5)
                                    //#012b254  ADDU t6, t7, s3
                                }
                                //#012b258  LW t7, $0008(s0)
                                //#012b25c  ADDU t7, t7, s3
                                //#012b260  BEQL t6, t7, $0012b290
                                if (true) {
                                    //#012b264  LHU t6, $0010(t4)
                                }
                                else {
                                    //#012b268  LQ t0, $0000(t7)
                                    //#012b26c  LQ t1, $0010(t7)
                                    //#012b270  LQ t2, $0020(t7)
                                    //#012b274  LQ t3, $0030(t7)
                                    //#012b278  SQ t0, $0000(t6)
                                    //#012b27c  SQ t1, $0010(t6)
                                    //#012b280  SQ t2, $0020(t6)
                                    //#012b284  SQ t3, $0030(t6)
                                    //#012b288  LW t4, $0004(s0)
                                    //#012b28c  LHU t6, $0010(t4)
                                }
                                //#012b290  SLT t7, s2, t6
                                //#012b294  BEQ t7, zero, $0012b6a8
                                //#012b298  SUBU t7, s2, t6
                                if (true) {
                                    //#012b6a8  LW t6, $0010(s0)
                                    //#012b6ac  SLL t7, t7, 6
                                    //#012b6b0  BEQ zero, zero, $0012b2a4
                                    //#012b6b4  ADDU t6, t6, t7
                                }
                                else {
                                    //#012b29c  LW t7, $002c(s5)
                                    //#012b2a0  ADDU t6, t7, s3
                                }
                                //#012b2a4  LW t7, $0014(s0)
                                //#012b2a8  ADDU t7, t7, s8
                                //#012b2ac  LQC2 vf4, $0000(t7)
                                //#012b2b0  LQC2 vf1, $0000(t6)
                                //#012b2b4  LQC2 vf2, $0010(t6)
                                //#012b2b8  LQC2 vf3, $0020(t6)
                                //#012b2bc  VMULx.xyzw vf1, vf1, vf4x
                                //#012b2c0  VMULy.xyzw vf2, vf2, vf4y
                                //#012b2c4  VMULz.xyzw vf3, vf3, vf4z
                                //#012b2c8  SQC2 vf1, $0000(t6)
                                //#012b2cc  SQC2 vf2, $0010(t6)
                                //#012b2d0  SQC2 vf3, $0020(t6)
                                //#012b2d4  LW t4, $0004(s0)
                                //#012b2d8  LHU t6, $0010(t4)
                                //#012b2dc  SLT t7, s2, t6
                                //#012b2e0  BEQ t7, zero, $0012b698
                                //#012b2e4  SUBU t7, s2, t6
                                if (true) {
                                    //#012b698  LW t6, $0010(s0)
                                    //#012b69c  SLL t7, t7, 6
                                    //#012b6a0  BEQ zero, zero, $0012b2f0
                                    //#012b6a4  ADDU t6, t6, t7
                                }
                                else {
                                    //#012b2e8  LW t7, $002c(s5)
                                    //#012b2ec  ADDU t6, t7, s3
                                }
                                //#012b2f0  ADDIU t6, t6, $0030
                                //#012b2f4  ADDIU t5, sp, $0080
                                //#012b2f8  BEQL t6, t5, $0012b310
                                if (true) {
                                    //#012b2fc  LHU t6, $0010(t4)
                                }
                                else {
                                    //#012b300  LQ t0, $0000(t5)
                                    //#012b304  SQ t0, $0000(t6)
                                    //#012b308  LW t4, $0004(s0)
                                    //#012b30c  LHU t6, $0010(t4)
                                }
                                //#012b310  SLT t7, s7, t6
                                //#012b314  BEQ t7, zero, $0012b68c
                                //#012b318  SUBU t7, s7, t6
                                if (true) {
                                    //#012b68c  LW t6, $000c(s0)
                                    //#012b690  BEQ zero, zero, $0012b324
                                    //#012b694  SLL t7, t7, 6
                                }
                                else {
                                    //#012b31c  LW t6, $0028(s5)
                                    //#012b320  SLL t7, s7, 6
                                }
                                //#012b324  LHU t5, $0010(t4)
                                //#012b328  ADDU t6, t6, t7
                                //#012b32c  SLT t7, s2, t5
                                //#012b330  BEQ t7, zero, $0012b674
                                //#012b334  ADDIU s4, t6, $0030
                                if (true) {
                                    //#012b674  LW t6, $0018(t4)
                                    //#012b678  SUBU t7, s2, t5
                                    //#012b67c  SLL t7, t7, 6
                                    //#012b680  ADDU t6, t4, t6
                                    //#012b684  BEQ zero, zero, $0012b350
                                    //#012b688  ADDU v0, t6, t7
                                }
                                else {
                                    //#012b338  LW a0, $0014(s5)
                                    //#012b33c  LW t7, $0000(a0)
                                    //#012b340  LW v0, $0020(t7)
                                    //#012b344  JALR ra, v0
                                    //#012b348  NOP 
                                    //#012b34c  ADDU v0, v0, s3
                                }
                                //#012b350  LWC1 $f0, $001c(v0)
                                //#012b354  SWC1 $f0, $0000(s4)
                                //#012b358  LW t7, $0004(s0)
                                //#012b35c  LW t6, $0008(s0)
                                //#012b360  LHU t3, $0010(t7)
                                //#012b364  ADDU t5, t6, s3
                                //#012b368  SLT t7, s7, t3
                                //#012b36c  BEQ t7, zero, $0012b664
                                //#012b370  ADDIU t4, t5, $0040
                                if (true) {
                                    //#012b664  SUBU t7, s7, t3
                                    //#012b668  LW t6, $000c(s0)
                                    //#012b66c  BEQ zero, zero, $0012b37c
                                    //#012b670  SLL t7, t7, 6
                                }
                                else {
                                    //#012b374  LW t6, $0028(s5)
                                    //#012b378  SLL t7, s7, 6
                                }
                                //#012b37c  ADDU t6, t6, t7
                                //#012b380  ADDIU t7, sp, $0120
                                //#012b384  LQC2 vf1, $0000(t5)
                                //#012b388  LQC2 vf2, $0010(t5)
                                //#012b38c  LQC2 vf3, $0020(t5)
                                //#012b390  LQC2 vf4, $0030(t5)
                                //#012b394  LQC2 vf5, $0000(t6)
                                //#012b398  LQC2 vf6, $0010(t6)
                                //#012b39c  LQC2 vf7, $0020(t6)
                                //#012b3a0  LQC2 vf8, $0030(t6)
                                //#012b3a4  VMULAx.xyzw ACC, vf1, vf5x
                                //#012b3a8  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012b3ac  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012b3b0  VMADDw.xyzw vf5, vf4, vf5w
                                //#012b3b4  VMULAx.xyzw ACC, vf1, vf6x
                                //#012b3b8  VMADDAy.xyzw ACC, vf2, vf6y
                                //#012b3bc  VMADDAz.xyzw ACC, vf3, vf6z
                                //#012b3c0  VMADDw.xyzw vf6, vf4, vf6w
                                //#012b3c4  VMULAx.xyzw ACC, vf1, vf7x
                                //#012b3c8  VMADDAy.xyzw ACC, vf2, vf7y
                                //#012b3cc  VMADDAz.xyzw ACC, vf3, vf7z
                                //#012b3d0  VMADDw.xyzw vf7, vf4, vf7w
                                //#012b3d4  VMULAx.xyzw ACC, vf1, vf8x
                                //#012b3d8  VMADDAy.xyzw ACC, vf2, vf8y
                                //#012b3dc  VMADDAz.xyzw ACC, vf3, vf8z
                                //#012b3e0  VMADDw.xyzw vf8, vf4, vf8w
                                //#012b3e4  SQC2 vf5, $0000(t7)
                                //#012b3e8  SQC2 vf6, $0010(t7)
                                //#012b3ec  SQC2 vf7, $0020(t7)
                                //#012b3f0  SQC2 vf8, $0030(t7)
                                //#012b3f4  BEQL t4, t7, $0012b424
                                if (true) {
                                    //#012b3f8  LW t4, $0004(s0)
                                }
                                else {
                                    //#012b3fc  DADDU t5, t7, zero
                                    //#012b400  LQ t0, $0000(t7)
                                    //#012b404  LQ t1, $0010(t7)
                                    //#012b408  LQ t2, $0020(t7)
                                    //#012b40c  LQ t3, $0030(t7)
                                    //#012b410  SQ t0, $0000(t4)
                                    //#012b414  SQ t1, $0010(t4)
                                    //#012b418  SQ t2, $0020(t4)
                                    //#012b41c  SQ t3, $0030(t4)
                                    //#012b420  LW t4, $0004(s0)
                                }
                                //#012b424  LHU t6, $0010(t4)
                                //#012b428  SLT t7, s7, t6
                                //#012b42c  BEQ t7, zero, $0012b658
                                //#012b430  SUBU t7, s7, t6
                                if (true) {
                                    //#012b658  LW t6, $0010(s0)
                                    //#012b65c  BEQ zero, zero, $0012b43c
                                    //#012b660  SLL t7, t7, 6
                                }
                                else {
                                    //#012b434  LW t6, $002c(s5)
                                    //#012b438  SLL t7, s7, 6
                                }
                                //#012b43c  ADDU t6, t6, t7
                                //#012b440  LW t7, $0008(s0)
                                //#012b444  ADDU t7, t7, s3
                                //#012b448  ADDIU t7, t7, $0040
                                //#012b44c  BEQL t6, t7, $0012b47c
                                if (true) {
                                    //#012b450  LHU t6, $0010(t4)
                                }
                                else {
                                    //#012b454  LQ t0, $0000(t7)
                                    //#012b458  LQ t1, $0010(t7)
                                    //#012b45c  LQ t2, $0020(t7)
                                    //#012b460  LQ t3, $0030(t7)
                                    //#012b464  SQ t0, $0000(t6)
                                    //#012b468  SQ t1, $0010(t6)
                                    //#012b46c  SQ t2, $0020(t6)
                                    //#012b470  SQ t3, $0030(t6)
                                    //#012b474  LW t4, $0004(s0)
                                    //#012b478  LHU t6, $0010(t4)
                                }
                                //#012b47c  SLT t7, s7, t6
                                //#012b480  BEQ t7, zero, $0012b64c
                                //#012b484  SUBU t7, s7, t6
                                if (true) {
                                    //#012b64c  LW t6, $0010(s0)
                                    //#012b650  BEQ zero, zero, $0012b490
                                    //#012b654  SLL t7, t7, 6
                                }
                                else {
                                    //#012b488  LW t6, $002c(s5)
                                    //#012b48c  SLL t7, s7, 6
                                }
                                //#012b490  ADDU t6, t6, t7
                                //#012b494  LW t7, $0014(s0)
                                //#012b498  ADDU t7, t7, s8
                                //#012b49c  ADDIU t7, t7, $0010
                                //#012b4a0  LQC2 vf4, $0000(t7)
                                //#012b4a4  LQC2 vf1, $0000(t6)
                                //#012b4a8  LQC2 vf2, $0010(t6)
                                //#012b4ac  LQC2 vf3, $0020(t6)
                                //#012b4b0  VMULx.xyzw vf1, vf1, vf4x
                                //#012b4b4  VMULy.xyzw vf2, vf2, vf4y
                                //#012b4b8  VMULz.xyzw vf3, vf3, vf4z
                                //#012b4bc  SQC2 vf1, $0000(t6)
                                //#012b4c0  SQC2 vf2, $0010(t6)
                                //#012b4c4  SQC2 vf3, $0020(t6)
                                //#012b4c8  LW t4, $0004(s0)
                                //#012b4cc  LHU t6, $0010(t4)
                                //#012b4d0  SLT t7, s7, t6
                                //#012b4d4  BEQ t7, zero, $0012b640
                                //#012b4d8  SUBU t7, s7, t6
                                if (true) {
                                    //#012b640  LW t6, $0010(s0)
                                    //#012b644  BEQ zero, zero, $0012b4e4
                                    //#012b648  SLL t7, t7, 6
                                }
                                else {
                                    //#012b4dc  LW t6, $002c(s5)
                                    //#012b4e0  SLL t7, s7, 6
                                }
                                //#012b4e4  LHU t5, $0010(t4)
                                //#012b4e8  ADDU t6, t6, t7
                                //#012b4ec  SLT t7, s2, t5
                                //#012b4f0  BEQ t7, zero, $0012b62c
                                //#012b4f4  ADDIU t3, t6, $0030
                                if (true) {
                                    //#012b62c  SUBU t7, s2, t5
                                    //#012b630  LW t6, $0010(s0)
                                    //#012b634  SLL t7, t7, 6
                                    //#012b638  BEQ zero, zero, $0012b500
                                    //#012b63c  ADDU v0, t6, t7
                                }
                                else {
                                    //#012b4f8  LW t7, $002c(s5)
                                    //#012b4fc  ADDU v0, t7, s3
                                }
                                //#012b500  LHU t6, $0010(t4)
                                //#012b504  SLT t7, s7, t6
                                //#012b508  BEQ t7, zero, $0012b620
                                //#012b50c  SUBU t7, s7, t6
                                if (true) {
                                    //#012b620  LW t6, $000c(s0)
                                    //#012b624  BEQ zero, zero, $0012b518
                                    //#012b628  SLL t7, t7, 6
                                }
                                else {
                                    //#012b510  LW t6, $0028(s5)
                                    //#012b514  SLL t7, s7, 6
                                }
                                //#012b518  ADDU t6, t6, t7
                                //#012b51c  ADDIU t7, t6, $0030
                                //#012b520  LQC2 vf5, $0000(t7)
                                //#012b524  LQC2 vf1, $0000(v0)
                                //#012b528  LQC2 vf2, $0010(v0)
                                //#012b52c  LQC2 vf3, $0020(v0)
                                //#012b530  LQC2 vf4, $0030(v0)
                                //#012b534  VMULAx.xyz ACC, vf1, vf5x
                                //#012b538  VMADDAy.xyz ACC, vf2, vf5y
                                //#012b53c  VMADDAz.xyz ACC, vf3, vf5z
                                //#012b540  VMADDw.xyz vf5, vf4, vf0w
                                //#012b544  VMOVE.w vf5, vf0
                                //#012b548  SQC2 vf5, $0000(t3)
                                //#012b54c  LW t6, $0220(sp)
                                //#012b550  LW t4, $0004(s0)
                                //#012b554  LHU a2, $0002(t6)
                                //#012b558  LHU t6, $0010(t4)
                                //#012b55c  ANDI t5, a2, $ffff
                                //#012b560  SLT t7, t5, t6
                                //#012b564  BEQ t7, zero, $0012b610
                                //#012b568  DADDU t3, t4, zero
                                if (true) {
                                    //#012b610  SUBU t7, t5, t6
                                    //#012b614  LW t6, $0010(s0)
                                    //#012b618  BEQ zero, zero, $0012b574
                                    //#012b61c  SLL t7, t7, 6
                                }
                                else {
                                    //#012b56c  LW t6, $002c(s5)
                                    //#012b570  SLL t7, t5, 6
                                }
                                //#012b574  ADDU t7, t6, t7
                                //#012b578  LHU t6, $0010(t3)
                                //#012b57c  ADDIU t2, t7, $0030
                                //#012b580  SLT t7, s7, t6
                                //#012b584  BEQ t7, zero, $0012b604
                                //#012b588  SUBU t7, s7, t6
                                if (true) {
                                    //#012b604  LW t6, $0010(s0)
                                    //#012b608  BEQ zero, zero, $0012b594
                                    //#012b60c  SLL t7, t7, 6
                                }
                                else {
                                    //#012b58c  LW t6, $002c(s5)
                                    //#012b590  SLL t7, s7, 6
                                }
                                //#012b594  LHU t5, $0010(t3)
                                //#012b598  ADDU t4, t6, t7
                                //#012b59c  ANDI a2, a2, $ffff
                                //#012b5a0  SLT t7, a2, t5
                                //#012b5a4  BEQ t7, zero, $0012b5e8
                                //#012b5a8  SLL t7, a2, 4
                                if (true) {
                                    //#012b5e8  LW t6, $0018(t3)
                                    //#012b5ec  SUBU t7, a2, t5
                                    //#012b5f0  SLL t7, t7, 6
                                    //#012b5f4  ADDU t6, t3, t6
                                    //#012b5f8  ADDU t6, t6, t7
                                    //#012b5fc  BEQ zero, zero, $0012b5b4
                                    //#012b600  ADDIU a2, t6, $0030
                                }
                                else {
                                    //#012b5ac  LW t6, $0024(s5)
                                    //#012b5b0  ADDU a2, t6, t7
                                }
                                //#012b5b4  LQC2 vf5, $0000(a2)
                                //#012b5b8  LQC2 vf1, $0000(t4)
                                //#012b5bc  LQC2 vf2, $0010(t4)
                                //#012b5c0  LQC2 vf3, $0020(t4)
                                //#012b5c4  LQC2 vf4, $0030(t4)
                                //#012b5c8  VMULAx.xyz ACC, vf1, vf5x
                                //#012b5cc  VMADDAy.xyz ACC, vf2, vf5y
                                //#012b5d0  VMADDAz.xyz ACC, vf3, vf5z
                                //#012b5d4  VMADDw.xyz vf5, vf4, vf0w
                                //#012b5d8  VMOVE.w vf5, vf0
                                //#012b5dc  SQC2 vf5, $0000(t2)
                                //#012b5e0  BEQ zero, zero, $00129e60
                                //#012b5e4  LW t3, $01f4(sp)
                            }
                        }
                    }
                    else {
                        //#0129e04  AND t7, s4, t7
                        //#0129e08  BEQ t7, zero, $00129e60
                        //#0129e0c  LW t3, $01f4(sp)
                        if (true) {
                            //#0129e10  LW t4, $0004(s0)
                            //#0129e14  LHU t6, $0010(t4)
                            //#0129e18  SLT t7, s2, t6
                            //#0129e1c  BEQ t7, zero, $0012a9bc
                            //#0129e20  SLL s3, s2, 6
                            if (true) {
                                //#012a9bc  SUBU t7, s2, t6
                                //#012a9c0  LW t6, $000c(s0)
                                //#012a9c4  SLL t7, t7, 6
                                //#012a9c8  BEQ zero, zero, $00129e2c
                                //#012a9cc  ADDU a0, t6, t7
                            }
                            else {
                                //#0129e24  LW t7, $0028(s5)
                                //#0129e28  ADDU a0, t7, s3
                            }
                            //#0129e2c  LHU t5, $0010(t4)
                            //#0129e30  SLT t7, s2, t5
                            //#0129e34  BNEL t7, zero, $0012a9b4
                            if (true) {
                                //#0129e38  LW t7, $0020(s5)
                                {
                                    fn012a9b4();
                                }
                            }
                            else {
                                //#0129e3c  LW t6, $0018(t4)
                                fn0129e40();
                            }
                        }
                    }
                    //#0129e60  BEQL t3, zero, $0012a498
                    if (true) {
                        //#0129e64  LW t3, $01fc(sp)
                    }
                    else {
                        //#0129e68  LHU t7, $0010(t3)
                        //#0129e6c  BNEL t7, s2, $0012a498
                        if (true) {
                            //#0129e70  LW t3, $01fc(sp)
                        }
                        else {
                            //#0129e74  LW t6, $0004(s0)
                            //#0129e78  LHU t6, $0010(t6)
                            //#0129e7c  SLT t7, s2, t6
                            //#0129e80  BEQ t7, zero, $0012a99c
                            //#0129e84  ADDIU s4, sp, $00d0
                            if (true) {
                                //#012a99c  SUBU t7, s2, t6
                                //#012a9a0  SLL s3, s2, 6
                                //#012a9a4  LW t6, $000c(s0)
                                //#012a9a8  SLL t7, t7, 6
                                //#012a9ac  BEQ zero, zero, $00129e94
                                //#012a9b0  ADDU t6, t6, t7
                            }
                            else {
                                //#0129e88  LW t7, $0028(s5)
                                //#0129e8c  SLL s3, s2, 6
                                //#0129e90  ADDU t6, t7, s3
                            }
                            //#0129e94  BEQ s4, t6, $00129ec0
                            //#0129e98  LW t4, $01f4(sp)
                            if (true) {
                            }
                            else {
                                //#0129e9c  LQ t0, $0000(t6)
                                //#0129ea0  LQ t1, $0010(t6)
                                //#0129ea4  LQ t2, $0020(t6)
                                //#0129ea8  LQ t3, $0030(t6)
                                //#0129eac  SQ t0, $0000(s4)
                                //#0129eb0  SQ t1, $0010(s4)
                                //#0129eb4  SQ t2, $0020(s4)
                                //#0129eb8  SQ t3, $0030(s4)
                                //#0129ebc  LW t4, $01f4(sp)
                            }
                            //#0129ec0  LUI t6, $0001
                            //#0129ec4  LD t7, $0010(t4)
                            //#0129ec8  AND t7, t7, t6
                            //#0129ecc  BNE t7, zero, $0012a6b4
                            //#0129ed0  LW t7, $0214(sp)
                            if (true) {
                                //#012a6b4  BLTZ t7, $0012a93c
                                //#012a6b8  ADDIU t8, sp, $0120
                                if (true) {
                                    //#012a93c  ADDIU t5, sp, $0080
                                    //#012a940  ADDIU t7, sp, $0100
                                    //#012a944  LQC2 vf5, $0000(t7)
                                    //#012a948  LQC2 vf1, $0000(sp)
                                    //#012a94c  LQC2 vf2, $0010(sp)
                                    //#012a950  LQC2 vf3, $0020(sp)
                                    //#012a954  LQC2 vf4, $0030(sp)
                                    //#012a958  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012a95c  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012a960  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012a964  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012a968  SQC2 vf5, $0000(t8)
                                    //#012a96c  BEQ t5, t8, $0012a980
                                    //#012a970  LW t3, $01f4(sp)
                                    if (true) {
                                    }
                                    else {
                                        //#012a974  LQ t0, $0000(t8)
                                        //#012a978  SQ t0, $0000(t5)
                                        //#012a97c  LW t3, $01f4(sp)
                                    }
                                    //#012a980  ADDIU t5, sp, $0110
                                    //#012a984  BEQL t5, t3, $0012a150
                                    if (true) {
                                        //#012a988  SW zero, $011c(sp)
                                    }
                                    else {
                                        //#012a98c  LQ t0, $0000(t3)
                                        //#012a990  SQ t0, $0000(t5)
                                        //#012a994  BEQ zero, zero, $0012a150
                                        //#012a998  SW zero, $011c(sp)
                                    }
                                }
                                else {
                                    //#012a6bc  LW t4, $0004(s0)
                                    //#012a6c0  LHU t6, $0010(t4)
                                    //#012a6c4  SLT t7, t7, t6
                                    //#012a6c8  BEQ t7, zero, $0012a928
                                    //#012a6cc  ADDIU s7, sp, $0040
                                    if (true) {
                                        //#012a928  LW t5, $0214(sp)
                                        //#012a92c  SUBU t7, t5, t6
                                        //#012a930  LW t6, $0010(s0)
                                        //#012a934  BEQ zero, zero, $0012a6dc
                                        //#012a938  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012a6d0  LW t3, $0214(sp)
                                        //#012a6d4  LW t6, $002c(s5)
                                        //#012a6d8  SLL t7, t3, 6
                                    }
                                    //#012a6dc  ADDU t6, t6, t7
                                    //#012a6e0  BEQL s7, t6, $0012a710
                                    if (true) {
                                        //#012a6e4  LHU t6, $0010(t4)
                                    }
                                    else {
                                        //#012a6e8  LQ t0, $0000(t6)
                                        //#012a6ec  LQ t1, $0010(t6)
                                        //#012a6f0  LQ t2, $0020(t6)
                                        //#012a6f4  LQ t3, $0030(t6)
                                        //#012a6f8  SQ t0, $0000(s7)
                                        //#012a6fc  SQ t1, $0010(s7)
                                        //#012a700  SQ t2, $0020(s7)
                                        //#012a704  SQ t3, $0030(s7)
                                        //#012a708  LW t4, $0004(s0)
                                        //#012a70c  LHU t6, $0010(t4)
                                    }
                                    //#012a710  LW t3, $0214(sp)
                                    //#012a714  SLT t7, t3, t6
                                    //#012a718  BEQ t7, zero, $0012a918
                                    //#012a71c  LW t4, $0214(sp)
                                    if (true) {
                                        //#012a918  SUBU t7, t4, t6
                                        //#012a91c  LW t6, $0010(s0)
                                        //#012a920  BEQ zero, zero, $0012a728
                                        //#012a924  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012a720  LW t6, $002c(s5)
                                        //#012a724  SLL t7, t3, 6
                                    }
                                    //#012a728  ADDIU t8, sp, $0120
                                    //#012a72c  ADDU a0, t6, t7
                                    //#012a730  SQ t8, $0290(sp)
                                    //#012a734  JAL $00129740
                                    //#012a738  DADDU a1, t8, zero
                                    //#012a73c  LW t6, $0004(s0)
                                    //#012a740  ADDIU t5, sp, $0080
                                    //#012a744  LQ t8, $0290(sp)
                                    //#012a748  LHU t6, $0010(t6)
                                    //#012a74c  LW t3, $0214(sp)
                                    //#012a750  SLT t7, t3, t6
                                    //#012a754  BEQ t7, zero, $0012a904
                                    //#012a758  DADDU a2, t8, zero
                                    if (true) {
                                        //#012a904  LW t4, $0214(sp)
                                        //#012a908  SUBU t7, t4, t6
                                        //#012a90c  LW t6, $0010(s0)
                                        //#012a910  BEQ zero, zero, $0012a764
                                        //#012a914  SLL t7, t7, 6
                                    }
                                    else {
                                        //#012a75c  LW t6, $002c(s5)
                                        //#012a760  SLL t7, t3, 6
                                    }
                                    //#012a764  ADDU t6, t6, t7
                                    //#012a768  ADDIU a3, sp, $0160
                                    //#012a76c  ADDIU t7, sp, $0100
                                    //#012a770  LQC2 vf5, $0000(t7)
                                    //#012a774  LQC2 vf1, $0000(t6)
                                    //#012a778  LQC2 vf2, $0010(t6)
                                    //#012a77c  LQC2 vf3, $0020(t6)
                                    //#012a780  LQC2 vf4, $0030(t6)
                                    //#012a784  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012a788  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012a78c  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012a790  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012a794  SQC2 vf5, $0000(a3)
                                    //#012a798  BEQ t5, a3, $0012a7a8
                                    //#012a79c  NOP 
                                    if (true) {
                                    }
                                    else {
                                        //#012a7a0  LQ t0, $0000(a3)
                                        //#012a7a4  SQ t0, $0000(t5)
                                    }
                                    //#012a7a8  LQC2 vf1, $0000(s7)
                                    //#012a7ac  LQC2 vf2, $0010(s7)
                                    //#012a7b0  LQC2 vf3, $0020(s7)
                                    //#012a7b4  LQC2 vf4, $0030(s7)
                                    //#012a7b8  VOPMULA.xyz ACC, vf2, vf3
                                    //#012a7bc  VOPMSUB.xyz vf5, vf3, vf2
                                    //#012a7c0  VOPMULA.xyz ACC, vf3, vf1
                                    //#012a7c4  VOPMSUB.xyz vf6, vf1, vf3
                                    //#012a7c8  VOPMULA.xyz ACC, vf1, vf2
                                    //#012a7cc  VOPMSUB.xyz vf7, vf2, vf1
                                    //#012a7d0  VMUL.xyz vf8, vf1, vf5
                                    //#012a7d4  VMUL.xyz vf1, vf4, vf5
                                    //#012a7d8  VMUL.xyz vf2, vf4, vf6
                                    //#012a7dc  VMUL.xyz vf3, vf4, vf7
                                    //#012a7e0  VADDy.x vf8, vf8, vf8y
                                    //#012a7e4  VADDy.x vf1, vf1, vf1y
                                    //#012a7e8  VADDx.y vf2, vf2, vf2x
                                    //#012a7ec  VADDx.z vf3, vf3, vf3x
                                    //#012a7f0  VADDz.x vf8, vf8, vf8z
                                    //#012a7f4  VADDz.x vf4, vf1, vf1z
                                    //#012a7f8  VADDz.y vf4, vf2, vf2z
                                    //#012a7fc  VADDy.z vf4, vf3, vf3y
                                    //#012a800  VDIV Q, vf0w, vf8x
                                    //#012a804  QMFC2 t0, vf5
                                    //#012a808  QMFC2 t1, vf6
                                    //#012a80c  QMFC2 t2, vf7
                                    //#012a810  QMFC2 t3, vf0
                                    //#012a814  PEXTLW t4, t1, t0
                                    //#012a818  PEXTUW t5, t1, t0
                                    //#012a81c  PEXTLW t6, t3, t2
                                    //#012a820  PEXTUW t7, t3, t2
                                    //#012a824  PCPYLD t0, t6, t4
                                    //#012a828  PCPYUD t1, t4, t6
                                    //#012a82c  PCPYLD t2, t7, t5
                                    //#012a830  QMTC2 t0, vf1
                                    //#012a834  QMTC2 t1, vf2
                                    //#012a838  QMTC2 t2, vf3
                                    //#012a83c  VSUB.xyz vf4, vf0, vf4
                                    //#012a840  VWAITQ 
                                    //#012a844  VMULq.xyzw vf1, vf1, Q
                                    //#012a848  VMULq.xyzw vf2, vf2, Q
                                    //#012a84c  VMULq.xyzw vf3, vf3, Q
                                    //#012a850  VMULq.xyz vf4, vf4, Q
                                    //#012a854  SQC2 vf1, $0000(s7)
                                    //#012a858  SQC2 vf2, $0010(s7)
                                    //#012a85c  SQC2 vf3, $0020(s7)
                                    //#012a860  SQC2 vf4, $0030(s7)
                                    //#012a864  ADDIU t5, sp, $0110
                                    //#012a868  LQC2 vf1, $0000(s7)
                                    //#012a86c  LQC2 vf2, $0010(s7)
                                    //#012a870  LQC2 vf3, $0020(s7)
                                    //#012a874  LQC2 vf4, $0030(s7)
                                    //#012a878  LQC2 vf5, $0000(a2)
                                    //#012a87c  LQC2 vf6, $0010(a2)
                                    //#012a880  LQC2 vf7, $0020(a2)
                                    //#012a884  LQC2 vf8, $0030(a2)
                                    //#012a888  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012a88c  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012a890  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012a894  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012a898  VMULAx.xyzw ACC, vf1, vf6x
                                    //#012a89c  VMADDAy.xyzw ACC, vf2, vf6y
                                    //#012a8a0  VMADDAz.xyzw ACC, vf3, vf6z
                                    //#012a8a4  VMADDw.xyzw vf6, vf4, vf6w
                                    //#012a8a8  VMULAx.xyzw ACC, vf1, vf7x
                                    //#012a8ac  VMADDAy.xyzw ACC, vf2, vf7y
                                    //#012a8b0  VMADDAz.xyzw ACC, vf3, vf7z
                                    //#012a8b4  VMADDw.xyzw vf7, vf4, vf7w
                                    //#012a8b8  VMULAx.xyzw ACC, vf1, vf8x
                                    //#012a8bc  VMADDAy.xyzw ACC, vf2, vf8y
                                    //#012a8c0  VMADDAz.xyzw ACC, vf3, vf8z
                                    //#012a8c4  VMADDw.xyzw vf8, vf4, vf8w
                                    //#012a8c8  SQC2 vf5, $0000(a3)
                                    //#012a8cc  SQC2 vf6, $0010(a3)
                                    //#012a8d0  SQC2 vf7, $0020(a3)
                                    //#012a8d4  SQC2 vf8, $0030(a3)
                                    //#012a8d8  LW t6, $01f4(sp)
                                    //#012a8dc  LQC2 vf4, $0000(t6)
                                    //#012a8e0  LQC2 vf1, $0000(a3)
                                    //#012a8e4  LQC2 vf2, $0010(a3)
                                    //#012a8e8  LQC2 vf3, $0020(a3)
                                    //#012a8ec  VMULAx.xyz ACC, vf1, vf4x
                                    //#012a8f0  VMADDAy.xyz ACC, vf2, vf4y
                                    //#012a8f4  VMADDz.xyz vf4, vf3, vf4z
                                    //#012a8f8  SQC2 vf4, $0000(t5)
                                    //#012a8fc  BEQ zero, zero, $0012a150
                                    //#012a900  SW zero, $011c(sp)
                                }
                            }
                            else {
                                //#0129ed4  LW t5, $0214(sp)
                                //#0129ed8  BLTZ t5, $0012a63c
                                //#0129edc  ADDIU a1, sp, $0040
                                if (true) {
                                    //#012a63c  LW t4, $01f0(sp)
                                    //#012a640  BEQ a1, t4, $0012a66c
                                    //#012a644  ADDIU t8, sp, $0120
                                    if (true) {
                                    }
                                    else {
                                        //#012a648  LQ t0, $0000(t4)
                                        //#012a64c  LQ t1, $0010(t4)
                                        //#012a650  LQ t2, $0020(t4)
                                        //#012a654  LQ t3, $0030(t4)
                                        //#012a658  SQ t0, $0000(a1)
                                        //#012a65c  SQ t1, $0010(a1)
                                        //#012a660  SQ t2, $0020(a1)
                                        //#012a664  SQ t3, $0030(a1)
                                        //#012a668  ADDIU t8, sp, $0120
                                    }
                                    //#012a66c  ADDIU t6, sp, $0080
                                    //#012a670  ADDIU a2, sp, $0100
                                    //#012a674  LQC2 vf5, $0000(a2)
                                    //#012a678  LQC2 vf1, $0000(sp)
                                    //#012a67c  LQC2 vf2, $0010(sp)
                                    //#012a680  LQC2 vf3, $0020(sp)
                                    //#012a684  LQC2 vf4, $0030(sp)
                                    //#012a688  VMULAx.xyzw ACC, vf1, vf5x
                                    //#012a68c  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#012a690  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#012a694  VMADDw.xyzw vf5, vf4, vf5w
                                    //#012a698  SQC2 vf5, $0000(t8)
                                    //#012a69c  BEQ t6, t8, $0012a004
                                    //#012a6a0  NOP 
                                    if (true) {
                                    }
                                    else {
                                        //#012a6a4  LQ t0, $0000(t8)
                                        //#012a6a8  SQ t0, $0000(t6)
                                        //#012a6ac  BEQ zero, zero, $0012a004
                                        //#012a6b0  NOP 
                                    }
                                }
                                else {
                                    //#0129ee0  LW t6, $0004(s0)
                                    //#0129ee4  LHU t6, $0010(t6)
                                    //#0129ee8  SLT t7, t5, t6
                                    //#0129eec  BEQ t7, zero, $0012a628
                                    //#0129ef0  DADDU t4, a1, zero
                                    if (true) {
                                        //#012a628  LW t3, $0214(sp)
                                        //#012a62c  SUBU t7, t3, t6
                                        //#012a630  LW t6, $0010(s0)
                                        //#012a634  BEQ zero, zero, $00129efc
                                        //#012a638  SLL t7, t7, 6
                                    }
                                    else {
                                        //#0129ef4  LW t6, $002c(s5)
                                        //#0129ef8  SLL t7, t5, 6
                                    }
                                    //#0129efc  ADDU t6, t6, t7
                                    //#0129f00  ADDIU t8, sp, $0120
                                    //#0129f04  LW t7, $01f0(sp)
                                    //#0129f08  DADDU t5, t8, zero
                                    //#0129f0c  LQC2 vf1, $0000(t7)
                                    //#0129f10  LQC2 vf2, $0010(t7)
                                    //#0129f14  LQC2 vf3, $0020(t7)
                                    //#0129f18  LQC2 vf4, $0030(t7)
                                    //#0129f1c  LQC2 vf5, $0000(t6)
                                    //#0129f20  LQC2 vf6, $0010(t6)
                                    //#0129f24  LQC2 vf7, $0020(t6)
                                    //#0129f28  LQC2 vf8, $0030(t6)
                                    //#0129f2c  VMULAx.xyzw ACC, vf1, vf5x
                                    //#0129f30  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#0129f34  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#0129f38  VMADDw.xyzw vf5, vf4, vf5w
                                    //#0129f3c  VMULAx.xyzw ACC, vf1, vf6x
                                    //#0129f40  VMADDAy.xyzw ACC, vf2, vf6y
                                    //#0129f44  VMADDAz.xyzw ACC, vf3, vf6z
                                    //#0129f48  VMADDw.xyzw vf6, vf4, vf6w
                                    //#0129f4c  VMULAx.xyzw ACC, vf1, vf7x
                                    //#0129f50  VMADDAy.xyzw ACC, vf2, vf7y
                                    //#0129f54  VMADDAz.xyzw ACC, vf3, vf7z
                                    //#0129f58  VMADDw.xyzw vf7, vf4, vf7w
                                    //#0129f5c  VMULAx.xyzw ACC, vf1, vf8x
                                    //#0129f60  VMADDAy.xyzw ACC, vf2, vf8y
                                    //#0129f64  VMADDAz.xyzw ACC, vf3, vf8z
                                    //#0129f68  VMADDw.xyzw vf8, vf4, vf8w
                                    //#0129f6c  SQC2 vf5, $0000(t8)
                                    //#0129f70  SQC2 vf6, $0010(t8)
                                    //#0129f74  SQC2 vf7, $0020(t8)
                                    //#0129f78  SQC2 vf8, $0030(t8)
                                    //#0129f7c  BEQL t4, t8, $00129fa8
                                    if (true) {
                                        //#0129f80  LW t6, $0004(s0)
                                    }
                                    else {
                                        //#0129f84  LQ t0, $0000(t8)
                                        //#0129f88  LQ t1, $0010(t8)
                                        //#0129f8c  LQ t2, $0020(t8)
                                        //#0129f90  LQ t3, $0030(t8)
                                        //#0129f94  SQ t0, $0000(t4)
                                        //#0129f98  SQ t1, $0010(t4)
                                        //#0129f9c  SQ t2, $0020(t4)
                                        //#0129fa0  SQ t3, $0030(t4)
                                        //#0129fa4  LW t6, $0004(s0)
                                    }
                                    //#0129fa8  LW t3, $0214(sp)
                                    //#0129fac  LHU t6, $0010(t6)
                                    //#0129fb0  SLT t7, t3, t6
                                    //#0129fb4  BEQ t7, zero, $0012a618
                                    //#0129fb8  ADDIU t4, sp, $0080
                                    if (true) {
                                        //#012a618  SUBU t7, t3, t6
                                        //#012a61c  LW t6, $0010(s0)
                                        //#012a620  BEQ zero, zero, $00129fc4
                                        //#012a624  SLL t7, t7, 6
                                    }
                                    else {
                                        //#0129fbc  LW t6, $002c(s5)
                                        //#0129fc0  SLL t7, t3, 6
                                    }
                                    //#0129fc4  ADDU t6, t6, t7
                                    //#0129fc8  ADDIU a2, sp, $0100
                                    //#0129fcc  LQC2 vf5, $0000(a2)
                                    //#0129fd0  LQC2 vf1, $0000(t6)
                                    //#0129fd4  LQC2 vf2, $0010(t6)
                                    //#0129fd8  LQC2 vf3, $0020(t6)
                                    //#0129fdc  LQC2 vf4, $0030(t6)
                                    //#0129fe0  VMULAx.xyzw ACC, vf1, vf5x
                                    //#0129fe4  VMADDAy.xyzw ACC, vf2, vf5y
                                    //#0129fe8  VMADDAz.xyzw ACC, vf3, vf5z
                                    //#0129fec  VMADDw.xyzw vf5, vf4, vf5w
                                    //#0129ff0  SQC2 vf5, $0000(t5)
                                    //#0129ff4  BEQ t4, t5, $0012a004
                                    //#0129ff8  NOP 
                                    if (true) {
                                    }
                                    else {
                                        //#0129ffc  LQ t0, $0000(t5)
                                        //#012a000  SQ t0, $0000(t4)
                                    }
                                }
                                //#012a004  LQC2 vf1, $0000(a1)
                                //#012a008  LQC2 vf2, $0010(a1)
                                //#012a00c  LQC2 vf3, $0020(a1)
                                //#012a010  LQC2 vf4, $0030(a1)
                                //#012a014  VOPMULA.xyz ACC, vf2, vf3
                                //#012a018  VOPMSUB.xyz vf5, vf3, vf2
                                //#012a01c  VOPMULA.xyz ACC, vf3, vf1
                                //#012a020  VOPMSUB.xyz vf6, vf1, vf3
                                //#012a024  VOPMULA.xyz ACC, vf1, vf2
                                //#012a028  VOPMSUB.xyz vf7, vf2, vf1
                                //#012a02c  VMUL.xyz vf8, vf1, vf5
                                //#012a030  VMUL.xyz vf1, vf4, vf5
                                //#012a034  VMUL.xyz vf2, vf4, vf6
                                //#012a038  VMUL.xyz vf3, vf4, vf7
                                //#012a03c  VADDy.x vf8, vf8, vf8y
                                //#012a040  VADDy.x vf1, vf1, vf1y
                                //#012a044  VADDx.y vf2, vf2, vf2x
                                //#012a048  VADDx.z vf3, vf3, vf3x
                                //#012a04c  VADDz.x vf8, vf8, vf8z
                                //#012a050  VADDz.x vf4, vf1, vf1z
                                //#012a054  VADDz.y vf4, vf2, vf2z
                                //#012a058  VADDy.z vf4, vf3, vf3y
                                //#012a05c  VDIV Q, vf0w, vf8x
                                //#012a060  QMFC2 t0, vf5
                                //#012a064  QMFC2 t1, vf6
                                //#012a068  QMFC2 t2, vf7
                                //#012a06c  QMFC2 t3, vf0
                                //#012a070  PEXTLW t4, t1, t0
                                //#012a074  PEXTUW t5, t1, t0
                                //#012a078  PEXTLW t6, t3, t2
                                //#012a07c  PEXTUW t7, t3, t2
                                //#012a080  PCPYLD t0, t6, t4
                                //#012a084  PCPYUD t1, t4, t6
                                //#012a088  PCPYLD t2, t7, t5
                                //#012a08c  QMTC2 t0, vf1
                                //#012a090  QMTC2 t1, vf2
                                //#012a094  QMTC2 t2, vf3
                                //#012a098  VSUB.xyz vf4, vf0, vf4
                                //#012a09c  VWAITQ 
                                //#012a0a0  VMULq.xyzw vf1, vf1, Q
                                //#012a0a4  VMULq.xyzw vf2, vf2, Q
                                //#012a0a8  VMULq.xyzw vf3, vf3, Q
                                //#012a0ac  VMULq.xyz vf4, vf4, Q
                                //#012a0b0  SQC2 vf1, $0000(a1)
                                //#012a0b4  SQC2 vf2, $0010(a1)
                                //#012a0b8  SQC2 vf3, $0020(a1)
                                //#012a0bc  SQC2 vf4, $0030(a1)
                                //#012a0c0  ADDIU a3, sp, $00b0
                                //#012a0c4  LW t6, $01f4(sp)
                                //#012a0c8  LQC2 vf5, $0000(t6)
                                //#012a0cc  LQC2 vf1, $0000(a1)
                                //#012a0d0  LQC2 vf2, $0010(a1)
                                //#012a0d4  LQC2 vf3, $0020(a1)
                                //#012a0d8  LQC2 vf4, $0030(a1)
                                //#012a0dc  VMULAx.xyz ACC, vf1, vf5x
                                //#012a0e0  VMADDAy.xyz ACC, vf2, vf5y
                                //#012a0e4  VMADDAz.xyz ACC, vf3, vf5z
                                //#012a0e8  VMADDw.xyz vf5, vf4, vf0w
                                //#012a0ec  VMOVE.w vf5, vf0
                                //#012a0f0  SQC2 vf5, $0000(a3)
                                //#012a0f4  ADDIU t5, sp, $0110
                                //#012a0f8  LQC2 vf1, $0000(a3)
                                //#012a0fc  LQC2 vf2, $0000(a2)
                                //#012a100  VSUB.xyzw vf1, vf1, vf2
                                //#012a104  SQC2 vf1, $0000(t8)
                                //#012a108  BEQ t5, t8, $0012a118
                                //#012a10c  NOP 
                                if (true) {
                                }
                                else {
                                    //#012a110  LQ t0, $0000(t8)
                                    //#012a114  SQ t0, $0000(t5)
                                }
                                //#012a118  LQC2 vf1, $0000(t5)
                                //#012a11c  VMUL.xyz vf2, vf1, vf1
                                //#012a120  VADDy.x vf2, vf2, vf2y
                                //#012a124  VADDz.x vf2, vf2, vf2z
                                //#012a128  VSQRT Q, vf2x
                                //#012a12c  VWAITQ 
                                //#012a130  CFC2 t0, $vi22
                                //#012a134  VADDq.x vf2, vf0, Q
                                //#012a138  MTC1 t0, $f0
                                //#012a13c  VNOP 
                                //#012a140  VDIV Q, vf0w, vf2x
                                //#012a144  VWAITQ 
                                //#012a148  VMULq.xyz vf1, vf1, Q
                                //#012a14c  SQC2 vf1, $0000(t5)
                            }
                            //#012a150  LW t4, $01f4(sp)
                            //#012a154  ADDIU t6, sp, $00e0
                            //#012a158  LWC1 $f0, $000c(t4)
                            //#012a15c  LQC2 vf1, $0000(t6)
                            //#012a160  LQC2 vf2, $0000(t5)
                            //#012a164  MFC1 t0, $f0
                            //#012a168  QMTC2 t0, vf3
                            //#012a16c  VADDw.x vf4, vf0, vf0w
                            //#012a170  VSUB.x vf4, vf4, vf3
                            //#012a174  VMULAx.xyzw ACC, vf2, vf3x
                            //#012a178  VMADDx.xyzw vf1, vf1, vf4x
                            //#012a17c  SQC2 vf1, $0000(t8)
                            //#012a180  BEQ t6, t8, $0012a190 
                            //#012a184  NOP 
                            if (true) {
                            }
                            else {
                                //#012a188  LQ t0, $0000(t8)
                                //#012a18c  SQ t0, $0000(t6)
                            }
                            //#012a190  LQC2 vf1, $0000(t6)
                            //#012a194  VMUL.xyz vf2, vf1, vf1
                            //#012a198  VADDy.x vf2, vf2, vf2y
                            //#012a19c  VADDz.x vf2, vf2, vf2z
                            //#012a1a0  VSQRT Q, vf2x
                            //#012a1a4  VWAITQ 
                            //#012a1a8  CFC2 t0, $vi22
                            //#012a1ac  VADDq.x vf2, vf0, Q
                            //#012a1b0  MTC1 t0, $f0
                            //#012a1b4  VNOP 
                            //#012a1b8  VDIV Q, vf0w, vf2x
                            //#012a1bc  VWAITQ 
                            //#012a1c0  VMULq.xyz vf1, vf1, Q
                            //#012a1c4  SQC2 vf1, $0000(t6)
                            //#012a1c8  ADDIU t7, sp, $00f0
                            //#012a1cc  LQC2 vf1, $0000(s4)
                            //#012a1d0  LQC2 vf2, $0000(t6)
                            //#012a1d4  VSUB.xyzw vf3, vf0, vf0
                            //#012a1d8  VOPMULA.xyz ACC, vf1, vf2
                            //#012a1dc  VOPMSUB.xyz vf3, vf2, vf1
                            //#012a1e0  SQC2 vf3, $0000(t7)
                            //#012a1e4  LQC2 vf1, $0000(t7)
                            //#012a1e8  VMUL.xyz vf2, vf1, vf1
                            //#012a1ec  VADDy.x vf2, vf2, vf2y
                            //#012a1f0  VADDz.x vf2, vf2, vf2z
                            //#012a1f4  VSQRT Q, vf2x
                            //#012a1f8  VWAITQ 
                            //#012a1fc  CFC2 t0, $vi22
                            //#012a200  VADDq.x vf2, vf0, Q
                            //#012a204  MTC1 t0, $f0
                            //#012a208  VNOP 
                            //#012a20c  VDIV Q, vf0w, vf2x
                            //#012a210  VWAITQ 
                            //#012a214  VMULq.xyz vf1, vf1, Q
                            //#012a218  SQC2 vf1, $0000(t7)
                            //#012a21c  LQC2 vf1, $0000(t6)
                            //#012a220  LQC2 vf2, $0000(t7)
                            //#012a224  VSUB.xyzw vf3, vf0, vf0
                            //#012a228  VOPMULA.xyz ACC, vf1, vf2
                            //#012a22c  VOPMSUB.xyz vf3, vf2, vf1
                            //#012a230  SQC2 vf3, $0000(s4)
                            //#012a234  LQC2 vf1, $0000(s4)
                            //#012a238  VMUL.xyz vf2, vf1, vf1
                            //#012a23c  VADDy.x vf2, vf2, vf2y
                            //#012a240  VADDz.x vf2, vf2, vf2z
                            //#012a244  VSQRT Q, vf2x
                            //#012a248  VWAITQ 
                            //#012a24c  CFC2 t0, $vi22
                            //#012a250  VADDq.x vf2, vf0, Q
                            //#012a254  MTC1 t0, $f0
                            //#012a258  VNOP 
                            //#012a25c  VDIV Q, vf0w, vf2x
                            //#012a260  VWAITQ 
                            //#012a264  VMULq.xyz vf1, vf1, Q
                            //#012a268  SQC2 vf1, $0000(s4)
                            //#012a26c  LQC2 vf1, $0000(s4)
                            //#012a270  LQC2 vf2, $0000(t6)
                            //#012a274  VSUB.xyzw vf3, vf0, vf0
                            //#012a278  VOPMULA.xyz ACC, vf1, vf2
                            //#012a27c  VOPMSUB.xyz vf3, vf2, vf1
                            //#012a280  SQC2 vf3, $0000(t7)
                            //#012a284  LQC2 vf1, $0000(t7)
                            //#012a288  VMUL.xyz vf2, vf1, vf1
                            //#012a28c  VADDy.x vf2, vf2, vf2y
                            //#012a290  VADDz.x vf2, vf2, vf2z
                            //#012a294  VSQRT Q, vf2x
                            //#012a298  VWAITQ 
                            //#012a29c  CFC2 t0, $vi22
                            //#012a2a0  VADDq.x vf2, vf0, Q
                            //#012a2a4  MTC1 t0, $f0
                            //#012a2a8  VNOP 
                            //#012a2ac  VDIV Q, vf0w, vf2x
                            //#012a2b0  VWAITQ 
                            //#012a2b4  VMULq.xyz vf1, vf1, Q
                            //#012a2b8  SQC2 vf1, $0000(t7)
                            //#012a2bc  LW t4, $0004(s0)
                            //#012a2c0  LHU t6, $0010(t4)
                            //#012a2c4  SLT t7, s2, t6
                            //#012a2c8  BEQ t7, zero, $0012a608
                            //#012a2cc  SUBU t7, s2, t6
                            if (true) {
                                //#012a608  LW t6, $000c(s0)
                                //#012a60c  SLL t7, t7, 6
                                //#012a610  BEQ zero, zero, $0012a2d8
                                //#012a614  ADDU t6, t6, t7
                            }
                            else {
                                //#012a2d0  LW t7, $0028(s5)
                                //#012a2d4  ADDU t6, t7, s3
                            }
                            //#012a2d8  BEQ t6, s4, $0012a308
                            //#012a2dc  LW t5, $0214(sp)
                            if (true) {
                            }
                            else {
                                //#012a2e0  LQ t0, $0000(s4)
                                //#012a2e4  LQ t1, $0010(s4)
                                //#012a2e8  LQ t2, $0020(s4)
                                //#012a2ec  LQ t3, $0030(s4)
                                //#012a2f0  SQ t0, $0000(t6)
                                //#012a2f4  SQ t1, $0010(t6)
                                //#012a2f8  SQ t2, $0020(t6)
                                //#012a2fc  SQ t3, $0030(t6)
                                //#012a300  LW t4, $0004(s0)
                                //#012a304  LW t5, $0214(sp)
                            }
                            //#012a308  BLTZ t5, $0012a534
                            //#012a30c  SLL t7, t5, 6
                            if (true) {
                                //#012a534  LHU t4, $0010(t4)
                                //#012a538  DADDU t3, sp, zero
                                //#012a53c  LW t7, $0008(s0)
                                //#012a540  SLT t6, s2, t4
                                //#012a544  BEQ t6, zero, $0012a5f4
                                //#012a548  ADDU t5, t7, s3
                                if (true) {
                                    //#012a5f4  SUBU t7, s2, t4
                                    //#012a5f8  LW t6, $000c(s0)
                                    //#012a5fc  SLL t7, t7, 6
                                    //#012a600  BEQ zero, zero, $0012a554
                                    //#012a604  ADDU t6, t6, t7
                                }
                                else {
                                    //#012a54c  LW t7, $0028(s5)
                                    //#012a550  ADDU t6, t7, s3
                                }
                                //#012a554  LQC2 vf1, $0000(t3)
                                //#012a558  LQC2 vf2, $0010(t3)
                                //#012a55c  LQC2 vf3, $0020(t3)
                                //#012a560  LQC2 vf4, $0030(t3)
                                //#012a564  LQC2 vf5, $0000(t6)
                                //#012a568  LQC2 vf6, $0010(t6)
                                //#012a56c  LQC2 vf7, $0020(t6)
                                //#012a570  LQC2 vf8, $0030(t6)
                                //#012a574  VMULAx.xyzw ACC, vf1, vf5x
                                //#012a578  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012a57c  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012a580  VMADDw.xyzw vf5, vf4, vf5w
                                //#012a584  VMULAx.xyzw ACC, vf1, vf6x
                                //#012a588  VMADDAy.xyzw ACC, vf2, vf6y
                                //#012a58c  VMADDAz.xyzw ACC, vf3, vf6z
                                //#012a590  VMADDw.xyzw vf6, vf4, vf6w
                                //#012a594  VMULAx.xyzw ACC, vf1, vf7x
                                //#012a598  VMADDAy.xyzw ACC, vf2, vf7y
                                //#012a59c  VMADDAz.xyzw ACC, vf3, vf7z
                                //#012a5a0  VMADDw.xyzw vf7, vf4, vf7w
                                //#012a5a4  VMULAx.xyzw ACC, vf1, vf8x
                                //#012a5a8  VMADDAy.xyzw ACC, vf2, vf8y
                                //#012a5ac  VMADDAz.xyzw ACC, vf3, vf8z
                                //#012a5b0  VMADDw.xyzw vf8, vf4, vf8w
                                //#012a5b4  SQC2 vf5, $0000(t8)
                                //#012a5b8  SQC2 vf6, $0010(t8)
                                //#012a5bc  SQC2 vf7, $0020(t8)
                                //#012a5c0  SQC2 vf8, $0030(t8)
                                //#012a5c4  BEQL t5, t8, $0012a3cc
                                //#012a5c8  LW t4, $0004(s0)
                                //#012a5cc  LQ t0, $0000(t8)
                                //#012a5d0  LQ t1, $0010(t8)
                                //#012a5d4  LQ t2, $0020(t8)
                                //#012a5d8  LQ t3, $0030(t8)
                                //#012a5dc  SQ t0, $0000(t5)
                                //#012a5e0  SQ t1, $0010(t5)
                                //#012a5e4  SQ t2, $0020(t5)
                                //#012a5e8  SQ t3, $0030(t5)
                                //#012a5ec  BEQ zero, zero, $0012a3cc
                                //#012a5f0  LW t4, $0004(s0)
                            }
                            else {
                                //#012a310  LHU t3, $0010(t4)
                                //#012a314  LW t6, $0008(s0)
                                //#012a318  SLT t5, s2, t3
                                //#012a31c  ADDU v0, t6, t7
                                //#012a320  BEQ t5, zero, $0012a520
                                //#012a324  ADDU t4, t6, s3
                                if (true) {
                                    //#012a520  SUBU t7, s2, t3
                                    //#012a524  LW t6, $000c(s0)
                                    //#012a528  SLL t7, t7, 6
                                    //#012a52c  BEQ zero, zero, $0012a330
                                    //#012a530  ADDU t6, t6, t7
                                }
                                else {
                                    //#012a328  LW t7, $0028(s5)
                                    //#012a32c  ADDU t6, t7, s3
                                }
                                //#012a330  LQC2 vf1, $0000(v0)
                                //#012a334  LQC2 vf2, $0010(v0)
                                //#012a338  LQC2 vf3, $0020(v0)
                                //#012a33c  LQC2 vf4, $0030(v0)
                                //#012a340  LQC2 vf5, $0000(t6)
                                //#012a344  LQC2 vf6, $0010(t6)
                                //#012a348  LQC2 vf7, $0020(t6)
                                //#012a34c  LQC2 vf8, $0030(t6)
                                //#012a350  VMULAx.xyzw ACC, vf1, vf5x
                                //#012a354  VMADDAy.xyzw ACC, vf2, vf5y
                                //#012a358  VMADDAz.xyzw ACC, vf3, vf5z
                                //#012a35c  VMADDw.xyzw vf5, vf4, vf5w
                                //#012a360  VMULAx.xyzw ACC, vf1, vf6x
                                //#012a364  VMADDAy.xyzw ACC, vf2, vf6y
                                //#012a368  VMADDAz.xyzw ACC, vf3, vf6z
                                //#012a36c  VMADDw.xyzw vf6, vf4, vf6w
                                //#012a370  VMULAx.xyzw ACC, vf1, vf7x
                                //#012a374  VMADDAy.xyzw ACC, vf2, vf7y
                                //#012a378  VMADDAz.xyzw ACC, vf3, vf7z
                                //#012a37c  VMADDw.xyzw vf7, vf4, vf7w
                                //#012a380  VMULAx.xyzw ACC, vf1, vf8x
                                //#012a384  VMADDAy.xyzw ACC, vf2, vf8y
                                //#012a388  VMADDAz.xyzw ACC, vf3, vf8z
                                //#012a38c  VMADDw.xyzw vf8, vf4, vf8w
                                //#012a390  SQC2 vf5, $0000(t8)
                                //#012a394  SQC2 vf6, $0010(t8)
                                //#012a398  SQC2 vf7, $0020(t8)
                                //#012a39c  SQC2 vf8, $0030(t8)
                                //#012a3a0  BEQL t4, t8, $0012a3cc
                                if (true) {
                                    //#012a3a4  LW t4, $0004(s0)
                                }
                                else {
                                    //#012a3a8  LQ t0, $0000(t8)
                                    //#012a3ac  LQ t1, $0010(t8)
                                    //#012a3b0  LQ t2, $0020(t8)
                                    //#012a3b4  LQ t3, $0030(t8)
                                    //#012a3b8  SQ t0, $0000(t4)
                                    //#012a3bc  SQ t1, $0010(t4)
                                    //#012a3c0  SQ t2, $0020(t4)
                                    //#012a3c4  SQ t3, $0030(t4)
                                    //#012a3c8  LW t4, $0004(s0)
                                }
                            }
                            //#012a3cc  LHU t6, $0010(t4)
                            //#012a3d0  SLT t7, s2, t6
                            //#012a3d4  BEQ t7, zero, $0012a510
                            //#012a3d8  SUBU t7, s2, t6
                            if (true) {
                                //#012a510  LW t6, $0010(s0)
                                //#012a514  SLL t7, t7, 6
                                //#012a518  BEQ zero, zero, $0012a3e4
                                //#012a51c  ADDU t6, t6, t7
                            }
                            else {
                                //#012a3dc  LW t7, $002c(s5)
                                //#012a3e0  ADDU t6, t7, s3
                            }
                            //#012a3e4  LW t7, $0008(s0)
                            //#012a3e8  ADDU t7, t7, s3
                            //#012a3ec  BEQL t6, t7, $0012a41c
                            if (true) {
                                //#012a3f0  LHU t6, $0010(t4)
                            }
                            else {
                                //#012a3f4  LQ t0, $0000(t7)
                                //#012a3f8  LQ t1, $0010(t7)
                                //#012a3fc  LQ t2, $0020(t7)
                                //#012a400  LQ t3, $0030(t7)
                                //#012a404  SQ t0, $0000(t6)
                                //#012a408  SQ t1, $0010(t6)
                                //#012a40c  SQ t2, $0020(t6)
                                //#012a410  SQ t3, $0030(t6)
                                //#012a414  LW t4, $0004(s0)
                                //#012a418  LHU t6, $0010(t4)
                            }
                            //#012a41c  SLT t7, s2, t6
                            //#012a420  BEQ t7, zero, $0012a500
                            //#012a424  SUBU t7, s2, t6
                            if (true) {
                                //#012a500  LW t6, $0010(s0)
                                //#012a504  SLL t7, t7, 6
                                //#012a508  BEQ zero, zero, $0012a430
                                //#012a50c  ADDU t6, t6, t7
                            }
                            else {
                                //#012a428  LW t7, $002c(s5)
                                //#012a42c  ADDU t6, t7, s3
                            }
                            //#012a430  LW t7, $0014(s0)
                            //#012a434  ADDU t7, t7, s8
                            //#012a438  LQC2 vf4, $0000(t7)
                            //#012a43c  LQC2 vf1, $0000(t6)
                            //#012a440  LQC2 vf2, $0010(t6)
                            //#012a444  LQC2 vf3, $0020(t6)
                            //#012a448  VMULx.xyzw vf1, vf1, vf4x
                            //#012a44c  VMULy.xyzw vf2, vf2, vf4y
                            //#012a450  VMULz.xyzw vf3, vf3, vf4z
                            //#012a454  SQC2 vf1, $0000(t6)
                            //#012a458  SQC2 vf2, $0010(t6)
                            //#012a45c  SQC2 vf3, $0020(t6)
                            //#012a460  LW t6, $0004(s0)
                            //#012a464  LHU t5, $0010(t6)
                            //#012a468  SLT t7, s2, t5
                            //#012a46c  BEQ t7, zero, $0012a4f0
                            //#012a470  SUBU t7, s2, t5
                            if (true) {
                                //#012a4f0  LW t6, $0010(s0)
                                //#012a4f4  SLL t7, t7, 6
                                //#012a4f8  BEQ zero, zero, $0012a47c
                                //#012a4fc  ADDU v0, t6, t7
                            }
                            else {
                                //#012a474  LW t7, $002c(s5)
                                //#012a478  ADDU v0, t7, s3
                            }
                            //#012a47c  ADDIU v0, v0, $0030
                            //#012a480  ADDIU t6, sp, $0080
                            //#012a484  BEQ v0, t6, $0012a498
                            //#012a488  LW t3, $01fc(sp)
                            if (true) {
                            }
                            else {
                                //#012a48c  LQ t0, $0000(t6)
                                //#012a490  SQ t0, $0000(v0)
                                //#012a494  LW t3, $01fc(sp)
                            }
                        }
                    }
                    //#012a498  LW t4, $0210(sp)
                    //#012a49c  LW t5, $0200(sp)
                    //#012a4a0  LHU t7, $0012(t3)
                    //#012a4a4  ADDIU t4, t4, $0001
                    //#012a4a8  ADDIU t5, t5, $0004
                    //#012a4ac  SW t4, $0210(sp)
                    //#012a4b0  SLT t7, t4, t7
                    //#012a4b4  BNE t7, zero, $00129ad8
                    //#012a4b8  SW t5, $0200(sp)
                    if (true) {
                        continue;
                    }
                    break;
                }
                //#012a4bc  LD s0, $02a0(sp)
            }
            //#012a4c0  LD s1, $02a8(sp)
            //#012a4c4  LD s2, $02b0(sp)
            //#012a4c8  LD s3, $02b8(sp)
            //#012a4cc  LD s4, $02c0(sp)
            //#012a4d0  LD s5, $02c8(sp)
            //#012a4d4  LD s6, $02d0(sp)
            //#012a4d8  LD s7, $02d8(sp)
            //#012a4dc  LD s8, $02e0(sp)
            //#012a4e0  LD ra, $02e8(sp)
            //#012a4e4  LWC1 $f20, $02f0(sp)
            //#012a4e8  JR ra
            //#012a4ec  ADDIU sp, sp, $0300
            //{}E_CALCB
        }

        float f0 = 0, f20 = 0;

        /// <summary>
        /// a0 = offMset
        /// </summary>
        private void fn128e40() {
            //#0128e40  ADDIU sp, sp, $ff90
            sp -= 112;
            //#0128e44  MTC1 zero, $f0
            f0 = 0;
            //#0128e48  SD s2, $0030(sp)
            int sp0030 = s2;
            //#0128e4c  ANDI a2, a2, $ffff
            a2 &= 0xffff;
            //#0128e50  SD s4, $0040(sp)
            int sp0040 = s4;
            //#0128e54  SD s0, $0020(sp)
            int sp0020 = s0;
            //#0128e58  DADDU s2, a0, zero
            s2 = a0;
            //#0128e5c  SD s1, $0028(sp)
            int sp0028 = s1;
            //#0128e60  SD s3, $0038(sp)
            int sp0038 = s3;
            //#0128e64  SD s5, $0048(sp)
            int sp0048 = s5;
            //#0128e68  SD s6, $0050(sp)
            int sp0050 = s6;
            //#0128e6c  SD ra, $0058(sp)
            int sp0058 = ra;
            //#0128e70  SWC1 $f20, $0060(sp)
            float sp0060 = f20;
            //#0128e74  LW s0, $0004(a0)
            //#0128e78  LW t4, $006c(s0)
            //#0128e7c  SLTU t7, a2, t4
            //#0128e80  BEQ t7, zero, $00128ee8
            //#0128e84  DADDU s4, a1, zero
            //#0128e88  ADDIU t7, zero, $000c
            //#0128e8c  LW t6, $0068(s0)
            //#0128e90  MULT t7, a2, t7
            //#0128e94  ADDU s1, s0, t6
            //#0128e98  ADDU s3, s1, t7
            //#0128e9c  LBU t3, $0000(s3)
            //#0128ea0  ANDI t6, t3, $00ff
            //#0128ea4  SLTIU t7, t6, $002c
            //#0128ea8  BEQ t7, zero, $00128ee8
            //#0128eac  SLL t7, t6, 2
            //#0128eb0  LUI t6, $0038
            //#0128eb4  ADDIU t6, t6, $82a0
            //#0128eb8  ADDU t7, t7, t6
            //#0128ebc  LW t5, $0000(t7)
            //#0128ec0  JR t5
            //#0128ec4  NOP 
            //#0128ec8  JAL $00128e40
            //#0128ecc  LHU a2, $0008(s3)
            //#0128ed0  LUI t7, $0038
            //#0128ed4  ADDIU t7, t7, $8290
            //#0128ed8  LWC1 $f1, $0000(t7)
            //#0128edc  MUL.S $f0, $f0, $f1
            //#0128ee0  JAL $002ff750
            //#0128ee4  MOV.S $f12, $f0
            //#0128ee8  LD s0, $0020(sp)
            //#0128eec  LD s1, $0028(sp)
            //#0128ef0  LD s2, $0030(sp)
            //#0128ef4  LD s3, $0038(sp)
            //#0128ef8  LD s4, $0040(sp)
            //#0128efc  LD s5, $0048(sp)
            //#0128f00  LD s6, $0050(sp)
            //#0128f04  LD ra, $0058(sp)
            //#0128f08  LWC1 $f20, $0060(sp)
            //#0128f0c  JR ra
            //#0128f10  ADDIU sp, sp, $0070
            sp += 112;
        }

        int Based(int readoff, int varoff, int varcnt, int unitSize) {
            int reloff = readoff - varoff;
            Debug.Assert((reloff % unitSize) == 0);
            int x = reloff / unitSize;
            Debug.Assert((uint)x < (uint)varcnt);
            return x;
        }

        /// <summary>
        /// Load identity matrix
        /// </summary>
        private void fn011b420() {
            //#011b420  LUI t7, $0035
            //#011b424  ADDIU t7, t7, $4260
            //#011b428  LQ t0, $0000(t7)
            //#011b42c  LQ t1, $0010(t7)
            //#011b430  LQ t2, $0020(t7)
            //#011b434  LQ t3, $0030(t7)
            //#011b438  SQ t0, $0000(a0)
            //#011b43c  SQ t1, $0010(a0)
            //#011b440  SQ t2, $0020(a0)
            //#011b444  SQ t3, $0030(a0)
            //#011b448  JR ra
        }

        private void fn0129b94() {
            //#0129b94  SLL t7, t7, 2
            //#0129b98  ADDU t7, t6, t7
            //#0129b9c  SWC1 $f1, $0000(t7)
        }

        private bool test012c738_then_012c74c_else_012c8d0() {
            //#012c738  BNEL a2, t7, $0012c74c
            if (true) {
                //#012c73c  SQ v0, $0260(sp)
                return true;
            }
            //#012c740  BEQL a3, a2, $0012c8d0
            if (true) {
                //#012c744  LW t5, $0004(s0)
                return false;
            }
            //#012c748  SQ v0, $0260(sp)
            return true;
        }

        private bool test012ba0c_then_012ba40_else_012bb34() {
            //#012ba0c  BNE a2, t7, $0012ba40
            //#012ba10  LW t4, $0214(sp)
            if (true) {
                return true;
            }
            //#012ba14  BNE a3, a2, $0012ba40
            //#012ba18  LW t7, $0218(sp)
            if (true) {
                return true;
            }
            //#012ba1c  BNE t7, zero, $0012ba40
            //#012ba20  LUI t7, $000c
            if (true) {
                return true;
            }
            //#012ba24  AND t7, s4, t7
            //#012ba28  BNE t7, zero, $0012ba40
            //#012ba2c  NOP 
            if (true) {
                return true;
            }
            //#012ba30  BNE v0, zero, $0012ba40
            //#012ba34  LW t3, $021c(sp)
            if (true) {
                return true;
            }
            //#012ba38  BEQ t3, zero, $0012bb34
            //#012ba3c  LUI t7, $0004
            if (true) {
                return false;
            }
            return true;
        }

        private void fn012a9b4() {
            //#012a9b4  BEQ zero, zero, $00129e54
            //#012a9b8  ADDU a1, t7, s8
            {
                fn0129e54();
            }
        }

        private void fn0129e40() {
            //#0129e40  SUBU t7, s2, t5
            //#0129e44  SLL t7, t7, 6
            //#0129e48  ADDU t6, t4, t6
            //#0129e4c  ADDU t6, t6, t7
            //#0129e50  ADDIU a1, t6, $0020
            fn0129e54();
        }

        private void fn0129e54() {
            //#0129e54  JAL $0011b840
            //#0129e58  NOP 
            //#0129e5c  LW t3, $01f4(sp)
        }

        private bool test012bcc8_then_012bce0_else_0129e60() {
            //#012bcc8  BNE t3, zero, $0012bce0
            //#012bccc  LW t4, $0214(sp)
            if (true) {
                return true;
            }
            //#012bcd0  LUI t7, $0008
            //#012bcd4  AND t7, s4, t7
            //#012bcd8  BEQ t7, zero, $00129e60
            //#012bcdc  LW t3, $01f4(sp)
            if (true) {
                return false;
            }
            return true;
        }

        private bool test012a9f4_then_012ba08_else_012aa04() {
            //#012a9f4  BEQ t6, zero, $0012ba08
            //#012a9f8  SLTIU t7, t6, $0004
            if (true) {
                return true;
            }
            //#012a9fc  BEQ t7, zero, $0012ba08
            //#012aa00  LW t7, $0218(sp)
            if (true) {
                return true;
            }
            return false;
        }

        private bool test012bbe0_then_012bf98_else_012bbfc() {
            //#012bbe0  BEQ t5, t7, $0012bf98
            //#012bbe4  SLTI t7, t5, $0004
            if (true) {
                return true;
            }
            //#012bbe8  BEQ t7, zero, $0012bf8c
            //#012bbec  ADDIU t7, zero, $0002
            if (true) {
                //#012bf8c  ADDIU t7, zero, $0005
                //#012bf90  BNEL t5, t7, $0012bbfc
                if (true) {
                    //#012bf94  LW t5, $0008(s0)
                    return false;
                }
                return true;
            }
            //#012bbf0  BEQ t5, t7, $0012bf48
            //#012bbf4  LW t6, $0218(sp)
            if (true) {
                //#012bf48  LW t5, $0008(s0)
                //#012bf4c  LHU t7, $0004(t6)
                //#012bf50  SLL t7, t7, 6
                //#012bf54  ADDU t6, t5, t7
                //#012bf58  ADDU t7, t5, s3
                //#012bf5c  BEQL t7, t6, $0012bc00
                if (true) {
                    //#012bf60  LW t4, $0004(s0) !skip:012bbfc
                    return false;
                }
                else {
                    //#012bf64  LQ t0, $0000(t6)
                    //#012bf68  LQ t1, $0010(t6)
                    //#012bf6c  LQ t2, $0020(t6)
                    //#012bf70  LQ t3, $0030(t6)
                    //#012bf74  SQ t0, $0000(t7)
                    //#012bf78  SQ t1, $0010(t7)
                    //#012bf7c  SQ t2, $0020(t7)
                    //#012bf80  SQ t3, $0030(t7)
                    //#012bf84  BEQ zero, zero, $0012bbfc
                    //#012bf88  LW t5, $0008(s0)
                    return false;
                }
            }
            else {
                //#012bbf8  LW t5, $0008(s0)
                return false;
            }
        }

        private bool test012cc88_then_012d130_else_012cc9c() {
            //#012cc88  BEQ t6, zero, $0012d12c
            //#012cc8c  ADDIU t7, zero, $0004
            if (true) {
                //#012d12c  LW t4, $0004(s0)
                return true;
            }
            //#012cc90  LBU t6, $0000(t6)
            //#012cc94  BNEL t6, t7, $0012d130
            if (true) {
                //#012cc98  LW t4, $0004(s0)
                return true;
            }
            return false;
        }

        int[] JT037838c = {
            0x0129c2c,
            0x0129c3c,
            0x012d3c8,
            0x012d3c8,
            0x012d3c8,
            0x012d3d0,
            0x012d3e0,
            0x0129c3c,
            0x0129c3c,
            0x0129c3c,
            0x0129c3c,
            0x0129c3c,
            0x012d3e8,
            0x012d410,
        };

        int[] JT0378368 = {
            0x0129b74,
            0x0129b74,
            0x0129b74,
            0x012d450,
            0x012d450,
            0x012d450,
            0x012d4ac,
            0x012d4ac,
            0x012d4ac,
        };
    }
}
