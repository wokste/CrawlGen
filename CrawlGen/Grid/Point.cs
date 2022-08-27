using System.Diagnostics;

namespace CrawlGen.Grid
{
    public struct PointD
    {
        public readonly double X;
        public readonly double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PointD operator +(PointD a, PointD b) => new PointD(a.X + b.X, a.Y + b.Y);
        public static PointD operator -(PointD a, PointD b) => new PointD(a.X - b.X, a.Y - b.Y);

        public (double, double) XY => (X, Y);

        internal double DistanceTo(PointD other)
        {
            var d = this - other;
            return Math.Sqrt(d.X * d.X + d.Y * d.Y);
        }


        internal double Atan2() => Math.Atan2(Y, X);

        internal string ToDir() {
            var dir = Atan2() * 4 / Math.PI;
            var dirI = (int)Math.Round(dir);

            return dirI switch
            {
                -4 => "west",
                -3 => "northwest",
                -2 => "north",
                -1 => "northeast",
                0 => "east",
                1 => "southeast",
                2 => "south",
                3 => "southwest",
                4 => "west",
                _ => "???",
            };
        }
    }
}
