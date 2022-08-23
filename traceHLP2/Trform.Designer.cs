namespace traceHLP2 {
    partial class Trform {
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
            this.lvMod = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.lvDA = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.flpLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.labelLStartAddr = new System.Windows.Forms.Label();
            this.labelLExitAddr = new System.Windows.Forms.Label();
            this.labelLTermination = new System.Windows.Forms.Label();
            this.labelLAlreadyWalked = new System.Windows.Forms.Label();
            this.labelLUnkys = new System.Windows.Forms.Label();
            this.labelLUnks = new System.Windows.Forms.Label();
            this.lvTopics = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.flphvMark = new System.Windows.Forms.FlowLayoutPanel();
            this.labelMWritten = new System.Windows.Forms.Label();
            this.lvVifacc = new System.Windows.Forms.ListView();
            this.columnHeaderVtAddr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVtDataAddr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVtTy = new System.Windows.Forms.ColumnHeader();
            this.hvee = new Readmset.HexVwer();
            this.hvsp = new Readmset.HexVwer();
            this.flpLegend.SuspendLayout();
            this.flphvMark.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMod
            // 
            this.lvMod.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvMod.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvMod.FullRowSelect = true;
            this.lvMod.GridLines = true;
            this.lvMod.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMod.HideSelection = false;
            this.lvMod.Location = new System.Drawing.Point(12, 12);
            this.lvMod.Name = "lvMod";
            this.lvMod.Size = new System.Drawing.Size(145, 298);
            this.lvMod.TabIndex = 0;
            this.lvMod.UseCompatibleStateImageBehavior = false;
            this.lvMod.View = System.Windows.Forms.View.Details;
            this.lvMod.SelectedIndexChanged += new System.EventHandler(this.lvMod_SelectedIndexChanged);
            this.lvMod.DoubleClick += new System.EventHandler(this.lvMod_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 120;
            // 
            // lvDA
            // 
            this.lvDA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lvDA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvDA.FullRowSelect = true;
            this.lvDA.GridLines = true;
            this.lvDA.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDA.HideSelection = false;
            this.lvDA.Location = new System.Drawing.Point(163, 12);
            this.lvDA.MultiSelect = false;
            this.lvDA.Name = "lvDA";
            this.lvDA.Size = new System.Drawing.Size(398, 298);
            this.lvDA.TabIndex = 1;
            this.lvDA.UseCompatibleStateImageBehavior = false;
            this.lvDA.View = System.Windows.Forms.View.Details;
            this.lvDA.VirtualMode = true;
            this.lvDA.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvDA_RetrieveVirtualItem);
            this.lvDA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvDA_KeyPress);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 250;
            // 
            // flpLegend
            // 
            this.flpLegend.Controls.Add(this.labelLStartAddr);
            this.flpLegend.Controls.Add(this.labelLExitAddr);
            this.flpLegend.Controls.Add(this.labelLTermination);
            this.flpLegend.Controls.Add(this.labelLAlreadyWalked);
            this.flpLegend.Controls.Add(this.labelLUnkys);
            this.flpLegend.Controls.Add(this.labelLUnks);
            this.flpLegend.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLegend.Location = new System.Drawing.Point(567, 12);
            this.flpLegend.Name = "flpLegend";
            this.flpLegend.Size = new System.Drawing.Size(166, 76);
            this.flpLegend.TabIndex = 2;
            // 
            // labelLStartAddr
            // 
            this.labelLStartAddr.AutoSize = true;
            this.labelLStartAddr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelLStartAddr.Location = new System.Drawing.Point(3, 0);
            this.labelLStartAddr.Name = "labelLStartAddr";
            this.labelLStartAddr.Size = new System.Drawing.Size(54, 12);
            this.labelLStartAddr.TabIndex = 0;
            this.labelLStartAddr.Text = "HLP start";
            // 
            // labelLExitAddr
            // 
            this.labelLExitAddr.AutoSize = true;
            this.labelLExitAddr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.labelLExitAddr.Location = new System.Drawing.Point(3, 12);
            this.labelLExitAddr.Name = "labelLExitAddr";
            this.labelLExitAddr.Size = new System.Drawing.Size(55, 12);
            this.labelLExitAddr.TabIndex = 4;
            this.labelLExitAddr.Text = "HLP exits";
            // 
            // labelLTermination
            // 
            this.labelLTermination.AutoSize = true;
            this.labelLTermination.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.labelLTermination.Location = new System.Drawing.Point(3, 24);
            this.labelLTermination.Name = "labelLTermination";
            this.labelLTermination.Size = new System.Drawing.Size(42, 12);
            this.labelLTermination.TabIndex = 1;
            this.labelLTermination.Text = "jr Term";
            // 
            // labelLAlreadyWalked
            // 
            this.labelLAlreadyWalked.AutoSize = true;
            this.labelLAlreadyWalked.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelLAlreadyWalked.Location = new System.Drawing.Point(3, 36);
            this.labelLAlreadyWalked.Name = "labelLAlreadyWalked";
            this.labelLAlreadyWalked.Size = new System.Drawing.Size(80, 12);
            this.labelLAlreadyWalked.TabIndex = 2;
            this.labelLAlreadyWalked.Text = "AlreadyWalked";
            // 
            // labelLUnkys
            // 
            this.labelLUnkys.AutoSize = true;
            this.labelLUnkys.BackColor = System.Drawing.Color.OrangeRed;
            this.labelLUnkys.Location = new System.Drawing.Point(3, 48);
            this.labelLUnkys.Name = "labelLUnkys";
            this.labelLUnkys.Size = new System.Drawing.Size(74, 12);
            this.labelLUnkys.TabIndex = 3;
            this.labelLUnkys.Text = "Unknown exit";
            // 
            // labelLUnks
            // 
            this.labelLUnks.AutoSize = true;
            this.labelLUnks.BackColor = System.Drawing.Color.Yellow;
            this.labelLUnks.Location = new System.Drawing.Point(3, 60);
            this.labelLUnks.Name = "labelLUnks";
            this.labelLUnks.Size = new System.Drawing.Size(115, 12);
            this.labelLUnks.TabIndex = 5;
            this.labelLUnks.Text = "Unknown exit(solved)";
            // 
            // lvTopics
            // 
            this.lvTopics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lvTopics.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvTopics.FullRowSelect = true;
            this.lvTopics.GridLines = true;
            this.lvTopics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTopics.HideSelection = false;
            this.lvTopics.Location = new System.Drawing.Point(622, 94);
            this.lvTopics.MultiSelect = false;
            this.lvTopics.Name = "lvTopics";
            this.lvTopics.Size = new System.Drawing.Size(111, 216);
            this.lvTopics.TabIndex = 3;
            this.lvTopics.UseCompatibleStateImageBehavior = false;
            this.lvTopics.View = System.Windows.Forms.View.Details;
            this.lvTopics.SelectedIndexChanged += new System.EventHandler(this.lvTopics_SelectedIndexChanged);
            this.lvTopics.DoubleClick += new System.EventHandler(this.lvTopics_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 80;
            // 
            // flphvMark
            // 
            this.flphvMark.Controls.Add(this.labelMWritten);
            this.flphvMark.Location = new System.Drawing.Point(622, 316);
            this.flphvMark.Name = "flphvMark";
            this.flphvMark.Size = new System.Drawing.Size(111, 33);
            this.flphvMark.TabIndex = 5;
            // 
            // labelMWritten
            // 
            this.labelMWritten.AutoSize = true;
            this.labelMWritten.BackColor = System.Drawing.Color.GreenYellow;
            this.labelMWritten.Location = new System.Drawing.Point(3, 0);
            this.labelMWritten.Name = "labelMWritten";
            this.labelMWritten.Size = new System.Drawing.Size(40, 12);
            this.labelMWritten.TabIndex = 0;
            this.labelMWritten.Text = "written";
            // 
            // lvVifacc
            // 
            this.lvVifacc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderVtAddr,
            this.columnHeaderVtDataAddr,
            this.columnHeaderVtTy});
            this.lvVifacc.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvVifacc.FullRowSelect = true;
            this.lvVifacc.GridLines = true;
            this.lvVifacc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvVifacc.HideSelection = false;
            this.lvVifacc.Location = new System.Drawing.Point(515, 355);
            this.lvVifacc.MultiSelect = false;
            this.lvVifacc.Name = "lvVifacc";
            this.lvVifacc.Size = new System.Drawing.Size(218, 275);
            this.lvVifacc.TabIndex = 6;
            this.lvVifacc.UseCompatibleStateImageBehavior = false;
            this.lvVifacc.View = System.Windows.Forms.View.Details;
            this.lvVifacc.DoubleClick += new System.EventHandler(this.lvVifacc_DoubleClick);
            // 
            // columnHeaderVtAddr
            // 
            this.columnHeaderVtAddr.Text = "tag a";
            this.columnHeaderVtAddr.Width = 66;
            // 
            // columnHeaderVtDataAddr
            // 
            this.columnHeaderVtDataAddr.Text = "data a";
            this.columnHeaderVtDataAddr.Width = 66;
            // 
            // columnHeaderVtTy
            // 
            this.columnHeaderVtTy.Text = "ty";
            this.columnHeaderVtTy.Width = 46;
            // 
            // hvee
            // 
            this.hvee.AntiFlick = true;
            this.hvee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hvee.ByteWidth = 16;
            this.hvee.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hvee.Location = new System.Drawing.Point(12, 316);
            this.hvee.Name = "hvee";
            this.hvee.OffDelta = 0;
            this.hvee.PgScroll = Readmset.PgScrollType.Absolute;
            this.hvee.Size = new System.Drawing.Size(497, 181);
            this.hvee.TabIndex = 4;
            this.hvee.UnitPg = 256;
            this.hvee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hvee_KeyPress);
            // 
            // hvsp
            // 
            this.hvsp.AntiFlick = true;
            this.hvsp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hvsp.ByteWidth = 16;
            this.hvsp.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hvsp.Location = new System.Drawing.Point(12, 503);
            this.hvsp.Name = "hvsp";
            this.hvsp.OffDelta = 0;
            this.hvsp.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hvsp.Size = new System.Drawing.Size(497, 127);
            this.hvsp.TabIndex = 7;
            this.hvsp.UnitPg = 256;
            this.hvsp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hvee_KeyPress);
            // 
            // Trform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 642);
            this.Controls.Add(this.hvsp);
            this.Controls.Add(this.lvVifacc);
            this.Controls.Add(this.flphvMark);
            this.Controls.Add(this.hvee);
            this.Controls.Add(this.lvTopics);
            this.Controls.Add(this.flpLegend);
            this.Controls.Add(this.lvDA);
            this.Controls.Add(this.lvMod);
            this.Name = "Trform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "trace HLP ][";
            this.Load += new System.EventHandler(this.Trform_Load);
            this.flpLegend.ResumeLayout(false);
            this.flpLegend.PerformLayout();
            this.flphvMark.ResumeLayout(false);
            this.flphvMark.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMod;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvDA;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.FlowLayoutPanel flpLegend;
        private System.Windows.Forms.Label labelLStartAddr;
        private System.Windows.Forms.Label labelLTermination;
        private System.Windows.Forms.Label labelLAlreadyWalked;
        private System.Windows.Forms.Label labelLUnkys;
        private System.Windows.Forms.ListView lvTopics;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelLExitAddr;
        private Readmset.HexVwer hvee;
        private System.Windows.Forms.FlowLayoutPanel flphvMark;
        private System.Windows.Forms.Label labelMWritten;
        private System.Windows.Forms.ListView lvVifacc;
        private System.Windows.Forms.ColumnHeader columnHeaderVtAddr;
        private System.Windows.Forms.ColumnHeader columnHeaderVtDataAddr;
        private System.Windows.Forms.ColumnHeader columnHeaderVtTy;
        private Readmset.HexVwer hvsp;
        private System.Windows.Forms.Label labelLUnks;
    }
}

