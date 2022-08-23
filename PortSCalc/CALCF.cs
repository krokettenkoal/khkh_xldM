using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PortSCalc {
    class CALCF {
        InfoTbl s5;
        Msetacc offMset;
        float f12;

        public CALCF(InfoTbl it, Msetacc offMset, float f12) {
            this.s5 = it;
            this.offMset = offMset;
            this.f12 = f12;
        }

        void fn1283d8(out int SP0, out int SP4) {
            //#                             ; a0=offMset
            //#                             ; a1=&SP0
            //#                             ; a2=&SP4

            //#01283d8  LW t5, $0004(a0)    ; t5=offMotionHeader
            //#01283dc  LW t6, $0044(t5)    ; t6=offMotionHeader.offt11
            //#01283e0  LW t4, $0020(t5)    ; t4=offMotionHeader.cntt11
            //#01283e4  ADDU t3, t5, t6     ; t3=offMotionHeader.offt11.ptr
            //#01283e8  SLL t7, t4, 2       ; t7=(offMotionHeader.cntt11 << 2)
            //#01283ec  ADDU t7, t3, t7     ; t7=&(offMotionHeader.offt11[offMotionHeader.cntt11])
            //#01283f0  LWC1 $f0, $fffc(t7) ; $f0=Get(&offMotionHeader.offt11[offMotionHeader.cntt11 -1])
            float f0 = offMset.Gett11(offMset.cntt11 - 1);
            //#01283f4  C.LE.S $f0, $f12    ; if ($f0 <= $f12)
            //#01283f8  BC1FL $00128414     ; else { $f0 = Get(offMotionHeader.offt11); goto _128414; }
            //#01283fc  LWC1 $f0, $0000(t3) ; .. BC1FL
            if (f0 <= f12) {
                //#0128400  ADDIU t7, t4, $ffff ; t7=offMotionHeader.cntt11 -1
                //#0128404  SW t7, $0000(a1)    ; Setw(&SP0, (offMotionHeader.cntt11 -1))
                SP0 = offMset.cntt11 - 1;
                //#0128408  LW t6, $0020(t5)    ; t6=offMotionHeader.cntt11
                //#012840c  JR ra               
                //#0128410  SW t6, $0000(a2)    ; Setw(&SP4, offMotionHeader.cntt11)
                SP4 = offMset.cntt11;
                return;
            }
            f0 = offMset.Gett11(0);
            //#0128414  C.LE.S $f12, $f0    ; if ($f12 <= $f0)
            //#0128418  BC1FL $00128430     ; else { Setw(&SP0, 0); goto _128430; }
            //#012841c  SW zero, $0000(a1)  ; .. BC1FL
            if (f12 <= f0) {
                //#0128420  ADDIU t7, zero, $ffff   ; t7=-1
                //#0128424  SW t7, $0000(a1)        ; Setw(&SP0, -1)
                SP0 = -1;
                //#0128428  JR ra   
                //#012842c  SW zero, $0000(a2)      ; Setw(&SP4, 0)
                SP4 = 0;
                return;
            }
            SP0 = 0;
            //#0128430  LW t7, $0020(t5)        ; t7=offMotionHeader.cntt11
            //#0128434  ADDIU t6, t7, $ffff     ; t6=offMotionHeader.cntt11 -1
            //#0128438  SW t6, $0000(a2)        ; Setw(&SP4, offMotionHeader.cntt11 -1)
            SP4 = offMset.cntt11 - 1;
            //#012843c  LW t5, $0000(a1)        ; t5=Getw(&SP0)
            //#0128440  SUBU t7, t6, t5         ; t7=offMotionHeader.cntt11 -1 -Getw(&SP0)
            //#0128444  SLTI t7, t7, $0002      ; t7=(t7<2)
            //#0128448  BNE t7, zero, $00128490 ; if (t7) { t7=Getw(&SP0)+(offMotionHeader.cntt11 -1); goto _128490; }
            if ((offMset.cntt11 - 1 - SP0) >= 2) {
                //#012844c  ADDU t7, t5, t6         ; t7=Getw(&SP0)+(offMotionHeader.cntt11 -1)
                int t7 = SP0 + offMset.cntt11 - 1;
                while (true) {
                    //#0128450  SRL t6, t7, 31          ; t6=(Getw(&SP0)+(offMotionHeader.cntt11 -1)) >> 31
                    //#0128454  ADDU t7, t7, t6         ; t7=Getw(&SP0)+(offMotionHeader.cntt11 -1) + ((Getw(&SP0)+(offMotionHeader.cntt11 -1)) >> 31)
                    if (t7 < 0) t7++;
                    //#0128458  SRA t7, t7, 1           ; t7=(Getw(&SP0)+(offMotionHeader.cntt11 -1) + ((Getw(&SP0)+(offMotionHeader.cntt11 -1)) >> 31)) * 2
                    t7 >>= 1;
                    //#012845c  SLL t6, t7, 2           ; t6=((Getw(&SP0)+(offMotionHeader.cntt11 -1) + ((Getw(&SP0)+(offMotionHeader.cntt11 -1)) >> 31)) * 2) << 2
                    //#0128460  ADDU t6, t6, t3         ; t6=offMotionHeader.t11[((Getw(&SP0)+(offMotionHeader.cntt11 -1) + ((Getw(&SP0)+(offMotionHeader.cntt11 -1)) >> 31)) * 2)]
                    //#0128464  LWC1 $f0, $0000(t6)     ; $f0=Getf(t6)
                    f0 = offMset.Gett11(t7);
                    //#0128468  C.LT.S $f12, $f0        ; if ($f12 < $f0)       
                    //#012846c  BC1FL $00128498         ; else { C=($f0 < $f12); goto _128498; }
                    //#0128470  C.LT.S $f0, $f12        ; .. BC1FL
                    if ((f12 < f0) == false) {
                        //#0128498  BC1FL $001284a8             ; if (!C) { goto _1284a8; }
                        //#012849c  SW t7, $0000(a2)            ; Setw(&SP4, Getw(&SP0)+Getw(&SP4));
                        if ((f0 < f12) == false) {
                            SP4 = t7;
                            //#01284a8  JR ra                       ;
                            //#01284ac  SW t7, $0000(a1)            ; Setw(&SP0, t7);
                            SP0 = t7;
                            return;
                        }
                        else {
                            //#01284a0  BEQ zero, zero, $00128478   ; goto _128478;
                            //#01284a4  SW t7, $0000(a1)            ; Setw(&SP0, Getw(&SP0)+Getw(&SP4)))
                            SP0 = t7;
                        }
                    }
                    else {
                        //#0128474  SW t7, $0000(a2)        ; Setw(&SP4, t7)
                        SP4 = t7;
                    }

                    //#0128478  LW t6, $0000(a2)        ; t6=Getw(&SP4)
                    //#012847c  LW t5, $0000(a1)        ; t5=Getw(&SP0)
                    //#0128480  SUBU t7, t6, t5         ; t7=Getw(&SP4)-Getw(&SP0)
                    t7 = SP4 - SP0;
                    //#0128484  SLTI t7, t7, $0002      ; t7=(t7<2)
                    //#0128488  BEQ t7, zero, $00128450 ; if (t7==0) { t7=Getw(&SP0)+Getw(&SP4); goto _128450; }
                    //#012848c  ADDU t7, t5, t6
                    if ((t7 < 2) == false) {
                        t7 = SP0 + SP4;
                        continue;
                    }

                    //#0128490  JR ra
                    //#0128494  NOP 
                    return;
                }
            }
            //#0128490  JR ra
            //#0128494  NOP 
            return;
        }

        const float _37823c = 1.0f, _378240 = 3.0f;

        float fn128530(T2 o2, int _SP0, int _SP4) {
            //#                             ; a0=offMset00
            //#                             ; a1=offt2.ptr
            //#                             ; a2=_SP0
            //#                             ; a3=_SP4
            //#                             ; $f12=?

            //#                             ; return $f0

            //#0128530  ADDIU sp, sp, $ffa0 ; use stack 96 bytes
            //#0128534  LW t5, $0004(a0)    ; t5=offMset00.offMotionHeader
            //#0128538  SD s0, $0010(sp)
            //#012853c  SD s1, $0018(sp)
            //#0128540  SD s2, $0020(sp)
            //#0128544  DADDU s0, a1, zero  ; s0=offt2.ptr
            T2 s0 = o2;
            //#0128548  SD s3, $0028(sp)
            //#012854c  SD s4, $0030(sp)
            //#0128550  SD s5, $0038(sp)
            //#0128554  SD s6, $0040(sp)
            //#0128558  SWC1 $f20, $0050(sp)    ; store
            //#012855c  SW a3, $0004(sp)    ; SP4=_SP4
            int SP4 = _SP4;
            //#0128560  SD ra, $0048(sp)
            //#0128564  MOV.S $f20, $f12    ; $f20=$f12
            float f20 = f12;
            //#0128568  SW a2, $0000(sp)    ; SP0=_SP0
            int SP0 = _SP0;
            //#012856c  LHU t0, $0004(a1)   ; t0=offt2.ptr.t9index
            int t0 = o2.t9index;
            //#0128570  LW t6, $0040(t5)    ; t6=offMset00.offMotionHeader.offt9
            //#0128574  ANDI t4, t0, $ffff  ; t4=(offt2.ptr.t9index & 0xFFFF)
            int t4 = t0 & 0xffff;
            //#0128578  LBU t1, $0003(a1)   ; t1=offt2.ptr.t9cnt
            //#012857c  ADDU s2, t5, t6     ; s2=offt9.ptr
            //#0128580  LW t3, $0048(t5)    ; t3=offMset00.offMotionHeader.offt10
            //#0128584  SLL t7, t4, 3       ; t7=(offt2.ptr.t9index & 0xFFFF) << 3
            //#0128588  ANDI t6, t1, $00ff  ; t6=(offt2.ptr.t9cnt & 0xFF)
            int t6 = o2.t9cnt;
            //#012858c  ADDU s1, s2, t7     ; s1=&offt9.ptr[(offt2.ptr.t9index & 0xFFFF) << 3]
            //#                             ; s1=&T9[T2.t9index]
            T9 s1 = offMset.Gett9(o2.t9index);
            //#0128590  ADDU t4, t4, t6     ; t4=(offt2.ptr.t9index & 0xFFFF)+(offt2.ptr.t9cnt & 0xFF)
            //#0128594  LHU t2, $0000(s1)   ; t2=Gethw(&offt9.ptr[(offt2.ptr.t9index & 0xFFFF) << 3])
            //#                             ; t2=offt9[offt2.ptr.t9index].t11off
            int t2 = s1.t11off;
            //#0128598  SLL t4, t4, 3       ; t4=((offt2.ptr.t9index & 0xFFFF)+(offt2.ptr.t9cnt & 0xFF)) << 3
            //#012859c  LW t6, $0044(t5)    ; t6=offMset00.offMotionHeader.offt11
            //#01285a0  ADDU t4, s2, t4     ; t4=offMset00.offMotionHeader.offt9.Add(((offt2.ptr.t9index & 0xFFFF)+(offt2.ptr.t9cnt & 0xFF)) << 3)
            //#                             ; t4=&offt9[offt2.ptr.t9index + offt2.ptr.t9cnt]
            //#01285a4  LW t7, $004c(t5)    ; t7=offMset00.offMotionHeader.offt12
            //#01285a8  SRL t2, t2, 2       ; t2=(offt9[offt2.ptr.t9index].t11off >> 2)
            //#01285ac  ADDU s3, t5, t6     ; s3=offMset00.offMotionHeader.offt11.ptr
            //#01285b0  ADDU s5, t5, t3     ; s5=offMset00.offMotionHeader.offt10.ptr
            //#01285b4  ADDU s6, t5, t7     ; s6=offMset00.offMotionHeader.offt12.ptr
            //#01285b8  SLT a3, t2, a3          ; a3=(offt9[offt2.ptr.t9index].t11off >> 2) < _SP4
            //#01285bc  BNE a3, zero, $00128840 ; if (a3) { goto _128840; }
            //#01285c0  ADDIU s4, t4, $fff8     ; s4=&offt9[offt2.ptr.t9index + offt2.ptr.t9cnt -1]
            //#                                 ; s4=&T9[T2.t9index + T2.t9cnt -1]
            T9 s4 = offMset.Gett9(o2.t9index + o2.t9cnt - 1);
            int t7;
            int t5;
            float f2, f0;
            if (s1.t11off < SP4) {
                //#0128840  LHU t7, $fff8(t4)
                t7 = s4.t11off;
                //#0128844  SRL t4, t7, 2
                t4 = t7 >> 2;
                //#0128848  SLT a2, a2, t4
                //#012884c  BNEL a2, zero, $0012868c
                //#0128850  ANDI t4, t0, $ffff
                if (SP0 < t4) {
                    t4 = t0 & 0xffff;
                    goto _12868c;
                }
                t4 = t0 & 0xffff;
                //#0128854  LHU t7, $0002(a1)
                t7 = o2.ax;
                //#0128858  ADDIU t6, zero, $0001
                t6 = 1;
                //#012885c  SRL t7, t7, 6
                t7 >>= 6;
                //#0128860  ANDI t5, t7, $0003
                t5 = t7 & 3;
                //#0128864  BEQ t5, t6, $001288d8
                //#0128868  SLTI t7, t5, $0002
                if (t5 == t6) {
                    //#01288d8  SLL t7, t4, 2
                    t7 = t4 << 2;
                    //#01288dc  LHU t6, $0006(s4)
                    t6 = s4.t12index2;
                    //#01288e0  ADDU t7, t7, s3     ; t7=&T11[t4]
                    //#01288e4  LHU t5, $0002(s4)   
                    t5 = s4.t10index;
                    //#01288e8  LWC1 $f0, $0000(t7)
                    f0 = offMset.Gett11(t4);
                    //#01288ec  SLL t6, t6, 2
                    t6 <<= 2;
                    //#01288f0  ADDU t6, t6, s6     ; t6=&T12[s4.t12index2]
                    //#01288f4  SLL t5, t5, 2
                    t5 <<= 2;
                    //#01288f8  SUB.S $f0, $f12, $f0
                    f0 = f12 - f0;
                    //#01288fc  ADDU t5, t5, s5     ; t5=&T10[s4.t10index]
                    //#0128900  LWC1 $f2, $0000(t6)
                    f2 = offMset.Gett12(s4.t12index2);
                    //#0128904  LWC1 $f1, $0000(t5)
                    float f1 = offMset.Gett10(s4.t10index);
                    //#0128908  MUL.S $f0, $f0, $f2
                    f0 *= f12;
                    //#012890c  BEQ zero, zero, $001285f0
                    //#0128910  ADD.S $f0, $f1, $f0
                    f0 += f1;
                    return f0;
                }
                //#012886c  BEQ t7, zero, $00128884
                //#0128870  ADDIU t7, zero, $0002
                else if (t5 >= 2) {
                    t7 = 2;
                    //#0128884  BNEL t5, t7, $001285f0
                    //#0128888  MTC1 zero, $f0
                    if (t5 != t7) {
                        return 0;
                    }
                    //#012888c  SLL t7, t2, 2
                    t7 = t2 << 2;
                    //#0128890  SLL t6, t4, 2
                    t6 = t4 << 2;
                    //#0128894  ADDU t7, t7, s3     ; t7=&T11[t2]
                    //#0128898  ADDU t6, t6, s3     ; t6=&T11[t4]
                    //#012889c  LWC1 $f2, $0000(t6)
                    f2 = offMset.Gett11(t4);
                    //#01288a0  LWC1 $f0, $0000(t7)
                    f0 = offMset.Gett11(t2);
                    //#01288a4  C.LT.S $f2, $f12
                    //#01288a8  BC1F $00128670
                    //#01288ac  SUB.S $f0, $f2, $f0
                    if (f2 >= f12) {
                        f0 = f2 - f0;
                    }
                    else {
                        f12 -= f0;
                        //#01288b0  SUB.S $f12, $f12, $f0
                        //#01288b4  C.LT.S $f2, $f12
                        //#01288b8  NOP 
                        //#01288bc  NOP 
                        //#01288c0  NOP 
                        //#01288c4  NOP 
                        //#01288c8  BC1TL $001288b4
                        //#01288cc  SUB.S $f12, $f12, $f0
                        while (f2 < f12) { f12 -= f0; }
                        //#01288d0  BEQ zero, zero, $00128670
                        //#01288d4  MOV.S $f20, $f12
                        f20 = f12;
                    }
                    Trace.Fail("goto _0128670;"); //goto _0128670;
                }
                else {
                    //#0128874  BNEL t5, zero, $001285f0
                    //#0128878  MTC1 zero, $f0
                    if (t5 != 0) {
                        return 0;
                    }
                    //#012887c  BEQ zero, zero, $0012861c
                    //#0128880  LHU t7, $0002(s4)               ; t7=s4.t10index
                    {
                        //#012861c  SLL t7, t7, 2               ; t7=(&offt9.ptr[(? & 0xFFFF) << 3].t10index)<<2
                        //#0128620  ADDU t7, t7, s5             ; t7=&offt10[?.t10index]
                        //#0128624  BEQ zero, zero, $001285f0
                        //#0128628  LWC1 $f0, $0000(t7)         ; $f0=Getf(t7)
                        return offMset.Gett10(s4.t10index);
                    }
                }
            }
            else {
                //#01285c4  LHU t7, $0002(a1)       ; t7=offt2.ptr.ax
                t7 = o2.ax;
                //#01285c8  ADDIU t6, zero, $0001   ; t6=1
                t6 = 1;
                //#01285cc  SRL t7, t7, 4           ; t7=offt2.ptr.ax >> 4
                t7 >>= 4;
                //#01285d0  ANDI t5, t7, $0003      ; t5=(offt2.ptr.ax >> 4) & 3
                t5 = t7 & 3; // anotherInit
                //#01285d4  BEQ t5, t6, $00128804   ; if (((offt2.ptr.ax >> 4) & 3) == 1) { t7=((offt2.ptr.ax >> 4) & 3) < 2; goto _128804; }
                //#01285d8  SLTI t7, t5, $0002      ; t7=((offt2.ptr.ax >> 4) & 3) < 2;
                if (t5 == 1) {
                    //#0128804  SLL t7, t2, 2
                    t7 = t2 << 2;
                    //#0128808  LHU t6, $0004(s1)
                    t6 = s1.t12index1;
                    //#012880c  ADDU t7, t7, s3     ; t7=&T11[t2]
                    //#0128810  LHU t5, $0002(s1)
                    t5 = s1.t10index;
                    //#0128814  LWC1 $f0, $0000(t7)
                    f0 = offMset.Gett11(t2);
                    //#0128818  SLL t6, t6, 2
                    t6 <<= 2;
                    //#012881c  ADDU t6, t6, s6     ; t6=&T12[s1.t12index1]
                    //#0128820  SLL t5, t5, 2
                    t5 <<= 2;
                    //#0128824  SUB.S $f0, $f0, $f12
                    f0 -= f12;
                    //#0128828  ADDU t5, t5, s5     ; t5=&T10[s1.t10index]
                    //#012882c  LWC1 $f2, $0000(t6)
                    f2 = offMset.Gett12(s1.t12index1);
                    //#0128830  LWC1 $f1, $0000(t5)
                    float f1 = offMset.Gett10(s1.t10index);
                    //#0128834  MUL.S $f0, $f0, $f2
                    f0 *= f2;
                    //#0128838  BEQ zero, zero, $001285f0
                    //#012883c  SUB.S $f0, $f1, $f0
                    f0 = f1 - f0;
                    return f0;
                }
                //#01285dc  BEQL t7, zero, $0012862c    ; if (t7 == 0) { t7=2; goto _12862c; }
                //#01285e0  ADDIU t7, zero, $0002       ; .. BEQL 
                else if (t7 != 0) {
                    //#01285e4  BEQL t5, zero, $0012861c    ; if (t5 == 0) { t7=&offt9.ptr[(offt2.ptr.t9index & 0xFFFF) << 3].t10index; goto _12861c; }
                    //#01285e8  LHU t7, $0002(s1)           ; .. BEQL
                    if (t5 != 0) {
                        //#01285ec  MTC1 zero, $f0              ; $f0=0
                        return 0;

                        //#01285f0  LD s0, $0010(sp)
                        //#01285f4  LD s1, $0018(sp)
                        //#01285f8  LD s2, $0020(sp)
                        //#01285fc  LD s3, $0028(sp)
                        //#0128600  LD s4, $0030(sp)
                        //#0128604  LD s5, $0038(sp)
                        //#0128608  LD s6, $0040(sp)
                        //#012860c  LD ra, $0048(sp)
                        //#0128610  LWC1 $f20, $0050(sp)    ; restore
                        //#0128614  JR ra
                        //#0128618  ADDIU sp, sp, $0060
                    }
                    else {
                        //#012861c  SLL t7, t7, 2               ; t7=(&offt9.ptr[(offt2.ptr.t9index & 0xFFFF) << 3].t10index)<<2
                        //#0128620  ADDU t7, t7, s5             ; t7=&offt10[offt9[offt2.t9index].t10index]
                        //#0128624  BEQ zero, zero, $001285f0
                        //#0128628  LWC1 $f0, $0000(t7)         ; $f0=Getf(t7)
                        return offMset.Gett10(s1.t10index);
                    }
                }
                else {
                    t7 = 2;
                    //#012862c  BNEL t5, t7, $001285f0  ; if (((offt2.ptr.ax >> 4) & 3) != 2) { $f0=0; goto _1285f0; }
                    //#0128630  MTC1 zero, $f0          ; .. BNEL
                    if (t5 != 2) {
                        return 0;
                    }
                    //#0128634  LHU t7, $fff8(t4)       ; t7=Getw(&offt9[offt2.ptr.t9index + offt2.ptr.t9cnt -1])
                    t7 = offMset.Gett9(o2.t9index + o2.t9cnt - 1).t11off;
                    //#0128638  SLL t6, t2, 2           ; t6=(offt9[offt2.ptr.t9index].t11off >> 2)<<2
                    //#012863c  ADDU t6, t6, s3         ; t6=offMset00.offMotionHeader.offt11.Add((offt9[offt2.ptr.t9index].t11off >> 2)<<2)
                    //#                                 ; t6=&offt11[offt9[offt2.t9index].t11off]
                    //#0128640  ANDI t7, t7, $fffc      ; t7=t7 & 0xFFFC
                    t7 &= 0xfffc;

                    //#0128644  LWC1 $f2, $0000(t6)     ; $f2=Getf(&offt11[offt9[offt2.t9index].t11off])
                    f2 = offMset.Gett11(s1.t11off);
                    //#0128648  ADDU t7, t7, s3         ; t7=offMset00.offMotionHeader.offt11.ptr.Add(Getw(&offt9[offt2.ptr.t9index + offt2.ptr.t9cnt -1]))
                    //#                                 ; t7=&offt11[offt9[offt2.t9index + offt2.t9cnt -1].w0]
                    //#012864c  LWC1 $f0, $0000(t7)     ; $f0=Getf(&offt11[offt2.t9index + offt2.t9cnt -1])
                    f0 = offMset.Gett11(offMset.Gett9(o2.t9index + o2.t9cnt - 1).t11off);
                    //#0128650  C.LT.S $f12, $f2        ; if ($f12 < $f2)
                    //#0128654  BC1F $00128670          ; else { $f0=$f0-$f2; goto _128670; }
                    //#0128658  SUB.S $f0, $f0, $f2     ; $f0=$f0-$f2
                    f0 -= f2;
                    if (f12 < f2) {
                        //#012865c  ADD.S $f12, $f12, $f0   ; $f12=$f12+$f0
                        f12 += f0;
                        //#0128660  C.LT.S $f12, $f2        ; if ($f12 < $f2)
                        //#0128664  BC1TL $00128660         ; then { $f12=$f12+$f0; goto _128660; }
                        //#0128668  ADD.S $f12, $f12, $f0   ; .. BC1TL
                        while (f12 < f2) f12 += f0;
                        //#012866c  MOV.S $f20, $f12
                        f20 = f12;
                    }
                }
            }
            //#0128670  MOV.S $f12, $f20
            f12 = f20;
            //#0128674  DADDU a1, sp, zero      ; a1=&SP0
            //#0128678  JAL $001283d8           ; fn1283d8();
            //#012867c  ADDIU a2, sp, $0004     ; a2=&SP4
            fn1283d8(out SP0, out SP4);
            //#0128680  LBU t1, $0003(s0)
            int t1 = s0.t9cnt;
            //#0128684  LHU t0, $0004(s0)
            t0 = s0.t9index;
            //#0128688  ANDI t4, t0, $ffff
            t4 = t0 & 0xffff;
        _12868c: ;
            //#012868c  ANDI t7, t1, $00ff
            t7 = s0.t9cnt & 0xff;
            //#0128690  ADDU t7, t4, t7
            t7 += t4;
            //#0128694  ADDIU a3, t7, $ffff
            int a3 = t7 - 1;
            //#0128698  SUBU t0, a3, t4
            t0 = a3 - t4;
            //#012869c  SLTI t7, t0, $0002
            //#01286a0  BNEL t7, zero, $00128708
            //#01286a4  LHU t4, $0000(s1)
            int t3;
            if ((t0 < 2) == false) {
                //#01286a8  ADDU t7, t4, a3
                t7 = t4 + a3;
                for (; ; t7 = t4 + a3) {
                    //#01286ac  SRL t6, t7, 31
                    //#01286b0  ADDU t7, t7, t6
                    if (t7 < 0) t7++;
                    //#01286b4  SRA t3, t7, 1
                    t3 = t7 >> 1;
                    //#01286b8  SLL t6, t3, 3
                    //#01286bc  ADDU t1, s2, t6     ; t1=&T9.ptr[t3]
                    //#01286c0  LHU t5, $0000(t1)
                    t5 = offMset.Gett9(t3).t11off;
                    //#01286c4  SRL t5, t5, 2
                    t5 >>= 2;
                    //#01286c8  SLL t7, t5, 2
                    //#01286cc  ADDU t7, t7, s3     ; t7=&T11.ptr[t5]
                    //#01286d0  LWC1 $f0, $0000(t7)
                    f0 = offMset.Gett11(t5);
                    //#01286d4  C.EQ.S $f0, $f20
                    //#01286d8  BC1T $001287fc
                    //#01286dc  LW t6, $0000(sp)
                    if (f0 == f20) {
                        //#01287fc  BEQ zero, zero, $0012861c
                        //#0128800  LHU t7, $0002(t1)           ; t7 = offMset.Gett9(t3).t10index;
                        {
                            //#012861c  SLL t7, t7, 2               ; t7=(&offt9.ptr[(t3 & 0xFFFF) << 3].t10index)<<2
                            //#0128620  ADDU t7, t7, s5             ; t7=&offt10[offt9[t3].t10index]
                            //#0128624  BEQ zero, zero, $001285f0
                            //#0128628  LWC1 $f0, $0000(t7)         ; $f0=Getf(t7)
                            return offMset.Gett10(offMset.Gett9(t3).t10index);
                        }
                    }
                    else {
                        t6 = SP0;
                        //#01286e0  SLT t7, t5, t6
                        //#01286e4  BEQ t7, zero, $001287c0
                        //#01286e8  LW t2, $0004(sp)
                        if ((t5 < t6) == false) {
                            t2 = SP4;
                            //#01287c0  SLT t7, t2, t5
                            //#01287c4  BEQ t7, zero, $001287d8
                            if ((t2 < t5) == false) {
                                //#01287d8  BEQL t5, t6, $001287f4
                                //#01287dc  DADDU s1, t1, zero
                                s1 = offMset.Gett9(t3);
                                if (t5 == t6) {
                                    //#01287f4  BEQ zero, zero, $00128704
                                    //#01287f8  ADDIU s4, t1, $0008
                                    s4 = offMset.Gett9(t3 + 1);
                                }
                                else {
                                    //#01287e0  BNE t5, t2, $001286fc
                                    //#01287e4  SLTI t7, t0, $0002
                                    if (t5 != t2) {
                                        goto _1286fc;
                                    }
                                    //#01287e8  DADDU s4, t1, zero
                                    s4 = offMset.Gett9(t3);
                                    //#01287ec  BEQ zero, zero, $00128704
                                    //#01287f0  ADDIU s1, t1, $fff8
                                    s1 = offMset.Gett9(t3);
                                }
                                Trace.Fail("goto _00128704;");
                                break;
                            }
                            else {
                                //#01287c8  NOP 
                                //#01287cc  DADDU a3, t3, zero
                                a3 = t3;
                                //#01287d0  BEQ zero, zero, $001286f4
                                //#01287d4  DADDU s4, t1, zero
                                s4 = offMset.Gett9(t3);
                            }
                        }
                        else {
                            t2 = SP4;
                            //#01286ec  DADDU t4, t3, zero
                            t4 = t3;
                            //#01286f0  DADDU s1, t1, zero  ; s1=&T9.ptr[t3]
                            s1 = offMset.Gett9(t3);
                        }
                        //#01286f4  SUBU t0, a3, t4
                        t0 = a3 - t4;
                    //#01286f8  SLTI t7, t0, $0002
                    _1286fc: ;
                        //#01286fc  BEQ t7, zero, $001286ac
                        //#0128700  ADDU t7, t4, a3
                        if ((t0 < 2) == false) continue;
                        break;
                    }
                }
                //#0128704  LHU t4, $0000(s1)
                t4 = s1.t11off;
            }
            else {
                t4 = s1.t11off;
            }
            //#0128708  ANDI t7, t4, $0003
            t7 = t4 & 3;
            //#012870c  ANDI t1, t7, $ffff
            t1 = t7;
            //#0128710  BEQL t1, zero, $0012861c
            //#0128714  LHU t7, $0002(s1)
            if (t1 == 0) {
                //#012861c  SLL t7, t7, 2               ; t7=(&offt9.ptr[(offt2.ptr.t9index & 0xFFFF) << 3].t10index)<<2
                //#0128620  ADDU t7, t7, s5             ; t7=&offt10[offt9[offt2.t9index].t10index]
                //#0128624  BEQ zero, zero, $001285f0
                //#0128628  LWC1 $f0, $0000(t7)         ; $f0=Getf(t7)
                return offMset.Gett10(s1.t10index);
            }

            //#0128718  LHU t7, $0002(s4)
            t7 = s4.t10index;
            //#012871c  ANDI t4, t4, $ffff
            //#0128720  LHU t3, $0006(s1)
            t3 = s1.t12index2; // anotherInit
            //#0128724  ANDI t4, t4, $fffc
            t4 &= 0xfffc;
            //#0128728  LHU t2, $0004(s4)
            t2 = s4.t12index1;
            //#012872c  SLL t7, t7, 2
            t7 <<= 2;
            //#0128730  LHU t6, $0002(s1)
            t6 = s1.t10index;
            //#0128734  ADDU t7, t7, s5         ; t7=&T10[s4.t10index]
            //#0128738  LHU t5, $0000(s4)
            t5 = s4.t11off;
            //#012873c  SLL t3, t3, 2
            t3 <<= 2;
            //#0128740  SLL t2, t2, 2
            t2 <<= 2;
            //#0128744  LWC1 $f17, $0000(t7)
            float f17 = offMset.Gett10(s4.t10index);
            //#0128748  SLL t6, t6, 2
            t6 <<= 2;
            //#012874c  ANDI t5, t5, $fffc
            t5 &= 0xfffc;
            //#0128750  ADDU t2, t2, s6         ; t2=&T12[s4.t12index1]
            //#0128754  ADDU t5, t5, s3         ; t5=&T11[s4.t11off & 0xfffc]
            //#0128758  ADDU t4, t4, s3         ; t4=&T11[t4]
            //#012875c  LWC1 $f16, $0000(t5)
            float f16 = offMset.Gett11((s4.t11off & 0xfffc) >> 2);
            //#0128760  ADDU t6, t6, s5         ; t6=&T10[s1.t10index]
            //#0128764  LWC1 $f13, $0000(t4)
            float f13 = offMset.Gett11(t4);
            //#0128768  ADDU t3, t3, s6         ; t3=&T12[s1.t12index2]
            //#012876c  LWC1 $f14, $0000(t6)
            float f14 = offMset.Gett10(s1.t10index);
            //#0128770  LWC1 $f15, $0000(t3)
            float f15 = offMset.Gett12(s1.t12index2);
            //#0128774  ADDIU t7, zero, $0001
            t7 = 1;
            //#0128778  BEQ t1, t7, $0012879c
            //#012877c  LWC1 $f18, $0000(t2)
            if (t1 == 1) {
                //#012879c  SUB.S $f0, $f17, $f14
                f0 = f17 - f14;
                //#01287a0  SUB.S $f1, $f20, $f13
                f14 = f20 - f13;
                //#01287a4  SUB.S $f2, $f16, $f13
                f2 = f16 - f13;
                //#01287a8  MUL.S $f0, $f0, $f1
                f0 *= f13;
                //#01287ac  NOP 
                //#01287b0  NOP 
                //#01287b4  DIV.S $f0, $f0, $f2
                f0 /= f2;
                //#01287b8  BEQ zero, zero, $001285f0
                //#01287bc  ADD.S $f0, $f14, $f0
                f0 += f14;
                return f0;
            }
            float f18 = offMset.Gett12(s4.t12index1);
            //#0128780  ADDIU t7, zero, $0002
            t7 = 2;
            //#0128784  BNEL t1, t7, $001285f0
            //#0128788  MTC1 zero, $f0
            if (t1 != 2) {
                return 0;
            }
            //#012878c  JAL $001284b0
            //#0128790  MOV.S $f12, $f20
            {
                f12 = f20;
                //#01284b0  SUB.S $f16, $f16, $f13
                f16 -= f13;
                //#01284b4  LUI t7, $0038
                //#01284b8  ADDIU t7, t7, $823c
                //#01284bc  SUB.S $f12, $f12, $f13
                f12 -= f13;
                //#01284c0  LWC1 $f7, $0000(t7)
                float f7 = _37823c;
                //#01284c4  NOP 
                //#01284c8  NOP 
                //#01284cc  DIV.S $f16, $f7, $f16
                f16 = f7 / f16;
                //#01284d0  LUI t7, $0038
                //#01284d4  MUL.S $f5, $f12, $f12
                float f5 = f12 * f12;
                //#01284d8  ADDIU t7, t7, $8240
                //#01284dc  LWC1 $f3, $0000(t7)
                float f3 = _378240;
                //#01284e0  MUL.S $f2, $f5, $f12
                f2 = f5 * f12;
                //#01284e4  MUL.S $f3, $f5, $f3
                f3 *= f5;
                //#01284e8  MUL.S $f1, $f16, $f16
                float f1 = f16 * f16;
                //#01284ec  MUL.S $f5, $f5, $f16
                f5 *= f16;
                //#01284f0  MUL.S $f2, $f2, $f1
                f2 *= f1;
                //#01284f4  MUL.S $f3, $f3, $f1
                f3 *= f1;
                //#01284f8  ADD.S $f6, $f5, $f5
                float f6 = f5 + f5;
                //#01284fc  ADD.S $f4, $f2, $f2
                float f4 = f2 + f2;
                //#0128500  SUB.S $f5, $f2, $f5
                f5 = f2 - f5;
                //#0128504  MUL.S $f4, $f4, $f16
                f4 *= f16;
                //#0128508  SUB.S $f2, $f2, $f6
                f2 -= f6;
                //#012850c  SUB.S $f1, $f4, $f3
                f1 = f4 - f3;
                //#0128510  ADD.S $f2, $f2, $f12
                f2 += f12;
                //#0128514  ADD.S $f1, $f1, $f7
                f1 += f7;
                //#0128518  SUB.S $f3, $f3, $f4
                f3 -= f4;
                //#012851c  MULA.S $f14, $f1
                //#0128520  MADDA.S $f17, $f3
                //#0128524  MADDA.S $f15, $f2
                //#0128528  JR ra
                //#012852c  MADD.S $f0, $f18, $f5
                return f0 = f14 * f1 + f17 * f3 + f15 * f2 + f18 * f5;
            }
            //#0128794  BEQ zero, zero, $001285f4
            //#0128798  LD s0, $0010(sp)
        }

        /*
            a0 009f8510 // mset +0x00
            a1 01abb590 // info tbl
            s0 009f85a0 // mset +0x90
            s4 01abb0b0 // temp?
            sp 0038eca0 // more than 80 bytes
            pc 00128918

            fpr12 // frame#
         */
        public void S_CALCF() {
            //#0128918  ADDIU sp, sp, $ffb0
            //#012891c  SWC1 $f20, $0048(sp)
            //#0128920  ADDIU a2, sp, $0004 ; a2=&SP4
            //#0128924  SD s5, $0038(sp)
            //#0128928  MOV.S $f20, $f12    ; $f20=$f12
            float f20 = f12;
            //#012892c  SD s4, $0030(sp)
            //#0128930  SD s0, $0010(sp)
            //#0128934  DADDU s5, a1, zero  ; s5=InfoTbl
            //#0128938  SD s1, $0018(sp)
            //#012893c  DADDU s4, a0, zero  ; s4=offMset00
            //#0128940  SD s2, $0020(sp)
            //#0128944  DADDU a1, sp, zero      ; a1=&SP0
            //#0128948  SD s3, $0028(sp)
            //#012894c  SD ra, $0040(sp)
            //#0128950  JAL $001283d8           ; fn1283d8(out SP0, out SP4)
            //#0128954  DADDU s2, zero, zero    ; s2=0
            int s2 = 0;
            int SP0, SP4;
            fn1283d8(out SP0, out SP4);

            //#0128958  LW s1, $0004(s4)    ; s1=offMset00.offMotionHeader
            //#012895c  LW t7, $0030(s1)    ; t7=offMset00.offMotionHeader.offt2
            //#0128960  LW s3, $0034(s1)    ; s3=offMset00.offMotionHeader.cntt2
            int s3 = offMset.cntt2;
            //#0128964  BLEZ s3, $001289e4  ; if (s3 <= 0) { s0=offt2.ptr; goto _1289e4; }
            //#0128968  ADDU s0, s1, t7     ; s0=offt2.ptr
            if (offMset.cntt2 > 0) {
                int i = 0;
                for (; ; i++) {
                    //#012896c  MOV.S $f12, $f20    ; $f12=$f20
                    f12 = f20;
                    //#0128970  LW a2, $0000(sp)    ; a2=SP0
                    //#0128974  LW a3, $0004(sp)    ; a3=SP4
                    //#0128978  DADDU a0, s4, zero  ; a0=offMset00
                    //#012897c  JAL $00128530       ; fn128530()
                    //#0128980  DADDU a1, s0, zero  ; a1=offt2.ptr
                    float f0 = fn128530(offMset.Gett2(i), SP0, SP4);
                    //#0128984  LBU t4, $0002(s0)
                    int t4 = offMset.Gett2(i).ax;
                    //#0128988  ANDI t7, t4, $000f
                    int t7 = t4 & 0x000f;
                    //#012898c  ANDI t7, t7, $00ff
                    t7 &= 0xff;
                    //#0128990  SLTIU t6, t7, $0009
                    //#0128994  BEQ t6, zero, $001289d4
                    //#0128998  LUI t6, $0038
                    if ((t7 < 9) != false) {
                        //#012899c  SLL t7, t7, 2
                        //#01289a0  ADDIU t6, t6, $8244 ; t6=0x378244
                        //#01289a4  ADDU t7, t7, t6
                        //#01289a8  LW t5, $0000(t7)
                        //#01289ac  JR t5
                        //#01289b0  NOP 
                        switch (t7) {
                            case 0: // 1289b4
                            case 1: // 1289b4
                            case 2: // 1289b4
                            {
                                    //#01289b4  LHU t7, $0000(s0)   ; t7=T2[i].w0
                                    t7 = offMset.Gett2(i).joint;
                                    //#01289b8  ANDI t5, t4, $000f
                                    int t5 = t4 & 0xf;
                                    //#01289bc  LW t6, $001c(s5)    ; t6=s5.Sxyz
                                    //#01289c0  SLL t5, t5, 2
                                    //#01289c4  SLL t7, t7, 4
                                    //#01289c8  ADDU t6, t6, t7
                                    //#01289cc  ADDU t6, t6, t5
                                    //#01289d0  SWC1 $f0, $0000(t6)
                                    s5.Sxyz[t7][t5] = f0;

                                    break;
                                }
                            case 3: // 128ae4
                            case 4: // 128ae4
                            case 5: // 128ae4
                            {
                                    //#0128ae4  ANDI t6, t4, $000f
                                    int t6 = t4 & 0xf;
                                    //#0128ae8  LHU t7, $0000(s0)   ; t7=T2[i].w0
                                    t7 = offMset.Gett2(i).joint;
                                    //#0128aec  LW t5, $0020(s5)    ; t5=s5.Rxyz
                                    //#0128af0  ADDIU t6, t6, $fffd
                                    t6 -= 3;
                                    //#0128af4  SLL t7, t7, 4
                                    //#0128af8  SLL t6, t6, 2
                                    //#0128afc  ADDU t5, t5, t7
                                    //#0128b00  ADDU t5, t5, t6
                                    //#0128b04  BEQ zero, zero, $001289d4
                                    //#0128b08  SWC1 $f0, $0000(t5)
                                    s5.Rxyz[t7][t6] = f0;

                                    break;
                                }
                            case 6: // 128b0c
                            case 7: // 128b0c
                            case 8: // 128b0c
                            {
                                    //#0128b0c  ANDI t6, t4, $000f
                                    int t6 = t4 & 0xf;
                                    //#0128b10  LHU t7, $0000(s0)   ; t7=T2[i].w0
                                    t7 = offMset.Gett2(i).joint;
                                    //#0128b14  LW t5, $0024(s5)    ; t5=s5.Txyz
                                    //#0128b18  BEQ zero, zero, $00128af4
                                    //#0128b1c  ADDIU t6, t6, $fffa
                                    t6 -= 6;
                                    {
                                        //#0128af4  SLL t7, t7, 4
                                        //#0128af8  SLL t6, t6, 2
                                        //#0128afc  ADDU t5, t5, t7
                                        //#0128b00  ADDU t5, t5, t6
                                        //#0128b04  BEQ zero, zero, $001289d4
                                        //#0128b08  SWC1 $f0, $0000(t5)
                                        s5.Txyz[t7][t6] = f0;
                                    }
                                    break;
                                }
                        }
                    }
                    //#01289d4  ADDIU s2, s2, $0001
                    s2++;
                    //#01289d8  SLT t7, s2, s3
                    //#01289dc  BNE t7, zero, $0012896c
                    //#01289e0  ADDIU s0, s0, $0006
                    if ((s2 < s3) != false) continue;
                    break;
                }
            }

            {
                //#01289e4  LW t6, $0018(s1)        ; t6=offMset.offt5
                //#01289e8  DADDU s2, zero, zero
                s2 = 0;
                //#01289ec  LW t7, $0038(s1)        ; t7=offMset.offt2x
                //#01289f0  LW s3, $003c(s1)
                s3 = offMset.cntt2x;
                //#01289f4  ADDU s5, s1, t6         ; s1=offMset.offt5.ptr
                //#01289f8  BLEZ s3, $00128a74
                if (s3 <= 0) {
                    return;
                }
                //#01289fc  ADDU s0, s1, t7         ; s0=offMset.offt2x.ptr
                int i = 0; // s0_i
                for (; ; i++) {
                    //#0128a00  MOV.S $f12, $f20
                    f12 = f20;
                    //#0128a04  LW a2, $0000(sp)
                    int a2 = SP0;
                    //#0128a08  LW a3, $0004(sp)
                    int a3 = SP4;
                    //#0128a0c  DADDU a0, s4, zero      ; a0=offMset
                    //#0128a10  JAL $00128530
                    //#0128a14  DADDU a1, s0, zero      ; a1=offMset.offt2x.ptr
                    float f0 = fn128530(offMset.Gett2x(i), SP0, SP4);
                    //#0128a18  LBU t4, $0002(s0)
                    int t4 = offMset.Gett2x(i).ax;
                    //#0128a1c  ANDI t7, t4, $000f
                    int t7 = t4 & 0xf;
                    //#0128a20  ANDI t7, t7, $00ff
                    t7 &= 0xff;
                    //#0128a24  SLTIU t6, t7, $0009
                    //#0128a28  BEQ t6, zero, $00128a64
                    //#0128a2c  LUI t6, $0038
                    if ((t7 < 9) != false) {
                        //#0128a30  SLL t7, t7, 2
                        //#0128a34  ADDIU t6, t6, $8268     ; t6=_378268
                        //#0128a38  ADDU t7, t7, t6
                        //#0128a3c  LW t5, $0000(t7)
                        //#0128a40  JR t5
                        //#0128a44  NOP 
                        switch (t7) {
                            case 0: // 128a48
                            case 1: // 128a48
                            case 2: // 128a48
                            {
                                    //#0128a48  LHU t7, $0000(s0)
                                    t7 = offMset.Gett2x(i).joint;
                                    //#0128a4c  ANDI t6, t4, $000f
                                    int t6 = t4 & 0xf;
                                    //#0128a50  SLL t6, t6, 2
                                    //#0128a54  SLL t7, t7, 6
                                    //#0128a58  ADDU t7, s5, t7     ; t7=&T5[t7][t6]
                                    //#0128a5c  ADDU t7, t7, t6     
                                    //#0128a60  SWC1 $f0, $0010(t7)
                                    AxBone _t7 = offMset.alt5[t7];
                                    switch (t6) {
                                        case 0: _t7.sx = f0; break;
                                        case 1: _t7.sy = f0; break;
                                        case 2: _t7.sz = f0; break;
                                        case 3: _t7.sw = f0; break;
                                    }
                                    break;
                                }
                            case 3: // 128a9c
                            case 4: // 128a9c
                            case 5: // 128a9c
                            {
                                    //#0128a9c  LHU t7, $0000(s0)
                                    t7 = offMset.Gett2x(i).joint;
                                    //#0128aa0  ANDI t6, t4, $000f
                                    int t6 = t4 & 0xf;
                                    //#0128aa4  ADDIU t6, t6, $fffd
                                    t6 -= 3;
                                    //#0128aa8  SLL t7, t7, 6
                                    //#0128aac  SLL t6, t6, 2
                                    //#0128ab0  ADDU t7, s5, t7
                                    //#0128ab4  ADDU t7, t7, t6
                                    //#0128ab8  BEQ zero, zero, $00128a64
                                    //#0128abc  SWC1 $f0, $0020(t7)
                                    AxBone _t7 = offMset.alt5[t7];
                                    switch (t6) {
                                        case 0: _t7.rx = f0; break;
                                        case 1: _t7.ry = f0; break;
                                        case 2: _t7.rz = f0; break;
                                        case 3: _t7.rw = f0; break;
                                    }
                                    break;
                                }
                            case 6: // 128ac0
                            case 7: // 128ac0
                            case 8: // 128ac0
                            {
                                    //#0128ac0  LHU t7, $0000(s0)
                                    t7 = offMset.Gett2x(i).joint;
                                    //#0128ac4  ANDI t6, t4, $000f
                                    int t6 = t4 & 0xf;
                                    //#0128ac8  ADDIU t6, t6, $fffa
                                    t6 -= 6;
                                    //#0128acc  SLL t7, t7, 6
                                    //#0128ad0  SLL t6, t6, 2
                                    //#0128ad4  ADDU t7, s5, t7
                                    //#0128ad8  ADDU t7, t7, t6
                                    //#0128adc  BEQ zero, zero, $00128a64
                                    //#0128ae0  SWC1 $f0, $0030(t7)
                                    AxBone _t7 = offMset.alt5[t7];
                                    switch (t6) {
                                        case 0: _t7.tx = f0; break;
                                        case 1: _t7.ty = f0; break;
                                        case 2: _t7.tz = f0; break;
                                        case 3: _t7.tw = f0; break;
                                    }
                                    break;
                                }
                        }
                    }
                    //#0128a64  ADDIU s2, s2, $0001
                    s2++;
                    //#0128a68  SLT t7, s2, s3
                    //#0128a6c  BNE t7, zero, $00128a00
                    //#0128a70  ADDIU s0, s0, $0006
                    if ((s2 < s3) != false) continue;
                    break;
                }
                //#0128a74  LD s0, $0010(sp)
                //#0128a78  LD s1, $0018(sp)
                //#0128a7c  LD s2, $0020(sp)
                //#0128a80  LD s3, $0028(sp)
                //#0128a84  LD s4, $0030(sp)
                //#0128a88  LD s5, $0038(sp)
                //#0128a8c  LD ra, $0040(sp)
                //#0128a90  LWC1 $f20, $0048(sp)
                //#0128a94  JR ra
                //#0128a98  ADDIU sp, sp, $0050
            }
        }
    }
}
