using parseSEQDv2.Models.D3D;
using SlimDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parseSEQDv2.Utils {
    public class RenderSeqd {
        private readonly List<CustomMesh> meshList = new List<CustomMesh>();

        public CustomMesh[] MeshList => meshList.ToArray();

        private const int SincInterpolationFlag = 0x00000001;
        private const int ScalingFlag = 0x00000040;
        private const int ColorMaskingFlag = 0x00000400;
        private const int ColorInterpolationFlag = 0x00000080;
        private const int TraslateFlag = 0x00004000;

        public void InterpretAndAddToMesh(
            int time,
            Seqd oneSeqd,
            int pictureIndex,
            int animGroupIndex,
            int posX,
            int posY,
            float fovX,
            float fovY,
            Func<PicIMGD, int> convertPicToTextureIndex,
            BitArray visibleMask,
            BitArray renderedMask
        ) {
            foreach (var animGroup in oneSeqd.AnimGroupList) {
                var picture = oneSeqd.Pictures[pictureIndex];
                var bitmap = picture.Bitmap;
                var texWidth = (float)bitmap.Width;
                var texHeight = (float)bitmap.Height;

                for (int y = 0; y < animGroup.Count; y++) {
                    var anim = oneSeqd.AnimList[animGroup.AnimationIndex + y];
                    var frameGroup = oneSeqd.FrameGroupList[anim.FrameGroupIndex];

                    if (time >= anim.FrameStart && anim.FrameEnd >= time) {
                        var frameBasedTime = time - anim.FrameStart;
                        var fraction = (float)frameBasedTime / anim.FrameTimeLength; // 0 to 1

                        var animMatrix = SlimDX.Matrix.Identity;

                        animMatrix *= SlimDX.Matrix.Translation(
                            x: CalcUtil.FloatLerp(anim.Xb0, anim.Xb1, fraction) + posX,
                            y: CalcUtil.FloatLerp(anim.Yb0, anim.Yb1, fraction) + posY,
                            z: 0
                        );

                        if (0 == (anim.Flags & ScalingFlag)) {
                            var scale = CalcUtil.FloatLerp(anim.ScaleStart, anim.ScaleEnd, fraction);

                            animMatrix *= SlimDX.Matrix.Scaling(
                                x: scale * CalcUtil.FloatLerp(anim.ScaleXStart, anim.ScaleXEnd, fraction),
                                y: scale * CalcUtil.FloatLerp(anim.ScaleYStart, anim.ScaleYEnd, fraction),
                                z: 0
                                );
                        }

                        animMatrix *= SlimDX.Matrix.RotationZ(
                            angle: CalcUtil.FloatLerp(anim.StartAngle, anim.EndAngle, fraction)
                            );

                        animMatrix *= SlimDX.Matrix.Translation(
                            x: CalcUtil.FloatLerp(anim.Xa0, anim.Xa1, fraction) + CalcUtil.Lerp4(0, anim.BounceXStart, anim.BounceXEnd, 0, fraction),
                            y: CalcUtil.FloatLerp(anim.Ya0, anim.Ya1, fraction) + CalcUtil.Lerp4(0, anim.BounceYStart, anim.BounceYEnd, 0, fraction),
                            z: 0
                            );

                        if (0 == (anim.Flags & TraslateFlag)) {
                            animMatrix *= SlimDX.Matrix.Translation(
                                x: CalcUtil.FloatLerp(anim.Xb0, anim.Xb1, fraction),
                                y: CalcUtil.FloatLerp(anim.Yb0, anim.Yb1, fraction),
                                z: 0
                                );
                        }

                        uint meshColor;
                        if (0 == (anim.Flags & ColorMaskingFlag)) {
                            if (0 == (anim.Flags & ColorInterpolationFlag)) {
                                meshColor = CalcUtil.ColorLerp(anim.ColorStart, anim.ColorEnd, fraction);
                            }
                            else {
                                meshColor = anim.ColorStart;
                            }
                        }
                        else {
                            meshColor = anim.ColorStart;
                        }

                        var mesh = new CustomMesh();
                        mesh.textureIndex = convertPicToTextureIndex(picture);
                        mesh.constantColor = KH2c4cvt.ToWin(meshColor);

                        meshList.Add(mesh);

                        for (int x = 0; x < frameGroup.Count; x++) {
                            var frameExIndex = frameGroup.Start + x;
                            var frameEx = oneSeqd.FrameExList[frameExIndex];
                            var frameIndex = frameEx.FrameIndex;
                            if (renderedMask != null) {
                                renderedMask[frameIndex] = true;
                            }
                            if (visibleMask != null && visibleMask[frameIndex] == false) {
                                continue;
                            }
                            var frame = oneSeqd.FrameList[frameEx.FrameIndex];

                            Vector3[] quadCoords = new Vector3[] {
                                new Vector3(frameEx.Left, frameEx.Top, 0),
                                new Vector3(frameEx.Right, frameEx.Top, 0),
                                new Vector3(frameEx.Left, frameEx.Bottom, 0),
                                new Vector3(frameEx.Right, frameEx.Bottom, 0),
                            };

                            Vector3.TransformCoordinate(quadCoords, ref animMatrix, quadCoords);

                            var vTL = new CustomVertex.PositionColoredTextured(quadCoords[0], KH2c4cvt.ToWin(frame.Color0), (float)(frame.Left / texWidth), (float)(frame.Top / texHeight));
                            var vTR = new CustomVertex.PositionColoredTextured(quadCoords[1], KH2c4cvt.ToWin(frame.Color1), (float)(frame.Right / texWidth), (float)(frame.Top / texHeight));
                            var vBL = new CustomVertex.PositionColoredTextured(quadCoords[2], KH2c4cvt.ToWin(frame.Color2), (float)(frame.Left / texWidth), (float)(frame.Bottom / texHeight));
                            var vBR = new CustomVertex.PositionColoredTextured(quadCoords[3], KH2c4cvt.ToWin(frame.Color3), (float)(frame.Right / texWidth), (float)(frame.Bottom / texHeight));

                            mesh.AddQuad(vTL, vTR, vBL, vBR);
                        }
                    }
                }
            }
        }

        class CalcUtil {
            public static int ColorLerp(int c0, int c1, float f) {
                byte x0 = (byte)(c0 >> 24);
                byte y0 = (byte)(c0 >> 16);
                byte z0 = (byte)(c0 >> 8);
                byte w0 = (byte)(c0 >> 0);

                byte x1 = (byte)(c1 >> 24);
                byte y1 = (byte)(c1 >> 16);
                byte z1 = (byte)(c1 >> 8);
                byte w1 = (byte)(c1 >> 0);
                return 0
                    | (((byte)(x0 + (x1 - x0) * f)) << 24)
                    | (((byte)(y0 + (y1 - y0) * f)) << 16)
                    | (((byte)(z0 + (z1 - z0) * f)) << 8)
                    | (((byte)(w0 + (w1 - w0) * f)) << 0)
                    ;
            }

            public static uint ColorLerp(uint c0, uint c1, float f) {
                byte x0 = (byte)(c0 >> 24);
                byte y0 = (byte)(c0 >> 16);
                byte z0 = (byte)(c0 >> 8);
                byte w0 = (byte)(c0 >> 0);

                byte x1 = (byte)(c1 >> 24);
                byte y1 = (byte)(c1 >> 16);
                byte z1 = (byte)(c1 >> 8);
                byte w1 = (byte)(c1 >> 0);
                return (uint)(0
                    | (((byte)(x0 + (x1 - x0) * f)) << 24)
                    | (((byte)(y0 + (y1 - y0) * f)) << 16)
                    | (((byte)(z0 + (z1 - z0) * f)) << 8)
                    | (((byte)(w0 + (w1 - w0) * f)) << 0)
                    );
            }

            public static float FloatLerp(float c0, float c1, float f) {
                return c0 + (c1 - c0) * f;
            }

            public static float Lerp4(float v0, float v1, float v2, float v3, float fact) {
                // 0 1 2 3
                if (fact <= 0.33f) {
                    return v0 + (v1 - v0) * fact * 3;
                }
                else if (fact <= 0.66f) {
                    return v1 + (v2 - v1) * (fact - 0.33f) * 3;
                }
                else {
                    return v2 + (v3 - v2) * (fact - 0.66f) * 3;
                }
            }
        }
    }
}
