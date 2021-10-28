using System;
using ChessLibrary;

namespace ChessLogger
{
    public class LoggerFile : IChessLogger
    {
        string PathName;

        public void Log(ChessPiece piece, FieldCoordinate fieldCoordinate, ChessStatus.Status status)
        {

        }

        public LoggerFile()
        {
            PathName = "ChessGame.Log";
        }

        public LoggerFile(string PathName)
        {
            this.PathName = PathName;
        }
    }
}
