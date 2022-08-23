using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    public class Csvw {
        private readonly TextWriter writer;
        private int x = 0;

        public Csvw(TextWriter writer) {
            this.writer = writer;
        }

        public void Write(string key) {
            ++x;
            if (x != 1) {
                writer.Write(",");
            }
            writer.Write("\"" + key.Replace("\"", "\"\"") + "\"");
        }

        public void NextRecord() {
            x = 0;
            writer.WriteLine();
        }
    }
}
