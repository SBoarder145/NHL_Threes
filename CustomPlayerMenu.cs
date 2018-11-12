using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class CustomPlayerMenu
    {
        public static List<Player> CallSubMenu(List<Player> loadCustomRoster)
        {
            List<Player> customRoster;
            string input = "";

            if (loadCustomRoster.Any())
            {
                customRoster = loadCustomRoster;
                Console.Clear();
                Console.WriteLine("Custom Players Main Menu\n\n" +
                    "1. Create new player\n" +
                    "2. Edit custom player\n" +
                    "3. Delete custom player\n" +
                    "0. Return to the previous menu\n");

                do
                {
                    Console.Write("Menu option: ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            return CreatePlayer.Create(new List<Player>());
                        case "2":
                            Console.WriteLine("Edit player option - In development\n");
                            break;
                        case "3":
                            Console.WriteLine("Delete player option - In development\n");
                            break;
                        case "0":
                            Console.Clear();
                            return customRoster;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                } while (!input.Equals("0"));
            }
            else
            {
                return CreatePlayer.Create(new List<Player>());
            }

            return customRoster;
        }
    }
}