using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHL_Threes
{
    public class Team
    {
        #region FIELDS
        private Player _center;
        private Player _winger;
        private Player _defenseman;
        private Goalie _goalie;
        #endregion

        #region PROPERTIES
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<byte> RetiredNumbers { get; set; }
        public byte Score { get; set; }
        public bool HasPuck { get; set; }
        public bool IsHome { get; set; }
        public bool IsUserTeam { get; set; }

        public Player Center
        {
            get { return _center; }
            set
            {
                if (value.Position == Positions.center)
                {
                    _center = value;
                }
            }
        }

        public Player Winger
        {
            get { return _winger; }
            set
            {
                if (value.Position == Positions.winger)
                {
                    _winger = value;
                }
            }
        }

        public Player Defenseman
        {
            get { return _defenseman; }
            set
            {
                if (value.Position == Positions.defenseman)
                {
                    _defenseman = value;
                }
            }
        }

        public Goalie Goalie
        {
            get { return _goalie; }
            set
            {
                if (value.Position == Positions.goalie)
                {
                    _goalie = value;
                }
            }
        }
        #endregion

        #region CONSTRUCTORS
        public Team() { }
        
        public Team(string name, string abbreviation, List<byte> retiredNumbers)
        {
            Name = name;
            Abbreviation = abbreviation;
            RetiredNumbers = retiredNumbers;
            Score = 0;
            HasPuck = false;
            IsHome = false;
            IsUserTeam = false;
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