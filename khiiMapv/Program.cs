using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace khiiMapv {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] ala) {
            String fpread = null;
            foreach (String fp in ala) {
                if (File.Exists(fp)) {
                    fpread = fp;
                    break;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RDForm(fpread));
        }
    }
}