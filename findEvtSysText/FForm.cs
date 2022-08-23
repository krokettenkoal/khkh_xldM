using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using findEvtSysText.Properties;

namespace findEvtSysText {
    public partial class FForm : Form {
        public FForm() {
            InitializeComponent();
        }

        class CharTable {
            public CharTable(String rows, int cx) {
                int y = 0;
                foreach (String row in rows.Replace("\r\n", "\n").Split('\n')) {
                    String[] cols = row.Split('\t');
                    if (cols.Length == cx) {
                        for (int x = 0; x < cols.Length; x++) {
                            if (cols[x].Length == 2) {
                                cols[x + 1] = "" + cols[x][1];
                                cols[x + 0] = "" + cols[x][0];
                            }

                            dict[cols[x]] = cx * y + x;

                            dictRev[cx * y + x] = cols[x];
                        }
                    }
                    y++;
                }
            }

            SortedDictionary<string, int> dict = new SortedDictionary<string, int>();

            public int[] GetPattern(String s) {
                try {
                    int[] ali = new int[s.Length];
                    for (int x = 0; x < s.Length; x++) {
                        ali[x] = dict["" + s[x]];
                    }
                    return ali;
                }
                catch (KeyNotFoundException) {
                    return null;
                }
            }

            SortedDictionary<int, string> dictRev = new SortedDictionary<int, string>();

            public string[] GetTable() {
                string[] list = new string[256];
                for (int x = 32; x < 256; x++) {
                    String s = dictRev.ContainsKey(x - 32) ? dictRev[x - 32] : "＊";
                    if (s == "") s = string.Format("[{0:x2}]", x);
                    list[x] = s;
                }
                return list;
            }
            public string[] GetTable(int map) {
                string[] list = new string[256];
                int baseIndex = 0;
                int count = 0;
                switch (map) {
                    case 0x19: baseIndex += 224; break;
                    case 0x1a: baseIndex += 480; break;
                    case 0x1b: baseIndex += 736; break;
                    case 0x1c: baseIndex += 992; break;
                    case 0x1d: baseIndex += 1248; break;
                    case 0x1e: baseIndex += 1504; break;
                    case 0x1f: baseIndex += 1504 + 256; break;
                }
                for (int x = 0; x < 256; x++) {
                    int k = baseIndex + x;
                    String s = dictRev.ContainsKey(k) ? dictRev[k] : "＊";
                    if (s == "") s = string.Format("[{0:x2} {1:x2}]", map, x);
                    list[x] = s;
                }
                return list;
            }
        }

        class FmtUt {
            public static string Str(int[] ali) {
                String s = "";
                foreach (int i in ali) s += i + " ";
                return s;
            }
        }

        CharTable evt, sys;

