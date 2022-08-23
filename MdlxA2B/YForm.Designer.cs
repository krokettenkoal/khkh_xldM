namespace MdlxA2B {
    partial class YForm {
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
            this.flpt = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonMdlx = new System.Windows.Forms.Button();
            this.buttonMset = new System.Windows.Forms.Button();
            this.buttonLHMdlx = new System.Windows.Forms.Button();
            this.buttonLHMset = new System.Windows.Forms.Button();
            this.buttonRHMdlx = new System.Windows.Forms.Button();
            this.buttonRHMset = new System.Windows.Forms.Button();
            this.bPresets = new System.Windows.Forms.ToolStripDropDownButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numlhj = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numrhj = new System.Windows.Forms.NumericUpDown();
            this.flpt.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numlhj)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numrhj)).BeginInit();
            this.SuspendLayout();
            // 
            // flpt
            // 
            this.flpt.AutoSize = true;
            this.flpt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpt.Controls.Add(this.buttonMdlx);
            this.flpt.Controls.Add(this.buttonMset);
            this.flpt.Controls.Add(this.buttonRHMdlx);
            this.flpt.Controls.Add(this.buttonRHMset);
            this.flpt.Controls.Add(this.label2);
            this.flpt.Controls.Add(this.numrhj);
            this.flpt.Controls.Add(this.buttonLHMdlx);
            this.flpt.Controls.Add(this.buttonLHMset);
            this.flpt.Controls.Add(this.label1);
            this.flpt.Controls.Add(this.numlhj);
            this.flpt.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpt.Location = new System.Drawing.Point(0, 25);
            this.flpt.Name = "flpt";
            this.flpt.Size = new System.Drawing.Size(617, 60);
            this.flpt.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPresets});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(617, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonMdlx
            // 
            this.buttonMdlx.AutoSize = true;
            this.buttonMdlx.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonMdlx.Image = global::MdlxA2B.Properties.Resources.mdlx;
            this.buttonMdlx.Location = new System.Drawing.Point(3, 3);
            this.buttonMdlx.Name = "buttonMdlx";
            this.buttonMdlx.Size = new System.Drawing.Size(78, 22);
            this.buttonMdlx.TabIndex = 0;
            this.buttonMdlx.Text = "button1";
            this.buttonMdlx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMdlx.UseVisualStyleBackColor = true;
            this.buttonMdlx.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // buttonMset
            // 
            this.buttonMset.AutoSize = true;
            this.buttonMset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonMset.Image = global::MdlxA2B.Properties.Resources.mset;
            this.buttonMset.Location = new System.Drawing.Point(87, 3);
            this.buttonMset.Name = "buttonMset";
            this.buttonMset.Size = new System.Drawing.Size(78, 22);
            this.buttonMset.TabIndex = 1;
            this.buttonMset.Text = "button1";
            this.buttonMset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMset.UseVisualStyleBackColor = true;
            this.buttonMset.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // buttonLHMdlx
            // 
            this.buttonLHMdlx.AutoSize = true;
            this.buttonLHMdlx.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLHMdlx.Image = global::MdlxA2B.Properties.Resources.lhmdlx;
            this.buttonLHMdlx.Location = new System.Drawing.Point(3, 31);
            this.buttonLHMdlx.Name = "buttonLHMdlx";
            this.buttonLHMdlx.Size = new System.Drawing.Size(110, 22);
            this.buttonLHMdlx.TabIndex = 2;
            this.buttonLHMdlx.Text = "button1";
            this.buttonLHMdlx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLHMdlx.UseVisualStyleBackColor = true;
            this.buttonLHMdlx.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // buttonLHMset
            // 
            this.buttonLHMset.AutoSize = true;
            this.buttonLHMset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLHMset.Image = global::MdlxA2B.Properties.Resources.lhmset;
            this.buttonLHMset.Location = new System.Drawing.Point(119, 31);
            this.buttonLHMset.Name = "buttonLHMset";
            this.buttonLHMset.Size = new System.Drawing.Size(110, 22);
            this.buttonLHMset.TabIndex = 3;
            this.buttonLHMset.Text = "button1";
            this.buttonLHMset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLHMset.UseVisualStyleBackColor = true;
            this.buttonLHMset.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // buttonRHMdlx
            // 
            this.buttonRHMdlx.AutoSize = true;
            this.buttonRHMdlx.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRHMdlx.Image = global::MdlxA2B.Properties.Resources.rhmdlx;
            this.buttonRHMdlx.Location = new System.Drawing.Point(171, 3);
            this.buttonRHMdlx.Name = "buttonRHMdlx";
            this.buttonRHMdlx.Size = new System.Drawing.Size(110, 22);
            this.buttonRHMdlx.TabIndex = 4;
            this.buttonRHMdlx.Text = "button1";
            this.buttonRHMdlx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRHMdlx.UseVisualStyleBackColor = true;
            this.buttonRHMdlx.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // buttonRHMset
            // 
            this.buttonRHMset.AutoSize = true;
            this.buttonRHMset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRHMset.Image = global::MdlxA2B.Properties.Resources.rhmset;
            this.buttonRHMset.Location = new System.Drawing.Point(287, 3);
            this.buttonRHMset.Name = "buttonRHMset";
            this.buttonRHMset.Size = new System.Drawing.Size(110, 22);
            this.buttonRHMset.TabIndex = 5;
            this.buttonRHMset.Text = "button1";
            this.buttonRHMset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRHMset.UseVisualStyleBackColor = true;
            this.buttonRHMset.Click += new System.EventHandler(this.buttonMdlx_Click);
            // 
            // bPresets
            // 
            this.bPresets.Image = global::MdlxA2B.Properties.Resources.FillDownHS;
            this.bPresets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPresets.Name = "bPresets";
            this.bPresets.Size = new System.Drawing.Size(80, 22);
            this.bPresets.Text = "Pre&sets";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Left hand joint";
            // 
            // numlhj
            // 
            this.numlhj.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numlhj.Location = new System.Drawing.Point(320, 32);
            this.numlhj.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numlhj.Name = "numlhj";
            this.numlhj.Size = new System.Drawing.Size(51, 19);
            this.numlhj.TabIndex = 7;
            this.numlhj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Right hand joint";
            // 
            // numrhj
            // 
            this.numrhj.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numrhj.Location = new System.Drawing.Point(495, 4);
            this.numrhj.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numrhj.Name = "numrhj";
            this.numrhj.Size = new System.Drawing.Size(51, 19);
            this.numrhj.TabIndex = 9;
            this.numrhj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // YForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 432);
            this.Controls.Add(this.flpt);
            this.Controls.Add(this.toolStrip1);
            this.Name = "YForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MdlxA2B";
            this.Load += new System.EventHandler(this.YForm_Load);
            this.flpt.ResumeLayout(false);
            this.flpt.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numlhj)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numrhj)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpt;
        private System.Windows.Forms.Button buttonMdlx;
        private System.Windows.Forms.Button buttonMset;
        private System.Windows.Forms.Button buttonLHMdlx;
        private System.Windows.Forms.Button buttonLHMset;
        private System.Windows.Forms.Button buttonRHMdlx;
        private System.Windows.Forms.Button buttonRHMset;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton bPresets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numlhj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numrhj;

    }
}