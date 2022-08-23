using mdlx2aset;
using mdlx2aset.Utils;

namespace khkh_xldMii_slim {
    public partial class Form1 : Form {

        private string mdlxPath = string.Empty;

        public Form1() {
            InitializeComponent();
        }

        private void btnOpenMdlx_Click(object sender, EventArgs e) {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\gamedev\Assets\Kingdom Hearts\test";
            openFileDialog.Filter = "MDLX files (*.mdlx)|*.mdlx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                //Get the path of specified file
                mdlxPath = openFileDialog.FileName;
                Log($"{mdlxPath} opened.");
            }
        }

        private void btnExportAset_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mdlxPath)) {
                Log("Open a valid MDLX file first.");
                return;
            }

            Log("Exporting ASET..");

            var success = MdlxConvert.ToAset(mdlxPath, slimDxPanel.Handle, Log);

            Log($"Export done. Success: {success}");
        }

        private void Log(string msg) {
            txtLog.AppendText(msg + "\r\n");
        }

        private void Log(ExportState state, ExportStatus status) {
            Log($"[{state}] {status.animName}");
        }
    }
}