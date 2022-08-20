using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model.Encounters
{
    public class Monster
    {
        // Source: https://oldschoolessentials.necroticgnome.com/srd/index.php/Skeleton
        // (C): https://oldschoolessentials.necroticgnome.com/srd/index.php/%E2%A7%BCOpen_Game_License%E2%A7%BD
        public string Name { get; init; }
        public short AC { get; init; }
        public string HD { get; init; } // < TODO: Different type
        public Attack[] Attacks { get; init; }
        public short THACO { get; init; }
        public string Movement { get; init; } // < TODO: Different type
        public short[] Saves { get; init; } = new short[5];
        public short Morale { get; init; }
        public Alignment Alignment { get; init; }
        public short XP { get; init; }
        public short NA { get; init; }
        public short NALair { get; init; }
        public string[] TreasureType { get; init; } // < TODO: Different type

        public static Monster MakeSkeleton() => new Monster
        {
            Name = "Skeleton",
            AC = 7,
            HD = "1",
            Attacks = new Attack[] { new(1, "weapon", "1d6") },
            THACO = 19,
            Movement = "60",
            Saves = new short[] { 12, 13, 14, 15, 16 },
            Morale = 12,
            Alignment = Alignment.Chaos,
            XP = 10,
            NA = 8,
            NALair = 17,
            TreasureType = { }
        };
    }

    public struct Attack {
        public short Count;
        public string Name;
        public string Damage;

        public Attack(short count, string name, string damage)
        {
            Count = count;
            Name = name;
            Damage = damage;
        }

        public override string ToString() => $"{Count} x {Name} ({Damage})";
    }
}
