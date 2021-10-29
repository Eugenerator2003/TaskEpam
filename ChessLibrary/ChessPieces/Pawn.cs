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
            else if (piece.Coordinate == coordinate)
            {
                result = false;
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
        /// Method for checking pawn possibility to beat to given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <returns>True if pawn is can to beat.</returns>
        public bool CanBeat(ChessPiece piece)
        {
            if (piece.PieceColor == this.PieceColor)
                return false;

            bool result = false;
            if (PieceColor == Color.White)
            {
                if ((this.Coordinate.X + 1 == piece.Coordinate.X || this.Coordinate.X - 1 == piece.Coordinate.X)
                    && (this.Coordinate.Y + 1 == piece.Coordinate.Y))
                {
                    result = true;
                }
            }
            else
            {
                if ((this.Coordinate.X + 1 == piece.Coordinate.X || this.Coordinate.X - 1 == piece.Coordinate.X)
                   && (this.Coordinate.Y - 1 == piece.Coordinate.Y))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Method for pawn's beat the given piece.
        /// </summary>
        /// <param name="piece">Given piece.</param>
        /// <returns>True if the pawn has beaten the given piece</returns>
        public bool Beat(ChessPiece piece)
        {
            bool result = CanBeat(piece);
            if (result)
            {
                this.Coordinate = piece.Coordinate;
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
        /// Method for comparsion current pawn with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current pawn.</returns>
        public override bool Equals(object obj)
        {
            return obj is Pawn pawn && pawn.Coordinate == this.Coordinate && pawn.PieceColor == this.PieceColor;
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
