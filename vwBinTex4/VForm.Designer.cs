namespace vwBinTex4 {
    partial class VForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lfp = new System.Windows.Forms.LinkLabel();
            this.flppic = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filepath ::";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lfp);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(744, 12);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // lfp
            // 
            this.lfp.AutoSize = true;
            this.lfp.Location = new System.Drawing.Point(63, 0);
            this.lfp.Name = "lfp";
            this.lfp.Size = new System.Drawing.Size(56, 12);
            this.lfp.TabIndex = 1;
            this.lfp.TabStop = true;
            this.lfp.Text = "linkLabel1";
            // 
            // flppic
            // 
            this.flppic.AutoScroll = true;
            this.flppic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flppic.Location = new System.Drawing.Point(0, 12);
            this.flppic.Name = "flppic";
            this.flppic.Size = new System.Drawing.Size(744, 518);
            this.flppic.TabIndex = 2;
            // 
            // VForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 530);
            this.Controls.Add(this.flppic);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "VForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "vwBinTex4";
            this.Load += new System.EventHandler(this.VForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.VForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.VForm_DragEnter);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel lfp;
        private System.Windows.Forms.FlowLayoutPanel flppic;
    }
}

