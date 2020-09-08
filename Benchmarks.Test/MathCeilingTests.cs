using Benchmarks.Math;
using NUnit.Framework;

namespace Benchmarks.Test
{
    public class Tests
    {
        [Test]
        [TestCase(17, 4, 5)]
        [TestCase(16, 4, 4)]
        [TestCase(15, 4, 4)]
        [TestCase(14, 4, 4)]
        [TestCase(13, 4, 4)]
        [TestCase(12, 4, 3)]
        [TestCase(11, 10, 2)]
        [TestCase(0, 2, 0)]
        public void CastComparisonCeiling_Ceils_Correctly(int value, int divisor, int expected)
        {
            var result = Ceiling.CastComparisonCeiling(value, divisor);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(17, 4, 5)]
        [TestCase(16, 4, 4)]
        [TestCase(15, 4, 4)]
        [TestCase(14, 4, 4)]
        [TestCase(13, 4, 4)]
        [TestCase(12, 4, 3)]
        [TestCase(11, 10, 2)]
        [TestCase(0, 2, 0)]
        public void CastOffsetCeiling_Ceils_Correctly(int value, int divisor, int expected)
        {
            var result = Ceiling.CastComparisonCeiling(value, divisor);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(17, 4, 5)]
        [TestCase(16, 4, 4)]
        [TestCase(15, 4, 4)]
        [TestCase(14, 4, 4)]
        [TestCase(13, 4, 4)]
        [TestCase(12, 4, 3)]
        [TestCase(11, 10, 2)]
        [TestCase(0, 2, 0)]
        public void ModuloCeiling_Ceils_Correctly(int value, int divisor, int expected)
        {
            var result = Ceiling.CastComparisonCeiling(value, divisor);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(17, 4, 5)]
        [TestCase(16, 4, 4)]
        [TestCase(15, 4, 4)]
        [TestCase(14, 4, 4)]
        [TestCase(13, 4, 4)]
        [TestCase(12, 4, 3)]
        [TestCase(11, 10, 2)]
        [TestCase(0, 2, 0)]
        public void SumMinusOneCeiling_Ceils_Correctly(int value, int divisor, int expected)
        {
            var result = Ceiling.CastComparisonCeiling(value, divisor);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(17, 4, 5)]
        [TestCase(16, 4, 4)]
        [TestCase(15, 4, 4)]
        [TestCase(14, 4, 4)]
        [TestCase(13, 4, 4)]
        [TestCase(12, 4, 3)]
        [TestCase(11, 10, 2)]
        [TestCase(0, 2, 0)]
        public void BuiltIn_Ceils_Correctly(int value, int divisor, int expected)
        {
            var result = Ceiling.CastComparisonCeiling(value, divisor);

            Assert.AreEqual(expected, result);
        }
    }
}