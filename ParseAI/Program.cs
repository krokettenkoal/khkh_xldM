using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ParseAI {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("ParseAI 03.bin");
                Environment.Exit(1);
            }

            //new Program().Run(args[0]);
            new Program().BatchConv(@"H:\Proj\kh2hlp\Coll03\bin\Debug\export\03", @"H:\Proj\kh2hlp\Coll03\bin\Debug\export\03\out");
        }

        private void BatchConv(string dirIn, string dirOut) {
            foreach (var inFile in Directory.GetFiles(dirIn, "*.bin")) {
                Console.WriteLine("# " + inFile);
                var writer = new StringWriter();
                try {
                    new Parse03(writer).Run(File.ReadAllBytes(inFile));
                    File.WriteAllText(Path.Combine(dirOut, Path.GetFileNameWithoutExtension(inFile) + ".txt"), writer.ToString());
                }
                catch (InvalidOperationException ex) {
                    Console.Error.WriteLine(ex);
                }
            }
        }

        private void Run(string fpin) {
            new Parse03(Console.Out).Run(File.ReadAllBytes(fpin));
        }
    }
}
