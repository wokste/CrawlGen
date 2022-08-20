using CrawlGen.Model.Encounters;
using CrawlGen.Writers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Writers
{
    internal class EncounterWriter
    {
        internal static void WriteEncounter(Encounter enc, HTMLPage page)
        {
            page.WriteElem("h3", $"{enc.Monster.Name} ({enc.Na})");
            WriteMonster(enc.Na, enc.Monster, page);
        }


        internal static void WriteMonster(int count, Monster m, HTMLPage page)
        {
            //page.WriteElem("h4", monster.Name);
            using var p = page.MakeDom("p");

            page.WriteKeyValue("AC", $"{m.AC} [{19 - m.AC}],");
            page.WriteKeyValue("HD", $"{m.HD},");
            page.WriteKeyValue("Att", $"{m.Attacks[0]},");
            page.WriteKeyValue("THACO", $"{m.THACO} [{19 - m.THACO}],");
            page.WriteKeyValue("MV", $"{m.Movement},");
            page.WriteKeyValue("SV", $"D{m.Saves[0]} W{m.Saves[1]} P{m.Saves[2]} B{m.Saves[3]} S{m.Saves[4]},");
            page.WriteKeyValue("ML", $"{m.Morale},");
            page.WriteKeyValue("XP", $"{m.XP}");

        }
    }
}
