namespace hex04BinTrack {
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialogBin = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogText = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxVIF = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonMake3D = new System.Windows.Forms.Button();
            this.linkLabelFn = new System.Windows.Forms.LinkLabel();
            this.loaderPane = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.loaderPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(11, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(508, 425);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialogBin
            // 
            this.openFileDialogBin.FileName = "H:\\Proj\\khkh_xldM\\bin\\Debug\\bin\\P_EX110.04.p_ex.bin";
            this.openFileDialogBin.Filter = "*.bin|*.bin||";
            // 
            // openFileDialogText
            // 
            this.openFileDialogText.FileName = "H:\\Proj\\khkh_xldM\\MEMO\\OUTPUT009.txt";
            this.openFileDialogText.Filter = "*.txt|*.txt||";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(370, 452);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(11, 467);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(177, 106);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 467);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 106);
            this.textBox1.TabIndex = 3;
            this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            this.textBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseMove);
            // 
            // textBoxVIF
            // 
            this.textBoxVIF.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxVIF.Location = new System.Drawing.Point(525, 24);
            this.textBoxVIF.Multiline = true;
            this.textBoxVIF.Name = "textBoxVIF";
            this.textBoxVIF.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxVIF.Size = new System.Drawing.Size(491, 353);
            this.textBoxVIF.TabIndex = 4;
            this.textBoxVIF.WordWrap = false;
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.IntegralHeight = false;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(248, 467);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(165, 106);
            this.listBox2.TabIndex = 5;
            this.listBox2.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "memory read accesses";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 452);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "keypad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 452);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "table analyzed";
            // 
            // listBox3
            // 
            this.listBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.IntegralHeight = false;
            this.listBox3.ItemHeight = 12;
            this.listBox3.Location = new System.Drawing.Point(419, 467);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(100, 106);
            this.listBox3.TabIndex = 9;
            this.listBox3.DoubleClick += new System.EventHandler(this.listBox3_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(417, 452);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "VIF1 tbl analyze";
            // 
            // listBox4
            // 
            this.listBox4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox4.FormattingEnabled = true;
            this.listBox4.IntegralHeight = false;
            this.listBox4.ItemHeight = 12;
            this.listBox4.Location = new System.Drawing.Point(525, 467);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(632, 106);
            this.listBox4.TabIndex = 11;
            this.listBox4.SelectedIndexChanged += new System.EventHandler(this.listBox4_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(523, 452);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "T2 tbl vals";
            // 
            // buttonMake3D
            // 
            this.buttonMake3D.Location = new System.Drawing.Point(525, 383);
            this.buttonMake3D.Name = "buttonMake3D";
            this.buttonMake3D.Size = new System.Drawing.Size(41, 66);
            this.buttonMake3D.TabIndex = 13;
            this.buttonMake3D.Text = "&3D View";
            this.buttonMake3D.UseVisualStyleBackColor = true;
            this.buttonMake3D.Click += new System.EventHandler(this.buttonMake3D_Click);
            // 
            // linkLabelFn
            // 
            this.linkLabelFn.AutoSize = true;
            this.linkLabelFn.Location = new System.Drawing.Point(12, 9);
            this.linkLabelFn.Name = "linkLabelFn";
            this.linkLabelFn.Size = new System.Drawing.Size(56, 12);
            this.linkLabelFn.TabIndex = 14;
            this.linkLabelFn.TabStop = true;
            this.linkLabelFn.Text = "linkLabel1";
            // 
            // loaderPane
            // 
            this.loaderPane.AutoSize = false;
            this.loaderPane.Dock = System.Windows.Forms.DockStyle.None;
            this.loaderPane.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.loaderPane.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.loaderPane.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.loaderPane.Location = new System.Drawing.Point(569, 383);
            this.loaderPane.Name = "loaderPane";
            this.loaderPane.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.loaderPane.Size = new System.Drawing.Size(447, 66);
            this.loaderPane.TabIndex = 17;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 15);
            this.toolStripLabel1.Text = "Load:";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 585);
            this.Controls.Add(this.loaderPane);
            this.Controls.Add(this.linkLabelFn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonMake3D);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBoxVIF);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "hex04BinTracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.loaderPane.ResumeLayout(false);
            this.loaderPane.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialogBin;
        private System.Windows.Forms.OpenFileDialog openFileDialogText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxVIF;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonMake3D;
        private System.Windows.Forms.LinkLabel linkLabelFn;
        private System.Windows.Forms.ToolStrip loaderPane;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}

