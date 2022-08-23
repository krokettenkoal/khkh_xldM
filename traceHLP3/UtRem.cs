using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace traceHLP3 {
    public class UtRem {
        NpgsqlConnection db;

        public UtRem() {
            db = new NpgsqlConnection("Server=127.0.0.1;Database=ytest;UserID=kh2fm;Password=kh2fm");
            db.Open();
        }

        public uint FindHLPfrmPC(uint pc) {
            object res = new NpgsqlCommand(String.Format("select hlp from proc.walked where walked={0}", (int)pc), db).ExecuteScalar();
            if (res is DBNull) throw new KeyNotFoundException();
            return Convert.ToUInt32(res);
        }

        public uint[] FindParentHLPfrmHLP(uint pc) {
            using (NpgsqlDataReader dr = new NpgsqlCommand(String.Format("select hlpfrm from proc.walked where hlpto={0}", (int)pc), db).ExecuteReader()) {
                List<uint> al = new List<uint>();
                while (dr.Read()) {
                    al.Add((uint)Convert.ToInt32(dr[0]));
                }
                return al.ToArray();
            }
        }

        public THLP GetTHLP(uint pc) {
            THLP o = new THLP();
            using (NpgsqlDataReader dr = new NpgsqlCommand(String.Format("select hlp,spadd,raoff from proc.entry where hlp={0}", (int)pc), db).ExecuteReader()) {
                if (!dr.Read()) throw new KeyNotFoundException();
                o.entrypc = (uint)Convert.ToInt32(dr[0]);
                o.spadd = Convert.ToInt16(dr[1]);
                o.raoff = Convert.ToInt16(dr[2]);
            }
#if false
            using (NpgsqlDataReader dr = new NpgsqlCommand(String.Format("select hlpto from proc.gosub where hlpfrm={0}", (int)pc), db).ExecuteReader()) {
                while (dr.Read()) {
                    o.gosubpcs[(uint)Convert.ToInt32(dr[0])] = null;
                }
            }
            using (NpgsqlDataReader dr = new NpgsqlCommand(String.Format("select walked from proc.walked where hlp={0}", (int)pc), db).ExecuteReader()) {
                while (dr.Read()) {
                    o.dictWalked[(uint)Convert.ToInt32(dr[0])] = null;
                }
            }
#endif
            return o;
        }
    }

    public class THLP {
        public uint entrypc;
        public SortedDictionary<uint, object> gosubpcs = new SortedDictionary<uint, object>();
        public SortedDictionary<uint, object> dictWalked = new SortedDictionary<uint, object>();
        public int spadd = 0, raoff = 0;

        public override string ToString() {
            return String.Format("pc {0:x8} sp+{1,-3} ra {2} walked {3} gosub {4}"
                , entrypc, spadd, raoff, dictWalked.Count, gosubpcs.Count
                );
        }
    }
}
