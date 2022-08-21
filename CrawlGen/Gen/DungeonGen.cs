using CrawlGen.Grid;
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
            DungeonMap map = new();
            var proto = Dungeons.ProtoDungeon.Generate();

            // Make the rooms
            foreach (var (x, y) in proto.GetSquaresAndAssignIDs())
                map.Rooms.Add(new Room(new PointD(x, y)));

            // Connect stuff
            foreach (var (id1, id2) in proto.GetPassages())
                Connect(map, map.Rooms[id1], map.Rooms[id2]);

            // Add encounters
            foreach (var room in map.Rooms)
                if (Rng.D(6) <= 2)
                    room.Encounter = EncounterGen.Make();

            // Add treasure
            foreach (var room in map.Rooms)
            {
                int treasureOdds = room.Encounter != null ? 3 : 1;

                if (Rng.D(6) <= treasureOdds)
                    room.Treasure.AddRange(TreasureGen.Make());
            }

            // Choose names
            foreach (var room in map.Rooms)
                room.ChooseName();

            SortRooms(map);
            return map;

        }

        private static void Connect(DungeonMap map, Room room1, Room room2)
        {
            Debug.Assert(map.Rooms.Contains(room1));
            Debug.Assert(map.Rooms.Contains(room2));

            var passage = new Passage(room1, room2);

            if (Rng.P(0.6))
            {
                // TODO: Materials based on the dungeon type and the room type.
                DoorMaterial[] materials = new[] { DoorMaterial.Wood, DoorMaterial.Wood, DoorMaterial.Metal, DoorMaterial.Stone };

                var door = new Door(Rng.TakeOne(materials));
                if (Rng.P(0.2)) // TODO: Property of the door material
                    door.Stuck = true;

                passage.Door = door;
            }
        }

        private static void SortRooms(DungeonMap map)
        {
            // TODO: Sort based on location
            for (int i = 0; i < map.Rooms.Count; i++)
                map.Rooms[i].ID = i + 1;
        }
    }
}
