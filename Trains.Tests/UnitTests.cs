using System;
using NUnit.Framework;

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
        [Ignore("TrainStarter.Start doesn't terminate on purpose")]
        public void TestExampleCase1()
        {
            var test = new[] { "0000ACDGC", "0000000DG" };

            var result = this._trainStarter.Start(test, 'C');

            Assert.AreEqual("A,1,2;C,1,0;DG,1,2;C,1,0", result);
        }

        [Test]
        [Ignore("TrainStarter.Start doesn't terminate on purpose")]
        public void TestExampleCase2()
        {
            var test = new[] { "0000AGCAG", "00DCACGDG" };

            var result = this._trainStarter.Start(test, 'C');

            Assert.AreEqual("AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0", result);
        }
    }   
}
