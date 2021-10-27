using System;

namespace ChessLibrary
{
    public class Pawn : ChessPiece
    {
        public int Moves { get; private set; }
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
                Moves++;
            }
            return result;
        }

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

        public bool Beat(FieldCoordinate coordinate)
        {
            bool result = CanBeat(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
                Moves++;
            }
            return result;
        }

        public Pawn(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {
            this.Moves = 0;
        }

        public Pawn(FieldCoordinate coordinate, Color color, int movesCount) : base(coordinate, color)
        {
            this.Moves = movesCount;
        }

        public override string ToString()
        {
            return base.ToString(); 
        }

        public override object Clone()
        {
            return new Pawn(this.Coordinate, this.PieceColor, this.Moves);
        }
    }
}
