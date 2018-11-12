using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class Goalie : BasePlayer
    {
        #region PROPERTIES
        public byte GloveHigh { get; set; }
        public byte GloveLow { get; set; }
        public byte StickHigh { get; set; }
        public byte StickLow { get; set; }
        public byte BetweenLegs { get; set; }
        public int Average { get; set; }
        #endregion

        #region CONSTRUCTORS
        public Goalie(Team assignedTeam, string name, byte number, byte gloveHigh,
            byte gloveLow, byte stickHigh, byte stickLow, byte betweenLegs)
            : base(assignedTeam, name, number, Positions.goalie)
        {
            StickLow = stickLow;
            GloveLow = gloveLow;
            StickHigh = stickHigh;
            GloveHigh = gloveHigh;
            BetweenLegs = betweenLegs;
            Average = (gloveHigh + gloveLow + stickHigh + stickLow + betweenLegs) / 5;
        }
        #endregion
    }
}