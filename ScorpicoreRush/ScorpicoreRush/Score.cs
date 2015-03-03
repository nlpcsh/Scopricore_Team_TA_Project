using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScorpicoreRush
{
    internal class Score
    {
        static string FilePath = @"..\..\Statistics.txt";
        static char HorizontalBorderSymbol = '-';
        static char VerticalBorderSymbol = '|';
        static string BorderLine = new String(HorizontalBorderSymbol, 21);
        static string FormatSpecifier = "{0,25}";

        public static void ShowHighScores()
        {
            SetUpWindow();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n         HIGH SCORES\n");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(FormatSpecifier, BorderLine);

            try
            {
                var streamReader = new StreamReader(FilePath);

                using (streamReader)
                {
                    string line = streamReader.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine("{0,25}", VerticalBorderSymbol + " " + line + " " + VerticalBorderSymbol);
                        Console.WriteLine(FormatSpecifier, BorderLine);
                        line = streamReader.ReadLine();
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("No file path provided!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incorrect file path: " + FilePath);
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("The entered file path is too long - 248 characters are the maximum!");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The file path contains a directory that cannot be found!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file " + FilePath + " was not found!");
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            catch (UnauthorizedAccessException uoae)
            {
                Console.WriteLine(uoae.Message);
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Invalid file path format!");
            }
            catch (SecurityException)
            {
                Console.WriteLine("You don't have the required permissions to access this file!");
            }
            finally
            {
                // Console.WriteLine("Press any key to continue...");
                // Console.ReadKey();
                // Menu.SelectOptions();
            }
        }

        public static void ShowScore(int points, int level)
        {
            SetUpWindow();

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
            string playerName = Console.ReadLine();
            Console.Write("{0}", name.KeyChar);

            SaveScoreToFile(playerName, points);
        }

        private static void SaveScoreToFile(string playerName, int playerPoints)
        {
            var streamWriter = new StreamWriter(@"..\..\Statistics.txt", true);
            using (streamWriter)
            {
                streamWriter.WriteLine("{0,-12}->{1,3}", playerName, playerPoints);
            }
        }

        static void SetUpWindow()
        {
            Console.Title = "Scorpicore Rush!";

            Console.WindowHeight = 30;
            Console.WindowWidth = 30;
            Console.BufferHeight = 100;
            Console.BufferWidth = Console.WindowWidth;

            Console.Clear();
            Console.CursorVisible = false;
        }
    }

}


