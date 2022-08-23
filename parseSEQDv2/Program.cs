using Autofac;
using CommandLine;
using khiiFileFmts.Models.COCT;
using parseSEQDv2.AutofacModules;
using parseSEQDv2.Models.Unbar;
using parseSEQDv2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace parseSEQDv2 {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static int Main(string[] args) {
            if (args.Length == 0) {
                args = new string[] { "app" }; // Hack for default launching.
            }
            var builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();
            return Parser.Default.ParseArguments<AppOptions, UnbarOpts, BarOpts, UnlaydOpts, LaydOpts, UncoctOpts, DummyLaydOpts>(args)
                .MapResult(
                    (AppOptions o) => {
                        using (var container = builder.Build()) {
                            FreeConsole();
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(container.Resolve<Form1>());
                        }
                        return 0;
                    },
                    (UnbarOpts unbar) => {
                        using (var container = builder.Build()) {
                            var fileIn = Path.Combine(Environment.CurrentDirectory, unbar.InFile);
                            var outDir = Path.Combine(Environment.CurrentDirectory, unbar.OutDir);
                            Directory.CreateDirectory(outDir);

                            var model = new BarYaml();
                            model.BarItems = ReadBar.Explode(new MemoryStream(File.ReadAllBytes(fileIn), false))
                                .Select(
                                    (entry, index) => {
                                        var item = new BarYaml.Item {
                                            Id = entry.id,
                                            Key = entry.key,
                                            File = $"{entry.id}.{DecideExtensionFromId(entry.key)}",
                                        };
                                        File.WriteAllBytes(
                                            Path.Combine(outDir, item.File),
                                            entry.bin
                                        );
                                        return item;
                                    }
                                )
                                .ToArray();

                            File.WriteAllText(
                                Path.Combine(outDir, "bar.yml"),
                                container.ResolveNamed<ISerializer>("yamlSer")
                                    .Serialize(model)
                                );
                            return 0;
                        }
                    },
                    (BarOpts pack) => {
                        using (var container = builder.Build()) {
                            var yamlFile = Path.Combine(Environment.CurrentDirectory, pack.InFile);
                            var baseDir = Path.GetDirectoryName(yamlFile);

                            var model = container.ResolveNamed<IDeserializer>("yamlDe")
                                .Deserialize<BarYaml>(File.ReadAllText(yamlFile));

                            var barStream = model.PackBar(baseDir);

                            var outFile = Path.Combine(Environment.CurrentDirectory, pack.OutFile);

                            Directory.CreateDirectory(Path.GetDirectoryName(outFile));

                            using (var fs = File.Create(outFile)) {
                                barStream.Position = 0;
                                barStream.CopyTo(fs);
                            }

                            File.WriteAllBytes(pack.OutFile, barStream.ToArray());
                            return 0;
                        }
                    },
                    (UnlaydOpts unpack) => {
                        using (var container = builder.Build()) {
                            var fileIn = Path.Combine(Environment.CurrentDirectory, unpack.InFile);
                            var outDir = Path.Combine(Environment.CurrentDirectory, unpack.OutDir);
                            Directory.CreateDirectory(outDir);

                            var bin = File.ReadAllBytes(fileIn);

                            var layd = new Layd(null, bin, "");
                            {
                                for (int x = 0; x < layd.ListSeqdOffset.Count; x++) {
                                    var offset = layd.ListSeqdOffset[x];
                                    var seqdBinSeg = new ArraySegment<byte>(bin, offset, bin.Length - offset);
                                    var seqd = new Seqd(null, seqdBinSeg, "", "");
                                    layd.SeqdList.Add(seqd);
                                }
                            }

                            var text = container.ResolveNamed<ISerializer>("yamlSer")
                                .Serialize(layd);
                            File.WriteAllText(Path.Combine(outDir, "layd.yml"), text);
                            return 0;
                        }
                    },
                    (LaydOpts pack) => {
                        using (var container = builder.Build()) {
                            var yamlFile = Path.Combine(Environment.CurrentDirectory, pack.InFile);
                            var baseDir = Path.GetDirectoryName(yamlFile);

                            var model = container.ResolveNamed<IDeserializer>("yamlDe")
                                .Deserialize<Layd>(File.ReadAllText(yamlFile));

                            var laydStream = model.PackLayd();

                            var outFile = Path.Combine(Environment.CurrentDirectory, pack.OutFile);

                            Directory.CreateDirectory(Path.GetDirectoryName(outFile));

                            File.WriteAllBytes(outFile, laydStream.ToArray());
                            return 0;
                        }
                    },
                    (UncoctOpts opts) => {
                        using (var container = builder.Build()) {
                            var fileIn = Path.Combine(Environment.CurrentDirectory, opts.InFile);
                            var fileOut = Path.Combine(Environment.CurrentDirectory, opts.OutFile);
                            Directory.CreateDirectory(Path.GetDirectoryName(fileOut));

                            var bin = File.ReadAllBytes(fileIn);
                            var stream = new MemoryStream(bin);
                            var reader = new BinaryReader(stream);

                            var pack = new CoctPack(reader);

                            var text = container.ResolveNamed<ISerializer>("yamlSer")
                                .Serialize(pack);
                            File.WriteAllText(fileOut, text);
                            return 0;
                        }
                    },
                    (DummyLaydOpts opts) => {
                        using (var container = builder.Build()) {
                            var fileOut = Path.Combine(Environment.CurrentDirectory, opts.OutFile);
                            Directory.CreateDirectory(Path.GetDirectoryName(fileOut));

                            var data = new DummyLaydCreator().layd;

                            var text = container.ResolveNamed<ISerializer>("yamlSer")
                                .Serialize(data);
                            File.WriteAllText(fileOut, text);
                            return 0;
                        }
                    },
                    ex => 1
                );
        }

        private static string DecideExtensionFromId(int key) {
            switch (key) {
                case 0x03:
                    return "ai";
                case 0x04:
                    return "vif";
                case 0x05:
                    return "doct";
                case 0x06:
                    return "coct-06";
                case 0x07:
                    return "tex";
                case 0x0b:
                    return "coct-0b";
                case 0x0f:
                    return "coct-0f";
                case 0x12:
                    return "pax";
                case 0x18:
                    return "imd";
                case 0x19:
                    return "seqd";
                case 0x1c:
                    return "layd";
                case 0x1d:
                    return "imz";
                default:
                    return $"bin-{key:X2}";
            }
        }

        [Verb("pack", HelpText = "Create seqd binary")]
        public class PackOptions {

        }

        [Verb("unbar", HelpText = "Unpack bar")]
        public class UnbarOpts {
            [Option('i', "barInputFile", Required = true)]
            public string InFile { get; set; }

            [Option('d', "outDir", Required = true)]
            public string OutDir { get; set; }
        }

        [Verb("unlayd", HelpText = "Unpack layd")]
        public class UnlaydOpts {
            [Option('i', "laydInputFile", Required = true)]
            public string InFile { get; set; }

            [Option('d', "outDir", Required = true)]
            public string OutDir { get; set; }
        }

        [Verb("bar", HelpText = "Pack bar")]
        public class BarOpts {
            [Option('i', "yamlInputFile", Required = true)]
            public string InFile { get; set; }

            [Option('o', "barOutputFile", Required = true)]
            public string OutFile { get; set; }
        }

        [Verb("layd", HelpText = "Pack layd")]
        public class LaydOpts {
            [Option('i', "yamlInputFile", Required = true)]
            public string InFile { get; set; }

            [Option('o', "laydOutputFile", Required = true)]
            public string OutFile { get; set; }
        }

        [Verb("uncoct", HelpText = "Unpack coct")]
        public class UncoctOpts {
            [Option('i', "coctInputFile", Required = true)]
            public string InFile { get; set; }

            [Option('o', "yamlOutputFile", Required = true)]
            public string OutFile { get; set; }
        }

        [Verb("dummylayd", HelpText = "Create dummy layd.yml")]
        public class DummyLaydOpts {
            [Option('o', "yamlOutputFile", Required = true)]
            public string OutFile { get; set; }
        }

        [Verb("app")]
        public class AppOptions {

        }

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
