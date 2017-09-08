using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessProject.Pieces
{
    class king:piece
    {
        private int xPos;
        private int yPos;

        public override int xPosition
        {
            get
            {
                return xPos;
            }
        }

        public override int yPosition
        {
            get
            {
                return yPos;
            }
        }

        private string type;

        public override string pieceType
        {
            get
            {
                return type;
            }
        }

        private string color;

        public override string pieceColor
        {
            get
            {
                return color;
            }
        }

        private Image img;

        public override Image pieceImage
        {
            get
            {
                return img;
            }
        }

        //TODO
        public override List<square> getLegalMoves()
        {
            List<square> pseudoLegal = new List<square>();
            List<square> ret = new List<square>();
            int x = this.xPosition;
            int y = this.yPosition;
            //starting from the top clockwise
            if (y - 1 >= 0 && (board.squareArray[x, y - 1].here == null 
                || board.squareArray[x, y - 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x, y - 1]);
            if (y - 1 >= 0 && x + 1 < 8 && (board.squareArray[x + 1, y - 1].here == null
                || board.squareArray[x + 1, y - 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x + 1, y - 1]);
            //90
            if (x + 1 < 8 && (board.squareArray[x + 1, y].here == null
                || board.squareArray[x + 1, y].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x + 1, y]);
            if (y + 1 < 8 && x + 1 < 8 && (board.squareArray[x + 1, y + 1].here == null
                || board.squareArray[x + 1, y + 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x + 1, y + 1]);
            //180
            if (y + 1 < 8 && (board.squareArray[x, y + 1].here == null
                || board.squareArray[x, y + 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x, y + 1]);
            if (x - 1 >= 0 && y + 1 < 8 && (board.squareArray[x - 1, y + 1].here == null
                || board.squareArray[x - 1, y + 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x - 1, y + 1]);
            //270
            if (x - 1 >= 0 && (board.squareArray[x - 1, y].here == null
                || board.squareArray[x - 1, y].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x - 1, y]);
            if (x - 1 >= 0 && y - 1 >= 0 && (board.squareArray[x - 1, y - 1].here == null
                || board.squareArray[x - 1, y - 1].here.pieceColor != board.turn)) pseudoLegal.Add(board.squareArray[x - 1, y - 1]);

            //check for castle options
            if(this.pieceColor == "white")
            {
                //white can castle right
                if (board.castleRightWhite)
                {
                    //conditions to check: not in check, nothing in the way, not castling through check
                    //we can assume the king to be at [4,7] because he hasn't moved
                    if(masks.kingDangerMask[5,7] == false && masks.kingDangerMask[4, 7] == false
                        && board.squareArray[5,7].here == null && board.squareArray[6, 7].here == null)
                    {
                        pseudoLegal.Add(board.squareArray[6, 7]);
                    }
                }
                //white can castle left
                if (board.castleLeftWhite)
                {
                    if (masks.kingDangerMask[2, 7] == false && masks.kingDangerMask[3, 7] == false
                        && board.squareArray[1, 7].here == null && board.squareArray[2, 7].here == null && board.squareArray[3,7].here == null)
                    {
                        pseudoLegal.Add(board.squareArray[2, 7]);
                    }
                }
            }
            else if(this.pieceColor == "black")
            {
                //black can castle right
                if (board.castleRightBlack)
                {
                    //conditions to check: not in check, nothing in the way, not castling through check
                    //we can assume the king to be at [4,0] because he hasn't moved
                    if (masks.kingDangerMask[5, 0] == false && masks.kingDangerMask[4, 0] == false
                        && board.squareArray[5, 0].here == null && board.squareArray[6, 0].here == null)
                    {
                        pseudoLegal.Add(board.squareArray[6, 0]);
                    }
                }
                //black can castle left
                if (board.castleLeftBlack)
                {
                    if (masks.kingDangerMask[2, 0] == false && masks.kingDangerMask[3, 0] == false
                        && board.squareArray[1, 0].here == null && board.squareArray[2, 0].here == null && board.squareArray[3, 0].here == null)
                    {
                        pseudoLegal.Add(board.squareArray[2, 0]);
                    }
                }
            }

            //now we have pseudolegal moves - need to use the masks
            foreach(square s in pseudoLegal)
            {
                if(masks.kingDangerMask[s.xPosition,s.yPosition] == false)
                {
                    ret.Add(s);
                }
            }
            return ret;
        }

        public override void move(square s)
        {
            //kill the piece if there is one
            if (s.here != null)
            {
                s.here.kill();
            }

            //moving the king prevents future castle
            if (this.pieceColor == "white")
            {
                //this means white is castling right
                if(s.xPosition == 6 && s.yPosition == 7 && this.xPosition == 4 && this.yPosition == 7)
                {
                    board.squareArray[7, 7].here.move(board.squareArray[5, 7]);
                }
                //this means white is castling right
                if (s.xPosition == 2 && s.yPosition == 7 && this.xPosition == 4 && this.yPosition == 7)
                {
                    board.squareArray[0, 7].here.move(board.squareArray[3, 7]);
                }
                board.castleLeftWhite = false;
                board.castleRightWhite = false;
            }
            else if (this.pieceColor == "black")
            {
                //this means black is castling right
                if (s.xPosition == 6 && s.yPosition == 0 && this.xPosition == 4 && this.yPosition == 0)
                {
                    board.squareArray[7, 0].here.move(board.squareArray[5, 0]);
                }
                //this means white is castling right
                if (s.xPosition == 2 && s.yPosition == 0 && this.xPosition == 4 && this.yPosition == 0)
                {
                    board.squareArray[0, 0].here.move(board.squareArray[3, 0]);
                }
                board.castleLeftBlack = false;
                board.castleRightBlack = false;
            }

            board.squareArray[this.xPosition, this.yPosition].here = null;
            board._chessBoardPanels[this.xPosition, this.yPosition].BackgroundImage = null;
            s.here = this;
            board._chessBoardPanels[s.xPosition, s.yPosition].BackgroundImage = this.pieceImage;
            this.xPos = s.xPosition;
            this.yPos = s.yPosition;
        }

        //if by some miracle a kill() is called on a king(SHOULD NOT HAPPEN), this ends the game properly
        public override void kill()
        {
            string gameResult;

            board.squareArray[xPosition, yPosition].here = null;
            board._chessBoardPanels[xPosition, yPosition].BackgroundImage = null;

            if (color == "white") gameResult = "black";
            else gameResult = "white";

            landing.gameBoard.gameOver(gameResult);
        }

        public king(int x, int y, string owner)
        {
            xPos = x;
            yPos = y;
            type = "king";
            color = owner;
            if (color == "white") img = Image.FromFile("king-white.png");
            if (color == "black") img = Image.FromFile("king-black.png");
        }
    }
}
