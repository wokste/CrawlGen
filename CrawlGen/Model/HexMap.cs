using System.Diagnostics;

namespace CrawlGen.Model
{
    public class HexMap : HexGrid<Hex>
    {
        public HexMap(int w, int h) : base(w, h)
        {
        }

        /// <summary>Tries to add a hex to the map</summary>
        /// <returns>The new hex or null if no hex could be added.</returns>
        public Hex? AddHex(Biome terrain, HexCoords coords)
        {
            if (!ValidKey(coords))
                return null;

            if (Grid[coords.X, coords.Y] != null)
                return null;

            var hex = new Hex(this, terrain, coords);
            Grid[coords.X, coords.Y] = hex;
            return hex;
        }
    }
}
