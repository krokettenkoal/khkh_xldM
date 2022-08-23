using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SlimDX;
using System.Diagnostics;
using System.Drawing;
using vcBinTex4;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using parsePAX_.Properties;
using vwBinTex2;

namespace Prayvif1 {
    public class BPyWriter {
        public TextWriter wr = new StringWriter();

        public BPyWriter() {
            wr.WriteLine(parsePAX_.Properties.Resources.Base);
        }

        public BPyWriter(StreamWriter wr) {
            this.wr = wr;

            wr.WriteLine(parsePAX_.Properties.Resources.Base);
            wr.WriteLine("mat = MyMats()");

            {
                Bitmap pic = new Bitmap(32, 32);
                using (Graphics cv = Graphics.FromImage(pic)) {
                    cv.Clear(Color.White);
                }
                String fp = Path.GetFullPath("white.png");
                pic.Save(fp, ImageFormat.Png);

                wr.WriteLine("mat.AddImage('{0}', '{1}', '{2}', {3})"
                    , String.Format("tex{0:0000}", dummyi)
                    , String.Format("mat{0:0000}", dummyi)
                    , fp.Replace("\\", "\\\\").Replace("'", "\\'")
                    , "False"
                    );
                dummyi++;

                wr.WriteLine("mat.AddImage('{0}', '{1}', '{2}', {3})"
                    , String.Format("tex{0:0000}", dummyi)
                    , String.Format("mat{0:0000}", dummyi)
                    , fp.Replace("\\", "\\\\").Replace("'", "\\'")
                    , "True"
                    );
                dummyi++;
            }
        }

        int whiteioff = 0, whiteion = 1;

        public class V {
            public Vector3 v = Vector3.Zero;
            public Vector2 t = Vector2.Zero;
            public Color clr = Color.White;

            public V() { }

            public V(Vector3 v, Vector2 t, Color c) {
                this.v = v;
                this.t = t;
                this.clr = c;
            }
        }

        V curv = new V();

        List<V> alv = new List<V>();

        public IList<V> Alv { get { return alv.AsReadOnly(); } }

        public class Tri {
            public int v0, v1, v2;

            public Tri(int v0, int v1, int v2) {
                this.v0 = v0;
                this.v1 = v1;
                this.v2 = v2;
            }
        }

        List<Tri> alt = new List<Tri>();

        public IList<Tri> Alt { get { return alt.AsReadOnly(); } }

        public void SetSTQ(float S, float T, float Q) {
            curv.t = new Vector2(S / Q, T / Q);
        }

        public void SetUV(float U, float V) {
            curv.t = new Vector2(U / texcx, V / texcy);
        }

        int texcx { get { return texsel == null ? 1 : texsel.size.Width; } }
        int texcy { get { return texsel == null ? 1 : texsel.size.Height; } }

        int pric = 0;

        public void SetXYZF(float X, float Y, float Z, bool stay) {
            curv.v = new Vector3(X, Y, Z);
            alv.Add(curv);
            Color lastclr = curv.clr;
            curv = new V();
            curv.clr = lastclr;

            if (stay) {
                pric = 0;
            }
            else {
                int cnt = alv.Count;
                switch (prty) {
                    case Prty.Tri:
                        if (cnt < 3) break;
                        alt.Add(new Tri(cnt - 1, cnt - 2, cnt - 3));
                        break;
                    case Prty.TriStrip:
                        if (cnt < 3) break;
                        if (0 == (pric & 1)) {
                            alt.Add(new Tri(cnt - 2, cnt - 3, cnt - 1));
                        }
                        else {
                            alt.Add(new Tri(cnt - 1, cnt - 3, cnt - 2));
                        }
                        break;
                    case Prty.Spr: {
                            if (cnt < 2) break;
                            Vector3 v0 = alv[cnt - 1].v;
                            Vector3 v3 = alv[cnt - 2].v;
                            Vector3 v1 = new Vector3(v3.X, v0.Y, v0.Z);
                            Vector3 v2 = new Vector3(v0.X, v3.Y, v0.Z);

                            Vector2 t0 = alv[cnt - 1].t;
                            Vector2 t3 = alv[cnt - 2].t;
                            Vector2 t1 = new Vector2(t3.X, t0.Y);
                            Vector2 t2 = new Vector2(t0.X, t3.Y);

                            alv.Add(new V(v0, t0, alv[cnt - 1].clr));
                            alv.Add(new V(v1, t1, alv[cnt - 1].clr));
                            alv.Add(new V(v2, t2, alv[cnt - 1].clr));
                            alv.Add(new V(v3, t3, alv[cnt - 1].clr));

                            cnt = alv.Count;

                            alt.Add(new Tri(cnt - 1, cnt - 2, cnt - 3));
                            alt.Add(new Tri(cnt - 4, cnt - 3, cnt - 2));
                            break;
                        }
                }
                pric++;
            }
        }

