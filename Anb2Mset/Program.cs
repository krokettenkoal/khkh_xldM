using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Anb2Mset {
    class Program {
        static void helpYa() {
            Console.Error.WriteLine("Anb2mset /select");
            Console.Error.WriteLine("Anb2mset /convertall input1.anb ...");
            Console.Error.WriteLine("Anb2mset input.anb output.mset");
            Environment.Exit(1);
        }

        [STAThread]
        static void Main(string[] args) {
            if (1 <= args.Length && args[0] == "/select") {
                Form form = new Form();
                OpenFileDialog ofdAnb = new OpenFileDialog();
                ofdAnb.Filter = "*.anb|*.anb";
                if (ofdAnb.ShowDialog(form) == DialogResult.OK) {
                    SaveFileDialog sfdMset = new SaveFileDialog();
                    sfdMset.Filter = "*.mset|*.mset";
                    sfdMset.FileName = Path.GetFileNameWithoutExtension(ofdAnb.FileName) + ".mset";
                    if (sfdMset.ShowDialog(form) == DialogResult.OK) {
                        new Program().Run(ofdAnb.FileName, sfdMset.FileName);
                    }
                }
            }
            else if (1 <= args.Length && args[0] == "/convertall") {
                List<string> alAnb = new List<string>();
                for (int x = 1; x < args.Length; x++) {
                    if (File.Exists(args[x])) {
                        try {
                            using (FileStream fs = File.OpenRead(args[x])) {
                                // verify file format
                                BAR bar = new BAR(fs, 0);
                                alAnb.Add(args[x]);
                            }
                        }
                        catch (Exception) {

                        }
                    }
                }
                Form form = new Form();
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Select folder to save " + alAnb.Count + " mset files?";
                fbd.SelectedPath = Application.StartupPath;
                if (fbd.ShowDialog(form) == DialogResult.OK) {
                    foreach (String fpAnb in alAnb) {
                        new Program().Run(fpAnb, Path.Combine(fbd.SelectedPath, Path.GetFileNameWithoutExtension(fpAnb) + ".mset"));
                    }
                }
            }
            else if (2 <= args.Length) {
                new Program().Run(args[0], args[1]);
            }
            else helpYa();
        }

        private void Run(string fpAnb, string fpMset) {
            using (FileStream fs = File.OpenRead(fpAnb)) {
                using (FileStream os = File.Create(fpMset)) {
                    String anicode = "xxxx";
                    Match M = Regex.Match(fpAnb, "#(?<k>[0-9A-Za-z ]{1,4})\\.");
                    if (M.Success)
                        anicode = M.Groups["k"].Value;

                    BinaryWriter wr = new BinaryWriter(os);
                    wr.Write((byte)'B');
                    wr.Write((byte)'A');
                    wr.Write((byte)'R');
                    wr.Write((byte)1);
                    wr.Write((int)1);
                    wr.Write((int)0);
                    wr.Write((int)1);
                    wr.Write((int)0x11);
                    wr.Write(Encoding.ASCII.GetBytes(anicode.PadRight(4, '\0').Substring(0, 4)));
                    wr.Write((int)0x20);
                    wr.Write(Convert.ToInt32(fs.Length));

                    byte[] buff = new byte[Convert.ToInt32(fs.Length)];

                    fs.Position = 0;
                    fs.Read(buff, 0, buff.Length);
                    wr.Write(buff);
                    wr.Close();
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
