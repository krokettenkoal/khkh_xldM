using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils.EEDis {
    public class EEDisarm {
        public static EEis parse(uint word, uint pc) {
            MIPSi a = new MIPSi(word);
            EEis e;
            // (CPU Instruction Set)
            if (false) { }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100000) { //ADD
                e = new EEis("ADD", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M001000) { //ADDI
                e = new EEis("ADDI", a.Trt, a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M001001) { //ADDIU
                e = new EEis("ADDIU", a.Trt, a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100001) { //ADDU
                e = new EEis("ADDU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100100) { //AND
                e = new EEis("AND", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M001100) { //ANDI
                e = new EEis("ANDI", a.Trt, a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000100) { //BEQ
                e = new EEis("BEQ", a.Trs, a.Trt, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010100) { //BEQL
                e = new EEis("BEQL", a.Trs, a.Trt, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M00001) { //BGEZ
                e = new EEis("BGEZ", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M10001) { //BGEZAL
                e = new EEis("BGEZAL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M10011) { //BGEZALL
                e = new EEis("BGEZALL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M00011) { //BGEZL
                e = new EEis("BGEZL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000111) { //BGTZ
                e = new EEis("BGTZ", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010111) { //BGTZL
                e = new EEis("BGTZL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000110) { //BLEZ
                e = new EEis("BLEZ", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010110) { //BLEZL
                e = new EEis("BLEZL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M00000) { //BLTZ
                e = new EEis("BLTZ", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M10000) { //BLTZAL
                e = new EEis("BLTZAL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M10010) { //BLTZALL
                e = new EEis("BLTZALL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M00010) { //BLTZL
                e = new EEis("BLTZL", a.Trs, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000101) { //BNE
                e = new EEis("BNE", a.Trs, a.Trt, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010101) { //BNEL
                e = new EEis("BNEL", a.Trs, a.Trt, a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001101) { //BREAK
                e = new EEis("BREAK");
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101100) { //DADD
                e = new EEis("DADD", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011000) { //DADDI
                e = new EEis("DADDI", a.Trt, a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M011001) { //DADDIU
                e = new EEis("DADDIU", a.Trt, a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101101) { //DADDU
                e = new EEis("DADDU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M011010) { //DIV
                e = new EEis("DIV", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M011011) { //DIVU
                e = new EEis("DIVU", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111000) { //DSLL
                e = new EEis("DSLL", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111100) { //DSLL32
                e = new EEis("DSLL32", a.Trd, a.Trt, a.Tsa32);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010100) { //DSLLV
                e = new EEis("DSLLV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111011) { //DSRA
                e = new EEis("DSRA", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111111) { //DSRA32
                e = new EEis("DSRA32", a.Trd, a.Trt, a.Tsa32);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010111) { //DSRAV
                e = new EEis("DSRAV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111010) { //DSRL
                e = new EEis("DSRL", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M111110) { //DSRL32
                e = new EEis("DSRL32", a.Trd, a.Trt, a.Tsa32);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010110) { //DSRLV
                e = new EEis("DSRLV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101110) { //DSUB
                e = new EEis("DSUB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101111) { //DSUBU
                e = new EEis("DSUBU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000010) { //J
                e = new EEis("J", a.Toff26(pc));
            }
            else if (a.Oc26 == Ma.M000011) { //JAL
                e = new EEis("JAL", a.Toff26(pc));
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001001) { //JALR
                e = new EEis("JALR", a.Trd, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001000) { //JR
                e = new EEis("JR", a.Trs);
            }
            else if (a.Oc26 == Ma.M100000) { //LB
                e = new EEis("LB", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100100) { //LBU
                e = new EEis("LBU", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M110111) { //LD
                e = new EEis("LD", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M011010) { //LDL
                e = new EEis("LDL", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M011011) { //LDR
                e = new EEis("LDR", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100001) { //LH
                e = new EEis("LH", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100101) { //LHU
                e = new EEis("LHU", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M001111) { //LUI
                e = new EEis("LUI", a.Trt, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M100011) { //LW
                e = new EEis("LW", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100010) { //LWL
                e = new EEis("LWL", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100110) { //LWR
                e = new EEis("LWR", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M100111) { //LWU
                e = new EEis("LWU", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010000) { //MFHI
                e = new EEis("MFHI", a.Trd);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010010) { //MFLO
                e = new EEis("MFLO", a.Trd);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001011) { //MOVN
                e = new EEis("MOVN", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001010) { //MOVZ
                e = new EEis("MOVZ", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010001) { //MTHI
                e = new EEis("MTHI", a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M010011) { //MTLO
                e = new EEis("MTLO", a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M011000) { //MULT
                e = new EEis("MULT", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M011001) { //MULTU
                e = new EEis("MULTU", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100111) { //NOR
                e = new EEis("NOR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100101) { //OR
                e = new EEis("OR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M001101) { //ORI
                e = new EEis("ORI", a.Trt, a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M110011) { //PREF
                e = new EEis("PREF", a.Thint, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101000) { //SB
                e = new EEis("SB", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M111111) { //SD
                e = new EEis("SD", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101100) { //SDL
                e = new EEis("SDL", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101101) { //SDR
                e = new EEis("SDR", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101001) { //SH
                e = new EEis("SH", a.Trt, a.Toffbrs);
            }
            else if (a.Oc32 == 0) { //NOP
                e = new EEis("NOP");
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000000) { //SLL
                e = new EEis("SLL", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000100) { //SLLV
                e = new EEis("SLLV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101010) { //SLT
                e = new EEis("SLT", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M001010) { //SLTI
                e = new EEis("SLTI", a.Trt, a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M001011) { //SLTIU
                e = new EEis("SLTIU", a.Trt, a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101011) { //SLTU
                e = new EEis("SLTU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000011) { //SRA
                e = new EEis("SRA", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000111) { //SRAV
                e = new EEis("SRAV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000010) { //SRL
                e = new EEis("SRL", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M000110) { //SRLV
                e = new EEis("SRLV", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100010) { //SUB
                e = new EEis("SUB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100011) { //SUBU
                e = new EEis("SUBU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M101011) { //SW
                e = new EEis("SW", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101010) { //SWL
                e = new EEis("SWL", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M101110) { //SWR
                e = new EEis("SWR", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001111 && (a.Ostype & 0x10) == 0x00) { //SYNC.L
                e = new EEis("SYNC.L");
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001111 && (a.Ostype & 0x10) == 0x10) { //SYNC.P
                e = new EEis("SYNC.P");
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M001100) { //SYSCALL
                e = new EEis("SYSCALL", a.Tcode6);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110110) { //TEQ
                e = new EEis("TEQ", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01100) { //TEQI
                e = new EEis("TEQI", a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110000) { //TGE
                e = new EEis("TGE", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01000) { //TGEI
                e = new EEis("TGEI", a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01000) { //TGEIU
                e = new EEis("TGEIU", a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110001) { //TGEU
                e = new EEis("TGEU", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110010) { //TLT
                e = new EEis("TLT", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01010) { //TLTI
                e = new EEis("TLTI", a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01011) { //TLTIU
                e = new EEis("TLTIU", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110011) { //TLTU
                e = new EEis("TLTU", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M110110) { //TNE
                e = new EEis("TNE", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M01110) { //TNEI
                e = new EEis("TNEI", a.Trs, a.Timm16s);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M100110) { //XOR
                e = new EEis("XOR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M001110) { //XORI
                e = new EEis("XORI", a.Trt, a.Trs, a.Timm16u);
            }
            // (EE-core spec. inst.)
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M011010) { //DIV1
                e = new EEis("DIV1", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M011011) { //DIVU1
                e = new EEis("DIVU1", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011110) { //LQ
                e = new EEis("LQ", a.Trt, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M000000) { //MADD
                e = new EEis("MADD", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M100000) { //MADD1
                e = new EEis("MADD1", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M000001) { //MADDU
                e = new EEis("MADDU", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M100001) { //MADDU1
                e = new EEis("MADDU1", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M010000) { //MFHI1
                e = new EEis("MFHI1", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M010010) { //MFLO1
                e = new EEis("MFLO1", a.Trd);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101000) { //MFSA
                e = new EEis("MFSA", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M010001) { //MTHI1
                e = new EEis("MTHI1", a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M010011) { //MTLO1
                // http://www.kernel-api.org/docs/online/2.6.17/d2/d5d/mipsregs_8h-source.html
                e = new EEis("MTLO1", a.Trs);
            }
            else if (a.Oc26 == Ma.M000000 && a.Oc0 == Ma.M101001) { //MTSA
                e = new EEis("MTSA", a.Trs);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M11000) { //MTSAB
                e = new EEis("MTSAB", a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M000001 && a.Oc16 == Ma.M11001) { //MTSAH
                e = new EEis("MTSAH", a.Trs, a.Timm16u);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M011000) { //MULT1
                e = new EEis("MULT1", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M011001) { //MULTU1
                e = new EEis("MULTU1", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M101000) { //PABSH
                e = new EEis("PABSH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00001 && a.Oc0 == Ma.M101000) { //PABSW
                e = new EEis("PABSW", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M001000) { //PADDB
                e = new EEis("PADDB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M001000) { //PADDH
                e = new EEis("PADDH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11000 && a.Oc0 == Ma.M001000) { //PADDSB
                e = new EEis("PADDSB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10100 && a.Oc0 == Ma.M001000) { //PADDSH
                e = new EEis("PADDSH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10000 && a.Oc0 == Ma.M001000) { //PADDSW
                e = new EEis("PADDSW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11000 && a.Oc0 == Ma.M101000) { //PADDUB
                e = new EEis("PADDUB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10100 && a.Oc0 == Ma.M101000) { //PADDUH
                e = new EEis("PADDUH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10000 && a.Oc0 == Ma.M101000) { //PADDUW
                e = new EEis("PADDUW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00000 && a.Oc0 == Ma.M001000) { //PADDW
                e = new EEis("PADDW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M101000) { //PADSBH
                e = new EEis("PADSBH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10010 && a.Oc0 == Ma.M001001) { //PAND
                e = new EEis("PAND", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M101000) { //PCEQB
                e = new EEis("PCEQB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00110 && a.Oc0 == Ma.M101000) { //PCEQH
                e = new EEis("PCEQH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00010 && a.Oc0 == Ma.M101000) { //PCEQW
                e = new EEis("PCEQW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M001000) { //PCGTB
                e = new EEis("PCGTB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00110 && a.Oc0 == Ma.M001000) { //PCGTH
                e = new EEis("PCGTH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00010 && a.Oc0 == Ma.M001000) { //PCGTW
                e = new EEis("PCGTW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11011 && a.Oc0 == Ma.M101001) { //PCPYH
                e = new EEis("PCPYH", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01110 && a.Oc0 == Ma.M001001) { //PCPYLD
                e = new EEis("PCPYLD", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01110 && a.Oc0 == Ma.M101001) { //PCPYUD
                e = new EEis("PCPYUD", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11101 && a.Oc0 == Ma.M001001) { //PDIVBW
                e = new EEis("PDIVBW", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01101 && a.Oc0 == Ma.M101001) { //PDIVUW
                e = new EEis("PDIVUW", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01101 && a.Oc0 == Ma.M001001) { //PDIVW
                e = new EEis("PDIVW", a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11010 && a.Oc0 == Ma.M101001) { //PEXCH
                e = new EEis("PEXCH", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11110 && a.Oc0 == Ma.M101001) { //PEXCW
                e = new EEis("PEXCW", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11010 && a.Oc0 == Ma.M001001) { //PEXEH
                e = new EEis("PEXEH", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11110 && a.Oc0 == Ma.M001001) { //PEXEW
                e = new EEis("PEXEW", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11110 && a.Oc0 == Ma.M001000) { //PEXT5
                e = new EEis("PEXT5", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11010 && a.Oc0 == Ma.M001000) { //PEXTLB
                e = new EEis("PEXTLB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10110 && a.Oc0 == Ma.M001000) { //PEXTLH
                e = new EEis("PEXTLH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10010 && a.Oc0 == Ma.M001000) { //PEXTLW
                e = new EEis("PEXTLW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11010 && a.Oc0 == Ma.M101000) { //PEXTUB
                e = new EEis("PEXTUB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10110 && a.Oc0 == Ma.M101000) { //PEXTUH
                e = new EEis("PEXTUH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10010 && a.Oc0 == Ma.M101000) { //PEXTUW
                e = new EEis("PEXTUW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10001 && a.Oc0 == Ma.M001001) { //PHMADH
                e = new EEis("PHMADH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10101 && a.Oc0 == Ma.M001001) { //PHMSBG
                e = new EEis("PHMSBG", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M101001) { //PINTEH
                e = new EEis("PINTEH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M000100) { //PLZCW
                e = new EEis("PLZCW", a.Trd, a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10000 && a.Oc0 == Ma.M001001) { //PMADDH
                e = new EEis("PMADDH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00000 && a.Oc0 == Ma.M101001) { //PMADDUW
                e = new EEis("PMADDUW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00000 && a.Oc0 == Ma.M001001) { //PMADDW
                e = new EEis("PMADDW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M001000) { //PMAXH
                e = new EEis("PMAXH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00011 && a.Oc0 == Ma.M001000) { //PMAXW
                e = new EEis("PMAXW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M001001) { //PMFHI
                e = new EEis("PMFHI", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00011 && a.Oc0 == Ma.M110000) { //PMFHL.LH
                e = new EEis("PMFHL.LH", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00000 && a.Oc0 == Ma.M110000) { //PMFHL.LW
                e = new EEis("PMFHL.LW", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M110000) { //PMFHL.SH
                e = new EEis("PMFHL.SH", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00010 && a.Oc0 == Ma.M110000) { //PMFHL.SLW
                e = new EEis("PMFHL.SLW", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00001 && a.Oc0 == Ma.M110000) { //PMFHL.UW
                e = new EEis("PMFHL.UW", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01001 && a.Oc0 == Ma.M001001) { //PMFLO
                e = new EEis("PMFLO", a.Trd);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M101000) { //PMINH
                e = new EEis("PMINH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00011 && a.Oc0 == Ma.M101000) { //PMINW
                e = new EEis("PMINW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10100 && a.Oc0 == Ma.M001001) { //PMSUBH
                e = new EEis("PMSUBH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M001001) { //PMSUBW
                e = new EEis("PMSUBW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M101001) { //PMTHI
                e = new EEis("PMTHI", a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00000 && a.Oc0 == Ma.M110001) { //PMTHL.LW
                e = new EEis("PMTHL.LW", a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01001 && a.Oc0 == Ma.M101001) { //PMTLO
                e = new EEis("PMTLO", a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11100 && a.Oc0 == Ma.M001001) { //PMULTH
                e = new EEis("PMULTH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01100 && a.Oc0 == Ma.M101001) { //PMULTUW
                e = new EEis("PMULTUW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01100 && a.Oc0 == Ma.M001001) { //PMULTW
                e = new EEis("PMULTW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10011 && a.Oc0 == Ma.M101001) { //PNOR
                e = new EEis("PNOR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10010 && a.Oc0 == Ma.M101001) { //POR
                e = new EEis("POR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11111 && a.Oc0 == Ma.M001000) { //PPAC5
                e = new EEis("PPAC5", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11011 && a.Oc0 == Ma.M001000) { //PPACB
                e = new EEis("PPACB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10111 && a.Oc0 == Ma.M001000) { //PPACH
                e = new EEis("PPACH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10011 && a.Oc0 == Ma.M001000) { //PPACW
                e = new EEis("PPACW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11011 && a.Oc0 == Ma.M001001) { //PREVH
                e = new EEis("PREVH", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11111 && a.Oc0 == Ma.M001001) { //PROT3W
                e = new EEis("PROT3W", a.Trd, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M110100) { //PSLLH
                e = new EEis("PSLLH", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00010 && a.Oc0 == Ma.M001001) { //PSLLVW
                e = new EEis("PSLLVW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M111100) { //PSLLW
                e = new EEis("PSLLW", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M110111) { //PSRAH
                e = new EEis("PSRAH", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00011 && a.Oc0 == Ma.M101001) { //PSRAVW
                e = new EEis("PSRAVW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M111111) { //PSRAW
                e = new EEis("PSRAW", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M110110) { //PSRLH
                e = new EEis("PSRLH", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00011 && a.Oc0 == Ma.M001001) { //PSRLVW
                e = new EEis("PSRLVW", a.Trd, a.Trt, a.Trs);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc0 == Ma.M111110) { //PSRLW
                e = new EEis("PSRLW", a.Trd, a.Trt, a.Tsa);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M01001 && a.Oc0 == Ma.M001000) { //PSUBB
                e = new EEis("PSUBB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M001000) { //PSUBH
                e = new EEis("PSUBH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11001 && a.Oc0 == Ma.M001000) { //PSUBSB
                e = new EEis("PSUBSB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10101 && a.Oc0 == Ma.M001000) { //PSUBSH
                e = new EEis("PSUBSH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10001 && a.Oc0 == Ma.M001000) { //PSUBSW
                e = new EEis("PSUBSW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11001 && a.Oc0 == Ma.M101000) { //PSUBUB
                e = new EEis("PSUBUB", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10101 && a.Oc0 == Ma.M101000) { //PSUBUH
                e = new EEis("PSUBUH", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10001 && a.Oc0 == Ma.M101000) { //PSUBUW
                e = new EEis("PSUBUW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M00001 && a.Oc0 == Ma.M001000) { //PSUBW
                e = new EEis("PSUBW", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M10011 && a.Oc0 == Ma.M001001) { //PXOR
                e = new EEis("PXOR", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011100 && a.Oc6 == Ma.M11011 && a.Oc0 == Ma.M101000) { //QFSRV
                e = new EEis("QFSRV", a.Trd, a.Trs, a.Trt);
            }
            else if (a.Oc26 == Ma.M011111) { //SQ
                e = new EEis("SQ", a.Trt, a.Toffbrs);
            }
            // (COP0)
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00000) { //BC0F
                e = new EEis("BC0F", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00010) { //BC0FL
                e = new EEis("BC0FL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00001) { //BC0T
                e = new EEis("BC0T", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00011) { //BC0TL
                e = new EEis("BC0TL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011000) { //ERET
                e = new EEis("ERET");
            }
            else if (a.Oc26 == Ma.M010000 && a.Oc21 == Ma.M00000 && a.Oc0 == Ma.M000000) { //MFC0
                e = new EEis("MFC0", a.Trt, a.Ord.ToString());
            }
            // (COP1)
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000101) { //ABS.S
                e = new EEis("ABS.S", a.Tfd, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000000) { //ADD.S
                e = new EEis("ADD.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011000) { //ADDA.S
                e = new EEis("ADDA.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00000) { //BC1F
                e = new EEis("BC1F", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00010) { //BC1FL
                e = new EEis("BC1FL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00001) { //BC1T
                e = new EEis("BC1T", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00011) { //BC1TL
                e = new EEis("BC1TL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M110010) { //C.EQ.S
                e = new EEis("C.EQ.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M110000) { //C.F.S
                e = new EEis("C.F.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M110110) { //C.LE.S
                e = new EEis("C.LE.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M110100) { //C.LT.S
                e = new EEis("C.LT.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M00010) { //CFC1
                e = new EEis("CFC1", a.Trt, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M00110) { //CTC1
                e = new EEis("CFC1", a.Trt, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10100 && a.Oc0 == Ma.M100000) { //CVT.S.W
                e = new EEis("CVT.S.W", a.Tfd, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M100100) { //CVT.W.S
                e = new EEis("CVT.W.S", a.Tfd, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000011) { //DIV.S
                e = new EEis("DIV.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M110001) { //LWC1
                e = new EEis("LWC1", a.Tft, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011100) { //MADD.S
                e = new EEis("MADD.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011110) { //MADDA.S
                e = new EEis("MADDA.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M101000) { //MAX.S
                e = new EEis("MAX.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M00000) { //MFC1
                e = new EEis("MFC1", a.Trt, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M110000) { //MIN.S
                e = new EEis("MIN.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000110) { //MOV.S
                e = new EEis("MOV.S", a.Tfd, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011101) { //MSUB.S
                e = new EEis("MSUB.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011111) { //MSUBA.S
                e = new EEis("MSUBA.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M00100) { //MTC1
                e = new EEis("MTC1", a.Trt, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000010) { //MUL.S
                e = new EEis("MUL.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011010) { //MULA.S
                e = new EEis("MULA.S", a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000111) { //NEG.S
                e = new EEis("NEG.S", a.Tfd, a.Tfs);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M010110) { //RSQRT.S
                e = new EEis("RSQRT.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000100) { //SQRT.S
                e = new EEis("SQRT.S", a.Tfd, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M000001) { //SUB.S
                e = new EEis("SUB.S", a.Tfd, a.Tfs, a.Tft);
            }
            else if (a.Oc26 == Ma.M010001 && a.Oc21 == Ma.M10000 && a.Oc0 == Ma.M011001) { //SUBA.S
                e = new EEis("SUBA.S", a.Tfs, a.Tft, "I");
            }
            else if (a.Oc26 == Ma.M111001) { //SWC1
                e = new EEis("SWC1", a.Tft, a.Toffbrs);
            }
            // (COP2)
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00000) { //BC2F
                e = new EEis("BC2F", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00010) { //BC2FL
                e = new EEis("BC2FL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00001) { //BC2T
                e = new EEis("BC2T", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M01000 && a.Oc16 == Ma.M00011) { //BC2TL
                e = new EEis("BC2TL", a.Toff16(pc));
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00010 && a.Oi == 1) { //CFC2.I
                e = new EEis("CFC2.I", a.Trt, a.Tid2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00010 && a.Oi == 0) { //CFC2
                e = new EEis("CFC2", a.Trt, a.Tid2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00110 && a.Oi == 1) { //CTC2.I
                e = new EEis("CTC2.I", a.Trt, a.Tid2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00110 && a.Oi == 0) { //CTC2
                e = new EEis("CTC2", a.Trt, a.Tid2);
            }
            else if (a.Oc26 == Ma.M110110) { //LQC2
                e = new EEis("LQC2", a.Tft2, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00001 && a.Oi == 1) { //QMFC2.I
                e = new EEis("QMFC2.I", a.Trt, a.Tfd2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00001 && a.Oi == 0) { //QMFC2
                e = new EEis("QMFC2", a.Trt, a.Tfd2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00101 && a.Oi == 1) { //QMTC2.I
                e = new EEis("QMTC2.I", a.Trt, a.Tfd2);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc21 == Ma.M00101 && a.Oi == 0) { //QMTC2
                e = new EEis("QMTC2", a.Trt, a.Tfd2);
            }
            else if (a.Oc26 == Ma.M111110) { //SQC2
                e = new EEis("SQC2", a.Tft2, a.Toffbrs);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M111101) { //VABS.
                e = new EEis("VABS." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101000) { //VADD.
                e = new EEis("VADD." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100010) { //VADDi.
                e = new EEis("VADDi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100000) { //VADDq.
                e = new EEis("VADDq." + a.Vd, a.Vfdd, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0000) { //VADDbc.
                e = new EEis("VADD" + a.Vbc + "." + a.Vd, a.Vfdd, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M111100) { //VADDA.
                e = new EEis("VADDA." + a.Vd, a.Vacc, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111110) { //VADDAi.
                e = new EEis("VADDAi." + a.Vd, a.Vacc, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111100) { //VADDAq.
                e = new EEis("VADDAq." + a.Vd, a.Vacc, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00000 && a.Oc2c4 == Ma.M1111) { //VADDAbc.
                e = new EEis("VADDA" + a.Vbc + "." + a.Vd, a.Vacc, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M111000) { //VCALLMS
                e = new EEis("VCALLMS", a.Vimm15);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M111001) { //VCALLMSR
                e = new EEis("VCALLMSR", "$vi27");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M111111) { //VCLIP.
                e = new EEis("VCLIPw.xyz", a.Vfsxyz, a.Vftw);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01110 && a.Oc0 == Ma.M111100) { //VDIV
                e = new EEis("VDIV", "Q", a.Vfsfsf, a.Vftftf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M111100) { //VFTOI0.
                e = new EEis("VFTOI0." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M111101) { //VFTOI4.
                e = new EEis("VFTOI4." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M111110) { //VFTOI12.
                e = new EEis("VFTOI12." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00101 && a.Oc0 == Ma.M111111) { //VFTOI15.
                e = new EEis("VFTOI15." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M110000) { //VIADD
                e = new EEis("VIADD", a.Vidr, a.Visr, a.Vitr);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M110010) { //VIADDI
                e = new EEis("VIADDI", a.Vitr, a.Visr, a.Vimm5);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M110000) { //VIAND
                e = new EEis("VIAND", a.Vidr, a.Visr, a.Vitr);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01111 && a.Oc0 == Ma.M111110) { //VILWR.
                e = new EEis("VILWR." + a.Vd, a.Vitr, a.Visd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M110101) { //VIOR
                e = new EEis("VIOR", a.Vidr, a.Visr, a.Vitr);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M110101) { //VISUB
                e = new EEis("VISUB", a.Vidr, a.Visr, a.Vitr);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01111 && a.Oc0 == Ma.M111111) { //VISWR.
                e = new EEis("VISWR." + a.Vd, a.Vitr, a.Visd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M111100) { //VITOF0.
                e = new EEis("VITOF0." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M111101) { //VITOF4.
                e = new EEis("VITOF4." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M111110) { //VITOF12.
                e = new EEis("VITOF12." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00100 && a.Oc0 == Ma.M111111) { //VITOF15.
                e = new EEis("VITOF15." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01101 && a.Oc0 == Ma.M111110) { //VLQD.
                e = new EEis("VLQD." + a.Vd, a.Vftd, a.Vmmisd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01101 && a.Oc0 == Ma.M111100) { //VLQI.
                e = new EEis("VLQI." + a.Vd, a.Vftd, a.Visppd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101001) { //VMADD.
                e = new EEis("VMADD." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100011) { //VMADDi.
                e = new EEis("VMADDi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100001) { //VMADDq.
                e = new EEis("VMADDq." + a.Vd, a.Vfdd, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0010) { //VMADDbc.
                e = new EEis("VMADD" + a.Vbc + "." + a.Vd, a.Vfd, a.Vfs, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M111101) { //VMADDA.
                e = new EEis("VMADDA." + a.Vd, a.Vacc, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111111) { //VMADDAi.
                e = new EEis("VMADDAi." + a.Vd, a.Vacc, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111101) { //VMADDAq.
                e = new EEis("VMADDAq." + a.Vd, a.Vacc, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M00010 && a.Oc2c4 == Ma.M1111) { //VMADDAbc.
                e = new EEis("VMADDA" + a.Vbc + "." + a.Vd, a.Vacc, a.Vfs, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101011) { //VMAX.
                e = new EEis("VMAX." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M011101) { //VMAXi.
                e = new EEis("VMAXi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0100) { //VMAXbc.
                e = new EEis("VMAX" + a.Vbc + "." + a.Vd, a.Vfdd, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01111 && a.Oc0 == Ma.M111101) { //VMFIR.
                e = new EEis("VMFIR." + a.Vd, a.Vftd, a.Visr);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101111) { //VMINI.
                e = new EEis("VMINI." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M011111) { //VMINIi.
                e = new EEis("VMINIi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0101) { //VMINIbc.
                e = new EEis("VMINI" + a.Vbc + "." + a.Vd, a.Vfdd, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01100 && a.Oc0 == Ma.M111100) { //VMOVE.
                e = new EEis("VMOVE." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01100 && a.Oc0 == Ma.M111101) { //VMR32.
                e = new EEis("VMR32." + a.Vd, a.Vftd, a.Vfsd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101101) { //VMSUB.
                e = new EEis("VMSUB." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M001111) { //VMSUBi.
                e = new EEis("VMSUBi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100101) { //VMSUBq.
                e = new EEis("VMSUBq." + a.Vd, a.Vfdd, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M00010 && a.Oc2c4 == Ma.M1111) { //VMSUBbc.
                e = new EEis("VMSUB" + a.Vbc + "." + a.Vd, a.Vfdd, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01011 && a.Oc0 == Ma.M111101) { //VMSUBA.
                e = new EEis("VMSUBA." + a.Vd, a.Vacc, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01001 && a.Oc0 == Ma.M111111) { //VMSUBAi.
                e = new EEis("VMSUBAi." + a.Vd, a.Vacc, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01001 && a.Oc0 == Ma.M111101) { //VMSUBAq.
                e = new EEis("VMSUBAq." + a.Vd, a.Vacc, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M00011 && a.Oc2c4 == Ma.M1111) { //VMSUBAbc.
                e = new EEis("VMSUBA" + a.Vbc + "." + a.Vd, a.Vacc, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01111 && a.Oc0 == Ma.M111100) { //VMTIR.
                e = new EEis("VMTIR." + a.Vd, a.Vitr, a.Vfsfsf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101010) { //VMUL.
                e = new EEis("VMUL." + a.Vd, a.Vfd, a.Vfs, a.Vft);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M011110) { //VMULi.
                e = new EEis("VMULi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M011100) { //VMULq.
                e = new EEis("VMULq." + a.Vd, a.Vfdd, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0110) { //VMULbc.
                e = new EEis("VMUL" + a.Vbc + "." + a.Vd, a.Vfd, a.Vfs, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01010 && a.Oc0 == Ma.M111110) { //VMULA.
                e = new EEis("VMULA." + a.Vd, a.Vacc, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M111110) { //VMULAi.
                e = new EEis("VMULAi." + a.Vd, a.Vacc, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00111 && a.Oc0 == Ma.M111100) { //VMULAq.
                e = new EEis("VMULAq." + a.Vd, a.Vacc, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M00110 && a.Oc2c4 == Ma.M1111) { //VMULAbc.
                e = new EEis("VMULA" + a.Vbc + "." + a.Vd, a.Vacc, a.Vfs, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01011 && a.Oc0 == Ma.M111111) { //VNOP
                e = new EEis("VNOP");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M01011 && a.Oc0 == Ma.M111110) { //VOPMULA.
                e = new EEis("VOPMULA.xyz", a.Vaccxyz, a.Vfsxyz, a.Vftxyz);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101110) { //VOPMSUB.
                e = new EEis("VOPMSUB.xyz", a.Vfdxyz, a.Vfsxyz, a.Vftxyz);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M10000 && a.Oc0 == Ma.M111101) { //VRGET.
                e = new EEis("VRGET." + a.Vd, a.Vftd, "R");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M10000 && a.Oc0 == Ma.M111110) { //VRINIT
                e = new EEis("VRINIT", "R", a.Vfsfsf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M10000 && a.Oc0 == Ma.M111100) { //VRNEXT
                e = new EEis("VRNEXT." + a.Vd, a.Vftd, "R");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M01110 && a.Oc0 == Ma.M111110) { //VRSQRT
                e = new EEis("VRSQRT", "Q", a.Vfsfsf, a.Vftftf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M10000 && a.Oc0 == Ma.M111111) { //VRXOR
                e = new EEis("VRXOR", "R", a.Vfsfsf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M01101 && a.Oc0 == Ma.M111111) { //VRSQD
                e = new EEis("VRSQD", "R", a.Vmmitd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M01101 && a.Oc0 == Ma.M111111) { //VSQI
                e = new EEis("VSQI." + a.Vd, a.Vfsd, a.Vitppd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6c5 == Ma.M01110 && a.Oc0 == Ma.M111101) { //VSQRT
                e = new EEis("VSQRT", "Q", a.Vftftf);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M101100) { //VSUB.
                e = new EEis("VSUB." + a.Vd, a.Vfdd, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100110) { //VSUBi.
                e = new EEis("VSUBi." + a.Vd, a.Vfdd, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc0 == Ma.M100000) { //VSUBq.
                e = new EEis("VSUBq." + a.Vd, a.Vfdd, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc2c4 == Ma.M0001) { //VSUBbc.
                e = new EEis("VSUB" + a.Vbc + "." + a.Vd, a.Vfdd, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01011 && a.Oc0 == Ma.M111100) { //VSUBA.
                e = new EEis("VSUBA." + a.Vd, a.Vacc, a.Vfsd, a.Vftd);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111110) { //VSUBAi.
                e = new EEis("VSUBAi." + a.Vd, a.Vacc, a.Vfsd, "I");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01000 && a.Oc0 == Ma.M111100) { //VSUBAq.
                e = new EEis("VSUBAq." + a.Vd, a.Vacc, a.Vfsd, "Q");
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M00001 && a.Oc2c4 == Ma.M1111) { //VSUBAbc.
                e = new EEis("VSUBA" + a.Vbc + "." + a.Vd, a.Vacc, a.Vfsd, a.Vftbc);
            }
            else if (a.Oc26 == Ma.M010010 && a.Oc25 == 1 && a.Oc6 == Ma.M01110 && a.Oc0 == Ma.M111111) { //VWAITQ
                e = new EEis("VWAITQ");
            }
            // TODO work HERE
            // ...
            // ...
            // 
            else {
                e = EUnk;
            }
            return e;
        }

        public static readonly EEis EUnk = new EEis("?");

        struct MIPSi {
            uint w;

            public MIPSi(uint w) {
                this.w = w;
            }

            // EE
            public string Trs { get { return GPR32[Ors]; } }
            public string Trt { get { return GPR32[Ort]; } }
            public string Trd { get { return GPR32[Ord]; } }
            public string Tsa { get { return Osa.ToString(); } }
            public string Tsa32 { get { return (Osa).ToString(); } }
            public string Thint { get { return (Ort).ToString(); } }
            public string Timm16s { get { return "$" + ((short)Oimm16s).ToString("x4"); } }
            public string Timm16u { get { return "$" + ((ushort)Oimm16u).ToString("x4"); } }
            public string Tft { get { return "$f" + Oft; } }
            public string Tfs { get { return "$f" + Ofs; } }
            public string Tfd { get { return "$f" + Ofd; } }

            public int Oc26 { get { return (int)((w >> 26) & 0x3F); } }
            public int Oc25 { get { return (int)((w >> 25) & 0x01); } }
            public int Oc21 { get { return (int)((w >> 21) & 0x1F); } }
            public int Oc16 { get { return (int)((w >> 16) & 0x1F); } }
            public int Oc6 { get { return (int)((w >> 6) & 0x1F); } }
            public int Oc0 { get { return (int)((w) & 0x3F); } }
            public int Oc2c4 { get { return (int)((w >> 2) & 0x0F); } }
            public int Oc6c5 { get { return (int)((w >> 6) & 0x1F); } }
            public int Ors { get { return (int)((w >> 21) & 0x1F); } }
            public int Ort { get { return (int)((w >> 16) & 0x1F); } }
            public int Ord { get { return (int)((w >> 11) & 0x1F); } }
            public int Osa { get { return (int)((w >> 6) & 0x1F); } }
            public int Ostype { get { return (int)((w >> 6) & 0x1F); } }
            public int Ocode6 { get { return (int)((w >> 6) & 0xFFFFF); } }
            public uint Oc32 { get { return w; } }
            public int Oft { get { return (int)((w >> 16) & 0x1F); } }
            public int Ofs { get { return (int)((w >> 11) & 0x1F); } }
            public int Ofd { get { return (int)((w >> 6) & 0x1F); } }
            public int Oi { get { return (int)(w & 1); } }

            public int Oimm16u {
                get { return (ushort)((w >> 0) & 0xFFFF); }
            }
            public int Oimm16s {
                get { return (short)((w >> 0) & 0xFFFF); }
            }
            public int Ooff16 {
                get { return (short)(w & 0xFFFF); }
            }
            public int Ooff26 {
                get { return (int)(w & 0x3FFFFFF); }
            }

            public static string[] GPR32 {
                get { return "zero:at:v0:v1:a0:a1:a2:a3:t0:t1:t2:t3:t4:t5:t6:t7:s0:s1:s2:s3:s4:s5:s6:s7:t8:t9:k0:k1:gp:sp:s8:ra".Split(':'); }
            }
            public string Toff16(uint pc) {
                return "$" + (pc + 4 + Ooff16 * 4).ToString("x8");
            }
            public string Toff26(uint pc) {
                return "$" + ((pc & 0xF0000000) | (uint)Ooff26 * 4).ToString("x8");
            }
            public string Tcode6 { get { return "$" + Ocode6.ToString("x5"); } }
            public string Toffbrs { get { return "$" + ((ushort)Oimm16s).ToString("x4") + "(" + Trs + ")"; } }

            public int Oid2 { get { return (int)((w >> 11) & 0x1F); } }
            public string Tid2 { get { return "$vi" + Oid2; } }
            public int Ofd2 { get { return (int)((w >> 11) & 0x1F); } }
            public string Tfd2 { get { return "vf" + Ofd2; } }
            public int Oft2 { get { return (int)((w >> 16) & 0x1F); } }
            public string Tft2 { get { return "vf" + Oft2; } }

            //public int Ort2 { get { return (int)((w >> 16) & 0x1F); } }
            //public string Trt2 { get { return "vf" + Ort2; } }

            // COP2(VU0)

            /// <summary>
            /// dest M21,4
            /// </summary>
            public string Vd {
                get {
                    return ""
                        + ((0 != (w & 0x01000000)) ? "x" : "")
                        + ((0 != (w & 0x00800000)) ? "y" : "")
                        + ((0 != (w & 0x00400000)) ? "z" : "")
                        + ((0 != (w & 0x00200000)) ? "w" : "")
                    ;
                }
            }

            public int Ovft { get { return (int)((w >> 16) & 0x1F); } }
            /// <summary>
            /// ftdest M16,5
            /// </summary>
            public string Vft { get { return "vf" + Ovft; } }
            public string Vftd { get { return Vft; } }
            public string Vftxyz { get { return Vft + ""; } }
            public string Vftw { get { return Vft + "w"; } }

            public int Ovfs { get { return (int)((w >> 11) & 0x1F); } }
            /// <summary>
            /// fsdest M11,5
            /// </summary>
            public string Vfs { get { return "vf" + Ovfs; } }
            public string Vfsd { get { return Vfs; } }

            public int Ovfd { get { return (int)((w >> 6) & 0x1F); } }
            /// <summary>
            /// fddest M6,5
            /// </summary>
            public string Vfd { get { return "vf" + Ovfd; } }
            public string Vfdd { get { return Vfd; } }
            public string Vfdxyz { get { return Vfd; } }

            public int Ovbc { get { return (int)((w) & 3); } }
            /// <summary>
            /// bc M0,2
            /// </summary>
            public string Vbc { get { return "" + "xyzw"[Ovbc]; } }

            public string Vacc { get { return "ACC"; } }
            public string Vaccxyz { get { return "ACC"; } }

            public int Ovimm15 { get { return (int)((w >> 6) & 0x7FFF); } }
            /// <summary>
            /// Imm15 M6,15
            /// </summary>
            public string Vimm15 { get { return "$" + Ovimm15.ToString("x4"); } }

            public int Ovimm5 { get { return (int)((w >> 6) & 0x1F); } }
            /// <summary>
            /// Imm5 M6,5
            /// </summary>
            public string Vimm5 { get { return "$" + Ovimm5.ToString("x4"); } }

            public string Vfsxyz { get { return "vf" + Ovfs + ""; } }
            public string Vfsf { get { return "" + "xyzw"[(int)((w >> 21) & 3)]; } }
            public string Vfsfsf { get { return "vf" + Ovfs + Vfsf; } }
            public string Vftf { get { return "" + "xyzw"[(int)((w >> 23) & 3)]; } }
            public string Vftftf { get { return "vf" + Ovft + Vftf; } }
            public string Vftbc { get { return "vf" + Ovft + Vbc; } }

            public int Ovidr { get { return (int)((w >> 6) & 0x1F); } }
            /// <summary>
            /// idreg M6,5
            /// </summary>
            public string Vidr { get { return "$vi" + Ovidr; } }

            public int Ovisr { get { return (int)((w >> 11) & 0x1F); } }
            /// <summary>
            /// isreg M11,5
            /// </summary>
            public string Visr { get { return "$vi" + Ovisr; } }
            public string Visd { get { return "$vi" + Ovisr + Vd; } }
            public string Vmmisd { get { return "(--$vi" + Ovisr + ")" + Vd; } }
            public string Visppd { get { return "($vi" + Ovisr + "++)" + Vd; } }

            public int Ovitr { get { return (int)((w >> 16) & 0x1F); } }
            /// <summary>
            /// itreg M16,5
            /// </summary>
            public string Vitr { get { return "$vi" + Ovitr; } }
            public string Vmmitd { get { return "(--$vi" + Ovitr + ")" + Vd; } }
            public string Vitppd { get { return "($vi" + Ovitr + "++)" + Vd; } }
        }

        class Ma {
            public const int M0000 = 0;
            public const int M0001 = 1;
            public const int M0010 = 2;
            public const int M0011 = 3;
            public const int M0100 = 4;
            public const int M0101 = 5;
            public const int M0110 = 6;
            public const int M0111 = 7;
            public const int M1000 = 8;
            public const int M1001 = 9;
            public const int M1010 = 10;
            public const int M1011 = 11;
            public const int M1100 = 12;
            public const int M1101 = 13;
            public const int M1110 = 14;
            public const int M1111 = 15;

            public const int M00000 = 0;
            public const int M00001 = 1;
            public const int M00010 = 2;
            public const int M00011 = 3;
            public const int M00100 = 4;
            public const int M00101 = 5;
            public const int M00110 = 6;
            public const int M00111 = 7;
            public const int M01000 = 8;
            public const int M01001 = 9;
            public const int M01010 = 10;
            public const int M01011 = 11;
            public const int M01100 = 12;
            public const int M01101 = 13;
            public const int M01110 = 14;
            public const int M01111 = 15;
            public const int M10000 = 16;
            public const int M10001 = 17;
            public const int M10010 = 18;
            public const int M10011 = 19;
            public const int M10100 = 20;
            public const int M10101 = 21;
            public const int M10110 = 22;
            public const int M10111 = 23;
            public const int M11000 = 24;
            public const int M11001 = 25;
            public const int M11010 = 26;
            public const int M11011 = 27;
            public const int M11100 = 28;
            public const int M11101 = 29;
            public const int M11110 = 30;
            public const int M11111 = 31;

            public const int M000000 = 0;
            public const int M000001 = 1;
            public const int M000010 = 2;
            public const int M000011 = 3;
            public const int M000100 = 4;
            public const int M000101 = 5;
            public const int M000110 = 6;
            public const int M000111 = 7;
            public const int M001000 = 8;
            public const int M001001 = 9;
            public const int M001010 = 10;
            public const int M001011 = 11;
            public const int M001100 = 12;
            public const int M001101 = 13;
            public const int M001110 = 14;
            public const int M001111 = 15;
            public const int M010000 = 16;
            public const int M010001 = 17;
            public const int M010010 = 18;
            public const int M010011 = 19;
            public const int M010100 = 20;
            public const int M010101 = 21;
            public const int M010110 = 22;
            public const int M010111 = 23;
            public const int M011000 = 24;
            public const int M011001 = 25;
            public const int M011010 = 26;
            public const int M011011 = 27;
            public const int M011100 = 28;
            public const int M011101 = 29;
            public const int M011110 = 30;
            public const int M011111 = 31;
            public const int M100000 = 32;
            public const int M100001 = 33;
            public const int M100010 = 34;
            public const int M100011 = 35;
            public const int M100100 = 36;
            public const int M100101 = 37;
            public const int M100110 = 38;
            public const int M100111 = 39;
            public const int M101000 = 40;
            public const int M101001 = 41;
            public const int M101010 = 42;
            public const int M101011 = 43;
            public const int M101100 = 44;
            public const int M101101 = 45;
            public const int M101110 = 46;
            public const int M101111 = 47;
            public const int M110000 = 48;
            public const int M110001 = 49;
            public const int M110010 = 50;
            public const int M110011 = 51;
            public const int M110100 = 52;
            public const int M110101 = 53;
            public const int M110110 = 54;
            public const int M110111 = 55;
            public const int M111000 = 56;
            public const int M111001 = 57;
            public const int M111010 = 58;
            public const int M111011 = 59;
            public const int M111100 = 60;
            public const int M111101 = 61;
            public const int M111110 = 62;
            public const int M111111 = 63;
        }
    }
}
