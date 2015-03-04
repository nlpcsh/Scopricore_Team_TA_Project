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
        static int WindowWidth = 100;
        static int WindowHeight = 30;

        static int GameFieldWidth = 70;
        static int GameFieldHeight = 20;

        static char[] RockSymbols = { '$', '&', '#' }; // shoot $ avoid & and #, # is indestructable...  
        static char PlayerLifeSymbol = '\u2665'; // A heart

        // To ensure the randomness :) 
        static Random Rand = new Random();
        static Random RockRandom = new Random();

        static int TimeToSleep = 200;

        static int difficulty = 10; // % of each row covered with rocks
        static int maxRocksPerRow = 10; // The max number of rocks per row. Integer division

        static char[,] rocksMatrix = new char[GameFieldHeight + 1, GameFieldWidth]; // The first row keeps new rocks positions

        static bool isPlaying = true;

        static Scorpicore hero = new Scorpicore();

        public static void SetUpWindow()
        {
            Console.Title = "Scorpicore Rush!";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.BufferWidth = Console.WindowWidth = WindowWidth;

            //Menu.ShowMenu();
            Console.Clear();
        }

        public static void InitializeHero()
        {
            hero.x = GameFieldWidth / 2;
            hero.y = GameFieldHeight;
            hero.avatar = "<^>";
            hero.level = 1;
            hero.lives = 3;
            hero.points = 0;
            hero.color = ConsoleColor.Blue;
            hero.currentWeapon = "W";
        }

        public static void Play()
        {
            SetUpWindow();
            InitializeHero();
            InitializeRocksMatrix();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer
                    MoveHeroAndControlGameOnKeyStroke(key);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Separate bullet movement from rocks falling - if possible ???
                        //Task.Run(() =>
                        //{
                        FireWeapon();

                        hero.level = hero.points / 10 + 1 ;
                        int bulletY = hero.y - 1;
                        int bulletX = hero.x + 1;

                        isPlaying = true;

                        while (isPlaying && (bulletY > 0))
                        {
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo key2 = Console.ReadKey(true);
                                while (Console.KeyAvailable) Console.ReadKey(true); // clears the ReadKey buffer

                                MoveHeroAndControlGameOnKeyStroke(key2);
                            }

                            ReDraw();

                            // Hero hit a rock !
                            CheckIfHeroHitByARock();

                            // bullet hit a rock !  // TODO - need to be improved
                            if ((rocksMatrix[bulletY, bulletX] != ' ') || (rocksMatrix[bulletY + 1, bulletX] != ' '))
                            {
                                using (SoundPlayer bulletHitARockSound = new SoundPlayer(@"..\..\Pop_And_Explosion.wav"))
                                {
                                    bulletHitARockSound.Play();
                                }
                                rocksMatrix[bulletY, bulletX] = ' ';
                                rocksMatrix[bulletY + 1, bulletX] = ' '; // if the bullet jump over the rock ( from y-=2 )
                                rocksMatrix[bulletY - 1, bulletX] = ' ';
                                hero.points++;
                                break;
                            }

                            Console.SetCursorPosition(bulletX, bulletY);
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            if (hero.currentWeapon.Equals("^") || hero.currentWeapon.Equals("@") || hero.currentWeapon.Equals("*") ||
                                hero.currentWeapon.Equals("o") || hero.currentWeapon.Equals("!") || hero.currentWeapon.Equals("$"))
                            {
                                //Console.WriteLine(currentWeapon);
                                PrintOnPosition(bulletX, bulletY, hero.currentWeapon, ConsoleColor.Cyan);
                            }
                            else
                            {
                                switch (bulletY % 3)
                                {
                                    case 0: Console.Write("/"); break;
                                    case 1: Console.Write("-"); break;
                                    case 2: Console.Write("\\"); break;
                                    case 3: Console.Write("|"); break;
                                }

                                //y--;
                                bulletY -= 2; // faster movement of the bullet
                            }

                            Thread.Sleep(TimeToSleep);
                        }
                        //});
                        //currentWeapon = SelectWeapon();
                    }
                }

                ReDraw();

                // Is hero hit by a rock?
                CheckIfHeroHitByARock();

                Thread.Sleep(TimeToSleep);
            }
        }

        public static void ReDraw()
        {
            Console.Clear();
            PrintMenu(hero.points, PlayerLifeSymbol, hero.level);
            DrawGameMenu();
            DrawLowerBorder();
            GenerateNewRowRocks();
            MoveAllRowsDown();
            ClearAndRedraw();
        }

        public static void CheckIfHeroHitByARock()
        {
            if ((rocksMatrix[hero.y, hero.x] != ' ') || (rocksMatrix[hero.y, hero.x + 1] != ' ') || (rocksMatrix[hero.y, hero.x + 2] != ' '))
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

        public static void InitializeRocksMatrix()
        {
            // initial rock matrix initialization
            for (int row = 0; row < GameFieldHeight; row++)
            {
                for (int col = 0; col < GameFieldWidth; col++)
                {
                    rocksMatrix[row, col] = ' ';
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
            PrintMenu(hero.points, PlayerLifeSymbol, hero.level);
            DrawGameMenu();
            DrawLowerBorder();

            InitializeRocksMatrix();

            hero.x = GameFieldWidth / 2;
            hero.y = GameFieldHeight;
        }

        public static void MoveHeroAndControlGameOnKeyStroke(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    hero.x = (hero.x > 0) ? hero.x - 1 : hero.x;
                    break;
                case ConsoleKey.RightArrow:
                    hero.x = (hero.x + 1 < GameFieldWidth) ? hero.x + 1 : hero.x;
                    break;
                case ConsoleKey.UpArrow:
                    hero.y = (hero.y > 0) ? hero.y - 1 : hero.y;
                    break;
                case ConsoleKey.DownArrow:
                    hero.y = (hero.y + 1 <= GameFieldHeight) ? hero.y + 1 : hero.y;
                    break;
                case ConsoleKey.Q:
                    QuitOrEndGame();
                    break;
                default:
                    // Do nothing, Ignore
                    break;
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
            PrintTextInGameMenu(GameFieldWidth + 22, 4, points.ToString(), ConsoleColor.Gray);
            PrintTextInGameMenu(GameFieldWidth + 20, 6, new string(playerLives, hero.lives), ConsoleColor.Red);  //TODO: Print this according to the logic for losing lives 
            PrintTextInGameMenu(GameFieldWidth + 22, 8, level.ToString(), ConsoleColor.Gray);
        }

        public static void DrawGameMenu()
        {
            for (int i = 0; i < GameFieldHeight + 1; i++)
            {
                PrintOnPosition(GameFieldWidth + 2, i, "||", ConsoleColor.DarkGray);
            }
            PrintTextInGameMenu(GameFieldWidth + 4, 0, "--------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(GameFieldWidth + 6, 1, "*** SCORPICORE RUSH ***", ConsoleColor.DarkGray);
            PrintTextInGameMenu(GameFieldWidth + 4, 2, "--------------------------", ConsoleColor.DarkGray);
            PrintTextInGameMenu(GameFieldWidth + 11, 4, "Points: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(GameFieldWidth + 11, 6, "Lives: ", ConsoleColor.DarkGray);
            PrintTextInGameMenu(GameFieldWidth + 11, 8, "Level: ", ConsoleColor.DarkGray);
        }

        public static void DrawLowerBorder()
        {
            Console.SetCursorPosition(0, GameFieldHeight + 1);
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
            for (int j = 0; j < GameFieldWidth; j++)  // First row contains new elements and it is not displayed
            {
                rocksMatrix[0, j] = ' ';
            }

            int randomRocksPerRow = RockRandom.Next(0, maxRocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)

            for (int i = 0; i < randomRocksPerRow; i++)
            {
                if (Rand.Next(1, 5) == 1) // one of 5 rows have rocks
                {
                    int nextPosition = RockRandom.Next(1, GameFieldWidth); // A random position between 1 and WIDTH (inclusive)
                    int nextRockType = RockRandom.Next(0, RockSymbols.Length); // A random type of rock
                    rocksMatrix[0, nextPosition] = RockSymbols[nextRockType];
                }
            }
        }

        public static void MoveAllRowsDown()
        {
            for (int i = GameFieldHeight; i > 0; i--)
            {
                for (int j = 0; j < GameFieldWidth; j++)
                {
                    rocksMatrix[i, j] = rocksMatrix[i - 1, j];  // make current to be == to the previous row
                }
            }
        }

        public static void ClearAndRedraw()
        {
            ConsoleColor color = ConsoleColor.White;

            // print only the elements with rocks
            for (int i = 1; i <= GameFieldHeight; i++)
            {
                for (int j = 0; j < GameFieldWidth; j++)
                {
                    if (rocksMatrix[i, j] == RockSymbols[0])
                    {
                        color = ConsoleColor.Yellow;
                        PrintOnPosition(j, i - 1, rocksMatrix[i, j].ToString(), color);
                    }
                    else if (rocksMatrix[i, j] == RockSymbols[1])
                    {
                        color = ConsoleColor.Red;
                        PrintOnPosition(j, i - 1, rocksMatrix[i, j].ToString(), color);
                    }
                    else if (rocksMatrix[i, j] == RockSymbols[2])
                    {
                        color = ConsoleColor.Green;
                        PrintOnPosition(j, i - 1, rocksMatrix[i, j].ToString(), color);
                    }
                }
            }

            PrintOnPosition(hero.x, hero.y, hero.avatar, hero.color);
        }
    }
}
