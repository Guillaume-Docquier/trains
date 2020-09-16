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
        public void Test_Each_New_Method_Estimates_A_More_Accurate_Cost_Without_Overestimating(State state)
        {
            var cost1 = PotentialCostWithMinimalMoves(state);
            var cost2 = PotentialCostWithMinimalMovesAndMinimalDistance(state);
            var cost3 = PotentialCostWithMinimalMovesAndMinimalDistanceWithRealisticEvaluation(state);
            var cost4 = Solution.GetCost(HeuristicsSolver.Solve(state.TrainLines, state.Destination));

            Assert.GreaterOrEqual(cost2, cost1);
            Assert.GreaterOrEqual(cost3, cost2);
            Assert.GreaterOrEqual(cost4, cost3);
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