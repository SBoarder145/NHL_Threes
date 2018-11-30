using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    class Exhibition
    {
        
        #region Variables
        private static Random rand = new Random();
        private static Team home;
        private static Team away;
        private static Player user;
        private static Player opponent;
        private static Player teammate;
        private static Goalie goalie;
        private static Player playerWithPuck;
        private static Player defendingPlayer;
        private static Zones zone = Zones.neutral;
        private static string input;
        private static bool isShooting = false;
        #endregion

        public static void Start(List<Player> customRoster)
        {
            Team userTeam;
            Team opponent;

            Console.Clear();

            Console.WriteLine("NHL Threes Exhibition Game\n");

            // Select teams
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

            userTeam = SelectTeam('p');
            userTeam.IsUserTeam = true;
            do
            {
                opponent = SelectTeam('c');

                if (userTeam.Name.Equals(opponent.Name))
                {
                    TextFormat.Error($"The {userTeam} has already been selected. Please try again.\n");
                }
            } while (userTeam.Name.Equals(opponent.Name));

            // Select player
            if (customRoster.Any())
            {
                userTeam = TeamRenders.RenderCustomTeam(userTeam, customRoster);
                opponent = TeamRenders.RenderCustomTeam(opponent, customRoster);
            }

            SelectPlayer(userTeam);

            // Home/away designations
            if (Roll(2) == 1)
            {
                home = userTeam;
                away = opponent;
                home.IsUserTeam = true;
            }
            else
            {
                home = opponent;
                away = userTeam;
                away.IsUserTeam = true;
            }
            home.IsHome = true;

            Console.Clear();
            Console.Write("TONIGHT! The visiting ");
            TextFormat.Away(away.Name);
            Console.WriteLine($" play against the {home}!\n");
            Thread.Sleep(1000);

            PlayGame();
        }

        private static void PlayGame()
        {
            do
            {
                bool scored = false;
                string choice;
                float result;

                DisplayScoreboard();
                Faceoff();

                do
                {
                    choice = "";
                    result = 0;

                    #region Choice
                    if (zone == Zones.goal)
                    {
                        choice = SetupShotAttempt();
                    }
                    else
                    {
                        choice = SetupPlay();
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(625);
                        Console.Write('.');
                    }
                    Thread.Sleep(625);
                    #endregion

                    #region Execute
                    if (zone != Zones.goal)
                    {
                        switch (choice)
                        {
                            case "one-timer":
                                result = ExecutePlay(playerWithPuck, defendingPlayer, "passing");
                                if (result > 0)
                                {
                                    isShooting = true;
                                    if (home.HasPuck)
                                    {
                                        Console.Write(teammate);
                                    }
                                    else
                                    {
                                        TextFormat.Away(teammate.Name);
                                    }
                                    Console.Write(" takes the shot!");

                                    for (int i = 0; i < 3; i++)
                                    {
                                        Thread.Sleep(625);
                                        Console.Write('.');
                                    }
                                    Thread.Sleep(625);

                                    result = ExecuteShotAttempt(teammate, goalie);
                                    // One-timer shot bonus
                                    result += 5;
                                }
                                break;
                            case "shooting":
                                result = ExecuteShotAttempt(playerWithPuck, goalie);
                                break;
                            default:
                                result = ExecutePlay(playerWithPuck, defendingPlayer, choice);
                                break;
                        }
                    }
                    else
                    {
                        result = ExecuteShotAttempt(playerWithPuck, goalie, choice);
                    }
                    #endregion

                    #region Result
                    if (result > 0 && isShooting)
                    {
                        Console.WriteLine("and scores!\n");
                        scored = !scored;
                    }
                    else if (result > 0 && !isShooting)
                    {
                        if (object.ReferenceEquals(playerWithPuck, user) || object.ReferenceEquals(playerWithPuck, teammate))
                        {
                            Console.WriteLine("and succeeds!\n");
                            switch (zone)
                            {
                                case Zones.defensive:
                                    zone = Zones.neutral;
                                    break;
                                case Zones.neutral:
                                    zone = Zones.offensive;
                                    break;
                                case Zones.offensive:
                                    zone = Zones.goal;
                                    break;
                            }

                            // if passing, change user <--> teammate with puck
                            if (object.ReferenceEquals(playerWithPuck, user) && choice.Equals("passing"))
                            {
                                do
                                {
                                    if (home.HasPuck)
                                    {
                                        teammate = PickRandomPlayer(home);
                                    }
                                    else
                                    {
                                        teammate = PickRandomPlayer(away);
                                    }
                                } while (teammate.Name.Equals(user.Name));

                                playerWithPuck = teammate;
                                playerWithPuck.HasPuck = true;
                                user.HasPuck = false;
                            }
                            else if (object.ReferenceEquals(playerWithPuck, teammate) && choice.Equals("passing"))
                            {
                                // Ensure teammate.HasPuck = false
                                playerWithPuck.HasPuck = false;
                                playerWithPuck = user;
                                playerWithPuck.HasPuck = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("and fails!\n");
                            switch (zone)
                            {
                                case Zones.defensive:
                                    zone = Zones.goal;
                                    break;
                                case Zones.neutral:
                                    zone = Zones.defensive;
                                    break;
                                case Zones.offensive:
                                    zone = Zones.neutral;
                                    break;
                            }
                        }
                    }
                    else if (result < 0 && isShooting)
                    {
                        Console.Write("and gets blocked by ");
                        if (goalie.AssignedTeam.IsHome)
                        {
                            Console.Write(goalie);
                        }
                        else
                        {
                            TextFormat.Away(goalie.Name);
                        }
                        Console.WriteLine("!\n");
                        GoalieTurnover();
                    }
                    else
                    {
                        if (object.ReferenceEquals(playerWithPuck, user) || object.ReferenceEquals(playerWithPuck, teammate))
                        {
                            Console.WriteLine("and fails!\n");
                            switch (zone)
                            {
                                case Zones.defensive:
                                    zone = Zones.goal;
                                    break;
                                case Zones.neutral:
                                    zone = Zones.defensive;
                                    break;
                                case Zones.offensive:
                                    zone = Zones.neutral;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("and succeeds!\n");
                            switch (zone)
                            {
                                case Zones.defensive:
                                    zone = Zones.neutral;
                                    break;
                                case Zones.neutral:
                                    zone = Zones.offensive;
                                    break;
                                case Zones.offensive:
                                    zone = Zones.goal;
                                    break;
                            }
                        }
                        PlayerTurnover();
                    }
                    #endregion

                    Thread.Sleep(1000);
                } while (!scored);

                if (home.HasPuck)
                {
                    home.Score++;
                }
                else
                {
                    away.Score++;
                }

                Thread.Sleep(1000);

            } while (home.Score < 3 && away.Score < 3);

            #region End Game
            Console.Write("The ");
            if (home.Score == 3)
            {
                Console.Write(home);
            }
            else
            {
                TextFormat.Away(away.Name);
            }
            Console.WriteLine(" win!\n");
            Thread.Sleep(1000);

            Console.WriteLine("Final score:");
            Thread.Sleep(1000);
            TextFormat.Away(away.Name);
            Console.WriteLine($" {away.Score}");
            Thread.Sleep(1000);
            Console.WriteLine($"{home} {home.Score}");
            ResetExhibitionGameData();

            Thread.Sleep(1000);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            #endregion
        }

        #region Shared Methods
        private static int Roll(int dNum)
        {
            // Create larger probability set, return original results
            int multi = dNum * 7;
            return rand.Next(0, multi) % dNum;
        }

        private static void ResetExhibitionGameData()
        {
            home.Score = 0;
            home.IsHome = false;
            home.IsUserTeam = false;

            away.Score = 0;
            away.IsUserTeam = false;

            List<Player> playerList = new List<Player>()
            {
                home.Center,
                home.Winger,
                home.Defenseman,
                away.Center,
                away.Winger,
                away.Defenseman,
            };

            foreach (Player p in playerList)
            {
                p.HasPuck = false;
            }

            user = null;
            opponent = null;
            teammate = null;
            goalie = null;
            playerWithPuck = null;
            defendingPlayer = null;
            zone = Zones.neutral;
        }
        #endregion

        #region Start() Methods
        private static Team SelectTeam(char sel)
        {
            do
            {
                input = "";

                Console.Write("Enter the 3-letter abbreviation for " +
                    $"{(sel.Equals('p') ? "your" : "opposing")} team selection: ");
                input = Console.ReadLine().ToUpper();

                if (IsAbbreviationValid(input))
                {
                    Console.WriteLine();
                    return TeamRenders.RenderStockTeam(input);
                }

                TextFormat.Error("Invalid selection. Please try again.\n");

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

        private static void SelectPlayer(Team t)
        {
            Console.WriteLine("Select a player:\n" +
                $"(C)enter: {t.Center}\n" +
                $"(W)inger: {t.Winger}\n" +
                $"(D)efensman: {t.Defenseman}\n");

            do
            {
                string p = "";
                Console.Write("Please select a position: ");
                p = Console.ReadLine().ToLower();

                switch (p)
                {
                    case "c":
                    case "center":
                        user = t.Center;
                        return;
                    case "w":
                    case "winger":
                        user = t.Winger;
                        return;
                    case "d":
                    case "defenseman":
                        user = t.Defenseman;
                        return;
                    case "g":
                    case "goalie":
                        TextFormat.Error("Goalie is not a playable position. Please try again.\n");
                        break;
                    default:
                        TextFormat.Error("Invalid selection. Please try again.\n");
                        break;
                }

            } while (true);
        }
        #endregion

        #region PlayGame() Methods
        private static void DisplayScoreboard()
        {
            Console.Write("-----------\n| ");
            TextFormat.Away(away.Abbreviation);
            Console.WriteLine($" | {away.Score} |");
            Console.WriteLine($"| {home.Abbreviation} | {home.Score} |");
            Console.WriteLine("-----------\n");
        }

        private static void Faceoff()
        {
            ResetPuckPossession();

            float result = ExecutePlay(home.Center, away.Center, "stickhandling");

            if (home.IsUserTeam)
            {
                opponent = PickRandomPlayer(away);
            }
            else
            {
                opponent = PickRandomPlayer(home);
            }

            // Home wins
            if (result > 0)
            {
                home.HasPuck = true;
                playerWithPuck = home.IsUserTeam ? user : opponent;
                if (home.IsUserTeam && user.Position == Positions.center)
                {
                    TextFormat.UserHome(user.Name);
                }
                else
                {
                    Console.Write(home.Center);
                }
            }
            else
            {
                away.HasPuck = true;
                playerWithPuck = home.IsUserTeam ? opponent : user;
                if (!home.IsUserTeam && user.Position == Positions.center)
                {
                    TextFormat.UserAway(user.Name);
                }
                else
                {
                    TextFormat.Away(away.Center.Name);
                }
            }

            playerWithPuck.HasPuck = true;
            Console.WriteLine(" wins the faceoff\n");
            Thread.Sleep(1000);
        }

        private static string SetupPlay()
        {
            goalie = playerWithPuck.AssignedTeam.IsHome ? away.Goalie : home.Goalie;

            if (user.HasPuck)
            {
                if (home.HasPuck)
                {
                    if (opponent == null)
                    {
                        opponent = PickRandomPlayer(away);
                    }

                    TextFormat.UserHome(user.Name);
                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine(" has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.WriteLine($" has the puck in the {user.AssignedTeam}'s {zone} zone.");
                    }
                    Thread.Sleep(1000);
                    TextFormat.Away(opponent.Name);
                    Console.WriteLine(" is attempting to defend.\n");
                }
                else
                {
                    if (opponent == null)
                    {
                        opponent = PickRandomPlayer(home);
                    }

                    TextFormat.UserAway(user.Name);
                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine(" has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.Write(" has the puck in the ");
                        TextFormat.Away(user.AssignedTeam.Name);
                        Console.WriteLine($"'s {zone} zone.");
                    }
                    Thread.Sleep(1000);
                    Console.WriteLine($"{opponent} is attempting to defend.\n");
                }

                defendingPlayer = opponent;
                Thread.Sleep(1000);
                return DisplayUserChoices();
            }
            else if (opponent.HasPuck)
            {
                if (home.HasPuck)
                {
                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine($"{opponent} has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.Write($"{opponent} has the puck in the ");
                        TextFormat.Away(user.AssignedTeam.Name);
                        Console.WriteLine($"'s {zone} zone.");
                    }
                    Thread.Sleep(1000);
                    TextFormat.UserAway(user.Name);
                    Console.WriteLine(" is attempting to defend.\n");
                }
                else
                {
                    TextFormat.Away(opponent.Name);
                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine(" has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.WriteLine($" has the puck in the {user.AssignedTeam}'s {zone} zone.");
                    }
                    Thread.Sleep(1000);
                    TextFormat.UserHome(user.Name);
                    Console.WriteLine(" is attempting to defend.\n");
                }

                defendingPlayer = user;
                Thread.Sleep(1000);
                return DisplayUserChoices();
            }
            else
            {
                // teammate
                if (home.HasPuck)
                {
                    opponent = PickRandomPlayer(away);
                    defendingPlayer = opponent;

                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine($"{playerWithPuck} has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.WriteLine($"{playerWithPuck} has the puck in the {home}'s {zone} zone.");
                    }

                    TextFormat.Away(defendingPlayer.Name);
                    Console.WriteLine(" is attempting to defend.\n");
                }
                else
                {
                    opponent = PickRandomPlayer(home);
                    defendingPlayer = opponent;

                    TextFormat.Away(playerWithPuck.Name);

                    if (zone == Zones.neutral)
                    {
                        Console.WriteLine(" has the puck in the neutral zone.");
                    }
                    else
                    {
                        Console.WriteLine($" has the puck in the {away}'s {zone} zone.");
                    }

                    Console.WriteLine($"{defendingPlayer} is attempting to defend.\n");
                }

                Thread.Sleep(1000);
                return SelectAIChoice();
            }
        }

        private static string SetupShotAttempt()
        {
            goalie = (home.HasPuck ? away.Goalie : home.Goalie);

            if (user.HasPuck)
            {
                if (home.HasPuck)
                {
                    TextFormat.UserHome(user.Name);
                    Console.Write(" has an open shot on ");
                    TextFormat.Away(goalie.Name);
                    Console.WriteLine('.');
                }
                else
                {
                    TextFormat.UserAway(user.Name);
                    Console.WriteLine($" has an open shot on {goalie}.");
                }

                Thread.Sleep(1000);
                return DisplayUserChoices();
            }
            else if (opponent.HasPuck)
            {
                if (home.HasPuck)
                {
                    Console.Write($"{opponent.Name} has an open shot on ");
                    TextFormat.Away(goalie.Name);
                    Console.WriteLine('.');
                }
                else
                {
                    TextFormat.Away(opponent.Name);
                    Console.WriteLine($" has an open shot on {goalie}.");
                }

                Thread.Sleep(1000);
                return SelectAIChoice();
            }
            else
            {
                Console.Write($"{teammate} has an open shot on ");
                TextFormat.Away(goalie.Name);
                Console.WriteLine('.');
                Thread.Sleep(1000);
                return SelectAIChoice();
            }
        }

        private static float ExecutePlay(Player offPlayer, Player defPlayer, string stat)
        {
            float offStat = 0;
            float defStat = 0;

            switch (stat)
            {
                case "speed":
                    offStat = offPlayer.Speed;
                    defStat = defPlayer.Speed;
                    break;
                case "strength":
                    offStat = offPlayer.Strength;
                    defStat = defPlayer.Strength;
                    break;
                case "stickhandling":
                    offStat = offPlayer.StickHandling;
                    defStat = defPlayer.StickHandling;
                    break;
                case "passing":
                    offStat = offPlayer.Passing;
                    defStat = defPlayer.Passing;
                    break;
            }

            // Awareness bonus
            offStat += (offPlayer.OffAwareness / 4);
            defStat += (defPlayer.DefAwareness / 4);

            // Home ice advantage stat boost
            if (offPlayer.AssignedTeam.IsHome)
            {
                offStat += 2;
            }
            else
            {
                defStat += 2;
            }

            return ExecutePlay(offStat, defStat);
        }

        private static float ExecuteShotAttempt(Player player, Goalie goalie)
        {
            float offStat = player.Shooting;
            float defStat = goalie.Average;

            // Home ice advantage stat boost
            if (player.AssignedTeam.IsHome)
            {
                offStat += 2;
            }
            else
            {
                defStat += 2;
            }

            return ExecutePlay(offStat, defStat);
        }

        private static float ExecuteShotAttempt(Player player, Goalie goalie, string shotLocation)
        {
            float offStat = player.Shooting;
            float defStat = 0;

            switch (shotLocation)
            {
                case "low stick":
                    defStat = goalie.StickLow;
                    break;
                case "low glove":
                    defStat = goalie.GloveLow;
                    break;
                case "high stick":
                    defStat = goalie.StickHigh;
                    break;
                case "high glove":
                    defStat = goalie.GloveHigh;
                    break;
                case "legs":
                    defStat = goalie.BetweenLegs;
                    break;
            }

            // Home ice advantage stat boost
            if (player.AssignedTeam.IsHome)
            {
                offStat += 2;
            }
            else
            {
                defStat += 2;
            }

            return ExecutePlay(offStat, defStat);
        }

        private static float ExecutePlay(float offStat, float defStat)
        {
            // "Luck"
            offStat += Roll(20);
            defStat += Roll(20);

            // Tiebreaker
            if (offStat == defStat)
            {
                if (Roll(2) == 1)
                {
                    offStat++;
                }
                else
                {
                    defStat++;
                }
            }

            return (offStat - defStat);
        }

        private static void GoalieTurnover()
        {
            ResetPuckPossession();

            if (goalie.AssignedTeam.IsHome)
            {
                home.HasPuck = true;

                if (home.IsUserTeam)
                {
                    user.HasPuck = true;
                    playerWithPuck = user;
                    opponent = PickRandomPlayer(away);
                    defendingPlayer = opponent;
                    zone = Zones.defensive;
                }
                else
                {
                    opponent = PickRandomPlayer(home);
                    playerWithPuck = opponent;
                    defendingPlayer = user;
                    zone = Zones.offensive;
                }
            }
            else
            {
                away.HasPuck = true;

                if (home.IsUserTeam)
                {
                    opponent = PickRandomPlayer(away);
                    playerWithPuck = opponent;
                    defendingPlayer = user;
                    zone = Zones.offensive;
                }
                else
                {
                    playerWithPuck = user;
                    opponent = PickRandomPlayer(home);
                    defendingPlayer = opponent;
                    zone = Zones.defensive;
                }
            }
            playerWithPuck.HasPuck = true;
        }

        private static void PlayerTurnover()
        {
            if (home.HasPuck)
            {
                home.HasPuck = false;
                away.HasPuck = true;
            }
            else
            {
                home.HasPuck = true;
                away.HasPuck = false;
            }

            Player temp = playerWithPuck;
            playerWithPuck = defendingPlayer;
            defendingPlayer = temp;
            playerWithPuck.HasPuck = true;
            defendingPlayer.HasPuck = false;
        }

        private static string DisplayUserChoices()
        {
            #region Shot Attempt
            if (user.HasPuck && zone == Zones.goal)
            {
                Console.WriteLine("\nWhere will you shoot the puck?\n");
                Console.WriteLine("1. Low stick side");
                Console.WriteLine("2. Low glove side");
                Console.WriteLine("3. High stick side");
                Console.WriteLine("4. High glove side");
                Console.WriteLine("5. Between the legs");

                isShooting = true;

                do
                {
                    input = "";

                    Console.Write("\nChoice: ");
                    input = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    if (home.IsUserTeam)
                    {
                        TextFormat.UserHome(user.Name);
                    }
                    else
                    {
                        TextFormat.UserAway(user.Name);
                    }

                    switch (input)
                    {
                        case "1":
                        case "low stick":
                        case "low stick side":
                            Console.Write(" shoots for the low stick side!");
                            return "low stick";
                        case "2":
                        case "low glove":
                        case "low glove side":
                            Console.Write(" shoots for the low glove side!");
                            return "low glove";
                        case "3":
                        case "high stick":
                        case "high stick side":
                            Console.Write(" shoots for the high stick side!");
                            return "high st shoots for ick";
                        case "4":
                        case "high glove":
                        case "high glove side":
                            Console.Write(" shoots for the high glove side!");
                            return "high glove";
                        case "5":
                        case "legs":
                        case "between legs":
                            Console.Write(" shoots for between the goalie's legs!");
                            return "legs";
                        default:
                            Console.WriteLine("Invalid selection. Please try again.");
                            break;
                    }
                } while (true);
            } 
            #endregion

            #region Offense
            else if (user.HasPuck)
            {
                Console.WriteLine("How will you proceed?\n");
                Console.WriteLine("1. Skate past the defender (Speed)");
                Console.WriteLine("2. Power through the defender (Strength)");
                Console.WriteLine("3. Deke past the defender (Stick Handling)");

                if (zone != Zones.offensive)
                {
                    Console.Write((user.OffAwareness >= 70 ? "4. Pass to a teammate (Off. Awareness 70+)\n" : ""));
                }
                else
                {
                    Console.WriteLine("4. Shoot the puck (Shooting)");
                    Console.Write((user.OffAwareness >= 86 ? "5. Attempt a one-timer (Passing 87+)\n" : ""));
                }

                do
                {
                    input = "";

                    Console.Write("\nChoice: ");
                    input = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    if (input.Equals("1") || input.Equals("speed"))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                            Console.Write(" attempts to skate past ");
                            TextFormat.Away(opponent.Name);
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                            Console.Write($" attempts to skate past {opponent}");
                        }
                        return "speed";
                    }
                    else if (input.Equals("2") || input.Equals("strength"))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                            Console.Write(" attempts to power through ");
                            TextFormat.Away(opponent.Name);
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                            Console.Write($" attempts to power through {opponent}");
                        }
                        return "strength";
                    }
                    else if (input.Equals("3") || input.Contains("stick") || input.Contains("handling"))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                            Console.Write(" attempts to deke around ");
                            TextFormat.Away(opponent.Name);
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                            Console.Write($" attempts to deke around {opponent}");
                        }
                        return "stickhandling";
                    }
                    else if (zone != Zones.offensive && user.OffAwareness >= 70 &&
                        (input.Equals("4") || input.Contains("passing")))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                        }
                        Console.Write(" attempts a stretch pass");
                        return "passing";
                    }
                    else if (zone == Zones.offensive && (input.Equals("4") || input.Contains("shooting")))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                        }
                        Console.Write(" shoots from the blue line!");
                        isShooting = true;
                        goalie = home.IsUserTeam ? away.Goalie : home.Goalie;
                        return "shooting";
                    }
                    else if (zone == Zones.offensive && user.OffAwareness > 86 &&
                        (input.Equals("5") || input.Contains("passing")))
                    {
                        if (home.HasPuck)
                        {
                            TextFormat.UserHome(user.Name);
                            Console.Write(" sets up the one-timer");
                            do
                            {
                                teammate = PickRandomPlayer(home);
                            } while (user.Name.Equals(teammate.Name));
                        }
                        else
                        {
                            TextFormat.UserAway(user.Name);
                            Console.Write(" sets up the one-timer");
                            do
                            {
                                teammate = PickRandomPlayer(away);
                            } while (user.Name.Equals(teammate.Name));
                        }
                        isShooting = true;
                        return "one-timer";
                    }
                    else
                    {
                        TextFormat.Error("Invalid selection. Please try again.");
                    }

                } while (true);
            }
            #endregion

            #region Defense
            Console.WriteLine("How will you defend?\n");
            Console.WriteLine("1. Poke check the puck carrier (Speed)");
            Console.WriteLine("2. Check the puck carrier (Strength)");
            Console.WriteLine("3. \"Pickpocket\" the puck carrier (Stick Handling)");

            do
            {
                input = "";

                Console.Write("\nChoice: ");
                input = Console.ReadLine().ToLower();
                Console.WriteLine();

                if (input.Equals("1") || input.Equals("speed"))
                {
                    if (away.HasPuck)
                    {
                        TextFormat.UserHome(user.Name);
                        Console.Write(" speeds up to poke check ");
                        TextFormat.Away(opponent.Name);
                    }
                    else
                    {
                        TextFormat.UserAway(user.Name);
                        Console.Write($" speeds up to poke check {opponent}");
                    }
                    return "speed";
                }
                else if (input.Equals("2") || input.Equals("strength"))
                {
                    if (away.HasPuck)
                    {
                        TextFormat.UserHome(user.Name);
                        Console.Write(" gets ready to check ");
                        TextFormat.Away(opponent.Name);
                    }
                    else
                    {
                        TextFormat.UserAway(user.Name);
                        Console.Write($" gets ready to check {opponent}");
                    }
                    return "strength";
                }
                else if (input.Equals("3") || input.Contains("stick") || input.Contains("handling"))
                {
                    if (away.HasPuck)
                    {
                        TextFormat.UserHome(user.Name);
                        Console.Write(" attempts to pick ");
                        TextFormat.Away(opponent.Name + "'s");
                        Console.Write(" pocket");
                    }
                    else
                    {
                        TextFormat.UserAway(user.Name);
                        Console.Write($" attempts to pick {opponent}'s pocket");
                    }
                    return "stickhandling";
                }
                else
                {
                    TextFormat.Error("Invalid selection. Please try again.");
                }

            } while (true);
            #endregion
        }

        private static string SelectAIChoice()
        {
            int selection;
            string choice = "";

            #region Teammate/Opponent Shooting
            if (zone == Zones.goal)
            {
                selection = Roll(5);

                switch (selection)
                {
                    case 0:
                        choice = "low stick";
                        break;
                    case 1:
                        choice = "low glove";
                        break;
                    case 2:
                        choice = "high stick";
                        break;
                    case 3:
                        choice = "high glove";
                        break;
                    case 4:
                        choice = "legs";
                        break;
                }

                if (home.HasPuck)
                {
                    Console.Write($"{playerWithPuck} shoots for ");
                    if (choice.Equals("legs"))
                    {
                        Console.Write("between the goalie's legs!");
                    }
                    else
                    {
                        Console.Write($"the {choice} side!");
                    }
                }
                else
                {
                    TextFormat.Away(playerWithPuck.Name);
                    Console.Write(" shoots for ");
                    if (choice.Equals("legs"))
                    {
                        Console.Write("between the goalie's legs!");
                    }
                    else
                    {
                        Console.Write($"the {choice} side!");
                    }
                }

                isShooting = true;
                return choice;
            }
            #endregion

            #region Teammate Advancing
            else
            {
                if (zone == Zones.offensive)
                {
                    if (playerWithPuck.OffAwareness >= 86)
                    {
                        selection = Roll(5);
                    }
                    else
                    {
                        selection = Roll(4);
                    }
                }
                else
                {
                    // 50% chance to make teammate pass back to the user
                    selection = Roll(6);
                }

                #region Base Selections
                switch (selection)
                {
                    case 0:
                        if (home.HasPuck)
                        {
                            Console.Write($"{playerWithPuck} attempts to skate past ");
                            TextFormat.Away(defendingPlayer.Name);
                        }
                        else
                        {
                            TextFormat.Away(playerWithPuck.Name);
                            Console.Write($" attempts to skate past {opponent}");
                        }
                        choice = "speed";
                        break;
                    case 1:
                        if (home.HasPuck)
                        {
                            Console.Write($"{playerWithPuck} attempts to power through ");
                            TextFormat.Away(defendingPlayer.Name);
                        }
                        else
                        {
                            TextFormat.Away(playerWithPuck.Name);
                            Console.Write($" attempts to power through {opponent}");
                        }
                        choice = "strength";
                        break;
                    case 2:
                        if (home.HasPuck)
                        {
                            Console.Write($"{playerWithPuck} attempts to deke around ");
                            TextFormat.Away(defendingPlayer.Name);
                        }
                        else
                        {
                            TextFormat.Away(playerWithPuck.Name);
                            Console.Write($" attempts to deke around {opponent}");
                        }
                        choice = "stickhandling";
                        break;
                }
                #endregion
                
                if (selection > 2 && zone == Zones.offensive)
                {
                    switch (selection)
                    {
                        case 3:
                            if (home.HasPuck)
                            {
                                Console.Write($"{playerWithPuck} shoots from the blue line!");
                            }
                            else
                            {
                                TextFormat.Away(playerWithPuck.Name);
                                Console.Write(" shoots from the blue line!");
                            }
                            choice = "shooting";
                            break;
                        default:
                            if (home.HasPuck)
                            {
                                Console.Write($"{playerWithPuck} sets up the one-timer");
                            }
                            else
                            {
                                TextFormat.Away(playerWithPuck.Name);
                                Console.Write(" sets up the one-timer");
                            }
                            choice = "one-timer";
                            break;
                    }
                }
                
                if (selection == 3 && zone == Zones.neutral)
                {
                    if (home.HasPuck)
                    {
                        // Making stretch pass
                    }
                    else
                    {

                    }
                    choice = "passing";
                }

            }
            #endregion

            return choice;
        }

        private static void ResetPuckPossession()
        {
            home.HasPuck = false;
            away.HasPuck = false;

            home.Center.HasPuck = false;
            home.Winger.HasPuck = false;
            home.Defenseman.HasPuck = false;

            away.Center.HasPuck = false;
            away.Winger.HasPuck = false;
            away.Defenseman.HasPuck = false;

            playerWithPuck = null;
            defendingPlayer = null;
            //goalie = null;
            zone = Zones.neutral;
            isShooting = false;
        }

        private static Player PickRandomPlayer(Team t)
        {
            int rp = Roll(3);

            switch (rp)
            {
                case 0:
                    return t.Center;
                case 1:
                    return t.Winger;
                case 2:
                    return t.Defenseman;
            }

            return null;
        }
        #endregion

        private enum Zones
        {
            defensive,
            neutral,
            offensive,
            goal
        }
    }
}