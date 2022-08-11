using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public struct Location
    {
        public readonly PointF Pos;
        public readonly double Radius;
        public readonly World Parent;

        public Location(World parent, PointF pos, double radius)
        {
            Parent = parent;
            Pos = pos;
            Radius = radius;
        }
    }

    public abstract class BaseFeature
    {
        public Location Loc;
        public int Key = -1;

        public string Name;

        public BaseFeature(Location loc)
        {
            Loc = loc;
        }

        public abstract string ChooseName();
    }
}
