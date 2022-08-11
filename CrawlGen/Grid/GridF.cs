using CrawlGen.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Grid
{
    public class GridF : Grid<double>
    {
        public GridF(int width, int height) : base(width, height)
        {
        }

        internal void AddField(FieldGen.IField field)
        {
            foreach (var pos in Keys)
            {
                double w = Width;
                double h = Height;
                PointF posF = new(pos.X / w, pos.Y / h);

                this[pos] += field.Get(posF);
            }
        }

        internal double Sample(PointF pos)
        {
            int xInt = (int)Math.Floor(pos.X);
            int yInt = (int)Math.Floor(pos.Y);

            double xFrac = pos.X - xInt;
            double yFrac = pos.Y - yInt;

            var v00 = Cells[xInt, yInt];
            var v01 = Cells[xInt, yInt + 1];
            var v10 = Cells[xInt + 1, yInt];
            var v11 = Cells[xInt + 1, yInt + 1];

            // TODO: Lerping
            return v00;
        }
    }
}
