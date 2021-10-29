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
    public class QueenTests
    {
        [DataTestMethod()]
        [DataRow(4, 1, 4, 8)]
        [DataRow(2, 8, 3, 8)]
        [DataRow(6, 4, 6, 2)]
        [DataRow(3, 5, 1, 5)]
        [DataRow(4, 1, 1, 4)]
        [DataRow(5, 4, 7, 2)]
        [DataRow(7, 6, 8, 7)]
        [DataRow(8, 8, 1, 1)]
        public void CanMoveTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Queen queen = new Queen(coordinateInitial, ChessPiece.Color.White);
            Assert.IsTrue(queen.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(6, 4, 3, 7, 2, 8)]
        [DataRow(4, 1, 4, 6, 4, 7)]
        [DataRow(5, 2, 7, 4, 8, 3)]
        [DataRow(3, 3, 7, 3, 8, 3)]
        [DataRow(2, 7, 2, 2, 2, 1)]
        [DataRow(7, 5, 5, 3, 3, 1)]
        [DataRow(8, 6, 4, 6, 3, 6)]
        [DataRow(4, 5, 4, 6, 4, 7)]
        public void CanMoveThroughTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Queen queen = new Queen(coordinateInitial, ChessPiece.Color.White);
            Pawn pawn = new Pawn(coordinatePiece, ChessPiece.Color.Black);
            Assert.IsFalse(queen.CanMoveThrough(pawn, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(4, 1, 4, 8)]
        [DataRow(2, 8, 3, 8)]
        [DataRow(6, 4, 6, 2)]
        [DataRow(3, 5, 1, 5)]
        [DataRow(4, 1, 1, 4)]
        [DataRow(5, 4, 7, 2)]
        [DataRow(7, 6, 8, 7)]
        [DataRow(8, 8, 1, 1)]
        public void MoveToTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Queen queen = new Queen(coordinateInitial, ChessPiece.Color.White);
            Queen queenExpected = new Queen(coordinateExpected, ChessPiece.Color.White);
            queen.MoveTo(coordinateExpected);
            Assert.AreEqual(queenExpected, queen);
        }


    }
}