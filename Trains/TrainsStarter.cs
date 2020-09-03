using Trains.Solvers;

namespace Trains
{
    public class TrainsStarter : ITrainsStarter
    {
        /// <summary>
        /// Cette méthode sera appelée par les tests.
        /// </summary>
        /// <param name="trainLines">Un array de string avec les lignes de trains.</param>
        /// <param name="destination">La lettre de la destination.</param>
        /// <returns>Retourne une liste de mouvements à faire pour obtenir la solution.</returns>
        public string Start(string[] trainLines, char destination)
        {
            // Find a base solution with a heuristics based algorithm
            var heuristicsSolution = HeuristicsSolver.Solve(trainLines, destination);

            // Use the base solution to start a searching algorithm
            var searchingSolution = SearchingSolver.Solve(trainLines, destination, heuristicsSolution);

            // yield return good solutions as we go
            return searchingSolution;
        }
    }
}
