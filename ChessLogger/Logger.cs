using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.ChessLogger
{
    public interface IChessLogger
    {
        void Log(ChessPiece piece, FieldCoordinate coordinate, ChessStatus.Status status);

        
    }

    public static class ChessStatus
    {
        public enum Status
        {
            nothing,
            check,
            checkmate
        }

    }
    //public interface IChessLogger
    //{
    //    void Log(FieldCoordinate fieldCoordinate, ChessPiece piece, );
    //}

    //public static class ChessSituation
    //{
    //    public enum Situation
    //    {
    //        nothing,
    //        check,
    //        checkmate
    //    }
    //}
}
