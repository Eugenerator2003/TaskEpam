using System;
using System.Collections.Generic;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Class of chess bishop.
    /// </summary>
    public class Bishop : ChessPiece
    {
        /// <summary>
        /// Method for checking bishop possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if bishop is can to move.</returns>
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            return this.Coordinate.isOnDiagonal(coordinate) != 0;
        }

        /// <summary>
        /// Method for checking bishop possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if bishop is can to move to coordinate through given piece.</returns>
        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            bool result = true;
            int diagonalPieceNum = this.Coordinate.isOnDiagonal(coordinate);
            int diagonalCoordinateNum = this.Coordinate.isOnDiagonal(coordinate);
            if (diagonalCoordinateNum != 0)
            {
                if (diagonalPieceNum == diagonalCoordinateNum)
                {
                    FieldCoordinate search = this.Coordinate;
                    int X_step = 0, Y_step = 0;
                    switch (diagonalPieceNum)
                    {
                        case (1):
                            X_step = 1;
                            Y_step = 1;
                            break;
                        case (2):
                            X_step = 1;
                            Y_step = -1;
                            break;
                        case (3):
                            X_step = -1;
                            Y_step = -1;
                            break;
                        case (4):
                            X_step = -1;
                            Y_step = 1;
                            break;
                    }
                    while (search.X <= 8 && search.X >= 1 && search.Y <= 8 && search.Y >= 1)
                    {
                        if (search == coordinate)
                        {
                            break;
                        }
                        else if (search == piece.Coordinate)
                        {
                            result = false;
                            break;
                        }
                        else
                        {
                            search.X += X_step;
                            search.Y += Y_step;
                        }
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Method for moving bishop to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if bishop was moved to coordinate.</returns>
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
        /// Constructor of bishop.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of bishop.</param>
        public Bishop(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        /// <summary>
        /// Method for convert from Bishop to String.
        /// </summary>
        /// <returns>Name and coordinate of current bishop.</returns>
        public override string ToString()
        {
            return "B" + base.ToString();
        }

        

        /// <summary>
        /// Method for getting clone object of current bishop.
        /// </summary>
        /// <returns>Clone object of current bishop.</returns>
        public override object Clone()
        {
            return new Bishop(this.Coordinate, this.PieceColor);
        }

        /// <summary>
        /// Method for comparsion current bishop with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current bishop.</returns>
        public override bool Equals(object obj)
        {
            return obj is Bishop bishop && bishop.Coordinate == this.Coordinate && bishop.PieceColor == this.PieceColor;
        }

        public override int GetHashCode()
        {
            int hashCode = -1219036983;
            hashCode = hashCode * -1521134295 + PieceColor.GetHashCode();
            hashCode = hashCode * -1521134295 + Coordinate.GetHashCode();
            return hashCode;
        }
    }
}
