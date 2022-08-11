using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Grid
{
    public class Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public (int, int) XY => (X, Y);

        public PointF AsReal => new PointF(X, Y);
    }

    public class PointF
    {
        public readonly double X;
        public readonly double Y;

        public PointF(double x, double y)
        {
            X = x;
            Y = y;
        }

        public (double, double) XY => (X, Y);
    }
}
