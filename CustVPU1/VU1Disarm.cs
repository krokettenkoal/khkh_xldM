using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace vu1Disarm {
    public class VU1Da {
        public Pki[] U = null;
        public Pki[] L = null;

        public void Decode(byte[] bin) {
            MemoryStream si = new MemoryStream(bin);
            BinaryReader br = new BinaryReader(si);
            U = new Pki[2048];
            L = new Pki[2048];
            //t < 0x46D && 
            for (int t = 0; t < 2048; t++) {
                int pc = t;
                uint vl = br.ReadUInt32();
                uint vu = br.ReadUInt32();
                string sl = "?";
                string su = "?";
                {
                    Pki p = null;
                    UIW w = new UIW(vu);
                    // Upper
                    if (false) { }
                    else if (0x1FD == w.M211) { //[ABS
                        p = new Pki("ABS." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x028 == w.M26) { //[ADD
                        p = new Pki("ADD." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x022 == w.M26) { //[ADDi
                        p = new Pki("ADDi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x020 == w.M26) {//[ADDq
                        p = new Pki("ADDq." + w.Odest, w.Ofddest, w.Ofsdest, "Q");
                    }
                    else if (0x000 == w.M24) {//[ADDbc
                        p = new Pki("ADD" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2BC == w.M211) {//[ADDA
                        p = new Pki("ADDA." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x43E == w.M211) {//[ADDAi
                        p = new Pki("ADDAi." + w.Odest, w.Oaccdest, w.Ofsdest, "I");
                    }
                    else if (0x43C == w.M211) { //[ADDAq
                        p = new Pki("ADDAq." + w.Odest, w.Oaccdest, w.Ofsdest, "Q");
                    }
                    else if (0x03C == w.M29) { //[ADDAbc
                        p = new Pki("ADDA" + w.Obc + "." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x1FF == w.M211) { //[CLIP
                        p = new Pki("CLIPw.xyz", w.Ofsxyz, w.Oftw);
                    }
                    else if (0x17C == w.M211) { //[FTOI0 
                        p = new Pki("FTOI0." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x17D == w.M211) { //[FTOI4
                        p = new Pki("FTOI4." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x17E == w.M211) { //[FTOI12
                        p = new Pki("FTOI12." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x17F == w.M211) { //[FTOI15
                        p = new Pki("FTOI15." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x13C == w.M211) { //[ITOF0
                        p = new Pki("ITOF0." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x13D == w.M211) { //[ITOF4
                        p = new Pki("ITOF4." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x13E == w.M211) { //[ITOF12
                        p = new Pki("ITOF12." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x13F == w.M211) { //[ITOF15
                        p = new Pki("ITOF15." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x029 == w.M26) { //[MADD
                        p = new Pki("MADD." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x023 == w.M26) { //[MADDi
                        p = new Pki("MADDi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x021 == w.M26) { //[MADDq
                        p = new Pki("MADDq." + w.Odest, w.Ofddest, w.Ofsdest, "Q");
                    }
                    else if (0x008 == w.M24) { //[MADDbc
                        p = new Pki("MADD" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2BD == w.M211) { //[MADDA
                        p = new Pki("MADDA." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x23F == w.M211) { //[MADDAi
                        p = new Pki("MADDAi." + w.Odest, w.Oaccdest, w.Ofsdest, "I");
                    }
                    else if (0x23D == w.M211) { //[MADDAq
                        p = new Pki("MADDAq." + w.Odest, w.Oaccdest, w.Ofsdest, "Q");
                    }
                    else if (0x0BC == w.M29) { //[MADDAbc
                        p = new Pki("MADDA" + w.Obc + "." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x02B == w.M26) { //[MAX
                        p = new Pki("MAX." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x01D == w.M26) { //[MAXi
                        p = new Pki("MAXi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x010 == w.M24) { //[MAXbc
                        p = new Pki("MAX" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x02F == w.M26) { //[MINI
                        p = new Pki("MINI." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x01F == w.M26) { //[MINIi
                        p = new Pki("MINIi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x014 == w.M24) { //[MINIbc
                        p = new Pki("MINI" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x02D == w.M26) { //[MSUB
                        p = new Pki("MSUB." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x027 == w.M26) { //[MSUBi
                        p = new Pki("MSUBi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x025 == w.M26) { //[MSUBq
                        p = new Pki("MSUBq." + w.Odest, w.Ofddest, w.Ofsdest, "Q");
                    }
                    else if (0x00C == w.M26) { //[MSUBbc
                        p = new Pki("MSUB" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2FD == w.M211) { //[MSUBA
                        p = new Pki("MSUBA." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x27F == w.M211) { //[MSUBAi
                        p = new Pki("MSUBAi." + w.Odest, w.Oaccdest, w.Ofsdest, "I");
                    }
                    else if (0x27D == w.M211) { //[MSUBAq
                        p = new Pki("MSUBAq." + w.Odest, w.Oaccdest, w.Ofsdest, "Q");
                    }
                    else if (0x0FC == w.M29) { //[MSUBAbc
                        p = new Pki("MSUBA" + w.Obc + "." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x02A == w.M26) { //[MUL
                        p = new Pki("MUL." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x01E == w.M26) { //[MULi
                        p = new Pki("MULi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x01C == w.M26) { //[MULq
                        p = new Pki("MULq." + w.Odest, w.Ofddest, w.Ofsdest, "Q");
                    }
                    else if (0x018 == w.M24) { //[MULbc
                        p = new Pki("MUL" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2BE == w.M211) { //[MULA
                        p = new Pki("MULA." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x1FE == w.M211) { //[MULAi
                        p = new Pki("MULAi." + w.Odest, w.Oaccdest, w.Ofsdest, "I");
                    }
                    else if (0x1FC == w.M211) { //[MULAq
                        p = new Pki("MULAq." + w.Odest, w.Oaccdest, w.Ofsdest, "Q");
                    }
                    else if (0x1BC == w.M29) { //[MULA
                        p = new Pki("MULA" + w.Obc + "." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2FF == w.M211) { //[NOP
                        p = new Pki("NOP");
                    }
                    else if (0x2FE == w.M211) { //[OPMULA
                        p = new Pki("OPMULA.xyz", w.Oaccdest, w.Ofsxyz, w.Oftxyz);
                    }
                    else if (0x02E == w.M26) { //[OPMSUB
                        p = new Pki("OPMSUB.xyz", w.Ofdxyz, w.Ofsxyz, w.Oftxyz);
                    }
                    else if (0x02C == w.M26) { //[SUB
                        p = new Pki("SUB." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x026 == w.M26) { //[SUBi
                        p = new Pki("SUBi." + w.Odest, w.Ofddest, w.Ofsdest, "I");
                    }
                    else if (0x024 == w.M26) { //[SUBq
                        p = new Pki("SUBq." + w.Odest, w.Ofddest, w.Ofsdest, "Q");
                    }
                    else if (0x004 == w.M24) { //[SUBbc
                        p = new Pki("SUB" + w.Obc + "." + w.Odest, w.Ofddest, w.Ofsdest, w.Oftbc);
                    }
                    else if (0x2FC == w.M211) { //[SUBA
                        p = new Pki("SUBA." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftdest);
                    }
                    else if (0x27E == w.M211) { //[SUBAi
                        p = new Pki("SUBAi." + w.Odest, w.Oaccdest, w.Ofsdest, "I");
                    }
                    else if (0x27C == w.M211) { //[SUBAq
                        p = new Pki("SUBAq." + w.Odest, w.Oaccdest, w.Ofsdest, "Q");
                    }
                    else if (0x07C == w.M29) { //[SUBAbc
                        p = new Pki("SUBA" + w.Obc + "." + w.Odest, w.Oaccdest, w.Ofsdest, w.Oftbc);
                    }
                    if (p != null) su = p.ToString();
                    U[t] = p;
                }
                {
                    Pki p = null;
                    LIW w = new LIW(vl);
                    // Lower
                    if (false) { }
                    else if (0 != (vu & 0x80000000)) { // ]LOI
                        p = new Pki("LOI", VC.UL2F(vl).ToString());
                    }
                    else if (0x8000033C == vl) {
                        p = new Pki("nop");
                    }
                    else if (0x40000000 == w.M70) { //]B
                        p = new Pki("B", w.Ob(pc));
                    }
                    else if (0x42000000 == w.M70) { //]BAL
                        p = new Pki("BAL", w.Oit, w.Ob(pc));
                    }
                    else if (0x800003BC == w.M711) { //]DIV
                        p = new Pki("DIV", "Q", w.Ofsfsf, w.Oftftf);
                    }
                    else if (0x800007FD == w.M711) { //]EATAN
                        p = new Pki("EATAN", "P", w.Ofsfsf);
                    }
                    else if (0x8000077C == w.M711) { //]EATANxy
                        p = new Pki("EATANxy", "P", w.Ofsfsf);
                    }
                    else if (0x8000077D == w.M711) { //]EATANxz
                        p = new Pki("EATANxz", "P", w.Ofs);
                    }
                    else if (0x800007FE == w.M711) { //]EEXP
                        p = new Pki("EEXP", "P", w.Ofsfsf);
                    }
                    else if (0x8000073E == w.M711) { //]ELENG
                        p = new Pki("ELENG", "P", w.Ofs);
                    }
                    else if (0x800007BE == w.M711) { //]ERCPR
                        p = new Pki("EPCPR", "P", w.Ofsfsf);
                    }
                    else if (0x8000073F == w.M711) { //]ERLENG
                        p = new Pki("ERLENG", "P", w.Ofs);
                    }
                    else if (0x8000073D == w.M711) { //]ERSADD
                        p = new Pki("ERSADD", "P", w.Ofs);
                    }
                    else if (0x800007BD == w.M711) { //]ERSQRT
                        p = new Pki("ERSQRT", "P", w.Ofsfsf);
                    }
                    else if (0x8000073C == w.M711) { //]ESADD
                        p = new Pki("ESADD", "P", w.Ofs);
                    }
                    else if (0x800007FC == w.M711) { //]ESIN
                        p = new Pki("ESIN", "P", w.Ofsfsf);
                    }
                    else if (0x800007BC == w.M711) { //]ESQRT
                        p = new Pki("ESQRT", "P", w.Ofsfsf);
                    }
                    else if (0x8000077E == w.M711) { //]ESUM
                        p = new Pki("ESUM", "P", w.Ofs);
                    }
                    else if (0x24000000 == w.M70) { //]FCAND
                        p = new Pki("FCAND", w.Lvi01, w.Oimm24);
                    }
                    else if (0x20000000 == w.M70) { //]FCEQ
                        p = new Pki("FCEQ", w.Lvi01, w.Oimm24);
                    }
                    else if (0x38000000 == w.M70) { //]FCGET
                        p = new Pki("FCGET", w.Oit);
                    }
                    else if (0x26000000 == w.M70) { //]FCOR
                        p = new Pki("FCOR", w.Lvi01, w.Oimm24);
                    }
                    else if (0x22000000 == w.M70) { //]FCSET
                        p = new Pki("FCSET", w.Oimm24);
                    }
                    else if (0x34000000 == w.M70) { //]FMAND
                        p = new Pki("FMAND", w.Oit, w.Ois);
                    }
                    else if (0x30000000 == w.M70) { //]FMEQ
                        p = new Pki("FMEQ", w.Oit, w.Ois);
                    }
                    else if (0x36000000 == w.M70) { //]FMOR
                        p = new Pki("FMOR", w.Oit, w.Ois);
                    }
                    else if (0x2C000000 == w.M70) { //]FSAND
                        p = new Pki("FSAND", w.Oit, w.Oimm12);
                    }
                    else if (0x28000000 == w.M70) { //]FSEQ
                        p = new Pki("FSEQ", w.Oit, w.Oimm12);
                    }
                    else if (0x2E000000 == w.M70) { //]FSOR
                        p = new Pki("FSOR", w.Oit, w.Oimm12);
                    }
                    else if (0x2A000000 == w.M70) { //]FSSET
                        p = new Pki("FSSET", w.Oimm12);
                    }
                    else if (0x80000030 == w.M76) { //]IADD
                        p = new Pki("IADD", w.Oid, w.Ois, w.Oit);
                    }
                    else if (0x80000032 == w.M76) { //]IADDI
                        p = new Pki("IADDI", w.Oit, w.Ois, w.Oimm5s);
                    }
                    else if (0x10000000 == w.M70) { //]IADDIU
                        p = new Pki("IADDIU", w.Oit, w.Ois, w.Oimm15);
                    }
                    else if (0x80000034 == w.M76) { //]IAND
                        p = new Pki("IAND", w.Oid, w.Ois, w.Oit);
                    }
                    else if (0x50000000 == w.M70) { //]IBEQ
                        p = new Pki("IBEQ", w.Oit, w.Ois, w.Ob(pc));
                    }
                    else if (0x5E000000 == w.M70) { //]IBGEZ
                        p = new Pki("IBGEZ", w.Ois, w.Ob(pc));
                    }
                    else if (0x5A000000 == w.M70) { //]IBGTZ
                        p = new Pki("IBGTZ", w.Ois, w.Ob(pc));
                    }
                    else if (0x5C000000 == w.M70) { //]IBLEZ
                        p = new Pki("IBLEZ", w.Ois, w.Ob(pc));
                    }
                    else if (0x58000000 == w.M70) { //]IBLTZ
                        p = new Pki("IBLTZ", w.Ois, w.Ob(pc));
                    }
                    else if (0x52000000 == w.M70) { //]IBNE
                        p = new Pki("IBNE", w.Oit, w.Ois, w.Ob(pc));
                    }
                    else if (0x08000000 == w.M70) { //]ILW
                        p = new Pki("ILW." + w.Odest, w.Oit, w.Oimm11isdest);
                    }
                    else if (0x800003FE == w.M711) { //]ILWR
                        p = new Pki("ILWR." + w.Odest, w.Oit, w.Oisdest);
                    }
                    else if (0x80000035 == w.M76) { //]IOR
                        p = new Pki("IOR", w.Oid, w.Ois, w.Oit);
                    }
                    else if (0x80000031 == w.M76) { //]ISUB
                        p = new Pki("ISUB", w.Oid, w.Ois, w.Oit);
                    }
                    else if (0x12000000 == w.M70) { //]ISUBIU
                        p = new Pki("ISUBIU", w.Oit, w.Ois, w.Oimm15);
                    }
                    else if (0x0A000000 == w.M70) { //]ISW
                        p = new Pki("ISW." + w.Odest, w.Oit, w.Oimm11isdest);
                    }
                    else if (0x800003FF == w.M711) { //]ISWR
                        p = new Pki("ISWR." + w.Odest, w.Oit, w.Oisdest);
                    }
                    else if (0x4A000000 == w.M70) { //]JALR
                        p = new Pki("JALR", w.Oit, w.Ois);
                    }
                    else if (0x48000000 == w.M70) { //]JR
                        p = new Pki("JR", w.Ois);
                    }
                    else if (0x00000000 == w.M70) { //]LQ
                        p = new Pki("LQ." + w.Odest, w.Oftdest, w.Oimm11is);
                    }
                    else if (0x8000037E == w.M711) { //]LQD
                        p = new Pki("LQD." + w.Odest, w.Oftdest, w.Ommis);
                    }
                    else if (0x8000037C == w.M711) { //]LQI
                        p = new Pki("LQI." + w.Odest, w.Oftdest, w.Oispp);
                    }
                    else if (0x800003FD == w.M711) { //]MFIR
                        p = new Pki("MFIR." + w.Odest, w.Oftdest, w.Ois);
                    }
                    else if (0x8000067C == w.M711) { //]MFP
                        p = new Pki("MFP." + w.Odest, w.Oftdest, "P");
                    }
                    else if (0x8000033C == w.M711) { //]MOVE
                        p = new Pki("MOVE." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x8000033D == w.M711) { //]MR32
                        p = new Pki("MR32." + w.Odest, w.Oftdest, w.Ofsdest);
                    }
                    else if (0x800003FC == w.M711) { //]MTIR
                        p = new Pki("MTIR", w.Oit, w.Ofsfsf);
                    }
                    else if (0x8000043D == w.M711) { //]RGET
                        p = new Pki("RGET." + w.Odest, w.Oftdest, "R");
                    }
                    else if (0x8000043E == w.M711) { //]RINIT
                        p = new Pki("RINIT", "R", w.Ofsfsf);
                    }
                    else if (0x8000043C == w.M711) { //]RNEXT
                        p = new Pki("RNEXT." + w.Odest, w.Oftdest, "R");
                    }
                    else if (0x800003BE == w.M711) { //]RSQRT
                        p = new Pki("RSQRT", "Q", w.Ofsfsf, w.Oftftf);
                    }
                    else if (0x8000043F == w.M711) { //]RXOR
                        p = new Pki("RXOR", "R", w.Ofsfsf);
                    }
                    else if (0x02000000 == w.M70) { //]SQ
                        p = new Pki("SQ." + w.Odest, w.Ofsdest, w.Oimm11it);
                    }
                    else if (0x8000037F == w.M711) { //]SQD
                        p = new Pki("SQD." + w.Odest, w.Ofsdest, w.Ommit);
                    }
                    else if (0x8000037D == w.M711) { //]SQI
                        p = new Pki("SQI." + w.Odest, w.Ofsdest, w.Oitpp);
                    }
                    else if (0x800003BD == w.M711) { //]SQRT
                        p = new Pki("SQRT", "Q", w.Oftftf);
                    }
                    else if (0x800007BF == w.M711) { //]WAITP
                        p = new Pki("WAITP");
                    }
                    else if (0x800003BF == w.M711) { //]WAITQ
                        p = new Pki("WAITQ");
                    }
                    else if (0x800006FC == w.M711) { //]XGKICK
                        p = new Pki("XGKICK", w.Ois);
                    }
                    else if (0x800006BD == w.M711) { //]XITOP
                        p = new Pki("XITOP", w.Oit);
                    }
                    else if (0x800006BC == w.M711) { //]XTOP
                        p = new Pki("XTOP", w.Oit);
                    }
                    sl = (p != null) ? p.ToString() : sl;
                    L[t] = p;
                }
                //Console.WriteLine("{0:x3}: {1,-40} {2,-40} {3}", t, su, sl, BUtil.B7455542Toa(vl));
            }
        }

        class VC {
            public static float UL2F(uint val) {
                MemoryStream os = new MemoryStream();
                new BinaryWriter(os).Write(val);
                os.Position = 0;
                return new BinaryReader(os).ReadSingle();
            }

            public static string Timm5s(int val) {
                val &= 0x1F;
                if (0 == (val & 0x10))
                    return "$" + (val).ToString("x");
                return "-$" + (0x20 - val).ToString("x");
            }

            public static string Timm11s(int val) {
                val &= 0x7FF;
                if (0 == (val & 0x400))
                    return "$" + (val).ToString("x");
                return "-$" + (0x800 - val).ToString("x");
            }

            public static string Timm15s(int val) {
                val &= 0x7FFF;
                if (0 == (val & 0x4000))
                    return "$" + (val).ToString("x");
                return "-$" + (0x8000 - val).ToString("x");
            }
        }

        class LIW {
            uint val;

            public LIW(uint val) {
                this.val = val;
            }
            public uint M70 { get { return val & 0xFE000000; } }
            public uint M711 { get { return val & 0xFE0007FF; } }
            public uint M76 { get { return val & 0xFE00003F; } }

            public string Lvi01 { get { return "$vi1"; } }

            public int dest { get { return (int)((val >> 21) & 0xF); } }
            public int itr { get { return (int)((val >> 16) & 0x1F); } }
            public int isr { get { return (int)((val >> 11) & 0x1F); } }
            public int imm11 { get { return (int)((val >> 0) & 0x7FF); } }
            public int fsr { get { return (int)((val >> 11) & 0x1F); } }
            public int ftr { get { return (int)((val >> 16) & 0x1F); } }
            public int fsf { get { return (int)((val >> 21) & 3); } }
            public int ftf { get { return (int)((val >> 23) & 3); } }
            public int imm24 { get { return (int)((val >> 0) & 0xFFFFFF); } }
            public int imm15 { get { return (int)(((val >> 0) & 0x7FF) | ((val >> 10) & 0x7800)); } }
            public int imm12 { get { return (int)(((val >> 0) & 0x7FF) | ((val >> 10) & 0x0800)); } }
            public int idr { get { return (int)((val >> 6) & 0x1F); } }
            public int imm5 { get { return (int)((val >> 6) & 0x1F); } }
            public int imm11s { get { int v = imm11; return (0 != (v & 0x400)) ? v - 0x800 : v; } }
            public int imm5s { get { int v = imm5; return (0 != (v & 0x10)) ? v - 0x20 : v; } }
            public int imm15s { get { int v = imm5; return (0 != (v & 0x4000)) ? v - 0x8000 : v; } }

            public string Ob(int pc) { return ((imm11s + 1)).ToString("+0;-0"); }
            public string Ofsfsf { get { return "vf" + fsr + Ofsf; } }
            public string Ofsf {
                get {
                    if (fsf == 0) return "x";
                    if (fsf == 1) return "y";
                    if (fsf == 2) return "z";
                    if (fsf == 3) return "w";
                    throw new NotSupportedException();
                }
            }
            public string Oftftf { get { return "vf" + ftr + Oftf; } }
            public string Oftf {
                get {
                    if (ftf == 0) return "x";
                    if (ftf == 1) return "y";
                    if (ftf == 2) return "z";
                    if (ftf == 3) return "w";
                    throw new NotSupportedException();
                }
            }
            public string Ofs { get { return "vf" + fsr; } }
            public string Oimm24 { get { return "$" + imm24.ToString("x"); } }
            public string Oit { get { return "$vi" + itr; } }
            public string Ois { get { return "$vi" + isr; } }
            public string Oimm12 { get { return "$" + imm12.ToString("x"); } }
            public string Oid { get { return "$vi" + idr; } }
            public string Oimm11isdest { get { return VC.Timm11s(imm11s * 16) + "(" + Ois + ")"; } }
            public string Odest {
                get {
                    string text = "";
                    if (0 != (dest & 8)) text += "x";
                    if (0 != (dest & 4)) text += "y";
                    if (0 != (dest & 2)) text += "z";
                    if (0 != (dest & 1)) text += "w";
                    return text;
                }
            }
            public string Oisdest { get { return "(" + Ois + ")" + Odest; } }
            public string Oimm11is { get { return VC.Timm15s(imm11s * 16) + "(" + Ois + ")"; } }
            public string Oft { get { return "vf" + ftr; } }
            public string Oftdest { get { return Oft; } }
            public string Ommis { get { return "(--" + Ois + ")"; } }
            public string Oispp { get { return "(" + Ois + "++)"; } }
            public string Ofsdest { get { return Ofs; } }
            public string Oimm11it { get { return VC.Timm15s(imm11s * 16) + "(" + Oit + ")"; } }
            public string Ommit { get { return "(--" + Oit + ")"; } }
            public string Oitpp { get { return "(" + Oit + "++)"; } }
            public string Oimm15 { get { return "$" + (imm15).ToString("x"); } }
            public string Oimm5s { get { return VC.Timm5s(imm5s); } }
        }
        class UIW {
            uint val;

            public UIW(uint val) {
                this.val = val;
            }
            public uint M211 { get { return val & 0x060007FF; } }
            public uint M26 { get { return val & 0x0600003F; } }
            public uint M24 { get { return val & 0x0600003C; } }
            public uint M29 { get { return val & 0x060007FC; } }

            public int dest { get { return (int)((val >> 21) & 0xF); } }
            public int ft { get { return (int)((val >> 16) & 0x1F); } }
            public int fs { get { return (int)((val >> 11) & 0x1F); } }
            public int fd { get { return (int)((val >> 6) & 0x1F); } }
            public int bc { get { return (int)((val >> 0) & 3); } }

            public string Odest { get { return VUr.destis(dest); } }
            public string Oftdest { get { return VUr.VFis(ft); } }
            public string Ofsdest { get { return VUr.VFis(fs); } }
            public string Ofddest { get { return VUr.VFis(fd); } }
            public string Oftbc { get { return VUr.VFis(ft) + VUr.BCis(bc); } }
            public string Oaccdest { get { return "ACC"; } }
            public string Ofsxyz { get { return VUr.VFis(fs); } }
            public string Oftw { get { return VUr.VFis(ft) + "w"; } }
            public string Oaccxyz { get { return "ACCxyz"; } }
            public string Oftxyz { get { return VUr.VFis(ft); } }
            public string Ofdxyz { get { return VUr.VFis(fd); } }
            public string Obc { get { return VUr.BCis(bc); } }
        }

        class Usn {
            public static int imm5s(int val) {
                val &= 0x1F;
                if (0 != (val & 0x10))
                    return val - 0x20;
                return val;
            }
            public static int imm11s(int val) {
                val &= 0x7FF;
                if (0 != (val & 0x400))
                    return val - 0x800;
                return val;
            }
        }
        class VUr {
            public static string VFdestis(int ft, int dest) {
                return VFis(ft) + destis(dest);
            }
            public static string destis(int dest) {
                string str = "";
                if (0 != (dest & 8)) str += "x";
                if (0 != (dest & 4)) str += "y";
                if (0 != (dest & 2)) str += "z";
                if (0 != (dest & 1)) str += "w";
                return str;
            }
            public static string imm24is(int imm24) {
                return "0x" + imm24.ToString("X6");
            }
            public static string imm12is(int imm11) {
                return "0x" + imm11.ToString("X6");
            }
            public static string imm15is(int imm11) {
                return "0x" + imm11.ToString("X6");
            }
            public static string imm11is(int imm11) {
                return "0x" + imm11.ToString("X6");
            }
            public static string VIis(int it) {
                return "vi" + it;
            }
            public static string VFis(int fs_ft) {
                return "vf" + fs_ft;
            }
            public static string VFis(int fs, int fsf) {
                char c;
                if (fsf == 0) c = 'x';
                else if (fsf == 1) c = 'y';
                else if (fsf == 2) c = 'z';
                else if (fsf == 3) c = 'w';
                else throw new NotSupportedException();

                return "VF" + fs + c;
            }
            public static string BCis(int bc) {
                if (bc == 0) return "x";
                if (bc == 1) return "y";
                if (bc == 2) return "z";
                if (bc == 3) return "w";
                throw new NotSupportedException();
            }
        }
    }

    public class Pki {
        public readonly string[] al;

        public Pki(string opc) {
            al = new string[] { opc };
        }
        public Pki(string opc, string opr1) {
            al = new string[] { opc, opr1 };
        }
        public Pki(string opc, string opr1, string opr2) {
            al = new string[] { opc, opr1, opr2 };
        }
        public Pki(string opc, string opr1, string opr2, string opr3) {
            al = new string[] { opc, opr1, opr2, opr3 };
        }

        public override string ToString() {
            string text = al[0] + " ";
            for (int x = 1; x < al.Length; x++) {
                if (x != 1)
                    text += ", ";
                text += al[x];
            }
            return text;
        }
    }
}
