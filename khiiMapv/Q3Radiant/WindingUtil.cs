using SlimDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace khiiMapv.Q3Radiant {
    public class WindingUtil {
        public static List<Vector3> MakeFaceWinding(IEnumerable<Plane> brushFaces, Plane face) {
            List<Vector3> w;
            bool past;

            // get a poly that covers an effectively infinite area
            w = BaseForPlane(face);

            // chop the poly by all of the other faces
            past = false;
            foreach (Plane temp in brushFaces) {
                if (w == null) {
                    break;
                }
                var clip = temp;

                if (clip == face) {
                    past = true;
                    continue;
                }
                if (Vector3.Dot(face.Normal, clip.Normal) > 0.999
                    && Math.Abs(face.D - clip.D) < 0.01) { // identical plane, use the later one
                    if (past) {
                        return null;
                    }
                    continue;
                }

                // flip the plane, because we want to keep the back side
                var plane = new Plane(-clip.Normal, -clip.D);

                w = Clip(w, plane, false);
                if (null == w)
                    return w;
            }

            if (w.Count < 3) {
                w = null;
            }

            if (null == w)
                Debug.WriteLine("unused plane");

            return w;
        }

        const int BOGUS_RANGE = 18000;

        public static List<Vector3> BaseForPlane(Plane p) {
            int i, x;
            float max, v;
            Vector3 org, vright, vup;
            List<Vector3> w = null;

            // find the major axis

            max = -BOGUS_RANGE;
            x = -1;
            for (i = 0; i < 3; i++) {
                v = Math.Abs(p.Normal[i]);
                if (v > max) {
                    x = i;
                    max = v;
                }
            }
            if (x == -1)
                throw new ApplicationException("no axis found");

            vup = new Vector3();
            switch (x) {
                case 0:
                case 1:
                    vup[2] = 1;
                    break;
                case 2:
                    vup[0] = 1;
                    break;
            }


            v = Vector3.Dot(vup, p.Normal);
            vup = vup - p.Normal * v;
            vup.Normalize();

            org = p.Normal * p.D;

            vright = Vector3.Cross(vup, p.Normal);

            vup = vup * BOGUS_RANGE;
            vright = vright * BOGUS_RANGE;

            // project a really big	axis aligned box onto the plane
            w = new List<Vector3>(4);
            w.Add(org - vright + vup);
            w.Add(org + vright + vup);
            w.Add(org + vright - vup);
            w.Add(org - vright - vup);

            return w;
        }

        const int SIDE_FRONT = 0;
        const int SIDE_ON = 2;
        const int SIDE_BACK = 1;
        const int SIDE_CROSS = -2;

        const int MAX_POINTS_ON_WINDING = 64;

        const float ON_EPSILON = 0.01f;

        public static List<Vector3> Clip(List<Vector3> @in, Plane split, bool keepon) {
            float[] dists = new float[MAX_POINTS_ON_WINDING];
            int[] sides = new int[MAX_POINTS_ON_WINDING];
            int[] counts = new int[3];
            float dot;
            int i, j;
            Vector3 mid = new Vector3();
            List<Vector3> neww;

            counts[0] = counts[1] = counts[2] = 0;

            // determine sides for each point
            for (i = 0; i < @in.Count; i++) {
                dot = Vector3.Dot(@in[i], split.Normal);
                dot -= (float)split.D;
                dists[i] = dot;
                if (dot > ON_EPSILON)
                    sides[i] = SIDE_FRONT;
                else if (dot < -ON_EPSILON)
                    sides[i] = SIDE_BACK;
                else {
                    sides[i] = SIDE_ON;
                }
                counts[sides[i]]++;
            }
            sides[i] = sides[0];
            dists[i] = dists[0];

            if (keepon && 0 == counts[0] && 0 == counts[1])
                return @in;

            if (0 == counts[0]) {
                return null;
            }
            if (0 == counts[1])
                return @in;

            neww = new List<Vector3>(@in.Count + 4);

            for (i = 0; i < @in.Count; i++) {
                var p1 = @in[i];

                if (sides[i] == SIDE_ON) {
                    neww.Add(p1);
                    continue;
                }

                if (sides[i] == SIDE_FRONT) {
                    neww.Add(p1);
                }

                if (sides[i + 1] == SIDE_ON || sides[i + 1] == sides[i])
                    continue;

                // generate a split point
                var p2 = @in[(i + 1) % @in.Count];

                dot = dists[i] / (dists[i] - dists[i + 1]);
                for (j = 0; j < 3; j++) {	// avoid round off error when possible
                    if (split.Normal[j] == 1)
                        mid[j] = (float)split.D;
                    else if (split.Normal[j] == -1)
                        mid[j] = -(float)split.D;
                    else
                        mid[j] = p1[j] + dot * (p2[j] - p1[j]);
                }

                neww.Add(mid);
            }

            return neww;
        }
    }
}
