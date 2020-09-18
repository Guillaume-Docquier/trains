using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trains.Models;
using Trains.Solvers;

namespace Trains.Tests
{
    public class HasPotentialTests
    {
        public static IEnumerable<TestCaseData> TestEachNewMethodEstimatesAMoreAccurateCostTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000AGCAG", "00DCACGDG", "0000BCDCG" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000CCCCC", "00CCCCCCC", "0000CCCCC" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000AAAAC", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00000AAAC", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000AAAACC", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000AAACC", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "AAAACBBBB", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "AAAACCBBB", "000000000", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "AAAABBBDD", "AAAABBBDD", "CAAABBBDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "AAAABBBDD", "AAAABBBDD", "0AAABBBDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ACACACACA", "CACACACAC", "000000000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000000DGG", "000CACGDG", "00AABCDCG" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000AADGG", "0000000DG", "CCGABCDCG" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
            }
        }

        [TestCaseSource(nameof(TestEachNewMethodEstimatesAMoreAccurateCostTestCases))]
        public void Test_Each_New_Method_Estimates_A_Higher_Cost_That_Is_Lower_Than_Than_The_Heuristic_Solution_Cost(State state)
        {
            var cost1 = PotentialCostWithMinimalMoves(state);
            var cost2 = PotentialCostWithMinimalMovesAndMinimalDistance(state);
            var cost3 = PotentialCostWithMinimalMovesAndMinimalDistanceWithRealisticEvaluation(state);
            var cost4 = Solution.GetCost(HeuristicsSolver.Solve(state.TrainLines, state.Destination));

            Assert.GreaterOrEqual(cost2, cost1);
            Assert.GreaterOrEqual(cost3, cost2);
            Assert.GreaterOrEqual(cost4, cost3);
        }
        
        public static IEnumerable<TestCaseData> PotentialEvaluationDoesNotOverestimateTheBestSolutionTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[]
                        {
                            "0000AGCAG",
                            "00DCACGDG",
                            "000000DCG",
                        },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    16);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[]
                        {
                            "0000AGCAG",
                            "00DCACGDG",
                            "0000BCDCG",
                        },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    21);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[]
                        {
                            "00000DGCDG",
                            "0000ACCACC",
                            "000CDGADGA",
                            "000DGDGADG",
                            "0000AADGAD",
                            "000ACGDCGD",
                        },
                        Destination = 'A',
                        Solution = new Solution("")
                    },
                    34);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[]
                        {
                            "00DGCDGEFA",
                            "0ACCACCGDE",
                            "CDGADGADEE",
                            "DGDGADGCAA",
                            "0AADGADCGD",
                            "ACGDCGDEGD",
                        },
                        Destination = 'D',
                        Solution = new Solution("")
                    },
                    78);
            }
        }

        [TestCaseSource(nameof(PotentialEvaluationDoesNotOverestimateTheBestSolutionTestCases))]
        public void Potential_Evaluation_Does_Not_Overestimate_The_Best_Solution(State state, int trueCost)
        {
            var potentialCost = PotentialCostWithMinimalMovesAndMinimalDistanceWithRealisticEvaluation(state);

            Assert.GreaterOrEqual(trueCost, potentialCost);
        }

        private int PotentialCostWithMinimalMoves(State state)
        {
            var estimatedAdditionalCost = 0;
            foreach (var trainLine in state.TrainLines)
            {
                var wagonsToMove = TrainLines.GetNumberOfWagonsToMoveToFreeTheLine(trainLine, state.Destination);
                var wagonsToMoveCost = Optimizations.Ceiling(wagonsToMove, TrainLines.LocomotiveStrength);

                var groupsOfDestinationWagonsToMove = SearchingSolver.RemoveConsecutiveDuplicates(trainLine).Count(wagon => wagon == state.Destination);
                
                estimatedAdditionalCost += wagonsToMoveCost + groupsOfDestinationWagonsToMove;
            }

            return estimatedAdditionalCost;
        }

        private int PotentialCostWithMinimalMovesAndMinimalDistance(State state)
        {
            return PotentialCostWithMinimalMoves(state) * (Move.MoveCost + Move.DistanceCost);
        }

        private int PotentialCostWithMinimalMovesAndMinimalDistanceWithRealisticEvaluation(State state)
        {
            return SearchingSolver.GetEstimatedAdditionalMoves(state) * (Move.MoveCost + Move.DistanceCost);
        }
    }
}