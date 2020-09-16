using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Trains.Models
{
    public class Solution
    {
        public static readonly Solution NoSolution = new Solution(string.Empty);

        private const string MoveSeparator = ";";

        public int Cost { get; private set; }

        public int NumberOfMoves { get; private set; }
        
        public string SolutionString { get; private set; }

        public Solution(string solutionString)
        {
            SolutionString = solutionString;
            Cost = GetCost(solutionString);
            NumberOfMoves = GetMoves(solutionString).Count();
        }

        private Solution()
        {
        }

        public void AddMove(Move move)
        {
            SolutionString = string.IsNullOrEmpty(SolutionString) ?
                move.ToString() :
                string.Concat(SolutionString, MoveSeparator, move);

            Cost += Move.GetCost(move);
            NumberOfMoves++;
        }

        public Solution Clone()
        {
            return new Solution
            {
                SolutionString = SolutionString,
                Cost = Cost,
                NumberOfMoves = NumberOfMoves
            };
        }

        // You shouldn't use this if you're going to do it a lot, instantiate a Solution instead.
        public static int GetCost(string solution)
        {
            if (string.IsNullOrEmpty(solution))
            {
                return 0;
            }

            return GetMoves(solution).Sum(Move.GetCost);
        }

        public static IEnumerable<Move> GetMoves(string solution)
        {
            if (string.IsNullOrEmpty(solution))
            {
                return ImmutableList<Move>.Empty;
            }

            return solution.Split(MoveSeparator).Select(Move.Parse);
        }

        protected bool Equals(Solution other)
        {
            return Cost == other.Cost && NumberOfMoves == other.NumberOfMoves && SolutionString == other.SolutionString;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Solution) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Cost, NumberOfMoves, SolutionString);
        }
    }
}