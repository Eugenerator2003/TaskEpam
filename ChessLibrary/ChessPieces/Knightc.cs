using System;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Class of chess Knight.
    /// </summary>
    public class Knight : ChessPiece
    {
        /// <summary>
        /// Method for checking knight possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if knight is can to move.</returns>
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            bool result = false;
            if (((coordinate.X == this.Coordinate.X - 2 || coordinate.X == this.Coordinate.X + 2)
                && (coordinate.Y == this.Coordinate.Y - 1 || coordinate.Y == this.Coordinate.Y + 1))
                || ((coordinate.X == this.Coordinate.X - 1 || coordinate.X == this.Coordinate.X + 1)
                && (coordinate.Y == this.Coordinate.Y - 2 || coordinate.Y == this.Coordinate.Y + 2)))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Method for checking knight possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if knight is can to move to coordinate through given piece.</returns>
        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            bool result = piece.PieceColor == this.PieceColor && piece.Coordinate != coordinate || this.PieceColor != piece.PieceColor;
            return result && CanMove(coordinate);
        }

        /// <summary>
        /// Method for moving knight to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if knight was moved to coordinate.</returns>
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
        /// Constructor of knight.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of knight.</param>
        public Knight(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        /// <summary>
        /// Method for convert from Knight to String.
        /// </summary>
        /// <returns>Name and coordinate of current knight.</returns>
        public override string ToString()
        {
            return "N" + base.ToString();
        }

        /// <summary>
        /// Method for comparsion current knight with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current knight.</returns>
        public override bool Equals(object obj)
        {
            return obj is Knight knight && knight.Coordinate == this.Coordinate && knight.PieceColor == this.PieceColor;
        }

        /// <summary>
        /// Method for getting clone object of current knight.
        /// </summary>
        /// <returns>Clone object of current knight.</returns>
        public override object Clone()
        {
            return new Knight(this.Coordinate, this.PieceColor);
        }
    }
}
