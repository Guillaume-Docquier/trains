using System;
using System.Collections.Generic;
using Trains.models;

namespace Trains.Solvers
{
    public class SearchingSolver
    {
        public string BestSolution;
        public int BestSolutionCost;

        public SearchingSolver(string bestSolution)
        {
            this.BestSolution = bestSolution;
            this.BestSolutionCost = Solution.GetCost(bestSolution);
        }

        public static IEnumerable<string> Solve(string[] trainLines, char destination, string bestSolution)
        {
            var state = new State
            {
                TrainLines = trainLines,
                Destination = destination,
                Solution = string.Empty,
            };
            
            return new SearchingSolver(bestSolution).Solve(state, GetAllPossibleMoves(state));
        }
        
        public IEnumerable<string> Solve(State state, List<Move> moves)
        {
            foreach (var move in moves)
            {
                var newState = State.ApplyMove(state, move);
                var newCost = Solution.GetCost(newState.Solution);
                if (State.IsDone(newState) && newCost < this.BestSolutionCost)
                {
                    this.BestSolution = newState.Solution;
                    this.BestSolutionCost = newCost;

                    yield return this.BestSolution;
                }
                else if (newCost >= this.BestSolutionCost)
                {
                    yield return string.Empty;
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

            // TODO Handle cycles to speed up the search
            return moves;
        }
    }
}