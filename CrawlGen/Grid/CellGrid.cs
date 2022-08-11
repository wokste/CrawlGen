using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Grid
{
    public class CellGrid<T>
    {
        public T[,] Cells;
        public int Width;
        public int Height;

        public CellGrid(int width, int height)
        {
            Cells = new T[width,height];

            Width = width;
            Height = height;
        }
    }
}
