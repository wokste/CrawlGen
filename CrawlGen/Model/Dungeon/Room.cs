using CrawlGen.Gen;
using CrawlGen.Grid;

namespace CrawlGen.Model.Dungeon
{
    public class Room
    {
        public int Key = -1;
        internal Rect Rect;
        internal List<Room> Connections = new();
        public string? Name;
        public List<string> Treasure = new();

        public override string ToString() => (Name != null) ? $"{Key}: {Name}" : $"{Key}";

        public Room(Rect rect)
        {
            Rect = rect;
        }

        public void ChooseName()
        {
            string[] names = new[] { "Storage Room", "Hallway", "Library", "Dungeon Room" };
            Name = Rng.TakeOne(names);
        }

        public void AddEncounter() {
            //TODO:
        }
    }
}
