using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class CreatePlayer
    {
        private static List<Player> customRoster;
        private static Team newPlayerTeam;
        private static Positions newPlayerPosition;
        private static string input;
        private static string newPlayerName;
        private static byte newPlayerNumber;

        public static List<Player> Create(List<Player> loadCustomRoster)
        {
            if (loadCustomRoster.Any())
            {
                customRoster = loadCustomRoster;
            }
            else
            {
                customRoster = new List<Player>();
            }

            #region Player Initialization
            // Team
            Console.Clear();
            newPlayerTeam = AssignPlayerTeam();

            // Name
            Console.Clear();
            TextFormat.Log("Team: " + newPlayerTeam + "\n");
            newPlayerName = AssignPlayerName();

            // Number
            Console.Clear();
            TextFormat.Log("Team: " + newPlayerTeam +
                "\nName: " + newPlayerName + "\n");
            newPlayerNumber = (byte)AssignPlayerNumber();

            // Position
            Console.Clear();
            TextFormat.Log("Team: " + newPlayerTeam +
                "\nName: " + newPlayerName +
                "\nNumber: " + newPlayerNumber + "\n");
            newPlayerPosition = (Positions)AssignPlayerPosition();
            #endregion

            #region Roster Validations
            Player newPlayer = new Player(newPlayerTeam, newPlayerName, newPlayerPosition, newPlayerNumber);
            Console.Clear();
            Console.Write($"Checking {newPlayerTeam} roster");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(625);
                Console.Write('.');
            }
            Thread.Sleep(625);
            Console.WriteLine();

            PositionValidation(newPlayer);
            NameNumberValidation(newPlayer);
            #endregion

            #region Stat Points Allocation
            Console.Clear();
            TextFormat.Log($"Success! Welcome to the {newPlayerTeam}, {newPlayer.Name}!\n");
            short spBank = (short)SelectDifficulty();
            Console.Clear();
            AllocatePoints(newPlayer, spBank);
            #endregion

            customRoster.Add(newPlayer);
            Console.Clear();
            TextFormat.Green($"{newPlayer.Name} has been successfully added to the {newPlayerTeam.Name} roster!\n");
            Thread.Sleep(1000);
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            return customRoster;
        }

        private static Team AssignPlayerTeam()
        {
            Team t;

            #region Team Selection Menu Options
            Console.WriteLine(@"ANA | Anaheim Ducks         ARI | Arizona Coyotes        BOS | Boston Bruins");
            Console.WriteLine(@"BUF | Buffalo Sabres        CGY | Calgary Flames         CAR | Carolina Hurricanes");
            Console.WriteLine(@"CHI | Chicago Blackhawks    COL | Colorado Avalanche     CBJ | Columbus Blue Jackets");
            Console.WriteLine(@"DAL | Dallas Stars          DET | Detroit Red Wings      EDM | Edmonton Oilers");
            Console.WriteLine(@"FLA | Florida Panthers      LAK | Los Angeles Kings      MIN | Minnesota Wild");
            Console.WriteLine(@"MTL | Montreal Canadiens    NSH | Nashville Predators    NJD | New Jersey Devils");
            Console.WriteLine(@"NYI | New York Islanders    NYR | New York Rangers       OTT | Ottawa Senators");
            Console.WriteLine(@"PHI | Philadelphia Flyers   PIT | Pittsburgh Penguins    STL | Saint Louis Blues");
            Console.WriteLine(@"SJS | San Jose Sharks       TBL | Tampa Bay Lightning    TOR | Toronto Maple Leafs");
            Console.WriteLine(@"VAN | Vancouver Canucks     VGK | Vegas Golden Knights   WSH | Washington Capitals");
            Console.WriteLine("WPG | Winnipeg Jets\n");
            #endregion

            do
            {
                Console.Write("Enter the 3-letter abbreviation for your team: ");
                input = Console.ReadLine().ToUpper();
                if (IsAbbreviationValid(input))
                {
                    t = TeamRenders.RenderCustomTeam(TeamRenders.RenderStockTeam(input), customRoster);

                    if (t.Center.IsCustom && t.Winger.IsCustom && t.Defenseman.IsCustom)
                    {
                        TextFormat.Error($"\nWarning! The {newPlayerTeam} roster contains all custom players.\n" +
                            "If you continue, you will be required to overwrite one of the preexisting " +
                            "custom players.\nWould you like to continue?\n");

                        do
                        {
                            Console.Write("Select (Y)es/(N)o: ");
                            string overwriteInput = Console.ReadLine().Trim().ToLower();

                            switch (overwriteInput)
                            {
                                case "yes":
                                case "y":
                                    return t;
                                case "no":
                                case "n":
                                    return AssignPlayerTeam();
                                default:
                                    TextFormat.Error("Invalid selection. Please try again.\n");
                                    break;
                            }
                        } while (true);
                    }
                    else
                    {
                        return t;
                    }
                }
                else
                {
                    TextFormat.Error("Invalid selection. Please try again.\n");
                }
            } while (true);
        }

        private static bool IsAbbreviationValid(string abbv)
        {
            if (abbv.Equals("ANA") || abbv.Equals("ARI") || abbv.Equals("BOS") ||
                abbv.Equals("BUF") || abbv.Equals("CGY") || abbv.Equals("CAR") ||
                abbv.Equals("CHI") || abbv.Equals("COL") || abbv.Equals("CBJ") ||
                abbv.Equals("DAL") || abbv.Equals("DET") || abbv.Equals("EDM") ||
                abbv.Equals("FLA") || abbv.Equals("LAK") || abbv.Equals("MIN") ||
                abbv.Equals("MTL") || abbv.Equals("NSH") || abbv.Equals("NJD") ||
                abbv.Equals("NYI") || abbv.Equals("NYR") || abbv.Equals("OTT") ||
                abbv.Equals("PHI") || abbv.Equals("PIT") || abbv.Equals("STL") ||
                abbv.Equals("SJS") || abbv.Equals("TBL") || abbv.Equals("TOR") ||
                abbv.Equals("VAN") || abbv.Equals("VGK") || abbv.Equals("WSH") ||
                abbv.Equals("WPG"))
            {
                return true;
            }
            return false;
        }

        private static string AssignPlayerName()
        {
            do
            {
                Console.Write("Enter your player's name: ");
                input = Console.ReadLine().Trim();

                if (input.Equals(""))
                {
                    TextFormat.Error("Player's name cannot be empty. Please try again.\n");
                }
                else if (input.Length > 35)
                {
                    TextFormat.Error("Player's name cannot be more than 35 characters. Please try again.\n");
                }
                else if (input.ToLower().Contains("gretzky") ||
                    input.ToLower().Contains("gretzki") ||
                    input.ToLower().Contains("gretsky") ||
                    input.ToLower().Contains("gretski"))
                {
                    TextFormat.Error("No, just...no. Please try again.\n");
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        private static byte? AssignPlayerNumber()
        {
            Console.WriteLine("Select a jersey number between #1 - #98");

            if (newPlayerTeam.RetiredNumbers.Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("NOTE: The following retired numbers cannot be chosen: ");
                foreach (byte retired in newPlayerTeam.RetiredNumbers)
                {
                    Console.Write("#" + retired + " ");
                }
                Console.ResetColor();
            }

            do
            {
                Console.Write("\nNumber: #");
                input = Console.ReadLine().Trim();
                int n = 0;

                if (!int.TryParse(input, out n) || (n < 1 || n > 99))
                {
                    TextFormat.Error("Player number must be between 1 - 98. Please try again.");
                }
                else
                {
                    if (n == 99)
                    {
                        TextFormat.Error("Wayne Gretzky's number, #99, was retired league-wide on " +
                            "Feb. 6, 2000. Please try again.");
                    }
                    else if (newPlayerTeam.RetiredNumbers.Contains((byte)n))
                    {
                        TextFormat.Error($"#{n} is a retired number and cannot be worn by " +
                            $"current and future {newPlayerTeam.Name} players. Please try again.");
                    }
                    else
                    {
                        return (byte)n;
                    }
                }
            } while (true);
        }

        private static Positions? AssignPlayerPosition()
        {
            do
            {
                Console.WriteLine("(C)enter\n(W)inger\n(D)efenseman");
                Console.Write("Please select a position: ");
                input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "c":
                    case "center":
                        return Positions.center;
                    case "w":
                    case "winger":
                        return Positions.winger;
                    case "d":
                    case "defenseman":
                        return Positions.defenseman;
                    case "g":
                    case "goalie":
                        TextFormat.Error("Custom goalie feature has not and may not be implemented. Please try again.\n");
                        break;
                    default:
                        TextFormat.Error("Invalid selection. Please try again.\n");
                        break;
                }

            } while (true);
        }

        private static void PositionValidation(Player newPlayer)
        {
            Player currentPlayer = newPlayer;

            switch (newPlayer.Position)
            {
                case Positions.center:
                    currentPlayer = newPlayerTeam.Center;
                    break;
                case Positions.winger:
                    currentPlayer = newPlayerTeam.Winger;
                    break;
                case Positions.defenseman:
                    currentPlayer = newPlayerTeam.Defenseman;
                    break;
            }

            if ((newPlayer.Position == currentPlayer.Position) && currentPlayer.IsCustom)
            {
                TextFormat.Error($"\nA custom player is already assigned to the {newPlayerTeam.Name} " +
                        $"{currentPlayer.Position} position.\nWould you like to overwrite {currentPlayer.Name}?");

                do
                {
                    Console.Write("Select (Y)es/(N)o: ");
                    string overwriteInput = Console.ReadLine().Trim().ToLower();

                    switch (overwriteInput)
                    {
                        case "yes":
                        case "y":
                            TextFormat.Error($"\nYou're about to overwrite {currentPlayer.Name}. " +
                                "You cannot undo this action.\nAre you sure you want to continue?");
                            do
                            {
                                Console.Write("Select (Y)es/(N)o: ");
                                string confirmInput = Console.ReadLine().Trim().ToLower();

                                switch (confirmInput)
                                {
                                    case "yes":
                                    case "y":
                                        customRoster.Remove(currentPlayer);
                                        return;
                                    case "no":
                                    case "n":
                                        AssignPlayerPosition();
                                        PositionValidation(newPlayer);
                                        return;
                                    default:
                                        TextFormat.Error("Invalid selection. Please try again.");
                                        break;
                                }
                            } while (true);
                        case "no":
                        case "n":
                            AssignPlayerPosition();
                            PositionValidation(newPlayer);
                            return;
                        default:
                            TextFormat.Error("Invalid selection. Please try again.");
                            break;
                    }

                } while (true);
            }
        }

        private static void NameNumberValidation(Player newPlayer)
        {
            // Name validation
            bool valid = false;

            do
            {
                List<string> playerNames = new List<string>() { newPlayerTeam.Goalie.Name.ToLower() };

                if (newPlayer.Position != Positions.center)
                {
                    playerNames.Add(newPlayerTeam.Center.Name.ToLower());
                }

                if (newPlayer.Position != Positions.winger)
                {
                    playerNames.Add(newPlayerTeam.Winger.Name.ToLower());
                }

                if (newPlayer.Position != Positions.defenseman)
                {
                    playerNames.Add(newPlayerTeam.Defenseman.Name.ToLower());
                }

                if (playerNames.Contains(newPlayer.Name.ToLower()))
                {
                    TextFormat.Error($"{newPlayer.Name} is already on the {newPlayerTeam.Name} roster.\n");
                    newPlayer.Name = AssignPlayerName();
                }
                else
                {
                    valid = true;
                }
            } while (!valid);

            // Number validation
            valid = false;

            do
            {
                if (newPlayer.Number == newPlayerTeam.Goalie.Number)
                {
                    TextFormat.Error($"#{newPlayer.Number} is already taken by {newPlayerTeam.Goalie}. " +
                        "Please select a different number.\n");
                    newPlayer.Number = (byte)AssignPlayerNumber();
                }
                else if (newPlayer.Number == newPlayerTeam.Center.Number && newPlayer.Position != Positions.center)
                {
                    TextFormat.Error($"#{newPlayer.Number} is already taken by {newPlayerTeam.Center}. " +
                        "Please select a different number.\n");
                    newPlayer.Number = (byte)AssignPlayerNumber();
                }
                else if (newPlayer.Number == newPlayerTeam.Winger.Number && newPlayer.Position != Positions.winger)
                {
                    TextFormat.Error($"#{newPlayer.Number} is already taken by {newPlayerTeam.Winger}. " +
                        "Please select a different number.\n");
                    newPlayer.Number = (byte)AssignPlayerNumber();
                }
                else if (newPlayer.Number == newPlayerTeam.Defenseman.Number && newPlayer.Position != Positions.defenseman)
                {
                    TextFormat.Error($"#{newPlayer.Number} is already taken by {newPlayerTeam.Defenseman}. " +
                        "Please select a different number.\n");
                    newPlayer.Number = (byte)AssignPlayerNumber();
                }
                else
                {
                    valid = true;
                }
            } while (!valid);
        }

        private static short? SelectDifficulty()
        {
            Console.WriteLine("Please select your player's skill level:");
            Console.WriteLine("(R)ookie (Hard): 560 skill points / overall avg ~80");
            Console.WriteLine("(V)eteran (Normal): 600 skill points / overall avg ~86");
            Console.WriteLine("(F)ranchise (Easy): 640 skill points / overall avg ~91\n");

            do
            {
                Console.Write("Skill level: ");
                input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "r":
                    case "rookie":
                        return 560;
                    case "v":
                    case "vet":
                    case "veteran":
                        return 600;
                    case "f":
                    case "franchise":
                        return 640;
                }

                TextFormat.Error("Invalid selection. Please try again.\n");
                input = "";

            } while (true);
        }

        private static void AllocatePoints(Player newPlayer, short spBank)
        {
            short spBankReset = spBank;
            byte speed;
            byte strength;
            byte stickHandling;
            byte passing;
            byte shooting;
            byte offAwareness;
            byte defAwareness = 0;
            bool valid = false;

            do
            {
                spBank = spBankReset;
                Console.Clear();
                DisplayStatExplaination(spBank);

                speed = AssignStat("Speed", spBank, 7);
                spBank -= speed;

                strength = AssignStat("Strength", spBank, 6);
                spBank -= strength;

                stickHandling = AssignStat("Stick Handling", spBank, 5);
                spBank -= stickHandling;

                passing = AssignStat("Passing", spBank, 4);
                spBank -= passing;

                shooting = AssignStat("Shooting", spBank, 3);
                spBank -= shooting;

                offAwareness = AssignStat("Offensive Awareness", spBank, 2);
                spBank -= offAwareness;

                if (spBank == 0)
                {
                    TextFormat.Error("You do not have enough skill points to proceed.");
                    Console.WriteLine("Press any key to re-enter stats...");
                    Console.ReadKey();
                }
                else
                {
                    defAwareness = AssignStat("Defensive Awareness", spBank, 1);
                    valid = (defAwareness > 0 ? true : false);
                }

            } while (!valid);

            newPlayer.Speed = speed;
            newPlayer.Strength = strength;
            newPlayer.StickHandling = stickHandling;
            newPlayer.Passing = passing;
            newPlayer.Shooting = shooting;
            newPlayer.OffAwareness = offAwareness;
            newPlayer.DefAwareness = defAwareness;

        }

        private static void DisplayStatExplaination(short spBank)
        {
            Console.Write("You have ");
            TextFormat.Green(spBank.ToString());
            Console.WriteLine(" points to spend on the following stats:");

            Console.WriteLine("    * Speed\n\tUsed for outskating your opponent and increase breakaway chances.");
            Console.WriteLine("    * Strength\n\tGood for checking opponents to force turnovers or break through zones.");
            Console.WriteLine("    * Stick Handling\n\tIncrease passing and shooting chances; used in faceoffs (Center position only).");
            Console.WriteLine("    * Passing\n\tSet up one-timers or pass through zones; CPU teammates will gain control of puck.");
            Console.WriteLine("    * Shooting\n\tSelf-explanitory.");
            Console.WriteLine("    * Offensive Awareness\n\tProvides stat bonuses and additional options in the offensive zone.");
            Console.WriteLine("    * Defensive Awareness\n\tProvides stat bonuses and additional options in the defensive zone.\n");
            TextFormat.Error("Skill point values may range from 1 - 99\n");
        }

        private static byte AssignStat(string desc, short spBank, short remainingStats)
        {
            int stat = 0;
            bool valid = false;

            do
            {
                Console.Write("You have ");
                TextFormat.Green(spBank.ToString());
                Console.Write($" skill points remaining (~");
                TextFormat.Green((spBank / remainingStats).ToString());
                Console.WriteLine("/stat).");


                Console.Write(desc + ": ");
                input = Console.ReadLine();

                if (!int.TryParse(input, out stat) || (stat < 1 || stat > 99))
                {
                    TextFormat.Error("Stat value must be between 1 and 99. Please try again.\n");
                }
                else if (stat > spBank)
                {
                    TextFormat.Error("You do not have enough skill points. Please try again.\n");
                }
                else if (stat < spBank && desc.Equals("Defensive Awareness"))
                {
                    Console.Write("\nYou still have ");
                    TextFormat.Green((spBank - stat).ToString());
                    Console.WriteLine(" skill points remaining." +
                        "\nWould you like to continue?\n");

                    do
                    {
                        Console.Write("Select (Y)es/(N)o: ");
                        input = Console.ReadLine().Trim().ToLower();

                        switch (input)
                        {
                            case "yes":
                            case "y":
                                Console.WriteLine();
                                return (byte)stat;
                            case "no":
                            case "n":
                                Console.Write("\nWould you like to reset ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("ALL");
                                Console.ResetColor();
                                Console.WriteLine(" stats and try again?\n");

                                do
                                {
                                    Console.Write("Select (Y)es/(N)o: ");
                                    input = Console.ReadLine().Trim().ToLower();

                                    switch (input)
                                    {
                                        case "yes":
                                        case "y":
                                            return 0;
                                        case "no":
                                        case "n":
                                            valid = true;
                                            break;
                                        default:
                                            TextFormat.Error("Invalid selection. Please try again.\n");
                                            break;
                                    }
                                } while (!valid);
                                break;
                            default:
                                TextFormat.Error("Invalid selection. Please try again.\n");
                                break;
                        }
                    } while (!valid);
                }
                else
                {
                    Console.WriteLine();
                    return (byte)stat;
                }
                    
            } while (true);
        }
    }
}