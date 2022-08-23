using mdlx2aset;
using mdlx2aset.Utils;

namespace khkh_xldMii_slim {
    public partial class Form1 : Form {

        private string mdlxPath = string.Empty;

        public Form1() {
            InitializeComponent();
        }

        #region Event handlers
        /// <summary>
        /// Method being called when the 'Open MDLX' button is clicked
        /// </summary>
        /// <param name="sender">Reference to the sender object</param>
        /// <param name="e">Click event arguments</param>
        private void btnOpenMdlx_Click(object sender, EventArgs e) {
            //  Show an 'Open file' dialog to the user for them to select an MDLX file
            using OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = @"C:\gamedev\Assets\Kingdom Hearts\test";
            openFileDialog.Filter = "MDLX files (*.mdlx)|*.mdlx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                //  Get the path of specified file
                mdlxPath = openFileDialog.FileName;

                Log($"{mdlxPath} opened.");
            }
        }
        /// <summary>
        /// Method being called when the 'Export ASET' button is clicked
        /// </summary>
        /// <param name="sender">Reference to the sender object</param>
        /// <param name="e">Click event arguments</param>
        private void btnExportAset_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mdlxPath)) {
                Log("Open a valid MDLX file first.");
                return;
            }

            Log("Exporting ASET..");

            //  Export an ASET animation file from the specified MDLX file.
            //  A corresponding MSET file is required in the same directory as the MDLX file.
            //  The resulting ASET file is saved to the MDLX/MSET source directory.
            var success = MdlxConvert.ToAset(mdlxPath, Log);

            Log($"Export done. Success: {success}");
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Print a text line to the log panel
        /// </summary>
        /// <param name="msg">The message to print</param>
        private void Log(string msg) {
            txtLog.AppendText(msg + "\r\n");
        }
        /// <summary>
        /// Print the export progress consisting of an ExportState and an ExportStatus to the log panel.
        /// </summary>
        /// <param name="state">State (stage) of the current export operation</param>
        /// <param name="status">Status (anim/frame info) of the current export operation</param>
        private void Log(ExportState state, ExportStatus status) {
            Log($"[{state}] {status.animName}");
        }
        #endregion
    }
}