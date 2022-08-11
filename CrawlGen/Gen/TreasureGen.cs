using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    public static class TreasureGen
    {
        public static IEnumerable<string> Make()
        {
            if (Rng.P(0.6))
                yield return MakeCoins();

            Func<string>[] factories = new[] { MakeGear, MakeScroll, MakeWeapon };

            int numTreasures = Rng.D(3);
            for (int i = 0; i < numTreasures; ++i)
            {
                yield return Rng.TakeOne(factories)();
            }
        }

        private static string MakeGear()
        {
            (string,int)[] gear = new[] { ("Antitoxin", 2), ("Climber's Kit", 1), ($"Healer's Kit", 1), ("Holy Water", 1), ($"Potion of Healing", 4), ( "Poison, Basic", 3), ("Rope (Hemp)", 2), ("Torch", 6) };
            var (type, max) = Rng.TakeOne(gear);
            var count = Rng.D(max);

            if (count == 1)
                return type;
            else
                return $"{count}x: {type}";

        }

        private static string MakeScroll()
        {
            // All 1st level spells in the srd
            string[] spells = { "Alarm", "Animal Friendship", "Bane", "Bless", "Burning Hands", "Charm Person", "Color Spray", "Command", "Comprehend Languages", "Create or Destroy Water", "Cure Wounds", "Detect Evil and Good", "Detect Magic", "Detect Poison and Disease", "Disguise Self", "Divine Favor", "Entangle", "Expeditious Retreat", "Faerie Fire", "False Life", "Feather Fall", "Find Familiar", "Floating Disk", "Fog Cloud", "Goodberry", "Grease", "Guiding Bolt", "Healing Word", "Hellish Rebuke", "Heroism", "Hideous Laughter", "Hunter's Mark", "Identify", "Illusory Script", "Inflict Wounds", "Jump", "Longstrider", "Mage Armor", "Magic Missile", "Protection from Evil and Good", "Purify Food and Drink", "Sanctuary", "Shield", "Shield of Faith", "Silent Image", "Sleep", "Speak with Animals", "Thunderwave", "Unseen Servant" };
            return $"scroll of {Rng.TakeOne(spells)}";
        }

        private static string MakeWeapon()
        {
            // All weapons in the srd
            string[] weapons = { "Club", "Dagger", "Greatclub", "Handaxe", "Javelin", "Light hammer", "Mace", "Quarterstaff", "Sickle", "Spear", "Crossbow, light", "Dart", "Shortbow", "Sling", "Battleaxe", "Flail", "Glaive", "Greataxe", "Greatsword", "Halberd", "Lance", "Longsword", "Maul", "Morningstar", "Pike", "Rapier", "Scimitar", "Shortsword", "Trident", "War pick", "Warhammer", "Whip", "Blowgun", "Crossbow, hand", "Crossbow, heavy", "Longbow", "Net" };
            if (Rng.P(0.25))
                return $"{Rng.TakeOne(weapons)} +{(Rng.P(0.15) ? 2 : 1)}";
            else
                return $"{Rng.TakeOne(weapons)}";
        }

        private static string MakeCoins()
        {
            int[] mults = new[] { 5, 10, 25, 50, 100 };

            return $"{Rng.D(10) * Rng.TakeOne(mults)} gp";
        }
    }
}
