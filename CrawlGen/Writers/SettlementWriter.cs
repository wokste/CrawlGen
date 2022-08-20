using CrawlGen.Model.Overworld;
using CrawlGen.Writers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Writers
{
    internal static class SettlementWriter
    {
        internal static void WriteTown(Settlement town, HTMLPage page)
        {
            page.WriteElem("h1", town.Name, town.Anchor.Id);

            page.WriteElem("p", $"Type: {town.Type}");
            page.WriteElem("p", $"Alignment: {town.Alignment}");
            // TODO: Rumour table
            // TODO: Specific buildings
            // TODO: Henchmen
            // TODO: Rulers, factions, etc
        }
    }
}
