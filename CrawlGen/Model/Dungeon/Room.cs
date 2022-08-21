using CrawlGen.Gen;
using CrawlGen.Grid;
using CrawlGen.Model.Encounters;

namespace CrawlGen.Model.Dungeon
{
    public class Room
    {
        public int ID = -1;
        public Anchor Anchor = new();

        internal List<Passage> Passages = new();
        public string? Name;
        public List<string> Treasure = new();
        internal Encounter? Encounter;
        public readonly PointD Loc;

        public Room(PointD loc)
        {
            Loc = loc;
        }

        public override string ToString() => $"{ID}: {Name ?? "???"}";

        public void ChooseName()
        {
            string[] names = new[] { "Storage Room", "Hallway", "Library", "Prison", "Dining Hall", "Courtyard", "Guard post", "Kitchen", "Labratory", "Stables", "Study/Office", "Throne Room", "Workspace", "Thophy Room" };
            string[] prefixes = new[] { "Dusty ", "Moldy ", "Slimy ", "Foggy ", "Creepy ", "Well Maintained " };

            Name = Rng.TakeOne(names);
            if (Rng.P(0.6))
                Name = Rng.TakeOne(prefixes) + Name;
        }
    }
}
