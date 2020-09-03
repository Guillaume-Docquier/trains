using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Trains.Tests
{
    [TestFixture]
    public class SolutionTests
    {
        [Test]
        [TestCase(new char[] { 'A', 'B', 'c' }, 1, 2, "ABc,1,2")]
        public void Create_Formats_Components_With_Separator(char[] wagons, int from, int to, string expected)
        {
            var solution = Solution.Create(wagons, from, to);

            Assert.AreEqual(expected, solution);
        }
        
        [Test]
        [TestCase("ABc,1,2", new char[] { 'D' }, 2, 3, "ABc,1,2;D,2,3")]
        public void AddMove_Formats_Moves_With_Separator(string baseSolution, char[] wagons, int from, int to, string expected)
        {
            var solution = Solution.AddMove(baseSolution, wagons, from, to);

            Assert.AreEqual(expected, solution);
        }
        
        [Test]
        [TestCase("ABc,1,3", 1 + 2)]
        [TestCase("D,5,9", 1 + 4)]
        public void GetMoveCost_Computes_Cost_Based_On_Components(string move, int expected)
        {
            var cost = Solution.GetMoveCost(move);

            Assert.AreEqual(expected, cost);
        }
        
        [Test]
        [TestCase("ABc,1,3", 1 + 2)]
        [TestCase("ABc,1,3;D,5,9", (1 + 2) + (1 + 4))]
        public void GetSolutionCost_Computes_Cost_Based_On_Moves(string solution, int expected)
        {
            var cost = Solution.GetSolutionCost(solution);

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