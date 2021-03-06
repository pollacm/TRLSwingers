﻿using TRLSwingers.Teams;

namespace TRLSwingers.Trophies
{
    public class BallerOfTheWeekTrophy : ITrophy
    {
        public BallerOfTheWeekTrophy(Team team, string additionalInfo)
        {
            Team = team;
            AdditionalInfo = additionalInfo;
            BuildTogether = true;
        }
        public Team Team { get; set; }
        public string AdditionalInfo { get; }
        public bool BuildTogether { get; }

        public string GetTrophyName()
        {
            return TrophyConstants.LargestMarginWeekWinner;
        }

        public string LeaguePageText()
        {
            return GetTrophyName() + @" OF THE WEEK";
        }

        public string GetTrophyBody()
        {
            return Team.TeamName.ToUpper();
        }

        public string GetHeadline(Team team)
        {
            return $"For winning by {AdditionalInfo} points!!!!!";
        }

        public string GetReason(Team team)
        {
            return string.Empty;
        }
    }
}
