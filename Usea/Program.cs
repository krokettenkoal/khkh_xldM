using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;

namespace Usea {
    class Program {

        static void Main(string[] args) {
            new Program().Run();
        }

        void Run() {
            testf012ba74();
        }

        private void testf012ba74() {
            Matrix M = Matrix.RotationAxis(new Vector3(0, 0, 1), 45.0f / 180.0f * 3.14159f);
            Matrix R1 = new f012ba74().Calc(M);
            Matrix R2 = Matrix.Invert(M);
            Console.WriteLine("in"); FMUt.Print(M);
            Console.WriteLine("R1"); FMUt.Print(R1);
            Console.WriteLine("R2"); FMUt.Print(R2);
        }

        class FMUt {
            public static void Print(Matrix M) {
                string fmt = ",15";
                Console.WriteLine("|{0" + fmt + "}|{1" + fmt + "}|{2" + fmt + "}|{3" + fmt + "}|", M.M11, M.M12, M.M13, M.M14);
                Console.WriteLine("|{0" + fmt + "}|{1" + fmt + "}|{2" + fmt + "}|{3" + fmt + "}|", M.M21, M.M22, M.M23, M.M24);
                Console.WriteLine("|{0" + fmt + "}|{1" + fmt + "}|{2" + fmt + "}|{3" + fmt + "}|", M.M31, M.M32, M.M33, M.M34);
                Console.WriteLine("|{0" + fmt + "}|{1" + fmt + "}|{2" + fmt + "}|{3" + fmt + "}|", M.M41, M.M42, M.M43, M.M44);
            }
        }

        class f012bd98 {
            public Vector4 vf5o, vf6o, vf7o, vf8o;

            public void Calc(Vector4 vf1, Vector4 vf2, Vector4 vf3, Vector4 vf4, Vector4 vf5, Vector4 vf6, Vector4 vf7, Vector4 vf8) {
                vf5o.X = (vf1.X * vf5.X + vf2.X * vf5.Y + vf3.X * vf5.Z + vf4.X * vf5.W);
                vf5o.Y = (vf1.Y * vf5.X + vf2.Y * vf5.Y + vf3.Y * vf5.Z + vf4.Y * vf5.W);
                vf5o.Z = (vf1.Z * vf5.X + vf2.Z * vf5.Y + vf3.Z * vf5.Z + vf4.Z * vf5.W);
                vf5o.W = (vf1.W * vf5.X + vf2.W * vf5.Y + vf3.W * vf5.Z + vf4.W * vf5.W);

                vf6o.X = (vf1.X * vf6.X + vf2.X * vf6.Y + vf3.X * vf6.Z + vf4.X * vf6.W);
                vf6o.Y = (vf1.Y * vf6.X + vf2.Y * vf6.Y + vf3.Y * vf6.Z + vf4.Y * vf6.W);
                vf6o.Z = (vf1.Z * vf6.X + vf2.Z * vf6.Y + vf3.Z * vf6.Z + vf4.Z * vf6.W);
                vf6o.W = (vf1.W * vf6.X + vf2.W * vf6.Y + vf3.W * vf6.Z + vf4.W * vf6.W);

                vf7o.X = (vf1.X * vf7.X + vf2.X * vf7.Y + vf3.X * vf7.Z + vf4.X * vf7.W);
                vf7o.Y = (vf1.Y * vf7.X + vf2.Y * vf7.Y + vf3.Y * vf7.Z + vf4.Y * vf7.W);
                vf7o.Z = (vf1.Z * vf7.X + vf2.Z * vf7.Y + vf3.Z * vf7.Z + vf4.Z * vf7.W);
                vf7o.W = (vf1.W * vf7.X + vf2.W * vf7.Y + vf3.W * vf7.Z + vf4.W * vf7.W);

                vf8o.X = (vf1.X * vf8.X + vf2.X * vf8.Y + vf3.X * vf8.Z + vf4.X * vf8.W);
                vf8o.Y = (vf1.Y * vf8.X + vf2.Y * vf8.Y + vf3.Y * vf8.Z + vf4.Y * vf8.W);
                vf8o.Z = (vf1.Z * vf8.X + vf2.Z * vf8.Y + vf3.Z * vf8.Z + vf4.Z * vf8.W);
                vf8o.W = (vf1.W * vf8.X + vf2.W * vf8.Y + vf3.W * vf8.Z + vf4.W * vf8.W);
            }

