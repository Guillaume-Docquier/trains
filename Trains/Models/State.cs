using System;
using System.Linq;

namespace Trains.Models
{
    public class State
    {
        public string[] TrainLines { get; set; }

        public char Destination { get; set; }

        public Solution Solution { get; set; }

        public static State ApplyMove(State state, Move move)
        {
            var newSolution = state.Solution.Clone();
            newSolution.AddMove(move);

            return new State
            {
                TrainLines = Models.TrainLines.ApplyMove(state.TrainLines, move),
                Destination = state.Destination,
                Solution = newSolution
            };
        }

        public static bool IsDone(State state)
        {
            return state.TrainLines.All(trainLine => Models.TrainLines.IsDone(trainLine, state.Destination));
        }

        protected bool Equals(State other)
        {
            return TrainLines.SequenceEqual(other.TrainLines) && Destination == other.Destination && Equals(Solution, other.Solution);
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