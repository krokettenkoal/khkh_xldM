using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    public class CPUState {
        public Stream fs = null;
        public int tick = 0;
        public uint pc = 0;
        public uint opc = 0;
    }
}
