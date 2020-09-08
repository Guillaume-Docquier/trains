using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Models;

namespace Trains.Solvers
{
    public class SearchingSolver
    {
        private Solution _bestSolution;
        private readonly Dictionary<string, int> _visitedStates;

        private SearchingSolver(string bestSolution)
        {
            this._bestSolution = new Solution(bestSolution);
            this._visitedStates = new Dictionary<string, int>();
        }

        public static IEnumerable<Solution> Solve(string[] trainLines, char destination, string bestSolution)
        {
            var state = new State
            {
                TrainLines = trainLines,
                Destination = destination,
                Solution = new Solution(""),
            };
            
            return new SearchingSolver(bestSolution).Solve(state, GetAllUsefulMoves(state), true);
        }

        private IEnumerable<Solution> Solve(State state, List<Move> moves, bool logProgress = false)
        {
            var numberOfMoves = moves.Count;
            if (logProgress)
            {
                Console.WriteLine($"Solve has {numberOfMoves} moves to explore");
            }

            for (var i = 0; i < numberOfMoves; i++)
            {
                var move = moves[i];
                var newState = State.ApplyMove(state, move);
                var newCost = newState.Solution.Cost;
                if (newCost >= this._bestSolution.Cost ||
                    !HasPotential(newState, this._bestSolution.Cost) ||
                    !this.TryStoreVisitedState(newState.TrainLines, newCost))
                {
                    yield return Solution.NoSolution;
                }
                else if (State.IsDone(newState))
                {
                    this._bestSolution = newState.Solution;

                    yield return this._bestSolution;
                }
                else
                {
                    foreach (var solution in Solve(newState, GetAllUsefulMoves(newState)))
                    {
                        yield return solution;
                    }
                }

                if (logProgress)
                {
                    Console.WriteLine($"Explored {i + 1} moves out of {numberOfMoves}");
                }
            }
        }

        public static bool HasPotential(State state, int maxCost)
        {
            var estimatedAdditionalCost = GetEstimatedAdditionalCost(state);

            return state.Solution.Cost + estimatedAdditionalCost < maxCost;
        }

        public static int GetEstimatedAdditionalCost(State state)
        {
            var estimatedAdditionalMoves = 0;
            foreach (var trainLine in state.TrainLines)
            {
                var wagons = TrainLines.GetTrainLineWagons(trainLine);
                if (wagons == string.Empty)
                {
                    continue;
                }

                var groupsOfWagons = wagons.Split(state.Destination).ToList();
                if (groupsOfWagons.All(wagon => wagon == string.Empty))
                {
                    estimatedAdditionalMoves += Optimizations.Ceiling(groupsOfWagons.Count, TrainLines.LocomotiveStrength);
                }
                else if (groupsOfWagons.Count == 2 && groupsOfWagons[0] == string.Empty)
                {
                    estimatedAdditionalMoves += 1;
                }
                else if (groupsOfWagons.Count == 2)
                {
                    var numberOfWagonMoves = Optimizations.Ceiling(groupsOfWagons[0].Length, TrainLines.LocomotiveStrength);

                    estimatedAdditionalMoves += numberOfWagonMoves + 1;
                }
                else if (groupsOfWagons.Count > 2)
                {
                    var groupsOfWagonsToMove = groupsOfWagons.Where(group => !string.IsNullOrEmpty(group)).ToList();
                    var endsWithDestinationWagons = groupsOfWagons.Last() == string.Empty;
                    if (!endsWithDestinationWagons)
                    {
                        groupsOfWagonsToMove.RemoveAt(groupsOfWagonsToMove.Count - 1);
                    }
                    var numberOfWagonMoves = groupsOfWagonsToMove.Select(group => Optimizations.Ceiling(group.Length, TrainLines.LocomotiveStrength)).Sum();
                    var numberOfDestinationWagonMoves = RemoveConsecutiveDuplicates(trainLine).Count(wagon => wagon == state.Destination);

                    estimatedAdditionalMoves += numberOfWagonMoves + numberOfDestinationWagonMoves;
                }
            }

            return estimatedAdditionalMoves * (Move.MoveCost + Move.DistanceCost);
        }

        public static string RemoveConsecutiveDuplicates(string trainLine)
        {
            var strResult = new StringBuilder();
            foreach (var element in trainLine)
            {
                if (strResult.Length == 0 || strResult[^1] != element)
                {
                    strResult.Append(element);
                }
            }
            
            return strResult.ToString();
        }

        public static List<Move> GetAllUsefulMoves(State state)
        {
            // For each trainLine
            // Move 1 to max wagons to every other line
            var moves = new List<Move>();
            for (var originTrainLineIndex = 0; originTrainLineIndex < state.TrainLines.Length; originTrainLineIndex++)
            {
                var trainLine = state.TrainLines[originTrainLineIndex];
                if (TrainLines.IsDone(trainLine, state.Destination))
                {
                    continue;
                }

                var maxWagonsToMove = TrainLines.GetMaximumMovableWagons(trainLine);
                for (var numberOfWagonsToMove = 1; numberOfWagonsToMove <= maxWagonsToMove.Length; numberOfWagonsToMove++)
                {
                    var wagonsToMove = maxWagonsToMove.Substring(0, numberOfWagonsToMove);
                    for (var targetTrainLineIndex = TrainLines.TriageLineNumber; targetTrainLineIndex < state.TrainLines.Length; targetTrainLineIndex++)
                    {
                        if (targetTrainLineIndex != originTrainLineIndex)
                        {
                            var move = Move.Create(wagonsToMove, originTrainLineIndex, targetTrainLineIndex);
                            if (TrainLines.IsValidMove(state.TrainLines, state.Destination, move))
                            {
                                moves.Add(move);
                            }
                        }
                    }
                }
            }

            // Shuffling the moves doesn't seem to improve the performance of the algorithm
            // I'm not sure if it's because of my test example or if it is applicable to every problem
            // It really looks like it's NOT helping
            // moves.Shuffle();

            return moves;
        }

        // Returns true if it's the best cost for this state we've seen so far
        private bool TryStoreVisitedState(string[] trainLines, int cost)
        {
            var stateKey = string.Concat(trainLines);
            if(this._visitedStates.TryGetValue(stateKey, out var bestCost) && cost >= bestCost)
            {
                return false;
            }

            this._visitedStates[stateKey] = cost;
            return true;
        }
    }
}