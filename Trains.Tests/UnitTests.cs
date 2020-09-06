using System;
using System.Linq;
using NUnit.Framework;
using Trains.models;
using Trains.Solvers;

namespace Trains.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private ITrainsStarter _trainStarter;

        [SetUp]
        public void Setup()
        {
            this._trainStarter = new TrainsStarter();
        }

        [Test]
        [TestCase(new[] { "0000ACDGC", "0000000DG" }, 'C', "A,1,2;C,1,0;DG,1,2;C,1,0")]
        [TestCase(new[] { "0000AGCAG", "00DCACGDG" }, 'C', "AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0")]
        public void TrainStarter_Finds_Good_Solutions(string[] trainLines, char destination, string expected)
        {
            var solution = this._trainStarter.Start(trainLines, destination);

            var cost = Solution.GetCost(solution);
            var expectedCost = Solution.GetCost(expected);

            Assert.LessOrEqual(cost, expectedCost);
        }
        
        [Test]
        [TestCase(new[] { "0000AGCAG", "00DCACGDG" }, 'C')]
        public void SearchingSolver_Can_Find_Better_Solutions_Than_HeuristicsSolver(string[] trainLines, char destination)
        {
            var heuristicsSolution = HeuristicsSolver.Solve(trainLines, destination);
            var searchingSolution = SearchingSolver.Solve(trainLines, destination, heuristicsSolution).Last(solution => !string.IsNullOrEmpty(solution));

            Assert.Less(Solution.GetCost(searchingSolution), Solution.GetCost(heuristicsSolution));
        }
    }   
}
