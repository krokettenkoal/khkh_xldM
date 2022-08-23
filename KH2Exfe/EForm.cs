using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using KH2Exfe.Properties;
using System.Threading;
using System.Text.RegularExpressions;

namespace KH2Exfe {
    public partial class EForm : Form {
        public EForm() {
            InitializeComponent();
        }

        private void EForm_Load(object sender, EventArgs e) {
            rbv01_CheckedChanged(sender, e);
            tbCmdFalo.Text = Resources.Cmd_Falo;
            tbCmdkkdf2.Text = Resources.Cmd_kkdf2;

            foreach (TabPage tp in tabControl1.TabPages) tabControl1.SelectedTab = tp;
            tabControl1.SelectedIndex = 0;
        }

        private void llSrcDrives_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            cmsDrives.Items.Clear();

            foreach (String drv in Environment.GetLogicalDrives()) {
                String adrv = drv;

                Bitmap pic = null;
                {
                    // http://pinvoke.net/default.aspx/shell32/SHGetFileInfo.html
                    SHFILEINFO fi = new SHFILEINFO();
                    IntPtr res = SHGetFileInfo(adrv, 0, ref fi, Convert.ToUInt32(Marshal.SizeOf(fi)), (uint)(SHGFI.LargeIcon | SHGFI.Icon));
                    if (res != IntPtr.Zero) {
                        using (Icon ico = Icon.FromHandle(fi.hIcon)) {
                            pic = ico.ToBitmap();
                            DestroyIcon(fi.hIcon);
                        }
                    }
                }

                ToolStripMenuItem tsi = (ToolStripMenuItem)cmsDrives.Items.Add(drv, pic, delegate {
                    tbSrc.Text = adrv.TrimEnd('\\');

                    EP.SetError(llSrcDrives, null);
                });

                SynchronizationContext Sync = SynchronizationContext.Current;

                ThreadPool.QueueUserWorkItem(delegate {
                    try {
                        if (Array.TrueForAll<String>("SYSTEM.CNF,SLPM_666.75,KH2.IDX,KH2.IMG".Split(','), (Predicate<String>)delegate(string fn) { return File.Exists(Path.Combine(adrv, fn)); })) {
                            Sync.Send(delegate { tsi.Text += " <KH2fm>"; tsi.Font = new Font(tsi.Font, FontStyle.Bold); }, null);
                            return;
                        }
                        if (Array.TrueForAll<String>("SYSTEM.CNF,KH2.IDX,KH2.IMG".Split(','), (Predicate<String>)delegate(string fn) { return File.Exists(Path.Combine(adrv, fn)); })) {
                            Sync.Send(delegate { tsi.Text += " <KH2>"; tsi.Font = new Font(tsi.Font, FontStyle.Bold); }, null);
                            return;
                        }
                        Sync.Send(delegate { tsi.Text += " <non KH2>"; tsi.ForeColor = Color.FromKnownColor(KnownColor.GrayText); }, null);
                    }
                    catch (Exception) { }
                }, null);
            }

            cmsDrives.Show(llSrcDrives, new Point(llSrcDrives.Width, 0));
        }

