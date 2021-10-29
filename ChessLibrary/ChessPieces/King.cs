using System;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Constuctor of chess King.
    /// </summary>
    public class King : ChessPiece
    {
        /// <summary>
        /// Method for checking king possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if king is can to move.</returns>
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            bool result = false;
            int X_min = this.Coordinate.X - 1;
            int X_max = this.Coordinate.X + 1;
            int Y_min = this.Coordinate.Y - 1;
            int Y_max = this.Coordinate.Y + 1;
            if (coordinate.X >= X_min && coordinate.X <= X_max && coordinate.Y >= Y_min && coordinate.Y <= Y_max)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method for checking king possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if king is can to move to coordinate through given piece.</returns>
        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            if (piece.Coordinate == coordinate && piece.PieceColor == this.PieceColor)
            {
                return false;
            }
            else
            {
                return CanMove(coordinate);
            }
        }

        /// <summary>
        /// Method for moving king to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if king was moved to coordinate.</returns>
        public override bool MoveTo(FieldCoordinate coordinate)
        {
            bool result = CanMove(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        /// <summary>
        /// Constructor of king.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of king.</param>
        public King(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        /// <summary>
        /// Method for convert from King to String.
        /// </summary>
        /// <returns>Name and coordinate of current king.</returns>
        public override string ToString()
        {
            return "K" + base.ToString();
        }

        /// <summary>
        /// Method for comparsion current king with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current king.</returns>
        public override bool Equals(object obj)
        {
            return obj is King king && king.Coordinate == this.Coordinate && king.PieceColor == this.PieceColor;
        }

        /// <summary>
        /// Method for getting hash code of king.
        /// </summary>
        /// <returns>Hash code of king.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Method for getting clone object of current king.
        /// </summary>
        /// <returns>Clone object of current king.</returns>
        public override object Clone()
        {
            return new King(this.Coordinate, this.PieceColor);
        }
    }
}
