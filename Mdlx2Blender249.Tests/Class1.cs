using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mdlx2Blender249.Tests {
    public class Class1 {
        private string KH2DumpRoot => @"C:\gamedev\Assets\Kingdom Hearts\_dump_full\KH2";
        private string ExportRoot => Path.Combine(TestContext.CurrentContext.WorkDirectory, "export");

        private static string Mkdir(string dir, params string[] paths) {
            var lastDir = Path.Combine(new string[] { dir }.Concat(paths).ToArray());
            Directory.CreateDirectory(lastDir);
            return lastDir;
        }

        [Test]
        public void P_EX110_WithMset() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\P_EX100.mdlx"),
                Path.Combine(KH2DumpRoot, @"obj\P_EX100.mset"),
                Path.Combine(Mkdir(ExportRoot, "P_EX110_WithMset"), @"model.py")
            );
        }

        [Test]
        public void P_EX100() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\P_EX100.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "P_EX100"), @"model.py")
            );
        }

        [Test]
        public void H_EX500_BTLF() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_EX500_BTLF.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_EX500_BTLF"), @"model.py")
            );
        }

        [Test]
        public void H_EX740() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_EX740.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_EX740"), @"model.py")
            );
        }

        [Test]
        public void H_ZZ010() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_ZZ010.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_ZZ010"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex2")]
        public void B_AL100_2ND() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\B_AL100_2ND.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "B_AL100_2ND"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex2")]
        public void N_EX550() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\N_EX550.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "N_EX550"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex3")]
        public void P_EX110_NPC_PAJAMAS() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\P_EX110_NPC_PAJAMAS.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "P_EX110_NPC_PAJAMAS"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex4")]
        public void F_TR570() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\F_TR570.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "F_TR570"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex5")]
        public void H_MU050() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_MU050.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_MU050"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex5")]
        public void H_AL030() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_AL030.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_AL030"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex6")]
        //[Category("Generate_0_0_0")] //fixed
        public void H_TR010() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_TR010.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_TR010"), @"model.py")
            );
        }

        [Test]
        [Category("MaxWeightsPerVertex7")]
        public void H_BB050_TSURU() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_BB050_TSURU.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_BB050_TSURU"), @"model.py")
            );
        }

        [Test]
        public void H_EX500() {
            new Program().Run(
                Path.Combine(KH2DumpRoot, @"obj\H_EX500.mdlx"),
                null,
                Path.Combine(Mkdir(ExportRoot, "H_EX500"), @"model.py")
            );
        }









    }
}
