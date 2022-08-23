using System;
using System.Collections.Generic;
using System.Text;

namespace ee1Dec.Utils {
    class JxxUt {
        public static bool isJxx(string inst) {
            return "#BEQ#BEQL#BGEZ#BGEZL#BGTZ#BGTZL#BLEZ#BLEZL#BLTZ#BLTZ#BNE#BNEL#BC0F#BC0FL#BC0T#BC0TL#BC1F#BC1FL#BC1T#BC1TL#BC2F#BC2FL#BC2T#BC2TL#J#JR#BGEZAL#BGEZALL#BLTZAL#BLTZALL#JAL#JALR#".IndexOf("#" + inst + "#") >= 0;
        }
    }
}
