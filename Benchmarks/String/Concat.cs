using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Benchmarks.String
{
    public static class Concat
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int StringLength = 9;
        private const int TestDataCount = 1000000;
        private const int StringsPerTest = 20;
        private static readonly Dictionary<string, Func<string[], string>> Methods = new Dictionary<string, Func<string[], string>> {
            {nameof(StringJoin), StringJoin},
            {nameof(StringConcatenate), StringConcatenate},
            {nameof(StringBuilder), StringBuilder},
            {nameof(ForeachPlusOperator), ForeachPlusOperator},
            {nameof(LinqAggregatePlusOperator), LinqAggregatePlusOperator},
            {nameof(ForeachStringInterpolation), ForeachStringInterpolation},
            {nameof(LinqAggregateStringInterpolation), LinqAggregateStringInterpolation},
        };

        public static void Benchmark()
        {
            Console.WriteLine($"\nStarting String.Concat benchmark on {Methods.Count} methods with {TestDataCount} test data");
            var results = new Dictionary<string, TimeSpan>();
            var stopWatch = new Stopwatch();

            Console.WriteLine("Generating test data");
            var rand = new Random();
            var testData = new List<string[]>();
            for (var i = 0; i < TestDataCount; i++)
            {
                var testStrings = new string[StringsPerTest];
                for (var j = 0; j < StringsPerTest; j++)
                {
                    var stringChars = new char[StringLength];
                    for (var k = 0; k < stringChars.Length; k++)
                    {
                        stringChars[k] = Chars[rand.Next(Chars.Length)];
                    }

                    testStrings[j] = new string(stringChars);
                }
                
                testData.Add(testStrings);
            }

            Console.WriteLine($"Benchmarking");
            foreach (var method in Methods)
            {
                Console.WriteLine($"{method.Key}...");
                stopWatch.Restart();
                foreach (var strings in testData)
                {
                    method.Value(strings);
                }
                stopWatch.Stop();
                results.Add(method.Key, stopWatch.Elapsed);
            }

            Console.WriteLine("\nString.Concat benchmark results:");
            var rank = 1;
            foreach (var result in results.OrderBy(result => result.Value))
            {
                Console.WriteLine($"#{rank++} {result.Key}: {result.Value}");
            }
        }

        public static string StringJoin(string[] strings)
        {
            return string.Join("", strings);
        }

        public static string StringConcatenate(string[] strings)
        {
            return string.Concat(strings);
        }

        public static string StringBuilder(string[] strings)
        {
            var sb = new StringBuilder();
            foreach (var testString in strings)
            {
                sb.Append(testString);
            }

            return sb.ToString();
        }

        public static string ForeachPlusOperator(string[] strings)
        {
            var result = "";
            foreach (var testString in strings)
            {
                result += testString;
            }

            return result;
        }

        public static string LinqAggregatePlusOperator(string[] strings)
        {
            return strings.Aggregate(string.Empty, (current, testString) => current + testString);
        }

        public static string ForeachStringInterpolation(string[] strings)
        {
            var result = "";
            foreach (var testString in strings)
            {
                result = $"{result}{testString}";
            }

            return result;
        }

        public static string LinqAggregateStringInterpolation(string[] strings)
        {
            return strings.Aggregate("", (current, testString) => $"{current}{testString}");
        }
    }
}