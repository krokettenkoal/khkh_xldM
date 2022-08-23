namespace khkh_xldMii
{
    partial class ExportProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.animProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelExportButton = new System.Windows.Forms.Button();
            this.frameProgressBar = new System.Windows.Forms.ProgressBar();
            this.frameProgressLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.animProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // animProgressBar
            // 
            this.animProgressBar.Location = new System.Drawing.Point(10, 34);
            this.animProgressBar.Name = "animProgressBar";
            this.animProgressBar.Size = new System.Drawing.Size(260, 15);
            this.animProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.animProgressBar.TabIndex = 0;
            // 
            // cancelExportButton
            // 
            this.cancelExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelExportButton.Location = new System.Drawing.Point(184, 88);
            this.cancelExportButton.Name = "cancelExportButton";
            this.cancelExportButton.Size = new System.Drawing.Size(86, 25);
            this.cancelExportButton.TabIndex = 3;
            this.cancelExportButton.Text = "&Cancel";
            this.cancelExportButton.UseVisualStyleBackColor = true;
            // 
            // frameProgressBar
            // 
            this.frameProgressBar.Location = new System.Drawing.Point(89, 65);
            this.frameProgressBar.Name = "frameProgressBar";
            this.frameProgressBar.Size = new System.Drawing.Size(181, 10);
            this.frameProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.frameProgressBar.TabIndex = 1;
            // 
            // frameProgressLabel
            // 
            this.frameProgressLabel.AutoSize = true;
            this.frameProgressLabel.Location = new System.Drawing.Point(11, 62);
            this.frameProgressLabel.Name = "frameProgressLabel";
            this.frameProgressLabel.Size = new System.Drawing.Size(35, 13);
            this.frameProgressLabel.TabIndex = 4;
            this.frameProgressLabel.Text = "label1";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(11, 94);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(35, 13);
            this.statusLabel.TabIndex = 6;
            this.statusLabel.Text = "label3";
            // 
            // animProgressLabel
            // 
            this.animProgressLabel.AutoSize = true;
            this.animProgressLabel.Location = new System.Drawing.Point(11, 11);
            this.animProgressLabel.Name = "animProgressLabel";
            this.animProgressLabel.Size = new System.Drawing.Size(35, 13);
            this.animProgressLabel.TabIndex = 7;
            this.animProgressLabel.Text = "label4";
            // 
            // ExportProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelExportButton;
            this.ClientSize = new System.Drawing.Size(284, 120);
            this.Controls.Add(this.animProgressLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.frameProgressLabel);
            this.Controls.Add(this.frameProgressBar);
            this.Controls.Add(this.cancelExportButton);
            this.Controls.Add(this.animProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportProgress";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exporting ASET...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar animProgressBar;
        public System.Windows.Forms.ProgressBar frameProgressBar;
        public System.Windows.Forms.Label frameProgressLabel;
        public System.Windows.Forms.Label statusLabel;
        public System.Windows.Forms.Label animProgressLabel;
        public System.Windows.Forms.Button cancelExportButton;
    }
}