using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members
        //Holds the current reuslts of cells in teh active game
        private MarkType[] mResults;
        //True if it is player 1s turn (X) or player 2's turn(0)
        private bool mPlayer1Turn;
        //True if the game is ended
        private bool mGameEnded;

        #endregion
        #region Constructor
        //defaul constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }


        #endregion
        private void NewGame()

        {
            //create a new blank array of free cells
            mResults = new MarkType[9];
            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;

            }

            //Make Sure player 1 starts the game

            mPlayer1Turn = true;
            //interate every button on the grid.
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;

            });
            //make sure the game hasn't finished
            mGameEnded = false;

        }

        //Handles a button click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;
            //find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            //dont do anything if the cell arleady has a value in it.
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which players turn it is. 
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            //set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";
            //change noughts to green
            if (mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //toggle the players turns.
            if (mPlayer1Turn)
            {
                mPlayer1Turn = false;
            }
            else
            {
                mPlayer1Turn = true;
            }

            //check for a winner

            CheckForWinner();



        }

        private void CheckForWinner()
        {
            #region Horizontal Wins  
            //check for horizontal wins - row 0

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //check fro horizontal wins - row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                //highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //check for horizontal wins - row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                //highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion


            #region Vertical Wins

            //Check for vertical wins - col 0

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                
                //highlight winning cells in green 
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //col 1

            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

                //col 2

                if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults [8]) == mResults[2])
                {
                    mGameEnded = true;

                    Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                }
            }


            #endregion

            #region Diagonal Wins

            //check for diagonal wins - top left botton right

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults [8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //check for diagonal wins = top right bottom left 

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }


            #endregion

            #region No Winners

            //check for no winner and full board

            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
             #endregion
            }
        }


    }
}
