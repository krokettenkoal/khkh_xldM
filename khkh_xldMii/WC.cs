using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace khkh_xldMii {
    public class WC : IDisposable {
        public WC() {
            Cursor.Current = Cursors.WaitCursor;
        }

        #region IDisposable ƒƒ“ƒo

        public void Dispose() {
            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
