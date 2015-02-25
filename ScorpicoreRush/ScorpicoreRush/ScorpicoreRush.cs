namespace ScorpicoreRush
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Media;
    using System.Text;
    using System.Threading;
    //   using System.Threading.Tasks;

    struct Object
    {
        public int x;
        public int y;
        public string face;
        public int lives;
        //public ConsoleColor color;
    }

    class ScorpicoreRush
    {
        static int gamefieldWidth = 70;
        static int windowWidth = 100;
        static int windowHeight = 30;
        static int gameFieldHeight = 20;


        static char[] rockSymbols = { '$', '&', '#', };            // shoot $ avoid & and #, # is indestructable...  
        static string hero = "<^>";
        static int difficulty = 10;                                // % of each row covered with rocks
        static int rocksPerRow = 10;         // The max number of rocks per row. Integer division
        static char[,] rocks = new char[25, gamefieldWidth];    // The first element of each row keeps the number of rocks contained - for easier calculation of the score

        // To ensure the randomness :) 
        static Random rand = new Random();
        static Random RockRandom = new Random();

        static Object Hero = new Object();

        static void Main()
        {
            Menu.ShowMenu();
            Play();
        }

        public static void Play()
        {
            Console.Title = "Scorpiocore Rush!";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = windowHeight;
            Console.BufferWidth = Console.WindowWidth = windowWidth;
            int points = 0;
            int level = 0;
            char playerLives = '\u2665';
            //Menu.ShowMenu();
            Console.Clear();

            Hero.x = gamefieldWidth / 2;
            Hero.y = gameFieldHeight;
            Hero.face = "<^>";
            Hero.lives = 3;

            string bullet = "o";
            string currentWeapon = "W";

            DrawGameMenu();
            DrawDownBorder();

            Console.ForegroundColor = ConsoleColor.DarkYellow;      //<---------Eventually change color with levels
            //Console.SetCursorPosition(20, 20);
            //Console.WriteLine(hero);

            while (true)
            {

                PrintMenu(points, playerLives, level);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeHeroPosition(key);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Separate bullet movement from rocks falling - if possible ???
                        //Task.Run(() =>
                        //{
                        FireWeapon();
                        points++;
                        level = points / 10;
                        int y = Hero.y - 2;
                        int x = Hero.x + 1;

                        while (y >= 0)
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key2 = Console.ReadKey(true);

                                ChangeHeroPosition(key2);
                            }

                            //DrawGameMenu();
                            //DrawDownBorder();
                            GenerateNewRowRocks();
                            MoveAllRowsDown();
                            ClearAndRedraw();

                            Console.SetCursorPosition(x, y + 1);
                            //Console.WriteLine("  ");                        // need to be changed in the matrix of rocks: rocks[y, x] = ' ';

                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            
                            if (currentWeapon.Equals("^") || currentWeapon.Equals("@") || currentWeapon.Equals("*") || currentWeapon.Equals("o") || currentWeapon.Equals("!") || currentWeapon.Equals("$"))
                            {
                                //Console.WriteLine(currentWeapon);
                                PrintOnPosition(x, y, currentWeapon, ConsoleColor.Cyan);
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
                                //    }
                                Console.SetCursorPosition(0, 0);
                                Console.Write(new string(' ', gamefieldWidth - 2));
                                Console.ForegroundColor = ConsoleColor.DarkYellow;

                                y--;

                            }

                        }
                        //});
                        //currentWeapon = SelectWeapon();




                        //TODO: When the game is finished logic

                    }
                    else if (key.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Stats.ShowScore(points, level);
                        Console.ResetColor();
                        Menu.ShowMenu();

                    }
                }

                DrawGameMenu();
                //DrawDownBorder();
                GenerateNewRowRocks();
                MoveAllRowsDown();
                ClearAndRedraw();

                Thread.Sleep(100);
            }
        }


        static void ChangeHeroPosition(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow)
            {
                Hero.x -= 1;
                if (Hero.x == 1)
                {
                    Hero.x += 1;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                 Hero.x += 1;
                if (Hero.x == gamefieldWidth - 1)
                {
                    Hero.x -= 1;
                }

            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                Hero.y -= 1;

                if (Hero.y == 0)
                {
                    Hero.y = Hero.y + 1;
                }

            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                Hero.y += 1;

                if (Hero.y == gameFieldHeight - 1)
                {
                    Hero.y = Hero.y - 1;
                }

            }
        }

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

        static void PrintMenu(int points, char playerLives, int level)
        {
            PrintTextInGameMenu(gamefieldWidth + 18, 4, points.ToString(), ConsoleColor.Gray);
            PrintTextInGameMenu(gamefieldWidth + 18, 6, new string(playerLives, Hero.lives), ConsoleColor.Red);  //TODO: Print this according to the logic for loosing lives 
            PrintTextInGameMenu(gamefieldWidth + 18, 8, level.ToString(), ConsoleColor.Gray);
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
            //for (int i = 0; i < Console.WindowWidth; i++)
            //{
            //Console.SetCursorPosition(i, Console.WindowHeight - 9);
            Console.SetCursorPosition(0, Console.WindowHeight - 9);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new String('-', Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            //}
        }

        public static void FireWeapon()
        {
            using (SoundPlayer fireWeapon = new SoundPlayer(@"..\..\FireWeapon.wav"))
            {
                fireWeapon.Play();
                //Thread.Sleep(50);
            }
        }

        public static string SelectWeapon()
        {
            string currentWeapon = "0";
            string[] weapons = { "^", "@", "*", "o", "!", "$" };
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

        }

        static void GenerateNewRowRocks()
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < gamefieldWidth; j++)
            {
                rocks[gameFieldHeight, j] = ' ';

            }


            int randomRocksPerRow = RockRandom.Next(0, rocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)

            rocks[gameFieldHeight, 0] = (char)randomRocksPerRow;

            for (int i = 0; i < randomRocksPerRow; i++)
            {
                if (rand.Next(1, 5) == 1) // one of 5 rows have rocks
                {
                    int nextPosition = RockRandom.Next(1, gamefieldWidth); // A random position between 1 and WIDTH (inclusive)
                    int nextRockType = RockRandom.Next(0, rockSymbols.Length); // A random type of rock
                    rocks[gameFieldHeight, nextPosition] = rockSymbols[nextRockType];
                }
            }
        }

        static void MoveAllRowsDown()
        {
            for (int i = 0; i <= gameFieldHeight; i++)
            {
                for (int j = 0; j < gamefieldWidth; j++)
                {
                    rocks[i, j] = rocks[i + 1, j];
                }
            }
        }

        static void ClearAndRedraw()
        {

            //DrawGameMenu();
            DrawDownBorder();
            Console.SetCursorPosition(0, 0);

            if ((rocks[Hero.y, Hero.x] != ' ') || (rocks[Hero.y, Hero.x + 1] != ' ') || (rocks[Hero.y, Hero.x + 2] != ' '))
            {
                Console.WriteLine(" I'M HIT !");  // TODO:  Implement game over !!!
            }

            for (int i = gameFieldHeight; i >= 0; i--)
            {
                for (int j = 1; j < gamefieldWidth; j++)
                {
                    Console.Write(rocks[i, j]);  // TODO: the whole matrix should be printed as a single string to speed up game performance
                }
                Console.WriteLine();
            }

            PrintOnPosition(Hero.x, Hero.y, Hero.face, ConsoleColor.White);

        }
    }
}








