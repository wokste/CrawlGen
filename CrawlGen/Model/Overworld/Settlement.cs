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

        public Settlement()
        {

            SettlementType[] types = new[] { SettlementType.Village, SettlementType.Town, SettlementType.Fort };
            Type = Rng.TakeOne(types);
        }

        public override string ChooseName()
        {
            string[] first = new[] { "River", "Green", "Black", "Red", "Grey", "Corn", "Sea", "Rea", "Aber", "Ox", "Canter", "Mar", "Farn", "Bur", "Nor", "Wo", "Ports", "salis", "Wey", "Dart", "Laven", "Bris", "Lin", "Led", "Here", "War", };
            string[] second = new[] { "ton", "wall", "side", "don", "wich", "ding", "mouth", "loch", "lake", "shire", "bury", "stable", "gate", "ham", "burn", "moor", "bridge", "pool", "burgh" };

            return $"{Rng.TakeOne(first)}{Rng.TakeOne(second)} {Type}";

            // TODO: Implement
        }


        public override IEnumerable<(string, string)> ListStats()
        {
            yield return ("Type", $"{Type}");
            // TODO: Species, Defensability, Alignment
        }
    }
}
