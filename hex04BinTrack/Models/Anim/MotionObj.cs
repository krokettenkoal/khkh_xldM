using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class MotionObj {
        public List<T1> t1Static = new List<T1>();
        public List<T2> t2Anim = new List<T2>();
        public List<T2> t2x = new List<T2>();
        public List<T4> t4Joint = new List<T4>();
        public List<AxBone> t5Bone = new List<AxBone>();
        public List<T3> t3IKC = new List<T3>();
        public List<T6> t6 = new List<T6>();
        public List<T7> t7 = new List<T7>();
        public List<T8> t8 = new List<T8>();

        public float mintick = 0;
        public float maxtick = 1;
    }
}
