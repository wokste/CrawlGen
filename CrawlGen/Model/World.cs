using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGen.Model
{
    public class World
    {
        public HexMap HexMap;
        public List<DungeonMap> Dungeons = new();

        public World(HexMap hexMap)
        {
            HexMap = hexMap;
        }
    }
}
