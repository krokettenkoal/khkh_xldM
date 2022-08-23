namespace traceHLP3 {
    partial class StackAna {
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
            this.lv = new System.Windows.Forms.ListView();
            this.cha = new System.Windows.Forms.ColumnHeader();
            this.chv = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cha,
            this.chv});
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(170, 350);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.VirtualListSize = 128;
            this.lv.VirtualMode = true;
            this.lv.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lv_RetrieveVirtualItem);
            // 
            // cha
            // 
            this.cha.Text = "addr";
            // 
            // chv
            // 
            this.chv.Text = "value";
            this.chv.Width = 80;
            // 
            // StackAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.Name = "StackAna";
            this.Size = new System.Drawing.Size(170, 350);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader cha;
        private System.Windows.Forms.ColumnHeader chv;
    }
}
