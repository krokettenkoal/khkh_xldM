namespace parseSEQDv2.Controls {
    partial class VisibleOfFramesControl {
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
            // VisibleOfFramesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "VisibleOfFramesControl";
            this.Load += new System.EventHandler(this.VisibleOfFramesControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VisibleOfFramesControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VisibleOfFramesControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VisibleOfFramesControl_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
