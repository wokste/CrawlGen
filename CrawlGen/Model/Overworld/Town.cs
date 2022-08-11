using CrawlGen.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model.Overworld
{
    public class Town : BaseFeature
    {
        public Town(Location loc) : base(loc)
        {
        }

        public override string ChooseName()
        {
            string[] first = new[] { "River", "Green", "Corn", "Sea", "Rea" };
            string[] second = new[] { "ton", "wall", "side", "don", "wich", "ding" };
            string[] third = new[] { "Village", "Town", "Hamlett" };

            return $"{Rng.TakeOne(first)}{Rng.TakeOne(second)} {Rng.TakeOne(third)}";

            // TODO: Implement
        }
    }
}
