namespace ScorpicoreRush
{

    using System;
    using System.IO;

    class Menu
    {

        public static void SetScreen()
        {
            Console.WindowHeight = 30;
            Console.BufferHeight = 30;
            Console.WindowWidth = 80;
            Console.BufferWidth = 80;
            Console.BackgroundColor = ConsoleColor.Yellow;
        }
        public static void ShowMenu()
        {

            Console.Clear();
            Console.CursorVisible = false;
            //  Console.ForegroundColor = ConsoleColor.Yellow;


            string[] choices = { "Start", "HighScores", "Help" };
            for (int i = 0; i < choices.Length; i++)
            {
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
                //  TODO: Show HighScores 

            }


            Console.ReadKey();
            CleanUp();
        }
        public static int ChooseItem(string[] choices)
        {
            ConsoleKeyInfo cki;
            char key;
            int choice = 0;
            int numItems = choices.Length-1;
            int i = 0;
            //Console.SetCursorPosition(0, i);
            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.WriteLine(choices[choice]);
            //Console.ResetColor();
           
            while (true)
            { 
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') // enter 
                {
                    return choice;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {

                    if (choice >= 0 && choice <=numItems)
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
                    if (choice==0)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[1]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 2);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[2]);
                        Console.ResetColor();
                    }
                    if (choice == 1)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[0]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 2);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[2]);
                        Console.ResetColor();
                    }
                    if (choice == 2)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[0]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[1]);
                        Console.ResetColor();


                    }

                   
                    
                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                   
                    if (choice >= 0&&choice<=numItems)
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
                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[1]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 2);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[2]);
                        Console.ResetColor();
                    }
                    if (choice == 1)
                    {
                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[0]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 2);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[2]);
                        Console.ResetColor();
                    }
                    if (choice == 2)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.WriteLine(choices[choice]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 0);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[0]);
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(choices[1]);
                        Console.ResetColor();


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