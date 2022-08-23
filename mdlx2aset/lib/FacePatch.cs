namespace khkh_xldMii.Models {
    public class FacePatch {
        public byte[] bits;
        public int patchX;
        public int patchY;
        public int patchWidth;
        public int patchHeight;
        public int numVerticalFaces;
        public int textureIndex;
        public FaceTexture[] faceTextureList = Array.Empty<FaceTexture>();

        public FacePatch(byte[] bits, int patchX, int patchY, int patchWidth, int patchHeight, int numVerticalFaces, int textureIndex) {
            this.bits = bits;
            this.patchX = patchX;
            this.patchY = patchY;
            this.patchWidth = patchWidth;
            this.patchHeight = patchHeight;
            this.numVerticalFaces = numVerticalFaces;
            this.textureIndex = textureIndex;
        }
    }
}
