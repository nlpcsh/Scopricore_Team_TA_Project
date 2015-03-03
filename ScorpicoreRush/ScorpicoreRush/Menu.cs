namespace ScorpicoreRush
{
    using System;
    using System.IO;

    class Menu
    {
        public static string[] Options = { "Start", "HighScores", "Help" };

        public static void ShowMenu()
        {
            SetUpWindow();

            int defaultSelectedItem = 0;
            DisplayMenuOptions(defaultSelectedItem);

            int selectedItem = SelectItem(Options);
            switch (Options[selectedItem])
            {
                case "Start":
                    ScorpicoreRush.Play();
                    break;
                case "HighScores":
                    Stats.PrintScores();
                    break;
                case "Help":
                    //  Help.GameHelp();
                    break;
                default:
                    break;
            }

            Console.ReadKey();
            CleanUpWindow();
        }

        public static int SelectItem(string[] choices)
        {
            ConsoleKeyInfo cki;
            char key;
            int choice = 0;
            int numItems = choices.Length - 1;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    key = cki.KeyChar;

                    if (cki.Key == ConsoleKey.DownArrow)
                    {
                        choice++;
                        if (choice > numItems)
                        {
                            choice = 0;
                        }
                        DisplayMenuOptions(choice);
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        choice--;
                        if (choice < 0)
                        {
                            choice = numItems;
                        }
                        DisplayMenuOptions(choice);
                    }
                    else if (key == '\r') // enter 
                    {
                        return choice;
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
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine(Options[selection]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(Options[i]);
                }
            }
        }

        public static void SetUpWindow()
        {
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