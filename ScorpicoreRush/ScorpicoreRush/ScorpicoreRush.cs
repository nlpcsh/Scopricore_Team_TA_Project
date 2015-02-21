namespace ScorpicoreRush
{
<<<<<<< HEAD
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Media;
    using System.Text;
    using System.Threading;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
>>>>>>> origin/master

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
<<<<<<< HEAD
=======
        static int playerLives = 10;
        static int gameLevel = 1;
        static int points = 0;

        static string hero = "<^>";
        static char[] rockSymbols = { '$', '&', '#', };            // shoot $ avoid & and #, # is indestructable...  
        static int difficulty = 10;                                // % of each row covered with rocks
        static int rocksPerRow = gamefieldWidth * difficulty / 100;         // The max number of rocks per row. Integer division
        static char[,] rocks = new char[windowHeight, gamefieldWidth];    // The first element of each row keeps the number of rocks contained - for easier calculation of the score
>>>>>>> origin/master


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
            for (int i = 0; i < Console.WindowHeight - 9; i++)
            {
                PrintOnPosition(gamefieldWidth, i, "||", ConsoleColor.DarkGray);
            }
            PrintTextInGameMenu(gamefieldWidth + 2, 0, "----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 4, 1, "*** SCORPICORE RUSH ***", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 2, 2, "----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 4, "Points: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 6, "Lives: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 8, "Level: ", ConsoleColor.DarkGray);

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
            Console.Title = "Scorpiocore Rush!";
            int points = 0;
            int level = 0;
            char playerLives = '\u2665';
            Menu.ShowMenu();
            Console.Clear();

            Random rand = new Random();
            Queue<Position> HeroPosition = new Queue<Position>();
            Position HeroStartPosition = new Position(20, 20);
            HeroPosition.Enqueue(HeroStartPosition);

            Queue<Position> bulletPosition = new Queue<Position>();
<<<<<<< HEAD



            string hero = "<^>";
            string bullet = "o";
=======
>>>>>>> origin/master

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
<<<<<<< HEAD



=======
>>>>>>> origin/master

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
                PrintTextInGameMenu(gamefieldWidth + 18, 6, new string(playerLives, 3), ConsoleColor.Red);  //TODO: Print this according to the logic for loosing lives 
                PrintTextInGameMenu(gamefieldWidth + 18, 8, level.ToString(), ConsoleColor.Gray);
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
                        Console.SetCursorPosition(0, 20);
                        Console.Write(new string(' ', Console.WindowWidth - 30));
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
                        Console.Write(new string(' ', Console.WindowWidth - 32));

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
                        level = points / 10;
                        int y = HeroStartPosition.Y - 2;
                        int x = HeroStartPosition.X;
                        char currentWeapon = SelectWeapon();
                        while (y >= 0)
                        {
                            Thread.Sleep(20);
                            Console.SetCursorPosition(x, y + 1);
                            Console.WriteLine("  ");
                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            if (currentWeapon == '^' || currentWeapon == '@' || currentWeapon == '*' || currentWeapon == 'o' || currentWeapon == '!' || currentWeapon == '$')
                            {
                                Console.WriteLine(currentWeapon);
                            }

                            else
                            {
                                switch (y % 2)
                                {
                                    case 0: Console.Write("/"); break;
                                    case 1: Console.Write("-"); break;
                                    case 2: Console.Write("\\"); break;
                                    case 3: Console.Write("|"); break;
                                }
                            }
                            Console.SetCursorPosition(0, 0);
                            Console.Write(new string(' ', Console.WindowWidth - 32));
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            y--;

                        }

                    }




                    //TODO: When the game is finished logic
                    if (key.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Stats.ShowScore(points, level);
                        Console.ResetColor();
                        Menu.ShowMenu();

                    }
                }

<<<<<<< HEAD
                // Thread.Sleep(20);

=======
           //   Thread.Sleep(200);

             //     GenerateNewRowRocks();
             //     MoveAllRowsDown();
             //     ClearAndRedraw();
>>>>>>> origin/master


            }

        }
<<<<<<< HEAD
        public static char SelectWeapon()
        {
            char currentWeapon = '0';
            char[] weapons = { '^', '@', '*', 'o', '!', '$' };
            ConsoleKeyInfo key = Console.ReadKey(true);
            ConsoleKeyInfo firstKey = new ConsoleKeyInfo('0', ConsoleKey.D0, false, true, false);
            List<ConsoleKeyInfo> keyList = new List<ConsoleKeyInfo>();
            keyList.Add(firstKey);

            switch (key.Key)
            {

                case ConsoleKey.D1: keyList.Add(key);
                    break;
                case ConsoleKey.D2: keyList.Add(key);
                    break;
                case ConsoleKey.D3: keyList.Add(key);
                    break;
                case ConsoleKey.D4: keyList.Add(key);
                    break;
                case ConsoleKey.D5: keyList.Add(key);
                    break;
                case ConsoleKey.D6: keyList.Add(key);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (keyList.Last().KeyChar.ToString() == i.ToString())
                {
                    currentWeapon = weapons[i];
                }
            }
            return currentWeapon;

=======
        static void GenerateNewRowRocks()
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j <= gamefieldWidth - 1; j++)
            {
                rocks[windowHeight - 10, j] = ' ';
         
            }
         
           Random RockRandom = new Random();
           int randomRocksPerRow = RockRandom.Next(0, rocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)
         
           rocks[windowHeight - 10, 0] = (char)randomRocksPerRow;
         
           for (int i = 0; i < randomRocksPerRow; i++)
           {
               int nextPosition = RockRandom.Next(1, gamefieldWidth - 1); // A random position between 1 and WIDTH (inclusive)
               int nextRockType = RockRandom.Next(0, rockSymbols.Length); // A random type of rock
               rocks[windowHeight - 10, nextPosition] = rockSymbols[nextRockType];
           }
        }

        static void MoveAllRowsDown()
        {
            for (int i = 0; i < windowHeight - 10; i++)
            {
                for (int j = 0; j <= gamefieldWidth - 1; j++)
                {
                    rocks[i, j] = rocks[i + 1, j];
                }
            }
        }
        static void ClearAndRedraw()
        {
         //  Console.Clear();
           DrawGameMenu();
           DrawDownBorder();
           Console.SetCursorPosition(0, 0);
           for (int i = windowHeight - 10; i >= 0; i--)
           {
               for (int j = 1; j <= gamefieldWidth - 1; j++)
               {
                   Console.Write(rocks[i, j]);
               }
           }
>>>>>>> origin/master
        }
    }
}