        public void SetRGBAQ(byte R, byte G, byte B, byte A) {
            curv.clr = Color.FromArgb(Math.Min(255, 2 * A), R, G, B);
        }

        enum Prty {
            Point = 0, Line = 1, LineStrip = 2, Tri = 3, TriStrip = 4, TriFan = 5, Spr = 6, X = 7,
        }

        Prty prty;
        bool isUV = false;
        bool isABE = false;
        bool isTex = false;

        public void StartPrimitive(byte pty, bool isUV, bool isABE, bool isTex) {
            this.prty = (Prty)pty;
            this.isUV = isUV;
            this.isABE = isABE;
            this.isTex = isTex;
        }

        String name;

        public void StartMesh(String name) {
            this.name = name;

            alv.Clear();
            alt.Clear();
            curv = new V();
            mati = 0;

            wr.WriteLine("# -- {0}", name);
            wr.WriteLine("ya = MyMesh()");
            wr.WriteLine("ya.PrepareMesh('{0}')", name);

            SelTex();
        }

        String GetGiftHash() {
            return String.Format("{0},{1},{2},{3},{4},{5},{6}"
                , gift.TW
                , gift.TH
                , gift.PSM
                , gift.TBW
                , gift.CBP
                , gift.CPSM
                , gift.CSA
                );
        }

        class Pic {
            public int pici;
            public Size size;
            public String fp;
            public String giftHash;

            public Pic(int pici, Size size, String fp, String giftHash) {
                this.pici = pici;
                this.size = size;
                this.fp = fp;
                this.giftHash = giftHash;
            }

            public override string ToString() {
                return String.Format("{0}, ({1}, {2}), {3}", pici, size.Width, size.Height, fp);
            }
        }

        class Mat {
            public int mati, geti;
            public Pic pic;

            public Size size { get { return pic.size; } }
            public String fp { get { return pic.fp; } }

            public Mat(int mati, int geti, Pic pic) {
                this.mati = mati;
                this.geti = geti;
                this.pic = pic;
            }
        }
        struct Matkey : IComparable<Matkey> {
            public int pici;
            public bool isABE;

            public Matkey(int pici, bool isABE) {
                this.pici = pici;
                this.isABE = isABE;
            }

            #region IComparable<Matkey> ÉÅÉìÉo

            public int CompareTo(Matkey r) {
                int t;
                if (0 != (t = pici.CompareTo(r.pici))) return t;
                if (0 != (t = isABE.CompareTo(r.isABE))) return t;
                return 0;
            }

            #endregion
        }

        SortedDictionary<Matkey, Mat> dictMat = new SortedDictionary<Matkey, Mat>();

        int pici = 0;
        int mati = 0;
        int dummyi = 0;
        Mat texsel = null;

