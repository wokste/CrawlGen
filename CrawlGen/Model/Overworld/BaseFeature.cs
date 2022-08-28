using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld;

public struct Location
{
    public readonly PointD Pos;
    public readonly World Parent;

    public Location(World parent, PointD pos)
    {
        Parent = parent;
        Pos = pos;
    }
}

public abstract class BaseFeature
{
    public Location? Loc;
    public Anchor Anchor = new();

    public string? Name;

    public abstract string ChooseName();
}
