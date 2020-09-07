using System;
using System.IO;
using System.Linq;
using Trains.models;

namespace Trains
{
    public class Program
    {
        private const string DefaultFileName = "easy-01";

        static void Main(string[] args)
        {
            while (true)
            {
                var trainLines = new string[0];
                while (trainLines.Length == 0)
                {
                    Console.WriteLine("\nData file name (without .txt):");
                    var dataFileName = Console.ReadLine();
                    if (string.IsNullOrEmpty(dataFileName))
                    {
                        dataFileName = DefaultFileName;
                    }

                    try
                    {
                        trainLines = File.ReadAllLines($"samples/{dataFileName}.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                Console.WriteLine("\nDesired destination:");
                var destination = Console.ReadLine()!.ToUpper().First();

                Console.WriteLine("\nMaximum execution time, in minutes:");
                var maxExecutionTime = int.Parse(Console.ReadLine()!);

                var solution = TrainsStarter.Start(trainLines, destination, maxExecutionTime);
                if (!string.IsNullOrEmpty(solution))
                {
                    Console.WriteLine("\nBest solution found:");
                    Console.WriteLine(solution);
                    Console.WriteLine($"Moves: {Solution.GetMoves(solution).Count()}, Cost: {Solution.GetCost(solution)}");
                }
                else
                {
                    Console.WriteLine("\nNo solution found...");
                }
            }
        }
    }
}
