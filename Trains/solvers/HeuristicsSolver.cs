using Trains.models;

namespace Trains.Solvers
{
    public static class HeuristicsSolver
    {
        public static string Solve(string[] trainLines, char destination)
        {
            var state = new State
            {
                TrainLines = trainLines,
                Destination = destination,
                Solution = string.Empty
            };

            // Check for wagons you can already remove and remove them
            state = State.ClearNonBlockedLines(state);

            // Sort lines by least wagons to move
            // Make moves to remove a wagon on the first line
            return state.Solution;
        }
    }
}