using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ffe4kh2dump {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SelForm());
        }
    }
}