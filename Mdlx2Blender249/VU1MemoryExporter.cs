using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using hex04BinTrack;
using SlimDX;
using Mdlx2Blender249.Utils;
using SlimDX.Direct3D9;
using System.Linq;

namespace khkh_xldMii.V {
    public class VU1MemoryExporter {
        private enum ModelType {
            BG = 0,
            SKL = 1,
            SHADOW = 2,
        }

        private static readonly System.Drawing.Color weightGroupCountColor = System.Drawing.Color.LightSkyBlue;
        private static readonly System.Drawing.Color headerColor = System.Drawing.Color.DarkGray;
        private static readonly System.Drawing.Color matrixColor = System.Drawing.Color.CornflowerBlue;
        private static readonly System.Drawing.Color stripNodeColor = System.Drawing.Color.GreenYellow;
        private static readonly System.Drawing.Color vertexCoordsColor = System.Drawing.Color.YellowGreen;

        public static VU1Export Export(
            VU1Mem vu1mem,
            int tops,
            int textureIndex,
            int[] matrixIndexArray,
            Matrix worldMatrix
        ) {
            MemoryStream si = new MemoryStream(vu1mem.vumem, true);
            BinaryReader br = new BinaryReader(si);
            var hd = new AnnotHexDump(vu1mem.vumem);
            var md = new AnnotMarkDump(vu1mem.vumem);

            var topOffset = 16 * (tops);
            si.Position = topOffset;

            var type = (ModelType)br.ReadInt32();
            if (type != ModelType.SKL && type != ModelType.BG) {
                throw new ProtInvalidTypeException();
            }
            int vertexColorPtrInc = br.ReadInt32();
            int magicNumber = br.ReadInt32();
            int vertexBuffersPointer = br.ReadInt32();
            int stripNodeNum = br.ReadInt32(); // cnt box2
            int stripNodeOffset = br.ReadInt32(); // off box2 {tx ty vi fl}
            int applyMatrixCountOffset = br.ReadInt32(); // off box1
            int matrixOffset = br.ReadInt32(); // off matrices
            int colorNum = (type == ModelType.SKL) ? br.ReadInt32() : 0; // cntvertscolor
            int colorOffset = (type == ModelType.SKL) ? br.ReadInt32() : 0; // offvertscolor
            int weightGroupNum = (type == ModelType.SKL) ? br.ReadInt32() : 0; // cnt spec
            int weightGroupCountOffset = (type == ModelType.SKL) ? br.ReadInt32() : 0; // off spec
            int vertexCoordNum = br.ReadInt32(); // cnt verts 
            int vertexCoordOffset = br.ReadInt32(); // off verts
            int vertexIndexOffset = br.ReadInt32(); // 
            int matrixNum = br.ReadInt32(); // cnt box1

            string FormatOffset(int offset) {
                return $"0x{offset}  (Look at {(0x400 + 16 * offset):X8})";
            }

            hd.AddComment(topOffset + 0, 4, $"type={type}");
            hd.AddComment(topOffset + 4, 4, $"vertexColorPtrInc={vertexColorPtrInc}");
            hd.AddComment(topOffset + 8, 4, $"magicNumber={magicNumber}");
            hd.AddComment(topOffset + 12, 4, $"vertexBuffersPointer={vertexBuffersPointer}");

            hd.AddComment(topOffset + 16, 4, $"stripNodeNum={stripNodeNum}");
            hd.AddComment(topOffset + 20, 4, $"stripNodeOffset={FormatOffset(stripNodeOffset)}");
            hd.AddComment(topOffset + 24, 4, $"applyMatrixCountOffset={FormatOffset(applyMatrixCountOffset)}");
            hd.AddComment(topOffset + 28, 4, $"matrixOffset={FormatOffset(matrixOffset)}");

            if (type == ModelType.SKL) {
                hd.AddComment(topOffset + 32, 4, $"colorNum={colorNum}");
                hd.AddComment(topOffset + 36, 4, $"colorOffset={FormatOffset(colorOffset)}");
                hd.AddComment(topOffset + 40, 4, $"weightGroupNum={weightGroupNum}");
                hd.AddComment(topOffset + 44, 4, $"weightGroupCountOffset={FormatOffset(weightGroupCountOffset)}");

                hd.AddComment(topOffset + 48, 4, $"vertexCoordNum={vertexCoordNum}");
                hd.AddComment(topOffset + 52, 4, $"vertexCoordOffset={FormatOffset(vertexCoordOffset)}");
                hd.AddComment(topOffset + 56, 4, $"vertexIndexOffset={FormatOffset(vertexIndexOffset)}");
                hd.AddComment(topOffset + 60, 4, $"matrixNum={matrixNum}");

                md.Mark(topOffset, 64, $"{type} Header", headerColor);
            }
            else {
                hd.AddComment(topOffset + 32, 4, $"vertexCoordNum={vertexCoordNum}");
                hd.AddComment(topOffset + 36, 4, $"vertexCoordOffset={FormatOffset(vertexCoordOffset)}");
                hd.AddComment(topOffset + 40, 4, $"vertexIndexOffset={FormatOffset(vertexIndexOffset)}");
                hd.AddComment(topOffset + 44, 4, $"matrixNum={matrixNum}");

                md.Mark(topOffset, 48, $"{type} Header", headerColor);
            }

            var hasFactor = 1 <= weightGroupNum;

            si.Position = 16 * (tops + applyMatrixCountOffset);
            int[] microMatrixRef = new int[matrixNum];
            {
                int x = 0;
                for (; x < microMatrixRef.Length; x++) {
                    var savedPos = (int)si.Position;
                    microMatrixRef[x] = br.ReadInt32();

                    hd.AddComment(savedPos, 4, $"matrix[{x}] has {microMatrixRef[x]} vertices to be transformed");
                    md.Mark(savedPos, 4, $"applyMatrixCount", headerColor);
                }
            }

            VU1Export export = new VU1Export();
            export.textureIndex = textureIndex;
            export.rawVerts = new Vector4[vertexCoordNum];
            export.avail = (weightGroupNum == 0) && (type == ModelType.SKL);
            export.vertexMixersGroups = new VertexMixer[vertexCoordNum][];

            VertexMixer[] mixers = new VertexMixer[vertexCoordNum];

            int mixerIndex = 0;
            si.Position = 16 * (tops + vertexCoordOffset);
            for (int x = 0; x < microMatrixRef.Length; x++) {
                int numVerts = microMatrixRef[x];

                var savedPos = (int)si.Position;
                hd.AddComment(savedPos + 0, 4, $"Reading {numVerts} vertexCoords which belong to matrix[{x}]");

                for (int t = 0; t < numVerts; t++, mixerIndex++) {
                    savedPos = (int)si.Position;
                    float fx = br.ReadSingle();
                    float fy = br.ReadSingle();
                    float fz = br.ReadSingle();
                    float factor = br.ReadSingle();
                    Vector4 v4 = new Vector4(fx, fy, fz, factor);
                    export.rawVerts[mixerIndex] = Vector4.Transform(v4, worldMatrix);
                    export.vertexMixersGroups[mixerIndex] = new VertexMixer[] {
                        mixers[mixerIndex] = new VertexMixer(matrixIndexArray[x], x, mixerIndex, factor)
                    };

                    hd.AddComment(savedPos + 0, 16, $"vertexCoord[{mixerIndex}]: x={fx,6:0.0}  y={fy,6:0.0}  z={fz,6:0.0}  factor={factor,6:0.000}");
                    md.Mark(savedPos, hasFactor ? 16 : 12, $"vertexCoords", vertexCoordsColor);
                }
            }

            export.uvs = new Vector2[stripNodeNum];
            export.vertexIndices = new int[stripNodeNum];
            export.flags = new int[stripNodeNum];

            si.Position = 16 * (tops + stripNodeOffset);
            for (int x = 0; x < stripNodeNum; x++) {
                var savedPos = (int)si.Position;
                int tx = br.ReadUInt16() / 16; br.ReadUInt16();
                int ty = br.ReadUInt16() / 16; br.ReadUInt16();
                export.uvs[x] = new Vector2(tx / 256.0f, ty / 256.0f);
                int refIdx = export.vertexIndices[x] = br.ReadUInt16(); br.ReadUInt16();
                export.flags[x] = br.ReadUInt16(); br.ReadUInt16();

                hd.AddComment(savedPos + 0, 16, $"stripNode[{x,3}]: u={export.uvs[x].X:0.00} v={export.uvs[x].Y:0.00} vertexIdx={refIdx,2} flags=0x{export.flags[x]:X2}");
                md.Mark(savedPos + 0, 2, $"stripNode", stripNodeColor);
                md.Mark(savedPos + 4, 2, $"stripNode", stripNodeColor);
                md.Mark(savedPos + 8, 1, $"stripNode", stripNodeColor);
                md.Mark(savedPos + 12, 1, $"stripNode", stripNodeColor);
            }

            for (int x = 0; x < matrixIndexArray.Length; x++) {
                var savedPos = 16 * (tops + matrixOffset + 4 * x);
                hd.AddComment(savedPos, 64, $"matrix[{x}] should be here");
                md.Mark(savedPos, 64, $"matrix", matrixColor);
            }

            if (weightGroupNum != 0) {
                si.Position = 16 * (tops + weightGroupCountOffset);

                var savedPos = (int)si.Position;

                int vt0 = br.ReadInt32();
                int vt1 = br.ReadInt32();
                int vt2 = br.ReadInt32();
                int vt3 = br.ReadInt32();
                int vt4 = 0;
                int vt5 = 0;
                int vt6 = 0;
                int vt7 = 0;

                if (weightGroupNum >= 5) {
                    vt4 = br.ReadInt32();
                    vt5 = br.ReadInt32();
                    vt6 = br.ReadInt32();
                    vt7 = br.ReadInt32();
                }

                {
                    var array = new int[] { vt0, vt1, vt2, vt3, vt4, vt5, vt6, vt7 };
                    for (int x = 0; x < weightGroupNum; x++) {
                        hd.AddComment(savedPos + 4 * x, 4, $"weightGroup{1 + x}Count={array[x]}");
                        md.Mark(savedPos + 4 * x, 4, $"weightGroupCount", weightGroupCountColor);
                    }
                }

                string FormatMixers(VertexMixer[] vertexMixers) {
                    return string.Join(", ",
                        vertexMixers
                            .Select(it => $"(vertexCoord:{it.vertexIndex})")
                        );

                    //.Select(it => $"(vertexIdx:{it.vertexIndex}, matrix:{it.microMatrixIndex}, factor:{it.factor})")
                }

                VertexMixer[][] vertexMixersGroups = new VertexMixer[vertexCoordNum][];
                int xi = 0;
                for (xi = 0; xi < vt0; xi++) {
                    savedPos = (int)si.Position;
                    int ai = br.ReadInt32();
                    vertexMixersGroups[xi] = (new VertexMixer[] { mixers[ai] });

                    hd.AddComment(savedPos, 4, $"weightGroup1[{xi}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                    md.Mark(savedPos, 4, $"weightGroup", weightGroupCountColor);
                }

                if (weightGroupNum >= 2) {
                    //Debug.Fail("v28: " + v28);

                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt1; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1] });

                        hd.AddComment(savedPos, 8, $"weightGroup2[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 8, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 3) {
                    //Debug.Fail("v28: " + v28);

                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt2; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1], mixers[i2] });

