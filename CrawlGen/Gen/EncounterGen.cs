using CrawlGen.Model.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal class EncounterGen
    {
        internal static Encounter Make()
        {
            Monster monster = Monster.MakeSkeleton();

            return new Encounter(monster, ChooseNA(monster, false));
        }

        private static int ChooseNA(Monster monster, bool inLair)
        {
            var na = inLair ? monster.NALair : monster.NA;

            if (na == 1)
                return 1;

            return Rng.D(na) + Rng.D(na);
        }
    }
}
