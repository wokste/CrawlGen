using CrawlGen.Grid;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CrawlGen.Model
{
    public class DungeonMap
    {
        public readonly List<Room> Rooms = new();
        public readonly CellGrid<DungeonCell> Cells;
        public string Name;

        public DungeonMap(string name, int width, int height)
        {
            Name = name;
            Cells = new CellGrid<DungeonCell>(width, height);
        }

        public Room AddRoom(Room room)
        {
            // TODO: Collision check
            Rooms.Add(room);
            return room;
        }
    }
}
