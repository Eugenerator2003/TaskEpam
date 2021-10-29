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
    public class RookTests
    {
        [DataTestMethod()]
        [DataRow(1, 1, 1, 4)]
        [DataRow(3, 2, 7, 2)]
        [DataRow(6, 5, 6, 1)]
        [DataRow(8, 7, 5, 7)]
        public void CanMoveTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Rook rook = new Rook(coordinateInitial, ChessPiece.Color.White);
            Assert.IsTrue(rook.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(4, 3, 4, 6, 4, 7)]
        [DataRow(3, 6, 7, 6, 8, 6)]
        [DataRow(6, 8, 6, 3, 6, 1)]
        [DataRow(7, 5, 6, 5, 2, 5)]
        public void CanMoveThroughTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Rook rook = new Rook(coordinateInitial, ChessPiece.Color.White);
            Queen queen = new Queen(coordinatePiece, ChessPiece.Color.Black);
            Assert.IsFalse(rook.CanMoveThrough(queen, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(1, 1, 1, 4)]
        [DataRow(3, 2, 7, 2)]
        [DataRow(6, 5, 6, 1)]
        [DataRow(8, 7, 5, 7)]
        public void MoveToTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Rook rook = new Rook(coordinateInitial, ChessPiece.Color.White);
            Rook rookExpected = new Rook(coordinateExpected, ChessPiece.Color.White);
            rook.MoveTo(coordinateExpected);
            Assert.AreEqual(rookExpected, rook);
        }
    }
}