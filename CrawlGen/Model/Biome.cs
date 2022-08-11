namespace CrawlGen.Model
{
    public class Biome
    {
        public readonly string Name;
        public readonly char C;

        public Biome(string name, char c = ' ')
        {
            Name = name;
            C = c;
        }
    }
}
