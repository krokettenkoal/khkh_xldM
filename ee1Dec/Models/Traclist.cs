using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ee1Dec.Models {
    [XmlRoot]
    public class Traclist {
        [XmlElement]
        public Trif[] Open;
    }
}
