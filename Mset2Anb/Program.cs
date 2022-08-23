using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Mset2Anb {
    class Program {
        static void helpYa() {
            Console.Error.WriteLine("Mset2Anb /select");
            Console.Error.WriteLine("Mset2Anb input.mset export_dir");
            Environment.Exit(1);
        }

        [STAThread]
        static void Main(string[] args) {
            if (1 <= args.Length && args[0] == "/select") {
                Form form = new Form();
                OpenFileDialog ofdMset = new OpenFileDialog();
                ofdMset.Filter = "*.mset|*.mset";
                if (ofdMset.ShowDialog(form) == DialogResult.OK) {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Select folder to output:";
                    fbd.SelectedPath = Application.StartupPath;
                    if (fbd.ShowDialog(form) == DialogResult.OK) {
                        new Program().Run(ofdMset.FileName, fbd.SelectedPath);
                    }
                }
            }
            else if (2 <= args.Length) {
                new Program().Run(args[0], args[1]);
            }
            else helpYa();
        }

        private void Run(string fpMset, string dirExp) {
            using (FileStream fs = File.OpenRead(fpMset)) {
                BAR bar1 = new BAR(fs, 0);
                foreach (BarEnt e1 in bar1.ents) {
                    if (e1.Key == 0x11) {
                        byte[] bin = new byte[e1.Length];
                        fs.Position = e1.Offset;
                        fs.Read(bin, 0, bin.Length);

                        String fnanb = Path.GetFileNameWithoutExtension(fpMset) + "#" + e1.Name.TrimEnd('\0') + ".anb";
                        Console.WriteLine("Writing {0}", fnanb);
                        File.WriteAllBytes(Path.Combine(dirExp, fnanb), bin);
                    }
                }
            }
        }

        class BAR {
            public List<BarEnt> ents = new List<BarEnt>();

            public BAR(Stream fs, Int64 off) {
                fs.Position = off;
                BinaryReader br = new BinaryReader(fs);
                if (br.ReadByte() != (byte)'B') throw new InvalidDataException("Not a mset file!");
                if (br.ReadByte() != (byte)'A') throw new InvalidDataException("Not a mset file!");
                if (br.ReadByte() != (byte)'R') throw new InvalidDataException("Not a mset file!");
                if (br.ReadByte() != (byte)001) throw new InvalidDataException("Not a mset file!");
                int cnt = br.ReadInt32();
                br.ReadInt32();
                br.ReadInt32();
                for (int x = 0; x < cnt; x++) {
                    ents.Add(new BarEnt(br, off));
                }
            }
        }
        class BarEnt {
            int k;
            String name;
            Int64 x;
            int cx;

            public Int64 Offset { get { return x; } }
            public int Length { get { return cx; } }
            public String Name { get { return name; } }
            public int Key { get { return k & 0xFFFF; } }

            public BarEnt(BinaryReader br, Int64 off) {
                k = br.ReadInt32();
                name = Encoding.ASCII.GetString(br.ReadBytes(4));
                x = off + br.ReadUInt32();
                cx = Convert.ToInt32(br.ReadUInt32());
            }
        }
    }
}
