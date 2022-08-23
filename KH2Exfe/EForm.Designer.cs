namespace KH2Exfe {
    partial class EForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbSrc = new System.Windows.Forms.TextBox();
            this.llSrcDrives = new System.Windows.Forms.LinkLabel();
            this.cmsDrives = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.fbdDst = new System.Windows.Forms.FolderBrowserDialog();
            this.rbv04 = new System.Windows.Forms.RadioButton();
            this.rbv01 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.llViewYaz0r = new System.Windows.Forms.LinkLabel();
            this.bRunYaz0r = new System.Windows.Forms.Button();
            this.tbCmdYaz0r = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.llViewFalo = new System.Windows.Forms.LinkLabel();
            this.bRunFalo = new System.Windows.Forms.Button();
            this.tbCmdFalo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.llViewkkdf2 = new System.Windows.Forms.LinkLabel();
            this.bRunkkdf2 = new System.Windows.Forms.Button();
            this.tbCmdkkdf2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.ttURL = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.llVC90 = new System.Windows.Forms.LinkLabel();
            this.llVC80 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbNameskkdf2 = new System.Windows.Forms.TextBox();
            this.rbSelkkdf2 = new System.Windows.Forms.RadioButton();
            this.rbMassivekkdf2 = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.bUsekkdf2 = new System.Windows.Forms.Button();
            this.bUseFalo = new System.Windows.Forms.Button();
            this.bUseYaz0r = new System.Windows.Forms.Button();
            this.bFind = new System.Windows.Forms.Button();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.llSampFind = new System.Windows.Forms.LinkLabel();
            this.il16 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.llUseSamp = new System.Windows.Forms.LinkLabel();
            this.bUsekkdf2Sel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select your KH2 source:";
            // 
            // tbSrc
            // 
            this.tbSrc.Location = new System.Drawing.Point(146, 12);
            this.tbSrc.Name = "tbSrc";
            this.tbSrc.Size = new System.Drawing.Size(77, 19);
            this.tbSrc.TabIndex = 1;
            // 
            // llSrcDrives
            // 
            this.llSrcDrives.AutoSize = true;
            this.llSrcDrives.Location = new System.Drawing.Point(229, 15);
            this.llSrcDrives.Name = "llSrcDrives";
            this.llSrcDrives.Size = new System.Drawing.Size(105, 12);
            this.llSrcDrives.TabIndex = 2;
            this.llSrcDrives.TabStop = true;
            this.llSrcDrives.Text = "Select a drive from:";
            this.llSrcDrives.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSrcDrives_LinkClicked);
            // 
            // cmsDrives
            // 
            this.cmsDrives.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsDrives.Name = "cmsDrives";
            this.cmsDrives.Size = new System.Drawing.Size(61, 4);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select extractor for you:";
            // 
            // rbv04
            // 
            this.rbv04.AutoSize = true;
            this.rbv04.Checked = true;
            this.rbv04.Location = new System.Drawing.Point(66, 39);
            this.rbv04.Name = "rbv04";
            this.rbv04.Size = new System.Drawing.Size(43, 16);
            this.rbv04.TabIndex = 2;
            this.rbv04.TabStop = true;
            this.rbv04.Text = "v0.4";
            this.rbv04.UseVisualStyleBackColor = true;
            this.rbv04.CheckedChanged += new System.EventHandler(this.rbv01_CheckedChanged);
            // 
            // rbv01
            // 
            this.rbv01.AutoSize = true;
            this.rbv01.Location = new System.Drawing.Point(6, 39);
            this.rbv01.Name = "rbv01";
            this.rbv01.Size = new System.Drawing.Size(43, 16);
            this.rbv01.TabIndex = 1;
            this.rbv01.Text = "v0.1";
            this.rbv01.UseVisualStyleBackColor = true;
            this.rbv01.CheckedChanged += new System.EventHandler(this.rbv01_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(162, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "\'ll extract 2,607(or16,414) files";
            // 
            // llViewYaz0r
            // 
            this.llViewYaz0r.AutoSize = true;
            this.llViewYaz0r.LinkArea = new System.Windows.Forms.LinkArea(8, 11);
            this.llViewYaz0r.Location = new System.Drawing.Point(6, 257);
            this.llViewYaz0r.Name = "llViewYaz0r";
            this.llViewYaz0r.Size = new System.Drawing.Size(190, 17);
            this.llViewYaz0r.TabIndex = 8;
            this.llViewYaz0r.TabStop = true;
            this.llViewYaz0r.Text = "Explore work folder after extraction!";
            this.llViewYaz0r.UseCompatibleTextRendering = true;
            this.llViewYaz0r.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llViewYaz0r_LinkClicked);
            // 
            // bRunYaz0r
            // 
            this.bRunYaz0r.Image = global::KH2Exfe.Properties.Resources.idr_dll;
            this.bRunYaz0r.Location = new System.Drawing.Point(6, 207);
            this.bRunYaz0r.Name = "bRunYaz0r";
            this.bRunYaz0r.Size = new System.Drawing.Size(150, 29);
            this.bRunYaz0r.TabIndex = 6;
            this.bRunYaz0r.Text = "Launch?";
            this.bRunYaz0r.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bRunYaz0r.UseVisualStyleBackColor = true;
            this.bRunYaz0r.Click += new System.EventHandler(this.bRunYaz0r_Click);
            // 
            // tbCmdYaz0r
            // 
            this.tbCmdYaz0r.Location = new System.Drawing.Point(6, 83);
            this.tbCmdYaz0r.Multiline = true;
            this.tbCmdYaz0r.Name = "tbCmdYaz0r";
            this.tbCmdYaz0r.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCmdYaz0r.Size = new System.Drawing.Size(326, 90);
            this.tbCmdYaz0r.TabIndex = 4;
            this.tbCmdYaz0r.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Command line template:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "\'ll extract 4,082 files";
            // 
            // llViewFalo
            // 
            this.llViewFalo.AutoSize = true;
            this.llViewFalo.LinkArea = new System.Windows.Forms.LinkArea(8, 11);
            this.llViewFalo.Location = new System.Drawing.Point(6, 236);
            this.llViewFalo.Name = "llViewFalo";
            this.llViewFalo.Size = new System.Drawing.Size(219, 17);
            this.llViewFalo.TabIndex = 5;
            this.llViewFalo.TabStop = true;
            this.llViewFalo.Text = "Explore work folder, after your extraction.";
            this.llViewFalo.UseCompatibleTextRendering = true;
            this.llViewFalo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llViewFalo_LinkClicked);
            // 
            // bRunFalo
            // 
            this.bRunFalo.Image = global::KH2Exfe.Properties.Resources.idr_dll;
            this.bRunFalo.Location = new System.Drawing.Point(6, 188);
            this.bRunFalo.Name = "bRunFalo";
            this.bRunFalo.Size = new System.Drawing.Size(150, 29);
            this.bRunFalo.TabIndex = 3;
            this.bRunFalo.Text = "Launch?";
            this.bRunFalo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bRunFalo.UseVisualStyleBackColor = true;
            this.bRunFalo.Click += new System.EventHandler(this.bRunFalo_Click);
            // 
            // tbCmdFalo
            // 
            this.tbCmdFalo.Location = new System.Drawing.Point(6, 32);
            this.tbCmdFalo.Multiline = true;
            this.tbCmdFalo.Name = "tbCmdFalo";
            this.tbCmdFalo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCmdFalo.Size = new System.Drawing.Size(326, 113);
            this.tbCmdFalo.TabIndex = 1;
            this.tbCmdFalo.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Command line template:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(162, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "\'ll extract 822 files";
            // 
            // llViewkkdf2
            // 
            this.llViewkkdf2.AutoSize = true;
            this.llViewkkdf2.LinkArea = new System.Windows.Forms.LinkArea(8, 11);
            this.llViewkkdf2.Location = new System.Drawing.Point(6, 273);
            this.llViewkkdf2.Name = "llViewkkdf2";
            this.llViewkkdf2.Size = new System.Drawing.Size(219, 17);
            this.llViewkkdf2.TabIndex = 8;
            this.llViewkkdf2.TabStop = true;
            this.llViewkkdf2.Text = "Explore work folder, after your extraction.";
            this.llViewkkdf2.UseCompatibleTextRendering = true;
            this.llViewkkdf2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llViewkkdf2_LinkClicked);
            // 
            // bRunkkdf2
            // 
            this.bRunkkdf2.Image = global::KH2Exfe.Properties.Resources.idr_dll;
            this.bRunkkdf2.Location = new System.Drawing.Point(6, 224);
            this.bRunkkdf2.Name = "bRunkkdf2";
            this.bRunkkdf2.Size = new System.Drawing.Size(150, 29);
            this.bRunkkdf2.TabIndex = 6;
            this.bRunkkdf2.Text = "Launch?";
            this.bRunkkdf2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bRunkkdf2.UseVisualStyleBackColor = true;
            this.bRunkkdf2.Click += new System.EventHandler(this.bRunkkdf2_Click);
            // 
            // tbCmdkkdf2
            // 
            this.tbCmdkkdf2.Location = new System.Drawing.Point(6, 65);
            this.tbCmdkkdf2.Multiline = true;
            this.tbCmdkkdf2.Name = "tbCmdkkdf2";
            this.tbCmdkkdf2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCmdkkdf2.Size = new System.Drawing.Size(312, 140);
            this.tbCmdkkdf2.TabIndex = 5;
            this.tbCmdkkdf2.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Command line template:";
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Looking for information?";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Image = global::KH2Exfe.Properties.Resources.WebInsertHyperlinkHS;
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 48);
            this.linkLabel1.Location = new System.Drawing.Point(77, 59);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.linkLabel1.Size = new System.Drawing.Size(351, 12);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "MSET RESEARCH PROJECT + FILES UPLOADED + SOURCES";
            this.ttURL.SetToolTip(this.linkLabel1, "http://forum.xentax.com/viewtopic.php?f=16&t=7069");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVisitURL_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Image = global::KH2Exfe.Properties.Resources.WebInsertHyperlinkHS;
            this.linkLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel3.LinkArea = new System.Windows.Forms.LinkArea(0, 26);
            this.linkLabel3.Location = new System.Drawing.Point(77, 101);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.linkLabel3.Size = new System.Drawing.Size(167, 12);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Kingdom Hearts 2 Map files";
            this.ttURL.SetToolTip(this.linkLabel3, "http://forum.xentax.com/viewtopic.php?f=16&t=5763");
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVisitURL_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Image = global::KH2Exfe.Properties.Resources.WebInsertHyperlinkHS;
            this.linkLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 55);
            this.linkLabel2.Location = new System.Drawing.Point(77, 80);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.linkLabel2.Size = new System.Drawing.Size(333, 12);
            this.linkLabel2.TabIndex = 2;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Kingdom Hearts MDLX and Re:coded File Viewer (Request?)";
            this.ttURL.SetToolTip(this.linkLabel2, "http://forum.xentax.com/viewtopic.php?f=16&t=5394");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVisitURL_LinkClicked);
            // 
            // llVC90
            // 
            this.llVC90.AutoSize = true;
            this.llVC90.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.llVC90.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llVC90.LinkArea = new System.Windows.Forms.LinkArea(33, 84);
            this.llVC90.Location = new System.Drawing.Point(6, 44);
            this.llVC90.Name = "llVC90";
            this.llVC90.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.llVC90.Size = new System.Drawing.Size(671, 17);
            this.llVC90.TabIndex = 22;
            this.llVC90.TabStop = true;
            this.llVC90.Text = "Get MSVCR90.DLL as vcredist_x86: Microsoft Visual C++ 2008 Service Pack 1 Redistr" +
                "ibutable Package ATL Security Update";
            this.ttURL.SetToolTip(this.llVC90, "http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=11895");
            this.llVC90.UseCompatibleTextRendering = true;
            this.llVC90.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVisitURL_LinkClicked);
            // 
            // llVC80
            // 
            this.llVC80.AutoSize = true;
            this.llVC80.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.llVC80.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llVC80.LinkArea = new System.Windows.Forms.LinkArea(33, 84);
            this.llVC80.Location = new System.Drawing.Point(6, 20);
            this.llVC80.Name = "llVC80";
            this.llVC80.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.llVC80.Size = new System.Drawing.Size(673, 17);
            this.llVC80.TabIndex = 23;
            this.llVC80.TabStop = true;
            this.llVC80.Text = "Get MSVCR80.DLL as vcredist_x86: Microsoft Visual C++ 2005 Service Pack 1 Redistr" +
                "ibutable Package MFC Security Update";
            this.ttURL.SetToolTip(this.llVC80, "http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=26347");
            this.llVC80.UseCompatibleTextRendering = true;
            this.llVC80.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llVisitURL_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KH2Exfe.Properties.Resources.FINDFILE16;
            this.pictureBox1.Location = new System.Drawing.Point(23, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.ImageList = this.il16;
            this.tabControl1.Location = new System.Drawing.Point(12, 140);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(590, 354);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.rbv04);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.llViewYaz0r);
            this.tabPage1.Controls.Add(this.rbv01);
            this.tabPage1.Controls.Add(this.bRunYaz0r);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbCmdYaz0r);
            this.tabPage1.ImageKey = "Flag_blueHS.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(582, 327);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Use yaz0r\'s extractor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 181);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "*MASSIVE FILE EXTRACTION*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(167, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Select a version which you like:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.llViewFalo);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbCmdFalo);
            this.tabPage2.Controls.Add(this.bRunFalo);
            this.tabPage2.ImageKey = "Flag_greenHS.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(582, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Use Falo\'s extractor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(168, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "*MASSIVE FILE EXTRACTION*";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.llUseSamp);
            this.tabPage3.Controls.Add(this.tbNameskkdf2);
            this.tabPage3.Controls.Add(this.rbSelkkdf2);
            this.tabPage3.Controls.Add(this.rbMassivekkdf2);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.llViewkkdf2);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.tbCmdkkdf2);
            this.tabPage3.Controls.Add(this.bRunkkdf2);
            this.tabPage3.ImageKey = "Flag_redHS.png";
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(582, 327);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Use kkdf2\'s extractor";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbNameskkdf2
            // 
            this.tbNameskkdf2.Location = new System.Drawing.Point(369, 13);
            this.tbNameskkdf2.Multiline = true;
            this.tbNameskkdf2.Name = "tbNameskkdf2";
            this.tbNameskkdf2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbNameskkdf2.Size = new System.Drawing.Size(195, 118);
            this.tbNameskkdf2.TabIndex = 3;
            this.tbNameskkdf2.WordWrap = false;
            // 
            // rbSelkkdf2
            // 
            this.rbSelkkdf2.AutoSize = true;
            this.rbSelkkdf2.Location = new System.Drawing.Point(262, 14);
            this.rbSelkkdf2.Name = "rbSelkkdf2";
            this.rbSelkkdf2.Size = new System.Drawing.Size(101, 16);
            this.rbSelkkdf2.TabIndex = 2;
            this.rbSelkkdf2.TabStop = true;
            this.rbSelkkdf2.Text = "Your filenames:";
            this.rbSelkkdf2.UseVisualStyleBackColor = true;
            this.rbSelkkdf2.CheckedChanged += new System.EventHandler(this.rbSelkkdf2_CheckedChanged);
            // 
            // rbMassivekkdf2
            // 
            this.rbMassivekkdf2.AutoSize = true;
            this.rbMassivekkdf2.Checked = true;
            this.rbMassivekkdf2.Location = new System.Drawing.Point(46, 14);
            this.rbMassivekkdf2.Name = "rbMassivekkdf2";
            this.rbMassivekkdf2.Size = new System.Drawing.Size(210, 16);
            this.rbMassivekkdf2.TabIndex = 1;
            this.rbMassivekkdf2.TabStop = true;
            this.rbMassivekkdf2.Text = "MASSIVE FILE EXTRACTION MODE";
            this.rbMassivekkdf2.UseVisualStyleBackColor = true;
            this.rbMassivekkdf2.CheckedChanged += new System.EventHandler(this.rbSelkkdf2_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "Mode:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureBox1);
            this.tabPage4.Controls.Add(this.linkLabel2);
            this.tabPage4.Controls.Add(this.linkLabel3);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.linkLabel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(582, 327);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Web links";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.bUsekkdf2Sel);
            this.tabPage5.Controls.Add(this.bUsekkdf2);
            this.tabPage5.Controls.Add(this.bUseFalo);
            this.tabPage5.Controls.Add(this.bUseYaz0r);
            this.tabPage5.Controls.Add(this.bFind);
            this.tabPage5.Controls.Add(this.tbFind);
            this.tabPage5.Controls.Add(this.llSampFind);
            this.tabPage5.ImageKey = "FindHS.png";
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(582, 327);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Find file?";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // bUsekkdf2
            // 
            this.bUsekkdf2.AutoSize = true;
            this.bUsekkdf2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bUsekkdf2.Enabled = false;
            this.bUsekkdf2.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.bUsekkdf2.Location = new System.Drawing.Point(22, 222);
            this.bUsekkdf2.Name = "bUsekkdf2";
            this.bUsekkdf2.Padding = new System.Windows.Forms.Padding(3);
            this.bUsekkdf2.Size = new System.Drawing.Size(198, 28);
            this.bUsekkdf2.TabIndex = 5;
            this.bUsekkdf2.Text = "kkdf2\'s extractor may help you!";
            this.bUsekkdf2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bUsekkdf2.UseVisualStyleBackColor = true;
            this.bUsekkdf2.Click += new System.EventHandler(this.bUsekkdf2_Click);
            // 
            // bUseFalo
            // 
            this.bUseFalo.AutoSize = true;
            this.bUseFalo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bUseFalo.Enabled = false;
            this.bUseFalo.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.bUseFalo.Location = new System.Drawing.Point(22, 176);
            this.bUseFalo.Name = "bUseFalo";
            this.bUseFalo.Padding = new System.Windows.Forms.Padding(3);
            this.bUseFalo.Size = new System.Drawing.Size(192, 28);
            this.bUseFalo.TabIndex = 4;
            this.bUseFalo.Text = "Falo\'s extractor may help you!";
            this.bUseFalo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bUseFalo.UseVisualStyleBackColor = true;
            this.bUseFalo.Click += new System.EventHandler(this.bUseFalo_Click);
            // 
            // bUseYaz0r
            // 
            this.bUseYaz0r.AutoSize = true;
            this.bUseYaz0r.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bUseYaz0r.Enabled = false;
            this.bUseYaz0r.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.bUseYaz0r.Location = new System.Drawing.Point(22, 130);
            this.bUseYaz0r.Name = "bUseYaz0r";
            this.bUseYaz0r.Padding = new System.Windows.Forms.Padding(3);
            this.bUseYaz0r.Size = new System.Drawing.Size(222, 28);
            this.bUseYaz0r.TabIndex = 3;
            this.bUseYaz0r.Text = "Yaz0r\'s extractor v0.4 may help you!";
            this.bUseYaz0r.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bUseYaz0r.UseVisualStyleBackColor = true;
            this.bUseYaz0r.Click += new System.EventHandler(this.bUseYaz0r_Click);
            // 
            // bFind
            // 
            this.bFind.Image = global::KH2Exfe.Properties.Resources.FINDFILE16;
            this.bFind.Location = new System.Drawing.Point(268, 40);
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(75, 69);
            this.bFind.TabIndex = 2;
            this.bFind.Text = "Find!";
            this.bFind.UseVisualStyleBackColor = true;
            this.bFind.Click += new System.EventHandler(this.bFind_Click);
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(22, 65);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(227, 19);
            this.tbFind.TabIndex = 1;
            this.tbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyDown);
            // 
            // llSampFind
            // 
            this.llSampFind.AutoSize = true;
            this.llSampFind.LinkArea = new System.Windows.Forms.LinkArea(18, 22);
            this.llSampFind.Location = new System.Drawing.Point(22, 36);
            this.llSampFind.Name = "llSampFind";
            this.llSampFind.Size = new System.Drawing.Size(227, 17);
            this.llSampFind.TabIndex = 0;
            this.llSampFind.TabStop = true;
            this.llSampFind.Text = "Proceed, use like obj/W_EX010_10_TR.mdlx:";
            this.llSampFind.UseCompatibleTextRendering = true;
            this.llSampFind.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSampFind_LinkClicked);
            // 
            // il16
            // 
            this.il16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il16.ImageStream")));
            this.il16.TransparentColor = System.Drawing.Color.Transparent;
            this.il16.Images.SetKeyName(0, "Flag_blueHS.png");
            this.il16.Images.SetKeyName(1, "Flag_greenHS.png");
            this.il16.Images.SetKeyName(2, "Flag_redHS.png");
            this.il16.Images.SetKeyName(3, "FindHS.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llVC90);
            this.groupBox1.Controls.Add(this.llVC80);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "The extractors will require vcredist:";
            // 
            // llUseSamp
            // 
            this.llUseSamp.AutoSize = true;
            this.llUseSamp.LinkArea = new System.Windows.Forms.LinkArea(4, 17);
            this.llUseSamp.Location = new System.Drawing.Point(369, 144);
            this.llUseSamp.Name = "llUseSamp";
            this.llUseSamp.Size = new System.Drawing.Size(136, 17);
            this.llUseSamp.TabIndex = 9;
            this.llUseSamp.TabStop = true;
            this.llUseSamp.Text = "Use obj/W_EX010_10_TR?";
            this.llUseSamp.UseCompatibleTextRendering = true;
            this.llUseSamp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llUseSamp_LinkClicked);
            // 
            // bUsekkdf2Sel
            // 
            this.bUsekkdf2Sel.AutoSize = true;
            this.bUsekkdf2Sel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bUsekkdf2Sel.Enabled = false;
            this.bUsekkdf2Sel.Image = global::KH2Exfe.Properties.Resources.GoLtrHS;
            this.bUsekkdf2Sel.Location = new System.Drawing.Point(22, 268);
            this.bUsekkdf2Sel.Name = "bUsekkdf2Sel";
            this.bUsekkdf2Sel.Padding = new System.Windows.Forms.Padding(3);
            this.bUsekkdf2Sel.Size = new System.Drawing.Size(294, 28);
            this.bUsekkdf2Sel.TabIndex = 6;
            this.bUsekkdf2Sel.Text = "Otherwise, try kkdf2\'s extractor for filename mode";
            this.bUsekkdf2Sel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bUsekkdf2Sel.UseVisualStyleBackColor = true;
            this.bUsekkdf2Sel.Click += new System.EventHandler(this.bUsekkdf2Sel_Click);
            // 
            // EForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 505);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tbSrc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.llSrcDrives);
            this.Name = "EForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KH2/fm, extractor frontend by kkdf2";
            this.Load += new System.EventHandler(this.EForm_Load);
            this.Activated += new System.EventHandler(this.EForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSrc;
        private System.Windows.Forms.LinkLabel llSrcDrives;
        private System.Windows.Forms.ContextMenuStrip cmsDrives;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog fbdDst;
        private System.Windows.Forms.Button bRunYaz0r;
        private System.Windows.Forms.TextBox tbCmdYaz0r;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel llViewYaz0r;
        private System.Windows.Forms.Button bRunFalo;
        private System.Windows.Forms.TextBox tbCmdFalo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel llViewFalo;
        private System.Windows.Forms.LinkLabel llViewkkdf2;
        private System.Windows.Forms.Button bRunkkdf2;
        private System.Windows.Forms.TextBox tbCmdkkdf2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ToolTip ttURL;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbv04;
        private System.Windows.Forms.RadioButton rbv01;
        private System.Windows.Forms.LinkLabel llVC90;
        private System.Windows.Forms.LinkLabel llVC80;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList il16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.LinkLabel llSampFind;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Button bFind;
        private System.Windows.Forms.Button bUseYaz0r;
        private System.Windows.Forms.Button bUseFalo;
        private System.Windows.Forms.Button bUsekkdf2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbMassivekkdf2;
        private System.Windows.Forms.RadioButton rbSelkkdf2;
        private System.Windows.Forms.TextBox tbNameskkdf2;
        private System.Windows.Forms.LinkLabel llUseSamp;
        private System.Windows.Forms.Button bUsekkdf2Sel;
    }
}

