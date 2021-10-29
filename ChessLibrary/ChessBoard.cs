using System;
using System.Collections.Generic;
using ChessLibrary.ChessPieces;

namespace ChessLibrary
{ 

    /// <summary>
    /// Chessboard class. Contains sets of chess pieces with their location on game board.
    /// </summary>
    public class Chessboard
    {
        private ChessPiece whiteKing, blackKing;
        private List<ChessPiece> White;
        private List<ChessPiece> Black;
        private ChessPiece[,] board = new ChessPiece[8, 8];
        
        /// <summary>
        /// Method for checking piece's possibility of moving to given coordinate
        /// </summary>
        /// <param name="piece">Chess piece</param>
        /// <param name="fieldCoordinate">Coordinate where piece is want to move</param>
        /// <returns>True if this piece is can to move</returns>
        public bool CanMoveOnBoard(ChessPiece piece, FieldCoordinate fieldCoordinate)
        {
            bool isCanMove = true;
            bool isPawn = piece is Pawn;
            ChessPiece peace = null;
            foreach (ChessPiece chessPiece in board)
            {
                if (chessPiece != null)
                {
                    if (chessPiece != piece)
                    {
                        if (!isPawn)
                        {
                            if (!piece.CanMoveThrough(chessPiece, fieldCoordinate))
                            {
                                isCanMove = false;
                                peace = piece;
                                break;
                            }
                        }
                        else
                        {
                            if (this[fieldCoordinate] == null)
                            {
                                if (!piece.CanMoveThrough(chessPiece, fieldCoordinate))
                                {
                                    isCanMove = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (!(piece as Pawn).CanBeat(this[fieldCoordinate]))
                                {
                                    isCanMove = false;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return isCanMove;
        }

        /// <summary>
        /// Method for checking the check status.
        /// </summary>
        /// <param name="isWhiteMoving">Does White play this move.</param>
        /// <param name="pieceBeating">A chess piece can beat one of the Kings.</param>
        /// <param name="chessCheck">Check status.</param>
        /// <returns>True if game situation is can to continue from this move (Check was putted or player left the check).</returns>
        public bool CheckTheCheck(bool isWhiteMoving, out ChessPiece pieceBeating, ref int chessCheck)
        {
            pieceBeating = null;
            int check = 0;
            bool CanMove = true;
            int unexpectedCheck = (chessCheck != 0) ? ((chessCheck == 1) ? 2 : 1) : (isWhiteMoving ? 2 : 1);
            for (int i = 0; i < 2; i++)
            {
                ChessPiece king = i == 0 ? blackKing : whiteKing;
                List<ChessPiece> current = i == 0 ? White : Black;
                foreach (ChessPiece pieceMove in current)
                {
                    if (pieceMove.CanMove(king.Coordinate))
                    {
                        if (this.CanMoveOnBoard(pieceMove, king.Coordinate))
                        {
                            pieceBeating = pieceMove;
                            check = i == 0 ? 1 : 2;
                            break;
                        }
                    }
                }
                if (check != 0)
                {
                    break;
                }
            }
            if (check == unexpectedCheck || (chessCheck == check && check != 0))
            {
                CanMove = false;
            }
            else
            {
                chessCheck = check;
            }
            return CanMove;
        }

        /// <summary>
        /// Method for checking the checkmate in current game situation.
        /// </summary>
        /// <param name="isWhiteMoving">Does White play this move.</param>
        /// <param name="pieceBeating">A chess piece beating one of the Kings.</param>
        /// <param name="check">Check status.</param>
        /// <returns>True if game situation is can to continue from this move.</returns>
        public bool CheckCheckmate(bool isWhiteMoving, ChessPiece pieceBeating, int check)
        {
            int checkOld = check;
            bool checkmate = true;
            ChessPiece king = (check == 1) ? blackKing : whiteKing;
            List<ChessPiece> currentSet = (check == 1) ? Black : White;
            ChessPiece kingOld = (ChessPiece)king.Clone();
            FieldCoordinate kingCoordinate = king.Coordinate;
            FieldCoordinate escapeCoordinates;
            for (int i = 0; i < 8; i++)
            {
                bool isCanMoveFromCheck = false;
                double arg = 2 * Math.PI * i / 8;
                int x_step = (int)Math.Round(Math.Cos(arg));
                int y_step = (int)Math.Round(Math.Sin(arg));
                escapeCoordinates = new FieldCoordinate(kingCoordinate.X + x_step, kingCoordinate.Y + y_step);
                if (this.CanMoveOnBoard(king, escapeCoordinates))
                {
                    king.MoveTo(escapeCoordinates);
                    _SetKing(king);
                    if (this.CheckTheCheck(isWhiteMoving, out ChessPiece temp, ref check))
                    {
                        isCanMoveFromCheck = true;
                    }
                    else
                    {
                        if (kingOld.PieceColor == ChessPiece.Color.White)
                        {
                            whiteKing = kingOld;
                        }
                        else
                        {
                            blackKing = kingOld;
                        }
                    }
                    RemovePiece(king);
                    king = (ChessPiece)kingOld.Clone();
                    AddPiece(king);
                    _SetKing(king);
                    this[kingCoordinate] = kingOld;
                    if (isCanMoveFromCheck)
                    {
                        check = checkOld;
                        checkmate = false;
                        break;
                    }

                }
            }
            RemovePiece(king);
            AddPiece(kingOld);
            _SetKing(kingOld);
            this[kingCoordinate] = kingOld;
            king = kingOld;

            if (checkmate)
            {
                for (int i = 0; i < currentSet.Count; i++)
                {
                    bool isCanBeat = false;
                    if (this.CanMoveOnBoard(currentSet[i], pieceBeating.Coordinate))
                    {
                        ChessPiece currentPieceOld = (ChessPiece)currentSet[i].Clone();
                        currentSet[i].MoveTo(pieceBeating.Coordinate);
                        RemovePiece(pieceBeating);
                        if (this.CheckTheCheck(isWhiteMoving, out ChessPiece temp, ref check))
                        {
                            isCanBeat = true;
                        }
                        currentSet[i] = currentPieceOld;
                        AddPiece(pieceBeating);
                    }
                    if (isCanBeat)
                    {
                        checkmate = false;
                        break;
                    }
                }
            }

            if (checkmate && !(pieceBeating is Knight))
            {
                List<FieldCoordinate> coordinateBetween = _GetFreeCoordinatesBetween(king.Coordinate, pieceBeating.Coordinate);
                for (int i = 0; i < currentSet.Count; i++)
                {
                    bool canMove = false;
                    foreach (FieldCoordinate coordinate in coordinateBetween)
                    {
                        if (this.CanMoveOnBoard(currentSet[i], coordinate) && currentSet[i] != king)
                        {
                            ChessPiece pieceOld = (ChessPiece)currentSet[i].Clone();
                            FieldCoordinate coordinatePieceOld = currentSet[i].Coordinate;
                            currentSet[i].MoveTo(coordinate);
                            this[coordinatePieceOld] = null;
                            this[coordinate] = currentSet[i];
                            if (this.CheckTheCheck(isWhiteMoving, out ChessPiece temp, ref check))
                            {
                                canMove = true;
                            }
                            this[coordinatePieceOld] = pieceOld;
                            this[coordinate] = null;
                            RemovePiece(currentSet[i]);
                            AddPiece(pieceOld);
                        }
                    }
                    if (canMove)
                    {
                        checkmate = false;
                        break;
                    }
                }
            }

            return checkmate;
        }


        /// <summary>
        /// Method for getting collection of field coordinate between two of cells.
        /// </summary>
        /// <param name="fieldCoordinate1">First coordinate.</param>
        /// <param name="fieldCoordinate2">Second coordinate.</param>
        /// <returns>Collection of field coordinate.</returns>
        private List<FieldCoordinate> _GetFreeCoordinatesBetween(FieldCoordinate fieldCoordinate1, FieldCoordinate fieldCoordinate2)
        {
            List<FieldCoordinate> coordinateBetween = new List<FieldCoordinate>();
            int diagonalNum = fieldCoordinate1.isOnDiagonal(fieldCoordinate2);
            int lineNum = fieldCoordinate1.isOnLine(fieldCoordinate2);
            int direction = 0;
            if (lineNum > 0)
            {
                if (lineNum == 1) direction = 2;
                else if (lineNum == 2) direction = 4;
                else if (lineNum == 3) direction = 6;
                else direction = 8;
            }
            else
            {
                if (diagonalNum == 1) direction = 1;
                else if (lineNum == 2) direction = 3;
                else if (lineNum == 3) direction = 5;
                else if (lineNum == 7) direction = 7;
            }
            if (direction != 0)
            {
                int x_step = 0, y_step = 0;
                switch (direction)
                {
                    case (1):
                        x_step = 1;
                        y_step = 1;
                        break;
                    case (2):
                        x_step = 1;
                        break;
                    case (3):
                        x_step = 1;
                        y_step = -1;
                        break;
                    case (4):
                        y_step = -1;
                        break;
                    case (5):
                        x_step = -1;
                        y_step = -1;
                        break;
                    case (6):
                        x_step = -1;
                        break;
                    case (7):
                        x_step = -1;
                        y_step = 1;
                        break;
                    case (8):
                        y_step = 1;
                        break;
                }
                int x = fieldCoordinate1.X + x_step, y = fieldCoordinate1.Y + y_step;
                while (x > 0 && x < 9 && y > 0 && y < 9 && this[x, y] == null && fieldCoordinate2 != new FieldCoordinate(x, y))
                {
                    coordinateBetween.Add(new FieldCoordinate(x, y));
                    x += x_step;
                    y += y_step;
                }
            }
            return coordinateBetween;
        }


        /// <summary>
        /// Method for removing piece from chessboard.
        /// </summary>
        /// <param name="pieceRemoved">Chess piece which to be removed.</param>
        public void RemovePiece(ChessPiece pieceRemoved)
        {
            this[pieceRemoved.Coordinate] = null;
            if (pieceRemoved.PieceColor == ChessPiece.Color.White)
            {
                White.Remove(pieceRemoved);
            }
            else
            {
                Black.Remove(pieceRemoved);
            }
        }

        /// <summary>
        /// Method for adding piece to chessboard.
        /// </summary>
        /// <param name="pieceAdded">Chess piece which to be adding.</param>
        public void AddPiece(ChessPiece pieceAdded)
        {
            this[pieceAdded.Coordinate] = pieceAdded;
            if (pieceAdded.PieceColor == ChessPiece.Color.White)
            {
                White.Add(pieceAdded);
            }
            else
            {
                Black.Add(pieceAdded);
            }
        }

        /// <summary>
        /// Set King piece.
        /// </summary>
        /// <param name="pieceKing">Piece King</param>
        private void _SetKing(ChessPiece pieceKing)
        {
            if (pieceKing is King)
            {
                if (pieceKing.PieceColor == ChessPiece.Color.White)
                {
                    whiteKing = pieceKing;
                }
                else
                {
                    blackKing = pieceKing;
                }
            }
        }

        /// <summary>
        /// Constructor of chessboard.
        /// </summary>
        /// <param name="white">Collection of White piece set.</param>
        /// <param name="black">Collection of Black piece set.</param>
        public Chessboard(List<ChessPiece> white, List<ChessPiece> black)
        {
            this.White = white;
            this.Black = black;
            foreach(ChessPiece piece in White)
            {
                this[piece.Coordinate] = piece;
                if (piece is King) this.whiteKing = piece;
            }
            foreach(ChessPiece piece in Black)
            {
                this[piece.Coordinate] = piece;
                if (piece is King) this.blackKing = piece;
            }
            
        }

        /// <summary>
        /// Indexator for access to chessboard.
        /// </summary>
        /// <param name="x">X coordinate of chessboard.</param>
        /// <param name="y">Y coordinate of chessboard.</param>
        /// <returns>Link to piece located on this coordinate.</returns>
        public ChessPiece this[int x, int y]
        {
            get => board[x - 1, y - 1];
            set
            {
                board[x - 1, y - 1] = value;
                
            }
        }

        /// <summary>
        /// Indexator for access to chessboard.
        /// </summary>
        /// <param name="coordinate">Coordinate of chessboard.</param>
        /// <returns>Link to piece located on thid coordinate.</returns>
        public ChessPiece this[FieldCoordinate coordinate]
        {
            get => board[coordinate.X - 1, coordinate.Y - 1];
            set
            {
                this[coordinate.X, coordinate.Y] = value;
            }
        }
    }
}
