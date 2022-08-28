using CrawlGen.Grid;
using CrawlGen.Model.Overworld;

namespace CrawlGen.Gen;

internal static class WorldGen
{
    const double MAP_WIDTH = 10;
    const double MAP_HEIGHT = 6;

    public static World MakeWorld()
    {
        World world = new();
        for (int i = 0; i < 1; ++i)
        {
            var gen = new DungeonGen();
            var dungeon = gen.Map;
            if (ChooseLocation(world,dungeon) is PointD pos)
                world.AddFeature(dungeon, pos);
        }

        for (int i = 0; i < 1; ++i)
        {
            var town = new Settlement();
            if (ChooseLocation(world, town) is PointD pos)
                world.AddFeature(town, pos);
        }

        foreach (var f in world.Features)
            f.Name = f.ChooseName();

        SortFeatures(world);

        return world;
    }

    public static PointD? ChooseLocation(World world, BaseFeature feature)
    {
        return new(Rng.UniformDouble(MAP_WIDTH), Rng.UniformDouble(MAP_HEIGHT));
    }

    private static void SortFeatures(World map)
    {
        // TODO: Sort based on location
    }
}
