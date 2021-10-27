using System;
using System.Collections.Generic;
using ChessLibrary;
using ChessLibrary.PieceFabric;
using ChessLibrary.ChessLogger;

namespace ChessGame
{
    public class ChessGame
    {
        private int check;
        private bool isWhiteMoving;
        public bool Checkmate { get; private set; }
        public int Check { get => check; private set { check = value; } }
        public ChessBoard Board;
        private IChessLogger Logger;
     
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
                        else if (takedPiece != null && pawn.CanBeat(fieldCoordinate))
                        {
                            pawn.Beat(fieldCoordinate);
                            Board[fieldCoordinate] = null;
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
            else if (piece == null)
            {
                Console.WriteLine($"No figures in this coordinate {pieceCoordinate}");
            }
            if (wasMoved)
            {
                if (takedPiece != null)
                {
                    Board.RemovePiece(takedPiece);
                    Board[takedPiece.Coordinate] = null;
                }
                if (Board.CheckTheCheck(isWhiteMoving, out ChessPiece pieceBeating, ref check))
                {
                    if (piece is Pawn && (fieldCoordinate.Y == 1 || fieldCoordinate.Y == 8))
                    {

                        Board[fieldCoordinate] = new Queen(fieldCoordinate, currentColor); //??
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
                    Board[fieldCoordinate] = null;
                    Board[pieceCoordinate] = pieceOld;
                    Board.RemovePiece(piece);
                    Board.AddPiece(pieceOld);
                    if (takedPiece != null)
                    {
                        Board[pieceCoordinate] = pieceOld;
                        Board[takedPiece.Coordinate] = takedPiece;
                        Board.AddPiece(takedPiece);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Can't move from {pieceCoordinate} to {fieldCoordinate}");
            }
            return wasMoved;
        }

        public ChessGame()
        {
            ChessPieceFabric.GetStandartComplect(out List<ChessPiece> White, out List<ChessPiece> Black);
            Board = new ChessBoard(White, Black);
            isWhiteMoving = true;
            Check = 0;
            Logger = new LoggerConsole();
        }

        public ChessGame(string LoggerType, string PathName = " ") : this() 
        {
            if (LoggerType == "console")
            {
                Logger = new LoggerConsole();
            }
            else if (LoggerType == "file")
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

        public void ConsoleShowBoard()
        {
            Board.ShowBoardInConsole();
        }
    }
}
