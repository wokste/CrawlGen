using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public class World
    {
        public GridF HeightMap;
        public GridF PlantsMap;
        public GridF LawMap;

        public List<BaseFeature> Features = new();
        public IEnumerable<Dungeon> Dungeons => Features.OfType<Dungeon>();
        public IEnumerable<Settlement> Towns => Features.OfType<Settlement>();

        public Point Size { get; internal set; }

        public World(int width, int height)
        {
            Size = new Point(width, height);

            HeightMap = new(width + 1, height + 1);
            PlantsMap = new(width + 1, height + 1);
            LawMap    = new(width + 1, height + 1);
        }

        internal void AddFeature(BaseFeature feature, PointD pos)
        {
            Features.Add(feature);
            feature.Loc = new(this, pos);
        }
    }
}
