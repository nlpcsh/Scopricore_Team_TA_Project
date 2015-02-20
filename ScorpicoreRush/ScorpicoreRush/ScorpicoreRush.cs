namespace ScorpicoreRush
{
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

    struct Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class ScorpicoreRush
    {
        static int gamefieldWidth = 70;
        static int windowWidth = 100;
        static int windowHeight = 30;
        static int playerLives = 10;
        static int gameLevel = 1;
        static int points = 0;
        static void PrintOnPosition(int positionX, int positionY, string itemCharacter, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(itemCharacter);
        }
        static void PrintTextInGameMenu(int positionX, int positionY, string menuText, ConsoleColor color)
        {
           Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(menuText);

        }
        static void DrawGameMenu()
        {
            for (int i = 0; i < Console.WindowHeight-9; i++)
            {
                PrintOnPosition(gamefieldWidth, i, "||", ConsoleColor.DarkGray);
            }
            PrintTextInGameMenu(gamefieldWidth + 2, 0, "-----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 4, 1, "*** SCORPICORE RUSH ***", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 2, 2, "-----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 4, "Points: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 6, "Lives: " + playerLives, ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 8, "Level: " + gameLevel, ConsoleColor.DarkGray);

        }
        public static void DrawDownBorder()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight - 9);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("-");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            }
        }
        public static void FireWeapon()
        {
            using (SoundPlayer fireWeapon = new SoundPlayer(@"..\..\FireWeapon.wav"))
            {
                fireWeapon.Play();
               //Thread.Sleep(50);
            }
        }
        static void Main()
        {
            int points = 0;
            Menu.ShowMenu();
            Console.Clear();

            Random rand = new Random();
            Queue<Position> HeroPosition = new Queue<Position>();
            Position HeroStartPosition = new Position(20, 20);
            HeroPosition.Enqueue(HeroStartPosition);

            Queue<Position> bulletPosition = new Queue<Position>();
           


            string hero = "<^>";
            string bullet = "o";

            Console.CursorVisible = false;
            for (int i = 20; i <= windowHeight; i++)
            {
                Console.BufferHeight = Console.WindowHeight = i;
                //Thread.Sleep(20);
            }
            for (int i = 20; i <= windowWidth; i++)
            {
                Console.BufferWidth = Console.WindowWidth = i;
                //Thread.Sleep(20);
            }
            
            


            DrawGameMenu();
            DrawDownBorder();
            
            //moving the Hero

            Console.ForegroundColor = ConsoleColor.DarkYellow;      //<---------Eventually change color with levels
            Console.SetCursorPosition(20, 20);
            Console.WriteLine(hero);

            //TODO: Implement movements --  Move Up and Down 

            while (true)
            {

                PrintTextInGameMenu(gamefieldWidth + 18, 4, points.ToString(), ConsoleColor.Gray);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    

                    if (key.Key == ConsoleKey.LeftArrow)
                    {

                        HeroPosition.Dequeue();
                        Position dwarfEnd = new Position(HeroStartPosition.X -= 1, HeroStartPosition.Y);
                        HeroPosition.Enqueue(dwarfEnd);
                        Console.SetCursorPosition(0,20);
                        Console.Write(new string(' ', Console.WindowWidth-30)); 
                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X - 1, position.Y);
                            Console.WriteLine(hero);

                            if (HeroStartPosition.X == 1)
                            {
                                HeroStartPosition.X = HeroStartPosition.X + 1;
                            }
                        }
                    }

                    else if (key.Key == ConsoleKey.RightArrow)
                    {
                        HeroPosition.Dequeue();
                        Position dwarfEnd = new Position(HeroStartPosition.X += 1, HeroStartPosition.Y);
                        HeroPosition.Enqueue(dwarfEnd);
                        Console.SetCursorPosition(0, 20);
                        Console.Write(new string(' ', Console.WindowWidth-32)); 
                       
                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X + 1, position.Y);
                            Console.WriteLine(hero);

                            if (HeroStartPosition.X == Console.BufferWidth - 34)
                            {
                                HeroStartPosition.X = HeroStartPosition.X - 1;
                            }

                        }
                        
                    }
                        
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        FireWeapon();
                        points++;
                            int y = HeroStartPosition.Y-2;
                            int x = HeroStartPosition.X;

                            while (y>=0)
                            {
                                 Thread.Sleep(50);
                                 Console.SetCursorPosition(x,y+1);
                                 Console.WriteLine("  ");
                                 Console.SetCursorPosition(x,y);
                                 Console.ForegroundColor = ConsoleColor.Cyan;
                                 switch (y % 2)
                                 {
                                    case 0: Console.Write("/"); break;
                                    case 1: Console.Write("-"); break;
                                    case 2: Console.Write("\\"); break;
                                    case 3: Console.Write("|"); break;
                                 }
                                 Console.SetCursorPosition(0, 0);
                                 Console.Write(new string(' ', Console.WindowWidth-32));
                                 Console.ForegroundColor = ConsoleColor.DarkYellow;
                                 y--;
                            
                            }
                            
                        
                    }
                   
                    
                    //TODO: When the game is finished logic
                    if (key.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Stats.ShowScore(points);
                        Console.ResetColor();
                        Menu.ShowMenu();
                    }
                }

             // Thread.Sleep(20);
                


            }

        }
    }
}








