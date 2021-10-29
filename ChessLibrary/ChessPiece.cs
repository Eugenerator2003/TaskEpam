using System;
using System.Collections.Generic;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Class of Chess piece. Contains piece's color and it coordinate on chessboard.
    /// </summary>
    public abstract class ChessPiece : ICloneable
    {
        /// <summary>
        /// Enumeration of colors of chess pieces.
        /// </summary>
        public enum Color
        {
            /// <summary>
            /// White color.
            /// </summary>
            White,
            /// <summary>
            /// Black color.
            /// </summary>
            Black 
        }

        /// <summary>
        /// Property for getting color of piece.
        /// </summary>
        public Color PieceColor { get; }

        /// <summary>
        /// Property of piece coordinate on chessboard.
        /// </summary>
        public FieldCoordinate Coordinate
        {
            get;
            private protected set;
        }

        /// <summary>
        /// Property for getting X coordinate of piece.
        /// </summary>
        public int X { get => Coordinate.X; }
        /// <summary>
        /// Property for getting Y coordinate of piece.
        /// </summary>
        public int Y { get => Coordinate.Y; }

        /// <summary>
        /// Method for checking given coordinate for validity.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <returns>True if coordinate is valid.</returns>
        private protected static bool ValidCoordinate(FieldCoordinate coordinate)
        {
            if (coordinate.X < 1 || coordinate.X > 8 || coordinate.Y < 1 || coordinate.Y > 8)
                return false;
            return true;
        }

        /// <summary>
        /// Method for checking piece possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if piece is can to move.</returns>
        public abstract bool CanMove(FieldCoordinate coordinate);

        /// <summary>
        /// Method for moving piece to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if piece was moved to coordinate.</returns>
        public abstract bool MoveTo(FieldCoordinate coordinate);

        /// <summary>
        /// Method for checking piece possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if piece is can to move to coordinate through given piece.</returns>
        public abstract bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate);

        /// <summary>
        /// Constructor of chess piece.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of chess piece.</param>
        public ChessPiece(FieldCoordinate coordinate, Color color)
        {
            this.Coordinate = coordinate;
            this.PieceColor = color;
        }

        /// <summary>
        /// Method for convert from ChessPiece to String.
        /// </summary>
        /// <returns>ChessPiece converted to String.</returns>
        public override string ToString()
        {
            return $"{Coordinate}";
        }

        /// <summary>
        /// Method for comparsion current chess piece with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current chess piece.</returns>
        public override bool Equals(object obj)
        {
            return obj is ChessPiece piece &&
                   PieceColor == piece.PieceColor &&
                   EqualityComparer<FieldCoordinate>.Default.Equals(Coordinate, piece.Coordinate);
        }

        /// <summary>
        /// Method for getting hash code of piece.
        /// </summary>
        /// <returns>Hash code of piece.</returns>
        public override int GetHashCode()
        {
            return PieceColor.GetHashCode() + Coordinate.GetHashCode();
        }

        /// <summary>
        /// Method for getting clone object of current piece.
        /// </summary>
        /// <returns>Clone object of current piece.</returns>
        public abstract object Clone();
    }
}