        private void SelTex() {
            texsel = null;

            if (!isTex) {
                int i = isABE ? whiteion : whiteioff;
                wr.WriteLine("ya.AddMat(mat.GetMat({0}))", i);
                return;
            }

            Pic picsel = null;

            int TBP0 = gift.TBP0;
            if (dictDBP2Mat.ContainsKey(TBP0)) {
                picsel = dictDBP2Mat[TBP0];

                if (picsel.giftHash != GetGiftHash())
                    picsel = null;
            }

            if (picsel == null) {
                Bitmap pic = null;
                int cx = 1 << gift.TW;
                int cy = 1 << gift.TH;
                if (gift.PSM == PSMT8 && gift.CPSM == PSMCT32) {
                    {
                        byte[] bin = Reform8.Decode8c(gsram, cx, cy, 256 * TBP0, gift.TBW / 2);
                        pic = BUt.Make8(cx, cy, bin);
                    }
                    {
                        byte[] bin = Reform32.Decode32c(gsram, 16, 16, 256 * gift.CBP, 0);
                        byte[] palbin = new byte[1024];
                        KHcv8pal_v2.repl(bin, 0, palbin, 0);
                        BUt.SelPal(pic, palbin, 256);
                    }
                }
                else if (gift.PSM == PSMT4 && gift.CPSM == PSMCT32) {
                    {
                        byte[] bin = Reform4.Decode4c(gsram, cx, cy, 256 * TBP0, gift.TBW / 2);
                        pic = BUt.Make4(cx, cy, bin);
                    }
                    {
                        byte[] bin = Reform32.Decode32c(gsram, 8, 2, 256 * gift.CBP + 64 * gift.CSA, 0);
                        byte[] palbin = new byte[64];
                        KHcv4pal_v2.repl(bin, 0, palbin, 0);
                        BUt.SelPal(pic, palbin, 16);
                    }
                }

                if (pic != null) {
                    String fn = String.Format("pic{0:000}.png", pici);
                    pic.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
                    dictDBP2Mat[TBP0] = picsel = new Pic(pici, pic.Size, Path.GetFullPath(fn), GetGiftHash());
                    pici++;
                }
                else {
                    return;
                }
            }

            Matkey k = new Matkey(picsel.pici, isABE);
            if (dictMat.ContainsKey(k) == false) {
                texsel = new Mat(mati, dummyi, picsel);

                wr.WriteLine("mat.AddImage('{0}', '{1}', '{2}', {3})"
                    , String.Format("tex{0:0000}", dummyi)
                    , String.Format("mat{0:0000}", dummyi)
                    , texsel.fp.Replace("\\", "\\\\").Replace("'", "\\'")
                    , isABE ? "True" : "False"
                    );

                dictMat[k] = texsel;

                mati++;
                dummyi++;
            }

            texsel = dictMat[k];
        }

        class BUt {
            public static Bitmap Make8(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                ColorPalette pal = pic.Palette;
                for (int x = 0; x < 256; x++) {
                    pal.Entries[x] = Color.FromArgb(255, x, x, x);
                }
                pic.Palette = pal;

                return pic;
            }

            public static Bitmap Make32(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format32bppArgb);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                return pic;
            }

            public static Bitmap Make4(int cx, int cy, byte[] bin) {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bd = pic.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);

                try {
                    int buffSize = bd.Stride * bd.Height;
                    Marshal.Copy(bin, 0, bd.Scan0, Math.Min(bin.Length, buffSize));
                }
                finally {
                    pic.UnlockBits(bd);
                }

                return pic;
            }

