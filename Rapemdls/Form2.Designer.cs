namespace Rapemdls {
    partial class Form2 {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxtr = new System.Windows.Forms.ListBox();
            this.openFileDialogMset = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogtr5 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxst = new System.Windows.Forms.ListBox();
            this.buttonExplode = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonExplodeU8 = new System.Windows.Forms.Button();
            this.buttonSpecu1 = new System.Windows.Forms.Button();
            this.hexVwer1 = new Readmset.HexVwer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "hex bin mset";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(527, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "memory access";
            // 
            // listBoxtr
            // 
            this.listBoxtr.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBoxtr.FormattingEnabled = true;
            this.listBoxtr.IntegralHeight = false;
            this.listBoxtr.ItemHeight = 12;
            this.listBoxtr.Location = new System.Drawing.Point(529, 24);
            this.listBoxtr.Name = "listBoxtr";
            this.listBoxtr.Size = new System.Drawing.Size(141, 235);
            this.listBoxtr.TabIndex = 3;
            this.listBoxtr.SelectedIndexChanged += new System.EventHandler(this.listBoxtr_SelectedIndexChanged);
            // 
            // openFileDialogMset
            // 
            this.openFileDialogMset.FileName = "V:\\KH1.yaz0r\\xa_ex_0010.mset";
            this.openFileDialogMset.Filter = "*.mset|*.mset||";
            // 
            // openFileDialogtr5
            // 
            this.openFileDialogtr5.FileName = "H:\\Proj\\khkh_xldM\\MEMO\\OUTPUT016.txt";
            this.openFileDialogtr5.Filter = "*.txt|*.txt||";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "structure";
            // 
            // listBoxst
            // 
            this.listBoxst.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBoxst.FormattingEnabled = true;
            this.listBoxst.IntegralHeight = false;
            this.listBoxst.ItemHeight = 12;
            this.listBoxst.Location = new System.Drawing.Point(529, 277);
            this.listBoxst.Name = "listBoxst";
            this.listBoxst.Size = new System.Drawing.Size(161, 187);
            this.listBoxst.TabIndex = 5;
            this.listBoxst.SelectedIndexChanged += new System.EventHandler(this.listBoxst_SelectedIndexChanged);
            // 
            // buttonExplode
            // 
            this.buttonExplode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExplode.Location = new System.Drawing.Point(696, 277);
            this.buttonExplode.Name = "buttonExplode";
            this.buttonExplode.Size = new System.Drawing.Size(75, 23);
            this.buttonExplode.TabIndex = 6;
            this.buttonExplode.Text = "explode";
            this.buttonExplode.UseVisualStyleBackColor = true;
            this.buttonExplode.Click += new System.EventHandler(this.buttonExplode_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(89, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // buttonExplodeU8
            // 
            this.buttonExplodeU8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExplodeU8.Location = new System.Drawing.Point(696, 306);
            this.buttonExplodeU8.Name = "buttonExplodeU8";
            this.buttonExplodeU8.Size = new System.Drawing.Size(75, 23);
            this.buttonExplodeU8.TabIndex = 8;
            this.buttonExplodeU8.Text = "expose U8";
            this.buttonExplodeU8.UseVisualStyleBackColor = true;
            this.buttonExplodeU8.Click += new System.EventHandler(this.buttonExplodeU8_Click);
            // 
            // buttonSpecu1
            // 
            this.buttonSpecu1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSpecu1.Location = new System.Drawing.Point(696, 335);
            this.buttonSpecu1.Name = "buttonSpecu1";
            this.buttonSpecu1.Size = new System.Drawing.Size(75, 23);
            this.buttonSpecu1.TabIndex = 9;
            this.buttonSpecu1.Text = "Specu.1";
            this.buttonSpecu1.UseVisualStyleBackColor = true;
            this.buttonSpecu1.Click += new System.EventHandler(this.buttonSpecu1_Click);
            // 
            // hexVwer1
            // 
            this.hexVwer1.AntiFlick = true;
            this.hexVwer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexVwer1.ByteWidth = 16;
            this.hexVwer1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hexVwer1.Location = new System.Drawing.Point(12, 24);
            this.hexVwer1.Name = "hexVwer1";
            this.hexVwer1.OffDelta = 0;
            this.hexVwer1.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hexVwer1.Size = new System.Drawing.Size(511, 440);
            this.hexVwer1.TabIndex = 0;
            this.hexVwer1.UnitPg = 512;
            this.hexVwer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexVwer1_KeyPress);
            this.hexVwer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hexVwer1_KeyDown);
            // 
            // Form2
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 487);
            this.Controls.Add(this.buttonSpecu1);
            this.Controls.Add(this.buttonExplodeU8);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.buttonExplode);
            this.Controls.Add(this.listBoxst);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxtr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hexVwer1);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Motion set v1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form2_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form2_DragEnter);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Readmset.HexVwer hexVwer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxtr;
        private System.Windows.Forms.OpenFileDialog openFileDialogMset;
        private System.Windows.Forms.OpenFileDialog openFileDialogtr5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxst;
        private System.Windows.Forms.Button buttonExplode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonExplodeU8;
        private System.Windows.Forms.Button buttonSpecu1;
    }
}