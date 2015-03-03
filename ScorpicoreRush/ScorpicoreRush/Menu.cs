namespace ScorpicoreRush
{
    using System;
    using System.IO;

    class Menu
    {
        public static string[] Options = { "Play", "HighScores", "Help" , "Exit"};
        public static int DefaultOption = 0;

        public static void SelectOptions()
        {
            SetUpWindow();

            DisplayMenuOptions(DefaultOption);

            SelectItem(Options);

            CleanUpWindow();
        }

        public static void ExecuteSelection(int selectedItem)
        {
            switch (Options[selectedItem])
            {
                case "Play":
                    Game.Play();
                    break;
                case "HighScores":
                    Score.ShowHighScores();
                    break;
                case "Help":
                    //  Help.GameHelp();
                    break;
                case "Exit":
                    CleanUpWindow();
                    Console.WriteLine("Bye-Bye!");
                    break;
                default:
                    break;
            }
        }

        public static void SelectItem(string[] choices)
        {
            int choice = 0;
            int maxItemIndex = choices.Length - 1;
            char beep = '\u0007';

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.DownArrow:
                            choice = (choice == maxItemIndex) ? 0 : choice + 1;
                            DisplayMenuOptions(choice);
                            break;
                        case ConsoleKey.Enter:
                            ExecuteSelection(choice);
                            break;
                        case ConsoleKey.UpArrow:
                            choice = (choice == 0) ? maxItemIndex : choice - 1;
                            DisplayMenuOptions(choice);
                            break;
                        default:
                            Console.WriteLine(beep);
                            break;
                    }
                }
            }
        }

        public static void DisplayMenuOptions(int selection)
        {
            for (int i = 0; i < Options.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + i);
                if (i == selection)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(Options[selection]);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(Options[i]);
                    Console.ResetColor();
                }
            }
        }

        public static void SetUpWindow()
        {
            Console.Title = "Scorpicore Rush!";

            Console.WindowWidth = 20;
            Console.WindowHeight = 10;
            Console.BufferWidth = 20;
            Console.BufferHeight = 10;

            Console.Clear();
            Console.CursorVisible = false;
        }

        public static void CleanUpWindow()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}