            public static void SelPal(Bitmap pic, byte[] bin, int cnt) {
                ColorPalette pal = pic.Palette;
                for (int x = 0; x < cnt; x++) {
                    pal.Entries[x] = Color.FromArgb(
                        Math.Min(255, bin[4 * x + 3] << 1),
                        bin[4 * x + 0],
                        bin[4 * x + 1],
                        bin[4 * x + 2]
                        );
                }
                pic.Palette = pal;
            }
        }

        readonly static float fact = 1.0f / 50;

        public void EndMesh() {
            if (alv.Count == 0 || alt.Count == 0) {
                wr.WriteLine();
                return;
            }

            if (texsel != null) {
                wr.WriteLine("ya.AddMat(mat.GetMat({0}))", texsel.geti);
            }

            wr.WriteLine("ya.AddCoords([");
            foreach (V v in alv) {
                Vector3 v3 = v.v;
                v3.X = (+v3.X - 2048) * fact;
                v3.Y = (-v3.Y + 2048) * fact;
                v3.Z = (v3.Z / 10000);

                wr.WriteLine("\t" + "[{0,10},{1,10},{2,10}],", v3.X, v3.Y, v3.Z);
            }
            wr.WriteLine("\t" + "])");

            wr.WriteLine("ya.AddColorUvMatFaces([");
            // faces
            foreach (Tri t in alt) {
                wr.WriteLine("\t" + "[{0,3},{1,3},{2,3}],", t.v0, t.v1, t.v2);
            }
            //
            wr.WriteLine("\t" + "],[");
            // facecolors
            foreach (Tri t in alt) {
                wr.WriteLine("\t" + "[{0},{1},{2}],"
                    , FUt.Format(alv[t.v0].clr)
                    , FUt.Format(alv[t.v1].clr)
                    , FUt.Format(alv[t.v2].clr)
                    );
            }
            //
            wr.WriteLine("\t" + "],[");
            // faceuvs
            foreach (Tri t in alt) {
                wr.WriteLine("\t" + "[{0},{1},{2}],"
                    , FUt.Format(alv[t.v0].t)
                    , FUt.Format(alv[t.v1].t)
                    , FUt.Format(alv[t.v2].t)
                    );
            }
            //
            wr.WriteLine("\t" + "],[");
            // faceMatImgs
            foreach (Tri t in alt) {
                if (texsel == null) {
                    wr.WriteLine("\t" + "None,");
                }
                else {
                    wr.WriteLine("\t" + "[{0}, mat.GetImage({1})],"
                        , texsel.mati
                        , texsel.geti
                        );
                }
            }
            //
            wr.WriteLine("\t" + "])");

            wr.WriteLine("ya.MeshToOb('{0}')", name);
            wr.WriteLine();
            wr.WriteLine();
        }

        class FUt {
            public static string Format(Color c) {
                return string.Format("[{0,3},{1,3},{2,3},{3,3}]", c.A, c.R, c.G, c.B);
            }
            public static string Format(Vector2 v) {
                return string.Format("[{0,10},{1,10}]", v.X, 1 - v.Y);
            }
        }

        byte[] gsram = new byte[4 * 1024 * 1024];

        const byte PSMCT32 = 0, PSMT8 = 19, PSMT4 = 20;

        SortedDictionary<int, Pic> dictDBP2Mat = new SortedDictionary<int, Pic>();

        public GIFt gift = null;

        public void SendIMG(MemoryStream si) {
            byte[] src = si.ToArray();

            dictDBP2Mat.Remove(gift.DBP);

            if (gift.DPSM == PSMT8) {
                Trace.Assert(gift.SSAX == 0, "SSAX is " + gift.SSAX);
                Trace.Assert(gift.SSAY == 0, "SSAY is " + gift.SSAY);

                Reform8.Encode8b(src, gsram, gift.RRW, gift.RRH, gift.DDAX, gift.DDAY, 256 * gift.DBP, gift.DBW / 2);
            }
            else if (gift.DPSM == PSMT4) {
                Trace.Assert(gift.SSAX == 0, "SSAX is " + gift.SSAX);
                Trace.Assert(gift.SSAY == 0, "SSAY is " + gift.SSAY);

                Reform4.Encode4b(src, gsram, gift.RRW, gift.RRH, gift.DDAX, gift.DDAY, 256 * gift.DBP, gift.DBW / 2);
            }
            else if (gift.DPSM == PSMCT32) {
                Trace.Assert(gift.SSAX == 0, "SSAX is " + gift.SSAX);
                Trace.Assert(gift.SSAY == 0, "SSAY is " + gift.SSAY);

                Reform32.Encode32b(src, gsram, gift.RRW, gift.RRH, gift.DDAX, gift.DDAY, 256 * gift.DBP, gift.DBW);
            }
            else Debug.Fail("DPSM is " + gift.DPSM);
        }
    }
}
