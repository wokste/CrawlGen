using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model.Encounters
{
    internal class Encounter
    {
        public Monster Monster;
        public int Na;

        public Encounter(Monster monster, int na)
        {
            Monster = monster;
            Na = na;
        }

        public int TotalXP => Na * Monster.XP;
    }
}