            public static Matrix Calc(Matrix M1, Matrix M2) {
                f012bd98 o = new f012bd98();
                // M2Ç™ì]íuçsóÒÇ∆Ç»Ç¡ÇƒÇ¢ÇÈâ¬î\ê´Ç™óLÇÈÅBê≥MtxÅ~ì]íuMtxÅ®ê≥Mtx
                o.Calc(
                    new Vector4(M1.M11, M1.M12, M1.M13, M1.M14),
                    new Vector4(M1.M21, M1.M22, M1.M23, M1.M24),
                    new Vector4(M1.M31, M1.M32, M1.M33, M1.M34),
                    new Vector4(M1.M41, M1.M42, M1.M43, M1.M44),

                    new Vector4(M2.M11, M2.M12, M2.M13, M2.M14),
                    new Vector4(M2.M21, M2.M22, M2.M23, M2.M24),
                    new Vector4(M2.M31, M2.M32, M2.M33, M2.M34),
                    new Vector4(M2.M41, M2.M42, M2.M43, M2.M44)
                    );

                Matrix R = new Matrix();
                R.M11 = o.vf5o.X; R.M12 = o.vf5o.Y; R.M13 = o.vf5o.Z; R.M14 = o.vf5o.W;
                R.M21 = o.vf6o.X; R.M22 = o.vf6o.Y; R.M23 = o.vf6o.Z; R.M24 = o.vf6o.W;
                R.M31 = o.vf7o.X; R.M32 = o.vf7o.Y; R.M33 = o.vf7o.Z; R.M34 = o.vf7o.W;
                R.M41 = o.vf8o.X; R.M42 = o.vf8o.Y; R.M43 = o.vf8o.Z; R.M44 = o.vf8o.W;
                return R;
            }
        }

        class f012bd14 {
            /// <summary>
            /// çsóÒÇÃì]íuÇ∆ÅCvf4Ç∆çsóÒÇÃä|éZÇé¿é{Ç∑ÇÈÇÊÇ§Ç≈Ç†ÇÈ
            /// </summary>
            /// <param name="M"></param>
            /// <param name="vf4"></param>
            /// <returns></returns>
            public Matrix Calc(Matrix M, ref Vector4 vf4) {
                Vector4 Va = new Vector4(M.M11, M.M12, M.M13, M.M14);
                Vector4 Vb = new Vector4(M.M21, M.M22, M.M23, M.M24);
                Vector4 Vc = new Vector4(M.M31, M.M32, M.M33, M.M34);

                Vector4 vf1 = new Vector4(Va.X, Va.Y, Va.Z, 0);
                Vector4 vf2 = new Vector4(Vb.X, Vb.Y, Vb.Z, 0);
                Vector4 vf3 = new Vector4(Vc.X, Vc.Y, Vc.Z, 0);

                vf4 = new Vector4(
                    vf1.X * vf4.X + vf2.X * vf4.Y + vf3.X * vf4.Z,
                    vf1.Y * vf4.X + vf2.Y * vf4.Y + vf3.Y * vf4.Z,
                    vf1.Z * vf4.X + vf2.Z * vf4.Y + vf3.Z * vf4.Z,
                    vf4.W
                    );
                vf4.X = -vf4.X;
                vf4.Y = -vf4.Y;
                vf4.Z = -vf4.Z;

                Matrix R = new Matrix();
                R.M11 = Va.X; R.M12 = Va.Y; R.M13 = Va.Z; R.M14 = Va.W;
                R.M21 = Vb.X ; R .M22 = Vb.Y; R.M23 = Vb.Z; R.M24 = Vb.W;
                R.M31 = Vc.X; R.M32 = Vc.Y; R.M33 = Vc.Z; R.M34 = Vc.W;
                return R;
            }
        }

