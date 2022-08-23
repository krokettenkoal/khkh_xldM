namespace khkh_xldMii {
    partial class FormII {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormII));
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderRxxx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxPerspective = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepCur = new System.Windows.Forms.CheckBox();
            this.checkBoxAsPNG = new System.Windows.Forms.CheckBox();
            this.radioButton60fps = new System.Windows.Forms.RadioButton();
            this.radioButton30fps = new System.Windows.Forms.RadioButton();
            this.radioButton10fps = new System.Windows.Forms.RadioButton();
            this.checkBoxLooks = new System.Windows.Forms.CheckBox();
            this.buttonBC = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxKeys = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxAutoFill = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRec = new System.Windows.Forms.CheckBox();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAutoNext = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoNext = new System.Windows.Forms.CheckBox();
            this.checkBoxAnim = new System.Windows.Forms.CheckBox();
            this.numericUpDownTick = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.numnear = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numfar = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numfov = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numx = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numy = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numz = new System.Windows.Forms.NumericUpDown();
            this.labelHelpKeys = new System.Windows.Forms.Label();
            this.panel1 = new hex04BinTrack.UC();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTick)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numnear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numz)).BeginInit();
            this.SuspendLayout();
            // 
            // timerTick
            // 
            this.timerTick.Interval = 16;
            this.timerTick.Tick += new System.EventHandler(this.timerTick_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.labelHelpKeys);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Size = new System.Drawing.Size(1072, 900);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(4, 5);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxPerspective);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxKeepCur);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxAsPNG);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton60fps);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton30fps);
            this.splitContainer2.Panel2.Controls.Add(this.radioButton10fps);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxLooks);
            this.splitContainer2.Panel2.Controls.Add(this.buttonBC);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxKeys);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoFill);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoRec);
            this.splitContainer2.Panel2.Controls.Add(this.numericUpDownFrame);
            this.splitContainer2.Panel2.Controls.Add(this.numericUpDownStep);
            this.splitContainer2.Panel2.Controls.Add(this.numericUpDownAutoNext);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoNext);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxAnim);
            this.splitContainer2.Panel2.Controls.Add(this.numericUpDownTick);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(234, 890);
            this.splitContainer2.SplitterDistance = 500;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRxxx});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 22);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(234, 478);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeaderRxxx
            // 
            this.columnHeaderRxxx.Text = "Motion";
            this.columnHeaderRxxx.Width = 188;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Anim";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(195, 185);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 43);
            this.button2.TabIndex = 21;
            this.button2.Text = "E&xport ASET";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxPerspective
            // 
            this.checkBoxPerspective.AutoSize = true;
            this.checkBoxPerspective.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxPerspective.Location = new System.Drawing.Point(8, 491);
            this.checkBoxPerspective.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPerspective.Name = "checkBoxPerspective";
            this.checkBoxPerspective.Size = new System.Drawing.Size(279, 24);
            this.checkBoxPerspective.TabIndex = 19;
            this.checkBoxPerspective.Text = "Perspective &view (3D Ripper likes it)";
            this.checkBoxPerspective.UseVisualStyleBackColor = true;
            this.checkBoxPerspective.CheckedChanged += new System.EventHandler(this.checkBoxPerspective_CheckedChanged);
            // 
            // checkBoxKeepCur
            // 
            this.checkBoxKeepCur.AutoSize = true;
            this.checkBoxKeepCur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxKeepCur.Location = new System.Drawing.Point(8, 205);
            this.checkBoxKeepCur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxKeepCur.Name = "checkBoxKeepCur";
            this.checkBoxKeepCur.Size = new System.Drawing.Size(172, 24);
            this.checkBoxKeepCur.TabIndex = 9;
            this.checkBoxKeepCur.Text = "&Loop current motion";
            this.checkBoxKeepCur.UseVisualStyleBackColor = true;
            // 
            // checkBoxAsPNG
            // 
            this.checkBoxAsPNG.AutoSize = true;
            this.checkBoxAsPNG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAsPNG.Location = new System.Drawing.Point(8, 363);
            this.checkBoxAsPNG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAsPNG.Name = "checkBoxAsPNG";
            this.checkBoxAsPNG.Size = new System.Drawing.Size(140, 24);
            this.checkBoxAsPNG.TabIndex = 14;
            this.checkBoxAsPNG.Text = "Use &png format";
            this.checkBoxAsPNG.UseVisualStyleBackColor = true;
            // 
            // radioButton60fps
            // 
            this.radioButton60fps.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton60fps.AutoSize = true;
            this.radioButton60fps.Checked = true;
            this.radioButton60fps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton60fps.Location = new System.Drawing.Point(258, 440);
            this.radioButton60fps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton60fps.Name = "radioButton60fps";
            this.radioButton60fps.Size = new System.Drawing.Size(61, 32);
            this.radioButton60fps.TabIndex = 18;
            this.radioButton60fps.TabStop = true;
            this.radioButton60fps.Text = "&60fps";
            this.radioButton60fps.UseVisualStyleBackColor = true;
            this.radioButton60fps.CheckedChanged += new System.EventHandler(this.radioButtonAny_CheckedChanged);
            // 
            // radioButton30fps
            // 
            this.radioButton30fps.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton30fps.AutoSize = true;
            this.radioButton30fps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton30fps.Location = new System.Drawing.Point(182, 440);
            this.radioButton30fps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton30fps.Name = "radioButton30fps";
            this.radioButton30fps.Size = new System.Drawing.Size(61, 32);
            this.radioButton30fps.TabIndex = 17;
            this.radioButton30fps.Text = "&30fps";
            this.radioButton30fps.UseVisualStyleBackColor = true;
            this.radioButton30fps.CheckedChanged += new System.EventHandler(this.radioButtonAny_CheckedChanged);
            // 
            // radioButton10fps
            // 
            this.radioButton10fps.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton10fps.AutoSize = true;
            this.radioButton10fps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton10fps.Location = new System.Drawing.Point(105, 440);
            this.radioButton10fps.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton10fps.Name = "radioButton10fps";
            this.radioButton10fps.Size = new System.Drawing.Size(61, 32);
            this.radioButton10fps.TabIndex = 16;
            this.radioButton10fps.Text = "&10fps";
            this.radioButton10fps.UseVisualStyleBackColor = true;
            this.radioButton10fps.CheckedChanged += new System.EventHandler(this.radioButtonAny_CheckedChanged);
            // 
            // checkBoxLooks
            // 
            this.checkBoxLooks.AutoSize = true;
            this.checkBoxLooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxLooks.Location = new System.Drawing.Point(8, 400);
            this.checkBoxLooks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxLooks.Name = "checkBoxLooks";
            this.checkBoxLooks.Size = new System.Drawing.Size(213, 24);
            this.checkBoxLooks.TabIndex = 15;
            this.checkBoxLooks.Text = "&Enable face looks change";
            this.checkBoxLooks.UseVisualStyleBackColor = true;
            this.checkBoxLooks.CheckedChanged += new System.EventHandler(this.checkBoxLooks_CheckedChanged);
            // 
            // buttonBC
            // 
            this.buttonBC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBC.Location = new System.Drawing.Point(8, 526);
            this.buttonBC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBC.Name = "buttonBC";
            this.buttonBC.Size = new System.Drawing.Size(318, 57);
            this.buttonBC.TabIndex = 20;
            this.buttonBC.Text = "&Bind Controller";
            this.buttonBC.UseVisualStyleBackColor = true;
            this.buttonBC.Click += new System.EventHandler(this.buttonBC_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(226, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "&DEB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxKeys
            // 
            this.checkBoxKeys.AutoSize = true;
            this.checkBoxKeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxKeys.Location = new System.Drawing.Point(8, 245);
            this.checkBoxKeys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxKeys.Name = "checkBoxKeys";
            this.checkBoxKeys.Size = new System.Drawing.Size(172, 24);
            this.checkBoxKeys.TabIndex = 10;
            this.checkBoxKeys.Text = "Show short cut &keys";
            this.checkBoxKeys.UseVisualStyleBackColor = true;
            this.checkBoxKeys.CheckedChanged += new System.EventHandler(this.checkBoxKeys_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 325);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Next &file no ...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "&Cur tick";
            // 
            // checkBoxAutoFill
            // 
            this.checkBoxAutoFill.AutoSize = true;
            this.checkBoxAutoFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAutoFill.Location = new System.Drawing.Point(8, 168);
            this.checkBoxAutoFill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoFill.Name = "checkBoxAutoFill";
            this.checkBoxAutoFill.Size = new System.Drawing.Size(143, 24);
            this.checkBoxAutoFill.TabIndex = 8;
            this.checkBoxAutoFill.Text = "Auto fill max &tick";
            this.checkBoxAutoFill.UseVisualStyleBackColor = true;
            this.checkBoxAutoFill.CheckedChanged += new System.EventHandler(this.checkBoxAutoFill_CheckedChanged);
            // 
            // checkBoxAutoRec
            // 
            this.checkBoxAutoRec.AutoSize = true;
            this.checkBoxAutoRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAutoRec.Location = new System.Drawing.Point(8, 282);
            this.checkBoxAutoRec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoRec.Name = "checkBoxAutoRec";
            this.checkBoxAutoRec.Size = new System.Drawing.Size(246, 24);
            this.checkBoxAutoRec.TabIndex = 11;
            this.checkBoxAutoRec.Text = "Capture &screen shot per frame";
            this.checkBoxAutoRec.UseVisualStyleBackColor = true;
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownFrame.Location = new System.Drawing.Point(220, 318);
            this.numericUpDownFrame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownFrame.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownFrame.Name = "numericUpDownFrame";
            this.numericUpDownFrame.Size = new System.Drawing.Size(105, 28);
            this.numericUpDownFrame.TabIndex = 13;
            this.numericUpDownFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFrame.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownStep
            // 
            this.numericUpDownStep.DecimalPlaces = 2;
            this.numericUpDownStep.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownStep.Location = new System.Drawing.Point(226, 82);
            this.numericUpDownStep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownStep.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStep.Name = "numericUpDownStep";
            this.numericUpDownStep.Size = new System.Drawing.Size(105, 28);
            this.numericUpDownStep.TabIndex = 5;
            this.numericUpDownStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownAutoNext
            // 
            this.numericUpDownAutoNext.DecimalPlaces = 2;
            this.numericUpDownAutoNext.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAutoNext.Location = new System.Drawing.Point(226, 126);
            this.numericUpDownAutoNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownAutoNext.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownAutoNext.Name = "numericUpDownAutoNext";
            this.numericUpDownAutoNext.Size = new System.Drawing.Size(105, 28);
            this.numericUpDownAutoNext.TabIndex = 7;
            this.numericUpDownAutoNext.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownAutoNext.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBoxAutoNext
            // 
            this.checkBoxAutoNext.AutoSize = true;
            this.checkBoxAutoNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAutoNext.Location = new System.Drawing.Point(8, 132);
            this.checkBoxAutoNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoNext.Name = "checkBoxAutoNext";
            this.checkBoxAutoNext.Size = new System.Drawing.Size(180, 24);
            this.checkBoxAutoNext.TabIndex = 6;
            this.checkBoxAutoNext.Text = "&Next motion on tick ...";
            this.checkBoxAutoNext.UseVisualStyleBackColor = true;
            // 
            // checkBoxAnim
            // 
            this.checkBoxAnim.AutoSize = true;
            this.checkBoxAnim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxAnim.Location = new System.Drawing.Point(82, 86);
            this.checkBoxAnim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAnim.Name = "checkBoxAnim";
            this.checkBoxAnim.Size = new System.Drawing.Size(119, 24);
            this.checkBoxAnim.TabIndex = 4;
            this.checkBoxAnim.Text = "&Auto step by";
            this.checkBoxAnim.UseVisualStyleBackColor = true;
            this.checkBoxAnim.CheckedChanged += new System.EventHandler(this.checkBoxAnim_CheckedChanged);
            // 
            // numericUpDownTick
            // 
            this.numericUpDownTick.DecimalPlaces = 2;
            this.numericUpDownTick.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownTick.Location = new System.Drawing.Point(82, 42);
            this.numericUpDownTick.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownTick.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownTick.Name = "numericUpDownTick";
            this.numericUpDownTick.Size = new System.Drawing.Size(114, 28);
            this.numericUpDownTick.TabIndex = 3;
            this.numericUpDownTick.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTick.ValueChanged += new System.EventHandler(this.numericUpDownTick_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Control";
            this.label3.DoubleClick += new System.EventHandler(this.label3_DoubleClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.numnear);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.numfar);
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Controls.Add(this.numfov);
            this.flowLayoutPanel1.Controls.Add(this.label8);
            this.flowLayoutPanel1.Controls.Add(this.numx);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.numy);
            this.flowLayoutPanel1.Controls.Add(this.label10);
            this.flowLayoutPanel1.Controls.Add(this.numz);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(508, 575);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(296, 290);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.Visible = false;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "near";
            // 
            // numnear
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numnear, true);
            this.numnear.Location = new System.Drawing.Point(53, 5);
            this.numnear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numnear.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numnear.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numnear.Name = "numnear";
            this.numnear.Size = new System.Drawing.Size(90, 26);
            this.numnear.TabIndex = 1;
            this.numnear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numnear.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numnear.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "far";
            // 
            // numfar
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numfar, true);
            this.numfar.Location = new System.Drawing.Point(40, 41);
            this.numfar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numfar.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numfar.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numfar.Name = "numfar";
            this.numfar.Size = new System.Drawing.Size(90, 26);
            this.numfar.TabIndex = 3;
            this.numfar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numfar.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numfar.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "fov";
            // 
            // numfov
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numfov, true);
            this.numfov.Location = new System.Drawing.Point(42, 77);
            this.numfov.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numfov.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numfov.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numfov.Name = "numfov";
            this.numfov.Size = new System.Drawing.Size(90, 26);
            this.numfov.TabIndex = 11;
            this.numfov.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numfov.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numfov.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 116);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "x";
            // 
            // numx
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numx, true);
            this.numx.Location = new System.Drawing.Point(28, 113);
            this.numx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numx.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numx.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numx.Name = "numx";
            this.numx.Size = new System.Drawing.Size(90, 26);
            this.numx.TabIndex = 5;
            this.numx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numx.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 152);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 6;
            this.label9.Text = "y";
            // 
            // numy
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numy, true);
            this.numy.Location = new System.Drawing.Point(28, 149);
            this.numy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numy.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numy.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numy.Name = "numy";
            this.numy.Size = new System.Drawing.Size(90, 26);
            this.numy.TabIndex = 7;
            this.numy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numy.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 188);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "z";
            // 
            // numz
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.numz, true);
            this.numz.Location = new System.Drawing.Point(29, 185);
            this.numz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numz.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numz.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numz.Name = "numz";
            this.numz.Size = new System.Drawing.Size(90, 26);
            this.numz.TabIndex = 9;
            this.numz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numz.Value = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numz.ValueChanged += new System.EventHandler(this.numnear_ValueChanged);
            // 
            // labelHelpKeys
            // 
            this.labelHelpKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHelpKeys.AutoSize = true;
            this.labelHelpKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelHelpKeys.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelpKeys.Location = new System.Drawing.Point(9, 638);
            this.labelHelpKeys.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelpKeys.Name = "labelHelpKeys";
            this.labelHelpKeys.Size = new System.Drawing.Size(369, 191);
            this.labelHelpKeys.TabIndex = 3;
            this.labelHelpKeys.Text = resources.GetString("labelHelpKeys.Text");
            this.labelHelpKeys.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 868);
            this.panel1.TabIndex = 2;
            this.panel1.UseTransparent = true;
            this.panel1.Load += new System.EventHandler(this.panel1_Load);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.panel1_KeyDown);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "3D viewport";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // FormII
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 900);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormII";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "khkh_xldM ][";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormII_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormII_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormII_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTick)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numnear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private hex04BinTrack.UC panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderRxxx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAnim;
        private System.Windows.Forms.NumericUpDown numericUpDownTick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerTick;
        private System.Windows.Forms.NumericUpDown numericUpDownStep;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoNext;
        private System.Windows.Forms.CheckBox checkBoxAutoNext;
        private System.Windows.Forms.CheckBox checkBoxAutoRec;
        private System.Windows.Forms.NumericUpDown numericUpDownFrame;
        private System.Windows.Forms.CheckBox checkBoxAutoFill;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxKeys;
        private System.Windows.Forms.Label labelHelpKeys;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBC;
        private System.Windows.Forms.CheckBox checkBoxLooks;
        private System.Windows.Forms.RadioButton radioButton60fps;
        private System.Windows.Forms.RadioButton radioButton30fps;
        private System.Windows.Forms.RadioButton radioButton10fps;
        private System.Windows.Forms.CheckBox checkBoxAsPNG;
        private System.Windows.Forms.CheckBox checkBoxKeepCur;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numnear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numfar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numz;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numfov;
        private System.Windows.Forms.CheckBox checkBoxPerspective;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

