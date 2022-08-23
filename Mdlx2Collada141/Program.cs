using System;
using System.Collections.Generic;
using System.Text;
using khkh_xldMii;
using System.IO;
using vconv122;
using System.Xml;
using System.Drawing.Imaging;
using khkh_xldMii.V;
using hex04BinTrack;
using Mdlx2Collada141.Properties;
using System.Windows.Forms;
using SlimDX;
using khkh_xldMii.Models;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Models.Mdlx;
using Resources = Mdlx2Collada141.Properties.Resources;

namespace Mdlx2Collada141 {
    static class CS141 {
        public static readonly string URI = "http://www.collada.org/2005/11/COLLADASchema";
    }
    class Program {
#if DEBUG
        static void Main(string[] args) {
            new Program().Run(@"H:\KH2fm.OpenKH\obj\P_EX100.mdlx", @"C:\A\p_ex100.dae");
        }
#else
        [STAThread]
        static void Main() {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select kh2 models extracted folder:";
            if (fbd.ShowDialog() == DialogResult.OK) {
                String dirExp = Path.Combine(Application.StartupPath, "export");
                Directory.CreateDirectory(dirExp);
                foreach (string fp in Directory.GetFiles(fbd.SelectedPath, "*.mdlx")) {
                    Console.WriteLine("# Converting " + Path.GetFileNameWithoutExtension(fp));
                    try {
                        Environment.CurrentDirectory = dirExp;
                        String fdae = Path.GetFileNameWithoutExtension(fp) + ".dae";
                        new Program().Run(fp, fdae);
                        Console.WriteLine("Ok");
                    }
                    catch (Exception) {
                        Console.WriteLine("Failed");
                    }
                }
            }
        }
#endif

