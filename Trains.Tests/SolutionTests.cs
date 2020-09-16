using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trains.Models;

namespace Trains.Tests
{
    [TestFixture]
    public class SolutionTests
    {
        [Test]
        [TestCase("ABc,1,2", "D,2,3", "ABc,1,2;D,2,3")]
        [TestCase("ABc,1,2;D,2,3", "ABD,5,6", "ABc,1,2;D,2,3;ABD,5,6")]
        public void AddMove_Formats_Moves_With_Separator(string baseSolution, string moveString, string expected)
        {
            var solution = new Solution(baseSolution);

            var move = Move.Parse(moveString);
            solution.AddMove(move);

            Assert.AreEqual(expected, solution.SolutionString);
        }
        
        [Test]
        [TestCase("", 0)]
        [TestCase("ABc,1,3", 1 + 2)]
        [TestCase("ABc,1,3;D,5,9", (1 + 2) + (1 + 4))]
        public void GetSolutionCost_Computes_Cost_Based_On_Moves(string solution, int expected)
        {
            var cost = Solution.GetCost(solution);

            Assert.AreEqual(expected, cost);
        }
        
        public static IEnumerable<TestCaseData> GetMovesSplitsSolutionIntoMovesTestCases
        {
            get
            {
                yield return new TestCaseData("", new List<Move>());
                yield return new TestCaseData("ABc,1,2", new List<Move> { Move.Parse("ABc,1,2") });
                yield return new TestCaseData("ABc,1,2;D,2,3", new List<Move> { Move.Parse("ABc,1,2"), Move.Parse("D,2,3") });
                yield return new TestCaseData("ABc,1,2;D,2,3;AAA,1,0", new List<Move> { Move.Parse("ABc,1,2"), Move.Parse("D,2,3"), Move.Parse("AAA,1,0") });
            }
        }

        [TestCaseSource(nameof(GetMovesSplitsSolutionIntoMovesTestCases))]
        public void GetMoves_Splits_Solution_Into_Moves(string solution, List<Move> expected)
        {
            var moves = Solution.GetMoves(solution);

            CollectionAssert.AreEqual(expected, moves.ToList());
        }
        
        [Test]
        [TestCase("", 0)]
        [TestCase("AB,1,2;DC,2,4", 2 + 3)]
        public void GetCost_Returns_The_Sum_Of_The_Cost_Of_The_Moves_In_The_Solution(string solution, int expected)
        {
            var cost = Solution.GetCost(solution);
            
            Assert.AreEqual(expected, cost);
        }
    }
}