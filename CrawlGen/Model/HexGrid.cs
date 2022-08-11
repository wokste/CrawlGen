using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model
{
    public class HexGrid<T>
    {
        protected readonly T[,] Grid;
        public readonly int Width;
        public readonly int Height;
        internal IEnumerable<HexCoords> Indices
        {
            get
            {
                for (int y = 0; y < Height; ++y)
                    for (int x = 0; x < Width; ++x)
                        yield return new HexCoords(x,y);
            }
        }

        public IEnumerable<T> Hexes
        {
            get
            {
                for (int y = 0; y < Height; ++y)
                    for (int x = 0; x < Width; ++x)
                        if (Grid[x, y] is T hex)
                            yield return hex;
            }
        }

        public HexGrid(int w, int h)
        {
            Grid = new T[w, h];
            Width = w;
            Height = h;
        }

        public T this[HexCoords coords]
        {
            get { return Grid[coords.X, coords.Y]; }
            set { Grid[coords.X, coords.Y] = value; }
        }

        public bool ValidKey(HexCoords coords)
        {
            return coords.X >= 0 && coords.Y >= 0 && coords.X < Width && coords.Y < Height;
        }
        public T? Find(HexCoords coords) => ValidKey(coords) ? this[coords] : default(T);
    }
}
