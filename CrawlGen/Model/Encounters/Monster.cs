namespace CrawlGen.Model.Encounters;

public class Monster
{
    public string Name { get; init; }
    public short AC { get; init; }
    public string HD { get; init; } // < TODO: Different type
    public string[]? Attacks { get; init; }
    public short THACO { get; init; }
    public Movement[]? Movement { get; init; }
    public string Saves { get; init; }
    public short Morale { get; init; }
    public Alignment Alignment { get; init; }
    public short XP { get; init; }
    public short NA { get; init; }
    public short NALair { get; init; }
    public TreasureType[]? Treasure { get; init; } // < TODO: Different type
    float Size { get; init; } = 1f;

    // Source: https://oldschoolessentials.necroticgnome.com/srd/index.php/Skeleton
    // (C): https://oldschoolessentials.necroticgnome.com/srd/index.php/%E2%A7%BCOpen_Game_License%E2%A7%BD
    public static Monster MakeSkeleton() => new()
    {
        Name = "Skeleton",
        AC = 7,
        HD = "1",
        Attacks = new string[] { "1x weapon (1d6)" },
        THACO = 19,
        Movement = new Movement[] { new(60) },
        Saves = "D12 P13 B14 W15 S16",
        Morale = 12,
        Alignment = Alignment.Chaos,
        XP = 10,
        NA = 8,
        NALair = 17,
        Treasure = { }
    };
}

public record struct Attack(short Count, string Name, string Damage)
{
    public override string ToString() => $"{Count} x {Name} ({Damage})";
}

public record struct TreasureType(string Type, float Weight)
{
}

public struct Movement
{
    public short Dist { get; set; }
    public MovementType MovementType { get; set; } = MovementType.Normal;

    public Movement(short dist, MovementType movementType = MovementType.Normal)
    {
        Dist = dist;
        MovementType = movementType;
    }

    public override string ToString()
    {
        return $"{Dist}' ({Dist / 3}'){MakeTypeString()}";
    }

    private string MakeTypeString()
    {
        return MovementType switch
        {
            MovementType.Normal => "",
            MovementType.Climb => " climbing",
            MovementType.Float => " floating",
            MovementType.Fly => " flying",
            MovementType.Burrow => " burrowing",
            MovementType.Swim => " swimming",
            MovementType.Phase => " phasing",
            MovementType.Teleport => " teleporting",
        };
    }
}

public enum MovementType
{
    Normal,
    Climb,
    Float,
    Fly,
    Burrow,
    Swim,
    Phase,
    Teleport,
}
