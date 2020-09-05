using System;
using System.Linq;
using Trains.models;
using Trains.Solvers;

namespace Trains
{
    public class TrainsStarter : ITrainsStarter
    {
        private readonly TimeSpan _maxExecutionTime = TimeSpan.FromSeconds(10);

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
            if (!string.IsNullOrEmpty(bestSolution))
            {
                // Use the base solution to start a searching algorithm
                var startTime = DateTime.UtcNow;
                foreach (var solution in SearchingSolver.Solve(trainLines, destination, bestSolution))
                {
                    bestSolution = solution;

                    var elapsed = DateTime.UtcNow - startTime;
                    if (elapsed > this._maxExecutionTime)
                    {
                        break;
                    }

                    Console.WriteLine($"\nSolution found after {elapsed} of {this._maxExecutionTime}:");
                    Console.WriteLine(solution);
                    Console.WriteLine($"Moves: {Solution.GetMoves(solution).Count()}, Cost: {Solution.GetCost(solution)}");
                }
            }

            return bestSolution;
        }
    }
}
