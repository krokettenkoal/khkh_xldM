namespace khiiMapv {
    partial class Visf {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visf));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbExpBlenderpy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lCntVert = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.lCntTris = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslMdls = new System.Windows.Forms.ToolStripLabel();
            this.tspModels = new System.Windows.Forms.ToolStripSeparator();
            this.tslCollisions = new System.Windows.Forms.ToolStripLabel();
            this.tspCollisions = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPlane = new System.Windows.Forms.ToolStripButton();
            this.tsbEnableDoct = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRenderModel = new System.Windows.Forms.ToolStripButton();
            this.tsbRenderShadow = new System.Windows.Forms.ToolStripButton();
            this.flppos = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.eyeX = new System.Windows.Forms.NumericUpDown();
            this.eyeY = new System.Windows.Forms.NumericUpDown();
            this.eyeZ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.fov = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.yaw = new System.Windows.Forms.NumericUpDown();
            this.pitch = new System.Windows.Forms.NumericUpDown();
            this.roll = new System.Windows.Forms.NumericUpDown();
            this.cbFog = new System.Windows.Forms.CheckBox();
            this.cbVertexColor = new System.Windows.Forms.CheckBox();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.p1 = new hex04BinTrack.UC();
            this.timerBall = new System.Windows.Forms.Timer(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tslCoctHit = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tslDoctInfo = new System.Windows.Forms.ToolStripLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.numCo3 = new System.Windows.Forms.NumericUpDown();
            this.numCo2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numCo1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numVifpkt = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.flppos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eyeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fov)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yaw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roll)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVifpkt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 674);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "* Mouse wheel: Zoom\r\n* Left btn drag: Rotate\r\n* Right btn drag: Move forward/back" +
    "\r\n* Middle btn: Collision test";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExpBlenderpy,
            this.toolStripSeparator1,
            this.lCntVert,
            this.toolStripLabel1,
            this.lCntTris,
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.tslMdls,
            this.tspModels,
            this.tslCollisions,
            this.tspCollisions,
            this.tsbPlane,
            this.tsbEnableDoct,
            this.toolStripSeparator4,
            this.tsbRenderModel,
            this.tsbRenderShadow});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(885, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // tsbExpBlenderpy
            // 
            this.tsbExpBlenderpy.Image = ((System.Drawing.Image)(resources.GetObject("tsbExpBlenderpy.Image")));
            this.tsbExpBlenderpy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExpBlenderpy.Name = "tsbExpBlenderpy";
            this.tsbExpBlenderpy.Size = new System.Drawing.Size(153, 22);
            this.tsbExpBlenderpy.Text = "Export to blender script ";
            this.tsbExpBlenderpy.Click += new System.EventHandler(this.tsbExpBlenderpy_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lCntVert
            // 
            this.lCntVert.Name = "lCntVert";
            this.lCntVert.Size = new System.Drawing.Size(13, 22);
            this.lCntVert.Text = "0";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "vertices, ";
            // 
            // lCntTris
            // 
            this.lCntTris.Name = "lCntTris";
            this.lCntTris.Size = new System.Drawing.Size(13, 22);
            this.lCntTris.Text = "0";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(26, 22);
            this.toolStripLabel2.Text = "tris.";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslMdls
            // 
            this.tslMdls.Name = "tslMdls";
            this.tslMdls.Size = new System.Drawing.Size(49, 22);
            this.tslMdls.Text = "Models:";
            // 
            // tspModels
            // 
            this.tspModels.Name = "tspModels";
            this.tspModels.Size = new System.Drawing.Size(6, 25);
            // 
            // tslCollisions
            // 
            this.tslCollisions.Name = "tslCollisions";
            this.tslCollisions.Size = new System.Drawing.Size(60, 22);
            this.tslCollisions.Text = "Collisions:";
            // 
            // tspCollisions
            // 
            this.tspCollisions.Name = "tspCollisions";
            this.tspCollisions.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPlane
            // 
            this.tsbPlane.CheckOnClick = true;
            this.tsbPlane.Image = ((System.Drawing.Image)(resources.GetObject("tsbPlane.Image")));
            this.tsbPlane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlane.Name = "tsbPlane";
            this.tsbPlane.Size = new System.Drawing.Size(97, 22);
            this.tsbPlane.Text = "Display plane";
            this.tsbPlane.Click += new System.EventHandler(this.tsbPlane_Click);
            // 
            // tsbEnableDoct
            // 
            this.tsbEnableDoct.CheckOnClick = true;
            this.tsbEnableDoct.Image = ((System.Drawing.Image)(resources.GetObject("tsbEnableDoct.Image")));
            this.tsbEnableDoct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnableDoct.Name = "tsbEnableDoct";
            this.tsbEnableDoct.Size = new System.Drawing.Size(199, 22);
            this.tsbEnableDoct.Text = "Enable Occlusion Culling (DOCT)";
            this.tsbEnableDoct.Click += new System.EventHandler(this.tsbEnableDoct_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRenderModel
            // 
            this.tsbRenderModel.Checked = true;
            this.tsbRenderModel.CheckOnClick = true;
            this.tsbRenderModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbRenderModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRenderModel.Image = ((System.Drawing.Image)(resources.GetObject("tsbRenderModel.Image")));
            this.tsbRenderModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRenderModel.Name = "tsbRenderModel";
            this.tsbRenderModel.Size = new System.Drawing.Size(23, 22);
            this.tsbRenderModel.Text = "Render Model";
            this.tsbRenderModel.Click += new System.EventHandler(this.tsbRenderModel_Click);
            // 
            // tsbRenderShadow
            // 
            this.tsbRenderShadow.CheckOnClick = true;
            this.tsbRenderShadow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRenderShadow.Image = ((System.Drawing.Image)(resources.GetObject("tsbRenderShadow.Image")));
            this.tsbRenderShadow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRenderShadow.Name = "tsbRenderShadow";
            this.tsbRenderShadow.Size = new System.Drawing.Size(23, 22);
            this.tsbRenderShadow.Text = "Render Shadow";
            this.tsbRenderShadow.Click += new System.EventHandler(this.tsbRenderShadow_Click);
            // 
            // flppos
            // 
            this.flppos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flppos.Controls.Add(this.label2);
            this.flppos.Controls.Add(this.eyeX);
            this.flppos.Controls.Add(this.eyeY);
            this.flppos.Controls.Add(this.eyeZ);
            this.flppos.Controls.Add(this.label3);
            this.flppos.Controls.Add(this.fov);
            this.flppos.Controls.Add(this.label4);
            this.flppos.Controls.Add(this.yaw);
            this.flppos.Controls.Add(this.pitch);
            this.flppos.Controls.Add(this.roll);
            this.flppos.Controls.Add(this.cbFog);
            this.flppos.Controls.Add(this.cbVertexColor);
            this.flppos.Location = new System.Drawing.Point(12, 641);
            this.flppos.Name = "flppos";
            this.flppos.Size = new System.Drawing.Size(873, 30);
            this.flppos.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "eye (x y z)";
            // 
            // eyeX
            // 
            this.eyeX.Location = new System.Drawing.Point(69, 3);
            this.eyeX.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.eyeX.Minimum = new decimal(new int[] {
            64000,
            0,
            0,
            -2147483648});
            this.eyeX.Name = "eyeX";
            this.eyeX.Size = new System.Drawing.Size(59, 19);
            this.eyeX.TabIndex = 1;
            this.eyeX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eyeX.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // eyeY
            // 
            this.eyeY.Location = new System.Drawing.Point(134, 3);
            this.eyeY.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.eyeY.Minimum = new decimal(new int[] {
            64000,
            0,
            0,
            -2147483648});
            this.eyeY.Name = "eyeY";
            this.eyeY.Size = new System.Drawing.Size(59, 19);
            this.eyeY.TabIndex = 2;
            this.eyeY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eyeY.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.eyeY.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // eyeZ
            // 
            this.eyeZ.Location = new System.Drawing.Point(199, 3);
            this.eyeZ.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.eyeZ.Minimum = new decimal(new int[] {
            64000,
            0,
            0,
            -2147483648});
            this.eyeZ.Name = "eyeZ";
            this.eyeZ.Size = new System.Drawing.Size(59, 19);
            this.eyeZ.TabIndex = 3;
            this.eyeZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eyeZ.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "fov";
            // 
            // fov
            // 
            this.fov.Location = new System.Drawing.Point(291, 3);
            this.fov.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.fov.Name = "fov";
            this.fov.Size = new System.Drawing.Size(59, 19);
            this.fov.TabIndex = 5;
            this.fov.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fov.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.fov.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "rotation (yaw pitch roll)";
            // 
            // yaw
            // 
            this.yaw.Location = new System.Drawing.Point(487, 3);
            this.yaw.Maximum = new decimal(new int[] {
            36000,
            0,
            0,
            0});
            this.yaw.Minimum = new decimal(new int[] {
            36000,
            0,
            0,
            -2147483648});
            this.yaw.Name = "yaw";
            this.yaw.Size = new System.Drawing.Size(59, 19);
            this.yaw.TabIndex = 7;
            this.yaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.yaw.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // pitch
            // 
            this.pitch.Location = new System.Drawing.Point(552, 3);
            this.pitch.Maximum = new decimal(new int[] {
            36000,
            0,
            0,
            0});
            this.pitch.Minimum = new decimal(new int[] {
            36000,
            0,
            0,
            -2147483648});
            this.pitch.Name = "pitch";
            this.pitch.Size = new System.Drawing.Size(59, 19);
            this.pitch.TabIndex = 8;
            this.pitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pitch.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // roll
            // 
            this.roll.Location = new System.Drawing.Point(617, 3);
            this.roll.Maximum = new decimal(new int[] {
            36000,
            0,
            0,
            0});
            this.roll.Minimum = new decimal(new int[] {
            36000,
            0,
            0,
            -2147483648});
            this.roll.Name = "roll";
            this.roll.Size = new System.Drawing.Size(59, 19);
            this.roll.TabIndex = 9;
            this.roll.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.roll.ValueChanged += new System.EventHandler(this.eyeX_ValueChanged);
            // 
            // cbFog
            // 
            this.cbFog.AutoSize = true;
            this.cbFog.Checked = true;
            this.cbFog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFog.Location = new System.Drawing.Point(682, 3);
            this.cbFog.Name = "cbFog";
            this.cbFog.Size = new System.Drawing.Size(64, 16);
            this.cbFog.TabIndex = 10;
            this.cbFog.Text = "Use &fog";
            this.cbFog.UseVisualStyleBackColor = true;
            this.cbFog.CheckedChanged += new System.EventHandler(this.cbFog_CheckedChanged);
            // 
            // cbVertexColor
            // 
            this.cbVertexColor.AutoSize = true;
            this.cbVertexColor.Checked = true;
            this.cbVertexColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVertexColor.Location = new System.Drawing.Point(752, 3);
            this.cbVertexColor.Name = "cbVertexColor";
            this.cbVertexColor.Size = new System.Drawing.Size(97, 16);
            this.cbVertexColor.TabIndex = 11;
            this.cbVertexColor.Text = "Use vertex &clr";
            this.cbVertexColor.UseVisualStyleBackColor = true;
            this.cbVertexColor.CheckedChanged += new System.EventHandler(this.cbFog_CheckedChanged);
            // 
            // timerRun
            // 
            this.timerRun.Interval = 25;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 675);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 48);
            this.label5.TabIndex = 1;
            this.label5.Text = "* W: move forward\r\n* S: move back\r\n* A: move left\r\n* D: move right";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(353, 675);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 36);
            this.label6.TabIndex = 1;
            this.label6.Text = "* Shift: Move fast\r\n* Up: move up\r\n* Down: move down";
            // 
            // p1
            // 
            this.p1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.p1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p1.Location = new System.Drawing.Point(12, 88);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(861, 547);
            this.p1.TabIndex = 0;
            this.p1.UseTransparent = true;
            this.p1.Load += new System.EventHandler(this.p1_Load);
            this.p1.SizeChanged += new System.EventHandler(this.p1_SizeChanged);
            this.p1.Paint += new System.Windows.Forms.PaintEventHandler(this.p1_Paint);
            this.p1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.p1_KeyDown);
            this.p1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.p1_KeyUp);
            this.p1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p1_MouseDown);
            this.p1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.p1_MouseMove);
            this.p1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.p1_MouseUp);
            this.p1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.p1_PreviewKeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tslCoctHit,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.tslDoctInfo});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(885, 23);
            this.toolStrip2.TabIndex = 4;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(38, 15);
            this.toolStripLabel3.Text = "COCT:";
            // 
            // tslCoctHit
            // 
            this.tslCoctHit.Name = "tslCoctHit";
            this.tslCoctHit.Size = new System.Drawing.Size(16, 15);
            this.tslCoctHit.Text = "...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(39, 15);
            this.toolStripLabel4.Text = "DOCT:";
            // 
            // tslDoctInfo
            // 
            this.tslDoctInfo.Name = "tslDoctInfo";
            this.tslDoctInfo.Size = new System.Drawing.Size(16, 15);
            this.tslDoctInfo.Text = "...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "Render Co3:";
            // 
            // numCo3
            // 
            this.numCo3.Location = new System.Drawing.Point(14, 63);
            this.numCo3.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCo3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo3.Name = "numCo3";
            this.numCo3.Size = new System.Drawing.Size(63, 19);
            this.numCo3.TabIndex = 6;
            this.numCo3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCo3.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo3.ValueChanged += new System.EventHandler(this.numCo3_ValueChanged);
            // 
            // numCo2
            // 
            this.numCo2.Location = new System.Drawing.Point(102, 63);
            this.numCo2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCo2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo2.Name = "numCo2";
            this.numCo2.Size = new System.Drawing.Size(63, 19);
            this.numCo2.TabIndex = 8;
            this.numCo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCo2.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo2.ValueChanged += new System.EventHandler(this.numCo2_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Render Co2:";
            // 
            // numCo1
            // 
            this.numCo1.Location = new System.Drawing.Point(192, 63);
            this.numCo1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCo1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo1.Name = "numCo1";
            this.numCo1.Size = new System.Drawing.Size(63, 19);
            this.numCo1.TabIndex = 10;
            this.numCo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCo1.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numCo1.ValueChanged += new System.EventHandler(this.numCo1_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(188, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "Render Co1:";
            // 
            // numVifpkt
            // 
            this.numVifpkt.Location = new System.Drawing.Point(278, 63);
            this.numVifpkt.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numVifpkt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numVifpkt.Name = "numVifpkt";
            this.numVifpkt.Size = new System.Drawing.Size(63, 19);
            this.numVifpkt.TabIndex = 12;
            this.numVifpkt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numVifpkt.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numVifpkt.ValueChanged += new System.EventHandler(this.numVifpkt_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(274, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "Render VIFpkt:";
            // 
            // Visf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 732);
            this.Controls.Add(this.numVifpkt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numCo1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numCo2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numCo3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.flppos);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.p1);
            this.Name = "Visf";
            this.Text = "map viewer test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Visf_FormClosing);
            this.Load += new System.EventHandler(this.Visf_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.flppos.ResumeLayout(false);
            this.flppos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eyeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fov)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yaw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roll)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVifpkt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private hex04BinTrack.UC p1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExpBlenderpy;
        private System.Windows.Forms.FlowLayoutPanel flppos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown eyeX;
        private System.Windows.Forms.NumericUpDown eyeY;
        private System.Windows.Forms.NumericUpDown eyeZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fov;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown yaw;
        private System.Windows.Forms.NumericUpDown pitch;
        private System.Windows.Forms.NumericUpDown roll;
        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbFog;
        private System.Windows.Forms.CheckBox cbVertexColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lCntVert;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lCntTris;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslMdls;
        private System.Windows.Forms.Timer timerBall;
        private System.Windows.Forms.ToolStripLabel tslCollisions;
        private System.Windows.Forms.ToolStripSeparator tspModels;
        private System.Windows.Forms.ToolStripSeparator tspCollisions;
        private System.Windows.Forms.ToolStripButton tsbEnableDoct;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel tslDoctInfo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel tslCoctHit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPlane;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numCo3;
        private System.Windows.Forms.NumericUpDown numCo2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numCo1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numVifpkt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbRenderModel;
        private System.Windows.Forms.ToolStripButton tsbRenderShadow;
    }
}