using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Lib03.Models;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ParseAI {
    public class Parse03 {
        MemoryStream si;
        BinaryReader br;
        TextWriter writer;

        public Parse03(TextWriter wri) {
            this.writer = wri;
        }

        public void Run(byte[] bin) {
            si = new MemoryStream(bin, false);
            br = new BinaryReader(si);

            {
                if (si.Length < 1) return;

                si.Position = 0;
                writer.WriteLine("---");
                writer.WriteLine("#Parseai 20210104");
                writer.WriteLine("name: \"{0}\"", Ut.Read0Str(br));

                if (si.Length < 16 + 12) return;

                si.Position = 16;
                int v0 = br.ReadInt32();
                int v4 = br.ReadInt32();
                int v8 = br.ReadInt32();
                writer.WriteLine("workSize: {0}", v0);
                writer.WriteLine("stackSize: {0}", v4);
                writer.WriteLine("termSize: {0}", v8);
            }

            writer.WriteLine("triggers:");
            for (int x = 0; ; x++) {
                si.Position = 0x1c + 8 * x;
                int key = br.ReadInt32();
                int off = br.ReadInt32();
                if (key == 0 && off == 0) break;
                writer.WriteLine("- key: {0}", key);
                writer.WriteLine("  entry: 'K{0}'", key);
            }

            writer.WriteLine("---");
            writer.WriteLine(";Format: label: ai-code ; ai-decimal-off real-hex-off ");

            for (int x = 0; ; x++) {
                si.Position = 0x1c + 8 * x;
                int key = br.ReadInt32();
                int off = br.ReadInt32();
                if (key == 0 && off == 0) break;
                Walk(key, off);
            }

            int firstPos = (int)(si.Position / 2) - 8;

            DecodeTailStr();
            Gen(firstPos);

            writer.WriteLine("===");
        }

        private void DecodeTailStr() {
            int endPos = -1;
            var len = (int)si.Length;
            for (int x = 0; x < si.Length; x++) {
                si.Position = len - x - 1;
                int c = si.ReadByte();
                if (c == 0) {
                    if (endPos != -1) {
                        var off = (len - x) / 2 - 8;
                        labels.Add(new Label { pos = off, name = $"S{off:0000}", isString = true });
                        endPos = -1;
                    }
                }
                else {
                    if (c < 0x20 || c >= 0x7f) {
                        // contains invalid char
                        break;
                    }
                    if (endPos == -1) {
                        endPos = x;
                    }
                }
            }
        }

        private void Gen(int x) {
            int cx = (int)(si.Length / 2) - 8;
            var labelLookup = labels.ToLookup(it => it.pos);
            var errorLookup = errors.ToLookup(it => it.Item1, it => it.Item2);
            var labelPoints = new int[0]
                .Concat(labels.Select(it => it.pos))
                .Concat(errors.Select(it => it.Item1))
                .ToArray();
            for (; x < cx;) {
                bool isString = false;
                foreach (var label in labelLookup[x]) {
                    writer.WriteLine("{0}:", label.name);
                    isString |= label.isString;
                }
                if (errorLookup[x].Any(it => it == null)) {
                    writer.WriteLine(" ; Can't reach here!");
                }
                Dis dis;
                if (dict.TryGetValue(x, out dis)) {
                    writer.WriteLine(" {0,-40}; {1,4} {2:x}", dis.Desc, x, 0x10 + 2 * x);

                    foreach (var error in errorLookup[x]) {
                        writer.WriteLine(" ; " + error);
                    }

                    x += dis.NumBytes / 2;
                }
                else {
                    si.Position = 16 + 2 * x;
                    if (isString) {
                        var text = " db '";
                        while (true) {
                            byte[] pair = new byte[] { (byte)si.ReadByte(), (byte)si.ReadByte(), };
                            text += Encoding.ASCII.GetString(pair);
                            x++;
                            if (x >= cx || labelPoints.Contains(x)) {
                                break;
                            }
                        }
                        text = Regex.Replace(text, "[\\x00-\\x1f\\x80-\\xff]", match => $"\\x{(int)match.Value[0]:X2}");
                        text += "'";
                        writer.WriteLine(text);
                    }
                    else {
                        int t = 0;
                        while (true) {
                            if (t == 0 || 0 == (x & 7)) {
                                if (t != 0) {
                                    writer.WriteLine();
                                }
                                writer.Write(" dw ");
                            }
                            else {
                                writer.Write(", ");
                            }
                            writer.Write($"0x{br.ReadUInt16():X4}");
                            x++;
                            t++;
                            if (x >= cx || labelPoints.Contains(x)) {
                                break;
                            }
                        }
                        writer.WriteLine();
                    }
                }
            }
        }

        class Label {
            public int pos;
            public string name;
            public bool isString;
        }

        SortedDictionary<int, Dis> dict = new SortedDictionary<int, Dis>();
        List<Tuple<int, string>> errors = new List<Tuple<int, string>>();
        List<Label> labels = new List<Label>();

        class Dis {
            public int NumBytes = 0;
            public string Desc = string.Empty;

            public override string ToString() => $"{Desc}";

            public Dis(int numBytes, String s) {
                this.NumBytes = numBytes;
                this.Desc = s;
            }
        }

        class Ut {
            public static string Read0Str(BinaryReader br) {
                String s = "";
                while (true) {
                    int v = br.ReadByte();
                    if (v == 0)
                        break;
                    s += (char)v;
                }
                return s;
            }
        }

        private void Walk(int key, int off) {
            Queue<int> ofsQueue = new Queue<int>();
            ofsQueue.Enqueue(off);
            labels.Add(new Label { pos = off, name = "K" + key });
            var pcode = PCode.Loader.Value;
            var maxPos = (int)(si.Length - 16) / 2;
            while (ofsQueue.Count != 0) {
                int nextoff = ofsQueue.Dequeue();
                while (true) {
                    off = nextoff;

                    if (dict.ContainsKey(off)) break;

                    var fileOff = 0x10 + off * 2;
                    si.Position = fileOff;

                    try {
                        int v0 = br.ReadUInt16();
                        int opc = v0 & 15;
                        int sub = (v0 >> 4) & 3;
                        var ssub = v0 >> 6;

                        Instr instr;
                        try {
                            instr = pcode.Instr.Single(
                                it => it.opcode == opc
                                && (it.sub == -1 || it.sub == sub)
                                && (it.ssub == -1 || it.ssub == ssub)
                            );
                        }
                        catch (InvalidOperationException) {
                            errors.Add(new Tuple<int, string>(off, $"Unknown opc {v0} sub {sub} ssub {ssub}"));
                            break;
                        }

                        var numWords = 1;
                        var args = new List<string>();
                        foreach (var arg in instr?.Arg ?? new Arg[0]) {
                            int value;
                            switch (arg.type) {
                                case "ssub": {
                                        value = ssub;
                                        break;
                                    }
                                case "imm16": {
                                        value = br.ReadInt16();
                                        numWords++;
                                        break;
                                    }
                                case "imm32": {
                                        value = br.ReadInt32();
                                        numWords += 2;
                                        break;
                                    }
                                default: throw new NotSupportedException();
                            }
                            if (arg.aiPos != 0) {
                                if (instr.jump != 0 || instr.gosub != 0) {
                                    var jumpTo = off + numWords + value;
                                    Debug.Assert(jumpTo < maxPos);
                                    ofsQueue.Enqueue(jumpTo);
                                    args.Add(GenLabel(jumpTo, true));
                                }
                                else {
                                    args.Add(GenLabel(value, false));
                                }
                            }
                            else {
                                args.Add($"{value}");
                            }
                        }

                        if (instr.syscall != 0) {
                            var arg = int.Parse(args[0]);
                            try {
                                var syscall = pcode.Syscall.Single(
                                    it => it.tableIdx == ssub && it.funcIdx == arg
                                );
                                dict[off] = new Dis(2 * numWords, syscall.name);
                            }
                            catch (InvalidOperationException) {
                                dict[off] = new Dis(2 * numWords, instr.name + " " + string.Join(" ", args));
                            }
                        }
                        else {
                            dict[off] = new Dis(2 * numWords, instr.name + " " + string.Join(" ", args));
                        }

                        if (instr.neverReturn != 0 || (instr.jump != 0 && instr.conditional == 0) || instr.gosubRet != 0) {
                            // break loop
                        }
                        else {
                            nextoff = off + numWords;
                        }
                    }
                    catch (EndOfStreamException) {
                        errors.Add(new Tuple<int, string>(off, null));
                        break;
                    }
                }
            }
        }

        private string GenLabel(int newoff, bool code) {
            var label = code ? $"L{newoff:0000}" : $"D{newoff:0000}";
            if (!labels.Any(it => it.name == label)) {
                labels.Add(new Label { pos = newoff, name = label });
            }
            return label;
        }
    }
}
