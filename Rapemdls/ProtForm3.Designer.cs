namespace Rapemdls {
    partial class ProtForm3 {
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.checkBoxWire = new System.Windows.Forms.CheckBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.checkBoxApplyPatch = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyMotion = new System.Windows.Forms.CheckBox();
            this.checkBoxShow2 = new System.Windows.Forms.CheckBox();
            this.buttonImportU = new System.Windows.Forms.Button();
            this.hScrollBarAnimSel = new System.Windows.Forms.HScrollBar();
            this.checkBoxStartTimer = new System.Windows.Forms.CheckBox();
            this.buttonOnce = new System.Windows.Forms.Button();
            this.hScrollBarFrame = new System.Windows.Forms.HScrollBar();
            this.timerStepFrame = new System.Windows.Forms.Timer(this.components);
            this.labelBhint = new System.Windows.Forms.Label();
            this.buttonResetApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApplier = new System.Windows.Forms.TextBox();
            this.buttonUsebin = new System.Windows.Forms.Button();
            this.openFileDialogUsebin = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxUseik = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCopyik = new System.Windows.Forms.Button();
            this.panel1 = new hex04BinTrack.UC();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 517);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(139, 100);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.IntegralHeight = false;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(145, 517);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(139, 100);
            this.listBox2.TabIndex = 2;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // checkBoxWire
            // 
            this.checkBoxWire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxWire.AutoSize = true;
            this.checkBoxWire.Checked = true;
            this.checkBoxWire.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWire.Location = new System.Drawing.Point(0, 495);
            this.checkBoxWire.Name = "checkBoxWire";
            this.checkBoxWire.Size = new System.Drawing.Size(46, 16);
            this.checkBoxWire.TabIndex = 3;
            this.checkBoxWire.Text = "&Wire";
            this.checkBoxWire.UseVisualStyleBackColor = true;
            this.checkBoxWire.CheckedChanged += new System.EventHandler(this.checkBoxLighting_CheckedChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(290, 517);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 100);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "B\'ones";
            this.columnHeader1.Width = 461;
            // 
            // checkBoxApplyPatch
            // 
            this.checkBoxApplyPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxApplyPatch.AutoSize = true;
            this.checkBoxApplyPatch.Location = new System.Drawing.Point(290, 495);
            this.checkBoxApplyPatch.Name = "checkBoxApplyPatch";
            this.checkBoxApplyPatch.Size = new System.Drawing.Size(119, 16);
            this.checkBoxApplyPatch.TabIndex = 6;
            this.checkBoxApplyPatch.Text = "Apply vone &patchh";
            this.checkBoxApplyPatch.UseVisualStyleBackColor = true;
            this.checkBoxApplyPatch.CheckedChanged += new System.EventHandler(this.checkBoxApplyPatch_CheckedChanged);
            // 
            // checkBoxApplyMotion
            // 
            this.checkBoxApplyMotion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxApplyMotion.AutoSize = true;
            this.checkBoxApplyMotion.Location = new System.Drawing.Point(290, 473);
            this.checkBoxApplyMotion.Name = "checkBoxApplyMotion";
            this.checkBoxApplyMotion.Size = new System.Drawing.Size(91, 16);
            this.checkBoxApplyMotion.TabIndex = 7;
            this.checkBoxApplyMotion.Text = "Apply &motion";
            this.checkBoxApplyMotion.UseVisualStyleBackColor = true;
            this.checkBoxApplyMotion.CheckedChanged += new System.EventHandler(this.checkBoxApplyMotion_CheckedChanged);
            // 
            // checkBoxShow2
            // 
            this.checkBoxShow2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShow2.AutoSize = true;
            this.checkBoxShow2.Location = new System.Drawing.Point(290, 451);
            this.checkBoxShow2.Name = "checkBoxShow2";
            this.checkBoxShow2.Size = new System.Drawing.Size(117, 16);
            this.checkBoxShow2.TabIndex = 8;
            this.checkBoxShow2.Text = "Show &other b\'ones";
            this.checkBoxShow2.UseVisualStyleBackColor = true;
            this.checkBoxShow2.CheckedChanged += new System.EventHandler(this.checkBoxShow2_CheckedChanged);
            // 
            // buttonImportU
            // 
            this.buttonImportU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImportU.Location = new System.Drawing.Point(415, 491);
            this.buttonImportU.Name = "buttonImportU";
            this.buttonImportU.Size = new System.Drawing.Size(75, 23);
            this.buttonImportU.TabIndex = 9;
            this.buttonImportU.Text = "Import &U";
            this.buttonImportU.UseVisualStyleBackColor = true;
            this.buttonImportU.Click += new System.EventHandler(this.buttonImportU_Click);
            // 
            // hScrollBarAnimSel
            // 
            this.hScrollBarAnimSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBarAnimSel.Location = new System.Drawing.Point(290, 431);
            this.hScrollBarAnimSel.Maximum = 200;
            this.hScrollBarAnimSel.Name = "hScrollBarAnimSel";
            this.hScrollBarAnimSel.Size = new System.Drawing.Size(139, 17);
            this.hScrollBarAnimSel.TabIndex = 10;
            this.hScrollBarAnimSel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarAnimSel_Scroll);
            // 
            // checkBoxStartTimer
            // 
            this.checkBoxStartTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxStartTimer.AutoSize = true;
            this.checkBoxStartTimer.Location = new System.Drawing.Point(0, 460);
            this.checkBoxStartTimer.Name = "checkBoxStartTimer";
            this.checkBoxStartTimer.Size = new System.Drawing.Size(53, 16);
            this.checkBoxStartTimer.TabIndex = 11;
            this.checkBoxStartTimer.Text = "&Timer";
            this.checkBoxStartTimer.UseVisualStyleBackColor = true;
            this.checkBoxStartTimer.CheckedChanged += new System.EventHandler(this.checkBoxStartTimer_CheckedChanged);
            // 
            // buttonOnce
            // 
            this.buttonOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOnce.Location = new System.Drawing.Point(0, 431);
            this.buttonOnce.Name = "buttonOnce";
            this.buttonOnce.Size = new System.Drawing.Size(75, 23);
            this.buttonOnce.TabIndex = 12;
            this.buttonOnce.Text = "&Run once";
            this.buttonOnce.UseVisualStyleBackColor = true;
            this.buttonOnce.Click += new System.EventHandler(this.buttonOnce_Click);
            // 
            // hScrollBarFrame
            // 
            this.hScrollBarFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hScrollBarFrame.Location = new System.Drawing.Point(56, 460);
            this.hScrollBarFrame.Maximum = 120;
            this.hScrollBarFrame.Name = "hScrollBarFrame";
            this.hScrollBarFrame.Size = new System.Drawing.Size(228, 17);
            this.hScrollBarFrame.TabIndex = 13;
            this.hScrollBarFrame.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarFrame_Scroll);
            // 
            // timerStepFrame
            // 
            this.timerStepFrame.Tick += new System.EventHandler(this.timerStepFrame_Tick);
            // 
            // labelBhint
            // 
            this.labelBhint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBhint.AutoSize = true;
            this.labelBhint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelBhint.Location = new System.Drawing.Point(496, 517);
            this.labelBhint.Name = "labelBhint";
            this.labelBhint.Size = new System.Drawing.Size(23, 12);
            this.labelBhint.TabIndex = 14;
            this.labelBhint.Text = "...";
            // 
            // buttonResetApply
            // 
            this.buttonResetApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResetApply.Location = new System.Drawing.Point(498, 456);
            this.buttonResetApply.Name = "buttonResetApply";
            this.buttonResetApply.Size = new System.Drawing.Size(75, 23);
            this.buttonResetApply.TabIndex = 20;
            this.buttonResetApply.Text = "Res&et apply";
            this.buttonResetApply.UseVisualStyleBackColor = true;
            this.buttonResetApply.Click += new System.EventHandler(this.buttonResetApply_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(496, 436);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "&Apply";
            // 
            // textBoxApplier
            // 
            this.textBoxApplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxApplier.Location = new System.Drawing.Point(536, 431);
            this.textBoxApplier.Name = "textBoxApplier";
            this.textBoxApplier.Size = new System.Drawing.Size(100, 19);
            this.textBoxApplier.TabIndex = 23;
            this.textBoxApplier.TextChanged += new System.EventHandler(this.textBoxApplier_TextChanged);
            this.textBoxApplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxApplier_KeyDown);
            // 
            // buttonUsebin
            // 
            this.buttonUsebin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUsebin.Location = new System.Drawing.Point(498, 491);
            this.buttonUsebin.Name = "buttonUsebin";
            this.buttonUsebin.Size = new System.Drawing.Size(75, 23);
            this.buttonUsebin.TabIndex = 24;
            this.buttonUsebin.Text = "Use &bin";
            this.buttonUsebin.UseVisualStyleBackColor = true;
            this.buttonUsebin.Click += new System.EventHandler(this.buttonUsebin_Click);
            // 
            // openFileDialogUsebin
            // 
            this.openFileDialogUsebin.FileName = "H:\\Proj\\khkh_xldM\\MEMO\\xa_ex_0010.mset.b\'one.bin";
            // 
            // checkBoxUseik
            // 
            this.checkBoxUseik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxUseik.AutoSize = true;
            this.checkBoxUseik.Location = new System.Drawing.Point(642, 433);
            this.checkBoxUseik.Name = "checkBoxUseik";
            this.checkBoxUseik.Size = new System.Drawing.Size(66, 16);
            this.checkBoxUseik.TabIndex = 25;
            this.checkBoxUseik.Text = "Apply i&k";
            this.checkBoxUseik.UseVisualStyleBackColor = true;
            this.checkBoxUseik.CheckedChanged += new System.EventHandler(this.checkBoxUseik_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(607, 459);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 19);
            this.numericUpDown1.TabIndex = 26;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 461);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "B2i";
            // 
            // buttonCopyik
            // 
            this.buttonCopyik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyik.Location = new System.Drawing.Point(579, 491);
            this.buttonCopyik.Name = "buttonCopyik";
            this.buttonCopyik.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyik.TabIndex = 28;
            this.buttonCopyik.Text = "C\'ik";
            this.buttonCopyik.UseVisualStyleBackColor = true;
            this.buttonCopyik.Click += new System.EventHandler(this.buttonCopyik_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 428);
            this.panel1.TabIndex = 0;
            this.panel1.UseTransparent = true;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ProtForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 617);
            this.Controls.Add(this.buttonCopyik);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBoxUseik);
            this.Controls.Add(this.buttonUsebin);
            this.Controls.Add(this.textBoxApplier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonResetApply);
            this.Controls.Add(this.labelBhint);
            this.Controls.Add(this.hScrollBarFrame);
            this.Controls.Add(this.buttonOnce);
            this.Controls.Add(this.checkBoxStartTimer);
            this.Controls.Add(this.hScrollBarAnimSel);
            this.Controls.Add(this.buttonImportU);
            this.Controls.Add(this.checkBoxShow2);
            this.Controls.Add(this.checkBoxApplyMotion);
            this.Controls.Add(this.checkBoxApplyPatch);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.checkBoxWire);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ProtForm3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProtForm3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProtForm3_FormClosed);
            this.Load += new System.EventHandler(this.ProtForm3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private hex04BinTrack.UC panel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckBox checkBoxWire;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxApplyPatch;
        private System.Windows.Forms.CheckBox checkBoxApplyMotion;
        private System.Windows.Forms.CheckBox checkBoxShow2;
        private System.Windows.Forms.Button buttonImportU;
        private System.Windows.Forms.HScrollBar hScrollBarAnimSel;
        private System.Windows.Forms.CheckBox checkBoxStartTimer;
        private System.Windows.Forms.Button buttonOnce;
        private System.Windows.Forms.HScrollBar hScrollBarFrame;
        private System.Windows.Forms.Timer timerStepFrame;
        private System.Windows.Forms.Label labelBhint;
        private System.Windows.Forms.Button buttonResetApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApplier;
        private System.Windows.Forms.Button buttonUsebin;
        private System.Windows.Forms.OpenFileDialog openFileDialogUsebin;
        private System.Windows.Forms.CheckBox checkBoxUseik;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCopyik;
    }
}