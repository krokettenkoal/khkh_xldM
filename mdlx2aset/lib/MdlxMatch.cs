using System.Text.RegularExpressions;

namespace mdlx2aset.Utils {
    internal class MdlxMatch {
        public static string FindMset(string fmdlx) {
            string s1 = Path.ChangeExtension(fmdlx, ".mset");
            if (File.Exists(s1)) return s1;
            string s2 = Regex.Replace(s1, "_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
            if (File.Exists(s2)) return s2;
            string s4 = Regex.Replace(s1, "_[a-z]+(_[a-z]+\\.)", "$1", RegexOptions.IgnoreCase);
            if (File.Exists(s4)) return s4;
            string s3 = Regex.Replace(s1, "_[a-z]+_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
            if (File.Exists(s3)) return s3;
            return s1;
        }
        public static bool HasMset(string fmdlx) => !string.IsNullOrEmpty(fmdlx) && File.Exists(FindMset(fmdlx));
    }
}
