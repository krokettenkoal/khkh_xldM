using NLog;
using parsePAX_v2.Models;
using parseSEQDv2.Utils;
using SlimDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xe.BinaryMapper;

namespace parsePAX_v2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        static Logger log = LogManager.GetCurrentClassLogger();

        private void Form1_Load(object sender, EventArgs e) {
        }

        private void ReadPax(string file) {
            log.Info("ReadPax: {0}", file);

            var bar = File.ReadAllBytes(file);
            foreach (var entry in ReadBar.Explode(new MemoryStream(bar))) {
                if (entry.key == 0x12) {
                    log.Info("id: {0}", entry.id);
                    File.WriteAllBytes(entry.id + ".bin", entry.bin);

                    var stream = new MemoryStream(entry.bin);
                    var pax = Pax.ReadObject(stream, 0);
                }
            }
        }
    }
}