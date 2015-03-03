﻿namespace ScorpicoreRush
{
    using System;
    using System.IO;

    class Menu
    {
        public static string[] Choices = { "Start", "HighScores", "Help" };

        public static void DisplayChoices(int choice)
        {
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + i);
                if (i == choice)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine(Choices[choice]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(Choices[i]);
                }
            }
        }

        public static void ShowMenu()
        {
            SetUpWindow();

            int defaultChoice = 0;

            DisplayChoices(defaultChoice);

            int choice = ChooseItem(Choices);

            if (Choices[choice] == "Start")
            {
                ScorpicoreRush.Play();
            }
            if (Choices[choice] == "Help")
            {
                //  Help.GameHelp();
            }
            if (Choices[choice] == "HighScores")
            {
                Stats.PrintScores();
            }

            Console.ReadKey();
            CleanUpWindow();
        }

        public static int ChooseItem(string[] choices)
        {
            ConsoleKeyInfo cki;
            char key;
            int choice = 0;
            int numItems = choices.Length - 1;
            int i = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    key = cki.KeyChar;

                    if (cki.Key == ConsoleKey.DownArrow)
                    {
                        if (choice >= 0 && choice <= numItems)
                        {
                            i++;
                            choice++;
                        }
                        else if (choice > numItems)
                        {
                            choice = 0;
                            i = 0;
                        }
                        else if (choice < 0)
                        {
                            choice = numItems;
                            i = numItems;
                        }
                        if (choice == 0)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[1]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[2]);
                            Console.ResetColor();
                        }
                        if (choice == 1)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[0]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[2]);
                            Console.ResetColor();
                        }
                        if (choice == 2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[0]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[1]);
                            Console.ResetColor();
                        }
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        if (choice >= 0 && choice <= numItems)
                        {
                            i--;
                            choice--;
                        }
                        else if (choice > numItems)
                        {
                            choice = 0;
                            i = 0;
                        }
                        else if (choice < 0)
                        {
                            choice = numItems;
                            i = numItems;
                        }
                        if (choice == 0)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[1]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[2]);
                            Console.ResetColor();
                        }
                        if (choice == 1)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[0]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[2]);
                            Console.ResetColor();
                        }
                        if (choice == 2)
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.WriteLine(choices[choice]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[0]);
                            Console.ResetColor();

                            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 1);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine(choices[1]);
                            Console.ResetColor();
                        }
                    }
                    else if (key == '\r') // enter 
                    {
                        return choice;
                    }
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