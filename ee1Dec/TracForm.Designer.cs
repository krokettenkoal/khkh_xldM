using ee1Dec.Controls;
using ee1Dec.Controls.HV;

namespace ee1Dec {
    partial class TracForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TracForm));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderOP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ilst = new System.Windows.Forms.ImageList(this.components);
            this.labelTick = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEE = new System.Windows.Forms.TabPage();
            this.labelEER = new System.Windows.Forms.Label();
            this.tabPageCOP1 = new System.Windows.Forms.TabPage();
            this.labelCOP1Reg = new System.Windows.Forms.Label();
            this.tabPageCOP2 = new System.Windows.Forms.TabPage();
            this.labelCOP2 = new System.Windows.Forms.Label();
            this.tabPageRpc = new System.Windows.Forms.TabPage();
            this.lbRecentpc = new System.Windows.Forms.ListBox();
            this.buttonRUpd = new System.Windows.Forms.Button();
            this.buttonStep = new System.Windows.Forms.Button();
            this.buttonRunUntil = new System.Windows.Forms.Button();
            this.textBoxLogging = new System.Windows.Forms.TextBox();
            this.labelContHint = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.buttonEnterprise = new System.Windows.Forms.Button();
            this.buttonOpenWith = new System.Windows.Forms.Button();
            this.buttonExpSim = new System.Windows.Forms.Button();
            this.buttonTocs = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabelPrefix = new System.Windows.Forms.LinkLabel();
            this.buttonDEB2 = new System.Windows.Forms.Button();
            this.cmsDEB = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.outputSxyzRxyzTxyzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createJMPTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconResult = new System.Windows.Forms.NotifyIcon(this.components);
            this.listViewMMap = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hexVwer2 = new ee1Dec.Controls.HV.HexVwer();
            this.hexVwer1 = new ee1Dec.Controls.HV.HexVwer();
            this.buttonCopyseled = new System.Windows.Forms.Button();
            this.buttonDump = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbto = new System.Windows.Forms.TextBox();
            this.tbfrm = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageEE.SuspendLayout();
            this.tabPageCOP1.SuspendLayout();
            this.tabPageCOP2.SuspendLayout();
            this.tabPageRpc.SuspendLayout();
            this.cmsDEB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAddr,
            this.columnHeaderOP,
            this.columnHeaderDesc});
            this.listView1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(434, 365);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.StateImageList = this.ilst;
            this.listView1.TabIndex = 16;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.VirtualListSize = 8388608;
            this.listView1.VirtualMode = true;
            this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listView1_KeyPress);
            // 
            // columnHeaderAddr
            // 
            this.columnHeaderAddr.Text = "Addr";
            this.columnHeaderAddr.Width = 88;
            // 
            // columnHeaderOP
            // 
            this.columnHeaderOP.Text = "OP";
            this.columnHeaderOP.Width = 166;
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "Description";
            this.columnHeaderDesc.Width = 152;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // ilst
            // 
            this.ilst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilst.ImageStream")));
            this.ilst.TransparentColor = System.Drawing.Color.Transparent;
            this.ilst.Images.SetKeyName(0, "End");
            this.ilst.Images.SetKeyName(1, "D");
            this.ilst.Images.SetKeyName(2, "U");
            this.ilst.Images.SetKeyName(3, "JR");
            // 
            // labelTick
            // 
            this.labelTick.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTick.Location = new System.Drawing.Point(360, 9);
            this.labelTick.Name = "labelTick";
            this.labelTick.Size = new System.Drawing.Size(86, 12);
            this.labelTick.TabIndex = 15;
            this.labelTick.Text = "tick: 0";
            this.labelTick.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPageEE);
            this.tabControl1.Controls.Add(this.tabPageCOP1);
            this.tabControl1.Controls.Add(this.tabPageCOP2);
            this.tabControl1.Controls.Add(this.tabPageRpc);
            this.tabControl1.Location = new System.Drawing.Point(452, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 358);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPageEE
            // 
            this.tabPageEE.AutoScroll = true;
            this.tabPageEE.Controls.Add(this.labelEER);
            this.tabPageEE.Location = new System.Drawing.Point(4, 22);
            this.tabPageEE.Name = "tabPageEE";
            this.tabPageEE.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEE.Size = new System.Drawing.Size(441, 332);
            this.tabPageEE.TabIndex = 0;
            this.tabPageEE.Text = "EE";
            this.tabPageEE.UseVisualStyleBackColor = true;
            // 
            // labelEER
            // 
            this.labelEER.AutoSize = true;
            this.labelEER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEER.Location = new System.Drawing.Point(6, 3);
            this.labelEER.Name = "labelEER";
            this.labelEER.Size = new System.Drawing.Size(23, 12);
            this.labelEER.TabIndex = 0;
            this.labelEER.Text = "...";
            // 
            // tabPageCOP1
            // 
            this.tabPageCOP1.AutoScroll = true;
            this.tabPageCOP1.Controls.Add(this.labelCOP1Reg);
            this.tabPageCOP1.Location = new System.Drawing.Point(4, 22);
            this.tabPageCOP1.Name = "tabPageCOP1";
            this.tabPageCOP1.Size = new System.Drawing.Size(441, 332);
            this.tabPageCOP1.TabIndex = 1;
            this.tabPageCOP1.Text = "COP1";
            this.tabPageCOP1.UseVisualStyleBackColor = true;
            // 
            // labelCOP1Reg
            // 
            this.labelCOP1Reg.AutoSize = true;
            this.labelCOP1Reg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCOP1Reg.Location = new System.Drawing.Point(6, 3);
            this.labelCOP1Reg.Name = "labelCOP1Reg";
            this.labelCOP1Reg.Size = new System.Drawing.Size(23, 12);
            this.labelCOP1Reg.TabIndex = 1;
            this.labelCOP1Reg.Text = "...";
            // 
            // tabPageCOP2
            // 
            this.tabPageCOP2.AutoScroll = true;
            this.tabPageCOP2.Controls.Add(this.labelCOP2);
            this.tabPageCOP2.Location = new System.Drawing.Point(4, 22);
            this.tabPageCOP2.Name = "tabPageCOP2";
            this.tabPageCOP2.Size = new System.Drawing.Size(441, 332);
            this.tabPageCOP2.TabIndex = 2;
            this.tabPageCOP2.Text = "COP2";
            this.tabPageCOP2.UseVisualStyleBackColor = true;
            // 
            // labelCOP2
            // 
            this.labelCOP2.AutoSize = true;
            this.labelCOP2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCOP2.Location = new System.Drawing.Point(6, 3);
            this.labelCOP2.Name = "labelCOP2";
            this.labelCOP2.Size = new System.Drawing.Size(23, 12);
            this.labelCOP2.TabIndex = 2;
            this.labelCOP2.Text = "...";
            // 
            // tabPageRpc
            // 
            this.tabPageRpc.Controls.Add(this.lbRecentpc);
            this.tabPageRpc.Controls.Add(this.buttonRUpd);
            this.tabPageRpc.Location = new System.Drawing.Point(4, 22);
            this.tabPageRpc.Name = "tabPageRpc";
            this.tabPageRpc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRpc.Size = new System.Drawing.Size(441, 332);
            this.tabPageRpc.TabIndex = 3;
            this.tabPageRpc.Text = "Recent pc";
            this.tabPageRpc.UseVisualStyleBackColor = true;
            // 
            // lbRecentpc
            // 
            this.lbRecentpc.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbRecentpc.FormattingEnabled = true;
            this.lbRecentpc.IntegralHeight = false;
            this.lbRecentpc.ItemHeight = 12;
            this.lbRecentpc.Items.AddRange(new object[] {
            "12345678"});
            this.lbRecentpc.Location = new System.Drawing.Point(6, 35);
            this.lbRecentpc.Name = "lbRecentpc";
            this.lbRecentpc.Size = new System.Drawing.Size(92, 280);
            this.lbRecentpc.TabIndex = 1;
            this.lbRecentpc.SelectedIndexChanged += new System.EventHandler(this.lbRecentpc_SelectedIndexChanged);
            // 
            // buttonRUpd
            // 
            this.buttonRUpd.Location = new System.Drawing.Point(6, 6);
            this.buttonRUpd.Name = "buttonRUpd";
            this.buttonRUpd.Size = new System.Drawing.Size(75, 23);
            this.buttonRUpd.TabIndex = 0;
            this.buttonRUpd.Text = "&Update";
            this.buttonRUpd.UseVisualStyleBackColor = true;
            this.buttonRUpd.Click += new System.EventHandler(this.buttonRUpd_Click);
            // 
            // buttonStep
            // 
            this.buttonStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStep.Location = new System.Drawing.Point(452, 376);
            this.buttonStep.Name = "buttonStep";
            this.buttonStep.Size = new System.Drawing.Size(75, 23);
            this.buttonStep.TabIndex = 0;
            this.buttonStep.Text = "&Step";
            this.buttonStep.UseVisualStyleBackColor = true;
            this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
            // 
            // buttonRunUntil
            // 
            this.buttonRunUntil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRunUntil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunUntil.Location = new System.Drawing.Point(533, 376);
            this.buttonRunUntil.Name = "buttonRunUntil";
            this.buttonRunUntil.Size = new System.Drawing.Size(75, 23);
            this.buttonRunUntil.TabIndex = 1;
            this.buttonRunUntil.Text = "Run un&til";
            this.buttonRunUntil.UseVisualStyleBackColor = true;
            this.buttonRunUntil.Click += new System.EventHandler(this.buttonRunUntil_Click);
            // 
            // textBoxLogging
            // 
            this.textBoxLogging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxLogging.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxLogging.Location = new System.Drawing.Point(452, 494);
            this.textBoxLogging.Multiline = true;
            this.textBoxLogging.Name = "textBoxLogging";
            this.textBoxLogging.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogging.Size = new System.Drawing.Size(449, 113);
            this.textBoxLogging.TabIndex = 10;
            // 
            // labelContHint
            // 
            this.labelContHint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelContHint.AutoSize = true;
            this.labelContHint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelContHint.Location = new System.Drawing.Point(12, 392);
            this.labelContHint.Name = "labelContHint";
            this.labelContHint.Size = new System.Drawing.Size(11, 36);
            this.labelContHint.TabIndex = 17;
            this.labelContHint.Text = "1\r\n2\r\n3";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(322, 9);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(32, 12);
            this.linkLabel2.TabIndex = 14;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Color";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // buttonEnterprise
            // 
            this.buttonEnterprise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEnterprise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnterprise.Location = new System.Drawing.Point(533, 405);
            this.buttonEnterprise.Name = "buttonEnterprise";
            this.buttonEnterprise.Size = new System.Drawing.Size(75, 23);
            this.buttonEnterprise.TabIndex = 4;
            this.buttonEnterprise.Text = "&DEBUG";
            this.buttonEnterprise.UseVisualStyleBackColor = true;
            this.buttonEnterprise.Visible = false;
            this.buttonEnterprise.Click += new System.EventHandler(this.buttonEnterprise_Click);
            // 
            // buttonOpenWith
            // 
            this.buttonOpenWith.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenWith.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenWith.Location = new System.Drawing.Point(614, 376);
            this.buttonOpenWith.Name = "buttonOpenWith";
            this.buttonOpenWith.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenWith.TabIndex = 2;
            this.buttonOpenWith.Text = "&Open...";
            this.buttonOpenWith.UseVisualStyleBackColor = true;
            this.buttonOpenWith.Click += new System.EventHandler(this.buttonOpenWith_Click);
            // 
            // buttonExpSim
            // 
            this.buttonExpSim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExpSim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExpSim.Location = new System.Drawing.Point(707, 376);
            this.buttonExpSim.Name = "buttonExpSim";
            this.buttonExpSim.Size = new System.Drawing.Size(75, 23);
            this.buttonExpSim.TabIndex = 5;
            this.buttonExpSim.Text = "Export sim";
            this.toolTip1.SetToolTip(this.buttonExpSim, "Export the trace info for later use in interpreter DEBUG mode. Run at last after " +
        "complete execution of your trac bin.");
            this.buttonExpSim.UseVisualStyleBackColor = true;
            this.buttonExpSim.Click += new System.EventHandler(this.buttonExpSim_Click);
            // 
            // buttonTocs
            // 
            this.buttonTocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTocs.Location = new System.Drawing.Point(707, 405);
            this.buttonTocs.Name = "buttonTocs";
            this.buttonTocs.Size = new System.Drawing.Size(75, 23);
            this.buttonTocs.TabIndex = 6;
            this.buttonTocs.Text = "To &C#";
            this.toolTip1.SetToolTip(this.buttonTocs, "Convert the disarm code to compatible C# source code. Run at first after trac bin" +
        " loaded.");
            this.buttonTocs.UseVisualStyleBackColor = true;
            this.buttonTocs.Click += new System.EventHandler(this.buttonTocs_Click);
            // 
            // linkLabelPrefix
            // 
            this.linkLabelPrefix.AutoSize = true;
            this.linkLabelPrefix.Location = new System.Drawing.Point(260, 9);
            this.linkLabelPrefix.Name = "linkLabelPrefix";
            this.linkLabelPrefix.Size = new System.Drawing.Size(56, 12);
            this.linkLabelPrefix.TabIndex = 13;
            this.linkLabelPrefix.TabStop = true;
            this.linkLabelPrefix.Text = "linkLabel3";
            // 
            // buttonDEB2
            // 
            this.buttonDEB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDEB2.ContextMenuStrip = this.cmsDEB;
            this.buttonDEB2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDEB2.Location = new System.Drawing.Point(452, 405);
            this.buttonDEB2.Name = "buttonDEB2";
            this.buttonDEB2.Size = new System.Drawing.Size(75, 23);
            this.buttonDEB2.TabIndex = 3;
            this.buttonDEB2.Text = "DEB2";
            this.buttonDEB2.UseVisualStyleBackColor = true;
            this.buttonDEB2.Visible = false;
            this.buttonDEB2.Click += new System.EventHandler(this.buttonDEB2_Click);
            // 
            // cmsDEB
            // 
            this.cmsDEB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputSxyzRxyzTxyzToolStripMenuItem,
            this.toolStripSeparator1,
            this.createJMPTableToolStripMenuItem});
            this.cmsDEB.Name = "cmsDEB";
            this.cmsDEB.Size = new System.Drawing.Size(185, 54);
            // 
            // outputSxyzRxyzTxyzToolStripMenuItem
            // 
            this.outputSxyzRxyzTxyzToolStripMenuItem.Name = "outputSxyzRxyzTxyzToolStripMenuItem";
            this.outputSxyzRxyzTxyzToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.outputSxyzRxyzTxyzToolStripMenuItem.Text = "Output SxyzRxyzTxyz";
            this.outputSxyzRxyzTxyzToolStripMenuItem.Click += new System.EventHandler(this.outputSxyzRxyzTxyzToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // createJMPTableToolStripMenuItem
            // 
            this.createJMPTableToolStripMenuItem.Name = "createJMPTableToolStripMenuItem";
            this.createJMPTableToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.createJMPTableToolStripMenuItem.Text = "Create JMP table";
            this.createJMPTableToolStripMenuItem.Click += new System.EventHandler(this.createJMPTableToolStripMenuItem_Click);
            // 
            // notifyIconResult
            // 
            this.notifyIconResult.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconResult.Icon")));
            this.notifyIconResult.Text = "TracForm";
            this.notifyIconResult.Visible = true;
            this.notifyIconResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconResult_MouseDoubleClick);
            // 
            // listViewMMap
            // 
            this.listViewMMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewMMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewMMap.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listViewMMap.FullRowSelect = true;
            this.listViewMMap.GridLines = true;
            this.listViewMMap.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMMap.HideSelection = false;
            this.listViewMMap.Location = new System.Drawing.Point(452, 431);
            this.listViewMMap.MultiSelect = false;
            this.listViewMMap.Name = "listViewMMap";
            this.listViewMMap.Size = new System.Drawing.Size(237, 57);
            this.listViewMMap.TabIndex = 9;
            this.listViewMMap.UseCompatibleStateImageBehavior = false;
            this.listViewMMap.View = System.Windows.Forms.View.Details;
            this.listViewMMap.VirtualListSize = 3145728;
            this.listViewMMap.VirtualMode = true;
            this.listViewMMap.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewMMap_RetrieveVirtualItem);
            this.listViewMMap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewMMap_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 200;
            // 
            // hexVwer2
            // 
            this.hexVwer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hexVwer2.AntiFlick = true;
            this.hexVwer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexVwer2.ByteWidth = 16;
            this.hexVwer2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hexVwer2.Location = new System.Drawing.Point(12, 494);
            this.hexVwer2.Name = "hexVwer2";
            this.hexVwer2.OffDelta = 0;
            this.hexVwer2.PgScroll = ee1Dec.Controls.HV.PgScrollType.ScreenSizeBased;
            this.hexVwer2.Size = new System.Drawing.Size(434, 114);
            this.hexVwer2.TabIndex = 19;
            this.hexVwer2.UnitPg = 512;
            this.hexVwer2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexVwer2_KeyPress);
            // 
            // hexVwer1
            // 
            this.hexVwer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hexVwer1.AntiFlick = true;
            this.hexVwer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexVwer1.ByteWidth = 16;
            this.hexVwer1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hexVwer1.Location = new System.Drawing.Point(12, 431);
            this.hexVwer1.Name = "hexVwer1";
            this.hexVwer1.OffDelta = 0;
            this.hexVwer1.PgScroll = ee1Dec.Controls.HV.PgScrollType.ScreenSizeBased;
            this.hexVwer1.Size = new System.Drawing.Size(434, 57);
            this.hexVwer1.TabIndex = 18;
            this.hexVwer1.UnitPg = 512;
            this.hexVwer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexVwer2_KeyPress);
            // 
            // buttonCopyseled
            // 
            this.buttonCopyseled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyseled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyseled.Location = new System.Drawing.Point(707, 431);
            this.buttonCopyseled.Name = "buttonCopyseled";
            this.buttonCopyseled.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyseled.TabIndex = 7;
            this.buttonCopyseled.Text = "Co&py seled";
            this.buttonCopyseled.UseVisualStyleBackColor = true;
            this.buttonCopyseled.Click += new System.EventHandler(this.buttonCopyseled_Click);
            // 
            // buttonDump
            // 
            this.buttonDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDump.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDump.Location = new System.Drawing.Point(29, 71);
            this.buttonDump.Name = "buttonDump";
            this.buttonDump.Size = new System.Drawing.Size(75, 23);
            this.buttonDump.TabIndex = 4;
            this.buttonDump.Text = "&dump to...";
            this.buttonDump.UseVisualStyleBackColor = true;
            this.buttonDump.Click += new System.EventHandler(this.buttonDump_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbto);
            this.groupBox1.Controls.Add(this.tbfrm);
            this.groupBox1.Controls.Add(this.buttonDump);
            this.groupBox1.Location = new System.Drawing.Point(788, 376);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "memory dump";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "frm";
            // 
            // tbto
            // 
            this.tbto.Location = new System.Drawing.Point(29, 43);
            this.tbto.Name = "tbto";
            this.tbto.Size = new System.Drawing.Size(75, 19);
            this.tbto.TabIndex = 3;
            this.tbto.Text = "02000000";
            // 
            // tbfrm
            // 
            this.tbfrm.Location = new System.Drawing.Point(29, 18);
            this.tbfrm.Name = "tbfrm";
            this.tbfrm.Size = new System.Drawing.Size(75, 19);
            this.tbfrm.TabIndex = 1;
            this.tbfrm.Text = "00000000";
            // 
            // TracForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 619);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewMMap);
            this.Controls.Add(this.buttonDEB2);
            this.Controls.Add(this.linkLabelPrefix);
            this.Controls.Add(this.buttonOpenWith);
            this.Controls.Add(this.buttonCopyseled);
            this.Controls.Add(this.buttonTocs);
            this.Controls.Add(this.buttonExpSim);
            this.Controls.Add(this.buttonEnterprise);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.labelContHint);
            this.Controls.Add(this.textBoxLogging);
            this.Controls.Add(this.buttonRunUntil);
            this.Controls.Add(this.hexVwer2);
            this.Controls.Add(this.hexVwer1);
            this.Controls.Add(this.buttonStep);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelTick);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.linkLabel1);
            this.Name = "TracForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TracForm";
            this.Load += new System.EventHandler(this.TracForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEE.ResumeLayout(false);
            this.tabPageEE.PerformLayout();
            this.tabPageCOP1.ResumeLayout(false);
            this.tabPageCOP1.PerformLayout();
            this.tabPageCOP2.ResumeLayout(false);
            this.tabPageCOP2.PerformLayout();
            this.tabPageRpc.ResumeLayout(false);
            this.cmsDEB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderAddr;
        private System.Windows.Forms.ColumnHeader columnHeaderOP;
        private System.Windows.Forms.Label labelTick;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageEE;
        private System.Windows.Forms.Label labelEER;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonStep;
        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private HexVwer hexVwer1;
        private HexVwer hexVwer2;
        private System.Windows.Forms.Button buttonRunUntil;
        private System.Windows.Forms.TabPage tabPageCOP1;
        private System.Windows.Forms.Label labelCOP1Reg;
        private System.Windows.Forms.TabPage tabPageCOP2;
        private System.Windows.Forms.TextBox textBoxLogging;
        private System.Windows.Forms.Label labelContHint;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label labelCOP2;
        private System.Windows.Forms.Button buttonEnterprise;
        private System.Windows.Forms.Button buttonOpenWith;
        private System.Windows.Forms.Button buttonExpSim;
        private System.Windows.Forms.Button buttonTocs;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabelPrefix;
        private System.Windows.Forms.Button buttonDEB2;
        private System.Windows.Forms.NotifyIcon notifyIconResult;
        private System.Windows.Forms.ListView listViewMMap;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonCopyseled;
        private System.Windows.Forms.ContextMenuStrip cmsDEB;
        private System.Windows.Forms.ToolStripMenuItem outputSxyzRxyzTxyzToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageRpc;
        private System.Windows.Forms.Button buttonRUpd;
        private System.Windows.Forms.ListBox lbRecentpc;
        private System.Windows.Forms.ImageList ilst;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem createJMPTableToolStripMenuItem;
        private System.Windows.Forms.Button buttonDump;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbto;
        private System.Windows.Forms.TextBox tbfrm;
    }
}