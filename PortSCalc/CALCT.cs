using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;

namespace PortSCalc {
    class CALCT {
        InfoTbl s0;
        Msetacc offMset;

        public CALCT(InfoTbl it, Msetacc offMset) {
            this.s0 = it;
            this.offMset = offMset;
        }

        /*
            a0 009f8510 // mset +0x00
            a1 01abb590 // info tbl
            s0 009f85a0 // mset +0x90
            s4 01abb0b0 // temp?
            sp 0038eca0 // more than 32 bytes
            pc 00128260
        */
        public void S_CALCT() {
            //#0128260  ADDIU sp, sp, $ffe0
            //#0128264  SD s0, $0000(sp)
            //#0128268  SD s1, $0008(sp)
            //#012826c  SD s2, $0010(sp)
            //#0128270  DADDU s0, a1, zero  ; s0=InfoTbl
            //#0128274  SD ra, $0018(sp)
            //#0128278  LW t7, $0014(a1)    ; t7=offMdlx04
            //#012827c  LW s2, $0004(a0)    ; s2=offMset +0x00
            //#0128280  LW t6, $0000(t7)    ; t6=offMdlx04.MdlxVtbl
            //#0128284  LHU s1, $0010(s2)   ; s1=offMset.cjMdlx
            //#0128288  LW v0, $0020(t6)    ; v0=offMdlx04.MdlxVtbl.fn0x20=fn1206a0
            //#012828c  JALR ra, v0         ; _1206a0();
            //#0128290  DADDU a0, t7, zero  ; a0=offMdlx04
            //                              ; v0=offAxBone

            //#0128294  BEQ s1, zero, $00128304 ; if (cjMdlx == 0) goto _128304;
            //#0128298  DADDU t2, zero, zero    ; t2=0
            if (offMset.cjMdlx != 0) {
                for (int t2 = 0; t2 < offMset.cjMdlx; t2++) {
                    AxBone axb = s0.offMdlx04.GetAxBone(t2);

                    //#012829c  LW t7, $001c(s0)        ; t7=s0.Sxyz
                    Vector4[] Sxyz = s0.Sxyz;
                    //#01282a0  SLL t5, t2, 4           ; t5=t2<<4
                    //#01282a4  ADDIU t6, v0, $0010     ; t6=offAxBone +0x10 =AxBoneSvec
                    //#01282a8  ADDU t7, t7, t5         ; t7=t7+t5
                    //#01282ac  BEQL t7, t6, $001282c0  ; if (t7 == t6) { t7=s0.Rxyz; goto _1282c0; }
                    //#01282b0  LW t7, $0020(s0)        ; .. BEQL

                    //#01282b4  LQ t0, $0000(t6)        ; t0=Get(AxBoneSvec[i])
                    //#01282b8  SQ t0, $0000(t7)        ; Set(s0.Sxyz[t2], t0)
                    Sxyz[t2] = axb.Sxyzw;
                    //#01282bc  LW t7, $0020(s0)        ; t7=s0.Rxyz
                    Vector4[] Rxyz = s0.Rxyz;
                    //#01282c0  ADDIU t6, v0, $0020     ; t6=offAxBone +0x20 =AxBoneRvec
                    //#01282c4  ADDU t7, t7, t5         ; t7=t7+t5
                    //#01282c8  BEQL t7, t6, $001282dc  ; if (t7 == t6) { t7 = s0.Txyz; goto _1282dc; }
                    //#01282cc  LW t7, $0024(s0)        ; .. BEQL

                    //#01282d0  LQ t0, $0000(t6)        ; t0=Get(AxBoneRvec[i])
                    //#01282d4  SQ t0, $0000(t7)        ; Set(s0.Rxyz[t2], t0)
                    Rxyz[t2] = axb.Rxyzw;
                    //#01282d8  LW t7, $0024(s0)        ; t7=s0.Txyz
                    Vector4[] Txyz = s0.Txyz;
                    //#01282dc  ADDIU t6, v0, $0030     ; t6=offAxBone +0x30 =AxBoneTvec
                    //#01282e0  ADDU t7, t7, t5         ; t7=t7+t5
                    //#01282e4  BEQL t7, t6, $001282f8  ; if (t7 == t6) { t2=t2+1; goto _1282f8; }
                    //#01282e8  ADDIU t2, t2, $0001     ; .. BEQL

                    //#01282ec  LQ t0, $0000(t6)        ; t0=Get(AxBoneTvec[i])
                    //#01282f0  SQ t0, $0000(t7)        ; Set(s0.Txyz[t2], t0)
                    Txyz[t2] = axb.Txyzw;
                    //#01282f4  ADDIU t2, t2, $0001     ; t2=t2+1
                    //#01282f8  SLT t7, t2, s1          ; t7=(t2<cjMdlx)
                    //#01282fc  BNE t7, zero, $0012829c ; if (t7) { v0=v0+64; goto _12829c; }
                    //#0128300  ADDIU v0, v0, $0040     ; v0=v0+64
                }
            }

            //#0128304  LW s1, $0028(s2)        ; s1=offMset.cntt1
            int s1 = offMset.cntt1;
            //#0128308  BEQL s1, zero, $00128384    ; if (s1 == 0) { s0=sp.ORGs0; goto _128384; }
            //#012830c  LD s0, $0000(sp)            ; .. BEQL
            if (s1 != 0) {
                //#0128310  LW t7, $0024(s2)        ; t7=offMset.offt1
                //#0128314  DADDU t2, zero, zero    ; t2=0
                //#0128318  BLEZ s1, $00128380      ; if (offMset.cntt1<=0) { t3=offMset.offt1; goto _128380; }
                //#012831c  ADDU t3, s2, t7         ; t3=offMset.offt1;
                if (!(s1 <= 0)) {
                    for (int t2 = 0; t2 < offMset.cntt1; t2++) {
                        //#0128320  LHU t4, $0002(t3)       ; t4=(offMset.offt1[i].ax)
                        //#0128324  ANDI t6, t4, $ffff      ; t6=(offMset.offt1[i].ax & 0xFFFF)
                        //#0128328  SLTIU t7, t6, $0009     ; t7=uint(t6)<9
                        //#012832c  BEQ t7, zero, $00128370 ; if (t7 == 0) { goto _128370; }
                        //#0128330  SLL t7, t6, 2           ; t7=t6<<2
                        //#0128334  LUI t6, $0038           ; ..
                        //#0128338  ADDIU t6, t6, $8218     ; t6=Struc378218
                        //#012833c  ADDU t7, t7, t6         ; t7=Struc378218.fn[t6]
                        //#0128340  LW t5, $0000(t7)        ; t5=Getfn(t7)
                        //#0128344  JR t5                   ; switch (t7) { ... }
                        switch (offMset.Gett1(t2).ax) {
                            //#0128348  NOP 
                            // 12834c case 0:
                            // 12834c case 1:
                            // 12834c case 2:
                            case 0:
                            case 1:
                            case 2: {
                                    //#012834c  LHU t6, $0000(t3)       ; t6=offMset.offt1[i].joint
                                    int t6 = offMset.Gett1(t2).joint;
                                    //#0128350  ANDI t5, t4, $ffff      ; t5=(offMset.offt1[i].ax & 0xFFFF)
                                    int t5 = offMset.Gett1(t2).ax;
                                    //#0128354  LW t7, $001c(s0)        ; t7=s0.Sxyz
                                    Vector4[] Sxyz = s0.Sxyz;
                                    //#0128358  SLL t5, t5, 2           ; t5=t5<<2
                                    //#012835c  SLL t6, t6, 4           ; t6=t6<<4
                                    //#0128360  LWC1 $f0, $0004(t3)     ; $f0=offMset.offt1[i].value
                                    //#0128364  ADDU t7, t7, t6         ; t7=&s0.Sxyz[offMset.offt1[i].joint]
                                    //#0128368  ADDU t7, t7, t5         ; t7=&s0.Sxyz[offMset.offt1[i].joint][(offMset.offt1[i].ax & 0xFFFF)]
                                    //#012836c  SWC1 $f0, $0000(t7)     ; Set(t7, $f0)
                                    Sxyz[t6][t5] = offMset.Gett1(t2).value;

                                    break;
                                }

                            // 128398 case 3:
                            // 128398 case 4:
                            // 128398 case 5:
                            case 3:
                            case 4:
                            case 5: {
                                    //#0128398  ANDI t7, t4, $ffff      ; t7=(offMset.offt1[i].ax & 0xFFFF)
                                    int t7 = offMset.Gett1(t2).ax;
                                    //#012839c  LHU t5, $0000(t3)       ; t5=offMset.offt1[i].joint
                                    int t5 = offMset.Gett1(t2).joint;
                                    //#01283a0  LW t6, $0020(s0)        ; t6=s0.Rxyz
                                    Vector4[] Rxyz = s0.Rxyz;
                                    //#01283a4  ADDIU t7, t7, $fffd     ; t7=t7 -3
                                    t7 -= 3;

                                    //#01283a8  SLL t5, t5, 4           ; t5=t5<<4
                                    //#01283ac  LWC1 $f0, $0004(t3)     ; $f0=offMset.offt1[i].value
                                    //#01283b0  SLL t7, t7, 2           ; t7=t7<<2
                                    //#01283b4  ADDU t6, t6, t5         ; t6=&s0.Rxyz[offMset.offt1[i].joint]
                                    //#01283b8  ADDU t6, t6, t7         ; t6=&s0.Rxyz[offMset.offt1[i].joint][(offMset.offt1[i].ax & 0xFFFF)]
                                    //#01283bc  BEQ zero, zero, $00128370   ; goto _128370;
                                    //#01283c0  SWC1 $f0, $0000(t6)     ; Set(t6, $f0)
                                    Rxyz[t5][t7] = offMset.Gett1(t2).value;

                                    break;
                                }

                            // 1283c4 case 6:
                            // 1283c4 case 7:
                            // 1289c4 case 8:
                            case 6:
                            case 7:
                            case 8: {
                                    //#01283c4  ANDI t7, t4, $ffff      ; t7=(offMset.offt1[i].ax & 0xFFFF)
                                    int t7 = offMset.Gett1(t2).ax;
                                    //#01283c8  LHU t5, $0000(t3)       ; t5=offMset.offt1[i].joint
                                    int t5 = offMset.Gett1(t2).joint;
                                    //#01283cc  LW t6, $0024(s0)        ; t6=s0.Txyz
                                    Vector4[] Txyz = s0.Txyz;
                                    //#01283d0  BEQ zero, zero, $001283a8   ; goto _1283a8;
                                    //#01283d4  ADDIU t7, t7, $fffa     ; t7=t7 -6
                                    t7 -= 6;

                                    Txyz[t5][t7] = offMset.Gett1(t2).value;

                                    break;
                                }
                        }
                        //#0128370  ADDIU t2, t2, $0001     ; t2=t2+1
                        //#0128374  SLT t7, t2, s1          ; t7=t2<offMset.cntt1
                        //#0128378  BNE t7, zero, $00128320 ; if (t7) { t3=t3+8; goto _128320; }
                        //#012837c  ADDIU t3, t3, $0008     ; t3=t3+8
                        continue;
                    }
                }
                //#0128380  LD s0, $0000(sp)        ; s0=sp.ORGs0
            }
            //#0128384  LD s1, $0008(sp)        ; s1=sp.ORGs1
            //#0128388  LD s2, $0010(sp)        ; s2=sp.ORGs2
            //#012838c  LD ra, $0018(sp)        ; ra=sp.ORGra
            //#0128390  JR ra                   ; return
            //#0128394  ADDIU sp, sp, $0020

        }

        /// <summary>
        /// return an AxBone offset
        /// </summary>
        /// <param name="a0">in</param>
        /// <returns>v0: offset AxBone</returns>
        private int fn1206a0(object a0) { // sp-16 bytes
            throw new NotImplementedException();
        }

        /// <summary>
        /// return mdlx joint count
        /// </summary>
        /// <param name="a0">in</param>
        /// <returns>v0: joint count</returns>
        private int fn120690(object a0) {
            throw new NotImplementedException();
        }
    }
}
