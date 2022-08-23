namespace khiiMapv {
    partial class RDForm {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RDForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.IL = new System.Windows.Forms.ImageList(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.p1 = new System.Windows.Forms.Panel();
            this.listViewMicroLabels = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bErrorPop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPixi = new System.Windows.Forms.Label();
            this.labelHelpMore = new System.Windows.Forms.Label();
            this.bExportBin = new System.Windows.Forms.Button();
            this.bExportAll = new System.Windows.Forms.Button();
            this.bBatExp = new System.Windows.Forms.LinkLabel();
            this.hexViewer = new Readmset.HexVwer();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.IL;
            this.treeView1.Location = new System.Drawing.Point(12, 23);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(246, 263);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // IL
            // 
            this.IL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IL.ImageStream")));
            this.IL.TransparentColor = System.Drawing.Color.Magenta;
            this.IL.Images.SetKeyName(0, "RightArrowHS.png");
            this.IL.Images.SetKeyName(1, "RightsRestrictedHS.png");
            this.IL.Images.SetKeyName(2, "SpeechMicHS.png");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.AutoScroll = true;
            this.p1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1.Location = new System.Drawing.Point(264, 24);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(370, 262);
            this.p1.TabIndex = 2;
            // 
            // listViewMicroLabels
            // 
            this.listViewMicroLabels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMicroLabels.HideSelection = false;
            this.listViewMicroLabels.Location = new System.Drawing.Point(640, 322);
            this.listViewMicroLabels.MultiSelect = false;
            this.listViewMicroLabels.Name = "listViewMicroLabels";
            this.listViewMicroLabels.Size = new System.Drawing.Size(53, 252);
            this.listViewMicroLabels.TabIndex = 10;
            this.listViewMicroLabels.UseCompatibleStateImageBehavior = false;
            this.listViewMicroLabels.View = System.Windows.Forms.View.List;
            this.listViewMicroLabels.SelectedIndexChanged += new System.EventHandler(this.listViewMicroLabels_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(640, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "Map &viewer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 578);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(295, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "* Drop a map file to window. Then click \"viewer\" button.";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 596);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(511, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "* Currently accepts map mdlx apdx fm 2dd bar 2ld mset pax wd vsb ard imd mag dpd " +
    "dpx vas vag";
            // 
            // bErrorPop
            // 
            this.bErrorPop.Image = global::khiiMapv.Properties.Resources.ActualSizeHS;
            this.bErrorPop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bErrorPop.Location = new System.Drawing.Point(12, 292);
            this.bErrorPop.Name = "bErrorPop";
            this.bErrorPop.Size = new System.Drawing.Size(131, 23);
            this.bErrorPop.TabIndex = 7;
            this.bErrorPop.Text = "Show error popup";
            this.bErrorPop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bErrorPop.UseVisualStyleBackColor = true;
            this.bErrorPop.Visible = false;
            this.bErrorPop.Click += new System.EventHandler(this.bserr_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(640, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pixel info:";
            // 
            // labelPixi
            // 
            this.labelPixi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPixi.AutoSize = true;
            this.labelPixi.Location = new System.Drawing.Point(640, 95);
            this.labelPixi.Name = "labelPixi";
            this.labelPixi.Size = new System.Drawing.Size(11, 96);
            this.labelPixi.TabIndex = 5;
            this.labelPixi.Text = "...\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            // 
            // labelHelpMore
            // 
            this.labelHelpMore.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelHelpMore.Location = new System.Drawing.Point(149, 289);
            this.labelHelpMore.Name = "labelHelpMore";
            this.labelHelpMore.Size = new System.Drawing.Size(720, 30);
            this.labelHelpMore.TabIndex = 8;
            this.labelHelpMore.Text = "...";
            // 
            // bExportBin
            // 
            this.bExportBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExportBin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExportBin.Location = new System.Drawing.Point(640, 194);
            this.bExportBin.Name = "bExportBin";
            this.bExportBin.Size = new System.Drawing.Size(59, 40);
            this.bExportBin.TabIndex = 6;
            this.bExportBin.Text = "&Export bin";
            this.bExportBin.UseVisualStyleBackColor = true;
            this.bExportBin.Click += new System.EventHandler(this.bExportBin_Click);
            // 
            // bExportAll
            // 
            this.bExportAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExportAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExportAll.Location = new System.Drawing.Point(640, 240);
            this.bExportAll.Name = "bExportAll";
            this.bExportAll.Size = new System.Drawing.Size(59, 46);
            this.bExportAll.TabIndex = 13;
            this.bExportAll.Text = "Export &all";
            this.bExportAll.UseVisualStyleBackColor = true;
            this.bExportAll.Click += new System.EventHandler(this.bExportAll_Click);
            // 
            // bBatExp
            // 
            this.bBatExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bBatExp.AutoSize = true;
            this.bBatExp.LinkArea = new System.Windows.Forms.LinkArea(5, 14);
            this.bBatExp.Location = new System.Drawing.Point(313, 578);
            this.bBatExp.Name = "bBatExp";
            this.bBatExp.Size = new System.Drawing.Size(104, 17);
            this.bBatExp.TabIndex = 14;
            this.bBatExp.TabStop = true;
            this.bBatExp.Text = "New: &Batch Export!";
            this.bBatExp.UseCompatibleTextRendering = true;
            this.bBatExp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bBatExp_LinkClicked);
            // 
            // hexViewer
            // 
            this.hexViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexViewer.AntiFlick = true;
            this.hexViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexViewer.ByteWidth = 16;
            this.hexViewer.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexViewer.Location = new System.Drawing.Point(12, 322);
            this.hexViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.OffDelta = 0;
            this.hexViewer.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hexViewer.Size = new System.Drawing.Size(622, 252);
            this.hexViewer.TabIndex = 9;
            this.hexViewer.UnitPg = 512;
            // 
            // RDForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 632);
            this.Controls.Add(this.bBatExp);
            this.Controls.Add(this.bExportAll);
            this.Controls.Add(this.bExportBin);
            this.Controls.Add(this.labelHelpMore);
            this.Controls.Add(this.labelPixi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bErrorPop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewMicroLabels);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.hexViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RDForm";
            this.Text = "map exploring";
            this.Load += new System.EventHandler(this.RDForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.RDForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.RDForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Readmset.HexVwer hexViewer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.ListView listViewMicroLabels;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bErrorPop;
        private System.Windows.Forms.ImageList IL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPixi;
        private System.Windows.Forms.Label labelHelpMore;
        private System.Windows.Forms.Button bExportBin;
        private System.Windows.Forms.Button bExportAll;
        private System.Windows.Forms.LinkLabel bBatExp;
    }
}

