using System.Collections.Generic;
using NUnit.Framework;
using Trains.Models;
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
                        Solution = new Solution("")
                    },
                    new State
                    {
                        TrainLines = new[] { "000BC", "00BEF", "00000" },
                        Destination = 'A',
                        Solution = new Solution("A,1,0;AA,3,0")
                    });
                
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00000", "ABDEF", "00000" },
                        Destination = 'B',
                        Solution = new Solution("")
                    },
                    new State
                    {
                        TrainLines = new[] { "00000", "ABDEF", "00000" },
                        Destination = 'B',
                        Solution = new Solution("")
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
                        Solution = new Solution("")
                    },
                    new State
                    {
                        TrainLines = new[] { "00000", "ACCEF", "00000" },
                        Destination = 'C',
                        Solution = new Solution("CCC,1,0;CC,1,0;CC,3,0")
                    });
            }
        }

        [TestCaseSource(nameof(ClearNonBlockedLinesRespectsLocomotiveStrengthTestCases))]
        public void ClearNonBlockedLines_Respects_Locomotive_Strength(State startState, State expected)
        {
            var finalState = HeuristicsSolver.ClearNonBlockedLines(startState);

            Assert.AreEqual(expected, finalState);
        }
        
        public static IEnumerable<TestCaseData> UnblockLineMovesAllNonDestinationWagonsOnLineToOtherLinesTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABDCC", "00DDD", "000DD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0,
                    new State
                    {
                        TrainLines = new[] { "000CC", "00DDD", "ABDDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,1,3")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABDDC", "00DDD", "000DD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0,
                    new State
                    {
                        TrainLines = new[] { "0000C", "0DDDD", "ABDDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,1,3;D,1,2")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "00DDD", "ABDCC", "000DD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1,
                    new State
                    {
                        TrainLines = new[] { "00DDD", "000CC", "ABDDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,2,3")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000DD", "ABDCC", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1,
                    new State
                    {
                        TrainLines = new[] { "ABDDD", "000CC", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,2,1")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000DD", "000CC", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1,
                    new State
                    {
                        TrainLines = new[] { "000DD", "000CC", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000DD", "00000", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1,
                    new State
                    {
                        TrainLines = new[] { "000DD", "00000", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
                                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "000DD", "00000", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1,
                    new State
                    {
                        TrainLines = new[] { "000DD", "00000", "00DDD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    });
            }
        }

        [TestCaseSource(nameof(UnblockLineMovesAllNonDestinationWagonsOnLineToOtherLinesTestCases))]
        public void UnblockLine_Moves_All_Non_Destination_Wagons_On_Line_To_Other_Lines(State startState, int lineNumber, State expected)
        {
            var finalState = HeuristicsSolver.UnblockLine(startState, lineNumber);

            Assert.AreEqual(expected, finalState);
        }
        
        public static IEnumerable<TestCaseData> UnblockLinePrioritizesFillingLinesThatAreDoneTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABDCC", "000DC", "000DD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0,
                    new State
                    {
                        TrainLines = new[] { "000CC", "000DC", "ABDDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,1,3")
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] { "ABDCC", "0000DC", "000DD" },
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0,
                    new State
                    {
                        TrainLines = new[] { "000CC", "0000DC", "ABDDD" },
                        Destination = 'C',
                        Solution = new Solution("ABD,1,3")
                    });
            }
        }

        [TestCaseSource(nameof(UnblockLinePrioritizesFillingLinesThatAreDoneTestCases))]
        public void UnblockLine_Prioritizes_Filling_Lines_That_Are_Done(State startState, int lineNumber, State expected)
        {
            var finalState = HeuristicsSolver.UnblockLine(startState, lineNumber);

            Assert.AreEqual(expected, finalState);
        }

        public static IEnumerable<TestCaseData> GetFullyClearableLineIndexTargetsLinesThatCanBeFullyClearedWithTheLeastWagonToMoveTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"ABDCC", "0000DC", "000DD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"DBDCC", "DDDDC", "DAADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    -1);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"0BDCC", "0DDDC", "0AADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"BBCBC", "0DDDC", "0AADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    -1);
            }
        }

        [TestCaseSource(nameof(GetFullyClearableLineIndexTargetsLinesThatCanBeFullyClearedWithTheLeastWagonToMoveTestCases))]
        public void GetFullyClearableLineIndex_Targets_Lines_That_Can_Be_Fully_Cleared_With_The_Least_Wagons_To_Move(State startState, int expected)
        {
            var lineIndex = HeuristicsSolver.GetFullyClearableLineIndex(startState);

            Assert.AreEqual(expected, lineIndex);
        }

        public static IEnumerable<TestCaseData> GetPartiallyClearableLineIndexTargetsLinesThatCanBeFullyClearedWithTheLeastWagonToMoveTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"ABDCC", "0000DC", "000DD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    1);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"DBDCC", "DDDDC", "DAADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    -1);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"0BDCC", "0DDDC", "0AADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0);
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"BBCBC", "0DDDC", "0AADD"},
                        Destination = 'C',
                        Solution = new Solution("")
                    },
                    0);
            }
        }

        [TestCaseSource(nameof(GetPartiallyClearableLineIndexTargetsLinesThatCanBeFullyClearedWithTheLeastWagonToMoveTestCases))]
        public void GetPartiallyClearableLineIndex_Targets_Lines_That_Can_Uncover_The_Next_Destination_Wagon_With_The_Least_Wagons_To_Move(State startState, int expected)
        {
            var lineIndex = HeuristicsSolver.GetPartiallyClearableLineIndex(startState);

            Assert.AreEqual(expected, lineIndex);
        }
    }
}