        [DllImport("User32.dll")]
        static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct SHFILEINFO {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [Flags]
        enum SHGFI : int {
            /// <summary>get icon</summary>
            Icon = 0x000000100,
            /// <summary>get display name</summary>
            DisplayName = 0x000000200,
            /// <summary>get type name</summary>
            TypeName = 0x000000400,
            /// <summary>get attributes</summary>
            Attributes = 0x000000800,
            /// <summary>get icon location</summary>
            IconLocation = 0x000001000,
            /// <summary>return exe type</summary>
            ExeType = 0x000002000,
            /// <summary>get system icon index</summary>
            SysIconIndex = 0x000004000,
            /// <summary>put a link overlay on icon</summary>
            LinkOverlay = 0x000008000,
            /// <summary>show icon in selected state</summary>
            Selected = 0x000010000,
            /// <summary>get only specified attributes</summary>
            Attr_Specified = 0x000020000,
            /// <summary>get large icon</summary>
            LargeIcon = 0x000000000,
            /// <summary>get small icon</summary>
            SmallIcon = 0x000000001,
            /// <summary>get open icon</summary>
            OpenIcon = 0x000000002,
            /// <summary>get shell size icon</summary>
            ShellIconSize = 0x000000004,
            /// <summary>pszPath is a pidl</summary>
            PIDL = 0x000000008,
            /// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = 0x000000010,
            /// <summary>apply the appropriate overlays</summary>
            AddOverlays = 0x000000020,
            /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = 0x000000040,
        }

        String DirYaz0r {
            get {
                if (rbv04.Checked)
                    return Path.Combine(Application.StartupPath, "yaz0r04");
                else
                    return Path.Combine(Application.StartupPath, "yaz0r");
            }
        }

        private void llViewYaz0r_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try {
                Process.Start("explorer.exe", DirYaz0r);
            }
            catch (Exception) { MessageBox.Show(this, "Sorry. failed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void bRunYaz0r_Click(object sender, EventArgs e) {
            if (!Verify()) return;
            String fpbat = Path.Combine(DirYaz0r, "export.bat");
            File.WriteAllText(fpbat, "SET SRC=" + tbSrc.Text + "\r\n"
                + tbCmdYaz0r.Text
                , Encoding.Default
                );

            ProcessStartInfo psi = new ProcessStartInfo(fpbat);
            psi.WorkingDirectory = DirYaz0r;
            Process p = Process.Start(psi);
            Ut.LockControl(p, (Control)sender);
        }

        String DirFalo { get { return Path.Combine(Application.StartupPath, "Falo"); } }

        private void bRunFalo_Click(object sender, EventArgs e) {
            if (!Verify()) return;
            String fpbat = Path.Combine(DirFalo, "export.bat");
            File.WriteAllText(fpbat, "SET SRC=" + tbSrc.Text + "\r\n"
                + tbCmdFalo.Text
                , Encoding.Default
                );

            ProcessStartInfo psi = new ProcessStartInfo(fpbat);
            psi.WorkingDirectory = DirFalo;
            Process p = Process.Start(psi);
            Ut.LockControl(p, (Control)sender);
        }

        private bool Verify() {
            EP.SetError(llSrcDrives, null);

            if (String.IsNullOrEmpty(tbSrc.Text) || (!Directory.Exists(tbSrc.Text) && !Directory.Exists(Path.GetDirectoryName(tbSrc.Text)))) {
                EP.SetError(llSrcDrives, "Select KH2 source!");
                return false;
            }

            return true;
        }

        private void llViewFalo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try {
                Process.Start("explorer.exe", DirFalo);
            }
            catch (Exception) { MessageBox.Show(this, "Sorry. failed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        String Dirkkdf2 { get { return Path.Combine(Application.StartupPath, "kkdf2"); } }

        private void llViewkkdf2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try {
                Process.Start("explorer.exe", Dirkkdf2);
            }
            catch (Exception) { MessageBox.Show(this, "Sorry. failed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void bRunkkdf2_Click(object sender, EventArgs e) {
            if (!Verify()) return;
            String fpbat = Path.Combine(Dirkkdf2, "export.bat");
            File.WriteAllText(fpbat, "SET SRC=" + tbSrc.Text + "\r\n"
                + "SET NAMES=" + Regex.Replace(tbNameskkdf2.Text, "\\s+", " ", RegexOptions.Singleline).Trim() + "\r\n"
                + tbCmdkkdf2.Text
                , Encoding.Default
                );

            ProcessStartInfo psi = new ProcessStartInfo(fpbat);
            psi.WorkingDirectory = Dirkkdf2;
            Process p = Process.Start(psi);
            Ut.LockControl(p, (Control)sender);
        }

        class Ut {
            internal static void LockControl(Process p, Control c) {
                if (c != null) {
                    c.Enabled = false;

                    SynchronizationContext Sync = SynchronizationContext.Current;

                    new Thread((ThreadStart)delegate {
                        p.WaitForExit();

                        Sync.Send(delegate { if (!c.IsDisposed) c.Enabled = true; }, null);
                    }).Start();
                }
            }
        }

        private void llVisitURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            String url = ttURL.GetToolTip((Control)sender);
            if (!String.IsNullOrEmpty(url)) Process.Start(url);
        }

        private void rbv01_CheckedChanged(object sender, EventArgs e) {
            if (rbv04.Checked)
                tbCmdYaz0r.Text = Resources.Cmd_yaz0r04;
            else
                tbCmdYaz0r.Text = Resources.Cmd_yaz0r;
        }

        [DllImport("msi.dll", SetLastError = true)]
        static extern INSTALLSTATE MsiQueryProductState(string product);

        enum INSTALLSTATE {
            INSTALLSTATE_NOTUSED = -7,  // component disabled
            INSTALLSTATE_BADCONFIG = -6,  // configuration data corrupt
            INSTALLSTATE_INCOMPLETE = -5,  // installation suspended or in progress
            INSTALLSTATE_SOURCEABSENT = -4,  // run from source, source is unavailable
            INSTALLSTATE_MOREDATA = -3,  // return buffer overflow
            INSTALLSTATE_INVALIDARG = -2,  // invalid function argument
            INSTALLSTATE_UNKNOWN = -1,  // unrecognized product or feature
            INSTALLSTATE_BROKEN = 0,  // broken
            INSTALLSTATE_ADVERTISED = 1,  // advertised feature
            INSTALLSTATE_REMOVED = 1,  // component being removed (action state, not settable)
            INSTALLSTATE_ABSENT = 2,  // uninstalled (or action state absent but clients remain)
            INSTALLSTATE_LOCAL = 3,  // installed on local drive
            INSTALLSTATE_SOURCE = 4,  // run from source, CD or net
            INSTALLSTATE_DEFAULT = 5,  // use default, local or source
        }

        private void EForm_Activated(object sender, EventArgs e) {
            llVC80.Enabled = !(MsiQueryProductState("{710F4C1C-CC18-4C49-8CBF-51240C89A1A2}") == INSTALLSTATE.INSTALLSTATE_DEFAULT);
            llVC90.Enabled = !(MsiQueryProductState("{1F1C2DFC-2D24-3E06-BCB8-725134ADF989}") == INSTALLSTATE.INSTALLSTATE_DEFAULT);
        }

        private void llSampFind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            tbFind.Text = "obj/W_EX010_10_TR.mdlx";
        }

        private void bFind_Click(object sender, EventArgs e) {
            bUseYaz0r.Enabled = Resources.Outyaz0r04.IndexOf(tbFind.Text, StringComparison.InvariantCultureIgnoreCase) >= 0;
            bUseFalo.Enabled = Resources.OutFalo.IndexOf(tbFind.Text, StringComparison.InvariantCultureIgnoreCase) >= 0;
            bUsekkdf2.Enabled = Resources.Outkkdf2.IndexOf(tbFind.Text, StringComparison.InvariantCultureIgnoreCase) >= 0;
            bUsekkdf2Sel.Enabled = tbFind.TextLength != 0;
        }

        private void bUseYaz0r_Click(object sender, EventArgs e) {
            tabControl1.SelectedTab = tabPage1;
            rbv04.Checked = true;
            bRunYaz0r.Select();
        }

        private void bUseFalo_Click(object sender, EventArgs e) {
            tabControl1.SelectedTab = tabPage2;
            bRunFalo.Select();
        }

        private void bUsekkdf2_Click(object sender, EventArgs e) {
            tabControl1.SelectedTab = tabPage3;
            bRunkkdf2.Select();
        }

        private void tbFind_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Return)
                bFind.PerformClick();
        }

        private void rbSelkkdf2_CheckedChanged(object sender, EventArgs e) {
            tbCmdkkdf2.Text = rbSelkkdf2.Checked ? Resources.Cmd_kkdf2Sel : Resources.Cmd_kkdf2;
        }

        private void llUseSamp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            tbNameskkdf2.Text = "obj/W_EX010_10_TR.mdlx\r\nobj/W_EX010_10_TR.a.fm\r\nobj/W_EX010_10_TR.apdx";
        }

        private void bUsekkdf2Sel_Click(object sender, EventArgs e) {
#if false
            SortedDictionary<String, String> names = new SortedDictionary<string, string>();
            String s = "";
            foreach (String rows in new string[] { Resources.OutFalo, Resources.Outkkdf2, Resources.Outyaz0r04 }) {
                foreach (String k in rows.Replace("\r\n", "\n").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
                    if (names.ContainsKey(k)) continue;
                    names[k] = null;
                    if (k.IndexOf(tbFind.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        s += k + "\r\n";
                }
            }
#endif

            tabControl1.SelectedTab = tabPage3;
            rbSelkkdf2.Checked = true;
            tbNameskkdf2.Text = tbFind.Text;
            bRunkkdf2.Select();

        }
    }
}