namespace ScorpicoreRush
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        static char[] rockSymbols = { '@', '&', '#', };            // shoot @ avoid & and #, # is indestructable...  
        static int difficulty = 15;                                // % of each row covered with rocks
        static int rocksPerRow = (Console.WindowWidth - 16) * difficulty / 100;         // The max number of rocks per row. Integer division
        static char[,] rocks = new char[Console.WindowHeight + 1, (Console.WindowWidth - 15)];    // The first element of each row keeps the number of rocks contained - for easier calculation of the score
      
        
        static string hero = "<^>";
        static string bullet = "o";

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

            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;





            //moving the Hero

            Console.ForegroundColor = ConsoleColor.DarkYellow;      //<---------Eventually change color with levels
            Console.SetCursorPosition(20, 20);
            Console.WriteLine(hero);

            //TODO: Implement movements --  Move Up and Down 

            while (true)
            {
               for (int i = 0; i < Console.WindowHeight; i++)
               {
                   Console.SetCursorPosition(Console.BufferWidth - 16, i);
                   Console.Write("||");
               }

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
                        Console.Write(new string(' ', Console.WindowWidth));
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
                        Console.Write(new string(' ', Console.WindowWidth));

                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X + 1, position.Y);
                            Console.WriteLine(hero);

                            if (HeroStartPosition.X == Console.BufferWidth - 20)
                            {
                                HeroStartPosition.X = HeroStartPosition.X - 1;
                            }

                        }
                        points++;
                    }

                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        Position bulletStartPosition = new Position();
                        bulletPosition.Enqueue(bulletStartPosition);
                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X, position.Y - 1);
                            Console.WriteLine(bullet);
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
             //   Thread.Sleep(200);

             
              //  GenerateNewRowRocks();
              //  MoveAllRowsDown();
              //  ClearAndRedraw();

            }

        }
        static void GenerateNewRowRocks()
        {
            for (int j = 0; j <= Console.WindowWidth - 16; j++)
            {
                rocks[Console.WindowHeight, j] = ' ';
         
            }
         
            Random random = new Random();
            int randomRocksPerRow = random.Next(0, rocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)

            rocks[Console.WindowHeight, 0] = (char)randomRocksPerRow;

            for (int i = 0; i < randomRocksPerRow; i++)
            {
                 int nextPosition = random.Next(0, Console.WindowWidth - 16); // A random position between 1 and WIDTH (inclusive)
                 int nextRockType = random.Next(0, rockSymbols.Length); // A random type of rock
                 rocks[Console.WindowHeight, nextPosition] = rockSymbols[nextRockType];
            }
        }
        static void MoveAllRowsDown()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j <= Console.WindowWidth-16; j++)
                {
                    rocks[i, j] = rocks[i + 1, j];
                }
            }
        }
        static void ClearAndRedraw()
        {
            Console.Clear();
            for (int i = Console.WindowHeight; i >= 0; i--)
            {
                for (int j = 1; j <= Console.WindowWidth - 16; j++)
                {
                    Console.Write(rocks[i, j]);
                }
            }
        }
    }
}








