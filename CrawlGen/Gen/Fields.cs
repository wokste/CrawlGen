using CrawlGen.Grid;

namespace CrawlGen.Gen
{
    internal static class Fields
    {
        public interface IField
        {
            public double Sample(PointD pos);
        }

        public class PerlinField : IField
        {
            int Seed;
            int Octaves;
            double Weight;
            double Scale;

            public PerlinField(int seed, int octaves, double weight = 1, double scale = 1)
            {
                Seed = seed;
                Octaves = octaves;
                Weight = weight;
                Scale = scale;
            }

            public double Sample(PointD pos)
            {
                double x = pos.X / Scale;
                double y = pos.Y / Scale;
                double sum = 0;
                for (int i = 0; i < Octaves - 1; i++)
                {
                    double frequency = Math.Pow(0.5, i);
                    double amplitude = Math.Pow(0.5, i) * Weight;
                    sum += NoiseIteration2d(x * frequency, y * frequency) * amplitude;
                }
                return sum;
            }

            double NoiseIteration2d(double x, double y)
            {
                double Interpolate(double value0, double value1, double fraction)
                {
                    //Smooth fraction with cosine
                    fraction = (1 - Math.Cos(fraction * Math.PI)) / 2.0;
                    return value0 * (1.0 - fraction) + value1 * (fraction);
                }

                int xFloored = (int)Math.Floor(x);
                int yFloored = (int)Math.Floor(y);
                double topLeft = Random2D(xFloored, yFloored);
                double topRight = Random2D(xFloored + 1, yFloored);
                double bottomLeft = Random2D(xFloored, yFloored + 1);
                double bottomRight = Random2D(xFloored + 1, yFloored + 1);

                double top = Interpolate(topLeft, topRight, x - xFloored);
                double bottom = Interpolate(bottomLeft, bottomRight, x - xFloored);
                return Interpolate(top, bottom, y - yFloored);
            }

            /// This function generates a pseudo-random number based on: x, y and the seed.
            /// It is based on prime magic. The return value is in range [-1,1]
            double Random2D(int x, int y)
            {

                int n = x + y * 57;
                n *= Seed;
                n ^= n << 13;
                int nn = (n * (n * n * 60493 + 19990303) + 1376312589) & 0x7fffffff;
                return 1.0 - ((double)nn / 1073741824.0);
            }
        }

        public class SlopeField : IField
        {
            double DX, DY;
            double Base;

            public static SlopeField MakeRand(Point mapSize, double slope)
            {
                SlopeField field = new();
                field.SetSlope(Rng.UniformDouble(2 * Math.PI), slope);
                field.Normalize(mapSize);
                return field;
            }

            public double Sample(PointD pos)
            {
                return Base + (pos.X * DX) + (pos.Y * DY);
            }

            internal void SetSlope(double alpha, double slope)
            {
                DX = Math.Sin(alpha) * slope;
                DY = Math.Cos(alpha) * slope;
            }

            internal void Normalize(Point mapSize)
            {
                Base = -0.5 * (mapSize.X * DX + mapSize.Y * DY);
            }
        }

        public class ConeField : IField
        {
            PointD Pos;
            double Radius, Height;

            public static ConeField MakeRand(Point mapSize, double radius, double height)
            {
                // TODO: Randomize radius and height
                return new()
                {
                    Pos = new PointD(Rng.UniformDouble(mapSize.X), Rng.UniformDouble(mapSize.Y)),
                    Radius = radius,
                    Height = height
                };
            }

            public double Sample(PointD pos)
            {
                double dist = pos.DistanceTo(Pos);

                return (dist < Radius) ? Height * (1 - dist / Radius) : 0;
            }
        }
    }
}
