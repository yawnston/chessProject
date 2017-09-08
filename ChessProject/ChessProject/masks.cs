using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    //masks are used to calculate legal moves
    //masks used:
    //  kingDangerMask - squares that are attacked by the enemy - including "through" the king
    //  captureMask - squares that can be captured on - used for handling checks
    //  pushMask - squares that can be moved to(for regular pieces) - used for handling checks
    //  checkingMask - squares where a piece that is checking the player's king is standing - for creation of captureMask
    static class masks
    {
        //stores how many pieces are currently checking the king
        public static int checkerCount = 0;

        public static bool[,] kingDangerMask = new bool[8, 8];

        public static bool[,] captureMask = new bool[8, 8];

        public static bool[,] pushMask = new bool[8, 8];

        public static bool[,] checkingMask = new bool[8, 8];

        //calculates how many pieces are in check for the player and creates the masks
        public static void calculateCheckers()
        {
            //init of masks
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    kingDangerMask[i, j] = false;
                    captureMask[i, j] = false;
                    pushMask[i, j] = false;
                    checkingMask[i, j] = false;
                }
            }
            checkerCount = 0;

            //pointer to the player's king (for ease of reading)
            piece playerKing;
            if (board.turn == "white") playerKing = board.whitePieces.Find(p => p.pieceType == "king");
            else playerKing = board.blackPieces.Find(p => p.pieceType == "king");

            //first calculate the danger squares
            if (board.turn == "white")
            {
                foreach (piece p in board.blackPieces)
                {
                    if (p.pieceType == "pawn")
                    {
                        if (p.xPosition - 1 >= 0)
                        {
                            kingDangerMask[p.xPosition - 1, p.yPosition + 1] = true;
                            if (board.squareArray[p.xPosition - 1, p.yPosition + 1].here != null
                                && board.squareArray[p.xPosition - 1, p.yPosition + 1].here.pieceType == "king"
                                && board.squareArray[p.xPosition - 1, p.yPosition + 1].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                        if (p.xPosition + 1 < 8)
                        {
                            kingDangerMask[p.xPosition + 1, p.yPosition + 1] = true;
                            if (board.squareArray[p.xPosition + 1, p.yPosition + 1].here != null
                                && board.squareArray[p.xPosition + 1, p.yPosition + 1].here.pieceType == "king"
                                && board.squareArray[p.xPosition + 1, p.yPosition + 1].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "knight")
                    {
                        List<square> m = utils.knightMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "bishop")
                    {
                        List<square> m = utils.bishopDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "rook")
                    {
                        List<square> m = utils.rookDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "queen")
                    {
                        //treat the queen like a rook + bishop
                        List<square> m = utils.bishopDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                        m = utils.rookDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "king")
                    {
                        List<square> m = utils.kingDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                        }
                    }
                }
            }
            else if (board.turn == "black")
            {
                foreach (piece p in board.whitePieces)
                {
                    if (p.pieceType == "pawn")
                    {
                        if (p.xPosition - 1 >= 0)
                        {
                            kingDangerMask[p.xPosition - 1, p.yPosition - 1] = true;
                            if (board.squareArray[p.xPosition - 1, p.yPosition - 1].here != null
                                && board.squareArray[p.xPosition - 1, p.yPosition - 1].here.pieceType == "king"
                                && board.squareArray[p.xPosition - 1, p.yPosition - 1].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                        if (p.xPosition + 1 < 8)
                        {
                            kingDangerMask[p.xPosition + 1, p.yPosition - 1] = true;
                            if (board.squareArray[p.xPosition + 1, p.yPosition - 1].here != null
                                && board.squareArray[p.xPosition + 1, p.yPosition - 1].here.pieceType == "king"
                                && board.squareArray[p.xPosition + 1, p.yPosition - 1].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "knight")
                    {
                        List<square> m = utils.knightMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "bishop")
                    {
                        List<square> m = utils.bishopDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "rook")
                    {
                        List<square> m = utils.rookDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "queen")
                    {
                        //treat the queen like a rook + bishop
                        List<square> m = utils.bishopDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                        m = utils.rookDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                            if (board.squareArray[s.xPosition, s.yPosition].here != null
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceType == "king"
                                && board.squareArray[s.xPosition, s.yPosition].here.pieceColor == board.turn)
                            {
                                checkingMask[p.xPosition, p.yPosition] = true;
                                checkerCount++;
                            }
                        }
                    }
                    else if (p.pieceType == "king")
                    {
                        List<square> m = utils.kingDangerMoves(board.squareArray[p.xPosition, p.yPosition]);
                        foreach (square s in m)
                        {
                            kingDangerMask[s.xPosition, s.yPosition] = true;
                        }
                    }
                }
            }

            //now the king danger squares are calculated - we know where the king can't move because that square is attacked
            //we know which squares have pieces that are GIVING CHECK to the player's king
            //we also know the number of pieces giving check to the player's king
            //if checkerCount >= 2 then the only legal moves are king moves to safe squares
            //if checkerCount == 1 then we have 3 options
            //  -move the king out of check
            //  -capture the checking piece
            //  -block the checking piece

            //we have to move the king => no capture is valid
            if (checkerCount >= 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        captureMask[i, j] = false;
                    }
                }
            }
            //we can evade the single check by capturing the piece
            if (checkerCount == 1)
            {
                captureMask = checkingMask;
            }

            //now the push mask - will include squares that we can move a piece to in order to block a single check
            //with double checks, check can't be blocked => mask is false
            if (checkerCount >= 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        pushMask[i, j] = false;
                    }
                }
            }

            piece checkingPiece = null;
            //if single check, then there is only one "true" on the checkingMask - find it to find the checking piece
            if (checkerCount == 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (checkingMask[i, j] == true) //we found the only checking piece
                        {
                            checkingPiece = board.squareArray[i, j].here;
                        }
                    }
                }

                //if it's a "slider" piece, find squares where we can block the check - between the piece and the king
                if (checkingPiece.pieceType == "rook" || checkingPiece.pieceType == "queen" || checkingPiece.pieceType == "bishop")
                {
                    List<square> pieceSlider = null;
                    List<square> kingSlider = null;
                    if (checkingPiece.pieceType == "rook"
                       || checkingPiece.pieceType == "queen")
                    {
                        pieceSlider = utils.rookSliderMoves(board.squareArray[checkingPiece.xPosition, checkingPiece.yPosition]);
                        kingSlider = utils.rookSliderMoves(board.squareArray[playerKing.xPosition, playerKing.yPosition]);
                        //overlap the two sliders in a 2 dimensional array to get the squares where blocking is possible
                        int[,] overlap = new int[8, 8];
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                overlap[i, j] = 0;
                            }
                        }

                        foreach (square s in kingSlider)
                        {
                            overlap[s.xPosition, s.yPosition]++;
                        }
                        foreach (square s in pieceSlider)
                        {
                            overlap[s.xPosition, s.yPosition]++;
                        }
                        //if both pieces "saw" the square => add it to the pushMask
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (overlap[i, j] == 2)
                                {
                                    pushMask[i, j] = true;
                                }
                            }
                        }
                    }

                    if (checkingPiece.pieceType == "bishop"
                       || checkingPiece.pieceType == "queen")
                    {
                        pieceSlider = utils.bishopSliderMoves(board.squareArray[checkingPiece.xPosition, checkingPiece.yPosition]);
                        kingSlider = utils.bishopSliderMoves(board.squareArray[playerKing.xPosition, playerKing.yPosition]);
                        //overlap the two sliders in a 2 dimensional array to get the squares where blocking is possible
                        int[,] overlap = new int[8, 8];
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                overlap[i, j] = 0;
                            }
                        }

                        foreach (square s in kingSlider)
                        {
                            overlap[s.xPosition, s.yPosition]++;
                        }
                        foreach (square s in pieceSlider)
                        {
                            overlap[s.xPosition, s.yPosition]++;
                        }
                        //if both pieces "saw" the square => add it to the pushMask
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (overlap[i, j] == 2)
                                {
                                    pushMask[i, j] = true;
                                }
                            }
                        }
                    }
                }
            }

            //now both the push mask and the capture mask is DONE
            //TODO: pinned pieces
        }
    }
}
