namespace parseSEQDv2 {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ps2ScreenSizeCheck = new System.Windows.Forms.CheckBox();
            this.resetLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dumpText = new System.Windows.Forms.TextBox();
            this.printableObjectsCombo = new System.Windows.Forms.ComboBox();
            this.inputFileLabel = new System.Windows.Forms.LinkLabel();
            this.savePicBtn = new System.Windows.Forms.Button();
            this.autoStepCheck = new System.Windows.Forms.CheckBox();
            this.timeSpin = new System.Windows.Forms.NumericUpDown();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.laySeqSelectorCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.fileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.historySep = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editMRUMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.renderPanel = new parseSEQDv2.Controls.D3D.D3DDevicePanel();
            this.visibleOfFramesControl1 = new parseSEQDv2.Controls.VisibleOfFramesControl();
            this.reloadBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeSpin)).BeginInit();
            this.table.SuspendLayout();
            this.fileMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.reloadBtn);
            this.splitContainer1.Panel1.Controls.Add(this.ps2ScreenSizeCheck);
            this.splitContainer1.Panel1.Controls.Add(this.resetLink);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dumpText);
            this.splitContainer1.Panel1.Controls.Add(this.printableObjectsCombo);
            this.splitContainer1.Panel1.Controls.Add(this.inputFileLabel);
            this.splitContainer1.Panel1.Controls.Add(this.savePicBtn);
            this.splitContainer1.Panel1.Controls.Add(this.autoStepCheck);
            this.splitContainer1.Panel1.Controls.Add(this.timeSpin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.renderPanel);
            this.splitContainer1.Panel2.Controls.Add(this.table);
            this.splitContainer1.Size = new System.Drawing.Size(920, 538);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // ps2ScreenSizeCheck
            // 
            this.ps2ScreenSizeCheck.AutoSize = true;
            this.ps2ScreenSizeCheck.Location = new System.Drawing.Point(94, 106);
            this.ps2ScreenSizeCheck.Name = "ps2ScreenSizeCheck";
            this.ps2ScreenSizeCheck.Size = new System.Drawing.Size(114, 16);
            this.ps2ScreenSizeCheck.TabIndex = 7;
            this.ps2ScreenSizeCheck.Text = "&Simulate 512x418";
            this.ps2ScreenSizeCheck.UseVisualStyleBackColor = true;
            this.ps2ScreenSizeCheck.CheckedChanged += new System.EventHandler(this.ps2ScreenSizeCheck_CheckedChanged);
            // 
            // resetLink
            // 
            this.resetLink.AutoSize = true;
            this.resetLink.Location = new System.Drawing.Point(12, 92);
            this.resetLink.Name = "resetLink";
            this.resetLink.Size = new System.Drawing.Size(59, 12);
            this.resetLink.TabIndex = 6;
            this.resetLink.TabStop = true;
            this.resetLink.Text = "&Reset to 0";
            this.resetLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.resetLink_LinkClicked);
            this.resetLink.Click += new System.EventHandler(this.resetLink_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Decoded objects:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Frame time:";
            // 
            // dumpText
            // 
            this.dumpText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpText.Font = new System.Drawing.Font("Courier New", 9F);
            this.dumpText.Location = new System.Drawing.Point(3, 178);
            this.dumpText.Multiline = true;
            this.dumpText.Name = "dumpText";
            this.dumpText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dumpText.Size = new System.Drawing.Size(254, 348);
            this.dumpText.TabIndex = 10;
            this.dumpText.Text = "...";
            this.dumpText.WordWrap = false;
            // 
            // printableObjectsCombo
            // 
            this.printableObjectsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printableObjectsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printableObjectsCombo.Font = new System.Drawing.Font("Calibri", 9F);
            this.printableObjectsCombo.FormattingEnabled = true;
            this.printableObjectsCombo.Location = new System.Drawing.Point(12, 140);
            this.printableObjectsCombo.Name = "printableObjectsCombo";
            this.printableObjectsCombo.Size = new System.Drawing.Size(232, 22);
            this.printableObjectsCombo.TabIndex = 9;
            this.printableObjectsCombo.SelectedIndexChanged += new System.EventHandler(this.printableObjectsCombo_SelectedIndexChanged);
            // 
            // inputFileLabel
            // 
            this.inputFileLabel.AutoSize = true;
            this.inputFileLabel.Location = new System.Drawing.Point(12, 9);
            this.inputFileLabel.Name = "inputFileLabel";
            this.inputFileLabel.Size = new System.Drawing.Size(11, 12);
            this.inputFileLabel.TabIndex = 0;
            this.inputFileLabel.TabStop = true;
            this.inputFileLabel.Text = "...";
            this.inputFileLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLabel_LinkClicked);
            // 
            // savePicBtn
            // 
            this.savePicBtn.Location = new System.Drawing.Point(184, 55);
            this.savePicBtn.Name = "savePicBtn";
            this.savePicBtn.Size = new System.Drawing.Size(73, 46);
            this.savePicBtn.TabIndex = 5;
            this.savePicBtn.Text = "&Export\r\nLAYD";
            this.savePicBtn.UseVisualStyleBackColor = true;
            this.savePicBtn.Click += new System.EventHandler(this.savePicBtn_Click);
            // 
            // autoStepCheck
            // 
            this.autoStepCheck.AutoSize = true;
            this.autoStepCheck.Location = new System.Drawing.Point(94, 71);
            this.autoStepCheck.Name = "autoStepCheck";
            this.autoStepCheck.Size = new System.Drawing.Size(74, 16);
            this.autoStepCheck.TabIndex = 4;
            this.autoStepCheck.Text = "&Auto step";
            this.autoStepCheck.UseVisualStyleBackColor = true;
            this.autoStepCheck.CheckedChanged += new System.EventHandler(this.autoStepCheck_CheckedChanged);
            // 
            // timeSpin
            // 
            this.timeSpin.Location = new System.Drawing.Point(12, 70);
            this.timeSpin.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.timeSpin.Name = "timeSpin";
            this.timeSpin.Size = new System.Drawing.Size(76, 19);
            this.timeSpin.TabIndex = 3;
            this.timeSpin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timeSpin.ValueChanged += new System.EventHandler(this.timeSpin_ValueChanged);
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Controls.Add(this.label3, 0, 0);
            this.table.Controls.Add(this.laySeqSelectorCombo, 0, 1);
            this.table.Controls.Add(this.label4, 0, 2);
            this.table.Controls.Add(this.visibleOfFramesControl1, 0, 3);
            this.table.Dock = System.Windows.Forms.DockStyle.Top;
            this.table.Font = new System.Drawing.Font("Arial", 8F);
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 4;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.Size = new System.Drawing.Size(652, 106);
            this.table.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select layer and sequence pair to be rendered:";
            // 
            // laySeqSelectorCombo
            // 
            this.laySeqSelectorCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this.laySeqSelectorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.laySeqSelectorCombo.FormattingEnabled = true;
            this.laySeqSelectorCombo.Location = new System.Drawing.Point(3, 17);
            this.laySeqSelectorCombo.Name = "laySeqSelectorCombo";
            this.laySeqSelectorCombo.Size = new System.Drawing.Size(646, 22);
            this.laySeqSelectorCombo.TabIndex = 1;
            this.laySeqSelectorCombo.SelectedIndexChanged += new System.EventHandler(this.layerCombo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(389, 42);
            this.label4.TabIndex = 2;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // timerTick
            // 
            this.timerTick.Interval = 20;
            this.timerTick.Tick += new System.EventHandler(this.timerTick_Tick);
            // 
            // fileMenu
            // 
            this.fileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenu,
            this.toolStripSeparator1,
            this.editMRUMenu,
            this.historySep});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(252, 60);
            this.fileMenu.Opening += new System.ComponentModel.CancelEventHandler(this.fileMenu_Opening);
            // 
            // openFileMenu
            // 
            this.openFileMenu.Name = "openFileMenu";
            this.openFileMenu.Size = new System.Drawing.Size(251, 22);
            this.openFileMenu.Text = "Open file...";
            this.openFileMenu.Click += new System.EventHandler(this.openFileMenu_Click);
            // 
            // historySep
            // 
            this.historySep.Name = "historySep";
            this.historySep.Size = new System.Drawing.Size(248, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(248, 6);
            // 
            // editMRUMenu
            // 
            this.editMRUMenu.Name = "editMRUMenu";
            this.editMRUMenu.Size = new System.Drawing.Size(251, 22);
            this.editMRUMenu.Text = "Edit MRU (most recent used) list...";
            this.editMRUMenu.Click += new System.EventHandler(this.editMRUMenu_Click);
            // 
            // renderPanel
            // 
            this.renderPanel.CustomMeshesForRendering = new parseSEQDv2.Models.D3D.CustomMesh[0];
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Location = new System.Drawing.Point(0, 106);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.PS2ScreenSize = false;
            this.renderPanel.Size = new System.Drawing.Size(652, 432);
            this.renderPanel.TabIndex = 0;
            // 
            // visibleOfFramesControl1
            // 
            this.visibleOfFramesControl1.AutoSize = true;
            this.visibleOfFramesControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.visibleOfFramesControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.visibleOfFramesControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.visibleOfFramesControl1.ForeColor = System.Drawing.Color.White;
            this.visibleOfFramesControl1.FrameCount = 1;
            this.visibleOfFramesControl1.Location = new System.Drawing.Point(3, 87);
            this.visibleOfFramesControl1.Name = "visibleOfFramesControl1";
            this.visibleOfFramesControl1.RenderedMask = ((System.Collections.BitArray)(resources.GetObject("visibleOfFramesControl1.RenderedMask")));
            this.visibleOfFramesControl1.Size = new System.Drawing.Size(646, 16);
            this.visibleOfFramesControl1.TabIndex = 3;
            this.visibleOfFramesControl1.VisibleMask = ((System.Collections.BitArray)(resources.GetObject("visibleOfFramesControl1.VisibleMask")));
            this.visibleOfFramesControl1.VisibleMaskChanged += new System.EventHandler(this.visibleOfFramesControl1_VisibleMaskChanged);
            // 
            // reloadBtn
            // 
            this.reloadBtn.Location = new System.Drawing.Point(12, 29);
            this.reloadBtn.Name = "reloadBtn";
            this.reloadBtn.Size = new System.Drawing.Size(75, 23);
            this.reloadBtn.TabIndex = 1;
            this.reloadBtn.Text = "&Reload";
            this.reloadBtn.UseVisualStyleBackColor = true;
            this.reloadBtn.Click += new System.EventHandler(this.reloadBtn_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(920, 538);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "parseSEQD v2 Play LAYD/SEQD (drop .2ld .2dd files)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeSpin)).EndInit();
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.fileMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown timeSpin;
        private System.Windows.Forms.CheckBox autoStepCheck;
        private System.Windows.Forms.Timer timerTick;
        private System.Windows.Forms.Button savePicBtn;
        private System.Windows.Forms.LinkLabel inputFileLabel;
        private System.Windows.Forms.ComboBox printableObjectsCombo;
        private System.Windows.Forms.TextBox dumpText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls.D3D.D3DDevicePanel renderPanel;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox laySeqSelectorCombo;
        private System.Windows.Forms.Label label4;
        private Controls.VisibleOfFramesControl visibleOfFramesControl1;
        private System.Windows.Forms.LinkLabel resetLink;
        private System.Windows.Forms.CheckBox ps2ScreenSizeCheck;
        private System.Windows.Forms.ContextMenuStrip fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openFileMenu;
        private System.Windows.Forms.ToolStripSeparator historySep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editMRUMenu;
        private System.Windows.Forms.Button reloadBtn;
    }
}

