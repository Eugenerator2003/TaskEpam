using System;
using ChessLibrary;
using ChessLibrary.ChessPieces;

namespace ChessLogger
{
    /// <summary>
    /// Interface for chess game logger.
    /// </summary>
    public interface IChessLogger
    {
        /// <summary>
        /// Method for logging moves of chess game.
        /// </summary>
        /// <param name="piece">A piece which will be moved.</param>
        /// <param name="coordinate">Coordinate on which piece will be moved.</param>
        /// <param name="status">Status of chess game.</param>
        void Log(ChessPiece piece, FieldCoordinate coordinate, ChessStatus.Status status);
    }
    
    /// <summary>
    /// Static class containing an enumeration of the status of a chess game.
    /// </summary>
    public static class ChessStatus
    {
        /// <summary>
        /// Enumeration of chess games status.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Simple move.
            /// </summary>
            nothing,
            /// <summary>
            /// The check was put.
            /// </summary>
            check,
            /// <summary>
            /// Checkmate was put.
            /// </summary>
            checkmate
        }
    }
}
