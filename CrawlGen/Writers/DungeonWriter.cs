using CrawlGen.Model.Dungeon;
using CrawlGen.Model.Encounters;
using CrawlGen.Model.Overworld;
using CrawlGen.Writers.Utils;

namespace CrawlGen.Writers
{
    public static class DungeonWriter
    {
        public static void WriteRoom(Room room1, HTMLPage page)
        {
            page.WriteElem("h2", room1.ToString(), room1.Anchor.Id);

            {
                using var ul = page.MakeDom("ul");
                foreach (Passage conn in room1.Passages)
                {
                    using var li = page.MakeDom("li");
                    Room room2 = conn.GetOtherRoom(room1);

                    page.WriteElem("b", ChooseDir(room1, room2));
                    var connType = (conn.Door != null) ? $"{conn.Door}" : "passage"; // TODO: Get from the connection
                    page.Write($"A {connType} leading to the ");
                    page.WriteElem("a", $"{room2.Name}", room2.Anchor.Href);
                }
            }

            if (room1.Encounter is Encounter enc) {
                EncounterWriter.WriteEncounter(enc, page);
            }

            if (room1.Treasure.Any())
                page.WriteElem("p", $"Treasure: {string.Join(", ", room1.Treasure)}");

            // TODO: Doors
            // TODO: Proper treasure
        }

        private static string ChooseDir(Room r1, Room r2) => (r2.Loc - r1.Loc).ToDir();

        public static void WriteDungeon(Dungeon dungeon, HTMLPage page)
        {
            page.WriteElem("h1", dungeon.Name, dungeon.Anchor.Id);
            foreach (var room in dungeon.Map.Rooms)
                WriteRoom(room, page);
        }
    }
}
