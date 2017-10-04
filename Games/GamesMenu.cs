using System;

namespace Games
{
    class GamesMenu
    {
        private GamesContent game;

        public void ShowGameMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose game:");
            Console.WriteLine("1. Tic Tac Toe");
            Console.WriteLine("2. Battleship");
            Console.WriteLine("0. Quit game");
        }

        public void ShowGameModeMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose game mode:");
            Console.WriteLine("1. Standard Mode");
            Console.WriteLine("2. Variation Mode");
            //Console.WriteLine("3. Go to Game Menu");
            Console.WriteLine("0. Quit game");
        }

        public bool PlaceAPiece(string input)
        {
            return game.PlacePiece(input);
        }

        public bool ChooseGame(string choice)
        {
            if (choice == "1")
            {
                ShowGameModeMenu();
            }
            else if (choice == "2")
            {
                //Battleship
            }
            else
            {
                Console.WriteLine("Wrong input!");
                return false;
            }
            return true;
        }

        public bool ChooseGameMode(string choice)
        {
            if (choice == "1")
            {
                CreateStandardGame();
            }
            else if (choice == "2")
            {
                CreateVariationGame();
            }
            //else if (choice == "3")
            //{
            //    ShowGameMenu();
            //}
            else
            {
                Console.WriteLine("Wrong input!");
                return false;
            }
            return true;
        }

        public string ChooseOption(string choice)
        {
            if (choice == "1")
            {
                return "1";
            }
            else if (choice == "2")
            {
                return "2";
            }
            else if (choice == "0")
            {
                return "0";
            }
            else
            {
                Console.WriteLine("Wrong input!");
                return "Error";
            }
        }

        public void ShowGameBoard()
        {
            Console.Clear();
            Console.WriteLine(game.GetGameBoardView());
            Console.WriteLine("Player " + game.CurrentPlayer + " it is your turn.");
        }

        private void CreateStandardGame()
        {
            game = new GamesContent("Standard");
        }
        private void CreateVariationGame()
        {
            game = new GamesContent("Variation");
        }

        public char GetPlayer()
        {
            return game.CurrentPlayer;
        }

        public int GetPlayerMoves()
        {
            return game.PlayerMoves;
        }

        public bool MovePiece(string input)
        {
            return game.MovePiece(input);
        }

        public bool PickAPiece(string input)
        {
            return game.PickAPiece(input);
        }

        public bool IsDraw()
        {
            return game.IsDraw();
        }
    }
}