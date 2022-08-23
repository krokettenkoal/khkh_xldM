namespace khkh_xldMii_slim {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnOpenMdlx = new System.Windows.Forms.Button();
            this.btnExportAset = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.slimDxPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnOpenMdlx
            // 
            this.btnOpenMdlx.Location = new System.Drawing.Point(76, 93);
            this.btnOpenMdlx.Name = "btnOpenMdlx";
            this.btnOpenMdlx.Size = new System.Drawing.Size(239, 101);
            this.btnOpenMdlx.TabIndex = 0;
            this.btnOpenMdlx.Text = "Open MDLX...";
            this.btnOpenMdlx.UseVisualStyleBackColor = true;
            this.btnOpenMdlx.Click += new System.EventHandler(this.btnOpenMdlx_Click);
            // 
            // btnExportAset
            // 
            this.btnExportAset.Location = new System.Drawing.Point(433, 93);
            this.btnExportAset.Name = "btnExportAset";
            this.btnExportAset.Size = new System.Drawing.Size(266, 101);
            this.btnExportAset.TabIndex = 1;
            this.btnExportAset.Text = "Export ASET";
            this.btnExportAset.UseVisualStyleBackColor = true;
            this.btnExportAset.Click += new System.EventHandler(this.btnExportAset_Click);
            // 
            // txtLog
            // 
            this.txtLog.Enabled = false;
            this.txtLog.Location = new System.Drawing.Point(76, 231);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(623, 164);
            this.txtLog.TabIndex = 2;
            // 
            // slimDxPanel
            // 
            this.slimDxPanel.Location = new System.Drawing.Point(726, 395);
            this.slimDxPanel.Name = "slimDxPanel";
            this.slimDxPanel.Size = new System.Drawing.Size(0, 0);
            this.slimDxPanel.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.slimDxPanel);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnExportAset);
            this.Controls.Add(this.btnOpenMdlx);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnOpenMdlx;
        private Button btnExportAset;
        private TextBox txtLog;
        private Panel slimDxPanel;
    }
}