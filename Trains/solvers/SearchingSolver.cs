using System.Collections.Generic;
using System.Threading;

namespace Trains.Solvers
{
    public static class SearchingSolver
    {
        public static IEnumerable<string> Solve(string[] trainLines, char destination, string bestSolution)
        {
            while (true)
            {
                Thread.Sleep(500);
                yield return bestSolution;
            }
        }
    }
}