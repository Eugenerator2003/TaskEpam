using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Bishop : ChessPiece
    {
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            return this.Coordinate.isOnDiagonal(coordinate) != 0;
        }

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

        public override bool MoveTo(FieldCoordinate coordinate)
        {
            bool result = CanMove(coordinate);
            if (result)
            {
                this.Coordinate = coordinate;
            }
            return result;
        }

        public Bishop(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        public override string ToString()
        {
            return "B" + base.ToString();
        }

        public override object Clone()
        {
            return new Bishop(this.Coordinate, this.PieceColor);
        }
    }
}
