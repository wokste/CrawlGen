using CrawlGen.Gen;
using CrawlGen.Grid;

namespace CrawlGen.Model.Overworld
{
    public enum SettlementType {
        Village, Town, City, Fort
    }

    public class Settlement : BaseFeature
    {
        public SettlementType Type;

        public Settlement()
        {

            SettlementType[] types = new[] { SettlementType.Village, SettlementType.Town, SettlementType.Fort };
            Type = Rng.TakeOne(types);
        }

        public override string ChooseName()
        {
            string[] first = new[] { "River", "Green", "Corn", "Sea", "Rea" };
            string[] second = new[] { "ton", "wall", "side", "don", "wich", "ding" };

            return $"{Rng.TakeOne(first)}{Rng.TakeOne(second)} {Type}";

            // TODO: Implement
        }


        public override IEnumerable<(string, string)> ListStats()
        {
            yield return ("Type", $"{Type}");
            // TODO: Species, Defensability, Alignment
        }

        public override double RateLocation(World world, PointD pos)
        {
            if (world.HeightMap.Sample(pos) < 0)
                return double.NaN;

            double error = 0;

            // TODO: Distance from main location (Based on CR)
            // TODO: Terrain preference (Based on type)

            return error;
        }
    }
}
