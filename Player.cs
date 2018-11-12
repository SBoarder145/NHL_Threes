using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class Player : BasePlayer
    {
        #region PROPERTIES
        public byte Speed { get; set; }
        public byte Strength { get; set; }
        public byte StickHandling { get; set; }
        public byte Passing { get; set; }
        public byte Shooting { get; set; }
        public byte OffAwareness { get; set; }
        public byte DefAwareness { get; set; }
        public bool HasPuck { get; set; }
        public bool IsCustom { get; set; }
        #endregion

        #region CONSTRUCTORS
        // Custom player ctor
        public Player(Team assignedTeam, string name, Positions position, byte number)
            : base(assignedTeam, name, number, position)
        {
            HasPuck = false;
            IsCustom = true;
        }

        // Stock player ctor
        public Player(Team assignedTeam, string name, Positions position, byte number,
             byte speed, byte strength, byte stickHandling, byte passing,
              byte shooting, byte offAwareness, byte defAwareness)
            : base(assignedTeam, name, number, position)
        {
            Speed = speed;
            Strength = strength;
            StickHandling = stickHandling;
            Passing = passing;
            Shooting = shooting;
            OffAwareness = offAwareness;
            DefAwareness = defAwareness;
            HasPuck = false;
            IsCustom = false;
        }
        #endregion
    }
}