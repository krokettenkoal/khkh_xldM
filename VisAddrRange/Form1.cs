using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VisAddrRange {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void doMap_Click(object sender, EventArgs e) {
            var dir1 = @"H:\KH2fm.OpenKH";
            var dir2 = @"H:\KH2fm.yaz0r";

            Func<string, FileInfo> FindFile = file => {
                {
                    var info = new FileInfo(Path.Combine(dir1, file.Trim()));
                    if (info.Exists) {
                        return info;
                    }
                }
                {
                    var info = new FileInfo(Path.Combine(dir2, file.Trim()));
                    if (info.Exists) {
                        return info;
                    }
                }
                return null;
            };

            // S_IEXPA: 00551c40  magic/BLIZZARD_3.mag 
            body.Text = string.Join(
                "\r\n",
                Regex.Matches(body.Text, "S_IEXPA: (?<addr>[0-9a-f]{8})  (?<file>\\S*)", RegexOptions.Multiline)
                    .Cast<Match>()
                    .Select(match => new AddrFile {
                        addr = Convert.ToUInt32(match.Groups["addr"].Value, 16),
                        file = match.Groups["file"].Value,
                    })
                    .Select(set => {
                        set.addrEnd = Convert.ToUInt32(set.addr + (FindFile(set.file)?.Length ?? 0));
                        return set;
                    })
                    .OrderBy(set => set.addr)
            );
        }

        class AddrFile {
            public uint addr;
            public uint addrEnd;
            public string file;

            public override string ToString() => $"{addr:X8}-{addrEnd - 1:X8} {file}";
        }
    }
}
