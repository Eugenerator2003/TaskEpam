using System;

namespace ChessLibrary.ChessPieces
{
    /// <summary>
    /// Class of chess pawn.
    /// </summary>
    public class Pawn : ChessPiece
    {
        /// <summary>
        /// Method for checking pawn possibility to move to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if pawn is can to move.</returns>
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            bool result = false;
            if (PieceColor == ChessPiece.Color.White)
            {
                if (Coordinate.X == coordinate.X && (Coordinate.Y + 1 == coordinate.Y || Coordinate.Y == 2 && coordinate.Y == 4))
                {
                    result = true;
                }
            }
            else
            {
                if (Coordinate.X == coordinate.X && (Coordinate.Y - 1 == coordinate.Y || Coordinate.Y == 7 && coordinate.Y == 5))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Method for checking pawn possibility to move to given coordinate through given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if pawn is can to move to coordinate through given piece.</returns>
        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            bool result;
            if (this.Coordinate.Y == 2 && piece.Coordinate.Y == 3 && coordinate.Y == 4
                || this.Coordinate.Y == 7 && piece.Coordinate.Y == 6 && coordinate.Y == 5)
            {
                if (this.Coordinate.X == piece.Coordinate.X && this.Coordinate.X == coordinate.X)
                {
                    result = false;
                }
                else
                {
                    result = CanMove(coordinate);
                }
            }
            else
            {
                result = CanMove(coordinate);
            }
            return result;
        }

        /// <summary>
        /// Method for moving pawn to given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if pawn was moved to coordinate.</returns>
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
        /// Method for checking pawn possibility to beat to given coordinte.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if pawn is can to beat.</returns>
        public bool CanBeat(FieldCoordinate coordinate)
        {
            bool result = false;
            if (PieceColor == Color.White)
            {
                if ((this.Coordinate.X + 1 == coordinate.X || this.Coordinate.X - 1 == coordinate.X)
                    && (this.Coordinate.Y + 1 == coordinate.Y))
                {
                    result = true;
                }
            }
            else
            {
                if ((this.Coordinate.X + 1 == coordinate.X || this.Coordinate.X - 1 == coordinate.X)
                   && (this.Coordinate.Y - 1 == coordinate.Y))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Method for pawn's beat the given coordinate.
        /// </summary>
        /// <param name="coordinate">Given coordinate.</param>
        /// <returns>True if the pawn has beaten the given coordinate</returns>
        public bool Beat(FieldCoordinate coordinate)
        {
            bool result = CanBeat(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        /// <summary>
        /// Constructor of pawn.
        /// </summary>
        /// <param name="coordinate">Coordinate on chessboard.</param>
        /// <param name="color">Color of pawn.</param>
        public Pawn(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        /// <summary>
        /// Method for convert from Pawn to String.
        /// </summary>
        /// <returns>Name and coordinate of current pawn.</returns>
        public override string ToString()
        {
            return base.ToString(); 
        }

        /// <summary>
        /// Method for getting clone object of current pawn.
        /// </summary>
        /// <returns>Clone object of current pawn.</returns>
        public override object Clone()
        {
            return new Pawn(this.Coordinate, this.PieceColor);
        }
    }
}
