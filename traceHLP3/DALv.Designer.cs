namespace traceHLP3 {
    partial class DALv {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DALv));
            this.lv = new System.Windows.Forms.ListView();
            this.cha = new System.Windows.Forms.ColumnHeader();
            this.chda = new System.Windows.Forms.ColumnHeader();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cha,
            this.chda});
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(0, 0);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(350, 150);
            this.lv.SmallImageList = this.il;
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.VirtualMode = true;
            this.lv.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lv_RetrieveVirtualItem);
            this.lv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lv_KeyPress);
            this.lv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lv_KeyUp);
            this.lv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            // 
            // cha
            // 
            this.cha.Text = "addr";
            this.cha.Width = 70;
            // 
            // chda
            // 
            this.chda.Text = "disarm";
            this.chda.Width = 220;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "key.png");
            // 
            // DALv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv);
            this.Name = "DALv";
            this.Size = new System.Drawing.Size(350, 150);
            this.Load += new System.EventHandler(this.DALv_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader cha;
        private System.Windows.Forms.ColumnHeader chda;
        private System.Windows.Forms.ImageList il;
    }
}
