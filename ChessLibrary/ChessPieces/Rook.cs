using System;

namespace ChessLibrary.ChessPieces
{
    public class Rook : ChessPiece
    {
        public bool WasMoved { get; private set; }
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            return this.Coordinate.isOnLine(coordinate) > 0;
        }

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

        public override bool MoveTo(FieldCoordinate coordinate)
        {
            bool result = CanMove(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        public bool Castling(FieldCoordinate coordinate)
        {
            bool result = false;
            if (!WasMoved && (this.Coordinate.Y == 1 || this.Coordinate.Y == 8) && this.Coordinate.Y == coordinate.Y
                && (this.Coordinate.X == 1 || this.Coordinate.X == 8))
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        public Rook(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {
            WasMoved = false;
        }

        public Rook(FieldCoordinate coordinate, Color color, bool wasMoved) : base(coordinate, color)
        {
            this.WasMoved = wasMoved;
        }

        public override string ToString()
        {
            return "R" + base.ToString();
        }

        public override object Clone()
        {
            return new Rook(this.Coordinate, this.PieceColor, this.WasMoved);
        }
    }
}
