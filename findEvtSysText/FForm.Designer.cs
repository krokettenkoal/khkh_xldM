namespace findEvtSysText {
    partial class FForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbIn = new System.Windows.Forms.ComboBox();
            this.bSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDirin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEVT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSYS = new System.Windows.Forms.TextBox();
            this.bwSearch = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRes = new System.Windows.Forms.TextBox();
            this.levt = new System.Windows.Forms.Label();
            this.lsys = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbOuta = new System.Windows.Forms.TextBox();
            this.bRead = new System.Windows.Forms.Button();
            this.bGenTbl = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "What do you wanna search?";
            // 
            // cbIn
            // 
            this.cbIn.FormattingEnabled = true;
            this.cbIn.Items.AddRange(new object[] {
            "ゼムナスはきっと",
            "っているはずだよ",
            "いつまたバラバラになるかわからないし",
            "このチャンスをムダにはできないよ"});
            this.cbIn.Location = new System.Drawing.Point(12, 24);
            this.cbIn.Name = "cbIn";
            this.cbIn.Size = new System.Drawing.Size(207, 20);
            this.cbIn.TabIndex = 2;
            this.cbIn.Text = "ゼムナス";
            // 
            // bSearch
            // 
            this.bSearch.Location = new System.Drawing.Point(225, 22);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(75, 23);
            this.bSearch.TabIndex = 3;
            this.bSearch.Text = "&Search";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Start find dir:";
            // 
            // tbDirin
            // 
            this.tbDirin.Location = new System.Drawing.Point(12, 62);
            this.tbDirin.Name = "tbDirin";
            this.tbDirin.Size = new System.Drawing.Size(265, 19);
            this.tbDirin.TabIndex = 5;
            this.tbDirin.Text = "H:\\EMU\\Pcsx2-0.9.7\\expa.f266b00b\\msg\\jp";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Table EVT";
            // 
            // tbEVT
            // 
            this.tbEVT.Location = new System.Drawing.Point(12, 99);
            this.tbEVT.Multiline = true;
            this.tbEVT.Name = "tbEVT";
            this.tbEVT.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEVT.Size = new System.Drawing.Size(147, 88);
            this.tbEVT.TabIndex = 7;
            this.tbEVT.Text = resources.GetString("tbEVT.Text");
            this.tbEVT.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Table SYS";
            // 
            // tbSYS
            // 
            this.tbSYS.Location = new System.Drawing.Point(165, 99);
            this.tbSYS.Multiline = true;
            this.tbSYS.Name = "tbSYS";
            this.tbSYS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSYS.Size = new System.Drawing.Size(147, 88);
            this.tbSYS.TabIndex = 7;
            this.tbSYS.Text = resources.GetString("tbSYS.Text");
            this.tbSYS.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Results:";
            // 
            // tbRes
            // 
            this.tbRes.Location = new System.Drawing.Point(12, 262);
            this.tbRes.Multiline = true;
            this.tbRes.Name = "tbRes";
            this.tbRes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbRes.Size = new System.Drawing.Size(379, 134);
            this.tbRes.TabIndex = 9;
            // 
            // levt
            // 
            this.levt.AutoSize = true;
            this.levt.Location = new System.Drawing.Point(12, 190);
            this.levt.Name = "levt";
            this.levt.Size = new System.Drawing.Size(35, 12);
            this.levt.TabIndex = 10;
            this.levt.Text = "label6";
            // 
            // lsys
            // 
            this.lsys.AutoSize = true;
            this.lsys.Location = new System.Drawing.Point(12, 202);
            this.lsys.Name = "lsys";
            this.lsys.Size = new System.Drawing.Size(35, 12);
            this.lsys.TabIndex = 11;
            this.lsys.Text = "label7";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbOuta);
            this.groupBox1.Controls.Add(this.bRead);
            this.groupBox1.Location = new System.Drawing.Point(411, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 421);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "text reader";
            // 
            // tbOuta
            // 
            this.tbOuta.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbOuta.Location = new System.Drawing.Point(6, 47);
            this.tbOuta.Multiline = true;
            this.tbOuta.Name = "tbOuta";
            this.tbOuta.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOuta.Size = new System.Drawing.Size(439, 368);
            this.tbOuta.TabIndex = 1;
            this.tbOuta.WordWrap = false;
            // 
            // bRead
            // 
            this.bRead.Location = new System.Drawing.Point(6, 18);
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(75, 23);
            this.bRead.TabIndex = 0;
            this.bRead.Text = "Read text";
            this.bRead.UseVisualStyleBackColor = true;
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // bGenTbl
            // 
            this.bGenTbl.Location = new System.Drawing.Point(316, 233);
            this.bGenTbl.Name = "bGenTbl";
            this.bGenTbl.Size = new System.Drawing.Size(75, 23);
            this.bGenTbl.TabIndex = 13;
            this.bGenTbl.Text = "&Gen tbl";
            this.bGenTbl.UseVisualStyleBackColor = true;
            this.bGenTbl.Click += new System.EventHandler(this.bGenTbl_Click);
            // 
            // FForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 445);
            this.Controls.Add(this.bGenTbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lsys);
            this.Controls.Add(this.levt);
            this.Controls.Add(this.tbRes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSYS);
            this.Controls.Add(this.tbEVT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDirin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bSearch);
            this.Controls.Add(this.cbIn);
            this.Controls.Add(this.label1);
            this.Name = "FForm";
            this.Text = "Find Evt Sys text";
            this.Load += new System.EventHandler(this.FForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbIn;
        private System.Windows.Forms.Button bSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDirin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEVT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSYS;
        private System.ComponentModel.BackgroundWorker bwSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRes;
        private System.Windows.Forms.Label levt;
        private System.Windows.Forms.Label lsys;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bRead;
        private System.Windows.Forms.TextBox tbOuta;
        private System.Windows.Forms.Button bGenTbl;
    }
}

