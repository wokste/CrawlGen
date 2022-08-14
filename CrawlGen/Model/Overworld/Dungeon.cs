using CrawlGen.Gen;
using CrawlGen.Grid;
using CrawlGen.Model.Dungeon;

namespace CrawlGen.Model.Overworld
{
    public class Dungeon : BaseFeature
    {
        public readonly DungeonMap Map;

        public Dungeon(DungeonMap map)
        {
            Map = map;
        }

        public override string ChooseName()
        {
            string[] first = new[] { "Dungeon", "Keep", "Castle", "Temple", "Ruins", "Mines" };
            string[] second = new[] { "Doom", "Chaos", "Death", "Carnage", "Rampage" };

            return $"{Rng.TakeOne(first)} of {Rng.TakeOne(second)}";
        }

        public override IEnumerable<(string, string)> ListStats()
        {
            yield return ("Rooms", $"{Map.Rooms.Count}");
            // TODO: Expected Party Level
        }

        public override double RateLocation(World world, PointD pos)
        {
            if (world.HeightMap.Sample(pos) < 0)
                return double.NaN;

            double error = 0;

            // TODO: Distance from main location (Based on CR)
            // TODO: Terrain preference (Based on type)

            return error;
        }
    }
}
