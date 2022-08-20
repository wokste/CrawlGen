using CrawlGen.Model.Dungeon;
using CrawlGen.Model.Encounters;
using CrawlGen.Model.Overworld;
using CrawlGen.Writers.Utils;

namespace CrawlGen.Writers
{
    public static class DungeonWriter
    {

        public static void WriteRoom(Room room, HTMLPage page)
        {
            page.WriteElem("h2", room.ToString(), room.Anchor.Id);

            {
                using var ul = page.MakeDom("ul");
                foreach (var conn in room.Connections)
                {
                    using var li = page.MakeDom("li");
                    page.WriteElem("b", ChooseDir(room, conn));
                    var connType = "door"; // TODO: Get from the connection
                    page.Write($"A {connType} leading to the ");
                    page.WriteElem("a", $"{conn.Name}", conn.Anchor.Href);
                }
            }

            if (room.Encounter is Encounter enc) {
                EncounterWriter.WriteEncounter(enc, page);
            }

            if (room.Treasure.Any())
                page.WriteElem("p", $"Treasure: {string.Join(", ", room.Treasure)}");

            // TODO: Doors
            // TODO: Proper treasure
        }

        private static string ChooseDir(Room r1, Room r2)
        {
            // TODO: Proper implementation
            var delta = r1.ID - r2.ID;

            if (delta < 0)
                return "West";
            else
                return "East";
        }

        public static void WriteDungeon(Dungeon dungeon, HTMLPage page)
        {
            page.WriteElem("h1", dungeon.Name, dungeon.Anchor.Id);
            foreach (var room in dungeon.Map.Rooms)
                WriteRoom(room, page);
        }
    }
}
