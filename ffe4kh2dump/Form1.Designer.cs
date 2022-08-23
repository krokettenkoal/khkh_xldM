namespace ffe4kh2dump {
    partial class SelForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDrv = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type file names which you want to extract:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 24);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(427, 326);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(14, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Extract now";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "kh2 media in which DVD ROM drive?";
            // 
            // comboBoxDrv
            // 
            this.comboBoxDrv.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ffe4kh2dump.Properties.Settings.Default, "Drive", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBoxDrv.FormattingEnabled = true;
            this.comboBoxDrv.Location = new System.Drawing.Point(178, 370);
            this.comboBoxDrv.Name = "comboBoxDrv";
            this.comboBoxDrv.Size = new System.Drawing.Size(63, 20);
            this.comboBoxDrv.TabIndex = 4;
            this.comboBoxDrv.Text = global::ffe4kh2dump.Properties.Settings.Default.Drive;
            this.comboBoxDrv.SelectedIndexChanged += new System.EventHandler(this.comboBoxDrv_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 393);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.label3.Size = new System.Drawing.Size(368, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "* You need kh2dump.exe extracted at same folder which this tool runs.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 409);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.label4.Size = new System.Drawing.Size(392, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "* All thanks to yaz0r for his talented and great tools: dumpers and viewers!";
            // 
            // SelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(453, 447);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxDrv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Name = "SelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "free file extraction for kh2dump";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelForm_FormClosing);
            this.Load += new System.EventHandler(this.SelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDrv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

