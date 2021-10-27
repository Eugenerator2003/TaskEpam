using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.ChessPieces.Tests
{
    [TestClass()]
    public class PawnTests
    {
        [DataTestMethod()]
        [DataRow(3, 2, 3, 3, ChessPiece.Color.White)]
        public void MotoToTest(int x_initial, int y_initial, int x_expected, int y_expected, ChessPiece.Color color)
        {
            FieldCoordinate initialCoordinate = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate expectedCoordinate = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(initialCoordinate, color);
            pawn.MoveTo(expectedCoordinate);
            Assert.AreEqual(expectedCoordinate, pawn.Coordinate);
        }

        [DataTestMethod()]
        [DataRow(4, 4, 5, 5, ChessPiece.Color.White)]
        public void BeatTest(int x_initial, int y_initial, int x_expected, int y_expected, ChessPiece.Color color)
        {
            FieldCoordinate initialCoordinate = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate expectedCoordinate = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(initialCoordinate, color);
            pawn.Beat(expectedCoordinate);
            Assert.AreEqual(expectedCoordinate, pawn.Coordinate);
        }

        [TestMethod()]
        public void CanBeatTest()
        {

        }

        [TestMethod()]
        public void PawnTest()
        {

        }

        [TestMethod()]
        public void ToStringTest()
        {

        }
    }
}