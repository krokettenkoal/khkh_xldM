using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Models.Coct {
    public class Co7 {
        public int v00 { get; set; }

        public Co7(BinaryReader br) {
            v00 = br.ReadInt32();
        }

        public override string ToString() {
            return $"{v00:X8}";
        }
    }
}
