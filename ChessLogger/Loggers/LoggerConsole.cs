using System;
using ChessLibrary;
using ChessLibrary.ChessPieces;

namespace ChessLogger
{
    /// <summary>
    /// Class of Logger for write to console. Realizing IChessLogger interface.
    /// </summary>
    public class LoggerConsole : IChessLogger
    {
        /// <summary>
        /// Method for logging moves of chess game.
        /// </summary>
        /// <param name="piece">A piece which will be moved.</param>
        /// <param name="coordinate">Coordinate on which piece will be moved.</param>
        /// <param name="status">Status of chess game.</param>
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

        /// <summary>
        /// Constructor of LoggerConsole.
        /// </summary>
        public LoggerConsole()
        {

        }
    }
}
