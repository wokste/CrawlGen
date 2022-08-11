using CrawlGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal class WorldGen
    {
        public static World MakeWorld()
        {
            var world = new World(new HexMap(15,15));
            HexMapGen.MakeMap(world.HexMap);

            for (int i = 0; i < 2; ++i)
            {
                var dungeon = DungeonGen.MakeMap(new Biome("Dungeon"));
                world.Dungeons.Add(dungeon);
            }
            return world;
        }
    }
}
