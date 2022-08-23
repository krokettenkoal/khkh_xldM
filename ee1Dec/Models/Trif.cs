using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ee1Dec.Models {
    public class Trif {
        [XmlAttribute("key")]
        public string prefix;

        [XmlAttribute("trac")]
        public string tracfp;

        [XmlAttribute("desc")]
        public string descfp;

        public Trif() {
        }

        public override string ToString() {
            return "Open " + prefix + ", " + Path.GetFileName(tracfp) + ", " + Path.GetFileName(descfp);
        }
    }
}
