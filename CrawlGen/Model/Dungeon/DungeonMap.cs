using CrawlGen.Grid;

namespace CrawlGen.Model.Dungeon
{
    public class DungeonMap
    {
        public readonly List<Room> Rooms = new();

        public Room AddRoom(Room room)
        {
            // TODO: Collision check
            Rooms.Add(room);
            return room;
        }
    }
}
