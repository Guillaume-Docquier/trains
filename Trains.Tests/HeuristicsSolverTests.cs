using System.Collections.Generic;
using NUnit.Framework;
using Trains.models;
using Trains.Solvers;

namespace Trains.Tests
{
    [TestFixture]
    public class HeuristicsSolverTests
    {
        public static IEnumerable<TestCaseData> ClearNonBlockedLinesReturnsNewStateWithUpdatedTrainLinesAndSolutionTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00ABC", "00BEF", "000AA" },
                        Destination = 'A',
                        Solution = ""
                    },
                    new State
                    {
                        TrainLines = new[] { "000BC", "00BEF", "00000" },
                        Destination = 'A',
                        Solution = "A,1,0;AA,3,0"
                    });
                
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00000", "ABDEF", "00000" },
                        Destination = 'B',
                        Solution = ""
                    },
                    new State
                    {
                        TrainLines = new[] { "00000", "ABDEF", "00000" },
                        Destination = 'B',
                        Solution = ""
                    });
            }
        }

        [TestCaseSource(nameof(ClearNonBlockedLinesReturnsNewStateWithUpdatedTrainLinesAndSolutionTestCases))]
        public void ClearNonBlockedLines_Returns_New_State_With_Updated_Train_Lines_And_Solution(State startState, State expected)
        {
            var finalState = HeuristicsSolver.ClearNonBlockedLines(startState);

            Assert.AreEqual(expected, finalState);
        }
        
        public static IEnumerable<TestCaseData> ClearNonBlockedLinesRespectsLocomotiveStrengthTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "CCCCC", "ACCEF", "000CC" },
                        Destination = 'C',
                        Solution = ""
                    },
                    new State
                    {
                        TrainLines = new[] { "00000", "ACCEF", "00000" },
                        Destination = 'C',
                        Solution = "CCC,1,0;CC,1,0;CC,3,0"
                    });
            }
        }

        [TestCaseSource(nameof(ClearNonBlockedLinesRespectsLocomotiveStrengthTestCases))]
        public void ClearNonBlockedLines_Respects_Locomotive_Strength(State startState, State expected)
        {
            var finalState = HeuristicsSolver.ClearNonBlockedLines(startState);

            Assert.AreEqual(expected, finalState);
        }
    }
}