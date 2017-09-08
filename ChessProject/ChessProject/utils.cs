using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    static class utils
    {
        //gets all "knight-like moves" from a given square - used for calculating checks
        public static List<square> knightMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            //starting clockwise from the top
            //0
            if (x + 1 < 8 && y - 2 >= 0) ret.Add(board.squareArray[x + 1, y - 2]);
            if (x + 2 < 8 && y - 1 >= 0) ret.Add(board.squareArray[x + 2, y - 1]);
            //90
            if (x + 2 < 8 && y + 1 < 8) ret.Add(board.squareArray[x + 2, y + 1]);
            if (x + 1 < 8 && y + 2 < 8) ret.Add(board.squareArray[x + 1, y + 2]);
            //180
            if (x - 1 >= 0 && y + 2 < 8) ret.Add(board.squareArray[x - 1, y + 2]);
            if (x - 2 >= 0 && y + 1 < 8) ret.Add(board.squareArray[x - 2, y + 1]);
            //270
            if (x - 2 >= 0 && y - 1 >= 0) ret.Add(board.squareArray[x - 2, y - 1]);
            if (x - 1 >= 0 && y - 2 >= 0) ret.Add(board.squareArray[x - 1, y - 2]);

            return ret;
        }
        
        //gets all bishop danger moves from a square - "x-rays" the king
        public static List<square> bishopDangerMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            int i = x; int j = y;
            bool shouldStop = false;

            //starting clockwise from the top
            i = x + 1; j = y - 1;
            while(i < 8 && j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i++; j--;
            }
            //90
            i = x + 1; j = y + 1; shouldStop = false;
            while (i < 8 && j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                    //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i++; j++;
            }
            //180
            i = x - 1; j = y + 1; shouldStop = false;
            while (i >= 0 && j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i--; j++;
            }
            //270
            i = x - 1; j = y - 1; shouldStop = false;
            while (i >= 0 && j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i--; j--;
            }

            return ret;
        }
        
        //returns all "danger squares" for a rook's movement from a square
        public static List<square> rookDangerMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            int i = x; int j = y;
            bool shouldStop = false;

            //starting from the top, clockwise
            i = x; j = y - 1; shouldStop = false;
            while (j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                j--;
            }
            //90
            i = x + 1; j = y; shouldStop = false;
            while (i < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i++;
            }
            //180
            i = x; j = y + 1; shouldStop = false;
            while (j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                j++;
            }
            //270
            i = x - 1; j = y; shouldStop = false;
            while (i >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType != "king")
                //&& board.squareArray[i, j].here.pieceColor != board.turn)
                {
                    shouldStop = true;
                }
                if (board.squareArray[i, j].here != null && board.squareArray[i, j].here.pieceType == "king"
                    && board.squareArray[i, j].here.pieceColor != board.turn) shouldStop = true;
                if (shouldStop) break;
                i--;
            }

            return ret;
        }
        
        //gets the king danger moves
        public static List<square> kingDangerMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            //starting from the top clockwise
            if (y - 1 >= 0) ret.Add(board.squareArray[x, y - 1]);
            if (y - 1 >= 0 && x + 1 < 8) ret.Add(board.squareArray[x + 1, y - 1]);
            //90
            if (x + 1 < 8) ret.Add(board.squareArray[x + 1, y]);
            if (y + 1 < 8 && x + 1 < 8) ret.Add(board.squareArray[x + 1, y + 1]);
            //180
            if (y + 1 < 8) ret.Add(board.squareArray[x, y + 1]);
            if (x - 1 >= 0 && y + 1 < 8) ret.Add(board.squareArray[x - 1, y + 1]);
            //270
            if (x - 1 >= 0) ret.Add(board.squareArray[x - 1, y]);
            if (x - 1 >= 0 && y - 1 >= 0) ret.Add(board.squareArray[x - 1, y - 1]);

            return ret;
        }

        //gets the rook slider moves - for calculating pushMask
        public static List<square> rookSliderMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            int i = x; int j = y;
            bool shouldStop = false;

            //starting from the top, clockwise
            i = x; j = y - 1; shouldStop = false;
            while (j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                j--;
            }
            //90
            i = x + 1; j = y; shouldStop = false;
            while (i < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i++;
            }
            //180
            i = x; j = y + 1; shouldStop = false;
            while (j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                j++;
            }
            //270
            i = x - 1; j = y; shouldStop = false;
            while (i >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i--;
            }

            return ret;
        }

        //gets the bishop slider moves
        public static List<square> bishopSliderMoves(square startSquare)
        {
            List<square> ret = new List<square>();
            int x = startSquare.xPosition;
            int y = startSquare.yPosition;
            int i = x; int j = y;
            bool shouldStop = false;

            //starting clockwise from the top
            i = x + 1; j = y - 1;
            while (i < 8 && j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i++; j--;
            }
            //90
            i = x + 1; j = y + 1; shouldStop = false;
            while (i < 8 && j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i++; j++;
            }
            //180
            i = x - 1; j = y + 1; shouldStop = false;
            while (i >= 0 && j < 8)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i--; j++;
            }
            //270
            i = x - 1; j = y - 1; shouldStop = false;
            while (i >= 0 && j >= 0)
            {
                ret.Add(board.squareArray[i, j]);
                if (board.squareArray[i, j].here != null)
                {
                    shouldStop = true;
                }
                if (shouldStop) break;
                i--; j--;
            }

            return ret;
        }
    }
}
