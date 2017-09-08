using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public partial class landing : Form
    {
        public static board gameBoard;

        public static string faction;

        public landing()
        {
            InitializeComponent();
            colorBox.SelectedIndex = 0;
        }

        private void startButtonHandler(object sender, EventArgs e)
        {
            string gameMode;
            //faction is only relevant for A.I. play
            Button senderButton = (Button)sender;
            gameMode = senderButton.Text;
            if (gameMode == "Play versus A.I.")
            {
                switch (colorBox.SelectedIndex)
                {
                    //random selection
                    case 0:
                        Random rnd = new Random();
                        int roll = rnd.Next(1, 3);
                        if (roll == 1)
                            faction = "white";
                        if (roll == 2)
                            faction = "black";
                        break;

                    //white
                    case 1:
                        faction = "while";
                        break;
                    //black
                    case 2:
                        faction = "black";
                        break;
                }
                this.Hide();
                gameBoard = new board(gameMode, faction);
                gameBoard.Tag = this;
                gameBoard.ShowDialog(this);
            }
            else
            {
                this.Hide();
                gameBoard = new board(gameMode);
                gameBoard.Tag = this;
                gameBoard.ShowDialog(this);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
