namespace vwmmap {
    partial class MForm {
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
            this.flptop = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbfpt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbexpadir = new System.Windows.Forms.TextBox();
            this.flpq = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbq = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.flpans = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.flptop.SuspendLayout();
            this.flpq.SuspendLayout();
            this.flpans.SuspendLayout();
            this.SuspendLayout();
            // 
            // flptop
            // 
            this.flptop.AutoSize = true;
            this.flptop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flptop.Controls.Add(this.label1);
            this.flptop.Controls.Add(this.tbfpt);
            this.flptop.Controls.Add(this.label2);
            this.flptop.Controls.Add(this.tbRex);
            this.flptop.Controls.Add(this.label3);
            this.flptop.Controls.Add(this.tbexpadir);
            this.flptop.Dock = System.Windows.Forms.DockStyle.Top;
            this.flptop.Location = new System.Drawing.Point(0, 0);
            this.flptop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flptop.Name = "flptop";
            this.flptop.Size = new System.Drawing.Size(501, 93);
            this.flptop.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "text";
            // 
            // tbfpt
            // 
            this.flptop.SetFlowBreak(this.tbfpt, true);
            this.tbfpt.Location = new System.Drawing.Point(40, 4);
            this.tbfpt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbfpt.Name = "tbfpt";
            this.tbfpt.Size = new System.Drawing.Size(439, 23);
            this.tbfpt.TabIndex = 1;
            this.tbfpt.Text = "H:\\Proj\\khkh_xldM\\MEMO\\pax_\\sstate0.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "regex";
            // 
            // tbRex
            // 
            this.flptop.SetFlowBreak(this.tbRex, true);
            this.tbRex.Location = new System.Drawing.Point(49, 35);
            this.tbRex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbRex.Name = "tbRex";
            this.tbRex.Size = new System.Drawing.Size(318, 23);
            this.tbRex.TabIndex = 3;
            this.tbRex.Text = "^S_IEXPA: (?<a>[0-9a-f]+)\\s+(?<fn>.+)$";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "expa dir";
            // 
            // tbexpadir
            // 
            this.tbexpadir.Location = new System.Drawing.Point(63, 66);
            this.tbexpadir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbexpadir.Name = "tbexpadir";
            this.tbexpadir.Size = new System.Drawing.Size(273, 23);
            this.tbexpadir.TabIndex = 5;
            this.tbexpadir.Text = "H:\\EMU\\Pcsx2-0.9.6\\expa";
            // 
            // flpq
            // 
            this.flpq.AutoSize = true;
            this.flpq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpq.Controls.Add(this.label4);
            this.flpq.Controls.Add(this.tbq);
            this.flpq.Controls.Add(this.button1);
            this.flpq.Controls.Add(this.button2);
            this.flpq.Controls.Add(this.label5);
            this.flpq.Controls.Add(this.flpans);
            this.flpq.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpq.Location = new System.Drawing.Point(0, 93);
            this.flpq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpq.Name = "flpq";
            this.flpq.Size = new System.Drawing.Size(501, 51);
            this.flpq.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "&query";
            // 
            // tbq
            // 
            this.tbq.Location = new System.Drawing.Point(49, 4);
            this.tbq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbq.Name = "tbq";
            this.tbq.Size = new System.Drawing.Size(151, 23);
            this.tbq.TabIndex = 1;
            this.tbq.Enter += new System.EventHandler(this.tbq_Enter);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Location = new System.Drawing.Point(206, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Answer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "answer";
            // 
            // flpans
            // 
            this.flpans.AutoSize = true;
            this.flpans.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpans.Controls.Add(this.label6);
            this.flpans.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.flpans.Location = new System.Drawing.Point(58, 36);
            this.flpans.Name = "flpans";
            this.flpans.Size = new System.Drawing.Size(47, 12);
            this.flpans.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "label6";
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpq.SetFlowBreak(this.button2, true);
            this.button2.Location = new System.Drawing.Point(272, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Then &paste";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 273);
            this.Controls.Add(this.flpq);
            this.Controls.Add(this.flptop);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "vwmmap";
            this.flptop.ResumeLayout(false);
            this.flptop.PerformLayout();
            this.flpq.ResumeLayout(false);
            this.flpq.PerformLayout();
            this.flpans.ResumeLayout(false);
            this.flpans.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flptop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbfpt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbexpadir;
        private System.Windows.Forms.FlowLayoutPanel flpq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbq;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flpans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}

