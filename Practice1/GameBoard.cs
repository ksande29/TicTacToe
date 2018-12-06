using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice1
{
    class GameBoard
    {
        private Marker[] pieces = new Marker[capacity];
        private static int capacity = 8;
        int index = 0;
        private Marker[] boardLocation = new Marker[10];
        char currentTurn = 'O';

        int marginTop = 2;
        int marginLeft = 5;
        int rowOne = 4;
        int rowTwo = 6;
        int rowThree = 8;
        int bottomText = 12;
        int colOne = 15;
        int colTwo = 19;
        int colThree = 23;

        int xFreq = 220;
        int oFreq = 329;
        int soundDur = 600;

        ConsoleColor mainBackgroundColor = ConsoleColor.Black;
        ConsoleColor mainTextColor = ConsoleColor.DarkGray;
        ConsoleColor gameBackgroundColor = ConsoleColor.White;
        ConsoleColor boardColor = ConsoleColor.Gray;
        ConsoleColor gameTextColor = ConsoleColor.Black;
        ConsoleColor xColor = ConsoleColor.Red;
        ConsoleColor oColor = ConsoleColor.Blue;

        public void NewGame()
        {
            SetUpGame();
            while (!IsWin())
            {
                ToggleTurn();
                TakeTurn();
            }
            EndGame();
        }

        /// <summary>
        /// Sets up the game, prints the game background, prints a welcome message, and prints the game board
        /// </summary>
        public void SetUpGame()
        {
            //Print Background
            Console.CursorTop = marginTop;
            Console.BackgroundColor = gameBackgroundColor;
            for (int i = 0; i < 11; i++)
            {
                Console.CursorLeft = marginLeft;
                Console.WriteLine("{0,42}", "");
            }

            //Print Welcome
            Console.CursorTop = marginTop + 1;
            Console.CursorLeft = marginLeft + 3;
            Console.ForegroundColor = gameTextColor;
            Console.WriteLine("Welcome to Tic-Tac-Toe with a Twist!");

            //Print Board
            Console.BackgroundColor = boardColor;
            //row 1
            Console.CursorTop = marginTop + 3;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("   |");
            Console.Write("   |");
            Console.WriteLine("   ");
            Console.CursorTop = marginTop + 4;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("_ _|");
            Console.Write("_ _|");
            Console.WriteLine("_ _");
            //row 2
            Console.CursorTop = marginTop + 5;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("   |");
            Console.Write("   |");
            Console.WriteLine("   ");
            Console.CursorTop = marginTop + 6;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("_ _|");
            Console.Write("_ _|");
            Console.WriteLine("_ _");
            //row 3
            Console.CursorTop = marginTop + 7;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("   |");
            Console.Write("   |");
            Console.WriteLine("   ");
            Console.CursorTop = marginTop + 8;
            Console.CursorLeft = marginLeft + colOne;
            Console.Write("   |");
            Console.Write("   |");
            Console.WriteLine("   ");

            ApplyDefaultColors();
            SendCursorToBottom();
        }

        /// <summary>
        /// Allows a palyer to take a turn in the game. It prints the palyer's turn, takes in user input and checks
        /// if the input is a valid. If the move is not valid then it restarts the turn and if the move is valid it
        /// prints the pice on the board and makes a sound
        /// </summary>
        public void TakeTurn()
        {
            //Get user input
            SendCursorToBottom();
            ApplyXYColor();
            Console.Write(currentTurn + "'s Turn!   ");
            ApplyDefaultColors();
            Console.Write("Select a square (1-9): ");
            string input = Console.ReadLine();
            bool invalidInput = false;
            int position;
            if (int.TryParse(input, out position) && position > 0 && position < 10)
                position = int.Parse(input);
            else
                invalidInput = true;

            if (boardLocation[position] != null)
                invalidInput = true;

            //clear input line
            SendCursorToBottom();
            Console.Write("                                                 ");

            if (invalidInput)
            {
                TakeTurn();
            }
            else
            {
                CreateMarker(position);
                RemoveMarker();
                MoveSound();
                PrintMarker(currentTurn, position);
            }
        }

        /// <summary>
        /// Creates a new marker which can be placed on the gameboard
        /// </summary>
        /// <param name="position"></param>
        public void CreateMarker(int position)
        {
            Marker marker = new Marker(currentTurn, position);
            pieces[index] = marker;
            index++;
            if (index == capacity)
                index = 0;
            boardLocation[position] = marker;
        }

        /// <summary>
        /// Removes a marker from the gameboard
        /// </summary>
        public void RemoveMarker()
        {
            if (pieces[index] != null)
            {
                //erase marker from display
                int square = pieces[index].position;
                PrintMarker(' ', square);
                //remove marker from board array
                boardLocation[square] = null;
            }
        }

        /// <summary>
        /// Prints a marker on a specified location on the gameboard
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="position"></param>
        public void PrintMarker(char symbol, int position)
        {
            //set color
            Console.BackgroundColor = boardColor;
            ApplyXYColor();

            //set cursor in selected square
            if (position == 1)
            {
                Console.CursorTop = marginTop + rowOne;
                Console.CursorLeft = marginLeft + colOne +1;
            }
            if (position == 2)
            {
                Console.CursorTop = marginTop + rowOne;
                Console.CursorLeft = marginLeft + colTwo +1;
            }
            if (position == 3)
            {
                Console.CursorTop = marginTop + rowOne;
                Console.CursorLeft = marginLeft + colThree +1;
            }
            if (position == 4)
            {
                Console.CursorTop = marginTop + rowTwo;
                Console.CursorLeft = marginLeft + colOne +1;
            }
            if (position == 5)
            {
                Console.CursorTop = marginTop + rowTwo;
                Console.CursorLeft = marginLeft + colTwo +1;
            }
            if (position == 6)
            {
                Console.CursorTop = marginTop + rowTwo;
                Console.CursorLeft = marginLeft + colThree +1;
            }
            if (position == 7)
            {
                Console.CursorTop = marginTop + rowThree;
                Console.CursorLeft = marginLeft + colOne +1;
            }
            if (position == 8)
            {
                Console.CursorTop = marginTop + rowThree;
                Console.CursorLeft = marginLeft + colTwo +1;
            }
            if (position == 9)
            {
                Console.CursorTop = marginTop + rowThree;
                Console.CursorLeft = marginLeft + colThree +1;
            }
            Console.Write(symbol);

            ApplyDefaultColors();
            SendCursorToBottom();
        }

        /// <summary>
        /// Switches which player's turn it is
        /// </summary>
        public void ToggleTurn()
        {
            if (currentTurn == 'X')
                currentTurn = 'O';
            else
                currentTurn = 'X';
        }

        /// <summary>
        /// Checks for a win
        /// </summary>
        /// <returns></returns>
        public bool IsWin()
        {
            foreach(Marker marker in pieces)
            {

                if (        (boardLocation[1] != null && boardLocation[2] != null && boardLocation[3] != null 
                                && boardLocation[1].symbol == boardLocation[2].symbol && boardLocation[2].symbol == boardLocation[3].symbol)
                        ||  (boardLocation[4] != null && boardLocation[5] != null && boardLocation[6] != null
                                && boardLocation[4].symbol == boardLocation[5].symbol && boardLocation[5].symbol == boardLocation[6].symbol)
                        ||  (boardLocation[7] != null && boardLocation[8] != null && boardLocation[9] != null
                                && boardLocation[7].symbol == boardLocation[8].symbol && boardLocation[8].symbol == boardLocation[9].symbol)
                        ||  (boardLocation[1] != null && boardLocation[4] != null && boardLocation[7] != null
                                && boardLocation[1].symbol == boardLocation[4].symbol && boardLocation[4].symbol == boardLocation[7].symbol)
                        ||  (boardLocation[2] != null && boardLocation[5] != null && boardLocation[8] != null
                                && boardLocation[2].symbol == boardLocation[5].symbol && boardLocation[5].symbol == boardLocation[8].symbol)
                        ||  (boardLocation[3] != null && boardLocation[6] != null && boardLocation[9] != null
                                && boardLocation[3].symbol == boardLocation[6].symbol && boardLocation[6].symbol == boardLocation[9].symbol)
                        ||  (boardLocation[1] != null && boardLocation[5] != null && boardLocation[9] != null
                                && boardLocation[1].symbol == boardLocation[5].symbol && boardLocation[5].symbol == boardLocation[9].symbol)
                        ||  (boardLocation[3] != null && boardLocation[5] != null && boardLocation[7] != null
                                && boardLocation[3].symbol == boardLocation[5].symbol && boardLocation[5].symbol == boardLocation[7].symbol))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ends the game and displays who the winner is
        /// </summary>
        public void EndGame()
        {
            //set color for X or Y
            ApplyXYColor();

            SendCursorToBottom();
            Console.WriteLine(currentTurn + " Wins!");
            System.Threading.Thread.Sleep(400);
            ApplyDefaultColors();
        }

        /// <summary>
        /// Sets colors to default values
        /// </summary>
        private void ApplyDefaultColors()
        {
            //reset colors
            Console.BackgroundColor = mainBackgroundColor;
            Console.ForegroundColor = mainTextColor;
        }

        /// <summary>
        /// Changes the color based on which player's turn it is
        /// </summary>
        private void ApplyXYColor()
        {
            if (currentTurn == 'X')
                Console.ForegroundColor = xColor;
            else
                Console.ForegroundColor = oColor;
        }

        /// <summary>
        /// Makes a sound depending on whos turn it is
        /// </summary>
        private void MoveSound()
        {
            if (currentTurn == 'X')
                Console.Beep(xFreq, soundDur);
            else
                Console.Beep(oFreq, soundDur);
        }

        /// <summary>
        /// Resets the cursor to the bottom of the display
        /// </summary>
        private void SendCursorToBottom()
        {
            Console.CursorTop = marginTop + bottomText;
            Console.CursorLeft = marginLeft;
        }
        
    }
}
