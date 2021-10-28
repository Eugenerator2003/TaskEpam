using System;
using System.IO;
using ChessLibrary;
using ChessLibrary.ChessPieces;

namespace ChessLogger
{
    /// <summary>
    /// Class of Logger for write to file. Realizing IChessLogger interface.
    /// </summary>
    public class LoggerFile : IChessLogger
    {
        private StreamWriter writer;
        /// <summary>
        /// Propetry for getting file's path
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Method for logging moves of chess game.
        /// </summary>
        /// <param name="piece">A piece which will be moved.</param>
        /// <param name="coordinate">Coordinate on which piece will be moved.</param>
        /// <param name="status">Status of chess game.</param>
        public void Log(ChessPiece piece, FieldCoordinate coordinate, ChessStatus.Status status)
        {
            char _status = ' ';
            switch (status)
            {
                case (ChessStatus.Status.check):
                    _status = '+';
                    break;
                case (ChessStatus.Status.checkmate):
                    _status = '#';
                    break;
            }
            writer.Write($"{piece}—{coordinate}{_status}");
            if (status == ChessStatus.Status.checkmate)
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Contructor of LoggerFile. The default is the path to file: "Chessgame.log";
        /// </summary>
        public LoggerFile()
        {
            FilePath = "Chessgame.Log";
            writer = new StreamWriter(FilePath, false);
        }

        /// <summary>
        /// Constructor of LoggerFile.
        /// </summary>
        /// <param name="directoryPath">The path to the directory where locate file to write to.</param>
        public LoggerFile(string directoryPath)
        {
            this.FilePath = directoryPath + "\\Chessgame.Log";
            writer = new StreamWriter(FilePath, false);
        }
    }
}
