using System;
using System.Collections.Generic;
using System.Text;

namespace kh2map2b249 {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpya(); Environment.Exit(1);
            }
            if (args[0] == "/select") {
            }
            else if (args.Length == 2) {
                new Program().Run(args[0], args[1]);
            }
            return;
        }

        private void Run(String fpin, String fppy) {

        }

        private static void helpya() {
            Console.Error.WriteLine("kh2map2b249 input.map output.py");
            Console.Error.WriteLine("kh2map2b249 /select");
        }
    }
}
