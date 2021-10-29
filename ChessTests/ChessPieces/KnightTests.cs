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
    public class KnightTests
    {
        [DataTestMethod()]
        [DataRow(5, 3, 4, 5)]
        [DataRow(5, 6, 6, 8)]
        [DataRow(6, 4, 8, 3)]
        [DataRow(3, 5, 1, 4)]
        [DataRow(6, 3, 4, 4)]
        public void CanMoveTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Knight knight = new Knight(coordinateInitial, ChessPiece.Color.White);
            Assert.IsTrue(knight.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(4, 4, 5, 6, 5, 6)]
        [DataRow(7, 7, 8, 5, 8, 5)]
        [DataRow(3, 4, 1, 4, 1, 4)]
        [DataRow(2, 6, 3, 8, 3, 8)]
        public void CanMoveThroughTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Knight knight = new Knight(coordinateInitial, ChessPiece.Color.White);
            Bishop bishop = new Bishop(coordinatePiece, ChessPiece.Color.White);
            Assert.IsFalse(knight.CanMoveThrough(bishop, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(5, 3, 4, 5)]
        [DataRow(5, 6, 6, 8)]
        [DataRow(6, 4, 8, 3)]
        [DataRow(3, 5, 1, 4)]
        [DataRow(6, 3, 4, 4)]
        public void MoveToTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Knight knight = new Knight(coordinateInitial, ChessPiece.Color.White);
            Knight knightExpteced = new Knight(coordinateExpected, ChessPiece.Color.White);
            knight.MoveTo(coordinateExpected);
            Assert.AreEqual(knightExpteced, knight);
        }
    }
}