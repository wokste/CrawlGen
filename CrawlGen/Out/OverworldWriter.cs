using CrawlGen.Model.Overworld;
using CrawlGen.Out.Utils;
using CrawlGen.Grid;

namespace CrawlGen.Out
{
    internal static class OverworldWriter
    {
        public static void WriteWrold(World world, HTMLPage page)
        {
            WriteGrid(world);
            WriteLocations(world, page);

            foreach (var town in world.Towns)
                SettlementWriter.WriteTown(town, page);

            foreach (var dungeon in world.Dungeons)
                DungeonWriter.WriteDungeon(dungeon, page);

        }

        private static void WriteGrid(World world)
        {
            int sizeX = 40;
            int sizeY = 20;

            // TODO: 1.1?
            double scaleX = (world.HeightMap.Width - 1.0001) / (double)sizeX;
            double scaleY = (world.HeightMap.Height - 1.0001) / (double)sizeY;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    var pos = new PointD(x * scaleX, y * scaleY);

                    var height = world.HeightMap.Sample(pos);
                    var plants = world.PlantsMap.Sample(pos);

                    if (height < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('~');
                    }
                    else if (plants > 0.5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write('f');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }

        public static void WriteLocations(World map, HTMLPage page)
        {
            page.WriteElem("h1", "Overworld"); // TODO: Name
            foreach (var feature in map.Features)
            {
                WriteFeature(feature, page);
            }
        }

        private static void WriteFeature(BaseFeature feature, HTMLPage page)
        {
            page.WriteElem("h1", $"{feature.Key}: {feature.Name}");
            if (feature.Loc?.Pos is PointD pos)
                page.WriteElem("p", $"location: {pos.X},{pos.Y}");

            foreach (var (key, val) in feature.ListStats())
            {
                page.WriteElem("p", $"{key}: {val}");
            }
        }
    }
}
