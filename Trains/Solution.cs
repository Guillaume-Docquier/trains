using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
    public class Solution
    {
        public const char MoveSeparator = ';';
        public const char MoveComponentsSeparator = ',';
        public const int MoveCost = 1;
        public const int DistanceCost = 1;

        public static string Create(char[] wagons, int from, int to)
        {
            return $"{new string(wagons)}{MoveComponentsSeparator}{from}{MoveComponentsSeparator}{to}";
        }
        
        public static string AddMove(string solution, char[] wagons, int from, int to)
        {
            return $"{solution}{MoveSeparator}{Create(wagons, from, to)}";
        }

        public static int GetSolutionCost(string solution)
        {
            return GetMoves(solution).Sum(GetMoveCost);
        }

        public static List<string> GetMoves(string solution)
        {
            return solution.Split(MoveSeparator).ToList();
        }

        public static int GetMoveCost(string move)
        {
            var components = move.Split(MoveComponentsSeparator);

            var from = int.Parse(components[1]);
            var to = int.Parse(components[2]);

            return MoveCost + Math.Abs(from - to) * DistanceCost;
        }
    }
}