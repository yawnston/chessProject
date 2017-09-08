using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    //core handles the calculation of legal moves - adding them to the list in board
    static class core
    {
        public static void calculateMoves()
        {
            //king moves are calculated first - if there is a double check, the rest doesn't need to be calculated
            piece playerKing = null;
            if (board.turn == "white") playerKing = board.whitePieces.Find(p => p.pieceType == "king");
            else playerKing = board.blackPieces.Find(p => p.pieceType == "king");
            bool currentPinned = false; pin currentPin = null;

            List<square> kingSquares = playerKing.getLegalMoves();
            foreach(square s in kingSquares)
            {
                board.legalMoves.Add(new move(playerKing, s));
            }
            //double check - only legal moves are to move the king
            if(masks.checkerCount >= 2)
            {
                return;
            }
            //single check - can move, block or capture checker
            //TODO: pinned pieces
            else if(masks.checkerCount == 1)
            {
                List<piece> pcs = null;
                if (board.turn == "white") pcs = board.whitePieces;
                else pcs = board.blackPieces;
                List<square> consideredMoves = null;

                foreach(piece p in pcs)
                {
                    //if the piece is pinned, give it special treatment
                    if(pins.existingPins.Exists(pi => pi.pinnedPiece == p)){
                        currentPinned = true;
                        currentPin = pins.existingPins.Find(pi => pi.pinnedPiece == p);
                    }
                    consideredMoves = p.getLegalMoves();
                    foreach(square s in consideredMoves)
                    {
                        //if the piece is not pinned, proceed normally
                        if (!currentPinned)
                        {
                            //move is only valid in check if it conincides with the pushmask or the capturemask
                            if (masks.captureMask[s.xPosition, s.yPosition] || masks.pushMask[s.xPosition, s.yPosition])
                            {
                                board.legalMoves.Add(new move(p, s));
                            }
                        }
                        else
                        {
                            //move has to coincide with the pin rules in addition to the pushmask or capturemask
                            if ((masks.captureMask[s.xPosition, s.yPosition] || masks.pushMask[s.xPosition, s.yPosition])
                                && (currentPin.pushSquares.Exists(sq => sq == s) || currentPin.takeSquares.Exists(sq => sq == s)))
                            {
                                board.legalMoves.Add(new move(p, s));
                            }
                        }
                    }
                    currentPin = null;
                    currentPinned = false;
                }
                //no other moves are valid except for king moves, blocks or checker captures
                return;
            }
            //no check is in play - calculate moves like normal
            //pins.cs handles the calculation of pins
            //TODO: pinned pieces(separately)
            else
            {
                List<piece> pcs = null;
                if (board.turn == "white") pcs = board.whitePieces;
                else pcs = board.blackPieces;
                List<square> consideredMoves = null;

                foreach(piece p in pcs)
                {
                    //separate behavior for pinned pieces
                    if (pins.existingPins.Exists(pi => pi.pinnedPiece == p))
                    {
                        currentPinned = true;
                        currentPin = pins.existingPins.Find(pi => pi.pinnedPiece == p);
                    }
                    consideredMoves = p.getLegalMoves();
                    foreach(square s in consideredMoves)
                    {
                        //normal behavior
                        if (!currentPinned)
                        {
                            board.legalMoves.Add(new move(p, s));
                        }
                        else
                        {
                            if(currentPin.pushSquares.Exists(sq => sq == s) || currentPin.takeSquares.Exists(sq => sq == s))
                            {
                                board.legalMoves.Add(new move(p, s));
                            }
                        }
                    }
                    currentPin = null;
                    currentPinned = false;
                }
            }
        }
    }
}
