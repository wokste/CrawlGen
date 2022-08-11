using CrawlGen.Grid;
using CrawlGen.Model.Dungeon;
using System.Diagnostics;

namespace CrawlGen.Model.Overworld
{
    public class World
    {
        public GridF HeightMap;
        public GridF PlantsMap;
        public GridF LawMap;

        public List<BaseFeature> Features = new();
        public IEnumerable<Dungeon> Dungeons => Features.OfType<Dungeon>();
        public IEnumerable<Town> Towns => Features.OfType<Town>();

        public World(int width, int height)
        {
            HeightMap = new GridF(width, height);
            PlantsMap = new GridF(width, height);
            LawMap = new GridF(width, height);
        }
    }
}
