namespace khkh_xldMii.Models {
    public class FaceTexture {
        public int i0, i1, i2;
        public short v0, v2, v4, v6;

        public override string ToString() {
            return string.Format("{0,3},{1,2},{2,2}|{3,4},{4,3},{5,3},{6,3}"
                , i0, i1, i2
                , v0, v2, v4, v6
                );
        }
    }
}
