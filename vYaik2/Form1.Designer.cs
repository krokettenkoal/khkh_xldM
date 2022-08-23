namespace vYaik2 {
    partial class Form1 {
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
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.numericUpDownCntik = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCntik)).BeginInit();
            this.SuspendLayout();
            // 
            // pb1
            // 
            this.pb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb1.Location = new System.Drawing.Point(12, 37);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(500, 500);
            this.pb1.TabIndex = 0;
            this.pb1.TabStop = false;
            this.pb1.Click += new System.EventHandler(this.pb1_Click);
            this.pb1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb1_MouseDown);
            this.pb1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pb1_MouseMove);
            this.pb1.Paint += new System.Windows.Forms.PaintEventHandler(this.pb1_Paint);
            // 
            // numericUpDownCntik
            // 
            this.numericUpDownCntik.Location = new System.Drawing.Point(12, 12);
            this.numericUpDownCntik.Name = "numericUpDownCntik";
            this.numericUpDownCntik.Size = new System.Drawing.Size(68, 19);
            this.numericUpDownCntik.TabIndex = 1;
            this.numericUpDownCntik.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 561);
            this.Controls.Add(this.numericUpDownCntik);
            this.Controls.Add(this.pb1);
            this.Name = "Form1";
            this.Text = "vYaik2";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCntik)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.NumericUpDown numericUpDownCntik;
    }
}

