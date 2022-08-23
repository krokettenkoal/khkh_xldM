using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using khiiMapv.Properties;

namespace khiiMapv.Parse02 {
    public class StrDec {
        public SortedDictionary<int, string> evt = new SortedDictionary<int, string>();
        public SortedDictionary<int, string> sys = new SortedDictionary<int, string>();

        public StrDec() {
            {
                String[] maps = new String[] { Resources.EVTjp0, Resources.EVTjp1, Resources.EVTjp2, Resources.EVTjp3 };
                for (int w = 0; w < 4; w++) {
                    String[] rows = maps[w].Replace("\r\n", "\n").Split('\n');
                    int y = 0;
                    foreach (String row in rows) {
                        String[] cols = row.Split('\t');
                        int x = 0;
                        foreach (String col in cols) {
                            evt[w * 21 * 21 + y * 21 + x] = col;
                            x++;
                        }
                        y++;
                    }
                }
            }
            {
                String[] rows = Resources.SYSjp.Replace("\r\n", "\n").Split('\n');
                int y = 0;
                foreach (String row in rows) {
                    String[] cols = row.Split('\t');
                    int x = 0;
                    foreach (String col in cols) {
                        sys[28 * y + x] = col;
                        x++;
                    }
                    y++;
                }
            }
        }

        public string DecodeSys(byte[] bin) {
            string s = "";
            int off = 0;
            for (int x = 0; x < bin.Length; x++) {
                byte b = bin[x];
                switch (b) {
                    case 0x18: off = 256 * 0; s += "∇"; continue;
                    case 0x19: off = 256 * 1; s += "∇"; continue;
                    case 0x1a: off = 256 * 2; s += "∇"; continue;
                    case 0x1b: off = 256 * 3; s += "∇"; continue;
                    case 0x1c: off = 256 * 4; s += "∇"; continue;
                    case 0x1d: off = 256 * 5; s += "∇"; continue;
                }
                if (b < 0x20) continue;

                s += sys[off + bin[x] - 0x20];
                off = 0;
            }
            return s;
        }

        class BUt {
            internal static byte[] Copy(byte[] bin, int start, int count) {
                byte[] res = new byte[count];
                for (int x = 0; x < count; x++) res[x] = bin[start + x];
                return res;
            }
        }

        public StrCodeCollection DecodeEvt(byte[] bin, int start) {
            StrCodeCollection al = new StrCodeCollection();
            for (int x = start; x < bin.Length; ) {
                byte b = bin[x]; x++;
                if (b == 0) {
                    al.Add(new EndCode(new byte[] { b }));
                    break;
                }
                else if (b == 0x01) {
                    al.Add(new CharCode(" ", new byte[] { b }));
                }
                else if (b == 0x02) {
                    al.Add(new CharCode("\r\n", new byte[] { b }));
                }
                else if (b == 0x03) {
                    al.Add(new Code03(new byte[] { b }));
                }
                else if (b == 0x04) {
                    int cx = 1;
                    al.Add(new Code04(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x06) {
                    int cx = 5;
                    al.Add(new Code06(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x08) {
                    int cx = 3;
                    al.Add(new Code08(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x0D) {
                    al.Add(new VarCode(new byte[] { b }));
                }
                else if (b == 0x10) {
                    al.Add(new WaitCode(new byte[] { b }));
                }
                else if (b == 0x13) {
                    int cx = 4;
                    al.Add(new Code13(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x14) {
                    int cx = 2;
                    al.Add(new Code14(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x15) {
                    int cx = 2;
                    al.Add(new Code15(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b == 0x17) {
                    int cx = 3;
                    al.Add(new Code17(BUt.Copy(bin, x - 1, cx + 1)));
                    x += cx;
                }
                else if (b >= 0x18 && 0x1e >= b) {
                    byte b1 = bin[x]; x++;
                    int off = 256 * (b - 0x18);
                    Debug.Assert(b1 >= 0x20);
                    string s = evt[off + b1 - 0x20];
                    al.Add(new KCode(s, new byte[] { b, b1 }));
                }
                else if (b >= 0x20) {
                    string s = evt[b - 0x20];
                    al.Add(new CharCode(s, new byte[] { b }));
                }
                else {
                    al.Add(new CodeX(new byte[] { b }));
                }
            }
            return al;
        }

        class CodeX : StrCode {
            public CodeX(byte[] bin)
                : base(bin) { }
        }

        class Code06 : StrCode {
            public Code06(byte[] bin)
                : base(bin) { }
        }

        class Code08 : StrCode {
            public Code08(byte[] bin)
                : base(bin) { }
        }

        class Code17 : StrCode {
            public Code17(byte[] bin)
                : base(bin) { }
        }

        class Code15 : StrCode {
            public Code15(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "Select: ";
            }
        }

        class Code14 : StrCode {
            public Code14(byte[] bin)
                : base(bin) { }
        }

        class Code13 : StrCode {
            public Code13(byte[] bin)
                : base(bin) { }
        }

        class Code03 : StrCode {
            public Code03(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "』";
            }
        }

        class Code04 : StrCode {
            public Code04(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "『";
            }
        }

        class WaitCode : StrCode {
            public WaitCode(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "\r\n〆\r\n";
            }
        }

        class VarCode : StrCode {
            public VarCode(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "#";
            }
        }

        class EndCode : StrCode {
            public EndCode(byte[] bin)
                : base(bin) { }

            public override string ToString() {
                return "";
            }
        }

        class KCode : StrCode {
            public string s;

            public KCode(string s, byte[] bin)
                : base(bin) {
                this.s = s;
            }

            public override string ToString() {
                return s;
            }
        }

        class CharCode : StrCode {
            public string s;

            public CharCode(string s, byte[] bin)
                : base(bin) {
                this.s = s;
            }

            public override string ToString() {
                return s;
            }
        }
    }

    public class StrCode {
        public byte[] bin;

        public StrCode(byte[] bin) {
            this.bin = bin;
        }

        public override string ToString() {
            return "<" + String.Join(" ", Array.ConvertAll<byte, string>(bin, delegate(byte b) { return b.ToString("x2"); })) + ">\r\n";
        }
    }

    public class StrCodeCollection : List<StrCode> {
        public override string ToString() {
            string s = "";
            foreach (StrCode o in this) {
                s += o.ToString();
            }
            return s;
        }
    }
}
