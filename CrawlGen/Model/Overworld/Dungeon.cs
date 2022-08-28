using CrawlGen.Gen;
using CrawlGen.Model.Dungeon;

namespace CrawlGen.Model.Overworld;

public class Dungeon : BaseFeature
{
    public readonly DungeonMap Map;

    public Dungeon(DungeonMap map)
    {
        Map = map;
    }

    public override string ChooseName()
    {
        var first = BucketTable<string>.FromString("Dungeon,Keep:0.3,Castle:0.6,Temple:0.6,Ruins:0.1,Mines:0.3");
        var second = BucketTable<string>.FromString("@ of Doom,Chaos @, Deadly @,@ of Carnage,@ of Rampage");

        return Names.Make(first, second);
    }
}
