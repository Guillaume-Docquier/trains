using System;
using System.Linq;

namespace Trains.models
{
    public class State
    {
        public string[] TrainLines { get; set; }

        public char Destination { get; set; }

        public string Solution { get; set; }

        public static State ApplyMove(State state, Move move)
        {
            return new State
            {
                TrainLines = models.TrainLines.ApplyMove(state.TrainLines, move),
                Destination = state.Destination,
                Solution = models.Solution.AddMove(state.Solution, move)
            };
        }

        public static bool IsDone(State state)
        {
            return state.TrainLines.All(trainLine => models.TrainLines.IsDone(trainLine, state.Destination));
        }

        protected bool Equals(State other)
        {
            return TrainLines.SequenceEqual(other.TrainLines) && Destination == other.Destination && Solution == other.Solution;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((State) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TrainLines, Destination, Solution);
        }
    }
}