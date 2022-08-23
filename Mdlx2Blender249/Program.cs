using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using khkh_xldMii;
using khkh_xldMii.V;
using hex04BinTrack;
using vconv122;
using System.Diagnostics;
using System.Windows.Forms;
using Mdlx2Blender249.Properties;
using SlimDX;
using khkh_xldMii.Models;
using khkh_xldMii.Models.Mset;
using khkh_xldMii.Models.Mdlx;
using khkh_xldMii.Utils.Mdlx;
using khkh_xldMii.Utils.Mset;
using System.Linq;

namespace Mdlx2Blender249 {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length < 1) {
                helpYa(); return;
            }
            switch (args[0].ToLowerInvariant()) {
                case "/conv":
                    if (args.Length < 3) {
                        helpYa(); return;
                    }
                    new Program().Run(args[1], null, args[2]);
                    break;

                case "/conv2":
                    if (args.Length < 4) {
                        helpYa(); return;
                    }
                    new Program().Run(args[1], args[2], args[3]);
                    break;

                case "/select": {
                        Form form = new Form();

                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "*.mdlx|*.mdlx||";
                        ofd.Title = "Select a mdlx";
                        ofd.Multiselect = false;
                        if (ofd.ShowDialog(form) == DialogResult.OK) {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.Filter = "Blender py (*.py)|*.py||";
                            sfd.FileName = Path.Combine(Path.GetDirectoryName(ofd.FileName), Path.GetFileNameWithoutExtension(ofd.FileName).ToLowerInvariant() + ".py");
                            sfd.Title = "Save py";
                            if (sfd.ShowDialog(form) == DialogResult.OK) {
                                new Program().Run(ofd.FileName, null, sfd.FileName);
                            }
                        }
                    }
                    break;

                case "/select2": {
                        Form form = new Form();

                        OpenFileDialog ofdMdlx = new OpenFileDialog();
                        ofdMdlx.Filter = "*.mdlx|*.mdlx||";
                        ofdMdlx.Title = "Select a mdlx";
                        ofdMdlx.Multiselect = false;
                        if (ofdMdlx.ShowDialog(form) == DialogResult.OK) {
                            OpenFileDialog ofdMset = new OpenFileDialog();
                            ofdMset.Filter = "*.mset|*.mset||";
                            ofdMset.Title = "Select a mset";
                            ofdMset.Multiselect = false;
                            if (ofdMset.ShowDialog(form) == DialogResult.OK) {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "Blender py (*.py)|*.py||";
                                sfd.FileName = Path.Combine(Path.GetDirectoryName(ofdMdlx.FileName), Path.GetFileNameWithoutExtension(ofdMdlx.FileName).ToLowerInvariant() + ".py");
                                sfd.Title = "Save py";
                                if (sfd.ShowDialog(form) == DialogResult.OK) {
                                    new Program().Run(ofdMdlx.FileName, ofdMset.FileName, sfd.FileName);
                                }
                            }
                        }
                    }
                    break;

                default:
                    helpYa();
                    break;
            }
        }

        private static void helpYa() {
            Console.Error.WriteLine("Mdlx2Blender249 /conv p_ex100.mdlx p_ex100.py");
            Console.Error.WriteLine("Mdlx2Blender249 /conv2 p_ex100.mdlx p_ex100.mset p_ex100.py");
            Console.Error.WriteLine("Mdlx2Blender249 /select");
            Console.Error.WriteLine("Mdlx2Blender249 /select2");
            Environment.ExitCode = 1;
        }

        // # Import instruction:
        // # * Launch Blender 2.4.8a
        // # * In Blender, type Shift+F11, then open then Script Window
        // # * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py
        // # * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!
        // # * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.

        internal void Run(String mdlxFile, String msetFile, String bpyFile) {
            Texex2[] textures = null;
            Mdlxfst mdlx = null;
            using (FileStream fmdlx = File.OpenRead(mdlxFile)) {
                var entries = ReadBar.Explode(fmdlx);

                {
                    // timf
                    var hit = entries.First(it => it.k == 7);
                    if (hit != null) {
                        textures = TIMCollection.Load(new MemoryStream(hit.bin, false));
                    }
                }

                {
                    // m_ex
                    var hit = entries.First(it => it.k == 4);
                    if (hit != null) {
                        mdlx = new Mdlxfst(new MemoryStream(hit.bin, false));
                    }
                }
            }

            Msetfst mset = null;
            if (msetFile != null) {
                using (FileStream fmset = File.OpenRead(msetFile)) {
                    mset = new Msetfst(fmset, Path.GetFileName(msetFile));
                }
            }

            var animEnabled = mset != null;

            var pyWriter = new StringWriter();
            pyWriter.WriteLine(Properties.Resources.Base);
            pyWriter.WriteLine("print 'IMPORTER3.5 -- {0} @{1} {2}'"
                , Path.GetFileName(mdlxFile)
                , DateTime.Now.ToShortTimeString()
                , DateTime.Now.ToShortDateString()
                );
            pyWriter.WriteLine("print 'start --'");

            foreach (var model in mdlx.ModelList.Take(1)) {
                var skelList = new List<ISkelProvider>();
                if (mset == null) {
                    skelList.Add(new MdlxBased(model.boneList.bones.ToArray()));
                }
                else {
                    foreach (var mt1 in mset.motionList) {
                        String key = mt1.label.Split('#')[0] + "_" + mt1.anbOff;
                        if (mt1.isRaw) {

                        }
                        else if (mt1.anbLen > 0) {
                            var blk = new AnbReader(new MemoryStream(mt1.anbBin, false));
                            skelList.Add(new MsetBased(model.boneList.bones.ToArray(), blk.model, key));
                        }
                    }
                }

                foreach (ISkelProvider skel in skelList) {
                    var bones = skel.Bones;
                    var matrices = skel.Matrices;
                    var exports = new List<VU1Export>();
                    foreach (var vif in model.vifList) {
                        var mem = new VU1Mem();
                        var tops = 0x40;
                        new ParseVIF1(mem).Parse(new MemoryStream(vif.bin, false), tops);
                        var export = VU1MemoryExporter.Export(mem, tops, vif.textureIndex, vif.marixIndexArray, Matrix.Identity);
                        exports.Add(export);
                    }

                    var matList = new List<BlenderMaterial>();
                    {
                        int textureIndex = 0;
                        foreach (var tex in textures[0].bitmapList) {
                            string matname = Path.GetFileNameWithoutExtension(mdlxFile).Replace(".", "_").ToLowerInvariant() + "x" + textureIndex;
                            string name = matname + ".png";
                            string fp = Path.Combine(Path.GetDirectoryName(bpyFile), name);
                            matList.Add(new BlenderMaterial(matname, fp));
                            tex.bitmap.Save(fp, System.Drawing.Imaging.ImageFormat.Png);
                            textureIndex++;
                        }
                    }
                    if (true) {
                        var bMesh = new BlenderMesh();
                        if (true) {
                            int nextVertIdx = 0;
                            int nextUvIdx = 0;
                            var bVerts = new BlenderVert[4];
                            int ring = 0;
                            int[] ord = new int[] { 1, 3, 2 };
                            foreach (VU1Export export in exports) {
                                for (int x = 0; x < export.vertexIndices.Length; x++) {
                                    BlenderVert o1 = new BlenderVert(nextVertIdx + export.vertexIndices[x], nextUvIdx + x);
                                    bVerts[ring] = o1;
                                    ring = (ring + 1) & 3;
                                    if (export.flags[x] == 0x20) {
                                        BlenderTri tri = new BlenderTri(export.textureIndex,
                                            bVerts[(ring - ord[0]) & 3],
                                            bVerts[(ring - ord[1]) & 3],
                                            bVerts[(ring - ord[2]) & 3]
                                            );
                                        bMesh.triList.Add(tri);
                                    }
                                    else if (export.flags[x] == 0x30) {
                                        BlenderTri tri = new BlenderTri(export.textureIndex,
                                            bVerts[(ring - ord[0]) & 3],
                                            bVerts[(ring - ord[2]) & 3],
                                            bVerts[(ring - ord[1]) & 3]
                                            );
                                        bMesh.triList.Add(tri);
                                    }
                                }
                                for (int x = 0; x < export.rawVerts.Length; x++) {
                                    if (export.vertexMixersGroups[x] == null) {
                                        bMesh.posList.Add(Vector3.Zero);
                                        bMesh.vertexMixerList.Add(new VertexMixer[0]);
                                        continue;
                                    }
                                    if (export.vertexMixersGroups[x].Length == 1) {
                                        var mixer = export.vertexMixersGroups[x][0];
                                        mixer.factor = 1.0f;
                                        var pos = Vector3.TransformCoordinate(VCUt.V4To3(export.rawVerts[mixer.vertexIndex]), matrices[mixer.matrixIndex]);
                                        bMesh.posList.Add(pos);
                                    }
                                    else {
                                        var pos = Vector3.Zero;
                                        foreach (var mixer in export.vertexMixersGroups[x]) {
                                            pos += VCUt.V4To3(Vector4.Transform(export.rawVerts[mixer.vertexIndex], matrices[mixer.matrixIndex]));
                                        }
                                        bMesh.posList.Add(pos);
                                    }
                                    bMesh.vertexMixerList.Add(export.vertexMixersGroups[x]);
                                }
                                for (int x = 0; x < export.uvs.Length; x++) {
                                    var uv = export.uvs[x];
                                    uv.Y = 1.0f - uv.Y;
                                    bMesh.uvList.Add(uv);
                                }
                                nextVertIdx += export.rawVerts.Length;
                                nextUvIdx += export.uvs.Length;
                            }
                        }

                        var map = new SortedDictionary<int, List<int>>();
                        pyWriter.WriteLine("mat = MyMats()");
                        {   // materials
                            for (int x = 0; x < matList.Count; x++) {
                                var mat = matList[x];
                                pyWriter.WriteLine("mat.AddImage('{0}', '{1}', '{2}', True)", mat.texname, mat.matname, mat.pngFile.Replace("\\", "/"));
                            }
                        }

                        var view = Matrix.Identity;

                        {   // joints, bones
                            pyWriter.WriteLine("bone = MyBone()");
                            pyWriter.WriteLine("bone.Prepare(scene, 1)");
                            for (int x = 0; x < matrices.Length; x++) {
                                Vector3 v0 = (Vector3.TransformCoordinate(new Vector3(0, 0, 0), matrices[x] * view));
                                Vector3 v1 = (Vector3.TransformCoordinate(new Vector3(1, 0, 0), matrices[x] * view));

                                pyWriter.WriteLine("bone.AddBone({0}, {1}, {2:r}, {3:r}, {4:r}, {5:r}, {6:r}, {7:r})"
                                    , naming.B(x)
                                    , naming.B(bones[x].parent)
                                    , v0.X, v0.Y, v0.Z
                                    , v1.X, v1.Y, v1.Z
                                    );
                            }
                            pyWriter.WriteLine("bone.PrepareEnd()");
                        }

                        if (animEnabled) {
                            // joints, bones x ... subjective animation
                            pyWriter.WriteLine("boneani = MyBone()");
                            pyWriter.WriteLine("boneani.Prepare(scene, 2)");
                            for (int x = 0; x < matrices.Length; x++) {
                                Vector3 v0 = (Vector3.TransformCoordinate(new Vector3(0, 0, 0), Matrix.Identity));
                                Vector3 v1 = (Vector3.TransformCoordinate(new Vector3(1, 0, 0), Matrix.Identity));

                                pyWriter.WriteLine("boneani.AddBone({0}, {1}, {2:r}, {3:r}, {4:r}, {5:r}, {6:r}, {7:r})"
                                    , naming.BAni(x)
                                    , naming.BAni(bones[x].parent)
                                    , v0.X, v0.Y, v0.Z
                                    , v1.X, v1.Y, v1.Z
                                    );
                            }
                            pyWriter.WriteLine("boneani.PrepareEnd()");
                        }

                        if (animEnabled) {
                            // bond bone sets
                            String boneaniNames = "";
                            for (int x = 0; x < matrices.Length; x++) {
                                boneaniNames += "" + naming.BAni(x) + ",";
                            }
                            String boneNames = "";
                            for (int x = 0; x < matrices.Length; x++) {
                                boneNames += "" + naming.B(x) + ",";
                            }
                            pyWriter.WriteLine("BondUt.BondArm(boneani.armObj, ");
                            pyWriter.WriteLine(" [" + boneaniNames + "],");
                            pyWriter.WriteLine(" bone.armObj,");
                            pyWriter.WriteLine(" [" + boneNames + "],");
                            pyWriter.WriteLine(" " + matrices.Length + ",");
                            pyWriter.WriteLine(" 'srt'");
                            pyWriter.WriteLine(" )");
                        }

                        pyWriter.WriteLine("alMyMesh = []");
                        pyWriter.WriteLine("ya = MyMesh()");
                        pyWriter.WriteLine("ya.PrepareMesh('Sola')");

                        {   // position xyz
                            int mapi = 0;
                            int ct = bMesh.triList.Count;
                            pyWriter.WriteLine("ya.AddCoords([");
                            for (int t = 0; t < ct; t++) {
                                BlenderTri X3 = bMesh.triList[t];
                                for (int i = 0; i < X3.verts.Length; i++) {
                                    BlenderVert X1 = X3.verts[i];
                                    Vector3 v = Vector3.TransformCoordinate(bMesh.posList[X1.vertexIdx], view);
                                    pyWriter.WriteLine(" [{0:r},{1:r},{2:r}], # pos{3}.{4}", v.X, v.Y, v.Z, t, i);

                                    if (map.ContainsKey(X1.vertexIdx) == false) map[X1.vertexIdx] = new List<int>();
                                    map[X1.vertexIdx].Add(mapi); mapi++;
                                }
                            }
                            pyWriter.WriteLine(" ])");
                        }
                        {   // mats
                            for (int x = 0; x < matList.Count; x++) {
                                pyWriter.WriteLine("ya.AddMat(mat.GetMat({0}))", x);
                            }
                        }
                        {   // AddColorUvMatFaces
                            int cf = bMesh.triList.Count;
                            pyWriter.WriteLine("ya.AddColorUvMatFaces(");
                            // faces
                            pyWriter.WriteLine(" [");
                            for (int f = 0; f < cf; f++) {
                                pyWriter.WriteLine(" [{0}, {1}, {2}], #face{3} tri", 3 * f + 0, 3 * f + 1, 3 * f + 2, f);
                            }
                            pyWriter.WriteLine(" ],[");
                            // clr
                            for (int f = 0; f < cf; f++) {
                                pyWriter.WriteLine(" [[255,255,255,255],[255,255,255,255],[255,255,255,255]], #face{0} clr", f);
                            }
                            pyWriter.WriteLine(" ],[");
                            // uv
                            for (int f = 0; f < cf; f++) {
                                pyWriter.WriteLine(" [{0}], #face{1} uv", LUt.GetUV2(bMesh.triList[f], bMesh.uvList), f);
                            }
                            pyWriter.WriteLine(" ],[");
                            // matimg
                            for (int f = 0; f < cf; f++) {
                                pyWriter.WriteLine(" [{0}, mat.GetImage({1})], #face{2} MatImg", bMesh.triList[f].materialIndex, bMesh.triList[f].materialIndex, f);
                            }
                            pyWriter.WriteLine(" ]");
                            pyWriter.WriteLine(" )");
                        }

                        pyWriter.WriteLine("ya.MeshToOb('Sola0')");

                        {   // vertgrps

                            var dict = new SortedDictionary<VertexMixer, List<int>>(new VertexMixerSorter()); // [grp] = vi[]
                            for (int x = 0; x < bMesh.vertexMixerList.Count; x++) {
                                foreach (var mixer in bMesh.vertexMixerList[x]) {
                                    int v = x;
                                    if (dict.ContainsKey(mixer) == false) dict[mixer] = new List<int>();
                                    dict[mixer].Add(v);
                                }
                            }
                            foreach (var pair in dict) {
                                pyWriter.WriteLine("ya.SetVertGr2({0},{1:r},{2})"
                                    , naming.B(pair.Key.matrixIndex)
                                    , pair.Key.factor
                                    , LUt.GetVi(pair.Value, map)
                                    );
                            }
                        }

                        pyWriter.WriteLine("bone.AddChildWithArmature(ya.yaOb)");
                        pyWriter.WriteLine("alMyMesh.append(ya)");

                    }
                    if (skel is MsetBased) {
                        var blk = ((MsetBased)skel).blk;
                        var key = ((MsetBased)skel).key;

                        int cnt1 = bones.Length - blk.t5List.Count;
                        int cnt2 = blk.t5List.Count;

                        float maxtick = (blk.t11List.Length != 0) ? blk.t11List[blk.t11List.Length - 1] : 0;
                        float mintick = (blk.t11List.Length != 0) ? blk.t11List[0] : 0;
                        SortedDictionary<float, UtF.M[]> dict = UtF.GetKeyframes(cnt1, cnt2, blk);
                        pyWriter.WriteLine("Aniut.SetPoseSRTv('{0}', boneani, [", key);
                        bool firstFrame = true;
                        foreach (KeyValuePair<float, UtF.M[]> kv in dict) {
                            List<AxBone> alaxb4ani = UtF.FillKeyframe(bones, blk, kv.Key, false);
                            pyWriter.WriteLine(" {'frame':" + (1 + kv.Key) + ",");
                            pyWriter.WriteLine("  'joints': [");
                            for (int ji = 0; ji < kv.Value.Length; ji++) {
                                UtF.M mask = firstFrame
                                    ? UtF.M.All
                                    : kv.Value[ji];
                                bool is2 = !(ji < cnt1);
                                if (mask != UtF.M.None) {
                                    String bName = naming.BAni(ji);
                                    pyWriter.WriteLine("   {'b':" + bName + ",");

                                    if ((mask & UtF.M.S) != UtF.M.None) {
                                        float tx = alaxb4ani[ji].x1;
                                        float ty = alaxb4ani[ji].y1;
                                        float tz = alaxb4ani[ji].z1;

                                        pyWriter.WriteLine("    'sv':[ {0:r}, {1:r}, {2:r} ],"
                                            , tx
                                            , ty
                                            , tz
                                            );
                                    }
                                    if ((mask & UtF.M.R) != UtF.M.None) {
                                        float rx = alaxb4ani[ji].x2;
                                        float ry = alaxb4ani[ji].y2;
                                        float rz = alaxb4ani[ji].z2;

                                        pyWriter.WriteLine("    'rv':[ {0:r}, {1:r}, {2:r} ],"
                                            , rx / 3.14159f * 180
                                            , ry / 3.14159f * 180
                                            , rz / 3.14159f * 180
                                            );

                                        Quaternion quat = Quaternion.Identity;
                                        if (rx != 0) quat *= Quaternion.RotationAxis(Vector3.UnitX, rx);
                                        if (ry != 0) quat *= Quaternion.RotationAxis(Vector3.UnitY, ry);
                                        if (rz != 0) quat *= Quaternion.RotationAxis(Vector3.UnitZ, rz);

                                        pyWriter.WriteLine("    'qv':[ {0:r}, {1:r}, {2:r}, {3:r} ],"
                                            , quat.X
                                            , quat.Y
                                            , quat.Z
                                            , quat.W
                                            );
                                    }
                                    if ((mask & UtF.M.T) != UtF.M.None) {
                                        float tx = alaxb4ani[ji].x3;
                                        float ty = alaxb4ani[ji].y3;
                                        float tz = alaxb4ani[ji].z3;

                                        pyWriter.WriteLine("    'tv':[ {0:r}, {1:r}, {2:r} ],"
                                            , tx
                                            , ty
                                            , tz
                                            );
                                    }
                                    pyWriter.WriteLine("   },");
                                }
                            }
                            firstFrame = false;
                            pyWriter.WriteLine("  ]");
                            pyWriter.WriteLine(" },");
                        }
                        pyWriter.WriteLine(" ])");
                    }
                    pyWriter.WriteLine("for o in alMyMesh:");
                    pyWriter.WriteLine(" o.yaOb.select(False)");
                    pyWriter.WriteLine("bone.SetScale(0.1, 0.1, 0.1)");
                    pyWriter.WriteLine("bone.armObj.select(False)");
                    if (animEnabled) {
                        pyWriter.WriteLine("boneani.SetScale(0.1, 0.1, 0.1)");
                        pyWriter.WriteLine("boneani.armObj.select(True)");
                    }
                    pyWriter.WriteLine("scene.update(1)");

                    pyWriter.WriteLine();

                    pyWriter.WriteLine("Blender.Window.RedrawAll()");
                    pyWriter.WriteLine("print 'end --'");

                    {
                        var filePath = Path.Combine(Path.GetDirectoryName(bpyFile), "vif.txt");
                        var writer = new StringWriter();
                        foreach (var (export, index) in exports.Select((export, index) => (export, index))) {
                            writer.WriteLine("");
                            writer.WriteLine($"VPU1 memory snapshot #{index} after VIF/DMA transmission");
                            writer.WriteLine("");
                            export.hexDump.WriteTo(writer);

                            var markPng = Path.Combine(Path.GetDirectoryName(bpyFile), $"mark{index}.png");
                            export.markDump.GetBitmap().Save(markPng);
                        }
                        File.WriteAllText(filePath, writer.ToString());
                    }
                    break;
                }
                break;
            }

            File.WriteAllText(bpyFile, pyWriter.ToString(), Encoding.ASCII);
        }

        private class MsetBased : MdlxBased {
            internal AnbModel blk;
            internal String key;

            internal MsetBased(AxBone[] bones, AnbModel blk, String key) {
                this.blk = blk;
                this.key = key;

                int cnt1 = bones.Length;
                int cnt2 = blk.t5List.Count;

                this.Bones = bones = UtF.FillKeyframe(bones, blk, float.NaN, true).ToArray();
                this.Matrices = new Matrix[bones.Length];

                this.scaleTransArray = new Vector3[Matrices.Length];
                this.rotateArray = new Quaternion[Matrices.Length];
                for (int x = 0; x < Matrices.Length; x++) {
                    Quaternion Qo;
                    Vector3 Vo;
                    AxBone axb = bones[x];
                    int parent = axb.parent;
                    if (parent < 0) {
                        Qo = Quaternion.Identity;
                        Vo = Vector3.Zero;
                    }
                    else {
                        Qo = rotateArray[parent];
                        Vo = scaleTransArray[parent];
                    }

                    Vector3 Vt = Vector3.TransformCoordinate(new Vector3(axb.x3, axb.y3, axb.z3), Matrix.RotationQuaternion(Qo));
                    scaleTransArray[x] = Vo + Vt;

                    Quaternion Qt = Quaternion.Identity;
                    if (axb.x2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(1, 0, 0), axb.x2));
                    if (axb.y2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 1, 0), axb.y2));
                    if (axb.z2 != 0) Qt *= (Quaternion.RotationAxis(new Vector3(0, 0, 1), axb.z2));
                    rotateArray[x] = Qt * Qo;
                }
                for (int x = 0; x < Matrices.Length; x++) {
                    Matrix M = Matrix.RotationQuaternion(rotateArray[x]);
                    M *= (Matrix.Translation(scaleTransArray[x]));
                    Matrices[x] = M;
                }
            }
        }

        private interface ISkelProvider {
            AxBone[] Bones { get; }
            Matrix[] Matrices { get; }
        }

        private class MdlxBased : ISkelProvider {
            public AxBone[] Bones { get; internal set; }
            public Matrix[] Matrices { get; internal set; }

            protected Vector3[] scaleTransArray;
            protected Quaternion[] rotateArray;

            internal MdlxBased() { }

            internal MdlxBased(AxBone[] bones) {
                this.Bones = bones;
                this.Matrices = new Matrix[bones.Length];

                this.scaleTransArray = new Vector3[Matrices.Length];
                this.rotateArray = new Quaternion[Matrices.Length];
                for (int x = 0; x < Matrices.Length; x++) {
                    Quaternion parentRotate;
                    Vector3 scaleTrans;
                    var bone = bones[x];
                    int parent = bone.parent;
                    if (parent < 0) {
                        parentRotate = Quaternion.Identity;
                        scaleTrans = Vector3.Zero;
                    }
                    else {
                        parentRotate = rotateArray[parent];
                        scaleTrans = scaleTransArray[parent];
                    }

                    var trans = Vector3.TransformCoordinate(new Vector3(bone.x3, bone.y3, bone.z3), Matrix.RotationQuaternion(parentRotate));
                    scaleTransArray[x] = scaleTrans + trans;

                    var rotate = Quaternion.Identity;
                    if (bone.x2 != 0) rotate *= (Quaternion.RotationAxis(new Vector3(1, 0, 0), bone.x2));
                    if (bone.y2 != 0) rotate *= (Quaternion.RotationAxis(new Vector3(0, 1, 0), bone.y2));
                    if (bone.z2 != 0) rotate *= (Quaternion.RotationAxis(new Vector3(0, 0, 1), bone.z2));
                    rotateArray[x] = rotate * parentRotate;
                }
                for (int x = 0; x < Matrices.Length; x++) {
                    var matrix = Matrix.RotationQuaternion(rotateArray[x]);
                    matrix *= (Matrix.Translation(scaleTransArray[x]));
                    Matrices[x] = matrix;
                }
            }
        }

        private class VertexMixerSorter : IComparer<VertexMixer> {
            public int Compare(VertexMixer x, VertexMixer y) {
                int v;
                v = x.matrixIndex.CompareTo(y.matrixIndex); if (v != 0) return v;
                v = -x.factor.CompareTo(y.factor); if (v != 0) return v;
                return 0;
            }
        }

        private class BlenderMaterial {
            public string matname;
            public string texname;
            public string pngFile;

            public BlenderMaterial(string basename, string pngFile) {
                this.matname = "mat" + basename;
                this.texname = "tex" + basename;
                this.pngFile = pngFile;
            }
        }

        private readonly Naming naming = new Naming();

        private class Naming {
            internal string B(int x) {
                if (x < 0)
                    return "None";
                return "'B" + x.ToString("000") + "'";
            }

            internal string BAni(int x) {
                if (x < 0)
                    return "None";
                return "'BAni" + x.ToString("000") + "'";
            }
        }

        private class VCUt {
            internal static Vector3 V4To3(Vector4 v) {
                return new Vector3(v.X, v.Y, v.Z);
            }
        }

        private class LUt {
            internal static string GetFaces(BlenderTri X3) {
                String s = "";
                s += String.Format("{0},{1},{2}", X3.verts[0].vertexIdx, X3.verts[1].vertexIdx, X3.verts[2].vertexIdx);
                return s;
            }

            internal static object GetUV2(BlenderTri X3, List<Vector2> uvList) {
                String s = "";
                foreach (BlenderVert X1 in X3.verts) {
                    Vector2 uv = uvList[X1.texIdx];
                    s += String.Format("[{0:r},{1:r}],", uv.X, uv.Y);
                }
                return s;
            }

            internal static string GetVi(List<int> al, IDictionary<int, List<int>> map) {
                String s = "";
                foreach (int v in al) {
                    if (map.ContainsKey(v)) {
                        foreach (int vv in map[v]) {
                            s += vv + ",";
                        }
                    }
                }
                return "[" + s + "]";
            }
        }

        private class BlenderVert {
            public int vertexIdx;
            public int texIdx;

            public BlenderVert(int vertexIdx, int texIdx) {
                this.vertexIdx = vertexIdx;
                this.texIdx = texIdx;
            }
        }

        private class BlenderTri {
            public BlenderTri(int materialIndex, BlenderVert a, BlenderVert b, BlenderVert c) {
                this.materialIndex = materialIndex;
                this.verts = new BlenderVert[] { a, b, c };
            }

            public BlenderVert[] verts;
            public int materialIndex;
        }

        private class BlenderMesh {
            public List<Vector3> posList = new List<Vector3>();
            public List<Vector2> uvList = new List<Vector2>();
            public List<BlenderTri> triList = new List<BlenderTri>();
            public List<VertexMixer[]> vertexMixerList = new List<VertexMixer[]>();
        }

        /// <summary>
        /// UtFrame
        /// </summary>
        class UtF {
            public static List<AxBone> FillKeyframe(AxBone[] alaxb1, AnbModel toval, float frame, bool fInc) {
                List<AxBone> alres = new List<AxBone>();
                int cnt1 = fInc ? alaxb1.Length : (alaxb1.Length - toval.t5List.Count);
                int cnt2 = toval.t5List.Count;
                int cx = cnt1 + cnt2;
                for (int x = 0; x < cx; x++) {
                    bool is1st = x < cnt1;
                    AxBone axb = (is1st ? alaxb1[x] : toval.t5List[x - cnt1]).Clone();
                    if (!float.IsNaN(frame)) {
                        foreach (T1 t1 in toval.t1List) {
                            if (t1.c00 == x) {
                                float fv = t1.c04;
                                switch (t1.c02) {
                                    case 0: axb.x1 = fv; break;// Sx
                                    case 1: axb.y1 = fv; break;// Sy
                                    case 2: axb.z1 = fv; break;// Sz
                                    case 3: axb.x2 = fv; break;// Rx
                                    case 4: axb.y2 = fv; break;// Ry
                                    case 5: axb.z2 = fv; break;// Rz
                                    case 6: axb.x3 = fv; break;// Tx
                                    case 7: axb.y3 = fv; break;// Ty
                                    case 8: axb.z3 = fv; break;// Tz
                                }
                            }
                        }
                        if (is1st)
                            foreach (T2 t2 in toval.t2List) {
                                int pos = t2.c00;
                                int ax = t2.c02;

                                int tpos = 8 * pos + (ax & 15) - 3;
                                int xpos = tpos / 8;
                                if (xpos == x) {
                                    float yv = YVal.calc2(frame, t2.al9f);

                                    if (false) { }
                                    else if ((tpos % 8) == 0)
                                        axb.x2 = yv;
                                    else if ((tpos % 8) == 1)
                                        axb.y2 = yv;
                                    else if ((tpos % 8) == 2)
                                        axb.z2 = yv;
                                    else if ((tpos % 8) == 3)
                                        axb.x3 = yv;
                                    else if ((tpos % 8) == 4)
                                        axb.y3 = yv;
                                    else if ((tpos % 8) == 5)
                                        axb.z3 = yv;
                                }
                            }
                        else
                            foreach (T2 t2 in toval.t2xList) {
                                int pos = t2.c00;
                                int ax = t2.c02;

                                int tpos = 8 * pos + (ax & 15) - 3;
                                int xpos = cnt1 + tpos / 8;
                                if (xpos == x) {
                                    float yv = YVal.calc2(frame, t2.al9f);

                                    if (false) { }
                                    else if ((tpos % 8) == 0)
                                        axb.x2 = yv;
                                    else if ((tpos % 8) == 1)
                                        axb.y2 = yv;
                                    else if ((tpos % 8) == 2)
                                        axb.z2 = yv;
                                    else if ((tpos % 8) == 3)
                                        axb.x3 = yv;
                                    else if ((tpos % 8) == 4)
                                        axb.y3 = yv;
                                    else if ((tpos % 8) == 5)
                                        axb.z3 = yv;
                                }
                            }
                    }
                    alres.Add(axb);
                }
                return alres;
            }

            [Flags]
            public enum M { // Mask
                None = 0, S = 1, R = 2, T = 4,
                All = S | R | T,
            }

            class SUt {
                SortedDictionary<float, M[]> dict;
                int cnt12;

                internal SUt(SortedDictionary<float, M[]> dict, int cnt12) {
                    this.dict = dict;
                    this.cnt12 = cnt12;
                }

                internal void Set(float frame, int ji, M val) {
                    if (dict.ContainsKey(frame) == false) {
                        dict[frame] = new M[cnt12];
                    }

                    dict[frame][ji] |= val;
                }
            }

            public static SortedDictionary<float, M[]> GetKeyframes(int cnt1, int cnt2, AnbModel toval) {
                SortedDictionary<float, M[]> dict = new SortedDictionary<float, M[]>();
                SUt sUt = new SUt(dict, cnt1 + cnt2);
                foreach (T2 t2 in toval.t2List) {
                    int pos = t2.c00;
                    int ax = t2.c02;

                    int tpos = 8 * pos + (ax & 15) - 3;
                    int xpos = tpos / 8;
                    foreach (T9f t9 in t2.al9f) {
                        if (false) { }
                        else if ((tpos % 8) == 0)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 1)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 2)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 3)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 4)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 5)
                            sUt.Set(t9.v0, xpos, M.T);
                    }
                }
                foreach (T2 t2 in toval.t2xList) {
                    int pos = t2.c00;
                    int ax = t2.c02;

                    int tpos = 8 * pos + (ax & 15) - 3;
                    int xpos = cnt1 + tpos / 8;
                    foreach (T9f t9 in t2.al9f) {
                        if (false) { }
                        else if ((tpos % 8) == 0)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 1)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 2)
                            sUt.Set(t9.v0, xpos, M.R);
                        else if ((tpos % 8) == 3)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 4)
                            sUt.Set(t9.v0, xpos, M.T);
                        else if ((tpos % 8) == 5)
                            sUt.Set(t9.v0, xpos, M.T);
                    }
                }
                return dict;
            }

            class YVal {
                public static float calc2(float fpos, List<T9f> alt9) {
                    T9f t9a = null;
                    for (int x9 = 0; x9 < alt9.Count; x9++) {
                        T9f t9b = alt9[x9];
                        if (t9a != null) {
                            if (fpos <= t9b.v0) {
                                float yv = Composite2.calc(
                                    fpos,
                                    t9a.v0, t9a.v1, t9a.v3,
                                    t9b.v0, t9b.v1, t9b.v2
                                    );

                                return yv;
                            }
                        }
                        t9a = t9b;
                    }
                    if (t9a != null)
                        return t9a.v1;
                    return float.NaN;
                }

            }

            class Composite2 {
                public static float calc(float tick, float v0x, float v0y, float v0w, float v1x, float v1y, float v1z) {
                    tick -= v0x;
                    v1x -= v0x;
                    float ratio = tick / v1x;
                    float r1 = ((1.0f - ratio) * v0y) + (ratio * v1y);
                    float r2 = ((1.0f - ratio) * v0w) + (ratio * v1z);
                    return r1 + r2;
                }
            }
        }
    }
}
