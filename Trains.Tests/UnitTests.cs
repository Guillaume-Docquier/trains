using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trains.Models;
using Trains.Solvers;

namespace Trains.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        [TestCase(new[] { "0000ACDGC", "0000000DG" }, 'C', "A,1,2;C,1,0;DG,1,2;C,1,0")]
        [TestCase(new[] { "0000AGCAG", "00DCACGDG" }, 'C', "AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0")]
        public void TrainStarter_Finds_Good_Solutions(string[] trainLines, char destination, string expected)
        {
            var solution = TrainsStarter.Start(trainLines, destination, 1);

            var cost = Solution.GetCost(solution);
            var expectedCost = Solution.GetCost(expected);

            Assert.LessOrEqual(cost, expectedCost);
        }

        [Test]
        [TestCase(new[] { "0000AGCAG", "00DCACGDG" }, 'C')]
        public void SearchingSolver_Can_Find_Better_Solutions_Than_HeuristicsSolver(string[] trainLines, char destination)
        {
            var heuristicsSolution = HeuristicsSolver.Solve(trainLines, destination);
            var searchingSolution = SearchingSolver.Solve(trainLines, destination, heuristicsSolution).Last(solution => !string.IsNullOrEmpty(solution.SolutionString));

            Assert.Less(Solution.GetCost(searchingSolution.SolutionString), Solution.GetCost(heuristicsSolution));
        }

        [Test]
        public void Solution_Cost_Calculator()
        {
            var solutionString = "A,2,0;AA,5,0;A,6,0;CC,2,1;A,2,0;CDG,3,2;A,3,0;DG,3,2;A,3,0;DGD,4,3;G,4,3;A,4,0;DG,5,4;A,5,0";
            
            var solution = new Solution(solutionString);
            
            var nbMoves = Solution.GetMoves(solution.SolutionString).Count();
            var cost = solution.Cost;
        }

        [Test]
        public void Moves_Cost_Calculator()
        {
            var moves = new List<Move>
            {
                Move.Parse("A,2,0"),
                Move.Parse("CC,2,1"),
                Move.Parse("A,2,0"),
                Move.Parse("CDG,3,2"),
                Move.Parse("A,3,0"),
                Move.Parse("DG,3,2"),
                Move.Parse("A,3,0"),
                Move.Parse("DGD,4,3"),
                Move.Parse("G,4,3"),
                Move.Parse("A,4,0"),
                Move.Parse("AA,5,0"),
                Move.Parse("DG,5,4"),
                Move.Parse("A,5,0"),
                Move.Parse("A,6,0")
            };

            var solution = new Solution("");
            foreach (var move in moves)
            {
                solution.AddMove(move);
            }

            var nbMoves = Solution.GetMoves(solution.SolutionString).Count();
            var cost = solution.Cost;
        }
    }
}
