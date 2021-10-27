using System;
using System.Collections.Generic;

namespace ChessClassLibrary
{
    public abstract class ChessPiece
    {
        public enum Color
        {
            White,
            Black 
        }

        public enum Type
        {
            Pawn,
            Knight,
            Rook, 
            Bishop,
            Queen,
            King
        }

        public Color PieceColor { get; }

        public FieldCoordinate Coordinate
        {
            get => Coordinate;
            private protected set
            {
                Coordinate = value;
            }
        }

        public abstract bool CanMove(FieldCoordinate coordinate);
        public abstract bool MoveTo(FieldCoordinate coordinate);
        public abstract bool CanMoveThrough(ChessPiece piece, FieldCoordinate coordinate);

        public ChessPiece(FieldCoordinate coordinate, Color color)
        {
            this.Coordinate = coordinate;
            this.PieceColor = color;
        }

        public override string ToString()
        {
            return $"{Coordinate}";
        }

        public override bool Equals(object obj)
        {
            return obj is ChessPiece piece &&
                   PieceColor == piece.PieceColor &&
                   EqualityComparer<FieldCoordinate>.Default.Equals(Coordinate, piece.Coordinate);
        }

        public override int GetHashCode()
        {
            int hashCode = -1650298685;
            hashCode = hashCode * -1521134295 + PieceColor.GetHashCode();
            hashCode = hashCode * -1521134295 + Coordinate.GetHashCode();
            return hashCode;
        }
    }
}
