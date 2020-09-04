using System.Linq;
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
                Solution = string.Empty,
            };

            while (!State.IsDone(state))
            {
                // Check for wagons you can already remove and remove them
                state = ClearNonBlockedLines(state);
                
                // Sort lines by least wagons to move and unblock the one with least wagons to move
                state = UnblockEasiestLine(state);
            }

            return state.Solution;
        }

        public static State ClearNonBlockedLines(State state)
        {
            for (int i = 0; i < state.TrainLines.Length; i++)
            {
                var trainLine = state.TrainLines[i];
                var wagons = TrainLines.GetTrainLineWagons(trainLine);
                while (!string.IsNullOrEmpty(wagons) && wagons[0] == state.Destination)
                {
                    var weight = 0;
                    var wagonsToMove = wagons
                        .TakeWhile(wagon => wagon == state.Destination)
                        .TakeWhile(wagon => (weight += Wagon.GetWeight(wagon)) <= State.LocomotiveStrength)
                        .Aggregate(string.Empty, (current, wagon) => current + wagon);

                    if (!string.IsNullOrEmpty(wagonsToMove))
                    {
                        var move = Move.Create(wagonsToMove, i, TrainLines.TriageLineNumber);

                        state = State.ApplyMove(state, move);
                        
                        trainLine = state.TrainLines[i];
                        wagons = TrainLines.GetTrainLineWagons(trainLine);
                    }
                }
            }

            return state;
        }

        public static State UnblockEasiestLine(State state)
        {
            return state; // TODO
        }
    }
}