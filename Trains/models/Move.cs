using System;

namespace Trains.models
{
    public class Move
    {
        public const char MoveComponentsSeparator = ',';
        public const int MoveCost = 1;
        public const int DistanceCost = 1;

        public string Wagons { get; set; }
        
        public int From { get; set; }

        public int To { get; set; }

        // TODO Do I want this ?
        public static Move Create(string wagons, int from, int to)
        {
            return new Move
            {
                Wagons = wagons,
                From = from,
                To = to
            };
        }

        public static int GetCost(Move move)
        {
            return MoveCost + Math.Abs(move.From - move.To) * DistanceCost;
        }

        public override string ToString()
        {
            return $"{Wagons}{MoveComponentsSeparator}{From + 1}{MoveComponentsSeparator}{To + 1}";
        }

        public static Move Parse(string move)
        {
            var components = move.Split(MoveComponentsSeparator);

            return new Move
            {
                Wagons = components[0],
                From = int.Parse(components[1]) - 1,
                To = int.Parse(components[2]) - 1
            };
        }

        protected bool Equals(Move other)
        {
            return Wagons == other.Wagons && From == other.From && To == other.To;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Move) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Wagons, From, To);
        }
    }
}