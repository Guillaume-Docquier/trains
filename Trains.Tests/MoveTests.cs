using NUnit.Framework;
using Trains.models;

namespace Trains.Tests
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        [TestCase("ABc", 1, 3, 1 + 2)]
        [TestCase("D", 5, 9, 1 + 4)]
        public void GetCost_Computes_Cost_Based_On_Components(string wagons, int from, int to, int expected)
        {
            var move = new Move
            {
                Wagons = wagons,
                From = from,
                To = to
            };

            Assert.AreEqual(expected, Move.GetCost(move));
        }

        [Test]
        [TestCase("ABc", 0, 1, "ABc,1,2")]
        public void ToString_Formats_Move_Components_With_Separator_And_Adjusts_Indexes(string wagons, int from, int to, string expected)
        {
            var move = new Move
            {
                Wagons = wagons,
                From = from,
                To = to
            };

            Assert.AreEqual(expected, move.ToString());
        }
        
        [Test]
        [TestCase("ABc,1,2", "ABc", 0, 1)]
        public void Parse_Restores_Components_And_Adjusts_Indexes(string move, string wagons, int from, int to)
        {
            var expected = new Move
            {
                Wagons = wagons,
                From = from,
                To = to
            };

            Assert.AreEqual(expected, Move.Parse(move));
        }
    }
}