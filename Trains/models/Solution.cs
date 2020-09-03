using System.Collections.Generic;
using System.Linq;

namespace Trains.models
{
    public static class Solution
    {
        public const char MoveSeparator = ';';
        
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
            return GetMoves(solution).Sum(Move.GetCost);
        }

        public static IEnumerable<Move> GetMoves(string solution)
        {
            return solution.Split(MoveSeparator).Select(Move.Parse);
        }
    }
}