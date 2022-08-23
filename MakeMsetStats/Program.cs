using CommandLine;
using khkh_xldMii;
using khkh_xldMii.Utils.Mset;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMsetStats {
    internal class Program {
        [Verb("make")]
        private class MakeOpts {
            [Option("dir-in", Default = @"H:\KH2fm.OpenKH\obj")]
            public string DirIn { get; set; }
        }

        static int Main(string[] args) {
            return Parser.Default.ParseArguments<MakeOpts>(args)
                .MapResult<MakeOpts, int>(
                    RunMake,
                    ex => 1
                );
        }

        private static int RunMake(MakeOpts arg) {
            var files = Directory.GetFiles(arg.DirIn);
            Console.WriteLine($"{files.Length:#,##0} files to be read.");
            foreach (var barFile in files) {
                using (var barStream = File.OpenRead(barFile)) {
                    var mset = new Msetfst(barStream, null);
                    foreach (var motion in mset.motionList) {
                        if (motion.isRaw) {
                            Console.WriteLine(barFile);
                        }
                    }
                }
            }
            return 0;
        }
    }
}
