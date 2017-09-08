using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject.Pieces
{
    class pawn : piece
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

        private bool moved = false;

        public override List<square> getLegalMoves()
        {
            List<square> moveList = new List<square>();

            if(color == "white")
            {
                //double move for pawns
                if (!moved && board.squareArray[xPosition, yPosition - 2].here == null && board.squareArray[xPosition,yPosition - 1].here == null)
                {
                    moveList.Add(board.squareArray[xPosition, yPosition - 2]);
                }
                //regular move 1 square forward
                if (board.squareArray[xPosition, yPosition - 1].here == null)
                {
                    moveList.Add(board.squareArray[xPosition, yPosition - 1]);
                }
                //take on the left
                if (((xPosition - 1) >= 0) && board.squareArray[xPosition - 1, yPosition - 1].here != null
                    && board.squareArray[xPosition - 1, yPosition - 1].here.pieceColor != this.pieceColor)
                {
                    moveList.Add(board.squareArray[xPosition - 1, yPosition - 1]);
                }
                //take on the right
                if (((xPosition + 1) <= 7) && board.squareArray[xPosition + 1, yPosition - 1].here != null
                    && board.squareArray[xPosition + 1, yPosition - 1].here.pieceColor != this.pieceColor)
                {
                    moveList.Add(board.squareArray[xPosition + 1, yPosition - 1]);
                }
            }

            if (color == "black")
            {
                //double move for pawns
                if (!moved && board.squareArray[xPosition, yPosition + 2].here == null && board.squareArray[xPosition, yPosition + 1].here == null)
                {
                    moveList.Add(board.squareArray[xPosition, yPosition + 2]);
                }
                //regular move 1 square forward
                if (board.squareArray[xPosition, yPosition + 1].here == null)
                {
                    moveList.Add(board.squareArray[xPosition, yPosition + 1]);
                }
                //take on the left
                if (((xPosition - 1) >= 0) && board.squareArray[xPosition - 1, yPosition + 1].here != null
                    && board.squareArray[xPosition - 1, yPosition + 1].here.pieceColor != this.pieceColor)
                {
                    moveList.Add(board.squareArray[xPosition - 1, yPosition + 1]);
                }
                //take on the right
                if (((xPosition + 1) <= 7) && board.squareArray[xPosition + 1, yPosition + 1].here != null
                    && board.squareArray[xPosition + 1, yPosition + 1].here.pieceColor != this.pieceColor)
                {
                    moveList.Add(board.squareArray[xPosition + 1, yPosition + 1]);
                }
            }

            return moveList;
        }

        //TODO: implement en passant
        public override void move(square s)
        {
            moved = true;
            //kill the piece if there is one
            if(s.here != null)
            {
                s.here.kill();
            }
            board.squareArray[this.xPosition, this.yPosition].here = null;
            board._chessBoardPanels[this.xPosition, this.yPosition].BackgroundImage = null;
            s.here = this;
            board._chessBoardPanels[s.xPosition, s.yPosition].BackgroundImage = this.pieceImage;
            this.xPos = s.xPosition;
            this.yPos = s.yPosition;
            //promote the pawn
            if(this.yPosition == 0 || this.yPosition == 7)
            {
                if(this.pieceColor == "white")
                {
                    board.squareArray[this.xPosition, this.yPosition].here = new Pieces.queen(this.xPosition, this.yPosition, "white");                    
                }
                else
                {
                    board.squareArray[this.xPosition, this.yPosition].here = new Pieces.queen(this.xPosition, this.yPosition, "black");
                }
                if (this.pieceColor == "white") board.whitePieces.Add(board.squareArray[this.xPosition, this.yPosition].here);
                else board.blackPieces.Add(board.squareArray[this.xPosition, this.yPosition].here);
                board._chessBoardPanels[this.xPosition, this.yPosition].BackgroundImage = board.squareArray[this.xPosition, this.yPosition].here.pieceImage;
                this.kill();
            }
        }

        public override void kill()
        {
            if (!(this.yPosition == 0 || this.yPosition == 7))
            {
                board.squareArray[xPosition, yPosition].here = null;
                board._chessBoardPanels[xPosition, yPosition].BackgroundImage = null; 
            }
            if (this.pieceColor == "white") board.whitePieces.Remove(this);
            else board.blackPieces.Remove(this);
        }

        public pawn(int x, int y, string owner)
        {
            xPos = x;
            yPos = y;
            type = "pawn";
            color = owner;
            if (color == "white") img = Image.FromFile("pawn-white.png");
            if (color == "black") img = Image.FromFile("pawn-black.png");
        }
    }
}
