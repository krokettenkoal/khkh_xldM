using Lib03.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lib03.Models {
    [XmlRoot]
    public class PCode {
        [XmlElement]
        public Instr[] Instr;

        [XmlElement]
        public Syscall[] Syscall;

        public static Lazy<PCode> Loader = new Lazy<PCode>(
            () => (PCode)new XmlSerializer(typeof(PCode)).Deserialize(
                new StringReader(Resources.PCode)
            )
        );
    }
}
