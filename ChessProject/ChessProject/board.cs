using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace ChessProject
{
    public partial class board : Form
    {
        public const int tileSize = 60;
        public const int gridSize = 8;

        public static string mode;
        public static string playerfaction; //only relevant for A.I. play, unused in 2player
        public static string turn = "black"; //indicates whose turn it is right now, starts at black because of the first end turn call
        public static int turnCount = -1; //the number of turns the game took

        //contains all the pieces in play
        public static List<piece> whitePieces = new List<piece>();
        public static List<piece> blackPieces = new List<piece>();

        //the piece that is currently selected by the played (e.g. has been clicked on)
        private static piece selectedPiece = null;
        private static List<square> movableSquares;

        //list of all the legal moves the player can make
        public static List<move> legalMoves = new List<move>();

        //boolean for checking whether it's the first move of the game
        public static bool firstMove = true;
        //booleans for checking whether the player has the right to castle
        public static bool castleLeftWhite = true;
        public static bool castleRightWhite = true;
        public static bool castleLeftBlack = true;
        public static bool castleRightBlack = true;

        //when the user closes the game board it returns him to the main menu instead
        //TODO: handle save states and save on quit?
        protected override void OnClosed(EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        // this contains all the tiles, set at 8x8 for now
        public static Panel[,] _chessBoardPanels;

        public static square[,] squareArray;

        // init procedure of the gameboard
        //TODO: settings menu for color scheme etc., change arguments of loadBoard
        private void loadBoard(object sender, EventArgs e)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            //pfc.AddFontFile(ChessProject.Properties.Resources.);

            
            var colorDark = Color.DarkGray;
            var colorLight = Color.White;

            _chessBoardPanels = new Panel[gridSize, gridSize];

            for (var n = 0; n < gridSize; n++)
            {
                for (var m = 0; m < gridSize; m++)
                {
                    //each loop is one tile created
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * n, tileSize * m)
                    };
                    newPanel.BorderStyle = BorderStyle.FixedSingle;

                    //testing label - for debugging purposes uncomment label code (currently not working)
                    /*var newLabel = new Label
                    {
                        //location is relative to the containing object - aka the panel
                        Size = new Size(10, 10),
                        Location = new Point(0, 0)
                    };
                    newLabel.Font = new Font(pfc.Families[0], 15, FontStyle.Regular);
                    newLabel.Text = Convert.ToString(tileSize * n);
                    newPanel.Controls.Add(newLabel);
                    */

                    //add to Form's Controls so that they show up
                    Controls.Add(newPanel);
                    _chessBoardPanels[n, m] = newPanel;

                    //set the method for handling the user clicking a square on the board
                    newPanel.MouseDown += new MouseEventHandler(boardClickHandler); 

                    //background color of the panel
                    if (n % 2 == 0)
                        newPanel.BackColor = m % 2 != 0 ? colorDark : colorLight;
                    else
                        newPanel.BackColor = m % 2 != 0 ? colorLight : colorDark;
                }
            }
        }

        //TODO
        //handler of the player clicking a square on the board
        private void boardClickHandler(object sender, EventArgs e)
        {
            var colorDark = Color.DarkGray;
            var colorLight = Color.White;
            var colorSelected = Color.OrangeRed;
            var colorMovable = Color.DarkSeaGreen;

            Panel clickedPanel = (Panel)sender;
            //the piece the player tried to click (can be null! = clicked an empty square)
            piece clickedPiece = squareArray[clickedPanel.Location.X / tileSize, clickedPanel.Location.Y / tileSize].here;
            square clickedSquare = squareArray[clickedPanel.Location.X / tileSize, clickedPanel.Location.Y / tileSize];

            //selecting squares works
            //TODO - check for present pieces
            //TODO - check of other selected squares - global variable?

            //dummy for debugging the game board - uncomment to use
            /*if(clickedPanel.BackColor == colorSelected)
            {
                if ((clickedPanel.Location.X / tileSize) % 2 == 0)
                    clickedPanel.BackColor = (clickedPanel.Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                else
                    clickedPanel.BackColor = (clickedPanel.Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
            }
            else clickedPanel.BackColor = colorSelected;*/
            

            //if it's the first turn of the game, end turn to evaluate moves for pieces
            if (firstMove)
            {
                firstMove = false;
                endTurn();
                if (board.mode == "Play versus A.I." && turn != playerfaction)
                {
                    move aiMove = ai_core.getMove();
                    //simulate the ai clicking on the board to make the move
                    boardClickHandler(_chessBoardPanels[aiMove.movablePiece.xPosition, aiMove.movablePiece.yPosition], null);
                    boardClickHandler(_chessBoardPanels[aiMove.targetSquare.xPosition, aiMove.targetSquare.yPosition], null);
                }
            }

            if (clickedPiece == null && selectedPiece == null) //player clicked meaninglessly
            {
                //do nothing
            }
            //the player attempted to select a piece while not having one selected
            else if (selectedPiece == null) //clicked piece can NOT be null
            {
                if(clickedPiece.pieceColor == turn) //the player clicked his own piece (selected)
                {
                    selectedPiece = clickedPiece;
                    movableSquares = new List<square>();
                    foreach(move m in legalMoves)
                    {
                        if (m.movablePiece == selectedPiece) movableSquares.Add(m.targetSquare);
                    }
                    //update the gameboard with movable squares (in green)
                    _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = colorSelected;
                    foreach (square s in movableSquares)
                    {
                        _chessBoardPanels[s.xPosition, s.yPosition].BackColor = colorMovable;
                    }
                }
                //else do nothing - the player tried to select the enemy piece
            }
            //the player attempted to deselect a piece
            else if ((clickedPiece == null && !(movableSquares.Contains(clickedSquare))) || (clickedPiece == selectedPiece))
            {
                //deselect the selected piece
                if ((_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.X / tileSize) % 2 == 0)
                    _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = (_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                else
                    _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = (_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
                selectedPiece = null;
                //update the board to deselect the pieces in color
                foreach(square s in movableSquares)
                {
                    Panel workingPanel = _chessBoardPanels[s.xPosition, s.yPosition];
                    if ((workingPanel.Location.X / tileSize) % 2 == 0)
                        workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                    else
                        workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
                }
                movableSquares = null;              
            }
            //the player selected one of his other pieces while having one selected already
            else if (selectedPiece != null && clickedPiece != null && clickedPiece.pieceColor == turn)
            {
                //deselect the selected piece
                if ((_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.X / tileSize) % 2 == 0)
                    _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = (_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                else
                    _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = (_chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
                selectedPiece = null;
                //update the board to deselect the pieces in color
                foreach (square s in movableSquares)
                {
                    Panel workingPanel = _chessBoardPanels[s.xPosition, s.yPosition];
                    if ((workingPanel.Location.X / tileSize) % 2 == 0)
                        workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                    else
                        workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
                }
                movableSquares = null;

                //now select the new piece
                selectedPiece = clickedPiece;
                movableSquares = new List<square>();
                foreach (move m in legalMoves)
                {
                    if (m.movablePiece == selectedPiece) movableSquares.Add(m.targetSquare);
                }
                //update the gameboard with movable squares (in green)
                _chessBoardPanels[selectedPiece.xPosition, selectedPiece.yPosition].BackColor = colorSelected;
                foreach (square s in movableSquares)
                {
                    _chessBoardPanels[s.xPosition, s.yPosition].BackColor = colorMovable;
                }
            }
            //the player performed a move (or a take)
            else if (selectedPiece != null && movableSquares.Contains(clickedSquare))
            {
                selectedPiece.move(clickedSquare);
                endTurn();

                //handler for the A.I. to make a move
                if(board.mode == "Play versus A.I." && turn != playerfaction)
                {
                    move aiMove = ai_core.getMove();
                    //simulate the ai clicking on the board to make the move
                    boardClickHandler(_chessBoardPanels[aiMove.movablePiece.xPosition, aiMove.movablePiece.yPosition],null);
                    boardClickHandler(_chessBoardPanels[aiMove.targetSquare.xPosition, aiMove.targetSquare.yPosition], null);
                }
            }
        }

        //separate constructors depending on gamemode for easy addition of modes
        public board(string mode)
        {
            InitializeComponent();
            board.mode = mode;
            loadBoard(this, null);

            //initiate square array with no pieces present
            squareArray = new square[8, 8];
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    squareArray[i, j] = new square(i, j);
                }
            }

            //only normal chess for now
            foreach (square sq in squareArray)
            {
                if(sq.yPosition == 1 || sq.yPosition == 6)
                {
                    if (sq.yPosition == 1)
                        sq.here = new Pieces.pawn(sq.xPosition, sq.yPosition, "black");
                    else
                        sq.here = new Pieces.pawn(sq.xPosition, sq.yPosition, "white");
                    //draw the piece on the chessboard
                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                if(sq.yPosition == 0) //black pieces
                {
                    if(sq.xPosition == 0 || sq.xPosition == 7)
                        sq.here = new Pieces.rook(sq.xPosition, sq.yPosition, "black");
                    if(sq.xPosition == 1 || sq.xPosition == 6)
                        sq.here = new Pieces.knight(sq.xPosition, sq.yPosition, "black");
                    if(sq.xPosition == 2 || sq.xPosition == 5)
                        sq.here = new Pieces.bishop(sq.xPosition, sq.yPosition, "black");
                    if(sq.xPosition == 3)
                        sq.here = new Pieces.queen(sq.xPosition, sq.yPosition, "black");
                    if(sq.xPosition == 4)
                        sq.here = new Pieces.king(sq.xPosition, sq.yPosition, "black");

                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                if (sq.yPosition == 7) //white pieces
                {
                    if (sq.xPosition == 0 || sq.xPosition == 7)
                        sq.here = new Pieces.rook(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 1 || sq.xPosition == 6)
                        sq.here = new Pieces.knight(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 2 || sq.xPosition == 5)
                        sq.here = new Pieces.bishop(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 3)
                        sq.here = new Pieces.queen(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 4)
                        sq.here = new Pieces.king(sq.xPosition, sq.yPosition, "white");

                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                //add the pieces to the piece list
                if(sq.here != null)
                {
                    if (sq.here.pieceColor == "white") whitePieces.Add(sq.here);
                    else blackPieces.Add(sq.here);
                }                
            }
        }
        
        //A.I. play constructor
        public board(string mode, string faction)
        {
            InitializeComponent();
            board.mode = mode;
            loadBoard(this, null);
            playerfaction = faction;

            //initiate square array with no pieces present
            squareArray = new square[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squareArray[i, j] = new square(i, j);
                }
            }

            //only normal chess for now
            foreach (square sq in squareArray)
            {
                if (sq.yPosition == 1 || sq.yPosition == 6)
                {
                    if (sq.yPosition == 1)
                        sq.here = new Pieces.pawn(sq.xPosition, sq.yPosition, "black");
                    else
                        sq.here = new Pieces.pawn(sq.xPosition, sq.yPosition, "white");
                    //draw the piece on the chessboard
                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                if (sq.yPosition == 0) //black pieces
                {
                    if (sq.xPosition == 0 || sq.xPosition == 7)
                        sq.here = new Pieces.rook(sq.xPosition, sq.yPosition, "black");
                    if (sq.xPosition == 1 || sq.xPosition == 6)
                        sq.here = new Pieces.knight(sq.xPosition, sq.yPosition, "black");
                    if (sq.xPosition == 2 || sq.xPosition == 5)
                        sq.here = new Pieces.bishop(sq.xPosition, sq.yPosition, "black");
                    if (sq.xPosition == 3)
                        sq.here = new Pieces.queen(sq.xPosition, sq.yPosition, "black");
                    if (sq.xPosition == 4)
                        sq.here = new Pieces.king(sq.xPosition, sq.yPosition, "black");

                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                if (sq.yPosition == 7) //white pieces
                {
                    if (sq.xPosition == 0 || sq.xPosition == 7)
                        sq.here = new Pieces.rook(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 1 || sq.xPosition == 6)
                        sq.here = new Pieces.knight(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 2 || sq.xPosition == 5)
                        sq.here = new Pieces.bishop(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 3)
                        sq.here = new Pieces.queen(sq.xPosition, sq.yPosition, "white");
                    if (sq.xPosition == 4)
                        sq.here = new Pieces.king(sq.xPosition, sq.yPosition, "white");

                    _chessBoardPanels[sq.xPosition, sq.yPosition].BackgroundImage = sq.here.pieceImage;
                }
                //add the pieces to the piece list
                if (sq.here != null)
                {
                    if (sq.here.pieceColor == "white") whitePieces.Add(sq.here);
                    else blackPieces.Add(sq.here);
                }
            }
        }

        //function that handles end of turn and calculation of the new turn
        //called after a player has made his move => switch sides
        //TODO
        private void endTurn()
        {
            var colorDark = Color.DarkGray;
            var colorLight = Color.White;
            var colorSelected = Color.OrangeRed;
            var colorMovable = Color.DarkSeaGreen;

            turnCount++;
            //change turn owner
            if (turn == "white") turn = "black";
            else turn = "white";
            legalMoves = new List<move>();
            //this handles the mask calculation
            masks.calculateCheckers();
            //deselect piece and reset the board colors
            selectedPiece = null;
            foreach (square s in squareArray)
            {
                Panel workingPanel = _chessBoardPanels[s.xPosition, s.yPosition];
                if ((workingPanel.Location.X / tileSize) % 2 == 0)
                    workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorDark : colorLight;
                else
                    workingPanel.BackColor = (workingPanel.Location.Y / tileSize) % 2 != 0 ? colorLight : colorDark;
            }

            //calculate new move list
            pins.calculatePins();
            core.calculateMoves();

            //conditions for end of the game
            if(legalMoves.Count == 0 && masks.checkerCount == 0)
            {
                //game ends in a draw
                gameOver("draw");
            }
            else if(legalMoves.Count == 0 && masks.checkerCount > 0)
            {
                //the opponent wins
                if (turn == "white")
                {
                    gameOver("black");
                }
                else gameOver("white");
            }
            //uncomment for debugging of masks - currently works(maybe?)
            /*for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (masks.kingDangerMask[i, j]) _chessBoardPanels[i, j].BackColor = Color.Blue;
                }
            }*/
        }

        //TODO: handle save slots
        private void savequitButton_Click(object sender, EventArgs e)
        {
            //cleanup
            pins.existingPins = new List<pin>();
            board.legalMoves = new List<move>();
            board.whitePieces = new List<piece>();
            board.blackPieces = new List<piece>();
            turn = "black";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    masks.kingDangerMask[i, j] = false;
                    masks.captureMask[i, j] = false;
                    masks.pushMask[i, j] = false;
                    masks.checkingMask[i, j] = false;
                    board.squareArray[i, j].here = null;
                }
            }
            firstMove = true;
            masks.checkerCount = 0;
            castleLeftWhite = true;
            castleRightWhite = true;
            castleLeftBlack = true;
            castleRightBlack = true;
            this.Close();
        }

        public void gameOver(string result)
        {
            if (result == "draw")
                MessageBox.Show("The game ended in a draw! \nTurn count: " + turnCount);
            else
                MessageBox.Show("The winner is " + result + "! \nTurn count: " + turnCount);
            this.Close();
        }
    }

    //the board state is represented with an array of squares - each containing a reference to the contained piece
    public class square
    {
        //numbering of squares starts in the top left of the board
        public int xPosition;
        public int yPosition;

        //represents the piece that occupies this square (or null if empty)
        public piece here;

        public square(int x, int y)
        {
            this.xPosition = x;
            this.yPosition = y;
            here = null;
        }
    }

    //this represents a move: a piece and a square where the piece can go
    public class move
    {
        public piece movablePiece;
        public square targetSquare;

        public move(piece p, square s)
        {
            movablePiece = p;
            targetSquare = s;
        }
    }
}
