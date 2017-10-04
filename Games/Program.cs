using System;

namespace Games
{
    class Program
    {
        GamesMenu menu = new GamesMenu();

        static void Main(string[] args)
        {
            Program myProgram = new Program();
            myProgram.RunGameLoop();
        }

        private void RunGameLoop()
        {
            bool isGameRunning = true;
            do
            {
                bool isValidInput = false;

                // Choose game select loop
                menu.ShowGameMenu();
                do
                {
                    string userInput = Console.ReadLine();
                    if (ShouldQuit(userInput))
                    {
                        return;
                    }
                    isValidInput = menu.ChooseGame(userInput);
                } while (isValidInput == false);

                // Choose game mode loop
                menu.ShowGameModeMenu();
                do
                {
                    string userInput = Console.ReadLine();
                    if (ShouldQuit(userInput))
                    {
                        return;
                    }
                    isValidInput = menu.ChooseGameMode(userInput);
                } while (isValidInput == false);

                // ----------------------------------

                // React to input
                bool shouldQuit = false;
                do
                {
                    // Show gamemode
                    menu.ShowGameBoard();

                    if (menu.GetPlayerMoves() > 0)
                    {
                        Console.WriteLine("Write coordinate x,y or 0 to quit gamemode.");
                        string userInput = Console.ReadLine();

                        if (ShouldQuit(userInput))
                        {
                            shouldQuit = true;
                        }
                        else
                        {
                            shouldQuit = menu.PlaceAPiece(userInput);

                            if (shouldQuit != true && menu.IsDraw())
                            {
                                shouldQuit = true;
                                Console.WriteLine("Draw! Play again");
                                PressKeyToContinue();
                            }
                            else
                            {
                                ShowWinner(shouldQuit);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pick piece x,y or 0 to quit gamemode");
                        string userInput = Console.ReadLine();

                        if (ShouldQuit(userInput))
                        {
                            shouldQuit = true;
                        }
                        else
                        {
                            bool isValid = menu.PickAPiece(userInput);
                            if (isValid)
                            {
                                Console.WriteLine("Move piece to x,y");
                                userInput = Console.ReadLine();
                                shouldQuit = menu.MovePiece(userInput);
                                ShowWinner(shouldQuit);
                            }
                        }
                    }
                } while (shouldQuit != true);
            } while (isGameRunning);
        }

        private void ShowWinner(bool isWinner)
        {
            menu.ShowGameBoard();
            if (isWinner)
            {
                Console.WriteLine("Winner winner chicken dinner! Player " + menu.GetPlayer());
                PressKeyToContinue();
            }
        }

        private void PressKeyToContinue()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private bool ShouldQuit(string input)
        {
            return (input == "0");
        }
    }
}