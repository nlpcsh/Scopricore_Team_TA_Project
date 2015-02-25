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
        public ConsoleColor color;
    }

    class ScorpicoreRush
    {
        static int windowWidth = 100;
        static int windowHeight = 30;

        static int gamefieldWidth = 70;
        static int gameFieldHeight = 20;



        static char[] rockSymbols = { '$', '&', '#', };            // shoot $ avoid & and #, # is indestructable...  
        static string hero = "<^>";
        static int difficulty = 10;                                // % of each row covered with rocks
        static int timeToSleep = 200;   
        static int rocksPerRow = 10;         // The max number of rocks per row. Integer division
        static char[,] rocks = new char[gameFieldHeight + 1, gamefieldWidth];    // The first row keeps new rocks positions

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

            // initial rock matrix initialization
            for (int row = 0; row < gameFieldHeight; row++)
            {
                for (int col = 0; col < gamefieldWidth; col++)
                {
                    rocks[row, col] = ' ';
                }
            }

            while (true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer

                    ChangeHeroPosition(key);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Separate bullet movement from rocks falling - if possible ???
                        //Task.Run(() =>
                        //{
                        FireWeapon();
                        
                        level = points / 10;
                        int y = Hero.y - 1;
                        int x = Hero.x + 1;

                        while (y >= 0)
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key2 = Console.ReadKey(true);
                                while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer

                                ChangeHeroPosition(key2);
                            }

                            Console.Clear();
                            PrintMenu(points, playerLives, level);
                            DrawGameMenu();
                            DrawDownBorder();
                            GenerateNewRowRocks();
                            MoveAllRowsDown();
                            ClearAndRedraw();

                            Console.SetCursorPosition(x, y + 1);

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

                                Console.SetCursorPosition(0, 0);
                                Console.Write(new string(' ', gamefieldWidth - 2));
                                Console.ForegroundColor = ConsoleColor.DarkYellow;

                                y--;
                            }

                            // hit a rock !
                            if ((y > 0) && (rocks[y, x] != ' '))
                            {
                                rocks[y, x] = ' ';
                                points++;
                                break;
                            }

                            Thread.Sleep(timeToSleep);
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

                Console.Clear();
                PrintMenu(points, playerLives, level);
                DrawGameMenu();
                DrawDownBorder();
                GenerateNewRowRocks();
                MoveAllRowsDown();
                ClearAndRedraw();

                Thread.Sleep(timeToSleep);
            }
        }


        static void ChangeHeroPosition(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (Hero.x - 1 >= 0)
                {
                    Hero.x -= 1;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (Hero.x + 1 < gamefieldWidth)
                {
                    Hero.x += 1;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (Hero.y - 1 >= 0)
                {
                    Hero.y -= 1;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (Hero.y + 1 <= gameFieldHeight)
                {
                    Hero.y += 1;
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
            PrintTextInGameMenu(gamefieldWidth + 22, 4, points.ToString(), ConsoleColor.Gray);
            PrintTextInGameMenu(gamefieldWidth + 20, 6, new string(playerLives, Hero.lives), ConsoleColor.Red);  //TODO: Print this according to the logic for loosing lives 
            PrintTextInGameMenu(gamefieldWidth + 22, 8, level.ToString(), ConsoleColor.Gray);
        }

        static void DrawGameMenu()
        {
            for (int i = 0; i < gameFieldHeight + 1; i++)
            {
                PrintOnPosition(gamefieldWidth + 2, i, "||", ConsoleColor.DarkGray);
            }
            PrintTextInGameMenu(gamefieldWidth + 4, 0, "----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 6, 1, "*** SCORPICORE RUSH ***", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 4, 2, "----------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 4, "Points: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 6, "Lives: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gamefieldWidth + 11, 8, "Level: ", ConsoleColor.DarkGray);

        }

        public static void DrawDownBorder()
        {
            Console.SetCursorPosition(0, gameFieldHeight + 1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new String('-', Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
            for (int j = 0; j < gamefieldWidth; j++)  // First row contains new elements and it is not displayed
            {
                rocks[0, j] = ' ';
            }

            int randomRocksPerRow = RockRandom.Next(0, rocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)

            for (int i = 0; i < randomRocksPerRow; i++)
            {
                if (rand.Next(1, 5) == 1) // one of 5 rows have rocks
                {
                    int nextPosition = RockRandom.Next(1, gamefieldWidth); // A random position between 1 and WIDTH (inclusive)
                    int nextRockType = RockRandom.Next(0, rockSymbols.Length); // A random type of rock
                    rocks[0, nextPosition] = rockSymbols[nextRockType];
                }
            }
        }

        static void MoveAllRowsDown()
        {
            for (int i = gameFieldHeight; i > 0; i--)
            {
                for (int j = 0; j < gamefieldWidth; j++)
                {
                    rocks[i, j] = rocks[i - 1, j];  // make current to be == to the previous row
                }
            }
        }

        static void ClearAndRedraw()
        {

            ConsoleColor color = ConsoleColor.White;

            // print only the elements with rocks
            for (int i = 1; i <= gameFieldHeight; i++)
            {
                for (int j = 0; j < gamefieldWidth; j++)
                {

                    if (rocks[i, j] == rockSymbols[0])
                    {
                        color = ConsoleColor.Yellow;
                        PrintOnPosition(j, i - 1, rocks[i, j].ToString(), color);
                    }
                    else if (rocks[i, j] == rockSymbols[1])
                    {
                        color = ConsoleColor.Red;
                        PrintOnPosition(j, i - 1, rocks[i, j].ToString(), color);
                    }
                    else if (rocks[i, j] == rockSymbols[2])
                    {
                        color = ConsoleColor.Green;
                        PrintOnPosition(j, i - 1, rocks[i, j].ToString(), color);
                    }
                }
            }

            PrintOnPosition(Hero.x, Hero.y, Hero.face, ConsoleColor.White);

        }
    }
}








