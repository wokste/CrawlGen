using CrawlGen.Gen;

namespace CrawlGen.Model
{
    /// <summary>A hex, dungeon room or something similar</summary>
    public abstract class ZoneBase
    {
        public abstract string Key { get; }
        public string? Name;

        public List<string> Treasure = new();

        protected ZoneBase()
        {
        }

        public override string ToString() => (Name != null) ? $"{Key}: {Name}" : $"{Key}";

        public void AddEncounter()
        {
            // TODO
        }
    }
}
