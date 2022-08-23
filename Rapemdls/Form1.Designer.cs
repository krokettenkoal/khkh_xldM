namespace Rapemdls {
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
            this.openFileDialogMdls = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelV = new System.Windows.Forms.Label();
            this.buttonThreed = new System.Windows.Forms.Button();
            this.buttonTex = new System.Windows.Forms.Button();
            this.hexVwer1 = new Readmset.HexVwer();
            this.buttonMset = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogMdls
            // 
            this.openFileDialogMdls.FileName = "V:\\KH1.yaz0r\\xa_ex_0010.mdls";
            this.openFileDialogMdls.Filter = "*.mdls|*.mdls||";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hex dumper";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(527, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "root vone";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(529, 24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(406, 105);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "root";
            this.columnHeader1.Width = 373;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "S4";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(529, 147);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(145, 169);
            this.listBox1.TabIndex = 6;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelV);
            this.panel1.Location = new System.Drawing.Point(680, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 96);
            this.panel1.TabIndex = 7;
            // 
            // labelV
            // 
            this.labelV.AutoSize = true;
            this.labelV.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelV.Location = new System.Drawing.Point(3, 0);
            this.labelV.Name = "labelV";
            this.labelV.Size = new System.Drawing.Size(23, 12);
            this.labelV.TabIndex = 0;
            this.labelV.Text = "...";
            // 
            // buttonThreed
            // 
            this.buttonThreed.Location = new System.Drawing.Point(860, 249);
            this.buttonThreed.Name = "buttonThreed";
            this.buttonThreed.Size = new System.Drawing.Size(75, 23);
            this.buttonThreed.TabIndex = 8;
            this.buttonThreed.Text = "&3D";
            this.buttonThreed.UseVisualStyleBackColor = true;
            this.buttonThreed.Click += new System.EventHandler(this.buttonThreed_Click);
            // 
            // buttonTex
            // 
            this.buttonTex.Location = new System.Drawing.Point(779, 249);
            this.buttonTex.Name = "buttonTex";
            this.buttonTex.Size = new System.Drawing.Size(75, 23);
            this.buttonTex.TabIndex = 9;
            this.buttonTex.Text = "&Tex.";
            this.buttonTex.UseVisualStyleBackColor = true;
            this.buttonTex.Click += new System.EventHandler(this.buttonTex_Click);
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
            // 
            // buttonMset
            // 
            this.buttonMset.Location = new System.Drawing.Point(698, 249);
            this.buttonMset.Name = "buttonMset";
            this.buttonMset.Size = new System.Drawing.Size(75, 23);
            this.buttonMset.TabIndex = 10;
            this.buttonMset.Text = "&Monition";
            this.buttonMset.UseVisualStyleBackColor = true;
            this.buttonMset.Click += new System.EventHandler(this.buttonMset_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(84, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 484);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.buttonMset);
            this.Controls.Add(this.buttonTex);
            this.Controls.Add(this.buttonThreed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hexVwer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rage Awakened";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogMdls;
        private Readmset.HexVwer hexVwer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelV;
        private System.Windows.Forms.Button buttonThreed;
        private System.Windows.Forms.Button buttonTex;
        private System.Windows.Forms.Button buttonMset;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

