#define KH2DED

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ee1Dec.C {
    public partial class Mobrc3 {
        public Mobrc3(CustEE eeref, Stateee ees, IExecEE exeee) {
            this.eeref = eeref;
            this.ees = ees;
            this.exeee = exeee;
        }

        CustEE eeref;
        Stateee ees;
        IExecEE exeee;
        int walkPos = 0;

        public void Exec2(string dir) {
            Array.Copy(eeref.ram, ee.ram, 32 * 1024 * 1024);

            initstate();

            for (int t = 0; t < 32; t++) ee.fpr[t].f = 0;

            ee.at.UD0 = 0U;
            ee.v0.UD0 = 0U;
            ee.v1.UD0 = 0U;
            ee.a0.UD0 = 0x9F8510U; // mset +0x00
            ee.a1.UD0 = 0x1ABB590U; // info tbl
            ee.a2.UD0 = 0U;
            ee.a3.UD0 = 0U;
            ee.t0.UD0 = 0U;
            ee.t1.UD0 = 0U;
            ee.t2.UD0 = 0U;
            ee.t3.UD0 = 0U;
            ee.t4.UD0 = 0U;
            ee.t5.UD0 = 0U;
            ee.t6.UD0 = 0U;
            ee.t7.UD0 = 0U;
            ee.s0.UD0 = 0x9F85A0U; // mset +0x90
            ee.s1.UD0 = 0U; //0x9F8510
            ee.s2.UD0 = 0U; //0x38EC70
            ee.s3.UD0 = 0U; //0x38ED70
            ee.s4.UD0 = 0x1ABB0B0U; // temp?
            ee.s5.UD0 = 0U;
            ee.s6.UD0 = 0U;
            ee.s7.UD0 = 0U;
            ee.t8.UD0 = 0U;
            ee.t9.UD0 = 0U;
            ee.k0.UD0 = 0U;
            ee.k1.UD0 = 0U;
            ee.gp.UD0 = 0U;
            ee.sp.UD0 = 0x2000000U;
            ee.s8.UD0 = 0U;
            ee.ra.UD0 = 0xFFFFFFFFU;

            ee.pc = 0x128260;
            while (ee.pc != 0xFFFFFFFFU) {
                if (dicti2a.ContainsKey(ee.pc)) {
                    dicti2a[ee.pc]();
                }
                else throw new RecfnnotFound(ee.pc, "rc3");
            }

            if (true) {
                int t = 0;
                byte[] binIn = File.ReadAllBytes(Path.Combine(dir, "tSxyz.bin"));
                for (; t < 16 * 0xE5 && ee.ram[0x1ABB6D0 + t] == binIn[t]; t++) ;
                Debug.Assert(t == 16 * 0xE5, "s.pos: " + t + " ‚ " + (16 * 0xE5));
            }
            if (true) {
                int t = 0;
                byte[] binIn = File.ReadAllBytes(Path.Combine(dir, "tRxyz.bin"));
                for (; t < 16 * 0xE5 && ee.ram[0x1ABC520 + t] == binIn[t]; t++) ;
                Debug.Assert(t == 16 * 0xE5, "r.pos: " + t + " ‚ " + (16 * 0xE5));
            }
            if (true) {
                int t = 0;
                byte[] binIn = File.ReadAllBytes(Path.Combine(dir, "tTxyz.bin"));
                for (; t < 16 * 0xE5 && ee.ram[0x1ABD370 + t] == binIn[t]; t++) ;
                Debug.Assert(t == 16 * 0xE5, "t.pos: " + t + " ‚ " + (16 * 0xE5));
            }
            MessageBox.Show("Passed.");
        }

        public void Exec(string dir) {
            Array.Copy(eeref.ram, ee.ram, 32 * 1024 * 1024);

            initstate();

            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABB6D0 + c] = 0xE5;
            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABC520 + c] = 0xE5;
            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABD370 + c] = 0xE5;

            ee.ra.UD0 = 0xFFFFFFFFU;

            ee.pc = 0x128260;
            while (ee.pc != 0xFFFFFFFFU) {
                if (dicti2a.ContainsKey(ee.pc)) {
                    dicti2a[ee.pc]();
                }
                else throw new RecfnnotFound(ee.pc, "rc3");
            }

            StringWriter twr = new StringWriter();
            Compareby6.Run(
                File.ReadAllBytes(Path.Combine(dir, "tSxyz.bin")), new MemoryStream(ee.ram, 0x1ABB6D0, 16 * 0xE5, false).ToArray(),
                File.ReadAllBytes(Path.Combine(dir, "tRxyz.bin")), new MemoryStream(ee.ram, 0x1ABC520, 16 * 0xE5, false).ToArray(),
                File.ReadAllBytes(Path.Combine(dir, "tTxyz.bin")), new MemoryStream(ee.ram, 0x1ABD370, 16 * 0xE5, false).ToArray(),
                twr
                );
            Debug.WriteLine(twr.ToString());
            //using (FileStream fs = File.Create(Path.Combine(dir, "tSxyz.bin"))) { fs.Write(ee.ram, 0x1ABB6D0, 16 * 0xE5); }
            //using (FileStream fs = File.Create(Path.Combine(dir, "tRxyz.bin"))) { fs.Write(ee.ram, 0x1ABC520, 16 * 0xE5); }
            //using (FileStream fs = File.Create(Path.Combine(dir, "tTxyz.bin"))) { fs.Write(ee.ram, 0x1ABD370, 16 * 0xE5); }
        }

        void here(uint pc) { }

        void here2(uint pc) {
            #region Compare many many many
            if (pc != ees.pc) throw new TraceDiffException("pc", pc, ees.pc);
            if (ee.at.UD0 != eeref.at.UD0 || ee.at.UD1 != eeref.at.UD1) throw new TraceDiffException("at", ee.at, eeref.at);
            if (ee.v0.UD0 != eeref.v0.UD0 || ee.v0.UD1 != eeref.v0.UD1) throw new TraceDiffException("v0", ee.v0, eeref.v0);
            if (ee.v1.UD0 != eeref.v1.UD0 || ee.v1.UD1 != eeref.v1.UD1) throw new TraceDiffException("v1", ee.v1, eeref.v1);
            if (ee.a0.UD0 != eeref.a0.UD0 || ee.a0.UD1 != eeref.a0.UD1) throw new TraceDiffException("a0", ee.a0, eeref.a0);
            if (ee.a1.UD0 != eeref.a1.UD0 || ee.a1.UD1 != eeref.a1.UD1) throw new TraceDiffException("a1", ee.a1, eeref.a1);
            if (ee.a2.UD0 != eeref.a2.UD0 || ee.a2.UD1 != eeref.a2.UD1) throw new TraceDiffException("a2", ee.a2, eeref.a2);
            if (ee.a3.UD0 != eeref.a3.UD0 || ee.a3.UD1 != eeref.a3.UD1) throw new TraceDiffException("a3", ee.a3, eeref.a3);
            if (ee.t0.UD0 != eeref.t0.UD0 || ee.t0.UD1 != eeref.t0.UD1) throw new TraceDiffException("t0", ee.t0, eeref.t0);
            if (ee.t1.UD0 != eeref.t1.UD0 || ee.t1.UD1 != eeref.t1.UD1) throw new TraceDiffException("t1", ee.t1, eeref.t1);
            if (ee.t2.UD0 != eeref.t2.UD0 || ee.t2.UD1 != eeref.t2.UD1) throw new TraceDiffException("t2", ee.t2, eeref.t2);
            if (ee.t3.UD0 != eeref.t3.UD0 || ee.t3.UD1 != eeref.t3.UD1) throw new TraceDiffException("t3", ee.t3, eeref.t3);
            if (ee.t4.UD0 != eeref.t4.UD0 || ee.t4.UD1 != eeref.t4.UD1) throw new TraceDiffException("t4", ee.t4, eeref.t4);
            if (ee.t5.UD0 != eeref.t5.UD0 || ee.t5.UD1 != eeref.t5.UD1) throw new TraceDiffException("t5", ee.t5, eeref.t5);
            if (ee.t6.UD0 != eeref.t6.UD0 || ee.t6.UD1 != eeref.t6.UD1) throw new TraceDiffException("t6", ee.t6, eeref.t6);
            if (ee.t7.UD0 != eeref.t7.UD0 || ee.t7.UD1 != eeref.t7.UD1) throw new TraceDiffException("t7", ee.t7, eeref.t7);
            if (ee.s0.UD0 != eeref.s0.UD0 || ee.s0.UD1 != eeref.s0.UD1) throw new TraceDiffException("s0", ee.s0, eeref.s0);
            if (ee.s1.UD0 != eeref.s1.UD0 || ee.s1.UD1 != eeref.s1.UD1) throw new TraceDiffException("s1", ee.s1, eeref.s1);
            if (ee.s2.UD0 != eeref.s2.UD0 || ee.s2.UD1 != eeref.s2.UD1) throw new TraceDiffException("s2", ee.s2, eeref.s2);
            if (ee.s3.UD0 != eeref.s3.UD0 || ee.s3.UD1 != eeref.s3.UD1) throw new TraceDiffException("s3", ee.s3, eeref.s3);
            if (ee.s4.UD0 != eeref.s4.UD0 || ee.s4.UD1 != eeref.s4.UD1) throw new TraceDiffException("s4", ee.s4, eeref.s4);
            if (ee.s5.UD0 != eeref.s5.UD0 || ee.s5.UD1 != eeref.s5.UD1) throw new TraceDiffException("s5", ee.s5, eeref.s5);
            if (ee.s6.UD0 != eeref.s6.UD0 || ee.s6.UD1 != eeref.s6.UD1) throw new TraceDiffException("s6", ee.s6, eeref.s6);
            if (ee.s7.UD0 != eeref.s7.UD0 || ee.s7.UD1 != eeref.s7.UD1) throw new TraceDiffException("s7", ee.s7, eeref.s7);
            if (ee.t8.UD0 != eeref.t8.UD0 || ee.t8.UD1 != eeref.t8.UD1) throw new TraceDiffException("t8", ee.t8, eeref.t8);
            if (ee.t9.UD0 != eeref.t9.UD0 || ee.t9.UD1 != eeref.t9.UD1) throw new TraceDiffException("t9", ee.t9, eeref.t9);
            if (ee.k0.UD0 != eeref.k0.UD0 || ee.k0.UD1 != eeref.k0.UD1) throw new TraceDiffException("k0", ee.k0, eeref.k0);
            if (ee.k1.UD0 != eeref.k1.UD0 || ee.k1.UD1 != eeref.k1.UD1) throw new TraceDiffException("k1", ee.k1, eeref.k1);
            if (ee.gp.UD0 != eeref.gp.UD0 || ee.gp.UD1 != eeref.gp.UD1) throw new TraceDiffException("gp", ee.gp, eeref.gp);
            if (ee.sp.UD0 != eeref.sp.UD0 || ee.sp.UD1 != eeref.sp.UD1) throw new TraceDiffException("sp", ee.sp, eeref.sp);
            if (ee.s8.UD0 != eeref.s8.UD0 || ee.s8.UD1 != eeref.s8.UD1) throw new TraceDiffException("s8", ee.s8, eeref.s8);
            if (ee.ra.UD0 != eeref.ra.UD0 || ee.ra.UD1 != eeref.ra.UD1) throw new TraceDiffException("ra", ee.ra, eeref.ra);

            if (ee.fpr[0].UL != eeref.fpr[0].UL) throw new TraceDiffException("$f0", (ee.fpr[0].UL), (eeref.fpr[0].UL));
            if (ee.fpr[1].UL != eeref.fpr[1].UL) throw new TraceDiffException("$f1", (ee.fpr[1].UL), (eeref.fpr[1].UL));
            if (ee.fpr[2].UL != eeref.fpr[2].UL) throw new TraceDiffException("$f2", (ee.fpr[2].UL), (eeref.fpr[2].UL));
            if (ee.fpr[3].UL != eeref.fpr[3].UL) throw new TraceDiffException("$f3", (ee.fpr[3].UL), (eeref.fpr[3].UL));
            if (ee.fpr[4].UL != eeref.fpr[4].UL) throw new TraceDiffException("$f4", (ee.fpr[4].UL), (eeref.fpr[4].UL));
            if (ee.fpr[5].UL != eeref.fpr[5].UL) throw new TraceDiffException("$f5", (ee.fpr[5].UL), (eeref.fpr[5].UL));
            if (ee.fpr[6].UL != eeref.fpr[6].UL) throw new TraceDiffException("$f6", (ee.fpr[6].UL), (eeref.fpr[6].UL));
            if (ee.fpr[7].UL != eeref.fpr[7].UL) throw new TraceDiffException("$f7", (ee.fpr[7].UL), (eeref.fpr[7].UL));
            if (ee.fpr[8].UL != eeref.fpr[8].UL) throw new TraceDiffException("$f8", (ee.fpr[8].UL), (eeref.fpr[8].UL));
            if (ee.fpr[9].UL != eeref.fpr[9].UL) throw new TraceDiffException("$f9", (ee.fpr[9].UL), (eeref.fpr[9].UL));
            if (ee.fpr[10].UL != eeref.fpr[10].UL) throw new TraceDiffException("$f10", (ee.fpr[10].UL), (eeref.fpr[10].UL));
            if (ee.fpr[11].UL != eeref.fpr[11].UL) throw new TraceDiffException("$f11", (ee.fpr[11].UL), (eeref.fpr[11].UL));
            if (ee.fpr[12].UL != eeref.fpr[12].UL) throw new TraceDiffException("$f12", (ee.fpr[12].UL), (eeref.fpr[12].UL));
            if (ee.fpr[13].UL != eeref.fpr[13].UL) throw new TraceDiffException("$f13", (ee.fpr[13].UL), (eeref.fpr[13].UL));
            if (ee.fpr[14].UL != eeref.fpr[14].UL) throw new TraceDiffException("$f14", (ee.fpr[14].UL), (eeref.fpr[14].UL));
            if (ee.fpr[15].UL != eeref.fpr[15].UL) throw new TraceDiffException("$f15", (ee.fpr[15].UL), (eeref.fpr[15].UL));
            if (ee.fpr[16].UL != eeref.fpr[16].UL) throw new TraceDiffException("$f16", (ee.fpr[16].UL), (eeref.fpr[16].UL));
            if (ee.fpr[17].UL != eeref.fpr[17].UL) throw new TraceDiffException("$f17", (ee.fpr[17].UL), (eeref.fpr[17].UL));
            if (ee.fpr[18].UL != eeref.fpr[18].UL) throw new TraceDiffException("$f18", (ee.fpr[18].UL), (eeref.fpr[18].UL));
            if (ee.fpr[19].UL != eeref.fpr[19].UL) throw new TraceDiffException("$f19", (ee.fpr[19].UL), (eeref.fpr[19].UL));
            if (ee.fpr[20].UL != eeref.fpr[20].UL) throw new TraceDiffException("$f20", (ee.fpr[20].UL), (eeref.fpr[20].UL));
            if (ee.fpr[21].UL != eeref.fpr[21].UL) throw new TraceDiffException("$f21", (ee.fpr[21].UL), (eeref.fpr[21].UL));
            if (ee.fpr[22].UL != eeref.fpr[22].UL) throw new TraceDiffException("$f22", (ee.fpr[22].UL), (eeref.fpr[22].UL));
            if (ee.fpr[23].UL != eeref.fpr[23].UL) throw new TraceDiffException("$f23", (ee.fpr[23].UL), (eeref.fpr[23].UL));
            if (ee.fpr[24].UL != eeref.fpr[24].UL) throw new TraceDiffException("$f24", (ee.fpr[24].UL), (eeref.fpr[24].UL));
            if (ee.fpr[25].UL != eeref.fpr[25].UL) throw new TraceDiffException("$f25", (ee.fpr[25].UL), (eeref.fpr[25].UL));
            if (ee.fpr[26].UL != eeref.fpr[26].UL) throw new TraceDiffException("$f26", (ee.fpr[26].UL), (eeref.fpr[26].UL));
            if (ee.fpr[27].UL != eeref.fpr[27].UL) throw new TraceDiffException("$f27", (ee.fpr[27].UL), (eeref.fpr[27].UL));
            if (ee.fpr[28].UL != eeref.fpr[28].UL) throw new TraceDiffException("$f28", (ee.fpr[28].UL), (eeref.fpr[28].UL));
            if (ee.fpr[29].UL != eeref.fpr[29].UL) throw new TraceDiffException("$f29", (ee.fpr[29].UL), (eeref.fpr[29].UL));
            if (ee.fpr[30].UL != eeref.fpr[30].UL) throw new TraceDiffException("$f30", (ee.fpr[30].UL), (eeref.fpr[30].UL));
            if (ee.fpr[31].UL != eeref.fpr[31].UL) throw new TraceDiffException("$f31", (ee.fpr[31].UL), (eeref.fpr[31].UL));
            if (ee.fpracc.UL != eeref.fpracc.UL) throw new TraceDiffException("acc", (ee.fpracc.UL), (eeref.fpracc.UL));

            if (ee.VF[1].CompareTo(eeref.VF[1]) != 0) throw new TraceDiffException("VF1", ee.VF[1], eeref.VF[1]);
            if (ee.VF[2].CompareTo(eeref.VF[2]) != 0) throw new TraceDiffException("VF2", ee.VF[2], eeref.VF[2]);
            if (ee.VF[3].CompareTo(eeref.VF[3]) != 0) throw new TraceDiffException("VF3", ee.VF[3], eeref.VF[3]);
            if (ee.VF[4].CompareTo(eeref.VF[4]) != 0) throw new TraceDiffException("VF4", ee.VF[4], eeref.VF[4]);
            if (ee.VF[5].CompareTo(eeref.VF[5]) != 0) throw new TraceDiffException("VF5", ee.VF[5], eeref.VF[5]);
            if (ee.VF[6].CompareTo(eeref.VF[6]) != 0) throw new TraceDiffException("VF6", ee.VF[6], eeref.VF[6]);
            if (ee.VF[7].CompareTo(eeref.VF[7]) != 0) throw new TraceDiffException("VF7", ee.VF[7], eeref.VF[7]);
            if (ee.VF[8].CompareTo(eeref.VF[8]) != 0) throw new TraceDiffException("VF8", ee.VF[8], eeref.VF[8]);
            if (ee.VF[9].CompareTo(eeref.VF[9]) != 0) throw new TraceDiffException("VF9", ee.VF[9], eeref.VF[9]);
            if (ee.VF[10].CompareTo(eeref.VF[10]) != 0) throw new TraceDiffException("VF10", ee.VF[10], eeref.VF[10]);
            if (ee.VF[11].CompareTo(eeref.VF[11]) != 0) throw new TraceDiffException("VF11", ee.VF[11], eeref.VF[11]);
            if (ee.VF[12].CompareTo(eeref.VF[12]) != 0) throw new TraceDiffException("VF12", ee.VF[12], eeref.VF[12]);
            if (ee.VF[13].CompareTo(eeref.VF[13]) != 0) throw new TraceDiffException("VF13", ee.VF[13], eeref.VF[13]);
            if (ee.VF[14].CompareTo(eeref.VF[14]) != 0) throw new TraceDiffException("VF14", ee.VF[14], eeref.VF[14]);
            if (ee.VF[15].CompareTo(eeref.VF[15]) != 0) throw new TraceDiffException("VF15", ee.VF[15], eeref.VF[15]);
            if (ee.VF[16].CompareTo(eeref.VF[16]) != 0) throw new TraceDiffException("VF16", ee.VF[16], eeref.VF[16]);
            if (ee.VF[17].CompareTo(eeref.VF[17]) != 0) throw new TraceDiffException("VF17", ee.VF[17], eeref.VF[17]);
            if (ee.VF[18].CompareTo(eeref.VF[18]) != 0) throw new TraceDiffException("VF18", ee.VF[18], eeref.VF[18]);
            if (ee.VF[19].CompareTo(eeref.VF[19]) != 0) throw new TraceDiffException("VF19", ee.VF[19], eeref.VF[19]);
            if (ee.VF[20].CompareTo(eeref.VF[20]) != 0) throw new TraceDiffException("VF20", ee.VF[20], eeref.VF[20]);
            if (ee.VF[21].CompareTo(eeref.VF[21]) != 0) throw new TraceDiffException("VF21", ee.VF[21], eeref.VF[21]);
            if (ee.VF[22].CompareTo(eeref.VF[22]) != 0) throw new TraceDiffException("VF22", ee.VF[22], eeref.VF[22]);
            if (ee.VF[23].CompareTo(eeref.VF[23]) != 0) throw new TraceDiffException("VF23", ee.VF[23], eeref.VF[23]);
            if (ee.VF[24].CompareTo(eeref.VF[24]) != 0) throw new TraceDiffException("VF24", ee.VF[24], eeref.VF[24]);
            if (ee.VF[25].CompareTo(eeref.VF[25]) != 0) throw new TraceDiffException("VF25", ee.VF[25], eeref.VF[25]);
            if (ee.VF[26].CompareTo(eeref.VF[26]) != 0) throw new TraceDiffException("VF26", ee.VF[26], eeref.VF[26]);
            if (ee.VF[27].CompareTo(eeref.VF[27]) != 0) throw new TraceDiffException("VF27", ee.VF[27], eeref.VF[27]);
            if (ee.VF[28].CompareTo(eeref.VF[28]) != 0) throw new TraceDiffException("VF28", ee.VF[28], eeref.VF[28]);
            if (ee.VF[29].CompareTo(eeref.VF[29]) != 0) throw new TraceDiffException("VF29", ee.VF[29], eeref.VF[29]);
            if (ee.VF[30].CompareTo(eeref.VF[30]) != 0) throw new TraceDiffException("VF30", ee.VF[30], eeref.VF[30]);
            if (ee.VF[31].CompareTo(eeref.VF[31]) != 0) throw new TraceDiffException("VF31", ee.VF[31], eeref.VF[31]);
            if (ee.Vacc.CompareTo(eeref.Vacc) != 0) throw new TraceDiffException("Vacc", ee.Vacc, eeref.Vacc);

            walkPos++;
            if (exeee != null) {
                exeee.Stepee();
            }
            #endregion
        }
    }

    public partial class Mobrc2 {
        public Mobrc2(CustEE eeref, Stateee ees, IExecEE exeee) {
            this.eeref = eeref;
            this.ees = ees;
            this.exeee = exeee;
        }

        CustEE eeref;
        Stateee ees;
        IExecEE exeee;
        int walkPos = 0;

        public void Exec() {
            Array.Copy(eeref.ram, ee.ram, 32 * 1024 * 1024);

            initstate();

            ee.pc = 0x128918;
            while (true) {
                (dicti2a[ee.pc])();
            }
        }

        public void Exec2(string dir) {
            Array.Copy(eeref.ram, ee.ram, 32 * 1024 * 1024);

            initstate();

            for (int t = 0; t < 32; t++) { ee.fpr[t].f = 0; }
            ee.fpracc.f = 0;
            for (int t = 1; t < 32; t++) { ee.VF[t].x = 0; ee.VF[t].y = 0; ee.VF[t].z = 0; ee.VF[t].w = 0; }
            for (int t = 0; t < 32; t++) { ee.VI[t].UL = 0; }
            ee.Vacc.x = ee.Vacc.y = ee.Vacc.z = ee.Vacc.w = 0;
            ee.Vp.f = 0;
            ee.Vq.f = 1;

            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABB6D0 + c] = 0xE5;
            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABC520 + c] = 0xE5;
            for (int c = 0; c < 16 * 0xE5; c++) ee.ram[0x1ABD370 + c] = 0xE5;

            for (int c = 0; c < 65536; c++) ee.ram[c] = 0;

            //ee.r0.UD0 = 0U;
            ee.at.UD0 = 0U;
            ee.v0.UD0 = 0U;
            ee.v1.UD0 = 0U;
            ee.a0.UD0 = 0x9F8510U; // mset +0x00
            ee.a1.UD0 = 0x1ABB590U; // info tbl
            ee.a2.UD0 = 0U;
            ee.a3.UD0 = 0U;
            ee.t0.UD0 = 0U;
            ee.t1.UD0 = 0U;
            ee.t2.UD0 = 0U;
            ee.t3.UD0 = 0U;
            ee.t4.UD0 = 0U;
            ee.t5.UD0 = 0U;
            ee.t6.UD0 = 0U;
            ee.t7.UD0 = 0U;
            ee.s0.UD0 = 0x9F85A0U; // mset +0x90
            ee.s1.UD0 = 0U;
            ee.s2.UD0 = 0U;
            ee.s3.UD0 = 0U;
            ee.s4.UD0 = 0x1ABB0B0U; // temp?
            ee.s5.UD0 = 0U;
            ee.s6.UD0 = 0U;
            ee.s7.UD0 = 0U;
            ee.t8.UD0 = 0U;
            ee.t9.UD0 = 0U;
            ee.k0.UD0 = 0U;
            ee.k1.UD0 = 0U;
            ee.gp.UD0 = 0U;
            ee.sp.UD0 = 0x2000000U;
            ee.s8.UD0 = 0U;
            ee.ra.UD0 = 0xFFFFFFFFU;

            ee.fpr[12].f = 8;

            ee.pc = 0x128918;
            while (ee.pc != 0xFFFFFFFFU) {
                if (dicti2a.ContainsKey(ee.pc)) {
                    dicti2a[ee.pc]();
                }
                else throw new RecfnnotFound(ee.pc, "rc2");
            }

            for (int c = 0; c < 65536; c++) if (ee.ram[c] != 0) { Debug.Fail("ee.ram at: " + c); break; }

            byte[] binSxxx = File.ReadAllBytes(Path.Combine(dir, "Sxyz.bin"));
            byte[] binRxxx = File.ReadAllBytes(Path.Combine(dir, "Rxyz.bin"));
            byte[] binTxxx = File.ReadAllBytes(Path.Combine(dir, "Txyz.bin"));
            StringWriter twr = new StringWriter();
            Compareby6.Run(
                binSxxx, new MemoryStream(ee.ram, 0x1ABB6D0, 16 * 0xE5, false).ToArray(),
                binRxxx, new MemoryStream(ee.ram, 0x1ABC520, 16 * 0xE5, false).ToArray(),
                binTxxx, new MemoryStream(ee.ram, 0x1ABD370, 16 * 0xE5, false).ToArray(),
                twr
                );
            Debug.WriteLine(twr.ToString());

            //using (FileStream fs = File.Create(Path.Combine(dir, "Sxyz.bin"))) { fs.Write(ee.ram, 0x1ABB6D0, 16 * 0xE5); }
            //using (FileStream fs = File.Create(Path.Combine(dir, "Rxyz.bin"))) { fs.Write(ee.ram, 0x1ABC520, 16 * 0xE5); }
            //using (FileStream fs = File.Create(Path.Combine(dir, "Txyz.bin"))) { fs.Write(ee.ram, 0x1ABD370, 16 * 0xE5); }
        }

        void here2(uint pc) {
            if (pc != ees.pc) throw new TraceDiffException("pc", pc, ees.pc);

            walkPos++;
            if (exeee != null) {
                exeee.Stepee();
            }
        }

        void here(uint pc) { }

        void here3(uint pc) {
            #region Compare many many many
            if (pc != ees.pc) throw new TraceDiffException("pc", pc, ees.pc);
            if (ee.at.UD0 != eeref.at.UD0 || ee.at.UD1 != eeref.at.UD1) throw new TraceDiffException("at", ee.at, eeref.at);
            if (ee.v0.UD0 != eeref.v0.UD0 || ee.v0.UD1 != eeref.v0.UD1) throw new TraceDiffException("v0", ee.v0, eeref.v0);
            if (ee.v1.UD0 != eeref.v1.UD0 || ee.v1.UD1 != eeref.v1.UD1) throw new TraceDiffException("v1", ee.v1, eeref.v1);
            if (ee.a0.UD0 != eeref.a0.UD0 || ee.a0.UD1 != eeref.a0.UD1) throw new TraceDiffException("a0", ee.a0, eeref.a0);
            if (ee.a1.UD0 != eeref.a1.UD0 || ee.a1.UD1 != eeref.a1.UD1) throw new TraceDiffException("a1", ee.a1, eeref.a1);
            if (ee.a2.UD0 != eeref.a2.UD0 || ee.a2.UD1 != eeref.a2.UD1) throw new TraceDiffException("a2", ee.a2, eeref.a2);
            if (ee.a3.UD0 != eeref.a3.UD0 || ee.a3.UD1 != eeref.a3.UD1) throw new TraceDiffException("a3", ee.a3, eeref.a3);
            if (ee.t0.UD0 != eeref.t0.UD0 || ee.t0.UD1 != eeref.t0.UD1) throw new TraceDiffException("t0", ee.t0, eeref.t0);
            if (ee.t1.UD0 != eeref.t1.UD0 || ee.t1.UD1 != eeref.t1.UD1) throw new TraceDiffException("t1", ee.t1, eeref.t1);
            if (ee.t2.UD0 != eeref.t2.UD0 || ee.t2.UD1 != eeref.t2.UD1) throw new TraceDiffException("t2", ee.t2, eeref.t2);
            if (ee.t3.UD0 != eeref.t3.UD0 || ee.t3.UD1 != eeref.t3.UD1) throw new TraceDiffException("t3", ee.t3, eeref.t3);
            if (ee.t4.UD0 != eeref.t4.UD0 || ee.t4.UD1 != eeref.t4.UD1) throw new TraceDiffException("t4", ee.t4, eeref.t4);
            if (ee.t5.UD0 != eeref.t5.UD0 || ee.t5.UD1 != eeref.t5.UD1) throw new TraceDiffException("t5", ee.t5, eeref.t5);
            if (ee.t6.UD0 != eeref.t6.UD0 || ee.t6.UD1 != eeref.t6.UD1) throw new TraceDiffException("t6", ee.t6, eeref.t6);
            if (ee.t7.UD0 != eeref.t7.UD0 || ee.t7.UD1 != eeref.t7.UD1) throw new TraceDiffException("t7", ee.t7, eeref.t7);
            if (ee.s0.UD0 != eeref.s0.UD0 || ee.s0.UD1 != eeref.s0.UD1) throw new TraceDiffException("s0", ee.s0, eeref.s0);
            if (ee.s1.UD0 != eeref.s1.UD0 || ee.s1.UD1 != eeref.s1.UD1) throw new TraceDiffException("s1", ee.s1, eeref.s1);
            if (ee.s2.UD0 != eeref.s2.UD0 || ee.s2.UD1 != eeref.s2.UD1) throw new TraceDiffException("s2", ee.s2, eeref.s2);
            if (ee.s3.UD0 != eeref.s3.UD0 || ee.s3.UD1 != eeref.s3.UD1) throw new TraceDiffException("s3", ee.s3, eeref.s3);
            if (ee.s4.UD0 != eeref.s4.UD0 || ee.s4.UD1 != eeref.s4.UD1) throw new TraceDiffException("s4", ee.s4, eeref.s4);
            if (ee.s5.UD0 != eeref.s5.UD0 || ee.s5.UD1 != eeref.s5.UD1) throw new TraceDiffException("s5", ee.s5, eeref.s5);
            if (ee.s6.UD0 != eeref.s6.UD0 || ee.s6.UD1 != eeref.s6.UD1) throw new TraceDiffException("s6", ee.s6, eeref.s6);
            if (ee.s7.UD0 != eeref.s7.UD0 || ee.s7.UD1 != eeref.s7.UD1) throw new TraceDiffException("s7", ee.s7, eeref.s7);
            if (ee.t8.UD0 != eeref.t8.UD0 || ee.t8.UD1 != eeref.t8.UD1) throw new TraceDiffException("t8", ee.t8, eeref.t8);
            if (ee.t9.UD0 != eeref.t9.UD0 || ee.t9.UD1 != eeref.t9.UD1) throw new TraceDiffException("t9", ee.t9, eeref.t9);
            if (ee.k0.UD0 != eeref.k0.UD0 || ee.k0.UD1 != eeref.k0.UD1) throw new TraceDiffException("k0", ee.k0, eeref.k0);
            if (ee.k1.UD0 != eeref.k1.UD0 || ee.k1.UD1 != eeref.k1.UD1) throw new TraceDiffException("k1", ee.k1, eeref.k1);
            if (ee.gp.UD0 != eeref.gp.UD0 || ee.gp.UD1 != eeref.gp.UD1) throw new TraceDiffException("gp", ee.gp, eeref.gp);
            if (ee.sp.UD0 != eeref.sp.UD0 || ee.sp.UD1 != eeref.sp.UD1) throw new TraceDiffException("sp", ee.sp, eeref.sp);
            if (ee.s8.UD0 != eeref.s8.UD0 || ee.s8.UD1 != eeref.s8.UD1) throw new TraceDiffException("s8", ee.s8, eeref.s8);
            if (ee.ra.UD0 != eeref.ra.UD0 || ee.ra.UD1 != eeref.ra.UD1) throw new TraceDiffException("ra", ee.ra, eeref.ra);

            if (ee.fpr[0].UL != eeref.fpr[0].UL) throw new TraceDiffException("$f0", (ee.fpr[0].UL), (eeref.fpr[0].UL));
            if (ee.fpr[1].UL != eeref.fpr[1].UL) throw new TraceDiffException("$f1", (ee.fpr[1].UL), (eeref.fpr[1].UL));
            if (ee.fpr[2].UL != eeref.fpr[2].UL) throw new TraceDiffException("$f2", (ee.fpr[2].UL), (eeref.fpr[2].UL));
            if (ee.fpr[3].UL != eeref.fpr[3].UL) throw new TraceDiffException("$f3", (ee.fpr[3].UL), (eeref.fpr[3].UL));
            if (ee.fpr[4].UL != eeref.fpr[4].UL) throw new TraceDiffException("$f4", (ee.fpr[4].UL), (eeref.fpr[4].UL));
            if (ee.fpr[5].UL != eeref.fpr[5].UL) throw new TraceDiffException("$f5", (ee.fpr[5].UL), (eeref.fpr[5].UL));
            if (ee.fpr[6].UL != eeref.fpr[6].UL) throw new TraceDiffException("$f6", (ee.fpr[6].UL), (eeref.fpr[6].UL));
            if (ee.fpr[7].UL != eeref.fpr[7].UL) throw new TraceDiffException("$f7", (ee.fpr[7].UL), (eeref.fpr[7].UL));
            if (ee.fpr[8].UL != eeref.fpr[8].UL) throw new TraceDiffException("$f8", (ee.fpr[8].UL), (eeref.fpr[8].UL));
            if (ee.fpr[9].UL != eeref.fpr[9].UL) throw new TraceDiffException("$f9", (ee.fpr[9].UL), (eeref.fpr[9].UL));
            if (ee.fpr[10].UL != eeref.fpr[10].UL) throw new TraceDiffException("$f10", (ee.fpr[10].UL), (eeref.fpr[10].UL));
            if (ee.fpr[11].UL != eeref.fpr[11].UL) throw new TraceDiffException("$f11", (ee.fpr[11].UL), (eeref.fpr[11].UL));
            if (ee.fpr[12].UL != eeref.fpr[12].UL) throw new TraceDiffException("$f12", (ee.fpr[12].UL), (eeref.fpr[12].UL));
            if (ee.fpr[13].UL != eeref.fpr[13].UL) throw new TraceDiffException("$f13", (ee.fpr[13].UL), (eeref.fpr[13].UL));
            if (ee.fpr[14].UL != eeref.fpr[14].UL) throw new TraceDiffException("$f14", (ee.fpr[14].UL), (eeref.fpr[14].UL));
            if (ee.fpr[15].UL != eeref.fpr[15].UL) throw new TraceDiffException("$f15", (ee.fpr[15].UL), (eeref.fpr[15].UL));
            if (ee.fpr[16].UL != eeref.fpr[16].UL) throw new TraceDiffException("$f16", (ee.fpr[16].UL), (eeref.fpr[16].UL));
            if (ee.fpr[17].UL != eeref.fpr[17].UL) throw new TraceDiffException("$f17", (ee.fpr[17].UL), (eeref.fpr[17].UL));
            if (ee.fpr[18].UL != eeref.fpr[18].UL) throw new TraceDiffException("$f18", (ee.fpr[18].UL), (eeref.fpr[18].UL));
            if (ee.fpr[19].UL != eeref.fpr[19].UL) throw new TraceDiffException("$f19", (ee.fpr[19].UL), (eeref.fpr[19].UL));
            if (ee.fpr[20].UL != eeref.fpr[20].UL) throw new TraceDiffException("$f20", (ee.fpr[20].UL), (eeref.fpr[20].UL));
            if (ee.fpr[21].UL != eeref.fpr[21].UL) throw new TraceDiffException("$f21", (ee.fpr[21].UL), (eeref.fpr[21].UL));
            if (ee.fpr[22].UL != eeref.fpr[22].UL) throw new TraceDiffException("$f22", (ee.fpr[22].UL), (eeref.fpr[22].UL));
            if (ee.fpr[23].UL != eeref.fpr[23].UL) throw new TraceDiffException("$f23", (ee.fpr[23].UL), (eeref.fpr[23].UL));
            if (ee.fpr[24].UL != eeref.fpr[24].UL) throw new TraceDiffException("$f24", (ee.fpr[24].UL), (eeref.fpr[24].UL));
            if (ee.fpr[25].UL != eeref.fpr[25].UL) throw new TraceDiffException("$f25", (ee.fpr[25].UL), (eeref.fpr[25].UL));
            if (ee.fpr[26].UL != eeref.fpr[26].UL) throw new TraceDiffException("$f26", (ee.fpr[26].UL), (eeref.fpr[26].UL));
            if (ee.fpr[27].UL != eeref.fpr[27].UL) throw new TraceDiffException("$f27", (ee.fpr[27].UL), (eeref.fpr[27].UL));
            if (ee.fpr[28].UL != eeref.fpr[28].UL) throw new TraceDiffException("$f28", (ee.fpr[28].UL), (eeref.fpr[28].UL));
            if (ee.fpr[29].UL != eeref.fpr[29].UL) throw new TraceDiffException("$f29", (ee.fpr[29].UL), (eeref.fpr[29].UL));
            if (ee.fpr[30].UL != eeref.fpr[30].UL) throw new TraceDiffException("$f30", (ee.fpr[30].UL), (eeref.fpr[30].UL));
            if (ee.fpr[31].UL != eeref.fpr[31].UL) throw new TraceDiffException("$f31", (ee.fpr[31].UL), (eeref.fpr[31].UL));
            if (ee.fpracc.UL != eeref.fpracc.UL) throw new TraceDiffException("acc", (ee.fpracc.UL), (eeref.fpracc.UL));

            if (ee.VF[1].CompareTo(eeref.VF[1]) != 0) throw new TraceDiffException("VF1", ee.VF[1], eeref.VF[1]);
            if (ee.VF[2].CompareTo(eeref.VF[2]) != 0) throw new TraceDiffException("VF2", ee.VF[2], eeref.VF[2]);
            if (ee.VF[3].CompareTo(eeref.VF[3]) != 0) throw new TraceDiffException("VF3", ee.VF[3], eeref.VF[3]);
            if (ee.VF[4].CompareTo(eeref.VF[4]) != 0) throw new TraceDiffException("VF4", ee.VF[4], eeref.VF[4]);
            if (ee.VF[5].CompareTo(eeref.VF[5]) != 0) throw new TraceDiffException("VF5", ee.VF[5], eeref.VF[5]);
            if (ee.VF[6].CompareTo(eeref.VF[6]) != 0) throw new TraceDiffException("VF6", ee.VF[6], eeref.VF[6]);
            if (ee.VF[7].CompareTo(eeref.VF[7]) != 0) throw new TraceDiffException("VF7", ee.VF[7], eeref.VF[7]);
            if (ee.VF[8].CompareTo(eeref.VF[8]) != 0) throw new TraceDiffException("VF8", ee.VF[8], eeref.VF[8]);
            if (ee.VF[9].CompareTo(eeref.VF[9]) != 0) throw new TraceDiffException("VF9", ee.VF[9], eeref.VF[9]);
            if (ee.VF[10].CompareTo(eeref.VF[10]) != 0) throw new TraceDiffException("VF10", ee.VF[10], eeref.VF[10]);
            if (ee.VF[11].CompareTo(eeref.VF[11]) != 0) throw new TraceDiffException("VF11", ee.VF[11], eeref.VF[11]);
            if (ee.VF[12].CompareTo(eeref.VF[12]) != 0) throw new TraceDiffException("VF12", ee.VF[12], eeref.VF[12]);
            if (ee.VF[13].CompareTo(eeref.VF[13]) != 0) throw new TraceDiffException("VF13", ee.VF[13], eeref.VF[13]);
            if (ee.VF[14].CompareTo(eeref.VF[14]) != 0) throw new TraceDiffException("VF14", ee.VF[14], eeref.VF[14]);
            if (ee.VF[15].CompareTo(eeref.VF[15]) != 0) throw new TraceDiffException("VF15", ee.VF[15], eeref.VF[15]);
            if (ee.VF[16].CompareTo(eeref.VF[16]) != 0) throw new TraceDiffException("VF16", ee.VF[16], eeref.VF[16]);
            if (ee.VF[17].CompareTo(eeref.VF[17]) != 0) throw new TraceDiffException("VF17", ee.VF[17], eeref.VF[17]);
            if (ee.VF[18].CompareTo(eeref.VF[18]) != 0) throw new TraceDiffException("VF18", ee.VF[18], eeref.VF[18]);
            if (ee.VF[19].CompareTo(eeref.VF[19]) != 0) throw new TraceDiffException("VF19", ee.VF[19], eeref.VF[19]);
            if (ee.VF[20].CompareTo(eeref.VF[20]) != 0) throw new TraceDiffException("VF20", ee.VF[20], eeref.VF[20]);
            if (ee.VF[21].CompareTo(eeref.VF[21]) != 0) throw new TraceDiffException("VF21", ee.VF[21], eeref.VF[21]);
            if (ee.VF[22].CompareTo(eeref.VF[22]) != 0) throw new TraceDiffException("VF22", ee.VF[22], eeref.VF[22]);
            if (ee.VF[23].CompareTo(eeref.VF[23]) != 0) throw new TraceDiffException("VF23", ee.VF[23], eeref.VF[23]);
            if (ee.VF[24].CompareTo(eeref.VF[24]) != 0) throw new TraceDiffException("VF24", ee.VF[24], eeref.VF[24]);
            if (ee.VF[25].CompareTo(eeref.VF[25]) != 0) throw new TraceDiffException("VF25", ee.VF[25], eeref.VF[25]);
            if (ee.VF[26].CompareTo(eeref.VF[26]) != 0) throw new TraceDiffException("VF26", ee.VF[26], eeref.VF[26]);
            if (ee.VF[27].CompareTo(eeref.VF[27]) != 0) throw new TraceDiffException("VF27", ee.VF[27], eeref.VF[27]);
            if (ee.VF[28].CompareTo(eeref.VF[28]) != 0) throw new TraceDiffException("VF28", ee.VF[28], eeref.VF[28]);
            if (ee.VF[29].CompareTo(eeref.VF[29]) != 0) throw new TraceDiffException("VF29", ee.VF[29], eeref.VF[29]);
            if (ee.VF[30].CompareTo(eeref.VF[30]) != 0) throw new TraceDiffException("VF30", ee.VF[30], eeref.VF[30]);
            if (ee.VF[31].CompareTo(eeref.VF[31]) != 0) throw new TraceDiffException("VF31", ee.VF[31], eeref.VF[31]);
            if (ee.Vacc.CompareTo(eeref.Vacc) != 0) throw new TraceDiffException("Vacc", ee.Vacc, eeref.Vacc);

            walkPos++;
            if (exeee != null) {
                exeee.Stepee();
            }
            #endregion
        }
    }

    class Compareby6 {
        public static void Run(byte[] sx1, byte[] sx2, byte[] rx1, byte[] rx2, byte[] tx1, byte[] tx2, TextWriter wr) {
            MemoryStream[] si6 = new MemoryStream[]{
                    new MemoryStream(sx1,false),
                    new MemoryStream(sx2,false),
                    new MemoryStream(rx1,false),
                    new MemoryStream(rx2,false),
                    new MemoryStream(tx1,false),
                    new MemoryStream(tx2,false),
                };
            BinaryReader[] br6 = new BinaryReader[]{
                    new BinaryReader(si6[0]),
                    new BinaryReader(si6[1]),
                    new BinaryReader(si6[2]),
                    new BinaryReader(si6[3]),
                    new BinaryReader(si6[4]),
                    new BinaryReader(si6[5]),
                };
            for (int t = 0; t < 0xE5; t++) {
                wr.Write("|{0,-3}|", t);
                for (int c = 0; c < 3; c++) {
                    for (int x = 0; x < 4; x++) {
                        uint lv = br6[2 * c + 0].ReadUInt32();
                        uint rv = br6[2 * c + 1].ReadUInt32();
                        if (x < 3) {
                            wr.Write((lv == rv) ? ("xyzXYZxyz"[3 * c + x]) : ' ');
                        }
                        Debug.Assert(lv == rv || rv == 0xe5e5e5e5);
                    }
                }
                wr.WriteLine();
            }
            wr.Write("");
        }
    }

    public partial class Mobrc1 {
        public Mobrc1(CustEE eeref, Stateee ees, IExecEE exeee) {
            this.eeref = eeref;
            this.ees = ees;
            this.exeee = exeee;
        }

        CustEE eeref;
        Stateee ees;
        IExecEE exeee;
        int walkPos = 0;

        public void Exec() {
            Array.Copy(eeref.ram, ee.ram, 32 * 1024 * 1024);

            ee.pc = 0x129A18;
            while (true) {
                (dicti2a[ee.pc])();
            }
        }

        public void Exec2(string fp) {
            using (FileStream fs = File.Create(fp)) {
                for (float fv = 0; fv < 20; fv++) {
                    initstate();

                    // 00354260-01ace680
                    Array.Copy(eeref.ram, 0x00354260, ee.ram, 0x00354260, 0x01ace680 - 0x00354260);

#if KH2DED
                    ee.s1.UD0 = ee.a0.UD0 = 0x009E0340 + 0x1D390;
                    ee.s0.UD0 = ee.s1.UD0 + 0x90;
                    ee.fpr[20].f = 17 * fv;
#endif

                    ee.pc = 0x129A18;
                    while (ee.pc != 0x130C90) {
                        (dicti2a[ee.pc])();
                    }

                    // OUT: 0x3B6BE0
                    fs.Write(ee.ram, 0x1ACAD50, 0x40 * 0xE5);
                }
            }
        }

        public void Exec2a(string dir, NotifyIcon ico) {
            Array.Copy(eeref.ram, ee.ram, 33554432U);

            for (int t = 0; t < 65536; t++) ee.ram[t] = 0;

            initstate();

            for (int t = 0; t < 32; t++) {
                ee.fpr[t].f = 0;
                if (t != 0) { ee.VF[t].x = 0; ee.VF[t].y = 0; ee.VF[t].z = 0; ee.VF[t].w = 0; }
            }
            ee.Vacc.x = 0; ee.Vacc.y = 0; ee.Vacc.z = 0; ee.Vacc.w = 0;
            ee.fpracc.f = 0;

#if true
            //ee.r0.UD0 = 0U;
            ee.at.UD0 = 0U;
            ee.v0.UD0 = 0U;
            ee.v1.UD0 = 0U;
            ee.a0.UD0 = 0x9F8510U; // mset +0x00
            ee.a1.UD0 = 0x1ABB590U; // info tbl
            ee.a2.UD0 = 0U;
            ee.a3.UD0 = 0U;
            ee.t0.UD0 = 0U;
            ee.t1.UD0 = 0U;
            ee.t2.UD0 = 0U;
            ee.t3.UD0 = 0U;
            ee.t4.UD0 = 0U;
            ee.t5.UD0 = 0U;
            ee.t6.UD0 = 0U;
            ee.t7.UD0 = 0U;
            ee.s0.UD0 = 0x9F85A0U; // mset +0x90
            ee.s1.UD0 = 0U;
            ee.s2.UD0 = 0U;
            ee.s3.UD0 = 0U;
            ee.s4.UD0 = 0x1ABB0B0U; // temp?
            ee.s5.UD0 = 0U;
            ee.s6.UD0 = 0U;
            ee.s7.UD0 = 0U;
            ee.t8.UD0 = 0U;
            ee.t9.UD0 = 0U;
            ee.k0.UD0 = 0U;
            ee.k1.UD0 = 0U;
            ee.gp.UD0 = 0U;
            ee.sp.UD0 = 0x2000000U;
            ee.s8.UD0 = 0U;
#endif

            ee.ra.UD0 = 0xFFFFFFFFU;

            ee.pc = 0x129A18;
            while (ee.pc != 0xFFFFFFFFU) {
                if (dicti2a.ContainsKey(ee.pc)) {
                    dicti2a[ee.pc]();
                }
                else {
                    throw new RecfnnotFound(ee.pc);
                }
            }

            if (true) {
                int t;
                for (t = 0; t < 65536 && ee.ram[t] == 0; t++) ;
                Debug.Assert(t == 65536, "ee.ram[" + t + "] ‚ 0");

                byte[] binIn = File.ReadAllBytes(Path.Combine(dir, "1ACAD50.bin"));
                for (t = 0; t < 0x40 * 0xE5 && binIn[t] == ee.ram[0x1ACAD50 + t]; t++) ;
                Debug.Assert(t == 0x40 * 0xE5, "1ACAD50.bin pos: " + t + " ‚ " + (0x40 * 0xE5));
            }

            ico.ShowBalloonTip(500, "Research result", "It passed. @ " + DateTime.Now, ToolTipIcon.Info);
        }

        public void Exec3(string fp) {
            // 2007/04/23  00:43         4,100,830 M_EX020_RAW.mset
            // 2007/04/23  00:44         2,269,144 B_LK120.mdlx
            Array.Copy(eeref.ram, 0x00354260, ee.ram, 0x00354260, 0x01ace680 - 0x00354260);

            File.WriteAllBytes(@"H:\Proj\khkh_xldM\MEMO\expSim\rc1\RAM.bin", ee.ram);

            MemoryStream os = new MemoryStream();

            if (true) {
                initstate();

                ee.at.UD0 = 0;
                ee.v0.UD0 = 0;
                ee.v1.UD0 = 0;
                ee.a0.UD0 = 0;
                //ee.a1.UD0 = 0;
                ee.a2.UD0 = 0;
                ee.a3.UD0 = 0;
                ee.t0.UD0 = 0;
                ee.t1.UD0 = 0;
                ee.t2.UD0 = 0;
                ee.t3.UD0 = 0;
                ee.t4.UD0 = 0;
                ee.t5.UD0 = 0;
                ee.t6.UD0 = 0;
                ee.t7.UD0 = 0;
                ee.t8.UD0 = 0;
                ee.t9.UD0 = 0;
                ee.k0.UD0 = 0;
                ee.k1.UD0 = 0;
                ee.s8.UD0 = 0;
                ee.gp.UD0 = 0;
                ee.sp.UD0 = 32 * 1024 * 1024;
                ee.s3.UD0 = 0;
                ee.s5.UD0 = 0;
                ee.s6.UD0 = 0;
                ee.s7.UD0 = 0;
                ee.s8.UD0 = 0;
                ee.gp.UD0 = 0;
                ee.ra.UD0 = 0xFFFFFFFFU;

                uint tmp1 = (32 * 1024 * 1024) - (768) - (65536);
                uint tmp2 = (32 * 1024 * 1024) - (768) - (65536) - (65536);
                uint tmp3 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536);
                uint tmp4 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256);
                uint tmp5 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256);
                uint tmp6 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256);
                uint tmp7 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256) - (16 * 256);
                uint tmp8 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256) - (16 * 256) - (65536);
                uint tmp9 = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256) - (16 * 256) - (65536) - (65536);
                uint tmpa = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256) - (16 * 256) - (65536) - (65536) - (65536);
                uint tmpb = (32 * 1024 * 1024) - (768) - (65536) - (65536) - (65536) - (64 * 256) - (16 * 256) - (16 * 256) - (16 * 256) - (65536) - (65536) - (65536) - (65536);

