using CrawlGen.Gen;
using CrawlGen.Model.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model.Overworld
{
    public class Dungeon : BaseFeature
    {
        public readonly DungeonMap Map;

        public Dungeon(DungeonMap map, Location loc) : base(loc)
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
