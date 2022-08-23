using SlimDX;
using khkh_xldMii.Mc;

namespace khkh_xldMii.Mc {
    public class AxBone {
        public int cur, parent, v08, v0c;
        public float x1, y1, z1, w1; // S
        public float x2, y2, z2, w2; // R
        public float x3, y3, z3, w3; // T

        public AxBone Clone() {
            return (AxBone)MemberwiseClone();
        }
    }
}
namespace khkh_xldMii.Mx {
    public class T31 {
        public int off, len;
        public int postbl3;
        public List<T11> al11 = new();
        public List<T12> al12 = new();
        public List<T13vif> al13 = new();
        public T21? t21;
        public T32? t32;

        public Matrix[]? Ma; // skeleton base pose
        public Matrix[]? Minv; // skeleton inverse transforms

        public T31(int off, int len, int postbl3) {
            this.off = off;
            this.len = len;
            this.postbl3 = postbl3;
        }
    }

    public class T11 {
        public int off, len;
        public int c1;

        public T11(int off, int len, int c1) {
            this.off = off;
            this.len = len;
            this.c1 = c1;
        }
    }

    public class T12 {
        public int off, len;
        public int c1;

        public T12(int off, int len, int c1) {
            this.off = off;
            this.len = len;
            this.c1 = c1;
        }
    }

    public class T13vif {
        public int off, len;
        public int texi;
        public int[] alaxi;
        public byte[] bin;

        public T13vif(int off, int len, int texi, int[] alaxi, byte[] bin) {
            this.off = off;
            this.len = len;
            this.texi = texi;
            this.alaxi = alaxi;
            this.bin = bin;
        }
    }

    public class T21 {
        public int off, len;
        public List<AxBone> alaxb = new();

        public T21(int off, int len) {
            this.off = off;
            this.len = len;
        }
    }

    public class T32 {
        public int off, len;

        public T32(int off, int len) {
            this.off = off;
            this.len = len;
        }
    }

    internal class RUtil {
        public static int RoundUpto16(int val) {
            return (val + 15) & (~15);
        }
    }

    internal class UtilAxBoneReader {
        public static AxBone read(BinaryReader br) {
            var o = new AxBone {
                cur = br.ReadInt32(),
                parent = br.ReadInt32(),
                v08 = br.ReadInt32(),
                v0c = br.ReadInt32(),
                x1 = br.ReadSingle(),
                y1 = br.ReadSingle(),
                z1 = br.ReadSingle(),
                w1 = br.ReadSingle(),
                x2 = br.ReadSingle(),
                y2 = br.ReadSingle(),
                z2 = br.ReadSingle(),
                w2 = br.ReadSingle(),
                x3 = br.ReadSingle(),
                y3 = br.ReadSingle(),
                z3 = br.ReadSingle(),
                w3 = br.ReadSingle()
            };
            return o;
        }
    }

    public class Mdlxfst {
        public List<T31> alt31 = new();

