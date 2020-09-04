using System.Linq;
using Trains.ExtensionMethods;

namespace Trains.models
{
    public static class TrainLines
    {
        public const int TriageLineNumber = -1;
        public const char FreeSpaceChar = '0';

        public static string GetTrainLineWagons(string trainLine)
        {
            return trainLine.Replace(FreeSpaceChar.ToString(), "");
        }
        
        public static string GetTrainLineFreeSpace(string trainLine)
        {
            return trainLine
                .TakeWhile(wagon => wagon == FreeSpaceChar)
                .Aggregate(string.Empty, (current, wagon) => current + wagon);
        }
        
        public static string[] ApplyMove(string[] trainLines, Move move)
        {
            trainLines[move.From] = RemoveWagonsFromTrainLine(trainLines[move.From], move.Wagons);
            if (move.To != TriageLineNumber)
            {
                trainLines[move.To] = AddWagonsToTrainLine(trainLines[move.To], move.Wagons);
            }

            return trainLines;
        }
        
        public static string RemoveWagonsFromTrainLine(string trainLine, string wagonsToRemove)
        {
            var freeSlots = Enumerable
                .Range(0, wagonsToRemove.Length)
                .Aggregate(string.Empty, (current, _) => current + FreeSpaceChar);

            return trainLine.ReplaceFirst(wagonsToRemove, freeSlots);
        }

        public static string AddWagonsToTrainLine(string trainLine, string wagonsToAdd)
        {
            var remainingFreeSlots = GetTrainLineFreeSpace(trainLine).Substring(wagonsToAdd.Length);

            return $"{remainingFreeSlots}{wagonsToAdd}{GetTrainLineWagons(trainLine)}";
        }
    }
}