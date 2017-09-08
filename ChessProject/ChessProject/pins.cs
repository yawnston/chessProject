using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    static class pins
    {
        //this list contains all the player's pinned pieces
        public static List<pin> existingPins = new List<pin>();

        //figures out which pieces are pinned and puts them in pinnedPieces list
        public static void calculatePins()
        {
            existingPins = new List<pin>();
            //reference to the player's king
            piece playerKing = null;
            if (board.turn == "white") playerKing = board.whitePieces.Find(p => p.pieceType == "king");
            else playerKing = board.blackPieces.Find(p => p.pieceType == "king");

            //pins are calculates in 8 directions
            //a search starts in a direction from the king - it "xrays" a friendly piece once and marks it as a candidate for a pin
            //if the search finds an enemy candidate for a pinner after the xray, the pinned piece is marked
            //the search ends unsuccesfully if multiple friendly pieces are hit in a row, if an unfit pinner candidate is found or if the edge of the board is hit

            int i = playerKing.xPosition; int j = playerKing.yPosition;
            bool xrayed = false; bool foundPin = false;
            List<square> potentialPush = new List<square>();
            List<square> potentialTake = new List<square>();
            piece pinCandidate = null;          

            //up
            j = playerKing.yPosition - 1;
            while(j >= 0)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if(board.squareArray[i,j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if(!xrayed && board.squareArray[i,j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if(xrayed && (board.squareArray[i,j].here.pieceType == "rook" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                j--;
            }
            //if we found a pin, save it
            if(foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //right
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition + 1;
            while (i < 8)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "rook" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i++;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //down
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            j = playerKing.xPosition + 1;
            while (j < 8)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "rook" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                j++;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //left
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition - 1;
            while (i >= 0)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "rook" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i--;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));


            //"rook" pins are done(working properly), now the diagonal "bishop" pins
            //TODO

            //top right
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition + 1; j = playerKing.yPosition - 1;
            while (i < 8 && j >= 0)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "bishop" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i++; j--;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //bottom right
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition + 1; j = playerKing.yPosition + 1;
            while (i < 8 && j < 8)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "bishop" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i++; j++;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //bottom left
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition - 1; j = playerKing.yPosition + 1;
            while (j < 8 && i >= 0)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "bishop" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i--; j++;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));

            //top left
            i = playerKing.xPosition; j = playerKing.yPosition;
            xrayed = false; foundPin = false;
            potentialPush = new List<square>();
            potentialTake = new List<square>();
            pinCandidate = null;
            i = playerKing.xPosition - 1; j = playerKing.yPosition - 1;
            while (i >= 0 && j >= 0)
            {
                //nothing there - add to potential moves for pinned piece on this line
                if (board.squareArray[i, j].here == null)
                {
                    potentialPush.Add(board.squareArray[i, j]);
                }
                else
                {
                    //xray the friendly piece and make it a candidate
                    if (!xrayed && board.squareArray[i, j].here.pieceColor == board.turn)
                    {
                        xrayed = true;
                        pinCandidate = board.squareArray[i, j].here;
                    }
                    //we found the pinning piece
                    else if (xrayed && (board.squareArray[i, j].here.pieceType == "bishop" || board.squareArray[i, j].here.pieceType == "queen")
                        && board.squareArray[i, j].here.pieceColor != board.turn)
                    {
                        potentialTake.Add(board.squareArray[i, j]);
                        foundPin = true;
                        break;
                    }
                    //we found something else => the search was not successful, there is no pin here
                    else
                    {
                        break;
                    }
                }
                i--; j--;
            }
            //if we found a pin, save it
            if (foundPin) existingPins.Add(new pin(pinCandidate, potentialTake, potentialPush));
        }
    }

    //describes one instance of a pin
    class pin
    {
        public piece pinnedPiece;

        public List<square> takeSquares;

        public List<square> pushSquares;

        public pin(piece p, List<square> take, List<square> push)
        {
            pinnedPiece = p;
            takeSquares = take;
            pushSquares = push;
        }
    }
}
