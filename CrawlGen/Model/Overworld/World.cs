using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public class World
    {
        public List<BaseFeature> Features = new();
        public IEnumerable<Dungeon> Dungeons => Features.OfType<Dungeon>();
        public IEnumerable<Settlement> Towns => Features.OfType<Settlement>();

        internal void AddFeature(BaseFeature feature, PointD pos)
        {
            Features.Add(feature);
            feature.Loc = new(this, pos);
        }
    }
}
