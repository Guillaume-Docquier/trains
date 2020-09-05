using System;
using NUnit.Framework;
using Trains.models;

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
        public void TestExampleCase1(string[] trainLines, char destination, string expected)
        {
            var solution = this._trainStarter.Start(trainLines, destination);

            var cost = Solution.GetCost(solution);
            var expectedCost = Solution.GetCost(expected);

            Assert.LessOrEqual(cost, expectedCost);
        }
    }   
}
