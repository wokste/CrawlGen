using CrawlGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal static class HeightGen
    {
        public static HexGrid<double> MakeGrid(int w, int h)
        {
            HeightField field = new HeightField(w, h);
            field.SetSlope(Rng.UniformDouble(2 * Math.PI), 0.2);
            field.AddCone(Rng.UniformDouble(-0.2, 0.4));

            HexGrid<double> grid = new(w,h);

            foreach (var c in grid.Indices)
            {
                var height = field.Get(c);
                height += Rng.UniformDouble(0.1);
                grid[c] = height;
            }

            return grid;
        }

        class HeightField {
            double Width, Height;

            double X, Y;
            double XX, YY;
            double XY;
            double Base;

            internal HeightField(double width, double height)
            {
                Width = width;
                Height = height;
            }

            internal void AddCone(double height) {
                XX -= height;
                YY -= height;
                Base += height * 0.66666666666666666666666667;
            }

            internal void SetSlope(double alpha, double slope)
            {
                X = Math.Sin(alpha) * slope;
                Y = Math.Cos(alpha) * slope;
            }

            internal double Get(HexCoords coords)
            {
                var (x, y) = coords.GetMapCoords();
                x = x * 2 / Width - 1;
                y = y * 2 / Height - 1;

                return Base + (x * X) + (y * Y) + (x * x * XX) + (y * y * YY) + (x * y * XY);
            }
        }
    }
}
