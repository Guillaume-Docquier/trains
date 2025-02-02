﻿using System.Linq;
using Trains.ExtensionMethods;

namespace Trains.Models
{
    public static class TrainLines
    {
        public const int LocomotiveStrength = 3;
        public const int TriageLineNumber = -1;
        private const char FreeSpaceChar = '0';
        private static readonly string FreeSpaceString = FreeSpaceChar.ToString();

        public static string[] ApplyMove(string[] trainLines, Move move)
        {
            var newTrainLines = (string[])trainLines.Clone();

            newTrainLines[move.From] = RemoveWagonsFromTrainLine(newTrainLines[move.From], move.Wagons);
            if (move.To != TriageLineNumber)
            {
                newTrainLines[move.To] = AddWagonsToTrainLine(newTrainLines[move.To], move.Wagons);
            }

            return newTrainLines;
        }

        public static bool IsDone(string trainLine, char destination)
        {
            return !trainLine.Contains(destination);
        }

        public static string GetTrainLineWagons(string trainLine)
        {
            return trainLine.Replace(FreeSpaceString, "");
        }
        
        public static string GetTrainLineFreeSpace(string trainLine)
        {
            return new string(trainLine.TakeWhile(wagon => wagon == FreeSpaceChar).ToArray());
        }

        public static int GetNumberOfWagonsToMoveToFreeTheLine(string trainLine, char destination)
        {
            // We count every non destination wagon that's before the last destination wagon
            var wagons = GetTrainLineWagons(trainLine);
            var lastIndexOfDestinationWagon = wagons.LastIndexOf(destination);

            if (lastIndexOfDestinationWagon != -1)
            {
                return wagons
                    .Substring(0, lastIndexOfDestinationWagon)
                    .Count(wagon => wagon != destination);
            }

            return 0;
        }

        public static int GetNumberOfWagonsToMoveToUnblockTheLine(string trainLine, char destination)
        {
            // We count every non destination wagon that's before the first destination wagon
            var wagons = GetTrainLineWagons(trainLine);
            var indexOfDestinationWagon = wagons.IndexOf(destination);

            if (indexOfDestinationWagon != -1)
            {
                return wagons
                    .Substring(0, indexOfDestinationWagon)
                    .Count(wagon => wagon != destination);
            }

            return 0;
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
            var existingWagons = GetTrainLineWagons(trainLine);
            
            return $"{remainingFreeSlots}{wagonsToAdd}{existingWagons}";
        }

        public static bool IsValidMove(string[] trainLines, char destination, Move move)
        {
            if (move.To == TriageLineNumber)
            {
                return move.Wagons.All(wagon => wagon == destination);
            }

            return GetTrainLineFreeSpace(trainLines[move.To]).Length >= move.Wagons.Length;
        }

        public static string GetMaximumMovableWagons(string trainLine)
        {
            var nbWagons = 0;
            return GetTrainLineWagons(trainLine)
                .TakeWhile(wagon => (nbWagons += 1) <= LocomotiveStrength)
                .Aggregate(string.Empty, (current, wagon) => current + wagon);
        }
    }
}