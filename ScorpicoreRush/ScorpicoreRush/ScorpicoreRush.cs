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
        
        static void Main()
        {
            
            Menu.ShowMenu();
            Console.Clear();
            Random rand = new Random();
            Queue<Position> HeroPosition = new Queue<Position>();
            Position HeroStartPosition = new Position(20, 20);

            HeroPosition.Enqueue(HeroStartPosition);

            string hero = "(о)(о)";

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
                Console.CursorVisible = false;
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X, position.Y);
                            Console.WriteLine(hero);
                        }
                    }

                    else if (key.Key == ConsoleKey.LeftArrow)
                    {

                        HeroPosition.Dequeue();
                        Position dwarfEnd = new Position(HeroStartPosition.X -= 1, HeroStartPosition.Y);
                        HeroPosition.Enqueue(dwarfEnd);
                        Console.Clear();
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
                        Console.Clear();
                        foreach (Position position in HeroPosition)
                        {
                            Console.SetCursorPosition(position.X + 1, position.Y);
                            Console.WriteLine(hero);
                            if (HeroStartPosition.X == Console.BufferWidth - 20)
                            {
                                HeroStartPosition.X = HeroStartPosition.X - 1;
                            }
                        }
                    }
                }

                //Thread.Sleep(20);

            }

        }
    }
}








