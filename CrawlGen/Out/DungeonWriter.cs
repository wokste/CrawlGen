using CrawlGen.Model.Dungeon;
using CrawlGen.Model.Overworld;
using CrawlGen.Out.Utils;

namespace CrawlGen.Out
{
    public static class DungeonWriter
    {

        public static void WriteRoom(Room room, HTMLPage page)
        {
            page.WriteElem("h2", room.ToString());

            if (room.Treasure.Any())
                page.WriteElem("p", $"Treasure: {string.Join(", ", room.Treasure)}");
            // TODO: Monsters
            // TODO: Doors
            // TODO: Proper treasure
        }

        public static void WriteDungeon(Dungeon dungeon, HTMLPage page)
        {
            page.WriteElem("h1", dungeon.Name);
            foreach (var room in dungeon.Map.Rooms)
                WriteRoom(room, page);
        }
    }
}
