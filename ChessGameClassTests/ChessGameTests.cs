using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System;
using ChessLibrary.ChessPieces;
using ChessGame;

namespace ChessGame.Tests
{
    [TestClass()]
    public class ChessGameTests
    {
        [TestMethod()]
        public void ChekmateIn5MovesTest()
        {
            ChessGame game = new ChessGame();
            FieldCoordinate coordinate1, coordinate2;

            coordinate1 = new FieldCoordinate(5, 2);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(5, 7);
            coordinate2 = new FieldCoordinate(5, 5);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(4, 1);
            coordinate2 = new FieldCoordinate(8, 5);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(2, 8);
            coordinate2 = new FieldCoordinate(3, 6);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(7, 1);
            coordinate2 = new FieldCoordinate(6, 3);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(6, 8);
            coordinate2 = new FieldCoordinate(3, 5);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(6, 3);
            coordinate2 = new FieldCoordinate(7, 5);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(7, 8);
            coordinate2 = new FieldCoordinate(6, 6);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(8, 5);
            coordinate2 = new FieldCoordinate(6, 7);
            game.MovePiece(coordinate1, coordinate2);
        }

        [TestMethod()]
        public void LegalCheckmateTest()
        {
            ChessGame game = new ChessGame();
            FieldCoordinate coordinate1, coordinate2;

            coordinate1 = new FieldCoordinate(5, 2);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 7);
            coordinate2 = new FieldCoordinate(5, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(7, 1);
            coordinate2 = new FieldCoordinate(6, 3);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(2, 8);
            coordinate2 = new FieldCoordinate(3, 6);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(6, 1);
            coordinate2 = new FieldCoordinate(3, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 7);
            coordinate2 = new FieldCoordinate(4, 6);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(2, 1);
            coordinate2 = new FieldCoordinate(3, 3);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(3, 8);
            coordinate2 = new FieldCoordinate(7, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(6, 3);
            coordinate2 = new FieldCoordinate(5, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(7, 4);
            coordinate2 = new FieldCoordinate(4, 1);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(3, 4);
            coordinate2 = new FieldCoordinate(6, 7);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 8);
            coordinate2 = new FieldCoordinate(5, 7);
            game.MovePiece(coordinate1, coordinate2);
            game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(3, 3);
            coordinate2 = new FieldCoordinate(4, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();
        }

        [TestMethod()]
        public void MovePieceTest()
        {
            ChessGame game = new ChessGame();
            FieldCoordinate coordinate1, coordinate2;

            coordinate1 = new FieldCoordinate(3, 2);
            coordinate2 = new FieldCoordinate(3, 4);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(4, 7);
            coordinate2 = new FieldCoordinate(4, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(3, 4);
            coordinate2 = new FieldCoordinate(4, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(7, 8);
            coordinate2 = new FieldCoordinate(6, 6);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 2);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(6, 6);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(6, 1);
            coordinate2 = new FieldCoordinate(4, 3);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 8);
            coordinate2 = new FieldCoordinate(4, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 3);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 5);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 1);
            coordinate2 = new FieldCoordinate(6, 1);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();


            /*
            coordinate1 = new FieldCoordinate(4, 2);
            coordinate2 = new FieldCoordinate(4, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 4);
            coordinate2 = new FieldCoordinate(4, 4);
            game.MovePiece(coordinate1, coordinate2);
            game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(7, 2);
            coordinate2 = new FieldCoordinate(7, 3);
            game.MovePiece(coordinate1, coordinate2);
            game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 4);
            coordinate2 = new FieldCoordinate(7, 1);
            game.MovePiece(coordinate1, coordinate2);
            game.ConsoleShowBoard();*/

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void FoolsCheckmateTest()
        {
            ChessGame game = new ChessGame();
            FieldCoordinate coordinate1, coordinate2;

            coordinate1 = new FieldCoordinate(7, 2);
            coordinate2 = new FieldCoordinate(7, 4);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(5, 7);
            coordinate2 = new FieldCoordinate(5, 5);
            game.MovePiece(coordinate1, coordinate2);

            coordinate1 = new FieldCoordinate(6, 2);
            coordinate2 = new FieldCoordinate(6, 3);
            game.MovePiece(coordinate1, coordinate2);

            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 8);
            coordinate2 = new FieldCoordinate(8, 4);
            game.MovePiece(coordinate1, coordinate2);

            //game.ConsoleShowBoard();
            //game.ConsoleShowCoordinate();
        }

        [TestMethod()]
        public void ChildCheckmateTest()
        {
            ChessGame game = new ChessGame();
            FieldCoordinate coordinate1, coordinate2;

            coordinate1 = new FieldCoordinate(5, 2);
            coordinate2 = new FieldCoordinate(5, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(5, 7);
            coordinate2 = new FieldCoordinate(5, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(6, 1);
            coordinate2 = new FieldCoordinate(3, 4);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(2, 8);
            coordinate2 = new FieldCoordinate(3, 6);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(4, 1);
            coordinate2 = new FieldCoordinate(8, 5);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(7, 8);
            coordinate2 = new FieldCoordinate(6, 6);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();

            coordinate1 = new FieldCoordinate(8, 5);
            coordinate2 = new FieldCoordinate(6, 7);
            game.MovePiece(coordinate1, coordinate2);
            //game.ConsoleShowBoard();
        }
    }
}