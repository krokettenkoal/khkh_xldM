using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace khiiMapv.Utils {
    public class GSRegsRecorder {
        private ulong val;
        private byte cmd;
        private string hint;

        private IDictionary<string, int> record = null;

        public List<IDictionary<string, int>> records = new List<IDictionary<string, int>>();

        internal void AddRecord() {
            record = new Dictionary<string, int>();
            records.Add(record);
        }

        internal void Walk(BinaryReader br, int count, string hint) {
            this.hint = hint;
            for (var y = 0; y < count; y++) {
                val = br.ReadUInt64();
                cmd = (byte)br.ReadUInt64();
                if (record != null) {
                    ReadOne();
                }
            }
        }

        private void ReadOne() {
            if (cmd == 0x50) {
                Add("BITBLTBUF", "SBP", 0, 13);
                Add("BITBLTBUF", "SBW", 16, 21);
                Add("BITBLTBUF", "SPSM", 24, 29);
                Add("BITBLTBUF", "DBP", 32, 45);
                Add("BITBLTBUF", "DBW", 48, 53);
                Add("BITBLTBUF", "DPSM", 56, 61);
            }
            if (cmd == 0x51) {
                Add("TRXPOS", "SSAX", 0, 10);
                Add("TRXPOS", "SSAY", 16, 26);
                Add("TRXPOS", "DSAX", 32, 42);
                Add("TRXPOS", "DSAY", 48, 58);
                Add("TRXPOS", "DIR", 59, 60);
            }
            if (cmd == 0x52) {
                Add("TRXREG", "RRW", 0, 11);
                Add("TRXREG", "RRH", 32, 43);
            }
            if (cmd == 0x53) {
                Add("TRXDIR", "XDIR", 0, 1);
            }
            if (cmd == 0x3f) {
            }
            if (cmd == 0x34) {
                Add("MIPTBP1_1", "TBP1", 0, 13);
                Add("MIPTBP1_1", "TBW1", 14, 19);
                Add("MIPTBP1_1", "TBP2", 20, 33);
                Add("MIPTBP1_1", "TBW2", 34, 39);
                Add("MIPTBP1_1", "TBP3", 40, 53);
                Add("MIPTBP1_1", "TBW3", 54, 59);
            }
            if (cmd == 0x35) {
                Add("MIPTBP1_2", "TBP1", 0, 13);
                Add("MIPTBP1_2", "TBW1", 14, 19);
                Add("MIPTBP1_2", "TBP2", 20, 33);
                Add("MIPTBP1_2", "TBW2", 34, 39);
                Add("MIPTBP1_2", "TBP3", 40, 53);
                Add("MIPTBP1_2", "TBW3", 54, 59);
            }
            if (cmd == 0x36) {
                Add("MIPTBP2_1", "TBP4", 0, 13);
                Add("MIPTBP2_1", "TBW4", 14, 19);
                Add("MIPTBP2_1", "TBP5", 20, 33);
                Add("MIPTBP2_1", "TBW5", 34, 39);
                Add("MIPTBP2_1", "TBP6", 40, 53);
                Add("MIPTBP2_1", "TBW6", 54, 59);
            }
            if (cmd == 0x37) {
                Add("MIPTBP2_2", "TBP4", 0, 13);
                Add("MIPTBP2_2", "TBW4", 14, 19);
                Add("MIPTBP2_2", "TBP5", 20, 33);
                Add("MIPTBP2_2", "TBW5", 34, 39);
                Add("MIPTBP2_2", "TBP6", 40, 53);
                Add("MIPTBP2_2", "TBW6", 54, 59);
            }
            if (cmd == 0x16) {
                Add("TEX2_1", "PSM", 20, 25);
                Add("TEX2_1", "CBP", 37, 50);
                Add("TEX2_1", "CPSM", 51, 54);
                Add("TEX2_1", "CSM", 55, 55);
                Add("TEX2_1", "CSA", 56, 60);
                Add("TEX2_1", "CLD", 61, 63);
            }
            if (cmd == 0x17) {
                Add("TEX2_2", "PSM", 20, 25);
                Add("TEX2_2", "CBP", 37, 50);
                Add("TEX2_2", "CPSM", 51, 54);
                Add("TEX2_2", "CSM", 55, 55);
                Add("TEX2_2", "CSA", 56, 60);
                Add("TEX2_2", "CLD", 61, 63);
            }
            if (cmd == 0x14) {
                Add("TEX1_1", "LCM", 0, 0);
                Add("TEX1_1", "MXL", 2, 4);
                Add("TEX1_1", "MMAG", 5, 5);
                Add("TEX1_1", "MMIN", 6, 8);
                Add("TEX1_1", "MTBA", 9, 9);
                Add("TEX1_1", "L", 19, 20);
                Add("TEX1_1", "K", 32, 43);
            }
            if (cmd == 0x15) {
                Add("TEX1_2", "LCM", 0, 0);
                Add("TEX1_2", "MXL", 2, 4);
                Add("TEX1_2", "MMAG", 5, 5);
                Add("TEX1_2", "MMIN", 6, 8);
                Add("TEX1_2", "MTBA", 9, 9);
                Add("TEX1_2", "L", 19, 20);
                Add("TEX1_2", "K", 32, 43);
            }
            if (cmd == 0x06) {
                Add("TEX0_1", "TBP0", 0, 13);
                Add("TEX0_1", "TBW", 14, 19);
                Add("TEX0_1", "PSM", 20, 25);
                Add("TEX0_1", "TW", 26, 29);
                Add("TEX0_1", "TH", 30, 33);
                Add("TEX0_1", "TCC", 34, 34);
                Add("TEX0_1", "TFX", 35, 36);
                Add("TEX0_1", "CBP", 37, 50);
                Add("TEX0_1", "CPSM", 51, 54);
                Add("TEX0_1", "CSM", 55, 55);
                Add("TEX0_1", "CSA", 56, 60);
                Add("TEX0_1", "CLD", 61, 63);
            }
            if (cmd == 0x07) {
                Add("TEX0_2", "TBP0", 0, 13);
                Add("TEX0_2", "TBW", 14, 19);
                Add("TEX0_2", "PSM", 20, 25);
                Add("TEX0_2", "TW", 26, 29);
                Add("TEX0_2", "TH", 30, 33);
                Add("TEX0_2", "TCC", 34, 34);
                Add("TEX0_2", "TFX", 35, 36);
                Add("TEX0_2", "CBP", 37, 50);
                Add("TEX0_2", "CPSM", 51, 54);
                Add("TEX0_2", "CSM", 55, 55);
                Add("TEX0_2", "CSA", 56, 60);
                Add("TEX0_2", "CLD", 61, 63);
            }
            if (cmd == 0x08) {
                Add("CLAMP_1", "WMS", 0, 1);
                Add("CLAMP_1", "WMT", 2, 3);
                Add("CLAMP_1", "MINU", 4, 13);
                Add("CLAMP_1", "MAXU", 14, 23);
                Add("CLAMP_1", "MINV", 24, 33);
                Add("CLAMP_1", "MAXV", 34, 43);
            }
            if (cmd == 0x09) {
                Add("CLAMP_2", "WMS", 0, 1);
                Add("CLAMP_2", "WMT", 2, 3);
                Add("CLAMP_2", "MINU", 4, 13);
                Add("CLAMP_2", "MAXU", 14, 23);
                Add("CLAMP_2", "MINV", 24, 33);
                Add("CLAMP_2", "MAXV", 34, 43);
            }
        }

        private void Add(string regName, string fieldName, int start, int end) {
            var mask = ((1UL << (end + 1 - start)) - 1UL);
            var memberValue = ((val >> start) & ((1UL << (end + 1 - start)) - 1UL));

            record[$"{fieldName}:{regName}:{hint}"] = Convert.ToInt32(memberValue);
        }
    }
}
