using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trains.models;

namespace Trains.Tests
{
    [TestFixture]
    public class SolutionTests
    {
        [Test]
        [TestCase("ABc,1,2", "D", 2, 3, "ABc,1,2;D,2,3")]
        public void AddMove_Formats_Moves_With_Separator(string baseSolution, string wagons, int from, int to, string expected)
        {
            var move = new Move
            {
                Wagons = wagons,
                From = from,
                To = to
            };

            var solution = Solution.AddMove(baseSolution, move);

            Assert.AreEqual(expected, solution);
        }
        
        [Test]
        [TestCase("ABc,1,3", 1 + 2)]
        [TestCase("ABc,1,3;D,5,9", (1 + 2) + (1 + 4))]
        public void GetSolutionCost_Computes_Cost_Based_On_Moves(string solution, int expected)
        {
            var cost = Solution.GetCost(solution);

            Assert.AreEqual(expected, cost);
        }
        
        [Test]
        [TestCase("ABc,1,2", new string[] { "ABc,1,2" })]
        [TestCase("ABc,1,2;D,2,3", new string[] { "ABc,1,2", "D,2,3" })]
        public void GetMoves_Splits_Solution_Into_Moves(string solution, string[] expected)
        {
            var moves = Solution.GetMoves(solution);

            CollectionAssert.AreEqual(expected.ToList(), moves);
        }
    }
}