using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WalkeeGPr {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            t1();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GForm());
        }

        private static void t1() {
            List<string> al = new List<string>();
            al.Add("(offMset.mhdr)+84");
            al.Add("(Getw(((offMset)+(0))+4))+16");
            al.Add("(((Getw(((offMset)+(0))+20))+((Gethw(((Getw((Getw((Getw(((InfoTbl)+(0))+20))+0))+32))+(((Getw(((offMset.mhdr)+(Getw((offMset.mhdr)+28)))+0))&0xffff)<<6))+4))<<4))+0)+(12)");
            foreach (String s in al) {
                ELeaf leaf = new CExpr(s).v;
            }
        }
    }
}