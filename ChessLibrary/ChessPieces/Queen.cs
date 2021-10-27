using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.ChessPieces
{
    public class Queen : ChessPiece
    {
        public override bool CanMove(FieldCoordinate coordinate)
        {
            if (!ValidCoordinate(coordinate))
                return false;
            return this.Coordinate.isOnLine(coordinate) > 0 || this.Coordinate.isOnDiagonal(coordinate) > 0;
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

        public Queen(FieldCoordinate coordinate, Color color) : base(coordinate, color)
        {

        }

        public override string ToString()
        {
            return "Q" + base.ToString();
        }

        public override object Clone()
        {
            return new Queen(this.Coordinate, this.PieceColor);
        }
    }
}
