using kenjiuno.AutoHourglass;
using parseSEQDv2.Models;
using parseSEQDv2.Models.D3D;
using parseSEQDv2.Models.LaydTypes;
using parseSEQDv2.Models.SeqdTypes;
using parseSEQDv2.Utils;
using SlimDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parseSEQDv2 {
    public partial class Form1 : Form {
        private List<Layd> LaydList = new List<Layd>();
        private string mruFile => Path.Combine(Application.StartupPath, "MRU.txt");

        public Form1() {
            InitializeComponent();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null) {
                foreach (var file in files.Take(1)) {
                    Read2ldFromFile(file);
                    AddToMRU(file);
                }
            }
        }


        private void Read2ldFromFile(string fp2ld) {
            inputFileLabel.Text = fp2ld;

            var decoded2ld = new Read2ld(fp2ld);
            LaydList = decoded2ld.LaydList;

            printableObjectsCombo.Items.Clear();
            printableObjectsCombo.Items.AddRange(decoded2ld.AllObjectsForDebug.ToArray());
            if (printableObjectsCombo.Items.Count >= 1) {
                printableObjectsCombo.SelectedIndex = 0;
            }

            laySeqSelectorCombo.Items.Clear();
            laySeqSelectorCombo.Items.AddRange(
                LaydList
                    .SelectMany(
                        oneLayd =>
                            oneLayd.PropList
                                .Select(
                                    (oneSeqProp, oneSeqPropIndex) =>
                                        new RenderPartSelect {
                                            layd = oneLayd,
                                            sequencePropIndex = oneSeqPropIndex,
                                            sequenceProp = oneSeqProp,
                                        }
                                )
                    )
                    .ToArray()
            );
            if (laySeqSelectorCombo.Items.Count >= 1) {
                laySeqSelectorCombo.SelectedIndex = 0;
            }
        }

        class RenderPartSelect {
            public Layd layd;
            public SequenceProperty sequenceProp;
            public int sequencePropIndex;

            public override string ToString() => $"{layd} … #{sequencePropIndex}: {sequenceProp}";
        }

        private void Form1_Load(object sender, EventArgs e) {
            // PS2 video is: 512x418
#if DEBUG
            var one = @"H:\KH2fm.yaz0r\menu\fm\pause.2ld";
            //var one = (@"H:\KH2fm.yaz0r\menu\fm\save.2ld");
            //var one = @"H:\KH2fm.yaz0r\menu\fm\title.2ld";
            //var one = @"H:\KH2fm.yaz0r\menu\fm\camp.2ld";
            //var one = (@"H:\KH2fm.yaz0r\map\jp\tt00.map");
            if (File.Exists(one)) {
                Read2ldFromFile(one);
            }
#endif
        }

        private void timeSpin_ValueChanged(object sender, EventArgs e) {
            RenderLayd();
        }

        private void RenderLayd() {
            var time = (int)timeSpin.Value;
            var renderPartSel = (RenderPartSelect)laySeqSelectorCombo.SelectedItem;

            if (renderPartSel != null) {
                var visibleMask = visibleOfFramesControl1.VisibleMask;
                if (visibleMask.Cast<bool>().All(it => it == false)) {
                    visibleMask.SetAll(true);
                }
                var renderedMask = new BitArray(visibleMask.Count);

                var renderer = new RenderSeqd();
                var oneLayd = renderPartSel.layd;
                var sequenceProp = renderPartSel.sequenceProp;
                var oneSeqd = oneLayd.SeqdList[sequenceProp.SequenceIndex];
                renderer.InterpretAndAddToMesh(
                    time,
                    oneSeqd,
                    sequenceProp.TextureIndex,
                    sequenceProp.AnimationGroup,
                    sequenceProp.PositionX,
                    sequenceProp.PositionY,
                    fovX,
                    fovY,
                    picImg => CacheBitmapToTextureList(picImg.Bitmap),
                    visibleMask,
                    renderedMask
                );

                visibleOfFramesControl1.RenderedMask = renderedMask;
                renderPanel.PS2ScreenSize = ps2ScreenSizeCheck.Checked;
                renderPanel.CustomMeshesForRendering = renderer.MeshList.ToArray();
            }
        }

        float fovX = 1;
        float fovY = 1;

        List<Bitmap> cachedTextureList = new List<Bitmap>();


        private int CacheBitmapToTextureList(Bitmap bitmap) {
            var index = cachedTextureList.IndexOf(bitmap);
            if (index < 0) {
                index = cachedTextureList.Count;
                cachedTextureList.Add(bitmap);
                renderPanel.AddBitmapToTextureList(bitmap);
            }
            return index;
        }

        private void autoStepCheck_CheckedChanged(object sender, EventArgs e) {
            timerTick.Enabled = autoStepCheck.Checked;
        }

        private void timerTick_Tick(object sender, EventArgs e) {
            timeSpin.Value += 1;
        }

        private void renderPanel_Resize(object sender, EventArgs e) {
            RenderLayd();
        }

        class TexSeqPair {
            public int SequenceIndex { get; internal set; }
            public int TextureIndex { get; internal set; }

            public override string ToString() {
                return $"seq{SequenceIndex} tex{TextureIndex}";
            }
        }

        private void savePicBtn_Click(object sender, EventArgs e) {
            using (new AH()) {
                var dirRoot = Path.Combine(Application.StartupPath, Path.GetFileName(inputFileLabel.Text).Replace(".", "_"));
                Directory.CreateDirectory(dirRoot);

                if (LaydList.Any()) {
                    foreach (var oneLayd in LaydList) {
                        foreach (var oneSeqd in oneLayd.SeqdList) {
                            foreach (var picture in oneSeqd.Pictures) {
                                var picId = picture.Id;
                                picture.Bitmap.Save(Path.Combine(dirRoot, $"{picId}.png"));
                            }
                        }
                    }

                    foreach (var (oneLayd, laydIndex) in LaydList.Select((it, i) => (it, i))) {
                        var pairs = oneLayd.PropList
                            .GroupBy(prop => $"{prop.TextureIndex}:{prop.SequenceIndex}")
                            .Select(
                                group => new TexSeqPair {
                                    SequenceIndex = group.First().SequenceIndex,
                                    TextureIndex = group.First().TextureIndex,
                                }
                            );

                        foreach (var pair in pairs) {
                            var oneSeqd = oneLayd.SeqdList[pair.SequenceIndex];
                            var texture = oneLayd.Pictures[pair.TextureIndex];

                            //var dir = Path.Combine(dirRoot, oneSeqd.ToString()); // danger
                            var dir = Path.Combine(dirRoot, $"{oneSeqd.BarId} {pair}");
                            Directory.CreateDirectory(dir);

                            foreach (var (frame, frameIndex) in oneSeqd.FrameList.Select((it, i) => (it, i))) {
                                if (frame.Width > 0 && frame.Height > 0) {
                                    var cut = new Bitmap(frame.Width, frame.Height);
                                    using (var canvas = Graphics.FromImage(cut)) {
                                        canvas.DrawImageUnscaled(texture.Bitmap, -frame.Left, -frame.Top);
                                    }
                                    cut.Save(Path.Combine(dir, $"Frame{frameIndex}.png"));
                                }
                            }
                        }
                    }
                }
                Process.Start(dirRoot);
            }
        }

        private void inputFileLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            fileMenu.Show(inputFileLabel, Point.Empty);
        }

        private void printableObjectsCombo_SelectedIndexChanged(object sender, EventArgs e) {
            var item = printableObjectsCombo.SelectedItem;

            StringWriter writer = new StringWriter();

            if (item is Layd oneLayd) {
                DescribePictures(writer, oneLayd.Pictures);
                {
                    writer.WriteLine("SequenceGroupList");
                    int y = 0;
                    foreach (var one in oneLayd.GroupList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
                {
                    writer.WriteLine("SequencePropertyList");
                    int y = 0;
                    foreach (var one in oneLayd.PropList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
            }
            if (item is Seqd oneSeqd) {
                DescribePictures(writer, oneSeqd.Pictures);
                {
                    writer.WriteLine("animGroupList");
                    int y = 0;
                    foreach (var one in oneSeqd.AnimGroupList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
                {
                    writer.WriteLine("animList");
                    int y = 0;
                    foreach (var one in oneSeqd.AnimList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
                {
                    writer.WriteLine("frameGroupList");
                    int y = 0;
                    foreach (var one in oneSeqd.FrameGroupList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
                {
                    writer.WriteLine("frameExList");
                    int y = 0;
                    foreach (var one in oneSeqd.FrameExList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
                {
                    writer.WriteLine("frameList");
                    int y = 0;
                    foreach (var one in oneSeqd.FrameList) {
                        writer.WriteLine("#{0,2}: {1}", y++, one);
                    }
                }
            }

            dumpText.Text = writer.ToString();
        }

        private void DescribePictures(StringWriter writer, PicIMGD[] pictures) {
            writer.WriteLine("imgList");
            int y = 0;
            foreach (var picture in pictures) {
                writer.WriteLine("#{0,2}: {1}", y++, picture);
            }
        }

        private void layerCombo_SelectedIndexChanged(object sender, EventArgs e) {
            var renderPartSel = (RenderPartSelect)laySeqSelectorCombo.SelectedItem;
            if (renderPartSel != null) {
                var oneLayd = renderPartSel.layd;
                var oneSeqd = oneLayd.SeqdList[renderPartSel.sequenceProp.SequenceIndex];

                visibleOfFramesControl1.FrameCount = oneSeqd.FrameList.Count;
            }

            RenderLayd();
        }

        private void visibleOfFramesControl1_VisibleMaskChanged(object sender, EventArgs e) {
            RenderLayd();
        }

        private void resetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            timeSpin.Value = 0;
        }

        private void ps2ScreenSizeCheck_CheckedChanged(object sender, EventArgs e) {
            RenderLayd();
        }

        private void resetLink_Click(object sender, EventArgs e) {
            timeSpin.Value = 0;
        }

        private void openFileMenu_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog(this) == DialogResult.OK) {
                Read2ldFromFile(ofd.FileName);
                AddToMRU(ofd.FileName);
            }
        }

        private void AddToMRU(string path) {
            File.AppendAllLines(mruFile, new string[] { path });
        }

        private void editMRUMenu_Click(object sender, EventArgs e) {
            using (File.Open(mruFile, FileMode.OpenOrCreate)) {
                // noop
            }
            Process.Start(mruFile);
        }

        private void fileMenu_Opening(object sender, CancelEventArgs e) {
            var at = fileMenu.Items.IndexOf(historySep);
            while (fileMenu.Items.Count > at + 1) {
                fileMenu.Items.RemoveAt(at + 1);
            }

            if (File.Exists(mruFile)) {
                foreach (var line in File.ReadAllLines(mruFile)) {
                    fileMenu.Items.Add(line, null, OnClickOpenThis);
                }
            }
        }

        private void OnClickOpenThis(object sender, EventArgs e) {
            var file = ((ToolStripItem)sender).Text;
            Read2ldFromFile(file);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {

        }

        private void reloadBtn_Click(object sender, EventArgs e) {
            using (new AH()) {
                Read2ldFromFile(inputFileLabel.Text);
            }
        }
    }
}
