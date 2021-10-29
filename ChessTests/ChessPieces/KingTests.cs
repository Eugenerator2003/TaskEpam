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
    public class KingTests
    {
        [DataTestMethod()]
        [DataRow(5, 1, 6, 2)]
        [DataRow(3, 3, 2, 3)]
        [DataRow(7, 2, 8, 1)]
        [DataRow(5, 2, 5, 1)]
        public void CanMoveTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            King king = new King(coordinateInitial, ChessPiece.Color.White);
            Assert.IsTrue(king.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(3, 3, 2, 3, 2, 3)]
        [DataRow(5, 3, 6, 4, 6, 4)]
        [DataRow(8, 1, 7, 2, 7, 2)]
        [DataRow(4, 1, 3, 1, 3, 1)]
        public void CanMoveThroughTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            King king = new King(coordinateInitial, ChessPiece.Color.White);
            Knight knight = new Knight(coordinatePiece, ChessPiece.Color.White);
            Assert.IsFalse(king.CanMoveThrough(knight, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(5, 1, 6, 2)]
        [DataRow(3, 3, 2, 3)]
        [DataRow(7, 2, 8, 1)]
        [DataRow(5, 2, 5, 1)]
        public void MoveToTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            King king = new King(coordinateInitial, ChessPiece.Color.White);
            King kingExpected = new King(coordinateExpected, ChessPiece.Color.White);
            king.MoveTo(coordinateExpected);
            Assert.AreEqual(kingExpected, king);
        }
    }
}