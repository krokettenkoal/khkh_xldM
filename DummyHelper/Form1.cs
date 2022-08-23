using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DummyHelper {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        class Excell {
            public static string R1C1Toxy(int x, int y) {
                Debug.Assert(1 <= x && x <= 256);
                Debug.Assert(1 <= y && y <= 65535);
                string s = "";
                x--;
                if (x >= 26) {
                    int xx = (x / 26) - 1;
                    do {
                        s += (char)('A' + (xx % 26));
                        xx /= 26;
                    } while (xx != 0);
                    x %= 26;
                }
                s += (char)('A' + x);
                return s + y;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            string s = "=\"\"";
            for (int x = 0; x < 32; x++) {
                s += "&" + Excell.R1C1Toxy(2 + x, 4) + "&\",\"";
            }
            setText(s);
        }

        void setText(string text) {
            text = text.Replace("\r\n", "\n").Replace("\n", "\r\n");
            textBox1.Text = text;
            Clipboard.SetText(text);
        }

        private void button2_Click(object sender, EventArgs e) {
            string s = "=\"\"";
            for (int x = 0; x < 16; x++) {
                s += "&" + Excell.R1C1Toxy(2 + x, 12) + "&\",\"";
            }
            setText(s);

        }
    }
}