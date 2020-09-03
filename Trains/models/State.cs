using System.Linq;

namespace Trains.models
{
    public class State
    {
        public string[] TrainLines { get; set; }

        public char Destination { get; set; }

        public string Solution { get; set; }

        public static State ClearNonBlockedLines(State state)
        {
            for (int i = 0; i < state.TrainLines.Length; i++)
            {
                var trainLine = state.TrainLines[i];
                
                // Get the move
                var wagons = models.TrainLines.GetTrainLineWagons(trainLine);
                var wagonsToMove = wagons
                    .TakeWhile(wagon => wagon == state.Destination)
                    .Aggregate(string.Empty, (current, wagon) => current + wagon);

                if (!string.IsNullOrEmpty(wagonsToMove))
                {
                    var move = new Move
                    {
                        Wagons = wagonsToMove,
                        From = i,
                        To = models.TrainLines.TriageLineNumber
                    };

                    state = ApplyMove(state, move);
                }
            }

            return state;
        }

        public static State ApplyMove(State state, Move move)
        {
            return new State
            {
                TrainLines = models.TrainLines.ApplyMove(state.TrainLines, move),
                Destination = state.Destination,
                Solution = models.Solution.AddMove(state.Solution, move)
            };
        }
    }
}