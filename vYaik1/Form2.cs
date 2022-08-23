using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using System.Diagnostics;

namespace vYaik1 {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
        }

        private void redraw() {
            panel1.Invalidate();
            panel3.Invalidate();
            panel2.Invalidate();
            panel4.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e) {
            alFkOrg.Add(new Bone(new Vector3(0f, 92.66396f, 1.177912f), new Quaternion(-0.5f, -0.4999999f, -0.4999999f, 0.4999999f), -1));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(-0.4782524f, 0.5208432f, 0.5208055f, 0.4782869f), 0));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 1));
            alFkOrg.Add(new Bone(new Vector3(13.85246f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 2));
            alFkOrg.Add(new Bone(new Vector3(-13.88673f, -0.4016438f, 3.067011E-05f), new Quaternion(-3.995853E-05f, -1.199147E-05f, -0.4273422f, 0.9040899f), 3));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 4));
            alFkOrg.Add(new Bone(new Vector3(13.74086f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 5));
            alFkOrg.Add(new Bone(new Vector3(0f, 7.629395E-06f, 1.190347E-06f), new Quaternion(0.3849073f, -0.3878726f, 0.2339126f, 0.8041679f), 6));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 7));
            alFkOrg.Add(new Bone(new Vector3(11.61883f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 8));
            alFkOrg.Add(new Bone(new Vector3(-9.289284f, -0.9183974f, 8.464805f), new Quaternion(0.001781533f, -0.193584f, -0.03976314f, 0.9802759f), 3));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 10));
            alFkOrg.Add(new Bone(new Vector3(6.852354f, 0f, 0f), new Quaternion(-0.01024991f, -1.430436E-06f, -1.466261E-08f, 0.9999475f), 11));
            alFkOrg.Add(new Bone(new Vector3(6.852411f, 0f, 0f), new Quaternion(0f, -7.152557E-07f, 0f, 1f), 12));
            alFkOrg.Add(new Bone(new Vector3(8.512952f, 0f, 0f), new Quaternion(0f, 7.417648E-07f, 0f, 1f), 13));
            alFkOrg.Add(new Bone(new Vector3(11.55658f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 14));
            alFkOrg.Add(new Bone(new Vector3(-1.525879E-05f, 0f, -6.875489f), new Quaternion(-5.970616E-05f, 4.593226E-06f, -0.07220707f, 0.9973897f), 3));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 16));
            alFkOrg.Add(new Bone(new Vector3(29.63077f, 0f, 0f), new Quaternion(8.946379E-05f, -2.803753E-06f, 0.08027726f, 0.9967726f), 17));
            alFkOrg.Add(new Bone(new Vector3(32.01383f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 18));
            alFkOrg.Add(new Bone(new Vector3(-1.907349E-06f, 1.692772E-05f, 1.335144E-05f), new Quaternion(-3.319005E-05f, -9.513379E-07f, -0.05064012f, 0.998717f), 19));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 20));
            alFkOrg.Add(new Bone(new Vector3(17.43355f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 21));
            alFkOrg.Add(new Bone(new Vector3(-8.756762f, -18.57504f, -0.0001187325f), new Quaternion(-3.580951E-12f, -2.384033E-07f, 2.392078E-07f, 1f), 22));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 23));
            alFkOrg.Add(new Bone(new Vector3(8.756783f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 24));
            alFkOrg.Add(new Bone(new Vector3(0.3729935f, -6.271236f, -0.0007529259f), new Quaternion(4.484034E-05f, 9.012701E-06f, -0.1194476f, 0.9928405f), 17));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 26));
            alFkOrg.Add(new Bone(new Vector3(15.08708f, 0f, 0f), new Quaternion(1.120834E-05f, -2.893499E-06f, 0.1488802f, 0.9888552f), 27));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 28));
            alFkOrg.Add(new Bone(new Vector3(3.051758E-05f, 0.0007600784f, -6.282331f), new Quaternion(-0.005588867f, 0.1858039f, 0.02921549f, 0.9821365f), 17));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 30));
            alFkOrg.Add(new Bone(new Vector3(15.4887f, 0f, 0f), new Quaternion(0.0001237077f, -0.1858917f, -2.493376E-05f, 0.9825702f), 31));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 32));
            alFkOrg.Add(new Bone(new Vector3(-0.3729477f, 6.271252f, 0.0007472038f), new Quaternion(-0.0002707064f, -8.555708E-05f, 0.2149648f, 0.9766217f), 17));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 34));
            alFkOrg.Add(new Bone(new Vector3(15.48838f, 0f, 0f), new Quaternion(0.0003277189f, 6.321773E-05f, -0.1858669f, 0.9825749f), 35));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 36));
            alFkOrg.Add(new Bone(new Vector3(1.499794f, 0.08840179f, 6.68505f), new Quaternion(0.002915387f, -0.1305441f, 0.02930105f, 0.9910051f), 17));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 38));
            alFkOrg.Add(new Bone(new Vector3(13.37176f, 0f, 0f), new Quaternion(0.00101873f, 0.1305693f, 0.0001351527f, 0.9914387f), 39));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 40));
            alFkOrg.Add(new Bone(new Vector3(0f, -2.384186E-06f, 6.875492f), new Quaternion(-6.00034E-05f, 4.614746E-06f, -0.072207f, 0.9973897f), 3));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 42));
            alFkOrg.Add(new Bone(new Vector3(29.63077f, 0f, 0f), new Quaternion(8.982026E-05f, -2.822886E-06f, 0.08027743f, 0.9967726f), 43));
            alFkOrg.Add(new Bone(new Vector3(32.01382f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 44));
            alFkOrg.Add(new Bone(new Vector3(5.722046E-06f, 1.096725E-05f, 1.66893E-05f), new Quaternion(-3.324958E-05f, -9.543794E-07f, -0.05064036f, 0.998717f), 45));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 46));
            alFkOrg.Add(new Bone(new Vector3(17.43356f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 47));
            alFkOrg.Add(new Bone(new Vector3(-8.756756f, -18.57503f, -0.0001225471f), new Quaternion(2.384223E-07f, -2.383969E-07f, 3.286172E-07f, 1f), 48));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 49));
            alFkOrg.Add(new Bone(new Vector3(8.756783f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 50));
            alFkOrg.Add(new Bone(new Vector3(0.3729782f, -6.271236f, -0.0007572174f), new Quaternion(4.525459E-05f, 9.034063E-06f, -0.1194476f, 0.9928405f), 43));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 52));
            alFkOrg.Add(new Bone(new Vector3(15.08708f, 0f, 0f), new Quaternion(1.120834E-05f, -2.893499E-06f, 0.1488802f, 0.9888552f), 53));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 54));
            alFkOrg.Add(new Bone(new Vector3(-7.629395E-06f, -0.0007486343f, 6.282328f), new Quaternion(0.006982212f, -0.1857699f, 0.02943404f, 0.9821275f), 43));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 56));
            alFkOrg.Add(new Bone(new Vector3(15.4887f, 0f, 0f), new Quaternion(-0.001406331f, 0.1858914f, -0.0002649777f, 0.9825692f), 57));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 58));
            alFkOrg.Add(new Bone(new Vector3(-0.3729782f, 6.271252f, 0.0007567406f), new Quaternion(-0.0003893012f, -0.000111903f, 0.2149651f, 0.9766217f), 43));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 60));
            alFkOrg.Add(new Bone(new Vector3(15.48839f, 0f, 0f), new Quaternion(0.0004480287f, 8.597678E-05f, -0.1858669f, 0.9825748f), 61));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 62));
            alFkOrg.Add(new Bone(new Vector3(1.499786f, 0.09000921f, -6.685031f), new Quaternion(-0.004900863f, 0.1304775f, 0.02959382f, 0.9909973f), 43));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 64));
            alFkOrg.Add(new Bone(new Vector3(13.37176f, 0f, 0f), new Quaternion(0.001080198f, -0.1305696f, -0.0001432105f, 0.9914385f), 65));
            alFkOrg.Add(new Bone(new Vector3(14.41826f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 66));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0.4809198f, 0.5183753f, 0.5184183f, -0.4808798f), 0));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 68));
            alFkOrg.Add(new Bone(new Vector3(5.116652f, 0f, 0f), new Quaternion(2.967646E-05f, 3.98505E-06f, -0.07487602f, 0.9971929f), 69));
            alFkOrg.Add(new Bone(new Vector3(5.301811f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 70));
            alFkOrg.Add(new Bone(new Vector3(-0.6833267f, -6.067156f, -0.0001706207f), new Quaternion(1.977153E-06f, 1.064579E-05f, -0.9432852f, 0.3319834f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 72));
            alFkOrg.Add(new Bone(new Vector3(8.127232f, 0f, 0f), new Quaternion(1.26641E-07f, 1.614069E-06f, 0.08683233f, 0.9962229f), 73));
            alFkOrg.Add(new Bone(new Vector3(9.484933f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 74));
            alFkOrg.Add(new Bone(new Vector3(-7.629395E-06f, 7.629395E-06f, -9.228243E-07f), new Quaternion(2.994828E-06f, 5.374698E-07f, -0.02926969f, 0.9995716f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 76));
            alFkOrg.Add(new Bone(new Vector3(11.71404f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 77));
            alFkOrg.Add(new Bone(new Vector3(1.591721f, -4.099829f, -7.338708E-05f), new Quaternion(-6.566444E-06f, -5.451519E-06f, 0.5999418f, -0.8000434f), 78));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 79));
            alFkOrg.Add(new Bone(new Vector3(3.527437f, 0f, 0f), new Quaternion(0f, 0f, -0.1316671f, 0.991294f), 80));
            alFkOrg.Add(new Bone(new Vector3(4.551478f, 0f, 0f), new Quaternion(0f, 0f, 3.515372E-05f, 1f), 81));
            alFkOrg.Add(new Bone(new Vector3(3.903543f, 0f, 0f), new Quaternion(-8.640198E-12f, -4.768371E-07f, -1.811981E-05f, 1f), 82));
            alFkOrg.Add(new Bone(new Vector3(6.801145f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 83));
            alFkOrg.Add(new Bone(new Vector3(-2.667183f, 6.087502f, 0.0001039159f), new Quaternion(3.651471E-07f, -1.580505E-05f, 0.9548967f, 0.2969379f), 78));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 85));
            alFkOrg.Add(new Bone(new Vector3(6.162843f, 0f, 0f), new Quaternion(-2.461229E-05f, -1.601717E-05f, 0.3793386f, 0.9252579f), 86));
            alFkOrg.Add(new Bone(new Vector3(6.008225f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 87));
            alFkOrg.Add(new Bone(new Vector3(-0.2522202f, 0.3843842f, -6.165942f), new Quaternion(0.5761355f, 0.08300284f, -0.8048972f, 0.1154064f), 78));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 89));
            alFkOrg.Add(new Bone(new Vector3(6.398477f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 90));
            alFkOrg.Add(new Bone(new Vector3(-5.722046E-06f, 2.384186E-06f, 0f), new Quaternion(0.02561872f, 0.163509f, 0.004247454f, 0.9861999f), 91));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 92));
            alFkOrg.Add(new Bone(new Vector3(18.76823f, 0f, 0f), new Quaternion(-0.02366923f, -0.0004085484f, -0.01726968f, 0.9995705f), 93));
            alFkOrg.Add(new Bone(new Vector3(16.44339f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 94));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, -7.629395E-06f), new Quaternion(-4.191671E-08f, -1.951014E-07f, -0.009207789f, -0.9999574f), 95));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 96));
            alFkOrg.Add(new Bone(new Vector3(1.419212f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 97));
            alFkOrg.Add(new Bone(new Vector3(0f, 4.768372E-07f, 0f), new Quaternion(0.002136324f, 6.780506E-05f, 0.03181403f, -0.9994914f), 98));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 99));
            alFkOrg.Add(new Bone(new Vector3(11.0002f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 100));
            alFkOrg.Add(new Bone(new Vector3(-0.0766331f, -0.5443779f, 4.018825f), new Quaternion(-0.7300396f, 0.6834047f, -0.0003094011f, 0.0001920135f), 101));
            alFkOrg.Add(new Bone(new Vector3(-5.334179f, -2.13812f, 0.9380569f), new Quaternion(0.515677f, -0.4819494f, -0.4857229f, 0.5156308f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 103));
            alFkOrg.Add(new Bone(new Vector3(2.74634f, 0f, 0f), new Quaternion(8.948219E-05f, 0f, 0f, 1f), 104));
            alFkOrg.Add(new Bone(new Vector3(4.362839f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 105));
            alFkOrg.Add(new Bone(new Vector3(3.546586f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 106));
            alFkOrg.Add(new Bone(new Vector3(0.168499f, -2.597148f, -0.00756073f), new Quaternion(-0.001446545f, -4.596876E-05f, -0.03181437f, -0.9994925f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 108));
            alFkOrg.Add(new Bone(new Vector3(3.358498f, 0f, 0f), new Quaternion(0.0006897585f, 0f, 0f, 0.9999998f), 109));
            alFkOrg.Add(new Bone(new Vector3(2.177891f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 110));
            alFkOrg.Add(new Bone(new Vector3(3.036217f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 111));
            alFkOrg.Add(new Bone(new Vector3(3.814697E-06f, 1.430511E-06f, 7.629395E-06f), new Quaternion(-4.362419E-06f, -2.143927E-07f, 0.03181405f, 0.9994936f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 113));
            alFkOrg.Add(new Bone(new Vector3(3.817722f, 0f, 0f), new Quaternion(0.002141446f, 0f, 0f, 0.9999977f), 114));
            alFkOrg.Add(new Bone(new Vector3(2.55769f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 115));
            alFkOrg.Add(new Bone(new Vector3(3.356918f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 116));
            alFkOrg.Add(new Bone(new Vector3(-0.3879852f, 2.538865f, 0.01454163f), new Quaternion(0.002838449f, 9.016618E-05f, 0.03181427f, 0.9994897f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 118));
            alFkOrg.Add(new Bone(new Vector3(3.404087f, 0f, 0f), new Quaternion(-0.0007030963f, 0f, 0f, 0.9999998f), 119));
            alFkOrg.Add(new Bone(new Vector3(2.506535f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 120));
            alFkOrg.Add(new Bone(new Vector3(2.679512f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 121));
            alFkOrg.Add(new Bone(new Vector3(-0.7995872f, 4.72857f, 0.0240097f), new Quaternion(-0.00213582f, -6.790875E-05f, -0.03181433f, -0.9994913f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 123));
            alFkOrg.Add(new Bone(new Vector3(2.351471f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 124));
            alFkOrg.Add(new Bone(new Vector3(2.14962f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 125));
            alFkOrg.Add(new Bone(new Vector3(2.362823f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 126));
            alFkOrg.Add(new Bone(new Vector3(-0.05202103f, -3.220691f, 0.1525955f), new Quaternion(0.02355411f, 0.002365916f, 0.09995783f, -0.9947098f), 93));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 128));
            alFkOrg.Add(new Bone(new Vector3(8.814704f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 129));
            alFkOrg.Add(new Bone(new Vector3(6.388369f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 130));
            alFkOrg.Add(new Bone(new Vector3(1.049042E-05f, 0.2495158f, 5.266785f), new Quaternion(-0.02545871f, -0.09211013f, -0.005960486f, 0.9954054f), 93));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 132));
            alFkOrg.Add(new Bone(new Vector3(8.814701f, 0f, 0f), new Quaternion(0.001151018f, 2.705258E-07f, -3.113802E-10f, 0.9999993f), 133));
            alFkOrg.Add(new Bone(new Vector3(6.388371f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 134));
            alFkOrg.Add(new Bone(new Vector3(0.05203152f, 3.22066f, -0.1525726f), new Quaternion(0.02422157f, -0.001933045f, -0.08389734f, -0.996178f), 93));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 136));
            alFkOrg.Add(new Bone(new Vector3(8.814703f, 0f, 0f), new Quaternion(0.0006343648f, -4.766681E-07f, 2.666185E-07f, 0.9999998f), 137));
            alFkOrg.Add(new Bone(new Vector3(6.388368f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 138));
            alFkOrg.Add(new Bone(new Vector3(4.768372E-06f, -0.125603f, -2.650658f), new Quaternion(-0.02257222f, 0.09173432f, -0.01023042f, 0.9954751f), 93));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 140));
            alFkOrg.Add(new Bone(new Vector3(8.814703f, 0f, 0f), new Quaternion(-0.0002589225f, -4.768371E-07f, -1.234639E-10f, 0.9999999f), 141));
            alFkOrg.Add(new Bone(new Vector3(6.388371f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 142));
            alFkOrg.Add(new Bone(new Vector3(7.629395E-06f, 0f, -2.527173E-06f), new Quaternion(3.037553E-05f, 2.245662E-06f, 0.1683968f, 0.9857193f), 78));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 144));
            alFkOrg.Add(new Bone(new Vector3(3.805229f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 145));
            alFkOrg.Add(new Bone(new Vector3(2.330644f, 0f, 0f), new Quaternion(-1.731815E-05f, 3.563465E-06f, 0.1331785f, 0.9910921f), 146));
            alFkOrg.Add(new Bone(new Vector3(2.535882f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 147));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, -1.609442E-07f), new Quaternion(-3.78298E-06f, -8.998766E-07f, -0.1603575f, 0.987059f), 148));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 149));
            alFkOrg.Add(new Bone(new Vector3(13.87189f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 150));
            alFkOrg.Add(new Bone(new Vector3(-1.252213f, -6.919635f, 1.580783E-05f), new Quaternion(-6.458562E-07f, -9.53882E-07f, -0.8114049f, 0.5844843f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 152));
            alFkOrg.Add(new Bone(new Vector3(13.00701f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 153));
            alFkOrg.Add(new Bone(new Vector3(6.729797f, 8.077484f, -2.433009f), new Quaternion(-0.1078611f, 0.1421139f, 0.4811281f, 0.8583037f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 155));
            alFkOrg.Add(new Bone(new Vector3(15.33441f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 156));
            alFkOrg.Add(new Bone(new Vector3(-7.464798f, -0.9106696f, -10.12188f), new Quaternion(0.3149067f, 0.08417461f, -0.9065411f, 0.2682007f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 158));
            alFkOrg.Add(new Bone(new Vector3(5.979013f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 159));
            alFkOrg.Add(new Bone(new Vector3(-2.965836f, -4.221889f, -8.469937f), new Quaternion(0.6497923f, 0.0001280374f, -0.7601117f, 0.0001104214f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 161));
            alFkOrg.Add(new Bone(new Vector3(14.71621f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 162));
            alFkOrg.Add(new Bone(new Vector3(-4.185989f, 3.978894f, -9.863686f), new Quaternion(-1.634193E-07f, -0.0006287097f, 0.9999998f, 7.906511E-07f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 164));
            alFkOrg.Add(new Bone(new Vector3(12.81947f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 165));
            alFkOrg.Add(new Bone(new Vector3(-0.8690338f, 10.99429f, -7.399717f), new Quaternion(0.2584011f, -0.2964916f, -0.9132531f, 0.1062557f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 167));
            alFkOrg.Add(new Bone(new Vector3(9.412037f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 168));
            alFkOrg.Add(new Bone(new Vector3(3.256775f, 9.249571f, -5.863527f), new Quaternion(-0.2856823f, 0.2429762f, 0.8452739f, 0.3806052f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 170));
            alFkOrg.Add(new Bone(new Vector3(9.670507f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 171));
            alFkOrg.Add(new Bone(new Vector3(-7.464798f, -0.9106405f, 10.12187f), new Quaternion(-0.3149066f, -0.08417649f, -0.9065409f, 0.2682005f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 173));
            alFkOrg.Add(new Bone(new Vector3(5.979011f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 174));
            alFkOrg.Add(new Bone(new Vector3(-2.96582f, -4.221862f, 8.46995f), new Quaternion(0.6497921f, -0.000410126f, 0.7601117f, 0.0003519581f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 176));
            alFkOrg.Add(new Bone(new Vector3(14.71621f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 177));
            alFkOrg.Add(new Bone(new Vector3(-4.185989f, 3.978924f, 9.863668f), new Quaternion(-6.396526E-07f, 0.0002444982f, 0.9999999f, 4.332822E-07f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 179));
            alFkOrg.Add(new Bone(new Vector3(12.81947f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 180));
            alFkOrg.Add(new Bone(new Vector3(0.7685852f, 8.776752f, 8.025702f), new Quaternion(-0.2839518f, 0.5098104f, -0.8119804f, -0.01233996f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 182));
            alFkOrg.Add(new Bone(new Vector3(9.936018f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 183));
            alFkOrg.Add(new Bone(new Vector3(4.385422f, 10.24054f, 2.260866f), new Quaternion(0.1694501f, -0.1246299f, 0.8166448f, 0.537443f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 185));
            alFkOrg.Add(new Bone(new Vector3(11.3924f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 186));
            alFkOrg.Add(new Bone(new Vector3(1.723343f, 12.13106f, -2.098661E-05f), new Quaternion(-1.176599E-06f, -1.498384E-06f, 0.9727926f, 0.2316773f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 188));
            alFkOrg.Add(new Bone(new Vector3(11.3006f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 189));
            alFkOrg.Add(new Bone(new Vector3(7.233582f, 6.762377f, -9.972629E-06f), new Quaternion(0.0004765118f, 0.0002294005f, 0.4324389f, 0.9016631f), 151));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 191));
            alFkOrg.Add(new Bone(new Vector3(11.10642f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 192));
            alFkOrg.Add(new Bone(new Vector3(-0.2522202f, 0.3841763f, 6.165952f), new Quaternion(-0.5762398f, -0.08224688f, -0.8049749f, 0.1148848f), 78));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 194));
            alFkOrg.Add(new Bone(new Vector3(6.398475f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 195));
            alFkOrg.Add(new Bone(new Vector3(-1.907349E-06f, 3.33786E-06f, 7.629395E-06f), new Quaternion(-0.02471172f, -0.1635131f, 0.004096745f, 0.9862231f), 196));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 197));
            alFkOrg.Add(new Bone(new Vector3(18.76822f, 0f, 0f), new Quaternion(0.0236651f, 0.0004091381f, -0.01726985f, 0.9995707f), 198));
            alFkOrg.Add(new Bone(new Vector3(16.4434f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 199));
            alFkOrg.Add(new Bone(new Vector3(0f, -2.384186E-06f, 0f), new Quaternion(3.014263E-09f, -6.598887E-08f, 0.009207808f, 0.9999576f), 200));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 201));
            alFkOrg.Add(new Bone(new Vector3(1.419212f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 202));
            alFkOrg.Add(new Bone(new Vector3(3.814697E-06f, 4.768372E-07f, 0f), new Quaternion(0.002139625f, 6.828096E-05f, -0.0318139f, 0.9994916f), 203));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 204));
            alFkOrg.Add(new Bone(new Vector3(11.0002f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 205));
            alFkOrg.Add(new Bone(new Vector3(-5.334175f, -2.138113f, -0.9380569f), new Quaternion(-0.5162945f, 0.4826037f, -0.4850729f, 0.5150124f), 206));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 207));
            alFkOrg.Add(new Bone(new Vector3(2.746341f, 0f, 0f), new Quaternion(-0.0001327991f, 0f, 0f, 1f), 208));
            alFkOrg.Add(new Bone(new Vector3(4.36284f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 209));
            alFkOrg.Add(new Bone(new Vector3(3.546585f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 210));
            alFkOrg.Add(new Bone(new Vector3(0.1684952f, -2.597148f, 0.007553101f), new Quaternion(-0.001449519f, -4.630741E-05f, 0.03181393f, 0.9994927f), 206));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 212));
            alFkOrg.Add(new Bone(new Vector3(3.358498f, 0f, 0f), new Quaternion(-0.0006906985f, 0f, 0f, 0.9999998f), 213));
            alFkOrg.Add(new Bone(new Vector3(2.177891f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 214));
            alFkOrg.Add(new Bone(new Vector3(3.036217f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 215));
            alFkOrg.Add(new Bone(new Vector3(3.814697E-06f, 4.768372E-07f, 0f), new Quaternion(4.417511E-08f, -8.514873E-08f, 0.03181373f, 0.9994938f), 206));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 217));
            alFkOrg.Add(new Bone(new Vector3(3.817722f, 0f, 0f), new Quaternion(-0.002140759f, 0f, 0f, 0.9999977f), 218));
            alFkOrg.Add(new Bone(new Vector3(2.55769f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 219));
            alFkOrg.Add(new Bone(new Vector3(3.356918f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 220));
            alFkOrg.Add(new Bone(new Vector3(-0.3879814f, 2.538865f, -0.01451874f), new Quaternion(-0.002841474f, -9.059512E-05f, 0.03181384f, 0.9994897f), 206));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 222));
            alFkOrg.Add(new Bone(new Vector3(3.404087f, 0f, 0f), new Quaternion(0.0007022754f, 0f, 0f, 0.9999998f), 223));
            alFkOrg.Add(new Bone(new Vector3(2.506531f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 224));
            alFkOrg.Add(new Bone(new Vector3(2.679512f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 225));
            alFkOrg.Add(new Bone(new Vector3(-0.7995872f, 4.728569f, -0.02397156f), new Quaternion(-0.002139391f, -6.824826E-05f, 0.03181366f, 0.9994916f), 206));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 227));
            alFkOrg.Add(new Bone(new Vector3(2.351471f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 228));
            alFkOrg.Add(new Bone(new Vector3(2.14962f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 229));
            alFkOrg.Add(new Bone(new Vector3(2.36282f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 230));
            alFkOrg.Add(new Bone(new Vector3(-0.05202579f, -3.220682f, -0.1526184f), new Quaternion(0.02354992f, 0.002367172f, -0.09995788f, 0.9947101f), 198));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 232));
            alFkOrg.Add(new Bone(new Vector3(8.814702f, 0f, 0f), new Quaternion(0f, 0f, -4.768371E-07f, 1f), 233));
            alFkOrg.Add(new Bone(new Vector3(6.388366f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 234));
            alFkOrg.Add(new Bone(new Vector3(2.861023E-06f, 0.2495489f, -5.266777f), new Quaternion(0.02252688f, 0.09212768f, -0.005689211f, 0.9954761f), 198));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 236));
            alFkOrg.Add(new Bone(new Vector3(8.814702f, 0f, 0f), new Quaternion(0.001790442f, -4.768364E-07f, 8.537493E-10f, 0.9999984f), 237));
            alFkOrg.Add(new Bone(new Vector3(6.38837f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 238));
            alFkOrg.Add(new Bone(new Vector3(0.05203247f, 3.220669f, 0.1525879f), new Quaternion(0.02362784f, -0.001983178f, 0.08389584f, 0.9961924f), 198));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 240));
            alFkOrg.Add(new Bone(new Vector3(8.814701f, 0f, 0f), new Quaternion(-4.36306E-05f, -1.161953E-11f, 2.663161E-07f, 1f), 241));
            alFkOrg.Add(new Bone(new Vector3(6.388366f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 242));
            alFkOrg.Add(new Bone(new Vector3(3.814697E-06f, -0.1256065f, 2.650665f), new Quaternion(0.02382551f, -0.09174693f, -0.01011592f, 0.9954459f), 198));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 244));
            alFkOrg.Add(new Bone(new Vector3(8.814701f, 0f, 0f), new Quaternion(-0.001004219f, 2.792525E-07f, 2.804308E-10f, 0.9999995f), 245));
            alFkOrg.Add(new Bone(new Vector3(6.388369f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 246));
            alFkOrg.Add(new Bone(new Vector3(-2.916733f, 6.460083f, 3.814929f), new Quaternion(-0.08282974f, -0.02373687f, -0.996273f, 0.003964263f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 248));
            alFkOrg.Add(new Bone(new Vector3(7.18579f, 0f, 0f), new Quaternion(0.005423481f, -0.0379741f, -0.05752105f, 0.9976071f), 249));
            alFkOrg.Add(new Bone(new Vector3(9.367114f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 250));
            alFkOrg.Add(new Bone(new Vector3(-3.253372f, 3.488787f, 7.719899f), new Quaternion(-0.1379701f, -0.02152699f, -0.9868309f, 0.08164306f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 252));
            alFkOrg.Add(new Bone(new Vector3(5.29524f, 0f, 0f), new Quaternion(0.004718692f, -0.1310388f, 0.01574588f, 0.991241f), 253));
            alFkOrg.Add(new Bone(new Vector3(4.267454f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 254));
            alFkOrg.Add(new Bone(new Vector3(-2.478653f, -2.416941f, 8.388333f), new Quaternion(-0.215875f, -0.01405905f, -0.9641567f, 0.1536298f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 256));
            alFkOrg.Add(new Bone(new Vector3(5.005202f, 0f, 0f), new Quaternion(-0.03913647f, -0.2166869f, 0.089942f, 0.9713009f), 257));
            alFkOrg.Add(new Bone(new Vector3(3.845697f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 258));
            alFkOrg.Add(new Bone(new Vector3(-0.7937546f, -5.651979f, 5.286433f), new Quaternion(-0.1355211f, 0.007501904f, -0.9564272f, 0.2585044f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 260));
            alFkOrg.Add(new Bone(new Vector3(6.833647f, 0f, 0f), new Quaternion(-0.05822291f, -0.1352987f, 0.1293944f, 0.9805924f), 261));
            alFkOrg.Add(new Bone(new Vector3(6.331366f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 262));
            alFkOrg.Add(new Bone(new Vector3(-2.916733f, 6.460262f, -3.814626f), new Quaternion(0.08285338f, 0.01741494f, -0.9964036f, 0.003434965f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 264));
            alFkOrg.Add(new Bone(new Vector3(7.185788f, 0f, 0f), new Quaternion(-0.01358782f, 0.03771152f, -0.05769368f, 0.9975292f), 265));
            alFkOrg.Add(new Bone(new Vector3(9.367112f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 266));
            alFkOrg.Add(new Bone(new Vector3(-3.253372f, 3.489149f, -7.719734f), new Quaternion(0.1381349f, 0.01957515f, -0.9868714f, 0.08136385f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 268));
            alFkOrg.Add(new Bone(new Vector3(5.295239f, 0f, 0f), new Quaternion(-0.006729044f, 0.1310694f, 0.01548696f, 0.9912294f), 269));
            alFkOrg.Add(new Bone(new Vector3(4.267454f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 270));
            alFkOrg.Add(new Bone(new Vector3(-2.478653f, -2.41655f, -8.388447f), new Quaternion(0.2155554f, 0.01610471f, -0.9641247f, 0.1540775f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 272));
            alFkOrg.Add(new Bone(new Vector3(5.005202f, 0f, 0f), new Quaternion(0.03875457f, 0.2162721f, 0.09093502f, 0.9713163f), 273));
            alFkOrg.Add(new Bone(new Vector3(3.845698f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 274));
            alFkOrg.Add(new Bone(new Vector3(-0.7937546f, -5.651731f, -5.286701f), new Quaternion(0.1350451f, -0.005697537f, -0.9564396f, 0.2587536f), 71));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 276));
            alFkOrg.Add(new Bone(new Vector3(6.833649f, 0f, 0f), new Quaternion(0.06431124f, 0.1356197f, 0.1290579f, 0.9802119f), 277));
            alFkOrg.Add(new Bone(new Vector3(6.331363f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 278));
            alFkOrg.Add(new Bone(new Vector3(-0.0766331f, -0.5443779f, 4.018825f), new Quaternion(-0.7300396f, 0.6834047f, -0.0003094011f, 0.0001920135f), 101));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 280));
            alFkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(1.947072E-07f, 0.9999999f, -4.371138E-08f, -8.510921E-15f), 281));
            alFkOrg.Add(new Bone(new Vector3(21.10731f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 282));
            alFkOrg.Add(new Bone(new Vector3(6.72208f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 283));
            alFkOrg.Add(new Bone(new Vector3(2.953814f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 284));
            alFkOrg.Add(new Bone(new Vector3(2.907539f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 285));
            alFkOrg.Add(new Bone(new Vector3(3.116349f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 286));
            alFkOrg.Add(new Bone(new Vector3(2.979294f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 287));
            alFkOrg.Add(new Bone(new Vector3(3.162624f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 288));
            alFkOrg.Add(new Bone(new Vector3(2.999702f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 289));
            alFkOrg.Add(new Bone(new Vector3(3.261963f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 290));
            alFkOrg.Add(new Bone(new Vector3(7.035393f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 291));

            alIkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), -1));
            alIkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 0));
            alIkOrg.Add(new Bone(new Vector3(0f, 92.66396f, 1.177912f), new Quaternion(0f, 0f, 0f, 1f), 1));
            alIkOrg.Add(new Bone(new Vector3(-2.32432f, -3.773982f, 8.923744f), new Quaternion(0f, -0.1779435f, 0f, 0.9840407f), 2));
            alIkOrg.Add(new Bone(new Vector3(-26.53911f, -12.35726f, -2.177667f), new Quaternion(2.781813E-08f, 2.185569E-08f, -3.473581E-16f, 1f), 3));
            alIkOrg.Add(new Bone(new Vector3(0.0236055f, -13.80228f, -2.947093f), new Quaternion(0f, 0f, 0f, 1f), 3));
            alIkOrg.Add(new Bone(new Vector3(3.627339E-07f, 5.102264f, -0.3834427f), new Quaternion(0f, 0.06540312f, 0f, 0.9978589f), 3));
            alIkOrg.Add(new Bone(new Vector3(23.26344f, -12.10931f, -14.89595f), new Quaternion(2.781813E-08f, 2.185569E-08f, -3.473581E-16f, 1f), 3));
            alIkOrg.Add(new Bone(new Vector3(15f, -75.23045f, 10f), new Quaternion(9.290539E-10f, 0.06975646f, -6.496576E-11f, 0.9975641f), 2));
            alIkOrg.Add(new Bone(new Vector3(-11.19084f, -75.23037f, -13.54689f), new Quaternion(0f, -0.4265687f, 0f, 0.9044551f), 2));
            alIkOrg.Add(new Bone(new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 1));

            numericUpDownSelBone.Maximum = alFkOrg.Count - 1;

            foreach (Bone o in alFkOrg) {
                o.v *= scale;
            }
            foreach (Bone o in alIkOrg) {
                o.v *= scale;
            }

            if (true) {
                Matrix M = new Matrix();
                M.M11 = 1;
                M.M22 = -1;
                M.M44 = 1;
                Mv[0] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M31 = 1;
                M.M22 = -1;
                M.M44 = 1;
                Mv[1] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M11 = 1;
                M.M32 = -1;
                M.M44 = 1;
                Mv[2] = M;
            }
            if (true) {
                Matrix M = new Matrix();
                M.M44 = 1;
                Mv[3] = M;
            }

            recalc();
            numericUpDownSelBone_ValueChanged(null, null);
        }

        const float scale = 5.0f;

        /// <summary>
        /// 一般的なBone構造
        /// </summary>
        class Bone {
            /// <summary>
            /// 頂点座標
            /// </summary>
            public Vector3 v = Vector3.Empty;
            /// <summary>
            /// 回転情報
            /// </summary>
            public Quaternion r = Quaternion.Identity;

            /// <summary>
            /// IK用　一時　回転情報
            /// </summary>
            public Quaternion tempr = Quaternion.Identity;

            public int parent = -1;

            public Bone() { }
            public Bone(Vector3 v, Quaternion r, int parent) { this.v = v; this.r = r; this.parent = parent; }

            public Bone Clone() {
                return (Bone)MemberwiseClone();
            }

            public override string ToString() {
                return string.Format("Qx {0,10:0.000} Qy {1,10:0.000} Qz {2,10:0.000} Qw {3,10:0.000} | Tx {4,10:0.000} Ty {5,10:0.000} Tz {6,10:0.000}"
                    , r.X, r.Y, r.Z, r.W, v.X, v.Y, v.Z);
            }
        }
        /// <summary>
        /// 相対的な量を持つ　計算前の　Bone構造（オリジナル）
        /// </summary>
        List<Bone> alFkOrg = new List<Bone>();
        /// <summary>
        /// 絶対的な量を持つ　計算後の　Bone構造
        /// </summary>
        List<Bone> alGof = new List<Bone>();

        /// <summary>
        /// 相対的な量を持つ　計算前の　IK-Bone構造（オリジナル）
        /// </summary>
        List<Bone> alIkOrg = new List<Bone>();
        /// <summary>
        /// 絶対的な量を持つ　計算後の　IK-Bone構造
        /// </summary>
        List<Bone> alGoi = new List<Bone>();

        /// <summary>
        /// 到着地点
        /// </summary>
        Vector3 ptik = Vector3.Empty;

        /// <summary>
        /// 指定した子Boneを回転する。但し親Boneの回転情報は無視してprotqの方を使う
        /// </summary>
        /// <param name="bi">再計算したい子ボーン</param>
        /// <param name="protq">親ボーンの回転情報</param>
        void calcBone(int bi) {
            Bone oo = alFkOrg[bi];
            Bone o = alGof[bi];
            if (o.parent >= 0) {
                Bone p = alGof[o.parent];
                o.r = p.tempr * o.r;
                o.v = p.v + Vector3.TransformCoordinate(oo.v, Matrix.RotationQuaternion(p.tempr));
            }
        }

        /// <summary>
        /// Boneを計算する
        /// </summary>
        void recalc() {
            if (alGoi.Count == 0 || checkBoxKeep.Checked == false) {
                alGoi.Clear();
                for (int t = 0; t < alIkOrg.Count; t++) {
                    Bone o = alIkOrg[t].Clone();
                    if (o.parent >= 0) {
                        Bone p = alGoi[o.parent];
                        o.r = o.r * p.r;
                        o.v = p.v + Vector3.TransformCoordinate(o.v, Matrix.RotationQuaternion(p.r));
                    }
                    alGoi.Add(o);
                }
            }
            if (alGof.Count == 0 || checkBoxKeep.Checked == false) {
                alGof.Clear();
                int basi = 94;
                int cw = (int)numericUpDownRepeat.Value;
                for (int t = 0; t < alFkOrg.Count; t++) {
                    Bone o = alFkOrg[t].Clone();
                    if (o.parent >= 0) {
                        Bone p = alGof[o.parent];
                        o.r = o.r * p.r;
                        o.v = p.v + Vector3.TransformCoordinate(o.v, Matrix.RotationQuaternion(p.r));
                    }
                    alGof.Add(o);

                    if (ptik != Vector3.Empty && t == 95) {
                        for (int w = 0; w < cw; w++) {
                            //
                            alGof[basi + 0].tempr = alGof[basi + 0].r * CalcUtil.cross(alGof[basi + 0].v, alGof[basi + 1].v, ptik);
                            calcBone(basi + 1);
                            //
                            alGof[basi + 0].r = alGof[basi + 0].tempr;
                        }
                        o.r = alFkOrg[t].r * alGof[basi + 0].r;
                    }
                }
            }
            //string s = "";
            //for (int t = 0; t < alGo.Count; t++) {
            //    s += string.Format("{0}|", t) + alGo[t].ToString() + "\n";
            //}
            //labelHint.Text = s;
            //labelHint.ForeColor = Color.FromArgb(50, this.ForeColor);
        }

        class CalcUtil {
            /// <summary>
            /// v0を基準にして，v1方向からv2方向に向くようなQuaternionを計算する。外積＋内積で算出する。
            /// </summary>
            /// <param name="v0">基点</param>
            /// <param name="v1">当来の点</param>
            /// <param name="v2">目標の点</param>
            /// <param name="r">回転量</param>
            /// <returns>回転軸</returns>
            public static Quaternion cross(Vector3 v0, Vector3 v1, Vector3 v2) {
                v1 -= v0; v1.Normalize(); NaNUtil.testNaN(v1);
                v2 -= v0; v2.Normalize(); NaNUtil.testNaN(v2);
                float dot = Vector3.Dot(v1, v2); Debug.Assert(!float.IsNaN(dot), "dot is NaN");
                float rad = (float)Math.Acos(Math.Min(1.0f, dot)); Debug.Assert(!float.IsNaN(rad), "rad is NaN");
                Vector3 cross = Vector3.Cross(v1, v2); NaNUtil.testNaN(cross);
                return Quaternion.RotationAxis(cross, rad);
            }
        }

        class NaNUtil {
            public static void testNaN(Vector3 v) {
                Debug.Assert(!float.IsNaN(v.X), "v.X is NaN");
                Debug.Assert(!float.IsNaN(v.Y), "v.Y is NaN");
                Debug.Assert(!float.IsNaN(v.Z), "v.Z is NaN");
            }
        }

        [Description("http://monsho.hp.infoseek.co.jp/dx/dx48.html")]
        private void Form1_Paint(object sender, PaintEventArgs e) {
        }

        /// <summary>
        /// ボーン間接をわかりやすく描画する
        /// </summary>
        class ArrowUtil {
            public static void draw(Graphics cv, Pen pen, Vector3 v0, Vector3 v1) {
                Vector3 vf = v1 - v0; vf.Normalize();
                Vector3 vr = Vector3.TransformCoordinate(vf, Matrix.RotationZ(3.14f / 2));
                float flen = 10;
                DUtil.drawLine(cv, pen, v0, v1);
                DUtil.drawCircle(cv, pen, v0, flen);
                Vector3 vxl = v0 - vr * flen;
                Vector3 vxr = v0 + vr * flen;
                Vector3 vxb = v0 - vf * flen;
                DUtil.drawLine(cv, pen, v0, vxl);
                DUtil.drawLine(cv, pen, v0, vxr);
                DUtil.drawLine(cv, pen, v0, vxb);
                DUtil.drawLine(cv, pen, vxl, v1);
                DUtil.drawLine(cv, pen, vxr, v1);
            }
        }
        class DUtil {
            public static void fillBPt(Graphics cv, Brush br, Vector3 v0) {
                cv.FillRectangle(br, v0.X - 1, v0.Y - 1, 3.0f, 3.0f);
            }
            public static void drawLine(Graphics cv, Pen pen, Vector3 v0, Vector3 v1) {
                cv.DrawLine(pen, v0.X, v0.Y, v1.X, v1.Y);
            }
            public static void drawCircle(Graphics cv, Pen pen, Vector3 v0, float r) {
                cv.DrawEllipse(pen, v0.X - r, v0.Y - r, 2 * r, 2 * r);
            }
            public static void drawAxStr(Graphics cv, Font font, Color clr, int x, int y, string s) {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                cv.DrawString(s, font, new SolidBrush(clr), x, y, sf);
            }
        }

        private void Form1_Resize(object sender, EventArgs e) {
            redraw();
        }

        private void numericUpDownRepeat_ValueChanged(object sender, EventArgs e) {
            recalc(); redraw();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        Matrix[] Mv = new Matrix[4];

        Matrix getM(object sender) {
            if (sender == panel2) return Mv[1];
            if (sender == panel3) return Mv[2];
            if (sender == panel4) return Mv[3];
            return Mv[0];
        }

        Vector3 offv = Vector3.Empty;

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Matrix M = getM(sender);
            Graphics cv = e.Graphics;
            System.Drawing.Drawing2D.GraphicsState gstate = cv.Save();
            Control Me = (Control)sender;
            try {
                int cx = Me.ClientSize.Width;
                int cy = Me.ClientSize.Height;
                cv.TranslateTransform(cx / 2, cy / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                cv.DrawLine(Pens.Gray, 0, -100, 0, +100);
                cv.DrawLine(Pens.Gray, -100, 0, +100, 0);
                if (sender == panel1) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, -cy / 2 + 10, "+y"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+x"); }
                if (sender == panel2) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, -cy / 2 + 10, "+y"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+z"); }
                if (sender == panel3) { DUtil.drawAxStr(cv, Me.Font, Color.Blue, 0, cy / 2 - 10, "+z"); DUtil.drawAxStr(cv, Me.Font, Color.Blue, cx / 2 - 10, 0, "+x"); }

                for (int t = 0; t < alGoi.Count; t++) {
                    Bone o = alGoi[t];
                    Bone p = (o.parent < 0) ? null : alGoi[o.parent];
                    if (p != null) {
                        ArrowUtil.draw(cv, Pens.DarkGreen, Vector3.TransformCoordinate(p.v + offv, M), Vector3.TransformCoordinate(o.v + offv, M));
                    }
                }
                for (int t = 0; t < alGof.Count; t++) {
                    Bone o = alGof[t];
                    Bone p = (o.parent < 0) ? null : alGof[o.parent];
                    if (p != null) {
                        ArrowUtil.draw(cv, Pens.DarkBlue, Vector3.TransformCoordinate(p.v + offv, M), Vector3.TransformCoordinate(o.v + offv, M));
                    }
                }
                DUtil.fillBPt(cv, Brushes.Magenta, Vector3.TransformCoordinate(ptik + offv, M));
            }
            finally {
                cv.Restore(gstate);
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            ((Control)sender).Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) {
            if (0 != (e.Button & MouseButtons.Left)) {
                int cx = ((Control)sender).ClientSize.Width;
                int cy = ((Control)sender).ClientSize.Height;

                if (sender == panel1) ptik = new Vector3(+(e.X - cx / 2) - offv.X, -(e.Y - cy / 2) - offv.Y, ptik.Z);
                if (sender == panel2) ptik = new Vector3(ptik.X, -(e.Y - cy / 2) - offv.Y, +(e.X - cx / 2) - offv.Z);
                if (sender == panel3) ptik = new Vector3(e.X - cx / 2, ptik.Y, -(e.Y - cy / 2));

                recalc(); redraw();
            }
        }

        private void numericUpDownSelBone_ValueChanged(object sender, EventArgs e) {
            int sel = (int)numericUpDownSelBone.Value;
            if ((uint)sel >= alGof.Count) return;
            offv = -alGof[sel].v;
            recalc(); redraw();
        }
    }
}