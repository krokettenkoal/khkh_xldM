using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Prayvif1 {
    public class Outxml {
        XmlDocument xmlo = new XmlDocument();
        XmlElement elroot;
        XmlElement elf;
        XmlElement elDMAc;
        XmlElement elexec;
        XmlElement elGS = null;
        XmlElement elGIFdma = null;

        public Outxml() {
            elroot = xmlo.CreateElement("Outxml");
            xmlo.AppendChild(elroot);
        }

        String dir = null;
        int bini = 0;

        public void Startfile(string fn, string dir) {
            elf = xmlo.CreateElement("f");
            elroot.AppendChild(elf);
            elf.SetAttribute("fn", fn);
            elf.SetAttribute("dir", this.dir = dir);
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

        Stack<XmlElement> alTag = new Stack<XmlElement>();

        public void ParseDMAc(uint x, uint cx, string ty, uint tadr) {
            elDMAc = xmlo.CreateElement("DMA-tag");
            XmlElement elparent = (alTag.Count != 0) ? alTag.Peek() : elf;
            elparent.AppendChild(elDMAc);
            elDMAc.SetAttribute("dat-addr", x.ToString("x8"));
            elDMAc.SetAttribute("size", cx.ToString());
            elDMAc.SetAttribute("tag-id", ty);
            elDMAc.SetAttribute("tag-addr", tadr.ToString("x8"));

            if (ty == "call") alTag.Push(elDMAc);
            if (ty == "ret" && alTag.Count != 0) elDMAc = alTag.Pop();
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
        public void Attach(byte[] bin) {
            String fn = String.Format("vif{0:0000}.bin", bini);
            bini++;
            elexec.SetAttribute("fn", fn);
            File.WriteAllBytes(Path.Combine(dir, fn), bin);
        }
        public void Endvif() {
            elexec = null;
        }

        XmlElement ParentGS {
            get {
                if (elexec != null) return elexec;
                if (elGIFdma != null) return elGIFdma;
                return elf;
            }
        }

        public void StartGIFdma(uint addr, uint qwc, byte[] bin) {
            elGIFdma = xmlo.CreateElement("GIFdma");
            elf.AppendChild(elGIFdma);
            elGIFdma.SetAttribute("addr", addr.ToString("x8"));
            elGIFdma.SetAttribute("qwc", XmlConvert.ToString(qwc));
            {
                String fn = String.Format("gif{0:0000}.bin", bini);
                bini++;
                elGIFdma.SetAttribute("fn", fn);
                File.WriteAllBytes(Path.Combine(dir, fn), bin);
            }
        }

        public void StartGS() {
            elGS = xmlo.CreateElement("GS");
            ParentGS.AppendChild(elGS);
        }
        public void EndGS() {
            elGS = null;
        }

        public void StartGIFtag(string what) {
            XmlElement elp = xmlo.CreateElement("GIFtag");
            XmlElement elparent = (elexec != null) ? elexec : elDMAc;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }

        public void StartPacked(string what) {
            XmlElement elp = xmlo.CreateElement("GIFpacked");
            XmlElement elparent = elGS;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }

        public void AddPrim(string what) {
            XmlElement elp = xmlo.CreateElement("GIFprim");
            XmlElement elparent = (elexec != null) ? elexec : elDMAc;
            elparent.AppendChild(elp);
            elp.AppendChild(xmlo.CreateTextNode(what));
        }

        static String[] alPrim = "point,line,line strip,tri,tri strip,tri fan,sprite,X".Split(',');
        static String[] alShad = "flat,gouraud".Split(',');
        static String[] alTC = "STQ,UV".Split(',');
        static String[] alContext = "Context 1,Context 2".Split(',');
        static String[] alFrag = "Unfixed,Fixed".Split(',');
        static String[] alBool = "off,on".Split(',');

        static String[] alFmt = "packed,reglist,image,X".Split(',');
        static String[] alRegDesc = "PRIM,RGBAQ,ST,UV,XYZF2,XYZ2,TEX0_1,TEX0_2,CLAMP_1,CLAMP_2,FOG,-,XYZF3,XYZ3,A+D,nop".Split(',');

        public void StartGIFtag2(int nloop, bool eop, bool pre, int prim, byte flg, byte nreg, byte[] regs) {
            XmlElement el = xmlo.CreateElement("GIFtag");
            elGS.AppendChild(el);
            el.SetAttribute("nloop", XmlConvert.ToString(nloop));
            el.SetAttribute("eop", XmlConvert.ToString(eop ? 1 : 0));
            el.SetAttribute("prim-enabled", XmlConvert.ToString(pre ? 1 : 0));

            el.SetAttribute("primitive", alPrim[(prim >> 0) & 7]);
            el.SetAttribute("shading", alShad[(prim >> 3) & 1]);
            el.SetAttribute("texture", alBool[(prim >> 4) & 1]);
            el.SetAttribute("fogging", alBool[(prim >> 5) & 1]);
            el.SetAttribute("alpha-blend", alBool[(prim >> 6) & 1]);
            el.SetAttribute("anti-alias", alBool[(prim >> 7) & 1]);
            el.SetAttribute("tex-coord", alTC[((prim >> 8) & 1)]);
            el.SetAttribute("context", alContext[((prim >> 9) & 1)]);
            el.SetAttribute("fragment", alFrag[((prim >> 10) & 1)]);

            el.SetAttribute("format", alFmt[flg]);

            for (byte x = 0; x < nreg; x++) {
                el.SetAttribute("reg" + x, alRegDesc[regs[x]]);
            }
        }
    }
}
