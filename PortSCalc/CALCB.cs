using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;
using System.Diagnostics;

namespace PortSCalc {
    class CALCB {
        InfoTbl s5;
        Msetacc offMset;
        float f0;

        Vector4[] tmpb;

        public CALCB(InfoTbl it, Msetacc offMset) {
            this.s5 = it;
            this.offMset = offMset;

            tmpb = new Vector4[offMset.cntt5];
        }

        /// <summary>
        /// Load matrix identity
        /// </summary>
        void fn11b420(out Matrix p) {
            //#                             ; a0=pMatrix4x4
            //#011b420  LUI t7, $0035
            //#011b424  ADDIU t7, t7, $4260 ; t7=_354260
            //#011b428  LQ t0, $0000(t7)
            //#011b42c  LQ t1, $0010(t7)
            //#011b430  LQ t2, $0020(t7)
            //#011b434  LQ t3, $0030(t7)
            //#011b438  SQ t0, $0000(a0)
            //#011b43c  SQ t1, $0010(a0)
            //#011b440  SQ t2, $0020(a0)
            //#011b444  SQ t3, $0030(a0)
            p = Matrix.Identity;
            //#011b448  JR ra
            //#011b44c  NOP 
        }

        float _378360 = 0.0174533f;

        /*
            a0 009f8510 // mset +0x00
            a1 01abb590 // info tbl
            s0 009f85a0 // mset +0x90
            s4 01abb0b0 // temp?
            sp 0038eca0 // more than 768 bytes
            pc 00129A18
         * */
        public void S_CALCB() {
            //#                                 ; a0=offMset
            //#                                 ; a1=InfoTbl
            //#                                 ; a3=?
            //#                                 ; s0=offMset.mhdr
            //#                                 ; s4=temp?
            //#                                 ; $f0=?

            //{}S_CALCB
            //#0129a18  ADDIU sp, sp, $fd00     ; sp +768 bytes 
            //#0129a1c  ADDIU t7, zero, $000c   ; t7=12
            //#0129a20  SD s0, $02a0(sp)        ; Setw(&SP02a0, offMset.mhdr)
            //#0129a24  SD s1, $02a8(sp)
            //#0129a28  SD s5, $02c8(sp)
            //#0129a2c  DADDU s0, a0, zero      ; s0=offMset
            //#0129a30  SD s6, $02d0(sp)
            //#0129a34  SD s2, $02b0(sp)
            //#0129a38  DADDU s5, a1, zero      ; s5=InfoTbl (const)
            //#0129a3c  SD s3, $02b8(sp)
            //#0129a40  SD s4, $02c0(sp)
            //#0129a44  SD s7, $02d8(sp)
            //#0129a48  SD s8, $02e0(sp)
            //#0129a4c  SD ra, $02e8(sp)
            //#0129a50  SWC1 $f20, $02f0(sp)    
            //#0129a54  LW t3, $0004(a0)        ; t3=offMset.mhdr.ptr
            //#0129a58  SW a2, $01f0(sp)
            //#0129a5c  DADDU a0, sp, zero      ; a0=&SP0
            Matrix SP0;
            //#0129a60  SW t3, $01fc(sp)        ; Setw(&SP01fc, offMset.mhdr.ptr);
            //#0129a64  SW a3, $01f4(sp)        ; Setw(&SP01f4, a3?);
            //#0129a68  LW t4, $01fc(sp)        ; t4=t3
            //#0129a6c  LW t6, $0054(t3)        ; t6=offMset.mhdr.cntt3
            //#0129a70  SW t0, $01f8(sp)
            //#0129a74  MULT t6, t6, t7         ; t6=t6*12
            //#0129a78  LW t3, $0050(t3)        ; t3=offMset.mhdr.offt3
            //#0129a7c  LW t5, $0060(t4)        ; t5=offMset.mhdr.offt7
            //#0129a80  ADDU s1, t4, t3         ; s1=&T3[0]
            IEnumerator<T3> s1 = offMset.alt3.GetEnumerator();
            //#0129a84  LW t7, $0064(t4)        ; t7=offMset.mhdr.cntt7
            //#0129a88  LW t3, $01fc(sp)        ; t3=Getw(&SP01fc)
            //#0129a8c  ADDU t6, s1, t6         ; t6=&T3[cntt3]
            //#0129a90  LW t4, $001c(t4)        ; t4=offMset.mhdr.offt4
            //#0129a94  SLL t7, t7, 3           ; t7=t7<<3
            //#0129a98  ADDU s6, t3, t5         ; s6=&T7[0]
            IEnumerator<T7> s6 = offMset.alt7.GetEnumerator();
            //#0129a9c  LW t3, $00a0(t3)        ; t3=offMset.mhdr.w_a0
            //#0129aa0  ADDU t7, s6, t7         ; t7=&T7[cntt7]
            //#0129aa4  LW t5, $01fc(sp)        ; t5=Getw(&SP01fc)
            //#0129aa8  SW t6, $0204(sp)        ; Setw(&SP0204, &T3[cntt3])
            T3 SP0204 = offMset.alt3[offMset.cntt3];
            //#0129aac  ADDU t4, t5, t4         ; t4=&T4[0]
            //#0129ab0  SW t7, $0208(sp)        ; Setw(&SP0208, &T7[cntt7])
            T7 SP0208 = offMset.alt7[offMset.cntt7];
            //#0129ab4  ADDU t3, t5, t3         ; t3=&T13[0]
            //#0129ab8  SW t4, $0200(sp)        ; Setw(&SP0200, &T4[0])
            fn11b420(out SP0);
            //#0129abc  JAL $0011b420           ; fn11b420();
            //#0129ac0  SW t3, $020c(sp)        ; Setw(&SP020c, &T13[0])
            IEnumerator<T13> SP020c = offMset.alt13.GetEnumerator();
            //#0129ac4  SW zero, $0210(sp)      ; Setw(&SP0210, 0)
            //#0129ac8  LW t6, $01fc(sp)        ; t6=offMset.mhdr
            //#0129acc  LHU t7, $0012(t6)       ; t7=offMset.mhdr.cntt5
            int t7 = offMset.cntt5;
            //#0129ad0  BEQL t7, zero, $0012a4c0    ; if (t7==0) return;
            //#0129ad4  LD s0, $02a0(sp)            ; .. BEQL
            if (t7 == 0) return;
            //#0129ad8  LW t7, $0200(sp)        ; t7=&T4[0]
            int i = 0;
            //#0129adc  LW t5, $0004(s0)        ; t5=offMset.mhdr
            //#0129ae0  LW s4, $0000(t7)        ; s4=T4[0].w0
            int s4 = offMset.Gett4(i);
            //#0129ae4  LHU t4, $0010(t5)       ; t4=offMset.mhdr.cjMdlx
            int t4 = offMset.cjMdlx;
            //#0129ae8  ANDI s2, s4, $ffff      ; s2=s4&0xffff
            int s2 = s4 & 0xffff;
            //#0129aec  SLT t7, s2, t4
            //#0129af0  BEQL t7, zero, $0012d4f4    ; 
            //#0129af4  LW t6, $0018(t5)            ; .. BEQL
            if ((s2 < t4) == false) {
                //HERE:
            }
            //#0129af8  LW a0, $0014(s5)    ; a0=InfoTbl.offMdlx04
            //#0129afc  LW t7, $0000(a0)    ; t7=InfoTbl.offMdlx04.MdlxVtbl
            //#0129b00  LW v0, $0020(t7)    ; v0=InfoTbl.offMdlx04.MdlxVtbl.fn0x20=fn1206a0
            //#0129b04  JALR ra, v0
            //#0129b08  SLL s3, s2, 6       ; v0=&mdlxAxBone
            int s3 = s2 << 6;
            //#0129b0c  ADDU v0, v0, s3     ; v0=&mdlxAxBone[s2]
            AxBone axb = s5.offMdlx04.GetAxBone(s2);
            //#0129b10  LH v0, $0004(v0)
            int v0 = axb.pi;
            //#0129b14  LW t3, $0208(sp)    ; t3=&T7[cntt7]
            //#0129b18  BEQ s6, t3, $0012d4ec   ; if (&T7[0] == &T7[cntt7]) { Setw(&SP0214, v0); goto _12d4ec; }
            //#0129b1c  SW v0, $0214(sp)        ; Setw(&SP0214, v0)
            int SP0214 = v0;
            if (object.ReferenceEquals(s6.Current, SP0208)) {
                //HERE:
            }
            //#0129b20  ANDI t6, s2, $ffff
            int t6 = s2 & 0xffff;
            //#0129b24  LHU t7, $0000(s6)       ; t7=&T7[0].hw0
            t7 = s6.Current.hw0;
            //#0129b28  DADDU s3, t6, zero
            s3 = t6;
            //#0129b2c  BNE t7, t6, $00129bc4
            //#0129b30  SLL s8, s2, 4
            int s8 = s2 << 4;
            if (!(t7 != t6)) {
                //#0129b34  LHU a2, $0006(s6)
                int a2 = s6.Current.hw6;
                while (true) {
                    //#0129b38  DADDU a0, s0, zero  ; a0=offMset
                    //#0129b3c  JAL $00128e40       ; fn128e40();
                    //#0129b40  DADDU a1, s5, zero  ; a1=InfoTbl
                    //#0129b44  LHU t4, $0002(s6)
                    t4 = s6.Current.hw2;
                    //#0129b48  ANDI t6, t4, $ffff
                    t6 = t4 & 0xffff;
                    //#0129b4c  SLTIU t7, t6, $0009
                    //#0129b50  BEQ t7, zero, $00129ba0
                    //#0129b54  MOV.S $f1, $f0
                    if ((t6 < 9) != false) {
                        float f1 = f0;
                        //#0129b58  SLL t7, t6, 2
                        t7 = t6 << 2;
                        //#0129b5c  LUI t6, $0038
                        //#0129b60  ADDIU t6, t6, $8368 ; t6=_378368
                        //#0129b64  ADDU t7, t7, t6
                        //#0129b68  LW t5, $0000(t7)
                        //#0129b6c  JR t5
                        //#0129b70  NOP 
                        switch (t6) {
                            case 0: // 129b74
                            case 1: // 129b74
                            case 2: // 129b74
                            {
                                    //#0129b74  LW t5, $0004(s0)    ; t5=offMset.mhdr
                                    //#0129b78  LHU t3, $0010(t5)   
                                    int t3 = offMset.cjMdlx;
                                    //#0129b7c  SLT t7, s2, t3
                                    //#0129b80  BEQL t7, zero, $0012d438
                                    //#0129b84  LW t6, $0018(t5)    ; t6=offMset.offt5
                                    if ((s2 < t3) == false) {
                                        //#012d438  SUBU t7, s2, t3
                                        t7 = s2 - t3;
                                        AxBone _t7 = offMset.alt5[t7];
                                        //#012d43c  SLL t7, t7, 6
                                        //#012d440  ADDU t6, t5, t6     ; t6=&T5[0]
                                        //#012d444  ADDU t6, t6, t7     ; t6=&T5[s2-t3]
                                        //#012d448  BEQ zero, zero, $00129b90
                                        //#012d44c  ADDIU t6, t6, $0010 ; t6
                                        {
                                            //#0129b90  ANDI t7, t4, $ffff
                                            t7 = t4 & 0xffff;
                                            //#0129b94  SLL t7, t7, 2
                                            //#0129b98  ADDU t7, t6, t7
                                            //#0129b9c  SWC1 $f1, $0000(t7)

                                            switch (t7) {
                                                case 0: _t7.sx = f1; break;
                                                case 1: _t7.sy = f1; break;
                                                case 2: _t7.sz = f1; break;
                                                case 3: _t7.sw = f1; break;
                                                default: throw new ArgumentOutOfRangeException();
                                            }
                                        }
                                    }
                                    else {
                                        //#0129b88  LW t7, $001c(s5)    ; t7=&s5.Sxyz
                                        //#0129b8c  ADDU t6, t7, s8     ; t6=&s5.Sxyz[s2]
                                        //#0129b90  ANDI t7, t4, $ffff
                                        t7 = t4 & 0xffff;
                                        //#0129b94  SLL t7, t7, 2
                                        //#0129b98  ADDU t7, t6, t7     ; t7=&s5.Sxyz[s2][t7]
                                        //#0129b9c  SWC1 $f1, $0000(t7)
                                        s5.Sxyz[s2][t7] = f1;
                                    }

                                    break;
                                }
                            case 3: // 12d450
                            case 4: // 12d450
                            case 5: // 12d450
                            {
                                    //#012d450  LW t5, $0004(s0)    ; t5=offMset.mhdr
                                    //#012d454  LHU t3, $0010(t5)
                                    int t3 = offMset.cjMdlx;
                                    //#012d458  SLT t7, s2, t3
                                    //#012d45c  BEQL t7, zero, $0012d494
                                    //#012d460  LW t6, $0018(t5)    ; t6=offMset.mhdr.offt5
                                    if ((s2 < t3) == false) {
                                        //#012d494  SUBU t7, s2, t3
                                        t7 = s2 - t3;
                                        AxBone _t7 = offMset.alt5[t7];
                                        //#012d498  SLL t7, t7, 6
                                        //#012d49c  ADDU t6, t5, t6     ; t6=&T5[0]
                                        //#012d4a0  ADDU t6, t6, t7     ; t6=&T5[t7]
                                        //#012d4a4  BEQ zero, zero, $0012d46c
                                        //#012d4a8  ADDIU t6, t6, $0020 ; t6=&T5[t7].rx
                                        {
                                            //#012d46c  ANDI t7, t4, $ffff  
                                            t7 = t4 & 0xffff;
                                            //#012d470  LUI t4, $0038
                                            //#012d474  ADDIU t7, t7, $fffd
                                            t7 -= 3;
                                            //#012d478  ADDIU t4, t4, $8360 ; t4=_378360
                                            //#012d47c  SLL t7, t7, 2
                                            //#012d480  LWC1 $f0, $0000(t4)
                                            f0 = _378360;
                                            //#012d484  ADDU t7, t6, t7     ; t7=&s5.Rxyz[s2][t7]
                                            //#012d488  MUL.S $f0, $f1, $f0
                                            f0 *= f1;
                                            //#012d48c  BEQ zero, zero, $00129ba0
                                            //#012d490  SWC1 $f0, $0000(t7)

                                            switch (t7) {
                                                case 0: _t7.rx = f0; break;
                                                case 1: _t7.ry = f0; break;
                                                case 2: _t7.rz = f0; break;
                                                case 3: _t7.rw = f0; break;
                                                default: throw new ArgumentOutOfRangeException();
                                            }

                                            break;
                                        }
                                    }
                                    else {
                                        //#012d464  LW t7, $0020(s5)    ; t7=&s5.Rxyz[0]
                                        //#012d468  ADDU t6, t7, s8     ; t6=&s5.Rxyz[s2]
                                        //#012d46c  ANDI t7, t4, $ffff  
                                        t7 = t4 & 0xffff;
                                        //#012d470  LUI t4, $0038
                                        //#012d474  ADDIU t7, t7, $fffd
                                        t7 -= 3;
                                        //#012d478  ADDIU t4, t4, $8360 ; t4=_378360
                                        //#012d47c  SLL t7, t7, 2
                                        //#012d480  LWC1 $f0, $0000(t4)
                                        f0 = _378360;
                                        //#012d484  ADDU t7, t6, t7     ; t7=&s5.Rxyz[s2][t7]
                                        //#012d488  MUL.S $f0, $f1, $f0
                                        f0 *= f1;
                                        //#012d48c  BEQ zero, zero, $00129ba0
                                        //#012d490  SWC1 $f0, $0000(t7)
                                        s5.Rxyz[s2][t7] = f0;

                                        break;
                                    }
                                }
                            case 6: // 12d4ac
                            case 7: // 12d4ac
                            case 8: // 12d4ac
                            {
                                    //#012d4ac  LW t5, $0004(s0)    ; t5=offMset.mhdr
                                    //#012d4b0  LHU t3, $0010(t5)
                                    int t3 = offMset.cjMdlx;
                                    //#012d4b4  SLT t7, s2, t3
                                    //#012d4b8  BEQL t7, zero, $0012d4d4
                                    //#012d4bc  LW t6, $0018(t5)    ; t6=offmset.mhdr.offt5
                                    if ((s2 < t3) == false) {
                                        //HERE:
                                        //#012d4d4  SUBU t7, s2, t3
                                        t7 = s2 - t3;
                                        AxBone _t7 = offMset.alt5[t7];
                                        //#012d4d8  SLL t7, t7, 6
                                        //#012d4dc  ADDU t6, t5, t6     ; t6=&T5[0]
                                        //#012d4e0  ADDU t6, t6, t7     ; t6=&T5[s2-t3]
                                        //#012d4e4  BEQ zero, zero, $0012d4c8
                                        //#012d4e8  ADDIU t6, t6, $0030 ; t6=&T5[s2-t3].tx
                                        {
                                            //#012d4c8  ANDI t7, t4, $ffff
                                            t7 = t4 & 0xffff;
                                            //#012d4cc  BEQ zero, zero, $00129b94
                                            //#012d4d0  ADDIU t7, t7, $fffa
                                            t7 -= 6;
                                            {
                                                //#0129b94  SLL t7, t7, 2
                                                //#0129b98  ADDU t7, t6, t7     ; t7=&s5.Txyz[s2][t7]
                                                //#0129b9c  SWC1 $f1, $0000(t7)
                                                switch (t7) {
                                                    case 0: _t7.tx = f1; break;
                                                    case 1: _t7.ty = f1; break;
                                                    case 2: _t7.tz = f1; break;
                                                    case 3: _t7.tw = f1; break;
                                                    default: throw new ArgumentOutOfRangeException();
                                                }

                                                break;
                                            }
                                        }
                                    }
                                    else {
                                        //#012d4c0  LW t7, $0024(s5)    ; t7=&s5.Txyz[0]
                                        //#012d4c4  ADDU t6, t7, s8     ; t6=&s5.Txyz[s2]
                                        //#012d4c8  ANDI t7, t4, $ffff
                                        t7 = t4 & 0xffff;
                                        //#012d4cc  BEQ zero, zero, $00129b94
                                        //#012d4d0  ADDIU t7, t7, $fffa
                                        t7 -= 6;
                                        {
                                            //#0129b94  SLL t7, t7, 2
                                            //#0129b98  ADDU t7, t6, t7     ; t7=&s5.Txyz[s2][t7]
                                            //#0129b9c  SWC1 $f1, $0000(t7)
                                            s5.Txyz[s2][t7] = f1;

                                            break;
                                        }
                                    }
                                }
                            default: throw new ArgumentOutOfRangeException();
                        }
                    }
                    //#0129ba0  LUI t7, $ffdf
                    //#0129ba4  LW t5, $0208(sp)    ; t5=&T7[cntt7]
                    //#0129ba8  ORI t7, t7, $ffff
                    t7 = -2097153; // 0xffdfffff;
                    //#0129bac  ADDIU s6, s6, $0008
                    s6.MoveNext();
                    //#0129bb0  BEQ s6, t5, $00129bc4
                    //#0129bb4  AND s4, s4, t7
                    s4 = s4 & t7;
                    if (object.ReferenceEquals(s6.Current, SP0208)) break;
                    //#0129bb8  LHU t7, $0000(s6)
                    t7 = s6.Current.hw0;
                    //#0129bbc  BEQL t7, s3, $00129b38
                    //#0129bc0  LHU a2, $0006(s6)
                    if (t7 == s3) {
                        a2 = s6.Current.hw6;
                        continue;
                    }
                    break;
                }
            }

            {
                //#0129bc4  SW zero, $0218(sp)
                T3 SP0218 = null;
                //#0129bc8  ADDIU a2, zero, $ffff
                int a2 = 0xffff;
                //#0129bcc  SW zero, $021c(sp)
                T8 SP021c = null;
                //#0129bd0  ADDIU a3, zero, $ffff
                int a3 = 0xffff;
                //#0129bd4  LW t6, $0204(sp)    ; t6=&T3[cntt3]
                //#0129bd8  DADDU v0, zero, zero
                v0 = 0;
                //#0129bdc  ADDIU v1, zero, $ffff
                int v1 = 0xffff;
                //#0129be0  BEQ s1, t6, $00129c58   ; if (&T3[s1_i] == &T3[cntt3]) { ... }
                //#0129be4  DADDU t3, zero, zero
                T3 t3 = null;
                if (!object.ReferenceEquals(s1.Current, SP0204)) {
                    //#0129be8  ANDI t6, s2, $ffff
                    t6 = s2 & 0xffff;
                    //#0129bec  LHU t7, $0002(s1)
                    t7 = s1.Current.hw2;
                    //#0129bf0  BNE t7, t6, $00129c58
                    //#0129bf4  DADDU t4, t6, zero
                    t4 = t6;
                    if (!(t7 != t6)) {
                        //#0129bf8  LBU t7, $0001(s1)
                        t7 = s1.Current.b1;
                        while (true) {
                            //#0129bfc  BEQ t7, zero, $00129c40
                            //#0129c00  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                            if (!(t7 == 0)) {
                                //#0129c04  LBU t6, $0000(s1)
                                t6 = s1.Current.b0;
                                //#0129c08  SLTIU t7, t6, $000e
                                //#0129c0c  BEQ t7, zero, $00129c3c
                                //#0129c10  SLL t7, t6, 2
                                if ((t6 < 14u) != false) {
                                    //#0129c14  LUI t6, $0038
                                    //#0129c18  ADDIU t6, t6, $838c ; t6=_37838c
                                    //#0129c1c  ADDU t7, t7, t6
                                    //#0129c20  LW t5, $0000(t7)
                                    //#0129c24  JR t5
                                    //#0129c28  NOP 
                                    switch (t6) {
                                        case 0: // 129c2c
                                        {
                                                //#0129c2c  ADDIU t7, zero, $ffff
                                                //#0129c30  BNE a2, t7, $0012d3b4
                                                //#0129c34  NOP 
                                                t7 = 0xffff;
                                                if (a2 != t7) {
                                                    //HERE:
                                                    //#012d3b4  BEQL a3, t7, $00129c3c
                                                    //#012d3b8  LHU a3, $0004(s1)
                                                    if (a3 == t7) {
                                                        a3 = s1.Current.hw4;
                                                        //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                        break;
                                                    }
                                                    else {
                                                        //#012d3bc  BEQ zero, zero, $00129c40
                                                        //#012d3c0  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                        break;
                                                    }
                                                }
                                                else {
                                                    //#0129c38  LHU a2, $0004(s1)
                                                    a2 = s1.Current.hw4;
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        case 1: // 129c3c
                                        {
                                                //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                break;
                                            }
                                        case 2: // 12d3c8
                                        case 3: // 12d3c8
                                        case 4: // 12d3c8
                                        {
                                                //#012d3c8  BEQ zero, zero, $00129c3c
                                                //#012d3cc  SW s1, $0218(sp)
                                                SP0218 = s1.Current;
                                                {
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        case 5: // 12d3d0
                                        {
                                                //#012d3d0  LHU a2, $0004(s1)
                                                a2 = s1.Current.hw4;
                                                //#012d3d4  SW s1, $0218(sp)
                                                SP0218 = s1.Current;
                                                //#012d3d8  BEQ zero, zero, $00129c3c
                                                //#012d3dc  LHU a3, $0006(s1)
                                                a3 = s1.Current.hw6;
                                                {
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        case 6: // 12d3e0
                                        {
                                                //#012d3e0  BEQ zero, zero, $00129c3c
                                                //#012d3e4  DADDU t3, s1, zero
                                                t3 = s1.Current;
                                                {
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        case 7: // 129c3c
                                        case 8: // 129c3c
                                        case 9: // 129c3c
                                        case 10: // 129c3c
                                        case 11: // 129c3c
                                        {
                                                //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                break;
                                            }
                                        case 12: // 12d3e8
                                        {
                                                //#012d3e8  LW t7, $01fc(sp)    ; t7=offMset.mhdr
                                                //#012d3ec  LHU t5, $0006(s1)
                                                int t5 = s1.Current.hw6;
                                                //#012d3f0  LW t6, $005c(t7)    ; t6=offMset.offt8
                                                //#012d3f4  LHU v1, $0004(s1)
                                                v1 = s1.Current.hw4;
                                                //#012d3f8  ADDIU t7, zero, $0030   ; t7=0x30
                                                t7 = 0x30;
                                                //#012d3fc  MULT t5, t5, t7
                                                //#012d400  LW t7, $01fc(sp)        ; t7=offMset.mhdr
                                                //#012d404  ADDU t6, t7, t6         ; t6=offMset.mhdr.offt8
                                                //#012d408  BEQ zero, zero, $00129c3c
                                                //#012d40c  ADDU v0, t6, t5         ; v0=offMset.mhdr.offt8[s1.hw6]
                                                v0 = -1; Trace.Fail("v0 is offset. but what's for?");
                                                {
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        case 13: // 12d410
                                        {
                                                //#012d410  LW t6, $01fc(sp)    ; t6=offMset.mhdr.ptr
                                                //#012d414  LHU t5, $0006(s1)   ; t5=s1.hw6
                                                int t5 = s1.Current.hw6;
                                                //#012d418  LW t7, $005c(t6)        ; t7=offMset.mhdr.offt8
                                                //#012d41c  ADDIU t6, zero, $0030   ; t6=0x30
                                                //#012d420  MULT t5, t5, t6         ; t5=s1.hw6*0x30
                                                //#012d424  LW t6, $01fc(sp)        ; t6=offMset.mhdr.ptr
                                                //#012d428  ADDU t7, t6, t7         ; t7=&T8[0]
                                                //#012d42c  ADDU t7, t7, t5         ; t7=&T8[s1.hw6]
                                                //#012d430  BEQ zero, zero, $00129c3c
                                                //#012d434  SW t7, $021c(sp)        ; Setw(SP021c, t7)
                                                SP021c = offMset.Gett8(t5);
                                                {
                                                    //#0129c3c  LW t7, $0204(sp)    ; t7=&T3[cntt3]
                                                    break;
                                                }
                                            }
                                        default: throw new ArgumentOutOfRangeException();
                                    }
                                }
                            }
                            //HERE:
                            //#0129c40  ADDIU s1, s1, $000c
                            s1.MoveNext();
                            //#0129c44  BEQ s1, t7, $00129c5c
                            //#0129c48  LUI t7, $0003
                            if (!object.ReferenceEquals(s1.Current, SP0204)) {
                                //#0129c4c  LHU t7, $0002(s1)
                                t7 = s1.Current.hw2;
                                //#0129c50  BEQL t7, t4, $00129bfc
                                //#0129c54  LBU t7, $0001(s1)
                                if (t7 == t4) {
                                    t7 = s1.Current.b1;
                                    continue;
                                }
                                //#0129c58  LUI t7, $0003
                            }
                            break;
                        }
                    }
                }
                else {
                    //#0129c58  LUI t7, $0003
                }
                t7 = 0x00030000;
                //#0129c5c  AND t6, s4, t7
                t6 = s4 & t7;
                //#0129c60  BEQ t6, zero, $00129cf8
                //#0129c64  SW zero, $0220(sp)
                T3 SP0220 = null;
                if (!(t6 == 0)) {
                    //#0129c68  LUI t7, $0080
                    t7 = 0x00800000;
                    //#0129c6c  AND t7, s4, t7
                    t7 &= s4;
                    //#0129c70  BEQ t7, zero, $0012d39c
                    //#0129c74  LW t4, $020c(sp)    ; t4=&T13[0]
                    int t5;
                    if (t7 == 0) {
                        //#012d39c  LUI t7, $0001
                        t7 = 0x00010000;
                        //#012d3a0  ADDIU t5, s2, $0001
                        t5 = s2 + 1;
                        //#012d3a4  XOR t7, t6, t7
                        t7 ^= t6;
                        //#012d3a8  ADDIU t6, s2, $0002
                        t6 = s2 + 2;
                        //#012d3ac  BEQ zero, zero, $00129c84
                        //#012d3b0  MOVN t5, t6, t7
                        if (t7 != 0) t5 = t6;
                    }
                    else {
                        //#0129c78  LH t5, $0000(t4)
                        t5 = SP020c.Current.hw0;
                        //#0129c7c  ADDIU t4, t4, $0002
                        SP020c.MoveNext();
                        //#0129c80  SW t4, $020c(sp)
                    }
                    //#0129c84  LW t6, $0204(sp)
                    //#0129c88  BEQ s1, t6, $00129ce4
                    //#0129c8c  ADDIU t4, zero, $0005
                    t4 = 5;
                    if (!object.ReferenceEquals(s1.Current, SP0204)) {
                        //#0129c90  LBU t7, $0001(s1)
                        t7 = s1.Current.b1;
                        while (true) {
                            //#0129c94  BEQ t7, zero, $00129cd8
                            //#0129c98  LW t7, $0204(sp)    ; t7=SP0204
                            if (!(t7 == 0)) {
                                //HERE:
                                //#0129c9c  LHU t7, $0002(s1)
                                t7 = s1.Current.hw2;
                                //#0129ca0  BNE t7, t5, $00129cd8
                                //#0129ca4  LW t7, $0204(sp)    ; t7=SP0204
                                if (t7 != t5) {
                                    //#0129ca8  LBU t6, $0000(s1)
                                    t6 = s1.Current.b0;
                                    //#0129cac  BLTZ t6, $00129ccc
                                    //#0129cb0  DADDU t7, zero, zero
                                    t7 = 0;
                                    if (!(t6 < 0)) {
                                        //#0129cb4  SLTI t7, t6, $0002
                                        //#0129cb8  BNE t7, zero, $00129ccc
                                        //#0129cbc  ADDIU t7, zero, $0001
                                        t7 = 1;
                                        if ((t6 < 2) == false) {
                                            //#0129cc0  BNE t6, t4, $00129ccc
                                            //#0129cc4  DADDU t7, zero, zero
                                            t7 = 0;
                                            if (!(t6 != t4)) {
                                                //#0129cc8  ADDIU t7, zero, $0001
                                                t7 = 1;
                                            }
                                        }
                                    }
                                    //#0129ccc  ANDI t7, t7, $00ff
                                    t7 &= 0xff;
                                    //#0129cd0  BNE t7, zero, $0012d390
                                    //#0129cd4  LW t7, $0204(sp)    ; t7=SP0204
                                    if (t7 != 0) {
                                        //#012d390  SW s1, $0220(sp)
                                        SP0220 = s1.Current;
                                        //#012d394  BEQ zero, zero, $00129ce4
                                        //#012d398  ADDIU s1, s1, $000c
                                        s1.MoveNext();
                                        break;
                                    }
                                }
                            }
                            //#0129cd8  ADDIU s1, s1, $000c
                            s1.MoveNext();
                            //#0129cdc  BNEL s1, t7, $00129c94
                            //#0129ce0  LBU t7, $0001(s1)
                            if (!object.ReferenceEquals(s1.Current, SP0204)) {
                                t7 = s1.Current.b1;
                                continue;
                            }
                            break;
                        }
                    }
                    //#0129ce4  LW t4, $0220(sp)
                    //#0129ce8  BNE t4, zero, $00129cf8
                    //#0129cec  LUI t7, $fffc
                    if (object.ReferenceEquals(SP0220, null)) {
                        //#0129cf0  ORI t7, t7, $ffff
                        t7 = -196609; // 0xfffcffff;
                        //#0129cf4  AND s4, s4, t7
                        s4 &= t7;
                    }
                }
                //#0129cf8  BEQ t3, zero, $0012d2c8
                //#0129cfc  LW t7, $0214(sp)
                t7 = SP0214;
                if (object.ReferenceEquals(t3, null)) {
                    //#012d2c8  BLTZ t7, $0012d33c
                    //#012d2cc  SLL t7, t7, 4
                    if (t7 < 0) {
                        //#012d33c  LW t5, $0004(s0)    ; t5=offMset.mhdr.ptr
                        //#012d340  LW t6, $0014(s0)    ; t6=&tmpb
                        //#012d344  LHU t3, $0010(t5)   ; t3=offMset.mhdr.cjMdlx
                        //#012d348  SLT t7, s2, t3
                        //#012d34c  BEQ t7, zero, $0012d374
                        //#012d350  ADDU t4, t6, s8     ; t4=&tmpb[s2]
                        if ((s2 < offMset.cjMdlx) == false) {
                            //#012d374  LW t6, $0018(t5)    ; t6=offMset.mhdr.offt5
                            //#012d378  SUBU t7, s2, t3
                            t7 = s2 - offMset.cjMdlx;
                            //#012d37c  SLL t7, t7, 6
                            //#012d380  ADDU t6, t5, t6     ; t6=&T5[0]
                            //#012d384  ADDU t6, t6, t7     ; t6=&T5[s2-t3]
                            //#012d388  BEQ zero, zero, $0012d35c
                            //#012d38c  ADDIU t6, t6, $0010 ; t6=&T5[s2-t3].sx
                            {
                                //#012d35c  BEQL t4, t6, $00129df8
                                //#012d360  LUI t7, $0020
                                if (false) {
                                    //#012d364  LQ t0, $0000(t6)
                                    //#012d368  SQ t0, $0000(t4)
                                    tmpb[s2] = offMset.alt5[s2 - offMset.cjMdlx].Sxyzw;
                                    //#012d36c  BEQ zero, zero, $00129df8
                                    //#012d370  LUI t7, $0020
                                }
                            }
                        }
                        else {
                            //#012d354  LW t7, $001c(s5)    ; t7=&s5.Sxyz
                            //#012d358  ADDU t6, t7, s8     ; t6=&s5.Sxyz[s2]
                            //#012d35c  BEQL t4, t6, $00129df8
                            //#012d360  LUI t7, $0020
                            if (false) {
                                //#012d364  LQ t0, $0000(t6)
                                //#012d368  SQ t0, $0000(t4)
                                tmpb[s2] = s5.Sxyz[s2];
                                //#012d36c  BEQ zero, zero, $00129df8
                                //#012d370  LUI t7, $0020
                            }
                        }
                    }
                    else {
                        //#012d2d0  LW t5, $0004(s0)    ; t5=offMset.mhdr.ptr
                        //#012d2d4  LW t6, $0014(s0)    ; t6=&tmpb
                        //#012d2d8  LHU t3, $0010(t5)   ; t3=offMset.mhdr.cjMdlx
                        //#012d2dc  ADDU t2, t6, t7     ; t2=&tmpb[SP0214]
                        //#012d2e0  SLT t7, s2, t3
                        //#012d2e4  BEQ t7, zero, $0012d320
                        //#012d2e8  ADDU t4, t6, s8     ; t4=&tmpb[s2]
                        Vector4 SP0120;
                        if ((s2 < offMset.cjMdlx) == false) {
                            //#012d320  LW t6, $0018(t5)    ; t6=offMset.mhdr.offt5
                            //#012d324  SUBU t7, s2, t3
                            t7 = s2 - offMset.cjMdlx;
                            //#012d328  SLL t7, t7, 6
                            //#012d32c  ADDU t6, t5, t6     ; t6=&T5[0]
                            //#012d330  ADDU t6, t6, t7     ; t6=&T5[s2-t3]
                            //#012d334  BEQ zero, zero, $0012d2f4
                            //#012d338  ADDIU t6, t6, $0010 ; t6=&T5[s2-t3].sx
                            {
                                //#012d2f4  ADDIU t8, sp, $0120     ; t8=&SP0120
                                //#012d2f8  LQC2 vf1, $0000(t2)     ; vf1=Getq(&tmpb[SP0214])
                                Vector4 vf1 = tmpb[SP0214];
                                //#012d2fc  LQC2 vf2, $0000(t6)     ; vf2=Getq(&T5[s2-t3].sx)
                                Vector4 vf2 = offMset.alt5[s2 - offMset.cjMdlx].Sxyzw;
                                //#012d300  VMUL.xyzw vf1, vf1, vf2
                                vf1 = new Vector4(
                                    vf1.X * vf2.X,
                                    vf1.Y * vf2.Y,
                                    vf1.Z * vf2.Z,
                                    vf1.W * vf2.W);
                                //#012d304  SQC2 vf1, $0000(t8)     ; Setq(&SP0120, vf1)
                                SP0120 = vf1;
                            }
                        }
                        else {
                            //#012d2ec  LW t7, $001c(s5)    ; t7=&s5.Sxyz
                            //#012d2f0  ADDU t6, t7, s8     ; t6=&s5.Sxyz[&s2]
                            //#012d2f4  ADDIU t8, sp, $0120     ; t8=&SP0120
                            //#012d2f8  LQC2 vf1, $0000(t2)     ; vf1=Getq(&tmpb[SP0214])
                            Vector4 vf1 = tmpb[SP0214];
                            //#012d2fc  LQC2 vf2, $0000(t6)     ; vf2=Getq(&s5.Sxyz[&s2])
                            Vector4 vf2 = s5.Sxyz[s2];
                            //#012d300  VMUL.xyzw vf1, vf1, vf2
                            vf1 = new Vector4(
                                vf1.X * vf2.X,
                                vf1.Y * vf2.Y,
                                vf1.Z * vf2.Z,
                                vf1.W * vf2.W);
                            //#012d304  SQC2 vf1, $0000(t8)     ; Setq(&SP0120, vf1)
                            SP0120 = vf1;
                        }

                        //#012d308  BEQL t4, t8, $00129df8  
                        //#012d30c  LUI t7, $0020
                        if (true) {
                            //#012d310  LQ t0, $0000(t8)    ; t8=&SP0120
                            //#012d314  SQ t0, $0000(t4)    ; t4=&tmpb[s2]
                            tmpb[s2] = SP0120;
                            //#012d318  BEQ zero, zero, $00129df8
                            //#012d31c  LUI t7, $0020
                        }
                    }
                }
                else {
                    //HERE:
                    //#0129d00  LHU t7, $0004(t3)
                    t7 = t3.hw4;
                    //#0129d04  LW t3, $0014(s0)    ; t3=&tmpb[0]
                    //#0129d08  SLL t7, t7, 4
                    //#0129d0c  ADDU t6, t3, t7     ; t6=&tmpb[t3.hw4]
                    //#0129d10  ADDU t7, t3, s8     ; t7=&tmpb[s2]
                    //#0129d14  BEQ t7, t6, $00129d2c
                    //#0129d18  LW t5, $0214(sp)    ; t5=SP0214
                    int t5 = SP0214;
                    if (!(t3.hw4 == s2)) {
                        //#0129d1c  LQ t0, $0000(t6)
                        //#0129d20  SQ t0, $0000(t7)
                        tmpb[s2] = tmpb[t3.hw4];
                        //#0129d24  LW t3, $0014(s0)    ; t3=&tmpb
                        //#0129d28  LW t5, $0214(sp)    ; t5=SP0214
                    }
                    //#0129d2c  BLTZ t5, $0012d27c
                    //#0129d30  LW t5, $0004(s0)    ; t5=offMset.mhdr.ptr
                    if (t5 < 0) {
                        //#012d27c  LHU t4, $0010(t5)   ; t4=offMset.mhdr.cjMdlx
                        t4 = offMset.cjMdlx;
                        //#012d280  SLT t7, s2, t4
                        //#012d284  BEQL t7, zero, $0012d2b0
                        //#012d288  LW t6, $0018(t5)    ; t6=offMset.mhdr.ptr.offt5
                        if ((s2 < t4) == false) {
                            //HERE:
                            //#012d2b0  SUBU t7, s2, t4
                            t7 = s2 - t4;
                            //#012d2b4  SLL t7, t7, 6
                            //#012d2b8  ADDU t6, t5, t6     ; t6=&T5[s2-t4]
                            //#012d2bc  ADDU t6, t6, t7
                            //#012d2c0  BEQ zero, zero, $0012d294
                            //#012d2c4  ADDIU t7, t6, $0010 ; t7=&T5[s2-t4].sx
                        }
                        else {
                            //#012d28c  LW t7, $001c(s5)    ; t7=&s5.Sxyz[0]
                            //#012d290  ADDU t7, t7, s8     ; t7=&s5.Sxyz[s2]
                        }
                        //#012d294  ADDU t6, t3, s8         ; t6=&tmpb[s2]
                        //#012d298  BEQL t7, t6, $00129df8
                        //#012d29c  LUI t7, $0020
                        if (true) {
                            //#012d2a0  LQ t0, $0000(t6)
                            //#012d2a4  SQ t0, $0000(t7)
                            tmpb[s2] = s5.Sxyz[s2];
                            //#012d2a8  BEQ zero, zero, $00129df8
                            //#012d2ac  LUI t7, $0020
                        }
                    }
                    else {
                        //HERE:
                        //#0129d34  LHU t4, $0010(t5)   ; t4=offMset.mhdr.cjMdlx
                        t4 = offMset.cjMdlx;
                        //#0129d38  SLT t7, s2, t4
                        //#0129d3c  BEQL t7, zero, $0012d264
                        //#0129d40  LW t6, $0018(t5)    ; t6=offMset.mhdr.offt5
                        if ((s2 < t4) == false) {
                            //#012d264  SUBU t7, s2, t4
                            t7 = s2 - t4;
                            //#012d268  SLL t7, t7, 6
                            //#012d26c  ADDU t6, t5, t6     ; t6=&T5[0]
                            //#012d270  ADDU t6, t6, t7     ; t6=&T5[s2-t4]
                            //#012d274  BEQ zero, zero, $00129d4c
                            //#012d278  ADDIU t5, t6, $0010 ; t5=&T5[s2-t4].sx
                            {
                                //#0129d4c  LW t6, $0214(sp)
                                t6 = SP0214;
                                //#0129d50  ADDU t7, t3, s8         ; t7=&tmpb[s2]
                                //#0129d54  LWC1 $f0, $0000(t7)
                                f0 = tmpb[s2].X;
                                //#0129d58  SLL t2, t6, 4
                                //#0129d5c  ADDU t6, t3, t2         ; t6=&tmpb[t6]
                                //#0129d60  LWC1 $f1, $0000(t6)
                                float f1 = tmpb[t6].X;
                                //#0129d64  NOP 
                                //#0129d68  NOP 
                                //#0129d6c  DIV.S $f0, $f0, $f1
                                f0 = f0 / f1;
                                //#0129d70  SWC1 $f0, $0000(t5)
                                offMset.alt5[s2 - t4].sx = f0;
                            }
                        }
                        else {
                            //#0129d44  LW t7, $001c(s5)    ; t7=&s5.Sxyz[0]
                            //#0129d48  ADDU t5, t7, s8     ; t5=&s5.Sxyz[s2]

                            //#0129d4c  LW t6, $0214(sp)
                            t6 = SP0214;
                            //#0129d50  ADDU t7, t3, s8         ; t7=&tmpb[s2]
                            //#0129d54  LWC1 $f0, $0000(t7)
                            f0 = tmpb[s2].X;
                            //#0129d58  SLL t2, t6, 4
                            //#0129d5c  ADDU t6, t3, t2         ; t6=&tmpb[t6]
                            //#0129d60  LWC1 $f1, $0000(t6)
                            float f1 = tmpb[t6].X;
                            //#0129d64  NOP 
                            //#0129d68  NOP 
                            //#0129d6c  DIV.S $f0, $f0, $f1
                            f0 = f0 / f1;
                            //#0129d70  SWC1 $f0, $0000(t5)
                            s5.Sxyz[s2].X = f0;
                        }

                        //#0129d74  LW t5, $0004(s0)    ; t5=offMset.mhdr.ptr
                        //#0129d78  LHU t4, $0010(t5)   ; t5=offMset.mhdr.ptr.cjMdlx
                        t4 = offMset.cjMdlx;
                        //#0129d7c  SLT t7, s2, t4
                        //#0129d80  BEQL t7, zero, $0012d24c
                        //#0129d84  LW t6, $0018(t5)    ; t6=offMset.mhdr.ptr.offt5
                        if ((s2 < t4) == false) {
                            //#012d24c  SUBU t7, s2, t4
                            t7 = s2 - t4;
                            //#012d250  SLL t7, t7, 6
                            //#012d254  ADDU t6, t5, t6
                            //#012d258  ADDU t6, t6, t7
                            //#012d25c  BEQ zero, zero, $00129d90
                            //#012d260  ADDIU t5, t6, $0010 ; t5=&T5[s2-t4].sx
                            {
                                //#0129d90  LW t7, $0014(s0)    ; t7=&tmpb
                                //#0129d94  ADDU t6, t7, t2     ; t6=&tmpb[t6]
                                //#0129d98  ADDU t7, t7, s8     ; t7=&tmpb[s2]
                                //#0129d9c  LWC1 $f1, $0004(t6)
                                float f1 = tmpb[t6].Y;
                                //#0129da0  LWC1 $f0, $0004(t7)
                                f0 = tmpb[s2].Y;
                                //#0129da4  NOP 
                                //#0129da8  NOP 
                                //#0129dac  DIV.S $f0, $f0, $f1
                                f0 = f0 / f1;
                                //#0129db0  SWC1 $f0, $0004(t5)
                                offMset.alt5[s2 - t4].sy = f0;
                            }
                        }
                        else {
                            //#0129d88  LW t7, $001c(s5)    ; t7=&s5.Sxyz[0]
                            //#0129d8c  ADDU t5, t7, s8     ; t5=&s5.Sxyz[s2]

                            //#0129d90  LW t7, $0014(s0)    ; t7=&tmpb
                            //#0129d94  ADDU t6, t7, t2     ; t6=&tmpb[t6]
                            //#0129d98  ADDU t7, t7, s8     ; t7=&tmpb[s2]
                            //#0129d9c  LWC1 $f1, $0004(t6)
                            float f1 = tmpb[t6].Y;
                            //#0129da0  LWC1 $f0, $0004(t7)
                            f0 = tmpb[s2].Y;
                            //#0129da4  NOP 
                            //#0129da8  NOP 
                            //#0129dac  DIV.S $f0, $f0, $f1
                            f0 = f0 / f1;
                            //#0129db0  SWC1 $f0, $0004(t5)
                            s5.Sxyz[s2].Y = f0;
                        }

                        //#0129db4  LW t5, $0004(s0)    ; t5=offMset.mhdr
                        //#0129db8  LHU t4, $0010(t5)   ; t4=cjMdlx
                        //#0129dbc  SLT t7, s2, t4      
                        //#0129dc0  BEQL t7, zero, $0012d234
                        //#0129dc4  LW t6, $0018(t5)            ; BEQL ...
                        if ((s2 < t4) == false) {

                            //#012d234  SUBU t7, s2, t4
                            t7 = s2 - t4;
                            //#012d238  SLL t7, t7, 6
                            //#012d23c  ADDU t6, t5, t6
                            //#012d240  ADDU t6, t6, t7
                            //#012d244  BEQ zero, zero, $00129dd0
                            //#012d248  ADDIU t5, t6, $0010     ; t5=&T5[s2-offMset.cjMdlx].Sxyz
                            {
                                //#0129dd0  LW t7, $0014(s0)    ; t7=&tmpb
                                //#0129dd4  ADDU t6, t7, t2
                                //#0129dd8  ADDU t7, t7, s8     ; t7=&tmpb[s2]
                                //#0129ddc  LWC1 $f1, $0008(t6) ; 
                                float f1 = tmpb[t6].Z;
                                //#0129de0  LWC1 $f0, $0008(t7)
                                f0 = tmpb[s2].Z;
                                //#0129de4  NOP 
                                //#0129de8  NOP 
                                //#0129dec  DIV.S $f0, $f0, $f1
                                f0 = f0 / f1;
                                //#0129df0  SWC1 $f0, $0008(t5)
                                //#0129df4  LUI t7, $0020
                                offMset.alt5[s2 - t4].sz = f0;
                            }
                        }
                        else {
                            //#0129dc8  LW t7, $001c(s5)    ; t7=&s5.Sxyz
                            //#0129dcc  ADDU t5, t7, s8     ; t5=&s5.Sxyz[s2]

                            //#0129dd0  LW t7, $0014(s0)    ; t7=&tmpb
                            //#0129dd4  ADDU t6, t7, t2
                            //#0129dd8  ADDU t7, t7, s8     ; t7=&tmpb[s2]
                            //#0129ddc  LWC1 $f1, $0008(t6) ; 
                            float f1 = tmpb[t6].Z;
                            //#0129de0  LWC1 $f0, $0008(t7)
                            f0 = tmpb[s2].Z;
                            //#0129de4  NOP 
                            //#0129de8  NOP 
                            //#0129dec  DIV.S $f0, $f0, $f1
                            f0 = f0 / f1;
                            //#0129df0  SWC1 $f0, $0008(t5)
                            //#0129df4  LUI t7, $0020
                            s5.Sxyz[s2].Z = f0;
                        }
                    }
                }
            }
            {
                t7 = 0x00200000;
                //#0129df8  AND t7, s4, t7
                t7 &= s4;
                //#0129dfc  BEQ t7, zero, $0012a9d0
                //#0129e00  LUI t7, $0040
                if (t7 == 0) {
                    //#012a9d0  SRL t3, s4, 16
                    int t3 = s4 >> 16;
                    //#012a9d4  MTC1 zero, $f20
                    float f20 = 0;
                    //#012a9d8  SW t3, $024c(sp)
                    int SP024c = t3;
                    //#012a9dc  ANDI t6, t3, $0003
                    t6 = t3 & 3;
                    //#012a9e0  SW zero, $0224(sp)
                    int SP0224 = 0;
                    //#012a9e4  ADDIU t7, zero, $0001
                    t7 = 1;
                    //#012a9e8  SW zero, $0228(sp)
                    int SP0228 = 0;
                    //#012a9ec  BEQ t6, t7, $0012caf0
                    //#012a9f0  SW zero, $022c(sp)
                    if (t6 == t7) {
                        //HERE:
                        //#012caf0  LW t4, $0004(s0)
                        //#012caf4  LHU t5, $0010(t4)
                        //#012caf8  SLT t7, s2, t5
                        //#012cafc  BEQ t7, zero, $0012d218
                        //#012cb00  ADDIU t3, sp, $0090
                        //#012cb04  LW t7, $0024(s5)
                        //#012cb08  ADDU t6, t7, s8
                        //#012cb0c  BEQ t3, t6, $0012cb24
                        //#012cb10  LUI t5, $0038
                        //#012cb14  LQ t0, $0000(t6)
                        //#012cb18  SQ t0, $0000(t3)
                        //#012cb1c  LW t4, $0004(s0)
                        //#012cb20  LUI t5, $0038
                        //#012cb24  ADDIU t5, t5, $8364
                        //#012cb28  LWC1 $f0, $0000(t5)
                        //#012cb2c  SWC1 $f0, $009c(sp)
                        //#012cb30  LHU t6, $0010(t4)
                        //#012cb34  LW t4, $0214(sp)
                        //#012cb38  SLT t7, t4, t6
                        //#012cb3c  BEQ t7, zero, $0012d200
                        //#012cb40  SLL t5, t4, 6
                        //#012cb44  LW t7, $002c(s5)
                        //#012cb48  SW t5, $0250(sp)
                        //#012cb4c  ADDU t6, t7, t5
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
                        //#012cb88  LW t7, $0008(s0)
                        //#012cb8c  LQ t0, $0000(t8)
                        //#012cb90  SQ t0, $0000(t6)
                        //#012cb94  LW t7, $0008(s0)
                        //#012cb98  ADDIU s3, sp, $0040
                        //#012cb9c  LW t3, $0250(sp)
                        //#012cba0  ADDU t7, t7, t3
                        //#012cba4  BEQ s3, t7, $0012cbcc
                        //#012cba8  NOP 
                        //#012cbac  LQ t0, $0000(t7)
                        //#012cbb0  LQ t1, $0010(t7)
                        //#012cbb4  LQ t2, $0020(t7)
                        //#012cbb8  LQ t3, $0030(t7)
                        //#012cbbc  SQ t0, $0000(s3)
                        //#012cbc0  SQ t1, $0010(s3)
                        //#012cbc4  SQ t2, $0020(s3)
                        //#012cbc8  SQ t3, $0030(s3)
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
                        //#012cc60  LQ t0, $0000(s7)
                        //#012cc64  SQ t0, $0000(t5)
                        //#012cc68  LW a2, $0220(sp)
                        //#012cc6c  ADDIU a3, sp, $0110
                        //#012cc70  DADDU a0, s0, zero
                        //#012cc74  DADDU a1, s5, zero
                        //#012cc78  DADDU t0, s3, zero
                        //#012cc7c  JAL $00128de8
                        //#012cc80  ADDIU t1, sp, $00b0
                        //#012cc84  LW t6, $0218(sp)
                        //#012cc88  BEQ t6, zero, $0012d12c
                        //#012cc8c  ADDIU t7, zero, $0004
                        //#012cc90  LBU t6, $0000(t6)
                        //#012cc94  BNEL t6, t7, $0012d130
                        //#012cc98  LW t4, $0004(s0)
                        //#012cc9c  LW t7, $0218(sp)
                        //#012cca0  LW t6, $0004(s0)
                        //#012cca4  LHU t5, $0004(t7)
                        //#012cca8  LHU t6, $0010(t6)
                        //#012ccac  SLT t7, t5, t6
                        //#012ccb0  BEQ t7, zero, $0012d120
                        //#012ccb4  SUBU t7, t5, t6
                        //#012ccb8  LW t6, $002c(s5)
                        //#012ccbc  SLL t7, t5, 6
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
                        //#012cd10  LW a0, $0014(s5)
                        //#012cd14  LW t7, $0000(a0)
                        //#012cd18  LW v0, $0020(t7)
                        //#012cd1c  JALR ra, v0
                        //#012cd20  SLL s3, s2, 6
                        //#012cd24  LW t4, $0004(s0)
                        //#012cd28  ADDU v0, v0, s3
                        //#012cd2c  LW t3, $0230(sp)
                        //#012cd30  LWC1 $f0, $001c(v0)
                        //#012cd34  LWC1 $f1, $0000(t3)
                        //#012cd38  LHU t6, $0010(t4)
                        //#012cd3c  SLT t7, s2, t6
                        //#012cd40  BEQ t7, zero, $0012d0f0
                        //#012cd44  MUL.S $f12, $f1, $f0
                        //#012cd48  LW t7, $0028(s5)
                        //#012cd4c  ADDU a3, t7, s3
                        //#012cd50  LHU t5, $0010(t4)
                        //#012cd54  SLT t7, s2, t5
                        //#012cd58  BEQL t7, zero, $0012d0d8
                        //#012cd5c  LW t6, $0018(t4)
                        //#012cd60  LW t7, $0020(s5)
                        //#012cd64  ADDU t6, t7, s8
                        //#012cd68  LWC1 $f14, $0000(t6)
                        //#012cd6c  ADDIU a0, sp, $00a0
                        //#012cd70  MTC1 zero, $f13
                        //#012cd74  ADDIU a1, sp, $00b0
                        //#012cd78  DADDU t1, s7, zero
                        //#012cd7c  DADDU a2, zero, zero
                        //#012cd80  DADDU t0, zero, zero
                        //#012cd84  JAL $00124a88
                        //#012cd88  MOV.S $f15, $f13
                        //#012cd8c  LUI t7, $0040
                        //#012cd90  AND t7, s4, t7
                        //#012cd94  BEQL t7, zero, $0012cddc
                        //#012cd98  LW t7, $0004(s0)
                        //#012cd9c  LW t4, $0004(s0)
                        //#012cda0  LHU t6, $0010(t4)
                        //#012cda4  SLT t7, s2, t6
                        //#012cda8  BEQ t7, zero, $0012d0c8
                        //#012cdac  SUBU t7, s2, t6
                        //#012cdb0  LW t7, $0028(s5)
                        //#012cdb4  ADDU a0, t7, s3
                        //#012cdb8  LHU t5, $0010(t4)
                        //#012cdbc  SLT t7, s2, t5
                        //#012cdc0  BEQL t7, zero, $0012d0b0
                        //#012cdc4  LW t6, $0018(t4)
                        //#012cdc8  LW t7, $0020(s5)
                        //#012cdcc  ADDU a1, t7, s8
                        //#012cdd0  JAL $0011b840
                        //#012cdd4  NOP 
                        //#012cdd8  LW t7, $0004(s0)
                        //#012cddc  LHU t7, $0010(t7)
                        //#012cde0  SLT t6, s2, t7
                        //#012cde4  BEQ t6, zero, $0012d0a0
                        //#012cde8  SUBU t7, s2, t7
                        //#012cdec  LW t7, $0028(s5)
                        //#012cdf0  ADDU t6, t7, s3
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
                        //#012ce1c  LW t7, $0028(s5)
                        //#012ce20  ADDU t6, t7, s3
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
                        //#012ce9c  LW t4, $0004(s0)
                        //#012cea0  LQ t0, $0000(t5)
                        //#012cea4  LQ t1, $0010(t5)
                        //#012cea8  LQ t2, $0020(t5)
                        //#012ceac  LQ t3, $0030(t5)
                        //#012ceb0  SQ t0, $0000(t4)
                        //#012ceb4  SQ t1, $0010(t4)
                        //#012ceb8  SQ t2, $0020(t4)
                        //#012cebc  SQ t3, $0030(t4)
                        //#012cec0  LW t4, $0004(s0)
                        //#012cec4  LHU t6, $0010(t4)
                        //#012cec8  SLT t7, s2, t6
                        //#012cecc  BEQ t7, zero, $0012d07c
                        //#012ced0  SUBU t7, s2, t6
                        //#012ced4  LW t7, $002c(s5)
                        //#012ced8  ADDU t6, t7, s3
                        //#012cedc  LW t7, $0008(s0)
                        //#012cee0  ADDU t7, t7, s3
                        //#012cee4  BEQL t6, t7, $0012cf14
                        //#012cee8  LHU t6, $0010(t4)
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
                        //#012cf14  SLT t7, s2, t6
                        //#012cf18  BEQ t7, zero, $0012d06c
                        //#012cf1c  SUBU t7, s2, t6
                        //#012cf20  LW t7, $002c(s5)
                        //#012cf24  ADDU t6, t7, s3
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
                        //#012cf6c  LW t7, $002c(s5)
                        //#012cf70  ADDU t6, t7, s3
                        //#012cf74  ADDIU t6, t6, $0030
                        //#012cf78  ADDIU t5, sp, $0080
                        //#012cf7c  BEQL t6, t5, $0012cf94
                        //#012cf80  LW t6, $0220(sp)
                        //#012cf84  LQ t0, $0000(t5)
                        //#012cf88  SQ t0, $0000(t6)
                        //#012cf8c  LW t4, $0004(s0)
                        //#012cf90  LW t6, $0220(sp)
                        //#012cf94  LHU a2, $0002(t6)
                        //#012cf98  LHU t6, $0010(t4)
                        //#012cf9c  ANDI t5, a2, $ffff
                        //#012cfa0  SLT t7, t5, t6
                        //#012cfa4  BEQ t7, zero, $0012d050
                        //#012cfa8  SUBU t7, t5, t6
                        //#012cfac  LW t6, $002c(s5)
                        //#012cfb0  SLL t7, t5, 6
                        //#012cfb4  LHU t5, $0010(t4)
                        //#012cfb8  ADDU t6, t6, t7
                        //#012cfbc  SLT t7, s2, t5
                        //#012cfc0  BEQ t7, zero, $0012d03c
                        //#012cfc4  ADDIU t3, t6, $0030
                        //#012cfc8  LW t7, $002c(s5)
                        //#012cfcc  ADDU v0, t7, s3
                        //#012cfd0  LHU t5, $0010(t4)
                        //#012cfd4  ANDI a2, a2, $ffff
                        //#012cfd8  SLT t7, a2, t5
                        //#012cfdc  BEQ t7, zero, $0012d020
                        //#012cfe0  SLL t7, a2, 4
                        //#012cfe4  LW t6, $0024(s5)
                        //#012cfe8  ADDU a2, t6, t7
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
                    //HERE:
                    //#012a9f4  BEQ t6, zero, $0012ba08
                    //#012a9f8  SLTIU t7, t6, $0004
                    //#012a9fc  BEQ t7, zero, $0012ba08
                    //#012aa00  LW t7, $0218(sp)
                    //#012aa04  BEQ t7, zero, $0012aa1c
                    //#012aa08  LW t3, $0218(sp)
                    //#012aa0c  LBU t7, $0000(t7)
                    //#012aa10  XORI t7, t7, $0004
                    //#012aa14  MOVN t3, zero, t7
                    //#012aa18  SW t3, $0218(sp)
                    //#012aa1c  LW t4, $0004(s0)
                    //#012aa20  LHU t5, $0010(t4)
                    //#012aa24  SLT t7, s2, t5
                    //#012aa28  BEQ t7, zero, $0012b9ec
                    //#012aa2c  ADDIU t3, sp, $0090
                    //#012aa30  LW t7, $0024(s5)
                    //#012aa34  ADDU t6, t7, s8
                    //#012aa38  BEQ t3, t6, $0012aa50
                    //#012aa3c  LUI t5, $0038
                    //#012aa40  LQ t0, $0000(t6)
                    //#012aa44  SQ t0, $0000(t3)
                    //#012aa48  LW t4, $0004(s0)
                    //#012aa4c  LUI t5, $0038
                    //#012aa50  ADDIU t5, t5, $8364
                    //#012aa54  LWC1 $f0, $0000(t5)
                    //#012aa58  SWC1 $f0, $009c(sp)
                    //#012aa5c  LHU t6, $0010(t4)
                    //#012aa60  LW t4, $0214(sp)
                    //#012aa64  SLT t7, t4, t6
                    //#012aa68  BEQ t7, zero, $0012b9d4
                    //#012aa6c  SLL t5, t4, 6
                    //#012aa70  LW t7, $002c(s5)
                    //#012aa74  SW t5, $0250(sp)
                    //#012aa78  ADDU t6, t7, t5
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
                    //#012aab4  LQ t0, $0000(t7)
                    //#012aab8  SQ t0, $0000(t6)
                    //#012aabc  LW t7, $0008(s0)
                    //#012aac0  ADDIU a1, sp, $0040
                    //#012aac4  LW t3, $0250(sp)
                    //#012aac8  ADDU t7, t7, t3
                    //#012aacc  BEQ a1, t7, $0012aaf4
                    //#012aad0  NOP 
                    //#012aad4  LQ t0, $0000(t7)
                    //#012aad8  LQ t1, $0010(t7)
                    //#012aadc  LQ t2, $0020(t7)
                    //#012aae0  LQ t3, $0030(t7)
                    //#012aae4  SQ t0, $0000(a1)
                    //#012aae8  SQ t1, $0010(a1)
                    //#012aaec  SQ t2, $0020(a1)
                    //#012aaf0  SQ t3, $0030(a1)
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
                    //#012ab88  LQ t0, $0000(t6)
                    //#012ab8c  SQ t0, $0000(t5)
                    //#012ab90  LW t7, $0204(sp)
                    //#012ab94  BEQ s1, t7, $0012abc8
                    //#012ab98  ADDIU s7, s2, $0001
                    //#012ab9c  LHU t7, $0002(s1)
                    //#012aba0  ANDI t6, s7, $ffff
                    //#012aba4  BNEL t7, t6, $0012abcc
                    //#012aba8  LW t5, $0004(s0)
                    //#012abac  LW t3, $0204(sp)
                    //#012abb0  ADDIU s1, s1, $000c
                    //#012abb4  BEQL s1, t3, $0012abcc
                    //#012abb8  LW t5, $0004(s0)
                    //#012abbc  LHU t7, $0002(s1)
                    //#012abc0  BEQL t7, t6, $0012abb4
                    //#012abc4  ADDIU s1, s1, $000c
                    //#012abc8  LW t5, $0004(s0)
                    //#012abcc  LW t7, $0014(s0)
                    //#012abd0  LHU t2, $0010(t5)
                    //#012abd4  ADDU t4, t7, s8
                    //#012abd8  SLT t7, s7, t2
                    //#012abdc  BEQ t7, zero, $0012b9b8
                    //#012abe0  ADDIU t3, t4, $0010
                    //#012abe4  LW t6, $001c(s5)
                    //#012abe8  SLL t7, s7, 4
                    //#012abec  ADDU t6, t6, t7
                    //#012abf0  ADDIU t5, sp, $0120
                    //#012abf4  LQC2 vf1, $0000(t4)
                    //#012abf8  LQC2 vf2, $0000(t6)
                    //#012abfc  VMUL.xyzw vf1, vf1, vf2
                    //#012ac00  SQC2 vf1, $0000(t5)
                    //#012ac04  BEQ t3, t5, $0012ac14
                    //#012ac08  DADDU t6, t5, zero
                    //#012ac0c  LQ t0, $0000(t6)
                    //#012ac10  SQ t0, $0000(t3)
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
                    //#012ac3c  SW t7, $0224(sp)
                    //#012ac40  LD t5, $0010(t7)
                    //#012ac44  ADDIU t7, zero, $0001
                    //#012ac48  DSLL32 t7, t7, 21
                    //#012ac4c  AND t7, t5, t7
                    //#012ac50  BNE t7, zero, $0012acd8
                    //#012ac54  LW t3, $0228(sp)
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
                    //#012ac90  LW t7, $0000(a0)
                    //#012ac94  LW v0, $0018(t7)
                    //#012ac98  JALR ra, v0
                    //#012ac9c  DADDU a1, s3, zero
                    //#012aca0  BEQL s2, v0, $0012b97c
                    //#012aca4  LW a0, $0014(s5)
                    //#012aca8  ADDIU s3, s3, $0001
                    //#012acac  SLTI t7, s3, $0024
                    //#012acb0  BNEL t7, zero, $0012ac90
                    //#012acb4  LW a0, $0014(s5)
                    //#012acb8  LW t5, $0224(sp)
                    //#012acbc  ADDIU t7, zero, $0001
                    //#012acc0  DSLL32 t7, t7, 19
                    //#012acc4  LD t6, $0010(t5)
                    //#012acc8  AND t6, t6, t7
                    //#012accc  BNEL t6, zero, $0012acd4
                    //#012acd0  LWC1 $f20, $0018(t5)
                    //#012acd4  LW t3, $0228(sp)
                    //#012acd8  BEQ t3, zero, $0012af80
                    //#012acdc  LW t4, $01f0(sp)
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
                    //#012ad1c  LQ t0, $0000(s3)
                    //#012ad20  SQ t0, $0000(t7)
                    //#012ad24  LWC1 $f0, $0134(sp)
                    //#012ad28  ADDIU t6, sp, $0120
                    //#012ad2c  ADD.S $f0, $f0, $f20
                    //#012ad30  BEQ t6, t7, $0012ad40
                    //#012ad34  SWC1 $f0, $0134(sp)
                    //#012ad38  LQ t0, $0000(t7)
                    //#012ad3c  SQ t0, $0000(t6)
                    //#012ad40  LW t5, $0004(s0)
                    //#012ad44  LW t7, $0014(s0)
                    //#012ad48  LHU t4, $0010(t5)
                    //#012ad4c  ADDU t7, t7, s8
                    //#012ad50  ADDIU t7, t7, $0010
                    //#012ad54  SLT t6, s7, t4
                    //#012ad58  BEQ t6, zero, $0012b964
                    //#012ad5c  SW t7, $0234(sp)
                    //#012ad60  LW a0, $0014(s5)
                    //#012ad64  LW t7, $0000(a0)
                    //#012ad68  LW v0, $0020(t7)
                    //#012ad6c  JALR ra, v0
                    //#012ad70  NOP 
                    //#012ad74  SLL t7, s7, 6
                    //#012ad78  ADDU v0, v0, t7
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
                    //#012ada4  LW t5, $0004(s0)
                    //#012ada8  LW t7, $0014(s0)
                    //#012adac  LHU t4, $0010(t5)
                    //#012adb0  ADDU t7, t7, s8
                    //#012adb4  ADDIU t7, t7, $0010
                    //#012adb8  SLT t6, s7, t4
                    //#012adbc  BEQ t6, zero, $0012b94c
                    //#012adc0  SW t7, $0238(sp)
                    //#012adc4  LW a0, $0014(s5)
                    //#012adc8  LW t7, $0000(a0)
                    //#012adcc  LW v0, $0020(t7)
                    //#012add0  JALR ra, v0
                    //#012add4  NOP 
                    //#012add8  SLL t7, s7, 6
                    //#012addc  ADDU v0, v0, t7
                    //#012ade0  LW t4, $0238(sp)
                    //#012ade4  LWC1 $f2, $001c(v0)
                    //#012ade8  LWC1 $f0, $0000(t4)
                    //#012adec  LWC1 $f1, $0134(sp)
                    //#012adf0  MUL.S $f0, $f0, $f2
                    //#012adf4  ADD.S $f0, $f0, $f20
                    //#012adf8  ADD.S $f1, $f1, $f0
                    //#012adfc  SWC1 $f1, $0134(sp)
                    //#012ae00  ADDIU a0, sp, $0120
                    //#012ae04  JAL $00143ec8
                    //#012ae08  DADDU a1, s3, zero
                    //#012ae0c  BEQ v0, zero, $0012af80
                    //#012ae10  LW t5, $0224(sp)
                    //#012ae14  LW t7, $0010(t5)
                    //#012ae18  ORI t7, t7, $0001
                    //#012ae1c  BEQ t5, s3, $0012ae2c
                    //#012ae20  SW t7, $0010(t5)
                    //#012ae24  LQ t0, $0000(s3)
                    //#012ae28  SQ t0, $0000(t5)
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
                    //#012af80  LW t6, $0218(sp)
                    //#012af84  BEQ t6, zero, $0012b7a8
                    //#012af88  LW t7, $0218(sp)
                    //#012af8c  LW t6, $0004(s0)
                    //#012af90  LHU t5, $0004(t7)
                    //#012af94  LHU t6, $0010(t6)
                    //#012af98  SLT t7, t5, t6
                    //#012af9c  BEQ t7, zero, $0012b79c
                    //#012afa0  SUBU t7, t5, t6
                    //#012afa4  LW t6, $002c(s5)
                    //#012afa8  SLL t7, t5, 6
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
                    //#012b004  LW a0, $0014(s5)
                    //#012b008  LW t7, $0000(a0)
                    //#012b00c  LW v0, $0020(t7)
                    //#012b010  JALR ra, v0
                    //#012b014  SLL s3, s2, 6
                    //#012b018  LW t4, $0004(s0)
                    //#012b01c  ADDU v0, v0, s3
                    //#012b020  LW t3, $0014(s0)
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
                    //#012b04c  LW a0, $0014(s5)
                    //#012b050  LW t7, $0000(a0)
                    //#012b054  LW v0, $0020(t7)
                    //#012b058  JALR ra, v0
                    //#012b05c  NOP 
                    //#012b060  SLL t7, s7, 6
                    //#012b064  LW t4, $0004(s0)
                    //#012b068  ADDU v0, v0, t7
                    //#012b06c  LW t3, $0240(sp)
                    //#012b070  LWC1 $f0, $001c(v0)
                    //#012b074  LWC1 $f1, $0000(t3)
                    //#012b078  LHU t6, $0010(t4)
                    //#012b07c  SLT t7, s2, t6
                    //#012b080  BEQ t7, zero, $0012b754
                    //#012b084  MUL.S $f13, $f1, $f0
                    //#012b088  LW t7, $0028(s5)
                    //#012b08c  ADDU a3, t7, s3
                    //#012b090  LHU t6, $0010(t4)
                    //#012b094  SLT t7, s7, t6
                    //#012b098  BEQ t7, zero, $0012b748
                    //#012b09c  SUBU t7, s7, t6
                    //#012b0a0  LW t6, $0028(s5)
                    //#012b0a4  SLL t7, s7, 6
                    //#012b0a8  LHU t5, $0010(t4)
                    //#012b0ac  ADDU t0, t6, t7
                    //#012b0b0  SLT t7, s2, t5
                    //#012b0b4  BEQL t7, zero, $0012b730
                    //#012b0b8  LW t6, $0018(t4)
                    //#012b0bc  LW t7, $0020(s5)
                    //#012b0c0  ADDU t3, t7, s8
                    //#012b0c4  LHU t5, $0010(t4)
                    //#012b0c8  SLT t7, s7, t5
                    //#012b0cc  BEQ t7, zero, $0012b714
                    //#012b0d0  SLL t7, s7, 4
                    //#012b0d4  LW t6, $0020(s5)
                    //#012b0d8  ADDU t6, t6, t7
                    //#012b0dc  LW t4, $024c(sp)
                    //#012b0e0  MOV.S $f12, $f20
                    //#012b0e4  LWC1 $f14, $0000(t3)
                    //#012b0e8  ADDIU a0, sp, $00a0
                    //#012b0ec  ANDI a2, t4, $0003
                    //#012b0f0  LWC1 $f15, $0000(t6)
                    //#012b0f4  ADDIU a2, a2, $fffe
                    //#012b0f8  ADDIU a1, sp, $00b0
                    //#012b0fc  ADDIU t1, sp, $0120
                    //#012b100  JAL $00124a88
                    //#012b104  NOP 
                    //#012b108  LUI t7, $0040
                    //#012b10c  AND t7, s4, t7
                    //#012b110  BEQL t7, zero, $0012b158
                    //#012b114  LW t7, $0004(s0)
                    //#012b118  LW t4, $0004(s0)
                    //#012b11c  LHU t6, $0010(t4)
                    //#012b120  SLT t7, s2, t6
                    //#012b124  BEQ t7, zero, $0012b704
                    //#012b128  SUBU t7, s2, t6
                    //#012b12c  LW t7, $0028(s5)
                    //#012b130  ADDU a0, t7, s3
                    //#012b134  LHU t5, $0010(t4)
                    //#012b138  SLT t7, s2, t5
                    //#012b13c  BEQL t7, zero, $0012b6ec
                    //#012b140  LW t6, $0018(t4)
                    //#012b144  LW t7, $0020(s5)
                    //#012b148  ADDU a1, t7, s8
                    //#012b14c  JAL $0011b840
                    //#012b150  NOP 
                    //#012b154  LW t7, $0004(s0)
                    //#012b158  LHU t7, $0010(t7)
                    //#012b15c  SLT t6, s2, t7
                    //#012b160  BEQ t6, zero, $0012b6dc
                    //#012b164  SUBU t7, s2, t7
                    //#012b168  LW t7, $0028(s5)
                    //#012b16c  ADDU t6, t7, s3
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
                    //#012b198  LW t7, $0028(s5)
                    //#012b19c  ADDU t6, t7, s3
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
                    //#012b21c  LQ t0, $0000(t4)
                    //#012b220  LQ t1, $0010(t4)
                    //#012b224  LQ t2, $0020(t4)
                    //#012b228  LQ t3, $0030(t4)
                    //#012b22c  SQ t0, $0000(t5)
                    //#012b230  SQ t1, $0010(t5)
                    //#012b234  SQ t2, $0020(t5)
                    //#012b238  SQ t3, $0030(t5)
                    //#012b23c  LW t4, $0004(s0)
                    //#012b240  LHU t6, $0010(t4)
                    //#012b244  SLT t7, s2, t6
                    //#012b248  BEQ t7, zero, $0012b6b8
                    //#012b24c  SUBU t7, s2, t6
                    //#012b250  LW t7, $002c(s5)
                    //#012b254  ADDU t6, t7, s3
                    //#012b258  LW t7, $0008(s0)
                    //#012b25c  ADDU t7, t7, s3
                    //#012b260  BEQL t6, t7, $0012b290
                    //#012b264  LHU t6, $0010(t4)
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
                    //#012b290  SLT t7, s2, t6
                    //#012b294  BEQ t7, zero, $0012b6a8
                    //#012b298  SUBU t7, s2, t6
                    //#012b29c  LW t7, $002c(s5)
                    //#012b2a0  ADDU t6, t7, s3
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
                    //#012b2e8  LW t7, $002c(s5)
                    //#012b2ec  ADDU t6, t7, s3
                    //#012b2f0  ADDIU t6, t6, $0030
                    //#012b2f4  ADDIU t5, sp, $0080
                    //#012b2f8  BEQL t6, t5, $0012b310
                    //#012b2fc  LHU t6, $0010(t4)
                    //#012b300  LQ t0, $0000(t5)
                    //#012b304  SQ t0, $0000(t6)
                    //#012b308  LW t4, $0004(s0)
                    //#012b30c  LHU t6, $0010(t4)
                    //#012b310  SLT t7, s7, t6
                    //#012b314  BEQ t7, zero, $0012b68c
                    //#012b318  SUBU t7, s7, t6
                    //#012b31c  LW t6, $0028(s5)
                    //#012b320  SLL t7, s7, 6
                    //#012b324  LHU t5, $0010(t4)
                    //#012b328  ADDU t6, t6, t7
                    //#012b32c  SLT t7, s2, t5
                    //#012b330  BEQ t7, zero, $0012b674
                    //#012b334  ADDIU s4, t6, $0030
                    //#012b338  LW a0, $0014(s5)
                    //#012b33c  LW t7, $0000(a0)
                    //#012b340  LW v0, $0020(t7)
                    //#012b344  JALR ra, v0
                    //#012b348  NOP 
                    //#012b34c  ADDU v0, v0, s3
                    //#012b350  LWC1 $f0, $001c(v0)
                    //#012b354  SWC1 $f0, $0000(s4)
                    //#012b358  LW t7, $0004(s0)
                    //#012b35c  LW t6, $0008(s0)
                    //#012b360  LHU t3, $0010(t7)
                    //#012b364  ADDU t5, t6, s3
                    //#012b368  SLT t7, s7, t3
                    //#012b36c  BEQ t7, zero, $0012b664
                    //#012b370  ADDIU t4, t5, $0040
                    //#012b374  LW t6, $0028(s5)
                    //#012b378  SLL t7, s7, 6
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
                    //#012b3f8  LW t4, $0004(s0)
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
                    //#012b424  LHU t6, $0010(t4)
                    //#012b428  SLT t7, s7, t6
                    //#012b42c  BEQ t7, zero, $0012b658
                    //#012b430  SUBU t7, s7, t6
                    //#012b434  LW t6, $002c(s5)
                    //#012b438  SLL t7, s7, 6
                    //#012b43c  ADDU t6, t6, t7
                    //#012b440  LW t7, $0008(s0)
                    //#012b444  ADDU t7, t7, s3
                    //#012b448  ADDIU t7, t7, $0040
                    //#012b44c  BEQL t6, t7, $0012b47c
                    //#012b450  LHU t6, $0010(t4)
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
                    //#012b47c  SLT t7, s7, t6
                    //#012b480  BEQ t7, zero, $0012b64c
                    //#012b484  SUBU t7, s7, t6
                    //#012b488  LW t6, $002c(s5)
                    //#012b48c  SLL t7, s7, 6
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
                    //#012b4dc  LW t6, $002c(s5)
                    //#012b4e0  SLL t7, s7, 6
                    //#012b4e4  LHU t5, $0010(t4)
                    //#012b4e8  ADDU t6, t6, t7
                    //#012b4ec  SLT t7, s2, t5
                    //#012b4f0  BEQ t7, zero, $0012b62c
                    //#012b4f4  ADDIU t3, t6, $0030
                    //#012b4f8  LW t7, $002c(s5)
                    //#012b4fc  ADDU v0, t7, s3
                    //#012b500  LHU t6, $0010(t4)
                    //#012b504  SLT t7, s7, t6
                    //#012b508  BEQ t7, zero, $0012b620
                    //#012b50c  SUBU t7, s7, t6
                    //#012b510  LW t6, $0028(s5)
                    //#012b514  SLL t7, s7, 6
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
                    //#012b56c  LW t6, $002c(s5)
                    //#012b570  SLL t7, t5, 6
                    //#012b574  ADDU t7, t6, t7
                    //#012b578  LHU t6, $0010(t3)
                    //#012b57c  ADDIU t2, t7, $0030
                    //#012b580  SLT t7, s7, t6
                    //#012b584  BEQ t7, zero, $0012b604
                    //#012b588  SUBU t7, s7, t6
                    //#012b58c  LW t6, $002c(s5)
                    //#012b590  SLL t7, s7, 6
                    //#012b594  LHU t5, $0010(t3)
                    //#012b598  ADDU t4, t6, t7
                    //#012b59c  ANDI a2, a2, $ffff
                    //#012b5a0  SLT t7, a2, t5
                    //#012b5a4  BEQ t7, zero, $0012b5e8
                    //#012b5a8  SLL t7, a2, 4
                    //#012b5ac  LW t6, $0024(s5)
                    //#012b5b0  ADDU a2, t6, t7
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
                //HERE:
                //#0129e04  AND t7, s4, t7
                //#0129e08  BEQ t7, zero, $00129e60
                //#0129e0c  LW t3, $01f4(sp)
                //#0129e10  LW t4, $0004(s0)
                //#0129e14  LHU t6, $0010(t4)
                //#0129e18  SLT t7, s2, t6
                //#0129e1c  BEQ t7, zero, $0012a9bc
                //#0129e20  SLL s3, s2, 6
                //#0129e24  LW t7, $0028(s5)
                //#0129e28  ADDU a0, t7, s3
                //#0129e2c  LHU t5, $0010(t4)
                //#0129e30  SLT t7, s2, t5
                //#0129e34  BNEL t7, zero, $0012a9b4
                //#0129e38  LW t7, $0020(s5)
                //#0129e3c  LW t6, $0018(t4)
                //#0129e40  SUBU t7, s2, t5
                //#0129e44  SLL t7, t7, 6
                //#0129e48  ADDU t6, t4, t6
                //#0129e4c  ADDU t6, t6, t7
                //#0129e50  ADDIU a1, t6, $0020
                //#0129e54  JAL $0011b840
                //#0129e58  NOP 
                //#0129e5c  LW t3, $01f4(sp)
                //#0129e60  BEQL t3, zero, $0012a498
                //#0129e64  LW t3, $01fc(sp)
                //#0129e68  LHU t7, $0010(t3)
                //#0129e6c  BNEL t7, s2, $0012a498
                //#0129e70  LW t3, $01fc(sp)
                //#0129e74  LW t6, $0004(s0)
                //#0129e78  LHU t6, $0010(t6)
                //#0129e7c  SLT t7, s2, t6
                //#0129e80  BEQ t7, zero, $0012a99c
                //#0129e84  ADDIU s4, sp, $00d0
                //#0129e88  LW t7, $0028(s5)
                //#0129e8c  SLL s3, s2, 6
                //#0129e90  ADDU t6, t7, s3
                //#0129e94  BEQ s4, t6, $00129ec0
                //#0129e98  LW t4, $01f4(sp)
                //#0129e9c  LQ t0, $0000(t6)
                //#0129ea0  LQ t1, $0010(t6)
                //#0129ea4  LQ t2, $0020(t6)
                //#0129ea8  LQ t3, $0030(t6)
                //#0129eac  SQ t0, $0000(s4)
                //#0129eb0  SQ t1, $0010(s4)
                //#0129eb4  SQ t2, $0020(s4)
                //#0129eb8  SQ t3, $0030(s4)
                //#0129ebc  LW t4, $01f4(sp)
                //#0129ec0  LUI t6, $0001
                //#0129ec4  LD t7, $0010(t4)
                //#0129ec8  AND t7, t7, t6
                //#0129ecc  BNE t7, zero, $0012a6b4
                //#0129ed0  LW t7, $0214(sp)
                //#0129ed4  LW t5, $0214(sp)
                //#0129ed8  BLTZ t5, $0012a63c
                //#0129edc  ADDIU a1, sp, $0040
                //#0129ee0  LW t6, $0004(s0)
                //#0129ee4  LHU t6, $0010(t6)
                //#0129ee8  SLT t7, t5, t6
                //#0129eec  BEQ t7, zero, $0012a628
                //#0129ef0  DADDU t4, a1, zero
                //#0129ef4  LW t6, $002c(s5)
                //#0129ef8  SLL t7, t5, 6
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
                //#0129f80  LW t6, $0004(s0)
                //#0129f84  LQ t0, $0000(t8)
                //#0129f88  LQ t1, $0010(t8)
                //#0129f8c  LQ t2, $0020(t8)
                //#0129f90  LQ t3, $0030(t8)
                //#0129f94  SQ t0, $0000(t4)
                //#0129f98  SQ t1, $0010(t4)
                //#0129f9c  SQ t2, $0020(t4)
                //#0129fa0  SQ t3, $0030(t4)
                //#0129fa4  LW t6, $0004(s0)
                //#0129fa8  LW t3, $0214(sp)
                //#0129fac  LHU t6, $0010(t6)
                //#0129fb0  SLT t7, t3, t6
                //#0129fb4  BEQ t7, zero, $0012a618
                //#0129fb8  ADDIU t4, sp, $0080
                //#0129fbc  LW t6, $002c(s5)
                //#0129fc0  SLL t7, t3, 6
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
                //#0129ffc  LQ t0, $0000(t5)
                //#012a000  SQ t0, $0000(t4)
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
                //#012a110  LQ t0, $0000(t8)
                //#012a114  SQ t0, $0000(t5)
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
                //#012a188  LQ t0, $0000(t8)
                //#012a18c  SQ t0, $0000(t6)
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
                //#012a2d0  LW t7, $0028(s5)
                //#012a2d4  ADDU t6, t7, s3
                //#012a2d8  BEQ t6, s4, $0012a308
                //#012a2dc  LW t5, $0214(sp)
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
                //#012a308  BLTZ t5, $0012a534
                //#012a30c  SLL t7, t5, 6
                //#012a310  LHU t3, $0010(t4)
                //#012a314  LW t6, $0008(s0)
                //#012a318  SLT t5, s2, t3
                //#012a31c  ADDU v0, t6, t7
                //#012a320  BEQ t5, zero, $0012a520
                //#012a324  ADDU t4, t6, s3
                //#012a328  LW t7, $0028(s5)
                //#012a32c  ADDU t6, t7, s3
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
                //#012a3a4  LW t4, $0004(s0)
                //#012a3a8  LQ t0, $0000(t8)
                //#012a3ac  LQ t1, $0010(t8)
                //#012a3b0  LQ t2, $0020(t8)
                //#012a3b4  LQ t3, $0030(t8)
                //#012a3b8  SQ t0, $0000(t4)
                //#012a3bc  SQ t1, $0010(t4)
                //#012a3c0  SQ t2, $0020(t4)
                //#012a3c4  SQ t3, $0030(t4)
                //#012a3c8  LW t4, $0004(s0)
                //#012a3cc  LHU t6, $0010(t4)
                //#012a3d0  SLT t7, s2, t6
                //#012a3d4  BEQ t7, zero, $0012a510
                //#012a3d8  SUBU t7, s2, t6
                //#012a3dc  LW t7, $002c(s5)
                //#012a3e0  ADDU t6, t7, s3
                //#012a3e4  LW t7, $0008(s0)
                //#012a3e8  ADDU t7, t7, s3
                //#012a3ec  BEQL t6, t7, $0012a41c
                //#012a3f0  LHU t6, $0010(t4)
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
                //#012a41c  SLT t7, s2, t6
                //#012a420  BEQ t7, zero, $0012a500
                //#012a424  SUBU t7, s2, t6
                //#012a428  LW t7, $002c(s5)
                //#012a42c  ADDU t6, t7, s3
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
                //#012a474  LW t7, $002c(s5)
                //#012a478  ADDU v0, t7, s3
                //#012a47c  ADDIU v0, v0, $0030
                //#012a480  ADDIU t6, sp, $0080
                //#012a484  BEQ v0, t6, $0012a498
                //#012a488  LW t3, $01fc(sp)
                //#012a48c  LQ t0, $0000(t6)
                //#012a490  SQ t0, $0000(v0)
                //#012a494  LW t3, $01fc(sp)
                //#012a498  LW t4, $0210(sp)
                //#012a49c  LW t5, $0200(sp)
                //#012a4a0  LHU t7, $0012(t3)
                //#012a4a4  ADDIU t4, t4, $0001
                //#012a4a8  ADDIU t5, t5, $0004
                //#012a4ac  SW t4, $0210(sp)
                //#012a4b0  SLT t7, t4, t7
                //#012a4b4  BNE t7, zero, $00129ad8
                //#012a4b8  SW t5, $0200(sp)
                //#012a4bc  LD s0, $02a0(sp)
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
        }
    }
}
