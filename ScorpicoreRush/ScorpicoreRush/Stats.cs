using System;
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
        public static void ShowScore(int points)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("GAME OVER!");
            Console.WriteLine("You scored {0} points", points);
            Console.Write("Enter your name: ");

            string playerName = Console.ReadLine();
            WriteStatsToFile(playerName, points);
        }

        public static void PrintScores()
        {
            try
            {
                var streamReader = new StreamReader(@"..\..\Statistics.txt");

                using (streamReader)
                {

                    string line = streamReader.ReadLine();
                    while (line != null)
                    {

                        Console.WriteLine(line);
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
                streamWriter.WriteLine("> {0} - {1} <", playerName, playerPoints);
                streamWriter.WriteLine("*********************");
            }
        }


    }

}


