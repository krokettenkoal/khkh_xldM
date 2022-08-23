namespace traceHLP {
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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("ノード3");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("ノード4");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("ノード1", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("ノード2");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("ノード0", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.openFileDialogTextin = new System.Windows.Forms.OpenFileDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBoxFE = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialogTextin
            // 
            this.openFileDialogTextin.DefaultExt = "txt";
            this.openFileDialogTextin.FileName = "H:\\Proj\\khkh_xldM\\MEMO\\outHLP\\00000136.0816.txt";
            this.openFileDialogTextin.Filter = "*.txt|*.txt||";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode11.Name = "ノード3";
            treeNode11.Text = "ノード3";
            treeNode12.Name = "ノード4";
            treeNode12.Text = "ノード4";
            treeNode13.Name = "ノード1";
            treeNode13.Text = "ノード1";
            treeNode14.Name = "ノード2";
            treeNode14.Text = "ノード2";
            treeNode15.Name = "ノード0";
            treeNode15.Text = "ノード0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15});
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(645, 591);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPath.Location = new System.Drawing.Point(12, 609);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(645, 12);
            this.textBoxPath.TabIndex = 1;
            // 
            // textBoxFE
            // 
            this.textBoxFE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFE.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxFE.Location = new System.Drawing.Point(12, 627);
            this.textBoxFE.Multiline = true;
            this.textBoxFE.Name = "textBoxFE";
            this.textBoxFE.Size = new System.Drawing.Size(112, 24);
            this.textBoxFE.TabIndex = 2;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 663);
            this.Controls.Add(this.textBoxFE);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Trace HLP";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogTextin;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBoxFE;
    }
}

