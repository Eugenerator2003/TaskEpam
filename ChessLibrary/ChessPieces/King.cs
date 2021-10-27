using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.ChessPieces
{
    public class King : ChessPiece
    {
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

        public override bool MoveTo(FieldCoordinate coordinate)
        {
            bool result = CanMove(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        public King(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        public override string ToString()
        {
            return "K" + base.ToString();
        }

        public override object Clone()
        {
            return new King(this.Coordinate, this.PieceColor);
        }
    }
}
