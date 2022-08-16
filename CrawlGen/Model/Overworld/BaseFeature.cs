using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public struct Location
    {
        public readonly PointD Pos;
        public readonly World Parent;

        public Location(World parent, PointD pos)
        {
            Parent = parent;
            Pos = pos;
        }
    }

    public abstract class BaseFeature
    {
        public Location? Loc;
        public int Key = -1;

        public string? Name;

        public abstract string ChooseName();
        public abstract IEnumerable<(string,string)> ListStats();
    }
}
