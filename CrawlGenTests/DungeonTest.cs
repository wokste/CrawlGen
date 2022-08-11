using CrawlGen.Model;
using CrawlGen.Grid;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlGenTests
{
    internal class DungeonTest
    {
        [Test]
        public void CanFindAdjacentRooms()
        {
            DungeonMap map = new("Test dungeon", 20,30);
            Room r1 = map.AddRoom(new Room(new Rect(4, 4, 6, 6)));
            Assert.IsNotNull(r1);
            Room r2 = map.AddRoom(new Room(new Rect(4, 14, 6, 6)));
            Assert.IsNotNull(r2);
            //map.Connect(r1, r2);

            
        }
    }
}
