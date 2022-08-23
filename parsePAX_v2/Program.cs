using NLog;
using parsePAX_v2.Models;
using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xe.BinaryMapper;

namespace parsePAX_v2
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            {

                BinaryMapping.SetMemberLengthMapping<Pax>(nameof(Pax.Dat1List), (o, m) => o.CountDat1);
                BinaryMapping.SetMemberLengthMapping<DPX>(nameof(DPX.DPDRefList), (o, m) => o.CountDat2);
                BinaryMapping.SetMemberLengthMapping<DPD>(nameof(DPD.EffectsGroupOffsets), (o, m) => o.n31);
                BinaryMapping.SetMemberLengthMapping<DPD>(nameof(DPD.Dat32Offsets), (o, m) => o.n32);
                BinaryMapping.SetMemberLengthMapping<DPD>(nameof(DPD.Dat33Offsets), (o, m) => o.n33);
                BinaryMapping.SetMemberLengthMapping<DPD>(nameof(DPD.Dat34Offsets), (o, m) => o.n34);
                BinaryMapping.SetMemberLengthMapping<DPD>(nameof(DPD.Dat35Offsets), (o, m) => o.n35);
                BinaryMapping.SetMapping<Vector4>(
                    new BinaryMapping.Mapping
                    {
                        Reader = io => new Vector4(
                            x: io.Reader.ReadSingle(),
                            y: io.Reader.ReadSingle(),
                            z: io.Reader.ReadSingle(),
                            w: io.Reader.ReadSingle()
                        ),
                    }
                );
                BinaryMapping.SetMapping<NullTerminatedInt32List>(
                    new BinaryMapping.Mapping
                    {
                        Reader = io =>
                        {
                            var list = new NullTerminatedInt32List();
                            while (true)
                            {
                                var value = io.Reader.ReadInt32();
                                if (value == 0)
                                {
                                    break;
                                }
                                list.Add(value);
                            }
                            return list;
                        }
                    }
                );
                BinaryMapping.SetMemberLengthMapping<Effect>(nameof(Effect.EffectCommandList), (o, m) => o.EffectCommandCount);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if true
            {
                Logger log = LogManager.GetCurrentClassLogger();

                var paxList = new List<Pax>();

                var _fileList = Directory.GetFiles(@"H:\Proj\CollectAllPAX_\bin\Debug\PAX", "*.pax");
                var fileList = new string[] {
                    @"H:\Proj\CollectAllPAX_\bin\Debug\PAX\WORLD_POINT.a.fm.f_ex.0.pax"
                };

                foreach (var paxFile in fileList)
                {
                    log.Info("pax: {0}", paxFile);

                    var stream = new MemoryStream(File.ReadAllBytes(paxFile), false);
                    try
                    {
                        var pax = Pax.ReadObject(stream, 0);
                        paxList.Add(pax);
                    }
                    catch (EndOfStreamException)
                    {
                        log.Error("EndOfStreamException: {0}", paxFile);
                    }
                }

                if (false)
                {
                    var lines = paxList
                        .SelectMany(it => it.DPX.DPDRefList)
                        .SelectMany(it => it.DPD.EffectsGroupList)
                        .SelectMany(it => it.EffectList)
                        .SelectMany(it => it.EffectCommandList)
                        .Select(it => it.ToString())
                        .ToArray()
                        .OrderBy(it => it)
                        .ToArray()
                        ;
                    File.WriteAllText("CommandStats.txt", string.Join("\r\n", lines));
                }
                if (false)
                {
                    var lines = paxList
                        .SelectMany(it => it.DPX.DPDRefList)
                        .SelectMany(it => it.DPD.EffectsGroupList)
                        .SelectMany(it => it.EffectList)
                        .SelectMany(it => it.EffectCommandList)
                        .GroupBy(it => it.EffectType)
                        .OrderByDescending(group => group.Count())
                        .Select(group => $"{group.Count(),7:#,##0} {group.Key}")
                        .ToArray()
                        ;
                    File.WriteAllText("CommandUseCount.txt", string.Join("\r\n", lines));
                }
                if (false)
                {
                    var groupByEffectType = paxList
                        .SelectMany(it => it.DPX.DPDRefList)
                        .SelectMany(it => it.DPD.EffectsGroupList)
                        .SelectMany(it => it.EffectList)
                        .SelectMany(it => it.EffectCommandList)
                        .GroupBy(it => it.EffectType)
                        .ToArray()
                        ;
                    foreach (var set in groupByEffectType)
                    {
                        File.WriteAllLines(
                            $"{set.Key}.txt",
                            set
                                .SelectMany(it => it.ChunksList.Select(chunk => $"{it.ParamLength} = {Bin2Hex(chunk)}"))
                                .GroupBy(it => it)
                                .OrderByDescending(group => group.Count())
                                .Select(group => $"{group.Key} ×{group.Count():#,##0}")
                                .ToArray()
                        );
                    }
                }
            }
#else
            Application.Run(new Form1());
#endif
        }

        static string Bin2Hex(byte[] bin)
        {
            var writer = new StringBuilder(3 * bin.Length);
            for (int x = 0; x < bin.Length; x++)
            {
                if (x != 0)
                {
                    writer.Append(" ");
                }
                writer.AppendFormat("{0:X2}", bin[x]);
            }
            return writer.ToString();
        }
    }
}
