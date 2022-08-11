namespace CrawlGen.Model
{
    public class Hex : ZoneBase
    {
        public readonly HexCoords Coords;

        public override string Key => Coords.ToString();

        public readonly HexMap Parent;
        public Biome Biome;

        public Hex(HexMap parent, Biome biome, HexCoords coords)
        {
            Parent = parent;
            Biome = biome;
            Coords = coords;
        }

        public IEnumerable<Hex> GetAdjacent()
        {
            return Coords.GetAdjacent().Select(c => Parent.Find(c)).OfType<Hex>();
        }

        public void ChooseName()
        {
            Name = Biome.Name;
        }
    }
}