        class f012ba74 {
            Vector4 acc = Vector4.Empty;
            Vector4 vf0 = new Vector4(0, 0, 0 , 1);
            Vector4 vf1;
            Vector4 vf2;
            Vector4 vf3;
            Vector4 vf4;
            Vector4 vf5, vf6, vf7;
            Vector4 vf8;
            float Q;

            /// <summary>
            /// ãtçsóÒÇÃåvéZÇ≈Ç†Ç¡ÇΩÇÊÇ§Ç≈Ç†ÇÈ
            /// </summary>
            /// <param name="M"></param>
            /// <returns></returns>
            public Matrix Calc(Matrix M) {
                // 012ba74-
                vf1 = new Vector4(M.M11, M.M12, M.M13, M.M14);
                vf2 = new Vector4(M.M21, M.M22, M.M23, M.M24);
                vf3 = new Vector4(M.M31, M.M32, M.M33, M.M34);
                vf4 = new Vector4(M.M41, M.M42, M.M43, M.M44);
                vf5 = Cross(vf2, vf3, vf5.W);
                vf6 = Cross(vf3, vf1, vf6.W);
                vf7 = Cross(vf1, vf2, vf7.W);

                vf8 = new Vector4(vf1.X * vf5.X, vf1.Y * vf5.Y, vf1.Z * vf5.Z, vf8.W);
                vf1 = new Vector4(vf4.X * vf5.X, vf4.Y * vf5.Y, vf4.Z * vf5.Z, vf1.W);
                vf2 = new Vector4(vf4.X * vf6.X, vf4.Y * vf6.Y, vf4.Z * vf6.Z, vf2.W);
                vf3 = new Vector4(vf4.X * vf7.X, vf4.Y * vf7.Y, vf4.Z * vf7.Z, vf3.W);

                vf8.X += vf8.Y;
                vf1.X += vf1.Y;
                vf2.Y += vf2.X;
                vf3.Z += vf3.X;
                vf8.X += vf8.Z;
                vf4.X = vf1.X + vf1.Z;
                vf4.Y = vf2.Y + vf2.Z;
                vf4.Z = vf3.Z + vf3.Y;

                Q = vf0.W / vf8.X;

                vf1 = new Vector4(vf5.X, vf6.X, vf7.X, vf0.X);
                vf2 = new Vector4(vf5.Y, vf6.Y, vf7.Y, vf0.Y);
                vf3 = new Vector4(vf5.Z, vf6.Z, vf7.Z, vf0.Z);

                vf4.X = -vf4.X;
                vf4.Y = -vf4.Y;
                vf4.Z = -vf4.Z;

                vf1.Multiply(Q);
                vf2.Multiply(Q);
                vf3.Multiply(Q);
                vf4 = new Vector4(vf4.X * Q, vf4.Y * Q, vf4.Z * Q, vf4.W);

                Matrix R = new Matrix();
                R.M11 = vf1.X; R.M12 = vf1.Y; R.M13 = vf1.Z; R.M14 = vf1.W;
                R.M21 = vf2.X; R.M22 = vf2.Y; R.M23 = vf2.Z; R.M24 = vf2.W;
                R.M31 = vf3.X; R.M32 = vf3.Y; R.M33 = vf3.Z; R.M34 = vf3.W;
                R.M41 = vf4.X; R.M42 = vf4.Y; R.M43 = vf4.Z; R.M44 = vf4.W;
                return R;
            }

            private Vector4 Cross(Vector4 fs, Vector4 ft, float w) {
                Vector3 v = Vector3.Cross(new Vector3(fs.X, fs.Y, fs.Z), new Vector3(ft.X, ft.Y, ft.Z));
                return new Vector4(v.X, v.Y, v.Z, w);
            }

            private Vector4 VOPMSUB(Vector4 fs, Vector4 ft) {
                Vector4 v = new Vector4(
                    acc.X - fs.Y * ft.Z,
                    acc.Y - fs.Z * ft.X,
                    acc.Z - fs.X * ft.Y,
                    0
                    );
                return v;
            }

            private Vector4 VOPMULA(Vector4 fs, Vector4 ft) {
                Vector4 v = new Vector4(
                    fs.Y * ft.Z,
                    fs.Z * ft.X,
                    fs.X * ft.Y,
                    0
                    );
                return v;
            }
        }
    }
}
