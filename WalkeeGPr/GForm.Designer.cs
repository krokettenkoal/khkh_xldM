namespace WalkeeGPr {
    partial class GForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvS = new System.Windows.Forms.ListView();
            this.chSL = new System.Windows.Forms.ColumnHeader();
            this.cmsTrackr = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ssAL = new System.Windows.Forms.StatusStrip();
            this.tsslTarreg = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslREGval = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbfn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ssAL.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvS);
            this.splitContainer1.Panel1.Controls.Add(this.ssAL);
            this.splitContainer1.Panel1.Controls.Add(this.cbfn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(790, 504);
            this.splitContainer1.SplitterDistance = 622;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvS
            // 
            this.lvS.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSL});
            this.lvS.ContextMenuStrip = this.cmsTrackr;
            this.lvS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvS.FullRowSelect = true;
            this.lvS.GridLines = true;
            this.lvS.Location = new System.Drawing.Point(0, 20);
            this.lvS.Name = "lvS";
            this.lvS.Size = new System.Drawing.Size(622, 462);
            this.lvS.SmallImageList = this.il;
            this.lvS.TabIndex = 5;
            this.lvS.UseCompatibleStateImageBehavior = false;
            this.lvS.View = System.Windows.Forms.View.Details;
            this.lvS.SelectedIndexChanged += new System.EventHandler(this.lvS_SelectedIndexChanged);
            this.lvS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvS_KeyDown);
            // 
            // chSL
            // 
            this.chSL.Text = "Source lines";
            this.chSL.Width = 572;
            // 
            // cmsTrackr
            // 
            this.cmsTrackr.Name = "cmsTrackr";
            this.cmsTrackr.Size = new System.Drawing.Size(61, 4);
            this.cmsTrackr.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTrackr_Opening);
            // 
            // ssAL
            // 
            this.ssAL.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTarreg,
            this.tsslREGval});
            this.ssAL.Location = new System.Drawing.Point(0, 482);
            this.ssAL.Name = "ssAL";
            this.ssAL.Size = new System.Drawing.Size(622, 22);
            this.ssAL.TabIndex = 4;
            this.ssAL.Text = "statusStrip1";
            // 
            // tsslTarreg
            // 
            this.tsslTarreg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsslTarreg.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.tsslTarreg.Name = "tsslTarreg";
            this.tsslTarreg.Size = new System.Drawing.Size(12, 17);
            this.tsslTarreg.Text = "?";
            // 
            // tsslREGval
            // 
            this.tsslREGval.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsslREGval.Name = "tsslREGval";
            this.tsslREGval.Size = new System.Drawing.Size(26, 17);
            this.tsslREGval.Text = "...";
            // 
            // cbfn
            // 
            this.cbfn.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbfn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbfn.FormattingEnabled = true;
            this.cbfn.Location = new System.Drawing.Point(0, 0);
            this.cbfn.Name = "cbfn";
            this.cbfn.Size = new System.Drawing.Size(622, 20);
            this.cbfn.TabIndex = 2;
            this.cbfn.SelectedIndexChanged += new System.EventHandler(this.cbfn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(2, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "UP");
            // 
            // GForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 504);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WalkeeGPr";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ssAL.ResumeLayout(false);
            this.ssAL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip cmsTrackr;
        private System.Windows.Forms.ComboBox cbfn;
        private System.Windows.Forms.ListView lvS;
        private System.Windows.Forms.ColumnHeader chSL;
        private System.Windows.Forms.StatusStrip ssAL;
        private System.Windows.Forms.ToolStripStatusLabel tsslREGval;
        private System.Windows.Forms.ToolStripStatusLabel tsslTarreg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList il;

    }
}

