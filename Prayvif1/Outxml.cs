using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Prayvif1 {
    public class Outxml {
        XmlDocument xmlo = new XmlDocument();
        XmlElement elroot;
        XmlElement elf;
        XmlElement elDMAc;
        XmlElement elexec;

        public Outxml() {
            elroot = xmlo.CreateElement("Outxml");
            xmlo.AppendChild(elroot);
        }

        public void Startfile(string fn) {
            elf = xmlo.CreateElement("f");
            elroot.AppendChild(elf);
            elf.SetAttribute("fn", fn);
        }

        class B2H {
            public static string Conv(byte[] bin, int x, int cx) {
                StringBuilder b = new StringBuilder(bin.Length * 3);
                int x1 = x + cx;
                for (; x < x1; x++) {
                    b.AppendFormat("{0:x2} ", bin[x]);
                }
                return b.ToString();
            }
        }

        public void Readfile(byte[] bin, int x, int cx) {
            XmlElement elbin = xmlo.CreateElement("bin");
            elDMAc.AppendChild(elbin);
            elbin.SetAttribute("cx", cx.ToString());
            elbin.AppendChild(xmlo.CreateTextNode(B2H.Conv(bin, x, cx)));
        }

        public void StartDMAc(uint x, uint cx, string ty) {
            elDMAc = xmlo.CreateElement("DMAc");
            elf.AppendChild(elDMAc);
            elDMAc.SetAttribute("x", x.ToString("x8"));
            elDMAc.SetAttribute("cx", cx.ToString());
            elDMAc.SetAttribute("ty", ty);
        }

        public void Save(string fp) {
            xmlo.Save(fp);
        }

        public void Startvif(string cmd, string wrs) {
            XmlElement elp = elexec = xmlo.CreateElement("VIF");
            elDMAc.AppendChild(elp);
            elp.SetAttribute("cmd", cmd);
            elp.SetAttribute("wrs", wrs);
        }
        public void Endvif() {
            elexec = null;
        }

        public void StartGIFtag(string what) {
            XmlElement elp = xmlo.CreateElement("GIFtag");
            XmlElement elparent = (elexec != null) ? elexec : elDMAc;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }

        public void StartPacked(string what) {
            XmlElement elp = xmlo.CreateElement("GIFpacked");
            XmlElement elparent = (elexec != null) ? elexec : elDMAc;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }

        public void AddPrim(string what) {
            XmlElement elp = xmlo.CreateElement("GIFprim");
            XmlElement elparent = (elexec != null) ? elexec : elDMAc;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }
    }
}
