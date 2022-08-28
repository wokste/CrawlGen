﻿using CrawlGen.Model.Encounters;
using CrawlGen.Writers.Utils;

namespace CrawlGen.Writers;

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
        page.WriteKeyValue("Att", $"{m.Attacks[0]},"); // TODO: Fix
        page.WriteKeyValue("THACO", $"{m.THACO} [{19 - m.THACO}],");
        page.WriteKeyValue("MV", $"{m.Movement[0]},"); // TODO: Fix
        page.WriteKeyValue("SV", m.Saves);
        page.WriteKeyValue("ML", $"{m.Morale},");
        page.WriteKeyValue("XP", $"{m.XP}");

    }
}
