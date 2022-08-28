namespace CrawlGen.Grid;

public record struct PointD(double X, double Y)
{
    public static PointD operator +(PointD a, PointD b) => new(a.X + b.X, a.Y + b.Y);
    public static PointD operator -(PointD a, PointD b) => new(a.X - b.X, a.Y - b.Y);
    public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);

    internal double DistanceTo(PointD other)
    {
        var d = this - other;
        return Math.Sqrt(d.X * d.X + d.Y * d.Y);
    }

    internal double Atan2() => Math.Atan2(Y, X);

    internal string ToDir() {
        var dir = Atan2() * 4 / Math.PI;
        var dirI = (int)Math.Round(dir);

        return dirI switch
        {
            -4 => "West",
            -3 => "Northwest",
            -2 => "North",
            -1 => "Northeast",
            0 => "East",
            1 => "Southeast",
            2 => "South",
            3 => "Southwest",
            4 => "West",
            _ => "???",
        };
    }
}
