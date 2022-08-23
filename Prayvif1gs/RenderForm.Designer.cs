namespace Prayvif1gs {
    partial class RenderForm {
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
            this.labelYT = new System.Windows.Forms.Label();
            this.labelXR = new System.Windows.Forms.Label();
            this.labelXL = new System.Windows.Forms.Label();
            this.labelYB = new System.Windows.Forms.Label();
            this.labelMM = new System.Windows.Forms.Label();
            this.pbB = new System.Windows.Forms.PictureBox();
            this.pbR = new System.Windows.Forms.PictureBox();
            this.lvI = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.p1 = new hex04BinTrack.UC();
            ((System.ComponentModel.ISupportInitialize)(this.pbB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbR)).BeginInit();
            this.SuspendLayout();
            // 
            // labelYT
            // 
            this.labelYT.AutoSize = true;
            this.labelYT.Location = new System.Drawing.Point(12, 24);
            this.labelYT.Name = "labelYT";
            this.labelYT.Size = new System.Drawing.Size(29, 12);
            this.labelYT.TabIndex = 3;
            this.labelYT.Text = "-123";
            // 
            // labelXR
            // 
            this.labelXR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXR.AutoSize = true;
            this.labelXR.Location = new System.Drawing.Point(712, 9);
            this.labelXR.Name = "labelXR";
            this.labelXR.Size = new System.Drawing.Size(29, 12);
            this.labelXR.TabIndex = 5;
            this.labelXR.Text = "+123";
            // 
            // labelXL
            // 
            this.labelXL.AutoSize = true;
            this.labelXL.Location = new System.Drawing.Point(45, 9);
            this.labelXL.Name = "labelXL";
            this.labelXL.Size = new System.Drawing.Size(29, 12);
            this.labelXL.TabIndex = 2;
            this.labelXL.Text = "-123";
            // 
            // labelYB
            // 
            this.labelYB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelYB.AutoSize = true;
            this.labelYB.Location = new System.Drawing.Point(12, 467);
            this.labelYB.Name = "labelYB";
            this.labelYB.Size = new System.Drawing.Size(29, 12);
            this.labelYB.TabIndex = 4;
            this.labelYB.Text = "+123";
            // 
            // labelMM
            // 
            this.labelMM.AutoSize = true;
            this.labelMM.Location = new System.Drawing.Point(1, 73);
            this.labelMM.Name = "labelMM";
            this.labelMM.Size = new System.Drawing.Size(11, 48);
            this.labelMM.TabIndex = 6;
            this.labelMM.Text = "1\r\n2\r\n3\r\n4";
            // 
            // pbB
            // 
            this.pbB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbB.Location = new System.Drawing.Point(47, 467);
            this.pbB.Name = "pbB";
            this.pbB.Size = new System.Drawing.Size(674, 12);
            this.pbB.TabIndex = 7;
            this.pbB.TabStop = false;
            // 
            // pbR
            // 
            this.pbR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbR.Location = new System.Drawing.Point(727, 24);
            this.pbR.Name = "pbR";
            this.pbR.Size = new System.Drawing.Size(10, 437);
            this.pbR.TabIndex = 8;
            this.pbR.TabStop = false;
            // 
            // lvI
            // 
            this.lvI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvI.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvI.FullRowSelect = true;
            this.lvI.GridLines = true;
            this.lvI.Location = new System.Drawing.Point(743, 24);
            this.lvI.Name = "lvI";
            this.lvI.Size = new System.Drawing.Size(85, 437);
            this.lvI.TabIndex = 9;
            this.lvI.UseCompatibleStateImageBehavior = false;
            this.lvI.View = System.Windows.Forms.View.Details;
            this.lvI.SelectedIndexChanged += new System.EventHandler(this.lvI_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 72;
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1.Location = new System.Drawing.Point(47, 24);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(674, 437);
            this.p1.TabIndex = 0;
            this.p1.UseTransparent = true;
            this.p1.Load += new System.EventHandler(this.p1_Load);
            this.p1.Paint += new System.Windows.Forms.PaintEventHandler(this.p1_Paint);
            this.p1.Resize += new System.EventHandler(this.p1_Resize);
            this.p1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.p1_KeyDown);
            // 
            // RenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 491);
            this.Controls.Add(this.lvI);
            this.Controls.Add(this.pbR);
            this.Controls.Add(this.pbB);
            this.Controls.Add(this.labelMM);
            this.Controls.Add(this.labelYT);
            this.Controls.Add(this.labelXR);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.labelXL);
            this.Controls.Add(this.labelYB);
            this.KeyPreview = true;
            this.Name = "RenderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prayvif1gs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private hex04BinTrack.UC p1;
        private System.Windows.Forms.Label labelYT;
        private System.Windows.Forms.Label labelXR;
        private System.Windows.Forms.Label labelXL;
        private System.Windows.Forms.Label labelYB;
        private System.Windows.Forms.Label labelMM;
        private System.Windows.Forms.PictureBox pbB;
        private System.Windows.Forms.PictureBox pbR;
        private System.Windows.Forms.ListView lvI;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

