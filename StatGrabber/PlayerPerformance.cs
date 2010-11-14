using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatGrabber
{
    public class PlayerPerformance
    {
        public string FirstName = "";
        public string LastName = "";
        public int Minutes = 0;
        public string TeamName = "";
        public int ShotAttempts = 0;
        public int ShotsMade = 0;
        public int FTAttempts = 0;
        public int FTsMade = 0;
        public int ThreeAttempts = 0;
        public int ThreesMade = 0;
        public int OffensiveRebounds = 0;
        public int DefensiveRebounds = 0;
        public int Assists;
        public int Steals = 0;
        public int Blocks = 0;
        public int Fouls = 0;
        public int Turnovers = 0;

        public int Offense
        {
            get
            {
                return ShotsMade * 2 + FTsMade + ThreesMade + 2 * OffensiveRebounds + Assists - Fouls;
            }
        }
        public int Defense
        {
            get
            {
                return 2 * Blocks + 2 * Steals + DefensiveRebounds - Turnovers;
            }
        }

    };
}
