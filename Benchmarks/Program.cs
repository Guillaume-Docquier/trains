using System;
using System.Collections.Generic;
using Benchmarks.Benchmarks;

namespace Benchmarks
{
    class Program
    {
        private static readonly Dictionary<string, Action> Benchmarks = new Dictionary<string, Action> {
            {nameof(MathCeiling), MathCeiling.Benchmark},
        };

        static void Main()
        {
            while (true)
            {
                Console.WriteLine($"\nBenchmark choices: [{string.Join(",", Benchmarks.Keys)}]");
                Console.WriteLine("Choose your benchmark:");
                var benchmarkName = Console.ReadLine();
                Benchmarks[benchmarkName!]();
            }
        }
    }
}