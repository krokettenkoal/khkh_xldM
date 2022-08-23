using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using khkh_xldM;
using System.IO;
using parseSEQD.Paxx;

namespace parseSEQD {
    public partial class XForm : Form {
        public XForm() {
            InitializeComponent();
        }

        //String fpbar = @"H:\EMU\Pcsx2-0.9.6\expa\obj\WORLD_POINT.a.fm";
        //String fpbar = @"H:\EMU\pcsx2-0.9.4\expa\obj_SAVE_POINT.a.fm";
        //String fpbar = @"H:\EMU\pcsx2-0.9.4\expa\obj_W_EX010.a.fm";
        String fpbar = @"H:\EMU\pcsx2-0.9.4\expa\event_effect_tt_event_101.pax";

        ReadPaxx rp = null;

        private void XForm_Load(object sender, EventArgs e) {
            LoadPaxx(fpbar);
        }

        void LoadPaxx(String fpbar) {
            if (fpbar.EndsWith(".pax")) {
                rp = new ReadPaxx(new MemoryStream(File.ReadAllBytes(fpbar), false));
            }
            else {
                using (FileStream fs = File.OpenRead(fpbar)) {
                    foreach (ReadBar.Barent ent in ReadBar.Explode(fs)) {
                        if (ent.k == 0x12) {
                            rp = new ReadPaxx(new MemoryStream(ent.bin, false));
                        }
                    }
                }
            }

            Console.Write("");
        }
    }


}