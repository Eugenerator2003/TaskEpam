using System;

namespace ChessLibrary.ChessPieces
{
    public class Knight : ChessPiece
    {
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

        public override bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate)
        {
            return CanMove(coordinate);
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

        public Knight(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        public override string ToString()
        {
            return "N" + base.ToString();
        }

        public override object Clone()
        {
            return new Knight(this.Coordinate, this.PieceColor);
        }
    }
}
