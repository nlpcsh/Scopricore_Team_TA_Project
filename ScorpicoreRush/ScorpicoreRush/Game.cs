using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
//   using System.Threading.Tasks;

namespace ScorpicoreRush
{
    class Game
    {
        static int windowWidth = 100;
        static int windowHeight = 30;

        static int gameFieldWidth = 70;
        static int gameFieldHeight = 20;

        static int difficulty = 10; // % of each row covered with rocks
        static int timeToSleep = 200;
        static int maxRocksPerRow = 10; // The max number of rocks per row. Integer division

        static char[] rockSymbols = { '$', '&', '#' }; // shoot $ avoid & and #, # is indestructable...  
        static char playerLifeSymbol = '\u2665'; // A heart

        static char[,] rocks = new char[gameFieldHeight + 1, gameFieldWidth];    // The first row keeps new rocks positions 

        // To ensure the randomness :) 
        static Random Rand = new Random();
        static Random RockRandom = new Random();

        static bool isPlaying = true;

        static Scorpicore hero = new Scorpicore();

        public static void Play()
        {
            Console.Title = "Scorpicore Rush!";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = windowHeight;
            Console.BufferWidth = Console.WindowWidth = windowWidth;

            //Menu.ShowMenu();
            Console.Clear();

            hero.x = gameFieldWidth / 2;
            hero.y = gameFieldHeight;
            hero.avatar = "<^>";
            hero.level = 1;
            hero.lives = 3;
            hero.points = 0;
            hero.color = ConsoleColor.Blue;

            string bullet = "o";
            string currentWeapon = "W";

            // initial rock matrix initialization
            InitialRocksInitialization();

            while (true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer

                    HeroMovementAndGameControls(key);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Separate bullet movement from rocks falling - if possible ???
                        //Task.Run(() =>
                        //{
                        FireWeapon();
                        
                        hero.level = hero.points / 10 + 1 ;
                        int y = hero.y - 1;
                        int x = hero.x + 1;

                        isPlaying = true;

                        while ((y > 0) && isPlaying)
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key2 = Console.ReadKey(true);
                                while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer

                                HeroMovementAndGameControls(key2);
                            }

                            Console.Clear();
                            PrintMenu(hero.points, playerLifeSymbol, hero.level);
                            DrawGameMenu();
                            DrawDownBorder();
                            GenerateNewRowRocks();
                            MoveAllRowsDown();
                            ClearAndRedraw();

                            // Hero hit a rock !
                            DoesHeroHitARock();

                            // bullet hit a rock !  // TODO - need to be improved
                            if ((rocks[y, x] != ' ') || (rocks[y + 1, x] != ' '))
                            {
                                using (SoundPlayer bulletHitARockSound = new SoundPlayer(@"..\..\Pop_And_Explosion.wav"))
                                {
                                    bulletHitARockSound.Play();
                                }
                                rocks[y, x] = ' ';
                                rocks[y + 1, x] = ' '; // if the bullet jump over the rock ( from y-=2 )
                                rocks[y - 1, x] = ' ';
                                hero.points++;
                                break;
                            }

                            Console.SetCursorPosition(x, y);
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            if (currentWeapon.Equals("^") || currentWeapon.Equals("@") || currentWeapon.Equals("*") || currentWeapon.Equals("o") || currentWeapon.Equals("!") || currentWeapon.Equals("$"))
                            {
                                //Console.WriteLine(currentWeapon);
                                PrintOnPosition(x, y, currentWeapon, ConsoleColor.Cyan);
                            }

                            else
                            {
                                switch (y % 3)
                                {
                                    case 0: Console.Write("/"); break;
                                    case 1: Console.Write("-"); break;
                                    case 2: Console.Write("\\"); break;
                                    case 3: Console.Write("|"); break;
                                }

                                //y--;
                                y -= 2; // faster movement of the bullet
                            }

                            Thread.Sleep(timeToSleep);
                        }
                        //});
                        //currentWeapon = SelectWeapon();

                    }
                    
                }

                Console.Clear();
                PrintMenu(hero.points, playerLifeSymbol, hero.level);
                DrawGameMenu();
                DrawDownBorder();
                GenerateNewRowRocks();
                MoveAllRowsDown();
                ClearAndRedraw();

                // Hero hit a rock !
                DoesHeroHitARock();
                
                Thread.Sleep(timeToSleep);
            }
        }

        public static void DoesHeroHitARock()
        {
            if ((rocks[hero.y, hero.x] != ' ') || (rocks[hero.y, hero.x + 1] != ' ') || (rocks[hero.y, hero.x + 2] != ' '))
            {

                hero.lives -= 1;

                if (hero.lives == 0)
                {
                    QuitOrEndGame();
                }
                else
                {
                    RestartGame();
                }
            }
        }

        public static void QuitOrEndGame()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Score.ShowScore(hero.points, hero.level);
            Console.ResetColor();
            Menu.SelectOptions();
        }

        public static void InitialRocksInitialization()
        {
            // initial rock matrix initialization
            for (int row = 0; row < gameFieldHeight; row++)
            {
                for (int col = 0; col < gameFieldWidth; col++)
                {
                    rocks[row, col] = ' ';
                }
            }
        }

        public static void RestartGame()
        {
            using (SoundPlayer restartLevel = new SoundPlayer(@"..\..\reload.wav"))
            {
                restartLevel.Play();
            }
            isPlaying = false; // ensure breaking the bullets while cycle
            Console.Clear();
            PrintMenu(hero.points, playerLifeSymbol, hero.level);
            DrawGameMenu();
            DrawDownBorder();

            InitialRocksInitialization();

            hero.x = gameFieldWidth / 2;
            hero.y = gameFieldHeight;
        }

        public static void HeroMovementAndGameControls(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (hero.x - 1 >= 0)
                {
                    hero.x -= 1;
                }
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                if (hero.x + 1 < gameFieldWidth)
                {
                    hero.x += 1;
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (hero.y - 1 >= 0)
                {
                    hero.y -= 1;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (hero.y + 1 <= gameFieldHeight)
                {
                    hero.y += 1;
                }
            }
            else if (key.Key == ConsoleKey.Q)
            {
                QuitOrEndGame();
            }
        }

        public static void PrintOnPosition(int positionX, int positionY, string itemCharacter, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(itemCharacter);
        }

        public static void PrintTextInGameMenu(int positionX, int positionY, string menuText, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(menuText);
        }

        public static void PrintMenu(int points, char playerLives, int level)
        {
            PrintTextInGameMenu(gameFieldWidth + 22, 4, points.ToString(), ConsoleColor.Gray);
            PrintTextInGameMenu(gameFieldWidth + 20, 6, new string(playerLives, hero.lives), ConsoleColor.Red);  //TODO: Print this according to the logic for loosing lives 
            PrintTextInGameMenu(gameFieldWidth + 22, 8, level.ToString(), ConsoleColor.Gray);
        }

        public static void DrawGameMenu()
        {
            for (int i = 0; i < gameFieldHeight + 1; i++)
            {
                PrintOnPosition(gameFieldWidth + 2, i, "||", ConsoleColor.DarkGray);
            }
            PrintTextInGameMenu(gameFieldWidth + 4, 0, "--------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gameFieldWidth + 6, 1, "*** SCORPICORE RUSH ***", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gameFieldWidth + 4, 2, "--------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gameFieldWidth + 11, 4, "Points: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gameFieldWidth + 11, 6, "Lives: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(gameFieldWidth + 11, 8, "Level: ", ConsoleColor.DarkGray);
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

        public static void GenerateNewRowRocks()
        {
            for (int j = 0; j < gameFieldWidth; j++)  // First row contains new elements and it is not displayed
            {
                rocks[0, j] = ' ';
            }

            int randomRocksPerRow = RockRandom.Next(0, maxRocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)

            for (int i = 0; i < randomRocksPerRow; i++)
            {
                if (Rand.Next(1, 5) == 1) // one of 5 rows have rocks
                {
                    int nextPosition = RockRandom.Next(1, gameFieldWidth); // A random position between 1 and WIDTH (inclusive)
                    int nextRockType = RockRandom.Next(0, rockSymbols.Length); // A random type of rock
                    rocks[0, nextPosition] = rockSymbols[nextRockType];
                }
            }
        }

        public static void MoveAllRowsDown()
        {
            for (int i = gameFieldHeight; i > 0; i--)
            {
                for (int j = 0; j < gameFieldWidth; j++)
                {
                    rocks[i, j] = rocks[i - 1, j];  // make current to be == to the previous row
                }
            }
        }

        public static void ClearAndRedraw()
        {

            ConsoleColor color = ConsoleColor.White;

            // print only the elements with rocks
            for (int i = 1; i <= gameFieldHeight; i++)
            {
                for (int j = 0; j < gameFieldWidth; j++)
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

            PrintOnPosition(hero.x, hero.y, hero.avatar, hero.color);
        }
    }
}
