namespace CrawlGen.Gen
{
    public record struct Treasure(string Name, int Value, int Count = 1, string? Desc = null) {
        public int TotalValue => Value * Count;

        public override string ToString()
        {
            if (Count == 1)
                return $"{Name} ({Value} gp)";
            else
                return $"{Count}x {Name} ({Value} gp each, {Value * Count} gp total)";
        }
    }

    public static class TreasureGen
    {
        public static Treasure Make(float averageValue)
        {
            Func<float, Treasure?>[] factories = new Func<float, Treasure?>[] { MakeCoins, MakeScroll };

            return Rng.TakeOne(factories)(averageValue) ?? (Treasure)MakeCoins(averageValue);
        }

        private static Treasure? MakeScroll(float averageValue)
        {
            averageValue *= (float)Rng.UniformDouble(0.2, 0.4); // Scrolls shouldn't be too valueable

            int scrollCount = Rng.D(2);

            double level = Math.Sqrt(averageValue / scrollCount / 25.0);
            int levelInt = Rng.Round(level);

            return new Treasure($"Lvl {levelInt} spell scroll", levelInt * levelInt * 25, scrollCount);
        }

        private static Treasure? MakeCoins(float averageValue)
        {
            int gpValue = Utils.Round(averageValue * Rng.UniformDouble(0.5, 1.5) * Rng.UniformDouble(0.5, 1.5));
            return new($"{gpValue} gp", gpValue);
        }
    }
}
