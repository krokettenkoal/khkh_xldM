namespace traceHLP3 {
    partial class HForm {
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
            this.buttonST = new System.Windows.Forms.Button();
            this.lvCall = new System.Windows.Forms.ListView();
            this.stackAna = new traceHLP3.StackAna();
            this.parserOb = new traceHLP3.ParserHaxkh2fm(this.components);
            this.hv = new Readmset.HexVwer();
            this.regAna = new traceHLP3.REGAna();
            this.daLv = new traceHLP3.DALv();
            this.SuspendLayout();
            // 
            // buttonST
            // 
            this.buttonST.Location = new System.Drawing.Point(593, 275);
            this.buttonST.Name = "buttonST";
            this.buttonST.Size = new System.Drawing.Size(98, 23);
            this.buttonST.TabIndex = 5;
            this.buttonST.Text = "Stack trace";
            this.buttonST.UseVisualStyleBackColor = true;
            this.buttonST.Click += new System.EventHandler(this.buttonST_Click);
            // 
            // lvCall
            // 
            this.lvCall.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lvCall.Location = new System.Drawing.Point(593, 304);
            this.lvCall.Name = "lvCall";
            this.lvCall.Size = new System.Drawing.Size(149, 205);
            this.lvCall.TabIndex = 6;
            this.lvCall.UseCompatibleStateImageBehavior = false;
            this.lvCall.View = System.Windows.Forms.View.List;
            this.lvCall.SelectedIndexChanged += new System.EventHandler(this.lvCall_SelectedIndexChanged);
            // 
            // stackAna
            // 
            this.stackAna.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.stackAna.Location = new System.Drawing.Point(593, 12);
            this.stackAna.Name = "stackAna";
            this.stackAna.ParserOb = this.parserOb;
            this.stackAna.Size = new System.Drawing.Size(170, 257);
            this.stackAna.TabIndex = 3;
            // 
            // parserOb
            // 
            this.parserOb.PC = ((uint)(0u));
            // 
            // hv
            // 
            this.hv.AntiFlick = true;
            this.hv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hv.ByteWidth = 16;
            this.hv.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hv.Location = new System.Drawing.Point(12, 421);
            this.hv.Name = "hv";
            this.hv.OffDelta = 0;
            this.hv.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hv.Size = new System.Drawing.Size(575, 88);
            this.hv.TabIndex = 1;
            this.hv.UnitPg = 512;
            this.hv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hv_KeyPress);
            // 
            // regAna
            // 
            this.regAna.AutoSize = true;
            this.regAna.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.regAna.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.regAna.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.regAna.Location = new System.Drawing.Point(12, 12);
            this.regAna.Name = "regAna";
            this.regAna.ParserOb = this.parserOb;
            this.regAna.Size = new System.Drawing.Size(240, 400);
            this.regAna.TabIndex = 2;
            // 
            // daLv
            // 
            this.daLv.Location = new System.Drawing.Point(258, 12);
            this.daLv.Name = "daLv";
            this.daLv.ParserOb = this.parserOb;
            this.daLv.PC0 = ((uint)(0u));
            this.daLv.PC1 = ((uint)(0u));
            this.daLv.Size = new System.Drawing.Size(329, 400);
            this.daLv.TabIndex = 0;
            this.daLv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.daLv_KeyPress);
            this.daLv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.daLv_KeyDown);
            // 
            // HForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 537);
            this.Controls.Add(this.lvCall);
            this.Controls.Add(this.buttonST);
            this.Controls.Add(this.stackAna);
            this.Controls.Add(this.hv);
            this.Controls.Add(this.regAna);
            this.Controls.Add(this.daLv);
            this.Name = "HForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "traceHLP3";
            this.Load += new System.EventHandler(this.HForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ParserHaxkh2fm parserOb;
        private DALv daLv;
        private REGAna regAna;
        private Readmset.HexVwer hv;
        private StackAna stackAna;
        private System.Windows.Forms.Button buttonST;
        private System.Windows.Forms.ListView lvCall;

    }
}

