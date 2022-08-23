using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Models.D3D {
    public class CustomMesh {
        public int textureIndex = -1;
        public int countTriangles = 0;
        public int constantColor = -1;
        public List<CustomVertex.PositionColoredTextured> vertices = new List<CustomVertex.PositionColoredTextured>();
        public List<int> indices = new List<int>();

        public void AddQuad(
            CustomVertex.PositionColoredTextured vTL,
            CustomVertex.PositionColoredTextured vTR,
            CustomVertex.PositionColoredTextured vBL,
            CustomVertex.PositionColoredTextured vBR
        ) {
            int lastVertexIndex = vertices.Count;

            vertices.Add(vTL);
            vertices.Add(vTR);
            vertices.Add(vBL);
            vertices.Add(vBR);

            indices.Add(lastVertexIndex + 0); indices.Add(lastVertexIndex + 1); indices.Add(lastVertexIndex + 2);
            indices.Add(lastVertexIndex + 3); indices.Add(lastVertexIndex + 2); indices.Add(lastVertexIndex + 1);

            countTriangles += 2;
        }
    }
}
