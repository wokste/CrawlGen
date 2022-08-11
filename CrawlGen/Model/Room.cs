using CrawlGen.Gen;
using CrawlGen.Grid;

namespace CrawlGen.Model
{
    public class Room : ZoneBase
    {
        public override string Key => KeyInt.ToString();
        public int KeyInt = -1;
        internal Rect Rect;
        internal List<Room> Connections = new();

        public Room(Rect rect) : base()
        {
            Rect = rect;
        }

        public void ChooseName()
        {
            string[] names = new[] { "Storage Room", "Hallway", "Library", "Dungeon Room" };
            Name = Rng.TakeOne(names);
        }
    }
}
