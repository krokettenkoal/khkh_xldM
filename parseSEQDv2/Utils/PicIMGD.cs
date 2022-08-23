using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class PicIMGD {
        public Bitmap Bitmap { get; }
        public string Id { get; }

        public PicIMGD(Bitmap bitmap, string id) {
            this.Bitmap = bitmap;
            this.Id = id;
        }

        public override string ToString() => $"{Id} {Bitmap.Width}x{Bitmap.Height}";
    }
}
