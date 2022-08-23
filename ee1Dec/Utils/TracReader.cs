using ee1Dec.C;
using ee1Dec.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ee1Dec.Utils {
    public abstract class TracReader {
        public TracReader(CustEE ee, CPUState stat) {
            this.ee = ee;
            this.stat = stat;
        }

        public abstract void Part1();
        public abstract void Part2();
        public abstract bool EOF { get; }

        public List<MemUpdated> memUpdates = new List<MemUpdated>();

        protected readonly CustEE ee;
        protected readonly CPUState stat;

        internal class Header {
            internal readonly string body;
            internal readonly string[] lines;

            public Header(BinaryReader br) {
                body = Encoding.ASCII.GetString(br.ReadBytes(1024)).Split('\x1a')[0].Split('\x0')[0];
                lines = body.Split('\n');
            }
        }

        public static TracReader CreateFrom(CustEE ee, CPUState stat) {
            var pos = stat.fs.Position;
            var header = new Header(new BinaryReader(stat.fs));

            var sig = header.lines[0];

            if (sig == "trac1") {
                return new Trac1Reader(ee, stat);
            }
            else if (sig == "trac2") {
                return new Trac2Reader(ee, stat);
            }
            throw new NotSupportedException(sig);
        }
    }
}
