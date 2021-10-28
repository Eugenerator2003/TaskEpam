using System;
using ChessLibrary;

namespace ChessLogger
{
    public class LoggerConsole : IChessLogger
    {
        public void Log(ChessPiece piece, FieldCoordinate coordinate, ChessStatus.Status status)
        {
            char _status = ' ';
            switch(status)
            {
                case (ChessStatus.Status.check):
                    _status = '+';
                    break;
                case (ChessStatus.Status.checkmate):
                    _status = '#';
                    break;
            }
            Console.WriteLine($"{piece}—{coordinate}{_status}");
        }

        public LoggerConsole()
        {

        }
    }
}
