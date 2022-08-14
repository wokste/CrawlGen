namespace CrawlGen.Grid
{
    public struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public (int, int) XY => (X, Y);

        public PointD AsReal => new PointD(X, Y);
    }

    public struct PointD
    {
        public readonly double X;
        public readonly double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public (double, double) XY => (X, Y);
    }
}
