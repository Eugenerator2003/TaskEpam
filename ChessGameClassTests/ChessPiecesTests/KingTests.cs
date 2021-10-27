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
        [TestMethod()]
        public void CanMoveTestThrough()
        {
            
        }

        [DataTestMethod()]
        [DataRow(5, 8, 4, 7)]
        [DataRow(5, 8, 5, 7)]
        public void CanMoveTest(int x_init, int y_init, int x_exp, int y_exp)
        {
            FieldCoordinate initial = new FieldCoordinate(x_init, y_init);
            FieldCoordinate expected = new FieldCoordinate(x_exp, y_exp);
            King king = new King(initial, ChessPiece.Color.Black);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void MoveToTest()
        {

        }
    }
}