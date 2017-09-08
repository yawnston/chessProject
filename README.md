# chessProject
My first year second semester chess programming project

# Features
-play against a local oponent on the same device

-play against an A.I. (currently the A.I. is a random move generator)

-easy extensibility -  simple addition of new modes or new pieces

-**very fast** - uses an efficient algorithm for calculating checks, pins and legal moves


### Move algorithm

Chess as a game has a very high branching factor, as in the amount of possible moves in a game quickly increases with the number of turns taken.

This means that if one uses something like speculative move finding (take all possible moves and then toss out the illegal ones), one can find themselves with a very slow program.

My approach uses searching functions to determine pins and "dangerous" squares, and then very simply stores that information in a mask to be later used to filter out moves.

### Extensibility

Using object oriented programming and the concept of abstract classes, I made a chess application where it's very easy to add new modes or pieces,
just creating a new class and writing code to place the piece on the board will be enough for some very simple new pieces. 
If one wanted to create a very strange-acting piece, only a couple more edits to the "masks.cs" class would be needed to apply the new piece's behavior to 
the way masks are calculated.

It's also very easy to create new A.I. for the game, as the "ai_core.cs" class is the only thing that needs editing. In the future I might hook up a real chess engine 
to this class to provide an actual challenging opponent.

### Possible future features

-new game modes (like Really Bad Chess for example) and pieces

-export to PDN

-savegame option

-options menu



*Made by Daniel Crha in 2017*
