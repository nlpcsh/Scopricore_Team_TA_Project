﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScorpicoreRush
{

    internal class Stats
    {
        public static void ShowScore(int points, int level)
        {
            Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 30;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.WindowWidth/3, 1);
            Console.WriteLine("GAME OVER!");
            Console.WriteLine(new string ('*',30));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("You have reached level: {0}",level);
            Console.SetCursorPosition(Console.WindowWidth / 4, 5);
            Console.WriteLine("You have {0} points!", points);
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 4, 7);
            Console.WriteLine("Enter your name: ");
            Console.SetCursorPosition(Console.WindowWidth / 4, 9);
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo name = Console.ReadKey();
           
            Console.Write("{0}", name.KeyChar);
            string playerName = Console.ReadLine();

            WriteStatsToFile(playerName, points);
        }

        public static void PrintScores()
        {   Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 30;
            Console.Clear();
            Console.WriteLine("   Press any key to start...");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         HIGH SCORES");
            Console.ForegroundColor = ConsoleColor.Gray;
            
            try
            {
                var streamReader = new StreamReader(@"..\..\Statistics.txt");

                using (streamReader)
                {

                    string line = streamReader.ReadLine();
                    while (line != null)
                    {

                        Console.WriteLine("{0,25}",line);
                        line = streamReader.ReadLine();

                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("FILE NOT FOUND!");
            }

        }

        private static void WriteStatsToFile(string playerName, int playerPoints)
        {
            

            var streamWriter = new StreamWriter(@"..\..\Statistics.txt", true);
            using (streamWriter)
            {
                streamWriter.WriteLine("*********************");
                streamWriter.WriteLine("> {0,-13} - {1} <", playerName, playerPoints);
                streamWriter.WriteLine("*********************");
               
            } 
        }


    }

}