                        hd.AddComment(savedPos, 12, $"weightGroup3[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 12, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 4) {
                    //Debug.Fail("v28: " + v28);

                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt3; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1], mixers[i2], mixers[i3] });

                        hd.AddComment(savedPos, 16, $"weightGroup4[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 16, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 5) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt4; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        int i4 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1], mixers[i2], mixers[i3], mixers[i4] });

                        hd.AddComment(savedPos, 20, $"weightGroup5[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 20, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 6) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt5; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        int i4 = br.ReadInt32();
                        int i5 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1], mixers[i2], mixers[i3], mixers[i4], mixers[i5] });

                        hd.AddComment(savedPos, 24, $"weightGroup6[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 24, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 7) {
                    si.Position = (si.Position + 15) & (~15);
                    for (int x = 0; x < vt6; x++, xi++) {
                        savedPos = (int)si.Position;
                        int i0 = br.ReadInt32();
                        int i1 = br.ReadInt32();
                        int i2 = br.ReadInt32();
                        int i3 = br.ReadInt32();
                        int i4 = br.ReadInt32();
                        int i5 = br.ReadInt32();
                        int i6 = br.ReadInt32();
                        vertexMixersGroups[xi] = (new VertexMixer[] { mixers[i0], mixers[i1], mixers[i2], mixers[i3], mixers[i4], mixers[i5], mixers[i6] });

                        hd.AddComment(savedPos, 28, $"weightGroup7[{x}]: [{FormatMixers(vertexMixersGroups[xi])}]");
                        md.Mark(savedPos, 28, $"weightGroup", weightGroupCountColor);
                    }
                }
                if (weightGroupNum >= 8) {
                    Debug.Fail("v28: " + weightGroupNum);
                }

                export.vertexMixersGroups = vertexMixersGroups;
            }

            export.hexDump = hd;
            export.markDump = md;
            return export;
        }
    }

    public class VU1Export {
        public Vector4[] rawVerts = null; // small
        public Vector2[] uvs = null; // large
        public int[] vertexIndices = null; // large2small
        public int[] flags = null; // large
        public VertexMixer[][] vertexMixersGroups = null; // small
        public int textureIndex = -1;
        public bool avail = false;
        public AnnotHexDump hexDump;
        public AnnotMarkDump markDump;
    }

    public class VertexMixer {
        public int matrixIndex;
        public int microMatrixIndex;
        public int vertexIndex;
        public float factor;

        public VertexMixer(int matrixIndex, int microMatrixIndex, int vertexIndex, float factor) {
            this.matrixIndex = matrixIndex;
            this.microMatrixIndex = microMatrixIndex;
            this.vertexIndex = vertexIndex;
            this.factor = factor;
        }
    }

    public class ProtInvalidTypeException : ApplicationException {
        public ProtInvalidTypeException() : base("Has to be typ1 or typ2") { }
    }
}
