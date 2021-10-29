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
    public class ChessGame
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
        public Chessboard Board;
        private IChessLogger Logger;

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
            ChessPiece piece = Board[pieceCoordinate];
            ChessPiece takedPiece = Board[fieldCoordinate];
            ChessPiece pieceOld = (ChessPiece)piece.Clone();
            if (piece != null && piece?.PieceColor == currentColor && takedPiece?.PieceColor != currentColor)
            {
                if (Board.CanMoveOnBoard(piece, fieldCoordinate))
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
                    Board.RemovePiece(takedPiece);
                }
                if (Board.CheckTheCheck(isWhiteMoving, out ChessPiece pieceBeating, ref check))
                {
                    if (piece is Pawn && (fieldCoordinate.Y == 1 || fieldCoordinate.Y == 8))
                    {

                        Board[fieldCoordinate] = new Queen(fieldCoordinate, currentColor);
                    }
                    else
                    {
                        Board[fieldCoordinate] = piece;
                    }
                    Board[pieceCoordinate] = null;
                    if (Check != 0)
                    {
                        if (Board.CheckCheckmate(isWhiteMoving, pieceBeating, check))
                        {
                            Logger.Log(pieceOld, fieldCoordinate, ChessStatus.Status.checkmate);
                            return true;
                        }
                    }
                    isWhiteMoving = isWhiteMoving ? false : true;
                    Logger.Log(pieceOld, fieldCoordinate, (Check != 0) ? ChessStatus.Status.check : ChessStatus.Status.nothing);
                }
                else
                {
                    Board.RemovePiece(piece);
                    Board.AddPiece(pieceOld);
                    if (takedPiece != null)
                    {
                        Board[pieceCoordinate] = pieceOld;
                        Board.AddPiece(takedPiece);
                    }
                }
            }
            return wasMoved;
        }

        /// <summary>
        /// Constructor of chessgame class.
        /// </summary>
        public ChessGame()
        {
            ChessPieceFabric.GetStandartComplect(out List<ChessPiece> White, out List<ChessPiece> Black);
            Board = new Chessboard(White, Black);
            isWhiteMoving = true;
            Check = 0;
            Logger = new LoggerConsole();
        }

        /// <summary>
        /// Constructor of chessgame class.
        /// </summary>
        /// <param name="LoggerType">Type of logger which will be used(ConsoleLogger / FileLogger).</param>
        /// <param name="PathName">Path to file where logger will be log game info (uses if logger type is file logger)</param>
        public ChessGame(string LoggerType, string PathName = " ") : this()
        {
            if (LoggerType == "ConsoleLogger")
            {
                Logger = new LoggerConsole();
            }
            else if (LoggerType == "FileLogger")
            {
                if (PathName != " ")
                {
                    Logger = new LoggerFile(PathName);
                }
                else
                {
                    Logger = new LoggerFile();
                }
            }
        }
    }
}
