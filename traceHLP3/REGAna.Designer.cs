namespace traceHLP3 {
    partial class REGAna {
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 396);
            this.label1.TabIndex = 0;
            this.label1.Text = "r0:00000000 00000000 00000000 00000000\r\nat\r\nv0\r\nv1\r\na0\r\na1\r\na2\r\na3\r\nt0\r\nt1\r\nt2\r\nt" +
                "3\r\nt4\r\nt5\r\nt6\r\nt7\r\ns0\r\ns1\r\ns2\r\ns3\r\ns4\r\ns5\r\ns6\r\ns7\r\nt8\r\nt9\r\nk0\r\nk1\r\ngp\r\nsp\r\ns8\r\nr" +
                "a\r\npc";
            // 
            // REGAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.Name = "REGAna";
            this.Size = new System.Drawing.Size(236, 396);
            this.Load += new System.EventHandler(this.REGAna_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}
