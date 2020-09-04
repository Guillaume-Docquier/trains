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
        [TestCase("ABc,1,2", "D,2,3", "ABc,1,2;D,2,3")]
        [TestCase("ABc,1,2;D,2,3", "ABD,5,6", "ABc,1,2;D,2,3;ABD,5,6")]
        public void AddMove_Formats_Moves_With_Separator(string baseSolution, string moveString, string expected)
        {
            var move = Move.Parse(moveString);

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
        
        public static IEnumerable<TestCaseData> GetMovesSplitsSolutionIntoMovesTestCases
        {
            get
            {
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
    }
}