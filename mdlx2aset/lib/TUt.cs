using SlimDX.Direct3D9;
using System.Drawing;
using System.Drawing.Imaging;

namespace mdlx2aset.Model {
    internal class TUt {
        public static Texture FromBitmap(Device device, Bitmap p) {
            MemoryStream os = new MemoryStream();
            p.Save(os, ImageFormat.Png);
            os.Position = 0;
            return Texture.FromStream(device, os);
        }
    }
}
