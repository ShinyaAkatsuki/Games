using System;

namespace Games
{
    public class GamesContent
    {
        private char playerX = 'X';
        private char playerO = 'O';
        private char currentPlayer;
        private int playerMoves = 6;
        private int coordinateToMoveX;
        private int coordinateToMoveY;
        private string gameMode;

        public int PlayerMoves
        {
            get
            {
                return playerMoves;
            }
        }
        public char CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
        }

        public char[,] GameBoard { get; set; }
        public GamesContent(string gameMode)
        {
            this.gameMode = gameMode;
            currentPlayer = playerX;

            GameBoard = new char[3, 3]
            {
                {' ', ' ', ' '},
                {' ', ' ', ' '},
                {' ', ' ', ' '}
            };
        }
        public string GetGameBoardView()
        {
            string resultat = "";
            resultat = resultat + "Y\n";
            resultat = resultat + "  *******************\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "3 *  " + GameBoard[0, 2] + "  *  " + GameBoard[1, 2] + "  *  " + GameBoard[2, 2] + "  *\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "  *******************\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "2 *  " + GameBoard[0, 1] + "  *  " + GameBoard[1, 1] + "  *  " + GameBoard[2, 1] + "  *\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "  *******************\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "1 *  " + GameBoard[0, 0] + "  *  " + GameBoard[1, 0] + "  *  " + GameBoard[2, 0] + "  *\n";
            resultat = resultat + "  *     *     *     *\n";
            resultat = resultat + "  *******************\n";
            resultat = resultat + "     1     2     3    X\n";

            return resultat;
        }

        private bool IsCoordinateValid(string[] inputs)
        {
            if (inputs.Length == 2)
            {
                string inputX = inputs[0];
                string inputY = inputs[1];

                if (inputX.Length == 1 && (inputX == "1" || inputX == "2" || inputX == "3") &&
                    (inputY.Length == 1 && (inputY == "1" || inputY == "2" || inputY == "3")))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PlacePiece(string userInput)
        {
            string[] inputs = userInput.Split(',');
            int x = 0;
            int y = 0;

            // Check if move is valid
            if (IsCoordinateValid(inputs))
            {
                x = Convert.ToInt32(inputs[0]) - 1;
                y = Convert.ToInt32(inputs[1]) - 1;

                if (GameBoard[x, y] == ' ')
                {
                    GameBoard[x, y] = currentPlayer;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            // Move is valid

            // Check for winning move
            bool winnerFound = CheckWin(x, y);
            if (winnerFound == false)
            {
                ChangePlayer();
            }
            return winnerFound;
        }

        private void ChangePlayer()
        {
            if (gameMode == "Variation")
            {
                playerMoves--;
            }

            if (currentPlayer == playerX)
            {
                currentPlayer = playerO;
            }
            else
            {
                currentPlayer = playerX;
            }
        }

        private bool CheckWin(int x, int y)
        {
            if (CheckWinRow(x) || CheckWinColumn(y))
            {
                return true;
            }
            if ((x == 0 && y == 2) || (x == 2 && y == 0))
            {
                return CheckWinDigDown();
            }
            if ((x == 0 && y == 0) || (x == 2 && y == 2))
            {
                return CheckWinDigUp();
            }
            if (x == 1 && y == 1)
            {
                return CheckWinDigDown() || CheckWinDigUp();
            }
            return false;
        }
        private bool CheckWinDigDown()
        {
            if (GameBoard[2, 0] == currentPlayer && GameBoard[1, 1] == currentPlayer && GameBoard[0, 2] == currentPlayer)
            {
                return true;
            }
            return false;
        }
        private bool CheckWinDigUp()
        {
            if (GameBoard[0, 0] == currentPlayer && GameBoard[1, 1] == currentPlayer && GameBoard[2, 2] == currentPlayer)
            {
                return true;
            }
            return false;
        }
        private bool CheckWinRow(int x)
        {
            bool winner = true;
            for (int i = 0; i < 3; i++)
            {
                if (GameBoard[x, i] != currentPlayer)
                {
                    winner = false;
                    break;
                }
            }
            return winner;
        }
        private bool CheckWinColumn(int y)
        {
            bool winner = true;
            for (int i = 0; i < 3; i++)
            {
                if (GameBoard[i, y] != currentPlayer)
                {
                    winner = false;
                    break;
                }
            }
            return winner;
        }

        public bool PickAPiece(string input)
        {
            string[] inputs = input.Split(',');

            if (IsCoordinateValid(inputs))
            {
                int x = Convert.ToInt32(inputs[0]) - 1;
                int y = Convert.ToInt32(inputs[1]) - 1;

                if (GameBoard[x, y] == currentPlayer)
                {
                    coordinateToMoveX = x;
                    coordinateToMoveY = y;

                    return true;
                }
            }
            return false;
        }

        public bool MovePiece(string input)
        {
            string[] inputs = input.Split(',');

            if (IsCoordinateValid(inputs))
            {
                int x = Convert.ToInt32(inputs[0]) - 1;
                int y = Convert.ToInt32(inputs[1]) - 1;

                if (GameBoard[x, y] == ' ')
                {
                    GameBoard[x, y] = currentPlayer;
                    GameBoard[coordinateToMoveX, coordinateToMoveY] = ' ';

                    if (CheckWin(x, y))
                    {
                        return true;
                    }
                    ChangePlayer();
                }
            }
            return false;
        }

        public bool IsDraw()
        {
            foreach (char spot in GameBoard)
            {
                if (spot == ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
