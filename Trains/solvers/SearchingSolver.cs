using System.Collections.Generic;
using System.Linq;
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

        private static string GetTrainLinesString(string[] trainLines)
        {
            return trainLines.Aggregate(string.Empty, (current, trainLine) => current + trainLine);
        }

        // Returns true if it's the best cost for this state we've seen so far
        private bool TryStoreVisitedState(string[] trainLines, int cost)
        {
            var stateKey = GetTrainLinesString(trainLines);
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
                if (!this.TryStoreVisitedState(newState.TrainLines, newCost) || newCost >= this._bestSolution.Cost)
                {
                    yield return Solution.NoSolution;
                }
                else if (State.IsDone(newState) && newCost < this._bestSolution.Cost)
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