using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace khkh_xldMii {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormII());
            }
            catch (Exception err) {
                String s = "Sorry.\n\nThis viewer stopped due to program error.\n\nHere is a bit information of error.\n\nPress Ok to halt.\n---\n" + err;
                MessageBox.Show(s, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static string fBPxml { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BindPreset.xml"); } }

        public static string fSPtext { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SearchFolders.txt"); } }
    }
}