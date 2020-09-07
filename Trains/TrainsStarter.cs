using System;
using System.Linq;
using Trains.models;
using Trains.Solvers;

namespace Trains
{
    public class TrainsStarter : ITrainsStarter
    {
        private readonly TimeSpan _maxExecutionTime = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Cette méthode sera appelée par les tests.
        /// </summary>
        /// <param name="trainLines">Un array de string avec les lignes de trains.</param>
        /// <param name="destination">La lettre de la destination.</param>
        /// <returns>Retourne une liste de mouvements à faire pour obtenir la solution.</returns>
        public string Start(string[] trainLines, char destination)
        {
            // Find a base solution with a heuristics based algorithm
            var bestSolution = HeuristicsSolver.Solve(trainLines, destination);
            Console.WriteLine("\nHeuristics solution found:");
            Console.WriteLine(bestSolution);
            Console.WriteLine($"Moves: {Solution.GetMoves(bestSolution).Count()}, Cost: {Solution.GetCost(bestSolution)}");

            if (!string.IsNullOrEmpty(bestSolution))
            {
                // Use the base solution to start a searching algorithm
                Console.WriteLine($"\nStarting search algorithm. Max execution time: {this._maxExecutionTime}");
                var nbSolutions = 0;
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

                    if (elapsed > this._maxExecutionTime)
                    {
                        break;
                    }
                }

                Console.WriteLine($"\nSearch algorithm explored {nbSolutions} solutions in {DateTime.UtcNow - startTime}");
            }

            return bestSolution;
        }
    }
}
