using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lib03.Models {
    public class Syscall {
        [XmlAttribute]
        public int tableIdx;

        [XmlAttribute]
        public int funcIdx;

        [XmlAttribute]
        public string name;
    }
}
