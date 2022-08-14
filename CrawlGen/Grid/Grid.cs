namespace CrawlGen.Grid
{
    public class Grid<T>
    {
        public T[,] Cells;
        public int Width => Cells.GetLength(0);
        public int Height => Cells.GetLength(1);

        public Grid(int width, int height)
        {
            Cells = new T[width,height];
        }

        internal IEnumerable<Point> Keys
        {
            get
            {
                for (int y = 0; y < Height; ++y)
                    for (int x = 0; x < Width; ++x)
                        yield return new Point(x, y);
            }
        }

        public IEnumerable<(Point,T)> GetPairsForRect(Rect rect)
        {
            foreach (Point key in rect.Iter)
                yield return (key, Cells[key.X, key.Y]);
        }

        public T this[Point coords]
        {
            get { return Cells[coords.X, coords.Y]; }
            set { Cells[coords.X, coords.Y] = value; }
        }

        public bool ContainsKey(Point coords)
        {
            return coords.X >= 0 && coords.Y >= 0 && coords.X < Width && coords.Y < Height;
        }
        public T? Find(Point coords) => ContainsKey(coords) ? this[coords] : default;
    }
}
