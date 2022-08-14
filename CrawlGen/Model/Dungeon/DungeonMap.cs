using CrawlGen.Grid;

namespace CrawlGen.Model.Dungeon
{
    public class DungeonMap
    {
        public readonly List<Room> Rooms = new();
        public readonly Grid<DungeonCell> Cells;

        public DungeonMap(int width, int height)
        {
            Cells = new Grid<DungeonCell>(width, height);
        }

        public Room AddRoom(Room room)
        {
            // TODO: Collision check
            Rooms.Add(room);
            return room;
        }
    }
}
