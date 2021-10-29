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
        [DataRow(6, 2, 6, 4)]
        [DataRow(5, 2, 5, 3)]
        public void CanMoveWhiteTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.White);
            Assert.IsTrue(pawn.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(5, 7, 5, 5)]
        [DataRow(3, 6, 3, 5)]
        public void CanMoveBlackTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.Black);
            Assert.IsTrue(pawn.CanMove(coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(5, 4, 5, 6, 5, 6)]
        [DataRow(2, 2, 2, 3, 2, 4)]
        public void CanMoveThroughWhiteTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Pawn pawn = new Pawn(coordinateInitial, ChessPiece.Color.White);
            Queen queen = new Queen(coordinatePiece, ChessPiece.Color.Black);
            Assert.IsFalse(pawn.CanMoveThrough(queen, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(8, 6, 8, 5, 8, 5)]
        [DataRow(4, 7, 4, 6, 4, 6)]
        public void CanMoveThroughBlackTest(int x_initial, int y_initial, int x_piece, int y_piece, int x_expected, int y_exptected)
        {
            FieldCoordinate coordinateInitial = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinatePiece = new FieldCoordinate(x_piece, y_piece);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_exptected);
            Pawn pawn = new Pawn(coordinateInitial, ChessPiece.Color.Black);
            Queen queen = new Queen(coordinatePiece, ChessPiece.Color.White);
            Assert.IsFalse(pawn.CanMoveThrough(queen, coordinateExpected));
        }

        [DataTestMethod()]
        [DataRow(6, 2, 6, 4)]
        [DataRow(5, 2, 5, 3)]
        public void MoveToWhiteTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.White);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.White);
            pawn.MoveTo(coordinateExpected);
            Assert.AreEqual(pawnExpected, pawn);
        }

        [DataTestMethod()]
        [DataRow(5, 7, 5, 5)]
        [DataRow(3, 6, 3, 5)]
        public void MoveToBlackTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.Black);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.Black);
            pawn.MoveTo(coordinateExpected);
            Assert.AreEqual(pawnExpected, pawn);
        }

        [DataTestMethod()]
        [DataRow(2, 2, 3, 3)]
        [DataRow(8, 4, 7, 5)]
        public void CanBeatWhiteTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.White);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.Black);
            Assert.IsTrue(pawn.CanBeat(pawnExpected));
        }

        [DataTestMethod()]
        [DataRow(4, 7, 3, 6)]
        [DataRow(5, 5, 6, 4)]
        public void CanBeatBlackTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.Black);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.White);
            Assert.IsTrue(pawn.CanBeat(pawnExpected));
        }

        [DataTestMethod()]
        [DataRow(2, 2, 3, 3)]
        [DataRow(8, 4, 7, 5)]
        public void BeatWhiteTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.White);
            Pawn pawnBeaten = new Pawn(coordinateExpected, ChessPiece.Color.Black);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.White);
            pawn.Beat(pawnBeaten);
            Assert.AreEqual(pawnExpected, pawn);
        }

        [DataTestMethod()]
        [DataRow(4, 7, 3, 6)]
        [DataRow(5, 5, 6, 4)]
        public void BeatBlackTest(int x_initial, int y_initial, int x_expected, int y_expected)
        {
            FieldCoordinate coordinateInital = new FieldCoordinate(x_initial, y_initial);
            FieldCoordinate coordinateExpected = new FieldCoordinate(x_expected, y_expected);
            Pawn pawn = new Pawn(coordinateInital, ChessPiece.Color.Black);
            Pawn pawnBeaten = new Pawn(coordinateExpected, ChessPiece.Color.White);
            Pawn pawnExpected = new Pawn(coordinateExpected, ChessPiece.Color.Black);
            pawn.Beat(pawnBeaten);
            Assert.AreEqual(pawnExpected, pawn);
        }
    }
}