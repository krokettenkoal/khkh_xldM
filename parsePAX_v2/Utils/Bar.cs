using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace parseSEQDv2.Utils {
    public class ReadBar {
        public class Entry {
            public int key;
            public string id;
            public int off, len;
            public byte[] bin;
        }
        public static Entry[] Explode(Stream si) {
            BinaryReader reader = new BinaryReader(si);
            if (reader.ReadByte() != 'B' || reader.ReadByte() != 'A' || reader.ReadByte() != 'R' || reader.ReadByte() != 1) {
                throw new NotSupportedException();
            }
            int cx = reader.ReadInt32();
            reader.ReadBytes(8);
            List<Entry> list = new List<Entry>();
            for (int x = 0; x < cx; x++) {
                Entry ent = new Entry();
                ent.key = reader.ReadInt32();
                ent.id = Encoding.ASCII.GetString(reader.ReadBytes(4)).TrimEnd((char)0);
                ent.off = reader.ReadInt32();
                ent.len = reader.ReadInt32();
                list.Add(ent);
            }
            for (int x = 0; x < cx; x++) {
                Entry entry = list[x];
                si.Position = entry.off;
                entry.bin = reader.ReadBytes(entry.len);
                Debug.Assert(entry.bin.Length == entry.len);
            }
            return list.ToArray();
        }
    }
}
