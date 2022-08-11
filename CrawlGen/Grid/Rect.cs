using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Grid
{
    public class Rect
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }


        internal IEnumerable<Point> Iter
        {
            get
            {
                for (int y = Y; y < Y + Height; ++y)
                    for (int x = X; x < X + Width; ++x)
                        yield return new Point(x, y);
            }
        }
    }
}
