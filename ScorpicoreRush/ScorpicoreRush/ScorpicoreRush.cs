using System;
using System.Threading;

class ScorpicoreRush
{
    static void Main()
    {
        // Title of the game
        Console.Title = "Falling Rocks Game!";

        // Set game window size
        const int WIDTH = 80;
        const int HEIGHT = 25;

        // Check the size
        if (WIDTH <= Console.LargestWindowWidth)
        {
            Console.WindowWidth = WIDTH;
        }

        if (HEIGHT + 1 <= Console.LargestWindowHeight)
        {
            Console.WindowHeight = HEIGHT + 1;
            // For testing
            //            Console.WindowHeight = HEIGHT + 2;
        }

        // Check the initialization
        /*
                Console.WriteLine(Console.WindowWidth);
                Console.WriteLine(Console.WindowHeight);

                for (int i = 0; i < HEIGHT; i++)
                {
                    Console.WriteLine(i);
                }

                Console.ReadLine();
        */
        char[] rockSymbols = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' }; // 11 elements. Rocks are the symbols ^, @, *, &, +, %, $, #, !, ., ;, - distributed with appropriate density.

        int dwarfPosition = (WIDTH - 1) / 2;

        int difficulty = 30; // % of each row covered with rocks
        int rocksPerRow = WIDTH * difficulty / 100; // The max number of rocks per row. Integer division

        // Initializing the result
        ulong score = 0;

        // The array of rocks - declaration and initialization
        char[,] rocks = new char[HEIGHT + 1, WIDTH + 1]; // The first element of each row keeps the number of rocks contained - for easier calculation of the score

        // Initialization
        for (int i = 0; i <= HEIGHT; i++)
        {
            rocks[i, 0] = (char)0;
            for (int j = 1; j <= WIDTH; j++)
            {
                rocks[i, j] = ' ';
            }
        }

        int waitTime = 150;

        bool success = true;

        while (success)
        {
            // Clear buffer area
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow && dwarfPosition > 1)
                {
                    dwarfPosition--;
                }
                else if (key.Key == ConsoleKey.RightArrow && dwarfPosition < WIDTH)
                {
                    dwarfPosition++;
                }
                else // Do nothing
                {
                    ;
                }
            }

            // Move all rows downwards (Falling)
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j <= WIDTH; j++)
                {
                    rocks[i, j] = rocks[i + 1, j];
                }
            }

            // Generate a new row on the top

            // Initialize
            for (int j = 0; j <= WIDTH; j++)
            {
                rocks[HEIGHT, j] = ' ';
            }

            Random random = new Random();
            int randomRocksPerRow = random.Next(0, rocksPerRow); // Random number of the rocks per row between 0 and rocksPerRow (rocksPerRow not included)
            //            Console.WriteLine(randomRocksPerRow);
            //            Console.ReadLine();

            rocks[HEIGHT, 0] = (char)randomRocksPerRow;
            for (int i = 0; i < randomRocksPerRow; i++)
            {
                int nextPosition = random.Next(1, WIDTH + 1); // A random position between 1 and WIDTH (inclusive)
                int nextRockType = random.Next(0, rockSymbols.Length); // A random type of rock
                rocks[HEIGHT, nextPosition] = rockSymbols[nextRockType];
            }

            // Check the bottom row for a hit on the dwarf
            if (rocks[0, dwarfPosition] == ' ' && rocks[0, dwarfPosition + 1] == ' ' && rocks[0, dwarfPosition + 2] == ' ') // No hit
            {
                // Success. We have successfully evaded the rocks and we turn them into "!"'s
                for (int i = 1; i <= WIDTH; i++)
                {
                    if (rocks[0, i] != ' ')
                    {
                        rocks[0, i] = '!';
                    }
                }

                rocks[0, dwarfPosition] = '(';
                rocks[0, dwarfPosition + 1] = '0';
                rocks[0, dwarfPosition + 2] = ')';

                score += (ulong)rocks[0, 0];
            }
            else
            {
                success = false;
                if (rocks[0, dwarfPosition] != ' ')
                {
                    rocks[0, dwarfPosition] = 'X';
                }
                else
                {
                    rocks[0, dwarfPosition] = '(';
                }

                if (rocks[0, dwarfPosition + 1] != ' ')
                {
                    rocks[0, dwarfPosition + 1] = 'X';
                }
                else
                {
                    rocks[0, dwarfPosition + 1] = '0';
                }

                if (rocks[0, dwarfPosition + 2] != ' ')
                {
                    rocks[0, dwarfPosition + 2] = 'X';
                }
                else
                {
                    rocks[0, dwarfPosition + 2] = ')';
                }
            }

            // Print the new status
            Console.Clear();

            for (int i = HEIGHT; i >= 0; i--)
            {
                for (int j = 1; j <= WIDTH; j++)
                {
                    Console.Write(rocks[i, j]);
                }
                if (i > 0)
                {
                    //                   Console.WriteLine();
                }
            }

            // Console.ReadLine();
            Thread.Sleep(waitTime);
        }

        Console.Write("You scored: {0}! ", score);
        // Clear buffer area to observe score
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey();
        }
    }
}