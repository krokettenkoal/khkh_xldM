namespace parseSEQD {
    partial class PForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numt = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLayd = new System.Windows.Forms.ListBox();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.cbAutoAdv = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbbar = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lvq1 = new System.Windows.Forms.ListView();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.label10 = new System.Windows.Forms.Label();
            this.lvl2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.lvq2 = new System.Windows.Forms.ListView();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.lvq3 = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.lvq4 = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.lvq5 = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.cmsQ5 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectOnlyThisQ5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvl1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sc2 = new System.Windows.Forms.SplitContainer();
            this.flppf = new System.Windows.Forms.FlowLayoutPanel();
            this.p1 = new parseSEQD.ThreeD();
            this.timerRecalc = new System.Windows.Forms.Timer(this.components);
            this.timerAutoAdv = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numt)).BeginInit();
            this.sc1.Panel1.SuspendLayout();
            this.sc1.Panel2.SuspendLayout();
            this.sc1.SuspendLayout();
            this.cmsQ5.SuspendLayout();
            this.sc2.Panel1.SuspendLayout();
            this.sc2.Panel2.SuspendLayout();
            this.sc2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "picture components";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Previewer";
            // 
            // numt
            // 
            this.numt.Location = new System.Drawing.Point(257, 15);
            this.numt.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numt.Name = "numt";
            this.numt.Size = new System.Drawing.Size(64, 19);
            this.numt.TabIndex = 4;
            this.numt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numt.ValueChanged += new System.EventHandler(this.numt_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "LAYD";
            // 
            // lbLayd
            // 
            this.lbLayd.ColumnWidth = 50;
            this.lbLayd.FormattingEnabled = true;
            this.lbLayd.IntegralHeight = false;
            this.lbLayd.ItemHeight = 12;
            this.lbLayd.Location = new System.Drawing.Point(141, 15);
            this.lbLayd.MultiColumn = true;
            this.lbLayd.Name = "lbLayd";
            this.lbLayd.Size = new System.Drawing.Size(110, 70);
            this.lbLayd.TabIndex = 6;
            this.lbLayd.SelectedIndexChanged += new System.EventHandler(this.lbLayd_SelectedIndexChanged);
            // 
            // sc1
            // 
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.Location = new System.Drawing.Point(0, 0);
            this.sc1.Name = "sc1";
            // 
            // sc1.Panel1
            // 
            this.sc1.Panel1.AutoScroll = true;
            this.sc1.Panel1.Controls.Add(this.cbAutoAdv);
            this.sc1.Panel1.Controls.Add(this.label6);
            this.sc1.Panel1.Controls.Add(this.label5);
            this.sc1.Panel1.Controls.Add(this.label4);
            this.sc1.Panel1.Controls.Add(this.lbbar);
            this.sc1.Panel1.Controls.Add(this.label11);
            this.sc1.Panel1.Controls.Add(this.lvq1);
            this.sc1.Panel1.Controls.Add(this.label10);
            this.sc1.Panel1.Controls.Add(this.lvl2);
            this.sc1.Panel1.Controls.Add(this.lvq2);
            this.sc1.Panel1.Controls.Add(this.lvq3);
            this.sc1.Panel1.Controls.Add(this.lvq4);
            this.sc1.Panel1.Controls.Add(this.lvq5);
            this.sc1.Panel1.Controls.Add(this.lvl1);
            this.sc1.Panel1.Controls.Add(this.label9);
            this.sc1.Panel1.Controls.Add(this.label8);
            this.sc1.Panel1.Controls.Add(this.label7);
            this.sc1.Panel1.Controls.Add(this.label3);
            this.sc1.Panel1.Controls.Add(this.numt);
            this.sc1.Panel1.Controls.Add(this.lbLayd);
            this.sc1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.sc1_Panel1_Paint);
            // 
            // sc1.Panel2
            // 
            this.sc1.Panel2.Controls.Add(this.sc2);
            this.sc1.Size = new System.Drawing.Size(984, 862);
            this.sc1.SplitterDistance = 494;
            this.sc1.TabIndex = 7;
            // 
            // cbAutoAdv
            // 
            this.cbAutoAdv.AutoSize = true;
            this.cbAutoAdv.Location = new System.Drawing.Point(257, 40);
            this.cbAutoAdv.Name = "cbAutoAdv";
            this.cbAutoAdv.Size = new System.Drawing.Size(64, 16);
            this.cbAutoAdv.TabIndex = 15;
            this.cbAutoAdv.Text = "&Auto +1";
            this.cbAutoAdv.UseVisualStyleBackColor = true;
            this.cbAutoAdv.CheckedChanged += new System.EventHandler(this.cbAutoAdv_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Q5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "L1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "L2";
            // 
            // lbbar
            // 
            this.lbbar.ColumnWidth = 50;
            this.lbbar.FormattingEnabled = true;
            this.lbbar.IntegralHeight = false;
            this.lbbar.ItemHeight = 12;
            this.lbbar.Location = new System.Drawing.Point(4, 15);
            this.lbbar.MultiColumn = true;
            this.lbbar.Name = "lbbar";
            this.lbbar.Size = new System.Drawing.Size(131, 58);
            this.lbbar.TabIndex = 13;
            this.lbbar.SelectedIndexChanged += new System.EventHandler(this.lbbar_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "BAR";
            // 
            // lvq1
            // 
            this.lvq1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvq1.CheckBoxes = true;
            this.lvq1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14});
            this.lvq1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvq1.FullRowSelect = true;
            this.lvq1.GridLines = true;
            this.lvq1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvq1.HideSelection = false;
            this.lvq1.Location = new System.Drawing.Point(26, 742);
            this.lvq1.MultiSelect = false;
            this.lvq1.Name = "lvq1";
            this.lvq1.Size = new System.Drawing.Size(465, 108);
            this.lvq1.TabIndex = 11;
            this.lvq1.UseCompatibleStateImageBehavior = false;
            this.lvq1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "i";
            this.columnHeader13.Width = 39;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "contents";
            this.columnHeader14.Width = 150;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 742);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "Q1";
            // 
            // lvl2
            // 
            this.lvl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvl2.CheckBoxes = true;
            this.lvl2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvl2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvl2.FullRowSelect = true;
            this.lvl2.GridLines = true;
            this.lvl2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvl2.HideSelection = false;
            this.lvl2.Location = new System.Drawing.Point(26, 91);
            this.lvl2.MultiSelect = false;
            this.lvl2.Name = "lvl2";
            this.lvl2.Size = new System.Drawing.Size(464, 96);
            this.lvl2.TabIndex = 9;
            this.lvl2.UseCompatibleStateImageBehavior = false;
            this.lvl2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "i";
            this.columnHeader3.Width = 39;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "contents";
            this.columnHeader4.Width = 150;
            // 
            // lvq2
            // 
            this.lvq2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvq2.CheckBoxes = true;
            this.lvq2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.lvq2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvq2.FullRowSelect = true;
            this.lvq2.GridLines = true;
            this.lvq2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvq2.HideSelection = false;
            this.lvq2.Location = new System.Drawing.Point(26, 627);
            this.lvq2.MultiSelect = false;
            this.lvq2.Name = "lvq2";
            this.lvq2.Size = new System.Drawing.Size(465, 109);
            this.lvq2.TabIndex = 8;
            this.lvq2.UseCompatibleStateImageBehavior = false;
            this.lvq2.View = System.Windows.Forms.View.Details;
            this.lvq2.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvq2_ItemChecked);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "i";
            this.columnHeader11.Width = 39;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "contents";
            this.columnHeader12.Width = 150;
            // 
            // lvq3
            // 
            this.lvq3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvq3.CheckBoxes = true;
            this.lvq3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvq3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvq3.FullRowSelect = true;
            this.lvq3.GridLines = true;
            this.lvq3.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvq3.HideSelection = false;
            this.lvq3.Location = new System.Drawing.Point(26, 513);
            this.lvq3.MultiSelect = false;
            this.lvq3.Name = "lvq3";
            this.lvq3.Size = new System.Drawing.Size(465, 108);
            this.lvq3.TabIndex = 8;
            this.lvq3.UseCompatibleStateImageBehavior = false;
            this.lvq3.View = System.Windows.Forms.View.Details;
            this.lvq3.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvq2_ItemChecked);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "i";
            this.columnHeader9.Width = 39;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "contents";
            this.columnHeader10.Width = 150;
            // 
            // lvq4
            // 
            this.lvq4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvq4.CheckBoxes = true;
            this.lvq4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.lvq4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvq4.FullRowSelect = true;
            this.lvq4.GridLines = true;
            this.lvq4.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvq4.HideSelection = false;
            this.lvq4.Location = new System.Drawing.Point(26, 398);
            this.lvq4.MultiSelect = false;
            this.lvq4.Name = "lvq4";
            this.lvq4.Size = new System.Drawing.Size(465, 109);
            this.lvq4.TabIndex = 8;
            this.lvq4.UseCompatibleStateImageBehavior = false;
            this.lvq4.View = System.Windows.Forms.View.Details;
            this.lvq4.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvq2_ItemChecked);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "i";
            this.columnHeader7.Width = 39;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "contents";
            this.columnHeader8.Width = 150;
            // 
            // lvq5
            // 
            this.lvq5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvq5.CheckBoxes = true;
            this.lvq5.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvq5.ContextMenuStrip = this.cmsQ5;
            this.lvq5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvq5.FullRowSelect = true;
            this.lvq5.GridLines = true;
            this.lvq5.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvq5.HideSelection = false;
            this.lvq5.Location = new System.Drawing.Point(26, 289);
            this.lvq5.MultiSelect = false;
            this.lvq5.Name = "lvq5";
            this.lvq5.Size = new System.Drawing.Size(464, 103);
            this.lvq5.TabIndex = 8;
            this.lvq5.UseCompatibleStateImageBehavior = false;
            this.lvq5.View = System.Windows.Forms.View.Details;
            this.lvq5.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvq2_ItemChecked);
            this.lvq5.SelectedIndexChanged += new System.EventHandler(this.lvq5_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "i";
            this.columnHeader5.Width = 39;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "contents";
            this.columnHeader6.Width = 150;
            // 
            // cmsQ5
            // 
            this.cmsQ5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectOnlyThisQ5ToolStripMenuItem});
            this.cmsQ5.Name = "cmsQ5";
            this.cmsQ5.Size = new System.Drawing.Size(186, 26);
            // 
            // selectOnlyThisQ5ToolStripMenuItem
            // 
            this.selectOnlyThisQ5ToolStripMenuItem.Name = "selectOnlyThisQ5ToolStripMenuItem";
            this.selectOnlyThisQ5ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.selectOnlyThisQ5ToolStripMenuItem.Text = "Select only this Q5";
            this.selectOnlyThisQ5ToolStripMenuItem.Click += new System.EventHandler(this.selectOnlyThisQ5ToolStripMenuItem_Click);
            // 
            // lvl1
            // 
            this.lvl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvl1.CheckBoxes = true;
            this.lvl1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvl1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvl1.FullRowSelect = true;
            this.lvl1.GridLines = true;
            this.lvl1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvl1.HideSelection = false;
            this.lvl1.Location = new System.Drawing.Point(26, 193);
            this.lvl1.MultiSelect = false;
            this.lvl1.Name = "lvl1";
            this.lvl1.Size = new System.Drawing.Size(464, 90);
            this.lvl1.TabIndex = 8;
            this.lvl1.UseCompatibleStateImageBehavior = false;
            this.lvl1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "i";
            this.columnHeader1.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "contents";
            this.columnHeader2.Width = 150;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 627);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Q2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 513);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Q3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 398);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "Q4";
            // 
            // sc2
            // 
            this.sc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc2.Location = new System.Drawing.Point(0, 0);
            this.sc2.Name = "sc2";
            this.sc2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc2.Panel1
            // 
            this.sc2.Panel1.Controls.Add(this.flppf);
            this.sc2.Panel1.Controls.Add(this.label1);
            // 
            // sc2.Panel2
            // 
            this.sc2.Panel2.Controls.Add(this.p1);
            this.sc2.Panel2.Controls.Add(this.label2);
            this.sc2.Size = new System.Drawing.Size(486, 862);
            this.sc2.SplitterDistance = 444;
            this.sc2.TabIndex = 0;
            // 
            // flppf
            // 
            this.flppf.AutoScroll = true;
            this.flppf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.flppf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flppf.Location = new System.Drawing.Point(0, 12);
            this.flppf.Name = "flppf";
            this.flppf.Size = new System.Drawing.Size(486, 432);
            this.flppf.TabIndex = 2;
            // 
            // p1
            // 
            this.p1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.p1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p1.Location = new System.Drawing.Point(0, 12);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(486, 402);
            this.p1.TabIndex = 3;
            this.p1.Trimeshes = null;
            // 
            // timerRecalc
            // 
            this.timerRecalc.Interval = 1;
            this.timerRecalc.Tick += new System.EventHandler(this.timerRecalc_Tick);
            // 
            // timerAutoAdv
            // 
            this.timerAutoAdv.Interval = 25;
            this.timerAutoAdv.Tick += new System.EventHandler(this.timerAutoAdv_Tick);
            // 
            // PForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 862);
            this.Controls.Add(this.sc1);
            this.Name = "PForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Play LAYD/SEQD (drop .2ld .2dd files)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PForm_DragEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numt)).EndInit();
            this.sc1.Panel1.ResumeLayout(false);
            this.sc1.Panel1.PerformLayout();
            this.sc1.Panel2.ResumeLayout(false);
            this.sc1.ResumeLayout(false);
            this.cmsQ5.ResumeLayout(false);
            this.sc2.Panel1.ResumeLayout(false);
            this.sc2.Panel1.PerformLayout();
            this.sc2.Panel2.ResumeLayout(false);
            this.sc2.Panel2.PerformLayout();
            this.sc2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbLayd;
        private System.Windows.Forms.SplitContainer sc1;
        private System.Windows.Forms.SplitContainer sc2;
        private System.Windows.Forms.FlowLayoutPanel flppf;
        private System.Windows.Forms.ListView lvl1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvl2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView lvq5;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView lvq4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvq3;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lvq2;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView lvq1;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lbbar;
        private System.Windows.Forms.Label label11;
        private ThreeD p1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerRecalc;
        private System.Windows.Forms.ContextMenuStrip cmsQ5;
        private System.Windows.Forms.ToolStripMenuItem selectOnlyThisQ5ToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbAutoAdv;
        private System.Windows.Forms.Timer timerAutoAdv;
    }
}

