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
        [TestCase("000DE", "ABC")]
        public void AddWagonsToTrainLine_Does_Not_Mutate_Train_Line(string trainLine, string wagonsToAdd)
        {
            var newTrainLine = TrainLines.AddWagonsToTrainLine(trainLine, wagonsToAdd);

            Assert.AreNotEqual(trainLine, newTrainLine);
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
        [TestCase("00CDE", "CD")]
        public void RemoveWagonsFromTrainLine_Does_Not_Mutate_Train_Line(string trainLine, string wagonsToAdd)
        {
            var newTrainLine = TrainLines.RemoveWagonsFromTrainLine(trainLine, wagonsToAdd);

            Assert.AreNotEqual(trainLine, newTrainLine);
        }

        [Test]
        [TestCase(new[] { "000AB", "00000", "00000" }, "AB,1,2", new[] { "00000", "000AB", "00000" })]
        [TestCase(new[] { "00ABC", "0000D", "0000E" }, "AB,1,2", new[] { "0000C", "00ABD", "0000E" })]
        [TestCase(new[] { "ABCAB", "0000D", "0000E" }, "AB,1,2", new[] { "00CAB", "00ABD", "0000E" })]
        public void ApplyMove_Removes_Wagons_From_Line_And_Adds_Wagons_To_Line(string[] trainLines, string moveString, string[] expected)
        {
            var move = Move.Parse(moveString);
            var newTrainLines = TrainLines.ApplyMove(trainLines, move);

            Assert.AreEqual(expected, newTrainLines);
        }

        [Test]
        [TestCase(new[] { "000AB", "00000", "00000" }, "AB,1,0", new[] { "00000", "00000", "00000" })]
        [TestCase(new[] { "00ABC", "00000", "00000" }, "AB,1,0", new[] { "0000C", "00000", "00000" })]
        [TestCase(new[] { "000AB", "ABCAB", "000DE" }, "AB,2,0", new[] { "000AB", "00CAB", "000DE" })]
        public void ApplyMove_Ignores_Triage_Line(string[] trainLines, string moveString, string[] expected)
        {
            var move = Move.Parse(moveString);
            var newTrainLines = TrainLines.ApplyMove(trainLines, move);

            Assert.AreEqual(expected, newTrainLines);
        }

        [Test]
        [TestCase(new[] { "000AB", "000CD", "000EF" }, "AB,1,2")]
        public void ApplyMove_Does_Not_Mutate_Train_Lines(string[] trainLines, string moveString)
        {
            var move = Move.Parse(moveString);
            var newTrainLines = TrainLines.ApplyMove(trainLines, move);

            Assert.AreNotEqual(newTrainLines, trainLines);
        }
        
        [Test]
        [TestCase("00000", 'C', true)]
        [TestCase("0000A", 'C', true)]
        [TestCase("000AB", 'C', true)]
        [TestCase("0000C", 'C', false)]
        [TestCase("000AC", 'C', false)]
        [TestCase("00BCA", 'C', false)]
        public void IsDone_Returns_True_When_No_Destination_Wagons(string trainLine, char destination, bool expected)
        {
            var isDone = TrainLines.IsDone(trainLine, destination);

            Assert.AreEqual(expected, isDone);
        }

        [Test]
        [TestCase("00000", 'C', 0)]
        [TestCase("0000A", 'C', 0)]
        [TestCase("000AB", 'C', 0)]
        [TestCase("0000C", 'C', 0)]
        [TestCase("000AC", 'C', 1)]
        [TestCase("00BCA", 'C', 1)]
        [TestCase("ABCCA", 'C', 2)]
        [TestCase("ACBCA", 'C', 1)]
        public void GetNumberOfWagonsToMoveToUnblockTheLine_Return_The_Number_Of_Wagons_Before_The_First_Destination_Wagon(string trainLine, char destination, int expected)
        {
            var numberOfWagonsToMove = TrainLines.GetNumberOfWagonsToMoveToUnblockTheLine(trainLine, destination);

            Assert.AreEqual(expected, numberOfWagonsToMove);
        }
        
        [Test]
        [TestCase("00000", 'C', 0)]
        [TestCase("0000A", 'C', 0)]
        [TestCase("000AB", 'C', 0)]
        [TestCase("0000C", 'C', 0)]
        [TestCase("000AC", 'C', 1)]
        [TestCase("00BCA", 'C', 1)]
        [TestCase("ABCCA", 'C', 2)]
        [TestCase("ACBCA", 'C', 2)]
        public void GetNumberOfWagonsToMoveToFreeTheLine_Return_The_Number_Of_Wagons_Before_The_First_Destination_Wagon(string trainLine, char destination, int expected)
        {
            var numberOfWagonsToMove = TrainLines.GetNumberOfWagonsToMoveToFreeTheLine(trainLine, destination);

            Assert.AreEqual(expected, numberOfWagonsToMove);
        }

        [Test]
        [TestCase(new[] { "00CCC", "000AA", "00000" }, 'C', "CCC,1,2", true)]
        [TestCase(new[] { "00CCC", "0000A", "00000" }, 'C', "CCC,1,2", true)]
        [TestCase(new[] { "00ABC", "00000", "0AAAA" }, 'C', "AB,1,3", false)]
        public void IsValidMove_Returns_True_When_There_Is_Enough_Free_Space_On_Target_Line(string[] trainLines, char destination, string moveString, bool expected)
        {
            var move = Move.Parse(moveString);
            var numberOfWagonsToMove = TrainLines.IsValidMove(trainLines, destination, move);

            Assert.AreEqual(expected, numberOfWagonsToMove);
        }

        [Test]
        [TestCase(new[] { "00CCC", "00000", "00000" }, 'C', "CCC,1,0", true)]
        [TestCase(new[] { "00ABC", "00000", "00000" }, 'C', "ABC,1,0", false)]
        [TestCase(new[] { "00ABD", "00000", "00000" }, 'C', "ABD,1,0", false)]
        public void IsValidMove_Returns_False_When_Trying_To_Move_Non_Destination_Wagons_To_Triage_Line(string[] trainLines, char destination, string moveString, bool expected)
        {
            var move = Move.Parse(moveString);
            var numberOfWagonsToMove = TrainLines.IsValidMove(trainLines, destination, move);

            Assert.AreEqual(expected, numberOfWagonsToMove);
        }
        
        [Test]
        [TestCase("ABCDE", "ABC")]
        [TestCase("000DE", "DE")]
        [TestCase("00000", "")]
        public void IsValidMove_Returns_False_When_Trying_To_Move_Non_Destination_Wagons_To_Triage_Line(string trainLine, string expected)
        {
            var wagonsToMove = TrainLines.GetMaximumMovableWagons(trainLine);

            Assert.AreEqual(expected, wagonsToMove);
        }
    }
}