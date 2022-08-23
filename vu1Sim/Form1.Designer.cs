namespace vu1Sim {
    partial class Form1vu1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1vu1));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderOff = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLo = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHi = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDesc = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelTick = new System.Windows.Forms.Label();
            this.buttonRunUntill = new System.Windows.Forms.Button();
            this.labelDump1 = new System.Windows.Forms.Label();
            this.labelDump2 = new System.Windows.Forms.Label();
            this.tbHashMicro = new System.Windows.Forms.TextBox();
            this.hexVwer2 = new Readmset.HexVwer();
            this.hexVwer1 = new Readmset.HexVwer();
            this.buttonSelSeta = new System.Windows.Forms.Button();
            this.openFileDialogfs = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(56, 12);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderOff,
            this.columnHeaderLo,
            this.columnHeaderHi,
            this.columnHeaderDesc});
            this.listView1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(14, 24);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(560, 367);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView1_RetrieveVirtualItem);
            // 
            // columnHeaderOff
            // 
            this.columnHeaderOff.Text = "Offset";
            // 
            // columnHeaderLo
            // 
            this.columnHeaderLo.Text = "Lo";
            this.columnHeaderLo.Width = 175;
            // 
            // columnHeaderHi
            // 
            this.columnHeaderHi.Text = "U";
            this.columnHeaderHi.Width = 180;
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "Description";
            this.columnHeaderDesc.Width = 116;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "blank.PNG");
            this.imageList1.Images.SetKeyName(1, "key.PNG");
            this.imageList1.Images.SetKeyName(2, "curs.PNG");
            this.imageList1.Images.SetKeyName(3, "here.PNG");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Step";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(14, 426);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(397, 97);
            this.listBox1.TabIndex = 5;
            this.listBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBox1_KeyPress);
            // 
            // labelTick
            // 
            this.labelTick.AutoSize = true;
            this.labelTick.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelTick.Location = new System.Drawing.Point(277, 394);
            this.labelTick.Name = "labelTick";
            this.labelTick.Size = new System.Drawing.Size(47, 12);
            this.labelTick.TabIndex = 6;
            this.labelTick.Text = "tick: 0";
            // 
            // buttonRunUntill
            // 
            this.buttonRunUntill.Location = new System.Drawing.Point(93, 397);
            this.buttonRunUntill.Name = "buttonRunUntill";
            this.buttonRunUntill.Size = new System.Drawing.Size(75, 23);
            this.buttonRunUntill.TabIndex = 7;
            this.buttonRunUntill.Text = "Run un&till";
            this.buttonRunUntill.UseVisualStyleBackColor = true;
            this.buttonRunUntill.Click += new System.EventHandler(this.buttonRunUntill_Click);
            // 
            // labelDump1
            // 
            this.labelDump1.AutoSize = true;
            this.labelDump1.Location = new System.Drawing.Point(10, 526);
            this.labelDump1.Name = "labelDump1";
            this.labelDump1.Size = new System.Drawing.Size(11, 12);
            this.labelDump1.TabIndex = 9;
            this.labelDump1.Text = "...";
            // 
            // labelDump2
            // 
            this.labelDump2.AutoSize = true;
            this.labelDump2.Location = new System.Drawing.Point(10, 604);
            this.labelDump2.Name = "labelDump2";
            this.labelDump2.Size = new System.Drawing.Size(11, 12);
            this.labelDump2.TabIndex = 11;
            this.labelDump2.Text = "...";
            // 
            // tbHashMicro
            // 
            this.tbHashMicro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHashMicro.Location = new System.Drawing.Point(338, 9);
            this.tbHashMicro.Name = "tbHashMicro";
            this.tbHashMicro.ReadOnly = true;
            this.tbHashMicro.Size = new System.Drawing.Size(236, 12);
            this.tbHashMicro.TabIndex = 12;
            this.tbHashMicro.Text = "md5=00000000111111112222222233333333";
            this.tbHashMicro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hexVwer2
            // 
            this.hexVwer2.AntiFlick = true;
            this.hexVwer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexVwer2.ByteWidth = 16;
            this.hexVwer2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hexVwer2.Location = new System.Drawing.Point(10, 619);
            this.hexVwer2.Name = "hexVwer2";
            this.hexVwer2.OffDelta = 0;
            this.hexVwer2.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hexVwer2.Size = new System.Drawing.Size(457, 60);
            this.hexVwer2.TabIndex = 10;
            this.hexVwer2.UnitPg = 512;
            // 
            // hexVwer1
            // 
            this.hexVwer1.AntiFlick = true;
            this.hexVwer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexVwer1.ByteWidth = 16;
            this.hexVwer1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hexVwer1.Location = new System.Drawing.Point(10, 541);
            this.hexVwer1.Name = "hexVwer1";
            this.hexVwer1.OffDelta = 0;
            this.hexVwer1.PgScroll = Readmset.PgScrollType.ScreenSizeBased;
            this.hexVwer1.Size = new System.Drawing.Size(457, 60);
            this.hexVwer1.TabIndex = 8;
            this.hexVwer1.UnitPg = 512;
            // 
            // buttonSelSeta
            // 
            this.buttonSelSeta.Location = new System.Drawing.Point(174, 397);
            this.buttonSelSeta.Name = "buttonSelSeta";
            this.buttonSelSeta.Size = new System.Drawing.Size(97, 23);
            this.buttonSelSeta.TabIndex = 13;
            this.buttonSelSeta.Text = "Open fileset ...";
            this.buttonSelSeta.UseVisualStyleBackColor = true;
            this.buttonSelSeta.Click += new System.EventHandler(this.buttonSelSeta_Click);
            // 
            // openFileDialogfs
            // 
            this.openFileDialogfs.DefaultExt = "txt";
            this.openFileDialogfs.FileName = "H:\\Proj\\khkh_xldM\\MEMO\\vu1Sim.fileset.txt";
            // 
            // Form1vu1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 689);
            this.Controls.Add(this.buttonSelSeta);
            this.Controls.Add(this.tbHashMicro);
            this.Controls.Add(this.labelDump2);
            this.Controls.Add(this.hexVwer2);
            this.Controls.Add(this.labelDump1);
            this.Controls.Add(this.hexVwer1);
            this.Controls.Add(this.buttonRunUntill);
            this.Controls.Add(this.labelTick);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.linkLabel1);
            this.Name = "Form1vu1";
            this.Text = "vu1Sim";
            this.Load += new System.EventHandler(this.Form1vu1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderOff;
        private System.Windows.Forms.ColumnHeader columnHeaderLo;
        private System.Windows.Forms.ColumnHeader columnHeaderHi;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelTick;
        private System.Windows.Forms.Button buttonRunUntill;
        private Readmset.HexVwer hexVwer1;
        private System.Windows.Forms.Label labelDump1;
        private System.Windows.Forms.Label labelDump2;
        private Readmset.HexVwer hexVwer2;
        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private System.Windows.Forms.TextBox tbHashMicro;
        private System.Windows.Forms.Button buttonSelSeta;
        private System.Windows.Forms.OpenFileDialog openFileDialogfs;
    }
}

