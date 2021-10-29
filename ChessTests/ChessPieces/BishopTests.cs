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
    public class BishopTests
    {
        [DataTestMethod()]
        [DataRow(1, 1, 8, 8)]
        [DataRow(5, 5, 8, 2)]
        [DataRow(4, 6, 1, 3)]
        [DataRow(8, 4, 4, 8)]
        public void CanMoveTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Bishop bishop = new Bishop(coordinateInitial, ChessPiece.Color.White);
            Assert.IsTrue(bishop.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(6, 3, 3, 6, 1, 8)]
        [DataRow(4, 5, 5, 6, 6, 7)]
        [DataRow(4, 6, 6, 4, 7, 3)]
        [DataRow(7, 4, 5, 2, 4, 1)]
        public void CanMoveThroughTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Bishop bishop = new Bishop(coordinateInitial, ChessPiece.Color.White);
            Rook rook = new Rook(coordinatePiece, ChessPiece.Color.Black);
            Assert.IsFalse(bishop.CanMoveThrough(rook, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(1, 1, 8, 8)]
        [DataRow(5, 5, 8, 2)]
        [DataRow(4, 6, 1, 3)]
        [DataRow(8, 4, 4, 8)]
        public void MoveToTest(int x_initial, int y_initial, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Bishop bishop = new Bishop(coordinateInitial, ChessPiece.Color.White);
            Bishop bishopExpected = new Bishop(coordinateExpected, ChessPiece.Color.White);
            bishop.MoveTo(coordinateExpected);
            Assert.AreEqual(bishop, bishopExpected);
        }
    }
}