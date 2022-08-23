using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;
using System.Drawing;
using System.IO;

namespace hex04BinTrack.Utils {
    public class TexUtil {
        public static Texture FromBitmap(Device device, Bitmap pic, Usage usage, Pool pool) {
            using (MemoryStream os = new MemoryStream()) {
                pic.Save(os, System.Drawing.Imaging.ImageFormat.Png);
                os.Seek(0, SeekOrigin.Begin);
                return Texture.FromStream(device, os, usage, pool);
            }
        }
    }
}
