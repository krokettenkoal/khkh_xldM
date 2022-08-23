namespace StudySwizzle {
    partial class StudyForm {
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
            this.pb8 = new System.Windows.Forms.PictureBox();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.flp8 = new System.Windows.Forms.FlowLayoutPanel();
            this.l8 = new System.Windows.Forms.Label();
            this.flp4 = new System.Windows.Forms.FlowLayoutPanel();
            this.l4 = new System.Windows.Forms.Label();
            this.pb4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb8)).BeginInit();
            this.flp.SuspendLayout();
            this.flp8.SuspendLayout();
            this.flp4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).BeginInit();
            this.SuspendLayout();
            // 
            // pb8
            // 
            this.pb8.Location = new System.Drawing.Point(3, 15);
            this.pb8.Name = "pb8";
            this.pb8.Size = new System.Drawing.Size(100, 50);
            this.pb8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb8.TabIndex = 0;
            this.pb8.TabStop = false;
            // 
            // flp
            // 
            this.flp.AutoSize = true;
            this.flp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp.Controls.Add(this.flp4);
            this.flp.Controls.Add(this.flp8);
            this.flp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp.Location = new System.Drawing.Point(0, 0);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(116, 156);
            this.flp.TabIndex = 1;
            // 
            // flp8
            // 
            this.flp8.AutoSize = true;
            this.flp8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flp8.Controls.Add(this.l8);
            this.flp8.Controls.Add(this.pb8);
            this.flp8.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp8.Location = new System.Drawing.Point(3, 81);
            this.flp8.Name = "flp8";
            this.flp8.Size = new System.Drawing.Size(110, 72);
            this.flp8.TabIndex = 0;
            // 
            // l8
            // 
            this.l8.AutoSize = true;
            this.l8.Location = new System.Drawing.Point(3, 0);
            this.l8.Name = "l8";
            this.l8.Size = new System.Drawing.Size(33, 12);
            this.l8.TabIndex = 0;
            this.l8.Text = "8BPP";
            // 
            // flp4
            // 
            this.flp4.AutoSize = true;
            this.flp4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flp4.Controls.Add(this.l4);
            this.flp4.Controls.Add(this.pb4);
            this.flp4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp4.Location = new System.Drawing.Point(3, 3);
            this.flp4.Name = "flp4";
            this.flp4.Size = new System.Drawing.Size(110, 72);
            this.flp4.TabIndex = 1;
            // 
            // l4
            // 
            this.l4.AutoSize = true;
            this.l4.Location = new System.Drawing.Point(3, 0);
            this.l4.Name = "l4";
            this.l4.Size = new System.Drawing.Size(33, 12);
            this.l4.TabIndex = 0;
            this.l4.Text = "4BPP";
            // 
            // pb4
            // 
            this.pb4.Location = new System.Drawing.Point(3, 15);
            this.pb4.Name = "pb4";
            this.pb4.Size = new System.Drawing.Size(100, 50);
            this.pb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb4.TabIndex = 1;
            this.pb4.TabStop = false;
            // 
            // StudyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(742, 477);
            this.Controls.Add(this.flp);
            this.Name = "StudyForm";
            this.Text = "Study Swizzle";
            this.Load += new System.EventHandler(this.MForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb8)).EndInit();
            this.flp.ResumeLayout(false);
            this.flp.PerformLayout();
            this.flp8.ResumeLayout(false);
            this.flp8.PerformLayout();
            this.flp4.ResumeLayout(false);
            this.flp4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb8;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.FlowLayoutPanel flp8;
        private System.Windows.Forms.Label l8;
        private System.Windows.Forms.FlowLayoutPanel flp4;
        private System.Windows.Forms.Label l4;
        private System.Windows.Forms.PictureBox pb4;
    }
}

