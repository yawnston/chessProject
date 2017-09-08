using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProject
{
    static class ai_core
    {
        //this "a.i." just returns random moves - not very challenging to play against
        public static move getMove()
        {
            move ret;
            int legalMoveCount = board.legalMoves.Count();

            //get a random legal move
            Random rnd = new Random();
            try
            {
                int roll = rnd.Next(0, legalMoveCount);
                ret = board.legalMoves.ElementAt(roll);
            }
            catch (IndexOutOfRangeException)
            {
                //this only happens specifically on the last turn of an A.I. game
                return null;
            }

            return ret;
        }
    }
}
