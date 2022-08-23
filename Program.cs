using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

// table with BAD texture: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_HB580.mdlx
// donald door: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_DC520.mdlx
// yellow choke: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_TT650_10.mdlx
// purple turquoise: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_AL690_MATSU.mdlx
// volcano: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_PO610.mdlx
// torch: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_LM720_MATSU.mdlx
// cyan circle: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_EX570.mdlx
// chest with BAD tex: H:\Data\TEMP\KH2.yaz0r\dump_kh2\obj\F_WI110.mdlx

// F_DC520.07.tim_.bin
// F_PO610.07.tim_.bin


// ADDRESS   00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F   0123456789ABCDEF 
// ------------------------------------------------------------------------------
//  00000000  00 00 00 00 48 00 00 00 01 00 00 00 01 00 00 00   ....H........... 
//  00000010  24 00 00 00 30 00 00 00 50 01 00 00 00 02 00 00   $...0...P....... 
//  00000020 <00 22>00 00 00 00 00 00 00 00 00 00 00 00 00 00   .".............. 
//  00000030  06 00 00 10 00 00 00 00 00 00 00 13 06 00 00 50   ...............P 

// @00000020: 0x2200 is offset of palette sets.
//            a palette set has 256 colors. (it is usually less than 256 colors about last palette set)
//            A color is <RR GG BB 80> or <BB GG RR 80> form.
//            ends with _KN5

namespace khkh_xldM {
    class Program {
        static void helpYou() {
            Console.Error.WriteLine("khkh_xldM <.mdlx>");
            Environment.Exit(1);
        }

        static void Main(string[] args) {
            if (args.Length < 1)
                helpYou();
            using (FileStream si = File.OpenRead(args[0])) {
                new ReadBar().Explode(si, Path.GetFileNameWithoutExtension(args[0]));
            }
        }
    }

    class ReadBar {
        class Barent {
            public int k;
            public string id;
            public int off, len;
            public byte[] bin;
        }
        public void Explode(Stream si, string prefix) {
            BinaryReader br = new BinaryReader(si);
            if (br.ReadByte() != 'B' || br.ReadByte() != 'A' || br.ReadByte() != 'R' || br.ReadByte() != 1)
                throw new NotSupportedException();
            int cx = br.ReadInt32();
            br.ReadBytes(8);
            List<Barent> al = new List<Barent>();
            for (int x = 0; x < cx; x++) {
                Barent ent = new Barent();
                ent.k = br.ReadInt32();
                ent.id = Encoding.ASCII.GetString(br.ReadBytes(4)).TrimEnd((char)0);
                ent.off = br.ReadInt32();
                ent.len = br.ReadInt32();
                al.Add(ent);
            }
            for (int x = 0; x < cx; x++) {
                Barent ent = al[x];
                si.Position = ent.off;
                ent.bin = br.ReadBytes(ent.len);
                Debug.Assert(ent.bin.Length == ent.len);

                using (FileStream wr = File.Create(prefix + "." + ent.k.ToString("X2") + "." + ent.id + ".bin")) {
                    wr.Write(ent.bin, 0, ent.len);
                    wr.Close();
                }
            }
        }
    }
}
