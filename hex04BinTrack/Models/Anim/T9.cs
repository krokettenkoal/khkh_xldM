using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex04BinTrack.Models.Anim {
    public class T9 {
        public int c00; // t11_xxxx
        public int c02; // t10_xxxx
        public int c04; // t12_xxxx
        public int c06; // t12_xxxx

        public int Interpolation { get; set; }
        public float KeyFrame { get; set; } // t11c00 
        public float Value { get; set; }// t10c00
        public float EaseIn { get; set; }// t12c00
        public float EaseOut { get; set; }// t12c00

        public T9(BinaryReader br) {
            this.c00 = br.ReadUInt16(); // t11_xxxx
            this.c02 = br.ReadUInt16(); // t10_xxxx
            this.c04 = br.ReadUInt16(); // t12_xxxx
            this.c06 = br.ReadUInt16(); // t12_xxxx
        }

        public override string ToString() {
            return string.Format("{0} {1} {2} {3}"
                , c00, c02, c04, c06
                );
        }
    }
}
