using System.Linq;

namespace CrawlGen.Model
{
    public enum HexDir {
        N, NE, SE, S, SW, NW
    }

    static class HexDirs {
        public static HexDir[] All => new[] { HexDir.N, HexDir.NE, HexDir.SE, HexDir.S, HexDir.SW, HexDir.NW };
        public static HexDir RotateCW(this HexDir dir)
        {
            return dir switch
            {
                HexDir.N => HexDir.NE,
                HexDir.NE => HexDir.SE,
                HexDir.SE => HexDir.S,
                HexDir.S => HexDir.SW,
                HexDir.SW => HexDir.NW,
                HexDir.NW => HexDir.N,
                _ => throw new ArgumentException($"Only pass valid enum values. Got {dir}"),
            };
        }
        public static HexDir RotateCCW(this HexDir dir)
        {
            return dir switch
            {
                HexDir.N => HexDir.NW,
                HexDir.NE => HexDir.N,
                HexDir.SE => HexDir.NE,
                HexDir.S => HexDir.SE,
                HexDir.SW => HexDir.S,
                HexDir.NW => HexDir.SW,
                _ => throw new ArgumentException($"Only pass valid enum values. Got {dir}"),
            };
        }
    }

    // Inspiration: https://www.redblobgames.com/grids/hexagons/
    // “even-q” vertical layout shoves even columns down
    public struct HexCoords
    {
        public readonly int X;
        public readonly int Y;

        public HexCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public HexCoords Step(HexDir dir)
        {
            int dyTop = -(X % 2);

            return dir switch
            {
                HexDir.N => new HexCoords(X, Y - 1),
                HexDir.NE => new HexCoords(X + 1, Y + dyTop),
                HexDir.SE => new HexCoords(X + 1, Y + dyTop + 1),
                HexDir.S => new HexCoords(X, Y + 1),
                HexDir.SW => new HexCoords(X - 1, Y + dyTop + 1),
                HexDir.NW => new HexCoords(X - 1, Y + dyTop),
                _ => throw new ArgumentException($"Only pass valid enum values. Got {dir}"),
            };
        }

        public IEnumerable<HexCoords> GetAdjacent()
        {
            var self = this;
            return HexDirs.All.Select(d => self.Step(d));
        }

        public (double, double) GetMapCoords(double scale = 1)
        {
            const double HALF_SQRT_3 = 0.866025405;

            double tempX = X * HALF_SQRT_3 + 0.5;
            double tempY = (X % 2) * -0.5 + Y + 1;
            return (tempX * scale, tempY * scale);
        }

        public override string ToString() => $"#{X + 1}{(Y + 1):0#}";
    }
}
