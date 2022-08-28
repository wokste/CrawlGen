using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Gen
{
    internal static class Utils
    {
        public static int Round(double x) {
            double logX = Math.Log10(x);
            double floor = Math.Floor(logX);
            double powMod = Math.Pow(10, logX - floor);

            double magic;
            if (powMod >= 7.5) magic = 2.5;
            else if (powMod >= 5) magic = 1;
            else if (powMod >= 2) magic = 0.5;
            else magic = 0.25;

            double rounder = Math.Pow(10, floor) * magic;
            return (int)(Math.Round(x / rounder) * rounder);
        }
    }
}
