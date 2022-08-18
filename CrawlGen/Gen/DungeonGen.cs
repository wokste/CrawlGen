using CrawlGen.Model.Dungeon;
using CrawlGen.Model.Overworld;
using System.Diagnostics;

namespace CrawlGen.Gen
{
    internal static class DungeonGen
    {
        public static Dungeon Make() {
            return new Dungeon(MakeMap());
        }

        public static DungeonMap MakeMap()
        {
            int w = 4; int h = 3;
            DungeonMap map = new();

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var room = new Room();

                    if (Rng.P(0.5))
                        room.Treasure.AddRange(TreasureGen.Make());

                    CarveRoom(map, room);
                }
            }

            Room GetRoom(int x, int y) => map.Rooms[y * w + x];

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h - 1; y++)
                    Connect(map, GetRoom(x, y), GetRoom(x, y + 1));

            for (int x = 0; x < w - 1; x++)
                for (int y = 0; y < h; y++)
                    Connect(map, GetRoom(x, y), GetRoom(x + 1, y));

            foreach (var room in map.Rooms)
                room.ChooseName();

            SortRooms(map);
            return map;

        }

        private static void CarveRoom(DungeonMap map, Room room)
        {
            map.AddRoom(room);
            // TODO: Carve room
        }

        private static void Connect(DungeonMap map, Room room1, Room room2)
        {
            Debug.Assert(map.Rooms.Contains(room1));
            Debug.Assert(map.Rooms.Contains(room2));

            room1.Connections.Add(room2);
            room2.Connections.Add(room1);
            // TODO: Is this the best way or should this be in CrawlGen.Gen?
        }

        private static void SortRooms(DungeonMap map)
        {
            // TODO: Sort based on location
            for (int i = 0; i < map.Rooms.Count; i++)
                map.Rooms[i].ID = i + 1;
        }
    }
}
