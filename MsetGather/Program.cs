using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MsetGather {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpYa();
                Environment.Exit(1);
            }
            else if (args[0] == "/select") {
                Form form = new Form();
                OpenFileDialog ofdMsets = new OpenFileDialog();
                ofdMsets.Multiselect = true;
                ofdMsets.Filter = "*.mset;*.anb|*.mset;*.anb";
                if (ofdMsets.ShowDialog(form) == DialogResult.OK) {
                    SaveFileDialog sfdMset = new SaveFileDialog();
                    sfdMset.Filter = "*.mset|*.mset";
                    if (sfdMset.ShowDialog(form) == DialogResult.OK) {
                        new Program().Run(sfdMset.FileName, ofdMsets.FileNames);
                    }
                }
            }
            else if (args[0] == "/concat") {
                Form form = new Form();
                SaveFileDialog sfdMset = new SaveFileDialog();
                sfdMset.Filter = "*.mset|*.mset";
                if (sfdMset.ShowDialog(form) == DialogResult.OK) {
                    String[] alMsets = new String[args.Length - 1];
                    Array.Copy(args, 1, alMsets, 0, args.Length - 1);
                    new Program().Run(sfdMset.FileName, alMsets);
                }
            }
            else {
                String[] alMsets = new String[args.Length - 1];
                Array.Copy(args, 1, alMsets, 0, args.Length - 1);
                new Program().Run(args[0], alMsets);
            }
        }

        private void Run(String saveMset, String[] loadMsets) {
            List<Ent> ents = new List<Ent>();
            foreach (String fp in loadMsets) {
                if (!File.Exists(fp)) continue;
                try {
                    using (FileStream fs = File.OpenRead(fp)) {
                        // verify file format
                        if (fs.Length != 0) {
                            BAR bar = new BAR(fs, 0);
                        }
                    }
                }
                catch (Exception) {
                    continue;
                }

                String fext = Path.GetExtension(fp);
                if (String.Compare(fext, ".mset", true) == 0) {
                    using (FileStream fs = File.OpenRead(fp)) {
                        foreach (BarEnt ent in new BAR(fs, 0).ents) {
                            byte[] bin = new byte[ent.Length];
                            fs.Position = ent.Offset;
                            if (fs.Read(bin, 0, bin.Length) != bin.Length) continue;
                            ents.Add(new Ent(bin, ent.Name));
                        }
                    }
                }
                else if (String.Compare(fext, ".anb", true) == 0) {
                    String anicode = "xxxx";
                    Match M = Regex.Match(Path.GetFileName(fp), "#(?<k>[0-9A-Za-z ]{1,4})\\.");
                    if (M.Success)
                        anicode = M.Groups["k"].Value;
                    ents.Add(new Ent(File.ReadAllBytes(fp), anicode));
                }
            }

            using (FileStream os = File.Create(saveMset)) {
                BinaryWriter wr = new BinaryWriter(os);
                wr.Write((byte)'B');
                wr.Write((byte)'A');
                wr.Write((byte)'R');
                wr.Write((byte)1);
                wr.Write(Convert.ToInt32(ents.Count));
                wr.Write((int)0);
                wr.Write((int)1);
                Int64 off = 0x10L + 0x10L * ents.Count;
                foreach (Ent ent in ents) {
                    wr.Write((int)0x11);
                    wr.Write(Encoding.ASCII.GetBytes(ent.key.PadRight(4, '\0').Substring(0, 4)));
                    wr.Write(Convert.ToInt32(off));
                    wr.Write(Convert.ToInt32(ent.bin.Length));
                    off = (off + ent.bin.Length + 15) & (~15);
                }
                byte[] padding = new byte[16];
                foreach (Ent ent in ents) {
                    os.Write(ent.bin, 0, ent.bin.Length);

                    if (0 != (os.Position & 15)) os.Write(padding, 0, 16 - Convert.ToInt32(os.Position & 15));
                }
            }
        }

        class Ent {
            internal byte[] bin;
            internal String key;

            public Ent(byte[] bin, String key) {
                this.bin = bin;
                this.key = key;
            }
        }

        private static void helpYa() {
            Console.Error.WriteLine("MsetGather /select");
            Console.Error.WriteLine("MsetGather /concat input1.mset ...");
            Console.Error.WriteLine("MsetGather output1.mset input1.mset ...");
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
