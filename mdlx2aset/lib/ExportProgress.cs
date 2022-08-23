namespace mdlx2aset.Utils {
    public class ExportProgress {
        private bool _cancel;
        public bool CancellationPending {
            get {
                var c = _cancel;
                _cancel = false;
                return c; 
            }

            private set => _cancel = value;
        }

        public event Action<ExportState, ExportStatus>? OnProgress;
        public void Update(ExportState state, ExportStatus status)=> OnProgress?.Invoke(state, status);
        public void Cancel() {
            CancellationPending = true;
        }
    }
}
