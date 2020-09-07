using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains.models
{
    public class Solution
    {
        public static readonly Solution NoSolution = new Solution(string.Empty);

        private const string MoveSeparator = ";";

        public int Cost { get; private set; }
        
        public string SolutionString { get; private set; }

        public Solution(string solutionString)
        {
            SolutionString = solutionString;
            Cost = GetCost(solutionString);
        }

        private Solution()
        {
        }

        public void AddMove(Move move)
        {
            if (string.IsNullOrEmpty(SolutionString))
            {
                SolutionString = move.ToString();
                Cost = Move.GetCost(move);
            }
            else
            {
                SolutionString += $"{MoveSeparator}{move}";
                Cost += Move.GetCost(move);
            }
        }

        public Solution Clone()
        {
            return new Solution
            {
                SolutionString = SolutionString,
                Cost = Cost
            };
        }

        public static string AddMove(string solution, Move move)
        {
            if (string.IsNullOrEmpty(solution))
            {
                return move.ToString();
            }

            return $"{solution}{MoveSeparator}{move}";
        }

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
            return solution.Split(MoveSeparator).Select(Move.Parse);
        }
        protected bool Equals(Solution other)
        {
            return Cost == other.Cost && SolutionString == other.SolutionString;
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
            return HashCode.Combine(Cost, SolutionString);
        }
    }
}