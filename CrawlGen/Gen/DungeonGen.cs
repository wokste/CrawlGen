using CrawlGen.Grid;
using CrawlGen.Model.Dungeon;
using CrawlGen.Model.Overworld;
using System.Diagnostics;

namespace CrawlGen.Gen;

internal class DungeonGen
{
    public readonly Dungeon Dungeon = new(new());
    DungeonMap Map;

    public DungeonGen()
    {
        Map = Dungeon.Map;
        InitMap();
    }

    public DungeonMap InitMap()
    {
        var proto = Dungeons.ProtoDungeon.Generate();

        // Make the rooms
        foreach (var (x, y) in proto.GetSquaresAndAssignIDs())
            Map.Rooms.Add(new Room(new PointD(x, y)));

        // Connect stuff
        foreach (var (id1, id2) in proto.GetPassages())
            Connect(Map.Rooms[id1], Map.Rooms[id2]);

        AddEncounters();
        AddTreasure();

        // Choose names
        foreach (var room in Map.Rooms)
            room.ChooseName();

        SortRooms(Map);
        return Map;
    }


    private void AddTreasure()
    {
        float totalXP = Map.Rooms.Sum(r => r.Encounter?.TotalXP ?? 0);
        var totalGP = totalXP * 3; // TODO: Magic number

        var averageTreasurePerRoom = totalGP / Map.Rooms.Count;

        int numDiscards = 5;
        while (numDiscards > 0)
        {
            var treasure = TreasureGen.Make(averageTreasurePerRoom);

            if (totalGP * (numDiscards + 1.0) / numDiscards < treasure.TotalValue)
            {
                // The treasure was too expensive.
                numDiscards--;
                continue;
            }

            totalGP -= treasure.TotalValue;

            Rng.TakeOne(Map.Rooms).Treasure.Add(treasure);
        }
    }

    void AddEncounters()
    {
        BucketTable<Action<Room>?> table = new();
        table.Add(null, 1);
        table.Add(r => r.Encounter = EncounterGen.Make(), 1);
        //table.Add(r => r.Treasure.Add("A FRIKKIN TRAP"), 0.5f);

        var mods = table.TakeN(Map.Rooms.Count);
        for (int i = 0; i < Map.Rooms.Count; ++i)
            mods[i]?.Invoke(Map.Rooms[i]);
    }

    private void Connect(Room room1, Room room2)
    {
        Debug.Assert(Map.Rooms.Contains(room1));
        Debug.Assert(Map.Rooms.Contains(room2));

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

    private void SortRooms(DungeonMap map)
    {
        // TODO: Sort based on location
        for (int i = 0; i < map.Rooms.Count; i++)
            map.Rooms[i].ID = i + 1;
    }
}
