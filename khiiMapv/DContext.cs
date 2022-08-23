using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace khiiMapv {
    /// <summary>
    /// Draw Context
    /// </summary>
    public class DContext {
        public String name;
        public M4 o4Map;
        public MTex o7;
        public Parse4Mdlx o4Mdlx;

        public bool initialVisible => name == "MAP" || name.StartsWith("SK");
    }
}
