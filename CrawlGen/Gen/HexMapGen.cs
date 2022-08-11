using CrawlGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal class HexMapGen
    {
        static Biome Sea = new("Sea", '~');
        static Biome Plains = new("Plains", '.');
        static Biome Forest = new("Forest", 'f');
        static Biome Hills = new("Hills", '^');
        static Biome Mountains = new("Mountains", '#');

        internal static void MakeMap(HexMap hexMap)
        {
            var heightmap = HeightGen.MakeGrid(hexMap.Width, hexMap.Height);
            foreach (var coords in hexMap.Indices)
            {
                var height = heightmap[coords];
                hexMap.AddHex(GetTerrain(height), coords);
            }

            foreach (var hex in hexMap.Hexes)
                hex.AddEncounter();

            foreach (var hex in hexMap.Hexes)
                hex.ChooseName();
        }

        private static Biome GetTerrain(double height)
        {
            if (height < 0) return Sea;

            if (height < 0.6 && Rng.P(0.4)) return Forest;

            if (height < 0.4) return Plains;
            if (height < 0.8) return Hills;
            return Mountains;
        }
    }
}
