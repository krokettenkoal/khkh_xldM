using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    static class HexDump {
        internal static string Print(byte[] bin, string linePrefix) {
            int pos = 0;
            var writer = new StringWriter();
            for (int y = 0; pos < bin.Length; y++) {
                writer.Write(linePrefix);
                for (int x = 0; x < 16 && pos < bin.Length; x++, pos++) {
                    writer.Write($"{bin[pos]:X2} ");
                }
                writer.WriteLine();
            }
            return writer.ToString();
        }
    }
}
