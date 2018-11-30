using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    class NHL_Threes_Main
    {
        static void Main(string[] args)
        {
            PrintHeader();
            Console.ReadKey();
            Console.Clear();
            
            #region Menu
            bool exit = false;
            string input = "";
            List<Player> customRoster = new List<Player>();

            // TODO Research and implement cursor functionality
            // https://stackoverflow.com/questions/46908148/controlling-menu-with-the-arrow-keys-and-enter

            PrintMenu(customRoster);
            do
            {
                Console.Write("Menu option: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        //customRoster.Add(new Player(TeamRenders.RenderStockTeam("STL"), "a", Positions.center,33,99,99,99,99,1,99,99));
                        Exhibition.Start(customRoster);
                        PrintMenu(customRoster);
                        break;
                    case "2":
                        customRoster = CustomPlayerMenu.CallSubMenu(customRoster);
                        PrintMenu(customRoster);
                        break;
                    case "3":
                        Console.WriteLine("Exiting game...");
                        exit = !exit;
                        break;
                    default:
                        TextFormat.Error("Invalid entry. Please try again.\n");
                        break;
                }
            } while (!exit);
            #endregion
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
       _   __ __  __ __
      / | / // / / // /
     /  |/ // /_/ // /
    / /|  // __  // /___
   /_/ |_//_/ /_//_____/
    ______ __  __ ____   ______ ______ _____
    /_  __// / / // __ \ / ____// ____// ___/
     / /  / /_/ // /_/ // __/  / __/   \__ \
    / /  / __  // _, _// /___ / /___  ___/ /
   /_/  /_/ /_//_/ |_|/_____//_____/ /____/


         Press any key to continue");
            // Art generated at http://patorjk.com/software/taag/

            Console.ResetColor();
        }

        private static void PrintMenu(List<Player> customRoster)
        {
            Console.Clear();
            Console.WriteLine("NHL Threes Main Menu\n\n" +
                "1. Play exhibition game");
            Console.WriteLine((customRoster.Any() ? "2. Create/Edit/Delete custom player" : "2. Create custom player"));
            Console.WriteLine("3. Exit\n");
        }
    }
}