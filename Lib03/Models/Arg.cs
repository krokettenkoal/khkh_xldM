using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lib03.Models {
    public class Arg {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string type;

        [XmlAttribute]
        public int aiPos;
    }
}
