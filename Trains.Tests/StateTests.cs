using System.Collections.Generic;
using NUnit.Framework;
using Trains.Models;

namespace Trains.Tests
{
    [TestFixture]
    public class StateTests
    {
        public static IEnumerable<TestCaseData> ApplyMoveReturnsNewStateWithUpdatedTrainLinesAndSolutionTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00ABC", "00DEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("DE,1,2")
                    },
                    Move.Parse("AB,1,2"),
                    new State
                    {
                        TrainLines = new[] { "0000C", "ABDEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("DE,1,2;AB,1,2")
                    });
                
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "0000C", "ABDEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("DE,1,2;AB,1,2")
                    },
                    Move.Parse("C,1,0"),
                    new State
                    {
                        TrainLines = new[] { "00000", "ABDEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("DE,1,2;AB,1,2;C,1,0")
                    });
            }
        }

        [TestCaseSource(nameof(ApplyMoveReturnsNewStateWithUpdatedTrainLinesAndSolutionTestCases))]
        public void ApplyMove_Returns_New_State_With_Updated_Train_Lines_And_Solution(State startState, Move move, State expected)
        {
            var finalState = State.ApplyMove(startState, move);

            Assert.AreEqual(expected, finalState);
        }

        public static IEnumerable<TestCaseData> IsDoneReturnsTrueWhenNoDestinationWagonsAreLeftTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABADE", "ABBEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    true);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00000", "00000", "00000" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    true);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABABA", "000EF", "00C00" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    false);
            }
        }

        [TestCaseSource(nameof(IsDoneReturnsTrueWhenNoDestinationWagonsAreLeftTestCases))]
        public void IsDone_Returns_True_When_No_Destination_Wagons_Are_Left(State state, bool expected)
        {
            var isDone = State.IsDone(state);

            Assert.AreEqual(expected, isDone);
        }
    }
}