        private void bSearch_Click(object sender, EventArgs e) {
            evt = new CharTable(tbEVT.Text, 21);
            sys = new CharTable(tbSYS.Text, 28);

            List<int[]> alai = new List<int[]>();
            alai.Add(evt.GetPattern(cbIn.Text));
            alai.Add(sys.GetPattern(cbIn.Text));

            levt.Text = FmtUt.Str(alai[0] ?? new int[0]);
            lsys.Text = FmtUt.Str(alai[1] ?? new int[0]);

            int cnt = 0;

            StringWriter wr = new StringWriter();
            foreach (String fp in Directory.GetFiles(tbDirin.Text, "*.*", SearchOption.AllDirectories)) {
                using (FileStream fs = File.OpenRead(fp)) {
                    for (int x = 0; x < alai.Count; x++) {
                        if (alai[x] != null) {
                            fs.Position = 0;
                            {
                                List<int> alr = FUt.Find8(fs, alai[x]);
                                if (alr.Count != 0) {
                                    foreach (int pos in alr) {
                                        wr.WriteLine(fp + " " + x + " b " + pos);

                                        cnt++; if (cnt > 100) throw new ArgumentOutOfRangeException("Too much matches!");
                                    }
                                }
                            }
                            {
                                List<int> alr = FUt.Find16(fs, alai[x]);
                                if (alr.Count != 0) {
                                    foreach (int pos in alr) {
                                        wr.WriteLine(fp + " " + x + " w " + pos);

                                        cnt++; if (cnt > 100) throw new ArgumentOutOfRangeException("Too much matches!");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            tbRes.Text = wr.ToString();
        }

        class FUt {
            public static List<int> Find8(Stream si, int[] ali) {
                List<int> alFound = new List<int>();

                int cb1 = 4096;
                int cb2 = cb1 + cb1;
                int cb2Mask = cb2 - 1;
                byte[] buff = new byte[cb2];
                int left = 0;
                int pos = 0;
                bool lower = true;
                int cbPat = ali.Length;
                while (true) {
                    if (left < cbPat) {
                        left += si.Read(buff, lower ? 0 : cb1, cb1);
                        if (left < cbPat)
                            break;
                        lower = !lower;
                    }
                    while (left >= cbPat) {
                        int i0 = ali[0];
                        byte b0 = buff[pos & cb2Mask];
                        int x = 1;
                        for (; x < cbPat; x++) {
                            if (((ali[x] - i0) & 255) != ((buff[(pos + x) & cb2Mask] - b0) & 255))
                                break;
                        }
                        if (x == cbPat) {
                            alFound.Add(pos);
                        }
                        ++pos;
                        --left;
                    }
                }

                return alFound;
            }

            public static List<int> Find16(Stream si, int[] ali) {
                List<int> alFound = new List<int>();

                int cb1 = 4096;
                int cb2 = cb1 + cb1;
                int cb2Mask = cb2 - 1;
                byte[] buff = new byte[cb2];
                int left = 0;
                int pos = 0;
                bool lower = true;
                int cbPat = ali.Length * 2;
                int cntPat = ali.Length;
                while (true) {
                    if (left < cbPat) {
                        left += si.Read(buff, lower ? 0 : cb1, cb1);
                        if (left < cbPat)
                            break;
                        lower = !lower;
                    }
                    while (left >= cbPat) {
                        int i0 = ali[0];
                        int w0 = (buff[pos & cb2Mask]) | (buff[(pos + 1) & cb2Mask] << 8);
                        int x = 1;
                        for (; x < cntPat; x++) {
                            if (((ali[x] - i0) & 65535) != ((((buff[(pos + x + x + 0) & cb2Mask]) | (buff[(pos + x + x + 1) & cb2Mask] << 8)) - w0) & 65535))
                                break;
                        }
                        if (x == cbPat) {
                            alFound.Add(pos);
                        }
                        ++pos;
                        --left;
                    }
                }

                return alFound;
            }
        }

        private void FForm_Load(object sender, EventArgs e) {
            //List<int> alr = FUt.Find8("", alai[x]);
        }

        private void bGenTbl_Click(object sender, EventArgs e) {
#if true
            sys = new CharTable(tbSYS.Text, 28);

            StringWriter writer = new StringWriter();
            String[] table = sys.GetTable(0x19);

            for (int y = 0; y < 10; y++) {
                for (int x = 0; x < 28; x++) {
                    int i = 28 * y + x;
                    if (i < table.Length) {
                        writer.Write("'{0}', ", table[i]);
                    }
                }
                writer.WriteLine();
            }
            tbRes.Text = writer.ToString();
#else
            evt = new CharTable(tbEVT.Text, 21);
            StringWriter writer = new StringWriter();
#if true
            String[] table = evt.GetTable();

            for (int x = 0x82; x <= 0xff; x++) {
                writer.WriteLine("            [0x{0:x2}] = new TextCmdModel('{1}'),", x, table[x]);
            }
#else
            String[] table = evt.GetTable(0x1f);

            for (int y = 0; y < 10; y++) {
                for (int x = 0; x < 28; x++) {
                    int i = 28 * y + x;
                    if (i < table.Length) {
                        writer.Write("'{0}', ", table[i]);
                    }
                }
                writer.WriteLine();
            }
#endif
            tbRes.Text = writer.ToString();
#endif
        }

        private void bRead_Click(object sender, EventArgs e) {
            byte[] bin = Resources.es_02;

            evt = new CharTable(tbEVT.Text, 21);
            String[] map = evt.GetTable();
            String[] map19 = evt.GetTable(0x19);
            String[] map1a = evt.GetTable(0x1a);
            String[] map1b = evt.GetTable(0x1b);
            String[] map1c = evt.GetTable(0x1c);
            String[] map1d = evt.GetTable(0x1d);
            String[] map1e = evt.GetTable(0x1e);
            String[] map1f = evt.GetTable(0x1f);

            MemoryStream stringListStream = new MemoryStream(bin, false);
            BinaryReader stringListReader = new BinaryReader(stringListStream);

            if (stringListReader.ReadInt32() != 1) throw new NotSupportedException("Not zero-one!");
            int count = stringListReader.ReadInt32();

            StringWriter wr = new StringWriter();
            for (int x = 0; x < count; x++) {
                stringListStream.Position = 8 + 8 * x;
                int off1 = stringListReader.ReadInt32();
                int off2 = stringListReader.ReadInt32();

                String textOut = "";
                stringListStream.Position = off2;
                while (true) {
                    byte b = stringListReader.ReadByte();
                    if (b == 0) {
                        break;
                    }
                    if (32 <= b) {
                        textOut += map[b];
                    }
                    else if (b == 0x19) {
                        b = stringListReader.ReadByte();
                        textOut += map19[b];
                    }
                    else if (b == 0x1a) {
                        b = stringListReader.ReadByte();
                        textOut += map1a[b];
                    }
                    else if (b == 0x1b) {
                        b = stringListReader.ReadByte();
                        textOut += map1b[b];
                    }
                    else if (b == 0x1c) {
                        b = stringListReader.ReadByte();
                        textOut += map1c[b];
                    }
                    else if (b == 0x1d) {
                        b = stringListReader.ReadByte();
                        textOut += map1d[b];
                    }
                    else if (b == 0x1e) {
                        b = stringListReader.ReadByte();
                        textOut += map1e[b];
                    }
                    else if (b == 0x1f) {
                        b = stringListReader.ReadByte();
                        textOut += map1f[b];
                    }
                    else if (b == 0x14) {
                        byte b1 = stringListReader.ReadByte();
                        byte b2 = stringListReader.ReadByte();
                        textOut += string.Format("[{0:x2} {1:x2} {2:x2}]", b, b1, b2);
                    }
                    else if (b == 0x09 || b == 0x13 || b == 0x0f) {
                        byte b1 = stringListReader.ReadByte();
                        textOut += string.Format("[{0:x2} {1:x2}]", b, b1);
                    }
                    else if (b == 4) {
                        textOut += "<緑色>";
                    }
                    else if (b == 3) {
                        textOut += "<通常>";
                    }
                    else if (b == 2) {
                        textOut += "\r\n";
                    }
                    else if (b == 1) {
                        textOut += "　";
                    }
                    else if (b == 16) {
                        textOut += "\r\n〆\r\n";
                    }
                    else {
                        textOut += string.Format("<{0:x2}>", b);
                    }
                    //else throw new NotSupportedException("charcode: " + b.ToString("x2"));
                }

                wr.WriteLine("Str{0:000} Num {1:x8} Off {2:x8}", x, off1, off2);
                wr.WriteLine(textOut);
                wr.WriteLine();
            }

            tbOuta.Text = wr.ToString();
        }

    }
}