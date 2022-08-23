namespace traceHLP3 {
    partial class RACCForm {
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
            this.lvpc = new System.Windows.Forms.ListView();
            this.chpc = new System.Windows.Forms.ColumnHeader();
            this.chcnt = new System.Windows.Forms.ColumnHeader();
            this.lboff = new System.Windows.Forms.ListBox();
            this.hv = new Readmset.HexVwer();
            this.daLv1 = new traceHLP3.DALv();
            this.parserOb = new traceHLP3.ParserHaxkh2fm(this.components);
            this.SuspendLayout();
            // 
            // lvpc
            // 
            this.lvpc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chpc,
            this.chcnt});
            this.lvpc.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvpc.FullRowSelect = true;
            this.lvpc.GridLines = true;
            this.lvpc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvpc.HideSelection = false;
            this.lvpc.Location = new System.Drawing.Point(348, 12);
            this.lvpc.Name = "lvpc";
            this.lvpc.Size = new System.Drawing.Size(132, 205);
            this.lvpc.TabIndex = 1;
            this.lvpc.UseCompatibleStateImageBehavior = false;
            this.lvpc.View = System.Windows.Forms.View.Details;
            this.lvpc.SelectedIndexChanged += new System.EventHandler(this.lvpc_SelectedIndexChanged);
            // 
            // chpc
            // 
            this.chpc.Text = "pc";
            // 
            // chcnt
            // 
            this.chcnt.Text = "cnt";
            this.chcnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chcnt.Width = 40;
            // 
            // lboff
            // 
            this.lboff.ColumnWidth = 60;
            this.lboff.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lboff.FormattingEnabled = true;
            this.lboff.IntegralHeight = false;
            this.lboff.ItemHeight = 12;
            this.lboff.Location = new System.Drawing.Point(486, 12);
            this.lboff.MultiColumn = true;
            this.lboff.Name = "lboff";
            this.lboff.Size = new System.Drawing.Size(129, 205);
            this.lboff.TabIndex = 3;
            this.lboff.SelectedIndexChanged += new System.EventHandler(this.lboff_SelectedIndexChanged);
            // 
            // hv
            // 
            this.hv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.hv.AntiFlick = true;
            this.hv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hv.ByteWidth = 16;
            this.hv.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hv.Location = new System.Drawing.Point(12, 223);
            this.hv.Name = "hv";
            this.hv.OffDelta = 0;
            this.hv.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hv.Size = new System.Drawing.Size(603, 454);
            this.hv.TabIndex = 2;
            this.hv.UnitPg = 512;
            // 
            // daLv1
            // 
            this.daLv1.Location = new System.Drawing.Point(12, 12);
            this.daLv1.Name = "daLv1";
            this.daLv1.ParserOb = this.parserOb;
            this.daLv1.PC0 = ((uint)(0u));
            this.daLv1.PC1 = ((uint)(0u));
            this.daLv1.Size = new System.Drawing.Size(330, 205);
            this.daLv1.TabIndex = 0;
            // 
            // parserOb
            // 
            this.parserOb.PC = ((uint)(0u));
            // 
            // RACCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 689);
            this.Controls.Add(this.lboff);
            this.Controls.Add(this.hv);
            this.Controls.Add(this.lvpc);
            this.Controls.Add(this.daLv1);
            this.Name = "RACCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "traceHLP3 Hax2 _ Read RACC";
            this.Load += new System.EventHandler(this.H2Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ParserHaxkh2fm parserOb;
        private DALv daLv1;
        private System.Windows.Forms.ListView lvpc;
        private System.Windows.Forms.ColumnHeader chpc;
        private Readmset.HexVwer hv;
        private System.Windows.Forms.ListBox lboff;
        private System.Windows.Forms.ColumnHeader chcnt;
    }
}