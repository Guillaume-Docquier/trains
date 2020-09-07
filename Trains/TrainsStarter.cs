using System;
using System.Linq;
using Trains.Models;
using Trains.Solvers;

namespace Trains
{
    public static class TrainsStarter
    {
        public static string Start(string[] trainLines, char destination, int maximumExecutionTimeMinutes)
        {
            // Find a base solution with a heuristics based algorithm
            var bestSolution = HeuristicsSolver.Solve(trainLines, destination);

            if (!string.IsNullOrEmpty(bestSolution))
            {
                Console.WriteLine("\nHeuristics solution found:");
                Console.WriteLine(bestSolution);
                Console.WriteLine($"Moves: {Solution.GetMoves(bestSolution).Count()}, Cost: {Solution.GetCost(bestSolution)}");

                // Use the base solution to start a searching algorithm
                var maxExecutionTime = TimeSpan.FromMinutes(maximumExecutionTimeMinutes);
                Console.WriteLine($"\n[{DateTime.Now}] Starting searching algorithm. Max execution time: {maxExecutionTime}");

                ulong nbSolutions = 0;
                var startTime = DateTime.UtcNow;
                foreach (var solution in SearchingSolver.Solve(trainLines, destination, bestSolution))
                {
                    nbSolutions++;
                    var elapsed = DateTime.UtcNow - startTime;
                    if (!string.IsNullOrEmpty(solution.SolutionString))
                    {
                        bestSolution = solution.SolutionString;

                        Console.WriteLine($"\nBetter solution found after {elapsed}:");
                        Console.WriteLine(bestSolution);
                        Console.WriteLine($"Moves: {Solution.GetMoves(bestSolution).Count()}, Cost: {Solution.GetCost(bestSolution)}");
                    }

                    if (elapsed > maxExecutionTime)
                    {
                        break;
                    }
                }

                Console.WriteLine($"\nSearching algorithm explored {nbSolutions} solutions in {DateTime.UtcNow - startTime}");
            }

            return bestSolution;
        }
    }
}
