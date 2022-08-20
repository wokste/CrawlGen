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
    }
}
