using System.Text;

namespace CrawlGen.Model.Dungeons;

public class Passage
{
    public Room Room1, Room2;
    public Door? Door;

    public Passage(Room room1, Room room2)
    {
        Room1 = room1;
        Room2 = room2;

        room1.Passages.Add(this);
        room2.Passages.Add(this);
    }

    internal Room GetOtherRoom(Room room) => (room == Room1) ? Room2 : Room1;
}

public class Door {
    public DoorMaterial Material;
    public bool Stuck = false;
    public bool Locked = false; // TODO: How to handle keys?
    public bool Hidden = false;

    public Door(DoorMaterial material)
    {
        Material = material;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        if (Hidden) sb.Append("hidden ");
        if (Stuck) sb.Append("stuck ");
        if (Locked) sb.Append("locked ");

        sb.Append(Material switch
        {
            DoorMaterial.Wood => "wooden",
            DoorMaterial.Stone => "stone",
            DoorMaterial.Metal => "metal",
            _ => throw new NotImplementedException(),
        });

        sb.Append(" door");
        return sb.ToString();
    }
}

public enum DoorMaterial
{
    Wood,
    Stone,
    Metal,
}
