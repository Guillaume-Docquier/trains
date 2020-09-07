﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.models;

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

        // Returns true if it's the best cost for this state we've seen so far
        private bool TryStoreVisitedState(string[] trainLines, int cost)
        {
            var stateKey = string.Join("", trainLines);
            if(this._visitedStates.TryGetValue(stateKey, out var bestCost) && cost >= bestCost)
            {
                return false;
            }

            this._visitedStates[stateKey] = cost;
            return true;
        }

        private IEnumerable<Solution> Solve(State state, List<Move> moves)
        {
            foreach (var move in moves)
            {
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
                    foreach (var solution in Solve(newState, GetAllPossibleMoves(newState)))
                    {
                        yield return solution;
                    }
                }
            }
        }

        public static bool HasPotential(State state, int maxCost)
        {
            var estimatedAdditionalMoves = 0;
            foreach (var trainLine in state.TrainLines)
            {
                var groupsOfWagons = TrainLines.GetTrainLineWagons(trainLine).Split(state.Destination).ToList();
                if (groupsOfWagons.Count == 2 && groupsOfWagons[0] == string.Empty)
                {
                    estimatedAdditionalMoves += 1;
                }
                else if (groupsOfWagons.Count == 2)
                {
                    var numberOfWagonMoves = FastCeiling((double)groupsOfWagons[0].Length / TrainLines.LocomotiveStrength);

                    estimatedAdditionalMoves += numberOfWagonMoves + 1;
                }
                else if (groupsOfWagons.Count > 2)
                {
                    var groupsOfWagonsToMove = groupsOfWagons.Where(group => !string.IsNullOrEmpty(group)).ToList();
                    var endsWithDestinationWagons = groupsOfWagons.Last() == string.Empty;
                    var numberOfDestinationWagonMoves = groupsOfWagonsToMove.Count - (endsWithDestinationWagons ? 0 : 1);

                    if (!endsWithDestinationWagons)
                    {
                        groupsOfWagonsToMove.RemoveAt(groupsOfWagonsToMove.Count - 1);
                    }
                    var numberOfWagonMoves = groupsOfWagonsToMove.Select(group => FastCeiling((double)group.Length / TrainLines.LocomotiveStrength)).Sum();

                    estimatedAdditionalMoves += numberOfWagonMoves + numberOfDestinationWagonMoves;
                }
            }

            return state.Solution.Cost + estimatedAdditionalMoves * (Move.MoveCost + Move.DistanceCost) < maxCost;
        }

        // Apparently Math.Ceiling is very slow
        // https://stackoverflow.com/a/53520604
        // https://www.codeproject.com/Tips/700780/Fast-floor-ceiling-functions
        public static int FastCeiling(double number)
        {
            var rounded = (int)number;
            return rounded < number ? rounded + 1 : rounded;
        }

        public static string RemoveConsecutiveDuplicates(string trainLine)
        {
            var strResult = new StringBuilder();
            foreach (var element in trainLine.ToCharArray())
            {
                if (strResult.Length == 0 || strResult[^1] != element)
                {
                    strResult.Append(element);
                }
            }
            
            return strResult.ToString();
        }

        public static IEnumerable<Solution> Solve(string[] trainLines, char destination, string bestSolution)
        {
            var state = new State
            {
                TrainLines = trainLines,
                Destination = destination,
                Solution = new Solution(""),
            };
            
            return new SearchingSolver(bestSolution).Solve(state, GetAllPossibleMoves(state));
        }

        public static List<Move> GetAllPossibleMoves(State state)
        {
            // For each trainLine
            // Move 1 to max wagons to every other line
            var moves = new List<Move>();
            for (var originTrainLineIndex = 0; originTrainLineIndex < state.TrainLines.Length; originTrainLineIndex++)
            {
                var maxWagonsToMove = TrainLines.GetMaximumMovableWagons(state.TrainLines[originTrainLineIndex]);
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
            // moves.Shuffle();

            return moves;
        }
    }
}