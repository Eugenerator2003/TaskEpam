using System;
using System.Collections.Generic;
using ChessLibrary.ChessPieces;

namespace ChessLibrary.PieceFabric
{
    /// <summary>
    /// Static class of fabric of chess pieces.
    /// </summary>
    public static class ChessPieceFabric
    {
        /// <summary>
        /// Method for getting stantard sets of White and Black pieces with their initial position.
        /// </summary>
        /// <param name="White">Collection of White pieces.</param>
        /// <param name="Black">Collection of Black pieces.</param>
        public static void GetStandartComplect(out List<ChessPiece> White, out List<ChessPiece> Black)
        {
            White = new List<ChessPiece>();
            Black = new List<ChessPiece>();
            int coordinate_x, i;
            coordinate_x = 0;
            for (i = 0; i < 16; i++)
            {
                coordinate_x++;
                switch (i)
                {   
                    case (0):
                    case (1):
                    case (2):
                    case (3):
                    case (4):
                    case (5):
                    case (6):
                    case (7):
                        White.Add(new Pawn(new FieldCoordinate(i + 1, 2), ChessPiece.Color.White));
                        Black.Add(new Pawn(new FieldCoordinate(i + 1, 7), ChessPiece.Color.Black));
                        break;
                    case (8):
                    case (15):
                        White.Add(new Rook(new FieldCoordinate(i - 7, 1), ChessPiece.Color.White));
                        Black.Add(new Rook(new FieldCoordinate(i - 7, 8), ChessPiece.Color.Black));
                        break;
                    case (9):
                    case (14):
                        White.Add(new Knight(new FieldCoordinate(i - 7, 1), ChessPiece.Color.White));
                        Black.Add(new Knight(new FieldCoordinate(i - 7, 8), ChessPiece.Color.Black));
                        break;
                    case (10):
                    case (13):
                        White.Add(new Bishop(new FieldCoordinate(i - 7, 1), ChessPiece.Color.White));
                        Black.Add(new Bishop(new FieldCoordinate(i - 7, 8), ChessPiece.Color.Black));
                        break;
                    case (11):
                        White.Add(new Queen(new FieldCoordinate(i - 7, 1), ChessPiece.Color.White));
                        Black.Add(new Queen(new FieldCoordinate(i - 7, 8), ChessPiece.Color.Black));
                        break;
                    case (12):
                        White.Add(new King(new FieldCoordinate(i - 7, 1), ChessPiece.Color.White));
                        Black.Add(new King(new FieldCoordinate(i - 7, 8), ChessPiece.Color.Black));
                        break;
                }
            }
        }
    }
}
