namespace khiiMapv {
    partial class BEXForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BEXForm));
            this.bSelOut = new System.Windows.Forms.Button();
            this.tbOutDir = new System.Windows.Forms.TextBox();
            this.fbdOut = new System.Windows.Forms.FolderBrowserDialog();
            this.bAddfp = new System.Windows.Forms.Button();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.lvfp = new System.Windows.Forms.ListView();
            this.bExp = new System.Windows.Forms.Button();
            this.ofdfp = new System.Windows.Forms.OpenFileDialog();
            this.bOpenOut = new System.Windows.Forms.Button();
            this.lCur = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bSelOut
            // 
            this.bSelOut.AutoSize = true;
            this.bSelOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bSelOut.Location = new System.Drawing.Point(12, 12);
            this.bSelOut.Name = "bSelOut";
            this.bSelOut.Size = new System.Drawing.Size(120, 22);
            this.bSelOut.TabIndex = 1;
            this.bSelOut.Text = "Select Output folder:";
            this.bSelOut.UseVisualStyleBackColor = true;
            this.bSelOut.Click += new System.EventHandler(this.bSelOut_Click);
            // 
            // tbOutDir
            // 
            this.tbOutDir.Location = new System.Drawing.Point(138, 14);
            this.tbOutDir.Name = "tbOutDir";
            this.tbOutDir.Size = new System.Drawing.Size(442, 19);
            this.tbOutDir.TabIndex = 2;
            // 
            // fbdOut
            // 
            this.fbdOut.Description = "Select an Output folder:";
            // 
            // bAddfp
            // 
            this.bAddfp.AllowDrop = true;
            this.bAddfp.AutoSize = true;
            this.bAddfp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bAddfp.ImageIndex = 0;
            this.bAddfp.ImageList = this.il;
            this.bAddfp.Location = new System.Drawing.Point(12, 40);
            this.bAddfp.Name = "bAddfp";
            this.bAddfp.Size = new System.Drawing.Size(95, 38);
            this.bAddfp.TabIndex = 3;
            this.bAddfp.Text = "Add files:";
            this.bAddfp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bAddfp.UseVisualStyleBackColor = true;
            this.bAddfp.Click += new System.EventHandler(this.bAddfp_Click);
            this.bAddfp.DragDrop += new System.Windows.Forms.DragEventHandler(this.bAddfp_DragDrop);
            this.bAddfp.DragEnter += new System.Windows.Forms.DragEventHandler(this.bAddfp_DragEnter);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "DROP1PG.ICO");
            // 
            // lvfp
            // 
            this.lvfp.AllowDrop = true;
            this.lvfp.LargeImageList = this.il;
            this.lvfp.Location = new System.Drawing.Point(12, 84);
            this.lvfp.Name = "lvfp";
            this.lvfp.Size = new System.Drawing.Size(568, 234);
            this.lvfp.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvfp.TabIndex = 4;
            this.lvfp.UseCompatibleStateImageBehavior = false;
            this.lvfp.DragDrop += new System.Windows.Forms.DragEventHandler(this.bAddfp_DragDrop);
            this.lvfp.DragEnter += new System.Windows.Forms.DragEventHandler(this.bAddfp_DragEnter);
            this.lvfp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvfp_KeyDown);
            // 
            // bExp
            // 
            this.bExp.AutoSize = true;
            this.bExp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bExp.Image = global::khiiMapv.Properties.Resources.ROCKET;
            this.bExp.Location = new System.Drawing.Point(505, 40);
            this.bExp.Name = "bExp";
            this.bExp.Size = new System.Drawing.Size(75, 38);
            this.bExp.TabIndex = 6;
            this.bExp.Text = "Export now!";
            this.bExp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bExp.UseVisualStyleBackColor = true;
            this.bExp.Click += new System.EventHandler(this.bExp_Click);
            // 
            // ofdfp
            // 
            this.ofdfp.Filter = "KH2 files|*.map;*.mdlx;*.apdx;*.fm;*.2dd;*.bar;*.2ld;*.mset;*.pax;*.wd;*.vsb;*.ar" +
                "d;*.imd;*.mag";
            this.ofdfp.Multiselect = true;
            this.ofdfp.ReadOnlyChecked = true;
            // 
            // bOpenOut
            // 
            this.bOpenOut.AutoSize = true;
            this.bOpenOut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bOpenOut.Image = global::khiiMapv.Properties.Resources.search4files;
            this.bOpenOut.Location = new System.Drawing.Point(138, 39);
            this.bOpenOut.Name = "bOpenOut";
            this.bOpenOut.Size = new System.Drawing.Size(38, 38);
            this.bOpenOut.TabIndex = 5;
            this.bOpenOut.UseVisualStyleBackColor = true;
            this.bOpenOut.Click += new System.EventHandler(this.bOpenOut_Click);
            // 
            // lCur
            // 
            this.lCur.AutoSize = true;
            this.lCur.Location = new System.Drawing.Point(182, 52);
            this.lCur.Name = "lCur";
            this.lCur.Size = new System.Drawing.Size(11, 12);
            this.lCur.TabIndex = 0;
            this.lCur.Text = "...";
            // 
            // BEXForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 339);
            this.Controls.Add(this.lCur);
            this.Controls.Add(this.bOpenOut);
            this.Controls.Add(this.bExp);
            this.Controls.Add(this.lvfp);
            this.Controls.Add(this.bAddfp);
            this.Controls.Add(this.tbOutDir);
            this.Controls.Add(this.bSelOut);
            this.Name = "BEXForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Export";
            this.Load += new System.EventHandler(this.BEXForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSelOut;
        private System.Windows.Forms.TextBox tbOutDir;
        private System.Windows.Forms.FolderBrowserDialog fbdOut;
        private System.Windows.Forms.Button bAddfp;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.ListView lvfp;
        private System.Windows.Forms.Button bExp;
        private System.Windows.Forms.OpenFileDialog ofdfp;
        private System.Windows.Forms.Button bOpenOut;
        private System.Windows.Forms.Label lCur;
    }
}