using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lib03.Models {
    public class Instr {
        [XmlElement]
        public Arg[] Arg;

        [XmlAttribute]
        public int opcode;

        [XmlAttribute]
        public int sub;

        [XmlAttribute]
        public int ssub;

        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public int syscall;

        [XmlAttribute]
        public int gosub;

        [XmlAttribute]
        public int jump;

        [XmlAttribute]
        public int conditional;

        [XmlAttribute]
        public int neverReturn;

        [XmlAttribute]
        public int gosubRet;

    }
}
