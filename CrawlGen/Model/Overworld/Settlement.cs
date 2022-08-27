using CrawlGen.Gen;
using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public enum SettlementType
    {
        Village, Town, City, Fort
    }

    public class Settlement : BaseFeature
    {
        public SettlementType Type;
        public Alignment Alignment;

        public Settlement()
        {

            SettlementType[] types = new[] { SettlementType.Village, SettlementType.Town, SettlementType.Fort };
            Type = Rng.TakeOne(types);

            // TODO: Proper weighted random tables
            // TODO: Weight based on species. Dwarves are more lawful while elves are more chaotic.
            Alignment[] alignments = new[] { Alignment.Law, Alignment.Law, Alignment.Law, Alignment.Neutral, Alignment.Neutral, Alignment.Chaos };
            Alignment = Rng.TakeOne(alignments);
        }

        public override string ChooseName()
        {
            var first = BucketTable<string>.FromString("River,Green,Black,Red,Grey,Corn,Sea,Rea,Aber,Ox,Canter,Mar,Farn,Bur,Nor,Wo,Ports,salis,Wey,Dart,Laven,Bris,Lin,Led,Here,War");
            var second = BucketTable<string>.FromString("@ton,@wall,@side,@don,@wich,@ding,@mouth,@loch,@lake,@shire,@bury,@stable,@gate,@ham,@burn,@moor,@bridge,@pool,@burgh");

            return Names.Make(first, second);
        }
    }
}
