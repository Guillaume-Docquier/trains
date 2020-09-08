using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Benchmarks.Math
{
    public static class Ceiling
    {
        private const int TestDataCount = 500000000;
        private static readonly Dictionary<string, Func<int, int, int>> Methods = new Dictionary<string, Func<int, int, int>> {
            {nameof(CastComparisonCeiling), CastComparisonCeiling},
            {nameof(CastOffsetCeiling), CastOffsetCeiling},
            {nameof(ModuloCeiling), ModuloCeiling},
            {nameof(SumMinusOneCeiling), SumMinusOneCeiling},
            {nameof(BuiltIn), BuiltIn},
        };

        public static void Benchmark()
        {
            Console.WriteLine($"\nStarting Math.Ceiling benchmark on {Methods.Count} methods with {TestDataCount} test data");
            var results = new Dictionary<string, TimeSpan>();
            var stopWatch = new Stopwatch();

            Console.WriteLine("Generating test data");
            var rand = new Random();
            var testData = new List<(int value, int divisor)>();
            for (var i = 0; i < TestDataCount; i++)
            {
                testData.Add((rand.Next(15), rand.Next(5) + 1));
            }

            Console.WriteLine($"Benchmarking");
            foreach (var method in Methods)
            {
                Console.WriteLine($"{method.Key}...");
                stopWatch.Restart();
                foreach (var (value, divisor) in testData)
                {
                    method.Value(value, divisor);
                }
                stopWatch.Stop();
                results.Add(method.Key, stopWatch.Elapsed);
            }

            Console.WriteLine("\nMath.Ceiling benchmark results:");
            var rank = 1;
            foreach (var result in results.OrderBy(result => result.Value))
            {
                Console.WriteLine($"#{rank++} {result.Key}: {result.Value}");
            }
        }

        public static int CastComparisonCeiling(int value, int divisor)
        {
            var floatingPoint = (double)value / divisor;
            var rounded = (int)floatingPoint;

            return rounded < floatingPoint ? rounded + 1 : rounded;
        }
        
        public static int CastOffsetCeiling(int value, int divisor)
        {
            var floatingPoint = (double)value / divisor;

            return int.MaxValue - (int)(int.MaxValue - floatingPoint);
        }

        public static int ModuloCeiling(int value, int divisor)
        {
            return (value / divisor) + (value % divisor == 0 ? 0 : 1);
        }

        public static int SumMinusOneCeiling(int value, int divisor)
        {
            return (value + divisor - 1) / divisor;
        }

        public static int BuiltIn(int value, int divisor)
        {
            var floatingPoint = (double)value / divisor;

            return (int)System.Math.Ceiling(floatingPoint);
        }
    }
}