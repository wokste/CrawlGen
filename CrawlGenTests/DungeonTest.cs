using CrawlGen.Grid;
using NUnit.Framework;
using CrawlGen.Model.Dungeon;

namespace CrawlGenTests
{
    internal class DungeonTest
    {
        [Test]
        public void CanFindAdjacentRooms()
        {
            DungeonMap map = new();
            Room r1 = new Room();
            Room r2 = new Room();
            map.Rooms.Add(r1);
            map.Rooms.Add(r2);
            Assert.IsNotNull(r1);
            Assert.IsNotNull(r2);
            //map.Connect(r1, r2);

            
        }
    }
}
