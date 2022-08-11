using CrawlGen.Model;
using CrawlGen.Model.Overworld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrawlGen.Gen.FieldGen;

namespace CrawlGen.Gen
{
    internal class WorldGen
    {
        public static World MakeWorld()
        {
            var world = new World(15,15);

            world.HeightMap.AddField(ConeField.MakeRand());
            world.HeightMap.AddField(new RandField(0.1));

            // TODO: Add village

            for (int i = 0; i < 2; ++i)
            {
                var dungeonMap = DungeonGen.MakeMap();
                // TODO: Choose a location
                var dungeon = new Dungeon(dungeonMap, new Location(world, new Grid.PointF(4, 4), 0));
                world.Features.Add(dungeon);
            }


            foreach (var f in world.Features)
                f.Name = f.ChooseName();

            return world;
        }
    }
}
