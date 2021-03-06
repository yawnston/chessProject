﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessProject.Pieces
{
    class knight:piece
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

        public override List<square> getLegalMoves()
        {
            //get knight moves from the util (reused from resolving checks)
            List<square> consideredMoves = utils.knightMoves(board.squareArray[this.xPosition, this.yPosition]);
            //throw out moves that would put the knight on a friendly piece
            List<square> ret = new List<square>();
            foreach (square s in consideredMoves)
            {
                if (s.here == null || s.here.pieceColor != board.turn) ret.Add(s);
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
            board.squareArray[this.xPosition, this.yPosition].here = null;
            board._chessBoardPanels[this.xPosition, this.yPosition].BackgroundImage = null;
            s.here = this;
            board._chessBoardPanels[s.xPosition, s.yPosition].BackgroundImage = this.pieceImage;
            this.xPos = s.xPosition;
            this.yPos = s.yPosition;
        }

        public override void kill()
        {
            board.squareArray[xPosition, yPosition].here = null;
            board._chessBoardPanels[xPosition, yPosition].BackgroundImage = null;
            if (this.pieceColor == "white") board.whitePieces.Remove(this);
            else board.blackPieces.Remove(this);
        }

        public knight(int x, int y, string owner)
        {
            xPos = x;
            yPos = y;
            type = "knight";
            color = owner;
            if (color == "white") img = Image.FromFile("knight-white.png");
            if (color == "black") img = Image.FromFile("knight-black.png");
        }
    }
}
