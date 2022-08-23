using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace ee1Dec.Utils.HexText {
    public class ParseHT {
        public SortedList<int, ResHT> dictres = new SortedList<int, ResHT>();

        public void openHelp(string fp) {
            List<HTBase> alht = new List<HTBase>();
            if (!string.IsNullOrEmpty(fp) && File.Exists(fp)) {
                foreach (string row in File.ReadAllLines(fp)) {
                    STkns st = new STkns(row);
                    if (false) { }
                    else if (st.keys.Length >= 2 && st.keys[1].Equals("desc")) {
                        alht.Add(new HTDesc(HTknUt.parseHexAt(st.keys[0]), st.val));
                    }
                    else if (st.keys.Length >= 2 && st.keys[1].Equals("clr")) {
                        alht.Add(new HTClr(HTknUt.parseHexAt(st.keys[0]), (Color)TypeDescriptor.GetConverter(Color.Empty).ConvertFromString(st.val)));
                    }
                    else if (st.keys.Length >= 2 && st.keys[1].Equals("out")) {
                        alht.Add(new HTOut(HTknUt.parseHexAt(st.keys[0]), st.val));
                    }
                    else if (st.keys.Length >= 2 && st.keys[1].Equals("ov")) {
                        alht.Add(new HTOv(HTknUt.parseHexAt(st.keys[0]), st.val));
                    }
                    else if (st.keys.Length >= 3 && st.keys[2].Equals("mm")) {
                        string[] vals = st.val.Split(':');
                        alht.Add(new HTMM(
                            HTknUt.parseHexAt(st.keys[0]),
                            HTknUt.parseHexAt(st.keys[1]),
                            vals[0],
                            vals.Length >= 2 ? vals[1] : ""
                            ));
                    }
                }
            }
            dictres.Clear();
            foreach (HTBase ht in alht) {
                if (ht is HTBase) {
                    if (dictres.ContainsKey(ht.off) == false) {
                        dictres[ht.off] = new ResHT(ht.off);
                    }
                    ResHT res = dictres[ht.off];
                    if (ht is HTDesc) {
                        res.text = ((HTDesc)ht).text;
                    }
                    if (ht is HTClr) {
                        res.clr = ((HTClr)ht).clr;
                    }
                    if (ht is HTOut) {
                        res.alform.Add(((HTOut)ht).form);
                    }
                    if (ht is HTOv) {
                        res.ov |= 1;
                        res.alovtext.Add(((HTOv)ht).text);
                    }
                }
            }
            foreach (HTBase ht in alht) {
                if (ht is HTMM) {
                    HTMM o = (HTMM)ht;
                    int off = 0;
                    for (int t = o.afrom; t < o.ato; t += 16, off += 16) {
                        if (dictres.ContainsKey(t) == false) {
                            dictres[t] = new ResHT(t);
                        }
                        ResHT res = dictres[t];
                        switch (o.cls) {
                            case "sp":
                                res.memmemo = o.text + string.Format(" (+{0:X2})", o.ato - t);
                                break;
                            case "a":
                                res.memmemo = o.text + string.Format(" (+{0:X2})", off);
                                break;
                        }
                    }
                }
            }
        }

        public ResHT findRes(int off) {
            if (dictres.ContainsKey(off)) {
                return dictres[off];
            }
            return null;
        }

        class HTF2iUt {
            public static ushort F2US(float val) {
                MemoryStream os = new MemoryStream(new byte[] { 0, 0, 0, 0 }, true);
                new BinaryWriter(os).Write(val);
                os.Position = 0;
                return new BinaryReader(os).ReadUInt16();
            }
        }

        class STkns {
            public string[] keys;
            public string val;

            public STkns(string parse) {
                int p = parse.IndexOf('=');
                string lv, rv;
                if (p < 0) {
                    lv = parse;
                    rv = "";
                }
                else {
                    lv = parse.Substring(0, p);
                    rv = parse.Substring(p + 1);
                }

                this.keys = lv.Split(':');
                this.val = rv;
            }
        }
        class HTknUt {
            public static int parseHexAt(string text) {
                if (!text.StartsWith("@")) throw new NotSupportedException(text);
                return Convert.ToInt32(text.Substring(1), 16);
            }
        }
    }
}
