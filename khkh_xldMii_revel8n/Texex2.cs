using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace khkh_xldMii.Models {
    public class Texex2 {
        public List<TIMBitmap> bitmapList = new List<TIMBitmap>();
        public List<FacePatch> facePatchList = new List<FacePatch>();

        public Texex2() { }

        public Bitmap GetBitmap(int index) {
            if (index < 0 || bitmapList.Count <= index) return null;
            return bitmapList[index].bitmap;
        }

        public Bitmap GetTex2(int w, byte[] al) {
            Bitmap tex = GetBitmap(w);
            if (tex != null) {
                Bitmap pic = (Bitmap)tex.Clone();
                for (int t = 0; t < facePatchList.Count && t < al.Length; t++) {
                    var pc = facePatchList[t];
                    int pi = al[t];
                    if (pi < pc.numVerticalFaces && pc.textureIndex == w) {
                        BitmapData bd = pic.LockBits(new Rectangle(pc.patchX, pc.patchY, pc.patchWidth, pc.patchHeight), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        try {
                            int baseSrc = pc.patchWidth * pc.patchHeight * pi;
                            for (int y = 0; y < pc.patchHeight; y++) {
                                Marshal.Copy(pc.bits, baseSrc + pc.patchWidth * y, new IntPtr(bd.Scan0.ToInt64() + y * bd.Stride), pc.patchWidth);
                            }
                        }
                        finally {
                            pic.UnlockBits(bd);
                        }
                    }
                }
                return pic;
            }
            return tex;
        }

        public Bitmap GetPatchBitmap(int facePatchIndex, int bitmapFrameIndex) {
            if (facePatchIndex < 0 || facePatchList.Count <= facePatchIndex) return null;
            FacePatch pc = facePatchList[facePatchIndex];
            if (bitmapFrameIndex < 0 || pc.numVerticalFaces <= bitmapFrameIndex) return null;
            Bitmap tex = GetBitmap(pc.textureIndex);
            if (tex != null) {
                Bitmap pic = new Bitmap(tex.Width, tex.Height, PixelFormat.Format32bppArgb);
                using (Bitmap tempic = new Bitmap(pc.patchWidth, pc.patchHeight, PixelFormat.Format8bppIndexed)) {
                    BitmapData bd = tempic.LockBits(new Rectangle(0, 0, pc.patchWidth, pc.patchHeight), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    try {
                        int baseSrc = pc.patchWidth * pc.patchHeight * bitmapFrameIndex;
                        for (int y = 0; y < pc.patchHeight; y++) {
                            Marshal.Copy(pc.bits, baseSrc + pc.patchWidth * y, new IntPtr(bd.Scan0.ToInt64() + y * bd.Stride), pc.patchWidth);
                        }
                    }
                    finally {
                        tempic.UnlockBits(bd);
                    }

                    tempic.Palette = tex.Palette;

                    using (Graphics cv = Graphics.FromImage(pic)) {
                        cv.Clear(Color.Empty);
                        cv.DrawImageUnscaled(tempic, pc.patchX, pc.patchY);
                    }
                }
                return pic;
            }
            return null;
        }
    }
}
