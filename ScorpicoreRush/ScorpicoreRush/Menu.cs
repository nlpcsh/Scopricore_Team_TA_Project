namespace ScorpicoreRush
{

    using System;
    using System.IO;

    class Menu
    {

        public static void SetScreen()
        {

            Console.WindowWidth = 20;//  
            Console.WindowHeight = 10;// 
            Console.BufferHeight = 10;
            Console.BufferWidth = 20;
        }
        public static void ShowMenu()
        {
            SetScreen();

            Console.Clear();
            Console.CursorVisible = false;



            string[] choices = { "Start", "HighScores", "Help" };

            for (int i = 0; i < choices.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + i);
                Console.WriteLine(choices[i]);
            }

            int choice = ChooseItem(choices);



            if (choices[choice] == "Start")
            {

                return;
            }
            if (choices[choice] == "Help")
            {
                //  Help.GameHelp();

            }
            if (choices[choice] == "HighScores")
            {


                Stats.PrintScores();
               

            }


            Console.ReadKey();
            CleanUp();
        }
        public static int ChooseItem(string[] choices)
        {
            ConsoleKeyInfo cki;
            char key;
            int choice = 1;
            int numItems = choices.Length - 1;
            int i = 1;


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

        public static void CleanUp()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }


    }
}