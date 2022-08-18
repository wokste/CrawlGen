using CrawlGen.Model.Overworld;
using CrawlGen.Out.Utils;
using CrawlGen.Grid;

namespace CrawlGen.Out
{
    internal static class OverworldWriter
    {
        public static void WriteWrold(World world, HTMLPage page)
        {
            WriteLocations(world, page);

            foreach (var town in world.Towns)
                SettlementWriter.WriteTown(town, page);

            foreach (var dungeon in world.Dungeons)
                DungeonWriter.WriteDungeon(dungeon, page);

        }

        public static void WriteLocations(World map, HTMLPage page)
        {
            page.WriteElem("h1", "Overworld"); // TODO: Name
            using var ul = page.MakeDom("ul");
            foreach (var feature in map.Features)
            {
                using var li = page.MakeDom("li");

                page.WriteElem("a", $"{feature.Name}", feature.Anchor.Href);
            }
        }
    }
}
