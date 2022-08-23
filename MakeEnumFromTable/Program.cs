using MakeEnumFromTable.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MakeEnumFromTable {
    internal class Program {
        static void Main(string[] args) {
            var pairs = new Dictionary<string, string>();
            File.WriteAllText("Range.txt", MakeEnum("RangeTriggerID", Resources.Range, pairs));
            File.WriteAllText("Frame.txt", MakeEnum("FrameTriggerID", Resources.Frame, pairs));
        }

        private static string MakeEnum(string name, string body, Dictionary<string, string> already) {
            var writer = new StringWriter();
            writer.WriteLine("typedef enum <byte> {");
            var pairs = new Dictionary<string, string>();
            foreach (var line in body.Replace("\r\n", "\n").Trim().Split('\n')) {
                var cells = line.Split('\t');
                if (cells.Length == 2) {
                    var key = ToVarName(cells[1]);
                    var value = cells[0];

                    if (already.ContainsKey(key) || pairs.ContainsKey(key)) {
                        key = key + value;
                    }
                    pairs[key] = value;
                    already[key] = value;
                }
            }
            foreach (var pair in pairs) {
                writer.WriteLine($"    {pair.Key} = {pair.Value},");
            }
            writer.WriteLine("} " + name + ";");
            return writer.ToString();
        }

        private static string ToVarName(string name) {
            return Regex.Replace(name, "[^0-9A-Za-z_]+", "").Trim();
        }
    }
}