#if false
                    uint Sxyz = 0x1ABB6D0;// tmp5;
                    uint Rxyz = 0x1ABC520;// tmp6;
                    uint Txyz = 0x1ABD370;// tmp7;
#else
                uint Sxyz = tmp5;
                uint Rxyz = tmp6;
                uint Txyz = tmp7;

                Array.Copy(ee.ram, 0x1ABB6D0, ee.ram, tmp5, 16 * 0xE5);
                Array.Copy(ee.ram, 0x1ABC520, ee.ram, tmp6, 16 * 0xE5);
                Array.Copy(ee.ram, 0x1ABD370, ee.ram, tmp7, 16 * 0xE5);
#endif

                uint offMdlxRoot = 20 * 1024 * 1024;
                using (FileStream fsMdlx = File.OpenRead(@"V:\KH2.yaz0r\dump_kh2\obj\P_EX110.mdlx")) {
                    fsMdlx.Read(ee.ram, (int)offMdlxRoot, 5 * 1024 * 1024);
                }
                uint offMdlx04 = new RelocMdlx(ee.ram, (int)offMdlxRoot, (int)offMdlxRoot, 0x354398, 0, tmp2, 1).Run();

                ee.s4.UD0 = tmp1; // temp;
                ee.s2.UD0 = ee.a1.UD0 = tmp2; // st1;

                for (int w = 0; w < 65536; w++) ee.ram[w] = 0;

                if (true) {
                    MemoryStream wri = new MemoryStream(ee.ram, true);
                    wri.Position = ee.s2.UL0;
                    BinaryWriter wr = new BinaryWriter(wri);
                    uint[] st1al = new uint[] { 
                            0x0       ,0         ,0    ,0,
                            0x0       ,offMdlx04 ,0x0  ,Sxyz,
                            Rxyz      ,Txyz      ,tmp3 ,tmp4,
                            0x0       ,0x0       ,0x0  ,0,
                        };
                    foreach (uint ui in st1al) wr.Write(ui);
                }

                uint offMsetRoot = 25 * 1024 * 1024;
                using (FileStream fsMset = File.OpenRead(@"V:\KH2.yaz0r\dump_kh2\obj\P_EX110.mset")) {
                    fsMset.Read(ee.ram, (int)offMsetRoot, 5 * 1024 * 1024);
                }
                RelocMset RM = new RelocMset(ee.ram, offMsetRoot, offMsetRoot, new uint[] { 0, 0, tmp8, tmp9, tmpa, tmpb, });
                RM.Run();

                //uint offMset = 0x009E0340 + 0x1D390;
                uint offMset = offMsetRoot + 0x3370;

                // s0, s1, s2, s4, a1
                ee.s1.UD0 = ee.a0.UD0 = offMset;
                ee.s0.UD0 = offMset + 0x90;

                ee.pc = 0x129A18;
                while (ee.pc != 0xFFFFFFFFU) {
                    if (dicti2a.ContainsKey(ee.pc)) {
                        dicti2a[ee.pc]();
                    }
                    else {
                        throw new RecfnnotFound(ee.pc);
                    }
                }

                os.Write(ee.ram, (int)tmp4, 0x40 * 0xE5);

                for (int w = 0; w < 65536; w++) Debug.Assert(ee.ram[w] == 0);
            }

            byte[] b1 = File.ReadAllBytes(@"H:\Proj\khkh_xldM\MEMO\expSim\rc1\0x1ACAD50.bin");
            byte[] b2 = os.ToArray();
            int a = 0;
            for (; a < b1.Length && b1[a] == b2[a]; a++) ;
            Debug.Assert(a == b1.Length, "The output is diff: " + a + " ‚ " + b1.Length);

            File.WriteAllBytes(fp, b2);
        }

        void here(uint pc) { }

        void here2(uint pc) {
            #region Many many verification
            if (pc != ees.pc) throw new TraceDiffException("pc", pc, ees.pc);
            if (ee.at.UD0 != eeref.at.UD0 || ee.at.UD1 != eeref.at.UD1) throw new TraceDiffException("at", ee.at, eeref.at);
            if (ee.v0.UD0 != eeref.v0.UD0 || ee.v0.UD1 != eeref.v0.UD1) throw new TraceDiffException("v0", ee.v0, eeref.v0);
            if (ee.v1.UD0 != eeref.v1.UD0 || ee.v1.UD1 != eeref.v1.UD1) throw new TraceDiffException("v1", ee.v1, eeref.v1);
            if (ee.a0.UD0 != eeref.a0.UD0 || ee.a0.UD1 != eeref.a0.UD1) throw new TraceDiffException("a0", ee.a0, eeref.a0);
            if (ee.a1.UD0 != eeref.a1.UD0 || ee.a1.UD1 != eeref.a1.UD1) throw new TraceDiffException("a1", ee.a1, eeref.a1);
            if (ee.a2.UD0 != eeref.a2.UD0 || ee.a2.UD1 != eeref.a2.UD1) throw new TraceDiffException("a2", ee.a2, eeref.a2);
            if (ee.a3.UD0 != eeref.a3.UD0 || ee.a3.UD1 != eeref.a3.UD1) throw new TraceDiffException("a3", ee.a3, eeref.a3);
            if (ee.t0.UD0 != eeref.t0.UD0 || ee.t0.UD1 != eeref.t0.UD1) throw new TraceDiffException("t0", ee.t0, eeref.t0);
            if (ee.t1.UD0 != eeref.t1.UD0 || ee.t1.UD1 != eeref.t1.UD1) throw new TraceDiffException("t1", ee.t1, eeref.t1);
            if (ee.t2.UD0 != eeref.t2.UD0 || ee.t2.UD1 != eeref.t2.UD1) throw new TraceDiffException("t2", ee.t2, eeref.t2);
            if (ee.t3.UD0 != eeref.t3.UD0 || ee.t3.UD1 != eeref.t3.UD1) throw new TraceDiffException("t3", ee.t3, eeref.t3);
            if (ee.t4.UD0 != eeref.t4.UD0 || ee.t4.UD1 != eeref.t4.UD1) throw new TraceDiffException("t4", ee.t4, eeref.t4);
            if (ee.t5.UD0 != eeref.t5.UD0 || ee.t5.UD1 != eeref.t5.UD1) throw new TraceDiffException("t5", ee.t5, eeref.t5);
            if (ee.t6.UD0 != eeref.t6.UD0 || ee.t6.UD1 != eeref.t6.UD1) throw new TraceDiffException("t6", ee.t6, eeref.t6);
            if (ee.t7.UD0 != eeref.t7.UD0 || ee.t7.UD1 != eeref.t7.UD1) throw new TraceDiffException("t7", ee.t7, eeref.t7);
            if (ee.s0.UD0 != eeref.s0.UD0 || ee.s0.UD1 != eeref.s0.UD1) throw new TraceDiffException("s0", ee.s0, eeref.s0);
            if (ee.s1.UD0 != eeref.s1.UD0 || ee.s1.UD1 != eeref.s1.UD1) throw new TraceDiffException("s1", ee.s1, eeref.s1);
            if (ee.s2.UD0 != eeref.s2.UD0 || ee.s2.UD1 != eeref.s2.UD1) throw new TraceDiffException("s2", ee.s2, eeref.s2);
            if (ee.s3.UD0 != eeref.s3.UD0 || ee.s3.UD1 != eeref.s3.UD1) throw new TraceDiffException("s3", ee.s3, eeref.s3);
            if (ee.s4.UD0 != eeref.s4.UD0 || ee.s4.UD1 != eeref.s4.UD1) throw new TraceDiffException("s4", ee.s4, eeref.s4);
            if (ee.s5.UD0 != eeref.s5.UD0 || ee.s5.UD1 != eeref.s5.UD1) throw new TraceDiffException("s5", ee.s5, eeref.s5);
            if (ee.s6.UD0 != eeref.s6.UD0 || ee.s6.UD1 != eeref.s6.UD1) throw new TraceDiffException("s6", ee.s6, eeref.s6);
            if (ee.s7.UD0 != eeref.s7.UD0 || ee.s7.UD1 != eeref.s7.UD1) throw new TraceDiffException("s7", ee.s7, eeref.s7);
            if (ee.t8.UD0 != eeref.t8.UD0 || ee.t8.UD1 != eeref.t8.UD1) throw new TraceDiffException("t8", ee.t8, eeref.t8);
            if (ee.t9.UD0 != eeref.t9.UD0 || ee.t9.UD1 != eeref.t9.UD1) throw new TraceDiffException("t9", ee.t9, eeref.t9);
            if (ee.k0.UD0 != eeref.k0.UD0 || ee.k0.UD1 != eeref.k0.UD1) throw new TraceDiffException("k0", ee.k0, eeref.k0);
            if (ee.k1.UD0 != eeref.k1.UD0 || ee.k1.UD1 != eeref.k1.UD1) throw new TraceDiffException("k1", ee.k1, eeref.k1);
            if (ee.gp.UD0 != eeref.gp.UD0 || ee.gp.UD1 != eeref.gp.UD1) throw new TraceDiffException("gp", ee.gp, eeref.gp);
            if (ee.sp.UD0 != eeref.sp.UD0 || ee.sp.UD1 != eeref.sp.UD1) throw new TraceDiffException("sp", ee.sp, eeref.sp);
            if (ee.s8.UD0 != eeref.s8.UD0 || ee.s8.UD1 != eeref.s8.UD1) throw new TraceDiffException("s8", ee.s8, eeref.s8);
            if (ee.ra.UD0 != eeref.ra.UD0 || ee.ra.UD1 != eeref.ra.UD1) throw new TraceDiffException("ra", ee.ra, eeref.ra);

            if (ee.fpr[0].UL != eeref.fpr[0].UL) throw new TraceDiffException("$f0", (ee.fpr[0].UL), (eeref.fpr[0].UL));
            if (ee.fpr[1].UL != eeref.fpr[1].UL) throw new TraceDiffException("$f1", (ee.fpr[1].UL), (eeref.fpr[1].UL));
            if (ee.fpr[2].UL != eeref.fpr[2].UL) throw new TraceDiffException("$f2", (ee.fpr[2].UL), (eeref.fpr[2].UL));
            if (ee.fpr[3].UL != eeref.fpr[3].UL) throw new TraceDiffException("$f3", (ee.fpr[3].UL), (eeref.fpr[3].UL));
            if (ee.fpr[4].UL != eeref.fpr[4].UL) throw new TraceDiffException("$f4", (ee.fpr[4].UL), (eeref.fpr[4].UL));
            if (ee.fpr[5].UL != eeref.fpr[5].UL) throw new TraceDiffException("$f5", (ee.fpr[5].UL), (eeref.fpr[5].UL));
            if (ee.fpr[6].UL != eeref.fpr[6].UL) throw new TraceDiffException("$f6", (ee.fpr[6].UL), (eeref.fpr[6].UL));
            if (ee.fpr[7].UL != eeref.fpr[7].UL) throw new TraceDiffException("$f7", (ee.fpr[7].UL), (eeref.fpr[7].UL));
            if (ee.fpr[8].UL != eeref.fpr[8].UL) throw new TraceDiffException("$f8", (ee.fpr[8].UL), (eeref.fpr[8].UL));
            if (ee.fpr[9].UL != eeref.fpr[9].UL) throw new TraceDiffException("$f9", (ee.fpr[9].UL), (eeref.fpr[9].UL));
            if (ee.fpr[10].UL != eeref.fpr[10].UL) throw new TraceDiffException("$f10", (ee.fpr[10].UL), (eeref.fpr[10].UL));
            if (ee.fpr[11].UL != eeref.fpr[11].UL) throw new TraceDiffException("$f11", (ee.fpr[11].UL), (eeref.fpr[11].UL));
            if (ee.fpr[12].UL != eeref.fpr[12].UL) throw new TraceDiffException("$f12", (ee.fpr[12].UL), (eeref.fpr[12].UL));
            if (ee.fpr[13].UL != eeref.fpr[13].UL) throw new TraceDiffException("$f13", (ee.fpr[13].UL), (eeref.fpr[13].UL));
            if (ee.fpr[14].UL != eeref.fpr[14].UL) throw new TraceDiffException("$f14", (ee.fpr[14].UL), (eeref.fpr[14].UL));
            if (ee.fpr[15].UL != eeref.fpr[15].UL) throw new TraceDiffException("$f15", (ee.fpr[15].UL), (eeref.fpr[15].UL));
            if (ee.fpr[16].UL != eeref.fpr[16].UL) throw new TraceDiffException("$f16", (ee.fpr[16].UL), (eeref.fpr[16].UL));
            if (ee.fpr[17].UL != eeref.fpr[17].UL) throw new TraceDiffException("$f17", (ee.fpr[17].UL), (eeref.fpr[17].UL));
            if (ee.fpr[18].UL != eeref.fpr[18].UL) throw new TraceDiffException("$f18", (ee.fpr[18].UL), (eeref.fpr[18].UL));
            if (ee.fpr[19].UL != eeref.fpr[19].UL) throw new TraceDiffException("$f19", (ee.fpr[19].UL), (eeref.fpr[19].UL));
            if (ee.fpr[20].UL != eeref.fpr[20].UL) throw new TraceDiffException("$f20", (ee.fpr[20].UL), (eeref.fpr[20].UL));
            if (ee.fpr[21].UL != eeref.fpr[21].UL) throw new TraceDiffException("$f21", (ee.fpr[21].UL), (eeref.fpr[21].UL));
            if (ee.fpr[22].UL != eeref.fpr[22].UL) throw new TraceDiffException("$f22", (ee.fpr[22].UL), (eeref.fpr[22].UL));
            if (ee.fpr[23].UL != eeref.fpr[23].UL) throw new TraceDiffException("$f23", (ee.fpr[23].UL), (eeref.fpr[23].UL));
            if (ee.fpr[24].UL != eeref.fpr[24].UL) throw new TraceDiffException("$f24", (ee.fpr[24].UL), (eeref.fpr[24].UL));
            if (ee.fpr[25].UL != eeref.fpr[25].UL) throw new TraceDiffException("$f25", (ee.fpr[25].UL), (eeref.fpr[25].UL));
            if (ee.fpr[26].UL != eeref.fpr[26].UL) throw new TraceDiffException("$f26", (ee.fpr[26].UL), (eeref.fpr[26].UL));
            if (ee.fpr[27].UL != eeref.fpr[27].UL) throw new TraceDiffException("$f27", (ee.fpr[27].UL), (eeref.fpr[27].UL));
            if (ee.fpr[28].UL != eeref.fpr[28].UL) throw new TraceDiffException("$f28", (ee.fpr[28].UL), (eeref.fpr[28].UL));
            if (ee.fpr[29].UL != eeref.fpr[29].UL) throw new TraceDiffException("$f29", (ee.fpr[29].UL), (eeref.fpr[29].UL));
            if (ee.fpr[30].UL != eeref.fpr[30].UL) throw new TraceDiffException("$f30", (ee.fpr[30].UL), (eeref.fpr[30].UL));
            if (ee.fpr[31].UL != eeref.fpr[31].UL) throw new TraceDiffException("$f31", (ee.fpr[31].UL), (eeref.fpr[31].UL));
            if (ee.fpracc.UL != eeref.fpracc.UL) throw new TraceDiffException("acc", (ee.fpracc.UL), (eeref.fpracc.UL));

            if (ee.VF[1].CompareTo(eeref.VF[1]) != 0) throw new TraceDiffException("VF1", ee.VF[1], eeref.VF[1]);
            if (ee.VF[2].CompareTo(eeref.VF[2]) != 0) throw new TraceDiffException("VF2", ee.VF[2], eeref.VF[2]);
            if (ee.VF[3].CompareTo(eeref.VF[3]) != 0) throw new TraceDiffException("VF3", ee.VF[3], eeref.VF[3]);
            if (ee.VF[4].CompareTo(eeref.VF[4]) != 0) throw new TraceDiffException("VF4", ee.VF[4], eeref.VF[4]);
            if (ee.VF[5].CompareTo(eeref.VF[5]) != 0) throw new TraceDiffException("VF5", ee.VF[5], eeref.VF[5]);
            if (ee.VF[6].CompareTo(eeref.VF[6]) != 0) throw new TraceDiffException("VF6", ee.VF[6], eeref.VF[6]);
            if (ee.VF[7].CompareTo(eeref.VF[7]) != 0) throw new TraceDiffException("VF7", ee.VF[7], eeref.VF[7]);
            if (ee.VF[8].CompareTo(eeref.VF[8]) != 0) throw new TraceDiffException("VF8", ee.VF[8], eeref.VF[8]);
            if (ee.VF[9].CompareTo(eeref.VF[9]) != 0) throw new TraceDiffException("VF9", ee.VF[9], eeref.VF[9]);
            if (ee.VF[10].CompareTo(eeref.VF[10]) != 0) throw new TraceDiffException("VF10", ee.VF[10], eeref.VF[10]);
            if (ee.VF[11].CompareTo(eeref.VF[11]) != 0) throw new TraceDiffException("VF11", ee.VF[11], eeref.VF[11]);
            if (ee.VF[12].CompareTo(eeref.VF[12]) != 0) throw new TraceDiffException("VF12", ee.VF[12], eeref.VF[12]);
            if (ee.VF[13].CompareTo(eeref.VF[13]) != 0) throw new TraceDiffException("VF13", ee.VF[13], eeref.VF[13]);
            if (ee.VF[14].CompareTo(eeref.VF[14]) != 0) throw new TraceDiffException("VF14", ee.VF[14], eeref.VF[14]);
            if (ee.VF[15].CompareTo(eeref.VF[15]) != 0) throw new TraceDiffException("VF15", ee.VF[15], eeref.VF[15]);
            if (ee.VF[16].CompareTo(eeref.VF[16]) != 0) throw new TraceDiffException("VF16", ee.VF[16], eeref.VF[16]);
            if (ee.VF[17].CompareTo(eeref.VF[17]) != 0) throw new TraceDiffException("VF17", ee.VF[17], eeref.VF[17]);
            if (ee.VF[18].CompareTo(eeref.VF[18]) != 0) throw new TraceDiffException("VF18", ee.VF[18], eeref.VF[18]);
            if (ee.VF[19].CompareTo(eeref.VF[19]) != 0) throw new TraceDiffException("VF19", ee.VF[19], eeref.VF[19]);
            if (ee.VF[20].CompareTo(eeref.VF[20]) != 0) throw new TraceDiffException("VF20", ee.VF[20], eeref.VF[20]);
            if (ee.VF[21].CompareTo(eeref.VF[21]) != 0) throw new TraceDiffException("VF21", ee.VF[21], eeref.VF[21]);
            if (ee.VF[22].CompareTo(eeref.VF[22]) != 0) throw new TraceDiffException("VF22", ee.VF[22], eeref.VF[22]);
            if (ee.VF[23].CompareTo(eeref.VF[23]) != 0) throw new TraceDiffException("VF23", ee.VF[23], eeref.VF[23]);
            if (ee.VF[24].CompareTo(eeref.VF[24]) != 0) throw new TraceDiffException("VF24", ee.VF[24], eeref.VF[24]);
            if (ee.VF[25].CompareTo(eeref.VF[25]) != 0) throw new TraceDiffException("VF25", ee.VF[25], eeref.VF[25]);
            if (ee.VF[26].CompareTo(eeref.VF[26]) != 0) throw new TraceDiffException("VF26", ee.VF[26], eeref.VF[26]);
            if (ee.VF[27].CompareTo(eeref.VF[27]) != 0) throw new TraceDiffException("VF27", ee.VF[27], eeref.VF[27]);
            if (ee.VF[28].CompareTo(eeref.VF[28]) != 0) throw new TraceDiffException("VF28", ee.VF[28], eeref.VF[28]);
            if (ee.VF[29].CompareTo(eeref.VF[29]) != 0) throw new TraceDiffException("VF29", ee.VF[29], eeref.VF[29]);
            if (ee.VF[30].CompareTo(eeref.VF[30]) != 0) throw new TraceDiffException("VF30", ee.VF[30], eeref.VF[30]);
            if (ee.VF[31].CompareTo(eeref.VF[31]) != 0) throw new TraceDiffException("VF31", ee.VF[31], eeref.VF[31]);
            if (ee.Vacc.CompareTo(eeref.Vacc) != 0) throw new TraceDiffException("Vacc", ee.Vacc, eeref.Vacc);

            walkPos++;
            if (exeee != null) {
                exeee.Stepee();
            }
            #endregion
        }
    }
}
