using NUnit.Framework;
using Trains.models;

namespace Trains.Tests
{
    [TestFixture]
    public class TrainLinesTests
    {
        [Test]
        [TestCase("D", "D")]
        [TestCase("GAD", "GAD")]
        [TestCase("00D", "D")]
        [TestCase("000AD", "AD")]
        public void GetTrainLineWagons_Returns_Train_Line_Without_Free_Spaces(string trainLine, string expected)
        {
            var wagons = TrainLines.GetTrainLineWagons(trainLine);

            Assert.AreEqual(expected, wagons);
        }

        [Test]
        [TestCase("D", "")]
        [TestCase("GAD", "")]
        [TestCase("00D", "00")]
        [TestCase("000AD", "000")]
        public void GetTrainLineFreeSpace_Returns_Train_Line_Without_Wagons(string trainLine, string expected)
        {
            var freeSpaces = TrainLines.GetTrainLineFreeSpace(trainLine);

            Assert.AreEqual(expected, freeSpaces);
        }

        [Test]
        [TestCase("0", "", "0")]
        [TestCase("0000", "A", "000A")]
        [TestCase("000DE", "ABC", "ABCDE")]
        public void AddWagonsToTrainLine_Replaces_Free_Spaces_With_Wagons(string trainLine, string wagonsToAdd, string expected)
        {
            var newTrainLine = TrainLines.AddWagonsToTrainLine(trainLine, wagonsToAdd);

            Assert.AreEqual(expected, newTrainLine);
        }

        [Test]
        [TestCase("A", "A", "0")]
        [TestCase("00B", "B", "000")]
        [TestCase("00CDE", "CD", "0000E")]
        public void RemoveWagonsFromTrainLine_Replaces_Wagons_To_Remove_With_Free_Spaces(string trainLine, string wagonsToRemove, string expected)
        {
            var newTrainLine = TrainLines.RemoveWagonsFromTrainLine(trainLine, wagonsToRemove);

            Assert.AreEqual(expected, newTrainLine);
        }

        [Test]
        [TestCase(new string[] { "000AB", "00000", "00000" }, "AB,1,2", new string[] { "00000", "000AB", "00000" })]
        [TestCase(new string[] { "00ABC", "0000D", "0000E" }, "AB,1,2", new string[] { "0000C", "00ABD", "0000E" })]
        [TestCase(new string[] { "ABCAB", "0000D", "0000E" }, "AB,1,2", new string[] { "00CAB", "00ABD", "0000E" })]
        public void ApplyMove_Removes_Wagons_From_Line_And_Adds_Wagons_To_Line(string[] trainLines, string moveString, string[] expected)
        {
            var move = Move.Parse(moveString);
            var newTrainLines = TrainLines.ApplyMove(trainLines, move);

            Assert.AreEqual(expected, newTrainLines);
        }

        [Test]
        [TestCase(new string[] { "000AB", "00000", "00000" }, "AB,1,0", new string[] { "00000", "00000", "00000" })]
        [TestCase(new string[] { "00ABC", "00000", "00000" }, "AB,1,0", new string[] { "0000C", "00000", "00000" })]
        [TestCase(new string[] { "000AB", "ABCAB", "000DE" }, "AB,2,0", new string[] { "000AB", "00CAB", "000DE" })]
        public void ApplyMove_Ignores_Triage_Line(string[] trainLines, string moveString, string[] expected)
        {
            var move = Move.Parse(moveString);
            var newTrainLines = TrainLines.ApplyMove(trainLines, move);

            Assert.AreEqual(expected, newTrainLines);
        }
    }
}