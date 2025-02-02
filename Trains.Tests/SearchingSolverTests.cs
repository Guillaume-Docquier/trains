﻿using System.Collections.Generic;
using NUnit.Framework;
using Trains.Models;
using Trains.Solvers;

namespace Trains.Tests
{
    [TestFixture]
    public class SearchingSolverTests
    {
        public static IEnumerable<TestCaseData> GetAllUsefulMovesReturnsAllMovesThatAreValidForTheGivenStateAndIgnoresMovesFromLinesThatHaveNoDestinationWagonsTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"0AAAA", "0BBBB", "0CCCC"},
                        Destination = 'D',
                        Solution = new Solution("")
                    },
                    new List<Move>
                    {
                    });
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"00000", "00000", "00000"},
                        Destination = 'D',
                        Solution = new Solution("")
                    },
                    new List<Move>());
                yield return new TestCaseData(
                    new State
                    {
                        TrainLines = new[] {"AAAAA", "00BBBA", "00000"},
                        Destination = 'A',
                        Solution = new Solution("")
                    },
                    new List<Move>
                    {
                        Move.Parse("A,1,0"),
                        Move.Parse("AA,1,0"),
                        Move.Parse("AAA,1,0"),
                        Move.Parse("A,1,2"),
                        Move.Parse("AA,1,2"),
                        Move.Parse("A,1,3"),
                        Move.Parse("AA,1,3"),
                        Move.Parse("AAA,1,3"),
                        Move.Parse("B,2,3"),
                        Move.Parse("BB,2,3"),
                        Move.Parse("BBB,2,3"),
                    });
            }
        }

        [TestCaseSource(nameof(GetAllUsefulMovesReturnsAllMovesThatAreValidForTheGivenStateAndIgnoresMovesFromLinesThatHaveNoDestinationWagonsTestCases))]
        public void GetAllUsefulMoves_Returns_All_Moves_That_Are_Valid_For_The_Given_State_And_Ignores_Moves_From_Lines_That_Have_No_Destination_Wagons(State startState, List<Move> expected)
        {
            var moves = SearchingSolver.GetAllUsefulMoves(startState);

            CollectionAssert.AreEquivalent(expected, moves);
        }
    }
}