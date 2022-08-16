using CrawlGen.Model.Overworld;
using CrawlGen.Out.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Out
{
    internal static class SettlementWriter
    {
        internal static void WriteTown(Settlement town, HTMLPage page)
        {
            page.WriteElem("h1", town.Name);

            page.WriteElem("p", $"Type: {town.Type}");
            // TODO: Rumour table
            // TODO: Specific buildings
            // TODO: Henchmen
            // TODO: Rulers, factions, etc
        }
    }
}
