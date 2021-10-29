using System;
using System.Collections.Generic;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Class of chess rook.
    /// </summary>
    public class Rook : ChessPiece
    {
        /// <summary>
        /// Method for checking rook possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if rook is can to move.</returns>
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            return this.Coordinate.isOnLine(coordinate) > 0;
        }

        /// <summary>
        /// Method for checking rook possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if rook is can to move to coordinate through given piece.</returns>
        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            bool result = true;

            int x_max = this.Coordinate.X > coordinate.X ? this.Coordinate.X : coordinate.X;
            int x_min = this.Coordinate.X < coordinate.X ? this.Coordinate.X : coordinate.X;
            int y_max = this.Coordinate.Y > coordinate.Y ? this.Coordinate.Y : coordinate.Y;
            int y_min = this.Coordinate.Y < coordinate.Y ? this.Coordinate.Y : coordinate.Y;

            if (this.Coordinate.isOnLine(coordinate) > 0)
            {
                if ((piece.Coordinate.X == this.Coordinate.X
                && (piece.Coordinate.Y < y_max && piece.Coordinate.Y > y_min)))
                {
                    result = false;
                }
                else if (this.Coordinate.Y == coordinate.Y && this.Coordinate.X != coordinate.X
                         && (piece.Coordinate.Y == this.Coordinate.Y
                         && (piece.Coordinate.X < x_max && piece.Coordinate.X > x_min)))
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method for moving rook to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if rook was moved to coordinate.</returns>
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
        /// Constructor of rook.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of rook.</param>
        public Rook(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        /// <summary>
        /// Method for convert from Rook to String.
        /// </summary>
        /// <returns>Name and coordinate of current rook.</returns>
        public override string ToString()
        {
            return "R" + base.ToString();
        }

        /// <summary>
        /// Method for getting clone object of current rook.
        /// </summary>
        /// <returns>Clone object of current rook.</returns>
        public override object Clone()
        {
            return new Rook(this.Coordinate, this.PieceColor);
        }

        /// <summary>
        /// Method for comparsion current rook with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current rook.</returns>
        public override bool Equals(object obj)
        {
            return obj is Rook rook && rook.Coordinate == this.Coordinate && rook.PieceColor == this.PieceColor;
        }
    }
}
