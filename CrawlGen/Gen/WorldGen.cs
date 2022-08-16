using CrawlGen.Grid;
using CrawlGen.Model.Overworld;
using static CrawlGen.Gen.Fields;

namespace CrawlGen.Gen
{
    internal class WorldGen
    {
        public static World MakeWorld()
        {
            var world = new World(50,50);

            for (int i = 0; i < 5; ++i)
                world.HeightMap.Add(ConeField.MakeRand(world.Size, 5, 3));

            world.HeightMap.Add(SlopeField.MakeRand(world.Size, 0.05));
            //world.HeightMap.Add(new PerlinField(Rng.UniformInt(), 5, 0.5, 5));

            world.PlantsMap.Add(new PerlinField(Rng.UniformInt(), 5, 0.5, 2.5));

            for (int i = 0; i < 3; ++i)
            {
                var dungeon = DungeonGen.Make();
                if (ChooseLocation(world,dungeon) is PointD pos)
                    world.AddFeature(dungeon, pos);
            }

            for (int i = 0; i < 2; ++i)
            {
                var town = new Settlement();
                if (ChooseLocation(world, town) is PointD pos)
                    world.AddFeature(town, pos);
            }

            foreach (var f in world.Features)
                f.Name = f.ChooseName();

            return world;
        }



        public static PointD? ChooseLocation(World world, BaseFeature feature)
        {
            PointD? lastPos = null;
            double lastError = double.PositiveInfinity;

            const int NUM_ATTEMPTS = 100;
            for (int i = 0; i < NUM_ATTEMPTS; ++i)
            {
                PointD pos = new(Rng.UniformDouble(world.Size.X), Rng.UniformDouble(world.Size.Y)); // TODO: Actual ccords
                var error = feature.RateLocation(world, pos);

                if (!double.IsNaN(error) && error < lastError)
                {
                    lastPos = pos;
                    lastError = error;

                    // Early out. It is good enough.
                    if (error < 0.1)
                        return lastPos;
                }
            }

            return lastPos;
        }
    }
}
