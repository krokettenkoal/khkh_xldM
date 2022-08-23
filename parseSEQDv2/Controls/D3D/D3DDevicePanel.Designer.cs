namespace parseSEQDv2.Controls.D3D {
    partial class D3DDevicePanel {
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // D3DDevicePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "D3DDevicePanel";
            this.Load += new System.EventHandler(this.D3DDevicePanel_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.D3DDevicePanel_Paint);
            this.Resize += new System.EventHandler(this.D3DDevicePanel_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
