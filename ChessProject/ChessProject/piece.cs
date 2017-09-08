using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessProject
{
    //describes a generic piece, all pieces inherit from this class
    public abstract class piece
    {
        //the piece's position on the board
        public abstract int xPosition { get; }
        public abstract int yPosition { get; }

        //the type of piece, e.g. pawn or bishop
        public abstract string pieceType { get; }

        //owner of the piece
        public abstract string pieceColor { get; }

        //the image which will be shown on the board
        public abstract Image pieceImage { get; }

        //returns a list of all legal moves this piece can make
        public abstract List<square> getLegalMoves();

        //move the piece to the specified square
        //NOTE: the square must be legal!
        public abstract void move(square s);

        //remove the piece from play
        public abstract void kill();
    }
}
