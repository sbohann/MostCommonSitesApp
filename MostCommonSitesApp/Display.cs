using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MostCommonSitesApp
{
    class Display
    {
        private static Dictionary<int, string> MainMenuSelection = new Dictionary<int, string>()
        {
            {1, "See" },
            {2, "Add" },
            {3, "Edit" },
            {4, "Quit" }
        };
        public static void MainMenuLoop()
        {
            PassObjectClass validation = new PassObjectClass();
            validation.PassObject.Add("pass", 0);
            validation.PassObject.Add("selection", 0);
            validation.PassObject.Add("retry", 0);
            validation = Display.MainMenu(validation);
            while (validation.PassObject["pass"] == 0)
            {
                validation.PassObject["retry"] = 1;
                validation = MainMenu(validation);
            }
            MainMenuSelected(validation.PassObject["selection"]);
        }
        private static PassObjectClass MainMenu(PassObjectClass validation)
        {
            if (validation.PassObject["retry"] == 1)
            {
                WriteColor("[Please try again]",ConsoleColor.Red);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("-- SITE SAVER --");
                Console.ResetColor();
            }
            WriteColor("[|1|] See sites",ConsoleColor.Red);
            WriteColor("[|2|] Add site",ConsoleColor.Blue);
            WriteColor("[|3|] Edit sites", ConsoleColor.DarkGreen);
            WriteColor("[|4|] Quit", ConsoleColor.Yellow);
            var chosenOption = Console.ReadLine();
            bool intSuccess = Int32.TryParse(chosenOption, out int intOption);
            if (intSuccess)
            {
                List<int> menuOptions = new List<int>(MainMenuSelection.Keys);
                for (int n = 0; n < menuOptions.Count; n++)
                {
                    if(menuOptions[n] == Int32.Parse(chosenOption))
                    {
                        validation.PassObject["pass"] = 1;
                        validation.PassObject["selection"] = menuOptions[n];
                        return validation;
                    }
                }
                return validation;
            }
            else
            {
                validation.PassObject["retry"] = 1;
                return validation;
            }
        }
        private static void MainMenuSelected(int selection)
        {

            if (!String.IsNullOrWhiteSpace(MainMenuSelection[selection]))
            {
                WriteColor($"Your Selection was '[{MainMenuSelection[selection]}]'", ConsoleColor.Yellow);
            }
            else
            {
                WriteColor("[ERROR]: Selection Handling in Display Class",ConsoleColor.Red);
                WriteColor("[CLOSING CONSOLE]", ConsoleColor.Red);
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
        static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }
}
