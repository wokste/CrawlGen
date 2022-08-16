using CrawlGen.Gen;
using static CrawlGen.Gen.Fields;

namespace CrawlGen.Grid
{
    public class GridF : Grid<double>, IField
    {
        public GridF(int width, int height) : base(width, height)
        {
            foreach (var pos in Keys)
                this[pos] = 0;
        }

        internal void Add(Fields.IField field)
        {
            foreach (var pos in Keys)
                this[pos] += field.Sample(pos.AsReal);
        }

        internal void Add(double v)
        {
            foreach (var pos in Keys)
                this[pos] += v;
        }

        public double Sample(PointD pos)
        {
            static double Lerp(double a, double b, double f) => (a * (1.0 - f)) + (b * f);

            int xInt = (int)Math.Floor(pos.X);
            int yInt = (int)Math.Floor(pos.Y);

            double xFrac = pos.X - xInt;
            double yFrac = pos.Y - yInt;

            var v00 = Cells[xInt, yInt];
            var v01 = Cells[xInt, yInt + 1];
            var v10 = Cells[xInt + 1, yInt];
            var v11 = Cells[xInt + 1, yInt + 1];

            var v0 = Lerp(v00, v01, yFrac);
            var v1 = Lerp(v10, v11, yFrac);
            return Lerp(v0, v1, xFrac);
        }
    }
}
