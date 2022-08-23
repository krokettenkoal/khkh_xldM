namespace hex04BinTrack {
    partial class ProtForm2Dev {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtForm2Dev));
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderVIFchip = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeaderFLcol = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new hex04BinTrack.UC();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTex = new System.Windows.Forms.RadioButton();
            this.radioButtonWire = new System.Windows.Forms.RadioButton();
            this.buttonSelGeneric = new System.Windows.Forms.Button();
            this.buttonSelShallow = new System.Windows.Forms.Button();
            this.hScrollBarTick = new System.Windows.Forms.HScrollBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Name = "label1";
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderVIFchip});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // columnHeaderVIFchip
            // 
            resources.ApplyResources(this.columnHeaderVIFchip, "columnHeaderVIFchip");
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Name = "label3";
            // 
            // listView2
            // 
            resources.ApplyResources(this.listView2, "listView2");
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFLcol});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.HideSelection = false;
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderFLcol
            // 
            resources.ApplyResources(this.columnHeaderFLcol, "columnHeaderFLcol");
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.Animation;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Name = "panel1";
            this.panel1.UseTransparent = true;
            this.panel1.Load += new System.EventHandler(this.panel1_Load);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButtonTex);
            this.groupBox1.Controls.Add(this.radioButtonWire);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radioButtonTex
            // 
            resources.ApplyResources(this.radioButtonTex, "radioButtonTex");
            this.radioButtonTex.Name = "radioButtonTex";
            this.radioButtonTex.UseVisualStyleBackColor = true;
            this.radioButtonTex.CheckedChanged += new System.EventHandler(this.radioButtonWire_CheckedChanged);
            // 
            // radioButtonWire
            // 
            resources.ApplyResources(this.radioButtonWire, "radioButtonWire");
            this.radioButtonWire.Checked = true;
            this.radioButtonWire.Name = "radioButtonWire";
            this.radioButtonWire.TabStop = true;
            this.radioButtonWire.UseVisualStyleBackColor = true;
            this.radioButtonWire.CheckedChanged += new System.EventHandler(this.radioButtonWire_CheckedChanged);
            // 
            // buttonSelGeneric
            // 
            resources.ApplyResources(this.buttonSelGeneric, "buttonSelGeneric");
            this.buttonSelGeneric.Name = "buttonSelGeneric";
            this.buttonSelGeneric.UseVisualStyleBackColor = true;
            this.buttonSelGeneric.Click += new System.EventHandler(this.buttonSelGeneric_Click);
            // 
            // buttonSelShallow
            // 
            resources.ApplyResources(this.buttonSelShallow, "buttonSelShallow");
            this.buttonSelShallow.Name = "buttonSelShallow";
            this.buttonSelShallow.UseVisualStyleBackColor = true;
            this.buttonSelShallow.Click += new System.EventHandler(this.buttonSelShallow_Click);
            // 
            // hScrollBarTick
            // 
            resources.ApplyResources(this.hScrollBarTick, "hScrollBarTick");
            this.hScrollBarTick.Maximum = 99;
            this.hScrollBarTick.Name = "hScrollBarTick";
            this.hScrollBarTick.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarTick_Scroll);
            // 
            // ProtForm2Dev
            // 
            resources.ApplyResources(this, "$this");
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBarTick);
            this.Controls.Add(this.buttonSelShallow);
            this.Controls.Add(this.buttonSelGeneric);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "ProtForm2Dev";
            this.Load += new System.EventHandler(this.ProtForm2Dev_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderVIFchip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeaderFLcol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTex;
        private System.Windows.Forms.RadioButton radioButtonWire;
        private System.Windows.Forms.Button buttonSelGeneric;
        private System.Windows.Forms.Button buttonSelShallow;
        private System.Windows.Forms.HScrollBar hScrollBarTick;
    }
}