using CrawlGen.Model;
using NUnit.Framework;
using System;
using System.Linq;

namespace CrawlGenTests
{
    public class HexTest
    {
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void UniqueKeys(int x, int y)
        {
            HexCoords coords = new HexCoords(x, y);
            var adjacent = coords.GetAdjacent().ToArray();
            Assert.AreEqual(6, adjacent.Length);
            CollectionAssert.AllItemsAreUnique(adjacent);
        }

        [Test]
        public void DistanceCorrect()
        {
            HexCoords hex1 = new HexCoords(3, 3);
            var (x1, y1) = hex1.GetMapCoords(6);

            foreach (var hex2 in hex1.GetAdjacent())
            {
                var (x2, y2) = hex2.GetMapCoords(6);
                var distSquared = Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2);

                Assert.That(distSquared, Is.EqualTo(36).Within(0.01));

            }
        }

        [Test]
        public void CanFindAdjacentHexes()
        {
            HexMap map = new HexMap(6,6);
            Biome plains = new("Plains");
            Hex h22 = map.AddHex(plains, 2, 2);
            Hex h23 = map.AddHex(plains, 2, 3);
            Hex h32 = map.AddHex(plains, 3, 2);
            Hex h33 = map.AddHex(plains, 3, 3);

            CollectionAssert.AreEquivalent(new[] { h23, h33, h32 }, h22.GetAdjacent());
            CollectionAssert.AreEquivalent(new[] { h22, h33 }, h23.GetAdjacent());
            CollectionAssert.AreEquivalent(new[] { h22, h33 }, h32.GetAdjacent());
            CollectionAssert.AreEquivalent(new[] { h23, h22, h32 }, h33.GetAdjacent());
        }
    }
}