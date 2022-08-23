using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace parseSEQDv2.Utils {
    class Read2ld {
        public List<Layd> LaydList { get; } = new List<Layd>();
        public List<Seqd> StandaloneSeqdList { get; } = new List<Seqd>();

        [YamlIgnore]
        public List<object> AllObjectsForDebug { get; } = new List<object>();

        public Read2ld(string fp2ld) {
            PicIMGD[] imgList = null; // img list is always overwritten on each IMGZ or IMGD encounters.
            string keyImg = null;
            using (var fs = File.OpenRead(fp2ld)) {
                foreach (ReadBar.Entry entry in ReadBar.Explode(fs)) {
                    if (false) { }
                    else if (entry.key == 0x1D) { // IMGZ: multi IMGD
                        imgList = ParseIMGZ.TakeIMGZ(entry.bin, entry.id);
                        keyImg = entry.id;
                    }
                    else if (entry.key == 0x18) { // IMGD
                        imgList = new PicIMGD[] { ParseIMGD.TakeIMGD(new MemoryStream(entry.bin, false), entry.id) };
                        keyImg = entry.id;
                    }
                    else if (entry.key == 0x1C) { // LAYD: multi SEQD
                        var layd = new Layd(imgList.ToArray(), entry.bin, $"img({keyImg}) LAYD({entry.id})");
                        LaydList.Add(layd);
                        AllObjectsForDebug.Add(layd);

                        for (int x = 0; x < layd.ListSeqdOffset.Count; x++) {
                            var offset = layd.ListSeqdOffset[x];
                            var seqdBinSeg = new ArraySegment<byte>(entry.bin, offset, entry.bin.Length - offset);
                            var seqd = new Seqd(imgList.ToArray(), seqdBinSeg, $"img({keyImg}) LAYD({entry.id})→SEQD#{x}", entry.id);
                            layd.SeqdList.Add(seqd);
                            AllObjectsForDebug.Add(seqd);
                        }
                    }
                    else if (entry.key == 0x19) { // SEQD
                        var seqdBinSeg = new ArraySegment<byte>(entry.bin);
                        var seqd = new Seqd(imgList.ToArray(), seqdBinSeg, $"img({keyImg}) SEQD({entry.id})", entry.id);
                        StandaloneSeqdList.Add(seqd);
                        var layd = Layd.Wrap2dd(imgList.ToArray(), seqd, $"img({keyImg}) LAYD(dummy)");
                        LaydList.Add(layd);
                        AllObjectsForDebug.Add(layd);
                        AllObjectsForDebug.Add(seqd);
                    }
                }
            }
        }
    }
}