        void Run(string fpmdlx, string fpdae) {
            using (FileStream fmdlx = File.OpenRead(fpmdlx)) {
                Texex2[] tal = null;
                Mdlxfst mdlx = null;
                foreach (ReadBar.Barent ent in ReadBar.Explode(fmdlx)) {
                    switch (ent.k) {
                        case 7: // timf
                            tal = TIMCollection.Load(new MemoryStream(ent.bin, false));
                            break;
                        case 4: // m_ex
                            mdlx = new Mdlxfst(new MemoryStream(ent.bin, false));
                            break;
                    }
                }
                C c = new C(Path.GetFileName(fpmdlx));
                nut = new Nameut(Path.GetDirectoryName(fpdae), Path.GetFileNameWithoutExtension(fpdae));
                IDictionary<string, string> matSymTarDict = new SortedDictionary<string, string>();
                List<string> almatName = new List<string>();
                {
                    int texi = 0;
                    foreach (var tex in tal[0].bitmapList) {
                        string fp = nut.Getfp(texi);
                        string name = nut.GetName(texi);
                        string matname = nut.GetMatName(texi);
                        //c.AddImage(fp, name);
                        //c.AddEffectImg(name, Path.GetFileName(fp));
                        c.AddEffectTex(name, Path.GetFileName(fp));
                        c.AddMat(matname, matname, name);
                        matSymTarDict[matname] = matname;
                        almatName.Add(matname);
                        tex.bitmap.Save(fp, ImageFormat.Jpeg);
                        texi++;
                    }
                }
                foreach (K2Model t31 in mdlx.alt31) {
                    AxBone[] alaxb = t31.boneList.bones.ToArray();
                    Matrix[] Ma = new Matrix[alaxb.Length];
                    {
                        Vector3[] Va = new Vector3[Ma.Length];
                        Quaternion[] Qa = new Quaternion[Ma.Length];
                        for (int x = 0; x < Ma.Length; x++) {
                            Quaternion Qo;
                            Vector3 Vo;
                            AxBone axb = alaxb[x];
                            int parent = axb.parent;
                            if (parent < 0) {
                                Qo = Quaternion.Identity;
                                Vo = Vector3.Zero;
                            }
                            else {
                                Qo = Qa[parent];
                                Vo = Va[parent];
                            }

                            Vector3 Vt = Vector3.TransformCoordinate(new Vector3(axb.x3, axb.y3, axb.z3), Matrix.RotationQuaternion(Qo));
                            Va[x] = Vo + Vt;

                            Quaternion Qt = Quaternion.Identity;
                            if (axb.x2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(1, 0, 0), axb.x2));
                            if (axb.y2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 1, 0), axb.y2));
                            if (axb.z2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 0, 1), axb.z2));
                            Qa[x] = Qt * Qo;
                        }
                        for (int x = 0; x < Ma.Length; x++) {
                            Matrix M = Matrix.RotationQuaternion(Qa[x]);
                            M *= (Matrix.Translation(Va[x]));
                            Ma[x] = M;
                        }
                    }
                    List<Body1e> albody1 = new List<Body1e>();
                    Matrix Mv = Matrix.Identity;
                    foreach (K2Vif t13 in t31.vifList) {
                        VU1Mem mem = new VU1Mem();
                        int tops = 0x40, top2 = 0x220;
                        new ParseVIF1(mem).Parse(new MemoryStream(t13.bin, false), tops);
                        Body1e b1 = SimaVU1e.Sima(mem, Ma, tops, top2, t13.textureIndex, t13.marixIndexArray, Mv);
                        albody1.Add(b1);
                    }

                    if (true) {
                        Skin skin = new Skin();
                        skin.alaxb = alaxb;
                        skin.alw.Add(1.0f);
                        ffMesh ffmesh = new ffMesh();
                        if (true) {
                            int svi = 0;
                            int sti = 0;
                            ff1[] alo1 = new ff1[4];
                            int ai = 0;
                            int[] ord = new int[] { 1, 3, 2 };
                            foreach (Body1e b1 in albody1) {
                                for (int x = 0; x < b1.alvi.Length; x++) {
                                    ff1 o1 = new ff1(svi + b1.alvi[x], sti + x);
                                    alo1[ai] = o1;
                                    ai = (ai + 1) & 3;
                                    if (b1.alfl[x] == 0x20) {
                                        ff3 o3 = new ff3(almatName[b1.t],
                                            alo1[(ai - ord[0]) & 3],
                                            alo1[(ai - ord[1]) & 3],
                                            alo1[(ai - ord[2]) & 3]
                                            );
                                        ffmesh.al3.Add(o3);
                                    }
                                    else if (b1.alfl[x] == 0x30) {
                                        ff3 o3 = new ff3(almatName[b1.t],
                                            alo1[(ai - ord[0]) & 3],
                                            alo1[(ai - ord[2]) & 3],
                                            alo1[(ai - ord[1]) & 3]
                                            );
                                        ffmesh.al3.Add(o3);
                                    }
                                }
                                for (int x = 0; x < b1.alvertraw.Length; x++) {
                                    Vector3 vpos = Vector3.TransformCoordinate(b1.alvertraw[x], Ma[b1.alni[x]]);
                                    ffmesh.alpos.Add(vpos);
                                }
                                for (int x = 0; x < b1.aluv.Length; x++) {
                                    Vector2 vst = b1.aluv[x];
                                    vst.Y = -vst.Y;
                                    ffmesh.alst.Add(vst);
                                }
                                for (int x = 0; x < b1.alni.Length; x++) {
                                    Vw vw = new Vw();
                                    vw.vi = b1.alni[x];
                                    vw.wi = 0;
                                    skin.alvw.Add(new Vw[] { vw });
                                }
                                svi += b1.alvertraw.Length;
                                sti += b1.aluv.Length;
                            }
                        }
                        c.AddSkin(skin, "skin", "#entire");

                        SRTUt srt = new SRTUt();

                        c.AddG(ffmesh, "entire", "entire");
                        c.AddVS("DefaultScene", "Entire", "Entire", matSymTarDict, alaxb, srt, "skin", Ma);
                        c.AddIVS("DefaultScene");
                    }
                    break;
                }

                c.xmlo.Save(fpdae);
            }
        }

        class Sepa {
            public int svi;
            public int cnt;
            public int t;
            public int sel;

            public Sepa(int startVertexIndex, int cntPrimitives, int ti, int sel) {
                this.svi = startVertexIndex;
                this.cnt = cntPrimitives;
                this.t = ti;
                this.sel = sel;
            }
        }

        class ff1 {
            public int vi, ti;

            public ff1(int vi, int ti) {
                this.vi = vi;
                this.ti = ti;
            }
        }
        class ff3 {
            public ff3(string matnamae, ff1 x, ff1 y, ff1 z) {
                this.matnamae = matnamae;
                this.al1 = new ff1[] { x, y, z };
            }

            public ff1[] al1;
            public string matnamae;
        }
        class ffMesh {
            public List<Vector3> alpos = new List<Vector3>();
            public List<Vector2> alst = new List<Vector2>();
            public List<ff3> al3 = new List<ff3>();
            public List<MJ1[]> almtxuse = new List<MJ1[]>();
        }

        class Vw {
            public int vi;
            public int wi;
        }
        class Skin {
            public AxBone[] alaxb;
            public List<Vw[]> alvw = new List<Vw[]>();
            public List<float> alw = new List<float>();
        }

        Nameut nut;

        class Nameut {
            string dir, prefix;

            public Nameut(string dir, string prefix) {
                this.dir = dir;
                this.prefix = prefix;
            }
            public string GetName(int i) {
                return "tex" + i;
            }
            public string GetMatName(int i) {
                return "mat" + i;
            }
            public string Getfp(int i) {
                return Path.Combine(dir, prefix + "_tex" + i + ".jpg");
            }
        }

        class GT {
            public string matname = null;
            public int[] meshTriVi = new int[0];
        }

        class G {
            public float[] meshVec5 = new float[0];
            public GT[] gts = new GT[0];
        }

        class C {
            public XmlDocument xmlo = new XmlDocument();
            public XmlElement elasset, ellibimages, ellibgeo, ellibvs, elscene, ellibeffect, ellibmat, ellibctrl;
            XmlNamespaceManager xns;
            public static readonly string URI = "http://www.collada.org/2005/11/COLLADASchema";

            public C(string subject) {
                xmlo.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<COLLADA xmlns=\"http://www.collada.org/2005/11/COLLADASchema\" version=\"1.4.1\">\n<asset />\n</COLLADA>");
                xns = new XmlNamespaceManager(xmlo.NameTable);
                xns.AddNamespace("c", URI);
                elasset = (XmlElement)xmlo.SelectSingleNode("/c:COLLADA/c:asset", xns);
                XmlElement el;
                elasset.AppendChild(el = xmlo.CreateElement("created", URI)); el.AppendChild(xmlo.CreateTextNode(DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")));
                elasset.AppendChild(el = xmlo.CreateElement("modified", URI)); el.AppendChild(xmlo.CreateTextNode(DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")));
                elasset.AppendChild(el = xmlo.CreateElement("subject", URI)); el.AppendChild(xmlo.CreateTextNode((subject)));
                elasset.AppendChild(el = xmlo.CreateElement("title", URI)); el.AppendChild(xmlo.CreateTextNode("Kingdom Hearts II"));
                elasset.ParentNode.AppendChild(ellibctrl = xmlo.CreateElement("library_controllers", URI));
                elasset.ParentNode.AppendChild(ellibeffect = xmlo.CreateElement("library_effects", URI));
                elasset.ParentNode.AppendChild(ellibgeo = xmlo.CreateElement("library_geometries", URI));
                elasset.ParentNode.AppendChild(ellibimages = xmlo.CreateElement("library_images", URI));
                elasset.ParentNode.AppendChild(ellibmat = xmlo.CreateElement("library_materials", URI));
                elasset.ParentNode.AppendChild(ellibvs = xmlo.CreateElement("library_visual_scenes", URI));
                elasset.ParentNode.AppendChild(elscene = xmlo.CreateElement("scene", URI));
            }

            public void AddSkin(Skin s, string skinId, string skinSrc) {
                String[] alnodoname = new string[s.alaxb.Length];
                for (int x = 0; x < alnodoname.Length; x++) {
                    alnodoname[x] = Namej.GetName(x);
                }

                XmlElement elctrl = xmlo.CreateElement("controller", URI);
                ellibctrl.AppendChild(elctrl);
                elctrl.SetAttribute("id", skinId);
                {
                    XmlElement elskin = xmlo.CreateElement("skin", URI);
                    elctrl.AppendChild(elskin);
                    elskin.SetAttribute("source", skinSrc);
                    {
                        XmlElement elbsm = xmlo.CreateElement("bind_shape_matrix", URI);
                        elskin.AppendChild(elbsm);
                        elbsm.AppendChild(xmlo.CreateTextNode(" 1 0 0 0  0 1 0 0  0 0 1 0  0 0 0 1 "));
                    }
                    {
                        int cntj = alnodoname.Length;
                        XmlElement elsrc1 = xmlo.CreateElement("source", URI);
                        elskin.AppendChild(elsrc1);
                        elsrc1.SetAttribute("id", "Joints");
                        {
                            XmlElement elida = xmlo.CreateElement("IDREF_array", URI);
                            elsrc1.AppendChild(elida);
                            elida.SetAttribute("count", cntj.ToString());

                            string str = " ";
                            for (int x = 0; x < cntj; x++) {
                                str += alnodoname[x] + " ";
                            }
                            elida.AppendChild(xmlo.CreateTextNode(str.Trim()));
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc1.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("count", cntj.ToString());
                                elacc.SetAttribute("source", "Joints");
                                elacc.SetAttribute("stride", "1");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("type", "IDREF");
                                    elp.SetAttribute("name", "JOINT");
                                }
                            }
                        }
                    }
                    {
                        int cntw = s.alw.Count;

                        XmlElement elsrc2 = xmlo.CreateElement("source", URI);
                        elskin.AppendChild(elsrc2);
                        elsrc2.SetAttribute("id", "Weights");
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc2.AppendChild(elfa);
                            elfa.SetAttribute("count", cntw.ToString());

                            string str = " ";
                            for (int x = 0; x < cntw; x++) {
                                str += s.alw[x] + " ";
                            }
                            elfa.AppendChild(xmlo.CreateTextNode(str.Trim()));
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc2.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("count", cntw.ToString());
                                elacc.SetAttribute("source", "Weights");
                                elacc.SetAttribute("stride", "1");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("type", "float");
                                    elp.SetAttribute("name", "WEIGHT");
                                }
                            }
                        }
                    }
                    {
                        int cntf = alnodoname.Length;

                        XmlElement elsrc3 = xmlo.CreateElement("source", URI);
                        elskin.AppendChild(elsrc3);
                        elsrc3.SetAttribute("id", "Inv_bind_mats");
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc3.AppendChild(elfa);
                            elfa.SetAttribute("count", (16 * cntf).ToString());

                            for (int x = 0; x < cntf; x++) {
                                elfa.AppendChild(xmlo.CreateTextNode("1 0 0 0  0 1 0 0  0 0 1 0  0 0 0 1   "));
                            }
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc3.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("count", (cntf).ToString());
                                elacc.SetAttribute("source", "Inv_bind_mats");
                                elacc.SetAttribute("stride", "16");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("type", "float4x4");
                                }
                            }
                        }
                    }
                    {
                        XmlElement eljnts = xmlo.CreateElement("joints", URI);
                        elskin.AppendChild(eljnts);
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            eljnts.AppendChild(elin);
                            elin.SetAttribute("semantic", "JOINT");
                            elin.SetAttribute("source", "#Joints");
                        }
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            eljnts.AppendChild(elin);
                            elin.SetAttribute("semantic", "INV_BIND_MATRIX");
                            elin.SetAttribute("source", "#Inv_bind_mats");
                        }
                    }
                    {
                        XmlElement elvw = xmlo.CreateElement("vertex_weights", URI);
                        elskin.AppendChild(elvw);
                        elvw.SetAttribute("count", s.alvw.Count.ToString());
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvw.AppendChild(elin);
                            elin.SetAttribute("semantic", "JOINT");
                            elin.SetAttribute("source", "#Joints");
                            elin.SetAttribute("offset", "0");
                        }
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvw.AppendChild(elin);
                            elin.SetAttribute("semantic", "WEIGHT");
                            elin.SetAttribute("source", "#Weights");
                            elin.SetAttribute("offset", "1");
                        }
                        {
                            XmlElement elvc = xmlo.CreateElement("vcount", URI);
                            elvw.AppendChild(elvc);
                            string str = " ";
                            for (int x = 0; x < s.alvw.Count; x++) {
                                str += s.alvw[x].Length + " ";
                            }
                            elvc.AppendChild(xmlo.CreateTextNode(str.Trim()));
                        }
                        {
                            XmlElement elv = xmlo.CreateElement("v", URI);
                            elvw.AppendChild(elv);
                            string str = " ";
                            for (int x = 0; x < s.alvw.Count; x++) {
                                for (int w = 0; w < s.alvw[x].Length; w++) {
                                    Vw vw = (s.alvw[x])[w];
                                    str += vw.vi + " " + vw.wi + "  ";
                                }
                            }
                            elv.AppendChild(xmlo.CreateTextNode(str.Trim()));
                        }
                    }
                }
            }

            public void AddImage(string fp, string id, string name) {
                XmlElement el;
                ellibimages.AppendChild(el = xmlo.CreateElement("image", URI));
                el.SetAttribute("id", id);
                el.SetAttribute("name", name);
                XmlElement elif;
                el.AppendChild(elif = xmlo.CreateElement("init_from", URI));
                elif.AppendChild(xmlo.CreateTextNode(fp));
            }

            public void AddG(ffMesh ff, string id, string name) {
                XmlElement elg = xmlo.CreateElement("geometry", URI);
                ellibgeo.AppendChild(elg);
                elg.SetAttribute("id", id);
                elg.SetAttribute("name", name);
                {
                    XmlElement elmesh = xmlo.CreateElement("mesh", URI);
                    elg.AppendChild(elmesh);
                    {
                        // X,Y,Z

                        int poscnt = ff.alpos.Count;

                        XmlElement elsrc = xmlo.CreateElement("source", URI);
                        elsrc.SetAttribute("id", id + "-Pos");
                        elmesh.AppendChild(elsrc);
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc.AppendChild(elfa);
                            elfa.SetAttribute("id", id + "-Pos-array");
                            elfa.SetAttribute("count", (3 * poscnt).ToString());
                            {
                                StringBuilder s = new StringBuilder();
                                for (int t = 0; t < poscnt; t++) {
                                    Vector3 v = ff.alpos[t];
                                    s.Append(v.X + " " + v.Y + " " + v.Z + "\r\n");
                                }
                                elfa.AppendChild(xmlo.CreateTextNode(s.ToString()));
                            }
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("source", "#" + id + "-Pos-array");
                                elacc.SetAttribute("count", poscnt.ToString());
                                elacc.SetAttribute("stride", "3");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "X");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "Y");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "Z");
                                    elp.SetAttribute("type", "float");
                                }
                            }
                        }
                    }
                    {
                        // S,T

                        int stcnt = ff.alst.Count;

                        XmlElement elsrc = xmlo.CreateElement("source", URI);
                        elsrc.SetAttribute("id", id + "-ST");
                        elmesh.AppendChild(elsrc);
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc.AppendChild(elfa);
                            elfa.SetAttribute("id", id + "-ST-array");
                            elfa.SetAttribute("count", stcnt.ToString());
                            {
                                StringBuilder s = new StringBuilder();
                                for (int t = 0; t < stcnt; t++) {
                                    Vector2 v = ff.alst[t];
                                    s.Append(v.X + " " + v.Y + "\r\n");
                                }
                                elfa.AppendChild(xmlo.CreateTextNode(s.ToString()));
                            }
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("source", "#" + id + "-ST-array");
                                elacc.SetAttribute("count", stcnt.ToString());
                                elacc.SetAttribute("stride", "2");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "S");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "T");
                                    elp.SetAttribute("type", "float");
                                }
                            }
                        }
                    }
                    {
                        XmlElement elvert = xmlo.CreateElement("vertices", URI);
                        elmesh.AppendChild(elvert);
                        elvert.SetAttribute("id", id + "-Vtx");
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvert.AppendChild(elin);
                            elin.SetAttribute("semantic", "POSITION");
                            elin.SetAttribute("source", "#" + id + "-Pos");
                        }
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvert.AppendChild(elin);
                            elin.SetAttribute("semantic", "TEXCOORD");
                            elin.SetAttribute("source", "#" + id + "-TexPos");
                        }
                    }
                    if (true) {
                        SortedDictionary<string, object> dict = new SortedDictionary<string, object>();
                        foreach (ff3 o3 in ff.al3) dict[o3.matnamae] = null;
                        SortedDictionary<string, List<ff3>> dict3 = new SortedDictionary<string, List<ff3>>();
                        foreach (string k in dict.Keys) dict3[k] = new List<ff3>();
                        foreach (ff3 o3 in ff.al3) {
                            dict3[o3.matnamae].Add(o3);
                        }

                        foreach (List<ff3> o3 in dict3.Values) {
                            int polycnt = o3.Count;

                            XmlElement elpoly = xmlo.CreateElement("triangles", URI);
                            elmesh.AppendChild(elpoly);
                            elpoly.SetAttribute("count", polycnt.ToString());
                            elpoly.SetAttribute("material", o3[0].matnamae);
                            //if (gt.matname != null) elpoly.SetAttribute("material", gt.matname);
                            {
                                XmlElement elin = xmlo.CreateElement("input", URI);
                                elpoly.AppendChild(elin);
                                elin.SetAttribute("semantic", "VERTEX");
                                elin.SetAttribute("source", "#" + id + "-Vtx");
                                elin.SetAttribute("offset", "0");
                            }
                            {
                                // http://www.feelingsoftware.com/component/option,com_smf/Itemid,85/action,profile/u,109/sa,showPosts/lang,jp
                                XmlElement elin = xmlo.CreateElement("input", URI);
                                elpoly.AppendChild(elin);
                                elin.SetAttribute("semantic", "TEXCOORD");
                                elin.SetAttribute("source", "#" + id + "-ST");
                                elin.SetAttribute("offset", "1");
                            }
                            XmlElement elp = xmlo.CreateElement("p", URI);
                            elpoly.AppendChild(elp);
                            StringBuilder s = new StringBuilder();
                            //s.AppendLine();
                            for (int t = 0; t < polycnt; t++) {
                                foreach (ff1 o1 in o3[t].al1) {
                                    s.Append(o1.vi + " " + o1.ti + "  ");
                                }
                                s.Append("\r\n");
                            }
                            elp.AppendChild(xmlo.CreateTextNode(s.ToString()));
                        }
                    }
                }
            }

            static class Namej {
                public static string GetName(int x) {
#if true
                    return "Joint" + x;
#else
                    if (x == 0) return "Bone";
                    return "Bone_" + x.ToString("000");
#endif
                }
            }

            public void AddG(G g, string id, string name) {
                XmlElement elg = xmlo.CreateElement("geometry", URI);
                ellibgeo.AppendChild(elg);
                elg.SetAttribute("id", id);
                elg.SetAttribute("name", name);
                {
                    XmlElement elmesh = xmlo.CreateElement("mesh", URI);
                    elg.AppendChild(elmesh);

                    int vertcnt = (g.meshVec5.Length / 5);
                    int vtxcnt = (g.meshVec5.Length);
                    int v3cnt = vtxcnt / 5 * 3;
                    int stcnt = vtxcnt / 5 * 2;
                    {
                        // X,Y,Z

                        XmlElement elsrc = xmlo.CreateElement("source", URI);
                        elsrc.SetAttribute("id", id + "-Pos");
                        elmesh.AppendChild(elsrc);
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc.AppendChild(elfa);
                            elfa.SetAttribute("id", id + "-Pos-array");
                            elfa.SetAttribute("count", v3cnt.ToString());
                            {
                                StringBuilder s = new StringBuilder();
                                for (int t = 0; t < vtxcnt; t++) {
                                    //if ((t % 5) == 0) s.AppendLine();
                                    switch (t % 5) {
                                        case 0:
                                        case 1:
                                        case 2:
                                            s.Append(" " + g.meshVec5[t].ToString());
                                            break;
                                    }
                                }
                                //s.AppendLine();
                                elfa.AppendChild(xmlo.CreateTextNode(s.ToString()));
                            }
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("source", "#" + id + "-Pos-array");
                                elacc.SetAttribute("count", vertcnt.ToString());
                                elacc.SetAttribute("stride", "3");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "X");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "Y");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "Z");
                                    elp.SetAttribute("type", "float");
                                }
                            }
                        }
                    }
                    {
                        // S,T

                        XmlElement elsrc = xmlo.CreateElement("source", URI);
                        elsrc.SetAttribute("id", id + "-ST");
                        elmesh.AppendChild(elsrc);
                        {
                            XmlElement elfa = xmlo.CreateElement("float_array", URI);
                            elsrc.AppendChild(elfa);
                            elfa.SetAttribute("id", id + "-ST-array");
                            elfa.SetAttribute("count", stcnt.ToString());
                            {
                                StringBuilder s = new StringBuilder();
                                for (int t = 0; t < vtxcnt; t++) {
                                    //if ((t % 5) == 0) s.AppendLine();
                                    switch (t % 5) {
                                        case 3:
                                        case 4:
                                            s.Append(" " + g.meshVec5[t].ToString());
                                            break;
                                    }
                                }
                                //s.AppendLine();
                                elfa.AppendChild(xmlo.CreateTextNode(s.ToString()));
                            }
                        }
                        {
                            XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                            elsrc.AppendChild(eltec);
                            {
                                XmlElement elacc = xmlo.CreateElement("accessor", URI);
                                eltec.AppendChild(elacc);
                                elacc.SetAttribute("source", "#" + id + "-ST-array");
                                elacc.SetAttribute("count", vertcnt.ToString());
                                elacc.SetAttribute("stride", "2");
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "S");
                                    elp.SetAttribute("type", "float");
                                }
                                {
                                    XmlElement elp = xmlo.CreateElement("param", URI);
                                    elacc.AppendChild(elp);
                                    elp.SetAttribute("name", "T");
                                    elp.SetAttribute("type", "float");
                                }
                            }
                        }
                    }
                    {
                        XmlElement elvert = xmlo.CreateElement("vertices", URI);
                        elmesh.AppendChild(elvert);
                        elvert.SetAttribute("id", id + "-Vtx");
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvert.AppendChild(elin);
                            elin.SetAttribute("semantic", "POSITION");
                            elin.SetAttribute("source", "#" + id + "-Pos");
                        }
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elvert.AppendChild(elin);
                            elin.SetAttribute("semantic", "TEXCOORD");
                            elin.SetAttribute("source", "#" + id + "-TexPos");
                        }
                    }
                    foreach (GT gt in g.gts) {
                        int polycnt = (gt.meshTriVi.Length / 3);

                        XmlElement elpoly = xmlo.CreateElement("triangles", URI);
                        elmesh.AppendChild(elpoly);
                        elpoly.SetAttribute("count", polycnt.ToString());
                        if (gt.matname != null) elpoly.SetAttribute("material", gt.matname);
                        {
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elpoly.AppendChild(elin);
                            elin.SetAttribute("semantic", "VERTEX");
                            elin.SetAttribute("source", "#" + id + "-Vtx");
                            elin.SetAttribute("offset", "0");
                        }
                        {
                            // http://www.feelingsoftware.com/component/option,com_smf/Itemid,85/action,profile/u,109/sa,showPosts/lang,jp
                            XmlElement elin = xmlo.CreateElement("input", URI);
                            elpoly.AppendChild(elin);
                            elin.SetAttribute("semantic", "TEXCOORD");
                            elin.SetAttribute("source", "#" + id + "-ST");
                            elin.SetAttribute("offset", "0");
                        }
                        XmlElement elp = xmlo.CreateElement("p", URI);
                        elpoly.AppendChild(elp);
                        StringBuilder s = new StringBuilder();
                        //s.AppendLine();
                        for (int t = 0; t < polycnt; t++) {
                            s.Append(gt.meshTriVi[3 * t + 0] + " " + gt.meshTriVi[3 * t + 1] + " " + gt.meshTriVi[3 * t + 2] + " ");
                        }
                        elp.AppendChild(xmlo.CreateTextNode(s.ToString()));
                    }
                }
            }

            public void AddVS(string vsid, string nid, string nname, IDictionary<string, string> matSymTarDict, AxBone[] alaxb, SRTUt srt, string skinId, Matrix[] Ma) {
                XmlElement elvs = xmlo.CreateElement("visual_scene", URI);
                ellibvs.AppendChild(elvs);
                elvs.SetAttribute("id", vsid);
                {
                    XmlElement eln = xmlo.CreateElement("node", URI);
                    elvs.AppendChild(eln);
                    eln.SetAttribute("id", nid);
                    eln.SetAttribute("name", nname);
                    {
                        XmlElement elic = xmlo.CreateElement("instance_controller", URI);
                        eln.AppendChild(elic);
                        elic.SetAttribute("url", "#" + skinId);
                        {
                            XmlElement elskel = xmlo.CreateElement("skeleton", URI);
                            elic.AppendChild(elskel);
                            elskel.AppendChild(xmlo.CreateTextNode("#" + Namej.GetName(0)));
                        }
                        {
                            XmlElement elbm = xmlo.CreateElement("bind_material", URI);
                            elic.AppendChild(elbm);
                            {
                                XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                                elbm.AppendChild(eltec);
                                if (matSymTarDict != null) {
                                    foreach (KeyValuePair<string, string> kv in matSymTarDict) {
                                        XmlElement elim = xmlo.CreateElement("instance_material", URI);
                                        eltec.AppendChild(elim);
                                        elim.SetAttribute("symbol", kv.Key);
                                        elim.SetAttribute("target", "#" + kv.Value);
                                    }
                                }
                            }
                        }
                    }
                }
                {
                    SortedDictionary<int, XmlElement> dictNodeHier = new SortedDictionary<int, XmlElement>();
                    for (int x = 0; x < alaxb.Length; x++) {
                        String jn = Namej.GetName(x);
                        AxBone axb = alaxb[x];
                        XmlElement eln = xmlo.CreateElement("node", URI);
                        //eln.SetAttribute("type", "JOINT");
                        dictNodeHier[x] = eln;
                        if (axb.parent < 0) {
                            elvs.AppendChild(eln);
                        }
                        else {
                            dictNodeHier[axb.parent].AppendChild(eln);

                            XmlElement elin = xmlo.CreateElement("instance_node", URI);
                            elin.SetAttribute("url", "#" + Namej.GetName(axb.parent));
                            //eln.AppendChild(elin);
                        }
                        eln.SetAttribute("id", jn);
                        eln.SetAttribute("name", jn);
                        eln.SetAttribute("sid", jn);

#if false
                        AxBone axbp = axb.parent < 0 ? null : alaxb[axb.parent];

                        Vector3 v3s = new Vector3(1, 1, 1);
                        if (axbp != null) v3s = new Vector3(axbp.x1, axbp.y1, axbp.z1);

                        Vector3 v3r = Vector3.Empty;
                        if (axbp != null) v3r = new Vector3(axbp.x2, axbp.y2, axbp.z2);

                        Vector3 v3t = new Vector3(axb.x3, axb.y3, axb.z3);
                        //if (axbp != null) v3t = new Vector3(axbp.x3, axbp.y3, axbp.z3);

                        srt.Translate(eln, v3t.X, v3t.Y, v3t.Z);
                        srt.Scale(eln, v3s.X, v3s.Y, v3s.Z);
                        srt.Rotate(eln, 1, 0, 0, v3r.X);
                        srt.Rotate(eln, 0, 1, 0, v3r.Y);
                        srt.Rotate(eln, 0, 0, 1, v3r.Z);
#endif

#if true
                        Matrix Mr = Matrix.Identity;
                        Mr *= (Matrix.RotationX(axb.x2));
                        Mr *= (Matrix.RotationY(axb.y2));
                        Mr *= (Matrix.RotationZ(axb.z2));
                        Matrix Mt = Matrix.Identity;
                        Vector3 v3t = new Vector3(axb.x3, axb.y3, axb.z3);
                        Mt *= (Matrix.Translation(Vector3.TransformCoordinate(v3t, Mr)));

                        Matrix M = Matrix.Transpose(Mt * Mr);

                        M = Ma[x];
                        if (axb.parent >= 0) M = M * Matrix.Invert(Ma[axb.parent]);
                        M = Matrix.Transpose(M);

                        XmlElement elmtx = xmlo.CreateElement("matrix", URI);
                        eln.AppendChild(elmtx);
                        string str = " ";
                        str += M.M11 + " " + M.M12 + " " + M.M13 + " " + M.M14 + " ";
                        str += M.M21 + " " + M.M22 + " " + M.M23 + " " + M.M24 + " ";
                        str += M.M31 + " " + M.M32 + " " + M.M33 + " " + M.M34 + " ";
                        str += M.M41 + " " + M.M42 + " " + M.M43 + " " + M.M44 + " ";
                        elmtx.AppendChild(xmlo.CreateTextNode(str));
#endif
                    }
                }
            }

            public void AddVS(string vsid, string nid, string nname, string gid, IDictionary<string, string> matSymTarDict) {
                XmlElement elvs = xmlo.CreateElement("visual_scene", URI);
                ellibvs.AppendChild(elvs);
                elvs.SetAttribute("id", vsid);
                {
                    XmlElement eln = xmlo.CreateElement("node", URI);
                    elvs.AppendChild(eln);
                    eln.SetAttribute("id", nid);
                    eln.SetAttribute("name", nname);
                    {
                        XmlElement elig = xmlo.CreateElement("instance_geometry", URI);
                        eln.AppendChild(elig);
                        elig.SetAttribute("url", "#" + gid);
                        {
                            XmlElement elbm = xmlo.CreateElement("bind_material", URI);
                            elig.AppendChild(elbm);
                            {
                                XmlElement eltec = xmlo.CreateElement("technique_common", URI);
                                elbm.AppendChild(eltec);
                                if (matSymTarDict != null) {
                                    foreach (KeyValuePair<string, string> kv in matSymTarDict) {
                                        XmlElement elim = xmlo.CreateElement("instance_material", URI);
                                        eltec.AppendChild(elim);
                                        elim.SetAttribute("symbol", kv.Key);
                                        elim.SetAttribute("target", "#" + kv.Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void AddIVS(string vsid) {
                XmlElement elivs = xmlo.CreateElement("instance_visual_scene", URI);
                elscene.AppendChild(elivs);
                elivs.SetAttribute("url", "#" + vsid);
            }

            public void AddEffectImg(string eid, string fp) {
                XmlElement eleff = xmlo.CreateElement("effect", URI);
                ellibeffect.AppendChild(eleff);
                eleff.SetAttribute("id", eid);
                {
                    XmlElement elimg;
                    eleff.AppendChild(elimg = xmlo.CreateElement("image", URI));
                    {
                        XmlElement elif;
                        elimg.AppendChild(elif = xmlo.CreateElement("init_from", URI));
                        elif.AppendChild(xmlo.CreateTextNode(fp));
                    }
                }
            }

            public void AddEffectTex(string eid, string fp) {
                Guid idif = Guid.NewGuid();
                Guid idnp;
                Guid idsmp;
                XmlElement eleff = xmlo.CreateElement("effect", URI);
                ellibeffect.AppendChild(eleff);
                eleff.SetAttribute("id", eid);
                {
                    XmlElement elpc = xmlo.CreateElement("profile_COMMON", URI);
                    eleff.AppendChild(elpc);
                    {
                        XmlElement elnewp = xmlo.CreateElement("newparam", URI);
                        elpc.AppendChild(elnewp);
                        elnewp.SetAttribute("sid", (idnp = Guid.NewGuid()).ToString("N"));
                        {
                            XmlElement elsurf = xmlo.CreateElement("surface", URI);
                            elnewp.AppendChild(elsurf);
                            elsurf.SetAttribute("type", "2D");
                            {
                                XmlElement elif = xmlo.CreateElement("init_from", URI);
                                elsurf.AppendChild(elif);
                                elif.AppendChild(xmlo.CreateTextNode(idif.ToString("N")));
                            }
                            {
                                XmlElement elfmt = xmlo.CreateElement("format", URI);
                                elsurf.AppendChild(elfmt);
                                elfmt.AppendChild(xmlo.CreateTextNode("R8G8B8"));
                            }
                        }
                    }
                    {
                        XmlElement elnewp = xmlo.CreateElement("newparam", URI);
                        elpc.AppendChild(elnewp);
                        elnewp.SetAttribute("sid", (idsmp = Guid.NewGuid()).ToString("N"));
                        {
                            XmlElement elsmp = xmlo.CreateElement("sampler2D", URI);
                            elnewp.AppendChild(elsmp);
                            {
                                XmlElement elsrc = xmlo.CreateElement("source", URI);
                                elsmp.AppendChild(elsrc);
                                elsrc.AppendChild(xmlo.CreateTextNode(idnp.ToString("N")));
                            }
                            {
                                XmlElement elmf = xmlo.CreateElement("minfilter", URI);
                                elsmp.AppendChild(elmf);
                                elmf.AppendChild(xmlo.CreateTextNode("LINEAR_MIPMAP_LINEAR"));
                            }
                            {
                                XmlElement elmf = xmlo.CreateElement("maxfilter", URI);
                                elsmp.AppendChild(elmf);
                                elmf.AppendChild(xmlo.CreateTextNode("LINEAR"));
                            }
                        }
                    }
                    {
                        XmlDocument xmli = new XmlDocument();
                        xmli.LoadXml(Resources.blender_tec);
                        XmlElement eltec;
                        elpc.AppendChild(eltec = (XmlElement)xmlo.ImportNode(xmli.DocumentElement, true));
                        XmlElement eltex = (XmlElement)eltec.SelectSingleNode("c:phong/c:diffuse/c:texture", xns);
                        eltex.SetAttribute("texture", idsmp.ToString("N"));
                    }
                }
                AddImage(fp, idif.ToString("N"), idif.ToString("N"));
            }

            public void AddMat(string mid, string mname, string eid) {
                XmlElement elmat = xmlo.CreateElement("material", URI);
                elmat.SetAttribute("id", mid);
                elmat.SetAttribute("name", mname);
                ellibmat.AppendChild(elmat);
                {
                    XmlElement elei = xmlo.CreateElement("instance_effect", URI);
                    elmat.AppendChild(elei);
                    elei.SetAttribute("url", "#" + eid);
                }
            }
        }
    }

    class SRTUt {
        public void Scale(XmlElement el, float x, float y, float z) {
            XmlDocument xmlo = el.OwnerDocument;
            if (x != 1 || y != 1 || z != 1) {
                XmlElement elr = xmlo.CreateElement("scale", CS141.URI);
                el.AppendChild(elr);
                elr.AppendChild(xmlo.CreateTextNode(string.Format("{0:r} {1:r} {2:r}", x, y, z)));
            }
        }
        public void Rotate(XmlElement el, float x, float y, float z, float a) {
            XmlDocument xmlo = el.OwnerDocument;
            if (a != 0) {
                a = a / 3.14159f * 180.0f;
                XmlElement elr = xmlo.CreateElement("rotate", CS141.URI);
                el.AppendChild(elr);
                elr.AppendChild(xmlo.CreateTextNode(string.Format("{0:r} {1:r} {2:r} {3:r}", x, y, z, a)));
            }
        }
        public void Translate(XmlElement el, float x, float y, float z) {
            XmlDocument xmlo = el.OwnerDocument;
            if (x != 0 || y != 0 || z != 0) {
                XmlElement elr = xmlo.CreateElement("translate", CS141.URI);
                el.AppendChild(elr);
                elr.AppendChild(xmlo.CreateTextNode(string.Format("{0:r} {1:r} {2:r}", x, y, z)));
            }
        }
    }
}
