using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class TeamRenders
    {
        public static Team RenderCustomTeam(Team t, List<Player> customRoster)
        {
            Team customRosterTeam = RenderStockTeam(t.Abbreviation);

            foreach (Player p in customRoster)
            {
                if (customRosterTeam.Name.Equals(p.AssignedTeam.Name))
                {
                    if (p.Position == Positions.center)
                    {
                        customRosterTeam.Center = (Player)p;
                    }
                    else if (p.Position == Positions.winger)
                    {
                        customRosterTeam.Winger = (Player)p;
                    }
                    else if (p.Position == Positions.defenseman)
                    {
                        customRosterTeam.Defenseman = (Player)p;
                    }
                }
            }

            return customRosterTeam;
        }

        public static Team RenderStockTeam(string abbv)
        {
            Team t = new Team();
            abbv = abbv.ToUpper();

            switch (abbv)
            {
                #region ANA
                case "ANA":
                    t = new Team("Anaheim Ducks", "ANA",
                        new List<Byte>() { 8 });
                    t.Center = new Player(t, "Ryan Getzlaf", Positions.center, 15,
                        83, 90, 90, 90, 87, 91, 87);
                    t.Winger = new Player(t, "Corey Perry", Positions.winger, 10,
                        83, 90, 88, 87, 91, 89, 86);
                    t.Defenseman = new Player(t, "Cam Fowler", Positions.defenseman, 42,
                        89, 87, 89, 89, 86, 90, 87);
                    t.Goalie = new Goalie(t, "John Gibson", 36,
                        86, 89, 86, 89, 88);
                    break;
                #endregion

                #region ARI
                case "ARI":
                    t = new Team("Arizona Coyotes", "ARI",
                        new List<Byte>() { 7, 9, 10, 25, 27, 97 });
                    t.Center = new Player(t, "Derek Stepan", Positions.center, 9,
                        81, 79, 81, 81, 80, 80, 81);
                    t.Winger = new Player(t, "Clayton Keller", Positions.winger, 16,
                        81, 75, 83, 82, 81, 82, 81);
                    t.Defenseman = new Player(t, "Oliver Ekman-Larsson", Positions.defenseman, 23,
                        81, 76, 82, 84, 78, 83, 81);
                    t.Goalie = new Goalie(t, "Antti Raanta", 32,
                        79, 81, 80, 82, 80);
                    break;
                #endregion

                #region BOS
                case "BOS":
                    t = new Team("Boston Bruins", "BOS",
                        new List<Byte>() { 2, 3, 4, 5, 7, 8, 9, 15, 24, 77 });
                    t.Center = new Player(t, "Patrice Bergeron", Positions.center, 37,
                        87, 86, 93, 90, 87, 91, 93);
                    t.Winger = new Player(t, "Brad Marchand", Positions.winger, 63,
                        89, 88, 90, 91, 91, 93, 89);
                    t.Defenseman = new Player(t, "Zdeno Chara", Positions.defenseman, 33,
                        86, 98, 91, 87, 87, 87, 91);
                    t.Goalie = new Goalie(t, "Tuukka Rask", 40,
                        90, 90, 90, 91, 90);
                    break;
                #endregion

                #region BUF
                case "BUF":
                    t = new Team("Buffalo Sabres", "BUF",
                        new List<Byte>() { 2, 7, 11, 14, 16, 18, 39 });
                    t.Center = new Player(t, "Jack Eichel", Positions.center, 15,
                        81, 76, 80, 81, 80, 81, 79);
                    t.Winger = new Player(t, "Kyle Okposo", Positions.winger, 21,
                        80, 80, 80, 81, 80, 80, 77);
                    t.Defenseman = new Player(t, "Rasmus Ristolainen", Positions.defenseman, 55,
                        78, 82, 81, 80, 77, 79, 81);
                    t.Goalie = new Goalie(t, "Robin Lehner", 40,
                        79, 80, 79, 81, 80);
                    break;
                #endregion

                #region CGY
                case "CGY":
                    t = new Team("Calgary Flames", "CGY",
                        new List<Byte>() { 9, 30 });
                    t.Center = new Player(t, "Sean Monahan", Positions.center, 23,
                        82, 84, 85, 84, 85, 85, 84);
                    t.Winger = new Player(t, "Johnny Gaudreau", Positions.winger, 13,
                        86, 75, 86, 87, 83, 87, 81);
                    t.Defenseman = new Player(t, "Mark Giordano", Positions.defenseman, 5,
                        83, 83, 85, 84, 80, 84, 87);
                    t.Goalie = new Goalie(t, "Mike Smith", 41,
                        84, 84, 83, 85, 84);
                    break;
                #endregion

                #region CAR
                case "CAR":
                    t = new Team("Carolina Hurricanes", "CAR",
                        new List<Byte>() { 2, 10, 17 });
                    t.Center = new Player(t, "Jordan Staal", Positions.center, 11,
                        82, 87, 85, 83, 83, 83, 86);
                    t.Winger = new Player(t, "Sebastian Aho", Positions.winger, 20,
                        84, 80, 84, 84, 84, 85, 83);
                    t.Defenseman = new Player(t, "Justin Faulk", Positions.defenseman, 5,
                        83, 82, 87, 86, 82, 86, 83);
                    t.Goalie = new Goalie(t, "Cam Ward", 30,
                        82, 84, 82, 84, 84);
                    break;
                #endregion

                #region CHI
                case "CHI":
                    t = new Team("Chicago Blackhawks", "CHI",
                        new List<Byte>() { 1, 3, 9, 18, 21, 35 });
                    t.Center = new Player(t, "Jonathan Toews", Positions.center, 19,
                        82, 80, 84, 83, 82, 78, 85);
                    t.Winger = new Player(t, "Patrick Kane", Positions.winger, 88,
                        81, 72, 84, 87, 83, 87, 80);
                    t.Defenseman = new Player(t, "Duncan Keith", Positions.defenseman, 2,
                        83, 77, 86, 85, 79, 82, 85);
                    t.Goalie = new Goalie(t, "Corey Crawford", 50,
                        81, 83, 81, 84, 83);
                    break;
                #endregion

                #region COL
                case "COL":
                    t = new Team("Colorado Avalanche", "COL",
                        new List<Byte>() { 19, 21, 23, 33, 52, 77 });
                    t.Center = new Player(t, "Nathan MacKinnon", Positions.center, 29,
                        88, 81, 85, 84, 84, 87, 85);
                    t.Winger = new Player(t, "Gabriel Landeskog", Positions.winger, 92,
                        84, 86, 85, 85, 85, 84, 85);
                    t.Defenseman = new Player(t, "Tyson Barrie", Positions.defenseman, 4,
                        85, 81, 86, 87, 82, 87, 85);
                    t.Goalie = new Goalie(t, "Semyon Varlamov", 1,
                        84, 86, 84, 87, 85);
                    break;
                #endregion

                #region CBJ
                case "CBJ":
                    t = new Team("Columbus Blue Jackets", "CBJ",
                        new List<Byte>() { });
                    t.Center = new Player(t, "Pierre-Luc Dubois", Positions.center, 18,
                        87, 85, 86, 84, 87, 88, 87);
                    t.Winger = new Player(t, "Artemi Panarin", Positions.winger, 71,
                        88, 79, 87, 89, 87, 89, 82);
                    t.Defenseman = new Player(t, "Seth Jones", Positions.defenseman, 3,
                        85, 86, 87, 87, 84, 89, 89);
                    t.Goalie = new Goalie(t, "Sergei Bobrovsky", 72,
                        85, 88, 85, 87, 87);
                    break;
                #endregion

                #region DAL
                case "DAL":
                    t = new Team("Dallas Stars", "DAL",
                        new List<Byte>() { 7, 8, 9, 19, 26 });
                    t.Center = new Player(t, "Tyler Seguin", Positions.center, 91,
                        85, 80, 84, 88, 86, 87, 80);
                    t.Winger = new Player(t, "Jamie Benn", Positions.winger, 14,
                        83, 84, 85, 86, 86, 86, 83);
                    t.Defenseman = new Player(t, "John Klingberg", Positions.defenseman, 3,
                        84, 81, 86, 87, 81, 88, 87);
                    t.Goalie = new Goalie(t, "Ben Bishop", 30,
                        84, 85, 84, 86, 84);
                    break;
                #endregion

                #region DET
                case "DET":
                    t = new Team("Detroit Red Wings", "DET",
                        new List<Byte>() { 1, 5, 7, 9, 10, 12, 19 });
                    t.Center = new Player(t, "Henrik Zetterberg", Positions.center, 40,
                        81, 81, 82, 82, 82, 81, 81);
                    t.Winger = new Player(t, "Gustav Nyquist", Positions.winger, 14,
                        85, 78, 81, 83, 82, 83, 80);
                    t.Defenseman = new Player(t, "Trevor Daley", Positions.defenseman, 25,
                        83, 82, 82, 81, 79, 80, 81);
                    t.Goalie = new Goalie(t, "Jimmy Howard", 35,
                        81, 83, 79, 82, 80);
                    break;
                #endregion

                #region EDM
                case "EDM":
                    t = new Team("Edmonton Oilers", "EDM",
                        new List<Byte>() { 3, 7, 9, 11, 17, 31 });
                    t.Center = new Player(t, "Connor McDavid", Positions.center, 97,
                        86, 76, 83, 85, 81, 87, 80);
                    t.Winger = new Player(t, "Milan Lucic", Positions.winger, 27,
                        82, 91, 81, 81, 84, 81, 81);
                    t.Defenseman = new Player(t, "Darnell Nurse", Positions.defenseman, 25,
                        83, 86, 84, 82, 81, 83, 84);
                    t.Goalie = new Goalie(t, "Cam Talbot", 33,
                        82, 84, 82, 84, 85);
                    break;
                #endregion

                #region FLA
                case "FLA":
                    t = new Team("Florida Panthers", "FLA",
                        new List<Byte>() { 37, 93 });
                    t.Center = new Player(t, "Aleksander Barkov", Positions.center, 17,
                        81, 85, 86, 88, 86, 88, 85);
                    t.Winger = new Player(t, "Jonathan Huberdeau", Positions.winger, 11,
                        86, 82, 86, 87, 85, 88, 83);
                    t.Defenseman = new Player(t, "Aaron Ekblad", Positions.defenseman, 3,
                        85, 85, 87, 86, 83, 86, 87);
                    t.Goalie = new Goalie(t, "Roberto Luongo", 1,
                        84, 86, 84, 87, 87);
                    break;
                #endregion

                #region LAK
                case "LAK":
                    t = new Team("Los Angeles Kings", "LAK",
                        new List<Byte>() { 4, 16, 18, 20, 30 });
                    t.Center = new Player(t, "Anze Kopitar", Positions.center, 11,
                        83, 87, 87, 88, 86, 89, 87);
                    t.Winger = new Player(t, "Dustin Brown", Positions.winger, 23,
                        86, 90, 88, 87, 88, 85, 87);
                    t.Defenseman = new Player(t, "Drew Doughty", Positions.defenseman, 8,
                        87, 83, 90, 80, 84, 91, 91);
                    t.Goalie = new Goalie(t, "Jonathan Quick", 32,
                        87, 87, 85, 88, 87);
                    break;
                #endregion

                #region MIN
                case "MIN":
                    t = new Team("Minnesota Wild", "MIN",
                        new List<Byte>() { 1 });
                    t.Center = new Player(t, "Mikko Koivu", Positions.center, 9,
                        87, 90, 89, 89, 87, 88, 89);
                    t.Winger = new Player(t, "Zach Parise", Positions.winger, 64,
                        91, 86, 90, 89, 88, 90, 88);
                    t.Defenseman = new Player(t, "Ryan Suter", Positions.defenseman, 20,
                        87, 87, 90, 89, 84, 90, 92);
                    t.Goalie = new Goalie(t, "Devan Dubnyk", 40,
                        86, 89, 86, 89, 90);
                    break;
                #endregion

                #region MTL
                case "MTL":
                    t = new Team("Montreal Canadiens", "MTL",
                        new List<Byte>() { 1, 2, 3, 4, 5, 7, 9, 10, 12, 16, 18, 19, 23, 29, 33 });
                    t.Center = new Player(t, "Jonathan Drouin", Positions.center, 92,
                        85, 76, 81, 82, 82, 83, 78);
                    t.Winger = new Player(t, "Max Pacioretty", Positions.winger, 67,
                        82, 81, 81, 82, 83, 82, 79);
                    t.Defenseman = new Player(t, "Shea Weber", Positions.defenseman, 26,
                        78, 86, 81, 79, 80, 82, 85);
                    t.Goalie = new Goalie(t, "Carey Price", 31,
                        78, 81, 81, 84, 84);
                    break;
                #endregion

                #region NSH
                case "NSH":
                    t = new Team("Nashville Predators", "NSH",
                        new List<Byte>() { });
                    t.Center = new Player(t, "Ryan Johansen", Positions.center, 92,
                        91, 93, 90, 93, 92, 91, 86);
                    t.Winger = new Player(t, "Filip Forsberg", Positions.winger, 9,
                        91, 91, 93, 92, 92, 92, 89);
                    t.Defenseman = new Player(t, "Roman Josi", Positions.defenseman, 59,
                        90, 90, 93, 92, 89, 93, 93);
                    t.Goalie = new Goalie(t, "Pekka Rinne", 35,
                        91, 91, 90, 93, 92);
                    break;
                #endregion

                #region NJD
                case "NJD":
                    t = new Team("New Jersey Devils", "NJD",
                        new List<Byte>() { 3, 4, 26, 27, 30 });
                    t.Center = new Player(t, "Nico Hischier", Positions.center, 13,
                        88, 82, 85, 84, 88, 87, 85);
                    t.Winger = new Player(t, "Taylor Hall", Positions.winger, 9,
                        91, 82, 86, 86, 87, 88, 84);
                    t.Defenseman = new Player(t, "Sami Vatanen", Positions.defenseman, 6,
                        89, 83, 87, 87, 85, 87, 85);
                    t.Goalie = new Goalie(t, "Cory Schneider", 35,
                        84, 86, 84, 88, 88);
                    break;
                #endregion

                #region NYI
                case "NYI":
                    t = new Team("New York Islanders", "NYI",
                        new List<Byte>() { 5, 9, 19, 22, 23, 31 });
                    t.Center = new Player(t, "John Tavares", Positions.center, 91,
                        78, 81, 85, 87, 86, 87, 82);
                    t.Winger = new Player(t, "Joshua Bailey", Positions.winger, 12,
                        83, 83, 84, 84, 82, 85, 83);
                    t.Defenseman = new Player(t, "Nick Leddy", Positions.defenseman, 2,
                        85, 80, 85, 85, 80, 86, 84);
                    t.Goalie = new Goalie(t, "Jaroslav Halak", 41,
                        83, 84, 82, 84, 84);
                    break;
                #endregion

                #region NYR
                case "NYR":
                    t = new Team("New York Rangers", "NYR",
                new List<Byte>() { 1, 2, 3, 7, 9, 11, 19, 35 });
                    t.Center = new Player(t, "Mika Zibanejad", Positions.center, 93,
                        84, 83, 83, 82, 82, 83, 81);
                    t.Winger = new Player(t, "Mats Zuccarello", Positions.winger, 36,
                        87, 75, 83, 83, 83, 83, 81);
                    t.Defenseman = new Player(t, "Kevin Shattenkirk", Positions.defenseman, 27,
                        83, 79, 83, 86, 78, 86, 82);
                    t.Goalie = new Goalie(t, "Henrik Lundqvist", 30,
                        81, 84, 81, 85, 84);
                    break;
                #endregion

                #region OTT
                case "OTT":
                    t = new Team("Ottawa Senators", "OTT",
                        new List<Byte>() { 8, 11 });
                    t.Center = new Player(t, "Matt Duchene", Positions.center, 95,
                        87, 78, 80, 81, 81, 80, 77);
                    t.Winger = new Player(t, "Mark Stone", Positions.winger, 61,
                        77, 81, 81, 81, 83, 82, 81);
                    t.Defenseman = new Player(t, "Erik Karlsson", Positions.defenseman, 65,
                        81, 74, 82, 84, 78, 83, 81);
                    t.Goalie = new Goalie(t, "Craig Anderson", 41,
                        81, 81, 80, 81, 81);
                    break;
                #endregion

                #region PHI
                case "PHI":
                    t = new Team("Philadelphia Flyers", "PHI",
                        new List<Byte>() { 1, 2, 4, 7, 16, 88 });
                    t.Center = new Player(t, "Claude Giroux", Positions.center, 28,
                        88, 81, 88, 91, 86, 90, 85);
                    t.Winger = new Player(t, "Jakub Voracek", Positions.winger, 93,
                        85, 84, 86, 88, 88, 88, 84);
                    t.Defenseman = new Player(t, "Shayne Gostisbehere", Positions.defenseman, 53,
                        86, 83, 88, 87, 86, 89, 88);
                    t.Goalie = new Goalie(t, "Brian Elliott", 37,
                        85, 87, 86, 88, 86);
                    break;
                #endregion

                #region PIT
                case "PIT":
                    t = new Team("Pittsburgh Penguins", "PIT",
                        new List<Byte>() { 21, 66 });
                    t.Center = new Player(t, "Sidney Crosby", Positions.center, 87,
                        86, 85, 89, 90, 87, 91, 86);
                    t.Winger = new Player(t, "Phil Kessel", Positions.winger, 81,
                        91, 83, 87, 89, 90, 91, 86);
                    t.Defenseman = new Player(t, "Kris Letang", Positions.defenseman, 58,
                        88, 84, 89, 89, 84, 89, 91);
                    t.Goalie = new Goalie(t, "Matt Murray", 30,
                        88, 88, 88, 88, 88);
                    break;
                #endregion

                #region STL
                case "STL":
                    t = new Team("Saint Louis Blues", "STL",
                        new List<Byte>() { 2, 3, 5, 8, 11, 16, 24 });
                    t.Center = new Player(t, "Brayden Schenn", Positions.center, 17,
                        85, 87, 88, 88, 88, 87, 86);
                    t.Winger = new Player(t, "Vladimir Tarasenko", Positions.winger, 91,
                        88, 83, 87, 89, 88, 89, 84);
                    t.Defenseman = new Player(t, "Alex Pietrangelo", Positions.defenseman, 27,
                        85, 86, 89, 89, 84, 90, 91);
                    t.Goalie = new Goalie(t, "Jake Allen", 34,
                        87, 88, 86, 88, 88);
                    break;
                #endregion

                #region SJS
                case "SJS":
                    t = new Team("San Jose Sharks", "SJS",
                        new List<Byte>() { });
                    t.Center = new Player(t, "Joe Thornton", Positions.center, 8,
                        80, 87, 86, 87, 83, 86, 84);
                    t.Winger = new Player(t, "Joe Pavelski", Positions.winger, 62,
                        86, 80, 85, 87, 85, 85, 84);
                    t.Defenseman = new Player(t, "Brent Burns", Positions.defenseman, 88,
                        81, 86, 84, 85, 84, 85, 86);
                    t.Goalie = new Goalie(t, "Martin Jones", 31,
                        85, 85, 84, 86, 85);
                    break;
                #endregion

                #region TBL
                case "TBL":
                    t = new Team("Tampa Bay Lightning", "TBL",
                        new List<Byte>() { 4, 26 });
                    t.Center = new Player(t, "Steven Stamkos", Positions.center, 91,
                        91, 85, 89, 92, 91, 93, 88);
                    t.Winger = new Player(t, "Nikita Kucherov", Positions.winger, 86,
                        91, 84, 90, 93, 90, 94, 91);
                    t.Defenseman = new Player(t, "Victor Hedman", Positions.defenseman, 77,
                        86, 93, 92, 90, 86, 91, 93);
                    t.Goalie = new Goalie(t, "Andrei Vasilevskiy", 88,
                        91, 91, 89, 91, 90);
                    break;
                #endregion

                #region TOR
                case "TOR":
                    t = new Team("Toronto Maple Leafs", "TOR",
                        new List<Byte>() { 1, 4, 5, 6, 7, 9,
                        10, 13, 14, 17, 21, 27, 93});
                    t.Center = new Player(t, "Auston Matthews", Positions.center, 34,
                        88, 87, 89, 89, 90, 92, 88);
                    t.Winger = new Player(t, "William Nylander", Positions.winger, 25,
                        92, 84, 90, 92, 90, 91, 86);
                    t.Defenseman = new Player(t, "Morgan Rielly", Positions.defenseman, 44,
                        91, 87, 90, 90, 86, 90, 90);
                    t.Goalie = new Goalie(t, "Frederik Andersen", 31,
                        88, 89, 88, 90, 90);
                    break;
                #endregion

                #region VAN
                case "VAN":
                    t = new Team("Vancouver Canucks", "VAN",
                        new List<Byte>() { 10, 12, 16, 19 });
                    t.Center = new Player(t, "Henrik Sedin", Positions.center, 33,
                        79, 82, 82, 82, 83, 81, 81);
                    t.Winger = new Player(t, "Daniel Sedin", Positions.winger, 22,
                        80, 83, 83, 83, 82, 82, 82);
                    t.Defenseman = new Player(t, "Alexander Edler", Positions.defenseman, 23,
                        81, 84, 82, 81, 79, 80, 83);
                    t.Goalie = new Goalie(t, "Jacob Markstrom", 25,
                        79, 82, 81, 83, 82);
                    break;
                #endregion

                #region VGK
                case "VGK":
                    t = new Team("Vegas Golden Knights", "VGK",
                        new List<Byte>() { });
                    t.Center = new Player(t, "William Karlsson", Positions.center, 71,
                        90, 86, 89, 90, 90, 92, 89);
                    t.Winger = new Player(t, "Jonathan Marchessault", Positions.winger, 57,
                        89, 85, 91, 91, 91, 92, 90);
                    t.Defenseman = new Player(t, "Nate Schmidt", Positions.defenseman, 6,
                        91, 89, 89, 88, 87, 89, 92);
                    t.Goalie = new Goalie(t, "Marc-Andre Fleury", 29,
                        89, 90, 88, 90, 89);
                    break;
                #endregion

                #region WSH
                case "WSH":
                    t = new Team("Washington Capitals", "WSH",
                        new List<Byte>() { 5, 7, 11, 32 });
                    t.Center = new Player(t, "Nicklas Backstrom", Positions.center, 92,
                        89, 83, 91, 93, 85, 92, 88);
                    t.Winger = new Player(t, "Alex Ovechkin", Positions.winger, 8,
                        87, 91, 89, 88, 92, 93, 83);
                    t.Defenseman = new Player(t, "John Carlson", Positions.defenseman, 74,
                        87, 89, 91, 90, 86, 91, 90);
                    t.Goalie = new Goalie(t, "Braden Holtby", 70,
                        87, 89, 88, 90, 90);
                    break;
                #endregion

                #region WPG
                case "WPG":
                    t = new Team("Winnipeg Jets", "WPG",
                        new List<Byte>() { 9, 10, 14, 15 });
                    t.Center = new Player(t, "Mark Scheifele", Positions.center, 55,
                        89, 87, 90, 93, 90, 93, 89);
                    t.Winger = new Player(t, "Blake Wheeler", Positions.winger, 26,
                        88, 92, 91, 91, 91, 93, 91);
                    t.Defenseman = new Player(t, "Jacob Trouba", Positions.defenseman, 33,
                        89, 93, 92, 90, 88, 90, 92);
                    t.Goalie = new Goalie(t, "Connor Hellebuyck", 37,
                        91, 91, 90, 90, 91);
                    break;
                    #endregion
            }

            return t;
        }
    }
}