        public Mdlxfst(Stream fs) {
            var br = new BinaryReader(fs);
            var aler = new Queue<int>();
            aler.Enqueue(0x90);
            var postbl3 = 0;
            while (aler.Count != 0) {
                var eroff = aler.Dequeue();
                fs.Position = eroff + 0x10;
                int cnt2 = br.ReadUInt16();
                fs.Position = eroff + 0x1C;
                int cnt1 = br.ReadUInt16();

                T31 t31;
                alt31.Add(t31 = new T31(eroff, 0x20 * (1 + cnt1), postbl3)); postbl3++;

                for (int c1 = 0; c1 < cnt1; c1++) {
                    fs.Position = eroff + 0x20u + 0x20u * c1 + 0x10u;
                    int pos1 = br.ReadInt32() + eroff;
                    int pos2 = br.ReadInt32() + eroff;
                    fs.Position = eroff + 0x20 + 0x20 * c1 + 0x04;
                    int texi = br.ReadInt32();
                    fs.Position = pos2;
                    int cnt1a = br.ReadInt32();
                    t31.al11.Add(new T11(pos2, RUtil.RoundUpto16(4 + 4 * cnt1a), c1));
                    var alv1 = new List<int>(cnt1a);
                    for (int c1a = 0; c1a < cnt1a; c1a++) alv1.Add(br.ReadInt32());

                    var aloffDMAtag = new List<int>();
                    var alaxi = new List<int[]>();
                    var alaxref = new List<int>();
                    aloffDMAtag.Add(pos1);
                    fs.Position = pos1 + 16;
                    for (int c1a = 0; c1a < cnt1a; c1a++) {
                        if (alv1[c1a] == -1) {
                            aloffDMAtag.Add((int)fs.Position + 0x10);
                            fs.Position += 0x20;
                        }
                        else {
                            fs.Position += 0x10;
                        }
                    }
                    for (int c1a = 0; c1a < cnt1a; c1a++) {
                        if (c1a + 1 == cnt1a) {
                            alaxref.Add(alv1[c1a]);
                            alaxi.Add(alaxref.ToArray()); alaxref.Clear();
                        }
                        else if (alv1[c1a] == -1) {
                            alaxi.Add(alaxref.ToArray()); alaxref.Clear();
                        }
                        else {
                            alaxref.Add(alv1[c1a]);
                        }
                    }

                    int pos1a = (int)fs.Position;
                    t31.al12.Add(new T12(pos1, pos1a - pos1, c1));

                    int tpos = 0;
                    foreach (int offDMAtag in aloffDMAtag) {
                        fs.Position = offDMAtag;
                        // Source Chain Tag
                        int qwcsrc = (br.ReadInt32() & 0xFFFF);
                        int offsrc = (br.ReadInt32() & 0x7FFFFFFF) + eroff;

                        fs.Position = offsrc;
                        byte[] bin = br.ReadBytes(16 * qwcsrc);
                        T13vif t13;
                        t31.al13.Add(t13 = new T13vif(offsrc, 16 * qwcsrc, texi, alaxi[tpos], bin)); tpos++;
                    }
                }

                fs.Position = eroff + 0x14;
                int off2 = br.ReadInt32();
                if (off2 != 0) {
                    off2 += eroff;
                    int len2 = 0x40 * cnt2;
                    t31.t21 = new T21(off2, len2);

                    // matrices used for skinning
                    t31.Ma = new Matrix[cnt2];
                    t31.Minv = new Matrix[cnt2];

                    fs.Position = off2;
                    for (int x = 0; x < cnt2/*len2 / 0x40*/; x++)
                    {
                        AxBone b = UtilAxBoneReader.read(br);

                        // get base transform matrix
                        t31.Ma[x] =
                            Matrix.Scaling(b.x1, b.y1, b.z1) *
                            Matrix.RotationX(b.x2) *
                            Matrix.RotationY(b.y2) *
                            Matrix.RotationZ(b.z2);
                        t31.Ma[x].M41 = b.x3;
                        t31.Ma[x].M42 = b.y3;
                        t31.Ma[x].M43 = b.z3;

                        // update hierarchy transform
                        if (b.parent >= 0)
                        {
                            t31.Ma[x] *= t31.Ma[b.parent];
                        }
                        // calculate inverse transform
                        t31.Minv[x] = Matrix.Invert(t31.Ma[x]);

                        t31.t21.alaxb.Add(b);
                    }
                }

                fs.Position = eroff + 0x18;
                int off4 = br.ReadInt32();
                if (off4 != 0) {
                    off4 += eroff;
                    int len4 = off2 - off4;
                    t31.t32 = new T32(off4, len4);
                }

                fs.Position = eroff + 0xC;
                int off3 = br.ReadInt32();
                if (off3 != 0) {
                    off3 += eroff;
                    aler.Enqueue(off3);
                }
            }
        }
    }
}
