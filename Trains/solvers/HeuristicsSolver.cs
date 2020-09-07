using System.Linq;
using Trains.models;

namespace Trains.Solvers
{
    public static class HeuristicsSolver
    {
        public static string Solve(string[] trainLines, char destination)
        {
            var state = new State
            {
                TrainLines = trainLines,
                Destination = destination,
                Solution = new Solution(""),
            };

            state = ClearNonBlockedLines(state);
            while (state != null && !State.IsDone(state))
            {
                state = UnblockEasiestLine(state);
                if (state != null)
                {
                    state = ClearNonBlockedLines(state);
                }
            }

            return state?.Solution.SolutionString ?? string.Empty;
        }

        public static State ClearNonBlockedLines(State state)
        {
            for (int i = 0; i < state.TrainLines.Length; i++)
            {
                var trainLine = state.TrainLines[i];
                var wagons = TrainLines.GetTrainLineWagons(trainLine);
                while (wagons.FirstOrDefault() == state.Destination)
                {
                    var nbWagons = 0;
                    var wagonsToMove = wagons
                        .TakeWhile(wagon => wagon == state.Destination)
                        .TakeWhile(wagon => (nbWagons += 1) <= TrainLines.LocomotiveStrength)
                        .Aggregate(string.Empty, (current, wagon) => current + wagon);

                    if (!string.IsNullOrEmpty(wagonsToMove))
                    {
                        var move = Move.Create(wagonsToMove, i, TrainLines.TriageLineNumber);

                        state = State.ApplyMove(state, move);
                        
                        trainLine = state.TrainLines[i];
                        wagons = TrainLines.GetTrainLineWagons(trainLine);
                    }
                }
            }

            return state;
        }

        public static State UnblockEasiestLine(State state)
        {
            var fullyClearableLineIndex = GetFullyClearableLineIndex(state);
            if (fullyClearableLineIndex != -1)
            {
                return UnblockLine(state, fullyClearableLineIndex);
            }
            
            var partiallyClearableLineIndex = GetPartiallyClearableLineIndex(state);
            if (partiallyClearableLineIndex != -1)
            {
                return UnblockLine(state, partiallyClearableLineIndex);
            }

            return null;
        }

        public static int GetFullyClearableLineIndex(State state)
        {
            var numberOfWagonsToMovePerLine = state.TrainLines
                .Select((trainLine, trainLineIndex) => (numberOfWagonsToMove: TrainLines.GetNumberOfWagonsToMoveToFreeTheLine(trainLine, state.Destination), trainLineIndex))
                .Where(line => line.numberOfWagonsToMove > 0)
                .OrderBy(line => line.numberOfWagonsToMove);

            var freeSpacesPerLine = state.TrainLines
                .Select((trainLine, trainLineIndex) => TrainLines.GetTrainLineFreeSpace(trainLine).Length)
                .ToList();

            foreach (var (numberOfWagonsToMove, trainLineIndex) in numberOfWagonsToMovePerLine)
            {
                var totalFreeSpaces = freeSpacesPerLine
                    .Where((trainLine, index) => index != trainLineIndex)
                    .Sum();

                if (totalFreeSpaces >= numberOfWagonsToMove)
                {
                    return trainLineIndex;
                }
            }

            return -1;
        }

        public static int GetPartiallyClearableLineIndex(State state)
        {
            var numberOfWagonsToMovePerLine = state.TrainLines
                .Select((trainLine, trainLineIndex) => (numberOfWagonsToMove: TrainLines.GetNumberOfWagonsToMoveToUnblockTheLine(trainLine, state.Destination), trainLineIndex))
                .Where(line => line.numberOfWagonsToMove > 0)
                .OrderBy(line => line.numberOfWagonsToMove);

            var freeSpacesPerLine = state.TrainLines
                .Select((trainLine, trainLineIndex) => TrainLines.GetTrainLineFreeSpace(trainLine).Length)
                .ToList();

            foreach (var (numberOfWagonsToMove, trainLineIndex) in numberOfWagonsToMovePerLine)
            {
                var totalFreeSpaces = freeSpacesPerLine
                    .Where((trainLine, index) => index != trainLineIndex)
                    .Sum();

                if (totalFreeSpaces >= numberOfWagonsToMove)
                {
                    return trainLineIndex;
                }
            }

            return -1;
        }

        public static State UnblockLine(State state, int lineIndex)
        {
            var trainLine = state.TrainLines[lineIndex];
            var wagons = TrainLines.GetTrainLineWagons(trainLine);
            while (wagons.FirstOrDefault() != state.Destination)
            {
                var nbWagons = 0;
                var wagonsToMove = wagons
                    .TakeWhile(wagon => wagon != state.Destination)
                    .TakeWhile(wagon => (nbWagons += 1) <= TrainLines.LocomotiveStrength)
                    .ToList();

                if (!wagonsToMove.Any())
                {
                    break;
                }

                var linesOrderedByFreeSpace = state.TrainLines
                    .Select((line, index) => (line, index))
                    .Where(line => line.index != lineIndex)
                    .OrderByDescending(line => TrainLines.IsDone(line.line, state.Destination))
                    .ThenByDescending(line => TrainLines.GetTrainLineFreeSpace(line.line))
                    .Where(line => TrainLines.GetTrainLineFreeSpace(line.line).Length > 0)
                    .ToList();
                
                if (!linesOrderedByFreeSpace.Any())
                {
                    break;
                }

                var (toTrainLine, toTrainLineIndex) = linesOrderedByFreeSpace.First();
                var maxFreeSpace = TrainLines.GetTrainLineFreeSpace(toTrainLine).Length;
                nbWagons = 0;
                var wagonsToMoveString = wagonsToMove
                    .TakeWhile(wagon => (nbWagons += 1) <= maxFreeSpace)
                    .Aggregate(string.Empty, (current, wagon) => current + wagon);

                var move = Move.Create(wagonsToMoveString, lineIndex, toTrainLineIndex);

                state = State.ApplyMove(state, move);
                    
                trainLine = state.TrainLines[lineIndex];
                wagons = TrainLines.GetTrainLineWagons(trainLine);
            }

            return state;
        }
    }
}