using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public abstract class BasePlayer
    {
        #region PROPERTIES
        public Team AssignedTeam { get; set; }
        public string Name { get; set; }
        public byte Number { get; set; }
        public Positions Position { get; set; }
        #endregion

        #region CONSTRUCTORS
        public BasePlayer(Team assignedTeam, string name, byte number, Positions position)
        {
            AssignedTeam = assignedTeam;
            Name = name;
            Number = number;
            Position = position;
        }
        #endregion

        #region METHODS
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}