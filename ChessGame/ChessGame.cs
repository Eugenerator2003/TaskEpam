using System;
using System.Collections.Generic;
using ChessLibrary.ChessPieces;
using ChessLibrary.PieceFabric;
using ChessLogger;

namespace ChessLibrary
{
    /// <summary>
    /// Class of game of Chess. Contains method to move piece on chessboard and status of chess game.
    /// </summary>
    public class Chessgame
    {
        private int check;
        private bool isWhiteMoving;
        /// <summary>
        /// Property of status of checkmate.
        /// </summary>
        public bool Checkmate { get; private set; }
        /// <summary>
        /// Propety for status of the check.
        /// The check was put on by White if check is 1 and was put on by Black if it's 2.
        /// </summary>
        public int Check { get => check; private set { check = value; } }
        private Chessboard board;
        private IChessLogger logger;

        /// <summary>
        /// Method to move piece on chessboard.
        /// </summary>
        /// <param name="pieceCoordinate">Coordinate where located piece.</param>
        /// <param name="fieldCoordinate">Coordinate where piece must be moved.</param>
        /// <returns>True if piece was moved</returns>
        public bool MovePiece(FieldCoordinate pieceCoordinate, FieldCoordinate fieldCoordinate)
        {
            if (Checkmate)
                return false;
            bool wasMoved = false;
            ChessPiece.Color currentColor = isWhiteMoving ? ChessPiece.Color.White : ChessPiece.Color.Black;
            ChessPiece piece = board[pieceCoordinate];
            ChessPiece takedPiece = board[fieldCoordinate];
            ChessPiece pieceOld = (ChessPiece)piece.Clone();
            if (piece != null && piece?.PieceColor == currentColor && takedPiece?.PieceColor != currentColor)
            {
                if (board.CanMoveOnBoard(piece, fieldCoordinate))
                {
                    if (piece is Pawn)
                    {
                        Pawn pawn = piece as Pawn;
                        if (pawn.CanMove(fieldCoordinate))
                        {
                            pawn.MoveTo(fieldCoordinate);
                            wasMoved = true;
                        }
                        else if (takedPiece != null && pawn.CanBeat(takedPiece))
                        {
                            pawn.Beat(takedPiece);
                            piece = (ChessPiece)pawn;
                            wasMoved = true;
                        }
                    }
                    else if (piece.CanMove(fieldCoordinate))
                    {
                        piece.MoveTo(fieldCoordinate);
                        wasMoved = true;
                    }

                }
            }
            if (wasMoved)
            {
                if (takedPiece != null)
                {
                    board.RemovePiece(takedPiece);
                }
                if (board.CheckTheCheck(isWhiteMoving, out ChessPiece pieceBeating, ref check))
                {
                    if (piece is Pawn && (fieldCoordinate.Y == 1 || fieldCoordinate.Y == 8))
                    {

                        board[fieldCoordinate] = new Queen(fieldCoordinate, currentColor);
                    }
                    else
                    {
                        board[fieldCoordinate] = piece;
                    }
                    board[pieceCoordinate] = null;
                    if (Check != 0)
                    {
                        if (board.CheckCheckmate(isWhiteMoving, pieceBeating, check))
                        {
                            Checkmate = true;
                            logger.Log(pieceOld, fieldCoordinate, ChessStatus.Status.checkmate);
                            return true;
                        }
                    }
                    isWhiteMoving = isWhiteMoving ? false : true;
                    logger.Log(pieceOld, fieldCoordinate, (Check != 0) ? ChessStatus.Status.check : ChessStatus.Status.nothing);
                }
                else
                {
                    board.RemovePiece(piece);
                    board.AddPiece(pieceOld);
                    if (takedPiece != null)
                    {
                        board[pieceCoordinate] = pieceOld;
                        board.AddPiece(takedPiece);
                    }
                }
            }
            return wasMoved;
        }

        /// <summary>
        /// Constructor of chessgame class.
        /// </summary>
        public Chessgame()
        {
            ChessPieceFabric.GetStandartComplect(out List<ChessPiece> White, out List<ChessPiece> Black);
            board = new Chessboard(White, Black);
            isWhiteMoving = true;
            Check = 0;
            Checkmate = false;
            logger = new LoggerConsole();
        }

        /// <summary>
        /// Constructor of chessgame class.
        /// </summary>
        /// <param name="LoggerType">Type of logger which will be used(ConsoleLogger / FileLogger).</param>
        /// <param name="PathName">Path to file where logger will be log game info (uses if logger type is file logger)</param>
        public Chessgame(string LoggerType, string PathName = " ") : this()
        {
            if (LoggerType == "ConsoleLogger")
            {
                logger = new LoggerConsole();
            }
            else if (LoggerType == "FileLogger")
            {
                if (PathName != " ")
                {
                    logger = new LoggerFile(PathName);
                }
                else
                {
                    logger = new LoggerFile();
                }
            }
        }

        /// <summary>
        /// Method for comparsion current chessgame with other object.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if object is equal to current chessgame.</returns>
        public override bool Equals(object obj)
        {
            return obj is Chessgame chessgame &&
                   check == chessgame.check &&
                   isWhiteMoving == chessgame.isWhiteMoving &&
                   Checkmate == chessgame.Checkmate &&
                   EqualityComparer<Chessboard>.Default.Equals(board, chessgame.board);
        }

        /// <summary>
        /// Method for getting hashcode of chessgame.
        /// </summary>
        /// <returns>Hashcode of chessgame.</returns>
        public override int GetHashCode()
        {
            return check.GetHashCode() + isWhiteMoving.GetHashCode() + Checkmate.GetHashCode()
                   + board.GetHashCode();
        }

        /// <summary>
        /// Method for convert from Chessgame to String.
        /// </summary>
        /// <returns>Chessgame converted to String.</returns>
        public override string ToString()
        {
            return board.ToString();
        }
    }
}
