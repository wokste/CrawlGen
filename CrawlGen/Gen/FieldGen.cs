using CrawlGen.Grid;
using CrawlGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal static class FieldGen
    {
        public interface IField {
            public double Get(PointF pos);
        }

        public class RandField : IField
        {
            readonly double Max;

            public RandField(double max)
            {
                Max = max;
            }

            public double Get(PointF pos) => Rng.UniformDouble(Max);
        }

        public class ConeField : IField {
            double Width, Height;

            double X, Y;
            double XX, YY;
            double XY;
            double Base;

            public static ConeField MakeRand()
            {
                ConeField field = new();
                field.SetSlope(Rng.UniformDouble(2 * Math.PI), 3);
                field.AddCone(Rng.UniformDouble(-0.2, 0.4));
                return field;
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

            public double Get(PointF pos)
            {
                var (x, y) = pos.XY;
                x = x * 2 / Width - 1;
                y = y * 2 / Height - 1;

                return Base + (x * X) + (y * Y) + (x * x * XX) + (y * y * YY) + (x * y * XY);
            }
        }
    }
}
