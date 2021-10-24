using System;

namespace ChessClassLibrary.ChessPieces
{
    public class Pawn : ChessPiece
    {
        public override bool CanMove(FieldCoordinate coordinate)
        {
            bool result = false;
            if (PieceColor == Color.White)
            {
                if (Coordinate.X == coordinate.X && (Coordinate.Y == coordinate.Y + 1 || Coordinate.Y == 2 && coordinate.X == 4))
                {
                    result = true;
                }
            }
            else
            {
                if (Coordinate.X == coordinate.X && (Coordinate.Y == coordinate.Y - 1 || Coordinate.Y == 7 && coordinate.X == 5))
                {
                    result = true;
                }
            }
            return result;
        }

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

        public override bool MoveTo(FieldCoordinate coordinate)
        {
            bool result = CanMove(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        public Pawn(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }
    }
}
